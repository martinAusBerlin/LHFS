using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;
using LHFS.Views.Users.ViewModel;
using LHFS.Helpers;
using Omu.ValueInjecter;
using LHFS.Models.Security;
using LHFS.Utils;
using System.Data.Objects;
using System.Configuration;

namespace LHFS.Controllers
{   
    public class UsersController : Controller
    {
		private readonly ICountryRepository countryRepository;
		private readonly IRankRepository rankRepository;
		private readonly IAirportRepository airportRepository;
		private readonly IUserRepository userRepository;
		private readonly ITypeRatingRepository typeRatingRepository;
		private readonly IUserTypeRatingRepository userTypeRatingRepository;
		private readonly IFlightRepository flightRepository;
		private readonly IAirportDepartureRepository airportDepartureRepository;
		private readonly IAirportArrivalRepository airportArrivalRepository;
		private readonly IAirportDetailRepository airportDetailRepository;
		private readonly IAirportTerrainRepository airportTerrainRepository;
		private readonly IAirportGroundOpRepository airportGroundOpRepository;
		private readonly IAirportHazardRepository airportHazardRepository;
		

		private bool ValidateNumberOfTyperatingsInternal(int selectedAicraftTypes, User userToCheck) {
			return selectedAicraftTypes <= userToCheck.Rank.NumberOfTyperatings && selectedAicraftTypes > 0;
		}

		// If you are using Dependency Injection, you can delete the following constructor
        public UsersController() : this(new CountryRepository(), new RankRepository(), 
			new AirportRepository(), new UserRepository(), new TypeRatingRepository(), 
			new FlightRepository(), new UserTypeRatingRepository(), 
			new AirportDepartureRepository(), 
			new AirportArrivalRepository(), 
			new AirportDetailRepository(), 
			new AirportTerrainRepository(), 
			new AirportGroundOpRepository(), 
			new AirportHazardRepository()			
			)
        {
        }

		public UsersController(ICountryRepository countryRepository, IRankRepository rankRepository, IAirportRepository airportRepository,
			IUserRepository userRepository, ITypeRatingRepository typeRatingRepository, IFlightRepository flightRepository, IUserTypeRatingRepository userTypeRatingRepository,
			IAirportDepartureRepository airportDepartureRepository, IAirportArrivalRepository airportArrivalRepository, IAirportDetailRepository airportDetailRepository,
			IAirportTerrainRepository airportTerrainRepository, IAirportGroundOpRepository airportGroundOpRepository, IAirportHazardRepository airportHazardRepository			
			)
        {
			this.countryRepository = countryRepository;
			this.rankRepository = rankRepository;
			this.airportRepository = airportRepository;
			this.userRepository = userRepository;
			this.typeRatingRepository = typeRatingRepository;
			this.flightRepository = flightRepository;
			this.userTypeRatingRepository = userTypeRatingRepository;
			this.airportDepartureRepository = airportDepartureRepository;
			this.airportArrivalRepository = airportArrivalRepository;
			this.airportDetailRepository = airportDetailRepository;
			this.airportTerrainRepository = airportTerrainRepository;
			this.airportGroundOpRepository = airportGroundOpRepository;
			this.airportHazardRepository = airportHazardRepository;

        }

		private IEnumerable<UserRepository.UserContributions> _contributions;
		public IEnumerable<UserRepository.UserContributions> Contributions { 
			get {
				if(_contributions == null) {
					_contributions = userRepository.GetUserContributions().ToList();
				}

				return _contributions;
			}
		}



		private int GetNumberOfContributions(int userID) {

			var singleUser = Contributions.Single(t => t.UserID == userID);

			int number = 0;
			number += singleUser.NumberOfDepartures;
			number += singleUser.NumberOfArrivals;
			number += singleUser.NumberOfAirportDetails;
			number += singleUser.NumberOfAirportTerrains;
			number += singleUser.NumberOfAirportHazards;
			number += singleUser.NumberOfGroundOps;

			return number;
		}

		private long GetActivityIndex(int userID, int numberOfFlights) {
			return GetNumberOfContributions(userID) * 2 + numberOfFlights + userID * 100;
		}

		[HttpPost]
		public void UploadPhoto(HttpPostedFileBase FileData) {
			string path = UserContext.GetCurrent().ID + @"\" + "Photo" + @"\";
			FileHelper.WriteFile(HttpContext.Server.MapPath("~") + ConfigurationManager.AppSettings["UserContentBasePath"] + @"\" + path, "me.jpg", FileData.InputStream);
		}

		public ViewResult Roster() {

			var sourceData = userRepository.AllIncluding(user => user.Country, user => user.Rank, user => user.UserTypeRatings, user => user.Flights)
				.Where(t => !t.MembershipEndedOn.HasValue && t.ApprovedOn.HasValue)
				.Select(t => new UserTableModel() {
					Country = t.Country,
					CountryISO = t.CountryISO,
					FlightTimeTicks = t.Flights.Where(h => h.ApprovedOn.HasValue && h.FlightTimeTicks.HasValue).Sum(h => h.FlightTimeTicks.Value),
					Surname = t.Surname,
					Forename = t.Forename,
					HomeBase = t.HomeBase,
					ID = t.ID,
					IvaoID = t.IvaoID,
					LastFlightOn = t.Flights.Any(g => g.ApprovedOn.HasValue) ? t.Flights.Where(g => g.ApprovedOn.HasValue).OrderBy(g => g.PerformedOn).FirstOrDefault().PerformedOn : null,
					MemberSince = t.ApprovedOn.Value,
					NumberOfFlights = t.Flights.Where(h => h.PerformedOn.HasValue && h.ApprovedOn.HasValue && h.FlightTimeTicks.HasValue).Count(),
					Rank = t.Rank,
					UserTypeRatings = t.UserTypeRatings,
					VatsimID = t.VatsimID,
					OffDutySince = t.OffDutySince
				}).ToList();

			var maxActivityIndex = 1000;
				
				//sourceData.Max(t => GetActivityIndex(t.ID, t.NumberOfFlights));

			foreach (var t in sourceData) {
				t.ActivityIndex = GetActivityIndex(t.ID, t.NumberOfFlights);
				t.Color =
					(127 + (long) Math.Floor((decimal)GetActivityIndex(t.ID, t.NumberOfFlights) * 128 / (decimal)maxActivityIndex)).ToString("X") +
					(127 + (long) Math.Floor((decimal)GetActivityIndex(t.ID, t.NumberOfFlights) * 128 / (decimal)maxActivityIndex)).ToString("X") +
					(127 + (long) Math.Floor((decimal)GetActivityIndex(t.ID, t.NumberOfFlights) * 128 / (decimal)maxActivityIndex)).ToString("X");
				t.TextColor =
					(255 - (long)Math.Floor((decimal)GetActivityIndex(t.ID, t.NumberOfFlights) * 128 / (decimal)maxActivityIndex)).ToString("X") +
					(255 - (long)Math.Floor((decimal)GetActivityIndex(t.ID, t.NumberOfFlights) * 128 / (decimal)maxActivityIndex)).ToString("X") +
					(255 - (long)Math.Floor((decimal)GetActivityIndex(t.ID, t.NumberOfFlights) * 128 / (decimal)maxActivityIndex)).ToString("X");
				t.Status = LHFS.Models.User.GetUserStatus(t.OffDutySince, t.LastFlightOn);
				t.Fullname = LHFS.Models.User.GetFullName(t.Forename, t.Surname);
				t.NumberOfContributions = GetNumberOfContributions(t.ID);
			}

			var outputData = sourceData.OrderByDescending(t => t.ActivityIndex);

			return View(outputData);
		}

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(userRepository.AllIncluding(user => user.Country, user => user.Rank, user => user.UserTypeRatings, user => user.Flights, user => user.Routes, user => user.GalleryImages));
        }

		public PartialViewResult Vacation() {

			return PartialView(userRepository.Find(UserContext.GetCurrent().ID));
		}

		public PartialViewResult StartVacation() {

			User currentUser = userRepository.Find(UserContext.GetCurrent().ID);

			if (!currentUser.LastVacationPeriodStartsOn.HasValue || currentUser.LastVacationPeriodStartsOn.Value.Year < DateTime.UtcNow.Year) {
				currentUser.LastVacationPeriodStartsOn = DateTime.UtcNow;

				userRepository.InsertOrUpdate(currentUser);
				userRepository.Save();
			} else {
				throw new NotSupportedException("You had your vacation already.");
			}

			return PartialView("Vacation", currentUser);
		}

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
			ViewBag.PhotoLink = "/ImageHandler.ashx?file=" + id + "/Photo/me.jpg&q=90&h=120&w=100&usercontent=1";
			ViewBag.HasPhoto = FileHelper.FileExists(HttpContext.Server.MapPath("~") + ConfigurationManager.AppSettings["UserContentBasePath"] + @"\" + id + @"\" + "Photo" + @"\", "me.jpg");

            return View(userRepository.Find(id));
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
			ViewBag.PossibleCountry = countryRepository.All;
			ViewBag.PossibleRank = rankRepository.All;
			ViewBag.PossibleAirport = airportRepository.All;
            return View();
        } 

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleCountry = countryRepository.All;
				ViewBag.PossibleRank = rankRepository.All;
				ViewBag.PossibleAirport = airportRepository.All;
				return View();
			}
        }


		public ActionResult UserEdit() {

			int id = UserContext.GetCurrent().ID;

			UserEditModel viewModel =  new UserEditModel();
			User user = userRepository.Find(id);
			viewModel.InjectFrom(user);

			SetupViewModel(viewModel);
			viewModel.TypeRatingIDs = viewModel.PossibleTypeRatings.Where(t => t.UserTypeRatingID != null).Select(t => t.TypeRatingID.ToString()).ToArray();

			return View(viewModel);
			
		}

		private UserEditModel SetupViewModel(UserEditModel viewModel) {

			viewModel.PossibleCountries = countryRepository.All.Select(t => new SelectListItem() { Text = t.English, Value = t.ISO }).ToList();

			viewModel.PossibleTypeRatings = userTypeRatingRepository.CurrentUserTypeRatings(UserContext.GetCurrent().ID).ToList();
			viewModel.PossibleHomeBases = airportRepository.All.Select(t => new SelectListItem() { Text = t.Name + " (" + t.ICAO + ")", Value = t.ICAO }).ToList();

			ViewBag.PhotoLink = "/ImageHandler.ashx?file=" + UserContext.GetCurrent().ID + "/Photo/me.jpg&q=90&h=120&w=100&usercontent=1";
			ViewBag.HasPhoto = FileHelper.FileExists(HttpContext.Server.MapPath("~") + ConfigurationManager.AppSettings["UserContentBasePath"] + @"\" + UserContext.GetCurrent().ID + @"\" + "Photo" + @"\", "me.jpg");


			return viewModel;
		}

		[HttpPost]
		public ActionResult UserEdit(UserEditModel viewModel) {

			User user = userRepository.Find(UserContext.GetCurrent().ID);
			user.InjectFrom<NotNullValueInjection>(viewModel);
			viewModel.Rank = user.Rank;

			if (!ValidateNumberOfTyperatingsInternal(viewModel.TypeRatingIDs.Count(), user)) {
				
				// TODO loc
				ModelState.AddModelError("TypeRatingIDs", "No way");
			}

			HashSet<int> notReleaseableRatingIDs = new HashSet<int>(userTypeRatingRepository.All.Where(t => t.UserID == user.ID && t.ValidTo == null).ToList().Where(t => !t.CanBeReleased()).Select(t => t.TypeRatingID));
			HashSet<int> currentRatingIDsSelected = new HashSet<int>(viewModel.TypeRatingIDs.Select(t => int.Parse(t)));

			if (!notReleaseableRatingIDs.IsSubsetOf(currentRatingIDsSelected)) {
				throw new NotSupportedException("You cannot release type ratings that you don't hold for at least 50 days.");
			}

			if (ModelState.IsValid) {

				userRepository.InsertOrUpdate(user);
				userRepository.Save();

				var currentUserTypeRatings = userTypeRatingRepository.All.Where(t => t.UserID == user.ID && t.ValidTo == null).ToList();

				foreach (var rating in currentUserTypeRatings) {
					if (!viewModel.TypeRatingIDs.Contains(rating.TypeRatingID.ToString())) {
						UserTypeRating toChange = userTypeRatingRepository.Find(rating.ID);
						toChange.ValidTo = DateTime.UtcNow;
						userTypeRatingRepository.InsertOrUpdate(toChange);
					}
				}

				foreach (string rating in viewModel.TypeRatingIDs) {
					if (!currentUserTypeRatings.Any(t => t.TypeRatingID.ToString() == rating)) {
						UserTypeRating newRating = new UserTypeRating();
						newRating.TypeRatingID = int.Parse(rating);
						newRating.UserID = user.ID;
						newRating.ValidFrom = DateTime.UtcNow;
						userTypeRatingRepository.InsertOrUpdate(newRating);
					}
				}

				userTypeRatingRepository.Save();

				return RedirectToAction("UserEdit", new { id = user.ID });
			} else {

				SetupViewModel(viewModel);
				return View(viewModel);
			}
		}

        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleCountry = countryRepository.All;
			ViewBag.PossibleRank = rankRepository.All;
			ViewBag.PossibleAirport = airportRepository.All;
             return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid) {
                userRepository.InsertOrUpdate(user);
                userRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleCountry = countryRepository.All;
				ViewBag.PossibleRank = rankRepository.All;
				ViewBag.PossibleAirport = airportRepository.All;
				return View();
			}
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(userRepository.Find(id));
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            userRepository.Delete(id);
            userRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


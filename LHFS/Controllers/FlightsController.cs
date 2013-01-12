using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;
using LHFS.Views.Flights.ViewModel;
using LHFS.Models.Security;

namespace LHFS.Controllers
{   
    public class FlightsController : Controller
    {
		private readonly IUserRepository userRepository;
		private readonly IFlightRepository flightRepository;
		private readonly IRotationRepository rotationRepository;
		private readonly IConnectionRepository connectionRepository;
		private readonly IAircraftTypeRepository aircraftTypeRepository;
		private readonly IUserTypeRatingRepository userTypeRatingRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public FlightsController() : this(new UserRepository(), new FlightRepository(), new RotationRepository(), new ConnectionRepository(), new AircraftTypeRepository(), new UserTypeRatingRepository())
        {
        }


		public FlightsController(IUserRepository userRepository, IFlightRepository flightRepository, IRotationRepository rotationRepository, IConnectionRepository connectionRepository, IAircraftTypeRepository aircraftTypeRepository, IUserTypeRatingRepository userTypeRatingRepository)
        {
			this.userRepository = userRepository;
			this.flightRepository = flightRepository;
			this.rotationRepository = rotationRepository;
			this.connectionRepository = connectionRepository;
			this.aircraftTypeRepository = aircraftTypeRepository;
			this.userTypeRatingRepository = userTypeRatingRepository;
        }

		[HttpPost]
		public ActionResult Book(FlightsBook viewModel) {

			Connection conn = connectionRepository.Find(viewModel.Connection.ID);

			Flight bookedFlight = new Flight();

			bookedFlight.AircraftTypeICAO = viewModel.AircraftTypeICAO;
			bookedFlight.AirlineICAO = conn.AirlineICAO;
			bookedFlight.BookedOn = DateTime.UtcNow;
			bookedFlight.DepartureAirportICAO = conn.DepartureAirportICAO;
			bookedFlight.DestinationAirportICAO = conn.DestinationAirportICAO;
			// TODO maybe change to prefix and id seperated
			bookedFlight.FlightNumber = conn.Name;
			bookedFlight.UserID = UserContext.GetCurrent().ID;

			flightRepository.InsertOrUpdate(bookedFlight);
			flightRepository.Save();

			if (viewModel.Option == 1 || viewModel.Option == 2) {
				
				Rotation rotation;

				if(viewModel.Option == 1) {
					rotation = new Rotation();
					rotation.Name = viewModel.NewRotationName;
					rotation.Flights = new List<Flight>();
					rotation.UserID = UserContext.GetCurrent().ID;
					
				} else {
					rotation = rotationRepository.AllIncluding(m => m.Flights).Single(t => t.ID == viewModel.RotationID.Value);
				}
				
				rotation.Flights.Add(bookedFlight);

				rotationRepository.InsertOrUpdate(rotation);
				rotationRepository.Save();
			}

			return RedirectToAction("BookingSaved", new { id = conn.ID });
		}

		public PartialViewResult BookingSaved(int id) {

			var selectedConnection = connectionRepository.AllIncluding(m => m.Departure, m => m.Destination).Single(t => t.ID == id);

			return PartialView(new FlightsBook() { Connection = selectedConnection } );
		}


		public PartialViewResult Book(int id)
        {
			FlightsBook viewModel = new FlightsBook();
			
			int userID = UserContext.GetCurrent().ID;

			var rotationsOfUser = rotationRepository.All.Where(t => t.UserID == userID).ToList();
			var selectedConnection = connectionRepository.AllIncluding(m => m.Departure, m => m.Destination).Single(t => t.ID == id);

			viewModel.PossibleRotations = rotationsOfUser.Select(t => new SelectListItem() { Text = t.Name, Value = t.ID.ToString() }).ToList();
			viewModel.Connection = selectedConnection;

			if (!selectedConnection.AircraftType.TypeGroup.HasValue) {
				throw new NotSupportedException("AircraftType of selected connection has no assigned TypeGroup");
			}

			int typeGroup = selectedConnection.AircraftType.TypeGroup.Value;
			
			List<int> currentTypeRatingsOfUser = userTypeRatingRepository.CurrentUserTypeRatings(userID).Select(t => t.TypeRatingID).ToList();

			if (!currentTypeRatingsOfUser.Any()) {
				throw new NotSupportedException("User has absolutely no type ratings.");
			}

			IQueryable<AircraftType> allPossibleAicraftTypes = aircraftTypeRepository.All.Where(t => t.TypeGroup == typeGroup && currentTypeRatingsOfUser.Contains(t.TypeRatingID.Value));

			viewModel.PossibleAircraftTypes = allPossibleAicraftTypes.ToList().Select(t => new SelectListItem() { Text = t.Longname, Value = t.ICAO }).ToList();
			
			

			return PartialView(viewModel);
        }

        public ViewResult Index()
        {
            return View(flightRepository.AllIncluding(flight => flight.User, flight => flight.ApprovedByUser));
        }

		public PartialViewResult FlightsByUser(int userID) {

			var list = flightRepository.AllIncluding(t => t.Airline).Where(t => t.UserID == userID && t.ApprovedOn.HasValue && t.PerformedOn.HasValue);

			return PartialView("FlightsTable", list);
		}

        //
        // GET: /Flights/Details/5

        public ViewResult Details(int id)
        {
            return View(flightRepository.Find(id));
        }

        //
        // GET: /Flights/Create

        public ActionResult Create()
        {
			ViewBag.PossibleUser = userRepository.All;
			ViewBag.PossibleApprovedByUser = userRepository.All;
            return View();
        } 

        //
        // POST: /Flights/Create

        [HttpPost]
        public ActionResult Create(Flight flight)
        {
            if (ModelState.IsValid) {
                flightRepository.InsertOrUpdate(flight);
                flightRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUser = userRepository.All;
				ViewBag.PossibleApprovedByUser = userRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Flights/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleUser = userRepository.All;
			ViewBag.PossibleApprovedByUser = userRepository.All;
             return View(flightRepository.Find(id));
        }

        //
        // POST: /Flights/Edit/5

        [HttpPost]
        public ActionResult Edit(Flight flight)
        {
            if (ModelState.IsValid) {
                flightRepository.InsertOrUpdate(flight);
                flightRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUser = userRepository.All;
				ViewBag.PossibleApprovedByUser = userRepository.All;
				return View();
			}
        }

        //
        // GET: /Flights/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(flightRepository.Find(id));
        }

        //
        // POST: /Flights/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            flightRepository.Delete(id);
            flightRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


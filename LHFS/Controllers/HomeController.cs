using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Views.Home.ViewModel;
using System.Web.Routing;
using LHFS.Models.Security;
using LHFS.Models;

namespace LHFS.Controllers
{
    public class HomeController : Controller
    {
		public IFormsAuthenticationService FormsService { get; set; }
		public IMembershipService MembershipService { get; set; }
		private readonly IUserRepository userRepository;
		private readonly IFlightRepository flightRepository;

		protected override void Initialize(RequestContext requestContext) {
			if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
			if (MembershipService == null) { MembershipService = new LHFSMembershipService(); }

			base.Initialize(requestContext);
		}

		public ActionResult Index() {

			HomeModel viewModel = new HomeModel() { BookedFlights = flightRepository.FindBookedFlightByUserID(UserContext.GetCurrent().ID).OrderBy(t => t.BookedOn) };
			viewModel.CurrentRank = userRepository.Find(UserContext.GetCurrent().ID).Rank;

			return View(viewModel);
		}

		public HomeController() : this(new UserRepository(), new FlightRepository())
        {
        }

		public HomeController(IUserRepository userRepository, IFlightRepository flightRepository)
        {
			this.userRepository = userRepository;
			this.flightRepository = flightRepository;
        }

		[HttpPost]
		public ActionResult Index(LoginModel model, string returnUrl) {
			if (ModelState.IsValid) {
				if (MembershipService.ValidateUser(model.Username, model.Password)) {
					FormsService.SignIn(model.Username, model.RememberMe);

					User loggedInUser = userRepository.FindByMail(model.Username);

					//UserContext.GetCurrent().User = loggedInUser;
					//UserContext.GetCurrent().User = userRepository.AllIncluding(u => u.UserTypeRatings, u => u.UserTypeRatings.Select(t => t.TypeRating.AircraftTypes.Select(g => g.ICAO))).Single(t => t.ID == 1);

					if (Url.IsLocalUrl(returnUrl)) {
						return Redirect(returnUrl);
					} else {
						return RedirectToAction("Index", "Home");
					}
				} else {
					ModelState.AddModelError("", "Der angegebene Benutzername oder das angegebene Kennwort ist ungültig.");
				}
			}

			// Wurde dieser Punkt erreicht, ist ein Fehler aufgetreten; Formular erneut anzeigen.
			return View(model);

		}

		public ActionResult LogOff() {
			FormsService.SignOut();

			UserContext.Abandon();

			return RedirectToAction("Index", "Home");
		}
    }
}

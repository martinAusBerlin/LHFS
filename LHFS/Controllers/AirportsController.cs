using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;
using LHFS.Views.Airports.ViewModel;
using LHFS.Models.Security;
using Omu.ValueInjecter;
using LHFS.ActionFilter;
using LHFS.Utils;
using System.Web.Mvc.Html;

namespace LHFS.Controllers
{   
    public class AirportsController : Controller
    {
		private readonly ICountryRepository countryRepository;
		private readonly IAirportRepository airportRepository;
		private readonly IAirportVersionRepository airportVersionRepository;
		private readonly IAirportDepartureRepository airportDepartureRepository;
		private readonly IAirportArrivalRepository airportArrivalRepository;
		private readonly IAirportDetailRepository airportDetailRepository;
		private readonly IAirportGroundOpRepository airportGroundOpRepository;
		private readonly IAirportHazardRepository airportHazardRepository;
		private readonly IAirportTerrainRepository airportTerrainRepository;
		private readonly IRegionRepository regionRepository;
		private readonly IChartTypeRepository chartTypeRepository;
		private readonly IAirportChartRepository airportChartRepository;
		private readonly IAirportAlternateRepository airportAlternateRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AirportsController() : this(new CountryRepository(), new AirportRepository(), new AirportVersionRepository(), new AirportDepartureRepository(), new AirportArrivalRepository(), new AirportDetailRepository(), new AirportGroundOpRepository(), new AirportHazardRepository(), new AirportTerrainRepository(), new RegionRepository(), new ChartTypeRepository(), new AirportChartRepository(), new AirportAlternateRepository())
        {
        }

		public AirportsController(ICountryRepository countryRepository, IAirportRepository airportRepository, IAirportVersionRepository airportVersionRepository, IAirportDepartureRepository airportDepartureRepository, IAirportArrivalRepository airportArrivalRepository, IAirportDetailRepository airportDetailRepository, IAirportGroundOpRepository airportGroundOpRepository, IAirportHazardRepository airportHazardRepository, IAirportTerrainRepository airportTerrainRepository, IRegionRepository regionRepository, IChartTypeRepository chartTypeRepository, IAirportChartRepository airportChartRepository, IAirportAlternateRepository airportAlternateRepository) {
			this.countryRepository = countryRepository;
			this.airportRepository = airportRepository;
			this.airportVersionRepository = airportVersionRepository;
			this.airportDepartureRepository = airportDepartureRepository;
			this.airportArrivalRepository = airportArrivalRepository;
			this.airportDetailRepository = airportDetailRepository;
			this.airportGroundOpRepository = airportGroundOpRepository;
			this.airportHazardRepository = airportHazardRepository;
			this.airportTerrainRepository = airportTerrainRepository;
			this.regionRepository = regionRepository;
			this.chartTypeRepository = chartTypeRepository;
			this.airportChartRepository = airportChartRepository;
			this.airportAlternateRepository = airportAlternateRepository;
        }

		public JsonResult GetAirport(string term) {

			string trimmedPattern = term;

			var list = airportRepository.AllIncluding(t => t.IcaoCountryCode.Country).Where(t => t.Name.Contains(trimmedPattern) || t.IATA.Contains(trimmedPattern) || t.ICAO.Contains(trimmedPattern) || t.IcaoCountryCode.Country.English.Contains(trimmedPattern)).Select(t => new { label = t.ICAO + " / " + t.Name + ", " + t.IcaoCountryCode.Country.English, value = t.ICAO }).Take(40).ToList();
			return Json(list, JsonRequestBehavior.AllowGet);
		}

		public PartialViewResult GetAirports(string pattern) {

			string trimmedPattern = pattern.Trim();

			var list = airportRepository.AllIncluding(t => t.IcaoCountryCode.Country).Where(t => t.Name.Contains(trimmedPattern) || t.IATA.Contains(trimmedPattern) || t.ICAO.Contains(trimmedPattern)).Take(20).ToList();

			return PartialView("List", list);

		}

		public ViewResult Search() {

			SearchModel viewModel = new SearchModel();
			//viewModel.AirportsByRegions = airportRepository.AllIncluding(airport => airport.Country.Region).GroupBy(t => t.Country.Region);

			viewModel.Regions = regionRepository.All.OrderBy(t => t.English.Length);

			return View(viewModel);
		}

        //
        // GET: /Airports/Details/5

        public ViewResult Details(string id, string message)
        {

			if (message == "NoChange") {
				ViewBag.ShowNoChangesMessage = true;
			} else {
				ViewBag.ShowNoChangesMessage = false;
			}

			if (message == "ChangesSaved") {
				ViewBag.ShowChangesSaved = true;
			} else {
				ViewBag.ShowChangesSaved = false;
			}

			ViewBag.IsPreview = false;

			return View(airportVersionRepository.FindCurrentIncluding(id, t => t.Arrival.CreatedByUser, t => t.Departure.CreatedByUser, t => t.Detail.CreatedByUser, t => t.GroundOp.CreatedByUser, t => t.Hazard.CreatedByUser, t => t.Terrain.CreatedByUser));
        }

		public ActionResult Preview(EditModel viewModel) {

			ViewBag.IsPreview = true;
			ViewBag.ShowNoChangesMessage = false;
			ViewBag.ShowChangesSaved = false;

			var original = airportVersionRepository.FindCurrentIncluding(viewModel.CurrentVersion.AirportICAO, t => t.Arrival.CreatedByUser, t => t.Departure.CreatedByUser, t => t.Detail.CreatedByUser, t => t.GroundOp.CreatedByUser, t => t.Hazard.CreatedByUser, t => t.Terrain.CreatedByUser);

			if (!viewModel.CurrentVersion.Detail.Equals(original.Detail)) {
				original.Detail.Elevation = viewModel.CurrentVersion.Detail.Elevation;
				original.Detail.MagVar = viewModel.CurrentVersion.Detail.MagVar;
				original.Detail.Category = viewModel.CurrentVersion.Detail.Category;
				original.Detail.LongestRunway = viewModel.CurrentVersion.Detail.LongestRunway;
				original.Detail.CreatedOn = DateTime.UtcNow;
				original.Detail.CreatedByUser = UserContext.GetCurrent().User;
			}


			if (!string.Equals(viewModel.CurrentVersion.Departure.Text, original.Departure.Text)) {
				original.Departure.Text = viewModel.CurrentVersion.Departure.Text;
				original.Departure.CreatedOn = DateTime.UtcNow;
				original.Departure.CreatedByUser = UserContext.GetCurrent().User;
			}

			if (!string.Equals(viewModel.CurrentVersion.Arrival.Text, original.Arrival.Text)) {
				original.Arrival.Text = viewModel.CurrentVersion.Arrival.Text;
				original.Arrival.CreatedOn = DateTime.UtcNow;
				original.Arrival.CreatedByUser = UserContext.GetCurrent().User;
			}

			if (!string.Equals(viewModel.CurrentVersion.GroundOp.Text, original.GroundOp.Text)) {
				original.GroundOp.Text = viewModel.CurrentVersion.GroundOp.Text;
				original.GroundOp.CreatedOn = DateTime.UtcNow;
				original.GroundOp.CreatedByUser = UserContext.GetCurrent().User;
			}

			if (!string.Equals(viewModel.CurrentVersion.Terrain.Text, original.Terrain.Text)) {
				original.Terrain.Text = viewModel.CurrentVersion.Terrain.Text;
				original.Terrain.CreatedOn = DateTime.UtcNow;
				original.Terrain.CreatedByUser = UserContext.GetCurrent().User;
			}

			if (!string.Equals(viewModel.CurrentVersion.Hazard.Text, original.Hazard.Text)) {
				original.Hazard.Text = viewModel.CurrentVersion.Hazard.Text;
				original.Hazard.CreatedOn = DateTime.UtcNow;
				original.Hazard.CreatedByUser = UserContext.GetCurrent().User;
			}

			return View("Details", original);

		}

		[HttpDelete]
		public void DeleteChart(int id) {

			airportChartRepository.Delete(id);
			airportChartRepository.Save();
		}

		[HttpDelete]
		public void DeleteAlternate(int id) {

			airportAlternateRepository.Delete(id);
			airportAlternateRepository.Save();
		}


		public PartialViewResult AddChart(string icao) {

			AddChartModel viewModel = new AddChartModel();
			SetupAddChartModel(icao, viewModel);

			return PartialView(viewModel);
		}

		public PartialViewResult AddAlternate(string icao) {

			AddAlternateModel viewModel = new AddAlternateModel();
			SetupAddAlternateModel(icao, viewModel);

			return PartialView(viewModel);
		}

		[HttpPost]
		public ActionResult AddAlternate(AddAlternateModel viewModel) {

			if (ModelState.IsValid) {
				airportAlternateRepository.InsertOrUpdate(viewModel.Alternate);
				airportAlternateRepository.Save();
				ViewBag.IsSaved = true;
			}

			SetupAddAlternateModel(viewModel.Alternate.AirportICAO, viewModel);

			return PartialView(viewModel);
		}


		private void SetupAddAlternateModel(string icao, AddAlternateModel viewModel) {
			viewModel.Alternate = new AirportAlternate();
			viewModel.Alternate.AirportICAO = icao;
		}


		private void SetupAddChartModel(string icao, AddChartModel viewModel) {
			viewModel.AirportChart = new AirportChart();
			viewModel.AirportChart.AirportICAO = icao;

			viewModel.PossibleChartTypes = chartTypeRepository.All;
		}

		public PartialViewResult GetCharts(string icao) {

			return PartialView(airportChartRepository.AllIncluding(t => t.ChartType).Where(t => t.AirportICAO == icao));
		}

		public PartialViewResult GetAlternates(string icao) {

			return PartialView(airportAlternateRepository.AllIncluding(t => t.AlternateAirport).Where(t => t.AirportICAO == icao));
		}


		[HttpPost]
		public ActionResult AddChart(AddChartModel viewModel) {

			if (ModelState.IsValid) {
				airportChartRepository.InsertOrUpdate(viewModel.AirportChart);
				airportChartRepository.Save();
				ViewBag.IsSaved = true;
			}

			SetupAddChartModel(viewModel.AirportChart.AirportICAO, viewModel);

			return PartialView(viewModel);
		}

		public ActionResult Edit(int id)
        {
			EditModel viewModel = new EditModel();
			viewModel.CurrentVersion = airportVersionRepository.FindIncluding(id, t => t.Arrival.CreatedByUser, t => t.Departure.CreatedByUser, t => t.Detail.CreatedByUser, t => t.GroundOp.CreatedByUser, t => t.Hazard.CreatedByUser, t => t.Terrain.CreatedByUser);
			SetupEditDropdowns(viewModel);

			return View(viewModel);
        }

		private void SetupEditDropdowns(EditModel viewModel) {
			viewModel.PossibleCountry = countryRepository.All;
			viewModel.PossibleCategory = new List<SelectListItem>() { new SelectListItem() { Text = "A", Value = "A" }, new SelectListItem() { Text = "B", Value = "B" }, new SelectListItem() { Text = "C", Value = "C" } };
		}

        //
        // POST: /Airports/Edit/5

		[HttpPost]
		[MultipleButton(Name = "action", Argument = "Preview")]
		public ActionResult Edit(EditModel viewModel) {
			if (ModelState.IsValid) {

				var updatedDetail = airportDetailRepository.Find(viewModel.CurrentVersion.Detail.ID);
				updatedDetail.Elevation = viewModel.CurrentVersion.Detail.Elevation;
				updatedDetail.MagVar = viewModel.CurrentVersion.Detail.MagVar;
				updatedDetail.Category = viewModel.CurrentVersion.Detail.Category;
				updatedDetail.LongestRunway = viewModel.CurrentVersion.Detail.LongestRunway;

				if (airportRepository.Update(UserContext.GetCurrent().ID, viewModel.CurrentVersion.ID, viewModel.CurrentVersion.Terrain, viewModel.CurrentVersion.Arrival, viewModel.CurrentVersion.Departure, updatedDetail, viewModel.CurrentVersion.GroundOp, viewModel.CurrentVersion.Hazard)) {
					return Preview(viewModel);
				} else {
					return GenerateAirportView(viewModel);
				}

			} else {

				return GenerateAirportView(viewModel);
			}
		}

		private ActionResult GenerateAirportView(EditModel viewModel) {
			EditModel editedViewModel = new EditModel();
			editedViewModel.CurrentVersion = airportVersionRepository.FindIncluding(viewModel.CurrentVersion.ID, t => t.Arrival.CreatedByUser, t => t.Departure.CreatedByUser, t => t.Detail.CreatedByUser, t => t.GroundOp.CreatedByUser, t => t.Hazard.CreatedByUser, t => t.Terrain.CreatedByUser);
			editedViewModel.CurrentVersion.Departure.Text = viewModel.CurrentVersion.Departure.Text;
			editedViewModel.CurrentVersion.Arrival.Text = viewModel.CurrentVersion.Arrival.Text;
			editedViewModel.CurrentVersion.GroundOp.Text = viewModel.CurrentVersion.GroundOp.Text;
			editedViewModel.CurrentVersion.Terrain.Text = viewModel.CurrentVersion.Terrain.Text;
			editedViewModel.CurrentVersion.Hazard.Text = viewModel.CurrentVersion.Hazard.Text;
			editedViewModel.CurrentVersion.Detail.Elevation = viewModel.CurrentVersion.Detail.Elevation;
			editedViewModel.CurrentVersion.Detail.MagVar = viewModel.CurrentVersion.Detail.MagVar;
			editedViewModel.CurrentVersion.Detail.Category = viewModel.CurrentVersion.Detail.Category;
			editedViewModel.CurrentVersion.Detail.LongestRunway = viewModel.CurrentVersion.Detail.LongestRunway;


			SetupEditDropdowns(editedViewModel);

			return View("Details", editedViewModel);
		}


        [HttpPost]
		[MultipleButton(Name = "action", Argument = "Save")]
		public ActionResult Save(EditModel viewModel)
        {
			if (ModelState.IsValid) {

				var updatedDetail = airportDetailRepository.Find(viewModel.CurrentVersion.Detail.ID);
				updatedDetail.Elevation = viewModel.CurrentVersion.Detail.Elevation;
				updatedDetail.MagVar = viewModel.CurrentVersion.Detail.MagVar;
				updatedDetail.Category = viewModel.CurrentVersion.Detail.Category;
				updatedDetail.LongestRunway = viewModel.CurrentVersion.Detail.LongestRunway;

				if (airportRepository.Update(UserContext.GetCurrent().ID, viewModel.CurrentVersion.ID, viewModel.CurrentVersion.Terrain, viewModel.CurrentVersion.Arrival, viewModel.CurrentVersion.Departure, updatedDetail, viewModel.CurrentVersion.GroundOp, viewModel.CurrentVersion.Hazard)) {
					airportRepository.Save();
					return RedirectToAction("Details", new { id = viewModel.CurrentVersion.AirportICAO, message = "ChangesSaved" });
				} else {
					return RedirectToAction("Details", new { id = viewModel.CurrentVersion.AirportICAO, message = "NoChange" });
				}
				
			} else {

				return GenerateAirportView(viewModel);
			}
        }

        //
        // GET: /Airports/Delete/5

		public ActionResult Delete(string id)
        {
			return View(airportRepository.Find(id));
        }

        //
        // POST: /Airports/Delete/5

        [HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(string id)
        {
			airportRepository.Delete(id);
            airportRepository.Save();

            return RedirectToAction("Overview");
        }
    }
}


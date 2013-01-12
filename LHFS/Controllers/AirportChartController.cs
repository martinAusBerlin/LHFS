using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class AirportChartController : Controller
    {
		private readonly IAirportRepository airportRepository;
		private readonly IAirportChartRepository airportchartRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AirportChartController() : this(new AirportRepository(), new AirportChartRepository())
        {
        }

        public AirportChartController(IAirportRepository airportRepository, IAirportChartRepository airportchartRepository)
        {
			this.airportRepository = airportRepository;
			this.airportchartRepository = airportchartRepository;
        }

        //
        // GET: /AirportChart/

        public ViewResult Index()
        {
            return View(airportchartRepository.AllIncluding(airportchart => airportchart.Airport));
        }

        //
        // GET: /AirportChart/Details/5

        public ViewResult Details(int id)
        {
            return View(airportchartRepository.Find(id));
        }

        //
        // GET: /AirportChart/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAirport = airportRepository.All;
            return View();
        } 

        //
        // POST: /AirportChart/Create

        [HttpPost]
        public ActionResult Create(AirportChart airportchart)
        {
            if (ModelState.IsValid) {
                airportchartRepository.InsertOrUpdate(airportchart);
                airportchartRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAirport = airportRepository.All;
				return View();
			}
        }
        
        //
        // GET: /AirportChart/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAirport = airportRepository.All;
             return View(airportchartRepository.Find(id));
        }

        //
        // POST: /AirportChart/Edit/5

        [HttpPost]
        public ActionResult Edit(AirportChart airportchart)
        {
            if (ModelState.IsValid) {
                airportchartRepository.InsertOrUpdate(airportchart);
                airportchartRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAirport = airportRepository.All;
				return View();
			}
        }

        //
        // GET: /AirportChart/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(airportchartRepository.Find(id));
        }

        //
        // POST: /AirportChart/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            airportchartRepository.Delete(id);
            airportchartRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


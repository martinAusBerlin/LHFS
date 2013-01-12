using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class AirportAlternateController : Controller
    {
		private readonly IAirportAlternateRepository airportalternateRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AirportAlternateController() : this(new AirportAlternateRepository())
        {
        }

        public AirportAlternateController(IAirportAlternateRepository airportalternateRepository)
        {
			this.airportalternateRepository = airportalternateRepository;
        }

        //
        // GET: /AirportAlternate/

        public ViewResult Index()
        {
            return View(airportalternateRepository.All);
        }

        //
        // GET: /AirportAlternate/Details/5

        public ViewResult Details(int id)
        {
            return View(airportalternateRepository.Find(id));
        }

        //
        // GET: /AirportAlternate/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /AirportAlternate/Create

        [HttpPost]
        public ActionResult Create(AirportAlternate airportalternate)
        {
            if (ModelState.IsValid) {
                airportalternateRepository.InsertOrUpdate(airportalternate);
                airportalternateRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /AirportAlternate/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(airportalternateRepository.Find(id));
        }

        //
        // POST: /AirportAlternate/Edit/5

        [HttpPost]
        public ActionResult Edit(AirportAlternate airportalternate)
        {
            if (ModelState.IsValid) {
                airportalternateRepository.InsertOrUpdate(airportalternate);
                airportalternateRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /AirportAlternate/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(airportalternateRepository.Find(id));
        }

        //
        // POST: /AirportAlternate/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            airportalternateRepository.Delete(id);
            airportalternateRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


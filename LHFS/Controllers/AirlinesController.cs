using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class AirlinesController : Controller
    {
		private readonly IAirlineRepository airlineRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AirlinesController() : this(new AirlineRepository())
        {
        }

        public AirlinesController(IAirlineRepository airlineRepository)
        {
			this.airlineRepository = airlineRepository;
        }

        //
        // GET: /Airlines/

        public ViewResult Index()
        {
            return View(airlineRepository.All);
        }

        //
        // GET: /Airlines/Details/5

        public ViewResult Details(string id)
        {
            return View(airlineRepository.Find(id));
        }

        //
        // GET: /Airlines/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Airlines/Create

        [HttpPost]
        public ActionResult Create(Airline airline)
        {
            if (ModelState.IsValid) {
                airlineRepository.Insert(airline);
                airlineRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Airlines/Edit/5
 
        public ActionResult Edit(string id)
        {
             return View(airlineRepository.Find(id));
        }

        //
        // POST: /Airlines/Edit/5

        [HttpPost]
        public ActionResult Edit(Airline airline)
        {
            if (ModelState.IsValid) {
                airlineRepository.Update(airline);
                airlineRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Airlines/Delete/5
 
        public ActionResult Delete(string id)
        {
            return View(airlineRepository.Find(id));
        }

        //
        // POST: /Airlines/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            airlineRepository.Delete(id);
            airlineRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


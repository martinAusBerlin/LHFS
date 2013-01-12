using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class DivisionsController : Controller
    {
		private readonly IDivisionRepository divisionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public DivisionsController() : this(new DivisionRepository())
        {
        }

        public DivisionsController(IDivisionRepository divisionRepository)
        {
			this.divisionRepository = divisionRepository;
        }

        //
        // GET: /Divisions/

        public ViewResult Index()
        {
            return View(divisionRepository.All);
        }

        //
        // GET: /Divisions/Details/5

        public ViewResult Details(int id)
        {
            return View(divisionRepository.Find(id));
        }

        //
        // GET: /Divisions/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Divisions/Create

        [HttpPost]
        public ActionResult Create(Division division)
        {
            if (ModelState.IsValid) {
                divisionRepository.InsertOrUpdate(division);
                divisionRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Divisions/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(divisionRepository.Find(id));
        }

        //
        // POST: /Divisions/Edit/5

        [HttpPost]
        public ActionResult Edit(Division division)
        {
            if (ModelState.IsValid) {
                divisionRepository.InsertOrUpdate(division);
                divisionRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Divisions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(divisionRepository.Find(id));
        }

        //
        // POST: /Divisions/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            divisionRepository.Delete(id);
            divisionRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


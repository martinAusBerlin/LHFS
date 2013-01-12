using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class ScheduleAircraftReplacementsController : Controller
    {
		private readonly IScheduleAircraftReplacementRepository scheduleaircraftreplacementRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ScheduleAircraftReplacementsController() : this(new ScheduleAircraftReplacementRepository())
        {
        }

        public ScheduleAircraftReplacementsController(IScheduleAircraftReplacementRepository scheduleaircraftreplacementRepository)
        {
			this.scheduleaircraftreplacementRepository = scheduleaircraftreplacementRepository;
        }

        //
        // GET: /ScheduleAircraftReplacements/

        public ViewResult Index()
        {
            return View(scheduleaircraftreplacementRepository.All);
        }

        //
        // GET: /ScheduleAircraftReplacements/Details/5

        public ViewResult Details(int id)
        {
            return View(scheduleaircraftreplacementRepository.Find(id));
        }

        //
        // GET: /ScheduleAircraftReplacements/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ScheduleAircraftReplacements/Create

        [HttpPost]
        public ActionResult Create(ScheduleAircraftReplacement scheduleaircraftreplacement)
        {
            if (ModelState.IsValid) {
                scheduleaircraftreplacementRepository.InsertOrUpdate(scheduleaircraftreplacement);
                scheduleaircraftreplacementRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /ScheduleAircraftReplacements/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(scheduleaircraftreplacementRepository.Find(id));
        }

        //
        // POST: /ScheduleAircraftReplacements/Edit/5

        [HttpPost]
        public ActionResult Edit(ScheduleAircraftReplacement scheduleaircraftreplacement)
        {
            if (ModelState.IsValid) {
                scheduleaircraftreplacementRepository.InsertOrUpdate(scheduleaircraftreplacement);
                scheduleaircraftreplacementRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /ScheduleAircraftReplacements/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(scheduleaircraftreplacementRepository.Find(id));
        }

        //
        // POST: /ScheduleAircraftReplacements/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            scheduleaircraftreplacementRepository.Delete(id);
            scheduleaircraftreplacementRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


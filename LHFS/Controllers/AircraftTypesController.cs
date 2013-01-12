using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;
using LHFS.Views.AircraftTypes.ViewModel;

namespace LHFS.Controllers
{   
    public class AircraftTypesController : Controller
    {
		private readonly IDivisionRepository divisionRepository;
		private readonly IAircraftTypeRepository aircrafttypeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AircraftTypesController() : this(new DivisionRepository(), new AircraftTypeRepository())
        {
        }

        public AircraftTypesController(IDivisionRepository divisionRepository, IAircraftTypeRepository aircrafttypeRepository)
        {
			this.divisionRepository = divisionRepository;
			this.aircrafttypeRepository = aircrafttypeRepository;
        }


		public ViewResult Overview() {

			OverviewModel viewModel = new OverviewModel();
			viewModel.Long = aircrafttypeRepository.All.Where(t => t.TypeGroup == 1).OrderBy(t => t.SortingOrder);
			viewModel.Medium = aircrafttypeRepository.All.Where(t => t.TypeGroup == 2).OrderBy(t => t.SortingOrder);
			viewModel.Short = aircrafttypeRepository.All.Where(t => t.TypeGroup == 3).OrderBy(t => t.SortingOrder);
			viewModel.Cargo = aircrafttypeRepository.All.Where(t => t.TypeGroup == 4).OrderBy(t => t.SortingOrder);

			return View(viewModel);
		}

        //
        // GET: /AircraftTypes/

        public ViewResult Index()
        {
            return View(aircrafttypeRepository.AllIncluding(aircrafttype => aircrafttype.Division, aircrafttype => aircrafttype.Aircrafts));
        }

        //
        // GET: /AircraftTypes/Details/5

		public PartialViewResult Details(string id)
        {
			return PartialView(aircrafttypeRepository.Find(id));
        }

        //
        // GET: /AircraftTypes/Create

        public ActionResult Create()
        {
			ViewBag.PossibleDivision = divisionRepository.All;
            return View();
        } 

        //
        // POST: /AircraftTypes/Create

        [HttpPost]
        public ActionResult Create(AircraftType aircrafttype)
        {
            if (ModelState.IsValid) {
                aircrafttypeRepository.Insert(aircrafttype);
                aircrafttypeRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleDivision = divisionRepository.All;
				return View();
			}
        }
        
        //
        // GET: /AircraftTypes/Edit/5
 
        public ActionResult Edit(string id)
        {
			ViewBag.PossibleDivision = divisionRepository.All;
             return View(aircrafttypeRepository.Find(id));
        }

        //
        // POST: /AircraftTypes/Edit/5

        [HttpPost]
        public ActionResult Edit(AircraftType aircrafttype)
        {
            if (ModelState.IsValid) {
                aircrafttypeRepository.Update(aircrafttype);
                aircrafttypeRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleDivision = divisionRepository.All;
				return View();
			}
        }

        //
        // GET: /AircraftTypes/Delete/5
 
        public ActionResult Delete(string id)
        {
            return View(aircrafttypeRepository.Find(id));
        }

        //
        // POST: /AircraftTypes/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            aircrafttypeRepository.Delete(id);
            aircrafttypeRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class TypeRatingsController : Controller
    {
		private readonly ITypeRatingRepository typeratingRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public TypeRatingsController() : this(new TypeRatingRepository())
        {
        }

        public TypeRatingsController(ITypeRatingRepository typeratingRepository)
        {
			this.typeratingRepository = typeratingRepository;
        }

        //
        // GET: /TypeRatings/

        public ViewResult Index()
        {
            return View(typeratingRepository.AllIncluding(typerating => typerating.AircraftTypes));
        }

        //
        // GET: /TypeRatings/Details/5

        public ViewResult Details(int id)
        {
            return View(typeratingRepository.Find(id));
        }

        //
        // GET: /TypeRatings/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /TypeRatings/Create

        [HttpPost]
        public ActionResult Create(TypeRating typerating)
        {
            if (ModelState.IsValid) {
                typeratingRepository.InsertOrUpdate(typerating);
                typeratingRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /TypeRatings/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(typeratingRepository.Find(id));
        }

        //
        // POST: /TypeRatings/Edit/5

        [HttpPost]
        public ActionResult Edit(TypeRating typerating)
        {
            if (ModelState.IsValid) {
                typeratingRepository.InsertOrUpdate(typerating);
                typeratingRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /TypeRatings/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(typeratingRepository.Find(id));
        }

        //
        // POST: /TypeRatings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            typeratingRepository.Delete(id);
            typeratingRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


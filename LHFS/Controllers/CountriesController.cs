using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class CountriesController : Controller
    {
		private readonly ICountryRepository countryRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public CountriesController() : this(new CountryRepository())
        {
        }

        public CountriesController(ICountryRepository countryRepository)
        {
			this.countryRepository = countryRepository;
        }

        //
        // GET: /Countries/

        public ViewResult Index()
        {
            return View(countryRepository.AllIncluding(country => country.Users));
        }

        //
        // GET: /Countries/Details/5

        public ViewResult Details(string id)
        {
            return View(countryRepository.Find(id));
        }

        //
        // GET: /Countries/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Countries/Create

        [HttpPost]
        public ActionResult Create(Country country)
        {
            if (ModelState.IsValid) {
                countryRepository.InsertOrUpdate(country);
                countryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Countries/Edit/5
 
        public ActionResult Edit(string id)
        {
             return View(countryRepository.Find(id));
        }

        //
        // POST: /Countries/Edit/5

        [HttpPost]
        public ActionResult Edit(Country country)
        {
            if (ModelState.IsValid) {
                countryRepository.InsertOrUpdate(country);
                countryRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Countries/Delete/5
 
        public ActionResult Delete(string id)
        {
            return View(countryRepository.Find(id));
        }

        //
        // POST: /Countries/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            countryRepository.Delete(id);
            countryRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


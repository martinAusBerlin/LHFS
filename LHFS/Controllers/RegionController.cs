using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class RegionController : Controller
    {
		private readonly IRegionRepository regionRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RegionController() : this(new RegionRepository())
        {
        }

        public RegionController(IRegionRepository regionRepository)
        {
			this.regionRepository = regionRepository;
        }

        //
        // GET: /Region/

        public ViewResult Index()
        {
            return View(regionRepository.All);
        }

        //
        // GET: /Region/Details/5

        public ViewResult Details(int id)
        {
            return View(regionRepository.Find(id));
        }

        //
        // GET: /Region/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Region/Create

        [HttpPost]
        public ActionResult Create(Region region)
        {
            if (ModelState.IsValid) {
                regionRepository.InsertOrUpdate(region);
                regionRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Region/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(regionRepository.Find(id));
        }

        //
        // POST: /Region/Edit/5

        [HttpPost]
        public ActionResult Edit(Region region)
        {
            if (ModelState.IsValid) {
                regionRepository.InsertOrUpdate(region);
                regionRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Region/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(regionRepository.Find(id));
        }

        //
        // POST: /Region/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            regionRepository.Delete(id);
            regionRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


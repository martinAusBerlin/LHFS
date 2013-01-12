using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class ChartTypeController : Controller
    {
		private readonly IChartTypeRepository charttypeRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ChartTypeController() : this(new ChartTypeRepository())
        {
        }

        public ChartTypeController(IChartTypeRepository charttypeRepository)
        {
			this.charttypeRepository = charttypeRepository;
        }

        //
        // GET: /ChartType/

        public ViewResult Index()
        {
            return View(charttypeRepository.All);
        }

        //
        // GET: /ChartType/Details/5

        public ViewResult Details(string id)
        {
            return View(charttypeRepository.Find(id));
        }

        //
        // GET: /ChartType/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ChartType/Create

        [HttpPost]
        public ActionResult Create(ChartType charttype)
        {
            if (ModelState.IsValid) {
                charttypeRepository.InsertOrUpdate(charttype);
                charttypeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /ChartType/Edit/5
 
        public ActionResult Edit(string id)
        {
             return View(charttypeRepository.Find(id));
        }

        //
        // POST: /ChartType/Edit/5

        [HttpPost]
        public ActionResult Edit(ChartType charttype)
        {
            if (ModelState.IsValid) {
                charttypeRepository.InsertOrUpdate(charttype);
                charttypeRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /ChartType/Delete/5
 
        public ActionResult Delete(string id)
        {
            return View(charttypeRepository.Find(id));
        }

        //
        // POST: /ChartType/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            charttypeRepository.Delete(id);
            charttypeRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


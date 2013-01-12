using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class RanksController : Controller
    {
		private readonly IRankRepository rankRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RanksController() : this(new RankRepository())
        {
        }

        public RanksController(IRankRepository rankRepository)
        {
			this.rankRepository = rankRepository;
        }

        //
        // GET: /Ranks/

        public ViewResult Index()
        {
            return View(rankRepository.AllIncluding(rank => rank.Users));
        }

        //
        // GET: /Ranks/Details/5

        public ViewResult Details(int id)
        {
            return View(rankRepository.Find(id));
        }

        //
        // GET: /Ranks/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Ranks/Create

        [HttpPost]
        public ActionResult Create(Rank rank)
        {
            if (ModelState.IsValid) {
                rankRepository.InsertOrUpdate(rank);
                rankRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Ranks/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(rankRepository.Find(id));
        }

        //
        // POST: /Ranks/Edit/5

        [HttpPost]
        public ActionResult Edit(Rank rank)
        {
            if (ModelState.IsValid) {
                rankRepository.InsertOrUpdate(rank);
                rankRepository.Save();
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Ranks/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(rankRepository.Find(id));
        }

        //
        // POST: /Ranks/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            rankRepository.Delete(id);
            rankRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


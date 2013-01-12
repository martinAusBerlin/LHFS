using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class RotationsController : Controller
    {
		private readonly IUserRepository userRepository;
		private readonly IRotationRepository rotationRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public RotationsController() : this(new UserRepository(), new RotationRepository())
        {
        }

        public RotationsController(IUserRepository userRepository, IRotationRepository rotationRepository)
        {
			this.userRepository = userRepository;
			this.rotationRepository = rotationRepository;
        }

        //
        // GET: /Rotations/

        public ViewResult Index()
        {
            return View(rotationRepository.AllIncluding(rotation => rotation.User, rotation => rotation.Flights));
        }

        //
        // GET: /Rotations/Details/5

        public ViewResult Details(int id)
        {
            return View(rotationRepository.Find(id));
        }

        //
        // GET: /Rotations/Create

        public ActionResult Create()
        {
			ViewBag.PossibleUser = userRepository.All;
            return View();
        } 

        //
        // POST: /Rotations/Create

        [HttpPost]
        public ActionResult Create(Rotation rotation)
        {
            if (ModelState.IsValid) {
                rotationRepository.InsertOrUpdate(rotation);
                rotationRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUser = userRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Rotations/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleUser = userRepository.All;
             return View(rotationRepository.Find(id));
        }

        //
        // POST: /Rotations/Edit/5

        [HttpPost]
        public ActionResult Edit(Rotation rotation)
        {
            if (ModelState.IsValid) {
                rotationRepository.InsertOrUpdate(rotation);
                rotationRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUser = userRepository.All;
				return View();
			}
        }

        //
        // GET: /Rotations/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(rotationRepository.Find(id));
        }

        //
        // POST: /Rotations/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            rotationRepository.Delete(id);
            rotationRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


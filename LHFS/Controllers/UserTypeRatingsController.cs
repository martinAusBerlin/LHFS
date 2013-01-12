using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;

namespace LHFS.Controllers
{   
    public class UserTypeRatingsController : Controller
    {
		private readonly IUserRepository userRepository;
		private readonly ITypeRatingRepository typeratingRepository;
		private readonly IUserTypeRatingRepository usertyperatingRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public UserTypeRatingsController() : this(new UserRepository(), new TypeRatingRepository(), new UserTypeRatingRepository())
        {
        }

        public UserTypeRatingsController(IUserRepository userRepository, ITypeRatingRepository typeratingRepository, IUserTypeRatingRepository usertyperatingRepository)
        {
			this.userRepository = userRepository;
			this.typeratingRepository = typeratingRepository;
			this.usertyperatingRepository = usertyperatingRepository;
        }

        //
        // GET: /UserTypeRatings/

        public ViewResult Index()
        {
            return View(usertyperatingRepository.AllIncluding(usertyperating => usertyperating.User, usertyperating => usertyperating.TypeRating));
        }

        //
        // GET: /UserTypeRatings/Details/5

        public ViewResult Details(int id)
        {
            return View(usertyperatingRepository.Find(id));
        }

        //
        // GET: /UserTypeRatings/Create

        public ActionResult Create()
        {
			ViewBag.PossibleUser = userRepository.All;
			ViewBag.PossibleTypeRating = typeratingRepository.All;
            return View();
        } 

        //
        // POST: /UserTypeRatings/Create

        [HttpPost]
        public ActionResult Create(UserTypeRating usertyperating)
        {
            if (ModelState.IsValid) {
                usertyperatingRepository.InsertOrUpdate(usertyperating);
                usertyperatingRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUser = userRepository.All;
				ViewBag.PossibleTypeRating = typeratingRepository.All;
				return View();
			}
        }
        
        //
        // GET: /UserTypeRatings/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleUser = userRepository.All;
			ViewBag.PossibleTypeRating = typeratingRepository.All;
             return View(usertyperatingRepository.Find(id));
        }

        //
        // POST: /UserTypeRatings/Edit/5

        [HttpPost]
        public ActionResult Edit(UserTypeRating usertyperating)
        {
            if (ModelState.IsValid) {
                usertyperatingRepository.InsertOrUpdate(usertyperating);
                usertyperatingRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUser = userRepository.All;
				ViewBag.PossibleTypeRating = typeratingRepository.All;
				return View();
			}
        }

        //
        // GET: /UserTypeRatings/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(usertyperatingRepository.Find(id));
        }

        //
        // POST: /UserTypeRatings/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            usertyperatingRepository.Delete(id);
            usertyperatingRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


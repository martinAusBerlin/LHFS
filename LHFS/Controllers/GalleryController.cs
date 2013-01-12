using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;
using System.IO;
using LHFS.Views.Gallery.ViewModel;
using LHFS.Models.Security;
using System.Configuration;
using LHFS.Utils;

namespace LHFS.Controllers
{   
    public class GalleryController : Controller
    {
		private readonly IUserRepository userRepository;
		private readonly IGalleryImageRepository galleryimageRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public GalleryController() : this(new UserRepository(), new GalleryImageRepository())
        {
        }

        public GalleryController(IUserRepository userRepository, IGalleryImageRepository galleryimageRepository)
        {
			this.userRepository = userRepository;
			this.galleryimageRepository = galleryimageRepository;
        }

        //
        // GET: /Gallery/

		public ViewResult Overview(string path) {

			//if (string.IsNullOrEmpty(path)) {
			//    path = @"C:\Users\martinM10\Documents\Visual Studio 2010\Projects\LHFS\LHFS\Images\Gallery\";
			//}

			//DirectoryInfo directoryInfo = new DirectoryInfo(path);
			//FileInfo[] fileInfos = directoryInfo.GetFiles("*.jpg");
			//string line;

			//foreach (FileInfo info in fileInfos) {
			//    Response.Write(@"UNION SELECT N'" + info.Name + "', NULL,GETUTCDATE(),1<br/>");
			//}

			
			
			return View(galleryimageRepository.AllIncluding(galleryimage => galleryimage.User).OrderByDescending(t => t.Url).Take(30));
		}

        public ViewResult Index()
        {
            return View(galleryimageRepository.AllIncluding(galleryimage => galleryimage.User));
        }

        //
        // GET: /Gallery/Details/5

        public ViewResult Details(int id)
        {
            return View(galleryimageRepository.Find(id));
        }

        //
        // GET: /Gallery/Create

        public ActionResult Create()
        {
            return View();
        }


		public PartialViewResult UsersList() {

			int userID = UserContext.GetCurrent().ID;

			return PartialView("List", galleryimageRepository.AllIncluding(galleryimage => galleryimage.User).Where(t => t.UserID == userID).OrderByDescending(t => t.CreatedOn));
		}

		

		[HttpPost]
		public void Upload(HttpPostedFileBase FileData) {

			try {

				string path = UserContext.GetCurrent().ID + @"\" + "Gallery" + @"\";

				FileHelper.WriteFile(HttpContext.Server.MapPath("~") + ConfigurationManager.AppSettings["UserContentBasePath"] + @"\" + path, FileData.FileName, FileData.InputStream);

				GalleryImage galleryimage = new GalleryImage();
				galleryimage.CreatedOn = DateTime.UtcNow;
				galleryimage.ID = default(int);
				galleryimage.NumberOfViews = 0;
				galleryimage.Url = path + FileData.FileName;
				galleryimage.UserID = UserContext.GetCurrent().ID;

				galleryimageRepository.InsertOrUpdate(galleryimage);
				galleryimageRepository.Save();
			} catch (Exception e) {
				throw e;	
			}
			
		}
        
		[HttpPost]
		public ActionResult Create(GalleryImage galleryimage, HttpPostedFileBase file)
        {
			// TODO UserID muss aus Session kommen

			//galleryimage.CreatedOn = DateTime.UtcNow;
			//galleryimage.UserID = 1;

			//if (ModelState.IsValid) {
			//    galleryimageRepository.InsertOrUpdate(galleryimage);
			//    galleryimageRepository.Save();
			//    return RedirectToAction("Index");
			//} else {
			    return View();
			//}
        }
        
        //
        // GET: /Gallery/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleUser = userRepository.All;
             return View(galleryimageRepository.Find(id));
        }

        //
        // POST: /Gallery/Edit/5

        [HttpPost]
        public ActionResult Edit(GalleryImage galleryimage)
        {
            if (ModelState.IsValid) {
                galleryimageRepository.InsertOrUpdate(galleryimage);
                galleryimageRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleUser = userRepository.All;
				return View();
			}
        }

        //
        // GET: /Gallery/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(galleryimageRepository.Find(id));
        }

        //
        // POST: /Gallery/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            galleryimageRepository.Delete(id);
            galleryimageRepository.Save();

            return RedirectToAction("Index");
        }
    }
}


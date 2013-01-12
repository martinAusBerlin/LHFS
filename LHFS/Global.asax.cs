using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using LHFS.Models;
using System.Data.Entity.ModelConfiguration.Conventions;
using LHFS.ActionFilter;

namespace LHFS {
	// Hinweis: Anweisungen zum Aktivieren des klassischen Modus von IIS6 oder IIS7 
	// finden Sie unter "http://go.microsoft.com/?LinkId=9394801".

	public class MvcApplication : System.Web.HttpApplication {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("{resource}.ashx/{*pathInfo}");
			routes.IgnoreRoute("favicon.ico");

			routes.MapRoute(
			   "LogOn", // Route name
			   "Account/{action}", // URL with parameters
			   new { controller = "Account", action = "LogOn" } // Parameter defaults
		   );

			routes.MapRoute(
				"Localization", // Route name
				"{lang}/{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

			/*

			routes.MapRoute(
				"LogOn", // Route name
				"Account/{action}", // URL with parameters
				new { controller = "Account", action = "LogOn" } // Parameter defaults
			);

			routes.MapRoute(
				"Localization", // Route name
				"{lang}/{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

			routes.MapRoute(
				"Extended", // Routenname
				"{controller}/{action}/{id}/{message}", // URL mit Parametern
				new { controller = "Home", action = "Index", id = UrlParameter.Optional, message = UrlParameter.Optional } // Parameterstandardwerte
			);

			routes.MapRoute(
				"Default", // Routenname
				"{controller}/{action}/{id}", // URL mit Parametern
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameterstandardwerte
			);
			 
			 */


		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			
			GlobalFilters.Filters.Add(new LocalizationAttribute());

			Database.SetInitializer<LHFSContext>(new ContextInitializer());

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
		}
	}
}
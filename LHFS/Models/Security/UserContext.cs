using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LHFS.Models.Security {
	public class UserContext {
		public User User { 
			get {
				return new UserRepository().AllIncluding(u => u.UserTypeRatings, u => u.UserTypeRatings.Select(t => t.TypeRating.AircraftTypes)).Single(t => t.ID == 1);
			} 
		}

		public int ID {
			get {
				//return User.ID;
				return 1;
			}
		}

		public static UserContext GetCurrent() {



			if (HttpContext.Current.Session["UserContext"] == null) {
				HttpContext.Current.Session["UserContext"] = new UserContext();
			}

			return (UserContext)HttpContext.Current.Session["UserContext"];
		}

		public static void Abandon() {
			HttpContext.Current.Session.Abandon();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LHFS.Models.Security {
	public class FormsAuthenticationService : IFormsAuthenticationService {
		public void SignIn(string userName, bool createPersistentCookie) {
		
			if (String.IsNullOrEmpty(userName))
				throw new ArgumentException("Der Wert darf nicht NULL oder leer sein.", "userName");

			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
		}

		public void SignOut() {
			FormsAuthentication.SignOut();
		}
	}
}
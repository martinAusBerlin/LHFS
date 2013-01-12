using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LHFS.Models.Security {
	
	public class LHFSMembershipService : IMembershipService {
		private readonly MembershipProvider _provider;

		public LHFSMembershipService()
			: this(null) {
		}

		public LHFSMembershipService(MembershipProvider provider) {
			_provider = provider ?? Membership.Provider;
		}

		public int MinPasswordLength {
			get {
				return _provider.MinRequiredPasswordLength;
			}
		}

		public bool ValidateUser(string userName, string password) {
			if (String.IsNullOrEmpty(userName))
				throw new ArgumentException("Der Wert darf nicht NULL oder leer sein.", "userName");
			if (String.IsNullOrEmpty(password))
				throw new ArgumentException("Der Wert darf nicht NULL oder leer sein.", "password");

			return _provider.ValidateUser(userName, password);
		}

		public MembershipCreateStatus CreateUser(string userName, string password, string email) {
			if (String.IsNullOrEmpty(userName))
				throw new ArgumentException("Der Wert darf nicht NULL oder leer sein.", "userName");
			if (String.IsNullOrEmpty(password))
				throw new ArgumentException("Der Wert darf nicht NULL oder leer sein.", "password");
			if (String.IsNullOrEmpty(email))
				throw new ArgumentException("Der Wert darf nicht NULL oder leer sein.", "email");

			MembershipCreateStatus status;
			_provider.CreateUser(userName, password, email, null, null, true, null, out status);
			return status;
		}

		public bool ChangePassword(string userName, string oldPassword, string newPassword) {
			if (String.IsNullOrEmpty(userName))
				throw new ArgumentException("Der Wert darf nicht NULL oder leer sein.", "userName");
			if (String.IsNullOrEmpty(oldPassword))
				throw new ArgumentException("Der Wert darf nicht NULL oder leer sein.", "oldPassword");
			if (String.IsNullOrEmpty(newPassword))
				throw new ArgumentException("Der Wert darf nicht NULL oder leer sein.", "newPassword");

			// In bestimmten Fehlerszenarios wird von der zugrunde liegenden ChangePassword()-Methode
			// nicht "false" zurückgegeben, sondern eine Ausnahme ausgelöst.
			try {
				MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
				return currentUser.ChangePassword(oldPassword, newPassword);
			} catch (ArgumentException) {
				return false;
			} catch (MembershipPasswordException) {
				return false;
			}
		}
	}
}
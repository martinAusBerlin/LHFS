using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;
using LHFS.Helpers;
using LHFS.Validation;
using System.ComponentModel.DataAnnotations;
using LHFS.Resources;

namespace LHFS.Views.Users.ViewModel
{

	
    public class UserEditModel
    {
		[Display(ResourceType = typeof(UserResource), Name = "Birthday")]
		public DateTime Birthday { get; set; }
		[EmailAddress]
		public string Mail { get; set; }
		public string CountryISO { get; set; }
		public string City { get; set; }

		public string AirportICAO { get; set; }
		public bool ReceiveNewsletter { get; set; }
		public bool HideEmail { get; set; }
		public bool ShowGallery { get; set; }
		public bool HideProfile { get; set; }
		public int? VatsimID { get; set; }
		public int? IvaoID { get; set; }
		public string RealPilotLicense { get; set; }
		public string Comments { get; set; }
		public bool CareerPathOption { get; set; }

		public virtual Rank Rank { get; set; }

		[Compare("ConfirmPassword")]
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		
		public List<SelectListItem> PossibleCountries { get; set; }
		public List<SelectListItem> PossibleLanguages { get; set; }
		public List<SelectListItem> PossibleHomeBases { get; set; }

		public List<UsersTypeRatingListItem> PossibleTypeRatings { get; set; }

		public string[] TypeRatingIDs { get; set; }
		
    }
}

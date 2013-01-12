using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	
	public class IcaoCountryCode {

		[MaxLength(2)]
		[ForeignKey("Country")]
		public string CountryISO { get; set; }
		[KeyAttribute, MaxLength(2)]
		public string IcaoCountryCodeID { get; set; }

		public virtual Country Country { get; set; }


	}
}
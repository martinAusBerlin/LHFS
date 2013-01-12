using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	[DisplayColumn("Name")]
	public class Airport {
		
		public string IATA { get; set; }
		public string Name { get; set; }
		[KeyAttribute]
		public string ICAO { get; set; }
		
		public string IcaoCountryCodeID { get; set; }
		[ForeignKey("IcaoCountryCodeID")]
		public virtual IcaoCountryCode IcaoCountryCode { get; set; }

		public string TimeZoneName { get; set; }
		
		public virtual ICollection<AirportScenery> Sceneries { get; set; }
		public virtual ICollection<Aircraft> BasedAircrafts { get; set; }
		public virtual ICollection<User> Users { get; set; }
	}
}
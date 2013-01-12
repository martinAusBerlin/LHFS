using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class AirportAlternate {

		[KeyAttribute]
		public int ID { get; set; }
	
		[Required]
		public string AirportICAO { get; set; }
		[ForeignKey("AirportICAO")]
		public virtual Airport Airport { get; set; }

		[Required]
		public string AlternateAirportICAO { get; set; }
		[ForeignKey("AlternateAirportICAO")]
		public virtual Airport AlternateAirport { get; set; }

		public int? FlightLevel { get; set; }
		public string Description { get; set; }
	}
}
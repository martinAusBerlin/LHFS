using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LHFS.Models.Interfaces;

namespace LHFS.Models {
	
	public class Airline {

		public string IATA { get; set; }
		[Key]
		public string ICAO { get; set; }
		public string Callsign { get; set; }
		public string Name { get; set; }
		
	}
}
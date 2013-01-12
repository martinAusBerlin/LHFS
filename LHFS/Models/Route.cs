using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class Route {
		[KeyAttribute]
		public int ID { get; set; }
		
		public string DepartureAirportID { get; set; }
		[ForeignKey("DepartureAirportID")]
		public virtual Airport Departure { get; set; }
		
		public string DestinationAirportID { get; set; }
		[ForeignKey("DestinationAirportID")]
		public virtual Airport Destination { get; set; }
		
		public int FlightLevel { get; set; }
		public string RouteText { get; set; }
	}
}
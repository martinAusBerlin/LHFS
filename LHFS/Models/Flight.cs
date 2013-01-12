using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class Flight {
		[KeyAttribute]
		public int ID { get; set; }
		public string FlightNumber { get; set; }
		
		public int UserID { get; set; }
		[ForeignKey("UserID")]
		public virtual User User { get; set; }

		public int? ApprovedByUserID { get; set; }
		[ForeignKey("ApprovedByUserID")]
		public virtual User ApprovedByUser { get; set; }

		public DateTime? ApprovedOn { get; set; }

		public decimal? TakeoffWeight { get; set; }
		public decimal? LandingWeight { get; set; }
		public DateTime? OffBlock { get; set; }
		public DateTime? OnBlock { get; set; }
		public DateTime? TakeOff { get; set; }
		public DateTime? Touchdown { get; set; }
		public string RouteText { get; set; }
		public string AdditionalText { get; set; }
		public DateTime BookedOn { get; set; }
		public DateTime? PerformedOn { get; set; }

		[ForeignKey("Airline")]
		public string AirlineICAO { get; set; }
		public virtual Airline Airline { get; set; }

		[ForeignKey("AircraftType")]
		public string AircraftTypeICAO { get; set; }
		public virtual AircraftType AircraftType { get; set; }

		[ForeignKey("Destination")]
		public string DestinationAirportICAO { get; set; }
		public virtual Airport Destination { get; set; }
		[ForeignKey("Departure")]
		public string DepartureAirportICAO { get; set; }
		public virtual Airport Departure { get; set; }

		public Int64? FlightTimeTicks { get; set; }

		[NotMapped]
		public TimeSpan? FlightTime {
			get { return FlightTimeTicks.HasValue ? TimeSpan.FromTicks(FlightTimeTicks.Value) : (TimeSpan?) null; }
		}

		public DateTime? NullifiedOn { get; set; }
	}
}
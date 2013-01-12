using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {

	[DisplayColumn("Name")]
	public class Connection {
		[KeyAttribute]
		public int ID { get; set; }
		public int FlightNumber { get; set; }
		public string Prefix { get; set; }

		[NotMappedAttribute]
		public string Name {
			get {
				return Prefix + " " + FlightNumber;
			}
		
		}

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

		public TimeSpan DepTimeUtc(DateTime day) {
			
			if(Departure.TimeZoneName != null) {

				DateTime dummyDate = new DateTime(day.Ticks, DateTimeKind.Unspecified).Date;
				DateTime dateAndTimeInUtc = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dummyDate.Add(DepTimeLocal), Departure.TimeZoneName, "UTC");
					
				return dateAndTimeInUtc.TimeOfDay;
			} else {
				return DepTimeLocal;
			}
		}

		
		public TimeSpan DepTimeLocal { get; set; }

		public TimeSpan ArrTimeUtc(DateTime day) {

			if (Destination.TimeZoneName != null) {
				DateTime dummyDate = new DateTime(day.Ticks, DateTimeKind.Unspecified).Date;
				DateTime dateAndTimeInUtc = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dummyDate.Add(ArrTimeLocal), Destination.TimeZoneName, "UTC");

				return dateAndTimeInUtc.TimeOfDay;
			} else {
				return ArrTimeLocal;
			}			
		}

		public TimeSpan ArrTimeLocal { get; set; }
		
		public DateTime CreatedOn { get; set; }
		public bool Monday { get; set; }
		public bool Tuesday { get; set; }
		public bool Wednesday { get; set; }
		public bool Thursday { get; set; }
		public bool Friday { get; set; }
		public bool Saturday { get; set; }
		public bool Sunday { get; set; }

		public DateTime? StartsOn { get; set; }
		public DateTime? EndsOn { get; set; }

		public TimeSpan FlightTime(DateTime day) {
			return ArrTimeUtc(day) - DepTimeUtc(day) < TimeSpan.Zero ? (ArrTimeUtc(day) - DepTimeUtc(day)).Add(new TimeSpan(24, 0, 0)) : ArrTimeUtc(day) - DepTimeUtc(day);
		}

		public bool PlusOneDay(DateTime day) {
			return DepTimeUtc(day) > ArrTimeUtc(day);
		}

	}
}
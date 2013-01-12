using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using LHFS.Resources;

namespace LHFS.Views.Connections.ViewModel {
	public class SearchModel {
		[Display(ResourceType = typeof(Schedule), Name = "Flight")]
		public int? FlightNumber { get; set; }
		[Display(ResourceType = typeof(Schedule), Name = "DepartureAirport")]
		public string DepartureAirport { get; set; }
		[Display(ResourceType = typeof(Schedule), Name = "DestinationAirport")]
		public string DestinationAirport { get; set; }
		public IEnumerable<Airport> PossibleDepartureAirports { get; set; }
		public IEnumerable<Airport> PossibleDestinationAirports { get; set; }
		public IEnumerable<Connection> Connections { get; set; }
		[Display(ResourceType = typeof(Schedule), Name = "AircraftType")]
		public string[] AircraftTypeIDs { get; set; }
		public MultiSelectList PossibleAircraftTypes { get; set; }
		public bool WithDepTime { get; set; }
		public bool WithMaxDuration { get; set; }
		[Display(ResourceType = typeof(Schedule), Name = "Airline")]
		public string[] AirlineIDs { get; set; }
		public List<SelectListItem> PossibleAirlines { get; set; }
		public string StartTime { get; set; }
		public string Duration { get; set; }
		[Display(ResourceType = typeof(Schedule), Name = "Date")]
		public DateTime Date { get; set; }
		public bool ShowReverseButton { get; set; }
	}
}
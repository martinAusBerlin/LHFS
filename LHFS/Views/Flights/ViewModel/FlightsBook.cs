using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models;
using System.Web.Mvc;

namespace LHFS.Views.Flights.ViewModel {
	public class FlightsBook {
		public List<SelectListItem> PossibleRotations { get; set; }
		// TODO
		public List<SelectListItem> PossibleAircraftTypes { get; set; }
		public string AircraftTypeICAO { get; set; }
		public int? RotationID { get; set; }
		public Connection Connection { get; set; }
		public string NewRotationName { get; set; }
		public int Option { get; set; }
		
		// TODO
		public DateTime Date { 
			get {
			return DateTime.Now;	
			}
		}
	}
}
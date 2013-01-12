using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models;

namespace LHFS.Views.AircraftTypes.ViewModel {
	public class OverviewModel {
		public IEnumerable<AircraftType> Long { get; set; }
		public IEnumerable<AircraftType> Medium { get; set; }
		public IEnumerable<AircraftType> Short { get; set; }
		public IEnumerable<AircraftType> Cargo { get; set; }
	}
}
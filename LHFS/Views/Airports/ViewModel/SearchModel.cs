using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models;

namespace LHFS.Views.Airports.ViewModel {
	public class SearchModel {
		public IEnumerable<Region> Regions { get; set; }
		//public IQueryable<IGrouping<Region, Airport>> AirportsByRegions { get; set; }
		public string Pattern { get; set; }
	}
}
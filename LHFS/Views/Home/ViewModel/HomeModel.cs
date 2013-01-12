using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models;

namespace LHFS.Views.Home.ViewModel {
	public class HomeModel {
		public IEnumerable<Flight> BookedFlights { get; set; }
		public Rank CurrentRank { get; set; }
	}
}
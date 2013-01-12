using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models;
using System.Web.Mvc;

namespace LHFS.Views.Airports.ViewModel {
	public class AddChartModel {
		public AirportChart AirportChart { get; set; }
		public IQueryable<ChartType> PossibleChartTypes { get; set; }
	}
}
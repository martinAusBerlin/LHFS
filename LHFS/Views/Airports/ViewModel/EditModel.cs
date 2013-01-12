using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models;
using System.Web.Mvc;

namespace LHFS.Views.Airports.ViewModel {
	public class EditModel {
		public AirportVersion CurrentVersion { get; set; }
		public IQueryable<Country> PossibleCountry { get; set; }
		public List<SelectListItem> PossibleCategory { get; set; }
		
		
	}
}
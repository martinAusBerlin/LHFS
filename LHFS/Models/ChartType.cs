using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class ChartType {
		[KeyAttribute]
		public string Key { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
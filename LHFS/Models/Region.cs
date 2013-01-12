using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class Region {
		[KeyAttribute]
		public int ID { get; set; }
		public string English { get; set; }
		public string German { get; set; }
		public string SmallIconPath { get; set; }
	}
}
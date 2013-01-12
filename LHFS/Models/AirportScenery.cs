using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class AirportScenery {
		[KeyAttribute]
		public int ID { get; set; }

		public string AirportID { get; set; }
		[ForeignKey("AirportID")]
		public virtual Airport Airport { get; set; }

		public string Publisher { get; set; }
		public string Link { get; set; }
		public string Text { get; set; }
		public bool IsHidden { get; set; }
	}
}
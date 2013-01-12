using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class Rotation {
		[KeyAttribute]
		public int ID { get; set; }

		public int UserID { get; set; }
		[ForeignKey("UserID")]
		public virtual User User { get; set; }
		public string Name { get; set; }

		public virtual ICollection<Flight> Flights { get; set; }
	}
}
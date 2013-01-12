using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	[DisplayColumn("English")]
	public class Country {
		[KeyAttribute, MaxLength(2)]
		public string ISO { get; set; }
		[Required,MaxLength(128)]
		public string English { get; set; }
		public string German { get; set; }

		[ForeignKey("Region")]
		public int? RegionID { get; set; }
		public virtual Region Region { get; set; }

		public virtual ICollection<User> Users { get; set; }
	}
}
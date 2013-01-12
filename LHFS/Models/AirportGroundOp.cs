using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class AirportGroundOp : IVersionedContent {
		[KeyAttribute]
		public int ID { get; set; }
		[MaxLength(10000)]
		public string Text { get; set; }
		public int CreatedByUserID { get; set; }
		[ForeignKey("CreatedByUserID")]
		public User CreatedByUser { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? ApprovedOn { get; set; }

		public int? ApprovedByUserID { get; set; }
		[ForeignKey("ApprovedByUserID")]
		public User ApprovedByUser { get; set; }
		public int VersionNumber { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LHFS.Models.Interfaces {
	public interface IVersionedContent {
		int CreatedByUserID { get; set; }
		User CreatedByUser { get; set; }
		DateTime CreatedOn { get; set; }
		DateTime? ApprovedOn { get; set; }
		int? ApprovedByUserID { get; set; }
		User ApprovedByUser { get; set; }
		int VersionNumber { get; set; }
	}
}

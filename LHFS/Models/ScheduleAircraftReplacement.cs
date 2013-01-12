using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class ScheduleAircraftReplacement {
		[KeyAttribute]
		public int ID { get; set; }
		public bool IsActive { get; set; }
		public string Source { get; set; }
		
		public string DirectReplacementID { get; set; }
		[ForeignKey("DirectReplacementID")]
		public virtual AircraftType AircraftType { get; set; }

		public bool AskTheWeb { get; set; }
	}
}
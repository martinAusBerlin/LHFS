using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LHFS.Models.Interfaces;

namespace LHFS.Models {
	
	[DisplayColumn("Registration")]
	public class Aircraft : IVersionedContent {

		[KeyAttribute]
		public int ID { get; set; }

		
		public string AircraftTypeID { get; set; }
		[ForeignKey("AircraftTypeID")]
		public virtual AircraftType AircraftType { get; set; }
		
		public string Registration { get; set; }
		public string ExactType { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		
		public string AirportID { get; set; }
		[ForeignKey("AirportID")]
		public virtual Airport HomeBase { get; set; }

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
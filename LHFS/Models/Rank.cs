using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	[DisplayColumn("Title")]
	public class Rank {

		[KeyAttribute]
		public int ID { get; set; }
		public string Title { get; set; }
		public string Shorttitle { get; set; }
		public int HourLimit { get; set; }
		public int VacationWeeks { get; set; }
		public int NumberOfTyperatings { get; set; }
		public int MinNumberOfFlightsPerMonth { get; set; }
		public string SmallRankIconPath { get; set; }

		public virtual ICollection<User> Users { get; set; }

	}
}
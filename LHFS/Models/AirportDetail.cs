using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class AirportDetail : IVersionedContent {
		[KeyAttribute]
		public int ID { get; set; }
		public int Elevation { get; set; }
		public decimal Lat { get; set; }
		public decimal Lon { get; set; }
		public decimal MagVar { get; set; }
		public string Category { get; set; }
		public int LongestRunway { get; set; }

		public int CreatedByUserID { get; set; }
		[ForeignKey("CreatedByUserID")]
		public User CreatedByUser { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime? ApprovedOn { get; set; }

		public int? ApprovedByUserID { get; set; }
		[ForeignKey("ApprovedByUserID")]
		public User ApprovedByUser { get; set; }
		public int VersionNumber { get; set; }

		public override bool Equals(Object obj) {
			AirportDetail detail = obj as AirportDetail;
			if (detail == null)
				return false;
			else
				return
					detail.ID.Equals(ID) &&
					detail.Elevation.Equals(Elevation) &&
					detail.Lat.Equals(Lat) &&
					detail.Lon.Equals(Lon) &&
					string.Equals(detail.Category, Category) &&
					detail.LongestRunway.Equals(LongestRunway) &&
					detail.CreatedByUserID.Equals(CreatedByUserID) &&
					DateTime.Equals(detail.CreatedOn, CreatedOn) &&
					DateTime.Equals(detail.ApprovedOn, ApprovedOn) &&
					detail.ApprovedByUserID.Equals(ApprovedByUserID) &&
					detail.VersionNumber.Equals(VersionNumber);
		}

		public override int GetHashCode() {
			unchecked // Overflow is fine, just wrap
			{
				int hash = 17;

				hash = hash * 23 + ID.GetHashCode();
				hash = hash * 23 + Elevation.GetHashCode();
				hash = hash * 23 + Lat.GetHashCode();
				hash = hash * 23 + Lon.GetHashCode();
				hash = hash * 23 + Lat.GetHashCode();
				hash = hash * 23 + Category.GetHashCode();
				hash = hash * 23 + LongestRunway.GetHashCode();
				hash = hash * 23 + CreatedByUserID.GetHashCode();
				hash = hash * 23 + CreatedOn.GetHashCode();
				hash = hash * 23 + ApprovedOn.GetHashCode();
				hash = hash * 23 + ApprovedByUserID.GetHashCode();
				hash = hash * 23 + VersionNumber.GetHashCode();

				return hash;
			}
		}
	}
}
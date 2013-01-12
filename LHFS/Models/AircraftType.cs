using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	[DisplayColumn("Model")]
	public class AircraftType {

		[KeyAttribute, MaxLength(4)]
		public string ICAO { get; set; }
		public string Manufacturer { get; set; }
		public string Model { get; set; }
		public string TypeImageUrl { get; set; }
		public string SmallTypeImageUrl { get; set; }

		public int? SortingOrder { get; set; }
		public int? TypeGroup { get; set; }

		[NotMappedAttribute]
		public string Longname {
			get {
				if (string.IsNullOrEmpty(Model)) {
					return ICAO;
				} else {
					return Manufacturer + " " + Model;
				}
			}
		}

		public int? DivisionID { get; set; }
		[ForeignKey("DivisionID")]
		public virtual Division Division { get; set; }

		public int? TypeRatingID { get; set; }
		[ForeignKey("TypeRatingID")]
		public virtual TypeRating TypeRating { get; set; }
		
		public int? SeatingCapacity { get; set; }
		public int? RangeInNm { get; set; }
		public string Length { get; set; }
		public string Wingspan { get; set; }
		public string Height { get; set; }
		public string Engines { get; set; }
		public string Thrust { get; set; }
		public string EconCruiseSpeed { get; set; }
		public string MaxCruiseAlt { get; set; }
		public int? Dow { get; set; }
		public int? Mtow { get; set; }
		public int? Mldw { get; set; }
		public string Addon { get; set; }
		public int? Quantity { get; set; }

		public virtual ICollection<Aircraft> Aircrafts { get; set; }

	}
}
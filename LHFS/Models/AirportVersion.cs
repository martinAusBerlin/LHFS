using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class AirportVersion {
		[KeyAttribute]
		public int ID { get; set; }
		[Required]
		public DateTime CreatedOn { get; set; }

		//[Required]
		public string AirportICAO { get; set; }
		[ForeignKey("AirportICAO")]
		public virtual Airport Airport { get; set; }
		
		
		public int AirportDepartureID { get; set; }
		[ForeignKey("AirportDepartureID")]
		public virtual AirportDeparture Departure { get; set; }
		
		
		public int AirportArrivalID { get; set; }
		[ForeignKey("AirportArrivalID")]
		public virtual AirportArrival Arrival { get; set; }
		
		
		public int AirportGroundOpID { get; set; }
		[ForeignKey("AirportGroundOpID")]
		public virtual AirportGroundOp GroundOp { get; set; }
		
		
		public int AirportDetailID { get; set; }
		[ForeignKey("AirportDetailID")]
		public virtual AirportDetail Detail { get; set; }
		
		
		public int AirportTerrainID { get; set; }
		[ForeignKey("AirportTerrainID")]
		public virtual AirportTerrain Terrain { get; set; }

		public int AirportHazardID { get; set; }
		[ForeignKey("AirportHazardID")]
		public virtual AirportHazard Hazard { get; set; }
	}
}
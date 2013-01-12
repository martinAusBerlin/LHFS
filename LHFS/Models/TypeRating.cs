using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {

	public class TypeRating {

		[KeyAttribute]
		public int ID { get; set; }
		public string Title { get; set; }
		public virtual ICollection<AircraftType> AircraftTypes { get; set; }

	
	}
}
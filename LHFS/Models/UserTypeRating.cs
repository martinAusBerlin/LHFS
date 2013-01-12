using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {

	public class UserTypeRating {
		[KeyAttribute]
		public int ID { get; set; }
		public DateTime ValidFrom { get; set; }
		public DateTime? ValidTo { get; set; }

		public int UserID { get; set; }
		[ForeignKey("UserID")]
		public virtual User User { get; set; }
		
		public int TypeRatingID { get; set; }
		[ForeignKey("TypeRatingID")]
		public virtual TypeRating TypeRating { get; set; }

	}

}
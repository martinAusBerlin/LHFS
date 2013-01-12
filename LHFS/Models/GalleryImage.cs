using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Models {
	public class GalleryImage {

		[KeyAttribute]
		public int ID { get; set; }
		public string Url { get; set; }
		public string Title { get; set; }
		public DateTime CreatedOn { get; set; }
		public int NumberOfViews { get; set; }
		
		public int UserID { get; set; }
		[ForeignKey("UserID")]
		public virtual User User { get; set; }		

	}
}
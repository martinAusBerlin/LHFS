using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using LHFS.Resources;

namespace LHFS.Models {
	public class AirportChart {
		[KeyAttribute]
		public int ID { get; set; }

		[Required]
		[Display(ResourceType = typeof(AirportChartResource), Name = "Link")]
		[RegularExpression(@"^(http|ftp|https|www)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?$", ErrorMessageResourceType = typeof(AirportChartResource), ErrorMessageResourceName="NotALink")]
		public string Link { get; set; }
		[Required]
		[Display(ResourceType = typeof(AirportChartResource), Name = "Title")]
		public string Title { get; set; }

		public string ChartTypeKey { get; set; }
		[ForeignKey("ChartTypeKey")]
		[Display(ResourceType = typeof(AirportChartResource), Name = "ChartType")]
		public virtual ChartType ChartType { get; set; }

		[Required]
		public string AirportICAO { get; set; }
		[ForeignKey("AirportICAO")]
		public virtual Airport Airport { get; set; }
	}
}
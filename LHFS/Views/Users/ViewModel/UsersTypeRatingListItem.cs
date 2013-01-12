using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Utils;

namespace LHFS.Views.Users.ViewModel {
	public class UsersTypeRatingListItem {

		public int TypeRatingID { get; set; }
		public int? UserTypeRatingID { get; set; }
		public string Title { get; set; }
		public DateTime? ValidFrom { get; set; }
		public bool Enabled {
			get {
				bool enabled;

				if(ValidFrom.HasValue) {
					return Extensions.CanBeReleased(ValidFrom.Value);
				} else {
					enabled = true;
				}

				return enabled;
			}
		}

		public int? DaysSinceChange {
			get {
				int? days;

				if (ValidFrom.HasValue) {
					days = (int) (DateTime.UtcNow.Date - ValidFrom.Value.Date).TotalDays;
				} else {
					days = null;
				}

				return days;
			}
		}
	}
}
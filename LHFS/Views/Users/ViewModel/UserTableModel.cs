using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LHFS.Models;
using LHFS.Helpers;
using LHFS.Validation;
using System.ComponentModel.DataAnnotations;
using LHFS.Resources;

namespace LHFS.Views.Users.ViewModel
{
    public class UserTableModel
    {
		public int ID { get; set; }
		public string Fullname { get; set; }
		public string CountryISO { get; set; }
		public Country Country { get; set; }
		public Rank Rank { get; set; }
		public int? VatsimID { get; set; }
		public int? IvaoID { get; set; }
		public Airport HomeBase { get; set; }
		public int NumberOfFlights { get; set; }
		public decimal? FlightTime {
			get {
				return _flightTimeTicks.HasValue ? (decimal?)TimeSpan.FromTicks(_flightTimeTicks.Value).TotalHours : (decimal?) null;
			}
		}

		private Int64? _flightTimeTicks;
		public Int64? FlightTimeTicks {
			set {
				_flightTimeTicks = value;
			}
		}


		public int NumberOfContributions { get; set; }
		public DateTime MemberSince { get; set; }
		public UserStatus Status { get; set; }
		public IEnumerable<UserTypeRating> UserTypeRatings { get; set; }
		public long ActivityIndex { get; set; }
		public DateTime? LastFlightOn { get; set; }
		public DateTime? OffDutySince { get; set; }
		public string Color { get; set; }
		public string TextColor { get; set; }
		public string Forename { get; set; }
		public string Surname { get; set; }
    }
}

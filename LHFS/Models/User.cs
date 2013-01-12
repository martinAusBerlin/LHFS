using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using LHFS.Resources;
using LHFS.Validation;

namespace LHFS.Models {

	[DisplayColumn("FullnameAndID")]
	public class User {
		[Key]
		public int ID { get; set; }
		public string Forename { get; set; }
		public string Surname { get; set; }
		[Display(ResourceType=typeof(UserResource),Name="Birthday")]
		public DateTime Birthday { get; set; }

		[EmailAddress]
		public string Mail { get; set; }

		[NotMapped]
		public string FullnameAndID {
			get {
				return Forename + " " + Surname + " (" + ID + ")";
			}
		}

		[NotMapped]
        public string Fullname
        {
            get
            {
				return GetFullName(Forename, Surname);
            }
        }

		public static string GetFullName(string forename, string surname) {
			return forename + " " + surname;
		}


		public string CountryISO { get; set; }
		[ForeignKey("CountryISO")]
		public virtual Country Country { get; set; }
		
		public string City { get; set; }
		public bool IsAdministrator { get; set; }
		public string Password { get; set; }

		
		public int RankID { get; set; }
		[ForeignKey("RankID")]
		public virtual Rank Rank { get; set; }

		
		public string AirportICAO { get; set; }
		[ForeignKey("AirportICAO")]
		public virtual Airport HomeBase { get; set; }

		public int DefaultLanguageID { get; set; }
		public bool ReceiveNewsletter { get; set; }
		public bool HideEmail { get; set; }
		public bool ShowGallery { get; set; }
		public bool HideProfile { get; set; }
		public int? VatsimID { get; set; }
		public int? IvaoID { get; set; }
		public string RealPilotLicense { get; set; }
		public string Comments { get; set; }

		public bool CareerPathOption { get; set; }

		public DateTime CreatedOn { get; set; }
		public DateTime? ApprovedOn { get; set; }
		public DateTime? MembershipEndedOn { get; set; }
		public DateTime? OffDutySince { get; set; }
		public DateTime? LastVacationPeriodStartsOn { get; set; }

		[NotMapped]
		public DateTime? LastFlightOn {
			get {
				return Flights != null && Flights.Any() ? (DateTime?)Flights.Where(t => t.PerformedOn.HasValue && t.ApprovedOn.HasValue).Max(t => t.PerformedOn) : null;
			}
		}

		[NotMapped]
		public DateTime? LastVacationPeriodEndsOn {
			get {

				if (LastVacationPeriodStartsOn.HasValue) {
					return LastVacationPeriodStartsOn.Value.AddDays(7 * Rank.VacationWeeks);
				} else {
					return null;
				}
			}
		}



		[NotMapped]
		public UserStatus Status {
			get {

				UserStatus currentStatus = GetUserStatus(OffDutySince, LastFlightOn);

				return currentStatus;
			}
		}

		public static UserStatus GetUserStatus(DateTime? offDutySince, DateTime? lastFlightOn) {
			UserStatus currentStatus;

			if (!offDutySince.HasValue && lastFlightOn.HasValue && lastFlightOn.Value.AddMonths(1) > DateTime.Now) {
				currentStatus = UserStatus.Active;
			} else if (!offDutySince.HasValue && lastFlightOn.HasValue && lastFlightOn.Value.AddMonths(1).AddDays(7) > DateTime.Now) {
				currentStatus = UserStatus.Inactive;
			} else if (!offDutySince.HasValue && (!lastFlightOn.HasValue || lastFlightOn.HasValue && lastFlightOn.Value.AddMonths(2) > DateTime.Now)) {
				currentStatus = UserStatus.Expired;
			} else if (offDutySince.HasValue) {
				currentStatus = UserStatus.OffDuty;
			} else {
				throw new NotImplementedException("Status unknown.");
			}
			return currentStatus;
		}

		public virtual ICollection<UserTypeRating> UserTypeRatings { get; set; }
		[ForeignKey("UserID")]
		public virtual ICollection<Flight> Flights { get; set; }
		public virtual ICollection<Route> Routes { get; set; }
		public virtual ICollection<GalleryImage> GalleryImages { get; set; }

		public virtual ICollection<AirportTerrain> AirportTerrains { get; set; }
		public virtual ICollection<AirportScenery> AirportSceneries { get; set; }
		public virtual ICollection<AirportHazard> AirportHazards { get; set; }
		public virtual ICollection<AirportGroundOp> AirportGroundOps { get; set; }
		public virtual ICollection<AirportDetail> AirportDetails { get; set; }
		public virtual ICollection<AirportDeparture> AirportDepartures { get; set; }
		public virtual ICollection<AirportArrival> AirportArrivals { get; set; }

	}

	public enum UserStatus {
		Active,
		OffDuty,
		Inactive,
		Expired,
		
	}
}
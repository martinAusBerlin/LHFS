using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using LHFS.Models;
using LHFS.Views.Users.ViewModel;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace LHFS.Utils {
	
	public static class Extensions {
		public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate) {
			if (condition)
				return source.Where(predicate);
			else
				return source;
		}

		public static bool CanBeReleased(this UserTypeRating usertypeRating) {
			return CanBeReleased(usertypeRating.ValidFrom);
		}

		public static bool CanBeReleased(DateTime validFrom) {
			bool releaseable;

			if (validFrom.Date.AddDays(49) > DateTime.UtcNow.Date) {
				releaseable = false;
			} else {
				releaseable = true;
			}

			return releaseable;
		}

		public static string ToShortMonthName(this DateTime dateTime) {
			return CultureInfo.CurrentUICulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
		}

	}

	
}
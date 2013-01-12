using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LHFS.Validation
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public class EmailAddressAttribute : RegularExpressionAttribute, IClientValidatable {

		public EmailAddressAttribute()
			: base("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$")
		{
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) {
			yield return new EmailValidationRule(ErrorMessage);
		}
	}

	public class EmailValidationRule : ModelClientValidationRule {
		public EmailValidationRule(string errorMessage) {
			ErrorMessage = errorMessage;
			ValidationType = "email";
		}
	}
}

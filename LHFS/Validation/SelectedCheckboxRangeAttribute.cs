using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using LHFS.Validation;

namespace LHFS.Validation
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
	public class SelectedCheckboxRangeAttribute : ValidationAttribute, IClientValidatable {

		public SelectedCheckboxRangeAttribute(int min, int max) {
			Max = max;
			Min = min;
		}

		public SelectedCheckboxRangeAttribute(int max)
			: this(0, max) {
		}

		public int Max { get; set; }
		public int Min { get; set; }

		public override bool IsValid(object value) {
			if (value == null) {
				return true;
			}

			var array = (string[])value;

			if (array.Length > Max || array.Length < Min) {
				return false;
			}

			return true;
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) {
			var rule = new SelectedCheckboxRangeValidationRule("Something went completely wrong.", this.Min, this.Max);
			yield return rule;
		}
	}

	public class SelectedCheckboxRangeValidationRule : ModelClientValidationRule {

		public SelectedCheckboxRangeValidationRule(string errorMessage, int min, int max) {
			ErrorMessage = errorMessage;
			ValidationType = "selectedcheckboxrange";
			ValidationParameters.Add("min", min);
			ValidationParameters.Add("max", max);
		}
	}

}

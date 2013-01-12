using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LHFS.Models;
using System.Web.Mvc;
using System.ComponentModel;
using System.Web.Routing;
using System.Linq.Expressions;
using System.Web.Mvc.Ajax;
using LHFS.Views.Users.ViewModel;
using System.Threading;

namespace LHFS.Helpers {

    
	public static class HtmlHelpers {

		/// <summary>
		/// ActionLinkUI.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="action">The action.</param>
		/// <returns>ActionLink string</returns>
		public static MvcHtmlString ActionLinkUI<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string action) {
			return ActionLinkUI(htmlHelper, expression, action, null);
		}

		/// <summary>
		/// ActionLinkUI.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="action">The action.</param>
		/// <param name="icon">The icon.</param>
		/// <returns>ActionLink string</returns>
		public static MvcHtmlString ActionLinkUI<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string action, string icon) {
			return ActionLinkUI(htmlHelper, expression, action, icon, null);
		}

		/// <summary>
		/// ActionLinkUI.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TValue">The type of the value.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="action">The action.</param>
		/// <param name="icon">The icon.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns>ActionLink string</returns>
		public static MvcHtmlString ActionLinkUI<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string action, string icon, object htmlAttributes) {
			string controllerName = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
			ModelMetadata metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

			TagBuilder a = new TagBuilder("a");
			switch (action) {
				case "Index":
					a.Attributes.Add("href", String.Format("/{0}", controllerName));
					a.Attributes.Add("title", "Back to list");
					break;

				case "Create":
					a.Attributes.Add("href", String.Format("/{0}/{1}", controllerName, action));
					a.Attributes.Add("title", "Create new");
					break;
				default:
					a.Attributes.Add("href", String.Format("/{0}/{1}/{2}", controllerName, action, metaData.Model));
					a.Attributes.Add("title", action);
					break;
			}


			a.AddCssClass("actionLinkUI");
			a.AddCssClass("ui-widget");
			a.AddCssClass("ui-state-default");
			a.AddCssClass("ui-corner-all");

			TagBuilder span = new TagBuilder("span");
			span.AddCssClass("ui-icon");
			span.AddCssClass(icon);
			span.InnerHtml = action;
			a.InnerHtml = span.ToString(TagRenderMode.Normal);

			if (htmlAttributes != null) {
				a.MergeAttributes(new RouteValueDictionary(htmlAttributes));
			}

			return MvcHtmlString.Create(a.ToString(TagRenderMode.Normal));
		}

        public static MvcHtmlString PropertyNameFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            return PropertyNameHelper(html,
                               ModelMetadata.FromLambdaExpression(expression, html.ViewData),
                               ExpressionHelper.GetExpressionText(expression));
        }

        internal static MvcHtmlString PropertyNameHelper(HtmlHelper html, ModelMetadata metadata, string htmlFieldName)
        {
            string text = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(text))
            {
                return MvcHtmlString.Empty;
            }

            MvcHtmlString textToRender = new MvcHtmlString(text);
            return textToRender;
        }

		public static MvcHtmlString GetWeekdayDisplay(this Connection item) {

			string weekdays = String.Empty;

			var current = Thread.CurrentThread.CurrentUICulture.DateTimeFormat;

			weekdays += "<table class=\"weekdays\"><tr>";

			weekdays += "<td title=\"" + current.GetDayName(DayOfWeek.Monday) + "\">" + (item.Monday ? current.GetAbbreviatedDayName(DayOfWeek.Monday).First().ToString() : "&nbsp;") + "</td>";
			weekdays += "<td title=\"" + current.GetDayName(DayOfWeek.Tuesday) + "\">" + (item.Tuesday ? current.GetAbbreviatedDayName(DayOfWeek.Tuesday).First().ToString() : "&nbsp;") + "</td>";
			weekdays += "<td title=\"" + current.GetDayName(DayOfWeek.Wednesday) + "\">" + (item.Wednesday ? current.GetAbbreviatedDayName(DayOfWeek.Wednesday).First().ToString() : "&nbsp;") + "</td>";
			weekdays += "<td title=\"" + current.GetDayName(DayOfWeek.Thursday) + "\">" + (item.Thursday ? current.GetAbbreviatedDayName(DayOfWeek.Thursday).First().ToString() : "&nbsp;") + "</td>";
			weekdays += "<td title=\"" + current.GetDayName(DayOfWeek.Friday) + "\">" + (item.Friday ? current.GetAbbreviatedDayName(DayOfWeek.Friday).First().ToString() : "&nbsp;") + "</td>";
			weekdays += "<td title=\"" + current.GetDayName(DayOfWeek.Saturday) + "\">" + (item.Saturday ? current.GetAbbreviatedDayName(DayOfWeek.Saturday).First().ToString() : "&nbsp;") + "</td>";
			weekdays += "<td title=\"" + current.GetDayName(DayOfWeek.Sunday) + "\">" + (item.Sunday ? current.GetAbbreviatedDayName(DayOfWeek.Sunday).First().ToString() : "&nbsp;") + "</td>";

			weekdays += "</tr></table>";

			return new MvcHtmlString(weekdays);

		}

		public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty[]>> expression, MultiSelectList multiSelectList, object htmlAttributes = null) {
			//Derive property name for checkbox name
			MemberExpression body = expression.Body as MemberExpression;
			string propertyName = body.Member.Name;

			//Get currently select values from the ViewData model
			TProperty[] list = expression.Compile().Invoke(htmlHelper.ViewData.Model);

			//Convert selected value list to a List<string> for easy manipulation
			List<string> selectedValues = new List<string>();

			if (list != null) {
				selectedValues = new List<TProperty>(list).ConvertAll<string>(delegate(TProperty i) { return i.ToString(); });
			}

			//Create div
			TagBuilder divTag = new TagBuilder("div");
			divTag.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

			//Add checkboxes
			foreach (SelectListItem item in multiSelectList) {
				divTag.InnerHtml += String.Format("<div><input type=\"checkbox\" name=\"{0}\" id=\"{0}_{1}\" " +
													"value=\"{1}\" {2} /><label for=\"{0}_{1}\">{3}</label></div>",
													propertyName,
													item.Value,
													selectedValues.Contains(item.Value) ? "checked=\"checked\"" : "",
													item.Text);
			}

			return MvcHtmlString.Create(divTag.ToString());
		}

		public static MvcHtmlString TypeRatingCheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty[]>> expression, IEnumerable<UsersTypeRatingListItem> typeRatingCheckboxListItems, string localisedDescription, object htmlAttributes = null) {
			//Derive property name for checkbox name
			MemberExpression body = expression.Body as MemberExpression;
			string propertyName = body.Member.Name;

			//Get currently select values from the ViewData model
			TProperty[] list = expression.Compile().Invoke(htmlHelper.ViewData.Model);

			//Convert selected value list to a List<string> for easy manipulation
			List<string> selectedValues = new List<string>();

			if (list != null) {
				selectedValues = new List<TProperty>(list).ConvertAll<string>(delegate(TProperty i) { return i.ToString(); });
			}

			//Create div
			TagBuilder divTag = new TagBuilder("div");
			divTag.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);

			//Add checkboxes
			foreach (var item in typeRatingCheckboxListItems) {


				//if () {

					TagBuilder innerDivTag = new TagBuilder("div");

					TagBuilder inputTag = new TagBuilder("input");
					inputTag.Attributes.Add("type", "checkbox");
					inputTag.Attributes.Add("name", propertyName);
					inputTag.Attributes.Add("id", string.Format("{0}_{1}", propertyName, item.TypeRatingID));
					inputTag.Attributes.Add("value", item.TypeRatingID.ToString());
				
					if (selectedValues.Contains(item.TypeRatingID.ToString())) {
						inputTag.Attributes.Add("checked", "checked");
					}

					if (!item.Enabled) {
						inputTag.Attributes.Add("disabled", "disabled");
					}

					ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
					IDictionary<string, object> validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(ExpressionHelper.GetExpressionText(expression), metadata);

					inputTag.MergeAttributes(validationAttributes);

					TagBuilder labelTag = new TagBuilder("label");
					labelTag.Attributes.Add("for", string.Format("{0}_{1}", propertyName, item.TypeRatingID));
					labelTag.InnerHtml = item.Title;

					if (item.UserTypeRatingID.HasValue) {
						labelTag.InnerHtml += " <em>" + string.Format(localisedDescription, item.DaysSinceChange) + "</em>";
					}

					divTag.InnerHtml += innerDivTag.ToString(TagRenderMode.StartTag);
					divTag.InnerHtml += inputTag.ToString(TagRenderMode.SelfClosing);

					if (!item.Enabled) {
						TagBuilder hiddenInputTag = new TagBuilder("input");
						hiddenInputTag.Attributes.Add("type", "hidden");
						hiddenInputTag.Attributes.Add("name", propertyName);
						hiddenInputTag.Attributes.Add("value", item.TypeRatingID.ToString());

						divTag.InnerHtml += hiddenInputTag.ToString(TagRenderMode.SelfClosing);
					}

					divTag.InnerHtml += labelTag.ToString(TagRenderMode.Normal);
					divTag.InnerHtml += innerDivTag.ToString(TagRenderMode.EndTag);

				


					//divTag.InnerHtml += String.Format("<div><input type=\"checkbox\" name=\"{0}\" id=\"{0}_{1}\" " +
					//                                    "value=\"{1}\" {2}/><label for=\"{0}_{1}\">{3}</label></div>",
					//                                    propertyName,
					//                                    item.TypeRatingID,
					//                                    selectedValues.Contains(item.TypeRatingID.ToString()) ? "checked=\"checked\"" : "",
					//                                    item.Title);

				//} else {

				//    divTag.InnerHtml += String.Format("<div><input type=\"checkbox\" name=\"{0}\" id=\"{0}_{1}\" " +
				//                                        "value=\"{1}\" {2} {4}/><label for=\"{0}_{1}\">{3} <em>{5}</em></label> <input type=\"hidden\" name=\"{0}\" value=\"{1}\"/></div>",
				//                                        propertyName,
				//                                        item.TypeRatingID,
				//                                        selectedValues.Contains(item.TypeRatingID.ToString()) ? "checked=\"checked\"" : "",
				//                                        item.Title,
				//                                        "disabled=\"disabled\"",
				//                                        string.Format(localisedDescription, item.DaysSinceChange));

				//}

			}

			return MvcHtmlString.Create(divTag.ToString());
		}


		public static MvcHtmlString ImageActionLink(
			this HtmlHelper helper,
			string imageUrl,
			string altText,
			string actionName,
			string controllerName,
			object routeValues,
			object linkHtmlAttributes,
			object imgHtmlAttributes) {
			var linkAttributes = AnonymousObjectToKeyValue(linkHtmlAttributes);
			var imgAttributes = AnonymousObjectToKeyValue(imgHtmlAttributes);

			var imgBuilder = new TagBuilder("img");

			imgBuilder.MergeAttribute("src", imageUrl);
			imgBuilder.MergeAttribute("alt", altText);
			imgBuilder.MergeAttributes(imgAttributes, true);

			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);

			var linkBuilder = new TagBuilder("a");

			linkBuilder.MergeAttribute("href", urlHelper.Action(actionName, controllerName, routeValues));
			linkBuilder.MergeAttributes(linkAttributes, true);

			var text = linkBuilder.ToString(TagRenderMode.StartTag);
			text += imgBuilder.ToString(TagRenderMode.SelfClosing);
			text += linkBuilder.ToString(TagRenderMode.EndTag);

			return MvcHtmlString.Create(text);
		}


		public static IHtmlString ImageActionLink(this AjaxHelper helper, string imageUrl, string altText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions) {
			var builder = new TagBuilder("img");
			builder.MergeAttribute("src", imageUrl);
			builder.MergeAttribute("alt", altText);
			var link = helper.ActionLink("[replaceme]", actionName, controllerName, routeValues, ajaxOptions).ToHtmlString();
			return new MvcHtmlString(link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
		}

		public static IHtmlString IconActionLink(this AjaxHelper helper, string buttonCssClass, string title, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions) {
			var builder = new TagBuilder("span");
			builder.AddCssClass("ui-icon");
			builder.AddCssClass(buttonCssClass);
			builder.MergeAttribute("title", title);
			var link = helper.ActionLink("[replaceme]", actionName, controllerName, routeValues, ajaxOptions, new { @class = "ui-state-default" }).ToHtmlString();
			return new MvcHtmlString(link.Replace("[replaceme]", builder.ToString(TagRenderMode.SelfClosing)));
		}


		private static Dictionary<string, object> AnonymousObjectToKeyValue(object anonymousObject) {
			var dictionary = new Dictionary<string, object>();

			if (anonymousObject != null) {
				foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(anonymousObject)) {
					dictionary.Add(propertyDescriptor.Name, propertyDescriptor.GetValue(anonymousObject));
				}
			}

			return dictionary;
		}



	}

}
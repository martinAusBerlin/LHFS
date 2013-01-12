<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.AirportChart>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Link) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Link) %>
    <%: Html.ValidationMessageFor(model => model.Link) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Title) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Title) %>
    <%: Html.ValidationMessageFor(model => model.Title) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.ChartTypeKey) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.ChartTypeKey) %>
    <%: Html.ValidationMessageFor(model => model.ChartTypeKey) %>
</div>

<div class="editor-label">
    Airport
</div>
<div class="editor-field">
    <%: Html.DropDownListFor(model => model.AirportID, ((IEnumerable<LHFS.Models.Airport>)ViewBag.PossibleAirport).Select(option => new SelectListItem {
		Text = (option == null ? "None" : option.Name), 
        Value = option.IATA.ToString(),
        Selected = (Model != null) && (option.IATA == Model.AirportID)
    }), "Choose...") %>
	<%: Html.ValidationMessageFor(model => model.AirportID) %>
</div>

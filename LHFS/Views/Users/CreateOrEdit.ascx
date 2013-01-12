<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.User>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Forename) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Forename) %>
    <%: Html.ValidationMessageFor(model => model.Forename) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Surname) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Surname) %>
    <%: Html.ValidationMessageFor(model => model.Surname) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Birthday) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Birthday) %>
    <%: Html.ValidationMessageFor(model => model.Birthday) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.City) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.City) %>
    <%: Html.ValidationMessageFor(model => model.City) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.IsAdministrator) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.IsAdministrator) %>
    <%: Html.ValidationMessageFor(model => model.IsAdministrator) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Password) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Password) %>
    <%: Html.ValidationMessageFor(model => model.Password) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.DefaultLanguageID) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.DefaultLanguageID) %>
    <%: Html.ValidationMessageFor(model => model.DefaultLanguageID) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.ReceiveNewsletter) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.ReceiveNewsletter) %>
    <%: Html.ValidationMessageFor(model => model.ReceiveNewsletter) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.HideEmail) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.HideEmail) %>
    <%: Html.ValidationMessageFor(model => model.HideEmail) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.ShowGallery) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.ShowGallery) %>
    <%: Html.ValidationMessageFor(model => model.ShowGallery) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.HideProfile) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.HideProfile) %>
    <%: Html.ValidationMessageFor(model => model.HideProfile) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.VatsimID) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.VatsimID) %>
    <%: Html.ValidationMessageFor(model => model.VatsimID) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.IvaoID) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.IvaoID) %>
    <%: Html.ValidationMessageFor(model => model.IvaoID) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.RealPilotLicense) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.RealPilotLicense) %>
    <%: Html.ValidationMessageFor(model => model.RealPilotLicense) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Comments) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Comments) %>
    <%: Html.ValidationMessageFor(model => model.Comments) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.CreatedOn) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.CreatedOn) %>
    <%: Html.ValidationMessageFor(model => model.CreatedOn) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.ApprovedOn) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.ApprovedOn) %>
    <%: Html.ValidationMessageFor(model => model.ApprovedOn) %>
</div>

<div class="editor-label">
    Country
</div>
<div class="editor-field">
    <%: Html.DropDownListFor(model => model.CountryID, ((IEnumerable<LHFS.Models.Country>)ViewBag.PossibleCountry).Select(option => new SelectListItem {
		Text = (option == null ? "None" : option.English), 
        Value = option.ISO.ToString(),
        Selected = (Model != null) && (option.ISO == Model.CountryID)
    }), "Choose...") %>
	<%: Html.ValidationMessageFor(model => model.CountryID) %>
</div>
<div class="editor-label">
    Rank
</div>
<div class="editor-field">
    <%: Html.DropDownListFor(model => model.RankID, ((IEnumerable<LHFS.Models.Rank>)ViewBag.PossibleRank).Select(option => new SelectListItem {
		Text = (option == null ? "None" : option.Title), 
        Value = option.ID.ToString(),
        Selected = (Model != null) && (option.ID == Model.RankID)
    }), "Choose...") %>
	<%: Html.ValidationMessageFor(model => model.RankID) %>
</div>
<div class="editor-label">
    Airport
</div>
<div class="editor-field">
    <%: Html.DropDownListFor(model => model.AirportIATA, ((IEnumerable<LHFS.Models.Airport>)ViewBag.PossibleAirport).Select(option => new SelectListItem {
		Text = (option == null ? "None" : option.Name), 
        Value = option.IATA.ToString(),
		Selected = (Model != null) && (option.IATA == Model.AirportIATA)
    }), "Choose...") %>
	<%: Html.ValidationMessageFor(model => model.AirportIATA)%>
</div>

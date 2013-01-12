<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.Flight>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.FlightNumber) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.FlightNumber) %>
    <%: Html.ValidationMessageFor(model => model.FlightNumber) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.TakeoffWeight) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.TakeoffWeight) %>
    <%: Html.ValidationMessageFor(model => model.TakeoffWeight) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.LandingWeight) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.LandingWeight) %>
    <%: Html.ValidationMessageFor(model => model.LandingWeight) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.OffBlock) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.OffBlock) %>
    <%: Html.ValidationMessageFor(model => model.OffBlock) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.OnBlock) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.OnBlock) %>
    <%: Html.ValidationMessageFor(model => model.OnBlock) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.TakeOff) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.TakeOff) %>
    <%: Html.ValidationMessageFor(model => model.TakeOff) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Touchdown) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Touchdown) %>
    <%: Html.ValidationMessageFor(model => model.Touchdown) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.RouteText) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.RouteText) %>
    <%: Html.ValidationMessageFor(model => model.RouteText) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.AdditionalText) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.AdditionalText) %>
    <%: Html.ValidationMessageFor(model => model.AdditionalText) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.BookedOn) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.BookedOn) %>
    <%: Html.ValidationMessageFor(model => model.BookedOn) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.PerformedOn) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.PerformedOn) %>
    <%: Html.ValidationMessageFor(model => model.PerformedOn) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.FlightTime) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.FlightTime) %>
    <%: Html.ValidationMessageFor(model => model.FlightTime) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.NullifiedOn) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.NullifiedOn) %>
    <%: Html.ValidationMessageFor(model => model.NullifiedOn) %>
</div>

<div class="editor-label">
    User
</div>
<div class="editor-field">
    <%: Html.DropDownListFor(model => model.UserID, ((IEnumerable<LHFS.Models.User>)ViewBag.PossibleUser).Select(option => new SelectListItem {
		Text = (option == null ? "None" : option.FullnameAndID), 
        Value = option.ID.ToString(),
        Selected = (Model != null) && (option.ID == Model.UserID)
    }), "Choose...") %>
	<%: Html.ValidationMessageFor(model => model.UserID) %>
</div>
<div class="editor-label">
    ApprovedByUser
</div>
<div class="editor-field">
    <%: Html.DropDownListFor(model => model.ApprovedByUserID, ((IEnumerable<LHFS.Models.User>)ViewBag.PossibleApprovedByUser).Select(option => new SelectListItem {
		Text = (option == null ? "None" : option.FullnameAndID), 
        Value = option.ID.ToString(),
        Selected = (Model != null) && (option.ID == Model.ApprovedByUserID)
    }), "Choose...") %>
	<%: Html.ValidationMessageFor(model => model.ApprovedByUserID) %>
</div>

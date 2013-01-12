<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.AircraftType>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Manufacturer) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Manufacturer) %>
    <%: Html.ValidationMessageFor(model => model.Manufacturer) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Model) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Model) %>
    <%: Html.ValidationMessageFor(model => model.Model) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.TypeImageUrl) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.TypeImageUrl) %>
    <%: Html.ValidationMessageFor(model => model.TypeImageUrl) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.SmallTypeImageUrl) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.SmallTypeImageUrl) %>
    <%: Html.ValidationMessageFor(model => model.SmallTypeImageUrl) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.SeatingCapacity) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.SeatingCapacity) %>
    <%: Html.ValidationMessageFor(model => model.SeatingCapacity) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.RangeInNm) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.RangeInNm) %>
    <%: Html.ValidationMessageFor(model => model.RangeInNm) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Length) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Length) %>
    <%: Html.ValidationMessageFor(model => model.Length) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Wingspan) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Wingspan) %>
    <%: Html.ValidationMessageFor(model => model.Wingspan) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Height) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Height) %>
    <%: Html.ValidationMessageFor(model => model.Height) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Engines) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Engines) %>
    <%: Html.ValidationMessageFor(model => model.Engines) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Thrust) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Thrust) %>
    <%: Html.ValidationMessageFor(model => model.Thrust) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.EconCruiseSpeed) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.EconCruiseSpeed) %>
    <%: Html.ValidationMessageFor(model => model.EconCruiseSpeed) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.MaxCruiseAlt) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.MaxCruiseAlt) %>
    <%: Html.ValidationMessageFor(model => model.MaxCruiseAlt) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Dow) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Dow) %>
    <%: Html.ValidationMessageFor(model => model.Dow) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Mtow) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Mtow) %>
    <%: Html.ValidationMessageFor(model => model.Mtow) %>
</div>

<div class="editor-label">
    Division
</div>
<div class="editor-field">
    <%: Html.DropDownListFor(model => model.DivisionID, ((IEnumerable<LHFS.Models.Division>)ViewBag.PossibleDivision).Select(option => new SelectListItem {
		Text = (option == null ? "None" : option.Name), 
        Value = option.ID.ToString(),
        Selected = (Model != null) && (option.ID == Model.DivisionID)
    }), "Choose...") %>
	<%: Html.ValidationMessageFor(model => model.DivisionID) %>
</div>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.AirportAlternate>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Text) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Text) %>
    <%: Html.ValidationMessageFor(model => model.Text) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.AirportIATA) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.AirportIATA) %>
    <%: Html.ValidationMessageFor(model => model.AirportIATA) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.FlightLevel) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.FlightLevel) %>
    <%: Html.ValidationMessageFor(model => model.FlightLevel) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Description) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Description) %>
    <%: Html.ValidationMessageFor(model => model.Description) %>
</div>


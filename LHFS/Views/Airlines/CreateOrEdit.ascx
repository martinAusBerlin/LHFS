<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.Airline>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.IATA) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.IATA)%>
    <%: Html.ValidationMessageFor(model => model.IATA)%>
</div>


<div class="editor-label">
    <%: Html.LabelFor(model => model.ICAO) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.ICAO) %>
    <%: Html.ValidationMessageFor(model => model.ICAO) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Callsign) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Callsign) %>
    <%: Html.ValidationMessageFor(model => model.Callsign) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Name) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Name) %>
    <%: Html.ValidationMessageFor(model => model.Name) %>
</div>


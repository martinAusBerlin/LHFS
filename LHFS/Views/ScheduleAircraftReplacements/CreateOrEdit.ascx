<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.ScheduleAircraftReplacement>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.IsActive) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.IsActive) %>
    <%: Html.ValidationMessageFor(model => model.IsActive) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Source) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Source) %>
    <%: Html.ValidationMessageFor(model => model.Source) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.DirectReplacementID) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.DirectReplacementID) %>
    <%: Html.ValidationMessageFor(model => model.DirectReplacementID) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.AskTheWeb) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.AskTheWeb) %>
    <%: Html.ValidationMessageFor(model => model.AskTheWeb) %>
</div>


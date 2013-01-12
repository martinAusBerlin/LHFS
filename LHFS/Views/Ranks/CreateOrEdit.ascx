<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.Rank>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Title) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Title) %>
    <%: Html.ValidationMessageFor(model => model.Title) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Shorttitle) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Shorttitle) %>
    <%: Html.ValidationMessageFor(model => model.Shorttitle) %>
</div>


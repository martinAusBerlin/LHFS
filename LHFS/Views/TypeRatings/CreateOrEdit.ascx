<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.TypeRating>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Title) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Title) %>
    <%: Html.ValidationMessageFor(model => model.Title) %>
</div>


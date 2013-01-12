<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.Region>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.English) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.English) %>
    <%: Html.ValidationMessageFor(model => model.English) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.German) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.German) %>
    <%: Html.ValidationMessageFor(model => model.German) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.SmallRankIconPath) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.SmallRankIconPath) %>
    <%: Html.ValidationMessageFor(model => model.SmallRankIconPath) %>
</div>


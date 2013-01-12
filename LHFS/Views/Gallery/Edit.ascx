<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.GalleryImage>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Url) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Url) %>
    <%: Html.ValidationMessageFor(model => model.Url) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Title) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Title) %>
    <%: Html.ValidationMessageFor(model => model.Title) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.CreatedOn) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.CreatedOn) %>
    <%: Html.ValidationMessageFor(model => model.CreatedOn) %>
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

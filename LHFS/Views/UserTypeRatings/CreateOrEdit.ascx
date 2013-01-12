<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.UserTypeRating>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.ValidFrom) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.ValidFrom) %>
    <%: Html.ValidationMessageFor(model => model.ValidFrom) %>
</div>

<div class="editor-label">
    <%: Html.LabelFor(model => model.ValidTo) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.ValidTo) %>
    <%: Html.ValidationMessageFor(model => model.ValidTo) %>
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
    TypeRating
</div>
<div class="editor-field">
    <%: Html.DropDownListFor(model => model.TypeRatingID, ((IEnumerable<LHFS.Models.TypeRating>)ViewBag.PossibleTypeRating).Select(option => new SelectListItem {
		Text = (option == null ? "None" : option.Title), 
        Value = option.ID.ToString(),
        Selected = (Model != null) && (option.ID == Model.TypeRatingID)
    }), "Choose...") %>
	<%: Html.ValidationMessageFor(model => model.TypeRatingID) %>
</div>

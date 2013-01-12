<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.Rotation>" %>

<div class="editor-label">
    <%: Html.LabelFor(model => model.Name) %>
</div>
<div class="editor-field">
    <%: Html.EditorFor(model => model.Name) %>
    <%: Html.ValidationMessageFor(model => model.Name) %>
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

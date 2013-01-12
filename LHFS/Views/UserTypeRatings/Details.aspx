<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.UserTypeRating>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>UserTypeRating</legend>

    <div class="display-label">ValidFrom</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.ValidFrom) %></div>

    <div class="display-label">ValidTo</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.ValidTo) %></div>

    <div class="display-label">User</div>
    <div class="display-field"><%: (Model.User == null ? "None" : Model.User.FullnameAndID) %></div>

    <div class="display-label">TypeRating</div>
    <div class="display-field"><%: (Model.TypeRating == null ? "None" : Model.TypeRating.Title) %></div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>



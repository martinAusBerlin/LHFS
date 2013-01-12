<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.Country>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>Country</legend>

    <div class="display-label">English</div>
    <div class="display-field"><%: Model.English %></div>

    <div class="display-label">German</div>
    <div class="display-field"><%: Model.German %></div>

    <div class="display-label">Users</div>
    <div class="display-field"><%: (Model.Users == null ? "None" : Model.Users.Count.ToString()) %></div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.ISO }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>



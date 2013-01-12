<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.Country>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>Country</legend>

    <div class="display-label">English</div>
    <div class="display-field"><%: Model.English %></div>

    <div class="display-label">German</div>
    <div class="display-field"><%: Model.German %></div>

    <div class="display-label">Users</div>
    <div class="display-field"><%: (Model.Users == null ? "None" : Model.Users.Count.ToString()) %></div>
</fieldset>
<% using (Html.BeginForm()) { %>
    <p>
        <input type="submit" value="Delete" /> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
<% } %>

</asp:Content>



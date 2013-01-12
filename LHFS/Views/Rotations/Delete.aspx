<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.Rotation>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>Rotation</legend>

    <div class="display-label">User</div>
    <div class="display-field"><%: (Model.User == null ? "None" : Model.User.FullnameAndID) %></div>

    <div class="display-label">Name</div>
    <div class="display-field"><%: Model.Name %></div>

    <div class="display-label">Flights</div>
    <div class="display-field"><%: (Model.Flights == null ? "None" : Model.Flights.Count.ToString()) %></div>
</fieldset>
<% using (Html.BeginForm()) { %>
    <p>
        <input type="submit" value="Delete" /> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
<% } %>

</asp:Content>



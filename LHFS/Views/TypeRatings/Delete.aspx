<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.TypeRating>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>TypeRating</legend>

    <div class="display-label">Title</div>
    <div class="display-field"><%: Model.Title %></div>

    <div class="display-label">AircraftTypes</div>
    <div class="display-field"><%: (Model.AircraftTypes == null ? "None" : Model.AircraftTypes.Count.ToString()) %></div>
</fieldset>
<% using (Html.BeginForm()) { %>
    <p>
        <input type="submit" value="Delete" /> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
<% } %>

</asp:Content>



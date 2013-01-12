<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.TypeRating>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>TypeRating</legend>

    <div class="display-label">Title</div>
    <div class="display-field"><%: Model.Title %></div>

    <div class="display-label">AircraftTypes</div>
    <div class="display-field"><%: (Model.AircraftTypes == null ? "None" : Model.AircraftTypes.Count.ToString()) %></div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>



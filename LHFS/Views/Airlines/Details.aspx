<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.Airline>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>Airline</legend>

    <div class="display-label">ICAO</div>
    <div class="display-field"><%: Model.ICAO %></div>

    <div class="display-label">Callsign</div>
    <div class="display-field"><%: Model.Callsign %></div>

    <div class="display-label">Name</div>
    <div class="display-field"><%: Model.Name %></div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.IATA }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>



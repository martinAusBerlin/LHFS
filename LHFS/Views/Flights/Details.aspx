<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.Flight>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>Flight</legend>

    <div class="display-label">FlightNumber</div>
    <div class="display-field"><%: Model.FlightNumber %></div>

    <div class="display-label">User</div>
    <div class="display-field"><%: (Model.User == null ? "None" : Model.User.FullnameAndID) %></div>

    <div class="display-label">ApprovedByUser</div>
    <div class="display-field"><%: (Model.ApprovedByUser == null ? "None" : Model.ApprovedByUser.FullnameAndID) %></div>

    <div class="display-label">TakeoffWeight</div>
    <div class="display-field"><%: String.Format("{0:F}", Model.TakeoffWeight) %></div>

    <div class="display-label">LandingWeight</div>
    <div class="display-field"><%: String.Format("{0:F}", Model.LandingWeight) %></div>

    <div class="display-label">OffBlock</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.OffBlock) %></div>

    <div class="display-label">OnBlock</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.OnBlock) %></div>

    <div class="display-label">TakeOff</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.TakeOff) %></div>

    <div class="display-label">Touchdown</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.Touchdown) %></div>

    <div class="display-label">RouteText</div>
    <div class="display-field"><%: Model.RouteText %></div>

    <div class="display-label">AdditionalText</div>
    <div class="display-field"><%: Model.AdditionalText %></div>

    <div class="display-label">BookedOn</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.BookedOn) %></div>

    <div class="display-label">PerformedOn</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.PerformedOn) %></div>

    <div class="display-label">FlightTime</div>
    <div class="display-field"><%: Model.FlightTime %></div>

    <div class="display-label">NullifiedOn</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.NullifiedOn) %></div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>



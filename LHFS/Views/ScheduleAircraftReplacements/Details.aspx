<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.ScheduleAircraftReplacement>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>ScheduleAircraftReplacement</legend>

    <div class="display-label">IsActive</div>
    <div class="display-field"><%: Model.IsActive %></div>

    <div class="display-label">Source</div>
    <div class="display-field"><%: Model.Source %></div>

    <div class="display-label">DirectReplacementID</div>
    <div class="display-field"><%: Model.DirectReplacementID %></div>

    <div class="display-label">AircraftType</div>
    <div class="display-field"><%: (Model.AircraftType == null ? "None" : Model.AircraftType.Model) %></div>

    <div class="display-label">AskTheWeb</div>
    <div class="display-field"><%: Model.AskTheWeb %></div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>



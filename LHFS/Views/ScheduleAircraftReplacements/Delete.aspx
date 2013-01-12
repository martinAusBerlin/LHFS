<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.ScheduleAircraftReplacement>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
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
<% using (Html.BeginForm()) { %>
    <p>
        <input type="submit" value="Delete" /> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
<% } %>

</asp:Content>



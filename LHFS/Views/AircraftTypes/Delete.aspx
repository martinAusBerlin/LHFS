<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.AircraftType>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>AircraftType</legend>

    <div class="display-label">Manufacturer</div>
    <div class="display-field"><%: Model.Manufacturer %></div>

    <div class="display-label">Model</div>
    <div class="display-field"><%: Model.Model %></div>

    <div class="display-label">TypeImageUrl</div>
    <div class="display-field"><%: Model.TypeImageUrl %></div>

    <div class="display-label">SmallTypeImageUrl</div>
    <div class="display-field"><%: Model.SmallTypeImageUrl %></div>

    <div class="display-label">Division</div>
    <div class="display-field"><%: (Model.Division == null ? "None" : Model.Division.Name) %></div>

    <div class="display-label">SeatingCapacity</div>
    <div class="display-field"><%: Model.SeatingCapacity %></div>

    <div class="display-label">RangeInNm</div>
    <div class="display-field"><%: Model.RangeInNm %></div>

    <div class="display-label">Length</div>
    <div class="display-field"><%: Model.Length %></div>

    <div class="display-label">Wingspan</div>
    <div class="display-field"><%: Model.Wingspan %></div>

    <div class="display-label">Height</div>
    <div class="display-field"><%: Model.Height %></div>

    <div class="display-label">Engines</div>
    <div class="display-field"><%: Model.Engines %></div>

    <div class="display-label">Thrust</div>
    <div class="display-field"><%: Model.Thrust %></div>

    <div class="display-label">EconCruiseSpeed</div>
    <div class="display-field"><%: Model.EconCruiseSpeed %></div>

    <div class="display-label">MaxCruiseAlt</div>
    <div class="display-field"><%: Model.MaxCruiseAlt %></div>

    <div class="display-label">Dow</div>
    <div class="display-field"><%: Model.Dow %></div>

    <div class="display-label">Mtow</div>
    <div class="display-field"><%: Model.Mtow %></div>

    <div class="display-label">Aircrafts</div>
    <div class="display-field"><%: (Model.Aircrafts == null ? "None" : Model.Aircrafts.Count.ToString()) %></div>

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



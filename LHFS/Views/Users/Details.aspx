<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.User>" %>
<%@ Import Namespace="LHFS.Utils" %>
<%@ Import Namespace="LHFS.Models.Security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Member Profile</h2>

<h1>Member ID</h1>

<div class="memberID">

<div class="name"><%: Html.DisplayFor(model => model.Fullname)%></div>

<% if (ViewBag.HasPhoto) { %>
<div class="memberImage">
<img src="<%: ViewBag.PhotoLink %>" />
</div>
<% } %>

<div style="top: 28px"><%: Html.DisplayFor(model => model.Birthday, "DateOnly")%></div>
<div style="top: 60px"><%: Html.DisplayFor(model => model.City)%></div>
<div style="top: 91px"><%: Html.DisplayFor(model => model.Country.English)%></div>
<div style="top: 123px"><%: Html.DisplayFor(model => model.VatsimID)%></div>
<div style="top: 154px"><%: Html.DisplayFor(model => model.IvaoID)%></div>
<div style="top: 185px"><%: Html.DisplayFor(model => model.Comments)%></div>


<div style="top: 300px;left: 164px; width: 70px;">Member since</div>
<div style="top: 310px; left: 164px; font-size: 22px; font-weight: bold; line-height: 22px"><%: Model.ApprovedOn.Value.ToShortMonthName() %><br /><%: Model.ApprovedOn.Value.Year %></div>
<% if (!String.IsNullOrEmpty(Model.RealPilotLicense)) { %>
<div style="top: 241px;left: 164px; width: 70px;">Real license</div>
<div style="top: 252px; left: 164px; font-size: 22px; font-weight: bold; line-height: 22px"><%: Html.DisplayFor(model => model.RealPilotLicense)%></div>
<% } %>

</div>

<h1>Flughistorie</h1>

<div style="margin-top: 20px">
<% Html.RenderAction("FlightsByUser", "Flights", new { UserID = Model.ID }); %>
</div>
</asp:Content>



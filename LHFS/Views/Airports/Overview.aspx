<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Views.Airports.ViewModel.AirportsOverview>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Overview
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Route Manual</h2>

<h1>Search</h1>

<fieldset>
<legend>Flughafen suchen</legend>

<% using (Ajax.BeginForm("GetAirports", "Airports", new AjaxOptions {  UpdateTargetId = "airportsSelection", LoadingElementId = "ajax-loader", InsertionMode = InsertionMode.Replace})) { %>

<table class="Filter">
<tr>
	<td><%: Html.TextBoxFor(model => model.Pattern)%> <%: Html.ValidationMessageFor(model => model.Pattern)%> <em>search by ICAO, IATA or City</em></td>
</tr>
</table>

<% } %>

</fieldset>

<div id="airportsSelection"></div>


<h1>Browse by region</h1>

<div id="tabs" style="margin-top: 20px;">
<ul>
	<% foreach (var item in Model.AirportsByRegions) { %>
	<li><a href="#tabs-<%: item.Key != null ? item.Key.ID : -1 %>"><%: item.Key != null ? item.Key.English : "Unknown"%></a></li>
	<% } %>
</ul>
<% foreach (var item in Model.AirportsByRegions) { %>
<div id="tabs-<%: item.Key != null ? item.Key.ID : -1 %>">

	<% Html.RenderPartial("AirportsTable", item.AsEnumerable()); %>

</div>
<% } %>

</div>

<div style="clear: both;"></div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<script>
	$(function () {
		$("#tabs").tabs();

		$("#Pattern").change(function () {
			if ($("#Pattern").val().length > 2) {
				$("#form0").submit();
			}
		});

		$("#Pattern").keyup(function () {
			if ($("#Pattern").val().length > 2) {
				$("#form0").submit();
			}
		});

		$("#Pattern").click(function () {
			if ($("#Pattern").val().length > 2) {
				$("#form0").submit();
			}
		});
	});

</script>
</asp:Content>
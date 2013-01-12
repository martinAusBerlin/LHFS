<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Views.Flights.ViewModel.FlightsBook>" %>

<% using (Ajax.BeginForm("Book", "Flights", new AjaxOptions { UpdateTargetId = "flightBookContent", InsertionMode = InsertionMode.Replace, OnSuccess = "OnBookingSaved" })) { %>

<fieldset>
<legend>Flug buchen</legend>

<div id="flightBookContent">

<table class="singleFlight">
<tr>
<th>Flight</th>
<th>Airline</th>
<th>Date</th>
<th>Flight time</th>
</tr>
<tr>
<td><%: Html.DisplayFor(t => t.Connection.Name) %></td>
<td><%: Html.DisplayFor(t => t.Connection.Airline.Name) %></td>
<td><%: DateTime.Today.ToString("yyyy-MM-dd") %></td>
<td><%: string.Format("{0:00}:{1:00}", Model.Connection.FlightTime(Model.Date).Hours, Model.Connection.FlightTime(Model.Date).Minutes) %></td>
</tr>
<tr>
<th colspan="4">Departure</th>
</tr>
<tr>
<td>
<%: string.Format("{0:00}:{1:00}", Model.Connection.DepTimeUtc(Model.Date).Hours, Model.Connection.DepTimeUtc(Model.Date).Minutes)%><br />
<em><%: string.Format("{0:00}:{1:00}", Model.Connection.DepTimeLocal.Hours, Model.Connection.DepTimeLocal.Minutes)%></em>
</td>
<td colspan="3">
<%: Html.DisplayFor(t => t.Connection.Departure.Name) %>, <%: Html.DisplayFor(t => t.Connection.Departure.IcaoCountryCode.Country.English) %><br />
<em><%: Html.DisplayFor(t => t.Connection.Departure.ICAO) %> &middot; <%: Html.DisplayFor(t => t.Connection.Departure.IATA) %></em>
</td>
</tr>
<tr>
<th colspan="4">Destination</th>
</tr>
<tr>
<td>
<%: string.Format("{0:00}:{1:00}", Model.Connection.ArrTimeUtc(Model.Date).Hours, Model.Connection.ArrTimeUtc(Model.Date).Minutes)%><br />
<em><%: string.Format("{0:00}:{1:00}", Model.Connection.ArrTimeLocal.Hours, Model.Connection.ArrTimeLocal.Minutes)%></em>
</td>
<td colspan="3">
<%: Html.DisplayFor(t => t.Connection.Destination.Name) %>, <%: Html.DisplayFor(t => t.Connection.Destination.IcaoCountryCode.Country.English)%><br />
<em><%: Html.DisplayFor(t => t.Connection.Destination.ICAO)%> &middot; <%: Html.DisplayFor(t => t.Connection.Destination.IATA)%></em>
</td>
</tr>
<tr>
<th colspan="4">Aircraft Type</th>
</tr>
<tr>
<td colspan="4"><%: Html.DropDownListFor(m => m.AircraftTypeICAO, Model.PossibleAircraftTypes) %></td>
</tr>
</table>

<%: Html.HiddenFor(m => m.Connection.ID) %>

<table class="optionGroup">

<tr>
<th><%: Html.RadioButton("Option", "1", new { id = "option1" })%></th>
<td><label for="option1">&hellip; einem neuen Umlauf hinzufügen <br /><%: Html.TextBoxFor(m => m.NewRotationName, new { @class = "optionGroupInput" })%></label></td>
</tr>

<tr>
<th><%: Html.RadioButton("Option", "2", new { id = "option2" })%></th>
<td><label for="option2">&hellip; einem bestehenden Umlauf hinzufügen <br /><%: Html.DropDownListFor(m => m.RotationID, Model.PossibleRotations, new { @class = "optionGroupInput" })%></label></td>
</tr>

<tr>
<th><%: Html.RadioButton("Option", "3", true, new { id = "option3" })%></th>
<td><label for="option3">&hellip; einfach nur buchen</label></td>
</tr>

</table>

<div style="float: right; margin-top: 10px">
<input type="submit" value="Flug buchen" style="margin-top: 10px;"/>
</div>

</div>

</fieldset>

<% } %>
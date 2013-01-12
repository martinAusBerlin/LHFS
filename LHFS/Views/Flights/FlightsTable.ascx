<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<LHFS.Models.Flight>>" %>

<table class="FlightsTable">
    <tr>
		<th>
            Datum
        </th>
        <th>
            Flug
        </th>
		<th>
			Abflughafen
		</th>
        <th>
            Zielflughafen
        </th>
		<th>
            Muster
        </th>
		<th>
            Flugzeit
        </th>
		<th>
			Flugges.
		</th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
		<td>
			<%: string.Format("{0:yyyy-MM-dd}", item.PerformedOn)%>
        </td>
        <td>
			<%= item.FlightNumber.Replace(" ", "&nbsp;")%>
        </td>
        <td>
			<%: item.Departure.Name%>, <%: item.Departure.Country != null ? item.Departure.Country.English : "Unknown"%><br />
			<em><%: item.Departure.ICAO%> &middot; <%: item.Departure.IATA%></em>
        </td>
        <td>
			<%: item.Destination.Name%>, <%: item.Destination.Country != null ? item.Destination.Country.English : "Unknown"%><br />
			<em><%: item.Destination.ICAO%> &middot; <%: item.Destination.IATA%></em>
        </td>
		<td title="<%: item.AircraftType.Longname %>">
			<%: item.AircraftType.ICAO %>
        </td>
        <td>
			<%: string.Format("{0:00}:{1:00}", item.FlightTime.Value.Hours, item.FlightTime.Value.Minutes)%>
        </td>
		<td>
			<%: item.Airline.ICAO %>
        </td>
    </tr>  
<% } %>
<tr>
	<th colspan="5">Total flight time</th>
	<th><%: string.Format("{0:00}:{1:00}", Model.Sum(t => t.FlightTime.Value.Hours), Model.Sum(t => t.FlightTime.Value.Minutes))%></th>
	<th></th>
</tr>
</table>

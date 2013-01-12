<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<LHFS.Models.Flight>>" %>

<table>
    <tr>
	    <th>
            #
        </th>
		<th>
			Dep
		</th>
		<th>
			Dest
		</th>
        <th>
            A/C
        </th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
		<td>
			<%: item.FlightNumber %>
        </td>
        <td>
			<%: item.Departure.ICAO %>
        </td>
        <td>
			<%: item.Destination.ICAO %>
        </td>
        <td>
			<%: item.AircraftTypeICAO %>
        </td>
    </tr>  
<% } %>
</table>

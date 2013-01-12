<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<LHFS.Views.Users.ViewModel.UserTableModel>>" %>

<table class="UsersTable">
    <tr>
	    <th>
            ID
        </th>
		<th>
			Name
		</th>
		<th>
			Country
		</th>
        <th>
            Rank
        </th>
        <th>
            VATSIM
        </th>
        <th>
			IVAO
        </th>
        <th>
			Homebase
		</th>
		<th>
			Typeratings
        </th>
		<th>
			Flights
        </th>
		<th>
            Hours
        </th>
		<th>
			Points
        </th>
       	<th>
            Last flight
        </th>
		<th>
			Member since
		</th>
		<th>
			Status
		</th>
    </tr>

	

<% foreach (var item in Model) { %>
    <tr style='background-color: #<%: item.Color%>; color: #<%: item.TextColor%>'>
		<td>
			<%: item.ID %>
        </td>
        <td>
            <%: Html.ActionLink(item.Fullname, "Details", new { item.ID }) %>
        </td>
        <td>
			<img src="/Images/Flags/24/<%: (item.Country == null ? "None" : item.CountryID) %>.png" title="<%: (item.Country == null ? "None" : item.Country.English) %>"/>
        </td>
        <td>
			<img src="<%: item.Rank.SmallRankIconPath %>" title="<%: (item.Rank == null ? "None" : item.Rank.Title) %>"/>
        </td>
        <td>
			<%: item.VatsimID %>
        </td>
        <td>
			<%: item.IvaoID %>
        </td>
		<td title="<%: (item.HomeBase == null ? "None" : item.HomeBase.Name) %>">
			<%: (item.HomeBase == null ? "None" : item.HomeBase.IATA) %>
		</td>
        <td>
			<% foreach (var typeRating in item.UserTypeRatings.Where(t => t.ValidTo == null)) { %>
				<%: typeRating.TypeRating.Title %><br />
			<% } %>
        </td>
        <td>
			<%: item.NumberOfFlights %>
        </td>
		<td>
			<%: item.FlightTime %>
		</td>
		<td>
			<%: item.NumberOfContributions %>
		</td>
        <td>
			<%: string.Format("{0:yyyy-MM-dd}", item.LastFlightOn) %>
        </td>
        <td>
			<%: string.Format("{0:yyyy-MM-dd}", item.MemberSince) %>
        </td>
		<td class="<%: item.Status %>">
			<%: item.Status %>
		</td>
    </tr>  
<% } %>
</table>

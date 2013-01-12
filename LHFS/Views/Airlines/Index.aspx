<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.Airline>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Index</h2>

<p>
    <%: Html.ActionLink("Create New", "Create") %>
</p>
<table>
    <tr>
        <th></th>
        <th>
            IATA
        </th>
		<th>
            ICAO
        </th>
        <th>
            Callsign
        </th>
        <th>
            Name
        </th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id=item.IATA }) %> |
            <%: Html.ActionLink("Details", "Details", new { id=item.IATA }) %> |
            <%: Html.ActionLink("Delete", "Delete", new { id=item.IATA }) %>
        </td>
		<td>
			<%: item.IATA %>
        </td>
        <td>
			<%: item.ICAO %>
        </td>
        <td>
			<%: item.Callsign %>
        </td>
        <td>
			<%: item.Name %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>



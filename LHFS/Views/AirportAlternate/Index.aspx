<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.AirportAlternate>>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Index</title>
</head>
<body>
    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>
    <table>
        <tr>
            <th></th>
            <th>
                Text
            </th>
            <th>
                AirportIATA
            </th>
            <th>
                Airport
            </th>
            <th>
                FlightLevel
            </th>
            <th>
                Description
            </th>
        </tr>
    
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.ID }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.ID }) %> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.ID }) %>
            </td>
            <td>
    			<%: item.Text %>
            </td>
            <td>
    			<%: item.AirportIATA %>
            </td>
            <td>
    			<%: (item.Airport == null ? "None" : item.Airport.Name) %>
            </td>
            <td>
    			<%: item.FlightLevel %>
            </td>
            <td>
    			<%: item.Description %>
            </td>
        </tr>  
    <% } %>
    
    </table>
</body>
</html>



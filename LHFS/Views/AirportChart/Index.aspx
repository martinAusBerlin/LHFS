<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.AirportChart>>" %>
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
                Link
            </th>
            <th>
                Title
            </th>
            <th>
                ChartTypeKey
            </th>
            <th>
                ChartType
            </th>
            <th>
                Airport
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
    			<%: item.Link %>
            </td>
            <td>
    			<%: item.Title %>
            </td>
            <td>
    			<%: item.ChartTypeKey %>
            </td>
            <td>
    			<%: (item.ChartType == null ? "None" : item.ChartType.Name) %>
            </td>
            <td>
    			<%: (item.Airport == null ? "None" : item.Airport.Name) %>
            </td>
        </tr>  
    <% } %>
    
    </table>
</body>
</html>



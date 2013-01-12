<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.ChartType>>" %>
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
                Name
            </th>
            <th>
                Description
            </th>
        </tr>
    
    <% foreach (var item in Model) { %>
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.Key }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.Key }) %> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.Key }) %>
            </td>
            <td>
    			<%: item.Name %>
            </td>
            <td>
    			<%: item.Description %>
            </td>
        </tr>  
    <% } %>
    
    </table>
</body>
</html>



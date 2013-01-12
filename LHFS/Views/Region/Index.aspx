<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.Region>>" %>
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
                English
            </th>
            <th>
                German
            </th>
            <th>
                SmallRankIconPath
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
    			<%: item.English %>
            </td>
            <td>
    			<%: item.German %>
            </td>
            <td>
    			<%: item.SmallRankIconPath %>
            </td>
        </tr>  
    <% } %>
    
    </table>
</body>
</html>



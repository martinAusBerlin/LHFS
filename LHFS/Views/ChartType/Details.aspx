<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.ChartType>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>ChartType</legend>
    
        <div class="display-label">Name</div>
        <div class="display-field"><%: Model.Name %></div>
    
        <div class="display-label">Description</div>
        <div class="display-field"><%: Model.Description %></div>
    </fieldset>
    <p>
    
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.Key }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
</body>
</html>



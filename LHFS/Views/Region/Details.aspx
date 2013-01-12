<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.Region>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>Region</legend>
    
        <div class="display-label">English</div>
        <div class="display-field"><%: Model.English %></div>
    
        <div class="display-label">German</div>
        <div class="display-field"><%: Model.German %></div>
    
        <div class="display-label">SmallRankIconPath</div>
        <div class="display-field"><%: Model.SmallRankIconPath %></div>
    </fieldset>
    <p>
    
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
</body>
</html>



<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.Region>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Delete</title>
</head>
<body>
    <h3>Are you sure you want to delete this?</h3>
    <fieldset>
        <legend>Region</legend>
    
        <div class="display-label">English</div>
        <div class="display-field"><%: Model.English %></div>
    
        <div class="display-label">German</div>
        <div class="display-field"><%: Model.German %></div>
    
        <div class="display-label">SmallRankIconPath</div>
        <div class="display-field"><%: Model.SmallRankIconPath %></div>
    </fieldset>
    <% using (Html.BeginForm()) { %>
        <p>
            <input type="submit" value="Delete" /> |
            <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>
    
</body>
</html>



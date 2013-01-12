<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.AirportChart>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>AirportChart</legend>
    
        <div class="display-label">Link</div>
        <div class="display-field"><%: Model.Link %></div>
    
        <div class="display-label">Title</div>
        <div class="display-field"><%: Model.Title %></div>
    
        <div class="display-label">ChartTypeKey</div>
        <div class="display-field"><%: Model.ChartTypeKey %></div>
    
        <div class="display-label">ChartType</div>
        <div class="display-field"><%: (Model.ChartType == null ? "None" : Model.ChartType.Name) %></div>
    
        <div class="display-label">Airport</div>
        <div class="display-field"><%: (Model.Airport == null ? "None" : Model.Airport.Name) %></div>
    </fieldset>
    <p>
    
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
</body>
</html>



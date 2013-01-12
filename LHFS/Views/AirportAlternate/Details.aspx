<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.AirportAlternate>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Details</title>
</head>
<body>
    <fieldset>
        <legend>AirportAlternate</legend>
    
        <div class="display-label">Text</div>
        <div class="display-field"><%: Model.Text %></div>
    
        <div class="display-label">AirportIATA</div>
        <div class="display-field"><%: Model.AirportIATA %></div>
    
        <div class="display-label">Airport</div>
        <div class="display-field"><%: (Model.Airport == null ? "None" : Model.Airport.Name) %></div>
    
        <div class="display-label">FlightLevel</div>
        <div class="display-field"><%: Model.FlightLevel %></div>
    
        <div class="display-label">Description</div>
        <div class="display-field"><%: Model.Description %></div>
    </fieldset>
    <p>
    
        <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
</body>
</html>



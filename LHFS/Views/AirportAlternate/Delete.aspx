<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.AirportAlternate>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Delete</title>
</head>
<body>
    <h3>Are you sure you want to delete this?</h3>
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
    <% using (Html.BeginForm()) { %>
        <p>
            <input type="submit" value="Delete" /> |
            <%: Html.ActionLink("Back to List", "Index") %>
        </p>
    <% } %>
    
</body>
</html>



<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.AirportChart>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Create</title>
</head>
<body>
    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>
        <fieldset>
            <legend>AirportChart</legend>
    
    		<%: Html.Partial("CreateOrEdit", Model) %>
            <p>
                <input type="submit" value="Create" />
            </p>
        </fieldset>
    <% } %>
    
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
</body>
</html>


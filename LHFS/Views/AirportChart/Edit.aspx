<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.AirportChart>" %>
<!DOCTYPE html>

<html>
<head runat="server">
    <title>Edit</title>
</head>
<body>
    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true) %>
        <fieldset>
            <legend>AirportChart</legend>
    
            <%: Html.HiddenFor(model => model.ID) %>
    		<%: Html.Partial("CreateOrEdit", Model) %>
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>
    <% } %>
    
    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>
</body>
</html>



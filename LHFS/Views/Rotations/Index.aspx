<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.Rotation>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Index</h2>

<p>
    <%: Html.ActionLink("Create New", "Create") %>
</p>
<table>
    <tr>
        <th></th>
        <th>
            User
        </th>
        <th>
            Name
        </th>
        <th>
            Flights
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
			<%: (item.User == null ? "None" : item.User.FullnameAndID) %>
        </td>
        <td>
			<%: item.Name %>
        </td>
        <td>
			<%: (item.Flights == null ? "None" : item.Flights.Count.ToString()) %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>



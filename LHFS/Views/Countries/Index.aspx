<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.Country>>" %>

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
            English
        </th>
        <th>
            German
        </th>
        <th>
            Users
        </th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id=item.ISO }) %> |
            <%: Html.ActionLink("Details", "Details", new { id=item.ISO }) %> |
            <%: Html.ActionLink("Delete", "Delete", new { id=item.ISO }) %>
        </td>
        <td>
			<%: item.English %>
        </td>
        <td>
			<%: item.German %>
        </td>
        <td>
			<%: (item.Users == null ? "None" : item.Users.Count.ToString()) %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>



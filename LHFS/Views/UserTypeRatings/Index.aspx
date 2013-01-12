<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.UserTypeRating>>" %>

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
            ValidFrom
        </th>
        <th>
            ValidTo
        </th>
        <th>
            User
        </th>
        <th>
            TypeRating
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
			<%: String.Format("{0:g}", item.ValidFrom) %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.ValidTo) %>
        </td>
        <td>
			<%: (item.User == null ? "None" : item.User.FullnameAndID) %>
        </td>
        <td>
			<%: (item.TypeRating == null ? "None" : item.TypeRating.Title) %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>



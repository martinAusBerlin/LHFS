<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.GalleryImage>>" %>

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
            Url
        </th>
        <th>
            Title
        </th>
        <th>
            CreatedOn
        </th>
        <th>
            User
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
			<%: item.Url %>
        </td>
        <td>
			<%: item.Title %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.CreatedOn) %>
        </td>
        <td>
			<%: (item.User == null ? "None" : item.User.FullnameAndID) %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>



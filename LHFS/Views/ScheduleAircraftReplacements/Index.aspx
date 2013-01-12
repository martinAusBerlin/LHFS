<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.ScheduleAircraftReplacement>>" %>

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
            IsActive
        </th>
        <th>
            Source
        </th>
        <th>
            DirectReplacementID
        </th>
        <th>
            AircraftType
        </th>
        <th>
            AskTheWeb
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
			<%: item.IsActive %>
        </td>
        <td>
			<%: item.Source %>
        </td>
        <td>
			<%: item.DirectReplacementID %>
        </td>
        <td>
			<%: (item.AircraftType == null ? "None" : item.AircraftType.Model) %>
        </td>
        <td>
			<%: item.AskTheWeb %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>



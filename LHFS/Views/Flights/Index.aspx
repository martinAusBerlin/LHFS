<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.Flight>>" %>

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
            FlightNumber
        </th>
        <th>
            User
        </th>
        <th>
            ApprovedByUser
        </th>
        <th>
            TakeoffWeight
        </th>
        <th>
            LandingWeight
        </th>
        <th>
            OffBlock
        </th>
        <th>
            OnBlock
        </th>
        <th>
            TakeOff
        </th>
        <th>
            Touchdown
        </th>
        <th>
            RouteText
        </th>
        <th>
            AdditionalText
        </th>
        <th>
            BookedOn
        </th>
        <th>
            PerformedOn
        </th>
        <th>
            FlightTime
        </th>
        <th>
            NullifiedOn
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
			<%: item.FlightNumber %>
        </td>
        <td>
			<%: (item.User == null ? "None" : item.User.FullnameAndID) %>
        </td>
        <td>
			<%: (item.ApprovedByUser == null ? "None" : item.ApprovedByUser.FullnameAndID) %>
        </td>
        <td>
			<%: String.Format("{0:F}", item.TakeoffWeight) %>
        </td>
        <td>
			<%: String.Format("{0:F}", item.LandingWeight) %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.OffBlock) %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.OnBlock) %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.TakeOff) %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.Touchdown) %>
        </td>
        <td>
			<%: item.RouteText %>
        </td>
        <td>
			<%: item.AdditionalText %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.BookedOn) %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.PerformedOn) %>
        </td>
        <td>
			<%: item.FlightTime %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.NullifiedOn) %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>



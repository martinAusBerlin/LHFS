<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.AircraftType>>" %>

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
            Manufacturer
        </th>
        <th>
            Model
        </th>
        <th>
            TypeImageUrl
        </th>
        <th>
            SmallTypeImageUrl
        </th>
        <th>
            Division
        </th>
        <th>
            SeatingCapacity
        </th>
        <th>
            RangeInNm
        </th>
        <th>
            Length
        </th>
        <th>
            Wingspan
        </th>
        <th>
            Height
        </th>
        <th>
            Engines
        </th>
        <th>
            Thrust
        </th>
        <th>
            EconCruiseSpeed
        </th>
        <th>
            MaxCruiseAlt
        </th>
        <th>
            Dow
        </th>
        <th>
            Mtow
        </th>
        <th>
            Aircrafts
        </th>
        <th>
            Users
        </th>
    </tr>

<% foreach (var item in Model) { %>
    <tr>
        <td>
            <%: Html.ActionLink("Edit", "Edit", new { id=item.ICAO }) %> |
            <%: Html.ActionLink("Details", "Details", new { id=item.ICAO }) %> |
            <%: Html.ActionLink("Delete", "Delete", new { id=item.ICAO }) %>
        </td>
        <td>
			<%: item.Manufacturer %>
        </td>
        <td>
			<%: item.Model %>
        </td>
        <td>
			<%: item.TypeImageUrl %>
        </td>
        <td>
			<%: item.SmallTypeImageUrl %>
        </td>
        <td>
			<%: (item.Division == null ? "None" : item.Division.Name) %>
        </td>
        <td>
			<%: item.SeatingCapacity %>
        </td>
        <td>
			<%: item.RangeInNm %>
        </td>
        <td>
			<%: item.Length %>
        </td>
        <td>
			<%: item.Wingspan %>
        </td>
        <td>
			<%: item.Height %>
        </td>
        <td>
			<%: item.Engines %>
        </td>
        <td>
			<%: item.Thrust %>
        </td>
        <td>
			<%: item.EconCruiseSpeed %>
        </td>
        <td>
			<%: item.MaxCruiseAlt %>
        </td>
        <td>
			<%: item.Dow %>
        </td>
        <td>
			<%: item.Mtow %>
        </td>
        <td>
			<%: (item.Aircrafts == null ? "None" : item.Aircrafts.Count.ToString()) %>
        </td>
        <td>
			<%: (item.Users == null ? "None" : item.Users.Count.ToString()) %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>



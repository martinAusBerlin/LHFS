<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.User>>" %>

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
            Forename
        </th>
        <th>
            Surname
        </th>
        <th>
            Birthday
        </th>
        <th>
            FullnameAndID
        </th>
        <th>
            Country
        </th>
        <th>
            City
        </th>
        <th>
            IsAdministrator
        </th>
        <th>
            Password
        </th>
        <th>
            Rank
        </th>
        <th>
            HomeBase
        </th>
        <th>
            DefaultLanguageID
        </th>
        <th>
            ReceiveNewsletter
        </th>
        <th>
            HideEmail
        </th>
        <th>
            ShowGallery
        </th>
        <th>
            HideProfile
        </th>
        <th>
            VatsimID
        </th>
        <th>
            IvaoID
        </th>
        <th>
            RealPilotLicense
        </th>
        <th>
            Comments
        </th>
        <th>
            CreatedOn
        </th>
        <th>
            ApprovedOn
        </th>
        <th>
            LastFlight
        </th>
        <th>
            Status
        </th>
        <th>
            UserTypeRatings
        </th>
        <th>
            Flights
        </th>
        <th>
            Routes
        </th>
        <th>
            GalleryImages
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
			<%: item.Forename %>
        </td>
        <td>
			<%: item.Surname %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.Birthday) %>
        </td>
        <td>
			<%: item.FullnameAndID %>
        </td>
        <td>
			<%: (item.Country == null ? "None" : item.Country.English) %>
        </td>
        <td>
			<%: item.City %>
        </td>
        <td>
			<%: item.IsAdministrator %>
        </td>
        <td>
			<%: item.Password %>
        </td>
        <td>
			<%: (item.Rank == null ? "None" : item.Rank.Title) %>
        </td>
        <td>
			<%: (item.HomeBase == null ? "None" : item.HomeBase.Name) %>
        </td>
        <td>
			<%: item.DefaultLanguageID %>
        </td>
        <td>
			<%: item.ReceiveNewsletter %>
        </td>
        <td>
			<%: item.HideEmail %>
        </td>
        <td>
			<%: item.ShowGallery %>
        </td>
        <td>
			<%: item.HideProfile %>
        </td>
        <td>
			<%: item.VatsimID %>
        </td>
        <td>
			<%: item.IvaoID %>
        </td>
        <td>
			<%: item.RealPilotLicense %>
        </td>
        <td>
			<%: item.Comments %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.CreatedOn) %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.ApprovedOn) %>
        </td>
        <td>
			<%: String.Format("{0:g}", item.LastFlightOn) %>
        </td>
        <td>
			<%: Html.DisplayTextFor(_ => item.Status).ToString() %>
        </td>
        <td>
			<%: (item.UserTypeRatings == null ? "None" : item.UserTypeRatings.Count.ToString()) %>
        </td>
        <td>
			<%: (item.Flights == null ? "None" : item.Flights.Count.ToString()) %>
        </td>
        <td>
			<%: (item.Routes == null ? "None" : item.Routes.Count.ToString()) %>
        </td>
        <td>
			<%: (item.GalleryImages == null ? "None" : item.GalleryImages.Count.ToString()) %>
        </td>
    </tr>  
<% } %>

</table>

</asp:Content>



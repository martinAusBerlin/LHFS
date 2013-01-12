<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Delete</h2>

<h3>Are you sure you want to delete this?</h3>
<fieldset>
    <legend>User</legend>

    <div class="display-label">Forename</div>
    <div class="display-field"><%: Model.Forename %></div>

    <div class="display-label">Surname</div>
    <div class="display-field"><%: Model.Surname %></div>

    <div class="display-label">Birthday</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.Birthday) %></div>

    <div class="display-label">FullnameAndID</div>
    <div class="display-field"><%: Model.FullnameAndID %></div>

    <div class="display-label">Country</div>
    <div class="display-field"><%: (Model.Country == null ? "None" : Model.Country.English) %></div>

    <div class="display-label">City</div>
    <div class="display-field"><%: Model.City %></div>

    <div class="display-label">IsAdministrator</div>
    <div class="display-field"><%: Model.IsAdministrator %></div>

    <div class="display-label">Password</div>
    <div class="display-field"><%: Model.Password %></div>

    <div class="display-label">Rank</div>
    <div class="display-field"><%: (Model.Rank == null ? "None" : Model.Rank.Title) %></div>

    <div class="display-label">HomeBase</div>
    <div class="display-field"><%: (Model.HomeBase == null ? "None" : Model.HomeBase.Name) %></div>

    <div class="display-label">DefaultLanguageID</div>
    <div class="display-field"><%: Model.DefaultLanguageID %></div>

    <div class="display-label">ReceiveNewsletter</div>
    <div class="display-field"><%: Model.ReceiveNewsletter %></div>

    <div class="display-label">HideEmail</div>
    <div class="display-field"><%: Model.HideEmail %></div>

    <div class="display-label">ShowGallery</div>
    <div class="display-field"><%: Model.ShowGallery %></div>

    <div class="display-label">HideProfile</div>
    <div class="display-field"><%: Model.HideProfile %></div>

    <div class="display-label">VatsimID</div>
    <div class="display-field"><%: Model.VatsimID %></div>

    <div class="display-label">IvaoID</div>
    <div class="display-field"><%: Model.IvaoID %></div>

    <div class="display-label">RealPilotLicense</div>
    <div class="display-field"><%: Model.RealPilotLicense %></div>

    <div class="display-label">Comments</div>
    <div class="display-field"><%: Model.Comments %></div>

    <div class="display-label">CreatedOn</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.CreatedOn) %></div>

    <div class="display-label">ApprovedOn</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.ApprovedOn) %></div>

    <div class="display-label">LastFlight</div>
    <div class="display-field"><%: String.Format("{0:g}", Model.LastFlight) %></div>

    <div class="display-label">Status</div>
    <div class="display-field"><%: Html.DisplayTextFor(_ => Model.Status).ToString() %></div>

    <div class="display-label">UserTypeRatings</div>
    <div class="display-field"><%: (Model.UserTypeRatings == null ? "None" : Model.UserTypeRatings.Count.ToString()) %></div>

    <div class="display-label">Flights</div>
    <div class="display-field"><%: (Model.Flights == null ? "None" : Model.Flights.Count.ToString()) %></div>

    <div class="display-label">Routes</div>
    <div class="display-field"><%: (Model.Routes == null ? "None" : Model.Routes.Count.ToString()) %></div>

    <div class="display-label">GalleryImages</div>
    <div class="display-field"><%: (Model.GalleryImages == null ? "None" : Model.GalleryImages.Count.ToString()) %></div>
</fieldset>
<% using (Html.BeginForm()) { %>
    <p>
        <input type="submit" value="Delete" /> |
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>
<% } %>

</asp:Content>



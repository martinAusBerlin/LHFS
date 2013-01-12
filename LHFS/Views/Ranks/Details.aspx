<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Models.Rank>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Details</h2>

<fieldset>
    <legend>Rank</legend>

    <div class="display-label">Title</div>
    <div class="display-field"><%: Model.Title %></div>

    <div class="display-label">Shorttitle</div>
    <div class="display-field"><%: Model.Shorttitle %></div>

    <div class="display-label">Users</div>
    <div class="display-field"><%: (Model.Users == null ? "None" : Model.Users.Count.ToString()) %></div>
</fieldset>
<p>

    <%: Html.ActionLink("Edit", "Edit", new { id=Model.ID }) %> |
    <%: Html.ActionLink("Back to List", "Index") %>
</p>

</asp:Content>



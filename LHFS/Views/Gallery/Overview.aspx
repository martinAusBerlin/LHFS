<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<LHFS.Models.GalleryImage>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Gallerie</h2>

<div class="Actions">
<%: Html.ImageActionLink("/Images/Icons/upload.png", "TODO", "Create", "Gallery", null, null, new { style = "border-right: 1px solid #fff" })%>
<%: Html.ImageActionLink("/Images/Icons/editimage.png", "TODO", "UserList", "Gallery", null, null, null)%>
</div>

<div style="clear: both"/>

<% foreach (var item in Model) { %>
    
	<div class="GalleryImageContainer">
		<div class="GalleryImage">
			<a href="/ImageHandler.ashx?file=<%: item.Url %>&q=90&h=700&usercontent=1" class="lightbox" /><img src="/ImageHandler.ashx?file=<%: item.Url %>&w=180&q=90&usercontent=1" /></a>
		</div>

		<p class="GalleryTitle"><%: item.User.FullnameAndID %></p>

	</div>

<% } %>

<div style="clear: both"/>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
	<link href="/Content/jquery.lightbox-0.5.css" rel="stylesheet" type="text/css" />
	<script src="/Scripts/jquery.lightbox-0.5.js" type="text/javascript"></script>
	
	<script type="text/javascript">
		$(function () {
			$('.lightbox').lightBox({
				imageLoading: '/Images/Lightbox/lightbox-ico-loading.gif',
				imageBtnClose: '/Images/Lightbox/lightbox-btn-close.gif',
				imageBtnPrev: '/Images/Lightbox/lightbox-btn-prev.gif',
				imageBtnNext: '/Images/Lightbox/lightbox-btn-next.gif',
				imageBlank: '/Images/Lightbox/lightbox-blank.gif'
			});
		});
    </script>
</asp:Content>

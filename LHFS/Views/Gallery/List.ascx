<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<LHFS.Models.GalleryImage>>" %>

<% foreach (var item in Model) { %>
    
	<div class="SmallGalleryImageContainer">
		<div class="SmallGalleryImage">
			<a href="/ImageHandler.ashx?file=<%: item.Url %>&q=90&h=700&usercontent=1" class="lightbox" /><img src="/ImageHandler.ashx?file=<%: item.Url %>&w=120&q=90&usercontent=1" /></a>
		</div>
	</div>

<% } %>



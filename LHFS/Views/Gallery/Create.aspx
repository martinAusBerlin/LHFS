<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LHFS.Views.Gallery.ViewModel.GalleryCreate>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Picture upload
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Picture upload</h2>

<fieldset>
    <legend>Bilder hochladen</legend>

<img src="/Images/icons/Uploads-icon.png" style="width: 60px; float: left;" />

<div style="margin-left: 70px">
<input type="file" name="file_upload" id="file_upload"/>
</div>

</fieldset>

<div class="clearfix"></div>

<div id="myGalleryImagesContainer">

<% Html.RenderAction("UsersList"); %>

</div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
<script src="/Scripts/jquery.uploadify.min.js" type="text/javascript"></script>
<script type="text/javascript">
	$(document).ready(function () {

		$("#myGalleryImagesContainer").load('<%=Url.Action("UsersList", "Gallery") %>');

		$('#file_upload').uploadify({
			'swf': '/Content/uploadify.swf',
			'uploader': '/Gallery/Upload',
			'buttonImage': '/ButtonImageHandler.ashx?text=Dateien auswählen&w=120',
			'width': '148',
			'height': '23',
			'onUploadSuccess': function (file, data, response) {
				$("#myGalleryImagesContainer").load('<%=Url.Action("UsersList", "Gallery") %>');
			}
		});
	});
</script>
<link href="/Content/uploadify.css" rel="stylesheet" type="text/css" />
</asp:Content>

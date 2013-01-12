<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<decimal>" %>
<%: Model < 0 ? "S" : "N" %>&thinsp;<%: Math.Truncate(Math.Abs(Model)).ToString().PadLeft(2, '0')%>° <%: (Math.Abs(Model) * 60) % 60%>'


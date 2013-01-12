<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<decimal>" %>
<%: Model < 0 ? "W" : "E" %>&thinsp;<%: Math.Truncate(Math.Abs(Model)).ToString().PadLeft(3,'0')%>° <%: (Math.Abs(Model) * 60) % 60%>'


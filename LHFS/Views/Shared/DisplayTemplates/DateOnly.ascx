﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.DateTime?>" %><%: Model.HasValue ? String.Format("{0:yyyy-MM-dd}", Model) : String.Empty%>
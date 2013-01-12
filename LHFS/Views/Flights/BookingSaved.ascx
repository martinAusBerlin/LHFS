<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Views.Flights.ViewModel.FlightsBook>" %>

Der Flug <%: Html.DisplayFor(t => t.Connection.Name) %> wurde erfolgreich gebucht.
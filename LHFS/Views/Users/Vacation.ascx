<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<LHFS.Models.User>" %>


<% if(!Model.LastVacationPeriodStartsOn.HasValue || Model.LastVacationPeriodStartsOn.Value.Year < DateTime.UtcNow.Year) { %>

<p>Your current rank is <%: Html.DisplayFor(model => model.Rank.Title)%>. In this position you are eligible to have a vaction of <%: Html.DisplayFor(model => model.Rank.VacationWeeks)%> weeks per year.</p>
	
<a id="takeVacation" href="#">
	<img src="/ButtonImageHandler.ashx?text=I want my vacation to start now." alt="I want my vacation to start now." />
</a>

<% } else { %>

<p>Your current rank is <%: Html.DisplayFor(model => model.Rank.Title)%>. In this position you are eligible to have a vaction of <%: Html.DisplayFor(model => model.Rank.VacationWeeks)%> weeks per year.</p>
<p>You have started your vacation on <%: Html.DisplayFor(model => model.LastVacationPeriodStartsOn, "DateOnly")%>, it will end on <%: Html.DisplayFor(model => model.LastVacationPeriodEndsOn, "DateOnly")%>.</p>
	
<% } %>
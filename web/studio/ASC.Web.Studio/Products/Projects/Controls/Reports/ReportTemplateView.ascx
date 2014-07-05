﻿<%@ Assembly Name="ASC.Projects.Core" %>
<%@ Assembly Name="ASC.Web.Projects" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportTemplateView.ascx.cs"
    Inherits="ASC.Web.Projects.Controls.Reports.ReportTemplateView" %>
<%@ Import Namespace="ASC.Web.Projects.Resources" %>
<%@ Import Namespace="ASC.Web.Projects" %>
<%@ Register TagPrefix="sc" Namespace="ASC.Web.Studio.Controls.Common" Assembly="ASC.Web.Studio" %>

<div class="input-name-container requiredField">
    <span class="requiredErrorText title" error="<%=ReportResource.EmptyTemplateTitle %>"><%=ReportResource.EmptyTemplateTitle %></span>
    <input id="templateTitle" type="text" class="textEdit" value="<%=Template.Name %>"/>
</div>

<asp:PlaceHolder ID="_filter" runat="server"/>

<div class="middle-button-container option-container">
    <%if (Template.AutoGenerated)
      {%>
        <input id="autoGeneration" type="checkbox" checked="checked"/>
    <% }
      else
      { %>
        <input id="autoGeneration" type="checkbox" />
    <% } %>
    <label for="autoGeneration"><%=ReportResource.AutomaticallyGenerate%></label>
</div>
 
 <div id="okWindow" style="display: none">
    <sc:Container ID="_hintPopup" runat="server">
        <Header>
            <%=ReportResource.TemplateSavedHeader %>
        </Header>
        <Body>
            <p><%=ReportResource.TemplateSavedNote%></p>
            <div class="middle-button-container">
                <a class="button blue middle"><%= ProjectResource.OkButton%></a> 
            </div>
        </Body>
    </sc:Container>
</div>
            
<div class="template-params">
       <%if (Template.AutoGenerated)
         {%>
        <select id="generatedPeriods" class="comboBox period-cbox" data-period="<%= TmplParamPeriod %>">
            <option value="day"><%= ReportResource.EveryDay %></option>
            <option value="week" selected="selected"><%= ReportResource.EveryWeek %></option>
            <option value="month"><%= ReportResource.EveryMonth %></option>
        </select>
        <div class="variant-conteiner">
            <select id="week" class="comboBox<% if (TmplParamPeriod != "week"){%> display-none<% } %>" data-value="<%=TmplParamWeek %>">
            <%=TemplateParamInitialiser.InitDaysOfWeek()%>
            </select>
            <select id="month" class="comboBox<% if (TmplParamPeriod != "month"){%> display-none<% } %>" data-value="<%=TmplParamMonth %>">
            <%=TemplateParamInitialiser.InitDaysOfMonth()%>
            </select>
            <select id="hours" class="comboBox" data-value="<%=TmplParamHour %>">
            <%=TemplateParamInitialiser.InitHoursCombobox()%>
            </select>
       </div>
    <% }
      else
      { %>
        <select id="generatedPeriods" class="comboBox period-cbox"  disabled="disabled">
            <option value="day"><%=ReportResource.EveryDay %></option>
            <option value="week"><%=ReportResource.EveryWeek %></option>
            <option value="month"><%=ReportResource.EveryMonth %></option>
        </select>
        <div class="variant-conteiner">
            <select id="week" class="comboBox display-none"  disabled="disabled">
            <%=TemplateParamInitialiser.InitDaysOfWeek()%>
            </select>
            <select id="month" class="comboBox display-none"  disabled="disabled">
            <%=TemplateParamInitialiser.InitDaysOfMonth()%>
            </select>
            <select id="hours" class="comboBox"  disabled="disabled">
            <%=TemplateParamInitialiser.InitHoursCombobox()%>
            </select>
        </div>
    <% } %>
    
</div>

<div class="middle-button-container" id="reportButtons">
    <a id="updateTemplate" class="button blue middle disable"><%= ProjectsCommonResource.Save%></a>
    <span class="splitter-buttons"></span>
    <a class="button gray middle" href="generatedreport.aspx?ID=<%=Template.Id %>"><%=ReportResource.GenerateReport%></a>
    <span class="splitter-buttons"></span>
    <a id="removeReport" class="button gray middle"><%= ProjectsCommonResource.Delete%></a>
</div>
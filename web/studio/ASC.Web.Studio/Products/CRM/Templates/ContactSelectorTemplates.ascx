﻿<%@ Control Language="C#" AutoEventWireup="false" EnableViewState="false" %>
<%@ Assembly Name="ASC.Web.CRM" %>
<%@ Import Namespace="ASC.Web.CRM.Classes" %>
<%@ Import Namespace="ASC.Web.CRM.Configuration" %>
<%@ Import Namespace="ASC.Web.Core.Utility.Skins" %>
<%@ Import Namespace="ASC.Web.CRM.Resources" %>


<script id="contactSelectorContainerTmpl" type="text/x-jquery-tmpl">
<div id="selector_${ObjName}">
    <div class="contactSelector-itemList"></div>
</div>
</script>

<script id="contactSelectorRowTmpl" type="text/x-jquery-tmpl">
<div id="item_${ObjID}" class="contactSelector-item">
    {{tmpl "contactSelectorContentTmpl"}}

    {{if ShowOnlySelectorContent === false}}
        {{tmpl "contactSelectorContactInfoContentTmpl"}}
    {{/if}}
    {{if ShowNewCompanyContent === true||ShowNewContactContent === true}}
        {{tmpl "contactSelectorNewContactContentTmpl"}}
    {{/if}}
</div>
</script>

<script id="contactSelectorContentTmpl" type="text/x-jquery-tmpl">
<div id="selectorContent_${ObjID}" class="contactSelector-selectorContent" style="{{if ShowOnlySelectorContent===false&&selectedContact != null}}display:none;{{/if}}">
    <table cellspacing="0" cellpadding="1" class="contactSelector-inputContainer" width="100%" style="height: 18px;">
        <tbody>
            <tr>
                <td width="16px" class="borderBase" style="border-right: 0 none;">
                    <label class="searchButton" id="searchImg_${ObjID}" onclick="window.${ObjName}.quickSearch('${ObjID}');"></label>
                    <img align="absmiddle" id="loaderImg_${ObjID}" src="<%= WebImageSupplier.GetAbsoluteWebPath("loader_16.gif" ) %>" style="display:none;"/>
                </td>
                <td class="borderBase" style="border-left: 0 none; border-right: 0 none;">
                    <input type="text" id="contactTitle_${ObjID}" value="" class="textEdit" placeholder="${WatermarkText}" autocomplete="off"/>
                    <input type="hidden" id="contactID_${ObjID}" value="{{if selectedContact != null}}${selectedContact.id}{{/if}}"/>
                </td>
                <td width="16px" class="borderBase" style="border-left: 0 none;">
                    <label class="crossButton" style="display:none;" onclick="window.${ObjName}.crossButtonEventClick('${ObjID}');"
                     title="<%=CRMCommonResource.Cancel%>"></label>
                </td>

                {{if ShowDeleteButton === true}}
                <td width="16px;">
                    <a class="crm-deleteLink" title="${DeleteContactText}" onclick="window.${ObjName}.deleteContact('${ObjID}');">
                        &nbsp;
                    </a>
                </td>
                {{/if}}

                {{if ShowAddButton === true}}
                <td width="16px;">
                    <a title="${AddButtonText}" class="crm-addNewLink" onclick="window.${ObjName}.AddNewSelector(jq(this));">
                        &nbsp;
                    </a>
                </td>
                {{/if}}
            </tr>
        </tbody>
    </table>

    {{tmpl "contactSelectorNoMatchesTmpl"}}
</div>
</script>

<script id="contactSelectorNoMatchesTmpl" type="text/x-jquery-tmpl">
<div id="noMatches_${ObjID}" class="borderBase noMatches">
    <div style="padding: 5px;"><%= CRMCommonResource.NoMatches %></div>
    
    {{if ShowNewCompanyContent === true}}
    <div style="padding:0 5px">
        <a class="link blue dotline small plus" onclick="window.${ObjName}.showNewCompany('${ObjID}');">
            <%= CRMContactResource.AddNewCompany %>
        </a>
    </div>
    {{/if}}
    
    {{if ShowNewContactContent === true}}
    <div style="padding:5px;">
        <a class="link blue dotline small plus" onclick="window.${ObjName}.showNewContact('${ObjID}');">
            <%= CRMContactResource.AddNewContact %>
        </a>
    </div>
    {{/if}}
</div>
</script>

<script id="contactSelectorContactInfoContentTmpl" type="text/x-jquery-tmpl">
<div id="infoContent_${ObjID}" {{if selectedContact==null}}style="display: none;"{{/if}}>
    <table width="100%" cellspacing="0" cellpadding="3">
        <tbody>
            <tr>
                {{if ShowContactImg === true}}
                <td width="40px">
                    <img src="{{if selectedContact != null}}${selectedContact.smallFotoUrl}{{/if}}" />
                </td>
                {{/if}}
                <td>
                    <span class="splitter">
                        <b>{{if selectedContact != null}}${selectedContact.displayName}{{/if}}</b>
                    </span>
                    
                    {{if ShowChangeButton === true}}
                    <a class="crm-removeLink" title="<%= CRMCommonResource.UnlinkContact%>" onclick="window.${ObjName}.changeContact('${ObjID}');">
                        &nbsp;
                    </a>
                    {{/if}}
                    
                    {{if ShowDeleteButton === true}}
                    <a class="crm-deleteLink" title="${DeleteContactText}" onclick="window.${ObjName}.deleteContact('${ObjID}');">
                        &nbsp;
                    </a>
                    {{/if}}

                    {{if ShowAddButton === true}}
                    <a title="${AddButtonText}" class="crm-addNewLink" onclick="window.${ObjName}.AddNewSelector(jq(this));">
                        &nbsp;
                    </a>
                    {{/if}}
                </td>
            </tr>
        </tbody>
    </table>
</div>
</script>

<script id="contactSelectorNewContactContentTmpl" type="text/x-jquery-tmpl">
<div id="newContactContent_${ObjID}" style="display: none;">
    <table width="100%" cellspacing="0" cellpadding="3">
        <tbody>
            <tr>
                <td width="40px">
                    {{if ShowNewCompanyContent === true}}
                    <img id="newCompanyImg_${ObjID}" src="<%= ContactPhotoManager.GetSmallSizePhoto(0, true) %>" />
                    {{/if}}
                    {{if ShowNewContactContent === true}}
                    <img id="newContactImg_${ObjID}" src="<%= ContactPhotoManager.GetSmallSizePhoto(0, false) %>" style="display:none;"/>
                    {{/if}}
                    <input type="hidden" id="hiddenIsCompany_${ObjID}" />
                </td>
                <td class="name-container-block">
                    <span class="splitter">
                    {{if ShowNewCompanyContent === true}}
                        <input type="text" id="newCompanyTitle_${ObjID}" class="textEdit" placeholder="<%= CRMContactResource.CompanyName %>" autocomplete="off"/>
                    {{/if}}
                    {{if ShowNewContactContent === true}}
                        <input type="text" id="newContactFirstName_${ObjID}" class="textEdit" placeholder="<%= CRMContactResource.FirstName %>" autocomplete="off" style="display:none;"/>
                        <input type="text" id="newContactLastName_${ObjID}" class="textEdit" placeholder="<%= CRMContactResource.LastName %>" autocomplete="off" style="display:none;"/>
                    {{/if}}
                    </span>
                    <a class="crm-acceptLink" title="<%=CRMCommonResource.Add%>" onclick="window.${ObjName}.acceptNewContact('${ObjID}');">
                        &nbsp;
                    </a>
                    <a class="crm-rejectLink" title="<%=CRMCommonResource.Cancel%>" onclick="window.${ObjName}.rejectNewContact('${ObjID}');">
                        &nbsp;
                    </a>
                    {{if ShowAddButton === true}}
                    <a title="${AddButtonText}" class="crm-addNewLink" onclick="window.${ObjName}.AddNewSelector(jq(this));">
                        &nbsp;
                    </a>
                    {{/if}}
                </td>
            </tr>
        </tbody>
    </table>
</div>
</script>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %><script id="template-projprojects" type="text/x-jquery-tmpl" async="true"><select class="${classname}">  <option value="-1"><%=Resources.MobileResource.LblSelectProject%></option>  {{each items}}    <option value="${$value.id}"{{if $value.id == $data.selectedid}} selected="selected"{{/if}}>${$value.title}</option>  {{/each}}</select></script>
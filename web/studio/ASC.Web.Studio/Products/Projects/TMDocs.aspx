﻿<%@ Assembly Name="ASC.Web.Projects" %>
<%@ Page Language="C#" MasterPageFile="~/Products/Projects/Masters/BasicTemplate.Master" AutoEventWireup="true" CodeBehind="TMDocs.aspx.cs" Inherits="ASC.Web.Projects.TMDocs" Title="Untitled Page" %>
<%@ MasterType  TypeName="ASC.Web.Projects.Masters.BasicTemplate" %>

<%@ Import Namespace="ASC.Web.Studio.Utility" %>

<asp:Content runat="server" ContentPlaceHolderID="BTPageContent" ID="BTPageContent">
    <asp:PlaceHolder runat="server" ID="CommonContainerHolder"></asp:PlaceHolder>
</asp:Content>

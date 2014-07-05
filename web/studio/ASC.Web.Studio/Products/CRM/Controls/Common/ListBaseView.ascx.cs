/*
(c) Copyright Ascensio System SIA 2010-2014

This program is a free software product.
You can redistribute it and/or modify it under the terms 
of the GNU Affero General Public License (AGPL) version 3 as published by the Free Software
Foundation. In accordance with Section 7(a) of the GNU AGPL its Section 15 shall be amended
to the effect that Ascensio System SIA expressly excludes the warranty of non-infringement of 
any third-party rights.

This program is distributed WITHOUT ANY WARRANTY; without even the implied warranty 
of MERCHANTABILITY or FITNESS FOR A PARTICULAR  PURPOSE. For details, see 
the GNU AGPL at: http://www.gnu.org/licenses/agpl-3.0.html

You can contact Ascensio System SIA at Lubanas st. 125a-25, Riga, Latvia, EU, LV-1021.

The  interactive user interfaces in modified source and object code versions of the Program must 
display Appropriate Legal Notices, as required under Section 5 of the GNU AGPL version 3.
 
Pursuant to Section 7(b) of the License you must retain the original Product logo when 
distributing the program. Pursuant to Section 7(e) we decline to grant you any rights under 
trademark law for use of our trademarks.
 
All the Product's GUI elements, including illustrations and icon sets, as well as technical writing
content are licensed under the terms of the Creative Commons Attribution-ShareAlike 4.0
International. See the License terms at http://creativecommons.org/licenses/by-sa/4.0/legalcode
*/

#region Import

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ASC.Core.Users;
using AjaxPro;
using ASC.Core;
using ASC.Core.Caching;
using ASC.CRM.Core;
using ASC.CRM.Core.Entities;
using ASC.Web.Core.Utility.Skins;
using ASC.Web.CRM.Classes;
using ASC.Web.CRM.Configuration;
using ASC.Web.CRM.Controls.Common;
using ASC.Web.CRM.Controls.Settings;
using ASC.Web.CRM.Resources;
using ASC.Web.Studio.Core;
using ASC.Web.Studio.Core.Users;
using ASC.Web.Studio.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

#endregion

namespace ASC.Web.CRM.Controls.Common
{
    public partial class ListBaseView : BaseUserControl
    {
        private static ICache showEmptyScreen = new AspCache();

        #region Properies

        public static string Location
        {
            get { return PathProvider.GetFileStaticRelativePath("Common/ListBaseView.ascx"); }
        }

        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            InitPage();

            if (showEmptyScreen.Get("crmScreen" + TenantProvider.CurrentTenantID) == null)
            {
                var hasactivity = Global.DaoFactory.GetContactDao().HasActivity();
                if (hasactivity)
                {
                    showEmptyScreen.Insert("crmScreen" + TenantProvider.CurrentTenantID, new object(), TimeSpan.FromMinutes(30));
                }
                else
                {
                    RenderDashboardEmptyScreen();
                }
            }
        }

        #endregion

        #region Methods

        private void InitPage()
        {
            /***
             * For Lists
             ***/
            Page.RegisterClientScript(typeof(Masters.ClientScripts.ListContactViewData));
            Page.RegisterClientScript(typeof(Masters.ClientScripts.ListTaskViewData));
            Page.RegisterClientScript(typeof(Masters.ClientScripts.ListCasesViewData));
            Page.RegisterClientScript(typeof(Masters.ClientScripts.ListDealViewData));
            Page.RegisterClientScript(typeof(Masters.ClientScripts.ListInvoiceViewData));
             /*******/

            Utility.RegisterTypeForAjax(typeof(CommonSettingsView));

            //init PrivatePanel for contacts
            var privatePanel = (PrivatePanel)LoadControl(PrivatePanel.Location);
            var usersWhoHasAccess = new List<string> { CustomNamingPeople.Substitute<CRMCommonResource>("CurrentUser").HtmlEncode() };
            privatePanel.UsersWhoHasAccess = usersWhoHasAccess;
            privatePanel.DisabledUsers = new List<Guid> { SecurityContext.CurrentAccount.ID };
            privatePanel.HideNotifyPanel = true;
            _phPrivatePanel.Controls.Add(privatePanel);

            Page.RegisterClientScript(typeof(Masters.ClientScripts.ListDealViewData));
            Page.RegisterClientScript(typeof(Masters.ClientScripts.ExchangeRateViewData));
        }


        protected void RenderDashboardEmptyScreen()
        {
            var dashboardEmptyScreen = (DashboardEmptyScreen)Page.LoadControl(DashboardEmptyScreen.Location);
            _phDashboardEmptyScreen.Controls.Add(dashboardEmptyScreen);
        }

        #endregion
    }
}
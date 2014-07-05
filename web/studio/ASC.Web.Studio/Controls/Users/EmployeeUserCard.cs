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

using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASC.Core.Users;
using ASC.Web.Core.Utility.Skins;
using System.Web;
using ASC.Core;
using ASC.Web.Studio.Utility;

namespace ASC.Web.Studio.Controls.Users
{
    [ToolboxData("<{0}:EmployeeUserCard runat=\"server\"/>")]
    public class EmployeeUserCard : Control
    {

        private string _CssClass = string.Empty;
        public string CssClass
        {
            get
            {
                if (ViewState["CssClass"] == null || ViewState["CssClass"].ToString().Equals(string.Empty))
                {
                    return _CssClass;
                }
                return ViewState["CssClass"].ToString();
            }
            set
            {
                ViewState["CssClass"] = value;
            }
        }

        public string EmployeeUrl
        {
            get
            {
                if (ViewState["EmployeeUrl"] == null || ViewState["EmployeeUrl"].ToString().Equals(string.Empty))
                {
                    return string.Empty; ;
                }
                return ViewState["EmployeeUrl"].ToString();
            }
            set
            {
                ViewState["EmployeeUrl"] = value;
            }
        }

        public Unit Height
        {
            get
            {
                if (ViewState["Height"] == null || ViewState["Height"].ToString().Equals(string.Empty))
                {
                    return Unit.Parse("122px");
                }
                return Unit.Parse(ViewState["Height"].ToString());
            }
            set
            {
                ViewState["Height"] = value.ToString();
            }
        }


        public Unit Width
        {
            get
            {
                if (ViewState["Width"] == null || ViewState["Width"].ToString().Equals(string.Empty))
                {
                    return Unit.Parse("352px");
                }
                return Unit.Parse(ViewState["Width"].ToString());
            }
            set
            {
                ViewState["Width"] = value.ToString();
            }
        }

        public UserInfo EmployeeInfo { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            if (EmployeeInfo != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<div {1} style=\"width: {0}px;overflow:hidden;\"><table cellpadding=\"0\" border=\"0\" cellspacing=\"0\" width=\"100%\" >", Width.Value,
                    EmployeeInfo.ActivationStatus == EmployeeActivationStatus.Pending ? "class=\"pending\"" : "");

                sb.Append("<tr valign='top'>");
                sb.Append("<td align=\"left\" style=\"width:56px; padding-right:10px;\">");
                sb.AppendFormat("<a class=\"borderBase\" {1} href=\"{0}\">", EmployeeUrl, "style=\"position:relative;  text-decoration:none; display:block; height:48px; width:48px;\"");
                sb.Append("<img align=\"center\" alt=\"\" style='display:block;margin:0; position:relative;' border=0 src=\"" + EmployeeInfo.GetMediumPhotoURL() + "\"/>");
                if (EmployeeInfo.ActivationStatus == EmployeeActivationStatus.Pending)
                {
                    sb.Append("<div class=\"pendingInfo borderBase tintMedium\"><div>" + Resources.Resource.PendingTitle + "</div></div>");
                }
                sb.Append("</a>");
                sb.Append("</td>");

                sb.Append("<td>");
                if (!EmployeeInfo.ID.Equals(ASC.Core.Users.Constants.LostUser.ID))
                {
                    sb.Append("<div>");
                    sb.AppendFormat("<a class=\"link header-base middle bold\" data-id=\"{2}\" href=\"{0}\" title=\"{1}\">{1}</a>", EmployeeUrl, EmployeeInfo.DisplayUserName(), EmployeeInfo.ID);
                    sb.Append("</div>");

                    //department
                    sb.Append("<div style=\"padding-top: 6px;\">");
                    if (EmployeeInfo.Status != EmployeeStatus.Terminated)
                    {
                        var removecoma = false;
                        foreach (var g in CoreContext.UserManager.GetUserGroups(EmployeeInfo.ID))
                        {
                            sb.AppendFormat("<a class=\"link\" href=\"{0}\">", CommonLinkUtility.GetDepartment(g.ID));
                            sb.Append(HttpUtility.HtmlEncode(g.Name));
                            sb.Append("</a>, ");
                            removecoma = true;
                        }
                        if (removecoma)
                        {
                            sb.Remove(sb.Length - 2, 2);
                        }
                    }
                    sb.Append("&nbsp;</div>");

                    sb.Append("<div style=\"padding-top: 6px;\">");
                    sb.Append(HttpUtility.HtmlEncode(EmployeeInfo.Title) ?? "");
                    sb.Append("&nbsp;</div>");
                }

                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("</table></div>");
                writer.Write(sb.ToString());
            }

            base.Render(writer);
        }
    }
}
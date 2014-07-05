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

using System;
using ASC.Notify.Model;
using ASC.Notify.Patterns;
using ASC.Web.UserControls.Wiki.Resources;
using AuthAction = ASC.Common.Security.Authorizing.Action;

namespace ASC.Web.Community.Wiki.Common
{
    public class Constants
    {
        public static readonly AuthAction Action_AddPage = new AuthAction(new Guid("D49F4E30-DA10-4b39-BC6D-B41EF6E039D3"), "New Page");
        public static readonly AuthAction Action_EditPage = new AuthAction(new Guid("D852B66F-6719-45e1-8657-18F0BB791690"), "Edit page");
        public static readonly AuthAction Action_RemovePage = new AuthAction(new Guid("557D6503-633B-4490-A14C-6473147CE2B3"), "Delete page");
        public static readonly AuthAction Action_UploadFile = new AuthAction(new Guid("088D5940-A80F-4403-9741-D610718CE95C"), "Upload file");
        public static readonly AuthAction Action_RemoveFile = new AuthAction(new Guid("7CB5C0D1-D254-433f-ABE3-FF23373EC631"), "Delete file");
        public static readonly AuthAction Action_AddComment = new AuthAction(new Guid("C426C349-9AD4-47cd-9B8F-99FC30675951"), "Add Comment");
        public static readonly AuthAction Action_EditRemoveComment = new AuthAction(new Guid("B630D29B-1844-4bda-BBBE-CF5542DF3559"), "Edit/Delete comment");

        public static INotifyAction NewPage = new NotifyAction("new wiki page", WikiResource.NotifyAction_NewPage);
        public static INotifyAction EditPage = new NotifyAction("edit wiki page", WikiResource.NotifyAction_ChangePage);
        public static INotifyAction AddPageToCat = new NotifyAction("add page to cat", WikiResource.NotifyAction_AddPageToCat);

        public static string TagPageName = "PageName";
        public static string TagURL = "URL";

        public static string TagUserName = "UserName";
        public static string TagUserURL = "UserURL";
        public static string TagDate = "Date";

        public static string TagPostPreview = "PagePreview";
        public static string TagCommentBody = "CommentBody";

        public static string TagChangePageType = "ChangeType";
        public static string TagCatName = "CategoryName";

    }
}
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

namespace ASC.Mail.Net.AUTH
{
    #region usings

    using System;

    #endregion

    /// <summary>
    /// This class provides data for server authentication mechanisms <b>GetUserInfo</b> event.
    /// </summary>
    public class AUTH_e_UserInfo : EventArgs
    {
        #region Members

        private readonly string m_UserName = "";
        private string m_Password = "";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if specified user exists.
        /// </summary>
        public bool UserExists { get; set; }

        /// <summary>
        /// Gets user name.
        /// </summary>
        public string UserName
        {
            get { return m_UserName; }
        }

        /// <summary>
        /// Gets or sets user password.
        /// </summary>
        public string Password
        {
            get { return m_Password; }

            set { m_Password = value; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>userName</b> is null reference.</exception>
        /// <exception cref="ArgumentException">Is raised when any of the arguments has invalid value.</exception>
        public AUTH_e_UserInfo(string userName)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("userName");
            }
            if (userName == string.Empty)
            {
                throw new ArgumentException("Argument 'userName' value must be specified.", "userName");
            }

            m_UserName = userName;
        }

        #endregion
    }
}
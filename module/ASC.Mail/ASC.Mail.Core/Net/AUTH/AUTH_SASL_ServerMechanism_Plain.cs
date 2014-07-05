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
    /// Implements "PLAIN" authenticaiton. Defined in RFC 4616.
    /// </summary>
    public class AUTH_SASL_ServerMechanism_Plain : AUTH_SASL_ServerMechanism
    {
        #region Events

        /// <summary>
        /// Is called when authentication mechanism needs to authenticate specified user.
        /// </summary>
        public event EventHandler<AUTH_e_Authenticate> Authenticate = null;

        #endregion

        #region Members

        private readonly bool m_RequireSSL;
        private bool m_IsAuthenticated;
        private bool m_IsCompleted;
        private string m_UserName = "";

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the authentication exchange has completed.
        /// </summary>
        public override bool IsCompleted
        {
            get { return m_IsCompleted; }
        }

        /// <summary>
        /// Gets if user has authenticated sucessfully.
        /// </summary>
        public override bool IsAuthenticated
        {
            get { return m_IsAuthenticated; }
        }

        /// <summary>
        /// Returns always "PLAIN".
        /// </summary>
        public override string Name
        {
            get { return "PLAIN"; }
        }

        /// <summary>
        /// Gets if specified SASL mechanism is available only to SSL connection.
        /// </summary>
        public override bool RequireSSL
        {
            get { return m_RequireSSL; }
        }

        /// <summary>
        /// Gets user login name.
        /// </summary>
        public override string UserName
        {
            get { return m_UserName; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="requireSSL">Specifies if this mechanism is available to SSL connections only.</param>
        public AUTH_SASL_ServerMechanism_Plain(bool requireSSL)
        {
            m_RequireSSL = requireSSL;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Continues authentication process.
        /// </summary>
        /// <param name="clientResponse">Client sent SASL response.</param>
        /// <returns>Retunrns challange response what must be sent to client or null if authentication has completed.</returns>
        /// <exception cref="ArgumentNullException">Is raised when <b>clientResponse</b> is null reference.</exception>
        public override string Continue(string clientResponse)
        {
            if (clientResponse == null)
            {
                throw new ArgumentNullException("clientResponse");
            }

            /* RFC 4616.2. PLAIN SASL Mechanism.                
                The mechanism consists of a single message, a string of [UTF-8]
                encoded [Unicode] characters, from the client to the server.  The
                client presents the authorization identity (identity to act as),
                followed by a NUL (U+0000) character, followed by the authentication
                identity (identity whose password will be used), followed by a NUL
                (U+0000) character, followed by the clear-text password.  As with
                other SASL mechanisms, the client does not provide an authorization
                identity when it wishes the server to derive an identity from the
                credentials and use that as the authorization identity.
             
                message   = [authzid] UTF8NUL authcid UTF8NUL passwd
             
                Example:
                    C: a002 AUTHENTICATE "PLAIN"
                    S: + ""
                    C: {21}
                    C: <NUL>tim<NUL>tanstaaftanstaaf
                    S: a002 OK "Authenticated"
            */

            if (clientResponse == string.Empty)
            {
                return "";
            }
                // Parse response
            else
            {
                string[] authzid_authcid_passwd = clientResponse.Split('\0');
                if (authzid_authcid_passwd.Length == 3 && !string.IsNullOrEmpty(authzid_authcid_passwd[1]))
                {
                    m_UserName = authzid_authcid_passwd[1];
                    AUTH_e_Authenticate result = OnAuthenticate(authzid_authcid_passwd[0],
                                                                authzid_authcid_passwd[1],
                                                                authzid_authcid_passwd[2]);
                    m_IsAuthenticated = result.IsAuthenticated;
                }

                m_IsCompleted = true;
            }

            return null;
        }

        #endregion

        #region Utility methods

        /// <summary>
        /// Raises <b>Authenticate</b> event.
        /// </summary>
        /// <param name="authorizationID">Authorization ID.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Returns authentication result.</returns>
        private AUTH_e_Authenticate OnAuthenticate(string authorizationID, string userName, string password)
        {
            AUTH_e_Authenticate retVal = new AUTH_e_Authenticate(authorizationID, userName, password);

            if (Authenticate != null)
            {
                Authenticate(this, retVal);
            }

            return retVal;
        }

        #endregion
    }
}
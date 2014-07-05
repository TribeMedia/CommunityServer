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

namespace ASC.Mail.Net
{
    #region usings

    using System;

    #endregion

    /// <summary>
    /// Implements absolute-URI. Defined in RFC 3986.4.3.
    /// </summary>
    public class AbsoluteUri
    {
        #region Members

        private string m_Scheme = "";
        private string m_Value = "";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        internal AbsoluteUri() {}

        #endregion

        #region Properties

        /// <summary>
        /// Gets URI scheme.
        /// </summary>
        public virtual string Scheme
        {
            get { return m_Scheme; }
        }

        /// <summary>
        /// Gets URI value after scheme.
        /// </summary>
        public string Value
        {
            get { return ToString().Split(new[] {':'}, 2)[1]; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parse URI from string value.
        /// </summary>
        /// <param name="value">String URI value.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>value</b> is null reference.</exception>
        /// <exception cref="ArgumentException">Is raised when <b>value</b> has invalid URI value.</exception>
        public static AbsoluteUri Parse(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            if (value == "")
            {
                throw new ArgumentException("Argument 'value' value must be specified.");
            }

            string[] scheme_value = value.Split(new[] {':'}, 2);
            if (scheme_value[0].ToLower() == UriSchemes.sip || scheme_value[0].ToLower() == UriSchemes.sips)
            {
                SIP_Uri uri = new SIP_Uri();
                uri.ParseInternal(value);

                return uri;
            }
            else
            {
                AbsoluteUri uri = new AbsoluteUri();
                uri.ParseInternal(value);

                return uri;
            }
        }

        /// <summary>
        /// Converts URI to string.
        /// </summary>
        /// <returns>Returns URI as string.</returns>
        public override string ToString()
        {
            return m_Scheme + ":" + m_Value;
        }

        #endregion

        #region Virtual methods

        /// <summary>
        /// Parses URI from the specified string.
        /// </summary>
        /// <param name="value">URI string.</param>
        /// <exception cref="ArgumentNullException">Is raised when <b>value</b> is null reference.</exception>
        protected virtual void ParseInternal(string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            string[] scheme_value = value.Split(new[] {':'}, 1);
            m_Scheme = scheme_value[0].ToLower();
            if (scheme_value.Length == 2)
            {
                m_Value = scheme_value[1];
            }
        }

        #endregion
    }
}
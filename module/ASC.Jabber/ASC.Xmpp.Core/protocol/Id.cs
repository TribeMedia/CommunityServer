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

// // --------------------------------------------------------------------------------------------------------------------
// // <copyright company="Ascensio System Limited" file="Id.cs">
// //   
// // </copyright>
// // <summary>
// //   (c) Copyright Ascensio System Limited 2008-2012
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

#region file header

#endregion

using System;

namespace ASC.Xmpp.Core.protocol
{

    #region usings

    #endregion

    /// <summary>
    /// </summary>
    public enum IdType
    {
        /// <summary>
        ///   Numeric Id's are generated by increasing a long value
        /// </summary>
        Numeric,

        /// <summary>
        ///   Guid Id's are unique, Guid packet Id's should be used for server and component applications, or apps which very long sessions (multiple days, weeks or years)
        /// </summary>
        Guid
    }

    /// <summary>
    ///   This class takes care anout out unique Message Ids
    /// </summary>
    public class Id
    {
        /// <summary>
        /// </summary>
        private static long m_id;

        /// <summary>
        /// </summary>
        private static string m_Prefix = "agsXMPP_";

        /// <summary>
        /// </summary>
        private static IdType m_Type = IdType.Numeric;

        /// <summary>
        /// </summary>
        public static IdType Type
        {
            get { return m_Type; }

#if !CF
            // readyonly on CF1
            set { m_Type = value; }

#endif
        }

#if !CF

        /// <summary>
        /// </summary>
        /// <returns> </returns>
        public static string GetNextId()
        {
            if (m_Type == IdType.Numeric)
            {
                m_id++;
                return m_Prefix + m_id;
            }
            else
            {
                return m_Prefix + Guid.NewGuid();
            }
        }

#else
        
    
    
    // On CF 1.0 we have no GUID class, so only increasing numberical id's are supported
    // We could create GUID's on CF 1.0 with the Crypto API if we want to.
        public static string GetNextId()
        {            
            m_id++;
            return m_Prefix + m_id.ToString();
        }

#endif

        /// <summary>
        ///   Reset the id counter to agsXmpp_1 again
        /// </summary>
        public static void Reset()
        {
            m_id = 0;
        }

        /// <summary>
        ///   to Save Bandwidth on Mobile devices you can change the prefix null is also possible to optimize Bandwidth usage
        /// </summary>
        public static string Prefix
        {
            get { return m_Prefix; }

            set
            {
                if (value == null)
                {
                    m_Prefix = string.Empty;
                }
                else
                {
                    m_Prefix = value;
                }
            }
        }
    }
}
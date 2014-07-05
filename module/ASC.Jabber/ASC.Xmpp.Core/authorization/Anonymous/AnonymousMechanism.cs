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
// // <copyright company="Ascensio System Limited" file="AnonymousMechanism.cs">
// //   
// // </copyright>
// // <summary>
// //   (c) Copyright Ascensio System Limited 2008-2012
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

#region using

using ASC.Xmpp.Core.utils.Xml.Dom;

#endregion

namespace ASC.Xmpp.Core.authorization.Anonymous
{

    #region usings

    #endregion

    /// <summary>
    ///   SALS ANONYMOUS Mechanism, this allows anonymous logins to a xmpp server.
    /// </summary>
    public class AnonymousMechanism : Mechanism
    {
        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="con"> </param>
        public override void Init()
        {
            //con.Send(new Auth(MechanismType.ANONYMOUS));
        }

        /// <summary>
        /// </summary>
        /// <param name="e"> </param>
        public override void Parse(Node e)
        {
            // *No Challenges* in SASL ANONYMOUS
        }

        #endregion

        /*
            S: <stream:features>
                    <mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>
                        <mechanism>DIGEST-MD5<mechanism>
                        <mechanism>ANONYMOUS<mechanism>
                    </mechanisms>
               </stream:features>
            
            * So, the proper exchange for this ANONYMOUS mechanism would be:

            C: <auth xmlns='urn:ietf:params:xml:ns:xmpp-sasl' mechanism='ANONYMOUS'/>
            S: <success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>

            or, in case of the optional trace information:

            C: <auth xmlns='urn:ietf:params:xml:ns:xmpp-sasl' mechanism='ANONYMOUS'>
                    c2lyaGM=
               </auth>
            S: <success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>

        */
    }
}
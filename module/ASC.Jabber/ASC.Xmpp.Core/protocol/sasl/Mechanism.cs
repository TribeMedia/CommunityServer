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
// // <copyright company="Ascensio System Limited" file="Mechanism.cs">
// //   
// // </copyright>
// // <summary>
// //   (c) Copyright Ascensio System Limited 2008-2012
// // </summary>
// // --------------------------------------------------------------------------------------------------------------------

using ASC.Xmpp.Core.utils.Xml.Dom;

//	<mechanism>DIGEST-MD5</mechanism>
//	<mechanism>PLAIN</mechanism>

//MECHANISMS           USAGE    REFERENCE   OWNER
//----------           -----    ---------   -----
//KERBEROS_V4          LIMITED  [RFC2222]   IESG <iesg@ietf.org>
//GSSAPI               COMMON   [RFC2222]   IESG <iesg@ietf.org> 
//SKEY                 OBSOLETE [RFC2444]   IESG <iesg@ietf.org>
//EXTERNAL             COMMON   [RFC2222]   IESG <iesg@ietf.org>
//CRAM-MD5             LIMITED  [RFC2195]   IESG <iesg@ietf.org> 
//ANONYMOUS            COMMON   [RFC2245]   IESG <iesg@ietf.org>
//OTP                  COMMON   [RFC2444]   IESG <iesg@ietf.org>
//GSS-SPNEGO           LIMITED  [Leach]     Paul Leach <paulle@microsoft.com>
//PLAIN                COMMON   [RFC2595]   IESG <iesg@ietf.org>
//SECURID              COMMON   [RFC2808]   Magnus Nystrom <magnus@rsasecurity.com>
//NTLM                 LIMITED  [Leach]     Paul Leach <paulle@microsoft.com>
//NMAS_LOGIN           LIMITED  [Gayman]    Mark G. Gayman <mgayman@novell.com>
//NMAS_AUTHEN          LIMITED  [Gayman]    Mark G. Gayman <mgayman@novell.com>
//DIGEST-MD5           COMMON   [RFC2831]   IESG <iesg@ietf.org>
//9798-U-RSA-SHA1-ENC  COMMON    [RFC3163]  robert.zuccherato@entrust.com
//9798-M-RSA-SHA1-ENC  COMMON   [RFC3163]   robert.zuccherato@entrust.com
//9798-U-DSA-SHA1      COMMON   [RFC3163]   robert.zuccherato@entrust.com
//9798-M-DSA-SHA1      COMMON   [RFC3163]   robert.zuccherato@entrust.com
//9798-U-ECDSA-SHA1    COMMON   [RFC3163]   robert.zuccherato@entrust.com
//9798-M-ECDSA-SHA1    COMMON   [RFC3163]   robert.zuccherato@entrust.com
//KERBEROS_V5          COMMON   [Josefsson] Simon Josefsson <simon@josefsson.org>
//NMAS-SAMBA-AUTH      LIMITED  [Brimhall]  Vince Brimhall <vbrimhall@novell.com>

namespace ASC.Xmpp.Core.protocol.sasl
{
    public enum MechanismType
    {
        NONE = 0,
        KERBEROS_V4,
        GSSAPI,
        SKEY,
        EXTERNAL,
        CRAM_MD5,
        ANONYMOUS,
        OTP,
        GSS_SPNEGO,
        PLAIN,
        SECURID,
        NTLM,
        NMAS_LOGIN,
        NMAS_AUTHEN,
        DIGEST_MD5,
        ISO_9798_U_RSA_SHA1_ENC,
        ISO_9798_M_RSA_SHA1_ENC,
        ISO_9798_U_DSA_SHA1,
        ISO_9798_M_DSA_SHA1,
        ISO_9798_U_ECDSA_SHA1,
        ISO_9798_M_ECDSA_SHA1,
        KERBEROS_V5,
        NMAS_SAMBA_AUTH,
        X_GOOGLE_TOKEN
    }

    /// <summary>
    ///   Summary description for Mechanism.
    /// </summary>
    public class Mechanism : Element
    {
        public Mechanism()
        {
            TagName = "mechanism";
            Namespace = Uri.SASL;
        }

        public Mechanism(MechanismType mechanism) : this()
        {
            MechanismType = mechanism;
        }

        /// <summary>
        ///   SASL mechanis as enum
        /// </summary>
        public MechanismType MechanismType
        {
            get { return GetMechanismType(Value); }
            set { Value = GetMechanismName(value); }
        }

        public static MechanismType GetMechanismType(string mechanism)
        {
            switch (mechanism)
            {
                    //case "KERBEROS_V4":
                    //    return MechanismType.KERBEROS_V4;
                    //case "GSSAPI":
                    //    return MechanismType.GSSAPI;
                    //case "SKEY":
                    //    return MechanismType.SKEY;
                    //case "EXTERNAL":
                    //    return MechanismType.EXTERNAL;
                    //case "CRAM-MD5":
                    //    return MechanismType.CRAM_MD5;
                    //case "ANONYMOUS":
                    //    return MechanismType.ANONYMOUS;
                    //case "OTP":
                    //    return MechanismType.OTP;
                    //case "GSS-SPNEGO":
                    //    return MechanismType.GSS_SPNEGO;
                case "PLAIN":
                    return MechanismType.PLAIN;
                    //case "SECURID":
                    //    return MechanismType.SECURID;
                    //case "NTLM":
                    //    return MechanismType.NTLM;
                    //case "NMAS_LOGIN":
                    //    return MechanismType.NMAS_LOGIN;
                    //case "NMAS_AUTHEN":
                    //    return MechanismType.NMAS_AUTHEN;
                case "DIGEST-MD5":
                    return MechanismType.DIGEST_MD5;
                    //case "9798-U-RSA-SHA1-ENC":
                    //    return MechanismType.ISO_9798_U_RSA_SHA1_ENC;
                    //case "9798-M-RSA-SHA1-ENC":
                    //    return MechanismType.ISO_9798_M_RSA_SHA1_ENC;
                    //case "9798-U-DSA-SHA1":
                    //    return MechanismType.ISO_9798_U_DSA_SHA1;
                    //case "9798-M-DSA-SHA1":
                    //    return MechanismType.ISO_9798_M_DSA_SHA1;
                    //case "9798-U-ECDSA-SHA1":
                    //    return MechanismType.ISO_9798_U_ECDSA_SHA1;
                    //case "9798-M-ECDSA-SHA1":
                    //    return MechanismType.ISO_9798_M_ECDSA_SHA1;
                    //case "KERBEROS_V5":
                    //    return MechanismType.KERBEROS_V5;
                    //case "NMAS-SAMBA-AUTH":
                    //    return MechanismType.NMAS_SAMBA_AUTH;				
                case "X-GOOGLE-TOKEN":
                    return MechanismType.X_GOOGLE_TOKEN;
                default:
                    return MechanismType.NONE;
            }
        }

        public static string GetMechanismName(MechanismType mechanism)
        {
            switch (mechanism)
            {
                case MechanismType.KERBEROS_V4:
                    return "KERBEROS_V4";
                case MechanismType.GSSAPI:
                    return "GSSAPI";
                case MechanismType.SKEY:
                    return "SKEY";
                case MechanismType.EXTERNAL:
                    return "EXTERNAL";
                case MechanismType.CRAM_MD5:
                    return "CRAM-MD5";
                case MechanismType.ANONYMOUS:
                    return "ANONYMOUS";
                case MechanismType.OTP:
                    return "OTP";
                case MechanismType.GSS_SPNEGO:
                    return "GSS-SPNEGO";
                case MechanismType.PLAIN:
                    return "PLAIN";
                case MechanismType.SECURID:
                    return "SECURID";
                case MechanismType.NTLM:
                    return "NTLM";
                case MechanismType.NMAS_LOGIN:
                    return "NMAS_LOGIN";
                case MechanismType.NMAS_AUTHEN:
                    return "NMAS_AUTHEN";
                case MechanismType.DIGEST_MD5:
                    return "DIGEST-MD5";
                case MechanismType.ISO_9798_U_RSA_SHA1_ENC:
                    return "9798-U-RSA-SHA1-ENC";
                case MechanismType.ISO_9798_M_RSA_SHA1_ENC:
                    return "9798-M-RSA-SHA1-ENC";
                case MechanismType.ISO_9798_U_DSA_SHA1:
                    return "9798-U-DSA-SHA1";
                case MechanismType.ISO_9798_M_DSA_SHA1:
                    return "9798-M-DSA-SHA1";
                case MechanismType.ISO_9798_U_ECDSA_SHA1:
                    return "9798-U-ECDSA-SHA1";
                case MechanismType.ISO_9798_M_ECDSA_SHA1:
                    return "9798-M-ECDSA-SHA1";
                case MechanismType.KERBEROS_V5:
                    return "KERBEROS_V5";
                case MechanismType.NMAS_SAMBA_AUTH:
                    return "NMAS-SAMBA-AUTH";
                case MechanismType.X_GOOGLE_TOKEN:
                    return "X-GOOGLE-TOKEN";
                default:
                    return null;
            }
        }
    }
}
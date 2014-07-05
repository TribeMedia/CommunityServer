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
using System.Text.RegularExpressions;
using System.Web;
using ASC.Common.Notify.Patterns;
using ASC.Notify.Messages;
using ASC.Notify.Patterns;

namespace ASC.Notify.Textile
{
    public class JabberStyler : IPatternStyler
    {
        static readonly Regex VelocityArguments = new Regex(NVelocityPatternFormatter.NoStylePreffix + "(?<arg>.*?)" + NVelocityPatternFormatter.NoStyleSuffix, RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        static readonly Regex LinkReplacer = new Regex(@"""(?<text>[\w\W]+?)"":""(?<link>[^""]+)""", RegexOptions.Singleline | RegexOptions.Compiled);
        static readonly Regex TextileReplacer = new Regex(@"(h1\.|h2\.|\*|h3\.|\^)", RegexOptions.Singleline | RegexOptions.Compiled);
        static readonly Regex BrReplacer = new Regex(@"<br\s*\/*>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.Singleline);
        static readonly Regex TagReplacer = new Regex(@"<(.|\n)*?>", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled | RegexOptions.Singleline);

        public void ApplyFormating(NoticeMessage message)
        {
            var body = string.Empty;
            if (!string.IsNullOrEmpty(message.Subject))
            {
                body += VelocityArguments.Replace(message.Subject, ArgMatchReplace) + Environment.NewLine;
                message.Subject = string.Empty;
            }
            if (string.IsNullOrEmpty(message.Body)) return;
            var lines = message.Body.Split(new[] {Environment.NewLine, "\n"}, StringSplitOptions.None);
            for (var i = 0; i < lines.Length - 1; i++)
            {
                if (string.IsNullOrEmpty(lines[i])) { body += Environment.NewLine; continue; }
                lines[i] = VelocityArguments.Replace(lines[i], ArgMatchReplace);
                body += LinkReplacer.Replace(lines[i], EvalLink) + Environment.NewLine;
            }
            lines[lines.Length - 1] = VelocityArguments.Replace(lines[lines.Length - 1], ArgMatchReplace);
            body += LinkReplacer.Replace(lines[lines.Length - 1], EvalLink);
            body = TextileReplacer.Replace(HttpUtility.HtmlDecode(body), ""); //Kill textile markup
            body = BrReplacer.Replace(body, Environment.NewLine);
            body = TagReplacer.Replace(body, Environment.NewLine);
            message.Body = body;
        }

        private static string EvalLink(Match match)
        {
            if (match.Success)
            {
                if (match.Groups["text"].Success && match.Groups["link"].Success)
                {
                    if (match.Groups["text"].Value.Equals(match.Groups["link"].Value,StringComparison.OrdinalIgnoreCase))
                    {
                        return " " + match.Groups["text"].Value + " ";
                    }
                    return match.Groups["text"].Value + string.Format(" ( {0} )", match.Groups["link"].Value);
                }
            }
            return match.Value;
        }

        private static string ArgMatchReplace(Match match)
        {
            return match.Result("${arg}");
        }
    }
}

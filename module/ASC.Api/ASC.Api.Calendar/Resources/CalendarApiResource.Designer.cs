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

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASC.Api.Calendar.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CalendarApiResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CalendarApiResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ASC.Api.Calendar.Resources.CalendarApiResource", typeof(CalendarApiResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reminds about users&apos; birthdays.
        /// </summary>
        internal static string BirthdayCalendarDescription {
            get {
                return ResourceManager.GetString("BirthdayCalendarDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Users&apos; birthdays.
        /// </summary>
        internal static string BirthdayCalendarName {
            get {
                return ResourceManager.GetString("BirthdayCalendarName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Common calendars.
        /// </summary>
        internal static string CommonCalendarsGroup {
            get {
                return ResourceManager.GetString("CommonCalendarsGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to My calendar.
        /// </summary>
        internal static string DefaultCalendarName {
            get {
                return ResourceManager.GetString("DefaultCalendarName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name can&apos;t be empty.
        /// </summary>
        internal static string ErrorEmptyName {
            get {
                return ResourceManager.GetString("ErrorEmptyName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Date format is incorrect.
        /// </summary>
        internal static string ErrorIncorrectDateFormat {
            get {
                return ResourceManager.GetString("ErrorIncorrectDateFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Item not found.
        /// </summary>
        internal static string ErrorItemNotFound {
            get {
                return ResourceManager.GetString("ErrorItemNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Full Access.
        /// </summary>
        internal static string FullAccessOption {
            get {
                return ResourceManager.GetString("FullAccessOption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to iCal Calendars.
        /// </summary>
        internal static string iCalCalendarsGroup {
            get {
                return ResourceManager.GetString("iCalCalendarsGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No title.
        /// </summary>
        internal static string NoNameCalendar {
            get {
                return ResourceManager.GetString("NoNameCalendar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No title.
        /// </summary>
        internal static string NoNameEvent {
            get {
                return ResourceManager.GetString("NoNameEvent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Owner.
        /// </summary>
        internal static string OwnerOption {
            get {
                return ResourceManager.GetString("OwnerOption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Personal calendars.
        /// </summary>
        internal static string PersonalCalendarsGroup {
            get {
                return ResourceManager.GetString("PersonalCalendarsGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Read only.
        /// </summary>
        internal static string ReadOption {
            get {
                return ResourceManager.GetString("ReadOption", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Calendars shared with me.
        /// </summary>
        internal static string SharedCalendarsGroup {
            get {
                return ResourceManager.GetString("SharedCalendarsGroup", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Here are all events, which another users shared with me.
        /// </summary>
        internal static string SharedEventsCalendarDescription {
            get {
                return ResourceManager.GetString("SharedEventsCalendarDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Events shared with me.
        /// </summary>
        internal static string SharedEventsCalendarName {
            get {
                return ResourceManager.GetString("SharedEventsCalendarName", resourceCulture);
            }
        }
        
        /// <summary>
        
        /// </summary>
        internal static string WrongiCalFeedLink {
            get {
                return ResourceManager.GetString("WrongiCalFeedLink", resourceCulture);
            }
        }
    }
}

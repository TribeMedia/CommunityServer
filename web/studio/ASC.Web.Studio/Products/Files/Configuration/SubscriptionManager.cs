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
using System.Collections.Generic;
using ASC.Notify.Model;
using ASC.Web.Core.Subscriptions;
using ASC.Web.Files.Services.NotifyService;

namespace ASC.Web.Files.Classes
{
    public class SubscriptionManager : IProductSubscriptionManager
    {
        private readonly Guid subscrTypeShareDoc = new Guid("{552846EC-AC94-4408-AAC6-17C8989B8B38}");
        private readonly Guid subscrTypeShareFolder = new Guid("{0292A4F4-0687-42a6-9CE4-E21215045ABE}");
        
        public GroupByType GroupByType
        {
            get { return GroupByType.Simple; }
        }

        public List<SubscriptionObject> GetSubscriptionObjects(Guid subItem)
        {
            return new List<SubscriptionObject>();
        }

        public List<SubscriptionType> GetSubscriptionTypes()
        {
            return new List<SubscriptionType>
                                    {
                                        new SubscriptionType
                                            {
                                                ID = subscrTypeShareDoc,
                                                Name = Resources.FilesCommonResource.SubscriptForAccess,
                                                NotifyAction = NotifyConstants.Event_ShareDocument,
                                                Single = true,
                                                CanSubscribe = true
                                            },
                                        new SubscriptionType
                                            {
                                                ID = subscrTypeShareFolder,
                                                Name = Resources.FilesCommonResource.ShareFolder,
                                                NotifyAction = NotifyConstants.Event_ShareFolder,
                                                Single = true,
                                                CanSubscribe = true
                                            }
                                    };
        }

        public ISubscriptionProvider SubscriptionProvider
        {
            get { return NotifySource.Instance.GetSubscriptionProvider(); }
        }

        public List<SubscriptionGroup> GetSubscriptionGroups()
        {
            return new List<SubscriptionGroup>();
        }
    }
}
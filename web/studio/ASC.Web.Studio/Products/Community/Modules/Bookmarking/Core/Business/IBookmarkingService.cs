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

#region Usings

using System;
using System.Collections.Generic;
using ASC.Bookmarking.Pojo;
using ASC.Core.Users;
using ASC.Notify.Model;

#endregion

namespace ASC.Bookmarking.Business
{
	public interface IBookmarkingService
	{
		IList<Bookmark> GetAllBookmarks(int firstResult, int maxResults);
		IList<Bookmark> GetAllBookmarks();

		Bookmark AddBookmark(Bookmark b, IList<Tag> tags);

		Bookmark UpdateBookmark(UserBookmark userBookmark, IList<Tag> tags);
		Bookmark UpdateBookmark(Bookmark bookmark, IList<Tag> tags);
		
		UserInfo GetCurrentUser();		
		
		IList<Tag> GetAllTags(string startSymbols, int limit);

		IList<Tag> GetAllTags();

		Bookmark GetBookmarkByUrl(string url);

		Bookmark GetBookmarkByID(long id);


		IList<UserBookmark> GetUserBookmarks(Bookmark b);

		UserBookmark GetCurrentUserBookmark(Bookmark b);

		Bookmark GetBookmarkWithUserBookmarks(string url);



		Bookmark RemoveBookmarkFromFavourite(long bookmarkID);

		IList<Bookmark> GetFavouriteBookmarksSortedByRaiting(int firstResult, int maxResults);
		IList<Bookmark> GetFavouriteBookmarksSortedByDate(int firstResult, int maxResults);


		IList<Bookmark> GetMostRecentBookmarks(int firstResult, int maxResults);
		IList<Bookmark> GetMostRecentBookmarksWithRaiting(int firstResult, int maxResults);
		IList<Bookmark> GetTopOfTheDay(int firstResult, int maxResults);
		IList<Bookmark> GetTopOfTheWeek(int firstResult, int maxResults);
		IList<Bookmark> GetTopOfTheMonth(int firstResult, int maxResults);
		IList<Bookmark> GetTopOfTheYear(int firstResult, int maxResults);

		#region Tags		

		IList<Bookmark> GetMostPopularBookmarksByTag(Tag t);
		IList<Bookmark> GetMostPopularBookmarksByTag(IList<Tag> tags);
		IList<Tag> GetBookmarkTags(Bookmark b);
		IList<Tag> GetUserBookmarkTags(UserBookmark b);
		
        #endregion

		#region Comments
		
        Comment GetCommentById(Guid commentID);

		void AddComment(Comment comment);

		void UpdateComment(Guid commentID, string text);

		void RemoveComment(Guid commentID);

		long GetCommentsCount(long bookmarkID);

		IList<Comment> GetBookmarkComments(Bookmark b);

		IList<Comment> GetChildComments(Comment c);
		
        #endregion

		void Subscribe(string objectID, INotifyAction notifyAction);

		bool IsSubscribed(string objectID, INotifyAction notifyAction);

		void UnSubscribe(string objectID, INotifyAction notifyAction);

		#region Search
		
        IList<Bookmark> SearchBookmarks(IList<string> searchStringList, int firstResult, int maxResults);

		IList<Bookmark> SearchAllBookmarks(IList<string> searchStringList);

		IList<Bookmark> SearchBookmarksSortedByRaiting(IList<string> searchStringList, int firstResult, int maxResults);

		IList<Bookmark> SearchBookmarksByTag(string searchString, int firstResult, int maxResults);

		IList<Bookmark> SearchMostPopularBookmarksByTag(string tagName, int firstResult, int maxResults);
		
        #endregion

		IList<Bookmark> GetBookmarksCreatedByUser(Guid userID, int firstResult, int maxResults);

		IList<Bookmark> GetMostPopularBookmarksCreatedByUser(Guid userID, int firstResult, int maxResults);

		long GetBookmarksCountCreatedByUser(Guid userID);


		IList<Bookmark> GetFullBookmarksInfo(IList<long> bookmarkIds);

		IList<Bookmark> GetFullBookmarksInfo(IList<Bookmark> bookmarks);

		void SetBookmarkTags(Bookmark b);
	}
}

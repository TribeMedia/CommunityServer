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

/*
    Copyright (c) Ascensio System SIA 2013. All rights reserved.
    http://www.teamlab.com
*/
window.ASC.Files.EventHandler = (function () {
    var isInit = false;
    var timoutTasksStatuses = null;

    var init = function () {
        if (isInit === false) {
            isInit = true;

            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.GetFolderItems, ASC.Files.EventHandler.onGetFolderItems);

            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.CheckEditing, ASC.Files.EventHandler.onCheckEditing);

            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.LockFile, ASC.Files.EventHandler.onLockFile);

            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.UpdateIfExist, ASC.Files.EventHandler.onUpdateIfExist);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.GetHelpCenter, ASC.Files.EventHandler.onGetHelpCenter);

            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.CreateFolder, ASC.Files.EventHandler.onCreateFolder);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.CreateNewFile, ASC.Files.EventHandler.onCreateNewFile);

            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.FolderRename, ASC.Files.EventHandler.onRenameFolder);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.FileRename, ASC.Files.EventHandler.onRenameFile);

            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.GetFileHistory, ASC.Files.EventHandler.onGetFileHistory);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.SetCurrentVersion, ASC.Files.EventHandler.onUpdateHistory);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.CompleteVersion, ASC.Files.EventHandler.onUpdateHistory);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.ReplaceVersion, ASC.Files.EventHandler.onReplaceVersion);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.UpdateComment, ASC.Files.EventHandler.onUpdateComment);

            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.MoveFilesCheck, ASC.Files.EventHandler.onMoveFilesCheck);

            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.MoveItems, ASC.Files.EventHandler.onGetTasksStatuses);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.DeleteItem, ASC.Files.EventHandler.onGetTasksStatuses);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.EmptyTrash, ASC.Files.EventHandler.onGetTasksStatuses);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.Download, ASC.Files.EventHandler.onGetTasksStatuses);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.TerminateTasks, ASC.Files.EventHandler.onGetTasksStatuses);
            ASC.Files.ServiceManager.bind(ASC.Files.ServiceManager.events.GetTasksStatuses, ASC.Files.EventHandler.onGetTasksStatuses);
        }
    };

    /* Events */

    var onGetFolderItems = function (xmlData, params, errorMessage) {
        if (typeof errorMessage != "undefined" || typeof xmlData == "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);

            if (ASC.Files.Tree) {
                ASC.Files.Tree.resetFolder(ASC.Files.Tree.getParentId(params.folderId));
            }

            ASC.Files.Anchor.defaultFolderSet();
            return;
        }

        var htmlXML = ASC.Files.TemplateManager.translate(xmlData);

        ASC.Files.UI.resetSelectAll();

        ASC.Files.EmptyScreen.hideEmptyScreen();

        if (params.append == true) {
            jq("#filesMainContent").append(htmlXML);
            ASC.Files.Mouse.collectEntryItems();
        } else {
            jq("#filesMainContent").html(htmlXML);
        }

        //remove duplicate row
        jq("#filesMainContent .file-row[name=\"addRow\"]").each(function () {
            ASC.Files.UI.getObjectData(this).entryObject.filter("[name!=\"addRow\"]").remove();
        });

        var countTotal = 0;
        if (htmlXML != "") {
            countTotal = xmlData.getElementsByTagName("total")[0];
            countTotal = parseInt(countTotal.text || countTotal.textContent) || 0;
        }

        var countShowOnPage = parseInt(ASC.Files.Constants.COUNT_ON_PAGE) || 0;
        ASC.Files.UI.amountPage = parseInt((countTotal / countShowOnPage).toFixed(0));

        if (ASC.Files.UI.amountPage - (countTotal / countShowOnPage) < 0) {
            ASC.Files.UI.amountPage++;
        }

        ASC.Files.UI.currentPage = parseInt((jq("#filesMainContent .file-row[name!=\"addRow\"]").length - 1) / countShowOnPage) + 1;
        var countLeft = countTotal - jq("#filesMainContent .file-row").length;
        if (ASC.Files.UI.currentPage < ASC.Files.UI.amountPage && countLeft > 0) {
            jq("#pageNavigatorHolder").show();
            jq("#pageNavigatorHolder a")
                .text(countShowOnPage < countLeft ?
                    ASC.Files.FilesJSResources.ButtonShowMoreOf.format(countShowOnPage, countLeft) :
                    ASC.Files.FilesJSResources.ButtonShowMore.format(countLeft));
        } else {
            jq("#pageNavigatorHolder").hide();
        }

        if (!isCorrectCurrentFolder(xmlData.getElementsByTagName("folder_info")[0])) {
            ASC.Files.Anchor.defaultFolderSet();
            return;
        }

        if (!isCorrectPathParts(xmlData.getElementsByTagName("path_parts")[0])) {
            ASC.Files.Anchor.defaultFolderSet();
            return;
        }

        ASC.Files.Marker.markRootAsNew(xmlData.getElementsByTagName("root_folders_id_marked_as_new")[0]);

        if (htmlXML == "" && (params.append != true || params.from == 0)) {
            ASC.Files.EmptyScreen.displayEmptyScreen();
        } else {
            ASC.Files.UI.addRowHandlers();
        }

        ASC.Files.UI.checkEditing();

        if (ASC.Files.Folders.eventAfter != null && typeof ASC.Files.Folders.eventAfter == "function") {
            ASC.Files.Folders.eventAfter();
            ASC.Files.Folders.eventAfter = null;
        }

        jq(ASC.Files.UI.lastSelectedEntry).each(function () {
            var entryObj = ASC.Files.UI.getEntryObject(this.entryType, this.entryId);
            ASC.Files.UI.selectRow(entryObj, true);
        });
        ASC.Files.UI.updateMainContentHeader();
        ASC.Files.UI.lastSelectedEntry = null;

        ASC.Files.CreateMenu.disableMenu(ASC.Files.UI.accessibleItem());

        ASC.Files.UI.stickContentHeader();
    };

    var isCorrectCurrentFolder = function (xmlData) {
        if (typeof xmlData == "undefined" || xmlData == null) {
            return false;
        }

        ASC.Files.Folders.currentFolder = {};

        var xmlArray = jq(xmlData.childNodes);
        for (var item in xmlArray) {
            if (item && typeof xmlArray[item] == "object") {
                ASC.Files.Folders.currentFolder[xmlArray[item].tagName]
                    = (xmlArray[item].textContent || xmlArray[item].text).replace(/\"/g, "\\\"");
            }
        }

        if (ASC.Files.Common.isCorrectId(ASC.Files.Folders.currentFolder.id)) {
            ASC.Files.Folders.currentFolder.entryId = ASC.Files.Folders.currentFolder.id;
        } else {
            return false;
        }

        ASC.Files.Folders.currentFolder.entryType = "folder";

        if (ASC.Files.Share) {
            ASC.Files.Folders.currentFolder.access = parseInt(ASC.Files.Folders.currentFolder.access || ASC.Files.Constants.AceStatusEnum.None);

            if (ASC.Files.Folders.currentFolder.access == ASC.Files.Constants.AceStatusEnum.Restrict) {
                ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.AceStatusEnum_Restrict, true);
                return false;
            }

            ASC.Files.Folders.currentFolder.shareable = (ASC.Files.Folders.currentFolder.shareable === "true");
        }

        if (ASC.Files.Folders.currentFolder.id == ASC.Files.Tree.folderIdCurrentRoot) {
            ASC.Files.Folders.currentFolder.title = ASC.Files.FilesJSResources.ProjectFiles;
        }

        ASC.Files.UI.setDocumentTitle(ASC.Files.Folders.currentFolder.title);

        return true;
    };

    var isCorrectPathParts = function (xmlData) {
        if (typeof xmlData == "undefined" || xmlData == null) {
            return false;
        }

        var data = jq(xmlData.childNodes).map(function (i, childNode) {
            var xmlKey = childNode.childNodes[0];
            return xmlKey.textContent || xmlKey.text;
        });

        if (data.length == 0) {
            return false;
        }

        var rootFolderId = parseInt(data[0]);
        if (ASC.Files.Tree) {
            ASC.Files.Tree.pathParts = data;
        }

        jq("#mainContentHeader .menuAction").removeClass("unlockAction").hide();

        jq("#filesMainContent")
            .removeClass("myFiles")
            .removeClass("corporateFiles")
            .removeClass("shareformeFiles")
            .removeClass("trashFiles")
            .removeClass("projectFiles");

        switch (rootFolderId) {
            case ASC.Files.Constants.FOLDER_ID_MY_FILES:
                ASC.Files.Folders.folderContainer = "my";
                jq("#filesMainContent").addClass("myFiles");
                jq("#mainDownload, #mainMove, #mainCopy, #mainDelete, #mainConvert").show();
                break;
            case ASC.Files.Constants.FOLDER_ID_SHARE:
                ASC.Files.Folders.folderContainer = "forme";
                jq("#filesMainContent").addClass("shareformeFiles");
                jq("#mainDownload, #mainCopy, #mainMarkRead, #mainUnsubscribe, #mainConvert").show();
                break;
            case ASC.Files.Constants.FOLDER_ID_COMMON_FILES:
                ASC.Files.Folders.folderContainer = "corporate";
                jq("#filesMainContent").addClass("corporateFiles");
                jq("#mainDownload, #mainMove, #mainCopy, #mainMarkRead, #mainDelete, #mainConvert").show();
                break;
            case ASC.Files.Constants.FOLDER_ID_PROJECT:
                ASC.Files.Folders.folderContainer = "project";
                jq("#filesMainContent").addClass("projectFiles");
                jq("#mainDownload, #mainMove, #mainCopy, #mainMarkRead, #mainDelete, #mainConvert").show();
                break;
            case ASC.Files.Constants.FOLDER_ID_TRASH:
                ASC.Files.Folders.folderContainer = "trash";
                jq("#filesMainContent").addClass("trashFiles");
                jq("#mainDownload, #mainRestore, #mainDelete, #mainEmptyTrash, #mainConvert").show();
                jq("#mainEmptyTrash").addClass("unlockAction");
                break;
            case ASC.Files.Tree.folderIdCurrentRoot:
                ASC.Files.Folders.folderContainer = "project";
                jq("#filesMainContent").addClass("projectFiles");
                jq("#mainDownload, #mainMove, #mainCopy, #mainDelete, #mainConvert").show();
                break;
            default:
                if (!ASC.Files.Common.isCorrectId(ASC.Files.Folders.currentFolder.id)) {
                    return false;
                }
        }

        ASC.Files.Filter.disableFilter();

        jq("#filesMainContent").toggleClass("without-share", !(ASC.Files.Share && ASC.Files.Folders.currentFolder.shareable));

        if (ASC.Files.Tree) {
            ASC.Files.Tree.updateTreePath();
        }

        ASC.Files.UI.checkButtonBack(".to-parent-folder", "#toParentFolder");

        return true;
    };

    var onCreateNewFile = function (xmlData, params, errorMessage) {
        var fileNewObj = ASC.Files.UI.getEntryObject("file", 1).filter("[spare_data=\"NEW_FILE\"]");
        ASC.Files.UI.blockObject(fileNewObj);

        var winEditor = params.winEditor;
        if (typeof errorMessage != "undefined") {
            fileNewObj.remove();
            winEditor.close();
            if (jq("#filesMainContent .file-row").length == 0) {
                ASC.Files.EmptyScreen.displayEmptyScreen();
            }
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        var htmlXML = ASC.Files.TemplateManager.translate(xmlData);

        fileNewObj.replaceWith(htmlXML);
        ASC.Files.UI.resetSelectAll();

        //TODO: get Object?
        var fileObj = jq("#filesMainContent .new-file").show().yellowFade().removeClass("new-file");
        var fileData = ASC.Files.UI.getObjectData(fileObj);
        fileObj = fileData.entryObject;
        var fileTitle = fileData.title;
        var fileId = fileData.entryId;

        ASC.Files.UI.addRowHandlers(fileObj);

        ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoCrateFile.format(fileTitle));

        ASC.Files.Actions.checkEditFile(fileId, winEditor, true);
    };

    var onCreateFolder = function (xmlData, params, errorMessage) {
        var folderNewObj = ASC.Files.UI.getEntryObject("folder", 1).filter("[spare_data=\"NEW_FOLDER\"]");

        if (typeof errorMessage != "undefined") {
            folderNewObj.remove();
            if (jq("#filesMainContent .file-row").length == 0) {
                ASC.Files.EmptyScreen.displayEmptyScreen();
            }

            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        var htmlXML = ASC.Files.TemplateManager.translate(xmlData);

        folderNewObj.replaceWith(htmlXML);
        ASC.Files.UI.resetSelectAll();

        //TODO: get Object
        var folderObj = jq("#filesMainContent .new-folder").yellowFade().removeClass("new-folder");
        var folderData = ASC.Files.UI.getObjectData(folderObj);
        folderObj = folderData.entryObject;
        var folderTitle = folderData.title;

        ASC.Files.UI.addRowHandlers(folderObj);

        if (ASC.Files.Tree) {
            ASC.Files.Tree.resetFolder(params.parentFolderID);
        }

        ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoCrateFolder.format(folderTitle));
        
        //track event
        
        trackingGoogleAnalitics("documents", "create", "folder");
    };

    var onRenameFolder = function (xmlData, params, errorMessage) {
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        var htmlXML = ASC.Files.TemplateManager.translate(xmlData);

        var folderId = params.folderId;
        var folderObj = ASC.Files.UI.getEntryObject("folder", folderId);

        var rowIndex = jq.inArray(folderObj[0], jq("#filesMainContent li.file-row"));

        folderObj.replaceWith(htmlXML);

        folderObj = ASC.Files.UI.getEntryObject("folder", folderId);
        if (folderObj == null || folderObj.length == 0) {
            folderObj = jq("#filesMainContent li.file-row")[rowIndex];
        }

        var itemNewData = ASC.Files.UI.getObjectData(folderObj);

        itemNewData = itemNewData || ASC.Files.UI.getObjectData("#files_mainContent .new-folder");
        folderObj = itemNewData.entryObject;

        var folderNewTitle = itemNewData.title;
        folderObj.yellowFade().removeClass("new-folder");

        ASC.Files.UI.addRowHandlers(folderObj);

        if (ASC.Files.Tree) {
            ASC.Files.Tree.resetFolder(params.parentFolderID);
        }

        ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoRenameFolder.format(params.name, folderNewTitle));
    };

    var onRenameFile = function (xmlData, params, errorMessage) {
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        var fileData = ASC.Files.EventHandler.onReplaceVersion(xmlData, params, errorMessage);
        var newName = fileData.title;

        ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoRenameFile.format(params.name, newName));
    };

    var onUpdateHistory = function (jsonData, params, errorMessage) {
        var fileId = params.fileId;
        var fileObj = ASC.Files.UI.getEntryObject("file", fileId);
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.blockObject(fileObj);
            jq(".version-operation").css("visibility", "");
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        jq.extend(params, { isStringXml: true, show: true });
        var xmlFile = ASC.Files.Common.jsonToXml({ file: jsonData.key });
        ASC.Files.EventHandler.onReplaceVersion(xmlFile, params);

        var xmlHistory = ASC.Files.Common.jsonToXml({ fileList: { entry: jsonData.value } });
        ASC.Files.EventHandler.onGetFileHistory(xmlHistory, params);
    };

    var onGetFileHistory = function (xmlData, params, errorMessage) {
        var fileId = params.fileId;
        var fileObj = ASC.Files.UI.getEntryObject("file", fileId);

        ASC.Files.UI.blockObject(fileObj);
        ASC.Files.UI.updateMainContentHeader();

        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }
        var htmlXML =
            (params.isStringXml === true
                ? ASC.Files.TemplateManager.translateFromString(xmlData)
                : ASC.Files.TemplateManager.translate(xmlData));

        jq("#contentVersions").remove();
        fileObj.append(htmlXML).addClass("file-row-fix");

        var fileData = ASC.Files.UI.getObjectData(fileObj);

        var canEdit = true;
        if (!ASC.Files.UI.accessibleItem(fileData, fileObj)
            || ASC.Files.UI.editingFile(fileObj)
            || ASC.Files.UI.lockedForMe(fileObj)) {
            jq(".version-comment-edit").remove();
            canEdit = false;
        } else {
            jq("#contentVersions").addClass("version-edit");
        }

        if (!canEdit
            || Teamlab.profile.isVisitor === true) {
            jq(".version-operation.version-restore span").remove();
            jq(".version-complete, .version-continue").remove();
            jq(".version-num span").addClass("display-num");
        }

        if (ASC.Files.Utility.CanWebView(fileData.title)) {
            jq(".not-preview").removeClass("not-preview");
        }

        jq("#contentVersions .version-row[data-version='" + fileData.version + "'] .version-restore").empty();
        jq("#contentVersions .version-row[data-version='" + fileData.version + "'] .version-sublist")
            .removeClass("version-sublist").find("span").text(ASC.Files.FilesJSResources.RevisionCurrent);

        var curVersionGroup = fileData.version_group;
        jq("#contentVersions .version-row[data-version-group=" + curVersionGroup + "]").show();
        jq("#contentVersions .version-row").each(function () {
            var row = jq(this);
            var versionGroup = row.attr("data-version-group");

            if (versionGroup != curVersionGroup) {
                row.find(".version-complete").remove();
                curVersionGroup = versionGroup;
                row.addClass("version-group-head");

                var groupList = jq("#contentVersions .version-row[data-version-group=" + curVersionGroup + "]");
                if (groupList.length > 1) {
                    row.find(".version-sublist span").text(jq.format(ASC.Files.FilesJSResources.RevisionCount, groupList.length));
                } else {
                    row.find(".version-sublist span").remove();
                }
            } else if (!canEdit) {
                row.find(".version-num").empty();
            } else {
                row.find(".version-num span, .version-continue").remove();
            }
        });
    };

    var onUpdateComment = function (jsonData, params, errorMessage) {
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        jsonData = jsonData || "";
        var fileObj = ASC.Files.UI.getEntryObject("file", params.fileId);
        fileObj.find(".version-row[data-version=" + params.version + "]")
            .removeClass("version-row-comment")
            .find(".version-comment")
            .attr("data-comment", jsonData)
            .attr("title", (jsonData.length ? jsonData : ""))
            .html("<div class=\"version-comment-fix\">" + Encoder.htmlEncode(jsonData) + "</div>");
    };

    var onReplaceVersion = function (xmlData, params, errorMessage) {
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }
        var fileId = params.fileId;
        if (!ASC.Files.Common.isCorrectId(fileId)) {
            return;
        }

        var htmlXML =
            (params.isStringXml === true
                ? ASC.Files.TemplateManager.translateFromString(xmlData)
                : ASC.Files.TemplateManager.translate(xmlData));

        var fileObj = ASC.Files.UI.getEntryObject("file", fileId);
        ASC.Files.Marker.removeNewIcon("file", fileId);

        fileObj.replaceWith(htmlXML);

        fileObj = ASC.Files.UI.getEntryObject("file", fileId);
        var itemNewData = ASC.Files.UI.getObjectData(fileObj);
        itemNewData = itemNewData || ASC.Files.UI.getObjectData("#filesMainContent .new-file");
        fileObj = itemNewData.entryObject;
        if (params.show) {
            ASC.Files.EmptyScreen.hideEmptyScreen();
            itemNewData.entryObject.removeClass("new-file").show().yellowFade();
        }

        if (fileObj.find(".is-new").is(":visible")) {
            ASC.Files.Marker.setNewCount(itemNewData.entryType, itemNewData.entryId, 1);
        }

        ASC.Files.UI.addRowHandlers(fileObj);

        ASC.Files.UI.resetSelectAll();

        ASC.Files.UI.checkEditing();

        ASC.Files.Actions.showActionsViewPanel();
        return itemNewData;
    };

    var onGetFile = function (xmlData, params, errorMessage) {
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }
        var fileId = params.fileId;
        if (!ASC.Files.Common.isCorrectId(fileId)) {
            return;
        }

        var htmlXML =
            (params.isStringXml === true
                ? ASC.Files.TemplateManager.translateFromString(xmlData)
                : ASC.Files.TemplateManager.translate(xmlData));

        jq("#filesMainContent").prepend(htmlXML);

        var fileObj = ASC.Files.UI.getEntryObject("file", fileId);

        var fileData = ASC.Files.UI.getObjectData(fileObj);
        fileData = fileData || ASC.Files.UI.getObjectData("#filesMainContent .new-file");
        fileObj = fileData.entryObject;
        if (params.show) {
            ASC.Files.EmptyScreen.hideEmptyScreen();
            fileData.entryObject.removeClass("new-file").show().yellowFade();
        }

        if (fileObj.find(".is-new").is(":visible")) {
            ASC.Files.Marker.setNewCount(fileData.entryType, fileData.entryId, 1);
        }

        ASC.Files.UI.addRowHandlers(fileObj);

        ASC.Files.UI.resetSelectAll();

        ASC.Files.UI.checkEditing();

        ASC.Files.Actions.showActionsViewPanel();
        return fileData;
    };

    var onMoveFilesCheck = function (data, params, errorMessage) {
        if (typeof errorMessage != "undefined") {
            jq(params.list.entry).each(function () {
                var curItem = ASC.Files.UI.parseItemId(this);
                ASC.Files.UI.blockObjectById(curItem.entryType, curItem.entryId, false, null, true);
            });
            ASC.Files.UI.updateMainContentHeader();
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        if (data != null && data.length > 0) {
            ASC.Files.Folders.showOverwriteMessage(params.list, params.folderToId, params.folderToTitle, params.isCopyOperation, data);
        } else {
            ASC.Files.ServiceManager.moveItems(ASC.Files.ServiceManager.events.MoveItems,
                {
                    folderToId: params.folderToId,
                    overwrite: false,
                    isCopyOperation: params.isCopyOperation,
                    doNow: true
                },
                { stringList: params.list });
            ASC.Files.Folders.isCopyTo = false;
        }
    };

    var onMoveItemsFinish = function (listData, isCopyOperation, countProcessed) {
        var folderToId = ASC.Files.UI.parseItemId(listData[0]).entryId;
        listData = listData.slice(1);
        var listItemId = new Array();
        for (var i = 0; i < listData.length; i++) {
            var curItem = ASC.Files.UI.parseItemId(listData[i]);
            if (curItem == null) {
                continue;
            }
            listItemId.push(curItem);
            ASC.Files.UI.blockObjectById(curItem.entryType, curItem.entryId, false, null, false);
        }
        ASC.Files.UI.updateMainContentHeader();

        var folderToObj = ASC.Files.UI.getEntryObject("folder", folderToId);
        folderToObj.removeClass("row-to");
        var folderFromId;

        var foldersCount = 0, filesCount = 0;
        var entryTitle = "";

        if (listItemId.length == 1) {
            entryTitle = ASC.Files.UI.getEntryTitle(listItemId[0].entryType, listItemId[0].entryId);
            if (typeof entryTitle == undefined || entryTitle == null) {
                entryTitle = "";
            }
        }

        for (i = 0; i < listItemId.length; i++) {
            var entryRowObj = ASC.Files.UI.getEntryObject(listItemId[i].entryType, listItemId[i].entryId);

            if (listItemId[i].entryType == "file") {
                filesCount++;
            } else {
                foldersCount += 1 + (parseInt(entryRowObj.find(".countFolders").html()) || 0);
                filesCount += parseInt(entryRowObj.find(".countFiles").html()) || 0;

                if (ASC.Files.Tree && !folderFromId) {
                    folderFromId = ASC.Files.Tree.getParentId(listItemId[i].entryId);
                }
            }

            if (!isCopyOperation && ASC.Files.Folders.currentFolder.id != folderToId) {
                ASC.Files.Marker.removeNewIcon(listItemId[i].entryType, listItemId[i].entryId);
                entryRowObj.remove();
            }
        }

        if (foldersCount > 0) {
            var folderCountObj = folderToObj.find(".countFolders");

            folderCountObj.html((parseInt(folderCountObj.html()) || 0) + foldersCount);

            if (ASC.Files.Tree) {
                ASC.Files.Tree.resetFolder(folderToId);
                if (!isCopyOperation && folderToId != ASC.Files.Folders.currentFolder.id) {
                    ASC.Files.Tree.resetFolder(folderFromId || ASC.Files.Folders.currentFolder.id);
                    ASC.Files.Tree.updateTreePath();
                }
            }
        }

        if (filesCount > 0) {
            var fileCountObj = folderToObj.find(".countFiles");

            fileCountObj.html((parseInt(fileCountObj.html()) || 0) + filesCount);
        }

        if (listItemId.length > 0 && ASC.Files.Folders.currentFolder.id != folderToId) {
            ASC.Files.UI.checkEmptyContent();
        }

        if (isCopyOperation) {
            if (listItemId.length == 1 && entryTitle != "") {
                ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoCopyItem.format(entryTitle));
            } else {
                ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoCopyGroup.format(countProcessed));
            }
        } else {
            if (listItemId.length == 1 && entryTitle != "") {
                ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoMoveItem.format(entryTitle));
            } else {
                ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoMoveGroup.format(countProcessed));
            }
        }
    };

    var onDeleteItemFinish = function (listData, totalCount) {
        var fromRootId = ASC.Files.UI.parseItemId(listData[0]).entryId;
        listData = listData.slice(1);
        var listItemId = new Array();
        for (var i = 0; i < listData.length; i++) {
            var curItem = ASC.Files.UI.parseItemId(listData[i]);
            if (curItem == null) {
                continue;
            }
            listItemId.push(curItem);
            ASC.Files.UI.blockObjectById(curItem.entryType, curItem.entryId, false, null, true);
        }
        ASC.Files.UI.updateMainContentHeader();

        var folderFromId;
        var foldersCountChange = false;
        var entryTitle = "";
        var redrawItems =
            ASC.Files.Tree &&
                (ASC.Files.Tree.pathParts.length > 0
                    && (ASC.Files.Tree.pathParts[0] != ASC.Files.Constants.FOLDER_ID_TRASH
                        || fromRootId == ASC.Files.Constants.FOLDER_ID_TRASH
                        || (ASC.Files.ThirdParty && ASC.Files.ThirdParty.isThirdParty())));

        if (listItemId.length == 1) {
            entryTitle = ASC.Files.UI.getEntryTitle(listItemId[0].entryType, listItemId[0].entryId);
            if (typeof entryTitle == undefined || entryTitle == null) {
                entryTitle = "";
            }
        }

        for (i = 0; i < listItemId.length; i++) {
            var entryRowObj = ASC.Files.UI.getEntryObject(listItemId[i].entryType, listItemId[i].entryId);

            if (listItemId[i].entryType == "folder") {
                if (!foldersCountChange) {
                    foldersCountChange = true;
                }
                if (ASC.Files.Tree && !folderFromId) {
                    folderFromId = ASC.Files.Tree.getParentId(listItemId[i].entryId);
                }
            }

            if (redrawItems) {
                ASC.Files.Marker.removeNewIcon(listItemId[i].entryType, listItemId[i].entryId);
                entryRowObj.remove();
            }
        }

        if (foldersCountChange && ASC.Files.Tree) {
            ASC.Files.Tree.resetFolder(folderFromId || ASC.Files.Folders.currentFolder.id);
            ASC.Files.Tree.updateTreePath();
        }

        if (listItemId.length > 0 && redrawItems) {
            ASC.Files.UI.checkEmptyContent();
        }

        if (listItemId.length == 1 && entryTitle != "") {
            if (foldersCountChange > 0) {
                ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoRemoveFolder.format(entryTitle));
            } else {
                ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoRemoveFile.format(entryTitle));
            }
        } else {
            ASC.Files.UI.displayInfoPanel(ASC.Files.FilesJSResources.InfoRemoveGroup.format(listItemId.length, totalCount));
        }

        if (fromRootId == ASC.Files.Constants.FOLDER_ID_TRASH) {
            ASC.Files.ChunkUploads.initTenantQuota();
        }
    };

    var onCheckEditing = function (jsonData, params, errorMessage) {
        clearTimeout(ASC.Files.UI.timeCheckEditing);
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        if (!jsonData) {
            jsonData = [];
        }

        var list = jq("#filesMainContent .file-row.on-edit");

        for (var i = 0; i < list.length; i++) {
            var fileData = ASC.Files.UI.getObjectData(list[i]);
            var fileObj = fileData.entryObject;
            var fileId = fileData.entryId;
            ASC.Files.UI.lockEditFile(fileObj, false);

            var repl = true;
            for (var j = 0; j < jsonData.length && repl; j++) {
                if (fileId == jsonData[j].Key) {
                    repl = false;
                }
            }

            if (repl) {
                ASC.Files.Folders.replaceVersion(fileId, true);
            }
        }

        for (var k = 0; k < jsonData.length; k++) {
            fileId = jsonData[k].Key;
            var listBy = jsonData[k].Value;
            ASC.Files.UI.lockEditFileById(fileId, true, listBy);
        }

        if (jsonData.length > 0) {
            ASC.Files.UI.timeCheckEditing = setTimeout(ASC.Files.UI.checkEditing, 5000);
        }
    };

    var onUpdateIfExist = function (jsonData, params, errorMessage) {
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        jq(".update-if-exist").prop("checked", jsonData === true);
    };

    var onLockFile = function (xmlData, params, errorMessage) {
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        var lock = params.lock;
        if (lock) {
            var fileObj = params.fileObj;
            fileObj.addClass("file-locked");
            ASC.Files.UI.addRowHandlers(fileObj);
        } else {
            params.show = true;
            ASC.Files.EventHandler.onReplaceVersion(xmlData, params, errorMessage);
        }
    };

    var onGetHelpCenter = function (jsonData, params, errorMessage) {
        if (typeof errorMessage != "undefined") {
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            ASC.Files.Anchor.defaultFolderSet();
            return;
        }

        if (params.update) {
            jq("#helpPanel").html(jsonData);
            StudioManager.initImageZoom();
        }

        LoadingBanner.hideLoading();
        jq("#helpPanel").show();

        showHelpPage(params.helpId);

        ASC.Files.UI.setDocumentTitle(ASC.Files.FilesJSResources.TitleSettingsHelp);
        ASC.Files.CreateMenu.disableMenu();
    };

    var onGetTasksStatuses = function (data, params, errorMessage) {
        if (typeof data !== "object" && typeof errorMessage != "undefined" || data == null) {
            ASC.Files.Folders.cancelTasksStatuses();
            if (ASC.Files.Import) {
                ASC.Files.Import.cancelImportData("");
            }
            ASC.Files.UI.displayInfoPanel(errorMessage, true);
            return;
        }

        if (data.length == 0) {
            ASC.Files.Folders.cancelTasksStatuses();
            if (ASC.Files.Import) {
                ASC.Files.Import.cancelImportData("");
            }
            return;
        }

        var finishImport = true;
        var progress = 0;
        var operationType;
        var operationTypes = [ASC.Files.FilesJSResources.TasksOperationMove,
            ASC.Files.FilesJSResources.TasksOperationCopy,
            ASC.Files.FilesJSResources.TasksOperationDelete,
            ASC.Files.FilesJSResources.TasksOperationBulkdownload,
            ASC.Files.FilesJSResources.TasksOperationMarkAsRead];
        var blockTypes = [ASC.Files.FilesJSResources.DescriptMove,
            ASC.Files.FilesJSResources.DescriptCopy,
            ASC.Files.FilesJSResources.DescriptRemove,
            ASC.Files.FilesJSResources.DescriptBulkdownload,
            ASC.Files.FilesJSResources.DescriptMarkAsRead];

        //import
        for (var i = 0; i < data.length && ASC.Files.Import; i++) {
            if (data[i].operation == 5) {
                ASC.Files.Import.createImportProgress();
                finishImport = ASC.Files.Import.onGetImportStatus(data.splice(i, 1)[0], params.isTerminate);
                break;
            }
        }

        if (data.length != 0) {
            //show
            if (jq("#tasksProgress:visible").length == 0) {
                clearTimeout(timoutTasksStatuses);

                if (jq("#tasksProgress").length == 0) {
                    jq("#progressTemplate").clone().attr("id", "tasksProgress").prependTo("#bottomLoaderPanel");
                    jq("#tasksProgress .progress-dialog-header").append("<a title=\"{0}\" class=\"actions-container close\"></a>".format(ASC.Files.FilesJSResources.TitleCancel));
                    jq("#tasksProgress .progress-dialog-header").append("<span></span>");
                }
                ASC.Files.UI.setProgressValue("#tasksProgress", 0);
                jq("#tasksProgress .asc-progress-percent").text("0%");
                jq("#tasksProgress .progress-dialog-header span").text("");
                jq("#tasksProgress").show();

                if (jq.browser.mobile) {
                    jq("#bottomLoaderPanel").css("bottom", "auto");
                    jq("#bottomLoaderPanel").css("top", jq(window).scrollTop() + jq(window).height() - jq("#bottomLoaderPanel").height() + "px");
                }
            }

            //type operation in progress
            if (data.length > 1) {
                operationType = ASC.Files.FilesJSResources.TasksOperationMixed.format(data.length);
            } else {
                operationType = operationTypes[data[0].operation];
            }
            jq("#tasksProgress .progress-dialog-header span").text(operationType);
        }

        //in each process
        for (i = 0; i < data.length; i++) {

            //block descript on each elemets
            var splitCharacter = ":";
            var listSource = data[i].source.trim().split(splitCharacter);
            jq(listSource).each(function () {
                var itemId = ASC.Files.UI.parseItemId(this);
                if (itemId == null) {
                    return true;
                }
                ASC.Files.UI.blockObjectById(itemId.entryType, itemId.entryId, true, blockTypes[data[i].operation], true);
            });
            ASC.Files.UI.updateMainContentHeader();

            var curProgress = data[i].progress;
            progress += curProgress;

            //finish
            if (curProgress == 100) {
                if (data[i].result != null) {
                    var listResult = data[i].result.trim().split(splitCharacter);

                    switch (data[i].operation) {
                        case 0:
                            //move
                            onMoveItemsFinish(listResult, false, data[i].processed);
                            break;
                        case 1:
                            //copy
                            onMoveItemsFinish(listResult, true, data[i].processed);
                            break;
                        case 2:
                            //delete
                            onDeleteItemFinish(listResult, listSource.length);
                            break;
                        case 3:
                            //download
                            if (listResult[0]) {
                                location.href = listResult[0];
                            }
                            ASC.Files.Folders.bulkStatuses = false;
                            break;
                        case 4:
                            //mark as read
                            ASC.Files.Marker.onMarkAsRead(listResult);
                            break;
                    }
                }
                //unblock
                jq(listSource).each(function () {
                    var itemId = ASC.Files.UI.parseItemId(this);
                    if (itemId == null) {
                        return true;
                    }
                    ASC.Files.UI.blockObjectById(itemId.entryType, itemId.entryId, false, null, true);
                });
                ASC.Files.UI.updateMainContentHeader();

                //on error
                if (data[i].error != null) {
                    ASC.Files.UI.displayInfoPanel(data[i].error, true);
                }
            }
        }

        //progress %
        progress = (data.length == 0 ? 100 : progress / data.length);

        ASC.Files.UI.setProgressValue("#tasksProgress", progress);
        jq("#tasksProgress .asc-progress-percent").text(progress + "%");

        //complate
        if (progress == 100) {
            clearTimeout(timoutTasksStatuses);
            timoutTasksStatuses = setTimeout(ASC.Files.Folders.cancelTasksStatuses, 500);

            if (finishImport) {
                jq("#importDataProcess").hide();
                return;
            }
        }

        //next iteration
        ASC.Files.Folders.getTasksStatuses(params.doNow);
    };

    return {
        init: init,

        onGetFolderItems: onGetFolderItems,
        onGetFile: onGetFile,
        onCreateNewFile: onCreateNewFile,
        onCreateFolder: onCreateFolder,
        onRenameFolder: onRenameFolder,
        onRenameFile: onRenameFile,
        onUpdateHistory: onUpdateHistory,
        onGetFileHistory: onGetFileHistory,
        onReplaceVersion: onReplaceVersion,
        onUpdateComment: onUpdateComment,
        onCheckEditing: onCheckEditing,
        onLockFile: onLockFile,

        onUpdateIfExist: onUpdateIfExist,
        onGetHelpCenter: onGetHelpCenter,

        onMoveFilesCheck: onMoveFilesCheck,
        onGetTasksStatuses: onGetTasksStatuses
    };
})();

(function ($) {

    $(function () {
        ASC.Files.EventHandler.init();

        jq("#bottomLoaderPanel").on("click", "#tasksProgress a.close", function () {
            ASC.Files.Folders.terminateTasks(false);
            return false;
        });
    });
})(jQuery);
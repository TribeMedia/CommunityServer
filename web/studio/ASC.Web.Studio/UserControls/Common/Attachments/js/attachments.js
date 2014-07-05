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
window.Attachments = (function() {
    var moduleName,
        isInit = false,
        isLoaded = false,
        entityId = null,
        projectId = null,
        rootFolderId = null,
        entityType = "",
        emptyScreenVisible = true,
        createNewEntityFlag = false,
        banOnEditingFlag = false,
        characterString = "@#$%&*+:;\"'<>?|\/",
        characterRegExp = new RegExp("[@#$%&*\+:;\"'<>?|\\\\/]", 'gim');

    var uploadWithAttach = true;

    var replaceSpecCharacter = function(str) {
        return str.replace(characterRegExp, '_');
    };
    var checkCharacter = function(input) {
        jq(input).unbind("keyup").bind("keyup", function() {
            var str = jq(this).val();
            if (str.search(characterRegExp) != -1) {
                jq(this).val(replaceSpecCharacter(str));
                jq("#wrongSign").show();
                setInterval('jq("#wrongSign").hide();', 15000);
            }
        });
    };

    var setCreateNewEntityFlag = function(flag) {
        createNewEntityFlag = flag;
    };

    var getEntityFiles = function() {
        switch (moduleName) {
            case "projects":
                {
                    Teamlab.getPrjEntityFiles(null, entityId, entityType, function() { onGetFiles(arguments); });
                    break;
                }
            case "crm":
                {
                    Teamlab.getCrmEntityFiles(null, entityId, entityType, function() { onGetFiles(arguments); });
                    break;
                }
            default:
                LoadingBanner.hideLoading();
        }
    };
    var loadFiles = function() {
        if (!isLoaded) {
            LoadingBanner.displayLoading();
            getEntityFiles();
        }
    };
    var checkEditingSupport = function() {
        var listTypes = "";
        listTypes += ASC.Files.Utility.CanWebEdit(ASC.Files.Utility.Resource.InternalFormats.Document) ? "" : "#files_create_text,";
        listTypes += ASC.Files.Utility.CanWebEdit(ASC.Files.Utility.Resource.InternalFormats.Spreadsheet) ? "" : "#files_create_spreadsheet,";
        listTypes += ASC.Files.Utility.CanWebEdit(ASC.Files.Utility.Resource.InternalFormats.Presentation) ? "" : "#files_create_presentation";

        jq(listTypes).hide();
        if (jq("#files_newDocumentPanel ul li").length == listTypes.split(",").length) {
            jq("#createFirstDocument, #firstDocComb, #showDocumentPanel").hide();
        }
    };
    var init = function() {
        if (!isInit) {
            isInit = true;

            checkEditingSupport();

            entityId = jq.getURLParam("id");

            emptyScreenVisible = jq("#emptyDocumentPanel").length == 0 ? false : true;

            var projId = jq(".wrapperFilesContainer").attr("projectId");
            var module = jq(".wrapperFilesContainer").attr("moduleName");

            if (projId != "0") projectId = projId;
            if (module != "") moduleName = module;

            entityType = jq(".wrapperFilesContainer").attr("entityType");
            if (!jq.browser.mobile) {
                var warnText = jq(".infoPanelAttachFile #wrongSign").text() + " " + characterString;
                jq(".infoPanelAttachFile #wrongSign").text(warnText);
            } else {
                jq("#emptyDocumentPanel .emptyScrBttnPnl").remove();
                jq(".infoPanelAttachFile, .containerAction, .information-upload-panel").remove();
            }
        }

        ASC.Controls.AnchorController.bind(/files/, initUploader);

        jq(document).on("keypress", "#newDocTitle", function(evt) {
            if (evt.keyCode == 13) {
                createFile();
            } else {
                if (evt.keyCode == 27) {
                    removeNewDocument();
                } else {
                    checkCharacter(jq("#newDocTitle"));
                }
            }
        });

        jq.dropdownToggle({
            switcherSelector: "#showDocumentPanel, #createFirstDocument, #firstDocComb",
            anchorSelector: ".newDocComb:visible",
            dropdownID: "files_newDocumentPanel",
            addTop: 5,
            addLeft: -17
        });
        if (emptyScreenVisible) {
            jq.dropdownToggle({
                switcherSelector: "#emptyDocumentPanel .hintCreate",
                dropdownID: "files_hintCreatePanel",
                fixWinSize: false
            });

            jq.dropdownToggle({
                switcherSelector: "#emptyDocumentPanel .hintUpload",
                dropdownID: "files_hintUploadPanel",
                fixWinSize: false
            });

            jq.dropdownToggle({
                switcherSelector: "#emptyDocumentPanel .hintOpen",
                dropdownID: "files_hintOpenPanel",
                fixWinSize: false
            });

            jq.dropdownToggle({
                switcherSelector: "#emptyDocumentPanel .hintEdit",
                dropdownID: "files_hintEditPanel",
                fixWinSize: false
            });
        }

        jq('#attachProjDocuments').on('click', function(event) {
            ProjectDocumentsPopup.showPortalDocUploader();
            return false;
        });
        jq('#questionWindowAttachments #noButton').bind('click', function() {
            jq.unblockUI();
            return false;
        });
        jq(".wrapperFilesContainer").on("click", ".unlinkDoc", function() {
            var fileId = parseInt(jq(this).attr("data-fileId"));
            jq(document).trigger("deleteFile", fileId);
            return false;
        });
        jq(".wrapperFilesContainer").on("click", ".deleteDoc", function() {
            var fileId = parseInt(jq(this).attr("data-fileId"));
            showQuestionWindow(fileId);
            return false;
        });
        jq("#storeOriginalFileFlag").change(function() {
            onChangeStoreFlag();
        });
    };
    var hideNewFileMenu = function() {
        jq("#files_newDocumentPanel").hide();
    };

    var initImageZoom = function() {
        StudioManager.initImageZoom();
    };

    var initUploader = function() {
        if (moduleName != 'crm') {
            createAjaxUploader("linkNewDocumentUpload");
        } else {
            if (jq("#attachmentsContainer tr").length) {
                createAjaxUploader("linkNewDocumentUpload");
                return;
            }
            createAjaxUploader("uploadFirstFile");
        }
    };

    var onChangeStoreFlag = function() {
        initUploader();
    };

    var createAjaxUploader = function(buttonId) {
        if (!jq.browser.mobile) {
            var storeFlag = jq("#storeOriginalFileFlag").is(":checked");
            if (moduleName == 'crm') {
                var ajaxUploader = Teamlab.createCrmUploadFile(
                    null,
                    entityType, entityId,
                    {
                        buttonId: buttonId,
                        data: {
                            storeOriginalFileFlag: storeFlag
                        },
                        autoSubmit: true
                    },
                    {
                        before: LoadingBanner.displayLoading,
                        error: function(params, errors) { onError(errors); },
                        success: onUploadFiles
                    }
                );
            } else {
                if (!uploadWithAttach) {
                    var ajaxUploader = Teamlab.uploadFilesToPrjEntity(
                        null,
                        entityId,
                        {
                            buttonId: buttonId,
                            autoSubmit: true,
                            data: {
                                entityType: entityType,
                                folderid: rootFolderId,
                                createNewIfExist: true,
                                storeOriginalFileFlag: storeFlag
                            }
                        },
                        {
                            before: LoadingBanner.displayLoading,

                            error: function(params, errors) { onError(errors); },

                            success: onUploadFiles
                        }
                    );
                } else {
                    var ajaxUploader = Teamlab.createDocUploadFile(
                        null,
                        rootFolderId,
                        {
                            buttonId: buttonId,
                            autoSubmit: true,
                            data: {
                                createNewIfExist: true,
                                storeOriginalFileFlag: storeFlag
                            }
                        },
                        {
                            before: LoadingBanner.displayLoading,

                            error: function(params, errors) { onError(errors); },

                            success: onUploadFiles
                        }
                    );
                }
            }
        }
        return;
    };

    var showQuestionWindow = function(fileId) {
        jq('#questionWindowAttachments #okButton').unbind('click');
        StudioBlockUIManager.blockUI("#questionWindowAttachments", 400, 300, 0);
        PopupKeyUpActionProvider.EnterAction = "jq(\"#okButton\").click();";
        jq('#questionWindowAttachments #okButton').bind('click', function() {
            jq.unblockUI();
            jq(document).trigger("deleteFile", fileId);
            return false;
        });
    };

    var createNewDocument = function(type) {
        hideNewFileMenu();

        if (!ASC.Resources.Master.TenantTariffDocsEdition) {
            StudioBlockUIManager.blockUI("#tariffLimitDocsEditionPanel", 500, 300, 0);
            return;
        }

        jq("#emptyDocumentPanel:not(.display-none)").addClass("display-none");
        jq("#attachmentsContainer tr.newDoc").remove();

        var tdClass = ASC.Files.Utility.getCssClassByFileTitle(type, true);
        var tmpl = { tdclass: tdClass, type: type };
        var htmlNewDoc = jq("#newFileTmpl").tmpl(tmpl);
        jq("#attachmentsContainer tbody").prepend(htmlNewDoc);
        jq("#attachmentsContainer tr.newDoc").show();
        jq("#newDocTitle").focus().select();
    };
    var removeNewDocument = function() {
        jq("#attachmentsContainer tr.newDoc").remove();
        if (jq("#attachmentsContainer tbody tr").length == 0) {
            jq("#emptyDocumentPanel.display-none").removeClass("display-none");
        }
    };
    var createFile = function() {
        var hWindow = null;
        hWindow = window.open('');
        var title = jq("#newDocTitle").val();
        if (jq.trim(title) == "") {
            title = jq("#newDocTitle").attr("data");
        }
        var ext = jq(".createFile").attr("id");
        title = replaceSpecCharacter(title) + ext;
        Teamlab.addDocFile(
            { handler: hWindow },
            rootFolderId,
            "file",
            { title: title, content: '', createNewIfExist: true },
            function() { onCreateFile(arguments); },
            { error: function(params) { params.handler.close(); } }
        );
        removeNewDocument();
    };

    var createFileTmpl = function (fileData) {
        var fileTmpl = {};

        fileTmpl.title = decodeURIComponent(fileData.title);

        fileTmpl.exttype = ASC.Files.Utility.getCssClassByFileTitle(fileTmpl.title, true);

        fileTmpl.access = fileData.access;

        var type;
        if (ASC.Files.Utility.CanImageView(fileTmpl.title)) {
            type = "image";
        } else {
            if (ASC.Files.Utility.CanWebEdit(fileTmpl.title) && ASC.Resources.Master.TenantTariffDocsEdition && !ASC.Files.Utility.MustConvert(fileTmpl.title)) {
                type = "editedFile";
            } else {
                if (ASC.Files.Utility.CanWebView(fileTmpl.title) && ASC.Resources.Master.TenantTariffDocsEdition && !ASC.Files.Utility.MustConvert(fileTmpl.title)) {
                    type = "viewedFile";
                } else {
                    type = "noViewedFile";
                }
            }
        }
        fileTmpl.type = type;
        fileTmpl.id = fileData.id;
        fileTmpl.viewUrl = fileData.viewUrl;
        var version = parseInt(fileData.version);
        var versionGroup = parseInt(fileData.versionGroup);
        if (!version) {
            version = 1;
            fileTmpl.viewUrl = ASC.Files.Utility.GetFileViewUrl(fileTmpl.id, version);
        }
        fileTmpl.version = version;
        if (!versionGroup) {
            versionGroup = 1;
        }
        fileTmpl.versionGroup = versionGroup;
        fileTmpl.downloadUrl = ASC.Files.Utility.GetFileDownloadUrl(fileTmpl.id);
        fileTmpl.docViewUrl = ASC.Files.Utility.GetFileWebViewerUrl(fileTmpl.id);
        fileTmpl.editUrl = ASC.Files.Utility.GetFileWebEditorUrl(fileTmpl.id);
        if (fileData.isNewFile) {
            fileTmpl.editUrl = fileTmpl.editUrl + "&new=true";
        }
        fileTmpl.fileStatus = fileData.fileStatus;
        fileTmpl.trashAction = fileData.trashAction ? fileData.trashAction : "delete";
        if (createNewEntityFlag && !fileData.fromProjectDocs) {
            fileTmpl.trashAction = "delete";
        } else {
            if (moduleName == "crm") {
                fileTmpl.trashAction = "delete";
            } else {
                fileTmpl.trashAction = "deattach";
            }
        }
        return fileTmpl;
    };

    var isAddedFile = function(title, fileId) {
        var listAttachFiles = jq("#attachmentsContainer tbody tr td:first-child");
        for (var i = 0, n = listAttachFiles.length; i < n; i++) {
            var fileName = jq.trim(jq(listAttachFiles[i]).children("a").children(".attachmentsTitle").text());
            var id = jq(listAttachFiles[i]).attr("id").split("_")[1];
            if (fileName == title && id == fileId) {
                return listAttachFiles[i];
            }
        }
        return false;
    };

    var showEmptyScreen = function() {
        if (!emptyScreenVisible) {
            jq(".containerAction").show();
            return;
        }
        var files = jq("#attachmentsContainer tr");
        if (!files.length) {
            jq("#attachmentsContainer tbody").empty();
            jq("#emptyDocumentPanel.display-none").removeClass("display-none");
            jq(".containerAction").hide();
            createAjaxUploader("uploadFirstFile");
        }
    };

    var deleteFileFromLayout = function(fileId) {
        if (isLoaded) {
            var td = jq("#af_" + fileId);
            jq(td).parent().remove();
            showEmptyScreen();
        }
    };

    var appendToListAttachFiles = function (listFiles) {
        jq("#emptyDocumentPanel:not(.display-none)").addClass("display-none");
        jq(".containerAction").show();
        jq("#attachmentsContainer tbody").prepend(jq("#fileAttachTmpl").tmpl(listFiles));

        jq("#attachmentsContainer tbody tr").show();
        initImageZoom();
        LoadingBanner.hideLoading();
    };

    var publicAppendToListAttachFiles = function (listFiles) {
        if (isLoaded) {
            jq("#emptyDocumentPanel:not(.display-none)").addClass("display-none");
            jq(".containerAction").show();
            var listFileTempl = new Array();
            for (var i = 0, n = listFiles.length; i < n; i++) {
                var fileTmpl = createFileTmpl(listFiles[i]);
                listFileTempl.push(fileTmpl);
                jq("#attachmentsContainer tr:has(#af_" + listFiles[i].id + ")").remove();
            }
            jq("#attachmentsContainer tbody").prepend(jq("#fileAttachTmpl").tmpl(listFileTempl));

            jq("#attachmentsContainer tbody tr").show();
            initImageZoom();
        }
        LoadingBanner.hideLoading();
    };

    var showAttachedFiles = function(documents) {
        var files = new Array();
        for (var i = 0, n = documents.length; i < n; i++) {
            var file = createFileTmpl(documents[i]);
            files.push(file);
        }
        jq("#attachmentsContainer tbody").empty();
        appendToListAttachFiles(files);
        showEmptyScreen();
        LoadingBanner.hideLoading();
    };

    var showAddedFile = function(files) {
        if (files.length) {
            jq(".containerAction").show();
            for (var i = 0, n = files.length; i < n; i++) {
                var file = files[i];
                var elem = isAddedFile(file.title, file.id);
                if (elem) {
                    jq(elem).parents("tr").remove();
                } else {
                    jq(document).trigger("addFile", file);
                }
                appendToListAttachFiles(createFileTmpl(file));
            }
        }
        LoadingBanner.hideLoading();
    };

    var setRootFolderId = function(folderId, uploadParam) {
        rootFolderId = folderId;
        if (!uploadParam) {
            uploadWithAttach = uploadParam;
        }
        initUploader();
    };

    var bind = function(eventName, handler) {
        jq(document).bind(eventName, handler);
    };

    var banOnEditing = function() {
        banOnEditingFlag = true;
    };
    var onError = function(error) {
        LoadingBanner.hideLoading();

        jq("#errorFileUpload").text(error[0]).show();
        setInterval('jq("#errorFileUpload").hide();', 15000);
    };
    var onGetFiles = function(response) {
        isLoaded = true;
        showAttachedFiles(response[1]);

        if (response[1].length != 0) {
            jq(document).trigger("loadAttachments", response[1].length);
            if (moduleName == "crm") {
                rootFolderId = response[1][0].parentId;
                createAjaxUploader('linkNewDocumentUpload');
            }
        } else {
            if (moduleName == "crm") {
                Teamlab.getCrmFolder(null, "root", function(params, folder) {
                    rootFolderId = folder.id;
                    createAjaxUploader('uploadFirstFile');
                });
            }
        }
        LoadingBanner.hideLoading();
        if (banOnEditingFlag) {
            jq(".containerAction, .information-upload-panel, .infoPanelAttachFile").remove();
            jq("#emptyDocumentPanel .emptyScrBttnPnl").remove();
            jq("#attachmentsContainer").find(".unlinkDoc").remove();
        }
    };

    var onUploadFiles = function(params, file) {
        createAjaxUploader('linkNewDocumentUpload');
        if (moduleName == "crm") {
            jq(document).trigger("addFile", file);
        } else {
            showAddedFile([file]);
        }
    };

    var onCreateFile = function(response) {
        var file = response[1];

        file.fileStatus = 1;
        file.isNewFile = true;

        if (jq("#attachmentsContainer tr").length) {
            createAjaxUploader('linkNewDocumentUpload');
        }
        jq(document).trigger("addFile", file);

        if (moduleName == "projects") {
            appendToListAttachFiles(createFileTmpl(file));
        }
        jq(".containerAction").show();

        if (response[0].handler.location) {
            response[0].handler.location.href = ASC.Files.Utility.GetFileWebEditorUrl(file.id) + "&new=true";
        }
    };

    return {
        init: init,
        bind: bind,
        loadFiles: loadFiles,
        isLoaded: isLoaded,
        appendToListAttachFiles: appendToListAttachFiles,
        isAddedFile: isAddedFile,
        appendFilesToLayout: publicAppendToListAttachFiles,
        deleteFileFromLayout: deleteFileFromLayout,
        createNewDocument: createNewDocument,
        removeNewDocument: removeNewDocument,
        createFile: createFile,
        banOnEditing: banOnEditing,

        setFolderId: setRootFolderId,
        setCreateNewEntityFlag: setCreateNewEntityFlag
    };
})(jQuery);
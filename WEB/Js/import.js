/// <reference path="/js/jquery.js" />

var loadXls = {
    array: new Array(),
    sel: null,
    box: null,
    selval: null,
    ///初使化arr二维数组， box:table容器 ，sel列表选择器
    init: function(arr, box, sel) {
        var that = this;
        that.array = arr;
        that.sel = sel;
        that.box = $(box);
        that._createTable();
        that.selectAll();
    },
    //建表
    _createTable: function() {
        var that = this;
        var html = [];
        if (that.array.length > 0) {
            //html.push("<table width=\"100%\" border=\"0\"  cellpadding=\"0\" cellspacing=\"1\" id=\"xlstable\" >");
            html.push("<tr style='background: url(/images/y-formykinfo.gif) repeat-x center top;'>");
            html.push("<td height='23px' class='alertboxTableT' bgcolor='#b7e0f3' align='center' width=\"35px\"><input type=\"checkbox\" id='cbxCheckAll' checked='checked'/></td>");
            for (var j = 0; j < that.array[0].length; j++) {
                html.push("<td class='alertboxTableT' bgcolor='#b7e0f3' align='center'>" + that.array[0][j] + "</td>");
            }
            html.push("</tr>");
            for (var i = 1; i < that.array.length; i++) {
                html.push("<tr tid='" + i + "'>");
                html.push("<td height='23px'><input type=\"checkbox\" checked='checked'/>" + i + "</td>");
                for (var j = 0; j < that.array[i].length; j++) {
                    html.push("<td>" + that.array[i][j] + "</td>");
                }
                html.push("</tr>");
            }
            //html.push("</table>");
            that.box.html(html.join(""));
        } else {
            that.box.html("");
        }
    },
    //绑定设置返回选择数组
    bindIndex: function() {
        var that = this;
        var retarr = [];
        retarr.push(that.array[0]);
        if (that.box != null) {
            that.box.find("input[type='checkbox']:checked").each(function() {
                var _this = $(this);
                var tid = parseInt(_this.parent().parent().attr("tid"));
                retarr.push(that.array[tid]);
            });
        }
        return retarr;
    },
    selectAll: function() {
        var that = this;
        $("#cbxCheckAll").click(function() {
            that.box.find("input[type='checkbox']").attr("checked", this.checked);
        })
    }
}
$(function() {
    var ImportUpload = new SWFUpload({
        // Backend Settings
        upload_url: "/ashx/ImportSource.ashx",
        file_post_name: "Filedata",
        post_params: {},

        // File Upload Settings
        file_size_limit: "1 MB",
        file_types: '*.xls;*.xlsx;',
        file_types_description: "Excel文件",
        file_upload_limit: "1",    // Zero means unlimited

        swfupload_loaded_handler: swfUploadLoaded,
        file_dialog_start_handler: fileDialogStart,
        file_queued_handler: fileQueued,
        file_queue_error_handler: fileQueueError,
        file_dialog_complete_handler: fileDialogComplete,
        upload_progress_handler: uploadProgress,
        upload_error_handler: uploadError,
        upload_success_handler: ExcelFileUploadSuccess,
        upload_complete_handler: uploadComplete,

        // Button settings
        button_image_url: "/images/swfupload/XPButtonNoText_92_24.gif",
        button_placeholder_id: "spanButtonPlaceholder",
        button_width: 92,
        button_height: 24,
        button_text: '<span class="button"></span></span>',
        button_text_style: '',
        button_text_top_padding: 1,
        button_text_left_padding: 5,
        button_cursor: SWFUpload.CURSOR.HAND,

        // Flash Settings
        flash_url: "/js/swfupload/swfupload.swf", // Relative to this file

        custom_settings: {
            upload_target: "divFileProgressContainer",
            HidFileNameId: "hidFileName",
            ErrMsgId: "errMsg",
            UploadSucessCallback: null
        },

        // Debug Settings
        debug: false,

        // SWFObject settings
        minimum_flash_version: "9.0.28",
        swfupload_pre_load_handler: swfUploadPreLoad,
        swfupload_load_failed_handler: swfUploadLoadFailed
    });

})
function startExcelFileUpload() {
    if (ImportUpload.getStats().files_queued > 0) {
        ImportUpload.startUpload();
    }
}
function ExcelFileUploadSuccess(file, serverData) {
    try {
        var obj = eval(serverData);
        if (obj.error) {
            alert(obj.error);
            resetSwfupload(ImportUpload, file);
            return;
        }
        else {
            var progress = new FileProgress(file, this.customSettings.upload_target);
            progress.setStatus("上传成功.");
            $("#divUpload").hide();
            try {
                loadexcel(obj);
            } catch (e) { }

            resetSwfupload(ImportUpload, file);
        }
    }
    catch (ex) { }
}

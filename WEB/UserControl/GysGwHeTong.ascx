<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GysGwHeTong.ascx.cs" Inherits="EyouSoft.Web.UserControl.GysGwHeTong" %>
<div style="margin: 0 auto; width: 99%;">
    <span class="formtableT formtableT02">合同信息</span>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="GwHeTongAutoAdd">
        <tr>
            <td width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                编号
            </td>
            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                到期时间
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                国籍/地区
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                流水
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                保底
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                成人人头
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                儿童人头
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                是否启用
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                附件
            </td>
            <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                操作
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rpt">
            <ItemTemplate>
                <tr class="tempRow">
                    <td align="center" bgcolor="#FFFFFF" class="index">
                        <%# Container.ItemIndex + 1%>
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input type="hidden" name="txtHeTongId" value="<%#Eval("HeTongId") %>" />
                        <input name="txtHeTongETime" type="text" class="formsize80 input-txt" value="<%#Eval("ETime","{0:yyyy-MM-dd}") %>"
                            onfocus="WdatePicker()" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="txtHeTongGuoJi" type="text" class="formsize80 input-txt" value="<%#Eval("GuoJi") %>"
                            maxlength="50" />
                    </td>                    
                    <td align="center" bgcolor="#FFFFFF" >
                        <input name="txtHeTongLS" type="text" class="formsize40 input-txt" value="<%#Eval("LiuShui","{0:F2}") %>"
                            valid="isNumber" errmsg="请输入正确的流水" maxlength="10" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF" >
                        <input name="txtHeTongBD" type="text" class="formsize40 input-txt" value="<%#Eval("BaoDiJinE","{0:F2}") %>"
                            valid="isNumber" errmsg="请输入正确的保底" maxlength="10" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF" >
                        <input name="txtHeTongCR" type="text" class="formsize40 input-txt" value="<%#Eval("RenTouCR","{0:F2}") %>"
                            valid="isNumber" errmsg="请输入正确的成人人头" maxlength="10" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="txtHeTongET" type="text" class="formsize40 input-txt" value="<%#Eval("RenTouET","{0:F2}") %>"
                            valid="isNumber" errmsg="请输入正确的儿童人头" maxlength="10" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input type="checkbox" name="txtHeTongIsQiYong" i_val="<%#(bool)Eval("IsQiYong")?"1":"0" %>" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <div style="margin: 0px 10px;" class="i_gwhetong_upload" style="float: left;">
                        </div>
                        <input type="hidden" name="i_gwhetong_upload_hidden_y" value="<%#Eval("FilePath") %>" />
                        <%#GetChaKanFuJian(Eval("FilePath"))%>
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <a href="javascript:void(0)" class="addbtn">
                            <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                class="delbtn">
                                <img src="/images/delimg.gif" width="48" height="20" /></a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
            <tr class="tempRow">
                <td align="center" bgcolor="#FFFFFF" class="index">
                    1
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input type="hidden" name="txtHeTongId" value="" />
                    <input name="txtHeTongETime" type="text" class="formsize80 input-txt" value="<%#Eval("ETime","{0:yyyy-MM-dd}") %>"
                        onfocus="WdatePicker()" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtHeTongGuoJi" type="text" class="formsize80 input-txt" value="" maxlength="50" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtHeTongLS" type="text" class="formsize40 input-txt" value="" valid="isNumber"
                        errmsg="请输入正确的流水" maxlength="10" />
                </td>
                <td align="center" bgcolor="#FFFFFF" >
                    <input name="txtHeTongBD" type="text" class="formsize40 input-txt" value="" valid="isNumber"
                        errmsg="请输入正确的保底" maxlength="10" />
                </td>
                <td align="center" bgcolor="#FFFFFF" >
                    <input name="txtHeTongCR" type="text" class="formsize40 input-txt" value="" valid="isNumber"
                        errmsg="请输入正确的成人人头" maxlength="10" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtHeTongET" type="text" class="formsize40 input-txt" value="" valid="isNumber"
                        errmsg="请输入正确的儿童人头" maxlength="10" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input type="checkbox" name="txtHeTongIsQiYong" i_val="0" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <div style="margin: 0px 10px;" class="i_gwhetong_upload" style="float: left;">
                    </div>
                    <input type="hidden" name="i_gwhetong_upload_hidden_y" value="" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <a href="javascript:void(0)" class="addbtn">
                        <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                            class="delbtn">
                            <img src="/images/delimg.gif" width="48" height="20" /></a>
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
</div>
<style type="text/css">
    #GwHeTongAutoAdd .progressWrapper
    {
        width: 200px;
        overflow: hidden;
    }
    #GwHeTongAutoAdd .progressName
    {
        overflow: hidden;
        width: 150px; height:20px;
    }
</style>

<script type="text/javascript">
    var iGwHeTongUpload = {
        _index: 1, _uniqid: 'i_gwhetong_upload_',
        _initHtml: function(options) {
            var _s = [];
            _s.push('<div>');
            _s.push('<input type="hidden" id="' + this._uniqid + 'hidden_' + this._index + '" name="' + this._uniqid + 'hidden" />');
            _s.push('<span id="' + this._uniqid + 'imgbtn_' + this._index + '"></span>');
            _s.push('<span id="' + this._uniqid + 'error_' + this._index + '" class="errmsg"></span>');
            _s.push('</div>');
            _s.push('<div id="' + this._uniqid + 'fileprogress_' + this._index + '"></div>');
            _s.push('<div id="' + this._uniqid + 'thumbnails_' + this._index + '"></div>');

            $(options.box).html(_s.join(''));
        },
        _initSwf: function() {
            var flashUpload = new SWFUpload({
                upload_url: "/CommonPage/upload.aspx",
                file_post_name: "Filedata",
                post_params: { "ASPSESSID": "<%=Session.SessionID %>" },
                file_size_limit: "2 MB",
                file_types: "<%=EyouSoft.Common.Utils.UploadFileExtensions %>",
                file_types_description: "附件上传",
                file_upload_limit: 1,
                swfupload_loaded_handler: function() { },
                file_dialog_start_handler: uploadStart,
                upload_start_handler: uploadStart,
                file_queued_handler: fileQueued,
                file_queue_error_handler: fileQueueError,
                file_dialog_complete_handler: fileDialogComplete,
                upload_progress_handler: uploadProgress,
                upload_error_handler: uploadError,
                upload_success_handler: uploadSuccess,
                upload_complete_handler: uploadComplete,
                button_placeholder_id: this._uniqid + "imgbtn_" + this._index,
                button_image_url: "/images/swfupload/XPButtonNoText_92_24.gif",
                button_width: 92,
                button_height: 24,
                button_text: '<span ></span>',
                button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt; } .buttonSmall { font-size: 10pt; }',
                button_text_top_padding: 1,
                button_text_left_padding: 5,
                button_cursor: SWFUpload.CURSOR.HAND,
                flash_url: "/js/swfupload/swfupload.swf",
                custom_settings: {
                    upload_target: this._uniqid + "fileprogress_" + this._index,
                    HidFileNameId: this._uniqid + "hidden_" + this._index,
                    HidFileName: this._uniqid + "hidden_nnn_" + this._index,
                    ErrMsgId: this._uniqid + "error_" + this._index
                },
                debug: false,
                minimum_flash_version: "9.0.28",
                swfupload_pre_load_handler: swfUploadPreLoad,
                swfupload_load_failed_handler: swfUploadLoadFailed
            });
            this._index++;
        },
        init: function(options) {
            this._initHtml(options);
            this._initSwf();
        }
    };

    var iGysGwHeTong = {
        addRowCallBack: function($tr) {
            var _$upload = $tr.find(".i_gwhetong_upload");
            if (_$upload.length > 0) {
                _$upload.html('');
                iGwHeTongUpload.init({ box: _$upload });
            }

            var _$chakan = $tr.find(".chakangwhetongfujian");
            if (_$chakan.length > 0) {
                _$chakan.remove();
            }
        },
        init: function() {
            $("input[name='txtHeTongIsQiYong']").each(function() {
                var _$obj = $(this);
                if (_$obj.attr("i_val") == "1") _$obj.attr("checked", "checked");
            });
        }
    };

    $(document).ready(function() {
        $(".i_gwhetong_upload").each(function() { iGwHeTongUpload.init({ box: this }); });
        $("#GwHeTongAutoAdd").autoAdd({ addCallBack: iGysGwHeTong.addRowCallBack });
        iGysGwHeTong.init();
    });

</script>

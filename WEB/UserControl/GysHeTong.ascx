<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GysHeTong.ascx.cs" Inherits="EyouSoft.Web.UserControl.GysHeTong" %>
<div style="margin: 0 auto; width: 99%;" id="i_div_hetong">
    <span class="formtableT formtableT02">合同信息</span>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" id="heTongAutoAdd">
        <tr>
            <td width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                编号
            </td>
            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                合同开始时间
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                结束时间
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                合同附件
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" <%=IsBaoDi?"":"style='display:none;'" %>>
                保底量
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" <%=IsJiangLi?"":"style='display:none;'" %>>
                奖励量
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                备注
            </td>
            <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT i_hetongcaozuo">
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
                <input name="txtHeTongSTime" type="text" class="formsize80 input-txt" value="<%#Eval("STime","{0:yyyy-MM-dd}") %>"
                    onfocus="WdatePicker()" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtHeTongETime" type="text" class="formsize80 input-txt" value="<%#Eval("ETime","{0:yyyy-MM-dd}") %>"
                    onfocus="WdatePicker()" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <div style="margin: 0px 10px;" class="i_hetong_upload" style="float:left;">
                </div>
                <input type="hidden" name="i_hetong_upload_hidden_y" value="<%#Eval("FilePath") %>" />
                <%#GetChaKanFuJian(Eval("FilePath"))%>
            </td>
            <td align="center" bgcolor="#FFFFFF" <%#IsBaoDi?"":"style='display:none;'" %>>
                <input name="txtHeTongBaoDi" type="text" class="formsize40 input-txt" value="<%#Eval("BaoDi") %>"
                    valid="RegInteger" errmsg="请输入正确的保底量" maxlength="10" />
            </td>
            <td align="center" bgcolor="#FFFFFF" <%#IsJiangLi?"":"style='display:none;'" %>>
                <input name="txtHeTongJiangLi" type="text" class="formsize40 input-txt" value="<%#Eval("JiangLi") %>"
                    valid="RegInteger" errmsg="请输入正确的奖励量" maxlength="10" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtHeTongBeiZhu" type="text" class="formsize140 input-txt" value="<%#Eval("BeiZhu") %>"
                    maxlength="255" />
            </td>
            <td align="center" bgcolor="#FFFFFF" class="i_hetongcaozuo">
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
                <input name="txtHeTongSTime" type="text" class="formsize80 input-txt" onfocus="WdatePicker()" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtHeTongETime" type="text" class="formsize80 input-txt" onfocus="WdatePicker()" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <div style="margin: 0px 10px;" class="i_hetong_upload">
                </div>
                <input type="hidden" name="i_hetong_upload_hidden_y" value="" />
            </td>
            <td align="center" bgcolor="#FFFFFF" <%=IsBaoDi?"":"style='display:none;'" %>>
                <input name="txtHeTongBaoDi" type="text" class="formsize40 input-txt" valid="RegInteger"
                    errmsg="请输入正确的保底量" maxlength="10" />
            </td>
            <td align="center" bgcolor="#FFFFFF" <%=IsJiangLi?"":"style='display:none;'" %>>
                <input name="txtHeTongJiangLi" type="text" class="formsize40 input-txt" valid="RegInteger"
                    errmsg="请输入正确的奖励量" maxlength="10" />
            </td>
            <td align="center" bgcolor="#FFFFFF">
                <input name="txtHeTongBeiZhu" type="text" class="formsize140 input-txt" maxlength="255" />
            </td>
            <td align="center" bgcolor="#FFFFFF" class="i_hetongcaozuo">
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
    #heTongAutoAdd .progressWrapper
    {
        width: 200px;
        overflow: hidden;
    }
    #heTongAutoAdd .progressName
    {
        overflow: hidden;
        width: 150px; height:20px;
    }
</style>

<script type="text/javascript">
    var iHeTongUpload = {
        _index: 1, _uniqid: 'i_hetong_upload_',
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

    var iGysHeTong = {
        addRowCallBack: function($tr) {
            var _$upload = $tr.find(".i_hetong_upload");
            if (_$upload.length > 0) {
                _$upload.html('');
                iHeTongUpload.init({ box: _$upload });
            }

            var _$chakan = $tr.find(".chakanhetongfujian");
            if (_$chakan.length > 0) {
                _$chakan.remove();
            }
        }
    };

    $(document).ready(function() {
        $(".i_hetong_upload").each(function() { iHeTongUpload.init({ box: this }); });
        $("#heTongAutoAdd").autoAdd({ addCallBack: iGysHeTong.addRowCallBack });
    });

</script>

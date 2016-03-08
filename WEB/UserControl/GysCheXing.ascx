<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GysCheXing.ascx.cs"
    Inherits="EyouSoft.Web.UserControl.GysCheXing" %>
<div style="margin: 0 auto; width: 99%;">
    <span class="formtableT formtableT02">车辆信息</span>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="cheXingAutoAdd">
        <tr>
            <td width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                编号
            </td>
            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                车型
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                车型照片
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                座位数
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                单价基数
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                备注
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
                    <td height="28" align="center" bgcolor="#FFFFFF">
                        <input type="hidden" name="txtCheXingId" value="<%#Eval("CheXingId") %>" />
                        <input name="txtCheXingName" type="text" class="formsize80 input-txt" value="<%#Eval("Name") %>" maxlength="255" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <div style="margin: 0px 10px;" class="i_chexing_upload">
                        </div>
                        <input type="hidden" name="i_chexing_upload_hidden_y" value="<%#Eval("FuJian.FilePath") %>" />
                        <%#GetChaKanFuJian(Eval("FuJian.FilePath"))%>
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="txtCheXingZuoWeiShu" type="text" class="formsize40 input-txt" value="<%#Eval("ZuoWeiShu") %>"
                            valid="RegInteger" errmsg="请输入正确的座位数" maxlength="2" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="txtCheXingDanJiaJiShu" type="text" class="formsize80 input-txt" value="<%#Eval("DanJiaJiShu","{0:F2}") %>"
                            valid="isNumber" errmsg="请输入正确的单价基数" maxlength="10" />
                    </td>
                    <td align="center" bgcolor="#FFFFFF">
                        <input name="txtCheXingBeiZhu" type="text" class="formsize140 input-txt" value="<%#Eval("BeiZhu") %>" maxlength="255" />
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
                <td height="28" align="center" bgcolor="#FFFFFF">
                    <input type="hidden" name="txtCheXingId" value="" />
                    <input name="txtCheXingName" type="text" class="formsize80 input-txt" value="" maxlength="255" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <div style="margin: 0px 10px;" class="i_chexing_upload">
                    </div>
                    <input type="hidden" name="i_chexing_upload_hidden_y" value="" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtCheXingZuoWeiShu" type="text" class="formsize40 input-txt" value=""
                        valid="RegInteger" errmsg="请输入正确的座位数" maxlength="2" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtCheXingDanJiaJiShu" type="text" class="formsize80 input-txt" value=""
                        valid="isNumber" errmsg="请输入正确的单价基数" maxlength="10" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtCheXingBeiZhu" type="text" class="formsize140 input-txt" value="" maxlength="255" />
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
    #cheXingAutoAdd .progressWrapper
    {
        width: 200px;
        overflow: hidden;
    }
    #cheXingAutoAdd .progressName
    {
        overflow: hidden;
        width: 150px; height:20px;
    }
</style>

<script type="text/javascript">
    var iCheXingUpload = {
        _index: 1, _uniqid: 'i_chexing_upload_',
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
                file_types: "*.bmp;*.jpg;*.gif;*.jpeg;*.png;",
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

    var iGysCheXing = {
        addRowCallBack: function($tr) {
            var _$upload = $tr.find(".i_chexing_upload");
            if (_$upload.length > 0) {
                _$upload.html('');
                iCheXingUpload.init({ box: _$upload });
            }

            var _$chakan = $tr.find(".chakanhetongfujian");
            if (_$chakan.length > 0) {
                _$chakan.remove();
            }
        }
    };

    $(document).ready(function() {
        $(".i_chexing_upload").each(function() { iCheXingUpload.init({ box: this }); });
        $("#cheXingAutoAdd").autoAdd({ addCallBack: iGysCheXing.addRowCallBack });
    });

</script>

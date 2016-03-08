<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GysJingDian.ascx.cs" Inherits="EyouSoft.Web.UserControl.GysJingDian" %>
<div style="margin: 0 auto; width: 99%;" id="i_div_jingdian">
    <span class="formtableT formtableT02">景点信息</span>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" id="JingDianAutoAdd">
        <tr>
            <td width="25" height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                编号
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                景点名称
            </td>
            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                景点星级
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                景点网址
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                景点浏览时间
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                景点总机
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                是否推荐图片
            </td>
            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                是否秀
            </td>
            <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT i_jingdiancaozuo">
                操作
            </td>
        </tr>
        <asp:Repeater runat="server" ID="rpt">
        <ItemTemplate>
        <tbody class="tempRow">
            <tr>
                <td rowspan="3" align="center" bgcolor="#FFFFFF" class="index">
                    <%# Container.ItemIndex + 1%>
                </td>
                <td rowspan="3" align="center" bgcolor="#FFFFFF">
                    <input type="hidden" name="txtJingDianId" value="<%#Eval("JingDianId") %>" />
                    <input name="txtJingDianName" type="text" class="formsize140 input-txt" value="<%#Eval("Name") %>"
                        maxlength="50" />
                </td>
                <td height="28" align="center" bgcolor="#FFFFFF">
                    <select name="txtJingDianXingJi" i_val="<%#(int)Eval("XingJi") %>">
                        <option value="0" selected="selected">请选择</option>
                        <option value="1">A</option>
                        <option value="2">AA</option>
                        <option value="3">AAA</option>
                        <option value="4">AAAA</option>
                        <option value="5">AAAAA</option>
                    </select>
                </td>
                <td height="28" align="center" bgcolor="#FFFFFF">
                    <input name="txtJingDianWangZhi" type="text" class="formsize120 input-txt" value="<%#Eval("WangZhi") %>"
                        maxlength="255" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtJingDianYouLanShiJian" type="text" class="formsize80 input-txt" value="<%#Eval("YouLanShiJian") %>"
                        maxlength="255" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtJingDianTelephone" type="text" class="formsize80 input-txt" value="<%#Eval("Telephone") %>"
                        maxlength="50" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input type="checkbox" name="txtJingDianTuiJian" i_val="<%#(bool)Eval("IsTuiJian")?"1":"0" %>" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input type="checkbox" name="txtIsXiu" i_val="<%#(bool)Eval("IsXiu")?"1":"0" %>" />
                </td>
                <td rowspan="3" align="center" bgcolor="#FFFFFF" class="i_jingdiancaozuo">
                    <a href="javascript:void(0)" class="addbtn">
                        <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                            class="delbtn">
                            <img src="/images/delimg.gif" width="48" height="20" /></a>
                </td>
            </tr>
            <tr>
                <td height="23" colspan="2" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    景点图片上传
                </td>
                <td colspan="4" align="left" bgcolor="#FFFFFF">
                    <div style="margin: 0px 10px;" class="i_jingdian_upload">
                    </div>
                    <input type="hidden" name="i_jingdian_upload_hidden_y" value="<%#Eval("FuJian.FilePath") %>" />
                    <%#GetChaKanFuJian(Eval("FuJian.FilePath"))%>
                </td>
            </tr>
            <tr>
                <td height="23" colspan="2" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    景点描述（导游词）
                </td>
                <td colspan="4" align="left" bgcolor="#FFFFFF">
                    <textarea name="txtJingDianMiaoShu" cols="6=" style="height: 36px; padding: 3px; width: 350px;"><%#Eval("JianJie")%></textarea>
                </td>
            </tr>
        </tbody>
        </ItemTemplate>
        </asp:Repeater>
        
        <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
        <tbody class="tempRow">
            <tr>
                <td rowspan="3" align="center" bgcolor="#FFFFFF" class="index">
                    1
                </td>
                <td rowspan="3" align="center" bgcolor="#FFFFFF">
                    <input type="hidden" name="txtJingDianId" />
                    <input name="txtJingDianName" type="text" class="formsize140 input-txt" maxlength="50" />
                </td>
                <td height="28" align="center" bgcolor="#FFFFFF">
                    <select name="txtJingDianXingJi" i_val="0">
                        <option value="0">请选择</option>
                        <option value="1">A</option>
                        <option value="2">AA</option>
                        <option value="3">AAA</option>
                        <option value="4">AAAA</option>
                        <option value="5">AAAAA</option>
                    </select>
                </td>
                <td height="28" align="center" bgcolor="#FFFFFF">
                    <input name="txtJingDianWangZhi" type="text" class="formsize120 input-txt" maxlength="255" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtJingDianYouLanShiJian" type="text" class="formsize80 input-txt" maxlength="255" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input name="txtJingDianTelephone" type="text" class="formsize80 input-txt" maxlength="50" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input type="checkbox" name="txtJingDianTuiJian" i_val="0" />
                </td>
                <td align="center" bgcolor="#FFFFFF">
                    <input type="checkbox" name="txtIsXiu" i_val="0" />
                </td>
                <td rowspan="3" align="center" bgcolor="#FFFFFF" class="i_jingdiancaozuo">
                    <a href="javascript:void(0)" class="addbtn">
                        <img src="/images/addimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                            class="delbtn">
                            <img src="/images/delimg.gif" width="48" height="20" /></a>
                </td>
            </tr>
            <tr>
                <td height="23" colspan="2" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    景点图片上传
                </td>
                <td colspan="4" align="left" bgcolor="#FFFFFF">
                    <div style="margin: 0px 10px;" class="i_jingdian_upload">
                    </div>
                    <input type="hidden" name="i_jingdian_upload_hidden_y" value="" />
                </td>
            </tr>
            <tr>
                <td height="23" colspan="2" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                    景点描述（导游词）
                </td>
                <td colspan="4" align="left" bgcolor="#FFFFFF">
                    <textarea name="txtJingDianMiaoShu" cols="6=" style="height: 36px; padding: 3px; width: 350px;"></textarea>
                </td>
            </tr>
        </tbody>
        </asp:PlaceHolder>
        
    </table>
</div>
<style type="text/css">
    #JingDianAutoAdd .progressWrapper
    {
        width: 200px;
        overflow: hidden;
    }
    #JingDianAutoAdd .progressName
    {
        overflow: hidden;
        width: 150px; height:20px;
    }
</style>

<script type="text/javascript">
    var iJingDianUpload = {
        _index: 1, _uniqid: 'i_jingdian_upload_',
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

    var iGysJingDian = {
        addRowCallBack: function($tr) {
            var _$upload = $tr.find(".i_jingdian_upload");
            if (_$upload.length > 0) {
                _$upload.html('');
                iJingDianUpload.init({ box: _$upload });
            }

            var _$chakan = $tr.find(".chakanjingdianfujian");
            if (_$chakan.length > 0) {
                _$chakan.remove();
            }
        },
        init: function() {
            $("select[name='txtJingDianXingJi']").each(function() {
                var _$obj = $(this);
                _$obj.val(_$obj.attr("i_val"));
            });

            $("input[name='txtJingDianTuiJian']").each(function() {
                var _$obj = $(this);
                if (_$obj.attr("i_val") == "1") _$obj.attr("checked", "checked");
            });
        	
        	 $("input[name='txtIsXiu']").each(function() {
                var _$obj = $(this);
                if (_$obj.attr("i_val") == "1") _$obj.attr("checked", "checked");
            });
        }
    };

    $(document).ready(function() {
        $(".i_jingdian_upload").each(function() { iJingDianUpload.init({ box: this }); });
        $("#JingDianAutoAdd").autoAdd({ addCallBack: iGysJingDian.addRowCallBack });
        iGysJingDian.init();
    });

</script>


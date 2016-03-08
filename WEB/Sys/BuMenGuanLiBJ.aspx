<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuMenGuanLiBJ.aspx.cs"
    Inherits="EyouSoft.Web.Sys.BuMenGuanLiBJ" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="HrSelect" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .errmsg
        {
            color: #f00;
            font-size: 12px;
        }
    </style>

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form id="form1" method="post" runat="server" enctype="multipart/form-data">
    <div class="alertbox-outbox">
        <table id="tb_box" width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="20%" height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>部门名称：
                </td>
                <td width="35%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtDepartName" class="inputtext formsize120" valid="required"
                        errmsg="部门名称不能为空！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidDepartId" />
                </td>
                <td width="16%" align="right" bgcolor="#B7E0F3">
                    部门主管：
                </td>
                <td width="31%" align="left" bgcolor="#e0e9ef" class="UserInfo">
                    <uc2:HrSelect runat="server" ID="HrSelect1" ReadOnly="true" SModel="1" IsValid="false" />
                </td>
            </tr>
            <tr>
                <td height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    上级部门：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:TextBox ID="txtUpSection" runat="server" Enabled="false" CssClass="inputtext formsize120"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidupsectionId" />
                </td>
                <td width="16%" align="right" bgcolor="#B7E0F3">
                    部门计调：
                </td>
                <td width="31%" align="left" bgcolor="#e0e9ef" class="UserInfo">
                    <uc2:HrSelect runat="server" ID="HrSelectJD" ReadOnly="true" SModel="1" IsValid="false"
                        SetTitle="部门计调" />
                </td>
            </tr>
            <tr>
                <td height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    联系电话：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtContact" class="inputtext formsize120"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#B7E0F3">
                    传真：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtFaxa" class="inputtext formsize120" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="25" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    是否使用公司打印配置：
                </td>
                <td height="28" colspan="3" align="left" bgcolor="#e0e9ef">
                    <asp:CheckBox ID="chk_isDefault" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    备注：
                </td>
                <td colspan="3" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtRemark" Height="100" class="inputtext formsize450"
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <asp:PlaceHolder ID="add" runat="server" Visible="false">
                <tbody class="tempRow">
                    <tr>
                        <td height="28" align="right" class="alertboxTableT">
                            名称：
                        </td>
                        <td height="28" align="left" colspan="2">
                            <input type="text" class="formsize140 input-txt" name="PrintName">
                        </td>
                        <td align="center" rowspan="5">
                            <a class="addbtn" href="javascript:void(0)">
                                <img width="48" height="20" src="../images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                                    <img width="48" height="20" src="../images/delimg.gif"></a>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" align="right" class="alertboxTableT">
                            打印页眉：
                        </td>
                        <td height="28" align="left" colspan="2">
                            <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox" data-id="Cruiseimg_upload_swfbox1">
                                <div>
                                    <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                        name="hide_head_file" value="|" />
                                    <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                        data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
                                </div>
                                <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                                </div>
                                <div data-class="Cruiseimg_upload" data-id="thumbnails">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" align="right" class="alertboxTableT">
                            打印页脚：
                        </td>
                        <td height="28" align="left" colspan="2">
                            <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox" data-id="Cruiseimg_upload_swfbox2">
                                <div>
                                    <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                        name="hide_Foot_file" value="|" />
                                    <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                        data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
                                </div>
                                <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                                </div>
                                <div data-class="Cruiseimg_upload" data-id="thumbnails">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" align="right" class="alertboxTableT">
                            打印模板：
                        </td>
                        <td height="28" align="left" colspan="2">
                            <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox" data-id="Cruiseimg_upload_swfbox3">
                                <div>
                                    <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                        name="hide_Temp_file" value="|" />
                                    <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                        data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
                                </div>
                                <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                                </div>
                                <div data-class="Cruiseimg_upload" data-id="thumbnails">
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" align="right" class="alertboxTableT">
                            是否默认：
                        </td>
                        <td height="28" align="left" colspan="2">
                            <input type="hidden" name="hdremind" value="0" />
                            <input type="checkbox" name="chk_Default">
                        </td>
                    </tr>
                </tbody>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="edit" runat="server" Visible="false">
                <cc1:CustomRepeater ID="CustomRepeater1" runat="server">
                    <ItemTemplate>
                        <tbody class="tempRow">
                            <tr>
                                <td height="28" align="right" class="alertboxTableT">
                                    名称：
                                </td>
                                <td height="28" align="left" colspan="2">
                                    <input type="text" id="textfield6" class="formsize140 input-txt" name="PrintName"
                                        value="<%#Eval("PrintName") %>">
                                </td>
                                <td align="center" rowspan="5">
                                    <a class="addbtn" href="javascript:void(0)">
                                        <img width="48" height="20" src="../images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                                            <img width="48" height="20" src="../images/delimg.gif"></a>
                                </td>
                            </tr>
                            <tr>
                                <td height="28" align="right" class="alertboxTableT">
                                    打印页眉：
                                </td>
                                <td height="28" align="left" colspan="2">
                                    <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox" data-id="Cruiseimg_upload_swfbox1">
                                        <div>
                                            <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                                name="hide_head_file" value="|" />
                                            <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                                data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
                                        </div>
                                        <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                                        </div>
                                        <div data-class="Cruiseimg_upload" data-id="thumbnails">
                                        </div>
                                    </div>
                                    <%# OutPutHTML(Eval("PrintHeader").ToString(),1)%>
                                </td>
                            </tr>
                            <tr>
                                <td height="28" align="right" class="alertboxTableT">
                                    打印页脚：
                                </td>
                                <td height="28" align="left" colspan="2">
                                    <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox" data-id="Cruiseimg_upload_swfbox2">
                                        <div>
                                            <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                                name="hide_Foot_file" value="|" />
                                            <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                                data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
                                        </div>
                                        <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                                        </div>
                                        <div data-class="Cruiseimg_upload" data-id="thumbnails">
                                        </div>
                                    </div>
                                    <%#  OutPutHTML(Eval("PrintFooter").ToString(),2)%>
                                </td>
                            </tr>
                            <tr>
                                <td height="28" align="right" class="alertboxTableT">
                                    打印模板：
                                </td>
                                <td height="28" align="left" colspan="2">
                                    <div style="margin: 0px 10px;" data-class="Cruiseimg_upload_swfbox" data-id="Cruiseimg_upload_swfbox3">
                                        <div>
                                            <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                                                name="hide_Temp_file" value="|" />
                                            <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                                                data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
                                        </div>
                                        <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
                                        </div>
                                        <div data-class="Cruiseimg_upload" data-id="thumbnails">
                                        </div>
                                    </div>
                                    <%# OutPutHTML(Eval("PrintTemplates").ToString(),3)%>
                                </td>
                            </tr>
                            <tr>
                                <td height="28" align="right" class="alertboxTableT">
                                    是否默认：
                                </td>
                                <td height="28" align="left" colspan="2">
                                    <input type="hidden" name="hdremind" value='<%#Convert.ToBoolean(Eval("IsDefault"))==true?"1":"0" %>' />
                                    <input type="checkbox" name="chk_Default" <%#Convert.ToBoolean(Eval("IsDefault"))==true?"checked='checked'":"" %>>
                                </td>
                            </tr>
                        </tbody>
                    </ItemTemplate>
                </cc1:CustomRepeater>
            </asp:PlaceHolder>
        </table>
        <div style="margin: 0px 10px; display: none;" id="divCruiseimgUploadTemp1">
            <div>
                <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                    name="hide_head_file" value="|" />
                <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                    data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
            </div>
            <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
            </div>
            <div data-class="Cruiseimg_upload" data-id="thumbnails">
            </div>
        </div>
        <div style="margin: 0px 10px; display: none;" id="divCruiseimgUploadTemp2">
            <div>
                <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                    name="hide_Foot_file" value="|" />
                <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                    data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
            </div>
            <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
            </div>
            <div data-class="Cruiseimg_upload" data-id="thumbnails">
            </div>
        </div>
        <div style="margin: 0px 10px; display: none;" id="divCruiseimgUploadTemp3">
            <div>
                <input type="hidden" data-class="Cruiseimg_upload" data-id="hide_Cruiseimg_file"
                    name="hide_Temp_file" value="|" />
                <span data-class="Cruiseimg_upload" data-id="spanButtonPlaceholder"></span><span
                    data-class="Cruiseimg_upload" data-id="errMsg" class="errmsg"></span>
            </div>
            <div data-class="Cruiseimg_upload" data-id="divFileProgressContainer">
            </div>
            <div data-class="Cruiseimg_upload" data-id="thumbnails">
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0);" hidefocus="true" id="btnSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" onclick="PageJsData.ResetForm()" hidefocus="true">
                    <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    </form>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>',
                doType: '<%=Request.QueryString["doType"] %>'
            },
            CreateFlashUpload: function(flashUpload, idNo) {
                flashUpload = new SWFUpload({
                    upload_url: "/CommonPage/upload.aspx",
                    file_post_name: "Filedata",
                    post_params: {
                        "ASPSESSID": "<%=Session.SessionID %>"
                    },

                    file_size_limit: "2 MB",
                    file_types: "*.jpg;*.gif;*.jpeg;*.png;*.dot;*.doc;",
                    file_types_description: "附件上传",
                    file_upload_limit: 1,
                    swfupload_loaded_handler: function() { document.title = "" },
                    file_dialog_start_handler: uploadStart,
                    upload_start_handler: uploadStart,
                    file_queued_handler: fileQueued,
                    file_queue_error_handler: fileQueueError,
                    file_dialog_complete_handler: fileDialogComplete,
                    upload_progress_handler: uploadProgress,
                    upload_error_handler: uploadError,
                    upload_success_handler: uploadSuccess,
                    upload_complete_handler: uploadComplete,

                    // Button settings
                    button_placeholder_id: "spanButtonPlaceholder_" + idNo,
                    button_image_url: "/images/swfupload/XPButtonNoText_92_24.gif",
                    button_width: 92,
                    button_height: 24,
                    button_text: '<span ></span>',
                    button_text_style: '',
                    button_text_top_padding: 1,
                    button_text_left_padding: 5,
                    button_cursor: SWFUpload.CURSOR.HAND,
                    flash_url: "/js/swfupload/swfupload.swf",
                    custom_settings: {
                        upload_target: "divFileProgressContainer_" + idNo,
                        HidFileNameId: "hide_Cruiseimg_file_" + idNo,
                        HidFileName: "hide_Cruiseimg_file_Old",
                        ErrMsgId: "errMsg_" + idNo,
                        UploadSucessCallback: null
                    },
                    debug: false,
                    minimum_flash_version: "9.0.28",
                    swfupload_pre_load_handler: swfUploadPreLoad,
                    swfupload_load_failed_handler: swfUploadLoadFailed
                });
            },
            UploadArgsList: [],
            InitSwfUpload: function(tr) {
                var $box = tr || $("#tb_box");
                $box.find("div[data-class='Cruiseimg_upload_swfbox']").each(function() {
                    var idNo = parseInt(Math.random() * 100000);
                    $(this).find("[data-class='Cruiseimg_upload']").each(function() {
                        if ($(this).attr("id") == "") {
                            $(this).attr("id", $(this).attr("data-id") + "_" + idNo);
                        }
                    })
                    var swf = null;
                    PageJsData.CreateFlashUpload(swf, idNo);
                    if (swf) {
                        PageJsData.UploadArgsList.push(swf);
                    }
                })
            },
            DelFile: function(obj) {
                var self = $(obj);
                self.closest("span").hide();
                self.closest("span").prev().val("");
            },
            ResetForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },
            Form: null,
            FormCheck: function() {
                this.Form = $("#btnSave").closest("form").get(0)
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            Save: function() {
                var that = this;
                if (that.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/Sys/BuMenGuanLiBJ.aspx?";
                    url += $.param({
                        doType: that.Query.doType,
                        save: "save",
                        sl: that.Query.sl
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(that.Form).serialize().replace(),
                        dataType: "json",
                        success: function(result) {
                            if (result.result == "1") {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    if (that.Query.doType == 'add') {
                                        if (result.id != 0) {
                                            window.parent.DM.callbackAddD(result.id);
                                        }
                                        else {
                                            parent.location.reload();
                                        }
                                    }
                                    else {
                                        window.parent.DM.callbackUpdateD(result.id, result.nowName, result.updateP)
                                    }
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                });

                            }
                            else { parent.tableToolbar._showMsg(result.msg, function() { PageJsData.BindBtn(); }); }
                        },
                        error: function() {
                            parent.tableToolbar._showMsg(tableToolbar.errorMsg, function() { PageJsData.BindBtn(); });
                        }
                    });
                }
            },
            AddRowCallBack: function(tr) {
                var $tr = tr;
                $tr.find("div[data-id='Cruiseimg_upload_swfbox1']").html($("#divCruiseimgUploadTemp1").html());
                $tr.find("div[data-id='Cruiseimg_upload_swfbox2']").html($("#divCruiseimgUploadTemp2").html());
                $tr.find("div[data-id='Cruiseimg_upload_swfbox3']").html($("#divCruiseimgUploadTemp3").html());

                $tr.find("span[class='errmsg']").html("");
                $tr.find("div[data-class='span_Cruiseimg_file']").remove();
                $tr.find("span[class='upload_filename']").remove();
                PageJsData.InitSwfUpload($tr);
            },
            changChk: function() {
                $("[name=chk_Default]").live("change", function() {
                    if (this.checked) {

                        $(this).closest("td").find("input:[name='hdremind']").val("1")
                    }
                    else {
                        $(this).closest("td").find("input:[name='hdremind']").val("0")
                    }
                })
            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    PageJsData.Save();
                    return false;
                })
                $("#btnSave").attr("class", "").html("<s class=\"baochun\"></s>保 存");
            }
        }

        $(function() {
            PageJsData.BindBtn();
            PageJsData.InitSwfUpload();
            PageJsData.changChk();
            $("#tb_box").autoAdd({ tempRowClass: "tempRow", addButtonClass: "addbtn", delButtonClass: "delbtn", addCallBack: PageJsData.AddRowCallBack })
        });
    </script>

</body>
</html>

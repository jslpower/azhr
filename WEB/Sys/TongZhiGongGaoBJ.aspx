<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TongZhiGongGaoBJ.aspx.cs"
    Inherits="EyouSoft.Web.Sys.TongZhiGongGaoBJ" %>

<%@ Register Src="~/UserControl/SelectSection.ascx" TagName="SelectSection" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="SingleFileUpload" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <style type="text/css">
        .errmsg
        {
            font-size: 12px;
            color: Red;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <form id="form1" runat="server">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="24%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>标题：
                </td>
                <td width="76%" height="28" align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtTitle" CssClass="inputtext formsize120" valid="required"
                        errmsg="标题不能为空！"></asp:TextBox>
                    <asp:HiddenField runat="server" ID="hidNotiecID" />
                </td>
            </tr>
            <tr class="selectReceived">
                <td rowspan="2" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>发布对象：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:CheckBox runat="server" ID="PointDept" Text="指定部门" />
                    <uc1:SelectSection ID="SelectSection1" runat="server" ReadOnly="true" />
                </td>
            </tr>
            <tr class="selectReceived">
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:CheckBox runat="server" ID="innerSection" onclick="PageJsData.IsChecked(this)"
                        Text="公司内部" />
                    <asp:CheckBox runat="server" ID="fenxiao" onclick="PageJsData.IsChecked(this)" Text="分销商" />
                    <asp:CheckBox runat="server" ID="gongying" onclick="PageJsData.IsChecked(this)" Text="供应商"
                        Visible="false" />
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="errmsg">*</span>内容：
                </td>
                <td align="left" bgcolor="#e0e9ef">
                    <asp:TextBox runat="server" ID="txtContent" Height="65" CssClass="inputtext formsize450"
                        valid="required" errmsg="内容不能为空！" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    附件：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <uc2:SingleFileUpload runat="server" ID="SingleFileUpload1" IsUploadMore="true" IsUploadSelf="true" />
                    <asp:Label runat="server" ID="lbFiles" class="labelFiles"></asp:Label><br />
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    是否提醒：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <span class="alertboxTableT">
                        <asp:RadioButton runat="server" Text="是" ID="warn" GroupName="isTixing" />
                        <asp:RadioButton runat="server" Text="否" ID="nowarn" Checked="true" GroupName="isTixing" />
                    </span>
                </td>
            </tr>
            <%-- <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    是否发送短信：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <span class="alertboxTableT" style="white-space: nowrap">
                        <asp:RadioButton runat="server" ID="sendMsg" Text="是" GroupName="isSendMsg" />
                        <asp:RadioButton runat="server" ID="noSendMsg" Text="否" Checked="true" GroupName="isSendMsg" />
                        <span id="spanContent" style="display: none">
                            <asp:TextBox runat="server" ID="msgContent" Width="300"></asp:TextBox></span>
                    </span>
                </td>
            </tr>--%>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布人：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label runat="server" ID="lbSender"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    发布时间：
                </td>
                <td height="28" align="left" bgcolor="#e0e9ef">
                    <asp:Label runat="server" ID="lbTime"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn" style="text-align: right;">
            <asp:PlaceHolder runat="server" ID="ph_Save"><a href="javascript:void(0);" hidefocus="true"
                id="btnSave"><s class="baochun"></s>保 存</a></asp:PlaceHolder>
            <a href="javascript:void(0);" onclick="PageJsData.HideForm()" hidefocus="true"><s
                class="chongzhi"></s>关 闭</a>
        </div>
        </form>
    </div>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/jquery.blockUI.js" type="text/javascript"></script>

    <script type="text/javascript">
        var PageJsData = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>',
                doType: '<%=Request.QueryString["doType"] %>'
            },
            IsChecked: function(obj) {
                if (obj.checked) {
                    $("#<%=PointDept.ClientID %>").attr("checked", "");
                    $("#<%=SelectSection1.SelectNameClient %>").removeAttr("valid");
                    $("#<%=SelectSection1.SelectNameClient %>").removeAttr("value");
                    $("#<%=SelectSection1.SelectIDClient %>").removeAttr("value");
                    $("#spanSelectSection1").css("display", "none");
                }
            },
            DelFile: function(obj) {
                var self = $(obj);
                self.closest("span").hide();
                self.next(":hidden").val("");
            },
            HideForm: function() {
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
            },

            Form: null,
            //表单验证
            FormCheck: function() {
                this.Form = $("#btnSave").closest("form").get(0)
                FV_onBlur.initValid(this.Form);
                return ValiDatorForm.validator(this.Form, "parent");
            },
            Save: function() {
                if (PageJsData.FormCheck()) {
                    $("#btnSave").unbind("click").addClass("alertbox-btn_a_active").html("<s class=\"baochun\"></s> 提交中...");
                    var url = "/Sys/TongZhiGongGaoBJ.aspx?";
                    url += $.param({
                        doType: PageJsData.Query.doType,
                        save: "save",
                        sl: PageJsData.Query.sl
                    });
                    $.newAjax({
                        type: "post",
                        cache: false,
                        url: url,
                        data: $(PageJsData.Form).serialize().replace(),
                        dataType: "json",
                        success: function(result) {
                            if (result.result == "1") {
                                top.tableToolbar._showMsg(result.msg, function() {
                                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
                                    parent.window.document.getElementById("btnSubmit").click();
                                });

                            }
                            else {
                                parent.tableToolbar._showMsg(result.msg, function() {
                                    PageJsData.BindBtn();
                                });
                            }
                        },
                        error: function() {
                            //ajax异常--你懂得
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                            PageJsData.BindBtn();
                        }
                    });
                }
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
            if ($("#<%=PointDept.ClientID %>").attr("checked")) {
                $("#spanSelectSection1").css("display", "inline");
                $("#<%=SelectSection1.SelectNameClient %>").attr({ valid: "required", errmsg: "指定部门不能为空！" });
            }
            if (!$("#<%=PointDept.ClientID %>").attr("checked")) {
                $("#spanSelectSection1").css("display", "none");
                $("#<%=SelectSection1.SelectNameClient %>").removeAttr("valid");
            }
            $("#<%=PointDept.ClientID %>").click(function() {
                if (this.checked) {
                    $("#spanSelectSection1").css("display", "inline");
                    $("#<%=SelectSection1.SelectNameClient %>").attr({ valid: "required", errmsg: "指定部门不能为空！" })
                    $(".selectReceived").last().find(":checkbox").each(function() {
                        this.checked = false;
                    })
                }
                if (!this.checked) {
                    $("#<%=SelectSection1.SelectNameClient %>").removeAttr("valid");
                    $("#spanSelectSection1").css("display", "none");
                }
            });
            PageJsData.BindBtn();

        });
    </script>

</body>
</html>

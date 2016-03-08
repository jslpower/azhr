<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiFen.aspx.cs" Inherits="EyouSoft.Web.Crm.JiFen" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>个人会员积分管理</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>
    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</head>
<body style="background:0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02">积分明细</span>
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            编号
                        </td>
                        <td bgcolor="#B7E0F3" align="center" height="23" class="alertboxTableT">
                            类别
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            积分时间
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            积分
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            说明
                        </td>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td bgcolor="#FFFFFF" align="center">
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td bgcolor="#FFFFFF" align="center" height="28">
                                    <%#Eval("ZengJianLeiBie").ToString()%>
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("JiFenShiJian"), this.ProviderToDate)%>
                                </td>
                                <td bgcolor="#FFFFFF" align="center">
                                    <%#Eval("JiFen")%>
                                </td>
                                <td bgcolor="#FFFFFF" align="left">
                                    <%#Eval("Remark")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <form id="form1" runat="server">
            <div style="position: relative; height: 32px;">
                <div class="pages">
                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                </div>
            </div>
            <div class="hr_5">
            </div>
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                        <td bgcolor="#B7E0F3" align="center" height="23" class="alertboxTableT">
                            类别
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            积分时间
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            积分
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            说明
                        </td>
                        <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                            操作
                        </td>
                    </tr>
                    <tr class="tempRow">
                        <td bgcolor="#FFFFFF" align="center" height="28">
                            <select name="sltType" class="inputselect">
                                <option value="1">增</option>
                                <option value="2">减</option>
                            </select>
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <input type="text" class="inputtext formsize80" name="txtDateTime" valid="required" errmsg="请选择日期!" onfocus="WdatePicker()" />
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <input type="text" class="inputtext formsize80" name="txtSource" valid="required" errmsg="请输入积分!" maxlength="10"/>
                        </td>
                        <td bgcolor="#FFFFFF" align="left">
                            <input type="text" style="width: 300px;" class="inputtext" name="txtRemarks" maxlength="255" />
                        </td>
                        <td bgcolor="#FFFFFF" align="center">
                            <a href="javascript:void(0);" id="btnSave">
                                <img width="48" height="20" src="/images/baocunimg.gif"></a>
                        </td>
                    </tr>
                </tbody>
            </table>
            </form>
        </div>
        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" onclick="javascript:parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();return false;">
                <s class="chongzhi"></s>关 闭</a>
        </div>
    </div>

    <script type="text/javascript">
        var JiFen = {
            PaegSave: function() {
                if (ValiDatorForm.validator($("#btnSave").closest("form").get(0), "parent")) {
                    $("#btnSave").unbind("click");
                    $.newAjax({
                        type: "post",
                        url: "/Crm/JiFen.aspx?" + $.param(JiFen.Data),
                        data: $("#btnSave").closest("form").serialize(),
                        dataType: "json",
                        success: function(ret) {
                            //ajax回发提示
                            if (ret.result == "1") {
                                parent.tableToolbar._showMsg(ret.msg, function() {
                                    window.location.href = window.location.href;
                                });
                            } else {
                                parent.tableToolbar._showMsg(ret.msg);
                            }
                            JiFen.BindClick();
                        },
                        error: function() {
                            parent.tableToolbar._showMsg("操作失败，请稍后再试!");
                            JiFen.BindClick();
                        }
                    });
                }
            },
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                doType: 'save',
                crmId: '<%=Request.QueryString["crmId"] %>'
            },
            BindClick: function() {
                $("#btnSave").click(function() {
                    JiFen.PaegSave();
                    return false;
                })
            }
        }
        $(function() {
            FV_onBlur.initValid($("#btnSave").closest("form").get(0));

            JiFen.BindClick();
        })
    </script>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LineSelect.aspx.cs" Inherits="EyouSoft.Web.CommonPage.LineSelect" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>导游中心-导游排班-导游安排选择</title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <div class="alertbox-outbox">
        <form id="formSearch" action="LineSelect.aspx" method="get">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9">
            <tr>
                <td width="89%" height="28" align="left" bgcolor="#C1E5F5">
                    <span class="alertboxTableT">线路名称：</span>
                    <input name="txtlineName" type="text" class="inputtext formsize120" id="txtlineName"
                        value="<%=Request.QueryString["txtlineName"] %>" />
                    <input type="hidden" name="iframeId" id="iframeId" value='<%=Request.QueryString["iframeId"] %>' />
                    <input type="hidden" name="pIframeId" id="pIframeId" value='<%=Request.QueryString["pIframeId"] %>' />
                    <input type="hidden" name="sModel" id="sModel" value='<%=Request.QueryString["sModel"] %>' />
                    <input type="hidden" name="id" id="id" value='<%=Request.QueryString["id"] %>' />
                    <input type="hidden" name="sl" id="sl" value='<%=Request.QueryString["sl"] %>' />
                    <input type="hidden" name="callBackFun" id="callBackFun" value='<%=Request.QueryString["callBackFun"] %>' />
                    团号：
                    <input name="txttourNo" type="text" class="inputtext formsize120" id="txttourNo"
                        value="<%=Request.QueryString["txttourNo"] %>" />
                    <button type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;">
                        查 询</button>
                </td>
            </tr>
        </table>
        </form>
        <div class="hr_10">
        </div>
        <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0" id="tblList">
            <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    序号
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    线路名称
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    团号
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    出发时间
                </td>
                <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                    返回时间
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rpt_list">
                <ItemTemplate>
                    <tr>
                        <td height="23" align="center">
                            <input type="checkbox" name="contactID" value='<%#Eval("TourCode") %>' />
                            <input type="hidden" name="tourID" value="<%# Eval("TourID") %>" />
                            <span data-linename='<%#Eval("RouteName")%>' data-ltime='<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>' data-rtime='<%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("RDate"),ProviderToDate)%>'>
                                <%#Container.ItemIndex+1 %>
                            </span>
                        </td>
                        <td align="center">
                            <%#Eval("RouteName")%>
                        </td>
                        <td align="center">
                            <%#Eval("TourCode")%>
                        </td>
                        <td align="center">
                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                        </td>
                        <td align="center">
                            <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("RDate"),ProviderToDate)%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        var SelectLinePage = {
            selectTxt: "",
            selectTourCode: "",
            selectTourID: "",
            selectLtime: "",
            selectRtime: "",
            aid: '<%=Request.QueryString["id"] %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetValue: function() {
                var txtArray = new Array();
                var tourCodeArray = new Array();
                var selectLtimeArray = new Array();
                var selectRtimeArray = new Array();
                var tourIDArray = new Array();
                $("#tblList").find("input[type='checkbox']:checked").each(function() {
                    txtArray.push($.trim($(this).next().next().attr("data-linename")));
                    tourCodeArray.push($.trim($(this).val()));
                    selectLtimeArray.push($.trim($(this).next().next().attr("data-ltime")));
                    selectRtimeArray.push($.trim($(this).next().next().attr("data-rtime")));
                    tourIDArray.push($.trim($(this).next().val()));
                })
                SelectLinePage.selectTxt = txtArray.join(',');
                SelectLinePage.selectTourCode = tourCodeArray.join(',');
                SelectLinePage.selectLtime = selectLtimeArray.join(',');
                SelectLinePage.selectRtime = selectRtimeArray.join(',');
                SelectLinePage.selectTourID = tourIDArray.join(',');
            },
            InitSetSelect: function() {
                if (SelectLinePage.aid) {
                    var oldValue = SelectLinePage.parentWindow.$('#' + SelectLinePage.aid).parent().find("input[type='hidden']");
                    if (oldValue.length > 0) {
                        var list = oldValue.val().split(',');
                        for (var i = 0; i < list.length; i++) {
                            $("input[name='contactID'][value='" + list[i] + "']").attr("checked", "checked");
                        }
                    }
                }
            }
        }
        $(function() {
            //判断是否为二级弹窗
            if (SelectLinePage.pIframeID) {
                SelectLinePage.parentWindow = window.parent.Boxy.getIframeWindow(SelectLinePage.pIframeID);
            }
            else {
                SelectLinePage.parentWindow = parent.window;
            }
            SelectLinePage.InitSetSelect();
            $("#a_btn").click(function() {
                SelectLinePage.SetValue();

                //根据父级是否为弹窗传值
                var data = { id: '<%=Request.QueryString["id"] %>', tourcode: SelectLinePage.selectTourCode, text: SelectLinePage.selectTxt, ldate: SelectLinePage.selectLtime, rdate: SelectLinePage.selectRtime, hide: '<%=Request.QueryString["hide"] %>', show: '<%=Request.QueryString["show"] %>', tourID: SelectLinePage.selectTourID };
                var callBackFun = '<%=Request.QueryString["callBackFun"] %>'
                if (callBackFun.indexOf('.') == -1) {
                    SelectLinePage.parentWindow[callBackFun](data);
                } else {
                    SelectLinePage.parentWindow[callBackFun.split('.')[0]][callBackFun.split('.')[1]](data);
                }
                parent.Boxy.getIframeDialog(SelectLinePage.iframeID).hide();
                return false;
            })
            //判断是否只能选中一项
            $("#tblList").find("input[type='checkbox']").click(function() {
                if ($("#sModel").val() != "2") {
                    $("#tblList").find("input[type='checkbox']").attr("checked", "");
                    $(this).attr("checked", "checked");
                }
            })
        })
    </script>

</body>
</html>

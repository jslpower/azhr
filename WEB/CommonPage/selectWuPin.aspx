<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectWuPin.aspx.cs" Inherits="EyouSoft.Web.CommonPage.selectWuPin" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <style type="text/css">
        table, tr, td
        {
            border: 1px solid #B8C5CE;
            border-collapse: collapse;
        }
        label
        {
            cursor: pointer;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div>
        <div style="margin: 0 auto; width: 99%;">
            <div style="width: 100%" id="tblList">
                <table id="tblList" align="center" bgcolor="#FFFFFF" cellpadding="0" cellspacing="0"
                    width="100%" class="alertboxbk1" border="0" style="border-collapse: collapse;
                    margin: 5px 0;">
                    <tr>
                        <asp:Repeater ID="rpt_WuPinList" runat="server">
                            <ItemTemplate>
                                <td align="left" height="28">
                                    &nbsp;&nbsp;
                                    <input type="hidden" name="hidpriceid" value="" data-jiagejs="" data-jiageth="" />
                                    <input id="<%#Eval("WuPinId") %>" name="radiogroup" data-num='<%# Eval("ShuLiang.KuCun")%>'
                                        data-price='<%#Convert.ToDecimal(Eval("DanJia")).ToString("f2") %>' type="radio"
                                        value="<%#Eval("WuPinId") %>" data-show="<%#Eval("Name")%>" data-remark="<%#Eval("BeiZhu") %>" />
                                    <label for="<%#Eval("WuPinId") %>">
                                        <%#Eval("Name")%>
                                    </label>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, listCount, 4)%>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                        <tr>
                            <td align="right" class="alertboxTableT" colspan="4" height="23">
                                <div class="pages">
                                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                                </div>
                            </td>
                        </tr>
                </table>
            </div>
            <div class="alertbox-btn">
                <a id="a_btn" hidefocus="true" href="javascript:void(0)"><s class="xuanzhe"></s>选 择</a></div>
        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    $(function() {
        $("#a_btn").click(function() {
            iPage.setValue();
            iPage.selectValue();
            return false;
        })
        //初始化选中                   
        var data = {
            id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("hideID") %>'
        }
        //选中单选钮
        $(":radio[value='" + data.id + "']").attr("checked", "checked");
    })
    var iPage = {
        wupin: { price: "", name: "", id: "", Num: "", remark: "" },
        setValue: function() {
            $("#tblList").find("input[type='radio']:checked").each(function() {
                iPage.wupin.price = $(this).attr("data-price");
                iPage.wupin.name = $(this).attr("data-show");
                iPage.wupin.id = $(this).val();
                iPage.wupin.Num = $(this).attr("data-num");
                iPage.wupin.remark = $(this).attr("data-remark");
            })
        },
        selectValue: function() {
            var data = {
                callBack: Boxy.queryString("callBack"),
                pIframeID: '<%=Request.QueryString["pIframeID"] %>'
            }

            var args = {
                aid: '<%=Request.QueryString["aid"] %>',
                wupin: iPage.wupin,
                remark: iPage.remark
            }

            //根据父级是否为弹窗传值
            if (data.pIframeID != "" && data.pIframeID.length > 0) {
                //定义父级弹窗
                var boxyParent = window.parent.Boxy.getIframeWindow(data.pIframeID) || window.parent.Boxy.getIframeWindowByID(data.pIframeID);
                //判断是否存在回调方法
                if (data.callBack != null && data.callBack.length > 0) {
                    if (data.callBack.indexOf('.') == -1) {
                        boxyParent[data.callBack](args);
                    }
                    else {
                        boxyParent[data.callBack.split('.')[0]][data.callBack.split('.')[1]](args);
                    }
                }
                //定义回调
            }
            else {
                //判断是否存在回调方法
                if (data.callBack != null && data.callBack.length > 0) {
                    if (data.callBack.indexOf('.') == -1) {
                        window.parent[data.callBack](args);
                    }
                    else {
                        window.parent[data.callBack.split('.')[0]][data.callBack.split('.')[1]](args);
                    }
                }
                //定义回调
            }
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        }
    };
</script>


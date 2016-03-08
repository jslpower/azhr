<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectCaiDan.aspx.cs" Inherits="EyouSoft.Web.CommonPage.selectCaiDan" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/bt.min.js"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02 t1">选择菜单 </span>
            <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
                id="tblList" style="margin: 0 auto">
                <tr>
                    <asp:Repeater runat="server" ID="RepList">
                        <ItemTemplate>
                            <td align="left" height="30px" width="25%">
                                <input name="1" type="radio" value="<%#Eval("CaiDanId") %>" data-pricejs='<%#Convert.ToDecimal(Eval("JiaGeRJS")).ToString("f2") %>'
                                    data-priceth='<%#Convert.ToDecimal(Eval("JiaGeRTH")).ToString("f2") %>' data-show="<%#Eval("Name")%>" data-gysid="<%#Eval("GysId")%>"
                                    <%#Eval("CaiDanId")==Request.QueryString["CaidanId"]?"checked=checked":"" %> />
                                <a href="javascript:void(0);">
                                    <%#Eval("Name")%></a>
                                <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex,recordCount,4) %>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal runat="server" ID="lbemptymsg"></asp:Literal>
                    <tr>
                        <td height="23" align="right" class="alertboxTableT" colspan="5">
                            <div style="position: relative; height: 32px;">
                                <div class="pages" id="div_AjaxPage">
                                    <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                                </div>
                            </div>
                        </td>
                    </tr>
            </table>
            <div class="alertbox-btn">
                <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
        </div>
    </div>
    </form>
</body>
</html>

<script type="text/javascript">
    $(function() {
        //CaidanId
        var data = {
            caidanid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("CaidanId") %>'
        }
        //选中单选钮
        $(":radio[value='" + data.caidanid + "']").attr("checked", "checked");
        $("#a_btn").click(function() {
            SelectPage.SetValue();
            SelectPage.SelectValue();
        })
    })
    var SelectPage = {
        gysid: "",
        caidanid: "",
        caidanname: "",
        pricejs: "",
        pricesell: "",
        parentiframeid: '<%=Request.QueryString["parentiframeid"] %>',
        SetValue: function() {
            $("#tblList").find("input[type='radio']:checked").each(function() {
                SelectPage.gysid = $(this).attr("data-gysid");
                SelectPage.caidanid = $(this).val();
                SelectPage.caidanname = $(this).attr("data-show");
                SelectPage.pricejs = $(this).attr("data-pricejs");
                SelectPage.pricesell = $(this).attr("data-priceth");
            })
        },
        SelectValue: function() {
            var data = {
                callBack: Boxy.queryString("callBack"),
                hideID: Boxy.queryString("hideID"),
                iframeID: Boxy.queryString("iframeId"),
                pIframeID: '<%=Request.QueryString["parentiframeid"] %>',
                caidanid: Boxy.queryString("CaidanId")
            }

            var args = {
                gysid: SelectPage.gysid,
                caidanid: SelectPage.caidanid,
                name: SelectPage.caidanname,
                pricejs: SelectPage.pricejs,
                pricesell: SelectPage.pricesell
            }

            var boxyParent = window.parent.Boxy.getIframeWindow(data.pIframeID) || window.parent.Boxy.getIframeWindowByID(data.pIframeID);
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
                        boxyParent[data.callBack](args);
                    }
                    else {
                        boxyParent[data.callBack.split('.')[0]][data.callBack.split('.')[1]](args);
                    }
                }
                //定义回调
            }
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        }
    }
</script>


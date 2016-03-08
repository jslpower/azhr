<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectRoute.aspx.cs" Inherits="EyouSoft.Web.CommonPage.SelectRoute" %>

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
        .bumenbox
        {
            line-height: 200%;
        }
        .bumenbox a
        {
            color: #000000;
        }
        .bumenbox a:hover
        {
            color: #FF0000;
        }
        .bottomline
        {
            border-bottom: 1px #FFFFFF solid;
            border-collapse: collapse;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk2"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td align="center">
                    <span style="font-size: 14px;">【选择线路】</span>
                </td>
            </tr>
            <tr>
                <td bgcolor="#B7E0F3" class="bumenbox">
                    <b><a href="/CommonPage/SelectRoute.aspx?aid=<%=Request.QueryString["aid"]%>&sl=<%=Request.QueryString["sl"]%>&callBackFun=<%=Request.QueryString["callBack"]%>&iframeId=<%=Request.QueryString["iframeId"]%>&type=<%=Request.QueryString["type"] %>">
                        所有线路区域</a></b>：<asp:Repeater ID="rptAreaList" runat="server">
                            <ItemTemplate>
                                <a href="/CommonPage/SelectRoute.aspx?areaID=<%#Eval("AreaId") %>&aid=<%=Request.QueryString["aid"]%>&sl=<%=Request.QueryString["sl"]%>&callBack=<%=Request.QueryString["callBack"]%>&iframeId=<%=Request.QueryString["iframeId"]%>&type=<%=Request.QueryString["type"] %>">
                                    <%#Eval("AreaName")%></a> |
                            </ItemTemplate>
                        </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>
                    <div class="fixed" style="width: 98%;" id="divSearch">
                        <form method="get" action="/CommonPage/SelectRoute.aspx">
                        线路名称：<input type="text" name="txtRouteName" class="formsize80 bk" value="<%=Request.QueryString["txtRouteName"]%>" />
                        <button type="submit" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                            border: 0 none; margin-left: 5px;">
                            查 询</button>
                        <input type="hidden" name="aid" value="<%=Request.QueryString["aid"]%>" />
                        <input type="hidden" name="areaId" value="<%=Request.QueryString["areaId"]%>" />
                        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"]%>" />
                        <input type="hidden" name="callBack" value="<%=Request.QueryString["callBack"]%>" />
                        <input type="hidden" name="type" value="<%=Request.QueryString["type"] %>" />
                        </form>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="tblList" width="100%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk1"
                        bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                        <tr>
                            <asp:Repeater ID="rpt_List" runat="server">
                                <ItemTemplate>
                                    <td align="left">
                                        <label>
                                            <input type="radio" name="radioValue" value="<%#Eval("TourId") %>" />
                                            <span>
                                                <%#Eval("RouteName")%></span></label>
                                    </td>
                                    <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, recordCount, 4)%>
                                </ItemTemplate>
                            </asp:Repeater>
                    </table>
                </td>
            </tr>
        </table>
        <div style="position: relative; height: 20px;">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" id="xuanzhe" hidefocus="true"><s class="xuanzhe"></s>选择线路</a></div>
    </div>

    <script type="text/javascript">
        var SelectLine = {
            Data: {
                aid: Boxy.queryString("aid"),
                callBack: Boxy.queryString("callBack"),
                pIframeID: '<%=Request.QueryString["pIframeID"] %>'
            },
            getXianZhongValues: function() {
                var items = { id: "", name: "", areaId: "" };
                $("#tblList").find("input[type='radio']:checked").each(function() {
                    var _$chk = $(this);
                    items = { id: _$chk.val(), name: _$chk.next().html(), areaId: $("#areaId").val() };
                });
                return items;
            },
            loading: function() {
                $("#xuanzhe").css({ color: "#999" }).unbind("click");
            },
            xuanZe_Click: function(obj) {
                var items = this.getXianZhongValues();

                if (items && items.id == "") {
                    parent.tableToolbar._showMsg("未选择任何线路信息");
                    return;
                }
                if (items && items.id != "") {
                    SelectLine.loading();
                    parent.window[SelectLine.Data.callBack](items);
                }

                parent.Boxy.getIframeDialog(SelectLine.Data.iframeid).hide();
            }
        }
        $(document).ready(function() {
            $("#xuanzhe").click(function() { SelectLine.xuanZe_Click(this); });
        });
    </script>

</body>
</html>

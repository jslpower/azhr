<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiuDianJiaGe.aspx.cs" Inherits="EyouSoft.Web.Gys.JiuDianJiaGe"
    MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                id="liststyle">
                <tr>
                    <td width="25" rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        编号
                    </td>
                    <td height="23" rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        类型
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        日期
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        房型
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        含早
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        早餐价格
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        门市价
                    </td>
                    <td colspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        团队
                    </td>
                    <td colspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        散客
                    </td>
                    <td width="105" rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        结算价
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        服务费
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        结算价
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        服务费
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr i_jiageid="<%#Eval("JiaGeId") %>">
                            <td align="center">
                                <%# Container.ItemIndex + 1%>
                            </td>
                            <td align="center">
                                <%#Eval("BinKeLeiXing")%>
                            </td>
                            <td align="center">
                                <%#Eval("STime","{0:yyyy-MM-dd}")%>-<%#Eval("ETime","{0:yyyy-MM-dd}")%>
                            </td>
                            <td align="center">
                                <%#Eval("FangXingName")%>
                            </td>
                            <td align="center">
                                <%#(bool)Eval("IsHanZao")?"含":"不含"%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeZC", "{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeMS", "{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeTJS", "{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeTFW", "{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeSJS", "{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeSFW", "{0:F2}")%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" class="i_update">
                                    <img src="/images/updateimg.gif" width="48" height="20" /></a> <a href="javascript:void(0)"
                                        class="i_delete">
                                        <img src="/images/delimg.gif" width="48" height="20" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="20">
                            暂无价格信息
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="alertbox-btn">
            <a id="i_a_insert" style="text-indent: 3px; color: #CC0033" hidefocus="true" href="javascript:void(0)">
                <b>+添加</b></a>
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            insert: function() {
                var _data = { sl: "<%=SL %>", gysid: "<%=GysId %>" };
                top.Boxy.iframeDialog({ title: "新增酒店价格信息", iframeUrl: "jiudianjiageedit.aspx", width: "700px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            update: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "<%=SL %>", gysid: "<%=GysId %>", jiageid: _$tr.attr("i_jiageid") };
                top.Boxy.iframeDialog({ title: "修改酒店价格信息", iframeUrl: "jiudianjiageedit.aspx", width: "700px", height: "400px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            del: function(obj) {
                var _$tr = $(obj).closest("tr");
                if (!confirm("价格信息删除后不可恢复，你确定要删除吗？")) return;

                $.ajax({
                    type: "GET", url: window.location.href + "&dotype=delete&jiageid=" + _$tr.attr("i_jiageid"), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.reload();
                        } else {
                            alert(response.msg);
                            iPage.reload();
                        }
                    },
                    error: function() {
                        iPage.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            tableToolbar.init({});
            $("#i_a_insert").click(function() { iPage.insert(); });
            $(".i_update").click(function() { iPage.update(this); });
            $(".i_delete").click(function() { iPage.del(this); });
        });
    </script>

</asp:Content>

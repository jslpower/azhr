<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CanGuanCaiDan.aspx.cs"
    Inherits="EyouSoft.Web.Gys.CanGuanCaiDan" MasterPageFile="~/MasterPage/Boxy.Master" %>

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
                        菜单名称
                    </td>
                    <td rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        菜单内容
                    </td>
                    <td colspan="3" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        价格/桌
                    </td>
                    <td colspan="3" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        价格/人
                    </td>
                    <td height="23" rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        是否可用
                    </td>
                    <td width="105" rowspan="2" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        门市
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        同行
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        结算
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        门市
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        同行
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT" style="width: 45px;">
                        结算
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr i_caidanid="<%#Eval("CaiDanId") %>">
                            <td align="center">
                                <%# Container.ItemIndex + 1%>
                            </td>
                            <td align="center">
                                <%#Eval("Name") %>
                            </td>
                            <td align="center">
                                <%#Eval("NeiRong") %>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeZMS","{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeZTH","{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeZJS","{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeRMS","{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeRTH","{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Eval("JiaGeRJS","{0:F2}")%>
                            </td>
                            <td align="center">
                                <%#Convert.ToBoolean(Eval("IsDisplay").ToString())==true?"不可用":"可用"%>
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
                        <td colspan="12">
                            暂无菜单信息
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
                top.Boxy.iframeDialog({ title: "新增菜单", iframeUrl: "canguancaidanedit.aspx", width: "520px", height: "300px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            update: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "<%=SL %>", gysid: "<%=GysId %>", caidanid: _$tr.attr("i_caidanid") };
                top.Boxy.iframeDialog({ title: "修改菜单", iframeUrl: "canguancaidanedit.aspx", width: "520px", height: "300px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            del: function(obj) {
                var _$tr = $(obj).closest("tr");
                if (!confirm("菜单删除后不可恢复，你确定要删除吗？")) return;

                $.ajax({
                    type: "GET", url: window.location.href + "&dotype=delete&caidanid=" + _$tr.attr("i_caidanid"), cache: false, dataType: "json", async: false,
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

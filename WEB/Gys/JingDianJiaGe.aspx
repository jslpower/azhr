<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JingDianJiaGe.aspx.cs"
    Inherits="EyouSoft.Web.Gys.JingDianJiaGe" MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                id="liststyle">
                <tr>
                    <td width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        编号
                    </td>
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        景点名称
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        星级
                    </td>
                    <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        价格
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr i_jingdianid="<%#Eval("JingDianId") %>">
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <%# Container.ItemIndex + 1%>
                            </td>
                            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <%#Eval("Name") %>
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <%#Eval("XingJi") %>
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <a href="javascript:void(0)" class="i_jiage">价格管理</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                    <tr>
                        <td colspan="10">
                            暂无景点信息
                        </td>
                    </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="alertbox-btn">
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            jiaGe: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = { sl: "<%=SL %>", gysid: "<%=GysId %>", jingdianid: _$tr.attr("i_jingdianid") };
                top.Boxy.iframeDialog({ title: "价格管理", iframeUrl: "JingDianJiaGeEdit.aspx", width: "800px", height: "300px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            }
        };

        $(document).ready(function() {
            tableToolbar.init({});
            $(".i_jiage").click(function() { iPage.jiaGe(this); });
        });
    </script>

</asp:Content>

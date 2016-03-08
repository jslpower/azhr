<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WuPinJieYueXX.aspx.cs"
    Inherits="EyouSoft.Web.Gys.WuPinJieYueXX" MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <div class="alertbox-outbox">
        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
            style="margin: 0 auto" id="liststyle">
            <tr>
                <td width="5%" height="23" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    序号
                </td>
                <td width="10%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    借阅时间
                </td>
                <td width="9%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    借阅人
                </td>
                <td width="8%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    数量
                </td>
                <td align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    用途说明
                </td>
                <td width="15%" align="center" bgcolor="#b7e0f3" class="alertboxTableT">
                    状态
                </td>
            </tr>
            <asp:Repeater runat="server" ID="rpt">
                <ItemTemplate>
                    <tr i_jieyueid="<%#Eval("LingYongId") %>">
                        <td align="center">
                            <%# Container.ItemIndex + 1%>
                        </td>
                        <td align="center">
                            <%#Eval("ShiJian","{0:yyyy-MM-dd}")%>
                        </td>
                        <td align="center">
                            <%#Eval("LingYongRenName")%>
                        </td>
                        <td align="center">
                            <%#Eval("ShuLiang")%>
                        </td>
                        <td align="left">
                            <%#Eval("YongTu")%>
                        </td>
                        <td align="center">
                            <%#GetStatus(Eval("JieYueStatus"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                <tr>
                    <td colspan="20">
                        暂无借阅信息
                    </td>
                </tr>
            </asp:PlaceHolder>
        </table>
    </div>
    
    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            guiHuan: function(obj) {
                var _$tr = $(obj).closest("tr");
                if (!confirm("归还操作不可逆，你确定要归还吗？")) return;
                $.ajax({
                    type: "POST", url: window.location.href + "&doType=guihuan&jieyueid=" + _$tr.attr("i_jieyueid"), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        alert(response.msg);
                        iPage.reload();
                    },
                    error: function() {
                        iPage.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            $(".i_guihuan").click(function() { iPage.guiHuan(this); });
        });
    </script>
</asp:Content>

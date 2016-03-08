<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheDuiCheXing.aspx.cs"
    Inherits="EyouSoft.Web.Gys.CheDuiCheXing" MasterPageFile="~/MasterPage/Boxy.Master" %>

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
                        车型
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        车型照片
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        座位数
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        单价基数
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        备注
                    </td>
                    <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        价格
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rpt"><ItemTemplate>
                <tr i_chexingid="<%#Eval("CheXingId") %>">
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <%# Container.ItemIndex + 1%>
                    </td>
                    <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <%#Eval("Name") %>
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <a href="<%#Eval("FuJian.FilePath") %>" target="_blank" class="i_fujian">查看</a>
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <%#Eval("ZuoWeiShu") %>
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <%#Eval("DanJiaJiShu","{0:F2}") %>
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <%#Eval("BeiZhu") %>
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
                            暂无车型信息
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
                var _data = { sl: "<%=SL %>", gysid: "<%=GysId %>", chexingid: _$tr.attr("i_chexingid") };
                top.Boxy.iframeDialog({ title: "价格管理", iframeUrl: "CheDuiCheXingJiaGe.aspx", width: "600px", height: "300px", data: _data, afterHide: function() { iPage.reload(); } });
                return false;
            },
            fuJian: function(obj) {
                var _$obj = $(obj);
                if ($.trim(_$obj.attr("href")).length == 0) {
                    _$obj.attr("href", "javascript:void(0)").removeAttr("target").text("暂无附件");
                }
            }
        };

        $(document).ready(function() {
            tableToolbar.init({});
            $(".i_jiage").click(function() { iPage.jiaGe(this); });
            $(".i_fujian").each(function() { iPage.fuJian(this); });
        });
    </script>
</asp:Content>

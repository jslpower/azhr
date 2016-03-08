<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="ZhiFuFangShi.aspx.cs" Inherits="Web.Sys.ZhiFuFangShi" %>

<%@ Register Src="../UserControl/BaseBar.ascx" TagName="BaseBar" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox">
        <uc1:BaseBar ID="BaseBar1" runat="server" />
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="add_xlqy" hidefocus="true" href="javascript:"><span>
                    添加</span></a></li><li class="line"></li>
            </ul>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th>
                            编号
                        </th>
                        <th align="center" class="th-line">
                            支付方式
                        </th>
                        <th align="center" class="th-line">
                            支付类别
                        </th>
                        <th align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <cc1:CustomRepeater ID="repList" runat="server">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <%#Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <%#Eval("Name")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ItemType")%>
                                </td>
                                <td align="center">
                                    <a class="update_zffs" href="ZhiFuFangShiBJ.aspx?id=<%#Eval("PaymentId") %>&sl=<%=SL%>&memuid=<%=memuid%>">
                                        <%#Convert.ToBoolean(Eval("IsSystem")) ? "":"修改&nbsp;|"%></a><a href="ZhiFuFangShi.aspx?ids=<%#Eval("PaymentId") %>&sl=<%=SL %>&state=del&memuid=<%=memuid %>"
                                            onclick=" return PayStyleList.Del(this);">
                                            <%#Convert.ToBoolean(Eval("IsSystem")) ?"":"&nbsp;删除"%></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </cc1:CustomRepeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead">
            <ul class="fixed">
                <li><s class="addicon"></s><a class="add_xlqy" hidefocus="true" href="javascript:"><span>
                    添加</span></a></li><li class="line"></li>
            </ul>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var PayStyleList = {
            Del: function(obj) {
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: obj.href,
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() { parent.window.location.href = '/Sys/ZhiFuFangShi.aspx?sl=' + querystring(location.href, "sl") + "&memuid=" + querystring(location.href, "memuid") });
                        } else {
                            parent.tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg("操作失败，请稍后再试!");
                    }
                });
                return false;
            }
        }
        $(function() {
            $(".add_xlqy").click(function() {
                var url = "ZhiFuFangShiBJ.aspx?sl=<%=SL%>&memuid=<%=memuid%>";
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "添加",
                    modal: true,
                    width: "600px",
                    height: "180px"
                });
                return false;
            });
            $(".update_zffs").click(function() {
                var url = $(this).attr("href");
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "修改",
                    modal: true,
                    width: "600px",
                    height: "180px"
                });
                return false;
            });
        })
    </script>

</asp:Content>

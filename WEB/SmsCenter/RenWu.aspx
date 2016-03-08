<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RenWu.aspx.cs" Inherits="EyouSoft.Web.SmsCenter.RenWu"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SysTop.ascx" TagName="SysTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="CPHBODY" runat="server">
    <div class="mainbox">
        <uc1:SysTop ID="SysTop1" runat="Server"></uc1:SysTop>
        <div class="searchbox fixed">
            <form action="" method="get">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" value="<%=SL %>" />
                    任务类型：
                    <select name="txtRenWuLeiXing" id="txtRenWuLeiXing">
                        <%=UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SmsStructure.RenWuLeiXing),new string[]{"0"}),"","","请选择") %>
                    </select>
                    任务发起人：
                    <input type="text" class="formsize80 input-txt" name="txtFaQiRenName" id="txtFaQiRenName" />
                    
                    接收人：
                    <input type="text" class="formsize80 input-txt" name="txtJieShouRenName" id="txtJieShouRenName" />
                    状态：
                    <select name="txtJieShouStatus" id="txtJieShouStatus">
                        <%=UtilsCommons.GetEnumDDL(EnumObj.GetList(typeof(EyouSoft.Model.EnumType.SmsStructure.RenWuJieShouStatus)), "", "", "请选择")%>
                    </select>
                    <button type="sbumit" class="search-btn">
                        搜索</button></p>
            </span>
            </form>
            <div class="pages">
                
            </div>
        </div>
        <div class="tablelist-box">
            <table border="0" cellspacing="0" id="liststyle" width="100%">
                <tr>
                    <th width="30" class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" valign="middle">
                        任务类型
                    </th>
                    <th align="center" valign="middle">
                        任务发起人
                    </th>
                    <th align="center" valign="middle">
                        接收部门
                    </th>
                    <th align="center" valign="middle">
                        接收人
                    </th>
                    <th align="center" valign="middle">
                        接收时间
                    </th>
                    <th align="center" valign="middle">
                        短信内容
                    </th>
                    <th align="center" valign="middle">
                        状态
                    </th>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                <ItemTemplate>
                <tr i_renwuid="<%#Eval("RenWuId") %>">
                    <td align="center">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </td>
                    <td align="center">
                        <%#Eval("LeiXing")%>
                    </td>
                    <td align="center">
                        <%#Eval("FaQiRenName")%>
                    </td>
                    <td align="center">
                        <%#Eval("JieShouRenDeptName")%>
                    </td>
                    <td align="center">
                        <%#Eval("JieShouRenName")%>
                    </td>
                    <td align="center">
                        <%#Eval("JieShouTime","{0:yyyy-MM-dd HH:mm}")%>
                    </td>
                    <td align="center">
                        <%#Eval("NeiRong")%>
                    </td>
                    <td align="center">
                        <%#Eval("Status")%>
                    </td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
                <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                <tr>
                    <td colspan="12">
                        暂无短信任务信息
                    </td>
                </tr>
                </asp:PlaceHolder>
            </table>
        </div>
        <div class="tablehead border-bot">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
    </div>
    
    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            jieShou: function(obj) {
                if (!confirm("你确定要接收吗？")) return false;
                var _$tr = $(obj).closest("tr");

                $.ajax({
                    type: "GET", url: "RenWu.aspx?sl=<%=SL %>&doType=JieShou&renwuid=" + _$tr.attr("i_renwuid"), cache: false, dataType: "json", async: false,
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
                        alert("请求异常");
                        iPage.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            utilsUri.initSearch();
            tableToolbar.init();
            $(".i_jieshou").click(function() { iPage.jieShou(this); });
        });
    </script>
</asp:Content>

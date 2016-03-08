<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="NoticeList.aspx.cs" Inherits="Web.UserCenter.Notice.NoticeList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <div class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" cellspacing="0" cellpadding="0" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center" class="th-line">
                            标题
                        </th>
                        <th align="center" class="th-line">
                            发布人
                        </th>
                        <th align="center" class="th-line">
                            发布时间
                        </th>
                        <th align="center" class="th-line">
                            浏览数
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="odd">
                            <input type="hidden" name="ItemUserID" />
                                <td align="center">
                                    <input type="checkbox" id="chbID" name="chbID" value="<%# Eval("NoticeId")%>">
                                </td>
                                <td align="center">
                                    <a class="ck_jl" href="javascript:void(0)" id="noticeTitle" onclick="BindBoxy('<%# Eval("NoticeId")%>')">
                                        <%# Eval("Title")%></a>
                                        <%# GetUrl(Eval("NoticeId").ToString())%>
                                </td>
                                <td align="center">
                                    <%# Eval("Operator")%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Views")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0;" class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function BindBoxy(parame)
        {
            Boxy.iframeDialog({
                iframeUrl: "NoticeInfo.aspx?sl=<%=this.SL %>&id="+parame,
                title: "公告查看",
                modal: true,
                width: "600px",
                height: "370px"
            });
            return false;
        }
    </script>

    </form>
</asp:Content>

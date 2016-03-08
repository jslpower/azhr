<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="BaoJiaBeiZhu.aspx.cs" Inherits="EyouSoft.Web.Sys.BaoJiaBeiZhu" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/BaseBar.ascx" TagName="BaseBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox">
        <uc1:BaseBar ID="BaseBar1" runat="server" />
        <div class="tablehead" id="pageHead">
            <input type="hidden" id="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" name="a_add" hidefocus="true" href="javascript:;">
                    <span>添加</span></a></li>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center">
                            语种
                        </th>
                        <th align="center" width="800">
                            行程备注
                        </th>
                        <th align="center">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <input type="checkbox" id="<%#Eval("Id") %>" data-lng="<%# (int)Eval("LngType")%>"
                                        data-master="<%#Eval("MasterId")%>" name="chk_ids">
                                </td>
                                <td align="center">
                                    <%#Eval("LngType").ToString()%>
                                </td>
                                <td align="center">
                                    <%#Eval("BackMark")%>
                                </td>
                                <td align="center">
                                    <a href="javascript:;" name="a_edit">修改</a> | <a href="javascript:;" name="a_del">删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="tablehead border-bot">

            <script type="text/javascript">
                document.write(document.getElementById('pageHead').innerHTML)
            </script>

        </div>
    </div>

    <script type="text/javascript">
        var pageOption = {
            openXLwindow: function(url, title, width, height) {
                url = url;
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: title,
                    modal: true,
                    width: width,
                    height: height
                });
            }, //
            sl: $("#sl").val()
        };
        $(function() {
            $("[name=a_add]").click(function() {
                pageOption.openXLwindow("BaoJiaBeiZhuBJ.aspx?dotype=add&sl=" + pageOption.sl, "添加", 750, 350);
            }); //添加
            $("a[name=a_edit]").each(function() {
                $(this).click(function() {
                    var chk = $(this).closest("tr").find("[name=chk_ids]");
                    var url = "BaoJiaBeiZhuBJ.aspx?dotype=update&id=" + chk.attr("id") + "&lngType=" + chk.attr("data-lng") + "&master=" + chk.attr("data-master");
                    pageOption.openXLwindow(url, "修改", 750, 350);
                })
            }); //修改
            $("a[name=a_del]").each(function() {
                $(this).click(function() {
                    $.newAjax({
                        type: "get",
                        cache: false,
                        url: "/Sys/BaoJiaBeiZhu.aspx?dotype=del&id=" + $(this).closest("tr").find("[name=chk_ids]").attr("id") + "&sl=" + pageOption.sl,
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "1") {
                                tableToolbar._showMsg(ret.msg, function() {
                                    window.location.href = window.location.href;
                                });
                            } else {
                                tableToolbar._showMsg(ret.msg);
                            }
                        },
                        error: function() {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    }); //ajax
                });
            }); //删除
        })
    </script>

    </form>
</asp:Content>

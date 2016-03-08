<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="FangXingGuanLi.aspx.cs" Inherits="EyouSoft.Web.Sys.FangXingGuanLi" %>

<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/BaseBar.ascx" TagName="BaseBar" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <uc1:BaseBar ID="BaseBar1" runat="server" />
        <div class="tablehead" id="pageHead">
            <input type="hidden" id="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <ul class="fixed">
                <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="javascript:;">
                    <span>添加</span></a></li>
                <li class="line"></li>
                <%--<li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:;">
                    <span>修改</span></a></li>
                <li class="line"></li>--%>
                <%--    <li><s class="delicon"></s><a class="toolbar_delete" hidefocus="true" href="javascript:;">
                    <span>删除</span></a></li>--%>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="tablelist-box" >
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center">
                            房型
                        </th>
                        <th align="center" width="200">
                            操作
                        </th>
                    </tr>
                    <cc2:CustomRepeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <tr class="">
                                <td align="center">
                                    <input type="checkbox" id="<%# Eval("RoomId")%>" name="chk_id">
                                </td>
                                <td align="center">
                                    <%# Eval("TypeName")%>
                                </td>
                                <td align="center">
                                    <a href="javascript:;" name="a_edit">修改</a> | <a href="javascript:;" name="a_del">删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </cc2:CustomRepeater>
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
            $(".toolbar_add").click(function() {
                pageOption.openXLwindow("FangXingGuanLiBJ.aspx?dotype=add&sl=" + pageOption.sl, "添加", 420, 180);
            }); //添加
            $("[name=a_edit]").click(function() {
                var chk = $(this).closest("tr").find("[name=chk_id]");
                var url = "FangXingGuanLiBJ.aspx?dotype=update&id=" + chk.attr("id") + "&sl=" + pageOption.sl;
                pageOption.openXLwindow(url, "修改", 420, 180);
            }); //修改
            $("[name=a_del]").click(function() {
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/Sys/FangXingGuanLi.aspx?dotype=del&id=" + $(this).closest("tr").find("[name=chk_id]").attr("id") + "&sl=" + pageOption.sl,
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
            }); //删除
        })
    </script>

</asp:Content>

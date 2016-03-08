<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="DaoYouXuZhi.aspx.cs" Inherits="EyouSoft.Web.Sys.DaoYouXuZhi" %>

<%@ Register Src="../UserControl/BaseBar.ascx" TagName="BaseBar" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                            <input type="checkbox" id="checkbox" name="chk_all">
                        </th>
                        <th align="center">
                            部门
                        </th>
                        <th align="center" width="800">
                            导游需知
                        </th>
                        <th align="center">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <input type="checkbox" id="<%#Eval("Id") %>" name="chk_ids">
                                </td>
                                <td align="center">
                                    <%#getDeptName(Eval("DepartId").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("KnowMark")%>
                                </td>
                                <td align="center">
                                    <a href="javascript:;" name="a_edit">修改</a> | <a href="javascript:;" onclick="return pageOption.del('<%#Eval("Id") %>')">
                                        删除</a>
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
            GetSelectItemValue: function() {
                var arrayList = new Array();
                $("#liststyle").find("input[name='chk_ids']").each(function() {
                    if ($(this).attr("checked") == true) {
                        arrayList.push($(this).attr("id"));
                    }
                });
                return arrayList;
            },
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
            sl: $("#sl").val(),
            del: function(id) {
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/Sys/DaoYouXuZhi.aspx?dotype=del&id=" + id + "&sl=" + pageOption.sl,
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
            }
        };
        $(function() {
            $("[name=a_add]").click(function() {
                pageOption.openXLwindow("DaoYouXuZhiBJ.aspx?&sl=" + pageOption.sl, "添加", 750, 350);
            }); //添加
            $("a[name=a_edit]").each(function() {
                $(this).click(function() {
                    var ids = $(this).closest("tr").find("[name=chk_ids]").attr("id");
                    var url = "DaoYouXuZhiBJ.aspx?id=" + ids + "&sl=" + pageOption.sl;
                    pageOption.openXLwindow(url, "修改", 750, 350);
                })
            }); //修改
            //            $("a[name=a_del]").each(function() {
            //                $(this).click(function() {
            //                    pageOption.del(pageOption.GetSelectItemValue().toString())
            //                });
            //            }); //批量删除
        })
    </script>

</asp:Content>

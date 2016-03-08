<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="XingChengLiangDian.aspx.cs" Inherits="EyouSoft.Web.Sys.XingChengLiangDian" %>

<%@ Register Src="../UserControl/BaseBar.ascx" TagName="BaseBar" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <uc1:BaseBar ID="BaseBar1" runat="server" />
        <div class="tablehead" id="pageHead">
            <input type="hidden" id="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <ul class="fixed">
                <li><s class="addicon"></s><a name="add" class="toolbar_add" hidefocus="true" href="javascript:;">
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
                        <th align="center">
                            部门
                        </th>
                        <th align="center">
                            线路区域
                        </th>
                        <th align="center" width="800">
                            行程亮点
                        </th>
                        <th align="center">
                            操作
                        </th>
                    </tr>
                    <cc2:CustomRepeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <input type="checkbox" id="checkbox" name="chk_id" value="<%# Eval("Id")%>">
                                </td>
                                <td align="center">
                                    <%# Eval("LngType").ToString()%>
                                </td>
                                <td align="center">
                                    <%#getComName(Eval("DeptID").ToString())%>
                                </td>
                                <td align="center">
                                    <%# getAreaName(Eval("AreaID").ToString())%>
                                </td>
                                <td align="center">
                                    <%# Eval("JourneySpot")%>
                                </td>
                                <td align="center">
                                    <a data-lng="<%# (int)Eval("LngType")%>" data-master="<%#Eval("MasterId")%>" class="toolbar_update"
                                        name="update" href="javascript:;">修改</a> | <a id="<%# Eval("Id")%>" name="del" href="javascript:;">
                                            删除</a>
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
        var pageOpt = {
            openXLwindow: function(url, title, width, height) {
                url = url;
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: title,
                    modal: true,
                    width: width,
                    height: height
                });
            }, //dialog
            ids: function() {
                var arrayList = new Array();
                $("#liststyle").find("[name=chk_id]:checked").each(function() {
                    arrayList.push($(this).val());
                })
                return arrayList;
            }, //ids
            sl: $("#sl").val()
        };


        $(function() {
            $("a[name=add]").click(function() {
                var url = "XingChengLiangDianBJ.aspx?dotype=add&sl=" + pageOpt.sl;
                pageOpt.openXLwindow(url, "添加", 640, 340);

            }); //添加
            $("a[name=update]").click(function() {
                var url = "XingChengLiangDianBJ.aspx?dotype=update&id=" + $(this).closest("tr").find("[name=chk_id]").val() + "&LngType=" + $(this).attr("data-lng") + "&master=" + $(this).attr("data-master");

                pageOpt.openXLwindow(url, "修改", 640, 340);

            }); //修改
            $("a[name=del]").click(function() {
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/Sys/XingChengLiangDian.aspx?state=del&id=" + $(this).attr("id") + "&sl=" + pageOpt.sl,
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
        });
    </script>

</asp:Content>

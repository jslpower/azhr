<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="XianLuQuYu.aspx.cs" Inherits="EyouSoft.Web.Sys.XianLuQuYu" %>

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
                <li><s class="addicon"></s><a id="a_add" name="a_add" class="toolbar_add" hidefocus="true"
                    href="javascript:;"><span>添加</span></a></li>
                <li class="line"></li>
                <li><s class="delicon"></s><a id="a_del" name="a_del" class="toolbar_delete" hidefocus="true"
                    href="javascript:;"><span>删除</span></a></li>
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
                        <th width="190" align="center">
                            语种
                        </th>
                        <th width="249" align="center">
                            分公司名称
                        </th>
                        <th width="190" align="center">
                            区域名称
                        </th>
                        <th width="328" align="center">
                            操作
                        </th>
                    </tr>
                    <cc2:CustomRepeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <input type="checkbox" id="checkbox" name="chk_id" value="<%#Eval("AreaId")%>" />
                                </td>
                                <td align="center">
                                    <%#Eval("LngType").ToString()%>
                                </td>
                                <td align="center">
                                    <%# getComName(Eval("ChildCompanyId").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("AreaName")%>
                                </td>
                                <td align="center">
                                    <a data-lng="<%# (int)Eval("LngType")%>" data-master="<%#Eval("MasterId")%>" class="toolbar_update"
                                        name="update" href="javascript:;">修改</a> | <a href="javascript:;" onclick="return pageOpt.del('<%# (int)Eval("AreaId")%>');">
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
            sl: $("#sl").val(),
            del: function(id) {
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/Sys/XianLuQuYu.aspx?state=del&ids=" + id + "&sl=" + pageOpt.sl,
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
            $("a[name=a_add]").click(function() {
                var url = "XianLuQuYuBJ.aspx?dotype=add&sl=" + pageOpt.sl;
                pageOpt.openXLwindow(url, "添加", 540, 240);

            }); //添加
            $("a[name=update]").click(function() {
                var url = "XianLuQuYuBJ.aspx?dotype=update&id=" + $(this).closest("tr").find("[name=chk_id]").val() + "&LngType=" + $(this).attr("data-lng") + "&master=" + $(this).attr("data-master") + "&sl=" + pageOpt.sl;

                pageOpt.openXLwindow(url, "修改", 540, 240);

            }); //修改
            $("a[name=a_del]").click(function() {
                pageOpt.del(pageOpt.ids());
            }); //删除
        });
    </script>

</asp:Content>

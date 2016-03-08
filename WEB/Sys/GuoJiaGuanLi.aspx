<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="GuoJiaGuanLi.aspx.cs" Inherits="EyouSoft.Web.Sys.GuoJiaGuanLi" %>
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
                <li><s class="addicon"></s><a class="toolbar_add" name="a_add" hidefocus="true" href="javascript:;">
                    <span>添加</span></a></li>
                <li><s class="addicon"></s><a class="toolbar_update" name="a_edit" hidefocus="true"
                    href="javascript:;"><span>修改</span></a></li>
                <li><s class="addicon"></s><a class="toolbar_del" name="a_del" hidefocus="true" href="javascript:;">
                    <span>删除</span></a></li>
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
                            <input type="checkbox" id="chk_all" name="chk_all">
                        </th>
                        <th align="center">
                            国家
                        </th>
                    </tr>
                    <cc2:CustomRepeater ID="rpt_list" runat="server">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <input type="checkbox" value="<%#Eval("CountryId") %>" name="chk_id">
                                </td>
                                <td align="center">
                                    <%#Eval("Name")%>
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
            }, //
            sl: $("#sl").val(), //
            ids: function() {
                var arrayList = new Array();
                $("#liststyle").find("[name=chk_id]:checked").each(function() {
                    arrayList.push($(this).val());
                })
                return arrayList;
            } //
        };


        $(function() {
            $("a[name=a_add]").each(function() {
                $(this).click(function() {
                    var url = "GuoJiaGuanLiBJ.aspx?dotype=add";
                    pageOpt.openXLwindow(url, "新增", 750, 350);
                })
            }); //添加
            $("a[name=a_edit]").each(function() {
                $(this).click(function() {
                    if (pageOpt.ids().length > 1) {
                        tableToolbar._showMsg("只能选择一条记录修改！");
                        return false;
                    }
                    var url = "GuoJiaGuanLiBJ.aspx?dotype=update&id=" + pageOpt.ids().toString();
                    pageOpt.openXLwindow(url, "修改", 750, 350);
                })
            }); //修改

            $("a[name=a_del]").each(function() {
                $(this).click(function() {
                    tableToolbar.ShowConfirmMsg("确定删除选中的记录？删除后不可恢复！", function() {
                        $.newAjax({
                            type: "get",
                            cache: false,
                            url: "/Sys/GuoJiaGuanLi.aspx?dotype=del&id=" + pageOpt.ids(),
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
                    });     //选项
                })
            })

        })
        
    </script>

</asp:Content>

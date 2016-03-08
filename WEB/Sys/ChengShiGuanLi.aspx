<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="ChengShiGuanLi.aspx.cs" Inherits="EyouSoft.Web.Sys.ChengShiGuanLi" %>

<%@ Register Src="../UserControl/BaseBar.ascx" TagName="BaseBar" TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="ControlLibrary" TagPrefix="cc2" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <uc1:BaseBar ID="BaseBar1" runat="server" />
        <div class="tablehead border-bot" id="pageHead">
            <ul class="fixed">
                <li><s class="addicon"></s><a id="add_province" class="toolbar_add" hidefocus="true"
                    href="javascript:;"><span>省份添加</span></a></li>
                <li class="line"></li>
                <li><s class="addicon"></s><a id="add_city" class="toolbar_add2" hidefocus="true"
                    href="javascript:;"><span>城市添加</span></a></li>
                <li><s class="addicon"></s><a id="add_distry" class="toolbar_add2" hidefocus="true"
                    href="javascript:;"><span>城市县区</span></a></li>
                <li class="line"></li>
                <li><s class="delicon"></s><a id="del_city" class="toolbar_delete" hidefocus="true"
                    href="javascript:;"><span>删除</span></a></li>
            </ul>
        </div>
        <div class="hr_10">
        </div>
        <div class="table_city" id="liststyle">
            <%=initList()%>
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
            }, //弹窗
            ids: function() {
                var arrayIList = new Array();
                $("#liststyle").find("[name=chk_id]:checked").each(function() {
                    arrayIList.push($(this).val());
                })
                return arrayIList;
            },
            pids: function() {
                var arrayPList = new Array();
                $("#liststyle").find("[name=chk_pid]:checked").each(function() {
                    arrayPList.push($(this).val());
                })
                return arrayPList;
            },
            dids: function() {
                var arrayDList = new Array();
                $("#liststyle").find("[name=chk_did]:checked").each(function() {
                    arrayDList.push($(this).val());
                })
                return arrayDList;
            }
        };


        $(function() {
            $("#add_province").click(function() {
                var url = "ChengShiGuanLiBJ.aspx?isPro=1&isAdd=add&";
                pageOpt.openXLwindow(url, "添加省份", 430, 300);
            }); //添加省份

            $("#add_city").click(function() {
                var url = "ChengShiGuanLiBJ.aspx?isPro=2&isAdd=add&";
                pageOpt.openXLwindow(url, "添加城市", 430, 300);
            }); //添加城市

            $("#add_distry").click(function() {
                var url = "ChengShiGuanLiBJ.aspx?isPro=3&isAdd=add&";
                pageOpt.openXLwindow(url, "添加县区", 430, 300);
            }); //添加县区


            $("#del_city").each(function() {
                $(this).click(function() {
                    tableToolbar.ShowConfirmMsg("确定删除选中的记录？删除后不可恢复！", function() {
                        $.newAjax({
                            type: "get",
                            cache: false,
                            url: "/Sys/ChengShiGuanLi.aspx?dotype=del&id=" + pageOpt.ids() + "&pid=" + pageOpt.pids() + "&did=" + pageOpt.dids(),
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
                        });
                    })
                })
            }); //删除城市
        })


        $("[name=chk_pid]").each(function() {
            $(this).click(function() {
                if ($(this).attr("checked")) { $(this).parent().next("table").find("input[type=checkbox]").attr("checked", true) }
                else { $(this).parent().next("table").find("input[type=checkbox]").attr("checked", false) }
            })
        })
        $("[name=chk_id]").each(function() {
            $(this).click(function() {
                if ($(this).attr("checked")) { $(this).parent().parent().next("td").find("input[type=checkbox]").attr("checked", true) }
                else { $(this).parent().parent().next("td").find("input[type=checkbox]").attr("checked", false) }
            })
        })
    </script>

</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BuMenGuanLi.aspx.cs" Inherits="EyouSoft.Web.Sys.BuMenGuanLi"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .step_1
        {
            background-color: #C8E7F5;
        }
        .step_2
        {
            background-color: #D8EEF8;
        }
        .step_3
        {
            background-color: #E8F5FB;
        }
        .step_4
        {
            background-color: #F8F8F8;
        }
        .step_5
        {
            background: url("/images/ptjh_09.gif") repeat scroll 0 0 transparent;
        }
        .fontSize_1
        {
            font-size: 14px;
            color: #000000;
        }
        .fontSize_2
        {
            font-size: 13px;
            color: #000000;
        }
        .fontSize_3, .fontSize_4, fontSize_5, fontSize_6, fontSize_7
        {
            font-size: 12px;
            color: #000000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="gerenzx-mainbox">
        <div class="tablehead zuzjg_bg">
            <ul class="fixed">
                <li><s class="orderformicon"></s><a href="BuMenGuanLi.aspx?sl=<%=Request.QueryString["sl"]%>"
                    hidefocus="true" class="ztorderform de-ztorderform" id="a_reload"><span>部门名称</span></a></li>
            </ul>
        </div>
        <div class="zzjg_box">
            <table width="100%" height="27" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="27" valign="bottom">
                        <table width="180" height="27" border="0" align="left" cellpadding="0" cellspacing="0">
                            <tr>
                                <td width="4" background="/images/zzjg_left.gif">
                                </td>
                                <td width="172" background="/images/zzjg_mid.gif">
                                    <b class="zzjg_font15">股东会&nbsp; </b>
                                    <asp:PlaceHolder runat="server" ID="ph_Add"><a href="javascript:void(0);" class="bumen_add">
                                        [添加分公司]</a></asp:PlaceHolder>
                                </td>
                                <td width="4" background="/images/zzjg_right.gif">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div class="">
                <table width="100%">
                    <%=depStrHTML %>
                </table>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        //打开弹窗
        var DM = {
            openDialog: function(p_url, p_title, tar_a) {
                Boxy.iframeDialog({ title: p_title, iframeUrl: p_url, width: "760px", height: "410px", tar: tar_a });
            },
            delD: function(dId, tar_a, pId) {
                tableToolbar.ShowConfirmMsg("您确定要删除吗？", function() {
                    $.newAjax({
                        url: "BuMenGuanLi.aspx",
                        data: { id: dId, doType: "delete", sl: '<%=Request.QueryString["sl"]%>' },
                        dataType: "json",
                        cache: false,
                        type: "get",
                        success: function(result) {
                            if (result.result == "3") {
                                tableToolbar._showMsg(result.msg, function() {
                                    $(tar_a).closest("tr").remove();
                                    var arrTr = [];
                                    DM.delRe(dId, arrTr);
                                    for (var i in arrTr) {
                                        arrTr[i].remove();
                                    }
                                });
                            }
                            else
                            { tableToolbar._showMsg(result.msg); }
                        },
                        error: function() {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    })
                })
                return false;
            },
            //修改部门
            updateD: function(dId, tar_a) {
                DM.openDialog("BuMenGuanLiBJ.aspx?dotype=update&id=" + dId + '&sl=<%=Request.QueryString["sl"]%>', "修改部门", tar_a);
                return false;
            },
            //添加下级部门
            addD: function(dId, tar_a) {
                DM.openDialog("BuMenGuanLiBJ.aspx?doType=add&id=" + dId + '&sl=<%=Request.QueryString["sl"]%>', "添加部门");
                return false;
            },
            //添加下级部门后回调
            callbackAddD: function(pId) {
                var arrTr = [];
                DM.delRe(pId, arrTr);
                for (var i in arrTr) {
                    arrTr[i].remove();
                }
                var tar = $("#strong" + pId).parent().get(0);
                if ($(tar).prev("img").attr("src").indexOf("7") > 0) {

                    $(tar).click(function() {
                        DM.getSonD(this, pId);
                    });
                    $(tar).prev("img").click(function() {
                        DM.getSonD2(this);
                    });
                }
                DM.getSonD(tar, pId, true);
            },
            //修改部门后回调
            callbackUpdateD: function(nowId, nowName, updateP) {
                $("#strong" + nowId).html(nowName);
            },
            //点击图片
            getSonD2: function(tar) {
                $(tar).next("a").click();
                return false;
            },
            //获取子部门
            getSonD: function(tar_a, dId, back) {
                var tarObj = $(tar_a);
                var tarImg = tarObj.prev("img");
                if (tarImg.attr("src").indexOf("5") > 0 || back) {
                    tarImg.attr("src", "/images/organization_06.gif");
                }
                else if (tarImg.attr("src").indexOf("6") > 0) {
                    var arrTr = [];
                    DM.delRe(dId, arrTr);
                    for (var i in arrTr) {
                        arrTr[i].remove();
                    }
                    tarImg.attr("src", "/images/organization_05.gif");
                    return false;
                }
                $.newAjax({
                    url: "BuMenGuanLi.aspx",
                    data: { id: dId, step: $(tar_a).attr("step"), sl: '<%=Request.QueryString["sl"]%>' },
                    dataType: "text",
                    cache: false,
                    type: "get",
                    success: function(result) {
                        $("#noDepart").remove();
                        $(tar_a).closest("tr").after(result);
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                })
            },
            delRe: function(id, arr1) {
                var arr = $("tr[parentid='" + id + "']");
                if (arr) {
                    arr.each(function() {
                        arr1.push($(this));
                        DM.delRe($(this).attr("sid"), arr1);
                    });
                }
            }
        }
        $(function() {
            $(".bumen_add").click(function() {
                DM.addD('0', this);
            })
        })
    </script>

</asp:Content>

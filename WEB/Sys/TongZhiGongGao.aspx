<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TongZhiGongGao.aspx.cs"
    Inherits="Web.Sys.TongZhiGongGao" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="searchFrom" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    标题：<input type="text" class="inputtext formsize140" id="txtTitle" name="txtTitle"
                        value="<%=Request.QueryString["txtTitle"]%>" />
                    发布人：<uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="发布人" SMode="false"
                        CallBackFun="PageJsDataObj.CallBackFun" />
                    <input type="submit" id="btnSubmit" value="搜索" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" value="<%=Request.QueryString["sl"]%>" name="sl" />
        </form>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="ph_Add">
                    <li><a href="javascript:void(0)" hidefocus="true" class="toolbar_add add_gg"><s class="addicon">
                    </s><span>添加</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="ph_Update" runat="server">
                    <li class="line"></li>
                    <li><a href="javascript:void(0)" hidefocus="true" class="toolbar_update"><s class="updateicon">
                    </s><span>修改</span></a> </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="ph_Del" runat="server">
                    <li class="line"></li>
                    <li><a href="javascript:void(0)" hidefocus="true" class="toolbar_delete"><s class="delicon">
                    </s><span>删除</span></a> </li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <th width="35" class="thinputbg">
                    <input type="checkbox" name="checkbox" value="0001" />
                </th>
                <th width="340" align="center" class="th-line">
                    标题
                </th>
                <th width="341" align="center" class="th-line">
                    是否提醒
                </th>
                <th width="177" align="center" class="th-line">
                    浏览数
                </th>
                <th width="134" align="center" class="th-line">
                    发布人
                </th>
                <th width="168" align="center" class="th-line">
                    发布时间
                </th>
                </tr>
                <asp:Repeater ID="RepList" runat="server">
                    <ItemTemplate>
                        <tr >
                            <td height="36" align="center">
                                <input type="checkbox" name="checkbox" value="<%#Eval("NoticeId")%>" />
                            </td>
                            <td align="center">
                                <a href="TongZhiGongGaoCK.aspx?id=<%#Eval("NoticeId")%>&sl=<%=Request.QueryString["sl"]%>"
                                    class="gg_show" title="<%#Eval("Title") %>">
                                    <%#EyouSoft.Common.Utils.GetText(Eval("Title").ToString(),15,true)%></a> &nbsp;
                                <%# GetAttach(Eval("ComAttachList"))%>
                            </td>
                            <td align="center">
                                <%#(bool)Eval("IsRemind")?"是":"<span style='color:#f00'>否</span>"%>
                            </td>
                            <td align="center">
                                <%#Eval("Views")%>
                            </td>
                            <td align="center">
                                <%#Eval("Operator")%>
                                <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                            </td>
                            <td align="center">
                                <%#Eval("IssueTime","{0:yyyy-MM-dd HH:mm}")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("select_Toolbar_Paging_1").innerHTML);
			</script>

        </div>
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            Query: {/*URL参数对象*/
                sl: '<%=Request.QueryString["sl"] %>'
            },
            //浏览弹窗关闭后刷新当前浏览数
            BindClose: function() {
                $("a[data-class='a_close']").unbind().click(function() {
                    window.location.reload();
                    return false;
                })
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: '/Sys',
                    title: "",
                    width: "710px",
                    height: "370px"
                }
            },
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            GoAjax: function(url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(result) {
                        if (result.result == "1") {
                            tableToolbar._showMsg(result.msg, function() {
                                $("#btnSubmit").click();
                            });

                        }
                        else { tableToolbar._showMsg(result.msg); }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            Add: function() {
                var data = this.DataBoxy();
                data.url += '/TongZhiGongGaoBJ.aspx?';
                data.title = '添加公告';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "add"
                });
                this.ShowBoxy(data);
            },
            Update: function(objsArr) {
                var data = this.DataBoxy();
                data.url += '/TongZhiGongGaoBJ.aspx?';
                data.title = '修改公告';
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "update",
                    id: objsArr[0].find('input[type="checkbox"]').val()
                });
                this.ShowBoxy(data);
            },
            Delete: function(objsArr) {
                var list = new Array();
                for (var i = 0; i < objsArr.length; i++) {
                    list.push(objsArr[i].find("input[type='checkbox']").val());
                }
                var data = this.DataBoxy();
                data.url += "/TongZhiGongGao.aspx?";
                data.url += $.param({
                    sl: this.Query.sl,
                    doType: "delete",
                    id: list.join(",")
                });
                this.GoAjax(data.url);
            },
            BindBtn: function() {
                $(".add_gg").click(function() {
                    PageJsDataObj.Add();
                    return false;
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "公告",
                    updateCallBack: function(objsArr) {
                        PageJsDataObj.Update(objsArr);
                        return false;
                    },
                    deleteCallBack: function(objsArr) {
                        PageJsDataObj.Delete(objsArr);
                    }
                });
            },
            PageInit: function() {
                //绑定功能按钮
                this.BindBtn();
                //当列表页面出现横向滚动条时使用以下方法 $("需要滚动最外层选择器").moveScroll();
                $('.tablelist-box').moveScroll();
            },
            CallBackFun: function(data) {
                newToobar.backFun(data);
            }

        }
        $(function() {
            PageJsDataObj.PageInit();
            //浏览详情
            $(".gg_liulan").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: $(this).attr("href"),
                    title: "浏览详情",
                    modal: true,
                    width: "408px",
                    height: "230px"
                });
                return false;
            });
            //公告信息
            $(".gg_show").click(function() {
                Boxy.iframeDialog({
                    iframeUrl: $(this).attr("href"),
                    title: "浏览详情",
                    modal: true,
                    width: "710px",
                    height: "340px"
                });
                PageJsDataObj.BindClose();
                return false;
            });
        })
    </script>

</asp:Content>

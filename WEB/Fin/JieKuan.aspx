<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JieKuan.aspx.cs" Inherits="EyouSoft.Web.Fin.JieKuan" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" method="get">
        <div class="searchbox border-bot fixed">
            <span class="searchT">
                <p>
                    团号：<input type="text" name="txt_teamNumber" value="<%=Request.QueryString["txt_teamNumber"] %>"
                        class="inputtext formsize120" />
                    借款人：<uc1:SellsSelect ID="txt_Seller" runat="server" SetTitle="借款人" SelectFrist="false" />
                    <input type="submit" id="submit_select" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
            <div id="pages" class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="center" class="th-line">
                        借款人
                    </th>
                    <th align="center" class="th-line">
                        借款日期
                    </th>
                    <th align="center" class="th-line">
                        预借金额
                    </th>
                    <th align="center" class="th-line">
                        <span class="th-line h20">实借金额</span>
                    </th>
                    <th align="center" class="th-line">
                        预领签单数
                    </th>
                    <th align="center" class="th-line">
                        实领签单数
                    </th>
                    <th align="center" class="th-line">
                        用途说明
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-debitid="<%#Eval("Id") %>" data-tourid="<%#Eval("TourId") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" />
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
                            </td>
                            <td align="center">
                                <%#Eval("Borrower")%>
                            </td>
                            <td align="center">
                                <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("BorrowTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("BorrowAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("RealAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="center">
                                <%#Eval("PreSignNum")%>
                            </td>
                            <td align="center">
                                <%#Eval("RelSignNum")%>
                            </td>
                            <td align="center">
                                <%#Eval("UseFor")%>
                            </td>
                            <td align="center" data-intstatus="<%#(int)Eval("Status") %>">
                                <a data-class="a_ExamineV" href="javascript:void(0);">未审批 </a><a data-class="a_Pay"
                                    href="javascript:void(0);">
                                    <%#((int)Eval("Status")) != (int)EyouSoft.Model.EnumType.FinStructure.FinStatus.账务已支付 ? "待支付" : "已支付"%>
                                </a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_msg" runat="server">
                    <tr align="center">
                        <td colspan="10">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div id="tablehead_clone">
        </div>
    </div>

    <script type="text/javascript">
        var LoanList = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: "/Fin",
                    title: "借款管理",
                    width: "650px",
                    height: "200px"
                }
            },
            Pay: function(Id, intstatus, tourid) {/*支付*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("Pay");
                data.height = "250px";
                data.url += ("/JieKuanZhiFu.aspx?" + $.param({
                    debitID: Id,
                    verificated:'<%=Request.QueryString["verificated"] %>',
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    intstatus: intstatus,
                    tourId: tourid
                }));
                this.ShowBoxy(data);
            },
            ExamineA: function(Id) { /*审核*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("ExamineA");
                data.url += ("/JieKuanShenHe.aspx?" + $.param({
                    debitID: Id,
                    verificated:'<%=Request.QueryString["verificated"] %>',
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                }));
                this.ShowBoxy(data);
            },
            BindBtn: function() {
                var that = this;
                $("#liststyle a[data-class='a_ExamineV']").click(function() {
                    that.ExamineA($(this).closest("tr").attr("data-debitid"));
                    return false;
                })
                $("#liststyle a[data-class='a_Pay']").click(function() {
                    var obj = $(this);
                    that.Pay(obj.closest("tr").attr("data-debitid"), obj.closest("td").attr("data-intstatus"), obj.closest("tr").attr("data-tourid"));
                    return false;
                })
            },
            PageInit: function() {
                tableToolbar.init({});
                var that = this;
                var isdefaul = '<%=Request.QueryString["verificated"]??"-1" %>';
                $("a[data-class='a_isDealt']").each(function() {
                    if ($(this).attr("data-value") == isdefaul) {
                        $(this).addClass("de-ztorderform");
                    }
                    else {
                        $(this).removeClass("de-ztorderform");
                    }
                })
                var intstatus;
                $("#liststyle td[data-intstatus]").each(function() {
                    var obj = $(this)
                    switch (parseInt(obj.attr("data-intstatus"))) {
                        case 0:
                            obj.find("[data-class='a_Pay']").remove();
                            break;
                        case 1:
                            obj.find("[data-class='a_ExamineV']").remove();
                            break;
                        case 2:
                            obj.find("[data-class='a_ExamineV']").remove();
                            if ('<%=IsEnableKis %>' == 'False') {
                                $(this).find("a[data-class='a_Pay']").html("查看");
                            }
                            break;
                        default:
                            obj.find("[data-class='a_ExamineV']").remove();
                            obj.find("[data-class='a_Pay']").remove();
                            break;
                    }
                })

                that.BindBtn();
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }
        $(function() {
            LoanList.PageInit();

        })
    </script>

</asp:Content>

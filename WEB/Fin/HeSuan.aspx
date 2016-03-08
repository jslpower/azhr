<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeSuan.aspx.cs" Inherits="EyouSoft.Web.Fin.HeSuan" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--内容-->
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <form id="SelectFrom" method="get">
    <div class="searchbox fixed">
        <span class="searchT">
            <p>
                团号：<input type="text" value="<%= Request.QueryString["teamNumber"] %>" name="teamNumber"
                    class="inputtext formsize120" />
                线路名称：<input type="text" value="<%= Request.QueryString["lineName"] %>" name="lineName"
                    class="inputtext formsize180" />
                出团时间：
                <input value="<%= Request.QueryString["sDate"] %>" name="sDate" type="text" class="inputtext formsize80"
                    onfocus="WdatePicker();" />
                -
                <input type="text" value="<%= Request.QueryString["eDate"] %>" name="eDate" class="inputtext formsize80"
                    onfocus="WdatePicker();" /><br />
                销售：<uc1:SellsSelect ID="txt_Seller" runat="server" selectfrist="false" SetTitle="销售员" />
                OP：<uc1:SellsSelect ID="txt_Plan" runat="server" selectfrist="false" SetTitle="OP" />
                导游：<uc1:SellsSelect ID="txt_Guide" runat="server" SetTitle="导游" />
                <input type="submit" id="submit_Select" class="search-btn" /></p>
        </span>
    </div>
    <input type="hidden" value="<%= Request.QueryString["adjustAccountsType"] %>" name="adjustAccountsType"
        id="hd_adjustAccountsType" />
    <input type="hidden" value="<%= Request.QueryString["sl"] %>" name="sl" />
    </form>
    <div id="tablehead" class="tablehead">
        <ul>
            <li data-class="li_adjustAccounts" data-type="-1"><s class="orderformicon"></s><a
                href="javascript:void();" hidefocus="true" class="ztorderform de-ztorderform"><span>
                    未核算</span></a></li>
            <li data-class="li_adjustAccounts" data-type="1"><s class="orderformicon"></s><a
                href="javascript:void();" hidefocus="true" class="ztorderform"><span>已核算</span></a></li>
        </ul>
        <div class="pages">
            <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
        </div>
    </div>
    <!--列表表格-->
    <div class="tablelist-box">
        <table width="100%" id="liststyle">
            <tr>
                <th width="30" class="thinputbg">
                    <input type="checkbox" name="checkbox" id="checkbox1" />
                </th>
                <th align="center" class="th-line">
                    团号
                </th>
                <th align="left" class="th-line">
                    线路名称
                </th>
                <th align="center" class="th-line">
                    出团时间
                </th>
                <th align="center" class="th-line">
                    人数
                </th>
                <th align="center" class="th-line">
                    销售
                </th>
                <th align="center" class="th-line">
                    计调
                </th>
                <th align="center" class="th-line">
                    导游
                </th>
                <th align="right" class="th-line">
                    收入
                </th>
                <th align="right" class="th-line">
                    支出
                </th>
                <th align="right" class="th-line">
                    毛利
                </th>
                <% if (this.IsShowGouWu)%>
                   <% {%>
                <th align="right" class="th-line">
                    隐形支出
                </th>
                   <% } %>
                <th align="right" class="th-line">
                    净利润
                </th>
                <th align="center" class="th-line">
                    操作
                </th>
            </tr>
            <asp:Repeater ID="rpt_list" runat="server">
                <ItemTemplate>
                    <tr data-tourid="<%#Eval("TourId") %>">
                        <td align="center">
                            <input type="checkbox" name="checkbox" />
                        </td>
                        <td align="center">
                            <%#Eval("TourCode")%>
                        </td>
                        <td align="left">
                            <%#Eval("RouteName")%>
                        </td>
                        <td align="center">
                            <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"), ProviderToDate)%>
                        </td>
                        <td align="center">
                            <b class="fontblue">
                                <%#Eval("Adults")%></b><sup class="fontred">+<%#Eval("Childs")%></sup>
                        </td>
                        <td align="center">
                            <%#Eval("SellerName")%>
                        </td>
                        <td align="center">
                            <%#Eval("Planers")%>
                        </td>
                        <td align="center">
                            <%#Eval("Guides")%>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("TourSettlement"),ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TourPay"),ProviderToMoney)%></b>
                        </td>
                        <td align="right">
                            <b>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Profit"), ProviderToMoney)%></b>
                        </td>
                <% if (this.IsShowGouWu)%>
                   <% {%>
                        <td align="right">
                            <b>
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("DisProfit"), ProviderToMoney)%></b>
                        </td>
                   <% } %>
                        <td align="right">
                            <b class="fontred">
                                <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("JProfit"), ProviderToMoney)%></b>
                        </td>
                        <td align="center">
                            <a href="javascript:void(0);" class="fontblue" data-class="a_accounting">核算</a>
                            <a href="javascript:void(0);" class="fontblue" data-class="a_show">查看</a>| <a target="_blank"
                                href="<%=PrintUri %>?sl=<%=this.SL %>&tourId=<%#Eval("TourId") %>" class="fontblue" data-class="">
                                核算单</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Panel ID="pan_msg" runat="server">
                <tr align="center">
                    <td colspan="14">
                        没有相关数据！
                    </td>
                </tr>
            </asp:Panel>
        </table>
    </div>
    <div id="tablehead_clone">
    </div>

    <script type="text/javascript">
        var PageJsDataObj = {
            BindBtn: function() {/*绑定功能按钮*/
                //列表按钮
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "记录"
                })
                //核算筛选按钮
                $("#tablehead li[data-class='li_adjustAccounts']").click(function() {
                    $("#hd_adjustAccountsType").val($(this).attr("data-type"))

                    $("#submit_Select").click();
                    return false;
                })
                //审核跳转
                $("#liststyle a[data-class='a_accounting'],#liststyle a[data-class='a_show']").click(function() {
                    var url="";
                   
                    var url = "/Fin/HeSuanJieShu.aspx?" + $.param({
                        sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                        tourID: $(this).closest("tr").attr("data-tourid"),
                        source: 2
                    });
                    //已核算标识
                    if($(this).attr("data-class")=="a_show"){
                        url+="&flag=1"
                    }
                    window.location.href = url;
                    return false;
                })

            },
            PageInit: function() {/*页面初始化*/
                var adjustAccountsType = Boxy.queryString("adjustAccountsType") || "-1";
                $("#tablehead a.de-ztorderform").removeClass("de-ztorderform");
                $("#tablehead li[data-type='" + adjustAccountsType + "'] a").addClass("de-ztorderform")
                //初始化查看和核算按钮
                if (adjustAccountsType == "-1") {
                    $("#liststyle a[data-class='a_show']").remove();
                }
                else {
                    $("#liststyle a[data-class='a_accounting']").remove();
                }
                //绑定功能按钮
                this.BindBtn();
                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }
        $(function() {
            //绑定功能按钮
            PageJsDataObj.PageInit();
        })
    </script>

</asp:Content>

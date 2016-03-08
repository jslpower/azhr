<%@ Page Language="C#" MasterPageFile="/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Web._Default" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .jsh_table td
        {
            border: solid 1px #c7c7c7;
        }
        .jsh_table th
        {
            border: solid 1px #c7c7c7;
        }
        .jsh_text_li
        {
            background: url(/images/icon_05.gif) no-repeat center;
            padding-left: 40px;
        }
        ._span
        {
            background: url(  "/images/bot_xian.gif" ) repeat-x scroll center bottom transparent;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="gg_mainbox">
        <!--left-->
        <div class="gg_mainbox-left">
            <div class="dchl">
                <h5>
                    待处理事项</h5>
                <ul>
                    <asp:Repeater ID="rptFirst" runat="server">
                        <ItemTemplate>
                            <li><span class="time">
                                <%# EyouSoft.Common.Utils.GetDateTime(Eval("MemoTime").ToString()).ToString("yy-MM-dd HH:MM")%></span><a
                                    href="/UserCenter/Memo/MemoInfo.aspx?Id=<%# Eval("Id") %>" class="ck_date" id="MemoTitle"><%# Eval("MemoTitle")%>
                                </a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div style="width: 100%; text-align: center; background-color: #ffffff">
                        <asp:Label ID="lblMsgFirst" runat="server" Text=""></asp:Label>
                    </div>
                </ul>
            </div>
            <div class="zhy" style="height: 408px">
                <h5>
                    重要提醒</h5>
                <ul class="fixed">
<%--                    <asp:PlaceHolder ID="phOrderremind" runat="server">
                        <li><a class="warmdefault" id="nav_1" href="/UserCenter/WorkAwake/OrderRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                            订单提醒(<%=this.GetRemindCount((int)EyouSoft.Model.EnumType.IndStructure.RemindType.订单提醒) %>)</a></li></asp:PlaceHolder>
--%>                    <asp:PlaceHolder ID="phjdremind" runat="server">
                        <li><a id="nav_2" href="/UserCenter/WorkAwake/OperaterRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                            计调提醒(<%=this.GetRemindCount((int)EyouSoft.Model.EnumType.IndStructure.RemindType.计调提醒) %>)</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phskremin" runat="server">
                        <li><a id="nav_5" href="/UserCenter/WorkAwake/CollectionRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                            收款提醒(<%=this.GetRemindCount((int)EyouSoft.Model.EnumType.IndStructure.RemindType.收款提醒)%>)</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phbgRemind" runat="server">
                        <li><a id="nav_7" href="/UserCenter/WorkAwake/PlanChangeRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                            变更提醒(<%=this.GetRemindCount((int)EyouSoft.Model.EnumType.IndStructure.RemindType.变更提醒)%>)</a></li></asp:PlaceHolder>
<%--                    <asp:PlaceHolder ID="phxjremind" runat="server">
                        <li><a id="nav_10" href="/UserCenter/WorkAwake/InquiryRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                            询价提醒(<%=this.GetRemindCount((int)EyouSoft.Model.EnumType.IndStructure.RemindType.询价提醒)%>)</a></li></asp:PlaceHolder>
                    <asp:PlaceHolder ID="phhtremind" runat="server">
                        <li><a id="nav_9" href="/UserCenter/WorkAwake/LaborRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">
                            劳动合同到期(<%=this.GetRemindCount((int)EyouSoft.Model.EnumType.IndStructure.RemindType.合同到期提醒)%>)</a></li></asp:PlaceHolder>
--%>                </ul>
                <div class="hr_10">
                </div>
                <%if (CheckGrant(EyouSoft.Model.EnumType.PrivsStructure.Privs.个人中心_事务提醒_收款提醒栏目))
                  { %><div style="position: relative;" class="addxc-box">
                      <span class="formtableT formtableT02">收款提醒</span><span class="zhy_table_xx"><a href="/UserCenter/WorkAwake/CollectionRemind.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.个人中心_事务提醒 %>">查看详情</a></span>
                      <table width="100%" cellspacing="0" cellpadding="0" border="0" class="tixinglist">
                          <tbody>
                              <tr>
                        <th width="30" class="th-line">
                            编号
                        </th>
                        <th align="center" class="th-line">
                            订单号
                        </th>
                        <th align="center" class="th-line">
                            欠款单位
                        </th>
                        <th align="center" class="th-line">
                            联系人
                        </th>
                        <th align="center" class="th-line">
                            电话
                        </th>
                        <th align="center" class="th-line">
                            应收金额
                        </th>
                        <th align="center" class="th-line">
                            欠款金额
                        </th>
                        <th align="center" class="th-line">
                            责任销售
                        </th>
                              </tr>
                              <asp:Repeater ID="rptSecond" runat="server">
                                  <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>"> 
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" />
                                    <%#Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDingDanChaKan(Eval("tourid"), Eval("orderid"), Eval("ordertype"), Eval("OrderCode"))%>
                                </td>
                                <td align="center">
                                    <%#Eval("Customer")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Contact")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Phone")%>
                                </td>
                                <td align="center" class="blue">
                                    <a href="javascript:void(0);" onclick="javascript:Boxy.iframeDialog({iframeUrl: '/Fin/YingShouDeng.aspx?sl=16&OrderId=<%#Eval("OrderId") %>&ReturnOrSet=1&ParentType=1&DefaultProofType=0&tourId=<%#Eval("TourId") %>',title: '应收管理-收款-',modal: true,width: 900,height: 350})"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"), this.ProviderToMoney)%></a>
                                </td>
                                <td align="center" class="red">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Arrear"), this.ProviderToMoney)%>
                                </td>
                                <td align="center">
                                    <%#Eval("SellerName")%>
                                </td>
                            </tr>
                                  </ItemTemplate>
                              </asp:Repeater>
                          </tbody>
                      </table>
                  </div>
                <div style="width: 100%; text-align: center; background-color: #ffffff">
                    <asp:Label ID="lblMsgSecond" runat="server" Text=""></asp:Label>
                </div>
                <div style="height: 30px;">
                    <div class="pages">
                        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                    </div>
                </div>
                <%}%>
            </div>
        </div>
        <!--left end-->
        <!--right-->
        <div class="gg_mainbox-right">
            <div class="gongg" style="height: 172px">
                <h5>
                    公告</h5>
                <ul>
                    <asp:Repeater ID="rptThird" runat="server">
                        <ItemTemplate>
                            <li><span class="time">
                                <%# EyouSoft.Common.Utils.GetDateTime(Eval("IssueTime").ToString()).ToString("MM-dd")%></span><%--<a
                                    href="#"><font color="#1B4F86">[<%# EyouSoft.Common.Utils.GetText(Eval("DepartName").ToString(),15) %>]</font></a>--%>
                                <a href="javascript:void(0)" onclick="BindBoxy('<%# Eval("NoticeId")%>')" title="<%#Eval("Title") %>">
                                    <%# EyouSoft.Common.Utils.GetText(Eval("Title").ToString(), 14,true)%></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div style="width: 100%; text-align: center; background-color: #ffffff">
                        <asp:Label ID="lblMsgThird" runat="server" Text=""></asp:Label>
                    </div>
                </ul>
            </div>
            <div class="jsh">
                <h5>
                    我的记事</h5>
                <span class="tab_rl">
                    <div class="hr_5">
                    </div>
                    <table width="98%" cellspacing="0" cellpadding="0" border="0" align="center">
                        <tbody>
                            <tr>
                                <td height="25" align="right">
                                    <a href="javascript:void(0);" id="a_left">
                                        <img width="14" height="14" src="/images/rili_l.gif" border="0" /></a>
                                </td>
                                <td width="99" align="center">
                                    <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
                                </td>
                                <td width="62" align="left">
                                    <a href="javascript:void(0);" id="a_right">
                                        <img width="14" height="14" src="/images/rili_r.gif" border="0" /></a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </span>
                <div id="divCalendarDate" style="margin-bottom: 10px;">
                </div>
                <div class="jsh_list">
                    <div class="jsh_btn">
                        <span class="jsh_ck">今天</span></div>
                    <div class="jsh_text">
                        <ul class="fixed">
                            <asp:Repeater ID="rptFour" runat="server">
                                <ItemTemplate>
                                    <li><font class="red">
                                        <%# EyouSoft.Common.Utils.GetDateTime(Eval("MemoTime").ToString()).ToShortDateString()%></font>
                                        <a href="/UserCenter/Memo/MemoInfo.aspx?Id=<%# Eval("Id") %>" class="ck_date" title="<%#Eval("MemoTitle") %>">
                                            <%# EyouSoft.Common.Utils.GetText2(Eval("MemoTitle").ToString(), 4, true)%></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <li style="text-align: right">
                                <asp:Literal ID="litTodayAll" runat="server"></asp:Literal></li>
                        </ul>
                    </div>
                </div>
                <div class="hr_5">
                </div>
                <div class="jsh_list">
                    <div class="jsh_btn">
                        <span class="jsh_ck">明天</span></div>
                    <div class="jsh_text">
                        <ul class="fixed">
                            <asp:Repeater ID="rptFive" runat="server">
                                <ItemTemplate>
                                    <li><font class="red">
                                        <%# EyouSoft.Common.Utils.GetDateTime(Eval("MemoTime").ToString()).ToShortDateString()%></font>
                                        <a href="/UserCenter/Memo/MemoInfo.aspx?Id=<%# Eval("Id") %>" class="ck_date" title="<%#Eval("MemoTitle") %>">
                                            <%# EyouSoft.Common.Utils.GetText2(Eval("MemoTitle").ToString(), 4, true)%></a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <li style="text-align: right">
                                <asp:Literal ID="litTomorrowAll" runat="server"></asp:Literal></li>
                        </ul>
                    </div>
                </div>
                <div class="hr_5">
                </div>
            </div>
        </div>
        <!--right end-->
        <table>
        </table>
    </div>

    <script type="text/javascript">

        function BindBoxy(parame) {
            Boxy.iframeDialog({
                iframeUrl: "UserCenter/Notice/NoticeInfo.aspx?sl=0&id=" + parame,
                title: "公告查看",
                modal: true,
                width: "600px",
                height: "370px"
            });
            return false;
        }

        function AllBoxy(date) {
            Boxy.iframeDialog({
                iframeUrl: "../UserCenter/Memo/AddDataMemo.aspx?stardate=" + date,
                title: "公告查看",
                modal: true,
                width: "600px",
                height: "370px"
            });
            return false;
        }

        $(function() {
            var year = parseInt('<%=DateTime.Now.Year %>');
            var mounth = parseInt('<%=DateTime.Now.Month %>');
            var mIndex = 1;
            $("#a_left").click(function() {
                mounth--;
                if (mounth == 0) { mounth = 12; year--; }
                ChangeMonth(year, mounth);

            })
            $("#a_right").click(function() {
                mounth++;
                if (mounth == 13) { mounth = 1; year++; }
                ChangeMonth(year, mounth);
            })

            $(".ck_date").live("click", function() {
                var url = $(this).attr("href");
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "查看备忘录",
                    modal: true,
                    width: "650px",
                    height: "360px"
                }); return false;
            })
            $(".dchl a").each(function() {

                $(this).click(function() {
                    var url = $(this).attr("href");
                    Boxy.iframeDialog({
                        iframeUrl: url,
                        title: "查看备忘录",
                        modal: true,
                        width: "650px",
                        height: "360px"
                    }); return false;
                });

            })


            ChangeMonth(year, mounth);
        })

        function ChangeMonth(year, mounth) {
            $("#divCalendarDate").html("<table width='100%'><tr align='center'><td><img src='/Images/loadingnew.gif' border='0' align='absmiddle' /></td></tr></table>");
            $.newAjax({
                type: "POST",
                url: "/UserCenter/Memorandum/AjaxMemorandum.aspx?flag=" + 1 + "&date=" + year + "-" + mounth + "-01" + "&IsBackDefault=" + 1 + "&ContainerID=" + 1 + "&rnd=" + Math.random(),
                cache: false,
                success: function(html) {
                    if (html != "") {
                        $("#divCalendarDate").html(html);
                        $("#<%=lblDate.ClientID %>").html(year + "年" + mounth + "月");
                    }
                }
            });
        }

        
    </script>

    <script type="text/javascript">

        var OrderRemind = {
            ToOrderInfoByType: function(tourtype, orderid) {
                var sl = '<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.整团报价_报价中心 %>';
            }
        }
    
    </script>

</asp:Content>

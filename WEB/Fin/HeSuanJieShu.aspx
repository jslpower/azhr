<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HeSuanJieShu.aspx.cs" Inherits="EyouSoft.Web.Fin.HeSuanJieShu" MasterPageFile="~/MasterPage/Front.Master"%>
<%@ Import Namespace="EyouSoft.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="addContent-box">
            <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                <tr>
                    <td width="10%" class="addtableT">
                        线路名称：
                    </td>
                    <td width="30%" class="kuang2">
                        <asp:Label ID="lbl_routeName" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="10%" class="addtableT">
                        出团时间：
                    </td>
                    <td width="20%" class="kuang2">
                        <asp:Label ID="lbl_lDate" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="10%" class="addtableT">
                        团号：
                    </td>
                    <td width="20%" class="kuang2">
                        <asp:Label ID="lbl_tourCode" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        天数：
                    </td>
                    <td class="kuang2">
                        <asp:Label ID="lbl_tourDays" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="addtableT">
                        人数：
                    </td>
                    <td class="kuang2">
                        <asp:Label ID="lbl_number" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="addtableT">
                        销售员：
                    </td>
                    <td class="kuang2">
                        <asp:Label ID="lbl_saleInfoName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        OP：
                    </td>
                    <td class="kuang2">
                        <asp:Label ID="lbl_tourPlaner" runat="server" Text=""></asp:Label>
                    </td>
                    <td class="addtableT">
                        导游：
                    </td>
                    <td colspan="3" class="kuang2">
                        <asp:Label ID="lbl_mGuidInfoName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <!--团队收入-->
        <div class="addContent-box">
            <span class="formtableT">团款收入</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th width="16%">
                        订单号
                    </th>
                    <th width="25%" align="left">
                        客源单位
                    </th>
                    <th>
                        下单人
                    </th>
                    <th>销售员</th>
                    <th align="right">
                        合同金额
                    </th>
                    <th align="right">
                        结算金额
                    </th>
                    <th align="right">
                        导游收款
                    </th>
                    <th align="right">
                        财务收款
                    </th>
                </tr>
                <asp:Repeater ID="rpt_tourMoneyIn" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%--<a href="<%#PringPageJSD %>?OrderId=<%#Eval("OrderId") %>&tourType=<%#(int)Eval("TourType") %>&ykxc=1"
                                    target="_blank">--%>
                                    <%#Eval("OrderCode")%>
                                <%--</a>--%>
                            </td>
                            <td align="left">
                                <%#Eval("BuyCompanyName")%>
                            </td>
                            <td align="center">
                                <%#Eval("Operator")%>
                            </td>
                            <td align="center"><%#Eval("SellerName")%></td>
                            <td align="right" class="fonthei">
                                <b class="<%#(bool)Eval("ConfirmMoneyStatus")?"":"fontred" %>">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SumPrice"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b class="fontgreen">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ConfirmMoney"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("GuideRealIncome"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <b>
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString((decimal)Eval("ConfirmMoney") - (decimal)Eval("GuideRealIncome"), ProviderToMoney)%></b>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_tourMoneyInMsg" runat="server">
                    <tr>
                        <td align="center" colspan="9">
                            暂无团款收入信息
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_tourMoneyInSum" runat="server">
                    <tr>
                        <td colspan="4" align="right">
                            <strong>合计：</strong>
                        </td>
                        <td align="right">
                            <b class="fontblue">
                                <asp:Label ID="lbl_sumPrice" runat="server" Text=""></asp:Label></b>
                        </td>
                        <td align="right">
                            <b class="fontgreen">
                                <asp:Label ID="lbl_confirmSettlementMoney" runat="server" Text=""></asp:Label></b>
                        </td>
                        <td align="right">
                            <b>
                                <asp:Label ID="lbl_guideRealIncome" runat="server" Text=""></asp:Label></b>
                        </td>
                        <td align="right">
                            <b>
                                <asp:Label ID="lbl_checkMoney" runat="server" Text=""></asp:Label></b>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        
        <asp:PlaceHolder runat="server" ID="phGouWuShouRu" Visible="false">
        
        <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">购物收入</span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia" id="tblGouWuShouRu">
                    <tr>
                        <th>
                            购物店
                        </th>
                        <th>
                            进店日期
                        </th>
                        <th>
                            进店人数
                        </th>
                        <th>
                            营业额
                        </th>
                        <th>
                            交给公司
                        </th>
                        <th>
                            交给导游
                        </th>
                        <th>
                            交给领队
                        </th>
                        <th>
                            备注
                        </th>
                    </tr>
                    <asp:Repeater ID="rptGouWuShouRu" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td width="16%" align="left">
                                    <%#Eval("SourceName")%>
                                </td>
                                <td width="18%" align="center">
                                    <%#Eval("StartDate","{0:yyyy-MM-dd}")%>
                                </td>
                                <td width="8%" align="center">
                                    <B class=fontblue><%#Eval("Adult")%></B><SUP class=fontred>+<%#Eval("Child")%></SUP>
                                </td>
                                <td width="8%" align="right">
                                    <%#Eval("YingYe","{0:C2}")%>
                                </td>
                                <td width="8%" align="right">
                                    <%#Eval("ToCompanyTotal","{0:C2}")%>
                                </td>
                                <td width="8%" align="right">
                                    <%#Eval("ToGuideTotal","{0:C2}")%>
                                </td>
                                <td width="8%" align="right">
                                    <%#Eval("ToLeaderTotal","{0:C2}")%>
                                </td>
                                <td width="38%" align="left">
                                    <%#Eval("Remarks")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="phGouWuShouRuEmpty" runat="server" Visible="false">
                        <tr align="center">
                            <td colspan="9">
                                暂无购物收入信息
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
                <div class="hr_10">
                </div>
            </div>   
         
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">其它收入</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th width="16%">
                        收入类型
                    </th>
                    <th width="25%">
                        付款单位
                    </th>
                    <th width="8%">
                        金额
                    </th>
                    <th width="10%">
                        支付方式
                    </th>
                    <th width="51%">
                        备注
                    </th>
                </tr>
                <asp:Repeater ID="rpt_restsMoneyIn" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <%#Eval("FeeItem")%>
                            </td>
                            <td align="left">
                                <%#Eval("Crm")%>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FeeAmount"), ProviderToMoney)%></b>
                            </td>
                            <td align="right">
                                <%#Eval("PayTypeName")%>
                            </td>
                            <td align="left">
                                <%#Eval("Remark")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                    <asp:Panel ID="phQiTaShouRuEmpty" runat="server" Visible="false">
                        <tr align="center">
                            <td colspan="9">
                                暂无其他收入信息
                            </td>
                        </tr>
                    </asp:Panel>
            </table>
            <div class="hr_10">
            </div>
        </div>
        
        <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">其它支出</span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                    <tr>
                        <th width="16%">
                            支出类型
                        </th>
                        <th width="25%">
                            收款单位
                        </th>
                        <th width="8%">
                            金额
                        </th>
                        <th width="10%">
                            支付方式
                        </th>
                        <th width="51%">
                            备注
                        </th>
                    </tr>
                    <asp:Repeater ID="repOtherMoneyOut" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Eval("FeeItem")%>
                                </td>
                                <td align="left">
                                    <%#Eval("Crm")%>
                                </td>
                                <td align="right">
                                    <b class="fontred">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FeeAmount"), ProviderToMoney)%></b>
                                </td>
                                <td align="center">
                                    <%#Eval("PayTypeName")%>
                                </td>
                                <td align="left">
                                    <%#Eval("Remark")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="phQiTaZhiChuEmpty" runat="server" Visible="false">
                        <tr align="center">
                            <td colspan="5">
                                暂无其他支出信息
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
                <div class="hr_10">
                </div>
            </div>
         
        <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">隐形支出</span><span>
                    <asp:PlaceHolder ID="pan_AddMongyAllot" runat="server" Visible="false"><a href="javascript:void(0);"
                        id="a_AddMongyAllot">
                        <img src="/images/addimg.gif" /></a></asp:PlaceHolder>
                </span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                    <tr>
                        <th height="31">
                            团号/订单号
                        </th>
                        <th align="center">
                            人员
                        </th>
                        <th align="right">
                            分配金额
                        </th>
                        <th align="right">
                            毛利
                        </th>
                        <th align="right">
                            净利
                        </th>
                        <th align="left">
                            备注
                        </th>
                        <th width="115" align="center">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_mongyAllot" runat="server">
                        <ItemTemplate>
                            <tr data-id="<%#Eval("Id") %>">
                                <td width="16%" align="center">
                                    <%#Eval("TourCode")%>/<%#Eval("OrderCode")%>
                                </td>
                                <td width="8%" align="center">
                                    <%#Eval("Staff")%>
                                </td>
                                <td width="8%" align="right">
                                    <b class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Amount"), ProviderToMoney)%></b>
                                </td>
                                <td width="9%" align="right">
                                    <b class="fontgreen">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Gross"), ProviderToMoney)%></b>
                                </td>
                                <td width="8%" align="right">
                                    <b class="fontred">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(((decimal)Eval("Gross"))- ((decimal)Eval("Amount")), ProviderToMoney)%></b>
                                </td>
                                <td align="left">
                                    <%#Eval("Remark")%>
                                </td>
                                <td align="center">
                                    <a data-class="a_UpdateMongyAllot" data-editbutton="edit" href="javascript:void(0);">
                                        <img src="/images/y-delupdateicon.gif" border="0" />
                                        修改 </a><a data-class="a_DelMongyAllot" data-editbutton="edit" href="javascript:void(0);">
                                            <img src="/images/y-delicon.gif" />
                                            删除</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pan_mongyAllotMsg" runat="server">
                        <tr>
                            <td align="center" colspan="7">
                                暂无隐形支出信息
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            <div class="hr_10">
            </div>
            </div>
          
        </asp:PlaceHolder>
                    
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">团队支出</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th>
                        类别
                    </th>
                    <th align="left">
                        供应商
                    </th>
                    <th align="center">
                        支付方式
                    </th>
                    <th align="center">
                        数量
                    </th>
                    <th align="right">
                        结算金额
                    </th>
                </tr>
                <asp:Repeater ID="rpt_tourMoneyOut" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td width="16%" align="center">
                                <%#Eval("Type")%>
                            </td>
                            <td width="25%" align="left">
                                <%#Eval("SourceName")%>
                            </td>
                            <td width="11%" align="center">
                                <%# Utils.GetEnumText(typeof(EyouSoft.Model.EnumType.PlanStructure.Payment),Eval("PaymentType"))%>
                            </td>
                            <td width="7%" align="center" data-class="td_Num">
                                <%#Eval("Num")%>
                            </td>
                            <td width="10%" align="right" data-confirmation="<%#Eval("Confirmation") %>">
                                <b class="fontred">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("Confirmation"), ProviderToMoney)%></b>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_tourMoneyOutMsg" runat="server">
                    <tr>
                        <td align="center" colspan="6">
                            暂无团队支出信息
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pan_tourMoneyOut" runat="server">
                    <tr>
                        <td colspan="4" align="right">
                            合计：
                        </td>
                        <td align="right">
                            <b class="fontred" data-calss="b_Confirmation">
                                <asp:Label ID="lbl_tourMoneyOutSumConfirmation" runat="server" Text="0"></asp:Label></b>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        <div class="hr_10">
        </div>
        </div>
                
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">报账汇总</span>
            <table id="tab_RSummary" width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th width="14%" height="20" align="right">
                        导游收入
                    </th>
                    <th width="13%" align="right">
                        导游借款
                    </th>
                    <th width="14%" align="right">
                        导游支出
                    </th>
                    <th width="14%" align="right">
                        补领/归还
                    </th>
                    <th width="15%" align="center">
                        实领签单数
                    </th>
                    <th width="15%" align="center">
                        已使用签单数
                    </th>
                    <th width="15%" align="center">
                        归还签单数
                    </th>
                </tr>
                <tr>
                    <td width="14%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_guidesIncome" runat="server" Text=""></asp:Label></b>
                    </td>
                    <td width="13%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_guidesBorrower" runat="server" Text=""></asp:Label></b>
                    </td>
                    <td width="14%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_guidesSpending" runat="server" Text=""></asp:Label></b>
                    </td>
                    <td width="14%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_replacementOrReturn" runat="server" Text=""></asp:Label></b>
                    </td>
                    <td width="15%" align="center">
                        <asp:Label ID="lbl_RCSN" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="15%" align="center">
                        <asp:Label ID="lbl_HUSN" runat="server" Text=""></asp:Label>
                    </td>
                    <td width="15%" align="center">
                        <asp:Label ID="lbl_RSN" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        <div class="hr_10">
        </div>
        </div>
        
        <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">团队收支表</span>
            <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                <tr>
                    <th width="25%" align="right">
                        团队收入
                    </th>
                    <th width="25%" align="right">
                        团队支出
                    </th>
                    <th width="25%" align="right">
                        团队利润
                    </th>
                    <th width="25%" align="right">
                        利润率
                    </th>
                </tr>
                <tr>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_tourMoneyIn" runat="server" Text="0">0</asp:Label></b>
                    </td>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_tourMoneyOut" runat="server" Text="0">0</asp:Label></b>
                    </td>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_tourMoney" runat="server" Text="0">0</asp:Label></b>
                    </td>
                    <td width="25%" align="right">
                        <b class="fontred">
                            <asp:Label ID="lbl_tourMoneyRate" runat="server" Text="0">0</asp:Label></b>
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
        </div>

        <div id="div_btn" class="mainbox cunline fixed" style="width: 450px;">
            <ul>
                <asp:PlaceHolder ID="pan_returnOperater" runat="server" Visible="false">
                    <li id="li_returnOperater" class="cun-cy"><a href="javascript:void(0);">退回计调</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_submitFinance" runat="server" Visible="false">
                    <li id="li_submitFinance" class="cun-cy"><a href="javascript:void(0);">提交财务</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_sealTour" runat="server" Visible="false">
                    <li id="li_sealTour" class="cun-cy">
                        <input type="hidden" runat="server" id="TourSettlement" name="TourSettlement" />
                        <input type="hidden" runat="server" id="TourPay" name="TourPay" />
                        <input type="hidden" runat="server" id="TourProfit" name="TourProfit" />
                        <input type="hidden" runat="server" id="DisOrderProfit" name="DisOrderProfit" />
                        <input type="hidden" runat="server" id="DisTourProfit" name="DisTourProfit" />
                        <input type="hidden" runat="server" id="TourIncome" name="TourIncome" />
                        <input type="hidden" runat="server" id="TourGouWu" name="TourGouWu" />
                        <input type="hidden" runat="server" id="TourOtherOutpay" name="TourOtherOutpay" />
                        <input type="hidden" runat="server" id="TourOtherIncome" name="TourOtherIncome" />
                        <a href="javascript:void(0);">核算结束</a></li>
                </asp:PlaceHolder>
                <li class="quxiao-cy"><a href="javascript:window.history.go(-1)">返回</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
        
        <div class="alertbox-outbox03" id="divTui" style="display: none; padding-bottom: 0px;">
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        退回说明：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <textarea id="txtTui" style="height: 100px;" class="inputtext formsize600"
                            name="txtTui"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a hidefocus="true" href="javascript:void(0);" id="btnTui"><s class="baochun">
            </s>保 存</a><a hidefocus="true" href="javascript:void(0);" onclick="CommPage.TuiBox.hide();return false;"><s
                class="chongzhi"></s>关闭</a>
        </div>
    </div>
    </div>

    <script type="text/javascript">
        var CommPage = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            TuiBox: null,//退回说明弹窗
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: "/Fin/LiRunFenPei.aspx?",
                    title: "添加隐形支出",
                    width: "530px",
                    height: "240px"
                }
            },
            UnBind: function() {
                var obj = $("#div_btn li.btn a");
                obj.css({ "background-position": "0 -62px" });
                obj.unbind("click");
            },
            Add: function() {/*添加 隐形支出*/
                var data = this.DataBoxy();
                data.url += $.param({
                    tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                    Price: '<%=Price %>',
                    TourCode: '<%=TourCode %>',
                    sl: '<%=SL %>'
                });
                this.ShowBoxy(data);
            },
            Updata: function(obj) {/*修改 隐形支出*/
                var data = this.DataBoxy();
                data.url += $.param({
                    tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                    Price: '<%=Price %>',
                    id: $(obj).closest("tr").attr("data-id"),
                    TourCode: '<%=TourCode %>',
                    sl: '<%=SL %>'
                });
                data.title = "修改隐形支出";
                this.ShowBoxy(data);
            },
            BindButtBtn: function() {/*页面底部按钮*/
                var that = this;
                var obj = $("#li_returnOperater")
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {/*退回计调*/
                    that.UnBind();
                	that.TuiBox = new Boxy($("#divTui"), { modal: true, fixed: false, title: "退回说明", width: "725px", height: "245px", display: "none" }); 
                })
            	$("#btnTui").click(function() {
                if ($.trim($("#txtTui").val()).length < 1) { tableToolbar._showMsg("退回说明不能为空!"); return false; }
                CommPage.TuiBox.hide();
                var data = {
                    sl: '<%=SL %>',
                    tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                    type: "ReturnOperater",
                	tourtype:"<%=this._TourType %>",
                	Tui:$("#txtTui").val()
                }
                CommPage.GoAjax(data);
            });
                obj = $("#li_submitFinance")
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {/*提交财务*/
                    that.UnBind();
                    var data = {
                        sl: '<%=SL %>',
                        tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                        type: "SubmitFinanceManage"
                    }
                    that.GoAjax(data);
                })
                obj = $("#li_sealTour")
                obj.css({ "background-position": "0 0px" });
                obj.unbind("click").click(function() {/*核算结束*/
                    that.UnBind();
                    var data = {
                        sl: '<%=SL %>',
                        tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
                        type: "AppealEnd"
                    }
                    that.GoAjax($.param(data) + "&" + $(this).closest("li").find(":hidden").serialize());
                })
            },
            BindListBtn: function() {/*绑定隐形支出列表上的按钮*/
//                //已核算
//                if ('<%=flag %>' == "1") {
//                    //控制隐形支出编辑按钮(已核算的时候不显示修改和删除按钮)
//                    $("a[data-editbutton='edit']").hide();
//                }
                var that = this;
                $("#a_AddMongyAllot").click(function() {
                    that.Add();
                    return false;
                })
                $("a[data-class='a_UpdateMongyAllot']").click(function() {
                    that.Updata(this);
                    return false;
                })
                $("a[data-class='a_DelMongyAllot']").click(function() {
                    var obj = this;
                    tableToolbar.ShowConfirmMsg("确定删除隐形支出？", function() {
                        var data = {
                            type: "Del",
                            sl: '<%=SL %>',
                            id: $(obj).closest("tr").attr("data-id")
                        };
                        CommPage.GoAjax(data);
                    })
                    return false;
                })

            },
            GoAjax: function(data) {/*通用Ajax请求*/
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/Fin/HeSuanJieShu.aspx",
                    data: data,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg("提交成功!", function() {
                                window.location = window.location;
                            });

                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        tableToolbar._showMsg("服务器忙!");
                    }
                });
            },
            PageInit: function() {
                this.BindListBtn();
                this.BindButtBtn();
            }
        }

        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            }
        };

        $(function() {
            CommPage.PageInit();
        })
    </script>

</asp:Content>

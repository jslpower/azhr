<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoZhangCaoZuo.aspx.cs" Inherits="EyouSoft.Web.Guide.BaoZhangCaoZuo" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Import Namespace="EyouSoft.Model.EnumType.ComStructure" %>
<%@ Import Namespace="EyouSoft.Model.EnumType.TourStructure" %>
<%@ Import Namespace="EyouSoft.Model.EnumType.FinStructure" %>
<%@ Register Src="~/UserControl/TourMoneyOut.ascx" TagName="TourMoneyOut" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="addContent-box">
            <table class="firsttable" cellpadding="0" cellspacing="0" width="100%">
                <tbody>
                    <tr>
                        <td class="addtableT">
                            团号：
                        </td>
                        <td class="kuang2" style="width: 150px">
                            <asp:Label ID="lbl_TourCode" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT" style="width: 80px">
                            线路区域：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_AreaName" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            线路名称：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_RouteName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            天数：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_TourDays" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            出团时间：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_LDate" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            回团时间：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_RDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            带团导游：
                        </td>
                        <td class="kuang2">
                            <asp:Label ID="lbl_TourGride" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            出发交通：
                        </td>
                        <td colspan="3" class="kuang2">
                            <asp:Label ID="lbl_LTraffic" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            本团销售：
                        </td>
                        <td class="kuang2">
                            <asp:HiddenField ID="hideSaleId" runat="server" />
                            <asp:Label ID="lbl_SaleInfo" runat="server" Text=""></asp:Label>
                        </td>
                        <td class="addtableT">
                            返程交通：
                        </td>
                        <td colspan="3" class="kuang2">
                            <asp:Label ID="lbl_RTraffic" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="117" class="addtableT">
                            本团计调：
                        </td>
                        <td colspan="5"class="kuang2">
                            <asp:Label ID="lbl_TourPlaner" runat="server" Text=""></asp:Label>
                        </td>
                        <%--<td class="addtableT">
                            集合方式：
                        </td>
                        <td colspan="3" class="kuang2">
                            <asp:Label ID="lbl_Gather" runat="server" Text=""></asp:Label>
                        </td>--%>
                    </tr>
                </tbody>
            </table>
        </div>
         
        <uc1:TourMoneyOut ID="TourMoneyOut" runat="server" />
        <div class="hr_10">
        </div>
        
        <asp:PlaceHolder ID="panMoneyViewGuid" runat="server" Visible="false">
        
            <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">导游代收团款</span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia" id="tabGuidMoneyIn">
                    <tr>
                        <th>
                            订单号
                        </th>
                        <th>
                            客源单位
                        </th>
                        <th>
                            导游应收
                        </th>
                        <th>
                            导游实收
                        </th>
                        <th>
                            备注
                        </th>
                        <th width="12%">
                            <strong>操作</strong>
                        </th>
                    </tr>
                    <asp:Repeater ID="repGuidInMoney" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td width="16%" align="left">
                                    <%#Eval("OrderCode")%><input name="txtOrderCode" type="hidden" value="<%#Eval("OrderCode")%>" />
                                    <input type="hidden" name="OrderIdhid" value="<%# Eval("OrderId") %>" />
                                </td>
                                <td width="18%" align="left">
                                    <%#Eval("BuyCompanyName")%><input name="txtbuyCompany" type="hidden" value="<%#Eval("BuyCompanyName")%>" />
                                </td>
                                <td width="8%" align="right">
                                    <b class="fontred"><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("GuideIncome"),ProviderToMoney)%></b>
                                    <input name="txtGuideIncome" type="hidden" value="<%# Eval("GuideIncome") %>" />
                                </td>
                                <td width="8%" align="right">
                                    <input name="txtRealIncome" type="text" class="inputtext formsize120" value="<%# EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Utils.GetDecimal(Eval("GuideRealIncome").ToString()))%>" />
                                </td>
                                <td width="38%" align="left">
                                    <textarea name="txtConfirmRemark" style="width: 96%; height: 28px;" class="inputtext"><%#Eval("GuideRemark")%></textarea>
                                </td>
                                <td align="center">
                                    <%if (this.status <= TourStatus.导游报帐 && this.IsGrant)%>
                                    <%{%>
                                        <a href="javascript:void(0);" class="addbtn" data-class="updateOrder" data-orderid="<%# Eval("OrderId") %>"><img src="/images/updateimg.gif" border="0" /></a>
                                    <%}%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="phDaoYouShouRuEmpty" runat="server" Visible="false">
                        <tr align="center">
                            <td colspan="6">
                                暂无导游代收团款信息
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
                <div class="hr_10">
                </div>
            </div>
            
            <asp:PlaceHolder runat="server" ID="phGouWuShouRu" Visible="false">
            
            <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">交给公司</span>
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
                        <th width="12%">
                            <strong>操作</strong>
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
                                    <textarea name="txtConfirmRemark" style="width: 96%; height: 28px;" class="inputtext"><%#Eval("Remarks")%></textarea>
                                </td>
                                <td align="center">
                                    <%if (this.status <= TourStatus.导游报帐 && this.IsGrant)%>
                                    <%{%>
                                        <a href="javascript:void(0);" class="addbtn" data-class="updgouwu" data-planid="<%# Eval("PlanId") %>" data-sourceid="<%# Eval("SourceId") %>"><img src="/images/updateimg.gif" border="0" /></a>
                                    <%}%>
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
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia" id="tabFreeItemList">
                    <tr>
                        <th width="13%">
                            收入类型
                        </th>
                        <th width="29%">
                            付款单位
                        </th>
                        <th width="8%">
                            金额
                        </th>
                        <th width="10%">
                            支付方式
                        </th>
                        <th width="28%">
                            备注
                        </th>
                        <th width="12%">
                            <strong>操作</strong>
                        </th>
                    </tr>
                    <asp:Repeater ID="repOtherMoneyIn" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Eval("FeeItem")%><input type="hidden" name="txtFreeItem" value="<%#Eval("FeeItem")%>" />
                                </td>
                                <td align="left">
                                    <%#Eval("Crm")%>
                                    <input type="hidden" name="hidcrmId" value="<%#Eval("CrmId")%>" />
                                    <input type="hidden" name="hidcrmName" value="<%#Eval("Crm")%>" />
                                </td>
                                <td align="right">
                                    <input name="txtFeeAmount" type="text" class="inputtext formsize50" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Utils.GetDecimal(Eval("FeeAmount").ToString()))%>" />
                                </td>
                                <td align="center">
                                    <%#GetPayMentStr(Eval("PayType").ToString(),ItemType.收入)%>
                                </td>
                                <td align="left">
                                    <textarea name="txtRemark" style="width: 96%; height: 35px;" class="inputtext"><%#Eval("Remark")%></textarea>
                                </td>
                                <td align="center">
                                    <%if (this.status <= TourStatus.导游报帐 && this.IsGrant)%>
                                    <%{%>
                                        <a href="javascript:void(0);" style='visibility: <%#(FinStatus)Eval("Status") < FinStatus.账务待支付?"visible":"hidden"%>' class="addbtn" data-class="updateFreeItem" data-id="<%# Eval("Id") %>"><img src="/images/updateimg.gif" border="0" alt="" /></a>
                                        <a href="javascript:void(0);" style='visibility: <%#(FinStatus)Eval("Status") < FinStatus.账务待支付?"visible":"hidden"%>' class="addbtn" data-class="deleteFreeItem" data-id="<%# Eval("Id") %>"><img src="/images/delimg.gif" border="0" alt="" /></a>
                                    <%}%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="panMoneyInView" runat="server" Visible="false">
                        <tr>
                            <td align="center">
                                <input name="txtFreeItem" type="text" class="inputtext formsize80" />
                            </td>
                            <td align="left">
                                <uc2:CustomerUnitSelect ID="UnitSelect1" runat="server" IsUniqueness="false" />
                            </td>
                            <td align="right">
                                <input name="txtFeeAmount" type="text" class="inputtext formsize50" value="0" />
                            </td>
                            <td align="center">
                                <%=GetPayMentStr(string.Empty,ItemType.收入)%>
                            </td>
                            <td align="left">
                                <textarea name="txtRemark" style="width: 96%; height: 35px;" class="inputtext"></textarea>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" class="addbtn" data-class="addOtherFreeItem"><img src="/images/addimg.gif" border="0" /></a>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
                <div class="hr_10">
                </div>
            </div>
            
            <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">其它支出</span>
                <table width="100%" cellpadding="0" cellspacing="0" class="add-baojia" id="tblQiTaZhiChu">
                    <tr>
                        <th width="13%">
                            支出类型
                        </th>
                        <th width="29%">
                            收款单位
                        </th>
                        <th width="8%">
                            金额
                        </th>
                        <th width="10%">
                            支付方式
                        </th>
                        <th width="28%">
                            备注
                        </th>
                        <th width="12%">
                            <strong>操作</strong>
                        </th>
                    </tr>
                    <asp:Repeater ID="repOtherMoneyOut" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Eval("FeeItem")%><input type="hidden" name="txtFreeItem" value="<%#Eval("FeeItem")%>" />
                                </td>
                                <td align="left">
                                    <%#Eval("Crm")%>
                                    <input type="hidden" name="hidcrmId" value="<%#Eval("CrmId")%>" />
                                    <input type="hidden" name="hidcrmName" value="<%#Eval("Crm")%>" />
                                </td>
                                <td align="right">
                                    <input name="txtFeeAmount" type="text" class="inputtext formsize50" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal(Utils.GetDecimal(Eval("FeeAmount").ToString()))%>" />
                                </td>
                                <td align="center">
                                    <%#GetPayMentStr(Eval("PayType").ToString(),ItemType.支出)%>
                                </td>
                                <td align="left">
                                    <textarea name="txtRemark" style="width: 96%; height: 35px;" class="inputtext"><%#Eval("Remark")%></textarea>
                                </td>
                                <td align="center">
                                    <%if (this.status <= TourStatus.导游报帐 && this.IsGrant)%>
                                    <%{%>
                                        <a href="javascript:void(0);" style='visibility: <%#(FinStatus)Eval("Status") < FinStatus.账务待支付?"visible":"hidden"%>' class="addbtn" data-class="updateFreeItem" data-id="<%# Eval("Id") %>"><img src="/images/updateimg.gif" border="0" alt="" /></a>
                                        <a href="javascript:void(0);" style='visibility: <%#(FinStatus)Eval("Status") < FinStatus.账务待支付?"visible":"hidden"%>' class="addbtn" data-class="deleteFreeItem" data-id="<%# Eval("Id") %>"><img src="/images/delimg.gif" border="0" alt="" /></a>
                                    <%}%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="phQiTaZhiChuAdd" runat="server" Visible="false">
                        <tr>
                            <td align="center">
                                <input name="txtFreeItem" type="text" class="inputtext formsize80" />
                            </td>
                            <td align="left">
                                <uc2:CustomerUnitSelect ID="UnitSelect2" runat="server" IsUniqueness="false" />
                            </td>
                            <td align="right">
                                <input name="txtFeeAmount" type="text" class="inputtext formsize50" value="0" />
                            </td>
                            <td align="center">
                                <%=GetPayMentStr(string.Empty,ItemType.支出)%>
                            </td>
                            <td align="left">
                                <textarea name="txtRemark" style="width: 96%; height: 35px;" class="inputtext"></textarea>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0);" class="addbtn" data-class="addOtherFreeItem"><img src="/images/addimg.gif" border="0" /></a>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </table>
                <div class="hr_10">
                </div>
            </div>
            
            </asp:PlaceHolder>
            
            <div class="tablelist-box " style="width: 98.5%">
            <span class="formtableT">导游借款</span>
            <table class="add-baojia" cellpadding="0" cellspacing="0" width="100%">
                <tbody>
                    <tr>
                        <th width="25%">
                            姓名
                        </th>
                        <th width="25%">
                            借款时间
                        </th>
                        <th width="25%">
                            实借金额
                        </th>
                        <th width="25%">
                            领用签单数
                        </th>
                    </tr>
                    <asp:Repeater ID="rpt_Debit" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <%#Eval("Borrower")%>
                                </td>
                                <td align="center">
                                    <%# EyouSoft.Common.UtilsCommons.GetDateString( Eval("BorrowTime"),ProviderToDate)%>
                                </td>
                                <td align="right">
                                    <b class="fontred">
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("RealAmount"),ProviderToMoney)%>
                                    </b>
                                </td>
                                <td align="center">
                                    <%#Eval("RelSignNum")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Panel ID="pan_DebitMsg" runat="server">
                        <tr align="center">
                            <td colspan="4">
                                暂无导游借款信息
                            </td>
                        </tr>
                    </asp:Panel>
                </tbody>
            </table>
            <div class="hr_10">
            </div>
        </div>
        
            <div class="tablelist-box " style="width: 98.5%">
                <span class="formtableT">报账汇总</span>
                <table id="tab_RSummary" width="100%" cellpadding="0" cellspacing="0" class="add-baojia">
                    <tr>
                        <th width="14%" height="20">
                            导游收入
                        </th>
                        <th width="13%">
                            导游借款
                        </th>
                        <th width="14%">
                            导游支出
                        </th>
                        <th width="14%">
                            补领/归还
                        </th>
                        <th width="15%">
                            实领签单数
                        </th>
                        <th width="15%">
                            已使用签单数
                        </th>
                        <th width="15%">
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
            
            <asp:PlaceHolder runat="server" ID="phTuanDuiShouZhi" Visible="false">
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
                <div class="hr_5">
                </div>
            </div>
            </asp:PlaceHolder>
        
        </asp:PlaceHolder>       
        
        <div class="hr_10"></div>
        <div class="mainbox cunline">
            <ul id="ul_btn_list">
                <asp:PlaceHolder ID="pan_OperaterExamineV" runat="server" Visible="false">
                    <li class="cun-cy"><a href="javascript:void(0);" id="a_OperaterExamineV">提交财务审核</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_DaoYouBianGeng" runat="server" Visible="false">
                    <li class="cun-cy"><a href="javascript:void(0);" id="DaoYouBianGeng">导游变更</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_ApplyOver" runat="server" Visible="false">
                    <li class="cun-cy"><a id="a_ApplyOver" href="javascript:void(0);">报销完成</a></li>
                </asp:PlaceHolder>
                <li class="quxiao-cy"><a href="javascript:window.history.go(-1)">返回</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>
                        <input type="hidden" runat="server" id="TourGouWu" name="TourGouWu" />
                        <input type="hidden" runat="server" id="TourOtherOutpay" name="TourOtherOutpay" />
                        <input type="hidden" runat="server" id="TourOtherIncome" name="TourOtherIncome" />

    <script type="text/javascript">
        var AccountedForPage = {
        	_sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
        	_tourId: '<%=EyouSoft.Common.Utils.GetQueryStringValue("tourId") %>',
        	_ajax: function(data) {
        		var that = this;
        		if (data && data["url"]) {
        			$.newAjax({
        					type: data["getType"] || "POST",
        					url: data["url"],
        					cache: false,
        					data: data["data"] || { },
        					dataType: "json",
        					success: function(ret) {
        						if (data["success"]) {
        							data["success"](ret)
        							return false;
        						}
        						else {
        							if (ret.result == "1") {
        								tableToolbar._showMsg("提交成功!", function() {
        									window.location.reload();
        								});

        							} else {
        								tableToolbar._showMsg(ret.msg, function() {
        									that.BindButtBtn();
        								});
        							}
        						}

        					},
        					error: function() {
        						tableToolbar._showMsg(tableToolbar.errorMsg, function() {
        							that.BindButtBtn();
        						});
        						return false;
        					}
        				});
        		}
        	},
        	Save: function(obj, ID,typ) { /*其他添加修改*/
        		var data = { };
        		var tr = obj.closest("tr");
        		var crmID = tr.find("td").eq(1).find("input[type='hidden']").eq(0).val();
        		if (crmID == "") {
        			tableToolbar._showMsg("请选择付款单位!");
        			tr.find("td").eq(1).find("input[type='text']").focus();
        			return false;
        		}
        		data["url"] = '/Guide/BaoZhangCaoZuo.aspx?' + $.param({
        				type: typ,
        				sl: AccountedForPage._sl,
        				source: '<%=Request.QueryString["source"] %>',
        				ID: ID,
        				tourId: AccountedForPage._tourId,
        				tourCode: $("#<%=lbl_TourCode.ClientID %>").html(),
        				sellerId: $("#<%=hideSaleId.ClientID %>").val(),
        				sellerName: $.trim($("#<%=lbl_SaleInfo.ClientID %>").html())
        			});
        		data["data"] = tr.find("input,textarea,select").serialize();

        		if (ID != "") {
        			data["data"] += "&" + $.param({ crmName: tr.find("td:eq(1)").find("input[type='hidden'][name='hidcrmName']").val(), crmId: crmID, paymentText: tr.find("select[name='other_payment'] :selected").text() });
        		}
        		else {
        			data["data"] += "&" + $.param({ crmName: tr.find("td").eq(1).find("input[type='text']").val(), crmId: crmID, paymentText: tr.find("select[name='other_payment'] :selected").text() });
        		}
        		this._ajax(data);
        	},
        	DeleteFreeItem: function(obj, ID,typ) { /*其他删除*/
        		var data = { };
        		data["url"] = '/Guide/BaoZhangCaoZuo.aspx?' + $.param({
        				type: typ,
        				sl: AccountedForPage._sl,
        				ID: ID,
        				tourId: AccountedForPage._tourId
        			})
        		data["data"] = obj.closest("tr").serialize();
        		data["success"] = function(ret) {
        			if (ret.result == "1") {
        				tableToolbar._showMsg(ret.msg);
        				obj.parent().parent().css("display", "none");
        			} else {
        				tableToolbar._showMsg(ret.msg);
        			}
        		}
        		this._ajax(data);
        	},
        	SaveGuidMoneyIn: function(obj, Id, actionType) { /*导游收入添加修改*/
        		var data = { };
        		var tr = obj.closest("tr");
        		if (tr.find("input[name='txtOrderCode']").val() == "") {
        			tr.find("input[name='txtOrderCode']").focus();
        			tableToolbar._showMsg("请选择订单");
        			return false;
        		}
        		data["url"] = '/Guide/BaoZhangCaoZuo.aspx?' + $.param({
        				type: "saveGuidMoneyIn",
        				sl: AccountedForPage._sl,
        				tourId: AccountedForPage._tourId,
        				OrderId: Id,
        				actionType: actionType
        			});
        		data["data"] = tr.find("input,textarea").serialize();
        		this._ajax(data);
        	},
        	DeleteGuidMoneyIn: function(obj, Id) { /*导游收入删除*/
        		var data = { };
        		data["url"] = '/Guide/BaoZhangCaoZuo.aspx?' + $.param({
        				type: "delGuidMoneyIn",
        				sl: AccountedForPage._sl,
        				OrderId: Id,
        				tourId: AccountedForPage._tourId
        			});
        		data["data"] = obj.closest("tr").find("input,textarea").serialize();
        		data["success"] = function(ret) {
        			if (ret.result == "1") {
        				tableToolbar._showMsg(ret.msg);
        				obj.parent().parent().css("display", "none");
        			} else {
        				tableToolbar._showMsg(ret.msg);
        			}
        		}
        		this._ajax(data);
        	},
        	UnBind: function() {
        		var obj = $("#ul_btn_list a");
        		obj.css({ "background-position": "0 -62px" });
        		obj.unbind("click");
        	},
        	BindButtBtn: function() { /*绑定底部大按钮*/
        		var that = this;
        		//提交财务
        		var obj = $("#a_OperaterExamineV");
        		obj.css({ "background-position": "0 0px" });
        		obj.unbind("click").click(function() {
        			that.UnBind();
        			var data = { };
        			data["url"] = '/Guide/BaoZhangCaoZuo.aspx?' + $.param({
        					type: "OperaterExamineV",
        					sl: that._sl,
        					tourId: that._tourId
        				})
        			that._ajax(data);
        			return false;
        		})
        		//导游变更
        		obj = $("#DaoYouBianGeng");
        		obj.css({ "background-position": "0 0px" });
        		obj.unbind("click").click(function() {
        			Boxy.iframeDialog({
        					iframeUrl: '/Guide/DaoYouBianGeng.aspx?sl=<%=this.SL %>&tourid=' + that._tourId + '&tourcode=' + $("#<%=lbl_TourCode.ClientID %>").html(),
        					title: "导游变更",
        					modal: true,
        					width: "600",
        					height: "350",
        					draggable: true
        				});
        		});
        		//报销完成
        		obj = $("#a_ApplyOver");
        		obj.css({ "background-position": "0 0px" });
        		obj.unbind("click").click(function() {
        			that.UnBind();
        			var data = { };
        			data["getType"] = "get";
        			data["url"] = '/Guide/BaoZhangCaoZuo.aspx?' + $.param({
        					type: "ApplyOver",
        					sl: that._sl,
        					tourId: that._tourId
        				})
        			that._ajax(data);
        		})
        	},
        	_BindBtn: function() { /*绑定页面按钮*/
        		var that = this;
        		//团款确认单                
        		$(".addContent-box").find("a[data-class='orderInfo']").unbind("click").click(function() {
        			var tourType = $(this).attr("data-tourType");
        			var orderId = $(this).attr("data-OrderId");
        			parent.Boxy.iframeDialog({
        					title: "团款确认单",
        					iframeUrl: "/CommonPage/tourMoneyStatements.aspx?" +
        					$.param({
        							tourType: tourType,
        							OrderId: orderId,
        							sl: AccountedForPage._sl,
        							action: 2,
        							tourId: AccountedForPage._tourId
        						}),
        					width: "715px",
        					height: "820px",
        					draggable: true
        				});
        			return false;
        		});
        		//购物收入
        		$("#tblGouWuShouRu").find("[data-class='updgouwu']").unbind("click");
        		$("#tblGouWuShouRu").find("[data-class='updgouwu']").click(function() {
        			var id = $(this).attr("data-planid");
        			var sourceid = $(this).attr("data-sourceid");
        			if (id) {
        				Boxy.iframeDialog({
        						iframeUrl: '/Guide/BaoZhangGouWu.aspx?sl=<%=this.SL %>&planid=' + id + '&sourceid=' + sourceid,
        						title: "交给公司",
        						modal: true,
        						width: "1000",
        						height: "1000",
        						draggable: true
        					});
        			}
        			return false;
        		});
        		//其它收入 修改
        		$("#tabFreeItemList").find("[data-class='updateFreeItem']").unbind("click");
        		$("#tabFreeItemList").find("[data-class='updateFreeItem']").click(function() {
        			var id = $(this).attr("data-ID");
        			if (id) {
        				AccountedForPage.Save($(this), id,"saveFreeItem");
        			}
        			return false;
        		});

        		//其它收入 删除
        		$("#tabFreeItemList").find("[data-class='deleteFreeItem']").unbind("click");
        		$("#tabFreeItemList").find("[data-class='deleteFreeItem']").click(function() {
        			var id = $(this).attr("data-ID");
        			if (id) {
        				AccountedForPage.DeleteFreeItem($(this), id,"DeleteFreeItem");
        			}
        			return false;
        		});

        		//其它收入 添加 
        		$("#tabFreeItemList").find("[data-class='addOtherFreeItem']").unbind("click");
        		$("#tabFreeItemList").find("[data-class='addOtherFreeItem']").click(function() {
        			AccountedForPage.Save($(this), "","saveFreeItem");
        			return false;
        		});
        		
        		//其它支出 修改
        		$("#tblQiTaZhiChu").find("[data-class='updateFreeItem']").unbind("click");
        		$("#tblQiTaZhiChu").find("[data-class='updateFreeItem']").click(function() {
        			var id = $(this).attr("data-ID");
        			if (id) {
        				AccountedForPage.Save($(this), id,"QiTaZhiChuEdit");
        			}
        			return false;
        		});

        		//其它支出 删除
        		$("#tblQiTaZhiChu").find("[data-class='deleteFreeItem']").unbind("click");
        		$("#tblQiTaZhiChu").find("[data-class='deleteFreeItem']").click(function() {
        			var id = $(this).attr("data-ID");
        			if (id) {
        				AccountedForPage.DeleteFreeItem($(this), id,"DelQiTaZhiChu");
        			}
        			return false;
        		});

        		//其它支出 添加 
        		$("#tblQiTaZhiChu").find("[data-class='addOtherFreeItem']").unbind("click");
        		$("#tblQiTaZhiChu").find("[data-class='addOtherFreeItem']").click(function() {
        			AccountedForPage.Save($(this), "","QiTaZhiChuEdit");
        			return false;
        		});

        		//导游收入  修改
        		$("#tabGuidMoneyIn").find("[data-class='updateOrder']").unbind("click");
        		$("#tabGuidMoneyIn").find("[data-class='updateOrder']").click(function() {
        			var id = $(this).attr("data-OrderID");
        			if (id) {
        				AccountedForPage.SaveGuidMoneyIn($(this), id, "update");
        			}
        			return false;
        		});

        		that.BindButtBtn();

        	},
        	PageInit: function() {
        		AccountedForPage._BindBtn();
        		var hidOrderIdLength = $("#tabGuidMoneyIn").find("input[type='hidden'][name='OrderIdhid']").length;
        		var hidOrderIdArr = "";
        		if (hidOrderIdLength > 0) {
        			for (var i = 0; i < hidOrderIdLength; i++) {
        				hidOrderIdArr += $("#tabGuidMoneyIn").find("input[type='hidden'][name='OrderIdhid']").eq(i).val() + ",";
        			}
        		}
        	}
        };

        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            }
        };

        $(document).ready(function() {
            AccountedForPage.PageInit();
        });
    </script>

</asp:Content>

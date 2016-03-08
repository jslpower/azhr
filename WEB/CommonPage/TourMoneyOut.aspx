<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TourMoneyOut.aspx.cs" Inherits="Web.CommonPage.TourMoneyOut" %>
<%@ Import Namespace="EyouSoft.Common" %>

<div id="div_tourMoneyOut" class="tablelist-box " style="width: 98.5%; margin: 0 auto;">
    <span class="formtableT">团队支出</span>
    <asp:panel id="pan_DiJie" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.地接%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30"><b>地<br />接</b></th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>地接社名称</strong></td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>接团时间</strong></td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>送团时间</strong></td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>人数</strong></td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>支付方式</strong></td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>预算费用</strong></td>
                    <td colspan="2" align="center" bgcolor="#D4ECF7" class="th-line" width="8%"><strong>计调变更</strong></td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>结算费用</strong></td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line title"><strong>操作</strong></td>
                </tr>
                <tr>
                  <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                </tr>
                <asp:Repeater ID="rpt_DiJie" runat="server">
                    <ItemTemplate>
                        <tr  data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>" data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="地接社名称不能为空!" class="inputtext formsize120" value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选用地接社!" value="<%#Eval("SourceId") %>" />
                            </td>
                            <td align="center">
                                <%#Eval("startdate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td align="center">
                                <%#Eval("enddate","{0:yyyy-MM-dd}") %>
                            </td>
                            <td align="center">
                                <input type="text" name="txt_num" valid="int" errmsg="人数必须是整数!" class="inputtext formsize40" value="<%#Eval("Num")%>" />
                            </td>
                            <td align="center">
                                <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred"><%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" class="inputtext formsize40" name="txt_confirmation" valid="decimal"
                                    errmsg="结算费用必须数字" style="background-color:#dadada;" readonly="readonly" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                                
                            </td>
                            <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                            <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center"><%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddDiJie" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="地接社名称不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选用地接社!"
                                value="" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="int" errmsg="人数必须是整数!" class="inputtext formsize40" />
                        </td>
                        <%--<td align="left">
                            <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" />
                        </td>--%>
                        <td align="center"> 
                            <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                        <td align="right">
                            <input type="text" class="inputtext formsize40" name="txt_confirmation" valid="decimal"
                                errmsg="结算费用必须数字" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_DaoYou" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0" data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游%>" data-operatorid='<%#Eval("OperatorId") %>'>
            <tr>
                <th rowspan="500" class="th-line w30"><b>导<br />游</b></th>
                <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>导游姓名</strong></td>
                <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>上团时间</strong></td>
                <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>上团地点</strong></td>
                <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>下团时间</strong></td>
                <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>下团地点</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>支付方式</strong></td>
                <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line"><strong>预算费用</strong></td>
                <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line"><strong>结算费用</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line title"><strong>操作</strong></td>
            </tr>
            <tr>
                <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
            </tr>
            <asp:Repeater ID="rpt_DaoYou" runat="server"><ItemTemplate>
            <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>" data-operatorid="<%#Eval("OperatorId") %>">
                <td align="left"><input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="导游名称不能为空!" class="inputtext formsize120" value="<%#Eval("SourceName")%>" /><a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a><input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选用导游!" value="<%#Eval("SourceId") %>" /></td>
                <td align="center"><%#Eval("startdate","{0:yyyy-MM-dd}") %></td>
                <td align="left"><%#Eval("PlanGuide.OnLocation")%></td>
                <td align="center"><%#Eval("enddate","{0:yyyy-MM-dd}") %></td>
                <td align="left"><%#Eval("PlanGuide.NextLocation")%></td>
                <td align="center"><%#GetPaymentStr((int)Eval("PaymentType")) %></td>
                <td align="right"><b class="fontred"><%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b></td>
                <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                <td align="right"><input type="text" class="inputtext formsize40" name="txt_confirmation" style="background-color:#dadada;" readonly="readonly" valid="decimal" errmsg="结算费用必须数字" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" /><b class="fontred"></b></td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                <td data-class="td_operate" data-addstatus="<%#(int)(int)Eval("AddStatus") %>" align="center"><%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%></td>
            </tr></ItemTemplate></asp:Repeater>
            <asp:Panel ID="pan_AddDaoYou" runat="server">
            <tr>
                <td align="left"><input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="导游名称不能为空!" class="inputtext formsize120" /><a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a><input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选用导游!" value="" /></td>
                <td align="left"><input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" /></td>
                <td align="center"><%=GetPaymentStr(-1) %></td>
                <td align="right"><b class="fontred"><%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b></td>
                <td align=center>—</td><td align=center>—</td>
                <td align="right"><input type="text" class="inputtext formsize40" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" /></td>
                <td data-class="td_operate" align="center"><a href="javascript:void(0);" class="addbtn" data-class="a_Add"><img src="/images/addimg.gif" border="0" /></a></td>
            </tr>
            </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_JiuDian" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.酒店%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>酒<br />
                            店</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>酒店名称</strong>
                    </td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>入住时间</strong>
                    </td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>离店时间</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>房间数量</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>免房数量</strong>
                    </td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
                    <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
                  <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                </tr>
                <asp:Repeater ID="rpt_JiuDian" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="酒店名称不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选用酒店!" value="<%#Eval("SourceId") %>" />
                            </td>
                            <td align="center"><%#Eval("startdate","{0:yyyy-MM-dd}") %></td>
                            <td align="center"><%#Eval("enddate","{0:yyyy-MM-dd}") %></td>
                            <td align="center">
                                <input type="text" name="txt_num" valid="int" errmsg="房间数量不正确,请输入整数!" class="inputtext formsize40"
                                    value="<%#Eval("Num") %>" />
                            </td>
                            <td align="center"><%#Eval("PlanHotel.FreeNumber")%></td>
                            <td align="center">
                               <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" class="inputtext formsize40" name="txt_confirmation" style="background-color:#dadada;" readonly="readonly" valid="decimal"
                                    errmsg="结算费用必须数字" value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                               
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddJiuDian" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="酒店名称不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选用酒店!" value="" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="int" errmsg="房间数量不正确,请输入整数!" class="inputtext formsize40" />
                        </td>
                        <!--<td align="center">
                            <input type="text" name="txt_freeNumber" valid="int" errmsg="免费房数不正确,请输入整数!" class="inputtext formsize40" />
                        </td>-->
                        <td align="left">
                            <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" />
                        </td>
                        <td align="center">
                          <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                        
                        <td align="right">
                            <input type="text" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_CheDui" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用车%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>车<br />
                            队</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>车队名称</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>数量</strong>
                    </td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                     
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
                  <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                </tr>
                <asp:Repeater ID="rpt_CheDui" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="车队名称不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选用车队!" value="<%#Eval("SourceId")%>" />
                            </td>
                            <td align="center">
                                <input type="text" name="txt_num" valid="int" errmsg="数量不正确,请输入整数!" class="inputtext formsize40"
                                    value="<%#Eval("Num")%>" />
                            </td>
                            <td align="center">
                                <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" name="txt_confirmation" valid="decimal" style="background-color:#dadada;" readonly="readonly" errmsg="结算费用必须数字" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                                
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddCheDui" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="车队名称不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选用车队!" value="" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="int" errmsg="数量不正确,请输入整数!" class="inputtext formsize40" />
                        </td>
                        <td align="left">
                            <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" />
                        </td>
                        <td align="center">
                            <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                        
                        <td align="right">
                            <input type="text" class="inputtext formsize40" name="txt_confirmation" valid="decimal"
                                errmsg="结算费用必须数字" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_FeiJi" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.飞机%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>飞<br />
                            机</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>出票点</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>出票数</strong>
                    </td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                </tr>
                <asp:Repeater ID="rpt_FeiJi" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="飞机售票点不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择飞机售票点!"
                                    value="<%#Eval("SourceId") %>" />
                            </td>
                            <td align="center">
                                <input type="text" name="txt_num" valid="int" errmsg="出票数不正确,请输入整数!" class="inputtext formsize40"
                                    value="<%#Eval("Num")%>" />
                            </td>
                            <td align="center">
                                <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" name="txt_confirmation" valid="decimal" style="background-color:#dadada;" readonly="readonly" errmsg="结算费用必须数字" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                                
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddFeiJi" runat="server">
                </asp:Panel>
                <tr>
                    <td align="left">
                        <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="飞机售票点不能为空!" class="inputtext formsize120" />
                        <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                        <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择飞机售票点!"
                            value="" />
                    </td>
                    <td align="center">
                        <input type="text" name="txt_num" valid="int" errmsg="出票数不正确,请输入整数!" class="inputtext formsize40" />
                    </td>
                    <td align="left">
                        <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" />
                    </td>
                    <td align="center">
                       <%=GetPaymentStr(-1) %>
                    </td>
                    <td align="right">
                        <b class="fontred">
                            <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                    </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                    
                    <td align="right">
                        <input type="text" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40" />
                    </td>
                    <td data-class="td_operate" align="center">
                        <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                            <img src="/images/addimg.gif" border="0" /></a>
                    </td>
                </tr>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_HuoChe" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.火车%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>火<br />
                            车</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>出票点</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>张数</strong>
                    </td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                </tr>
                <asp:Repeater ID="rpt_HuoChe" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="火车售票点不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择火车售票点!"
                                    value="<%#Eval("SourceId") %>" />
                            </td>
                            <td align="center" data-special="/">
                                <input type="text" name="txt_num" valid="int" errmsg="张数不正确,请输入整数!" class="inputtext"
                                    style="width: 18px;" value="<%#Eval("Num")%>" />
                            </td>
                            <td align="center">
                                <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" name="txt_confirmation" style="background-color:#dadada;" readonly="readonly" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                                
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddHuoChe" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="火车售票点不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择火车售票点!"
                                value="" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="RegInteger" errmsg="张数不正确,请输入整数!" class="inputtext"
                                style="width: 18px;" />
                        </td>
                        <%--<td align="left">
                            <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" />
                        </td>--%>
                        <td align="center">
                           <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                        
                        <td align="right">
                            <input type="text" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_QiChe" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.汽车%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>汽<br />
                            车</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>出票点</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>张数</strong>
                    </td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
                  <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                </tr>
                <asp:Repeater ID="rpt_QiChe" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="汽车售票点不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择汽车售票点!"
                                    value="<%#Eval("SourceId") %>" />
                            </td>
                            <td align="center">
                                <input type="text" name="txt_num" valid="int" errmsg="张数不正确,请输入整数!" class="inputtext formsize40"
                                    value="<%#Eval("Num")%>" />
                            </td>
                            <%--<td align="left">
                                <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" value="<%#Eval("CostDetail")%>" />
                            </td>--%>
                            <td align="center">
                              <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" name="txt_confirmation" style="background-color:#dadada;" readonly="readonly" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                                
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddQiChe" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="汽车售票点不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择汽车售票点!"
                                value="" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="int" errmsg="张数不正确,请输入整数!" class="inputtext formsize40" />
                        </td>
                        <%--<td align="left">
                            <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" />
                        </td>--%>
                        <td align="center">
                            <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                       
                        <td align="right">
                            <input type="text" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_GuoNeiYouLun" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.轮船%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>
                            轮<br />
                            船</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>出票点</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>人数</strong>
                    </td>
                    <%--<td rowspan="2" align="left" bgcolor="#D4ECF7" class="th-line">
                        <strong>费用明细</strong>
                    </td>--%>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
<%--                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>导游变更</strong></td>
                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>销售变更</strong></td>
--%>                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                     
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
<%--                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
--%>                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                    
                </tr>
                <asp:Repeater ID="rpt_GuoNeiYouLun" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="游船公司不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择游船公司!"
                                    value="<%#Eval("SourceId") %>" />
                            </td>
                            <td align="center" data-special="+">
                                <input type="text" name="txt_num" valid="isMoney" errmsg="请填写正确的人数" class="inputtext formsize40" value="<%#Eval("Num")%>" />
                            </td>
                            <%--<td align="left">
                                <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%;" value="<%#Eval("CostDetail")%>" />
                            </td>--%>
                            <td align="center">
                               <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" name="txt_confirmation" style="background-color:#dadada;" readonly="readonly" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddGuoNeiYouLun" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="游船公司不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择游船公司!"
                                value="" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="isMoney" errmsg="请填写正确的人数!" class="inputtext formsize40" />
                        </td>
                        <%--<td align="left">
                            <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%;" />
                        </td>--%>
                        <td align="center">
                            <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                        
                        <td align="right">
                            <input type="text" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_JinDian" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.景点%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>景<br />
                            点</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>景点名称</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>人数</strong>
                    </td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
                  <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                </tr>
                <asp:Repeater ID="rpt_JinDian" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="景点名称不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择景点名称!"
                                    value="<%#Eval("SourceId") %>" />
                            </td>
                            <td align="center" data-special="+">
                                <input type="text" name="txt_num" valid="int" errmsg="人数必须是整数!" class="inputtext formsize40" value="<%#Eval("Num")%>" />
                            </td>
                            <%--<td align="left">
                                <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" value="<%#Eval("CostDetail")%>" />
                            </td>--%>
                            <td align="center">
                                <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" name="txt_confirmation" style="background-color:#dadada;" readonly="readonly" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                                
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddJinDian" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="景点名称不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择景点名称!"
                                value="" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="int" errmsg="人数必须是整数!" class="inputtext formsize40" />
                        </td>
                       <%-- <td align="left">
                            <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" />
                        </td>--%>
                        <td align="center">
                           <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                        
                        <td align="right">
                            <input type="text" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_YongCan" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.用餐%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>用<br />
                            餐</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>餐馆名称</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>人数</strong>
                    </td>
                    <%--<td rowspan="2" align="left" bgcolor="#D4ECF7" class="th-line">
                        <strong>费用明细</strong>
                    </td>--%>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
<%--                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>导游变更</strong></td>
                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>销售变更</strong></td>
--%>                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                     
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
<%--                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
--%>                   
                </tr>
                <asp:Repeater ID="rpt_YongCan" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="餐馆名称不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择餐馆名称!"
                                    value="<%#Eval("SourceId") %>" />
                            </td>
                            <td align="center">
                                <input type="text" name="txt_num" class="inputtext" valid="int" errmsg="人数必须是整数!"
                                    style="width: 30px;" value="<%#Eval("Num")%>" />
                            </td>
                            <%--<td align="left">
                                <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" value="<%#Eval("CostDetail")%>" />
                            </td>--%>
                            <td align="center">
                               <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" name="txt_confirmation" style="background-color:#dadada;" readonly="readonly" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddYongCan" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="餐馆名称不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择餐馆名称!"
                                value="" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="int" errmsg="人数必须是整数!" class="inputtext"
                                style="width: 30px;" />
                        </td>
                        <%--<td align="left">
                            <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" />
                        </td>--%>
                        <td align="center">
                           <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                        
                        <td align="right">
                            <input type="text" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_LinLiao" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料%>">
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>领<br />
                            料</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>领料内容</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>数量</strong>
                    </td>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>领料人</strong>
                    </td>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>单价</strong>
                    </td>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
<%--                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>导游变更</strong></td>
                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>销售变更</strong></td>
--%>                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                     
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
<%--                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
--%>                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                    
                </tr>
                <asp:Repeater ID="rpt_LingLiao" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>" data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="领料内容不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择领料内容!"
                                    value="<%#Eval("PlanGood")!=null?((EyouSoft.Model.GovStructure.MGovGoodUse)Eval("PlanGood")).GoodId:"" %>" />
                            </td>
                            <td align="center">
                                <input type="text" name="txt_num" valid="int" errmsg="领料必须是整数!" class="inputtext"
                                    style="width: 30px;" value="<%#Eval("Num")%>" />
                            </td>
                            <td align="center">
                                <input type="text" name="txt_contactName" class="inputtext formsize80" valid="required"
                                    errmsg="领料人不能为空!" value="<%#Eval("ContactName")%>" />
                                <a href="javascript:void(0);" class="xuanyong" data-class="a_contactName"></a>
                                <input type="hidden" data-class="hd_Id" name="txt_UserId" valid="required" errmsg="请使用选用功能,选择领料人!" value="<%#Eval("PlanGood")!=null?((EyouSoft.Model.GovStructure.MGovGoodUse)Eval("PlanGood")).UserId:"" %>" />
                            </td>
                            <td align="center">
                           
                                <input type="text" name="txt_costDetail" valid="decimal" errmsg="领料单价必须是数字" class="inputtext formsize50"
                                    value="<%# Utils.FilterEndOfTheZeroDecimal(Utils.GetDecimal(Eval("ContactFax").ToString())) %>" />
                            </td>
                            <td align="center">
                               <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" name="txt_confirmation" style="background-color:#dadada;" readonly="readonly" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>   
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddLinLiao" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="领料内容不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择领料内容!" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="int" data-sum="+" errmsg="领料必须是整数!" class="inputtext"
                                style="width: 30px;" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_contactName" class="inputtext formsize80" valid="required"
                                errmsg="领料人不能为空!" value="<%=SiteUserInfo.Name %>" />
                            <a href="javascript:void(0);" class="xuanyong" data-class="a_contactName"></a>
                            <input type="hidden" data-class="hd_Id" name="txt_UserId" valid="required" errmsg="请使用选用功能,选择领料人!" value="<%= SiteUserInfo.UserId%>" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_costDetail" class="inputtext formsize50" valid="decimal"
                                data-sum="+" errmsg="领料单价必须是数字!" />
                        </td>
                        <td align="center">
                          <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                        
                        <td align="right">
                            <input type="text" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
            <div class="hr_5">
            </div>
    </asp:panel>
    <asp:panel id="pan_QiTa" runat="server">
            <table data-class="tab_list" width="100%" border="0" cellspacing="0" cellpadding="0"
                data-addtype="<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.其它%>"
                data-operatorid='<%#Eval("OperatorId") %>'>
                <tr>
                    <th rowspan="500" class="th-line w30">
                        <b>其<br />
                            它</b>
                    </th>
                    <td rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>供应商名称</strong>
                    </td>
                    <td width="7%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>人数</strong>
                    </td>
                    <%--<td rowspan="2" align="left" bgcolor="#D4ECF7" class="th-line">
                        <strong>支出项目</strong>
                    </td>--%>
                    <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line">
                        <strong>支付方式</strong>
                    </td>
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>预算费用</strong>
                    </td>
<%--                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>导游变更</strong></td>
                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>销售变更</strong></td>
--%>                     <td colspan=2 align=center bgcolor=#D4ECF7 class=th-line width=8%><strong>计调变更</strong></td>
                     
                    <td width="8%" rowspan="2" align="right" bgcolor="#D4ECF7" class="th-line">
                        <strong>结算费用</strong>
                    </td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>已付金额</strong></td>
                <td width="8%" rowspan="2" align="center" bgcolor="#D4ECF7" class="th-line"><strong>未付金额</strong></td>
                    <td data-class="td_operate" width="12%" rowspan="2" align="center" bgcolor="#D4ECF7"
                        class="th-line title">
                        <strong>操作</strong>
                    </td>
                </tr>
                <tr>
<%--                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
--%>                   <td width=4% align=center bgcolor=#D4ECF7 class=th-line>增</td><td width=4% align=center bgcolor=#D4ECF7 class=th-line>减</td>
                    
                </tr>
                <asp:Repeater ID="rpt_QiTa" runat="server">
                    <ItemTemplate>
                        <tr data-contactname="<%#Eval("ContactName") %>" data-contactphone="<%#Eval("ContactPhone") %>" data-planid="<%#Eval("PlanId") %>" data-addstatus="<%#(int)Eval("AddStatus") %>"
                            data-operatorid="<%#Eval("OperatorId") %>">
                            <td align="left">
                                <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="供应商名称不能为空!" class="inputtext formsize120"
                                    value="<%#Eval("SourceName")%>" />
                                <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                                <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择供应商名称!"
                                    value="<%#Eval("SourceId") %>" />
                            </td>
                            <td align="center">
                                <input type="text" name="txt_num" valid="int" errmsg="人数必须是整数!" class="inputtext formsize50"
                                    value="<%#Eval("Num")%>" />
                            </td>
                            <%--<td align="left">
                                <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" value="<%#Eval("CostDetail")%>" />
                            </td>--%>
                            <td align="center">
                               <%#GetPaymentStr((int)Eval("PaymentType")) %>
                            </td>
                            <td align="right">
                                <b class="fontred">
                                    <%# EyouSoft.Common.UtilsCommons.GetMoneyString( Eval("PlanCost"),ProviderToMoney)%></b>
                            </td>
                            <%#GetBianGengHtml(Eval("PlanCostChange"))%>
                            <td align="right">
                                <input type="text" name="txt_confirmation" style="background-color:#dadada;" readonly="readonly" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40"
                                    value="<%#EyouSoft.Common.Utils.FilterEndOfTheZeroDecimal((decimal)Eval("Confirmation")) %>" />
                            </td>
                <td align="right"><b class="fontred"><%#Eval("prepaid","{0:C2}") %></b></td>
                <td align="right"><b class="fontblue"><%#Eval("unpaid","{0:C2}") %></b></td>
                            <td data-class="td_operate" data-addstatus="<%#(int)Eval("AddStatus") %>" align="center">
                                 <%#GetOperate((int)Eval("AddStatus"), (int)Eval("PaymentType"))%>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_AddQiTa" runat="server">
                    <tr>
                        <td align="left">
                            <input type="text" name="txt_sourceName" style="background-color:#dadada;" readonly="readonly" valid="required" errmsg="供应商名称不能为空!" class="inputtext formsize120" />
                            <a data-class="a_sourceNameBtn" href="javascript:void(0);" class="xuanyong"></a>
                            <input type="hidden" data-class="hd_Id" name="hd_sourceId" valid="required" errmsg="请使用选用功能,选择供应商名称!"
                                value="" />
                        </td>
                        <td align="center">
                            <input type="text" name="txt_num" valid="int" errmsg="人数必须是整数!" class="inputtext formsize50" />
                        </td>
                        <%--<td align="left">
                            <input type="text" name="txt_costDetail" class="inputtext" style="width: 90%" />
                        </td>--%>
                        <td align="center">
                            <%=GetPaymentStr(-1) %>
                        </td>
                        <td align="right">
                            <b class="fontred">
                                <%=EyouSoft.Common.UtilsCommons.GetMoneyString(0, ProviderToMoney)%></b>
                        </td>
                        <td align=center>—</td><td align=center>—</td><%--<td align=center>—</td><td align=center>—</td><td align=center>—</td><td align=center>—</td>--%>
                        
                        <td align="right">
                            <input type="text" name="txt_confirmation" valid="decimal" errmsg="结算费用必须数字" class="inputtext formsize40" />
                        </td>
                        <td data-class="td_operate" align="center">
                            <a href="javascript:void(0);" class="addbtn" data-class="a_Add">
                                <img src="/images/addimg.gif" border="0" /></a>
                        </td>
                    </tr>
                </asp:Panel>
            </table>
    </asp:panel>
    <asp:panel id="pan_Msg" runat="server" visible="false">
            <table width=" 100%" border="0" cellspacing="0" cellpadding="0">
                <tr align="center">
                    <td>
                        暂无团队支出数据!
                    </td>
                </tr>
            </table>
        </asp:panel>
</div>

<script type="text/javascript">
    var PageTourMoneyOut = {
        _isShowOperate: '<%=IsShowOperate %>',
        _sourceNameBtn: null,
        _removeList: {
            'objToHtml': function() {
                $("table[data-class='tab_list'] input[type='text']").each(function() {
                    var obj = $(this);
                    obj.after(obj.val());
                    if (obj.next("a").length > 0) {
                        obj.next("a").remove();
                    }
                    obj.remove();
                })
//                $(":selected").each(function() {
//                    var obj = $(this);
//                    obj.closest("td").html(obj.html())
//                })
            }
        },
        Contact: {
            ContactName: "",
            ContactPhone: ""
        },
        PageInit: function() {
        	//return
            var that = this;
            that._isShowOperate = parseInt(this._isShowOperate) ? parseInt(this._isShowOperate) : -1

            //选中支付方式
            $("select[data-paymenttype]").each(function() {
                var obj = $(this);
                obj.val(obj.attr("data-paymenttype"));
            })
            if (that._isShowOperate === -1) {
            	
                //移除操作列
                that._removeList['objToHtml']();
                $("table[data-class='tab_list']").each(function() {
                    var obj = $(this).find("tr:last");
                    if ('<%=ParentType %>'.length > 0) {
                        obj.attr("align", "center");
                        obj.html("<td colspan=20> </td>")
                    }
                    else {
                        $("td[data-class='td_operate']").remove();
                        obj.remove();
                    }
                })
            }
            //供应商选用
            $("a[data-class='a_sourceNameBtn']").click(function() {
                that._sourceNameBtn = this;
                that.SourceNameBtnClick();
                return false;
            });
            $("a[data-class='a_contactName']").click(function() {
                that._sourceNameBtn = this;
                that.SourceNameBtn.LingLiao();
                return false;
            })
            $("input[type='text'][data-sum]").change(function() {
                var sum = 1;
                var obj = $(this).closest("tr");
                obj.find("[type='text'][data-sum]").each(function() {
                    sum = tableToolbar.calculate(sum, $(this).val(), "*");
                })
                obj.find("[type='text'][name='txt_confirmation']").val(sum);
            })
        },
        SourceNameBtnClick: function() {
            var that = this;
            var obj = $(that._sourceNameBtn);
            var suppliertype = obj.closest("table").attr("data-addtype");
            var supplierid = obj.closest("td").find(":hidden").val();
            if (that.SourceNameBtn[suppliertype]) {
                that.SourceNameBtn[suppliertype](supplierid);
            }
            else {
                that.SourceNameBtn.GongYingShang(suppliertype, supplierid, $.trim(obj.closest("table").find("tr th:eq(0)").text()));
            }
            return false;
        },
        SourceNameBtn: {
            BoxyiframeDialog: function(url, title) {
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: title,
                    modal: true,
                    width: "700px",
                    height: "400px"
                });
            },
            "<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.导游%>": function(supplierid) {
                var url = "/CommonPage/OrderSells.aspx?" + $.param({
                    callBackFun: "PageTourMoneyOut.SourceNameBtnCallBack",
                    title: "导游-选用-",
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                });
                PageTourMoneyOut.SourceNameBtn.BoxyiframeDialog(url, "导游-选用-");

            },
            "<%=(int)EyouSoft.Model.EnumType.PlanStructure.PlanProject.领料%>": function(supplierid) {
                var url = "/CommonPage/SelectObject.aspx?" + $.param({
                    callBackFun: "PageTourMoneyOut.SourceNameBtnCallBack",
                    id: supplierid
                });
                PageTourMoneyOut.SourceNameBtn.BoxyiframeDialog(url, "领料-选用-");
            },
            "LingLiao": function() {
                var url = "/CommonPage/OrderSells.aspx?" + $.param({
                    callBackFun: "PageTourMoneyOut.SourceNameBtnCallBack",
                    title: "领料人-选用-"
                });
                PageTourMoneyOut.SourceNameBtn.BoxyiframeDialog(url, "领料人-选用-");
            },
            "GongYingShang": function(suppliertype, supplierid, title) {
                var url = "/CommonPage/UseSupplier.aspx?" + $.param({
                    suppliertype: suppliertype,
                    callBack: "PageTourMoneyOut.SourceNameBtnCallBack",
                    supplierid: supplierid
                });
                PageTourMoneyOut.SourceNameBtn.BoxyiframeDialog(url, title + "-选用-");
            }
        },
        SourceNameBtnCallBack: function(data) {
            if (data) {
                var that = this;
                var obj = $(that._sourceNameBtn).closest("td");
                obj.find(":text").val(data["name"] || data["text"]);
                obj.find("input[data-class='hd_Id']").val(data["value"] || data["id"]);
                var tr = obj.closest("tr");
                if (data["price"]) {
                    tr.find(":text[name='txt_costDetail']").val(data["price"]);
                }
                if (data["nums"]) {
                    tr.find(":text[name='txt_num']").attr("max", data["nums"][0] || "0");
                }
                tr.attr("data-contactname", data["contactname"] || data["text"]);
                tr.attr("data-contactphone", data["contacttel"] || data["phone"]);
            }
        },
        AddControl: function() {/*返回所有添加按钮*/
            return $("#div_tourMoneyOut a[data-class='a_Add']");
        },
        UpdataControl: function() {/*返回所有修改按钮*/
            return $("#div_tourMoneyOut a[data-class='a_Updata']");
        },
        DeleteControl: function() {/*返回所有删除按钮*/
            return $("#div_tourMoneyOut a[data-class='a_Del']");
        }
    }
</script>


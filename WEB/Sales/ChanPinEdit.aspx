<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChanPinEdit.aspx.cs" Inherits="EyouSoft.Web.Sales.ChanPinEdit"
    MasterPageFile="~/MasterPage/Front.Master" ValidateRequest="false" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Src="~/UserControl/TuanXingCheng.ascx" TagName="TuanXingCheng" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/TuanFengWeiCan.ascx" TagName="TuanFengWeiCan" TagPrefix="uc3" %>
<%@ Register Src="~/UserControl/TuanZiFei.ascx" TagName="TuanZiFei" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/TuanZengSong.ascx" TagName="TuanZengSong" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/TuanXiaoFei.ascx" TagName="TuanXiaoFei" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/TuanDiJie.ascx" TagName="TuanDiJie" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/TravelControl.ascx" TagName="TravelControl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectJourneySpot.ascx" TagName="SelectJourneySpot"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectJourney.ascx" TagName="SelectJourney" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectPriceRemark.ascx" TagName="SelectPriceRemark"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SupplierControl.ascx" TagName="SupplierControl" TagPrefix="uc4" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="CPHBODY" runat="server">
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="addContent-box">
            <table width="100%" cellpadding="0" cellspacing="0" class="firsttable">
                <tr>
                    <td class="addtableT">
                        团型：
                    </td>
                    <td class="kuang2">
                        <input type="hidden" name="hidtourmode" value="0" id="hidtourmode" />
                        <input type="hidden" name="hidTourModeValue" runat="server" id="hidTourModeValue" />
                        <select name="txtTuanXing" id="txtTuanXing" runat="server">
                            <option value="0">整团</option>
                            <option value="1">纯车</option>
                        </select>
                    </td>
                    <td class="addtableT">
                        团号：
                    </td>
                    <td class="kuang2" colspan="3">
                        <input name="txtTourCode" id="txtTourCode" type="text" class="formsize140 input-txt"
                            runat="server" readonly="readonly" style="background: #dadada;" />
                    </td>
                    <%--                    <td class="addtableT">
                    </td>
                    <td class="kuang2">
                    </td>
--%>
                </tr>
                <tr>
                    <td width="12%" class="addtableT">
                        客户团号：
                    </td>
                    <td class="kuang2">
                        <input type="text" class="formsize80 input-txt" id="txtKeHuTourCode" runat="server" />
                    </td>
                    <td width="12%" class="addtableT">
                        <font class="fontbsize12">* </font>团队名称：
                    </td>
                    <td class="kuang2" colspan="3">
                        <input type="text" class="formsize180 input-txt" id="txtRouteName" runat="server"
                            valid="required" errmsg="请输入团队名称!" />
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        <font class="fontbsize12">* </font>天数：
                    </td>
                    <td class="kuang2">
                        <input type="text" class="formsize40 input-txt" id="txtTourDays" runat="server" valid="required|isInt|range"
                            min="1" errmsg="请输入天数!|请输入正确的天数!|天数必须大于0!" />
                        <button type="button" class="addtimebtn" id="btnAddDays">
                            增加日程</button>
                    </td>
                    <td class="addtableT">
                        <font class="fontbsize12">* </font>客户单位：
                    </td>
                    <td class="kuang2">
                        <uc1:CustomerUnitSelect runat="server" ID="txtKeHu" CallBack="CallBackCustomerUnit" />
                        <input type="hidden" value="" id="LvPrice" runat="server" />
                    </td>
                    <td class="addtableT">
                        <font class="fontbsize12">* </font>团队国籍/地区：
                    </td>
                    <td class="kuang2">
                        <select id="txtCountryId" name="txtCountryId" valid="required" errmsg="请选择国家">
                        </select>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        <font class="fontbsize12">* </font>抵达日期：
                    </td>
                    <td class="kuang2">
                        <input type="hidden" id="hidsdate" value="" />
                        <input type="text" class="formsize80 input-txt" id="txtLDate" runat="server" onfocus="WdatePicker()"
                            valid="required" errmsg="请输入抵达日期!" />
                    </td>
                    <td class="addtableT">
                        抵达城市：
                    </td>
                    <td class="kuang2">
                        <input name="txtDiDaChengShi" type="text" class="formsize80 input-txt" id="txtDiDaChengShi"
                            runat="server" />
                    </td>
                    <td class="addtableT">
                        航班/时间：
                    </td>
                    <td class="kuang2">
                        <input type="text" class="formsize180 input-txt" id="txtDiDaHangBan" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        <font class="fontbsize12">* </font>离开日期：
                    </td>
                    <td class="kuang2">
                        <input type="hidden" id="hidedate" value="" />
                        <input type="text" class="formsize80 input-txt" id="txtRDate" runat="server" onfocus="WdatePicker()"
                            valid="required" errmsg="请输入离开日期!" />
                    </td>
                    <td class="addtableT">
                        离开城市：
                    </td>
                    <td class="kuang2">
                        <input name="txtLiKaiChengShi" type="text" class="formsize80 input-txt" runat="server"
                            id="txtLiKaiChengShi" />
                    </td>
                    <td class="addtableT">
                        航班/时间：
                    </td>
                    <td class="kuang2">
                        <input type="text" class="formsize180 input-txt" id="txtLiKaiHangBan" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        <font class="fontbsize12">* </font>业务员：
                    </td>
                    <td class="kuang2">
                        <uc1:SellsSelect ID="txtXiaoShouYuan" runat="server" SetTitle="业务员" ReadOnly="true" />
                    </td>
                    <td class="addtableT">
                        <font class="fontbsize12">* </font>OP：
                    </td>
                    <td class="kuang2">
                        <uc1:SellsSelect ID="txtJiDiaoYuan" runat="server" SetTitle="OP" SMode="true" ReadOnly="true" />
                    </td>
                    <td class="addtableT">
                        <font class="fontbsize12">* </font>人数：
                    </td>
                    <td class="kuang2">
                        <img src="../images/chengren.gif" width="16" height="15" style="vertical-align: middle" />
                        成人
                        <input name="txtCR" data-class="heji" type="text" class="formsize40 input-txt" id="txtCR"
                            runat="server" valid="required|RegInteger" errmsg="请输入成人数!|请输入正确的成人数!" />
                        &nbsp;
                        <img src="../images/child.gif" style="vertical-align: middle" />
                        儿童
                        <input name="txtET" type="text" data-class="heji" class="formsize40 input-txt" id="txtET"
                            runat="server" />
                        &nbsp;
                        <img src="../images/lindui.gif" style="vertical-align: middle" />
                        领队
                        <input name="txtLD" type="text" data-class="heji" class="formsize40 input-txt" id="txtLD"
                            runat="server" />
                        司陪<input name="txtSiPei" type="text" data-class="heji" class="formsize40 input-txt"
                            id="txtSiPei" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        用房数：
                    </td>
                    <td colspan="5" class="kuang2">
                        <asp:Literal runat="server" ID="ltrFangXing"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        文件上传：
                    </td>
                    <td colspan="5" class="kuang2 pand4">
                        <uc1:UploadControl ID="txtFuJian1" FileTypes="*.jpg;*.gif;*.jpeg;*.png" runat="server"
                            IsUploadMore="true" IsUploadSelf="true" />
                    </td>
                </tr>
                <tr>
                    <td class="addtableT">
                        团队确认上传：
                    </td>
                    <td colspan="5" class="kuang2 pand4">
                        <uc1:UploadControl ID="txtFuJian2" FileTypes="*.jpg;*.gif;*.jpeg;*.png" runat="server"
                            IsUploadMore="false" IsUploadSelf="true" />
                    </td>
                </tr>
                <asp:PlaceHolder ID="PhDinnerPrice" runat="server">
                    <tr>
                        <td class="addtableT">
                            用餐默认价格：
                        </td>
                        <td class="kuang2 pand4" colspan="5">
                            早：<input type="text" class="Default_Price formsize40 inputtext " data-id="txtDefault_Breakfast"
                                value="" />&nbsp; 中：<input type="text" class="Default_Price formsize40 inputtext "
                                    data-id="txtDefault_Lunch" value="" />&nbsp; 晚：<input type="text" class="Default_Price formsize40 inputtext "
                                        data-id="txtDefault_Dinner" value="" />&nbsp;
                        </td>
                    </tr>
                </asp:PlaceHolder>
                <tr>
                    <td class="addtableT">
                        行程亮点：<uc1:SelectJourneySpot runat="server" ID="SelectJourneySpot1" />
                    </td>
                    <td colspan="5" class="kuang2 pand4">
                        <span id="spanPlanContent" style="display: inline-block;">
                            <asp:TextBox ID="txtLiangDian" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <uc2:TuanXingCheng ID="TuanXingCheng1" runat="server" />
        <div class="hr_10">
        </div>
        <div style="width: 98.5%" class="tablelist-box" id="TabList_Box">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <th align="right">
                            风味餐：
                        </th>
                        <td align="left">
                            <uc3:TuanFengWeiCan ID="TuanFengWeiCan1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            自费项目：
                        </th>
                        <td align="left">
                            <uc1:TuanZiFei runat="server" ID="TuanZiFei1" />
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            赠送：
                        </th>
                        <td align="left">
                            <uc1:TuanZengSong runat="server" ID="TuanZengSong1" />
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            小费：
                        </th>
                        <td align="left">
                            <uc1:TuanXiaoFei runat="server" ID="TuanXiaoFei1" />
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            个性服务要求：
                        </th>
                        <td align="left">
                            <span id="spanSpecificRequire" style="display: inline-block;">
                                <asp:TextBox ID="txtSpecificRequire" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            行程备注：<uc1:SelectJourney runat="server" ID="SelectJourney1" />
                        </th>
                        <td align="left">
                            <span id="spanJourney" style="display: inline-block;">
                                <asp:TextBox ID="txtJourney" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <uc1:TuanDiJie runat="server" ID="TuanDiJie1" />
        <div class="hr_10">
        </div>
        <div style="width: 98.5%; margin: 0 auto;" id="DivBaoJia">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td valign="top" align="left">
                            <span class="formtableT">成本信息</span>
                            <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia" id="DivItems">
                                <tbody>
                                    <tr>
                                        <th align="left">
                                            项目
                                        </th>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">飞机</span>
                                            <asp:TextBox data-UnitType="0" runat="server" ID="txt_Item_BTraffic" data-name="trafficPrice"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" alt="" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_BTrafficRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">火车</span>
                                            <asp:TextBox data-UnitType="0" runat="server" ID="txt_Item_BTrain" data-name="trainPrice"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" alt="" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_BTrainRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">汽车</span>
                                            <asp:TextBox data-UnitType="0" runat="server" ID="txt_Item_BBus" data-name="busPrice"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            <asp:DropDownList runat="server" ID="ddlItemCarTicketUnit">
                                                <asp:ListItem Value="0">元/人</asp:ListItem>
                                                <asp:ListItem Value="1">元/团</asp:ListItem>
                                            </asp:DropDownList>
                                            <img width="19" height="18" src="../images/bei.jpg" alt="" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_BBusRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">轮船</span>
                                            <asp:TextBox data-UnitType="0" runat="server" ID="txt_Item_BShip" data-name="shipPrice"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" alt="" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_BShipRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left" bgcolor="#dadada">
                                            <span style="width: 10%; display: inline-block">用车</span>
                                            <asp:TextBox runat="server" data-UnitType="0" ID="txt_Item_Car" data-name="carPrice"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            <asp:DropDownList runat="server" ID="ddlItemCarUnit">
                                                <asp:ListItem Value="0">元/人</asp:ListItem>
                                                <asp:ListItem Value="1">元/团</asp:ListItem>
                                            </asp:DropDownList>
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_CarRemark" CssClass="inputtext formsize180"
                                                    Text="车价*天数+过路过桥费+司机小费+司机房费+司机餐费+空驶费+其他"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">房</span>
                                            <asp:TextBox runat="server" data-UnitType="0" ID="txt_Item_Room1" data-name="hotel1Price"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            <asp:DropDownList runat="server" ID="ddlItemRoomUnit1">
                                                <asp:ListItem Value="0">元/人</asp:ListItem>
                                                <asp:ListItem Value="1">元/团</asp:ListItem>
                                            </asp:DropDownList>
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark">
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_RoomRemark1" CssClass="inputtext formsize180"
                                                    Text="所有行程中的酒店1价格累加"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">餐</span>
                                            <asp:TextBox runat="server" data-UnitType="0" ID="txt_Item_Dinner" data-name="canItemPrice"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_DinnerRemark" CssClass="inputtext formsize180"
                                                    Text="所有餐累加"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">导服</span>
                                            <asp:TextBox runat="server" data-UnitType="0" ID="txt_Item_Guide" CssClass="inputtext formsize50"></asp:TextBox>
                                            <asp:DropDownList runat="server" ID="ddlItemGuide">
                                                <asp:ListItem Value="0">元/人</asp:ListItem>
                                                <asp:ListItem Value="1">元/团</asp:ListItem>
                                            </asp:DropDownList>
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_GuideRemark" CssClass="inputtext formsize180"
                                                    Text="陪同房费+陪同餐费+补贴+来回交通+其他"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left" class="tdScenicCost">
                                            <span style="width: 10%; display: inline-block">景点</span>
                                            <asp:TextBox runat="server" data-UnitType="0" ID="txt_Item_Scenic" CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_ScenicRemark" CssClass="inputtext formsize180"
                                                    Text="所有景累加+自费项目的对外收取金额-减少成本金额"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">保险</span>
                                            <asp:TextBox runat="server" data-UnitType="0" ID="txt_Item_Insure" CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_InsureRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">小交通</span>
                                            <asp:TextBox runat="server" data-UnitType="0" ID="txt_Item_STraffic" CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_STrafficRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">综费</span>
                                            <asp:TextBox runat="server" data-UnitType="0" data-name="zongfei" ID="txt_Item_Sum"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_SumRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">其他</span>
                                            <asp:TextBox runat="server" data-UnitType="0" data-name="otherprice" ID="txt_Item_Other"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            <asp:DropDownList runat="server" ID="ddlItemQiTaUnit">
                                                <asp:ListItem Value="0">元/人</asp:ListItem>
                                                <asp:ListItem Value="1">元/团</asp:ListItem>
                                            </asp:DropDownList>
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_OtherRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr class="tempRow">
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">十六免一</span>
                                            <asp:TextBox runat="server" data-UnitType="1" ID="txt_Item_FreeOne" CssClass="inputtext formsize50"></asp:TextBox>
                                            元/团
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_FreeOneRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="hr_5">
                            </div>
                            <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia autoAdd" id="Tab_Item_Price">
                                <tbody>
                                    <tr>
                                        <th align="center">
                                            价格
                                        </th>
                                    </tr>
                                    <asp:PlaceHolder runat="server" ID="phItem">
                                        <tr class="tempRowitem">
                                            <td align="left" id="td5">
                                                <img width="16" height="15" align="absmiddle" src="../images/chengren.gif">
                                                成人价
                                                <input type="text" name="txt_Item_AdultPrice" class="inputtext formsize40" value="" />
                                                元/人
                                                <img align="absmiddle" src="../images/child.gif">
                                                儿童价
                                                <input type="text" name="txt_Item_ChildPrice" class="inputtext formsize40" value="" />
                                                元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                                领队
                                                <input type="text" name="txt_Item_LeadPrice" class="inputtext formsize40" value="" />
                                                元/人&nbsp;<br>
                                                单房差
                                                <input type="text" name="txt_Item_SingleRoomPrice" class="inputtext formsize40" value="" />
                                                元 其它
                                                <input type="text" name="txt_Item_OtherPrice" class="inputtext formsize40" value="" />
                                                元/团
                                            </td>
                                        </tr>
                                    </asp:PlaceHolder>
                                    <asp:Repeater runat="server" ID="rptItem">
                                        <ItemTemplate>
                                            <tr class="tempRowitem">
                                                <td align="left" id="td5">
                                                    <img width="16" height="15" align="absmiddle" src="../images/chengren.gif">
                                                    成人价
                                                    <input type="text" name="txt_Item_AdultPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>"
                                                        data-ordinal="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>" />
                                                    元/人
                                                    <img align="absmiddle" src="../images/child.gif">
                                                    儿童价
                                                    <input type="text" name="txt_Item_ChildPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>"
                                                        data-ordinal="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>" />
                                                    元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                                    领队
                                                    <input type="text" name="txt_Item_LeadPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("LeadPrice")).ToString("f2") %>" />
                                                    元/人&nbsp;<br>
                                                    单房差
                                                    <input type="text" name="txt_Item_SingleRoomPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("SingleRoomPrice")).ToString("f2") %>" />
                                                    元 其它
                                                    <input type="text" name="txt_Item_OtherPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("OtherPrice")).ToString("f2") %>" />
                                                    元/团
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </td>
                        <td width="10">
                        </td>
                        <td valign="top" align="left">
                            <span class="formtableT">价格信息</span>
                            <input type="hidden" value="1" id="hidItemType" runat="server" />
                            <input type="radio" <%=tourMode=="0"?"checked='checked'":"" %> onclick="PriceItemType(0)"
                                value="0" name="rdTourQuoteType" id="radzengtuan">
                            整团 &nbsp; &nbsp;
                            <input type="radio" <%=tourMode=="1"?"checked='checked'":"" %> value="1" onclick="PriceItemType(1)"
                                name="rdTourQuoteType" id="radfenxiang">
                            分项
                            <div <%=tourMode=="1"?"style='display:none'":"" %> id="divGroup">
                                <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia autoAdd" id="Tab_Group_Price">
                                    <tbody>
                                        <tr>
                                            <th align="center">
                                                价格
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="Phzengtuan">
                                            <tr class="tempRowgroup">
                                                <td align="left" id="td_Price_Price1">
                                                    <img width="16" height="15" align="absmiddle" src="../images/chengren.gif">
                                                    成人价
                                                    <input type="text" data-class="heji" name="txt_zengtuan_AdultPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元/人
                                                    <img align="absmiddle" src="../images/child.gif">
                                                    儿童价
                                                    <input type="text" data-class="heji" name="txt_zengtuan_ChildPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                                    领队
                                                    <input type="text" data-class="heji" name="txt_zengtuan_LeadPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元/人&nbsp;<br>
                                                    单房差
                                                    <input type="text" data-class="heji" name="txt_zengtuan_SingleRoomPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元 其它
                                                    <input type="text" data-class="heji" name="txt_zengtuan_OtherPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元/团
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:Repeater runat="server" ID="rptzengtuan">
                                            <ItemTemplate>
                                                <tr class="tempRowgroup">
                                                    <td align="left" id="td3">
                                                        <img width="16" height="15" align="absmiddle" src="../images/chengren.gif">
                                                        成人价
                                                        <input type="text" data-class="heji" name="txt_zengtuan_AdultPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>" data-ordinal="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>" />
                                                        元/人
                                                        <img align="absmiddle" src="../images/child.gif">
                                                        儿童价
                                                        <input type="text" data-class="heji" name="txt_zengtuan_ChildPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>" data-ordinal="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>" />
                                                        元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                                        领队
                                                        <input type="text" data-class="heji" name="txt_zengtuan_LeadPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("LeadPrice")).ToString("f2") %>" />
                                                        元/人&nbsp;<br>
                                                        单房差
                                                        <input type="text" data-class="heji" name="txt_zengtuan_SingleRoomPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("SingleRoomPrice")).ToString("f2") %>" />
                                                        元 其它
                                                        <input type="text" data-class="heji" name="txt_zengtuan_OtherPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("OtherPrice")).ToString("f2") %>" />
                                                        元/团
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <div class="hr_5">
                                </div>
                                <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia">
                                    <tbody>
                                        <tr>
                                            <th align="center">
                                                备注
                                            </th>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:TextBox runat="server" ID="TxtQuoteRemark" TextMode="MultiLine" Height="80px"
                                                    Width="95%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <div id="divprice" <%=tourMode=="0"?"style='display:none'":"" %>>
                                <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia" id="DivPrices">
                                    <tbody>
                                        <tr>
                                            <th align="left">
                                                项目
                                            </th>
                                        </tr>
                                        <tr>
                                            <td align="left">
                                                <span style="width: 10%; display: inline-block">飞机</span>
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_BTraffic" data-name="trafficPrice"
                                                    CssClass="inputtext formsize50"></asp:TextBox>
                                                元/人
                                                <img width="19" height="18" src="../images/bei.jpg" alt="" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_BTrafficRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">火车</span>
                                            <asp:TextBox data-UnitType="0" runat="server" ID="txt_Price_BTrain" data-name="trainPrice"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" alt="" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Price_BTrainRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">汽车</span>
                                            <asp:TextBox data-UnitType="0" runat="server" ID="txt_Price_BBus" data-name="busPrice"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                                <asp:DropDownList runat="server" ID="ddlPriceCarTicket">
                                                    <asp:ListItem Value="0">元/人</asp:ListItem>
                                                    <asp:ListItem Value="1">元/团</asp:ListItem>
                                                </asp:DropDownList>
                                            <img width="19" height="18" src="../images/bei.jpg" alt="" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Price_BBusRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <span style="width: 10%; display: inline-block">轮船</span>
                                            <asp:TextBox data-UnitType="0" runat="server" ID="txt_Price_BShip" data-name="shipPrice"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            元/人
                                            <img width="19" height="18" src="../images/bei.jpg" alt="" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Price_BShipRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                        <tr class="tempRow">
                                            <td align="left" bgcolor="#dadada">
                                                <span style="width: 10%; display: inline-block">用车</span>
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_Car" data-name="carPrice"
                                                    CssClass="inputtext formsize50"></asp:TextBox>
                                                <asp:DropDownList runat="server" ID="ddlPriceCar">
                                                    <asp:ListItem Value="0">元/人</asp:ListItem>
                                                    <asp:ListItem Value="1">元/团</asp:ListItem>
                                                </asp:DropDownList>
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_CarRemark" CssClass="inputtext formsize180"
                                                        Text="车价*天数+过路过桥费+司机小费+司机房费+司机餐费+空驶费+其他"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr class="tempRow">
                                            <td align="left">
                                                <span style="width: 10%; display: inline-block">房</span>
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_Room1" data-name="hotel1Price"
                                                    CssClass="inputtext formsize50"></asp:TextBox>
                                                <asp:DropDownList runat="server" ID="ddlPriceRoom1">
                                                    <asp:ListItem Value="0">元/人</asp:ListItem>
                                                    <asp:ListItem Value="1">元/团</asp:ListItem>
                                                </asp:DropDownList>
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark">
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_RoomRemark1" CssClass="inputtext formsize180"
                                                        Text="所有行程中的酒店1价格累加"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr class="tempRow">
                                            <td align="left">
                                                <span style="width: 10%; display: inline-block">餐</span>
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_Dinner" data-name="canPrice"
                                                    CssClass="inputtext formsize50"></asp:TextBox>
                                                元/人
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_DinnerRemark" CssClass="inputtext formsize180"
                                                        Text="所有餐累加"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr class="tempRow">
                                            <td align="left">
                                                <span style="width: 10%; display: inline-block">导服</span>
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_Guide" CssClass="inputtext formsize50"></asp:TextBox>
                                                <asp:DropDownList runat="server" ID="ddlPriceGuide">
                                                    <asp:ListItem Value="0">元/人</asp:ListItem>
                                                    <asp:ListItem Value="1">元/团</asp:ListItem>
                                                </asp:DropDownList>
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_GuideRemark" CssClass="inputtext formsize180"
                                                        Text="陪同房费+陪同餐费+补贴+来回交通+其他"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr class="tempRow">
                                            <td align="left" class="tdScenicPrice">
                                                <span style="width: 10%; display: inline-block">景点</span>
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_Scenic" CssClass="inputtext formsize50"></asp:TextBox>
                                                元/人
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_ScenicRemark" CssClass="inputtext formsize180"
                                                        Text="所有景累加+自费项目的对外收取金额-减少成本金额"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr class="tempRow">
                                            <td align="left">
                                                <span style="width: 10%; display: inline-block">保险</span>
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_Insure" CssClass="inputtext formsize50"></asp:TextBox>
                                                元/人
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_InsureRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr class="tempRow">
                                            <td align="left">
                                                <span style="width: 10%; display: inline-block">小交通</span>
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_STraffic" CssClass="inputtext formsize50"></asp:TextBox>
                                                元/人
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_STrafficRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr class="tempRow">
                                            <td align="left">
                                                <span style="width: 10%; display: inline-block">综费</span>
                                                <asp:TextBox runat="server" data-UnitType="0" data-name="zongfei" ID="txt_Price_Sum"
                                                    CssClass="inputtext formsize50"></asp:TextBox>
                                                元/人
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_SumRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr class="tempRow">
                                            <td align="left">
                                                <span style="width: 10%; display: inline-block">其他</span>
                                                <asp:TextBox runat="server" data-UnitType="0" data-name="otherprice" ID="txt_Price_Other"
                                                    CssClass="inputtext formsize50"></asp:TextBox>
                                                <asp:DropDownList runat="server" ID="ddlPriceQiTa">
                                                    <asp:ListItem Value="0">元/人</asp:ListItem>
                                                    <asp:ListItem Value="1">元/团</asp:ListItem>
                                                </asp:DropDownList>
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_OtherRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                        <tr class="tempRow">
                                            <td align="left">
                                                <span style="width: 10%; display: inline-block">十六免一</span>
                                                <asp:TextBox runat="server" data-UnitType="1" ID="txt_Price_FreeOne" CssClass="inputtext formsize50"></asp:TextBox>
                                                元/团
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_FreeOneRemark" CssClass="inputtext formsize180"></asp:TextBox>
                                                </span>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <div class="hr_5">
                                </div>
                                <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia autoAdd" id="Tab_Price_Price">
                                    <tbody>
                                        <tr>
                                            <th align="center">
                                                价格
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="PhPrice">
                                            <tr class="tempRowprice">
                                                <td align="left" id="td1">
                                                    <img width="16" height="15" align="absmiddle" src="../images/chengren.gif">
                                                    成人价
                                                    <input type="text" data-class="heji" name="txt_Price_AdultPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元/人
                                                    <img align="absmiddle" src="../images/child.gif">
                                                    儿童价
                                                    <input type="text" data-class="heji" name="txt_Price_ChildPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                                    领队
                                                    <input type="text" data-class="heji" name="txt_Price_LeadPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元/人&nbsp;<br>
                                                    单房差
                                                    <input type="text" data-class="heji" name="txt_Price_SingleRoomPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元 其它
                                                    <input type="text" data-class="heji" name="txt_Price_OtherPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元/团
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:Repeater runat="server" ID="rptPrice">
                                            <ItemTemplate>
                                                <tr class="tempRowprice">
                                                    <td align="left" id="td2">
                                                        <img width="16" height="15" align="absmiddle" src="../images/chengren.gif">
                                                        成人价
                                                        <input type="text" data-class="heji" name="txt_Price_AdultPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>" data-ordinal="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>" />
                                                        元/人
                                                        <img align="absmiddle" src="../images/child.gif">
                                                        儿童价
                                                        <input type="text" data-class="heji" name="txt_Price_ChildPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>" data-ordinal="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>" />
                                                        元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                                        领队
                                                        <input type="text" data-class="heji" name="txt_Price_LeadPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("LeadPrice")).ToString("f2") %>" />
                                                        元/人&nbsp;<br>
                                                        单房差
                                                        <input type="text" data-class="heji" name="txt_Price_SingleRoomPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("SingleRoomPrice")).ToString("f2") %>" />
                                                        元 其它
                                                        <input type="text" data-class="heji" name="txt_Price_OtherPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("OtherPrice")).ToString("f2") %>" />
                                                        元/团
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="hr_5">
        </div>
        <div style="width: 98.5%;" class="tablelist-box ">
            <span class="formtableT">应收金额</span>
            <table width="100%" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <th valign="middle">
                            导游现收
                        </th>
                        <%--<th valign="middle">
                            销售现收
                        </th>--%>
                        <th valign="middle">
                            合计金额
                        </th>
                    </tr>
                    <tr>
                        <td align="center">
                            <input type="text" class="formsize50 input-txt" id="txtDaoYouShouKuanJinE" runat="server">
                        </td>
                        <%-- <td align="center">
                            <input type="text" class="formsize50 input-txt" id="txtXiaoShouShouKuanJinE" runat="server">
                        </td>--%>
                        <td align="center">
                            <input type="text" class="formsize50 input-txt" id="txtHeJiJinE" runat="server" valid="required|isNumber"
                                errmsg="请输入合计金额!|请输入正确的合计金额!">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="addContent-box">
            <div class="hr_5">
            </div>
            <table width="100%" cellspacing="1" cellpadding="0" bgcolor="#5898B7" class="add-neibuxx">
                <tbody>
                    <tr>
                        <td class="addtableT da">
                            内部提醒信息（内部备注，提醒计调操作注意事项，不在行程单显示，在计调安排中显示）
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="100%" cellspacing="1" cellpadding="0">
                <tbody>
                    <tr>
                        <td>
                            <textarea style="width: 99%; height: 50px;" name="txtNeiBuXinXi" id="txtNeiBuXinXi"
                                runat="server"></textarea>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div style="width: 98.5%" class="tablelist-box ">
            <uc1:TravelControl runat="server" ID="YouKe" />
        </div>
        <div class="hr_10">
        </div>
        <div class="mainbox cunline fixed">
            <ul>
                <li class="cun-cy"><a href="javascript:void(0)" id="i_a_submit">保存</a></li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <div class="alertbox-outbox03" id="div_Change" style="display: none; padding-bottom: 0px;">
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        变更标题：
                    </td>
                    <td bgcolor="#E9F4F9" align="left">
                        <input type="text" id="txt_ChangeTitle" class="inputtext formsize600" style="height: 17px;"
                            name="txt_ChangeTitle">
                    </td>
                </tr>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        变更明细：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <textarea id="txt_ChangeRemark" style="height: 100px;" class="inputtext formsize600"
                            name="txt_ChangeRemark"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a hidefocus="true" href="javascript:void(0);" id="btnChangeSave"><s class="baochun">
            </s>保 存</a><a hidefocus="true" href="javascript:void(0);" onclick="AddPrice.BianGengBox.hide();return false;"><s
                class="chongzhi"></s>关闭</a>
        </div>
    </div>
    <input type="hidden" value="0" id="txtIsBianGeng" runat="server" />
    <input type="hidden" name="hidbinkemode" id="hidbinkemode" value="1" />
    </form>

    <script type="text/javascript">
        /*
        @分项整团分类显示
        1 整团
        2 分项
        */
        function PriceItemType(ItemType) {
            switch (ItemType) {
                case 0:
                    $("#<%=hidItemType.ClientID %>").val(0);
                    $("#divGroup").fadeIn("slow");
                    $("#divprice").hide();
                    $("#radzengtuan").attr("checked", "checked");

                    $("#radfenxiang").removeAttr("checked");
                    break;
                case 1:
                    $("#<%=hidItemType.ClientID %>").val(1);
                    $("#divGroup").hide();
                    $("#divprice").fadeIn("slow");
                    $("#radfenxiang").attr("checked", "checked");

                    $("#radzengtuan").removeAttr("checked");
                    break;
            }
            AddPrice.SumHeJiPrice();
        }

        var AddPrice = {
            createLiangDianEdit: function() {
                //创建行程编辑器
                //items: keMore //功能模式(keMore:多功能,keSimple:简易,keSimple_HaveImage:简易附带上传图片)
                //langType:en（英文） zh_CN（简体中文）ar(泰文) zh_TW（繁体中文）
                KEditer.init('<%=txtLiangDian.ClientID %>', { items: keSimple_HaveImage });
                KEditer.init('<%=txtJourney.ClientID %>');
            },
            init: function() {
                $("#<%=txtTuanXing.ClientID %>").val("<%=TuanXing %>");
                $("#hidtourmode").val("<%=TuanXing %>");

                if (fangXing != null && fangXing.length > 0) {
                    var j = 0;
                    for (var i = 0; i < fangXing.length; i++) {
                        if (fangXing[i].Num < 1) continue;
                        $("select[name='txtFangXing']").eq(j).val(fangXing[i].RoomId);
                        $("input[name='txtFangXingShuLiang']").eq(j).val(fangXing[i].Num);
                        j++;
                    }
                }
            },
            BianGengBox: null,
            SetTrafficPrice: function() {//计算大交通价格
                var sumPrice = 0;
                var sumTrain = 0;//火车
                var sumBus = 0;//汽车
                var sumShip = 0;//轮船
            	$("#tbl_Journey_AutoAdd input[name='txttrafficprice']").each(function() {
            		switch ($(this).closest("td").find(":hidden").eq(0).val()) {
            		case "9":
            			sumPrice += tableToolbar.getFloat($.trim($(this).val()));
            			break;
            		case "10":
            			sumTrain += tableToolbar.getFloat($.trim($(this).val()));
            			break;
            		case "11":
            			sumBus += tableToolbar.getFloat($.trim($(this).val()));
            			break;
            		case "12":
            			sumShip += tableToolbar.getFloat($.trim($(this).val()));
            			break;
            		}
            	})
                $("input[data-name='trafficPrice']").val(sumPrice);
                $("input[data-name='trainPrice']").val(sumTrain);
                $("input[data-name='busPrice']").val(sumBus);
                $("input[data-name='shipPrice']").val(sumShip);
            },
            SetHotelPrice: function() {//计算酒店价格
                var sumPrice1 = 0; var sumPrice2 = 0;
                $("#tbl_Journey_AutoAdd input[name='txthotel1price']").each(function() {
                    sumPrice1 = tableToolbar.calculate(sumPrice1, $.trim($(this).val()), "+");
                })
                $("input[data-name='hotel1Price']").val(sumPrice1 / 2);
                $("#tbl_Journey_AutoAdd input[name='txthotel2price']").each(function() {
                    sumPrice2 = tableToolbar.calculate(sumPrice2, $.trim($(this).val()), "+");
                })
                $("input[data-name='hotel2Price']").val(sumPrice2 / 2);
            },
            SetTotalPrice: function() {//汇总价格
                var adultprice1 = 0.00; //结算成人价格(房1)
                var adultprice2 = 0.00; //结算成人价格(房2)

                var price1 = 0.00; //销售成人价格(房1)
                var price2 = 0.00; //销售成人价格(房2)

                var otherprice1 = 0.00; //其他费用(结算)
                var otherprice2 = 0.00; //其他费用(销售)

                var hotelitme_price = 0;

                var hotelprice_price = 0;

                var price = 0;
                $("#DivItems input[data-UnitType='0']").each(function() {
                    price = tableToolbar.calculate(price, $.trim($(this).val()), "+");
                })
                adultprice1 = adultprice2 = price;

                $("#DivItems input[data-UnitType='0']").each(function() {
                    if ($(this).attr("data-name") == "hotel2Price") {
                        adultprice1 = price - tableToolbar.getFloat($.trim($(this).val()));
                        hotelitem_price2 = tableToolbar.getFloat($.trim($(this).val()));
                    }
                    if ($(this).attr("data-name") == "hotel1Price") {
                        adultprice2 = price - tableToolbar.getFloat($.trim($(this).val()));
                        hotelitme_price1 = tableToolbar.getFloat($.trim($(this).val()));

                    }
                })


                $("#DivItems input[data-UnitType='1']").each(function() {
                    otherprice1 = tableToolbar.calculate(otherprice1, $.trim($(this).val()), "+");
                })


                //========================================================================
                var price3 = 0.00;
                $("#divprice input[data-UnitType='0']").each(function() {
                    price3 = tableToolbar.calculate(price3, $.trim($(this).val()), "+");
                })

                price1 = price2 = price3;

                $("#divprice input[data-UnitType='0']").each(function() {
                    if ($(this).attr("data-name") == "hotel2Price") {
                        price1 = price3 - tableToolbar.getFloat($.trim($(this).val()));
                        hotelprice_price2 = tableToolbar.getFloat($.trim($(this).val()));
                    }
                    if ($(this).attr("data-name") == "hotel1Price") {
                        price2 = price3 - tableToolbar.getFloat($.trim($(this).val()));
                        hotelprice_price1 = tableToolbar.getFloat($.trim($(this).val()));
                    }
                })

                $("#divprice input[data-UnitType='1']").each(function() {
                    otherprice2 = tableToolbar.calculate(otherprice2, $.trim($(this).val()), "+");
                })
                //========================================================================
                var totalliushui = 0.00; //购物店流水
                var totalpeoplemoney = 0.00; //购物店成人人头
                var totalchildmoney = 0.00; //购物店儿童人头
                $("td.tdshop").find("input[type='checkbox']:checked").each(function() {
                    totalliushui = tableToolbar.calculate(tableToolbar.getFloat(totalliushui), tableToolbar.getFloat($(this).attr("data-liushui")), "+");
                    totalpeoplemoney = tableToolbar.calculate(tableToolbar.getFloat(totalpeoplemoney), tableToolbar.getFloat($(this).attr("data-peoplemoney")), "+");
                    totalchildmoney = tableToolbar.calculate(tableToolbar.getFloat(totalchildmoney), tableToolbar.getFloat($(this).attr("data-childmoney")), "+");
                });
                $("#Tab_Item_Price").find("input[name='txt_Item_SingleRoomPrice']").eq(0).val(tableToolbar.getFloat(hotelitme_price1));
                $("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").eq(0).attr("data-ordinal", tableToolbar.getFloat(adultprice1));
                $("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").eq(0).val(tableToolbar.calculate(tableToolbar.getFloat(adultprice1), tableToolbar.calculate(totalliushui, totalpeoplemoney, "+"), "-"));
                $("#Tab_Item_Price").find("input[name='txt_Item_OtherPrice']").eq(0).val(tableToolbar.getFloat(otherprice1));
                if ($("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").length >= 2) {
                    $("#Tab_Item_Price").find("input[name='txt_Item_OtherPrice']").eq(1).val(tableToolbar.getFloat(otherprice1));
                    $("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").eq(1).attr("data-ordinal", tableToolbar.getFloat(adultprice2));
                    $("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").eq(1).val(tableToolbar.calculate(tableToolbar.getFloat(adultprice2), tableToolbar.calculate(totalliushui, totalpeoplemoney, "+"), "-"));
                }
                $("#Tab_Price_Price").find("input[name='txt_Price_SingleRoomPrice']").eq(0).val(tableToolbar.getFloat(hotelprice_price1));
                $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(0).attr("data-ordinal", tableToolbar.getFloat(price1));
                $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(0).val(tableToolbar.calculate(tableToolbar.getFloat(price1), tableToolbar.calculate(totalliushui, totalpeoplemoney, "+"), "-"));
                $("#Tab_Price_Price").find("input[name='txt_Price_OtherPrice']").eq(0).val(tableToolbar.getFloat(otherprice2));
                if ($("#Tab_Price_Price").find("input[name='txt_Price_OtherPrice']").length >= 2) {
                    $("#Tab_Price_Price").find("input[name='txt_Price_OtherPrice']").eq(1).val(tableToolbar.getFloat(otherprice2));
                    $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(1).attr("data-ordinal", tableToolbar.getFloat(price2));
                    $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(1).val(tableToolbar.calculate(tableToolbar.getFloat(price2), tableToolbar.calculate(totalliushui, totalpeoplemoney, "+"), "-"));
                }

            },
            SetSumPrice: function() {//计算总价
                $("#tbl_Journey_AutoAdd").delegate("input[name='txthotel1price']", "blur", function() {
                    AddPrice.SetHotelPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SumHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txthotel2price']", "blur", function() {
                    AddPrice.SetHotelPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SumHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txttrafficprice']", "blur", function() {
                    AddPrice.SetTrafficPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SumHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txtbreakprice']", "blur", function() {
                    var self = $(this);
                    if ($.trim(self.parent().find("input[name='txtbreakname']").val()) == "") {
                        self.parent().find("input[class='pricejs']").val(self.val());
                    }
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SumHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txtsecondprice']", "blur", function() {
                    var self = $(this);
                    if ($.trim(self.parent().find("input[name='txtsecondname']").val()) == "") {
                        self.parent().find("input[class='pricejs']").val(self.val());
                    }
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SumHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txtthirdprice']", "blur", function() {
                    var self = $(this);
                    if ($.trim(self.parent().find("input[name='txtthirdname']").val()) == "") {
                        self.parent().find("input[class='pricejs']").val(self.val());
                    }
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SumHeJiPrice();
                })
                $("#TabList_Box").delegate("input[name='txtfprice']", "blur", function() {
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SumHeJiPrice();
                })

            },
            SumHeJiPrice: function() {
                var adultcount = $("#<%=txtCR.ClientID %>").val();
                var childcount = $("#<%=txtET.ClientID %>").val();
                var leadcount = $("#<%=txtLD.ClientID %>").val();
                var adultprice = 0;
                var childprice = 0;
                var leadprice = 0;
                var singleprice = 0;
                var otherprice = 0;
                var lvprice = tableToolbar.getFloat($("#<%=LvPrice.ClientID %>").val());
                if ($("#<%=hidItemType.ClientID %>").val() == "1") {
                    adultprice = $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").val();
                    childprice = $("#Tab_Price_Price").find("input[name='txt_Price_ChildPrice']").val();
                    leadprice = $("#Tab_Price_Price").find("input[name='txt_Price_LeadPrice']").val();
                    singleprice = $("#Tab_Price_Price").find("input[name='txt_Price_SingleRoomPrice']").val();
                    otherprice = $("#Tab_Price_Price").find("input[name='txt_Price_OtherPrice']").val();
                }
                else {
                    adultprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_AdultPrice']").val();
                    childprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_ChildPrice']").val();
                    leadprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_LeadPrice']").val();
                    singleprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_SingleRoomPrice']").val();
                    otherprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_OtherPrice']").val();
                }

                $("#<%=txtHeJiJinE.ClientID %>").val(tableToolbar.calculate(tableToolbar.getFloat(tableToolbar.calculate(adultcount, adultprice, "*") + tableToolbar.calculate(childcount, childprice, "*") + tableToolbar.calculate(leadcount, leadprice, "*") + tableToolbar.calculate(singleprice, otherprice, "+")), lvprice, "+"));
            },
            SumTipPrice: function() {//小费合计
                var tipprice = 0.00;
                $("#table_Tip").find("input[name='txt_Quote_SumPrice']").each(function() {
                    tipprice = tableToolbar.calculate(tipprice, $(this).val(), "+");
                })
                $("#DivPrices").find("input[data-name='otherprice']").val(tipprice);
            },
            SumZongFeiPrice: function() {//计算综费
                var ZongFeiPrice = 0.00;
                $("#Tab_Give").find("input[name='txt_WuPinPrice']").each(function() {
                    ZongFeiPrice = tableToolbar.calculate(ZongFeiPrice, $(this).val(), "+");
                })
                $("#DivBaoJia").find("input[data-name='zongfei']").val(ZongFeiPrice);
            },
            SumMenuPrice: function() {//计算餐馆菜单的价格
                var sumPrice = 0; //销售价总价
                var breakprice = 0; //早餐价格
                var secondprice = 0; //午餐价格
                var thirdprice = 0; //晚餐价格

                var fweiprice = 0; //风味餐价格（自定义添加）

                var sumPricejs = 0; //行程结算价总和
                var sumPricefjs = 0; //风味餐结算价总和
                $("#tbl_Journey_AutoAdd input[name='txtbreakprice'][eatfrist='yes']").each(function() {
                    breakprice = tableToolbar.calculate(breakprice, $.trim($(this).val()), "+");
                })
                $("#tbl_Journey_AutoAdd input[name='txtsecondprice'][eatsecond='yes']").each(function() {
                    secondprice = tableToolbar.calculate(secondprice, $.trim($(this).val()), "+");
                })
                $("#tbl_Journey_AutoAdd input[name='txtthirdprice'][eatthird='yes']").each(function() {
                    thirdprice = tableToolbar.calculate(thirdprice, $.trim($(this).val()), "+");
                })
                $("input[class='pricejs'][Eatpricejs='yes']").each(function() {
                    sumPricejs = tableToolbar.calculate(sumPricejs, $.trim($(this).val()), "+");
                })
                $("input[class='pricefjs']").each(function() {
                    sumPricefjs += tableToolbar.getFloat($.trim($(this).val()));
                })
                $("#TabList_Box input[data-name='txtfweiprice']").each(function() {
                    fweiprice = tableToolbar.calculate(fweiprice, $.trim($(this).val()), "+");
                })

                sumPrice = tableToolbar.calculate(breakprice, secondprice, "+");
                sumPrice = tableToolbar.calculate(sumPrice, thirdprice, "+");
                sumPrice = tableToolbar.calculate(sumPrice, fweiprice, "+");

                $("input[data-name='canPrice']").val(sumPrice);
                $("input[data-name='canItemPrice']").val(tableToolbar.calculate(sumPricejs, sumPricefjs, "+"));

            },
            submit: function(obj) {
                var _$obj = $(obj);
                var _v = ValiDatorForm.validator(_$obj.closest("form").get(0), "alert");
                if (!_v) return false;

                KEditer.sync();

                if ($("#<%=txtIsBianGeng.ClientID %>").val() == "1") { this.BianGengBox = new Boxy($("#div_Change"), { modal: true, fixed: false, title: "变更明细", width: "725px", height: "245px", display: "none" }); return false; }

                this.submit1(obj)
            },
            submit1: function(obj) {
                var _$obj = $(obj);
                _$obj.unbind("click");
                AddPrice.SetCityAndTraffic();
                var type = '<%=Request.QueryString["type"] %>';
                $.ajax({
                    type: "POST", url: window.location.href + "&doType=submit", data: $("#<%=form1.ClientID %>").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            if (type == "free")
                                location.href = "/Sales/TJChanPin.aspx?sl=3"; //自由行
                            else
                                location.href = "/Sales/ChanPin.aspx?sl=2"; //团队产品
                            return false;
                        } else {
                            alert(response.msg);
                            $("#i_a_submit").click(function() { AddPrice.submit(this); });
                        }
                    },
                    error: function() {
                        alert("请求异常");
                        $("#i_a_submit").click(function() { AddPrice.submit(this); });
                    }
                });
            },
            SetCityAndTraffic: function() {
                $(".TabCity").each(function() {
                    var self = $(this);
                    var cityid = [];
                    var cityname = [];
                    var traffic = [];
                    var trafficprice = [];
                    self.find("input[name='hidcityid']").each(function() { cityid.push($(this).val()); });
                    self.find("input[name='txtcity']").each(function() { cityname.push($(this).val()); });
                    self.find("input[name='hidtraffictype']").each(function() { traffic.push($(this).val()); });
                    self.find("input[name='txttrafficprice']").each(function() { trafficprice.push($(this).val()); });

                    self.closest(".TabCity").parent().find("input[name='hidcityids']").val(cityid.join(','));
                    self.closest(".TabCity").parent().find("input[name='hidcitys']").val(cityname.join(','));
                    self.closest(".TabCity").parent().find("input[name='hidtraffics']").val(traffic.join(','));
                    self.closest(".TabCity").parent().find("input[name='hidtrafficprices']").val(trafficprice.join(','));
                })
            }
        };

        $(document).ready(function() {
            $(".Default_Price").blur(function() {
                var self = $(this);
                if (self.attr("data-id") == "txtDefault_Breakfast") {
                    $("#tbl_Journey_AutoAdd").find("input[name='txtbreakprice']").val(self.val());
                }
                if (self.attr("data-id") == "txtDefault_Lunch") {
                    $("#tbl_Journey_AutoAdd").find("input[name='txtsecondprice']").val(self.val());
                }
                if (self.attr("data-id") == "txtDefault_Dinner") {
                    $("#tbl_Journey_AutoAdd").find("input[name='txtthirdprice']").val(self.val());
                }
                AddPrice.SumMenuPrice();
                AddPrice.SetTotalPrice();
            })
            pcToobar.init({ gID: "#txtCountryId", gSelect: '<%=CountryId %>' });
            AddPrice.createLiangDianEdit();
            AddPrice.init();
            AddPrice.SetSumPrice();
            $("#tbl_Journey_AutoAdd").autoAdd({ changeInput: $("#<%=txtTourDays.ClientID %>"), addCallBack: Journey.AddRowCallBack, upCallBack: Journey.MoveRowCallBack, downCallBack: Journey.MoveRowCallBack, delCallBack: Journey.DelRowCallBack, delStartCall: Journey.StartFun });
            $("#btnAddDays").click(function() { var day = tableToolbar.getInt($("#<%=txtTourDays.ClientID %>").val()); day++; $("#<%=txtTourDays.ClientID %>").val(day); $("#<%=txtTourDays.ClientID %>").change(); });
            $("#DivBaoJia").find("select").change(function() { $(this).closest("td").find("input[type='text']").eq(0).attr("data-UnitType", $(this).val()); AddPrice.SetHotelPrice(); AddPrice.SetTotalPrice(); });
            $("#DivItems input").blur(function() { AddPrice.SetTotalPrice(); AddPrice.SumHeJiPrice(); });
            $("#DivPrices input").blur(function() { AddPrice.SetTotalPrice(); AddPrice.SumHeJiPrice(); });
            $("._imgremark").toggle(function() { $(this).next("span").show(); }, function() { $(this).next("span").hide(); });

            $("#i_a_submit").click(function() { AddPrice.submit(this); });

            $("#btnChangeSave").click(function() {
                if ($.trim($("#txt_ChangeTitle").val()).length < 1) { tableToolbar._showMsg("变更标题不能为空!"); return false; }
                if ($.trim($("#txt_ChangeRemark").val()).length < 1) { tableToolbar._showMsg("变更明细不能为空!"); return false; }
                AddPrice.BianGengBox.hide();
                AddPrice.submit1($("#i_a_submit"));
            });

            $("input[data-class='heji']").blur(function() {
                AddPrice.SumHeJiPrice();
                return false;
            })

            $("#hidtourmode").val($("#<%=txtTuanXing.ClientID %>").val());
            $("#hidsdate").val($("#<%=txtLDate.ClientID %>").val());
            $("#hidedate").val($("#<%=txtRDate.ClientID %>").val());

            $("#<%=this.txtLDate.ClientID %>").blur(function() {
                $("#hidsdate").val($(this).val());
            });
            $("#<%=this.txtRDate.ClientID %>").blur(function() {
                $("#hidedate").val($(this).val());
            });
            $("#<%=txtTuanXing.ClientID %>").change(function() {
                $("#hidtourmode").val($(this).val());
                $("#<%=hidTourModeValue.ClientID %>").val($(this).val());
            });
            $("#hidbinkemode").val("<%=CountryId %>");

            $("#txtCountryId").change(function() {
                $("#hidbinkemode").val($(this).val())
            });


        });
        function CallBackCustomerUnit(obj) {
            $("#hidbinkemode").val(obj.CustomerUnitGuoji);
            $("#txtCountryId").val(obj.CustomerUnitGuoji);
            $("#<%=LvPrice.ClientID %>").val(obj.CustomerLvPrice);
            AddPrice.SetTotalPrice();
            AddPrice.SumHeJiPrice();
        }
    </script>

</asp:Content>

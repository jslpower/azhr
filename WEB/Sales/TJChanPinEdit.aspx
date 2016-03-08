<%@ Page Title="特价产品" Language="C#" AutoEventWireup="true" CodeBehind="TJChanPinEdit.aspx.cs"
    Inherits="EyouSoft.Web.Sales.TJChanPinEdit" MasterPageFile="~/MasterPage/Front.Master"
    ValidateRequest="false" %>

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
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        团队确认上传：
                    </td>
                    <td class="kuang2 pand4">
                        <uc1:UploadControl ID="txtFuJian2" FileTypes="*.jpg;*.gif;*.jpeg;*.png" runat="server"
                            IsUploadMore="false" IsUploadSelf="true" />
                    </td>
                    <td class="addtableT">
                        团号：
                    </td>
                    <td class="kuang2">
                        <input name="txtTourCode" id="txtTourCode" type="text" class="formsize140 input-txt"
                            runat="server" readonly="readonly" style="background: #dadada;" />
                    </td>
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
                        <font class="fontbsize12">* </font>出团日期：
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
                        <font class="fontbsize12">* </font>回团日期：
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
                    <td class="kuang2 PeopleCount">
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
                        行程亮点：<uc1:SelectJourneySpot runat="server" ID="SelectJourneySpot1" />
                    </td>
                    <td colspan="5" class="kuang2 pand4">
                        <span id="spanPlanContent" class="spanPlanContent" style="display: inline-block;">
                            <asp:TextBox ID="txtLiangDian" name="txtLiangDian" runat="server" TextMode="MultiLine"
                                CssClass="inputtext formsize800"></asp:TextBox>
                        </span>
                    </td>
                </tr>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div style="width: 100%" class="tablelist-box" id="TabList_Box">
            <div style="width: 100%">
                <span class="formtableT"><s></s>行程安排</span>
                <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-bottom: 3px;"
                    id="Tab_XingCheng">
                    <tbody>
                        <tr>
                            <th align="right" width="13%">
                                选择行程：
                            </th>
                            <td align="left">
                                <input type="hidden" id="hidRouteIds" value="" runat="server" />
                                <a class="xuanyong" id="xingcheng" href="javascript:;"></a>
                                <label id="lbrouteName" runat="server">
                                </label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="hr_10">
            </div>
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
                        <td valign="top" align="left" style="display: none">
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
                        <td valign="top" align="left">
                            <span class="formtableT">价格信息</span>
                            <input type="hidden" value="1" id="hidItemType" runat="server" />
                            <input type="radio" <%=tourMode=="0"?"checked='checked'":"" %> onclick="PriceItemType(0)"
                                value="0" name="rdTourQuoteType" id="radzengtuan" />
                            整团 &nbsp; &nbsp;
                            <input type="radio" <%=tourMode=="1"?"checked='checked'":"" %> value="1" onclick="PriceItemType(1)"
                                name="rdTourQuoteType" id="radfenxiang" />
                            分项
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
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_Dinner1" data-name="canPrice"
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
                                                    <input type="text" name="txt_Price_SingleRoomPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元 其它费用
                                                    <input type="text" name="txt_Price_OtherPrice" class="inputtext formsize40" value="" />
                                                    元/团 <font class="fontbsize12">* </font>合计金额
                                                    <input type="text" name="txtfenxiang_HeJiPrice" class="inputtext formsize40" value="" />元
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
                                                        <input type="text" name="txt_Price_SingleRoomPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("SingleRoomPrice")).ToString("f2") %>" />
                                                        元 其它费用
                                                        <input type="text" name="txt_Price_OtherPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("OtherPrice")).ToString("f2") %>" />
                                                        元/团 <font class="fontbsize12">* </font>合计金额
                                                        <input type="text" name="txtfenxiang_HeJiPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("TotalPrice")).ToString("f2") %>" />元
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <div class="hr_5">
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="hr_5">
        </div>
        <div <%=tourMode=="1"?"style='display:none;width:98.5%'":"" %> id="divGroup">
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
                                <input type="text" name="txt_zengtuan_AdultPrice" class="inputtext formsize40" value="" />
                                元/人
                                <img align="absmiddle" src="../images/child.gif">
                                儿童价
                                <input type="text" name="txt_zengtuan_ChildPrice" class="inputtext formsize40" value="" />
                                元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                领队
                                <input type="text" name="txt_zengtuan_LeadPrice" class="inputtext formsize40" value="" />
                                元/人&nbsp;<br>
                                单房差
                                <input type="text" name="txt_zengtuan_SingleRoomPrice" class="inputtext formsize40"
                                    value="" />
                                元 其它费用
                                <input type="text" name="txt_zengtuan_OtherPrice" class="inputtext formsize40" value="" />
                                元/团 <font class="fontbsize12">* </font>合计金额
                                <input type="text" name="txtzengtuan_HeJiPrice" class="inputtext formsize40" value="" />元
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:Repeater runat="server" ID="rptzengtuan">
                        <ItemTemplate>
                            <tr class="tempRowgroup">
                                <td align="left" id="td3">
                                    <img width="16" height="15" align="absmiddle" src="../images/chengren.gif">
                                    成人价
                                    <input type="text" name="txt_zengtuan_AdultPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>"
                                        data-ordinal="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>" />
                                    元/人
                                    <img align="absmiddle" src="../images/child.gif">
                                    儿童价
                                    <input type="text" name="txt_zengtuan_ChildPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>"
                                        data-ordinal="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>" />
                                    元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                    领队
                                    <input type="text" name="txt_zengtuan_LeadPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("LeadPrice")).ToString("f2") %>" />
                                    元/人&nbsp;<br>
                                    单房差
                                    <input type="text" name="txt_zengtuan_SingleRoomPrice" class="inputtext formsize40"
                                        value="<%#Convert.ToDecimal(Eval("SingleRoomPrice")).ToString("f2") %>" />
                                    元 其它费用
                                    <input type="text" name="txt_zengtuan_OtherPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("OtherPrice")).ToString("f2") %>" />
                                    元/团 <font class="fontbsize12">* </font>合计金额
                                    <input type="text" name="txtzengtuan_HeJiPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("TotalPrice")).ToString("f2") %>" />元
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
        <div class="hr_5">
        </div>
        <div style="width: 98.5%; display: none" class="tablelist-box ">
            <span class="formtableT">应收金额</span>
            <table width="100%" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <th valign="middle">
                            导游现收
                        </th>
                        <%--
                        <th valign="middle">
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
                        <%--<td align="center">
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
            </s>保 存</a><a hidefocus="true" href="javascript:void(0);" onclick=""><s class="chongzhi"></s>关闭</a>
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
            var routeids = $.trim($("#<%=hidRouteIds.ClientID %>").val()); //线路编号集合
            if (routeids != "") {
                tableToolbar.ShowConfirmMsg("如果修改报价类型,将初始化行程,是否继续?", function() {
                    AddPrice.ChangeRadio(ItemType);
                    AddPrice.NewContent();
                }, function() {
                    if (ItemType == 0)
                        ItemType = 1;
                    else
                        ItemType = 0;
                    AddPrice.ChangeRadio(ItemType);
                })
            } else {
                AddPrice.ChangeRadio(ItemType);
            }
        }

        var AddPrice = {
            ChangeRadio: function(ItemType) {
                switch (ItemType) {
                    case 0:
                        $("#<%=hidItemType.ClientID %>").val(ItemType);
                        $("#divGroup").fadeIn("slow");
                        $("#divprice").hide();
                        $("#radzengtuan").attr("checked", "checked");
                        $("#radfenxiang").removeAttr("checked");
                        break;
                    case 1:
                        $("#<%=hidItemType.ClientID %>").val(ItemType);
                        $("#divGroup").hide();
                        $("#divprice").fadeIn("slow");
                        $("#radfenxiang").attr("checked", "checked");
                        $("#radzengtuan").removeAttr("checked");
                        break;
                }
            },
            //初始化行程安排(用于切换报价类型后的操作)
            NewContent: function() {
                $("#<%=hidRouteIds.ClientID %>").val(""); //清空线路编号集合
                $("#xingcheng").closest("td").find("span[class='upload_filename']").remove();
                $("#<%=lbrouteName.ClientID %>").html("");
                $("#TabFengWeiCan").find("tr[class='tempRowfwei']").remove();
                AddPrice.AutoFengWeiCan("", "", "", true, "", "", "");
                if ($("#Tab_Give").find("tr[class='tempRowwupin']").length == 1) {
                    $("#Tab_Give").find("tr[class='tempRowwupin']").find("input").val("");
                } else {
                    $("#Tab_Give").find("tr[class='tempRowwupin']").remove();
                    AddPrice.AutoZengSong("", "", "", "");
                }
                if ($("#table_Tip").find("tr[class='tempRowTip']").length == 1) {
                    $("#table_Tip").find("tr[class='tempRowTip']").find("input").val("");
                }
                else {
                    $("#table_Tip").find("tr[class='tempRowTip']").remove();
                    AddPrice.AutoTip("", "", "", "", "");
                }
                AddPrice.SumMenuPrice();
                AddPrice.SetTotalPrice();
                $("#divGroup").find("input").val("");
                $("#divprice").find("input").val("");

            },
            createLiangDianEdit: function() {
                //创建行程编辑器
                //items: keMore //功能模式(keMore:多功能,keSimple:简易,keSimple_HaveImage:简易附带上传图片)
                //langType:en（英文） zh_CN（简体中文）ar(泰文) zh_TW（繁体中文）
                KEditer.init('<%=txtLiangDian.ClientID %>', { items: keSimple_HaveImage });
                KEditer.init('<%=txtJourney.ClientID %>');
            },
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
            CustomerLvPrice: 0,
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
                var lvprice = tableToolbar.getFloat($("#<%=LvPrice.ClientID %>").val());

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
                AddPrice.SetZongHePrice();

            },
            SetZongHePrice: function() {
                var lvprice = tableToolbar.getFloat($("#<%=LvPrice.ClientID %>").val());
                /*分项开始*/
                //成人价格（单价*数量）
                var adultPrice = tableToolbar.getFloat($("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(0).val()) * tableToolbar.getFloat($("#<%=txtCR.ClientID %>").val());
                //儿童价格（单价*数量）
                var childPrice = tableToolbar.getFloat($("#Tab_Price_Price").find("input[name='txt_Price_ChildPrice']").eq(0).val()) * tableToolbar.getFloat($("#<%=txtET.ClientID %>").val());
                //领队价格（单价*数量）
                var LeadPrice = tableToolbar.getFloat($("#Tab_Price_Price").find("input[name='txt_Price_LeadPrice']").eq(0).val()) * tableToolbar.getFloat($("#<%=txtLD.ClientID %>").val());
                //单房差
                var SingleRoomPrice = $("#Tab_Price_Price").find("input[name='txt_Price_SingleRoomPrice']").eq(0).val();
                //其他费用
                var OtherPrice = $("#Tab_Price_Price").find("input[name='txt_Price_OtherPrice']").eq(0).val();

                $("#Tab_Price_Price").find("input[name='txtfenxiang_HeJiPrice']").eq(0).val(tableToolbar.getFloat(adultPrice) + tableToolbar.getFloat(childPrice) + tableToolbar.getFloat(LeadPrice) + tableToolbar.getFloat(SingleRoomPrice) + tableToolbar.getFloat(OtherPrice) + tableToolbar.getFloat(lvprice));
                /*分项结束*/

                /*整团开始*/
                //成人价格（单价*数量）
                var adultPrice2 = tableToolbar.getFloat($("#Tab_Group_Price").find("input[name='txt_zengtuan_AdultPrice']").eq(0).val()) * tableToolbar.getFloat($("#<%=txtCR.ClientID %>").val());
                //儿童价格（单价*数量）
                var childPrice2 = tableToolbar.getFloat($("#Tab_Group_Price").find("input[name='txt_zengtuan_ChildPrice']").eq(0).val()) * tableToolbar.getFloat($("#<%=txtET.ClientID %>").val());
                //领队价格（单价*数量）
                var LeadPrice2 = tableToolbar.getFloat($("#Tab_Group_Price").find("input[name='txt_zengtuan_LeadPrice']").eq(0).val()) * tableToolbar.getFloat($("#<%=txtLD.ClientID %>").val());
                //单房差
                var SingleRoomPrice2 = $("#Tab_Group_Price").find("input[name='txt_zengtuan_SingleRoomPrice']").eq(0).val();
                //其他费用
                var OtherPrice2 = $("#Tab_Group_Price").find("input[name='txt_zengtuan_OtherPrice']").eq(0).val();

                $("#Tab_Group_Price").find("input[name='txtzengtuan_HeJiPrice']").eq(0).val(tableToolbar.getFloat(adultPrice2) + tableToolbar.getFloat(childPrice2) + tableToolbar.getFloat(LeadPrice2) + tableToolbar.getFloat(SingleRoomPrice2) + tableToolbar.getFloat(OtherPrice2) + tableToolbar.getFloat(lvprice));
                /*整团开始*/

            }, SetSumPrice: function() {//计算总价
                $("#TabList_Box").delegate("input[name='txtfprice']", "blur", function() {
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SumHeJiPrice();
                })
                $("#Tab_Price_Price").find("input[type='text']").blur(function() {
                    AddPrice.SetZongHePrice();
                })

                $("#Tab_Group_Price").find("input[type='text']").blur(function() {
                    AddPrice.SetZongHePrice();
                })
                $(".PeopleCount").find("input[type='text']").blur(function() {
                    AddPrice.SetTotalPrice();
                })

            },
            SumHeJiPrice: function() {
                var adultcount = $("#<%=txtCR.ClientID %>").val();
                var childcount = $("#<%=txtET.ClientID %>").val();
                var leadcount = $("#<%=txtLD.ClientID %>").val();
                if ($("#hidItemType").val() == "1") {
                    var adultprice = $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").val();
                    var childprice = $("#Tab_Price_Price").find("input[name='txt_Price_ChildPrice']").val();
                    var leadprice = $("#Tab_Price_Price").find("input[name='txt_Price_LeadPrice']").val();
                    var singleprice = $("#Tab_Price_Price").find("input[name='txt_Price_SingleRoomPrice']").val();
                    var otherprice = $("#Tab_Price_Price").find("input[name='txt_Price_OtherPrice']").val();
                }
                else {
                    var adultprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_AdultPrice']").val();
                    var childprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_ChildPrice']").val();
                    var leadprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_LeadPrice']").val();
                    var singleprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_SingleRoomPrice']").val();
                    var otherprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_OtherPrice']").val();
                }
                $("#<%=txtHeJiJinE.ClientID %>").val(tableToolbar.getFloat(tableToolbar.calculate(adultcount, adultprice, "*") + tableToolbar.calculate(childcount, childprice, "*") + tableToolbar.calculate(leadcount, leadprice, "*") + tableToolbar.calculate(singleprice, otherprice, "+")));
            },
            SumTipPrice: function() {//小费合计
                //                var tipprice = 0.00;
                //                $("#table_Tip").find("input[name='txt_Quote_SumPrice']").each(function() {
                //                    tipprice = tableToolbar.calculate(tipprice, $(this).val(), "+");
                //                })
                //                $("#DivPrices").find("input[data-name='otherprice']").val(tipprice);
            },
            SumZongFeiPrice: function() {//计算综费
                //                var ZongFeiPrice = 0.00;
                //                $("#Tab_Give").find("input[name='txt_WuPinPrice']").each(function() {
                //                    ZongFeiPrice = tableToolbar.calculate(ZongFeiPrice, $(this).val(), "+");
                //                })
                //                $("#DivBaoJia").find("input[data-name='zongfei']").val(ZongFeiPrice);
            },
            SumMenuPrice: function() {//计算餐馆菜单的价格
                //                var sumPrice = 0; //销售价总价
                //                var fweiprice = 0; //风味餐价格

                //                $("#TabFengWeiCan").find("input[name='txtfprice']").each(function() {
                //                    fweiprice = tableToolbar.calculate(fweiprice, $.trim($(this).val()), "+");
                //                })
                //                sumPrice = tableToolbar.calculate(fweiprice, sumPrice, "+");
                //                $("input[data-name='canPrice']").val(sumPrice);

            },
            submit: function(obj) {
                var _$obj = $(obj);
                var _v = ValiDatorForm.validator(_$obj.closest("form").get(0), "alert");
                if (!_v) return false;

                KEditer.sync();
                this.submit1(obj)
            },
            submit1: function(obj) {
                var _$obj = $(obj);
                _$obj.unbind("click");
                AddPrice.SetCityAndTraffic();
                $.ajax({
                    type: "POST", url: window.location.href + "&doType=submit", data: $("#<%=form1.ClientID %>").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            location.href = '/Sales/TJChanPin.aspx?typ=1&sl=<%=Request.QueryString["sl"] %>';
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
            },
            //获得选中线路的信息
            GetRouteJson: function(routeId) {
                $.ajax({
                    type: "get",
                    cache: false,
                    url: '/Ashx/GetRouteInfo.ashx?routeID=' + routeId,
                    dataType: "json",
                    async: false,
                    success: function(r) {
                        if (r.result == "1") {
                            AddPrice.CreateHtmlByRoute(r.data);
                            //向隐藏域中添加线路编号
                            var oldRouteIds = $.trim($("#<%=hidRouteIds.ClientID %>").val());
                            if (oldRouteIds.length > 0) {
                                $("#<%=hidRouteIds.ClientID %>").val(oldRouteIds + "," + r.data.TourId);
                            } else {
                                $("#<%=hidRouteIds.ClientID %>").val(r.data.TourId);
                            }
                        }
                    },
                    error: function() {
                    }
                });
                AddPrice.SumMenuPrice();
                AddPrice.SetTotalPrice();
                AddPrice.SumHeJiPrice();
            }, //删除线路的时候要移除追加的风味餐
            DelFengWei: function(data) {
                //首先获取线路下风味餐实体，然后遍历该实体中风味餐(footid)与页面Tabel(TabFengWeiCan)中的hidfootid的值，如果二者相等就移除掉本行数据。
                if (data.TourFootList && data.TourFootList.length > 0) {
                    for (var i = 0; i < data.TourFootList.length; i++) {
                        $("#TabFengWeiCan").find("input[name='hid_fcaidanid']").each(function() {
                            if ($.trim($(this).val()) == data.TourFootList[i].MenuId) {
                                $(this).closest("tr").remove();
                                if ($("#TabFengWeiCan").find("tr[class='tempRowfwei']").length == 0) {
                                    AddPrice.AutoFengWeiCan("", "", "", true, "", "", "");
                                }
                            }
                        })
                    }
                }
            }, //删除线路时移除追加的赠送项目
            DelZengSong: function(data) {
                if (data.TourGiveList && data.TourGiveList.length > 0) {
                    for (var i = 0; i < data.TourGiveList.length; i++) {
                        $("#Tab_Give").find("input[name='hidWuPinId']").each(function() {
                            if ($.trim($(this).val()) == data.TourGiveList[i].ItemId) {
                                if ($("#Tab_Give").find("tr[class='tempRowwupin']").length == 1) {
                                    $("#Tab_Give").find("input").val("");
                                }
                                else
                                    $(this).closest("tr").remove();
                            }
                        })
                    }
                }

            }, //删除线路时移除追加的小费
            DelTip: function(data) {
                if (data.TourTipList && data.TourTipList.length > 0) {
                    for (var i = 0; i < data.TourTipList.length; i++) {
                        $("#table_Tip").find("input[name='txt_Quote_Tip']").each(function() {
                            if ($.trim($(this).val()) == data.TourTipList[i].Tip) {
                                if ($("#table_Tip").find("tr[class='tempRowTip']").length == 1) {
                                    $("#table_Tip").find("input").val("");
                                }
                                else
                                    $(this).closest("tr").remove();
                            }
                        })
                    }
                }
            },
            DelDiJie: function(data) {
                if (data.TourDiJieList && data.TourDiJieList.length > 0) {
                    for (var i = 0; i < data.TourDiJieList.length; i++) {
                        $("#tblDiJie").find("input[name='hiddijieid']").each(function() {
                            var self = $(this);
                            if (self.val() == data.TourDiJieList[i].DiJieId) {
                                if ($("#tblDiJie").find("tr[class='tempDiJieRow']").length == 1) {
                                    $("#tblDiJie").find("input").val("");
                                }
                                else
                                    $(this).closest("tr").remove();
                            }
                        })
                    }
                }
            }, //移除线路后减去价格信息
            DelPrice: function(data) {
                if (data.TourCostList && data.TourCostList.length > 0) {
                    if (data.OutQuoteType == '<%=(int)EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项 %>') {//分项
                        for (var i = 0; i < data.TourCostList.length; i++) {
                            if (data.TourCostList[i].CostMode == '<%=(int)EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格 %>') {//销售价格
                                switch (data.TourCostList[i].Pricetype.toString()) {
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机 %>':
                                        var oldtraffic = $("#<%=txt_Price_BTraffic.ClientID %>").val();
                                        $("#<%=txt_Price_BTraffic.ClientID %>").val(tableToolbar.calculate(oldtraffic, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.火车 %>':
                                        var oldtrain = $("#<%=txt_Price_BTrain.ClientID %>").val();
                                        $("#<%=txt_Price_BTrain.ClientID %>").val(tableToolbar.calculate(oldtrain, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车 %>':
                                        var oldbus = $("#<%=txt_Price_BBus.ClientID %>").val();
                                        $("#<%=txt_Price_BBus.ClientID %>").val(tableToolbar.calculate(oldbus, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.轮船 %>':
                                        var oldship = $("#<%=txt_Price_BShip.ClientID %>").val();
                                        $("#<%=txt_Price_BShip.ClientID %>").val(tableToolbar.calculate(oldship, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.用车 %>':
                                        var oldcar = $("#<%=txt_Price_Car.ClientID %>").val();
                                        $("#<%=txt_Price_Car.ClientID %>").val(tableToolbar.calculate(oldcar, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.房1 %>':
                                        var oldroom = $("#<%=txt_Price_Room1.ClientID %>").val();
                                        $("#<%=txt_Price_Room1.ClientID %>").val(tableToolbar.calculate(oldroom, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.餐 %>':
                                        var olddinner = $("#<%=txt_Price_Dinner1.ClientID %>").val();
                                        $("#<%=txt_Price_Dinner1.ClientID %>").val(tableToolbar.calculate(olddinner, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.导服 %>':
                                        var oldguide = $("#<%=txt_Price_Guide.ClientID %>").val();
                                        $("#<%=txt_Price_Guide.ClientID %>").val(tableToolbar.calculate(oldguide, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.景点 %>':
                                        var oldscenic = $("#<%=txt_Price_Scenic.ClientID %>").val();
                                        $("#<%=txt_Price_Scenic.ClientID %>").val(tableToolbar.calculate(oldscenic, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.保险 %>':
                                        var oldInsure = $("#<%=txt_Price_Insure.ClientID %>").val();
                                        $("#<%=txt_Price_Insure.ClientID %>").val(tableToolbar.calculate(oldInsure, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通 %>':
                                        var oldstraffic = $("#<%=txt_Price_STraffic.ClientID %>").val();
                                        $("#<%=txt_Price_STraffic.ClientID %>").val(tableToolbar.calculate(oldstraffic, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.综费 %>':
                                        var oldsum = $("#<%=txt_Price_Sum.ClientID %>").val();
                                        $("#<%=txt_Price_Sum.ClientID %>").val(tableToolbar.calculate(oldsum, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.其他 %>':
                                        var oldother = $("#<%=txt_Price_Other.ClientID %>").val();
                                        $("#<%=txt_Price_Other.ClientID %>").val(tableToolbar.calculate(oldother, data.TourCostList[i].Price, "-"));
                                        break;
                                    default:

                                        break;
                                }
                            } else {
                                switch (data.TourCostList[i].Pricetype.toString()) {
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机 %>':
                                        var oldtraffic = $("#<%=txt_Price_BTraffic.ClientID %>").val();
                                        $("#<%=txt_Item_BTraffic.ClientID %>").val(tableToolbar.calculate(oldtraffic, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.火车 %>':
                                        var oldtrain = $("#<%=txt_Item_BTrain.ClientID %>").val();
                                        $("#<%=txt_Item_BTrain.ClientID %>").val(tableToolbar.calculate(oldtrain, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车 %>':
                                        var oldbus = $("#<%=txt_Item_BBus.ClientID %>").val();
                                        $("#<%=txt_Item_BBus.ClientID %>").val(tableToolbar.calculate(oldbus, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.轮船 %>':
                                        var oldship = $("#<%=txt_Item_BShip.ClientID %>").val();
                                        $("#<%=txt_Item_BShip.ClientID %>").val(tableToolbar.calculate(oldship, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.用车 %>':
                                        var oldcar = $("#<%=txt_Price_Car.ClientID %>").val();
                                        $("#<%=txt_Item_Car.ClientID %>").val(tableToolbar.calculate(oldcar, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.房1 %>':
                                        var oldroom = $("#<%=txt_Price_Room1.ClientID %>").val();
                                        $("#<%=txt_Item_Room1.ClientID %>").val(tableToolbar.calculate(oldroom, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.餐 %>':
                                        var olddinner = $("#<%=txt_Price_Dinner1.ClientID %>").val();
                                        $("#<%=txt_Item_Dinner.ClientID %>").val(tableToolbar.calculate(olddinner, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.导服 %>':
                                        var oldguide = $("#<%=txt_Price_Guide.ClientID %>").val();
                                        $("#<%=txt_Item_Guide.ClientID %>").val(tableToolbar.calculate(oldguide, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.景点 %>':
                                        var oldscenic = $("#<%=txt_Price_Scenic.ClientID %>").val();
                                        $("#<%=txt_Item_Scenic.ClientID %>").val(tableToolbar.calculate(oldscenic, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.保险 %>':
                                        var oldInsure = $("#<%=txt_Price_Insure.ClientID %>").val();
                                        $("#<%=txt_Item_Insure.ClientID %>").val(tableToolbar.calculate(oldInsure, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通 %>':
                                        var oldstraffic = $("#<%=txt_Price_STraffic.ClientID %>").val();
                                        $("#<%=txt_Item_STraffic.ClientID %>").val(tableToolbar.calculate(oldstraffic, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.综费 %>':
                                        var oldsum = $("#<%=txt_Item_Sum.ClientID %>").val();
                                        $("#<%=txt_Price_Sum.ClientID %>").val(tableToolbar.calculate(oldsum, data.TourCostList[i].Price, "-"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.其他 %>':
                                        var oldother = $("#<%=txt_Price_Other.ClientID %>").val();
                                        $("#<%=txt_Item_Other.ClientID %>").val(tableToolbar.calculate(oldother, data.TourCostList[i].Price, "-"));
                                        break;
                                    default:

                                        break;
                                }
                            }
                        }
                    }
                    else { /*整团*/ }
                }

            }, //填充地接社
            AutoDiJie: function(CityId, CityName, Contact, DiJieId, DiJieName, Fax, Phone, Remark) {
                var html = "<tr class='tempDiJieRow'>";
                html += "<td align='center'>";
                html += "<input type='hidden' name='hiddijiecityid' value='" + CityId + "' />";
                html += "<input type='text' class='formsize80 inputtext' name='txtdijiecity' value='" + CityName + "' />";
                html += "<a class='xuanyong citybox' data-mode='0' href='javascript:;'></a>";
                html += "</td>";
                html += "<td align='center'>";
                html += "<input type='hidden' name='hiddijieid' value='" + DiJieId + "' />";
                html += "<input type='text' readonly='readonly' style='background-color: #dadada' class='formsize120 inputtext' name='txtdijiename' value='" + DiJieName + "'><a class='xuanyongdijie xuanyong' href='javascript:;'>&nbsp;</a>";
                html += "</td>";
                html += "<td align='center'><input type='text' class='formsize80 inputtext' name='dijie_contact' value='" + Contact + "' /></td>";
                html += "<td align='center'><input type='text' class='formsize80 inputtext' name='dijie_tel' value='" + Phone + "' /></td>";

                html += "<td align='center'><input type='text' class='inputtext' style='width: 80%' name='dijie_tel' value='" + Remark + "' /></td>";
                html += "<td align='center'><a href='javascript:void(0)' class='addDiJie'><img width='48' height='20' src='../images/addimg.gif'></a> <a href='javascript:void(0)'class='delDiJie'><img width='48' height='20' src='../images/delimg.gif'></a></td>";
                html += "</tr>";
                $("#tblDiJie tbody").append(html);

            }, //创建编辑器
            CreateEdit: function(obj) {
                var _self = $(obj);
                if ($.trim(_self.attr("id")).length == 0) _self.attr("id", "txtRemark" + parseInt(Math.random() * 1000));
                KEditer.remove(_self.attr("id"));
                KEditer.init(_self.attr("id"), { resizeMode: 0, items: keSimple, height: "200px", width: "750px" });
            }, //选择线路后回填内容(风味餐,行程备注,行程亮点，小费，自费项目，赠送)
            CreateHtmlByRoute: function(data) {
                /*线路名称*/
                $("#xingcheng").closest("td").append("<span class='upload_filename' data-id='" + data.TourId + "'><a href='javascript:;' data-id='" + data.TourId + "' >" + data.RouteName + "<img src='/images/cha.gif' class='delRoute' data-id='" + data.TourId + "' /></a></span> ");
                /*行程亮点*/
                //首先获取原来的值
                KEditer.sync();
                var oldValue = $("#<%=txtLiangDian.ClientID %>").val();

                $("#spanPlanContent").closest("tr").find("textarea").each(function() {
                    if ($(this).attr("id")) {
                        KEditer.html($(this).attr("id"), oldValue + data.JourneySpot + "<br />");
                    }
                })
                /*行程备注*/
                $("#spanJourney").closest("tr").find("textarea").each(function() {
                    if ($(this).attr("id")) {
                        KEditer.html($(this).attr("id"), data.TravelNote + "<br />");
                    }
                })
                /*个性服务要求*/
                var oldSpecificRequire = $("#spanSpecificRequire").find("input[type='text']").val();
                $("#spanSpecificRequire").find("input[type='text']").val(oldSpecificRequire + data.SpecificRequire + "<br />");


                /*风味餐*/
                if (data.TourFootList && data.TourFootList.length > 0) {
                    for (var i = 0; i < data.TourFootList.length; i++) {
                        AddPrice.AutoFengWeiCan(data.TourFootList[i].MenuId, data.TourFootList[i].RestaurantId, data.TourFootList[i].Menu, false, data.TourFootList[i].Price, data.TourFootList[i].FootId, data.TourFootList[i].Remark);
                    }
                    $("#TabFengWeiCan tr").find("input[name='txtfcaidanname']").each(function() {
                        var self = $(this);
                        if ($.trim(self.val()) == "" && $("#TabFengWeiCan tr[class='tempRowfwei']").length > 1) {
                            self.closest("tr").remove();
                        }
                    })
                }
                /*赠送项目*/

                if (data.TourGiveList && data.TourGiveList.length > 0) {
                    for (var i = 0; i < data.TourGiveList.length; i++) {
                        AddPrice.AutoZengSong(data.TourGiveList[i].ItemId, data.TourGiveList[i].Item, data.TourGiveList[i].Price, data.TourGiveList[i].Remark);
                    }
                    $("#Tab_Give tr").find("input[name='txtWuPin']").each(function() {
                        var self = $(this);
                        if ($.trim(self.val()) == "" && $("#Tab_Give tr[class='tempRowwupin']").length > 1) {
                            self.closest("tr").remove();
                        }
                    })
                }
                /*小费*/
                if (data.TourTipList && data.TourTipList.length > 0) {
                    for (var i = 0; i < data.TourTipList.length; i++) {
                        AddPrice.AutoTip(data.TourTipList[i].Tip, data.TourTipList[i].Price, data.TourTipList[i].Days, data.TourTipList[i].SumPrice, data.TourTipList[i].Remark);
                    }
                    $("#table_Tip tr").find("input[name='txt_Quote_Tip']").each(function() {
                        var self = $(this);
                        if ($.trim(self.val()) == "" && $("#table_Tip tr[class='tempRowTip']").length > 1) {
                            self.closest("tr").remove();
                        }
                    })
                }
                /*地接社*/
                if (data.TourDiJieList && data.TourDiJieList.length > 0) {
                    for (var i = 0; i < data.TourDiJieList.length; i++) {
                        AddPrice.AutoDiJie(data.TourDiJieList[i].CityId, data.TourDiJieList[i].CityName, data.TourDiJieList[i].Contact, data.TourDiJieList[i].DiJieId, data.TourDiJieList[i].DiJieName, data.TourDiJieList[i].Fax, data.TourDiJieList[i].Phone, data.TourDiJieList[i].Remark);
                    }
                    $("#tblDiJie tr").find("input[name='txtdijiecity']").each(function() {
                        var self = $(this);
                        if ($.trim(self.val()) == "" && $("#tblDiJie tr[class='tempDiJieRow']").length > 1) {
                            self.closest("tr").remove();
                        }
                    })
                }



                /*销售价格*/ //选择线路后自动填充销售价格(累加)
                //TourPriceList  CostMode Pricetype (大交通 = 0,用车 = 1,房1 = 2,房2 = 3, 餐 = 4,导服 = 5,景点 = 6,保险 = 7,小交通 = 8,综费 = 9,十六免一 = 10,其他 = 11) Price  Remark
                //TourCostList = Count = 24
                if (data.TourCostList && data.TourCostList.length > 0) {
                    if (data.OutQuoteType == '<%=(int)EyouSoft.Model.EnumType.TourStructure.TourQuoteType.分项 %>') {//分项
                        for (var i = 0; i < data.TourCostList.length; i++) {
                            if (data.TourCostList[i].CostMode == '<%=(int)EyouSoft.Model.EnumType.TourStructure.CostMode.销售价格 %>') {//销售价格
                                switch (data.TourCostList[i].Pricetype.toString()) {
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机 %>':
                                        var oldtraffic = $("#<%=txt_Price_BTraffic.ClientID %>").val();
                                        $("#<%=txt_Price_BTraffic.ClientID %>").val(tableToolbar.calculate(oldtraffic, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.火车 %>':
                                        var oldtrain = $("#<%=txt_Price_BTrain.ClientID %>").val();
                                        $("#<%=txt_Price_BTrain.ClientID %>").val(tableToolbar.calculate(oldtrain, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车 %>':
                                        var oldbus = $("#<%=txt_Price_BBus.ClientID %>").val();
                                        $("#<%=txt_Price_BBus.ClientID %>").val(tableToolbar.calculate(oldbus, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.轮船 %>':
                                        var oldship = $("#<%=txt_Price_BShip.ClientID %>").val();
                                        $("#<%=txt_Price_BShip.ClientID %>").val(tableToolbar.calculate(oldship, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.用车 %>':
                                        var oldcar = $("#<%=txt_Price_Car.ClientID %>").val();
                                        $("#<%=txt_Price_Car.ClientID %>").val(tableToolbar.calculate(oldcar, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.房1 %>':
                                        var oldroom = $("#<%=txt_Price_Room1.ClientID %>").val();
                                        $("#<%=txt_Price_Room1.ClientID %>").val(tableToolbar.calculate(oldroom, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.餐 %>':
                                        var olddinner = $("#<%=txt_Price_Dinner1.ClientID %>").val();
                                        $("#<%=txt_Price_Dinner1.ClientID %>").val(tableToolbar.calculate(olddinner, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.导服 %>':
                                        var oldguide = $("#<%=txt_Price_Guide.ClientID %>").val();
                                        $("#<%=txt_Price_Guide.ClientID %>").val(tableToolbar.calculate(oldguide, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.景点 %>':
                                        var oldscenic = $("#<%=txt_Price_Scenic.ClientID %>").val();
                                        $("#<%=txt_Price_Scenic.ClientID %>").val(tableToolbar.calculate(oldscenic, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.保险 %>':
                                        var oldInsure = $("#<%=txt_Price_Insure.ClientID %>").val();
                                        $("#<%=txt_Price_Insure.ClientID %>").val(tableToolbar.calculate(oldInsure, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通 %>':
                                        var oldstraffic = $("#<%=txt_Price_STraffic.ClientID %>").val();
                                        $("#<%=txt_Price_STraffic.ClientID %>").val(tableToolbar.calculate(oldstraffic, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.综费 %>':
                                        var oldsum = $("#<%=txt_Price_Sum.ClientID %>").val();
                                        $("#<%=txt_Price_Sum.ClientID %>").val(tableToolbar.calculate(oldsum, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.其他 %>':
                                        var oldother = $("#<%=txt_Price_Other.ClientID %>").val();
                                        $("#<%=txt_Price_Other.ClientID %>").val(tableToolbar.calculate(oldother, data.TourCostList[i].Price, "+"));
                                        break;
                                    default:
                                        break;
                                }
                            } else {
                                switch (data.TourCostList[i].Pricetype.toString()) {
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.飞机 %>':
                                        var oldtraffic = $("#<%=txt_Price_BTraffic.ClientID %>").val();
                                        $("#<%=txt_Item_BTraffic.ClientID %>").val(tableToolbar.calculate(oldtraffic, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.火车 %>':
                                        var oldtrain = $("#<%=txt_Item_BTrain.ClientID %>").val();
                                        $("#<%=txt_Item_BTrain.ClientID %>").val(tableToolbar.calculate(oldtrain, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.汽车 %>':
                                        var oldbus = $("#<%=txt_Item_BBus.ClientID %>").val();
                                        $("#<%=txt_Item_BBus.ClientID %>").val(tableToolbar.calculate(oldbus, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.轮船 %>':
                                        var oldship = $("#<%=txt_Item_BShip.ClientID %>").val();
                                        $("#<%=txt_Item_BShip.ClientID %>").val(tableToolbar.calculate(oldship, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.用车 %>':
                                        var oldcar = $("#<%=txt_Price_Car.ClientID %>").val();
                                        $("#<%=txt_Item_Car.ClientID %>").val(tableToolbar.calculate(oldcar, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.房1 %>':
                                        var oldroom = $("#<%=txt_Price_Room1.ClientID %>").val();
                                        $("#<%=txt_Item_Room1.ClientID %>").val(tableToolbar.calculate(oldroom, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.餐 %>':
                                        var olddinner = $("#<%=txt_Price_Dinner1.ClientID %>").val();
                                        $("#<%=txt_Item_Dinner.ClientID %>").val(tableToolbar.calculate(olddinner, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.导服 %>':
                                        var oldguide = $("#<%=txt_Price_Guide.ClientID %>").val();
                                        $("#<%=txt_Item_Guide.ClientID %>").val(tableToolbar.calculate(oldguide, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.景点 %>':
                                        var oldscenic = $("#<%=txt_Price_Scenic.ClientID %>").val();
                                        $("#<%=txt_Item_Scenic.ClientID %>").val(tableToolbar.calculate(oldscenic, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.保险 %>':
                                        var oldInsure = $("#<%=txt_Price_Insure.ClientID %>").val();
                                        $("#<%=txt_Item_Insure.ClientID %>").val(tableToolbar.calculate(oldInsure, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.小交通 %>':
                                        var oldstraffic = $("#<%=txt_Price_STraffic.ClientID %>").val();
                                        $("#<%=txt_Item_STraffic.ClientID %>").val(tableToolbar.calculate(oldstraffic, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.综费 %>':
                                        var oldsum = $("#<%=txt_Price_Sum.ClientID %>").val();
                                        $("#<%=txt_Item_Sum.ClientID %>").val(tableToolbar.calculate(oldsum, data.TourCostList[i].Price, "+"));
                                        break;
                                    case '<%=(int)EyouSoft.Model.EnumType.TourStructure.Pricetype.其他 %>':
                                        var oldother = $("#<%=txt_Price_Other.ClientID %>").val();
                                        $("#<%=txt_Item_Other.ClientID %>").val(tableToolbar.calculate(oldother, data.TourCostList[i].Price, "+"));
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                    else { //整团

                    }
                }

            }, //选择完线路后自动填充风味餐信息
            AutoFengWeiCan: function(caidanid, id, caidanname, isdel, pricejs, ramNum, remark) {
                var _$html = "";
                _$html = "<tr class='tempRowfwei'>";
                if (isdel) {
                    _$html += "<td align='center'><input type='hidden' name='hidfootid' value='' /><input type='hidden' class='pricejs' value='" + pricejs + "' name='hid_fpricejs' /><input type='hidden' name='hid_fcaidanid' class='canid' value='" + caidanid + "' /><input type='hidden' name='hid_fcanguanid' value='" + id + "' class='menuid' /><input type='text' class='formsize120 inputtext' name='txtfcaidanname' readonly='readonly' style='background-color: #dadada' value='" + caidanname + "' /><a href='javascript:;' class='xuanyong fengweican'></a></td>";
                }
                else {
                    _$html += "<td align='center'><input type='hidden' name='hidfootid' value='" + ramNum + "' /><input type='hidden' value='" + pricejs + "' name='hid_fpricejs' /><input type='hidden' name='hid_fcaidanid' class='canid' value='" + caidanid + "' /><input type='hidden' name='hid_fcanguanid' value='" + id + "' class='menuid' /><input type='text' class='formsize120 inputtext' name='txtfcaidanname' readonly='readonly' style='background-color: #dadada' value='" + caidanname + "' /><a href='javascript:;' class='xuanyong'></a></td>";
                }
                if (isdel)
                    _$html += "<td align='center'><input type='text' class='formsize50 inputtext' data-name='txtfweiprice' name='txtfprice' value='" + pricejs + "'></td>";
                else
                    _$html += "<td align='center'><input type='text' class='formsize50 inputtext' readonly='readonly' style='background-color:#dadada' name='txtfprice' value='" + pricejs + "'></td>";
                _$html += "<td align='center'><input type='text' class='formsize140 inputtext' name='txtfremark' value='" + remark + "'></td>";
                _$html += "<td align='center'><a href='javascript:void(0)' class='addbtnfwei'><img width='48' height='20' src='../images/addimg.gif'></a>";
                if (isdel)
                    _$html += "<a href='javascript:void(0)' class='delbtnfwei'><img width='48' height='20' src='../images/delimg.gif'></a>";
                _$html += "</td>";
                _$html += "</tr>";
                $("#TabFengWeiCan tbody").append(_$html);
            },
            //选择线路后自动填充赠送项目
            AutoZengSong: function(id, name, price, remark) {
                var _html = "";
                _html = "<tr class='tempRowwupin'>";
                _html += "<td align='center'><input type='hidden' name='hidWuPinId' value='" + id + "' /><input type='text' class='formsize120 inputtext' name='txtWuPin' readonly='readonly' style='background-color: #dadada' value='" + name + "' /><a href='javascript:;' class='xuanyongWuPin xuanyong'></a></td>";

                _html += "<td align='center'><input type='text' errmsg='请输入正确的赠送金额' valid='isMoney' class='formsize120 inputtext'  name='txt_WuPinPrice' value='" + price + "'></td>";
                _html += "<td align='center'><input type='text' class='formsize140 inputtext' name='txt_WuPinRemark' value='" + remark + "'></td>";
                _html += "<td align='center'><a href='javascript:void(0)' class='addbtnwp'><img width='48' height='20' src='../images/addimg.gif'></a>";
                _html += "<a href='javascript:void(0)' class='delbtnwp'><img width='48' height='20' src='../images/delimg.gif'></a>";
                _html += "</td>";
                _html += "</tr>";
                $("#Tab_Give tbody").append(_html);
            },
            //选择线路后自动填充小费
            AutoTip: function(name, price, days, sumprice, remark) {
                var _html = "";
                _html = "<tr class='tempRowTip'>";
                _html += "<td align='center'><input type='text' class='formsize120 inputtext' name='txt_Quote_Tip' value='" + name + "' /></td>";

                _html += "<td align='center'><input type='text' errmsg='请输入正确的单价' valid='isMoney' class='formsize120 inputtext'  name='txt_Quote_Price' value='" + price + "'></td>";

                _html += "<td align='center'><input type='text' errmsg='请输入正确的天数' valid='isInt' name='txt_Quote_Days' class='formsize120 inputtext' value='" + days + "'></td>";
                _html += "<td align='center'><input type='text' class='formsize120 inputtext' errmsg='请输入正确的合计金额' valid='isMoney' name='txt_Quote_SumPrice' data-name='sumtipprice' value='" + sumprice + "'></td>";

                _html += "<td align='center'><input type='text' class='formsize120 inputtext' name='txt_Quote_Remark' value='" + remark + "'></td>";
                _html += "<td align='center'><a href='javascript:void(0)' class='addbtntip'><img width='48' height='20' src='../images/addimg.gif'></a>";
                _html += "<a href='javascript:void(0)' class='delbtntip'><img width='48' height='20' src='../images/delimg.gif'></a>";
                _html += "</td>";
                _html += "</tr>";
                $("#table_Tip tbody").append(_html);
            }
        };

        $(document).ready(function() {
            pcToobar.init({ gID: "#txtCountryId", gSelect: '<%=CountryId %>' });
            AddPrice.createLiangDianEdit();
            AddPrice.SetSumPrice();


            $("#btnAddDays").click(function() { var day = tableToolbar.getInt($("#<%=txtTourDays.ClientID %>").val()); day++; $("#<%=txtTourDays.ClientID %>").val(day); $("#<%=txtTourDays.ClientID %>").change(); });
            $("#DivBaoJia").find("select").change(function() { $(this).closest("td").find("input[type='text']").eq(0).attr("data-UnitType", $(this).val()); AddPrice.SetHotelPrice(); AddPrice.SetTotalPrice(); });

            $("#DivPrices input").blur(function() { AddPrice.SetTotalPrice(); AddPrice.SumHeJiPrice(); });
            $("._imgremark").toggle(function() { $(this).next("span").show(); }, function() { $(this).next("span").hide(); });

            $("#i_a_submit").click(function() { AddPrice.submit(this); });

            $("#btnChangeSave").click(function() {
                if ($.trim($("#txt_ChangeTitle").val()).length < 1) { tableToolbar._showMsg("变更标题不能为空!"); return false; }
                if ($.trim($("#txt_ChangeRemark").val()).length < 1) { tableToolbar._showMsg("变更明细不能为空!"); return false; }
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

            $("#xingcheng").click(function() {
                var type = $("#<%=hidItemType.ClientID %>").val(); //获取是整团还是分项(0:整团;1:分项)
                var url = "/CommonPage/SelectRoute.aspx?aid=" + $(this).attr("id") + "&type=" + type + "&";
                url += $.param({ callBack: "CallBackRoute" })
                Boxy.iframeDialog({
                    iframeUrl: url,
                    title: "选择行程",
                    modal: true,
                    width: "900",
                    height: "470"
                });
            })
            $("#Tab_XingCheng").delegate("img[class='delRoute']", "click", function() {
                var self = $(this);
                $.ajax({
                    type: "get",
                    cache: false,
                    url: '/Ashx/GetRouteInfo.ashx?routeId=' + self.attr("data-id"),
                    dataType: "json",
                    async: false,
                    success: function(r) {
                        if (r.result == "1") {
                            AddPrice.DelFengWei(r.data); //移除该线路下风味餐

                            AddPrice.DelZengSong(r.data); //移除该线路下赠送项目

                            AddPrice.DelTip(r.data); //移除该线路下小费
                            AddPrice.DelPrice(r.data); //减去线路下的价格信息
                            AddPrice.DelDiJie(r.data); //移除地接社
                            //移除线路
                            $("#xingcheng").closest("td").find("span[class='upload_filename'][data-id='" + r.data.TourId + "']").remove();

                            //删除隐藏域中线路编号
                            var oldRouteIds = $.trim($("#<%=hidRouteIds.ClientID %>").val());
                            if (oldRouteIds.length > 0) {
                                //                                if(oldRouteIds.endWith(",")){
                                //                                    $("#<%=hidRouteIds.ClientID %>").val(oldRouteIds.replace(r.data.TourId+",",""));
                                //                                }else{
                                $("#<%=hidRouteIds.ClientID %>").val(oldRouteIds.replace(r.data.TourId, ""));
                                //                                }
                            }

                            AddPrice.SumMenuPrice();
                            AddPrice.SetTotalPrice();
                            AddPrice.SumHeJiPrice();
                        }
                    },
                    error: function() {
                        alert("请求异常!");
                    }
                });

                AddPrice.SumMenuPrice();
                AddPrice.SetTotalPrice();
                AddPrice.SumHeJiPrice();
            })

            $("#TabFengWeiCan").delegate(".addbtnfwei", "click", function() {
                AddPrice.AutoFengWeiCan("", "", "", true, "", "", "");
            })
            $("#TabFengWeiCan").delegate(".delbtnfwei", "click", function() {
                if ($("#TabFengWeiCan").find("tr[class='tempRowfwei']").length == 1) {
                    tableToolbar._showMsg("最少保留一行!");
                    return false;
                }
                $(this).closest("tr").remove();
                AddPrice.SumMenuPrice();
                setTimeout(AddPrice.SetTotalPrice, 200);
                AddPrice.SumHeJiPrice();
            })
            $("#Tab_Give").delegate(".addbtnwp", "click", function() {
                AddPrice.AutoZengSong("", "", "", "");
                return false;
            })
            $("#Tab_Give").delegate(".delbtnwp", "click", function() {
                if ($("#Tab_Give").find("tr[class='tempRowwupin']").length == 1) {
                    tableToolbar._showMsg("最少保留一行!");
                    return false;
                }
                $(this).closest("tr").remove();
                AddPrice.SumMenuPrice();
                setTimeout(AddPrice.SetTotalPrice, 200);
                AddPrice.SumHeJiPrice();
            })
            $("#table_Tip").delegate(".addbtntip", "click", function() {
                AddPrice.AutoTip("", "", "", "", "");
            })
            $("#table_Tip").delegate(".delbtntip", "click", function() {
                if ($("#table_Tip").find("tr[class='tempRowTip']").length == 1) {
                    tableToolbar._showMsg("最少保留一行!");
                    return false;
                }
                $(this).closest("tr").remove();
                AddPrice.SumMenuPrice();
                setTimeout(AddPrice.SetTotalPrice, 200);
                AddPrice.SumHeJiPrice();
            })

            $("#tblDiJie").delegate(".addDiJie", "click", function() {
                AddPrice.AutoDiJie("", "", "", "", "", "", "", "");
            })
            $("#tblDiJie").delegate(".delDiJie", "click", function() {
                if ($("#tblDiJie").find("tr[class='tempDiJieRow']").length == 1) {
                    tableToolbar._showMsg("最少保留一行!");
                    return false;
                }
                $(this).closest("tr").remove();
            })


        });
        $(".citybox").live("click", function() {
            $(this).attr("id", "btn_" + parseInt(Math.random() * 100000));
            var isMore = $(this).attr("data-mode");
            var url = "/CommonPage/selectCity.aspx?aid=" + $(this).attr("id") + "&";
            var hideObj = $(this).parent().find("input[type='hidden']");
            var showObj = $(this).parent().find("input[type='text']");
            if (!hideObj.attr("id")) {
                hideObj.attr("id", "hideID_" + parseInt(Math.random() * 10000000));
            }
            if (!showObj.attr("id")) {
                showObj.attr("id", "ShowID_" + parseInt(Math.random() * 10000000));
            }
            url += $.param({ hideID: $(hideObj).val(), CityName: $(showObj).val(), callBack: "CallBackCity", ShowID: showObj.attr("id"), isMore: isMore, isxuanyong: 1 })
            Boxy.iframeDialog({
                iframeUrl: url,
                title: "选择城市",
                modal: true,
                width: "450",
                height: "300"
            });
        })
        var lgtype = '<%=Request.QueryString["LgType"] %>';
        $("input[name='txtcity']").autocomplete("/ashx/GetCityInfo.ashx?companyID=" + tableToolbar.CompanyID + "&LgType=" + lgtype, {
            width: 130,
            selectFirst: 'true',
            blur: 'true'
        }).result(function(e, data) {
            $(this).prev("input[type='hidden']").val(data[1]);
            $(this).attr("data-old", data[0]);
            $(this).attr("data-oldvalue", data[1]);
            Journey.AjaxGetShop($(this));
        });

        //选择城市回调方法
        function CallBackCity(ret) {
            $("#" + ret.aid).parent().find("input[type='text']").val(ret.name);
            $("#" + ret.aid).parent().find("input[type='hidden']").val(ret.id);
        }

        function CallBackRoute(items) {
            if (items && items.id == "") return;
            AddPrice.GetRouteJson(items.id);
            return false;
        }

        function CallBackCustomerUnit(obj) {
            $("#hidbinkemode").val(obj.CustomerUnitGuoji);
            $("#txtCountryId").val(obj.CustomerUnitGuoji);
            $("#<%=LvPrice.ClientID %>").val(obj.CustomerLvPrice);
            AddPrice.SetTotalPrice();

        }
    </script>

</asp:Content>

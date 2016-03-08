<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoJiaEdit.aspx.cs" Inherits="EyouSoft.Web.Quote.BaoJiaEdit"
    MasterPageFile="~/MasterPage/Front.Master" ValidateRequest="false" %>

<%@ Register Src="../UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/UploadControl.ascx" TagName="UploadControl" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/SelectJourneySpot.ascx" TagName="SelectJourneySpot"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/SelectJourney.ascx" TagName="SelectJourney" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/SelectPriceRemark.ascx" TagName="SelectPriceRemark"
    TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Tip.ascx" TagName="Tip" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Give.ascx" TagName="Give" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/SelfPay.ascx" TagName="SelfPay" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Journey.ascx" TagName="Journey" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/selectFengWeiCan.ascx" TagName="selectFengWeiCan"
    TagPrefix="uc3" %>
<%@ Register Src="../UserControl/TuanDiJie.ascx" TagName="TuanDiJie" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />
    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="addContent-box">
            <div class="tablehead1">
                <ul class="fixed">
                    <asp:Repeater ID="rptChildPrice" runat="server">
                        <ItemTemplate>
                            <li><a data-id="<%#Eval("QuoteId") %>" class="<%#Eval("QuoteId").ToString()==Request.QueryString["id"]?"de-ztorderform":"ztorderform" %>"
                                href="/Quote/BaoJiaEdit.aspx?id=<%#Eval("QuoteId") %>&sl=<%#sl %>&type=<%#type%>&act=<%#act%>">
                                <span>第<%#Eval("Times")%>次报价</span></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <table width="100%" cellspacing="0" cellpadding="0" class="firsttable">
                <tbody>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>团型：
                        </td>
                        <td class="kuang2">
                            <input type="hidden" name="hidtourmode" value="0" id="hidtourmode" />
                            <input type="hidden" name="hidTourModeValue" runat="server" id="hidTourModeValue" />
                            <asp:DropDownList runat="server" ID="ddlTourMode" CssClass="inputselect">
                            </asp:DropDownList>
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">* </font>询价单位：
                        </td>
                        <td class="kuang2">
                            <uc1:CustomerUnitSelect runat="server" ID="CustomerUnitSelect1" CallBack="CallBackCustomerUnit" />
                            <input type="hidden" value="" id="LvPrice" runat="server" />
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>报价类型:
                        </td>
                        <td class="kuang2">
                            <asp:DropDownList ID="ddlTourType" CssClass="inputselect" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td width="14%" class="addtableT">
                            联系人：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox runat="server" ID="txtContact" CssClass="inputtext formsize80"></asp:TextBox>
                        </td>
                        <td width="12%" class="addtableT">
                            联系电话：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox runat="server" ID="txtPhone" CssClass="inputtext formsize80"></asp:TextBox>
                        </td>
                        <td width="12%" class="addtableT">
                            联系传真：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox runat="server" ID="txtFax" CssClass="inputtext formsize80"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>询价日期：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtBuyTime" runat="server" onfocus="WdatePicker({minDate:'%y-%M-#{%d}'})"
                                CssClass="inputtext formsize100" valid="required" errmsg="请填写询价日期"></asp:TextBox>
                            <a class="timesicon" href="javascript:void(0);" onclick="WdatePicker({el:'<%=txtBuyTime.ClientID %>',minDate:'%y-%M-#{%d}'});">
                            </a>
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>有效期：
                        </td>
                        <td class="kuang2" colspan="3">
                            <input type="hidden" value="" name="hidsdate" id="hidsdate" />
                            <asp:TextBox ID="txtStartEffectTime" runat="server" onfocus="WdatePicker()" CssClass="inputtext formsize80"
                                valid="required" errmsg="请填写有开始效期"></asp:TextBox>
                            <a class="timesicon" href="javascript:void(0);" onclick="WdatePicker({el:'<%=txtStartEffectTime.ClientID %>'});">
                            </a>&nbsp;到&nbsp;
                            <input type="hidden" value="" name="hidedate" id="hidedate" />
                            <asp:TextBox ID="txtEndEffectTime" runat="server" onfocus="WdatePicker()" CssClass="inputtext formsize80"
                                valid="required" errmsg="请填写结束有效期"></asp:TextBox>
                            <a class="timesicon" href="javascript:void(0);" onclick="WdatePicker({el:'<%=txtEndEffectTime.ClientID %>'});">
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>团队名称：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txtRouteName" runat="server" CssClass="inputtext formsize140" valid="required"
                                errmsg="请输入团队名称!"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>天数：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox ID="txt_Days" runat="server" CssClass="inputtext formsize40" errmsg="请输入天数!|请输入正确的天数!|天数必须大于0!"
                                valid="required|isInt|range" min="1"></asp:TextBox>
                            <button class="addtimebtn" type="button" id="btnAddDays">
                                增加日程</button>
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>团队国籍/地区：
                        </td>
                        <td class="kuang2">
                            <input type="hidden" name="hidbinkemode" id="hidbinkemode" value="1" />
                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="inputselect" errmsg="请选择国籍!"
                                valid="required">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            抵达城市：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox runat="server" ID="txtArriveCity" CssClass="inputtext formsize80"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            航班/时间：
                        </td>
                        <td class="kuang2" colspan="3">
                            <asp:TextBox runat="server" ID="txtArriveCityFlight" CssClass="inputtext formsize180"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            离开城市：
                        </td>
                        <td class="kuang2">
                            <asp:TextBox runat="server" ID="txtLeaveCity" CssClass="inputtext formsize80"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            航班/时间：
                        </td>
                        <td class="kuang2" colspan="3">
                            <asp:TextBox runat="server" ID="txtLeaveCityFlight" CssClass="inputtext formsize180"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>业务员：
                        </td>
                        <td class="kuang2">
                            <uc1:SellsSelect ID="SellsSelect1" runat="server" SetTitle="业务员" />
                        </td>
                        <td class="addtableT">
                            <font class="fontbsize12">*</font>人数：
                        </td>
                        <td class="kuang2">
                            <img width="16" height="15" style="vertical-align: middle" src="../images/chengren.gif">
                            成人
                            <asp:TextBox runat="server" ID="txtMinAdults" CssClass="inputtext formsize40" valid="required|isInt|range"
                                errmsg="请输入人数!|请输入正确的人数!|人数必须大于0!"></asp:TextBox>
                            儿童数
                            <asp:TextBox runat="server" ID="txtMaxAdults" CssClass="inputtext formsize40" valid="required|isInt|range"
                                errmsg="请输入儿童数!|请输入正确的儿童数!|人数必须大于0!"></asp:TextBox>
                        </td>
                        <td class="addtableT">
                            酒店星级要求：
                        </td>
                        <td class="kuang2">
                            <asp:DropDownList runat="server" ID="ddlJiuDianXingJi" class="ddlJiuDianXingJi" />
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            <img width="14" height="15" src="../images/shch.gif">
                            外语报价单上传：
                        </td>
                        <td class="kuang2 pand4" colspan="5">
                            <uc1:UploadControl runat="server" ID="UploadControl1" IsUploadMore="true" IsUploadSelf="true" />
                            <div style="width: 560px; float: left; margin-left: 5px;">
                                <asp:Repeater runat="server" ID="rptorder">
                                    <ItemTemplate>
                                        <span class="upload_filename"><a target="_blank" href="<%#Eval("FilePath") %>">
                                            <%#Eval("FileName")%>
                                            <img src="/images/cha.gif" class="delimg"><input type="hidden" name="hideorder" value="<%#Eval("FileName") %>|<%#Eval("FilePath") %>" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></span>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="addtableT">
                            文件上传：
                        </td>
                        <td class="kuang2 pand4" colspan="5">
                            <uc1:UploadControl runat="server" ID="UploadControl2" IsUploadMore="true" IsUploadSelf="true" />
                            <div style="width: 560px; float: left; margin-left: 5px;">
                                <asp:Repeater ID="rptfile" runat="server">
                                    <ItemTemplate>
                                        <span class="upload_filename"><a target="_blank" href="<%#Eval("FilePath") %>">
                                            <%#Eval("FileName")%>
                                            <img src="/images/cha.gif" class="delimg"><input type="hidden" name="hidefile" value="<%#Eval("FileName") %>|<%#Eval("FilePath") %>" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></span>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
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
                        <td class="kuang2" colspan="5">
                            <span id="spanPlanContent" style="display: inline-block;">
                                <asp:TextBox ID="txtPlanContent" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <uc2:Journey ID="Journey1" runat="server" />
        &nbsp;<div class="hr_10">
        </div>
        <div style="width: 98.5%" class="tablelist-box" id="TabList_Box">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td colspan="7" align="left">
                            <img src="../images/google-map.gif" onclick='$(".directions").toggle(function(){googlemap.init();})'
                                alt="行程区间线路图" />
                            <table class="directions" style="display: none">
                                <tr>
                                    <th>
                                        路线规划结果
                                    </th>
                                    <th>
                                        地图
                                    </th>
                                </tr>
                                <tr>
                                    <td valign="top">
                                        <div id="directions" style="width: 375px; height: 500px; overflow: auto">
                                        </div>
                                    </td>
                                    <td valign="top">
                                        <div id="map_canvas" style="width: 800px; height: 500px">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            风味餐：
                        </th>
                        <td align="left">
                            <uc3:selectFengWeiCan ID="selectFengWeiCan1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            自费项目：
                        </th>
                        <td align="left">
                            <uc1:SelfPay runat="server" ID="SelfPay1" />
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            赠送：
                        </th>
                        <td align="left">
                            <uc1:Give runat="server" ID="Give1" />
                        </td>
                    </tr>
                    <tr>
                        <th align="right">
                            小费：
                        </th>
                        <td align="left">
                            <uc1:Tip runat="server" ID="Tip1" />
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
                                            <span style="width: 10%; display: inline-block">房1</span>
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
                                            <span style="width: 10%; display: inline-block">房2</span>
                                            <asp:TextBox runat="server" data-UnitType="0" ID="txt_Item_Room2" data-name="hotel2Price"
                                                CssClass="inputtext formsize50"></asp:TextBox>
                                            <asp:DropDownList runat="server" ID="ddlItemRoomUnit2">
                                                <asp:ListItem Value="0">元/人</asp:ListItem>
                                                <asp:ListItem Value="1">元/团</asp:ListItem>
                                            </asp:DropDownList>
                                            <img width="19" height="18" src="../images/bei.jpg" class="_imgremark" />
                                            <span style="display: none;">
                                                <asp:TextBox runat="server" ID="txt_Item_RoomRemark2" CssClass="inputtext formsize180"
                                                    Text="所有行程中的酒店2价格累加"></asp:TextBox>
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
                                        <th width="110" align="center">
                                            操作
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
                                                元/团 合计金额
                                                <input type="text" name="txt_Item_HeJiPrice" class="inputtext formsize40" value="" />
                                            </td>
                                            <td align="center">
                                                <a class="addbtnitem" href="javascript:void(0)">
                                                    <img width="48" height="20" src="../images/addimg.gif"></a> <a class="delbtnitem"
                                                        href="javascript:void(0)">
                                                        <img width="48" height="20" src="../images/delimg.gif"></a>
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
                                                    元/团 合计金额
                                                    <input type="text" name="txt_Item_HeJiPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("HeJiPrice")).ToString("f2") %>" />
                                                </td>
                                                <td align="center">
                                                    <a class="addbtnitem" href="javascript:void(0)">
                                                        <img width="48" height="20" src="../images/addimg.gif"></a> <a class="delbtnitem"
                                                            href="javascript:void(0)">
                                                            <img width="48" height="20" src="../images/delimg.gif"></a>
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
                            <input type="radio" <%=tourMode=="0"?"checked='checked'":"" %> onclick="PriceItemType(0,0)"
                                value="0" name="rdTourQuoteType" id="radzengtuan">
                            整团 &nbsp; &nbsp;
                            <input type="radio" <%=tourMode=="1"?"checked='checked'":"" %> value="1" onclick="PriceItemType(1,0)"
                                name="rdTourQuoteType" id="radfenxiang">
                            分项
                            <div <%=tourMode=="1"?"style='display:none'":"" %> id="divGroup">
                                <table width="100%" cellspacing="0" cellpadding="0" class="add-baojia autoAdd" id="Tab_Group_Price">
                                    <tbody>
                                        <tr>
                                            <th align="center">
                                                价格
                                            </th>
                                            <th width="110" align="center">
                                                操作
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
                                                    元 其它
                                                    <input type="text" name="txt_zengtuan_OtherPrice" class="inputtext formsize40" value="" />
                                                    元/团 合计金额
                                                    <input type="text" name="txt_zengtuan_HeJiPrice" class="inputtext formsize40" />
                                                </td>
                                                <td align="center">
                                                    <a class="addbtngroup" href="javascript:void(0)">
                                                        <img width="48" height="20" src="../images/addimg.gif"></a> <a class="delbtngroup"
                                                            href="javascript:void(0)">
                                                            <img width="48" height="20" src="../images/delimg.gif"></a>
                                                </td>
                                            </tr>
                                            <tr class="tempRowgroup">
                                                <td align="left" id="td6">
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
                                                    元 其它
                                                    <input type="text" name="txt_zengtuan_OtherPrice" class="inputtext formsize40" value="" />
                                                    元/团 合计金额
                                                    <input type="text" name="txt_zengtuan_HeJiPrice" class="inputtext formsize40" />
                                                </td>
                                                <td align="center">
                                                    <a class="addbtngroup" href="javascript:void(0)">
                                                        <img width="48" height="20" src="../images/addimg.gif"></a> <a class="delbtngroup"
                                                            href="javascript:void(0)">
                                                            <img width="48" height="20" src="../images/delimg.gif"></a>
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
                                                        元 其它
                                                        <input type="text" name="txt_zengtuan_OtherPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("OtherPrice")).ToString("f2") %>" />
                                                        元/团 合计金额
                                                        <input type="text" name="txt_zengtuan_HeJiPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("HeJiPrice")).ToString("f2") %>" />
                                                    </td>
                                                    <td align="center">
                                                        <a class="addbtngroup" href="javascript:void(0)">
                                                            <img width="48" height="20" src="../images/addimg.gif"></a> <a class="delbtngroup"
                                                                href="javascript:void(0)">
                                                                <img width="48" height="20" src="../images/delimg.gif"></a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </tbody>
                                </table>
                                <div class="hr_5">
                                </div>
                                <%--<table width="100%" cellspacing="0" cellpadding="0" class="add-baojia">
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
                                </table>--%>
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
                                                <span style="width: 10%; display: inline-block">房1</span>
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
                                                <span style="width: 10%; display: inline-block">房2</span>
                                                <asp:TextBox runat="server" data-UnitType="0" ID="txt_Price_Room2" data-name="hotel2Price"
                                                    CssClass="inputtext formsize50"></asp:TextBox>
                                                <asp:DropDownList runat="server" ID="ddlPriceRoom2">
                                                    <asp:ListItem Value="0">元/人</asp:ListItem>
                                                    <asp:ListItem Value="1">元/团</asp:ListItem>
                                                </asp:DropDownList>
                                                <img width="19" height="18" src="../images/bei.jpg" class="_imgremark">
                                                <span style="display: none;">
                                                    <asp:TextBox runat="server" ID="txt_Price_RoomRemark2" CssClass="inputtext formsize180"
                                                        Text="所有行程中的酒店2价格累加"></asp:TextBox>
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
                                            <th width="110" align="center">
                                                操作
                                            </th>
                                        </tr>
                                        <asp:PlaceHolder runat="server" ID="PhPrice">
                                            <tr class="tempRowprice">
                                                <td align="left" id="td1">
                                                    <img width="16" height="15" align="absmiddle" src="../images/chengren.gif">
                                                    成人价
                                                    <input type="text" name="txt_Price_AdultPrice" class="inputtext formsize40" value="" />
                                                    元/人
                                                    <img align="absmiddle" src="../images/child.gif">
                                                    儿童价
                                                    <input type="text" name="txt_Price_ChildPrice" class="inputtext formsize40" value="" />
                                                    元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                                    领队
                                                    <input type="text" name="txt_Price_LeadPrice" class="inputtext formsize40" value="" />
                                                    元/人&nbsp;<br>
                                                    单房差
                                                    <input type="text" name="txt_Price_SingleRoomPrice" class="inputtext formsize40"
                                                        value="" />
                                                    元 其它
                                                    <input type="text" name="txt_Price_OtherPrice" class="inputtext formsize40" value="" />
                                                    元/团 合计金额
                                                    <input type="text" name="txt_Price_HeJiPrice" class="inputtext formsize40" value="" />
                                                </td>
                                                <td align="center">
                                                    <a class="addbtnprice" href="javascript:void(0)">
                                                        <img width="48" height="20" src="../images/addimg.gif"></a> <a class="delbtnprice"
                                                            href="javascript:void(0)">
                                                            <img width="48" height="20" src="../images/delimg.gif"></a>
                                                </td>
                                            </tr>
                                        </asp:PlaceHolder>
                                        <asp:Repeater runat="server" ID="rptPrice">
                                            <ItemTemplate>
                                                <tr class="tempRowprice">
                                                    <td align="left" id="td2">
                                                        <img width="16" height="15" align="absmiddle" src="../images/chengren.gif">
                                                        成人价
                                                        <input type="text" name="txt_Price_AdultPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>"
                                                            data-ordinal="<%#Convert.ToDecimal(Eval("AdultPrice")).ToString("f2") %>" />
                                                        元/人
                                                        <img align="absmiddle" src="../images/child.gif">
                                                        儿童价
                                                        <input type="text" name="txt_Price_ChildPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>"
                                                            data-ordinal="<%#Convert.ToDecimal(Eval("ChildPrice")).ToString("f2") %>" />
                                                        元/人&nbsp;<img style="vertical-align: middle" src="../images/lindui.gif">
                                                        领队
                                                        <input type="text" name="txt_Price_LeadPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("LeadPrice")).ToString("f2") %>" />
                                                        元/人&nbsp;<br>
                                                        单房差
                                                        <input type="text" name="txt_Price_SingleRoomPrice" class="inputtext formsize40"
                                                            value="<%#Convert.ToDecimal(Eval("SingleRoomPrice")).ToString("f2") %>" />
                                                        元 其它
                                                        <input type="text" name="txt_Price_OtherPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("OtherPrice")).ToString("f2") %>" />
                                                        元/团 合计金额
                                                        <input type="text" name="txt_Price_HeJiPrice" class="inputtext formsize40" value="<%#Convert.ToDecimal(Eval("HeJiPrice")).ToString("f2") %>" />
                                                    </td>
                                                    <td align="center">
                                                        <a class="addbtnprice" href="javascript:void(0)">
                                                            <img width="48" height="20" src="../images/addimg.gif"></a> <a class="delbtnprice"
                                                                href="javascript:void(0)">
                                                                <img width="48" height="20" src="../images/delimg.gif"></a>
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
        <div class="hr_10">
        </div>
        <div style="width: 98.5%" class="tablelist-box ">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <th align="right">
                            报价备注：<uc1:SelectPriceRemark runat="server" ID="SelectPriceRemark1" />
                        </th>
                        <td align="left">
                            <span id="spanPriceRemark" style="display: inline-block;">
                                <asp:TextBox ID="txtPriceRemark" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                            </span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <div class="mainbox cunline fixed">
            <ul id="ul_AddPrice_Btn">
                <asp:PlaceHolder ID="phdQuote" runat="server">
                    <li class="cun-cy"><a id="btnAddSuccessCx" href="javascript:void(0);" data-name="报价成功">
                        报价成功</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phdNewAdd" runat="server">
                    <li class="cun-cy"><a id="btnAddNew" href="javascript:void(0);" data-name="另存为报价">另存为报价</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phdSave" runat="server">
                    <li class="cun-cy"><a href="javascript:void(0);" id="btnSave" data-name="保存报价">保存报价</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phdCanel" runat="server">
                    <li class="quxiao-cy"><a id="btnCanel" href="javascript:void(0);" data-name="取消报价">取消报价</a></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phMingPrint" runat="server">
                    <li class="cun-cy"><a class="orderlist" target="_blank" href="/PrintPage/QuoteDetail.aspx?quoteId=<%=Request.QueryString["id"] %>&LngType=<%=(int)EyouSoft.Model.EnumType.SysStructure.LngType.中文 %>">
                        明细报价单</a></li></asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phJianPrint">
                    <li class="cun-cy"><a class="orderlist" target="_blank" href="/PrintPage/QuoteSimple.aspx?quoteId=<%=Request.QueryString["id"] %>&LngType=<%=(int)EyouSoft.Model.EnumType.SysStructure.LngType.中文 %>">
                        简要报价单</a></li></asp:PlaceHolder>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <!--报价成功-->
    <div class="alertbox-outbox" id="div_Addprice_SuccessPrice" style="display: none;
        padding-bottom: 0px; width: 950px;">
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto;">
            <tbody>
                <tr>
                    <td width="13%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        团号：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="5">
                        <asp:Literal runat="server" ID="ltTourCode"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>抵达日期：
                    </td>
                    <td bgcolor="#E9F4F9" align="left">
                        <asp:TextBox runat="server" ID="txtTourLDate" data-class="bitian" errmsg="请填写抵达日期!"
                            CssClass="inputtext formsize120" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                    <td width="13%" height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        抵达城市：
                    </td>
                    <td height="28">
                        <asp:TextBox runat="server" ID="txtTourArriveCity" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <td width="13%" align="right" class="alertboxTableT">
                        航班/时间：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtTourArriveCityFlight" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>离开日期：
                    </td>
                    <td bgcolor="#E9F4F9" align="left">
                        <asp:TextBox runat="server" ID="txtTourRDate" data-class="bitian" errmsg="请填写离开日期!"
                            CssClass="inputtext formsize120" onfocus="WdatePicker()"></asp:TextBox>
                    </td>
                    <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                        离开城市：
                    </td>
                    <td height="28">
                        <asp:TextBox runat="server" ID="txtTourLeaveCity" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                    <td align="right" class="alertboxTableT">
                        航班/时间：
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txTourLeaveCityFlight" CssClass="inputtext formsize140"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        OP：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="5">
                        <uc1:SellsSelect ID="SellsSelect2" runat="server" SetTitle="OP" IsMust="False" />
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>人数：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="5">
                        <img width="16" height="15" style="vertical-align: middle" src="../images/chengren.gif">
                        成人
                        <asp:TextBox runat="server" data-class="peoplecount" errmsg="请填写成人数!" ID="txtTourAdults"
                            CssClass="inputtext formsize40" Text="0"></asp:TextBox>
                        <img style="vertical-align: middle" src="../images/child.gif">
                        儿童
                        <asp:TextBox runat="server" data-class="peoplecount" errmsg="请填写儿童数!" ID="txtTourChilds"
                            CssClass="inputtext formsize40" Text="0"></asp:TextBox>
                        <img style="vertical-align: middle" src="../images/lindui.gif">
                        领队
                        <asp:TextBox runat="server" data-class="peoplecount" errmsg="请填写领队数!" ID="txtTourLeaders"
                            CssClass="inputtext formsize40" Text="0"></asp:TextBox>
                        司陪
                        <asp:TextBox runat="server" ID="txtToursipei" CssClass="inputtext formsize40" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        <font class="fontbsize12">*</font>价格：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="5">
                        成人
                        <asp:TextBox runat="server" ID="txtTourAdultsPrice" data-class="bitian" errmsg="请填写成人价格!"
                            CssClass="inputtext formsize40"></asp:TextBox>
                        儿童
                        <asp:TextBox runat="server" ID="txtTourChildsPrice" data-class="bitian" errmsg="请填写儿童价格!"
                            CssClass="inputtext formsize40"></asp:TextBox>
                        领队
                        <asp:TextBox runat="server" ID="txtTourLeadersPrice" CssClass="inputtext formsize40"></asp:TextBox>
                        单房差
                        <asp:TextBox runat="server" ID="txtTourSingleRoomPrice" CssClass="inputtext formsize40"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        用房数：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="5">
                        <%=BindHotelRoomType()%>
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        地接社：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="5">
                        <uc5:TuanDiJie ID="TuanDiJie1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                        内部提醒信息：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left" colspan="5">
                        <span id="spanInsideInformation" style="display: inline-block;">
                            <asp:TextBox ID="txtTourInsideInformation" runat="server" TextMode="MultiLine" CssClass="inputtext formsize600"></asp:TextBox>
                        </span>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="hr_10">
        </div>
        <div class="alertbox-btn" style="text-align: center; position: static">
            <a href="javascript:void(0);" id="btn_Addprice_SuccessSave"><s class="baochun"></s>保
                存</a> <a href="javascript:void(0);" onclick="javascript:AddPrice.SuccessPriceBox.hide();return false;"
                    id="btn_Addprice_SuccessClose"><s class="chongzhi"></s>关 闭</a>
        </div>
    </div>
    <!--报价成功-->
    <!--取消报价-->
    <div class="alertbox-outbox03" id="div_Canel" style="display: none; padding-bottom: 0px;
        position: static; width: 585px; height: 150px">
        <div class="hr_10">
        </div>
        <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        取消原因：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <span id="spanCanelRemark" style="display: inline-block;">
                            <asp:TextBox ID="txtCanelRemark" runat="server" TextMode="MultiLine" CssClass="inputtext formsize450"></asp:TextBox>
                        </span>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a href="javascript:void(0);" id="btnCanelSave"><s class="baochun"></s>提 交</a><a
                href="javascript:void(0);" onclick="javascript:AddPrice.CanelBox.hide();return false;"><s
                    class="chongzhi"></s>关闭</a>
        </div>
    </div>
    <input type="hidden" value="" id="hidDayShopid" runat="server" />
    <input type="hidden" runat="server" id="hidQuoteId" value="" />
    <input type="hidden" runat="server" id="hidparentid" value="" />
    <input type="hidden" id="hidisExistBuyId" value="0" />
    <!--取消报价-->
    </form>

    <script type="text/javascript" src="http://ditu.google.cn/maps?file=api&amp;v=2&amp;key=AIzaSyBf0wag-At_3jV6NThPt5zTkx9jTB1q-Cs"></script>

    <script type="text/javascript">
        var googlemap = {
            map: null,
            gdir: null,
            init: function() {
                if (GBrowserIsCompatible) {
                    map = new GMap2(document.getElementById("map_canvas"));
                    map.addControl(new GLargeMapControl()); //缩放平移按钮及滑块控件
                    map.addControl(new GScaleControl()); //地图比例尺控件
                    map.addControl(new GOverviewMapControl(new GSize(200, 200))); //添加地图组件　一个可折叠的鹰眼地图，在地图的角落
                    map.addControl(new GMapTypeControl()); //创建带有切换地图类型的按钮的控件
                    map.enableDragging(); //设置地图可以被拖动
                    map.enableContinuousZoom(); //设置地图可以连续平滑地缩放。
                    map.enableScrollWheelZoom(); //设置地图可以由鼠标滚轮控制缩放。
                    gdir = new GDirections(map, document.getElementById("directions"));
                    GEvent.addListener(gdir, "error", function() {
                        var status = gdir.getStatus();
                        switch (status.code) {
                            case G_GEO_BAD_REQUEST:
                                alert("路线规划查询条件设定有误");
                                break;
                            case G_GEO_SERVER_ERROR:
                                alert("服务器不能正确解析你输入的地址");
                                break;
                            case G_GEO_MISSING_QUERY:
                            case G_GEO_MISSING_ADDRESS:
                                alert("查询条件（地址）不能为空");
                                break;
                            case G_GEO_UNKNOWN_ADDRESS:
                                alert("查询地址未知");
                                break;
                            case G_GEO_UNAVAILABLE_ADDRESS:
                                alert("因当地法律或其他原因不能解析给出地址");
                                break;
                            case G_GEO_UNKNOWN_DIRECTIONS:
                                alert("给出的两地之间无路可走或我们的现有的数据中缺少路线规划路线");
                                break;
                            case G_GEO_BAD_KEY:
                                alert("导入类库是指定的密钥有误");
                                break;
                            case G_GEO_TOO_MANY_QUERIES:
                                alert("查询太过频繁，超出密钥允许的查询次数");
                                break;
                        }
                    });

                    var i = 0, address = "";

                    //行程区间路线图
                    $("input[name=txtcity]").each(function() {
                        if (i != 0) {
                            address += " to: " + $(this).val();
                        }
                        else {
                            address += "from: " + $(this).val();
                        }
                        i += 1;
                    });
                    gdir.load(address);
                }
            }
        };
        var AddPrice = {
            Data: {
                sl: '<%=Request.QueryString["sl"] %>',
                type: '<%=Request.QueryString["type"] %>',
                act: '<%=Request.QueryString["act"] %>',
                id: '<%=Request.QueryString["id"] %>'
            },
            //超限弹窗
            ApplyPriceBox: null,
            //1=保存,2=报价超限，3=报价未超，4=保存新报价
            SaveType: 1,
            //报价成功弹窗
            SuccessPriceBox: null,
            //取消报价弹窗
            CanelBox: null,
            CreatePlanEdit: function() {
                //创建行程编辑器
                //items: keMore //功能模式(keMore:多功能,keSimple:简易,keSimple_HaveImage:简易附带上传图片)
                //langType:en（英文） zh_CN（简体中文）ar(泰文) zh_TW（繁体中文）
                KEditer.init('<%=txtPlanContent.ClientID %>', { items: keSimple_HaveImage });
                KEditer.init('<%=txtJourney.ClientID %>');
                KEditer.init('<%=txtPriceRemark.ClientID %>');
            },
            SetTrafficPrice: function() {//计算大交通价格
                var sumPrice = 0;//飞机
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
            	});
                $("input[data-name='trafficPrice']").val(sumPrice);
                $("input[data-name='trainPrice']").val(sumTrain);
                $("input[data-name='busPrice']").val(sumBus);
                $("input[data-name='shipPrice']").val(sumShip);
            },
            SetHotelPrice: function() {//计算酒店价格
                var sumPrice1 = 0; var sumPrice2 = 0;
                $("#tbl_Journey_AutoAdd input[name='txthotel1price']").each(function() {
                    sumPrice1 += tableToolbar.getFloat($.trim($(this).val()));
                })
                $("input[data-name='hotel1Price']").val(sumPrice1 / 2);
                $("#tbl_Journey_AutoAdd input[name='txthotel2price']").each(function() {
                    sumPrice2 += tableToolbar.getFloat($.trim($(this).val()));
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
                var hotelitme_price1 = 0;
                var hotelitem_price2 = 0;
                var hotelprice_price1 = 0;
                var hotelprice_price2 = 0;
                var price = 0;
                $("#DivItems input[data-UnitType='0']").each(function() {
                    price += tableToolbar.getFloat($.trim($(this).val()));
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
                    otherprice1 += tableToolbar.getFloat($.trim($(this).val()));
                })


                //========================================================================
                var price3 = 0.00;
                $("#divprice input[data-UnitType='0']").each(function() {
                    price3 += tableToolbar.getFloat($.trim($(this).val()));
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
                    otherprice2 += tableToolbar.getFloat($.trim($(this).val()));
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
                // txt_Item_SingleRoomPrice  txt_Price_SingleRoomPrice
                $("#Tab_Item_Price").find("input[name='txt_Item_SingleRoomPrice']").eq(0).val(tableToolbar.getFloat(hotelitme_price1));
                $("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").eq(0).attr("data-ordinal", tableToolbar.getFloat(adultprice1));
                var lvprice = tableToolbar.getFloat($("#<%=LvPrice.ClientID %>").val());
                $("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").eq(0).val(tableToolbar.calculate(tableToolbar.getFloat(adultprice1), tableToolbar.calculate(totalliushui, totalpeoplemoney, "+"), "-"));
                $("#Tab_Item_Price").find("input[name='txt_Item_OtherPrice']").eq(0).val(tableToolbar.getFloat(otherprice1));
                if ($("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").length >= 2) {
                    $("#Tab_Item_Price").find("input[name='txt_Item_SingleRoomPrice']").eq(1).val(tableToolbar.getFloat(hotelitem_price2));
                    $("#Tab_Item_Price").find("input[name='txt_Item_OtherPrice']").eq(1).val(tableToolbar.getFloat(otherprice1));
                    $("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").eq(1).attr("data-ordinal", tableToolbar.getFloat(adultprice2));
                    $("#Tab_Item_Price").find("input[name='txt_Item_AdultPrice']").eq(1).val(tableToolbar.calculate(tableToolbar.getFloat(adultprice2), tableToolbar.calculate(totalliushui, totalpeoplemoney, "+"), "-"));
                }

                $("#Tab_Price_Price").find("input[name='txt_Price_SingleRoomPrice']").eq(0).val(tableToolbar.getFloat(hotelprice_price1));
                $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(0).attr("data-ordinal", tableToolbar.getFloat(price1));
                $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(0).val(tableToolbar.calculate(tableToolbar.calculate(tableToolbar.getFloat(price1), tableToolbar.calculate(totalliushui, totalpeoplemoney, "+"), "-"), lvprice, "+"));
                $("#Tab_Price_Price").find("input[name='txt_Price_OtherPrice']").eq(0).val(tableToolbar.getFloat(otherprice2));
                if ($("#Tab_Price_Price").find("input[name='txt_Price_OtherPrice']").length >= 2) {
                    $("#Tab_Price_Price").find("input[name='txt_Price_SingleRoomPrice']").eq(1).val(tableToolbar.getFloat(hotelprice_price2));
                    $("#Tab_Price_Price").find("input[name='txt_Price_OtherPrice']").eq(1).val(tableToolbar.getFloat(otherprice2));
                    $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(1).attr("data-ordinal", tableToolbar.getFloat(price2));
                    $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(1).val(tableToolbar.calculate(tableToolbar.calculate(tableToolbar.getFloat(price2), tableToolbar.calculate(totalliushui, totalpeoplemoney, "+"), "-"), lvprice, "+"));
                }

            },
            SetSumPrice: function() {//计算总价
                $("#tbl_Journey_AutoAdd").delegate("input[name='txthotel1price']", "blur", function() {
                    AddPrice.SetHotelPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SetHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txthotel2price']", "blur", function() {
                    AddPrice.SetHotelPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SetHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txttrafficprice']", "blur", function() {
                    AddPrice.SetTrafficPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SetHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txtbreakprice']", "blur", function() {
                    var self = $(this);
                    if ($.trim(self.parent().find("input[name='txtbreakname']").val()) == "") {
                        self.parent().find("input[class='pricejs']").val(self.val());
                    }
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SetHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txtsecondprice']", "blur", function() {
                    var self = $(this);
                    if ($.trim(self.parent().find("input[name='txtsecondname']").val()) == "") {
                        self.parent().find("input[class='pricejs']").val(self.val());
                    }
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SetHeJiPrice();
                })
                $("#tbl_Journey_AutoAdd").delegate("input[name='txtthirdprice']", "blur", function() {
                    var self = $(this);
                    if ($.trim(self.parent().find("input[name='txtthirdname']").val()) == "") {
                        self.parent().find("input[class='pricejs']").val(self.val());
                    }
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SetHeJiPrice();
                })
                $("#TabList_Box").delegate("input[name='txtfprice']", "blur", function() {
                    AddPrice.SumMenuPrice();
                    AddPrice.SetTotalPrice();
                    AddPrice.SetHeJiPrice();
                })
                $("#Tab_Item_Price").delegate("input", "blur", function() {
                    AddPrice.SetHeJiPrice();
                })
                $("#Tab_Price_Price").delegate("input", "blur", function() {
                    AddPrice.SetHeJiPrice();
                })
                $("#Tab_Group_Price").delegate("input", "blur", function() {
                    AddPrice.SetHeJiPrice();
                })

            },
            SetHeJiPrice: function() {//自动计算合计金额
                $("#Tab_Item_Price").find("tr[class='tempRowitem']").each(function() {
                    var self = $(this);
                    var adultprice = self.find("input[name='txt_Item_AdultPrice']").val();
                    var childprice = self.find("input[name='txt_Item_ChildPrice']").val();
                    var leadprice = self.find("input[name='txt_Item_LeadPrice']").val();
                    var singleprice = self.find("input[name='txt_Item_SingleRoomPrice']").val();
                    var otherprice = self.find("input[name='txt_Item_OtherPrice']").val();
                    self.find("input[name='txt_Item_HeJiPrice']").val(tableToolbar.getFloat(adultprice) + tableToolbar.getFloat(childprice) + tableToolbar.getFloat(leadprice) + tableToolbar.getFloat(singleprice) + tableToolbar.getFloat(otherprice));
                })
                $("#Tab_Price_Price").find("tr[class='tempRowprice']").each(function() {
                    var self = $(this);
                    var adultprice = self.find("input[name='txt_Price_AdultPrice']").val();
                    var childprice = self.find("input[name='txt_Price_ChildPrice']").val();
                    var leadprice = self.find("input[name='txt_Price_LeadPrice']").val();
                    var singleprice = self.find("input[name='txt_Price_SingleRoomPrice']").val();
                    var otherprice = self.find("input[name='txt_Price_OtherPrice']").val();
                    self.find("input[name='txt_Price_HeJiPrice']").val(tableToolbar.getFloat(adultprice) + tableToolbar.getFloat(childprice) + tableToolbar.getFloat(leadprice) + tableToolbar.getFloat(singleprice) + tableToolbar.getFloat(otherprice));
                })
                $("#Tab_Group_Price").find("tr[class='tempRowgroup']").each(function() {
                    var self = $(this);
                    var adultprice = self.find("input[name='txt_zengtuan_AdultPrice']").val();
                    var childprice = self.find("input[name='txt_zengtuan_ChildPrice']").val();
                    var leadprice = self.find("input[name='txt_zengtuan_LeadPrice']").val();
                    var singleprice = self.find("input[name='txt_zengtuan_SingleRoomPrice']").val();
                    var otherprice = self.find("input[name='txt_zengtuan_OtherPrice']").val();
                    self.find("input[name='txt_zengtuan_HeJiPrice']").val(tableToolbar.getFloat(adultprice) + tableToolbar.getFloat(childprice) + tableToolbar.getFloat(leadprice) + tableToolbar.getFloat(singleprice) + tableToolbar.getFloat(otherprice));
                })
            },
            SumTipPrice: function() {//小费合计
                var tipprice = 0.00;
                $("#table_Tip").find("input[name='txt_Quote_SumPrice']").each(function() {
                    tipprice += tableToolbar.getFloat($(this).val());
                })
                $("#DivPrices").find("input[data-name='otherprice']").val(tableToolbar.getFloat(tipprice));
            },
            SumZongFeiPrice: function() {//计算综费
                var ZongFeiPrice = 0.00;
                $("#Tab_Give").find("input[name='txt_WuPinPrice']").each(function() {
                    ZongFeiPrice += tableToolbar.getFloat($(this).val());
                })
                $("#DivBaoJia").find("input[data-name='zongfei']").val(tableToolbar.getFloat(ZongFeiPrice));
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
                    breakprice += tableToolbar.getFloat($.trim($(this).val()));
                })
                $("#tbl_Journey_AutoAdd input[name='txtsecondprice'][eatsecond='yes']").each(function() {
                    secondprice += tableToolbar.getFloat($.trim($(this).val()));
                })
                $("#tbl_Journey_AutoAdd input[name='txtthirdprice'][eatthird='yes']").each(function() {
                    thirdprice += tableToolbar.getFloat($.trim($(this).val()));
                })
                $("input[class='pricejs'][Eatpricejs='yes']").each(function() {
                    sumPricejs += tableToolbar.getFloat($.trim($(this).val()));
                })
                $("input[class='pricefjs']").each(function() {
                    sumPricefjs += tableToolbar.getFloat($.trim($(this).val()));
                })
                $("#TabList_Box input[data-name='txtfweiprice']").each(function() {
                    fweiprice += tableToolbar.getFloat($.trim($(this).val()));
                })
                sumPrice = breakprice + secondprice + thirdprice + fweiprice;
                $("input[data-name='canPrice']").val(tableToolbar.getFloat(sumPrice));
                $("input[data-name='canItemPrice']").val(tableToolbar.calculate(sumPricejs, sumPricefjs, "+"));

            },
            CheckHotel: function() {//报价完成时检查行程中每天的酒店数量是否只有一个...
                var result = true;
                $("#tbl_Journey_AutoAdd .tdHotel").each(function() {
                    var self = $(this);
                    if ($.trim(self.find("input[name='txthotel1']").val()) != "" && $.trim(self.find("input[name='txthotel2']").val()) != "") {
                        result = false;
                        return result;
                    }
                })
                return result;
            },
            CheckPeoPleCount: function() {
                var result = true;
                $("#div_Addprice_SuccessPrice").find("input[data-class='bitian']").each(function() {
                    var self = $(this);
                    //if()
                })
            },
            CheckPriceCount: function() {//报价完成时检查价格组成是否只有一条
                var result = true;
                var itemcount = $("#Tab_Item_Price").find("tr").length;
                var pricecount = $("#Tab_Price_Price").find("tr").length;
                var type = $("#<%=hidItemType.ClientID %>").val();

                var zengtuancount = $("#divGroup").find("tr").length;
                if (type == "0") {
                    if (itemcount > 2 || pricecount > 2 || zengtuancount > 2) {
                        result = false;
                        return false;
                    }
                } else {
                    if (itemcount > 2 || pricecount > 2) {
                        result = false;
                        return false;
                    }
                }
                //初始化成团时的单价
                var adultprice = "";
                var childprice = "";
                var leadprice = "";
                var singleroomprice = "";
                var type = $("#<%=hidItemType.ClientID %>").val();
                if (type == "0") { //整团
                    adultprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_AdultPrice']").eq(0).val();
                    childprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_ChildPrice']").eq(0).val();
                    leadprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_LeadPrice']").eq(0).val();
                    singleroomprice = $("#Tab_Group_Price").find("input[name='txt_zengtuan_SingleRoomPrice']").eq(0).val();
                }
                else {//分项
                    adultprice = $("#Tab_Price_Price").find("input[name='txt_Price_AdultPrice']").eq(0).val();
                    childprice = $("#Tab_Price_Price").find("input[name='txt_Price_ChildPrice']").eq(0).val();
                    leadprice = $("#Tab_Price_Price").find("input[name='txt_Price_LeadPrice']").eq(0).val();
                    singleroomprice = $("#Tab_Price_Price").find("input[name='txt_Price_SingleRoomPrice']").eq(0).val();
                }

                $("#<%=txtTourAdultsPrice.ClientID %>").val(adultprice);
                $("#<%=txtTourChildsPrice.ClientID %>").val(childprice);
                $("#<%=txtTourLeadersPrice.ClientID %>").val(leadprice);
                $("#<%=txtTourSingleRoomPrice.ClientID %>").val(singleroomprice);

                $("#<%=txtTourAdultsPrice.ClientID %>").attr("readonly", "readonly");
                $("#<%=txtTourChildsPrice.ClientID %>").attr("readonly", "readonly");
                $("#<%=txtTourLeadersPrice.ClientID %>").attr("readonly", "readonly");
                $("#<%=txtTourSingleRoomPrice.ClientID %>").attr("readonly", "readonly");

                return result;
            },
            //按钮绑定事件
            BindBtn: function() {
                //未超限保存
                $("#btn_Addprice_SuccessSave").unbind("click").click(function() {
                    //验证表单必填项
                    var msg = "";
                    $("#div_Addprice_SuccessPrice").find("input[data-class='bitian']").each(function() {
                        var self = $(this);
                        if ($.trim(self.val()) == "0" || $.trim(self.val()) == "") {
                            msg += self.attr("errmsg") + "<br />";
                        }
                    })
                    var peoplecount = 0;
                    $("#div_Addprice_SuccessPrice").find("input[data-class='peoplecount']").each(function() {
                        var self = $(this);
                        peoplecount = tableToolbar.getFloat(self.val()) + peoplecount;
                    })
                    if (peoplecount <= 0) {
                        msg += "请填写人数<br />";
                    }

                    if (tableToolbar.getFloat($("#<%=txtTourAdultsPrice.ClientID %>").val()) <= 0) {
                        msg += "成人价格不能小于零!";
                    }


                    if (msg != "") {
                        tableToolbar._showMsg(msg);
                        msg = "";
                        return false;
                    }
                    if (ValiDatorForm.validator($("#ul_AddPrice_Btn").closest("form").get(0), "alert")) {
                        AddPrice.SaveType = 3;
                        AddPrice.SuccessPriceBox.hide();
                        $("#btnAddSuccessCx").html("正在提交");
                        AddPrice.Save();
                    }
                    return false;
                })

                //关闭
                //                $("#btn_Addprice_SuccessClose").unbind("click").click(function() {
                //                    $("#div_Addprice_SuccessPrice").find("input[type='text']").val("");
                //                    $("#div_Addprice_SuccessPrice").find("input[type='textarea']").val("");
                //                    $("#div_Addprice_SuccessPrice").find("input[type='checkbox']").attr("checked", "");
                //                    AddPrice.SuccessPriceBox.hide();
                //                    return false;
                //                })

                $("#ul_AddPrice_Btn").find("a").css("background-position", "0 0");
                $("#ul_AddPrice_Btn").find("a").each(function() {
                    $(this).html($(this).attr("data-name"));
                })

                $("#ul_AddPrice_Btn").find("a").unbind("click").click(function() {
                    var _s = $(this);
                    var id = _s.attr("id");
                    var isExistBuyId = $("#hidisExistBuyId").val();
                    AddPrice.SetCityAndTraffic();
                    switch (id) {
                        //保存                                                                                                                                                                                                                                                                                                      
                        case "btnSave":
                            AddPrice.SaveType = 1;
                            if (isExistBuyId == "1") {
                                tableToolbar._showMsg("存在相同的询价编号!");
                                return false;
                            }
                            if (ValiDatorForm.validator($("#ul_AddPrice_Btn").closest("form").get(0), "alert")) {
                                _s.html("正在提交..");
                                AddPrice.Save();
                            }
                            break;
                        //报价成功                                                                                                 
                        case "btnAddSuccessCx":
                            if (isExistBuyId == "1") {
                                tableToolbar._showMsg("存在相同的询价编号!");
                                return false;
                            }
                            if (!AddPrice.CheckHotel()) {
                                tableToolbar._showMsg("一天只能选择一个酒店!");
                                return false;
                            }
                            if (!AddPrice.CheckPriceCount()) {
                                tableToolbar._showMsg("只能添加一条价格组成!");
                                return false;
                            }
                            //验证表单
                            if (ValiDatorForm.validator($("#ul_AddPrice_Btn").closest("form").get(0), "alert")) {
                                if (AddPrice.SuccessPriceBox) { AddPrice.SuccessPriceBox.hide(); }
                                AddPrice.SuccessPriceBox = new Boxy($("#div_Addprice_SuccessPrice"), { modal: true, fixed: false, title: "报价成功", "z-index": "1000", closeable: false });
                            }
                            //初始化抵达城市和出发城市及航班信息
                            var txtArriveCity = $("#<%=txtArriveCity.ClientID %>").val();
                            var txtArriveCityFlight = $("#<%=txtArriveCityFlight.ClientID %>").val();
                            var txtLeaveCity = $("#<%=txtLeaveCity.ClientID %>").val();
                            var txtLeaveCityFlight = $("#<%=txtLeaveCityFlight.ClientID %>").val();
                            $("#<%=txtTourArriveCity.ClientID %>").val(txtArriveCity);
                            $("#<%=txtTourArriveCityFlight.ClientID %>").val(txtArriveCityFlight);
                            $("#<%=txtTourLeaveCity.ClientID %>").val(txtLeaveCity);
                            $("#<%=txTourLeaveCityFlight.ClientID %>").val(txtLeaveCityFlight);
                            break;

                        //添加新的报价                                                                                                 
                        case "btnAddNew":
                            if (isExistBuyId == "1") {
                                tableToolbar._showMsg("存在相同的询价编号!");
                                return false;
                            }
                            AddPrice.SaveType = 4;
                            if (ValiDatorForm.validator($("#ul_AddPrice_Btn").closest("form").get(0), "alert")) {
                                _s.html("正在提交..");
                                AddPrice.Save();
                            }
                            break;
                        //取消报价                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
                        case "btnCanel":
                            if (AddPrice.CanelBox) { AddPrice.CanelBox.hide(); }
                            AddPrice.CanelBox = new Boxy($("#div_Canel"), { modal: true, fixed: false, title: "报价(取消)" });
                            break;
                        //打印                                                                                                                                                                                                                                                                                                                                                                      
                        case "btnPrint":
                            window.open(this.href);
                            break;
                    }

                    return false;
                })


                $("#btnCanelSave").unbind("click").click(function() {
                    var remarks = $.trim($("#<%=txtCanelRemark.ClientID %>").val());
                    if (remarks.length > 200) {
                        tableToolbar._showMsg("输入的取消原因过长!");
                        return false;
                    }
                    if (remarks.length == 0) {
                        tableToolbar._showMsg("请输入取消原因!");
                        return false;
                    }
                    AddPrice.CanelBox.hide();
                    AddPrice.SaveType = 5;
                    AddPrice.Save();
                    return false;
                })

            },
            Save: function() {
                AddPrice.UnBindBtn();
                //同步编辑器数据到文本框
                KEditer.sync();
                if (AddPrice.Data.act == "copy") {
                    AddPrice.Data.id = "";
                }
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Quote/BaoJiaEdit.aspx?dotype=save&saveType=" + this.SaveType + "&" + $.param(AddPrice.Data),
                    data: $("#ul_AddPrice_Btn").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = "/Quote/BaoJia.aspx?type=" + AddPrice.Data.type + "&sl=" + AddPrice.Data.sl;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                            AddPrice.BindBtn();
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        AddPrice.BindBtn();
                    }
                });
            },
            //按钮解除绑定
            UnBindBtn: function() {
                $("#ul_AddPrice_Btn").find("a").unbind("click");
                $("#ul_AddPrice_Btn").find("a").css("background-position", "0 -62px");
            },
            PageInit: function() {
                $("#<%=ddlCountry.ClientID %>,#<%=ddlTourMode.ClientID %>").change(function() {
                    $(this).parent().find("input[type='hidden']").val($(this).val());
                })
                $("#<%=txtStartEffectTime.ClientID %>,#<%=txtEndEffectTime.ClientID %>").blur(function() {
                    $(this).prev("input[type='hidden']").val($(this).val());
                })

                $("#DivItems input").blur(function() {
                    AddPrice.SetTotalPrice();
                    AddPrice.SetHeJiPrice();
                })
                $("#DivPrices input").blur(function() {
                    AddPrice.SetTotalPrice();
                    AddPrice.SetHeJiPrice();
                })

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
                    AddPrice.SetHeJiPrice();
                })
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

        $(function() {
            //alert($("#ctl00_ContentPlaceHolder1_SelectJourney1_hd_OldId").val());
            //创建编辑器
            AddPrice.CreatePlanEdit();
            AddPrice.SetSumPrice();
            FV_onBlur.initValid($("#ul_AddPrice_Btn").closest("form").get(0));
            AddPrice.BindBtn();
            AddPrice.PageInit();
            $("#hidtourmode").val($("#<%=ddlTourMode.ClientID %>").val());

            $("#hidsdate").val($("#<%=txtStartEffectTime.ClientID %>").val());
            $("#hidedate").val($("#<%=txtEndEffectTime.ClientID %>").val());

            $("#<%=this.txtStartEffectTime.ClientID %>").blur(function() {
                $("#hidsdate").val($(this).val());
            });
            $("#<%=this.txtEndEffectTime.ClientID %>").blur(function() {
                $("#hidedate").val($(this).val());
            });



            $("#<%=this.ddlTourMode.ClientID %>").change(function() {
                $("#hidtourmode").val($(this).val());
                $("#<%=hidTourModeValue.ClientID %>").val($(this).val());
            });
            //ddlCountry


            $("#hidbinkemode").val($("#<%=this.ddlCountry.ClientID %>").val());

            $("#<%=this.ddlCountry.ClientID %>").change(function() {
                $("#hidbinkemode").val($(this).val());
            });
            $("#tbl_Journey_AutoAdd").autoAdd({ changeInput: $("#<%=txt_Days.ClientID %>"), addCallBack: Journey.AddRowCallBack, upCallBack: Journey.MoveRowCallBack, downCallBack: Journey.MoveRowCallBack, delCallBack: Journey.DelRowCallBack, delStartCall: Journey.StartFun });

            $("#Tab_Price_Price").autoAdd({ tempRowClass: "tempRowprice", addButtonClass: "addbtnprice", delButtonClass: "delbtnprice" });
            $("#Tab_Item_Price").autoAdd({ tempRowClass: "tempRowitem", addButtonClass: "addbtnitem", delButtonClass: "delbtnitem" });
            $("#Tab_Group_Price").autoAdd({ tempRowClass: "tempRowgroup", addButtonClass: "addbtngroup", delButtonClass: "delbtngroup" });

            //添加天数
            $("#btnAddDays").click(function() {
                var day = tableToolbar.getInt($("#<%=txt_Days.ClientID %>").val());
                day++;
                $("#<%=txt_Days.ClientID %>").val(day);
                $("#<%=txt_Days.ClientID %>").change();
            });

            $("#ul_AddPrice_Btn .orderlist").click(function() {
                var url = $(this).attr("href");
                window.open(url);
                return false;
            })

            //备注
            $("._imgremark").toggle(function() {
                $(this).next("span").show();
            }, function() {
                $(this).next("span").hide();
            });
            var type = $("#<%=hidItemType.ClientID %>").val();
            PriceItemType(type, 1);

            var quoteid = $(".tablehead1").find("li").eq(0).find("a").attr("data-id");
            if (quoteid) {
                $("#<%=hidQuoteId.ClientID %>").val(quoteid);
            }

            $("#DivBaoJia").find("select").change(function() {
                $(this).closest("td").find("input[type='text']").eq(0).attr("data-UnitType", $(this).val());
                AddPrice.SetHotelPrice();
                AddPrice.SetTotalPrice();
                AddPrice.SetHeJiPrice();
            });
            $(".firsttable").delegate(".delimg", "click", function() {
                $(this).closest("span[class='upload_filename']").remove();
                return false;
            })

            $("#<%=this.txtTourLDate.ClientID %>").blur(function() {
                var data = { LDate: $(this).val(), Days: $("#<%=this.txt_Days.ClientID %>").val() };
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Quote/BaoJiaEdit.aspx?dotype=GetRDate&" + $.param(data),
                    data: $("#ul_AddPrice_Btn").closest("form").serialize(),
                    dataType: "text",
                    success: function(ret) {
                        //ajax回发提示
                        $("#<%=this.txtTourRDate.ClientID %>").val(ret);
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            });
        });

        /*
        @分项整团分类显示
        1 整团
        2 分项
        */
        function PriceItemType(ItemType, type) {
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
            if (type == 0) {//等于一的时候是新增初始化页面，不需要调用计算的方法
                Journey.SumPriceJingDian();
                AddPrice.SetHotelPrice();
                AddPrice.SetTrafficPrice();
                AddPrice.SumMenuPrice();
                AddPrice.SetTotalPrice();
                AddPrice.SetHeJiPrice();
            }
        }

        function CallBackCustomerUnit(obj) {
            $("#<%=txtContact.ClientID %>").val(obj.CustomerUnitContactName);
            $("#<%=txtPhone.ClientID %>").val(obj.CustomerUnitMobilePhone);
            $("#<%=txtFax.ClientID %>").val(obj.ContactFax);
            $("#hidbinkemode").val(obj.CustomerUnitGuoji);
            $("#<%=ddlCountry.ClientID %>").val(obj.CustomerUnitGuoji);
            $("#<%=LvPrice.ClientID %>").val(obj.CustomerLvPrice);
            AddPrice.SetTotalPrice();
            AddPrice.SetHeJiPrice();
        }
	  
    </script>

</asp:Content>

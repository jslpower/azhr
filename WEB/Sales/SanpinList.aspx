<%@ Page Title="组团散拼" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="SanpinList.aspx.cs" Inherits="EyouSoft.Web.Sales.SanpinList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form method="get">
            <input type="hidden" name="type" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>" />
            <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <span class="searchT">
                <p>
                    <%-- 客户单位：
                    <input type="text" name="txtCompanyName" class="formsize120 input-txt" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtCompanyName") %>">--%>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                    团号：
                    <input type="text" class="inputtext formsize100" name="txtTourCode" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>" />
                    <%} %>
                    线路区域：
                    <select name="ddlArea" class="inputselect" style="width: 120px;">
                        <%=EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("ddlArea")),SiteUserInfo.CompanyId) %>
                    </select>
                    线路名称：
                    <input class="inputtext formsize120" name="txtRouteName" size="28" type="text" value='<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>' />
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                    出团时间：
                    <input onfocus="WdatePicker()" type="text" class="inputtext formsize100" name="txtOutDateS"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOutDateS") %>" />
                    -
                    <input onfocus="WdatePicker()" type="text" class="inputtext formsize100" name="txtOutDateE"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtOutDateE") %>" />
                    回团时间：
                    <input onfocus="WdatePicker()" type="text" class="inputtext formsize100" name="txtGetDateS"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtGetDateS") %>" />
                    -
                    <input onfocus="WdatePicker()" type="text" class="inputtext formsize100" name="txtGetDateE"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtGetDateE") %>" />
                    <%} %>
                    业务员：
                    <uc2:SellsSelect ID="SellsSelect1" runat="server" SetTitle="销售员" SelectFrist="false" />
                    <%--团队状态：
                    <select name="selState" class="inputselect" style="width: 80px;">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourSureStatus)), EyouSoft.Common.Utils.GetQueryStringValue("selState"))%>
                    </select>--%>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                    操作状态：<select name="sltTourState" class="inputselect">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus),new string[]{"13","14","15"}), EyouSoft.Common.Utils.GetQueryStringValue("sltTourState")) %>
                    </select>
                    <%} %>
                    <button class="search-btn" type="submit">
                        搜索</button></p>
            </span>
            </form>
        </div>
        <div class="tablehead">
            <ul class="fixed" id="btnAction">
                <asp:PlaceHolder ID="phForAdd" runat="server">
                    <li><s class="addicon"></s><a id="GoURL" class="toolbar_add" hidefocus="true" href="AddSanpin.aspx?type=<%=type %>&sl=<%=sl %>&act=add">
                        <span>
                            <label id="addRoute" runat="server" style="display: inline" for="GoURL">
                                新增</label>
                        </span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                <asp:PlaceHolder ID="phForOper" runat="server">
                    <li><s class="ptjdicon"></s><a class="toolbar_paiduan" hidefocus="true" href="javascript:void(0);">
                        <span>派团</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                    <%} %>
                <%--                <asp:PlaceHolder ID="phForKeMan" runat="server">
                    <li><s class="keman"></s><a class="toolbar_keman" hidefocus="true" href="javascript:void(0);">
                        <span>客满</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForStop" runat="server">
                    <li><s class="stop"></s><a class="toolbar_stop" hidefocus="true" href="javascript:void(0);">
                        <span>停收</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <li><s class="yichuli"></s><a class="toolbar_zhengc" hidefocus="true" href="javascript:void(0);">
                    <span>正常</span></a></li><li class="line"></li>
                <asp:PlaceHolder ID="phForShemHe" runat="server">
                    <li><s class="shenhe"></s><a class="toolbar_shenhe" hidefocus="true" href="javascript:void(0);">
                        <span>审核计划</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
--%>
                <asp:PlaceHolder ID="phForUpdate" runat="server">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="AddSanpin.aspx?type=<%=type %>&sl=<%=sl %>&act=update">
                        <span>修改</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForCopy" runat="server">
                    <li><s class="copyicon"></s><a class="toolbar_copy" hidefocus="true" href="AddSanpin.aspx?type=<%=type %>&sl=<%=sl %>&act=copy">
                        <span>复制</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                <asp:PlaceHolder ID="phForCanel" runat="server">
                    <li><s class="cancelicon"></s><a class="toolbar_cancel" hidefocus="true" href="javascript:void(0);">
                        <span>取消</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                    <%} %>
                <asp:PlaceHolder ID="phForDelete" runat="server">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0);"><s class="delicon">
                    </s><span>删除</span></a></li></asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" cellspacing="0" border="0" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg" rowspan="<%=this.RowSpan %>">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                        <th align="center" class="th-line" rowspan="2">
                            团号
                        </th>
                    <%} %>
                        <th align="center" class="th-line" rowspan="<%=this.RowSpan %>">
                            线路区域
                        </th>
                        <th align="left" class="th-line" rowspan="<%=this.RowSpan %>">
                            线路名称
                        </th>
                        <th align="center" class="th-line" rowspan="<%=this.RowSpan %>">
                            发布人
                        </th>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                        <th align="center" class="th-line" rowspan="2">
                            出团日期
                        </th>
                    <%} %>
                        <th align="center" class="th-line" rowspan="<%=this.RowSpan %>">
                            天数
                        </th>
                        <th align="right" class="th-line" rowspan="<%=this.RowSpan %>">
                            价格
                        </th>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                        <th align="center" class="th-line" colspan="4">
                            人数
                        </th>
                    <%} %>
                        <th align="center" class="th-line" rowspan="<%=this.RowSpan %>">
                            报名
                        </th>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                        <th align="center" class="th-line" rowspan="2">
                            订单
                        </th>
                    <%} %>
                        <th align="center" class="th-line" rowspan="<%=this.RowSpan %>">
                            销售员
                        </th>
                        <th align="center" class="th-line" rowspan="<%=this.RowSpan %>">
                            OP
                        </th>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                        <th align="center" class="th-line" rowspan="2">
                            查看计调
                        </th>
                    <%} %>
                        <%if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "3")
                          { %>
                        <th align="center" class="th-line" rowspan="2">
                            签证状态
                        </th>
                        <%} %>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                       <th align="center" class="th-line" rowspan="2">
                            状态
                        </th>
                    <%} %>
                     </tr>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                    <tr class="">
                        <th bgcolor="#137CBF" align="center" class="th-line nojiacu">
                            预
                        </th>
                        <th bgcolor="#137CBF" align="center" class="th-line nojiacu">
                            留
                        </th>
                        <th bgcolor="#137CBF" align="center" class="th-line nojiacu">
                            实
                        </th>
                        <th bgcolor="#137CBF" align="center" class="th-line nojiacu">
                            剩
                        </th>
                    </tr>
                    <%} %>
                    <asp:Repeater runat="server" ID="rpt_List">
                        <ItemTemplate>
                            <tr <%#Eval("SourceId") == null || Eval("SourceId").ToString().Trim()=="" ? "style='background-color:#fff'":" style='background-color:#F3F3F3'"%>>
                                <td align="center">
                                    <input type="checkbox" id="checkbox" data-tourtype='<%#Eval("TourType") %>' value="<%#Eval("TourId") %>"
                                        tourstatus='<%#Eval("TourSureStatus") %>'>
                                </td>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                                <td align="center">
                                    <a target="_blank" href="/PrintPage/XingChengDan.aspx?TourId=<%#Eval("TourId")%>">
                                        <%#Eval("TourCode")%></a><%#GetChangeInfo((bool)Eval("IsChange"), (bool)Eval("IsSure"), Eval("tourId").ToString(), Eval("TourStatus").ToString())%>
                                </td>
                    <%} %>
                                <td align="center">
                                    <%#Eval("AreaName")%>
                                </td>
                                <td align="left" data-class="newSet" data-tourid='<%#Eval("TourId") %>' data-tourtype='<%#Eval("TourType") %>'>
                                    
                                        <%#Eval("RouteName")%><font class="fontred"><%#GetTourType(Eval("TourType")) %></font>
                                </td>
                                <td align="center" data-class="opertorInfo">
                                    <%#GetOperatorInfo(Eval("SourceId"), Eval("OperatorInfo"), Eval("SourceCompanyName"))%>
                                </td>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString( Eval("LDate"),this.ProviderToDate)%>
                                </td>
                    <%} %>
                                <td align="center">
                                    <%#Eval("TourDays")%>
                                </td>
                                <td align="right">
                                    <a data-class="paopaoPrice" href="javascript:void(0);"><b class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), this.ProviderToMoney)%>
                                    </b></a>
                                    <div style="display: none;">
                                        <%#GetPriceByTour(Eval("tourId").ToString()) %></div>
                                </td>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                                <td align="center">
                                    <%#Eval("PlanPeopleNumber")%>
                                </td>
                                <td align="center">
                                    
                                        <%#Eval("LeavePeopleNumber")%>
                                </td>
                                <td align="center">
                                    
                                        <%#Eval("Adults")%><sup class="fontred">+<%#Eval("Childs")%></sup>
                                </td>
                                <td align="center">
                                    <%#Eval("PeopleNumberLast")%>
                                </td>
                    <%} %>
                                <td align="center">
                                    <%#GetHtmlByShouKeState(Eval("tourId"), Eval("TourShouKeStatus"), Eval("SourceId"), Eval("IsCheck"), Eval("TourStatus"), Eval("TourType"))%>
                                </td>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                                <td align="center">
                                    <a class="fontblue" title="查看" href="/Sales/OrderList.aspx?tourID=<%#Eval("TourId") %>&sl=<%#sl %>">
                                        共计<%#Eval("OrderCount")%>单</a>
                                </td>
                    <%} %>
                                <td align="center">
                                    <%#Eval("SaleInfo.Name")%>
                                </td>
                                <td align="center">
                                    <%#(Eval("TourPlaner") != null && ((System.Collections.Generic.IList<EyouSoft.Model.TourStructure.MTourPlaner>)Eval("TourPlaner")).Count>0)?((System.Collections.Generic.IList<EyouSoft.Model.TourStructure.MTourPlaner>)Eval("TourPlaner"))[0].Planer:"" %>
                                    <input type="hidden" name="ItemUserID" value="<%# Eval("SaleInfo")==null ?"": ((EyouSoft.Model.TourStructure.MSaleInfo)Eval("SaleInfo")).SellerId%>" />
                                </td>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                                <td align="center" data-class="GetJiDiaoIcon" data-tourid="<%#Eval("tourId") %>">
                                    <%#EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.HTourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%>
                                </td>
                    <%} %>
                                <%if (EyouSoft.Common.Utils.GetQueryStringValue("type") == "3")
                                  { %>
                                <td align="center">
                                    <a href="javascript:void(0);" data-id="<%#Eval("TourId") %>" data-class="visaFiles">
                                        查看资料</a>
                                </td>
                                <%} %>
                    <%if (!string.IsNullOrEmpty(this.ParentId)) %>
                    <%{ %>
                                <td align="center">
                                    <input type="hidden" name="hideTourStatus" value="<%#(int)Eval("TourStatus")%>" />
                                    <input type="hidden" name="hideSourceId" value="<%#Eval("SourceId")%>" />
                                    <input type="hidden" name="hideIsCheck" value="<%#Eval("IsCheck").ToString().ToLower()%>" />
                                    <input type="hidden" name="hideIsPayMoney" value="<%#Eval("IsPayMoney").ToString().ToLower()%>" />
                                    <%#Eval("TourStatus").ToString() == "已取消" ? "<a data-class='cancelReason'><span class='fontgray' data-class='QuoteState' data-state='0'>已取消</span></a><div style='display: none'><b>取消原因</b>:" + EyouSoft.Common.Function.StringValidate.TextToHtml( Eval("CancelReson").ToString()) + "</div>" : Eval("TourStatus").ToString()%>
                                </td>
                    <%} %>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litMsg" runat="server" Text="<tr><td align='center' colspan='18'>暂无散拼计划!</td></tr>"></asp:Literal>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border-top: 0 none;" class="tablehead">
            <ul class="fixed">

                <script type="text/javascript">
                    document.write(document.getElementById("btnAction").innerHTML);
				</script>

            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
    </div>
    <div class="alertbox-outbox03" id="div_Canel" style="display: none; padding-bottom: 0px;">
        <div class="hr_10">
        </div>
        <table width="600px" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto">
            <tbody>
                <tr>
                    <td width="80" height="28" align="right" class="alertboxTableT">
                        取消原因：
                    </td>
                    <td height="28" bgcolor="#E9F4F9" align="left">
                        <textarea style="height: 93px;" class="inputtext formsize450" id="txtCanelRemark"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a href="javascript:void(0);" id="btnCanelSave"><s class="baochun"></s>保 存</a><a
                href="javascript:void(0);" onclick="javascript:SanPinList.CanelBox.hide();return false;"><s
                    class="chongzhi"></s>关闭</a>
        </div>
    </div>

    <script type="text/javascript">
        var SanPinList = {
            Data: {
                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                type: '<%=EyouSoft.Common.Utils.GetQueryStringValue("type") %>'
            },
            OpenBaoMing: function(obj) {
                window.location.href = $(obj).attr("href") + '&type=' +SanPinList.Data.type+ "&url=<%=Server.UrlEncode(Request.Url.ToString()) %>";
            },
            GetStringByArr: function(arr) {
                var list = new Array();
                //遍历按钮返回数组对象
                for (var i = 0; i < arr.length; i++) {
                    //从数组对象中找到数据所在，并保存到数组对象中
                    if (arr[i].find("input[type='checkbox']").val() != "on") {
                        list.push(arr[i].find("input[type='checkbox']").val());
                    }
                }
                //获取默认路径并重新拼接url（注：全局变量劲量不要改变，当常量就行）
                return list.join(',');
            },
            CanelBox: null,
            RightClick: function(type, id,status) {
                switch (type) {
                    case "update":
                        var tr = $("#liststyle").find("input[type='checkbox'][value='" + id + "']").closest("tr");
                            window.location.href = "/Sales/AddSanpin.aspx?isparent=<%=string.IsNullOrEmpty(this.ParentId)%>&act=update&id=" + id + "&" + $.param(SanPinList.Data);
                        break;
                    case "paituan":
                        
                        if (tableToolbar.IsHandleElse == "false") {
                            var msgList = [], tr = $("#liststyle").find("input[type='checkbox'][value='" + id + "']").closest("tr");
                            if (tr.find("input[name='ItemUserID']").val() != tableToolbar.UserID) {
                                msgList.push("你不是该计划的销售员,无法派团计调!");
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join("<br />"));
                                return false;
                            }
                        }

                        Boxy.iframeDialog({
                            iframeUrl: "/Sales/PaiTuan.aspx?id=" + id + "&" + $.param(SanPinList.Data),
                            title: "派团给计调",
                            modal: true,
                            width: "560px",
                            height: "252px"
                        });
                        $("div.bt-wrapper").hide();
                        break;
//                    case "shenhe":
//                        Boxy.iframeDialog({
//                            iframeUrl: "/TeamCenter/CheckTour.aspx?id=" + id + "&" + $.param(SanPinList.Data),
//                            title: "审核计划",
//                            modal: true,
//                            width: "820px",
//                            height: "300px"
//                        });
//                        $("div.bt-wrapper").hide();
//                        break;
//                    case "copy":
//                         var tr = $("#liststyle").find("input[type='checkbox'][value='" + id + "']").closest("tr");
//                        if ($.trim(tr.find(":checkbox").attr("data-tourtype")) == "组团散拼短线") {
//                            window.location.href = "/TeamCenter/AddShortSanpin.aspx?act=copy&id=" + id + "&" + $.param(SanPinList.Data);
//                         }
//                         else{
//                            window.location.href = "/TeamCenter/AddSanpin.aspx?act=copy&id=" + id + "&" + $.param(SanPinList.Data);
//                         }
//                        break;
//                    case "updateCarType":
//                        if (tableToolbar.IsHandleElse == "false") {
//                            var msgList = [], tr = $("#liststyle").find("input[type='checkbox'][value='" + id + "']").closest("tr");
//                            if (tr.find("input[name='ItemUserID']").val() != tableToolbar.UserID) {
//                                msgList.push("你不是该计划的销售员,无法修改车型!");
//                            }
//                            if (msgList.length > 0) {
//                                tableToolbar._showMsg(msgList.join("<br />"));
//                                return false;
//                            }
//                        }
//                         Boxy.iframeDialog({
//                            iframeUrl: "/TeamCenter/UpdatePresetBusType.aspx?tourId=" + id + "&" + $.param(SanPinList.Data),
//                            title: "修改预定车型",
//                            modal: true,
//                            width: "450px",
//                            height: "270px"
//                        });
//                        $("div.bt-wrapper").hide();
//                        break;
                }
                return false;
            },
            GetFristHtml: function(tr) {
                var html = [], id = tr.find("td:eq(0)").find("input[type='checkbox']").val();
                var Status=tr.find("input[type='checkbox']").attr("TourStatus");
                if (this.Power[0]) {
                    html.push("<a onclick=SanPinList.RightClick('update','" + id + "','') href='javascript:void(0);' hidefocus='true' class='toolbar_update'><s class='updateicon'></s>修改</a>");
                    if($.trim(tr.find("input[type='checkbox']").attr("data-tourtype"))=="组团散拼短线"){
                        html.push("<a onclick=SanPinList.RightClick('updateCarType','" + id + "','') href='javascript:void(0);' hidefocus='true' class='toolbar_updatecartype'><s class='updateicon'></s>修改预定车型</a>");
                    }
                }

                if ((tr.find("input[name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 %>"||tr.find("input[name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 %>") && this.Power[1]) {
                    html.push("<a onclick=SanPinList.RightClick('paituan','" + id + "','"+Status+"') href='javascript:void(0);' hidefocus='true' class='toolbar_paiduan'><s class='ptjdicon'></s>派团给计调</a>");
                }
//                if (this.Power[2] && tr.find("input[name='hideIsCheck']").val() == "false") {
//                    html.push("<a onclick=SanPinList.RightClick('shenhe','" + id + "') href='javascript:void(0);' hidefocus='true' class='toolbar_shenhe'><s class='shenhe'></s>审核计划</a>");
//                }
                if (this.Power[3]) {
                    html.push("<a onclick=SanPinList.RightClick('copy','" + id + "','') href='javascript:void(0);' hidefocus='true' class='toolbar_shenhe'><s class='copyicon'></s>复制</a>");
                }
                return html.join('');
            },
            Power:[<%=ListPower %>]
        }

        $(function() {
            var url = location.href;
            var type = querystring(url, "type");
            var sl = querystring(url, "sl");
            tableToolbar.init({
                objectName: "计划",
                copyCallBack: function(arr) {
                    if ($.trim(arr[0].find(":checkbox").attr("data-tourtype")) == "组团散拼短线") {
                        location.href = "AddShortSanpin.aspx?type=" + type + "&sl=" + sl + "&act=copy&id=" + arr[0].find(":checkbox").val();
                    } else {
                        location.href = "AddSanpin.aspx?type=" + type + "&sl=" + sl + "&act=copy&id=" + arr[0].find(":checkbox").val();
                    }
                    return false;
                },
                updateCallBack: function(arr) {
                    if ($.trim(arr[0].find(":checkbox").attr("data-tourtype")) == "组团散拼短线") {
                        location.href = "AddShortSanpin.aspx?type=" + type + "&sl=" + sl + "&act=update&id=" + arr[0].find(":checkbox").val();
                    } else {
                        location.href = "AddSanpin.aspx?isparent=<%=string.IsNullOrEmpty(this.ParentId)%>&type=" + type + "&sl=" + sl + "&act=update&id=" + arr[0].find(":checkbox").val();
                    }
                    return false;
                },
                deleteCallBack: function(arr) {
                    var msgList = new Array();
                    var state = "";
                    //遍历按钮返回数组对象
                    for (var i = 0; i < arr.length; i++) {
                        //从数组对象中找到数据所在，并保存到数组对象中
                        if (arr[i].find("input[type='checkbox']").val() != "on") {
                            state = arr[i].find("input[name='hideIsPayMoney']").val();
                            if (state == "true") {
                                msgList.push("当前选中项中第" + (i + 1) + "行有计调支出,无法删除!");
                            }
                        }
                    }
                    if (msgList.length > 0) {
                        tableToolbar._showMsg(msgList.join("<br />"));
                        return false;
                    }
                    //执行
                    $.newAjax({
                        type: "get",
                        cache: false,
                        url: "/Sales/SanpinList.aspx?dotype=delete&ids=" + SanPinList.GetStringByArr(arr) + "&" + $.param(SanPinList.Data),
                        dataType: "json",
                        success: function(ret) {
                            if (ret.result == "1") {
                                tableToolbar._showMsg("删除成功!正在刷新列表..", function() {
                                    window.location.href = window.location.href;
                                })
                            } else {
                                tableToolbar._showMsg("删除失败!");
                            }
                        },
                        error: function() {
                            tableToolbar._showMsg(tableToolbar.errorMsg);
                        }
                    });
                },
                cancelCallBack: function(arr) {
                    var msgList = new Array();
                    var state = "";
                    var cancel = "";
                    //遍历按钮返回数组对象
                    for (var i = 0; i < arr.length; i++) {
                        //从数组对象中找到数据所在，并保存到数组对象中
                        if (arr[i].find("input[type='checkbox']").val() != "on") {
                            state = arr[i].find("input[name='hideIsPayMoney']").val();
                            if (state == "true") {
                                msgList.push("当前选中项中第" + (i + 1) + "行有计调支出,无法取消!");
                            }
                            if (cancel == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消 %>") {
                                msgList.push("当前选中项中第" + (i + 1) + "行已取消,无法再次取消!");
                            }
                        }
                    }
                    if (msgList.length > 0) {
                        tableToolbar._showMsg(msgList.join("<br />"));
                        return false;
                    }
                    SanPinList.CanelBox = new Boxy($("#div_Canel"), { modal: true, fixed: false, title: "取消", width: "580px", height: "210px" });
                },
                otherButtons: [{
                    button_selector: '.toolbar_paiduan',
                    sucessRulr: 1,
                    msg: '未选中任何 计划 ',
                    msg2: '只能选择一计划 ',
                    buttonCallBack: function(arr) {
                        
                        if (tableToolbar.IsHandleElse == "false") {
                            var msgList = new Array();
                            if (arr[0].find("input[name='ItemUserID']").val() != tableToolbar.UserID) {
                                msgList.push("你不是该计划的销售员,无法派团计调!");
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join("<br />"));
                                return false;
                            }
                        }
                        if (arr[0].find("input[type='hidden'][name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 %>"
                            ||arr[0].find("input[type='hidden'][name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 %>") {
                            Boxy.iframeDialog({
                                iframeUrl: "/Sales/PaiTuan.aspx?id=" + SanPinList.GetStringByArr(arr) + "&" + $.param(SanPinList.Data),
                                title: "派团给计调",
                                modal: true,
                                width: "560px",
                                height: "252px"
                            });
                        } else {
                            tableToolbar._showMsg("该团已派计调,无法重复派团!");
                        }
                        return false;
                    }
                }, 
//                {
//                    button_selector: '.toolbar_shenhe',
//                    sucessRulr: 1,
//                    msg: '未选中任何 计划 ',
//                    msg2: '只能选择一计划 ',
//                    buttonCallBack: function(arr) {
//                        if (arr[0].find("input[type='hidden'][name='hideSourceId']").val() != "" && arr[0].find("input[type='hidden'][name='hideIsCheck']").val() == "false") {
//                            Boxy.iframeDialog({
//                                iframeUrl: "/TeamCenter/CheckTour.aspx?id=" + SanPinList.GetStringByArr(arr) + "&" + $.param(SanPinList.Data),
//                                title: "审核计划",
//                                modal: true,
//                                width: "820px",
//                                height: "300px"
//                            });
//                        } else {
//                            tableToolbar._showMsg("该团已被审核或不是供应商发布，无法审核!");
//                        }

//                        return false;
//                    }
//                },
//                    {
//                        button_selector: '.toolbar_keman',
//                        sucessRulr: 2,
//                        msg: '未选中任何 计划 ',
//                        buttonCallBack: function(arr) {
//                            $.newAjax({
//                                type: "get",
//                                cache: false,
//                                url: "/TeamCenter/SanpinList.aspx?dotype=keman&ids=" + SanPinList.GetStringByArr(arr) + "&" + $.param(SanPinList.Data),
//                                dataType: "json",
//                                success: function(ret) {
//                                    if (ret.result == "1") {
//                                        tableToolbar._showMsg(ret.msg, function() {
//                                            window.location.href = window.location.href;
//                                        })
//                                    } else {
//                                        tableToolbar._showMsg(ret.msg);
//                                    }
//                                },
//                                error: function() {
//                                    tableToolbar._showMsg(tableToolbar.errorMsg);
//                                }
//                            });
//                            return false;
//                        }
//                    }
//                    ,
//                    {
//                        button_selector: '.toolbar_stop',
//                        sucessRulr: 2,
//                        msg: '未选中任何 计划 ',
//                        buttonCallBack: function(arr) {
//                            $.newAjax({
//                                type: "get",
//                                cache: false,
//                                url: "/TeamCenter/SanpinList.aspx?dotype=stop&ids=" + SanPinList.GetStringByArr(arr) + "&" + $.param(SanPinList.Data),
//                                dataType: "json",
//                                success: function(ret) {
//                                    if (ret.result == "1") {
//                                        tableToolbar._showMsg(ret.msg, function() {
//                                            window.location.href = window.location.href;
//                                        })
//                                    } else {
//                                        tableToolbar._showMsg(ret.msg);
//                                    }
//                                },
//                                error: function() {
//                                    tableToolbar._showMsg(tableToolbar.errorMsg);
//                                }
//                            });
//                            return false;
//                        }
//                    },
                    {
                        button_selector: '.toolbar_zhengc',
                        sucessRulr: 2,
                        msg: '未选中任何 计划 ',
                        buttonCallBack: function(arr) {
                            var msgList = new Array();
                            //遍历按钮返回数组对象
                            for (var i = 0; i < arr.length; i++) {
                                if (arr[i].find("b[data-class='tingshou']").length > 0) {
                                    msgList.push("当前选中项中第" + (i + 1) + "行已自动停收或客满,不能设置收客!");
                                }
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join("<br />"));
                                return false;
                            }

                            $.newAjax({
                                type: "get",
                                cache: false,
                                url: "/TeamCenter/SanpinList.aspx?dotype=zhengc&ids=" + SanPinList.GetStringByArr(arr) + "&" + $.param(SanPinList.Data),
                                dataType: "json",
                                success: function(ret) {
                                    if (ret.result == "1") {
                                        tableToolbar._showMsg(ret.msg, function() {
                                            window.location.href = window.location.href;
                                        })
                                    } else {
                                        tableToolbar._showMsg(ret.msg);
                                    }
                                },
                                error: function() {
                                    tableToolbar._showMsg(tableToolbar.errorMsg);
                                }
                            });
                            return false;
                        }
                    }
]
            });

//            $("#liststyle").find("a[data-class='visaFiles']").click(function() {
//                Boxy.iframeDialog({
//                    iframeUrl: "/TeamCenter/VisaFileList.aspx?tourId=" + $(this).attr("data-id") + "&" + $.param(SanPinList.Data),
//                    title: "查看签证资料",
//                    modal: true,
//                    width: "650px",
//                    height: "420px"
//                });
//                return false;
//            })

            $("#liststyle").find("a[data-class='paopaoPrice']").each(function() {
                if ($.trim($(this).next().html()) != "") {
                    $(this).next().find("tr").removeClass("odd");
                    $(this).bt({
                        contentSelector: function() {
                            return $(this).next().html();
                        },
                        positions: ['bottom'],
                        fill: '#FFF2B5',
                        strokeStyle: '#D59228',
                        noShadowOpts: { strokeStyle: "#D59228" },
                        spikeLength: 5,
                        spikeGirth: 15,
                        width: 700,
                        overlap: 0,
                        centerPointY: 4,
                        cornerRadius: 4,
                        shadow: true,
                        shadowColor: 'rgba(0,0,0,.5)',
                        cssStyles: { color: '#00387E', 'line-height': '200%' }
                    });
                }
            })

//            $("#liststyle").find("td[data-class='opertorInfo']").mouseover(function() {
//                var td = $(this);
//                var a = td.find("a[data-class='paopao']");
//                var data = { sl: SanPinList.Data.sl, doType: 'contact', sourceId: "", operId: "" };
//                data.sourceId = a.attr("data-sourceId");
//                data.operId = a.attr("data-operId");
//                if (td.find("span").length == 0) {
//                    $.newAjax({
//                        type: "get",
//                        cache: false,
//                        url: "/TeamCenter/SanpinList.aspx?" + $.param(data),
//                        dataType: "html",
//                        success: function(ret) {
//                            if (ret) {
//                                td.append(ret);
//                                a.bt({
//                                    contentSelector: function() {
//                                        return $(this).next().html();
//                                    },
//                                    positions: ['right'],
//                                    fill: '#FFF2B5',
//                                    strokeStyle: '#D59228',
//                                    noShadowOpts: { strokeStyle: "#D59228" },
//                                    spikeLength: 10,
//                                    spikeGirth: 15,
//                                    width: 200,
//                                    overlap: 0,
//                                    centerPointY: 1,
//                                    cornerRadius: 4,
//                                    shadow: true,
//                                    shadowColor: 'rgba(0,0,0,.5)',
//                                    cssStyles: { color: '#00387E', 'line-height': '180%' }
//                                });
//                            }
//                        }
//                    });
//                }
//            })

            $("#liststyle").find("a[data-class='cancelReason']").each(function() {
                if ($.trim($(this).next().html()) != "") {
                    $(this).bt({
                        contentSelector: function() {
                            return $(this).next().html();
                        },
                        positions: ['left', 'right', 'bottom'],
                        fill: '#FFF2B5',
                        strokeStyle: '#D59228',
                        noShadowOpts: { strokeStyle: "#D59228" },
                        spikeLength: 10,
                        spikeGirth: 15,
                        width: 200,
                        overlap: 0,
                        centerPointY: 1,
                        cornerRadius: 4,
                        shadow: true,
                        shadowColor: 'rgba(0,0,0,.5)',
                        cssStyles: { color: '#00387E', 'line-height': '180%' }
                    });
                }
            })

            //计划取消 确认事件
            $("#btnCanelSave").click(function() {
                var ids = new Array();
                $("#liststyle").find("input[type='checkbox']:checked").each(function() {
                    if (this.value && this.value != "on") {
                        ids.push(this.value);
                    }
                })
                var remarks = $.trim($("#txtCanelRemark").val());
                if (remarks.length == 0) {
                    tableToolbar._showMsg("请输入取消原因!");
                    return false;
                }
                if (remarks.length > 200) {
                    tableToolbar._showMsg("输入的取消原因过长!");
                    return false;
                }
                $.newAjax({
                    type: "GET",
                    url: "/Sales/SanpinList.aspx?doType=canel&ids=" + ids.join(',') + "&remarks=" + encodeURIComponent(remarks) + "&" + $.param(SanPinList.Data),
                    dataType: "json",
                    success: function(r) {
                        if (r.result == "1") {
                            SanPinList.CanelBox.hide();
                            tableToolbar._showMsg("取消成功,正在刷新页面!", function() {
                                window.location.href = window.location.href;
                            });
                        } else {
                            tableToolbar._showMsg(r.msg);
                        }
                    }
                })
                return false;
            })

            var NewSetP = {

        }

        $('td[data-class="newSet"]').bt({
            contentSelector: function() {
                var DivStr="";
                var powers= SanPinList.Power;
                if(!(powers[0]==false && powers[1]==false && powers[2]==false && powers[3]==false)){
                    DivStr="<div class='td_tablehead'>" + SanPinList.GetFristHtml($(this).parent()) + "</div>";
                }
                return DivStr;
            },
            positions: ['right'],
            fill: '#dff5ff',
            strokeStyle: '#46abdc',
            noShadowOpts: { strokeStyle: "#46abdc" },
            spikeLength: 5,
            spikeGirth: 15,
            width: 120,
            overlap: 0,
            centerPointY: 1,
            cornerRadius: 0,
            shadow: true,
            shadowColor: 'rgba(0,0,0,.5)',
            cssStyles: { color: '#00387E', 'line-height': '180%' }
        });

        BtFun.InitBindBt("GetJiDiaoIcon");
    });
    </script>

</asp:Content>

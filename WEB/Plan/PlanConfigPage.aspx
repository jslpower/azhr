<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="PlanConfigPage.aspx.cs" Inherits="EyouSoft.Web.Plan.PlanConfigPage" %>

<%@ Register TagName="PlanConfigMenu" TagPrefix="uc1" Src="/UserControl/PlanConfigMenu.ascx" %>
<%@ Import Namespace="EyouSoft.Common" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <div class="hr_10">
        </div>
        <div class="jd-mainbox fixed">
            <uc1:PlanConfigMenu id="PlanConfigMenu1" runat="server" />
            <div class="jdcz-main" style="background-color: #fff">
                <div class="hr_10">
                </div>
                <div class="jidiao-r">
                    <div class="bt">
                        <strong>
                            <img src="/images/jidiao-jt.gif" class="kai" alt="" />
                            团队信息 </strong>
                    </div>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="jd-table01 line-b"
                        style="display: none">
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                团号：
                            </th>
                            <td width="40%">
                                <asp:Literal ID="litTourCode" runat="server"></asp:Literal>
                                <input type="hidden" value="" runat="server" id="hidTourMode" />
                            </td>
                            <th width="15%" align="right">
                                线路区域：
                            </th>
                            <td width="30%">
                                <asp:Literal ID="litAreaName" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                线路名称：
                            </th>
                            <td>
                                <asp:Literal ID="litRouteName" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                天数：
                            </th>
                            <td>
                                <asp:Literal ID="litDays" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                人数：
                            </th>
                            <td>
                                <asp:Literal ID="litPeoples" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                用房数：
                            </th>
                            <td>
                                <asp:Literal ID="litHouses" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                团队国籍/地区：
                            </th>
                            <td>
                                <asp:Literal ID="litCountry" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                抵达时间：
                            </th>
                            <td>
                                <asp:Literal ID="litDDDate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                抵达城市：
                            </th>
                            <td>
                                <asp:Literal ID="litDDCity" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                航班/时间：
                            </th>
                            <td>
                                <asp:Literal ID="litDDHBDate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                离境时间：
                            </th>
                            <td colspan="3">
                                <asp:Literal ID="litLJDate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th width="15%" align="right" class="border-l">
                                离开城市：
                            </th>
                            <td>
                                <asp:Literal ID="litLKCity" runat="server"></asp:Literal>
                            </td>
                            <th width="15%" align="right">
                                航班/时间：
                            </th>
                            <td>
                                <asp:Literal ID="litLKHBDate" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <th align="right" class="border-l">
                                业务员：
                            </th>
                            <td>
                                <asp:Literal ID="litSellers" runat="server"></asp:Literal>
                            </td>
                            <th align="right">
                                OP：
                            </th>
                            <td>
                                <asp:Literal ID="litOperaters" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <div class="hr_10">
                    </div>
                    <div class="mass_box fontred">
                        <b>内部提醒信息：</b><asp:Literal ID="LitInterInfo" runat="server"></asp:Literal></div>
                    <div class="hr_10">
                    </div>
                    <asp:PlaceHolder ID="planItemView" runat="server" Visible="true">
                        <h2 id="secTable">
                            <p>
                                <img src="../images/jidiao-jtx.gif" class="guan" id="imgForToggle"/>
                                <asp:PlaceHolder ID="holerView5" runat="server"><a class="hover" data-id="5">地接安排</a><span
                                    class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView4" runat="server"><a data-id="4">导游安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView1" runat="server"><a data-id="1">酒店安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView2" runat="server"><a data-id="2">用车安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView9" runat="server"><a data-id="9">区间交通</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView3" runat="server"><a data-id="3">景点安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView6" runat="server"><a data-id="6">用餐安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView7" runat="server"><a data-id="7">购物安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView8" runat="server"><a data-id="8">领料安排</a><span class="line"></span></asp:PlaceHolder>
                                <asp:PlaceHolder ID="holerView13" runat="server"><a data-id="13">其它安排</a></asp:PlaceHolder>
                            </p>
                        </h2>
                        <div id="boxy-wrapper" style="position: relative;">
                            <div id="divShowList" class="floatbox" style="position: absolute;">
                                <div class="tlist">
                                    <a id="a_closeDiv" class="closeimg" href="javascript:void(0);">关闭</a>
                                    <table width="99%" border="0" id="tblItemInfoList">
                                    </table>
                                </div>
                            </div>
                            <iframe id="boxIframeId" name="boxIframeId" class="iframeUrl" style="width: 100%" scrolling="no" frameborder="0"
                                src="/Plan/PlanAyencyList.aspx?sl=<%=Utils.GetQueryStringValue("sl") %>&tourId=<%=Utils.GetQueryStringValue("tourId") %>&iframeId=boxIframeId">
                            </iframe>
                        </div>
                    </asp:PlaceHolder>
                    <div class="hr_10">
                    </div>
                </div>
            </div>
        </div>
        <div class="hr_10">
        </div>
    </div>

    <script type="text/javascript" src="/js/jquery.easydrag.handler.beta2.js"></script>

    <script type="text/javascript">
        var ConfigPage = {
            sl: '<%=Utils.GetQueryStringValue("sl") %>',
            tourID: '<%=Utils.GetQueryStringValue("tourId") %>',
            iframeId: 'boxIframeId',
            planID: '<%=Utils.GetQueryStringValue("planID") %>',
            pID: '<%=Utils.GetQueryStringValue("pID") %>',
            //修改操作
            actionType: '<%=Utils.GetQueryStringValue("action") %>',
            //折叠图标 内容隐藏，显示
            _imgIcoClick: function(obj) {
                if (obj.next().is(":hidden")) {
                    obj.find("img").attr("class", "guan").attr("src", "/images/jidiao-jtx.gif");
                    obj.next().fadeIn("slow");
                }
                else {
                    obj.find("img").attr("class", "kai").attr("src", "/images/jidiao-jt.gif");
                    obj.next().hide();
                }
            },
        	//折叠图标控制iframe内容显示隐藏
        	_img:$("#imgForToggle"),
        	_toggle:true,
        	_imgChange:function () {
                if (!$(window.frames["boxIframeId"].document).find("div[action=divfortoggle]").is(":hidden")) {
                    $(ConfigPage._img).attr("class", "guan").attr("src", "/images/jidiao-jtx.gif");
                }
                else {
                    $(ConfigPage._img).attr("class", "kai").attr("src", "/images/jidiao-jt.gif");
                }
        	},
        	_imgForToggle:function () {
        		$(window.frames["boxIframeId"].document).find("div[action=divfortoggle]").toggle();
        		ConfigPage._imgChange();
        	},
        	_ForToggle:function () {
        		$(window.frames["boxIframeId"].document).find("div[action=divfortoggle]").fadeIn();
        		ConfigPage._imgChange();
        		ConfigPage._toggle = true;
        	},
        	_ForUnToggle:function () {
        		$(window.frames["boxIframeId"].document).find("div[action=divfortoggle]").hide();
        		ConfigPage._imgChange();
        		ConfigPage._toggle = true;
        	},
            _OpenBoxy: function(title, iframeUrl, width, height, draggable) {
                parent.Boxy.iframeDialog({ title: title, iframeUrl: iframeUrl, width: width, height: height, draggable: draggable });
            },
            _iframeUrlFun: function(objId, Url) {
                $(".iframeUrl").attr("src", Url);
                objId.addClass("hover");
                objId.siblings("a").removeClass("hover");
            },
            _Url: function(pageName) {
                return "/Plan/" + pageName + ".aspx?sl=" + ConfigPage.sl + "&tourId=" + ConfigPage.tourID + "&TourMode=" + $("#<%=hidTourMode.ClientID %>").val() + "&iframeId=" + ConfigPage.iframeId + "&m=" + new Date().getTime();
            },
            _UrlGlob: function(pageName, PlanId, show) {
                return "/Plan/" + pageName + ".aspx?sl=" + ConfigPage.sl + "&tourId=" + ConfigPage.tourID + "&action=" + ConfigPage.actionType + "&" + PlanId + "=" + ConfigPage.planID + "&iframeId=" + ConfigPage.iframeId + "&typeId=" + ConfigPage.pID + "&show=" + show + "&m=" + new Date().getTime();
            },
            _BindBtn: function() {
                //折叠图标
                $("div.bt").unbind("click");
                $("div.bt").click(function() {
                    ConfigPage._imgIcoClick($(this));
                    return false;
                });
                //iframeUrl
                $("#secTable p").find("a").unbind("click");
                $("#secTable p").find("a").click(function() {
                    var _dataAID = $(this).attr("data-id");
                    $("#tblItemInfoList").html("");
                    $("#divShowList").hide();
                    if (_dataAID) {
                        switch (_dataAID) {
                            case "13":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanOtherList'));
                                break;
                            case "1":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanHotelList'));
                                break;
                            case "2":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanCarList'));
                                break;
                            case "3":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanAttractionsList'));
                                break;
                            case "4":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanGuiderList'));
                                break;
                            case "5":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanAyencyList'));
                                break;
                            case "6":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanDiningList'));
                                break;
                            case "7":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanShoppingList'));
                                break;
                            case "8":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanPickingList'));
                                break;
                            case "9":
                                ConfigPage._iframeUrlFun($(this), ConfigPage._Url('PlanPlaneList'));
                                break;
                            default: break;
                        }
                    }
                    return false;
                });

            	//折叠图标控制内容显示隐藏
            	$("#imgForToggle").unbind("click").click(function() {
            		ConfigPage._imgForToggle();
            	});
            	//iframe初始化
            	$("#boxIframeId").unbind("load").load(function() {
            		ConfigPage._imgForToggle();
            	});

            },
            SetWinHeight: function() {
                var _opiframe = document.getElementById("boxIframeId");
                if (_opiframe && !window.opera) {
                    if (_opiframe.contentDocument && _opiframe.contentDocument.body.offsetHeight) {
                        _opiframe.height = _opiframe.contentDocument.body.offsetHeight;
                    }
                    else if (_opiframe.Document && _opiframe.Document.body.scrollHeight) {
                        _opiframe.height = _opiframe.Document.body.scrollHeight;
                    }
                }

            },
            _dataInit: function() {
                ConfigPage._BindBtn();
            },
            ShowListDiv: function(html) {
                if (html != "") {
                    $("#tblItemInfoList").html(html);
                    $("#divShowList").fadeIn("fast");
                } else {
                    if ($("#tblItemInfoList").find("tr").length > 0) {
                        $("#divShowList").fadeIn("fast");
                    }
                }

                $("#tblItemInfoList").find("tr").eq(0).attr("id", "i_tr_EasydragHandler");
                $("#divShowList").setHandler("i_tr_EasydragHandler"); //set easydrag handler
            },
        	ChangePlanStatus:function (planId,status) {
        		var _Url = '/Ashx/AjaxChangePlanStatus.ashx?PlanId=' + planId + '&Status=' + status;
                $.newAjax({
                    type: "get",
                    url: _Url,
                    cache: false,
                    dataType: "text",
                    success: function(msg) {
                        tableToolbar._showMsg(msg);
//                    	window.frames["boxIframeId"].location.reload();
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                        return false;
                    }
                });
        	}
        };
        $(document).ready(function() {
            ConfigPage._dataInit();
            $("#a_closeDiv").click(function() { $("#divShowList").fadeOut("fast"); return false; });
            $("#divShowList").easydrag(); //easydrag
        });
    </script>

</asp:Content>

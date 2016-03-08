<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XunJiaEdit.aspx.cs" Inherits="EyouSoft.WebFX.XunJiaEdit"
    ValidateRequest="false" %>

<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/Journey.ascx" TagName="Journey" TagPrefix="uc2" %>
<%@ Register Src="UserControl/selectFWeiCan.ascx" TagName="selectFWeiCan" TagPrefix="uc3" %>
<%@ Register Src="UserControl/SelfPay.ascx" TagName="SelfPay" TagPrefix="uc4" %>
<%@ Register Src="UserControl/Give.ascx" TagName="Give" TagPrefix="uc5" %>
<%@ Register Src="UserControl/Tip.ascx" TagName="Tip" TagPrefix="uc6" %>
<%@ Register Src="/UserControl/SelectPriceRemark.ascx" TagName="SelectPriceRemark"
    TagPrefix="uc1" %>
<%@ Register Src="UserControl/SelectJourneySpot.ascx" TagName="SelectJourneySpot"
    TagPrefix="uc7" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=(String)GetGlobalResourceObject("string", "询价")%></title>
    <!-- InstanceEndEditable -->
    <link href="Css/fx_style.css" rel="stylesheet" type="text/css" />
    <link href="Css/boxynew.css" rel="stylesheet" type="text/css" />
    <!-- InstanceBeginEditable name="head" -->
    <!-- InstanceEndEditable -->

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <!--[if IE]><script src="Js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script type="text/javascript" src="/Js/bt.min.js"></script>

    <script type="text/javascript" src="/Js/jquery.boxy.js"></script>

    <!--[if lte IE 6]><script src="Js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

    <script src="/Js/kindeditor-4.1/kindeditor-min.js" type="text/javascript"></script>

    <script src="Js/jquery.blockUI.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <link href="/Css/jquery.autocomplete.css" rel="stylesheet" type="text/css" />

    <script src="/Js/Newjquery.autocomplete.js" type="text/javascript"></script>

    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <!--[if IE 6]>
<script type="text/javascript" src="Js/PNG.js" ></script>
<script type="text/javascript">
DD_belatedPNG.fix('*,div,img,a:hover,ul,li,p');
</script>
<![endif]-->
</head>

<script type="text/javascript">    tableToolbar.CompanyID = '<%=SiteUserInfo.CompanyId %>';</script>

<body style="background: 0 none;">
    <form id="form1" runat="server">
    <uc1:HeadDistributorControl runat="server" ID="HeadDistributorControl1" ProcductClass="default Producticon" />
    <!-- InstanceBeginEditable name="EditRegion3" -->
    <!--paopao star-->

    <script type="text/javascript">
        $(function() {
            $('#btnSave').bt({
                contentSelector: function() {
                    return "<span class='fontb-red'>请向海峡 刘兰 销售 电话 0571-87898687 进行询价。</span>";
                },
                positions: ['top', 'right', 'bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 10,
                spikeGirth: 15,
                width: 350,
                overlap: 0,
                centerPointY: 1,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '180%' }
            });
        });
    </script>

    <!--paopao end-->
    <div class="hr_10">
    </div>
    <div class="list-maincontent">
        <div class="listtablebox">
            <table width="100%" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td width="88" align="right" bgcolor="#DCEFF3">
                        <font class="fontbsize12">* </font>
                        <%=(String)GetGlobalResourceObject("string", "询价日期")%>：
                    </td>
                    <td>
                        <input name="txtxunjiatime" id="txtxunjiatime" type="text" valid="required" errmsg="询价日期不能为空"
                            class="searchInput size80" value="" onfocus="WdatePicker({minDate:'%y-%M-#{%d}'})"
                            runat="server" />
                    </td>
                    <td width="88" align="right" bgcolor="#DCEFF3">
                        <font class="fontbsize12">* </font>
                        <%=(String)GetGlobalResourceObject("string", "线路区域")%>：
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlArea" runat="server" CssClass="inputselect" valid="required"
                            errmsg="请选择线路区域!">
                        </asp:DropDownList>
                    </td>
                    <td width="88" align="right" bgcolor="#DCEFF3">
                        <font class="fontbsize12">* </font>
                        <%=(String)GetGlobalResourceObject("string", "线路名称")%>：
                    </td>
                    <td align="left">
                        <input type="text" class="searchInput size170" id="txtroutename" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#DCEFF3">
                        <font class="fontbsize12">* </font>
                        <%=(String)GetGlobalResourceObject("string", "天数")%>：
                    </td>
                    <td>
                        <input type="text" class="searchInput size40" id="txt_Days" runat="server" errmsg="请输入正确的天数!"
                            valid="required" />
                        <button class="addtimebtn" type="button" id="btnAddDays">
                            <%=(String)GetGlobalResourceObject("string", "增加日程")%></button>
                    </td>
                    <td align="right" bgcolor="#DCEFF3">
                        <font class="fontbsize12">* </font>
                        <%=(String)GetGlobalResourceObject("string", "国家地区")%>：
                    </td>
                    <td align="left">
                        <span class="kuang2">
                            <select id="ddlCountry" name="ddlCountry" class="inputselect">
                            </select>
                            <%--<select id="ddlProvice" class="inputselect" name="ddlProvice">
                            </select>--%>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#DCEFF3">
                        <font class="fontbsize12">* </font>
                        <%=(String)GetGlobalResourceObject("string", "抵达日期")%>：
                    </td>
                    <td>
                        <input type="text" class="formsize80 searchInput" id="txtLDate" runat="server" onfocus="WdatePicker()"
                            valid="required" errmsg="请输入抵达日期!" />
                    </td>
                    <td align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "抵达城市")%>：
                    </td>
                    <td>
                        <input name="txtDiDaChengShi" type="text" class="formsize80 searchInput" id="txtDiDaChengShi"
                            runat="server" />
                    </td>
                    <td align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "航班时间")%>：
                    </td>
                    <td>
                        <input type="text" class="formsize180 searchInput" id="txtDiDaHangBan" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#DCEFF3">
                        <font class="fontbsize12">* </font>
                        <%=(String)GetGlobalResourceObject("string", "离开日期")%>：
                    </td>
                    <td>
                        <input type="text" class="formsize80 searchInput" id="txtRDate" runat="server" onfocus="WdatePicker()"
                            valid="required" errmsg="请输入离开日期!" />
                    </td>
                    <td align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "离开城市")%>：
                    </td>
                    <td>
                        <input name="txtLiKaiChengShi" type="text" class="formsize80 searchInput" runat="server"
                            id="txtLiKaiChengShi" />
                    </td>
                    <td align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "航班时间")%>：
                    </td>
                    <td>
                        <input type="text" class="formsize180 searchInput" id="txtLiKaiHangBan" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#DCEFF3">
                        <font class="fontbsize12">* </font><span class="addtableT">
                            <%=(String)GetGlobalResourceObject("string", "人数")%>：</span>
                    </td>
                    <td>
                        <span class="kuang2">
                            <img src="/images/chengren.gif" width="16" height="15" style="vertical-align: middle" />
                            <%=(String)GetGlobalResourceObject("string", "成人")%>：
                            <input name="txtminadultcount" id="txtminadultcount" type="text" class="searchInput size40"
                                value="" errmsg="请输入最小成人数!" valid="required" runat="server" />
                            -
                            <input name="txtmaxadultcount" id="txtmaxadultcount" type="text" class="searchInput size40"
                                value="" errmsg="请输入最大成人数!" valid="isInt" runat="server" />
                        </span>
                    </td>
                    <td align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "酒店星级要求")%>：
                    </td>
                    <td colspan="3" align="left">
                        <span class="kuang2">
                            <asp:DropDownList runat="server" ID="ddlJiuDianXingJi" class="ddlJiuDianXingJi" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#DCEFF3">
                        用餐默认价格：
                    </td>
                    <td colspan="5">
                        早：<input type="text" class="Default_Price searchInput size40" data-id="txtDefault_Breakfast"
                            value="" />&nbsp; 中：<input type="text" class="Default_Price searchInput size40" data-id="txtDefault_Lunch"
                                value="" />&nbsp; 晚：<input type="text" class="Default_Price searchInput size40" data-id="txtDefault_Dinner"
                                    value="" />&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "行程亮点")%>:
                        <uc7:SelectJourneySpot ID="SelectJourneySpot1" runat="server" />
                    </td>
                    <td colspan="5">
                        <span id="spanPlanContent" style="display: inline-block;">
                            <asp:TextBox ID="txtPlanContent" runat="server" TextMode="MultiLine" CssClass="searchInput formsize800"></asp:TextBox>
                        </span>
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
            <uc2:Journey ID="Journey1" runat="server" />
            <div class="hr_10">
            </div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" id="TabList_Box">
                <tr>
                    <td colspan="7" align="left" style="height: 50px;">
                        <img src="../images/google-map.gif" onclick='$(".directions").toggle(function(){googlemap.init();})'
                            alt="行程区间线路图" />
                        <table class="directions" style="display: none">
                            <tr>
                                <th align="center">
                                    <%=(String)GetGlobalResourceObject("string", "路线")%>
                                </th>
                                <th align="center">
                                    <%=(String)GetGlobalResourceObject("string", "地图")%>
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
                    <td width="88" align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "购物")%>：
                    </td>
                    <td class="tdshop">
                        <%=shopStr %>
                    </td>
                </tr>
                <tr>
                    <td width="88" align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "风味餐")%>：
                    </td>
                    <td>
                        <uc3:selectFWeiCan ID="selectFWeiCan1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="88" align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "自费项目")%>：
                    </td>
                    <td>
                        <uc4:SelfPay ID="SelfPay1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="88" align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "赠送")%>：
                    </td>
                    <td>
                        <uc5:Give ID="Give1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="88" align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "小费")%>：
                    </td>
                    <td>
                        <uc6:Tip ID="Tip1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td width="88" height="14" align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "个性服务要求")%>：
                    </td>
                    <td>
                        <span id="spanSpecificRequire" style="display: inline-block;">
                            <asp:TextBox ID="txtSpecificRequire" runat="server" TextMode="MultiLine" Height="100px"
                                CssClass="searchInput formsize800"></asp:TextBox>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td width="88" align="right" bgcolor="#DCEFF3">
                        <%=(String)GetGlobalResourceObject("string", "报价备注")%>：<uc1:selectpriceremark runat="server"
                            id="SelectPriceRemark1" />
                    </td>
                    <td>
                        <span id="spanPriceRemark" style="display: inline-block;">
                            <asp:TextBox ID="txtPriceRemark" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                        </span>
                    </td>
                </tr>
            </table>
            <div class="hr_10">
            </div>
            <div class="mainbox cunline fixed">
                <ul id="ul_AddPrice_Btn">
                    <asp:PlaceHolder runat="server" ID="PHBtnSave">
                        <li class="cun-cy"><a href="javascript:;" data-id="btnSave" id="btnSave" data-name='<%=(String)GetGlobalResourceObject("string", "保存")%>'>
                            <%=(String)GetGlobalResourceObject("string", "保存")%></a></li>
                    </asp:PlaceHolder>
                    <li class="quxiao-cy"><a href="javascript:;" data-id="btncencer" id="btncencer" data-name='<%=(String)GetGlobalResourceObject("string", "取消")%>'>
                        <%=(String)GetGlobalResourceObject("string", "取消")%></a></li>
                </ul>
            </div>
        </div>
        <div class="hr_10">
        </div>
    </div>
    <input type="hidden" runat="server" id="hidQuoteId" value="" />
    <input type="hidden" runat="server" id="hidparentid" value="" />
    <input type="hidden" id="hidAreaId" />
    <input type="hidden" id="hidLngType" value="" />
    <input type="hidden" runat="server" id="hidpricetraffic" />
    <input type="hidden" runat="server" id="hidpricehotel1" />
    <input type="hidden" runat="server" id="hidpricehotel2" />
    <input type="hidden" runat="server" id="hidcancost" />
    <input type="hidden" runat="server" id="hidcanprice" />
    <input type="hidden" runat="server" id="hidsceniccost" />
    <input type="hidden" runat="server" id="hidscenicprice" />
    <input type="hidden" runat="server" id="hidpricezongfei" />
    <input type="hidden" runat="server" id="hidpriceother" />
    </form>
</body>
</html>

<script type="text/javascript" src="http://ditu.google.com/maps?file=api&hl=<%=this.LgType %>&amp;v=2&amp;key=AIzaSyBf0wag-At_3jV6NThPt5zTkx9jTB1q-Cs"></script>

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
        					alert("<%=GetGlobalResourceObject("string","路线规划查询条件设定有误") %>");
        					break;
        				case G_GEO_SERVER_ERROR:
        					alert("<%=GetGlobalResourceObject("string","服务器不能正确解析你输入的地址") %>");
        					break;
        				case G_GEO_MISSING_QUERY:
        				case G_GEO_MISSING_ADDRESS:
        					alert("<%=GetGlobalResourceObject("string","查询条件不能为空") %>");
        					break;
        				case G_GEO_UNKNOWN_ADDRESS:
        					alert("<%=GetGlobalResourceObject("string","查询地址未知") %>");
        					break;
        				case G_GEO_UNAVAILABLE_ADDRESS:
        					alert("<%=GetGlobalResourceObject("string","因当地法律或其他原因不能解析给出地址") %>");
        					break;
        				case G_GEO_UNKNOWN_DIRECTIONS:
        					alert("<%=GetGlobalResourceObject("string","给出的两地之间无路可走或我们的现有的数据中缺少路线规划路线") %>");
        					break;
        				case G_GEO_BAD_KEY:
        					alert("<%=GetGlobalResourceObject("string","导入类库是指定的密钥有误") %>");
        					break;
        				case G_GEO_TOO_MANY_QUERIES:
        					alert("<%=GetGlobalResourceObject("string","查询太过频繁") %>");
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
            id: '<%=Request.QueryString["id"] %>',
            Lgtype: '<%=Request.QueryString["LgType"] %>'
        },
        CreatePlanEdit: function() {

            //创建行程编辑器
            //items: keMore //功能模式(keMore:多功能,keSimple:简易,keSimple_HaveImage:简易附带上传图片)
            //langType:en（英文） zh_CN（简体中文）ar(泰文) zh_TW（繁体中文）
            var lantype = '<%=Request.QueryString["LgType"] %>';
            if (lantype == "1") { lantype = "zh_CN"; }
            else if (lantype == "2") { lantype = "en"; }
            else if (lantype == "3") { lantype = "ar"; }
            else lantype = "zh_CN";
            KEditer.init('<%=txtPlanContent.ClientID %>', { items: keSimple_HaveImage, langType: lantype });

            KEditer.init('<%=txtPriceRemark.ClientID %>', { items: keSimple, langType: lantype });
        },
        SetTrafficPrice: function() {//计算大交通价格
            var sumPrice = 0;
            $("#tbl_Journey_AutoAdd input[name='txttrafficprice']").each(function() {
                sumPrice += tableToolbar.getFloat($.trim($(this).val()));
            })
            $("#<%=hidpricetraffic.ClientID %>").val(sumPrice);
        },
        SetHotelPrice: function() {//计算酒店价格
            var sumPrice1 = 0; var sumPrice2 = 0;
            $("#tbl_Journey_AutoAdd input[name='txthotel1price']").each(function() {
                sumPrice1 += tableToolbar.getFloat($.trim($(this).val()));
            })
            $("#<%=hidpricehotel1.ClientID %>").val(sumPrice1/2);
            $("#tbl_Journey_AutoAdd input[name='txthotel2price']").each(function() {
                sumPrice2 += tableToolbar.getFloat($.trim($(this).val()));
            })
            $("#<%=hidpricehotel2.ClientID %>").val(sumPrice2/2);
        },
        SetSumPrice: function() {//计算总价
            $("#tbl_Journey_AutoAdd").delegate("input[name='txthotel1price']", "blur", function() {
                AddPrice.SetHotelPrice();
            })
            $("#tbl_Journey_AutoAdd").delegate("input[name='txthotel2price']", "blur", function() {
                AddPrice.SetHotelPrice();
            })
            $("#tbl_Journey_AutoAdd").delegate("input[name='txttrafficprice']", "blur", function() {
                AddPrice.SetTrafficPrice();
            })
            $("#tbl_Journey_AutoAdd").delegate("input[name='txtbreakprice']", "blur", function() {
                AddPrice.SumMenuPrice();
            })
            $("#tbl_Journey_AutoAdd").delegate("input[name='txtsecondprice']", "blur", function() {
                AddPrice.SumMenuPrice();
            })
            $("#tbl_Journey_AutoAdd").delegate("input[name='txtthirdprice']", "blur", function() {
                AddPrice.SumMenuPrice();
            })
            $("#TabList_Box").delegate("input[name='txtfprice']", "blur", function() {
                AddPrice.SumMenuPrice();
            })

        },
        SumPriceJingDian: function() {//计算景点价格合计
            var sumpricejs = 0; var sumpriceth = 0; var selfpricejs = 0.00; var selfpriceth = 0.00; var selfcost = 0.00;
            $("#tbl_Journey_AutoAdd").find("a[data-class='a_Journey_Since']").each(function() {
                sumpricejs += tableToolbar.getFloat($(this).attr("data-pricejs"));
                sumpriceth += tableToolbar.getFloat($(this).attr("data-priceth"));
            })

            $("#Tab_SelfPay").find("input[name='hidselfpricejs']").each(function() {
                selfpricejs += tableToolbar.getFloat($(this).val());
            })
            $("#Tab_SelfPay").find("input[name='txt_SelfPayPrice']").each(function() {
                selfpriceth += tableToolbar.getFloat($(this).val());
            })
            //
            $("#Tab_SelfPay").find("input[name='txt_SelfPayCost']").each(function() {
                selfcost += tableToolbar.getFloat($(this).val());
            })


            $("#<%=hidsceniccost.ClientID %>").val(tableToolbar.getFloat(sumpricejs + selfpricejs - selfcost));
            $("#<%=hidscenicprice.ClientID %>").val(tableToolbar.getFloat(sumpriceth + selfpriceth - selfcost));
        },
        SumTipPrice: function() {//其他费用合计
            var tipprice = 0.00;
            $("#table_Tip").find("input[name='txt_Quote_SumPrice']").each(function() {
                tipprice += tableToolbar.getFloat($(this).val());
            })
            $("#<%=hidpriceother.ClientID %>").val(tableToolbar.getFloat(tipprice));
        },
        SumZongFeiPrice: function() {//计算综费
            var ZongFeiPrice = 0.00;
            $("#Tab_Give").find("input[name='txt_WuPinPrice']").each(function() {
                ZongFeiPrice += tableToolbar.getFloat($(this).val());
            })
            $("#<%=hidpricezongfei.ClientID %>").val(tableToolbar.getFloat(ZongFeiPrice));
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
            $("#<%=hidcancost.ClientID %>").val(tableToolbar.calculate(sumPricejs, sumPricefjs, "+"));
            $("#<%=hidcanprice.ClientID %>").val(tableToolbar.getFloat(sumPrice));

        },
        //按钮绑定事件
        BindBtn: function() {
            $("#ul_AddPrice_Btn").find("a").css("background-position", "0 0");
            $("#ul_AddPrice_Btn").find("a").each(function() {
                $(this).html($(this).attr("data-name"));
            })
            $("#ul_AddPrice_Btn").find("a").unbind("click").click(function() {
                AddPrice.SetCityAndTraffic();
                var _s = $(this);
                var id = _s.attr("data-id");
                var isExistBuyId = $("#hidisExistBuyId").val();
                switch (id) {
                    //保存                                                                          
                    case "btnSave":
                        _s.html("正在提交..");
                        AddPrice.Save();
                        break;
                    case "btncencer":
                        location.href = '/XunJia.aspx?sl=0&LgType=<%=Request.QueryString["LgType"] %>';
                        break;
                }
                return false;
            })
        },
        Save: function() {
            AddPrice.UnBindBtn();
            //同步编辑器数据到文本框
            KEditer.sync();
            $.newAjax({
                type: "post",
                cache: false,
                url: "/XunJiaEdit.aspx?dotype=save&" + $.param(AddPrice.Data),
                data: $("#ul_AddPrice_Btn").closest("form").serialize(),
                dataType: "json",
                success: function(ret) {
                    //ajax回发提示
                    if (ret.result == "1") {
                        tableToolbar._showMsg(ret.msg, function() {
                            var lgtype = '<%=Request.QueryString["LgType"] %>';
                            window.location.href = "/XunJia.aspx?type=" + AddPrice.Data.type + "&sl=" + AddPrice.Data.sl + "&LgType=" + lgtype;
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
            pcToobar.init({
                gID: "#ddlCountry",
//                pID: "#ddlProvice",
                comID: '<%=this.SiteUserInfo.CompanyId %>',
                gSelect: '<%=CountryId %>',
                pSelect: '<%=Province %>',
                lng: '<%=Request.QueryString["LgType"] %>'
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
        //创建编辑器
        AddPrice.CreatePlanEdit();
        AddPrice.SetSumPrice();
        FV_onBlur.initValid($("#ul_AddPrice_Btn").closest("form").get(0));
        AddPrice.BindBtn();
        AddPrice.PageInit();
        $("#hidAreaId").val($("#<%=this.ddlArea.ClientID %>").val());
        $("#<%=this.ddlArea.ClientID %>").change(function() {
            $("#hidAreaId").val($(this).val());
        });
        $("#tbl_Journey_AutoAdd").autoAdd({ changeInput: $("#<%=txt_Days.ClientID %>"), addCallBack: Journey.AddRowCallBack, upCallBack: Journey.MoveRowCallBack, downCallBack: Journey.MoveRowCallBack, delCallBack: Journey.DelRowCallBack, delStartCall: Journey.StartFun });



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


        var quoteid = $(".tablehead1").find("li").eq(0).find("a").attr("data-id");
        if (quoteid) {
            $("#<%=hidQuoteId.ClientID %>").val(quoteid);
        }
        var lytype = '<%=Request.QueryString["LgType"] %>';
        $("#hidLngType").val(lytype);
    });
	  
</script>


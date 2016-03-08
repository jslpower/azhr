<%@ Page Title="特价产品" Language="C#" AutoEventWireup="true" CodeBehind="TJChanPin.aspx.cs"
    Inherits="EyouSoft.Web.Sales.TJChanPin" MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox border-bot fixed">
            <form action="/Sales/TJChanPin.aspx" method="get">
            <input type="hidden" name="sl" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>" />
            <span class="searchT">
                <p>
                    客户单位: <uc1:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" BoxyTitle="选择客户单位"
                        SelectFrist="false" />
                    团号：
                    <input type="text" class="inputtext formsize100" name="txtTourCode" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtTourCode") %>" />
                    团队名称：
                    <input type="text" class="inputtext formsize120" name="txtRouteName" size="28" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtRouteName") %>" />
                    出团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtBeginDateF"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginDateF") %>">
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtBeginDateS"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtBeginDateS") %>">
                    回团时间：
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtEndDateF"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndDateF") %>">
                    -
                    <input type="text" onfocus="WdatePicker()" class="inputtext formsize80" name="txtEndDateS"
                        value="<%=EyouSoft.Common.Utils.GetQueryStringValue("txtEndDateS") %>">
                    <br>
                    业务员：
                    <uc1:SellsSelect runat="server" ID="SellsSelect1" runat="server" SetTitle="业务员" SelectFrist="false" />
                    团队状态:<select name="sltTourState" class="inputselect" style="width: 120px;">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourStatus)), EyouSoft.Common.Utils.GetQueryStringValue("sltTourState")) %>
                    </select>
                    操作状态:<select name="selState" class="inputselect" style="width: 80px;">
                        <option value="">-请选择-</option>
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.TourStructure.TourSureStatus)), EyouSoft.Common.Utils.GetQueryStringValue("selState"))%>
                    </select>
                   <button class="search-btn" type="submit">
                        搜索</button></p>
            </span>
            </form>
        </div>
         <div class="tablehead">
            <ul class="fixed" id="btnAction">
                <asp:PlaceHolder ID="phForAdd" runat="server">
                    <li><s class="addicon"></s><a class="toolbar_add" hidefocus="true" href="TJChanPinEdit.aspx?sl=<%=SL %>&act=add">
                        <span>新增</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForUpdate" runat="server">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0);">
                        <span>修改</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <%--<asp:PlaceHolder ID="phForCopy" runat="server">
                    <li><s class="copyicon"></s><a class="toolbar_copy" hidefocus="true" href="javascript:void(0);">
                        <span>复制</span></a></li><li class="line"></li>
                </asp:PlaceHolder>--%>
                <asp:PlaceHolder ID="phForCanel" runat="server">
                    <li><s class="cancelicon"></s><a class="toolbar_cancel" hidefocus="true" href="javascript:void(0);">
                        <span>取消</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phForDelete" runat="server">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0);"><s class="delicon">
                    </s><span>删除</span></a></li></asp:PlaceHolder>
                <asp:PlaceHolder ID="phForOper" runat="server">
                    <li><s class="ptjdicon"></s><a class="toolbar_paiduan" hidefocus="true" href="javascript:void(0);">
                        <span>派团给计调</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="phQueRen" runat="server">
                    <li><s class="querenicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_queren">
                        <span>团队确认</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phImg" Visible="false">
                    <li><img src="/Images/渐变(1).png"/></li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input name="checkbox" id="checkbox1" type="checkbox">
                        </th>
                        <th class="th-line">
                            团号
                        </th>
                        <th class="th-line">
                            团队名称
                        </th>
                        <th class="th-line">
                            天数
                        </th>
                        <th class="th-line">
                            抵达日期
                        </th>
                        <th class="th-line">
                            价格
                        </th>
                        <th class="th-line">
                            人数
                        </th>
                        <th class="th-line">
                            客户单位
                        </th>
                        <th class="th-line">
                            业务员
                        </th>
                        <%if(this.SL==((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.团队中心_团队产品).ToString()) %>
                        <%{ %>
                        <th class="th-line">
                            查看计调
                        </th>
                        <%} %>
                        <%else %>
                        <%{ %>
                        <th class="th-line">
                            应收金额
                        </th>
                        <th class="th-line">
                            已收金额
                        </th>
                        <th class="th-line">
                            未收金额
                        </th>
                        <%} %>
                        <th class="th-line">
                            状态
                        </th>
                    </tr>
                    <asp:Repeater runat="server" ID="rpt_List">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input name="checkbox" id="checkbox" value="<%#Eval("tourId") %>" data-surestate='<%#Eval("TourSureStatus").ToString() %>' type="checkbox">
                                </td>
                                <td align="center">
                                    <a href='/PrintPage/XingChengDan.aspx?TourId=<%#Eval("TourId")%>&type=<%=(int)EyouSoft.Model.EnumType.TourStructure.TourType.自由行 %>' target="_blank">
                                        <%#Eval("TourCode")%></a>
                                    <%#GetChangeInfo((bool)Eval("IsChange"), (bool)Eval("IsSure"), Eval("tourId").ToString(), Eval("TourStatus").ToString())%>
                                </td>
                                <td align="center">
                                    <%#Eval("RouteName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("TourDays") %>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),this.ProviderToDate )%>
                                </td>
                                <td align="right">
                                    <b class="a_price_info fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), this.ProviderToMoney)%>
                                    </b><span style='display: none;'><b>成人价：</b><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("AdultPrice"), this.ProviderToMoney)%></br>
                                        <b>儿童价：</b><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("ChildPrice"), this.ProviderToMoney)%></br>
                                        <b>领队价：</b><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("LeaderPrice"), this.ProviderToMoney)%></br>
                                        <b>单房差：</b><%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("SingleRoomPrice"), this.ProviderToMoney)%>
                                    </span>
                                </td>
                                <td align="center">
                                    <b><a href='<%#GetPrintUrl(Eval("OrderId"))%>' target="_blank">
                                        <%#Eval("Adults") %></a><sup class="fontred">+<%#Eval("Childs") %></sup></b>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0);" data-class='paopao'>
                                        <%#Eval("BuyCompanyName")%></a> <span style="display: none"><b>
                                            <%#Eval("CrmLinkman") == null ? "" : ((EyouSoft.Model.CrmStructure.MCrmLinkman)Eval("CrmLinkman")).Name%></b><br />
                                            Tel：<%#Eval("CrmLinkman") == null ? "" : ((EyouSoft.Model.CrmStructure.MCrmLinkman)Eval("CrmLinkman")).Telephone%><br />
                                            Mob：<%#Eval("CrmLinkman") == null ? "" : ((EyouSoft.Model.CrmStructure.MCrmLinkman)Eval("CrmLinkman")).MobilePhone%></span>
                                    </span>
                                </td>
                                <td align="center">
                                    <%# Eval("SellerName")%>
                                    <input type="hidden" name="ItemUserID" value="<%# Eval("SellerId")%>" />
                                </td>
                                <% if (this.SL == ((int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.团队中心_团队产品).ToString())%>
                                   <%{%>
                                <td align="center" data-class="GetJiDiaoIcon" data-tourid="<%#Eval("tourId") %>">
                                    <a href='javascript:;' class="showJiDiao">
                                        <%#EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.HTourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%></a>
                                </td>
                                  <% } %>
                                  <% else %>
                                  <%{%>
                                <td align="right">
                                  <b class="fontblue"><%#Eval("ConfirmMoney", "{0:C2}")%></b>
                                </td>
                                <td align="right">
                                  <b class="fontgreen"><%#Eval("CheckMoney","{0:C2}") %></b>
                                </td>
                                <td align="right">
                                  <b class="fontred"><%#Eval("WeiShouJinE","{0:C2}") %></b>
                                </td>
                                  <% } %>
                                <td align="center">
                                    <input type="hidden" name="hideTourStatus" value="<%#(int)Eval("TourStatus")%>" />
                                    <%#Eval("TourStatus").ToString() == "已取消" ? "<a data-class='cancelReason'><span class='fontgray' data-class='QuoteState' data-state='0'>已取消</span></a><div style='display: none'><b>取消原因</b>:" +EyouSoft.Common.Function.StringValidate.TextToHtml( Eval("CancelReson").ToString()) + "</div>" : Eval("TourStatus").ToString()%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litMsg" runat="server" Text="<tr><td align='center' colspan='14'>暂无计划!</td></tr>"></asp:Literal>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0 none;" class="tablehead">
            <ul class="fixed">

                <script type="text/javascript">
                    document.write(document.getElementById("btnAction").innerHTML);
                </script>

            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
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
                    href="javascript:void(0);" onclick="javascript:TeamplanList.CanelBox.hide();return false;"><s
                        class="chongzhi"></s>关闭</a>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        var TeamplanList = {
            Data: { sl: '<%=Request.QueryString["sl"] %>'
            },
            CanelBox: null,
            DelAll: function(objArr) {
                var list = new Array();
                //遍历按钮返回数组对象
                for (var i = 0; i < objArr.length; i++) {
                    //从数组对象中找到数据所在，并保存到数组对象中
                    if (objArr[i].find("input[type='checkbox']").val() != "on") {
                        list.push(objArr[i].find("input[type='checkbox']").val());
                    }
                }
                //执行
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/Sales/TJChanPin.aspx?dotype=delete&ids=" + list.join(',') + "&" + $.param(TeamplanList.Data),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg("删除成功,正在刷新页面..", function() {
                                window.location.href = window.location.href;
                            });
                        } else {
                            tableToolbar._showMsg("删除失败！");
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });

            },
            RightClick: function(type, id) {
                switch (type) {
                    case "update":
                        window.location.href = "/Sales/ChanPinEdit.aspx?act=update&id=" + id + "&type=free&" + $.param(TeamplanList.Data);
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
                            iframeUrl: "/Sales/PaiTuan.aspx?id=" + id + "&" + $.param(TeamplanList.Data),
                            title: "派团给计调",
                            modal: true,
                            width: "560px",
                            height: "252px"
                        });
                        $("div.bt-wrapper").hide();
                        break;
                    case "queren":
                         if (tableToolbar.IsHandleElse == "false") {
                            var msgList=[],tr=$("#liststyle").find("input[type='checkbox'][value='"+id+"']").closest("tr");
                            if(tr.find("input[name='ItemUserID']").val()!=tableToolbar.UserID){
                                msgList.push("你不是该计划的销售员,无法确认!");
                            }
                            if (msgList.length > 0) {
                                tableToolbar._showMsg(msgList.join("<br />"));
                                return false;
                            }
                         }
                        Boxy.iframeDialog({
                            iframeUrl: "/Sales/QueRen.aspx?tourId=" + id + "&" + $.param(TeamplanList.Data),
                            title: "团队确认",
                            modal: true,
                            width: "600px",
                            height: "300px"
                        });
                        $("div.bt-wrapper").hide();
                        break;
                    case "copy":
                        window.location.href = "/Sales/ChanPinEdit.aspx?act=copy&id=" + id + "&type=free&" + $.param(TeamplanList.Data);
                        break;
                }
                return false;
            },
            GetFristHtml: function(tr) {
                var html = [], id = tr.find("td:eq(0)").find("input[type='checkbox']").val();

                if (this.Power[0]) {
                    html.push("<a onclick=TeamplanList.RightClick('update','" + id + "') href='javascript:void(0);' hidefocus='true' class='toolbar_update'><s class='updateicon'></s>修改</a>");
                }

                if ((tr.find("input[name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 %>"||tr.find("input[name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 %>") && this.Power[1]) {
                    html.push("<a onclick=TeamplanList.RightClick('paituan','" + id + "') href='javascript:void(0);' hidefocus='true' class='toolbar_paiduan'><s class='ptjdicon'></s>派团给计调</a>");
                }
                
                if (this.Power[2]) {
                    html.push("<a onclick=TeamplanList.RightClick('queren','" + id + "') href='javascript:void(0);' hidefocus='true' class='toolbar_shenqing'><s class='shoukuan'></s>团队确认</a>");
                }
                else{
                    this.Power[2]=false;
                }

            	if (this.Power[3]) {
                    html.push("<a onclick=TeamplanList.RightClick('copy','" + id + "') href='javascript:void(0);' hidefocus='true' class='toolbar_copy'><s class='copyicon'></s>复制</a>");
                }

                return html.join('');
            },
            Power:[<%=ListPower %>]

        }

        $(function() {
            $(".showJiDiao").click(function(){
                var url="/Plan/PlanLobal.aspx?";
                var tourid=$(this).closest("tr").find("input[type='checkbox']").val();
                var sl='<%=Request.QueryString["sl"] %>';
                location.href=url+"tourId="+tourid+"&sl="+sl;
                return false;
            })
        	tableToolbar.init({
        			tableContainerSelector: "#liststyle", //表格选择器
        			objectName: "计划",
        			copyCallBack: function(arr) {
        				location.href = "ChanPinEdit.aspx?act=copy&id=" + arr[0].find(":checkbox").val() + "&type=free&" + $.param(TeamplanList.Data);
        				return false;
        			},
        			updateCallBack: function(arr) {
        				location.href = "ChanPinEdit.aspx?act=update&id=" + arr[0].find(":checkbox").val() + "&type=free&" + $.param(TeamplanList.Data);
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
        				TeamplanList.DelAll(arr);
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
        						cancel = arr[i].find("input[name='hideTourStatus']").val();
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
        				TeamplanList.CanelBox = new Boxy($("#div_Canel"), { modal: true, fixed: false, title: "取消", width: "580px", height: "210px" });
        			},
        			otherButtons: [{
        				button_selector: '.toolbar_paiduan',
        				sucessRulr: 1,
        				msg: '未选中任何 计划 ',
        				msg2: '只能选中一个 计划 ',
        				buttonCallBack: function(arr) {
        					if (tableToolbar.IsHandleElse == "false") {
        						var msgList = new Array();
        						if (arr[0].find("input[name='ItemUserID']").val() != tableToolbar.UserID) {
        							msgList.push("你不是该计划的销售员,无法派团计调!");
        						}
        						if (msgList.length > 0) {
        							tableToolbar._showMsg(msgList.join(''));
        							return false;
        						}
        					}
                            if($.trim(arr[0].find(":checkbox").attr("data-surestate"))!='<%=EyouSoft.Model.EnumType.TourStructure.TourSureStatus.已确认 %>'){
             tableToolbar._showMsg("不能对未确认的团队进行派团!");
             return false;
         }
        					if (arr[0].find("input[type='hidden'][name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 %>"
	        					|| arr[0].find("input[type='hidden'][name='hideTourStatus']").val() == "<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调未接收 %>") {
        						Boxy.iframeDialog({
        								iframeUrl: "PaiTuan.aspx?id=" + arr[0].find(":checkbox").val() + "&" + $.param(TeamplanList.Data),
        								title: "派团给计调",
        								modal: true,
        								width: "560px",
        								height: "260px"
        							});
        						return false;
        					} else {
        						tableToolbar._showMsg("该团不能进行派团操作!");
        					}
        				}
        			}, {
        				button_selector: '.toolbar_queren',
        				sucessRulr: 1,
        				msg: '未选中任何 计划 ',
        				msg2: '只能选中一个 计划 ',
        				buttonCallBack: function(arr) {
        					if (tableToolbar.IsHandleElse == "false") {
        						var msgList = new Array();
        						if (arr[0].find("input[name='ItemUserID']").val() != tableToolbar.UserID) {
        							msgList.push("你不是该计划的销售员,无法确认!");
        						}
        						if (msgList.length > 0) {
        							tableToolbar._showMsg(msgList.join(''));
        							return false;
        						}
        					}

        					Boxy.iframeDialog({
        							iframeUrl: "QueRen.aspx?tourid=" + arr[0].find(":checkbox").val() + "&" + $.param(TeamplanList.Data),
        							title: "团队确认",
        							modal: true,
        							width: "600px",
        							height: "300px"
        						});
        					return false;
        				}
        			}]
        		});

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
        				url: "/Sales/TJChanPin.aspx?doType=canel&ids=" + ids.join(',') + "&remarks=" + encodeURIComponent(remarks) + "&" + $.param(TeamplanList.Data),
        				dataType: "json",
        				success: function(r) {
        					if (r.result == "1") {
        						TeamplanList.CanelBox.hide();
        						tableToolbar._showMsg("取消成功,正在刷新页面!", function() {
        							window.location.href = window.location.href;
        						});
        					} else {
        						tableToolbar._showMsg("设置取消失败!");
        					}
        				}
        			})
        		return false;
        	})

        	//计调项泡泡
        	BtFun.InitBindBt("GetJiDiaoIcon");

        	$("#liststyle").find("a[data-class='paopao']").bt({
        			contentSelector: function() {
        				return $(this).next().html();
        			},
        			positions: ['left'],
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

        	$('td[data-class="right"]').bt({
        			contentSelector: function() {
        				var DivStr = "";
        				var powers = TeamplanList.Power;
        				if (!(powers[0] == false && powers[1] == false && this.Power[2] == false && powers[3] == false)) {
        					DivStr = "<div class='td_tablehead'>" + TeamplanList.GetFristHtml($(this).parent()) + "</div>";
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

        	//价格
        	$(".a_price_info").bt({
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

        });

    </script>

</asp:Content>

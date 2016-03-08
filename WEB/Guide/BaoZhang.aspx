<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoZhang.aspx.cs" Inherits="EyouSoft.Web.Guide.BaoZhang" MasterPageFile="~/MasterPage/Front.Master"%>
<%@ Import Namespace="EyouSoft.Model.EnumType.PrivsStructure" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register TagName="sellers" TagPrefix="uc1" Src="~/UserControl/SellsSelect.ascx" %>

<asp:Content ContentPlaceHolderID="head" ID="Content1" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="Content2" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <div class="mainbox">
        <form id="formSearch" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    <input type="hidden" name="sl" id="sl" value="<%=Request.QueryString["sl"]%>" />
                    团号：<input type="text" class="inputtext" size="formsize80" name="txtTourCode" value="<%=Request.QueryString["txtTourCode"] %>" />
                    线路名称：<input type="text" class="inputtext" size="formsize80" name="txtRouteName" value="<%=Request.QueryString["txtRouteName"] %>" />
                    出团日期：
                    <input type="text" class="inputtext" style="width: 60px;" name="txtStarTime" onfocus="WdatePicker();"
                        value="<%=Request.QueryString["txtStarTime"] %>" />
                    至
                    <input type="text" class="inputtext" style="width: 60px;" name="txtStarEnd" onfocus="WdatePicker();"
                        value="<%=Request.QueryString["txtStarEnd"] %>" />
                    <% if (this._sl==Menu2.财务管理_报销报账)%>
                    <% {%>
                            计调：<uc1:sellers ID="planer" runat="server" />
                    <% }%>
                    <% else%>
                    <% {%>
                            团队状态：<%=TourStatusHtml.ToString()%>
                    <% } %>
                    导游：
                    <uc1:sellers ID="guid1" runat="server" />
                    销售员：<uc1:sellers ID="sellers1" runat="server" CompanyID="<%=this.SiteUserInfo.CompanyId %>" SelectFrist="false" />
                    <button type="submit" class="search-btn" id="submit">
                        搜索</button></p>
            </span>
        </div>
        <input type="hidden" name="isDealt" id="isDealt" value="<%=Request.QueryString["isDealt"]??"-1" %>" />
        </form>
        <div class="tablehead" id="tablehead">
            <asp:PlaceHolder runat="server" ID="phHead" Visible="false">
            <ul class="fixed">
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="-1"
                    href="javascript:void(0);"><span>未报账团队</span></a></li><li class="line"></li>
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="1"
                    href="javascript:void(0);"><span>已报账团队</span></a></li><li class="line"></li>
                <asp:PlaceHolder runat="server" ID="phbx" Visible="false">
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="-2"
                    href="javascript:void(0);"><span>未报销团队</span></a></li><li class="line"></li>
                <li><s class="xiaoshou-bz"></s><a data-class="a_isDealt" hidefocus="true" data-value="2"
                    href="javascript:void(0);"><span>已报销团队</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
            </ul>
            </asp:PlaceHolder>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" id="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        出团日期
                    </th>
                    <th align="center" class="th-line">
                        人数
                    </th>
                    <th align="center" class="th-line">
                        导游
                    </th>
                    <th align="center" class="th-line">
                        计调
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <% if (this._sl==Menu2.财务管理_报销报账)%>
                    <% {%>
                    <th align="center" class="th-line">
                        收入
                    </th>
                    <th align="center" class="th-line">
                        支出
                    </th>
                    <th align="center" class="th-line">
                        利润
                    </th>
                    <% }%>
                    <% else%>
                    <% {%>
                    <th align="center" class="th-line">
                        状态
                    </th>
                    <% } %>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="replist" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%# Eval("TourId") %>" />
                            </td>
                            <td align="center">
                                <%# Eval("TourCode")%>
                            </td>
                            <td align="left">
                                <a href='<%=PrintUrl %>?tourId=<%# Eval("TourId") %>' target="_blank">
                                    <%#Eval("RouteName")%></a>
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("LDate"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <b class="fontblue">
                                    <%# Eval("Adults")%></b><sup class="fontred">+<%# Eval("Childs")%></sup>
                            </td>
                            <td align="center">
                                <%#Eval("Guides")%>
                            </td>
                            <td align="center">
                                <%#Eval("Planers")%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                    <% if (this._sl==Menu2.财务管理_报销报账)%>
                    <% {%>
                            <td align="right">
                                <%#Eval("TourSettlement","{0:C2}")%>
                            </td>
                            <td align="right">
                                <%#Eval("TourPay", "{0:C2}")%>
                            </td>
                            <td align="right">
                                <%#Eval("Profit", "{0:C2}")%>
                            </td>
                    <% }%>
                    <% else%>
                    <% {%>
                            <td align="center">
                                <%#Eval("TourStatus").ToString()%>
                            </td>
                    <% } %>
                            <td align="center" data-tourid="<%#Eval("TourId") %>">
                                <%=this._caozuo %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" Visible="false" runat="server">
                    <tr align="center">
                        <td colspan="15">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
       <div id="tablehead_clone">
        </div>
    </div>

  <script type="text/javascript">
        var PageJsDataObj = {
        	BaoXiao: "/Guide/BaoZhangCaoZuo.aspx?", /*报销页面URL*/
        	BaoZhang: "/Fin/HeSuanJieShu.aspx?", /*审批页面URL*/
        	BindBtn: function() { /*绑定按钮*/
        		var that = this;
        		tableToolbar.init({
        				tableContainerSelector: "#liststyle", //表格选择器
        				objectName: "记录"
        			});
        		//导游报账、计调报账操作列点击事件        		
        		//报销报账操作列报销点击事件
        		$("#liststyle a[data-class='a_ExamineA']").click(function() {
        			window.location.href = that.BaoXiao + $.param({
        					tourId: $(this).closest("td").attr("data-tourid"),
        					sl: '<%=SL %>'
        				});
        			return false;
        		});

        		//报销报账操作列审批点击事件
        		$("#liststyle a[data-class='a_Apply']").click(function() {
        			window.location.href = that.BaoZhang + $.param({
        					tourId: $(this).closest("td").attr("data-tourid"),
        					sl: '<%=SL %>'
        				});
        			return false;
        		});

        		//列表查询
        		$("a[data-class='a_isDealt']").click(function() {
        			$("#isDealt").val($(this).attr("data-value"));
        			$("#submit").click();
        		});
        		var isdefaul = '<%=Request.QueryString["isDealt"]??"-1" %>';
        		$("a[data-class='a_isDealt']").each(function() {
        			if ($(this).attr("data-value") == isdefaul) {
        				$(this).addClass("ztorderform");
        			}
        			else {
        				$(this).removeClass("ztorderform");
        			}
        		});
        	},
        	PageInit: function() {
        		//绑定功能按钮
        		this.BindBtn();
        		$("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
        	}

        };
        
        //页面初始化
        $(function() {
        	PageJsDataObj.PageInit();
        });
        
    </script>

</asp:Content>

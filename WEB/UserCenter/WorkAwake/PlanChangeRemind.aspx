<%@ Page Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="PlanChangeRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.PlanChangeRemind" %>
<%@ Import Namespace="EyouSoft.Model.EnumType.TourStructure" %>

<%@ Register Src="../../UserControl/UserCenterNavi.ascx" TagName="UserCenterNavi"
    TagPrefix="uc1" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="grzxtabelbox">
        <div class="list_btn basicbg_01">
            <uc1:UserCenterNavi ID="UserCenterNavi1" runat="server" />
        </div>
        <div class="tablehead">
<%--            <ul class="fixed">
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                    href="PlanChangeRemind.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                    <span>计划变更</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="ChangeRemind.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                    <span>订单变更</span></a></li>
            </ul>
--%>            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th width="30" class="th-line">
                        序号
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="left" class="th-line">
                        线路名称
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        OP
                    </th>
                    <th align="center" class="th-line">
                        导游
                    </th>
                    <th align="center" class="th-line">
                        变更时间
                    </th>
                    <th align="center" class="th-line">
                        变更人
                    </th>
                    <th align="left" class="th-line">
                        变更标题
                    </th>
                    <th align="center" class="th-line">
                        状态
                    </th>
                </tr>
                <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                    <ItemTemplate>
                        <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>">
                            <td align="center"> 
                                <input type="hidden" name="ItemUserID" />
                                <%# Container.ItemIndex+1%>
                            </td>
                            <td align="center">
                                <a href="javascript:void(0)" data-class="PlanChange">
                                    <%# Eval("TourCode")%></a><span style="display: none;"><%# GetOperaterInfo(Eval("TourId").ToString())%></span><%#GetTourPlanIschange(Convert.ToBoolean(Eval("State")),Convert.ToString(Eval("TourId")))%>
                            </td>
                            <td align="left">
                                <%# Eval("RouteName")%>
                            </td>
                            <td align="center">
                                <%# Eval("SellerName")%>
                            </td>
                            <td align="center">
                                <asp:Repeater ID="rptTourPlaner" runat="server">
                                    <ItemTemplate>
                                        <%# Eval("Planer")%>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <td align="center">
                                <asp:Repeater ID="rptTourGuide" runat="server">
                                    <ItemTemplate>
                                        <%# Eval("Name")%>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                            <td align="center">
                                <%# Eval("IssueTime")%>
                            </td>
                            <td align="center">
                                <%# Eval("Operator")%>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0)" id="link1" onclick="clickboxy(this)">
                                    <%# Eval("Title")%></a>
                                <div style="display: none">
                                    <div class="alertbox-outbox" style="width: 550px;" dataclass="Plan_ChangeContent">
                                        <div style="height: 30px; line-height: 30px; width: 98%; margin: auto; font-size: 14px;">
                                        </div>
                                        <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                                            style="margin: 0 auto">
                                            <tr>
                                                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                                                    变更标题：
                                                </td>
                                                <td colspan="3" align="left">
                                                    <%#Eval("Title")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                                                    变更人：
                                                </td>
                                                <td width="35%" align="left" bgcolor="#e0e9ef">
                                                    <%#Eval("Operator")%>
                                                </td>
                                                <td width="15%" height="28" align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                                                    变更时间：
                                                </td>
                                                <td width="35%" height="28" bgcolor="#e0e9ef">
                                                    <%#Eval("IssueTime")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="15%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                                                    变更内容：
                                                </td>
                                                <td colspan="3" style="padding: 6px 4px;">
                                                    <%#Eval("Content")%>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="height: 30px; line-height: 30px; width: 98%; margin: auto; font-size: 14px;">
                                        </div>
                                        <div class="alertbox-btn" style="position: static; width: 99.5%; bottom:0px">
                                            <a href="javascript:void(0)" onclick="SureChange(this)" hidefocus="true" style='<%#(ChangeType)this.Eval("changetype") == ChangeType.导游变更&&(ChangeStatus)this.Eval("state")==ChangeStatus.销售未确认?"visibility:visible":"visibility:hidden"%>'
                                                hidvalueid="<%# Eval("Id")%>" hidtourid="<%#Eval("TourId") %>" hidchangetype="<%#(int)(ChangeType)Eval("changetype") %>" hidchangestatus="<%=(int)ChangeStatus.销售暂不处理 %>">暂不处理</a>
                                            <a href="javascript:void(0)" onclick="SureChange(this)" hidefocus="true"
                                                hidvalueid="<%# Eval("Id")%>" hidtourid="<%#Eval("TourId") %>" hidchangetype="<%#(int)(ChangeType)Eval("changetype") %>"  hidchangestatus="<%#(ChangeType)this.Eval("changetype") == ChangeType.导游变更&&(ChangeStatus)this.Eval("state")==ChangeStatus.销售未确认?(int)ChangeStatus.计调已确认:(int)ChangeStatus.计调未确认%>">确认变更</a>                                        
                                            <a href="javascript:void(0)" hidefocus="true"
                                                    class="close"><s class="chongzhi"></s>关闭</a>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td align="center">
                                <%#"<b class=\"fontred\">"+Eval("State")+"</b>" %>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <!--列表结束-->
        <div style="width: 100%; text-align: center; background-color: #ffffff">
            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        </div>
        <div style="border: 0 none;" class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect2" runat="server" />
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        var planBoxy={
            pboxy:null,
            ClickBoxy:function(a){
                var $this=$(a);
                planBoxy.pboxy=new Boxy($this.next().html(), { modal: true, title: "计划变更", width: "550px", height: "330px" })
            }        
        }
        function clickboxy(a)
        {
            planBoxy.ClickBoxy(a);
            return false;
        }
        
        $(function() {
            tableToolbar.init({});
        	$("#liststyle a[data-class='PlanChange']").bt({
                    contentSelector: function() {
                        return $(this).next("span").html();
                    },
                    positions: ['left', 'right','bottom'],
                    fill: '#FFF2B5',
                    strokeStyle:'#D59228',
			        noShadowOpts:{strokeStyle:"#D59228" },
                    spikeLength: 10,
                    spikeGirth: 15,
                    width: 200,
                    overlap: 0,
                    centerPointY: 1,
                    cornerRadius: 4,
                    shadow: true,
                    shadowColor: 'rgba(0,0,0,.5)',
			        cssStyles:{color:'#00387E','line-height':'200%'}
            });
        })
        
        
        function SureChange(obj)
        {
            var $this=$(obj);
            var id=$this.attr("hidvalueid");
            var tourid=$this.attr("hidtourid");
        	var changetype=$this.attr("hidchangetype");
        	var changestatus=$this.attr("hidchangestatus");
            $.newAjax({
                type: "post",
                cache: false,
                dataType:"json",
                url: "PlanChangeRemind.aspx?sl=" + <%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %> + "&doType=IsSureChange&id=" + id+"&tourid=" + tourid+"&changetype=" + changetype+"&changestatus=" + changestatus,
                success: function(ret) {
                	tableToolbar._showMsg(ret.msg, function() {
                		if (ret.result) {
                			window.location.reload();
                		}
                	});
                },
                error: function() {
                    if(arguments[1]!=null)
                        tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                    else
                        tableToolbar._showMsg("服务器忙");
                }
            });
        }
        
    </script>

</asp:Content>

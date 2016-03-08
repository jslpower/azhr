<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="OperaterRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.OperaterRemind" %>

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
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr>
                        <th align="center" class="th-line">
                            序号
                        </th>
                        <th align="center" class="th-line">
                            团号
                        </th>
                        <th align="center" class="th-line">
                            线路名称
                        </th>
                        <th align="center" class="th-line">
                            销售员
                        </th>
                        <th align="center" class="th-line">
                            出团时间
                        </th>
                        <th align="center" class="th-line">
                            计划人数
                        </th>
                        <th align="center" class="th-line">
                            查看计调
                        </th>
                        <th align="center" class="th-line">
                            操作
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>">
                                <input type="hidden" name="ItemUserID" />
                                <td align="center">
                                    <%# Container.ItemIndex+1%>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0)" id="trTourCode">
                                        <%#Eval("TourCode")%></a><div style="display: none;">
                                            <%# GetOperaterInfo(Eval("TourId").ToString())%></div>
                                </td>
                                <td align="left">
                                    <%#Eval("RouteName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("SellerName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("LDate","{0:yyyy-MM-dd}")%>
                                </td>
                                <td align="center">
                                    <%#Eval("PlanPeopleNumber")%>
                                </td>
                                <td align="left" data-class="GetJiDiaoIcon" data-tourid="<%# Eval("TourId")%>">
                                    <%# EyouSoft.Common.UtilsCommons.GetJiDiaoIcon((EyouSoft.Model.HTourStructure.MTourPlanStatus)Eval("TourPlanStatus"))%>
                                </td>
                                <td align="center">
                                    <a data-class="receiveOp" data-TourId="<%# Eval("TourId") %>" data-tourtype="<%# Eval("TourType") %>" href="javascript:void(0);">接收任务</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
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

    <script type="text/javascript">
        var OperaterRemind = {
                
                //计调任务接受
                _OperaterReceive: function(comID, tourId,type) {
                    $.newAjax({
                        type: "POST",
                        url: '/Ashx/ReceiveJob.ashx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&type=receive&com=' + comID + "&Operator=<%=this.SiteUserInfo.Name %>" + "&OperatorID=<%=this.SiteUserInfo.UserId %>" + "&OperatDepID=<%=this.SiteUserInfo.DeptId  %>&tourId=" + tourId,
                        async: false,
                        dataType: "json",
                        success: function(data) {
                            if (data.result) {
                                tableToolbar._showMsg(data.msg, function() {
                                    if(type=='<%=EyouSoft.Model.EnumType.TourStructure.TourType.团队产品 %>'){
                                        window.location.href = "/Plan/PlanConfigPage.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>&type=OrderRemind&tourId=" + tourId + "&tourType=zutuan";
                                   }
                                });
                                
                            }
                        },
                        error: function() {
                            if(arguments[1]!=null)
                                tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                            else
                                tableToolbar._showMsg("服务器忙");
                        }
                    });
                },
                _BindBt:function(){
                        $("#liststyle").find("[data-class='receiveOp']").click(function() {
                        var companyID = "<%=this.SiteUserInfo.CompanyId %>";
                        var tourId = $(this).attr("data-tourid");
                        var tourtype = $(this).attr("data-tourtype");
                        OperaterRemind._OperaterReceive(companyID, tourId,tourtype);
                        return false;
                    });
                }
         }
    
    
        $(function(){
            tableToolbar.init({});
            OperaterRemind._BindBt();
            $("#liststyle").find('tr').each(function(){
                $(this).find('td:eq(1) a').bt({
                        contentSelector: function() {
                            return $(this).next().html();
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
                })
            })
            //查看计调泡泡
            BtFun.InitBindBt("GetJiDiaoIcon");
            
        })
              
    </script>

    </form>
</asp:Content>

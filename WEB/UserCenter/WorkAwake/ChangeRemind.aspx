<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/UserCenter.Master" AutoEventWireup="true"
    CodeBehind="ChangeRemind.aspx.cs" Inherits="Web.UserCenter.WorkAwake.ChangeRemind" %>

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
                <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="PlanChangeRemind.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                    <span>计划变更</span></a></li>
                <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                    href="ChangeRemind.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>">
                    <span>订单变更</span></a></li>
            </ul>
--%>            <%--            <div style="float: left; padding-top: 5px;">
                <ul class="fixed">
                    <li>&nbsp;&nbsp;&nbsp; 设定提醒的天数：
                        <input type="text" size="20" class="formsize80" name="text" id="txtDay">&nbsp;&nbsp;&nbsp;</li>
                    <li style="margin: 0px;"><a id="btnSave" class="ztorderform" style="padding-left: 2px;"
                        hidefocus="true" href="javascript:void(0);"><span>确定</span></a></li></ul>
            </div>--%>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th width="30" class="th-line">
                            序号
                        </th>
                        <th align="center" class="th-line">
                            订单号
                        </th>
                        <th align="center" class="th-line">
                            销售员
                        </th>
                        <th align="center" class="th-line">
                            变更时间
                        </th>
                        <th align="center" class="th-line">
                            变更人
                        </th>
                        <th align="center" class="th-line">
                            变更内容
                        </th>
                        <th align="center" class="th-line">
                            状态
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex%2==0?"":"odd" %>">
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" />
                                    <%# Container.ItemIndex+1 %>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderCode")%>
                                </td>
                                <td align="center">
                                    <%#Eval("OrderSale")%>
                                </td>
                                <td align="center">
                                    <%#Eval("IssueTime")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Operator")%>
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0)" id="orderremind" onclick="ClickBoxy(this)">
                                        <%#Eval("Content")%></a>
                                    <div style="display: none;">
                                        <div class="alertbox-outbox" style="width: 550px;" id="Order_ChangeContent<%# Eval("TourId")%>">
                                            <%--                                            <div style="height: 30px; line-height: 30px; width: 98%; margin: auto; font-size: 14px;">
                                            </div>--%>
                                            <table width="98%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
                                                style="margin: 20px auto">
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
                                            <%--                                          <div style="height: 30px; line-height: 30px; width: 98%; margin: auto; font-size: 14px;">
                                            </div>--%>
                                            <div class="alertbox-btn" style="position: absolute; width: 548px;float: left; border-bottom:#00446b solid 1px;">
                                                <a href="javascript:void(0)" hidefocus="true" onclick="SureChange(this)" id="IsSureChange"
                                                    hidvalueid="<%# Eval("Id")%>">确认变更</a><a href="javascript:void(0)" hidefocus="true"
                                                        class="close"><s class="chongzhi"></s>关闭</a>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td align="center">
                                    <%--<%#Eval("ChangeType")%>--%><b class="fontred">未确认变更</b>
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
    </form>

    <script type="text/javascript">
    
    function ClickBoxy(a)
    {   
       var $this=$(a);
       var htmlnext=$.trim($this.next().html());
       new Boxy(htmlnext, { modal: true, title: "订单变更", width: "550px", height: "330px" })
       return false;
    }
    
    function SureChange(obj){
        var $this=$(obj);
        var tourid=$this.attr("hidvalueid");
        $.newAjax({
            type: "post",
            cache: false,
            dataType:"json",
            url: "ChangeRemind.aspx?doType=IsSureChange&argument=" + tourid,
            success: function(ret) {
                tableToolbar._showMsg(ret.msg);
                if (ret.result) {
                        window.location.reload(); 
                }
            },
            error: function() {
                if(arguments[1]!=null)
                    tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                else
                    tableToolbar._showMsg("服务器忙");
            }
        })
    }
    
    $(function(){
         tableToolbar.init({});
    
    })
    </script>

</asp:Content>

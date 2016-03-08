<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Web.Crm.List"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/CaiWuShaiXuan.ascx" TagName="CaiWuShaiXuan" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%=SL %>' />
            <span class="searchT">
                <p>
                    会员卡号：
                    <input type="text" size="30" class="inputtext formsize120" name="txtCardCode" value='<%=Utils.GetQueryStringValue("txtCardCode") %>' />
                    会员类型：
                    <select class="inputselect" name="txtMemberTypeId" id="txtMemberTypeId">
                        <option value="">--未选择--</option>
                        <%= GetMemberTypesHTML()%>
                    </select>
                    会员姓名：
                    <input type="text" size="30" class="inputtext formsize80" name="txtName" value='<%=Utils.GetQueryStringValue("txtName") %>' />
                    会员积分：
                    <uc1:CaiWuShaiXuan ID="txtJiFen" runat="server" />
                    联系电话：
                    <input type="text" size="30" class="inputtext formsize80" name="txtTelephone" value='<%=Utils.GetQueryStringValue("txtTelephone") %>' />
                    <input type="submit" value="搜索" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="phXinZeng" Visible="false">
                    <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                        <span>新增</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phXiuGai" Visible="false">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0)">
                        <span>修改</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phShanChu" Visible="false">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                    </s><span>删除</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th rowspan="2" class="thinputbg">
                            <input type="checkbox" name="checkbox" id="checkbox1" />
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            会员类型
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            会员卡号
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            会员姓名
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            性别
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            联系电话
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            手机
                        </th>
                        <th colspan="3" align="center" class="th-line">
                            交易记录
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            最后消费时间
                        </th>
                        <th rowspan="2" align="center" class="th-line">
                            积分
                        </th>
                    </tr>
                    <tr>
                        <th align="center" class="th-line">
                            订单数
                        </th>
                        <th align="center" class="th-line">
                            人数
                        </th>
                        <th align="right" class="th-line">
                            金额
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr i_crmid="<%#Eval("CrmId") %>">
                                <td align="center">
                                    <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("CrmId") %>'>
                                </td>
                                <td align="center">
                                    <%# Eval("MemberTypeName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("MemberCardCode")%>
                                </td>
                                <td align="center">
                                    <a onclick="personalList.openDetailsWindow('<%# Eval("CrmId") %>')" href="javascript:void(0)"><%# Eval("Name")%></a>
                                </td>
                                <td align="center">
                                    <%# Eval("Gender") %>
                                </td>
                                <td align="center">
                                    <%# Eval("Telephone")%>
                                </td>
                                <td align="center">
                                    <%# Eval("Mobile")%>
                                </td>
                                <td align="center">
                                    <%# Eval("DingDanShu")%>
                                </td>
                                <td align="center">
                                    <%# Eval("DingDanRenShu")%>
                                </td>
                                <td align="right">
                                    <b class="fontblue">
                                        <%# EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("DingDanJinE"),this.ProviderToMoney)%>
                                    </b>
                                </td>
                                <td style="text-align:center;">
                                    <b><%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LatestTime"), this.ProviderToDate)%></b>
                                </td>
                                <td align="right">
                                    <a href="javascript:void(0)" class="i_jifen">
                                        <%# Eval("JiFen","{0:F2}")%></a>
                                         <input type="hidden" name="ItemUserID" value="<%#Eval("SellerId") %>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                        <tr>
                            <td colspan="12" align="center">
                                暂无数据。
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="tablehead" style="border-top: 0 none;" id="select_Toolbar_Paging_2">
        </div>
    </div>

    <script type="text/javascript">
        var personalList = {
            //查询参数
            winParams: {}
            //打开添加窗口
            , openAddWindow: function() {
                var params = { "sl": "<%=SL %>" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("Edit.aspx", params),
                    title: "新增-个人会员",
                    modal: true,
                    width: "980px",
                    height: "500px"
                });
            }
            //打开修改窗口
            , openUpdateWindow: function(arrs) {
                var params = { "crmId": arrs[0].find("input[type='checkbox']").val(), "sl": "<%=SL %>" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("Edit.aspx", params),
                    title: "修改-个人会员",
                    modal: true,
                    width: "980px",
                    height: "500px"
                });
            }
            //查看
            , openDetailsWindow: function(crmId) {
                var params = { "crmId": crmId, "sl": "<%=SL %>", "edittype": "details" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("Edit.aspx", params),
                    title: "个人会员资料查看",
                    modal: true,
                    width: "980px",
                    height: "500px"
                });
            }
            //删除
            , del: function(arrs) {
                var deleteids = [];
                $(arrs).each(function() {
                    var _crmid = this.find("input[type='checkbox']").val();
                    if (_crmid != "on") deleteids.push(_crmid);
                })

                var params = { "doType": "delete", "deleteids": deleteids.join(","), "sl": "<%=SL %>" };

                $.newAjax({
                    type: "GET",
                    cache: false,
                    url: "List.aspx",
                    data: params,
                    dataType: "json",
                    async: false,
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg("删除成功！", function() {
                                window.location.href = window.location.href;
                            })

                        }
                        else {
                            parent.tableToolbar._showMsg("删除失败")
                        }
                    }
                });
            }
            //初始处理
            , init: function() {
                this.winParams = Boxy.getUrlParams();
                $("#txtMemberTypeId").val(this.winParams["txtMemberTypeId"]);
                var jiFenShaiXuan = new wuc.caiWuShaiXuan(window['<%=txtJiFen.ClientUniqueID %>']);
                jiFenShaiXuan.setOperator(this.winParams['<%=txtJiFen.ClientUniqueIDOperator %>']);
                jiFenShaiXuan.setOperatorNumber(this.winParams['<%=txtJiFen.ClientUniqueIDOperatorNumber %>']);

                $(".toolbar_add").bind("click", function() { personalList.openAddWindow(); });

                tableToolbar.init({ tableContainerSelector: "#liststyle"
                    , objectName: "个人会员 !"
                    , updateCallBack: function(objsArr) { personalList.openUpdateWindow(objsArr); }
                    , deleteCallBack: function(objsArr) { personalList.del(objsArr); }
                });

                //clone toolbar、paging
                $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            },
            openJiFenWindow: function(obj) {
                var _$tr = $(obj).closest("tr");

                var para = { sl: "<%=SL %>", crmId: _$tr.attr("i_crmid") };
                
                Boxy.iframeDialog({
                    iframeUrl: "/Crm/JiFen.aspx?" + $.param(para),
                    title: "积分明细",
                    modal: true,
                    width: "860px",
                    height: "400px"
                });
            }
        };

        $(document).ready(function() {
            personalList.init();
            $(".i_jifen").click(function() { personalList.openJiFenWindow(this); });
        });
    </script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>

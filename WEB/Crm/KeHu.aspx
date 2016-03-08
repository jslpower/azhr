<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KeHu.aspx.cs" Inherits="Web.Crm.KeHu"
    MasterPageFile="~/MasterPage/Front.Master" %>

<%@ Import Namespace="EyouSoft.Common" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <input type="hidden" name="sl" value='<%= SL %>' />
            <span class="searchT">
                <p>
                    单位名称：
                    <input type="text" size="30" class="inputtext formsize120" name="txtUnitName" value='<%=Utils.GetQueryStringValue("txtUnitName") %>' />
                    省份：
                    <select id="ddlProvice" name="ddlProvice" class="inputselect">
                    </select>
                    城市：
                    <select id="ddlCity" name="ddlCity" class="inputselect">
                    </select>
                    客户等级：
                    <select name="ddlLevId" class="inputselect" id="ddlLevId">
                        <%=BindCrmLevId()%>
                    </select>
                    责任销售：
                    <uc1:SellsSelect ID="txtXiaoShouYuan" runat="server" SelectFrist="false" />
                    联系人：<input type="text" class="inputtext formsize80" name="txtLxrName" id="txtLxrName"
                        value="<%=Utils.GetQueryStringValue("txtLxrName") %>" />
                    <input type="submit" value="搜索" class="search-btn" />
                </p>
            </span>
            </form>
        </div>
        <div class="tablehead" id="select_Toolbar_Paging_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="phXinZeng" Visible="false">
                    <li><s class="addicon"></s><a class="toolbar_add" id="A1" hidefocus="true" href="javascript:void(0)">
                        <span>新增</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phXiuGai" Visible="false">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0)">
                        <span>修改</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="phShanChu" Visible="false">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                    </s><span>删除</span></a> </li>
                    <li class="line"></li>
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
                        <th class="thinputbg" rowspan="2">
                            <input type="checkbox" id="checkbox" name="checkbox" />
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            所在地
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            公司名称
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            是否签订合同
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            责任销售
                        </th>
                        <th align="center" class="th-line" colspan="4">
                            交易记录
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            最后消费时间
                        </th>
                        <th align="center" class="th-line" rowspan="2">
                            操作
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
                        <th align="right" class="th-line">
                            拖欠款
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server" OnItemDataBound="rptList_ItemDataBound">
                        <ItemTemplate>
                          <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %>>
                                <td align="center">
                                    <input type="checkbox" id="checkbox" name="checkbox" value='<%# Eval("CrmId") %>'>
                                </td>
                                <td align="center">
                                    <asp:Literal runat="server" ID="ltrSuoZaiDi"></asp:Literal>
                                </td>
                                <td align="center">
                                    <%# Eval("Name")%>
                                </td>
                                <td align="center">
                                    <%# Convert.ToBoolean(Eval("IsXieYi"))?"是":"否"%>
                                </td>
                                <td align="center">
                                    <%# Eval("SellerName")%>
                                </td>
                                <td align="center">
                                    <%# Eval("DingDanShu")%>
                                </td>
                                <td align="center">
                                    <%# Eval("DingDanRenShu")%>
                                </td>
                                <td align="right">
                                    <b class="fontblue">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("DingDanJinE"), this.ProviderToMoney)%></b>
                                </td>
                                <td align="right">
                                    <b class="fontbsize12">
                                        <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("TuoQianJinE"), this.ProviderToMoney)%></b>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("LastTime"), this.ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <a onclick="crmList.openAccountWindow('<%# Eval("CrmId") %>')" href="javascript:void(0)">
                                        帐号管理</a>&nbsp;&nbsp; <a onclick="crmList.openDetailsWindow('<%# Eval("CrmId") %>')"
                                            href="javascript:void(0)">查看</a>
                                    <input type="hidden" name="ItemUserID" value="<%#Eval("SellerId") %>" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder runat="server" ID="phEmpty" Visible="false">
                        <tr>
                            <td colspan="11" align="center">
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
        var crmList = {
            //查询参数
            winParams: {}
            //添加
            , openAddWindow: function() {
                var params = { "sl": "<%=SL %>" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("KeHuBianJi.aspx", params),
                    title: "新增-<%=ListTypeName %>",
                    modal: true,
                    width: "980px",
                    height: "550px"
                });
            }
            //修改
            , openUpdateWindow: function(objsArr) {
                var params = { "crmId": objsArr[0].find("input[type='checkbox']").val(), "sl": "<%=SL %>" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("KeHuBianJi.aspx", params),
                    title: "修改-<%=ListTypeName %>",
                    modal: true,
                    width: "980px",
                    height: "550px"
                });
            }
            //删除
            , del: function(objArr) {
                var deleteids = [];
                //遍历按钮返回数组对象
                for (var i = 0; i < objArr.length; i++) {
                    var _crmid = objArr[i].find("input[type='checkbox']").val();
                    //从数组对象中找到数据所在，并保存到数组对象中
                    if (_crmid != "on") {
                        deleteids.push(_crmid);
                    }
                }

                var params = { "doType": "delete", "deleteids": deleteids.join(","), "sl": "<%=SL %>" };

                $.newAjax({
                    type: "GET",
                    cache: false,
                    url: "KeHu.aspx",
                    data: params,
                    dataType: "json",
                    async: false,
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg("删除成功！")
                            window.location.reload();
                        }
                        else {
                            tableToolbar._showMsg("删除失败")
                        }
                    }
                });
            }
            //账号管理
            , openAccountWindow: function(crmId) {
                var params = { "crmId": crmId, "sl": "<%=SL %>" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("FenPeiZhangHao.aspx", params),
                    title: "账号管理-<%=ListTypeName %>",
                    modal: true,
                    width: "980px",
                    height: "390px"
                });
            }
            //查看
            , openDetailsWindow: function(crmId) {
                var params = { "crmId": crmId, "sl": "<%=SL %>", "type": "chk" };
                Boxy.iframeDialog({
                    iframeUrl: Boxy.createUri("KeHuBianJi.aspx", params),
                    title: "客户资料查看-<%=ListTypeName %>",
                    modal: true,
                    width: "980px",
                    height: "550px"
                });
            }
            //初始化
            , init: function() {
                this.winParams = Boxy.getUrlParams();

                $("#ddlLevId").val(this.winParams["ddlLevId"]);
                $("#<%=txtXiaoShouYuan.SellsIDClient %>").val('<%=Utils.GetQueryStringValue(txtXiaoShouYuan.SellsIDClient) %>');
                $("#<%=txtXiaoShouYuan.SellsNameClient %>").val('<%=Utils.GetQueryStringValue(txtXiaoShouYuan.SellsNameClient) %>');
                $(".toolbar_add").bind("click", function() { crmList.openAddWindow(); });

                tableToolbar.init({ tableContainerSelector: "#liststyle"
                    , objectName: "客户单位 !"
                    , updateCallBack: function(objsArr) { crmList.openUpdateWindow(objsArr); }
                    , deleteCallBack: function(objsArr) { crmList.del(objsArr); }
                });

                $('.tablelist-box').moveScroll();

                //init province and city
                pcToobar.init({ pID: "#ddlProvice"
                    , cID: "#ddlCity"
                    , pSelect: crmList.winParams["ddlProvice"]
                    , cSelect: crmList.winParams["ddlCity"]
                });

                //clone toolbar、paging
                $("#select_Toolbar_Paging_1").children().clone(true).prependTo("#select_Toolbar_Paging_2");
            }
        };

        $(document).ready(function() {
            crmList.init();
        });
    </script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>

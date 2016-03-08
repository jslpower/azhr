<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="SingleServerList.aspx.cs" Inherits="EyouSoft.WEB.Sales.SingleServerList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="../UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/ValiDatorForm.js" type="text/javascript"></script>

    <div class="mainbox">
        <form id="form2" method="get">
        <div class="searchbox fixed">
            <span class="searchT">
                <p>
                    订单号：<input type="text" size="20" class="inputtext formsize180" name="txtOrderId"
                        value='<%=Request.QueryString["txtOrderId"]%>' />
                    下单时间：
                    <input type="text" size="20" onfocus="WdatePicker()" class="inputtext formsize80"
                        name="txtOrderSTime" id="txtOrderSTime" value='<%=Request.QueryString["txtOrderSTime"]%>' />-<input
                            type="text" size="20" onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtOrderSTime\')}'})"
                            class="inputtext formsize80" name="txtOrderETime" id="txtOrderETime" value='<%=Request.QueryString["txtOrderETime"]%>' />
                    操作员：<uc1:SellsSelect ID="SellsSelect1" runat="server" SelectFrist="false" />
                    &nbsp;客户单位：<uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" SelectFrist="false" />
                    &nbsp;状态：
                    <select name="status" id="status" class="inputselect">
                        <option value="-1" selected="selected">请选择</option>
                        <option value="<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.销售未派计划 %>">操作中</option>
                        <option value="<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.计调配置 %>">已落实</option>
                        <option value="<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.财务待审 %>">待终审</option>
                        <option value="<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.单团核算 %>">财务核算</option>
                        <option value="<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.封团 %>">核算结束</option>
                        <option value="<%=(int)EyouSoft.Model.EnumType.TourStructure.TourStatus.已取消 %>">已取消</option>
                    </select>
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" /><input type="submit"
                        value="搜索" class="search-btn" /></p>
            </span>
        </div>
        </form>
        <div class="tablehead" id="ToolBar_1">
            <ul class="fixed">
                <li><s class="addicon"></s><a id="link1" class="toolbar_add" hidefocus="true" href="javascript:void(0)">
                    <span>新增</span></a></li>
                <li class="line"></li>
                <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0)">
                    <span>修改</span></a></li>
                <li class="line"></li>
                <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                </s><span>删除</span></a></li>
                <li class="line"></li>
                <%--<li><s class="cancelicon"></s><a class="toolbar_cancel" hidefocus="true" href="javascript:void(0);">
                    <span>取消</span></a></li><li class="line"></li>--%>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table cellspacing="0" border="0" width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            订单号
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            客户单位
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            联系人
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            电话
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            客人姓名
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            人数
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            销售员
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            OP
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            服务类别
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            操作员
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            状态
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            收款
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            Voucher
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr <%#Container.ItemIndex%2==0?" class=\"odd\" ":""  %> i_tourid="<%#Eval("TourId") %>">
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                                    <input type="checkbox" id="checkbox" name="checkbox" value="<%#Eval("TourId")%>">
                                </td>
                                <td align="center">
                                    <a href="javascript:void(0);" class="a_update">
                                        <%#Eval("OrderCode")%></a>
                                </td>
                                <td align="left">
                                    <%#Eval("BuyCompanyName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("ContactTel")%>
                                </td>
                                <td align="center">
                                    <%#this.GetKeRen(Eval("orderid"))%>
                                </td>
                                <td align="center" class="fontblue">
                                    <b>
                                        <%#Eval("Adults")%></b>
                                </td>
                                <td align="center">
                                    <%#Eval("SellerName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("Planers")%>
                                </td>
                                <td align="center" data-tourid="<%# Eval("TourId") %>">
                                    <%# GetJiDiaoIcon(Eval("TourId").ToString())%>
                                </td>
                                <td>
                                    <%#Eval("Operator")%>
                                </td>
                                <td align="center">
                                    <%# getState(Eval("TourStatus"))%>
                                </td>
                                <td align="center">
                                    <a data-tourid="<%# Eval("TourId") %>" data-orderid="<%# Eval("OrderId") %>" class="Objdengji_a"
                                        data-objtype="1" href="javascript:;">登记</a>
                                </td>
                                <td align="center">
                                    <a href="/PrintPage/SinglePrint.aspx?tourid=<%#Eval("TourId") %>" class="print_a" target="_blank">Voucher</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0pt none;" class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("ToolBar_1").innerHTML);
            </script>

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
                        <input type="hidden" id="txtQuXiaoId" />
                        <textarea style="height: 93px;" class="inputtext formsize450" id="txtQuXiaoYuanYin"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a href="javascript:void(0);" id="i_a_btn_quxiao"><s class="baochun"></s>保存</a>
        </div>
    </div>

    <script type="text/javascript">
        var SinglePage = {
            quXiaoBox: null,
            reload: function() {
                window.location.href = window.location.href;
            },
            PageInit: function() {
                tableToolbar.IsHandleElse = true;
                SinglePage.BindBtn();
                $('.tablelist-box').moveScroll();
            },
            BindBtn: function() {
                //绑定Add事件
                $(".toolbar_add").click(function() {
                    SinglePage.Add();
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "行!", //这个参数讲不明白，请联系柴逸宁，哈哈

                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    updateCallBack: function(objsArr) {
                        //修改
                        SinglePage.Update(objsArr);
                    },
                    deleteCallBack: function(objsArr) {
                        var _items = [];
                        for (var i = 0; i < objsArr.length; i++) {
                            var _v = objsArr[i].find("input[type='checkbox']").val();
                            if (_v != "on") _items.push(_v);
                        }

                        if (_items.length == 0) { tableToolbar._showMsg("请选择要删除的单项业务"); return; }
                        if (_items.length > 1) { tableToolbar._showMsg("一次只能删除一个单项业务"); return; }

                        var _data = { txtTourId: _items[0] };

                        $.ajax({
                            type: "POST", url: window.location.href + "&doType=delete",
                            cache: false, dataType: "json", async: false, data: _data,
                            success: function(response) {
                                tableToolbar._showMsg(response.msg, function() { SinglePage.reload(); });
                            },
                            error: function() {
                                SinglePage.reload();
                            }
                        });
                    },
                    cancelCallBack: function(arr) {
                        var _items = [];
                        for (var i = 0; i < arr.length; i++) {
                            var _v = arr[i].find("input[type='checkbox']").val();
                            if (_v != "on") _items.push(_v);
                        }

                        if (_items.length == 0) { tableToolbar._showMsg("请选择要取消的单项业务"); return; }
                        if (_items.length > 1) { tableToolbar._showMsg("一次只能取消一个单项业务"); return; }

                        $("#txtQuXiaoId").val(_items[0]);

                        SinglePage.quXiaoBox = new Boxy($("#div_Canel"), { modal: true, fixed: false, title: "取消单项业务-请输入取消原因", width: "580px", height: "210px" });
                    }

                });
            },

            Add: function() {
                var data = SinglePage.dataBoxy();
                data.title = "新增单项业务";
                data.url = "/Sales/SingleServerEdit.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.单项业务_单项业务 %>";
                data.width = "1000px";
                data.height = "800px";
                SinglePage.ShowBoxy(data);
            },
            Update: function(objsArr) {
                var data = SinglePage.dataBoxy();
                data.title = "修改单项业务";
                data.url = "/Sales/SingleServerEdit.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.单项业务_单项业务 %>&doType=update&id=" + objsArr[0].find("input[type='checkbox']").val();
                data.width = "1000px";
                data.height = "800px";
                SinglePage.ShowBoxy(data);
            },
            dataBoxy: function() {
                return {
                    url: "",
                    title: "",
                    width: "",
                    height: ""
                }
            },
            ShowBoxy: function(data) {
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },

            quXiao: function(obj) {
                var _data = { txtQuXiaoId: $("#txtQuXiaoId").val(), txtYuanYin: $.trim($("#txtQuXiaoYuanYin").val()) };
                if (_data.txtYuanYin.length == 0) { tableToolbar._showMsg("请输入取消原因！"); return; };
                if (!confirm("取消业务操作不可逆，你确定要取消吗？")) return;

                $(obj).unbind("click").text("处理中....");
                $.ajax({
                    type: "POST", url: window.location.href + "&doType=quxiao",
                    cache: false, dataType: "json", async: false, data: _data,
                    success: function(response) {
                        tableToolbar._showMsg(response.msg, function() { SinglePage.reload(); });
                    },
                    error: function() {
                        SinglePage.reload();
                    }
                });
            }
        }

        $(function() {
            $("#status").val('<%=Request.QueryString["status"] %>');
            SinglePage.PageInit();

            $(".Objdengji_a").click(function() {
                var data = SinglePage.dataBoxy();
                data.title = "销售收款";
                data.url = "/Fin/YingShouDeng.aspx?" + $.param({
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    OrderId: $(this).attr("data-orderid"),
                    ReturnOrSet: 1,
                    ParentType: 1,
                    DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.订单收款%>',
                    tourId: $(this).attr("data-tourid")
                });

                data.width = "980px";
                data.height = "500px";
                SinglePage.ShowBoxy(data);
            })
            $(".ObjAdd_a").click(function() {
                var data = SinglePage.dataBoxy();
                data.title = "服务安排";
                data.url = "/Sales/SingleServerTypeAdd.aspx?tourid=" + $(this).closest("td").attr("data-tourid") + "&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.单项业务_单项业务 %>&tp=" + $(this).attr("data-objtype");
                data.width = "980px";
                data.height = "500px";
                SinglePage.ShowBoxy(data);
            })

            $("#i_a_btn_quxiao").click(function() { SinglePage.quXiao(this); });

            $("#liststyle").find("a[data-class='QuXiaoYuanYin']").each(function() {
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
            
            $(".a_update").click(function(){var arr=[];arr.push($(this).closest("tr"));SinglePage.Update(arr);})
        })
    </script>

</asp:Content>

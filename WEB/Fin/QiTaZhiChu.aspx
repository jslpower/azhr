<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QiTaZhiChu.aspx.cs" Inherits="EyouSoft.Web.Fin.QiTaZhiChu" MasterPageFile="~/MasterPage/Front.Master"%>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="/UserControl/SellsSelect.ascx" TagName="SellsSelect" TagPrefix="uc1" %>
<%@ Register Src="/UserControl/CustomerUnitSelect.ascx" TagName="CustomerUnitSelect"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mainbox">
        <form id="SelectFrom" method="get">
        <div class="searchbox border-bot fixed">
            <span class="searchT">
                <p>
                    支出项目：<input type="text" name="txt_PayItemName" value="<%= Request.QueryString["txt_PayItemName"]%>"
                        class="inputtext formsize140" />
                    支付单位：
                    <uc2:CustomerUnitSelect ID="CustomerUnitSelect1" runat="server" IsUniqueness="false"
                        selectfrist="false" />
                    请款人：<uc1:SellsSelect ID="txt_SellsSelect" runat="server" selectfrist="false" /><br />
                    付款日期：
                    <input type="text" name="txt_inDateS" onclick="WdatePicker()" value="<%=Request.QueryString["txt_inDateS"] %>"
                        class="inputtext formsize80" />
                    -
                    <input type="text" name="txt_inDateE" onclick="WdatePicker()" value="<%=Request.QueryString["txt_inDateE"] %>"
                        class="inputtext formsize80" />
                    状态：
                    <select name="Status" class="inputselect" id="sel_Status">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.FinStructure.FinStatus)), Request.QueryString["Status"] ?? "-1", true)%>
                    </select>
                    <input type="submit" class="search-btn" /></p>
            </span>
        </div>
        <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
        </form>
        <div id="tablehead" class="tablehead">
            <ul class="fixed">
                <asp:PlaceHolder ID="pan_Add" runat="server">
                    <li id="li_Add"><s class="dengji"></s><a href="javascript:void(0);" hidefocus="true">
                        <span>登记</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_copy" runat="server">
                    <li><s class="copyicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_copy">
                        <span>复制</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_update" runat="server">
                    <li><s class="updateicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_update">
                        <span>修改</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_delete" runat="server">
                    <li><s class="delicon"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_delete">
                        <span>删除</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_shenhe" runat="server">
                    <li><s class="shenhe"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_shenhe">
                        <span>批量审核</span></a></li>
                    <li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pan_zhifu" runat="server">
                    <li><s class="shenhe"></s><a href="javascript:void(0);" hidefocus="true" class="toolbar_zhifu">
                        <span>批量支付</span></a></li></asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box">
            <table width="100%" id="liststyle">
                <tr>
                    <th class="thinputbg">
                        <input type="checkbox" name="checkbox" />
                    </th>
                    <th align="center" class="th-line">
                        付款日期
                    </th>
                    <th align="center" class="th-line">
                        支付项目
                    </th>
                    <th align="center" class="th-line">
                        支付单位
                    </th>
                    <th align="center" class="th-line">
                        请款人
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="right" class="th-line">
                        <span class="th-line h20">支付金额</span>
                    </th>
                    <th align="center" class="th-line">
                        支付方式
                    </th>
                    <th align="center" class="th-line">
                        GST
                    </th>
                    <th align="center" class="th-line">
                        备注
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-otherfeeid="<%#Eval("Id") %>" data-intstatus="<%#(int)Eval("Status") %>"
                            data-tourid="<%#Eval("TourId") %>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" />
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("DealTime"), ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("FeeItem")%>
                            </td>
                            <td align="left">
                                <a href="javascript:void(0);" data-class="a_popo">
                                    <%#Eval("Crm")%></a><span style="display: none"><b><%#Eval("Crm")%></b><br />
                                        联系人：<%#Eval("ContactName")%><br />
                                        联系方式：<%#Eval("ContactPhone")%></span>
                            </td>
                            <td align="center">
                                <%#Eval("Dealer")%>
                            </td>
                            <td align="center">
                                <%#Eval("Seller")%>
                            </td>
                            <td align="right">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FeeAmount"),ProviderToMoney)%>
                                </b>
                            </td>
                            <td align="center">
                                <%#Eval("PayTypeName")%>
                            </td>
                            <td align="center">
                                <%#(bool)Eval("IsTax")?"√":"×"%>
                            </td>
                            <td align="center">
                                <%#Eval("Remark")%>
                            </td>
                            <td align="center" data-intstatus="<%#(int)Eval("Status") %>">
                                <a data-class="a_ExamineV" href="javascript:void(0);">审核 </a><a data-class="a_Pay"
                                    href="javascript:void(0);">支付</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Panel ID="pan_Msg" runat="server">
                    <tr align="center">
                        <td colspan="11">
                            暂无数据!
                        </td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <!--列表结束-->
        <div id="tablehead_clone">
        </div>
    </div>

    <script type="text/javascript">
        var PayList = {
            ShowBoxy: function(data) {/*显示弹窗*/
                Boxy.iframeDialog({
                    iframeUrl: data.url,
                    title: data.title,
                    modal: true,
                    width: data.width,
                    height: data.height
                });
            },
            DataBoxy: function() {/*弹窗默认参数*/
                return {
                    url: "/Fin",
                    title: "其他支出",
                    width: "650px",
                    height: "340px"
                }
            },
            Bt: function(obj) {/*泡泡提示*/
                $("#liststyle a[data-class='a_popo']").bt({
                    contentSelector: function() {
                        return $(this).next("span").html();
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


            },
            Updata: function(arr) {/*修改*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("Update");
                data.url += ("/QiTaEdit.aspx?" + $.param({
                    parent: 2,
                    type: "Updata",
                    OtherFeeID: $(arr[0]).attr("data-otherfeeid"),
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                }));
                this.ShowBoxy(data);
            },
            Copy: function(arr) {/*复制*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("Copy");
                data.url += ("/QiTaEdit.aspx?" + $.param({
                    parent: 2,
                    type: "Copy",
                    OtherFeeID: $(arr[0]).attr("data-otherfeeid"),
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                }));
                this.ShowBoxy(data);
            },
            ExamineV: function(Ids, Title) { /*审核弹窗*/
                var data = this.DataBoxy();
                data.title += (Title + EnglishToChanges.Ping("ExamineV"));
                data.url += ("/QiTaShenHe.aspx?" + $.param({
                    parent: 2, /*收入的枚举*/
                    type: "ExamineV",
                    OtherFeeID: typeof (Ids) == "string" ? Ids : Ids.join(','),
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>'
                }));
                this.ShowBoxy(data);
            },
            Verify: function(arr) {/*批量审核*/
                var that = this, Ids = [], errIds = [];
                $(arr).each(function() {
                    var obj = $(this);
                    if (parseInt(obj.attr("data-intstatus")) === 0) {
                        Ids.push(obj.attr("data-otherfeeid"));
                    }
                    else {
                        errIds.push(obj.attr("data-otherfeeid"));
                    }
                })
                if (errIds.length == arr.length) {
                    parent.tableToolbar._showMsg('无可审批记录!')
                    return false;
                }
                that.ExamineV(Ids, "-批量");
            },
            QuantityPay: function(arr) {
                var that = this, Ids = [];
                $(arr).each(function() {
                    var obj = $(this);
                    if (parseInt(obj.attr("data-intstatus")) === 1) {
                        Ids.push(obj.attr("data-otherfeeid"));
                    }
                    else {
                        tableToolbar._showMsg('请确定勾选的记录中,不存在 已支付的记录!');
                        return false;
                    }
                })
                if (Ids.length == arr.length) {
                    that.Pay(Ids, "-批量");
                }
            },
            Pay: function(Ids, Title, isShow) {/*支付*/
                var data = this.DataBoxy();
                data.title += (Title + EnglishToChanges.Ping("Pay"));
                data.url += ("/QiTaZhiFu.aspx?" + $.param({
                    OtherFeeID: typeof (Ids) == "string" ? Ids : Ids.join(','),
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    isShow: isShow || ""
                }));
                this.ShowBoxy(data);
            },
            DelAll: function(arr) {/*删除*/
                var Ids = [], url = '/Fin/QiTaZhiChu.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';
                $(arr).each(function() {
                    Ids.push($(this).attr("data-otherfeeid"));
                })
                $.newAjax({
                    type: "post",
                    data: $.param({
                        Ids: Ids.join(','),
                        doType: "Del"
                    }),
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        if (parseInt(ret.result) === 1) {
                            parent.tableToolbar._showMsg('删除成功!', function() {
                                location.href = location.href;
                            });
                        }
                        else {
                            parent.tableToolbar._showMsg(ret.msg);
                            that.PageInit();
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        tableToolbar._showMsg("服务器忙！");
                    }
                });
            },
            BindBtn: function() {/*绑定功能按钮*/
                var that = this;
                $("#li_Add").click(function() {
                    var data = that.DataBoxy();
                    data.title += EnglishToChanges.Ping("Register");
                    data.url += ("/QiTaEdit.aspx?" + $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', parent: 2 }));
                    that.ShowBoxy(data);
                    return false;

                })
                $("#liststyle a[data-class='a_ExamineV']").click(function() {
                    var obj = $(this);
                    that.ExamineV(obj.closest("tr").attr("data-otherfeeid"), "");
                    return false;
                })
                $("#liststyle a[data-class='a_Pay']").click(function() {
                    var obj = $(this);
                    that.Pay(obj.closest("tr").attr("data-otherfeeid"), "");
                    return false;
                })

                $("a[data-class='a_InMoney']").unbind("click").click(function() {/*财务入账*/
                    var obj = $(this)
                    var tr = obj.closest("tr");
                    if (obj.attr("data-type") == "InMoney") {

                        parent.Boxy.iframeDialog({
                            iframeUrl: "/FinanceManage/Common/InAccount.aspx?" + $.param({
                                sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                                KeyId: tr.attr("data-otherfeeid"),
                                DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.杂费支出%>',
                                tourId: tr.attr("data-tourid")
                            }),
                            title: "财务入账",
                            modal: true,
                            width: "900px",
                            height: "500px",
                            draggable: true
                        });
                    }
                    else {
                        that.Pay(tr.attr("data-otherfeeid"), "", "false");
                    }
                })
                tableToolbar.init({
                    tableContainerSelector: "#liststyle", //表格选择器
                    objectName: "记录",
                    //默认按钮事件
                    updateCallBack: function(objArr) {/*修改*/
                        var obj = $(objArr[0]);
                        if (parseInt(obj.attr("data-intstatus")) === 0) {
                            that.Updata(obj);
                        }
                        else {
                            tableToolbar._showMsg('已审批的支出,无法修改!');
                        }
                    },
                    copyCallBack: function(objArr) {/*复制*/
                        that.Copy(objArr);
                    },
                    deleteCallBack: function(objArr) {/*删除(批量)*/
                        var obj = $(objArr[0]);
                        if (parseInt(obj.attr("data-intstatus")) === 0) {
                            that.DelAll(objArr);
                        }
                        else {
                            tableToolbar._showMsg('已审批的支出,无法删除!');
                        }
                    },
                    otherButtons: [//自定义按钮（审核）
                    {
                    button_selector: '.toolbar_shenhe', //按钮选择器
                    sucessRulr: 2, //验证选择记录---1表示单选，2表示多选
                    msg: '未选中任何记录 ', //验证未通过提示文本
                    buttonCallBack: function(objArr) {//验证通过执行函数
                        //审核(批量)
                        that.Verify(objArr);
                    }
                },
                    {
                        button_selector: '.toolbar_zhifu', //按钮选择器
                        sucessRulr: 2, //验证选择记录---1表示单选，2表示多选
                        msg: '未选中任何记录 ', //验证未通过提示文本
                        buttonCallBack: function(objArr) {//验证通过执行函数
                            //审核(批量)
                            that.QuantityPay(objArr);
                        }
                    }
                    ]
            })
        },
        PageInit: function() {/*初始化*/
            var that = this;
            $("#sel_Status option[value='<%=(int)EyouSoft.Model.EnumType.FinStructure.FinStatus.销售待确认 %>']").remove();
            var intstatus;
            $("#liststyle td[data-intstatus]").each(function() {
                var obj = $(this)
                switch (parseInt(obj.attr("data-intstatus")) || '<%=(int)EyouSoft.Model.EnumType.FinStructure.FinStatus.财务待审批%>' - 0) {
                    case '<%=(int)EyouSoft.Model.EnumType.FinStructure.FinStatus.账务已支付%>' - 0:
                        obj.find("[data-class='a_Pay']").remove();
                        obj.find("[data-class='a_ExamineV']").remove();
                        break;
                    case '<%=(int)EyouSoft.Model.EnumType.FinStructure.FinStatus.财务待审批%>' - 0:
                        obj.find("[data-class='a_InMoney']").remove();
                        obj.find("[data-class='a_Pay']").remove();
                        break;
                    case '<%=(int)EyouSoft.Model.EnumType.FinStructure.FinStatus.账务待支付%>' - 0:
                        obj.find("[data-class='a_ExamineV']").remove();
                        obj.find("[data-class='a_InMoney']").remove();
                        break;
                }
            })
            that.Bt();
            if ('<%=IsEnableKis %>' == 'False') {
                var obj = $("a[data-class='a_InMoney']");
                obj.html("查看");
                obj.attr("data-type", "");
            }
            that.BindBtn();
            $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));

        }
    }
    $(function() {
        //绑定功能按钮
        PayList.PageInit();
    })
    </script>

</asp:Content>

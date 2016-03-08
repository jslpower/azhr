<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QiTaShouRu.aspx.cs" Inherits="EyouSoft.Web.Fin.QiTaShouRu" MasterPageFile="~/MasterPage/Front.Master"%>
<%@ Import Namespace="EyouSoft.Model.EnumType.FinStructure" %>

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
                    收入项目：<input type="text" name="txt_inItemName" value="<%=Request.QueryString["txt_inItemName"] %>"
                        class="inputtext formsize140" />
                    客户单位：<uc2:customerunitselect id="CustomerUnitSelect1" isuniqueness="false" runat="server"
                        selectfrist="false" />
                    销售员：<uc1:sellsselect id="txt_SellsSelect" runat="server" selectfrist="false" />
                    收款时间：
                    <input type="text" name="txt_inDateS" onclick="WdatePicker()" value="<%=Request.QueryString["txt_inDateS"] %>"
                        class="inputtext formsize80" />
                    -
                    <input type="text" name="txt_inDateE" onclick="WdatePicker()" value="<%=Request.QueryString["txt_inDateE"] %>"
                        class="inputtext formsize80" />
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
                </asp:PlaceHolder>
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
                        <input type="checkbox" name="checkbox" id="checkbox1" />
                    </th>
                    <th align="center" class="th-line">
                        收款时间
                    </th>
                    <th align="center" class="th-line">
                        收入项目
                    </th>
                    <th align="center" class="th-line">
                        团号
                    </th>
                    <th align="center" class="th-line">
                        客户单位
                    </th>
                    <th align="center" class="th-line">
                        销售员
                    </th>
                    <th align="center" class="th-line">
                        登记人
                    </th>
                    <th align="right" class="th-line">
                        <span class="th-line h20">金额</span>
                    </th>
                    <th align="center" class="th-line">
                        支付方式
                    </th>
                    <th align="center" class="th-line">
                        GST
                    </th>
                    <th align="left" class="th-line">
                        备注
                    </th>
                    <th align="center" class="th-line">
                        操作
                    </th>
                </tr>
                <asp:Repeater ID="rpt_list" runat="server">
                    <ItemTemplate>
                        <tr data-otherfeeid="<%#Eval("Id") %>" data-intstatus="<%#(int)Eval("Status") %>"
                            data-crm="<%#Eval("Crm") %>" data-crmid="<%#Eval("CrmId") %>" data-tourid="<%#Eval("TourId") %>"
                            data-bill="<%#Eval("Bill") %>" data-feeamount="<%#Eval("FeeAmount") %>" data-sellerid="<%#Eval("DealerId") %>"
                            data-sellername="<%#Eval("Dealer") %>" data-contact="<%#Eval("ContactName")%>"
                            data-phone="<%#Eval("ContactPhone")%>">
                            <td align="center">
                                <input type="checkbox" name="checkbox" id="checkbox" value="<%#Eval("TourId") %>" />
                                <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                            </td>
                            <td align="center">
                                <%# EyouSoft.Common.UtilsCommons.GetDateString(Eval("DealTime"),ProviderToDate)%>
                            </td>
                            <td align="center">
                                <%#Eval("FeeItem")%>
                            </td>
                            <td align="center">
                                <%#Eval("TourCode")%>
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
                                <%#Eval("Operator")%>
                            </td>
                            <td align="right">
                                <b class="fontbsize12">
                                    <%#EyouSoft.Common.UtilsCommons.GetMoneyString(Eval("FeeAmount"),ProviderToMoney)%></b>
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
                            <td align="center">
                                <%if (this.PrivsPage[0] == "1")
                                  { %>
                                <a data-class="a_ExamineV" data-intstatus="<%#(int)Eval("Status") %>" href="javascript:void(0);"><%#(FinStatus)Eval("Status") == FinStatus.财务待审批 ? "未审核" : "已审核"%>
                                </a>
                                <%} %>
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
        var IncomeList = {
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
                    title: "其他收入",
                    width: "650px",
                    height: "230px"
                }
            },
            Bt: function(obj) {/*泡泡提示*/
                $(".bt-wrapper").html("");
                var obj = $(obj);
                obj.each(function() {
                    $(this).bt({
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
                })

            },
            Updata: function(arr) {/*修改*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("Update");
                data.url += ("/QiTaEdit.aspx?" + $.param({
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    parent: 1,
                    type: "Updata",
                    OtherFeeID: $(arr[0]).attr("data-otherfeeid")
                }));
                this.ShowBoxy(data);
            },
            Copy: function(arr) {/*复制*/
                var data = this.DataBoxy();
                data.title += EnglishToChanges.Ping("Copy");
                data.url += ("/QiTaEdit.aspx?" + $.param({
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    parent: 1,
                    type: "Copy",
                    OtherFeeID: $(arr[0]).attr("data-otherfeeid")
                }));
                this.ShowBoxy(data);
            },
            ExamineV: function(Ids, Title, tourid) { /*审核弹窗*/
                var data = this.DataBoxy();
                data.title += (EnglishToChanges.Ping("ExamineV") + "-" + Title + "-");
                data.height = "400px";
                data.url += ("/QiTaShenHe.aspx?" + $.param({
                    parent: 1, /*收入的枚举*/
                    type: "ExamineV",
                    OtherFeeID: typeof (Ids) == "string" ? Ids : Ids.join(','),
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    DefaultProofType: '<%=(int)EyouSoft.Model.EnumType.KingDee.DefaultProofType.杂费收入%>',
                    tourId: tourid || ""
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
                that.ExamineV(Ids, "未审核");
            },
            DelAll: function(arr) {/*删除*/
                var Ids = [], url = "/Fin/QiTaShouRu.aspx?" + $.param({
                    sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                    doType: 'del'
                });
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
                        parent.tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                    }
                });
            },
            BindBtn: function() {/*绑定功能按钮*/
                var that = this;
                $("#li_Add").click(function() {
                    var data = that.DataBoxy();
                    data.title += EnglishToChanges.Ping("Register");
                    data.url += ("/QiTaEdit.aspx?" + $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', parent: 1 }));
                    that.ShowBoxy(data);
                    return false;
                })
                $("#liststyle a[data-class='a_ExamineV']").click(function() {
                    var obj = $(this);
                    that.ExamineV(obj.closest("tr").attr("data-otherfeeid"), obj.html(), obj.closest("tr").attr("data-tourid"));
                    return false;
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
                            tableToolbar._showMsg('已审批的收入,无法修改!');
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
                            tableToolbar._showMsg('已审批的收入,无法删除!');
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
                    } }]
                })
            },
            PageInit: function() {/*初始化*/
                var that = this;
                that.BindBtn();
                var intstatus;
                $("#liststyle a[data-class='a_ExamineV']").each(function() {
                    var obj = $(this)
                    if (parseInt(obj.attr("data-intstatus")) === 0) {
                        obj.html("未审核 ");
                        obj.addClass("fontred");
                    }
//                    else {
//                    	obj.html("已审核 ");
//                    }
                })
                that.Bt($("#liststyle a[data-class='a_popo']"))


                $("#tablehead_clone").html($("#tablehead").clone(true).css("border-top", "0 none"));
            }
        }
        $(function() {
            //绑定功能按钮
            IncomeList.PageInit();
        })

       
        
    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="YeWuPeiZhi.aspx.cs" Inherits="EyouSoft.Web.Sys.YeWuPeiZhi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <!--列表表格-->
        <div class="tablelist-box">
            <div style="background: none #f6f6f6;" class="tablehead">
                <ul class="fixed">
                    <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="XiTongPeiZhi.aspx?sl=<%=SL %>">
                        <span>打印配置</span></a></li>
                    <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                        href="YeWuPeiZhi.aspx?sl=<%=SL %>"><span>业务配置</span></a></li>
                    <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="TuanHaoPeiZhi.aspx?sl=<%=SL %>">
                        <span>团号配置</span></a></li>
                </ul>
            </div>
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr>
                        <th align="center" class="th-line" colspan="3">
                            业务配置
                        </th>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            列表显示控制：
                        </td>
                        <td align="left">
                            默认显示当前时间前
                            <input type="text" class=" inputtext formsize40" id="showMonthBegin" name="showMonthBegin"
                                runat="server" />
                            月至
                            <input type="text" class="inputtext formsize40" id="showMonthEnd" name="showMonthEnd"
                                runat="server" />
                            月的数据(显示规则：默认显示当月前X个月和后X个多数据，当X=0时不做控制)
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right">
                            留位时间控制：
                        </td>
                        <td align="left">
                            <span style="font-size: 14px">
                                <input type="text" class="inputtext formsize40" id="txtLeaveHours" runat="server" />
                            </span>小时<span style="font-size: 14px">
                                <input type="text" class="inputtext formsize40" id="txtLeaveMinutes" runat="server" />
                            </span>分
                        </td>
                    </tr>
                    <%--<tr>
                        <td bgcolor="#e0e9ef" align="right">
                            提醒时间配置：
                        </td>
                        <td align="left">
                            每隔<span style="font-size: 14px">
                                <input type="text" class="inputtext formsize40" id="txtTipHours" runat="server" />
                            </span>时<span style="font-size: 14px">
                                <input type="text" class="inputtext formsize40" id="txtTipMinutes" runat="server" />
                            </span>分<span style="font-size: 14px">
                                <input type="text" class="inputtext formsize40" id="txtTipSecond" runat="server" />
                            </span>秒提醒
                        </td>
                    </tr>--%>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right" rowspan="3">
                            计划停收配置：
                        </td>
                        <td width="848" align="left">
                            <label>
                                <%--<input type="checkbox" value="否" id="chkCountry" name="chkCountry" runat="server" />--%>
                                国内线&nbsp; 提前<span style="font-size: 14px"></label>
                            <input type="text" class="inputtext formsize40" id="txtAdvanceDayforCountry" runat="server" />
                            天自动停止收客
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label>
                                <%--<input type="checkbox" value="否" id="chkProvince" name="chkProvince" runat="server" />--%>
                                省内线&nbsp; 提前<span style="font-size: 14px"></label>
                            <input type="text" class="inputtext formsize40" id="txtAdvanceDayforProvince" runat="server" />
                            天自动停止收客
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label>
                                <%--<input type="checkbox" value="否" id="chkForeign" name="chkForeign" runat="server" />--%>
                                出境线&nbsp; 提前<span style="font-size: 14px"></label>
                            <input type="text" class="inputtext formsize40" id="txtAdvanceDayforForeign" runat="server" />
                            天自动停止收客
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right" rowspan="3">
                            报账配置：
                        </td>
                        <td align="left">
                            <label>
                                <input type="checkbox" value="否" id="chkGuidBZ" name="chkGuidBZ" runat="server" />
                                跳过导游报账</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label>
                                <label>
                                    <input type="checkbox" value="否" id="chkSaleBZ" name="chkSaleBZ" runat="server" />
                                    跳过销售报账</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <label>
                                <input type="checkbox" value="否" id="chkEndBZ" name="chkEndBZ" runat="server" />
                                跳过报账终审</label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="mainbox cunline fixed">
            <div class="hr_10">
            </div>
            <ul>
                <li class="cun-cy"><a id="btnSave" href="javascript:">保存</a> </li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">

        var TourRule = {
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click");
                $("#btnSave").css("background-position", "0px 0px");
                $("#btnSave").text("保存");
                $("#btnSave").click(function() {
                    TourRule.Save();
                    return false;
                });
            },
            //提交表单
            Save: function() {
                $("#btnSave").text("提交中...");
                $("#btnSave").unbind("click");
                $("#btnSave").css("background-position", "0px -62px");
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sys/YeWuPeiZhi.aspx?dotype=save&sl=<%=SL %>",
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg);
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                        TourRule.BindBtn();
                    },
                    error: function() {
                        tableToolbar._showMsg("操作失败，请稍后再试!");
                        TourRule.BindBtn();
                    }
                });
            }
        };
        $(function() {
            TourRule.BindBtn();
        });
    </script>

</asp:Content>

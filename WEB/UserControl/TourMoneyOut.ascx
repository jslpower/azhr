<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TourMoneyOut.ascx.cs"
    Inherits="Web.UserControl.TourMoneyOut" %>
<div id="div_TourMoneyOutLoadMsg" style="width: 100%">
    <div class="tablelist-box " style="width: 98.5%">
        <span class="formtableT">团队支出</span>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td align="center">
                    加载中......
                </td>
            </tr>
        </table>
    </div>
</div>
<div id="div_TourMoneyOut" style="width: 100%; display: none">
</div>
<div id="div_boxy" style="display: none">
    <div class="alertbox-outbox03" style="padding-bottom: 0px;">
        <table align="center" cellspacing="0" cellpadding="0" border="0" width="400px">
            <tbody>
                <tr>
                    <td align="right" width="25%">
                        <span class="alertboxTableT"><span data-class="sp_Title"></span>人数：</span>
                    </td>
                    <td align="left" width="75%">
                        <input id="txt_peopleNumber" type="text" class="formsize80 bk">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span data-class="sp_Title"></span>费用：
                    </td>
                    <td align="left">
                        <input type="hidden" id="hd_isIncrease" name="isIncrease" />
                        <input type="text" id="txt_changeCost" class="formsize80 bk">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        备注：
                    </td>
                    <td align="left">
                        <textarea id="txt_remark" style="width: 300px; height: 50px;" class="bk" cols="35"
                            name="textarea2"></textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="alertbox-btn" style="position: static">
            <a hidefocus="true" id="a_Yes" href="javascript:void(0);"><s class="baochun"></s>确 定</a><a
                id="a_hide" hidefocus="true" href="javascript:void(0);"><s class="chongzhi"></s>关
                闭</a></div>
    </div>
</div>

<script type="text/javascript">
    var ControlTourMoneyOut = {
        OpstData: null, /*保存,修改,提交数据对象*/
        Check: function(tr) {/*验证*/
            var errmsg = "";
            var errorObj;
            tr.find("input[valid='required']").each(function() {
                var obj = $(this);
                if (obj.val().length <= 0) {
                    if (!errorObj) {
                        errorObj = obj;
                    }
                    errmsg += obj.attr("errmsg") + "<br/>";
                }
            })
            tr.find("input[valid='int']").each(function() {
                var obj = $(this);
                if (obj.val().length <= 0 || !RegExps.RegInteger.test(obj.val())) {
                    if (!errorObj) {
                        errorObj = obj;
                    }
                    errmsg += obj.attr("errmsg") + "<br/>";
                }
            })
            tr.find("input[valid='decimal']").each(function() {
                var obj = $(this);
                if (obj.val().length <= 0 || !RegExps.isMoney.test(obj.val())) {
                    if (!errorObj) {
                        errorObj = obj;
                    }
                    errmsg += obj.attr("errmsg") + "<br/>";
                }
            })
            if (errmsg.length > 0) {
                errorObj.focus();
                tableToolbar._showMsg(errmsg);
                return false;
            }
            else {
                return true;
            }
        },
        BindBtn: function() {/*绑定按钮*/
            var that = this;
            var tr = null;
            var ajaxURL = ""; /*保存,修改,删除AjaxURL*/
            //保存按钮
            var addControl = PageTourMoneyOut.AddControl();
            //修改按钮
            var updataControl = PageTourMoneyOut.UpdataControl();
            //删除按钮
            var deleteControl = PageTourMoneyOut.DeleteControl();
            if (addControl.length > 0) {/*添加按钮*/
                addControl.click(function() {
                    tr = $(this).closest("tr");
                    if (that.Check(tr)) {
                        ajaxURL = '/Ashx/TourMoneyOut.ashx?'; /*保存,修改,删除AjaxURL*/
                        ajaxURL += $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', doType: "Add" });
                        that.OpstData = tr.find("input,select").serialize() +
                        "&" + $.param({
                            type: tr.closest("table").attr("data-addtype"),
                            addStatusPlan: '<%=AddStatusPlan !=null?(int)AddStatusPlan:-1%>',
                            TourID: '<%=TourID %>',
                            ContactName: tr.attr("data-contactname") || "",
                            ContactPhone: tr.attr("data-contactphone") || "",
                            planid: tr.attr("data-planid")
                        });
                        that.Ajax(ajaxURL);
                    }
                    return false;
                })
            }
            if (updataControl.length > 0) {/*修改*/
                updataControl.click(function() {
                    tr = $(this).closest("tr");
                    if (that.Check(tr)) {
                        ajaxURL = '/Ashx/TourMoneyOut.ashx?'; /*保存,修改,删除AjaxURL*/
                        ajaxURL += $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', doType: "Updata" });
                        that.OpstData = tr.find("input,select").serialize() +
                        "&" + $.param({
                            planid: tr.attr("data-planid"),
                            type: tr.closest("table").attr("data-addtype"),
                            addStatusPlan: '<%=AddStatusPlan !=null?(int)AddStatusPlan:-1%>',
                            TourID: '<%=TourID %>',
                            ContactName: tr.attr("data-contactname") || "",
                            ContactPhone: tr.attr("data-contactphone") || ""
                        });
                        that.Ajax(ajaxURL);
                    }
                    return false;
                })
            }
            if (deleteControl.length > 0) {/*删除*/
                deleteControl.click(function() {
                    tr = $(this).closest("tr");
                    tableToolbar.ShowConfirmMsg("确定删除该计调数据?", function() {
                        ajaxURL = '/Ashx/TourMoneyOut.ashx?';
                        ajaxURL += $.param({ sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>', doType: "Del" });
                        that.OpstData = $.param({
                            planid: tr.attr("data-planid"),
                            type: tr.closest("table").attr("data-addtype"),
                            addStatusPlan: '<%=AddStatusPlan !=null?(int)AddStatusPlan:-1%>',
                            TourID: '<%=TourID %>'
                        });
                        tr.hide();
                        tr.closest("table").find("th:eq(0)").attr("rowspan", tr.closest("table").find("th:eq(0)").attr("rowspan") - 1)
                        $.ajax({ type: "post", data: that.OpstData, cache: false, url: ajaxURL, dataType: "json",
                            success: function(json) {
                                if (json) {
                                    if (json.result == 1) {
                                        tableToolbar._showMsg("删除成功!", function() {
                                            window.location.href = window.location.href;
                                        })
                                    }
                                    else {
                                        tableToolbar._showMsg("删除失败!")
                                        window.location.href = window.location.href;
                                    }
                                }
                            },
                            error: function() {
                                tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                            }
                        });
                    });
                    return false;
                });
            }
        },
        Ajax: function(ajaxURL) {
            var that = this;
            $.ajax({ type: "post", data: that.OpstData, cache: false, url: ajaxURL, dataType: "json",
                success: function(json) {
                    if (json && json.result == 1) {
                        window.tableToolbar._showMsg("保存成功!", function() {
                            window.location.href = window.location.href;
                        })
                    }
                    else {
                        window.tableToolbar._showMsg("保存失败!")
                    }
                },
                error: function() {
                    tableToolbar._showMsg(EnglishToChanges.Ping(arguments[1]));
                }
            })
            return false;
        },
        ControlInit: function() {
            var that = this;
            $("#div_TourMoneyOutLoadMsg").css("display", "");
            $.newAjax({
                type: "get",
                data: $.param({
                    tourID: '<%=TourID %>',
                    parentType: '<%=IntParentType %>',
                    isop: '<%=IsPlanChangeChange ? 1 : 2 %>',
                    ischangedaoyou: '<%=IsChangeDaoYou ? 1 : 2 %>'
                }),
                cache: false,
                url: '/CommonPage/TourMoneyOut.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
                dataType: "html",
                success: function(html) {
                    $("#div_TourMoneyOut").html(html);
                    if (PageTourMoneyOut && PageTourMoneyOut.PageInit) {
                        PageTourMoneyOut.PageInit();
                    }
                    that.BindBtn();
                    that._initBianGeng();
                    $("#div_TourMoneyOutLoadMsg").css("display", "none");
                    $("#div_TourMoneyOut").css("display", "");
                },
                error: function() {
                    $("#div_TourMoneyOut td").html("获取异常!");
                }
            })
        },
        //打开变更窗口 obj:a对象 bianGengLeiXing:daoyou,xiaoshou,jidiao,jiaJianLeiXing:jia,jian
        openBianGengWin: function(obj, bianGengLeiXing, jiaJianLeiXing) {
            var _$obj = $(obj);
            var _$tr = _$obj.closest("tr");
            var _title = "导游变更";
            var _title1 = "-增加";
            if (jiaJianLeiXing == "jian") _title1 = "-减少";
            if (bianGengLeiXing == "xiaoshou") _title = "销售变更";
            else if (bianGengLeiXing == "jidiao") _title = "计调变更";
            _title += _title1;

            var _data = { "anpaiid": _$tr.attr("data-planid"), "biangengleixing": bianGengLeiXing, "jiajianleixing": jiaJianLeiXing, "sl": '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>' };
            Boxy.iframeDialog({ iframeUrl: "/CommonPage/JiDiaoAnPaiBianGengEdit.aspx", title: _title, modal: true, width: "700px", height: "400px", data: _data });
        },
        //初始化变更按钮
        _initBianGeng: function() {
            var _self = this;
            $(".i_daoyoujia").click(function() { _self.openBianGengWin(this, "daoyou", "jia") });
            $(".i_daoyoujian").click(function() { _self.openBianGengWin(this, "daoyou", "jian") });
            $(".i_xiaoshoujia").click(function() { _self.openBianGengWin(this, "xiaoshou", "jia") });
            $(".i_xiaoshoujian").click(function() { _self.openBianGengWin(this, "xiaoshou", "jian") });
            $(".i_jidiaojia").click(function() { _self.openBianGengWin(this, "jidiao", "jia") });
            $(".i_jidiaojian").click(function() { _self.openBianGengWin(this, "jidiao", "jian") });
        }
    }
    $(function() {
        ControlTourMoneyOut.ControlInit();
    })
</script>


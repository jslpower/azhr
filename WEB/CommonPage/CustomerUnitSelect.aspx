<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerUnitSelect.aspx.cs"
    Inherits="Web.CommonPage.CustomerUnitSelect" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>客户单位-选用-</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <!--[if IE]><script src="/Js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script type="text/javascript" src="/Js/bt.min.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <script type="text/javascript" src="/Js/utilsUri.js"></script>

    <!--paopao star-->
    <style type="text/css">
        .hide
        {
            display: none;
        }
    </style>
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <asp:Panel ID="pan_titleBtn" runat="server">
            <div style="background: none; border-top: none;" class="tablehead">
                <ul class="fixed">
                    <li><s class="orderformicon"></s><a href="javascript:void(0);" hidefocus="true" class="ztorderform de-ztorderform">
                        <span>从客户单位选用</span></a></li>
                    <li><s class="orderformicon"></s><a href="javascript:void(0);" id="a_GoUseSupplier"
                        hidefocus="true" class="ztorderform"><span>从供应商选用</span></a>&nbsp;&nbsp; </li>
                </ul>
            </div>
            <div class="hr_10">
            </div>
        </asp:Panel>
        <form id="SelectFrom" action="CustomerUnitSelect.aspx" accept="get">
        <table width="99%" align="center" cellpadding="0" cellspacing="0" bgcolor="#e9f4f9"
            style="margin: 0 auto">
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    公司名称：
                </td>
                <td width="90%" align="left">
                    <input id="txt_corporationName" type="text" class="inputtext formsize140" />
                    国家：
                    <select id="selCountry" class="inputselect">
                    </select>
                    省份：
                    <select id="sel_province" class="inputselect">
                    </select>
                    城市：
                    <select id="sel_city" class="inputselect">
                    </select>
                    <input type="button" value="查询" id="submit_select" class="search-btn" style="width: 64px;
                        height: 24px; background: url(/images/cx.gif) no-repeat center center; border: 0 none;
                        margin-left: 5px;" />
                   &nbsp;&nbsp;<input type="button" value="新增客户" id="btnAddNew" class="search-btn" style="height: 24px;
                        background: url(/images/cx.gif) no-repeat center center; border: 0 none; margin-left: 5px;
                        width: 64px;" />
                </td>
            </tr>
        </table>
        </form>
        <div class="hr_10">
        </div>
        <div id="div_coun" style="margin: 0 auto; width: 99%;">
            <input type="hidden" runat="server" id="hd_tab" name="hd_tab" />
            <div id="div_tablist" style="text-align: center">
            </div>
            <div class="alertbox-btn">
                <a href="javascript:void(0);" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
        </div>
    </div>
    <div id="div_tablistMsg" style="display: none">
        <table width="99%" id="tab_list" border="0" cellspacing="0" cellpadding="0" class="noborderInput">
            <tr>
                <td style="background-color: #FFF">
                    加载中......
                </td>
            </tr>
        </table>
    </div>
    <div id="div_PoPo" style="display: none; width: 400px;">
    </div>

    <script type="text/javascript">
        var CommPageJsObj = {
            _parentWindow: null, /*父级页面Window*/
            _dataObj: {}, //交互数据集合
            _queryString: {}, //URL传递参数集合
            _setdataObj: function(data) {
                var that = this;
                that._dataObj.aid = data.aid.join(',');
                that._dataObj.id = data.id.join(',');
                that._dataObj.CustomerUnitName = data.name.join(',');
                that._dataObj.CustomerUnitId = data.id.join(',');
                that._dataObj.CustomerUnitContactId = data.contactId.join(',');
                that._dataObj.CustomerUnitContactName = data.contactName.join(',');
                that._dataObj.CustomerUnitMobilePhone = data.mobilePhone.join(',');
                that._dataObj.CustomerUnitContactPhone = data.contactPhone.join(',');
                that._dataObj.CustomerUnitDepartment = data.department.join(',');
                that._dataObj.CustomerUnitLV = data.lv.join(',');
                that._dataObj.CustomerLvPrice = data.lvprice.join(',');
                that._dataObj.ContactFax = data.fax.join(',');
                that._dataObj.CustomerUnitGuoji = data.guoji.join(',');
            },
            _getdata: function(data, obj) {
                var obj = $(obj);
                data.aid.push($.trim('<%=Request.QueryString["id"] %>'));
                data.id.push($.trim(obj.val() || obj.attr("data-value")));
                data.name.push($.trim(obj.attr("data-name") || obj.parent().text()));
                data.contactName.push($.trim(obj.attr("data-contactname")));
                data.mobilePhone.push($.trim(obj.attr("data-mobilephone")));
                data.contactPhone.push($.trim(obj.attr("data-contactphone")));
                data.contactId.push($.trim(obj.attr("data-contactid")) || "");
                data.department.push($.trim(obj.attr("data-department")) || "");
                data.lv.push($.trim(obj.attr("data-kehudengjibh")) || "");
                data.lvprice.push($.trim(obj.attr("data-lvprice")) || "");
                data.fax.push($.trim(obj.attr("data-contactfax")) || "");
                data.guoji.push($.trim(obj.attr("data-guoji")) || "");
            },
            SetReturn: function() {/*执行默认回调和自定义回调*/
                var that = this;
                var parents = that._parentWindow;
                //判断是否是用用户控件调用该页面
                if (parents[that._queryString["thisClientID"]] && parents[that._queryString["thisClientID"]]["BackFun"]) {
                    parents[that._queryString["thisClientID"]]["BackFun"](this._dataObj);
                }
                var callBackFun = this._queryString["callBackFun"];
                //判断自定义回调,若没有则返回null
                var callBackFunArr = callBackFun ? callBackFun.split('.') : null;
                //存在回调函数
                if (callBackFunArr) {
                    for (var item in callBackFunArr) {
                        if (callBackFunArr.hasOwnProperty(item)) {/*筛选掉原型链属性*/
                            parents = parents[callBackFunArr[item]];
                        }
                    }
                    parents(this._dataObj);
                }
            },
            GetData: function() {
                var that = this;
                var data = { aid: [], id: [], name: [], contactId: [], contactName: [], mobilePhone: [], contactPhone: [], department: [], lv: [], fax: [], guoji: [], lvprice: [] }
                if ($("#div_tablist :checkbox:checked").length > 0) {
                    $("#div_tablist :checkbox:checked").each(function() {
                        that._getdata(data, $(this));
                    })
                }
                else if ($("#div_tablist :radio:checked").length > 0) {
                    that._getdata(data, $("#div_tablist :radio:checked"));
                }
                that._setdataObj(data);

            },
            Save: function() {/*选择功能*/

                this.GetData();
                this.SetReturn();
                parent.Boxy.getIframeDialog(this._queryString["iframeId"]).hide();
                return false;
            },
            InitData: function() {
                var CustomerUnitVal =
                    this._parentWindow[this._queryString["thisClientID"]] &&
                    this._parentWindow[this._queryString["thisClientID"]]["GetVal"] ?
                    this._parentWindow[this._queryString["thisClientID"]]["GetVal"]() : {};
                this._dataObj.CustomerUnitName = CustomerUnitVal.CustomerUnitName || "";
                this._dataObj.CustomerUnitId = CustomerUnitVal.CustomerUnitId || Boxy.queryString("customerUnitid") || "";
                this._dataObj.type = CustomerUnitVal.type; /*供应商类型*/
                this._dataObj.isgys = '<%=Request.QueryString["isgys"]%>';
            },
            InitUseSupplier: function() {
                var that = this;
                $("#a_GoUseSupplier").click(function() {
                    var obj = $(this);
                    var url = "/CommonPage/Supplier.aspx?" + $.param({
                        Sourcetype: that._dataObj.type && that._dataObj.type.length ? that._dataObj.type : "1",
                        pIframeID: that._queryString["pIframeID"],
                        iframeId: that._queryString["iframeId"],
                        callBack: that._queryString["thisClientID"] + ".BackFun",
                        supplierid: that._dataObj["CustomerUnitId"]
                    })

                    window.location = url + "&" + $.param(Boxy.getUrlParams(["pIframeID", "iframeId"]) || {});
                    return false;
                })
                if (that._dataObj.type && that._dataObj.type.length > 0 && !that._dataObj.isgys.length > 0) {
                    $("#a_GoUseSupplier").click();
                }
            },
            InitList: function(page) {/*初始化列表*/
                var that = this;
                $("#div_tablist").html($("#div_tablistMsg").html());
                var url = "/CommonPage/AjaxCustomerUnitSelect.aspx?" + $.param({ page: page || 1 })
                $(".bt-wrapper").remove();
                $.newAjax({
                    type: "get",
                    data: {
                        currName: $("#txt_corporationName").val(), //客户单位名称
                        cId: $("#sel_city").val(), //城市ID
                        pId: $("#sel_province").val(), //省份ID
                        gId: $("#selCountry").val(), //国家ID
                        IsMultiple: $.trim(Boxy.queryString("IsMultiple")), //单选多选
                        IsApply: $.trim(Boxy.queryString("IsApply"))/*是否报名是选用*/
                    },
                    cache: false,
                    url: url,
                    dataType: "html",
                    success: function(html) {
                        $("#div_tablist").html(html);
                        that.InitContactName();
                        that.InitExporPageInfoSelect();
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });

            },
            InitExporPageInfoSelect: function() {
                var that = this;
                $("#div_page a").click(function() {
                    var obj = $(this);
                    that.InitList(obj.attr("href").split("Page=")[1]);
                    return false;
                })
                $("#div_page select").removeAttr("onchange").change(function() {
                    that.InitList($(this).find(":selected").val());
                })

            },
            InitBtn: function() {/*初始化 选项卡按钮*/
                var that = this;
                $("#submit_select").click(function() {/*查询按钮*/
                    that.InitList();
                    return false;
                })
                $("#txt_corporationName,#selCountry,#sel_province,#sel_city").keypress(function(e) {
                    if (e.keyCode == 13) {
                        that.InitList();
                        return false;
                    }
                });
                $("#a_btn").click(function() {/*初始化 选择按钮*/
                    that.Save();
                    return false;
                })

                $("#btnAddNew").click(function() {
                    parent.Boxy.iframeDialog({
                        iframeUrl: '/Crm/KeHuBianJi.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Privs.客户中心_分销商_栏目%>&callbackfun=AddCallBack&pIframeID=<%=Request.QueryString["iframeId"] %>',
                        title: "新增-分销商",
                        modal: true,
                        width: "980px",
                        height: "550px"
                    });
                });

            },
            InitSelectFrom: function() {/*初始化 查询条件*/
                //城市省份下拉绑定
                pcToobar.init({
                    comID: "<%=CurrentUserCompanyID %>",
                    gID: "#selCountry",
                    pID: "#sel_province",
                    cID: "#sel_city"
                })
            },
            InitContactName: function() { /*初始化联系人  报名选用时初始化 联系人 泡泡*/
                var that = this;
                if ((parseInt($.trim(Boxy.queryString("IsApply"))) || 0) == 1) { /*报名选用*/
                    $("#a_btn").remove();
                    var that = this;
                    $("span[data-class='sp_ContactName'] :radio[name='contact']").click(function() {
                        var data = { id: [], name: [], contactId: [], contactName: [], mobilePhone: [], contactPhone: [], department: [], lv: [], lvprice: [] }
                        that._getdata(data, $(this).closest("tr"));
                        that._setdataObj(data);
                        that.SetReturn();
                        parent.Boxy.getIframeDialog(that._queryString["iframeId"]).hide();
                        return false;
                    })
                    //阻止冒泡~
                    $("td").click(function() {
                        return false;
                    })
                    //清除客户单位的单选复选框
                    $("#div_tablist :checkbox,#div_tablist :radio:not([name='contact'])").remove();
                    $("#div_tablist td[data-class='td_popo'] a").bt({
                        contentSelector: function() {
                            return $(this).find("span[data-class='sp_ContactName']").clone(true);
                        },
                        positions: ['left', 'right', 'bottom'],
                        fill: '#FFF2B5',
                        strokeStyle: '#D59228',
                        noShadowOpts: { strokeStyle: "#D59228" },
                        spikeLength: 10,
                        spikeGirth: 15,
                        width: 400,
                        overlap: 0,
                        centerPointY: 1,
                        cornerRadius: 4,
                        shadow: true,
                        shadowColor: 'rgba(0,0,0,.5)',
                        cssStyles: { color: '#00387E', 'line-height': '180%' }
                    });
                }
            },
            Init: function() {/*初始化*/
                this._queryString = utilsUri.getUrlParams();
                this._parentWindow = this._queryString["pIframeID"] ? window.parent.Boxy.getIframeWindow(this._queryString["pIframeID"]) : parent;
                this.InitData();
                this.InitUseSupplier();
                this.InitSelectFrom();
                this.InitList();
                this.InitBtn();
            }
        }

        //新增回调
        function AddCallBack() {
            $("#submit_select").click();
        }
        $(function() {
            CommPageJsObj.Init();
        })
    </script>

</body>
</html>

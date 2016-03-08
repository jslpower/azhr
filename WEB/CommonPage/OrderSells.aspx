<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSells.aspx.cs" Inherits="Web.CommonPage.OrderSells" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <div class="alertbox-outbox02">
        <asp:Repeater ID="rep_depart" runat="server">
            <ItemTemplate>
                <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
                    style="margin: 0 auto">
                    <tbody>
                        <tr>
                            <td bgcolor="#C1E5F5" align="left" style="padding: 5px 8px;" class="alertboxTableT bumenbox">
                                <b><a href='javascript:void(0);' onclick="OrderSellsPage.SearchFun('','<%#Eval("DepartId") %>')">
                                    <%#Eval("DepartName")%></a> </b>：
                                <%#GetAllDepart(Eval("DepartId"))%>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <div class="hr_10">
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <table width="99%" cellspacing="0" cellpadding="0" class="alertboxbk1" border="0"
            style="margin: 0 auto; border-collapse: collapse;">
            <tbody>
                <tr>
                    <td height="30">
                        姓名：
                        <input type="text" id="userName" name="userName" class="inputtext formsize80" value='<%=Request.QueryString["userName"] %>' />
                        <input type="button" value="查询" style="width: 64px; height: 24px; background: url(/images/cx.gif) no-repeat center center;
                            border: 0 none; margin-left: 5px;" id="btnSearch" />
                        <%if (EyouSoft.Common.Utils.GetQueryStringValue("sModel") != "1")
                          { %>
                        &nbsp;&nbsp;已选择：<span id="spanSelectMore"></span>
                        <%} %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblList" width="100%" cellspacing="0" cellpadding="0" class="alertboxbk1"
                            border="0" bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                            <tbody>
                                <tr>
                                    <asp:Repeater ID="rptList" runat="server">
                                        <ItemTemplate>
                                            <td align="left">
                                                <label>
                                                    <%if (EyouSoft.Common.Utils.GetQueryStringValue("sModel") == "1")
                                                      { %>
                                                    <input type="radio" name="contactID" value="<%#Eval("UserId")%>" />
                                                    <%}
                                                      else
                                                      { %>
                                                    <input type="checkbox" name="contactID" value="<%#Eval("UserId")%>" />
                                                    <%} %>
                                                    <span data-deptid='<%#Eval("DeptId") %>' data-tel='<%#Eval("ContactMobile") %>' data-sex='<%#(int)Eval("ContactSex") %>'
                                                        data-deptname='<%#Eval("DeptName") %>'>
                                                        <%#Eval("ContactName")%></span>
                                                </label>
                                                <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, listCount, 4)%>
                                        </ItemTemplate>
                                    </asp:Repeater>
                            </tbody>
                        </table>
                        <div style="width: 100%; text-align: center; background-color: #ffffff">
                            <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div style="position: relative; height: 25px;">
            <div class="pages" id="divPages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <div class="alertbox-btn">
            <a id="a_btn" hidefocus="true" href="javascript:void(0);"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        var OrderSellsPage = {
            selectValue: "",
            selectTxt: "",
            deptID: "",
            deptName: "",
            Sex: "",
            Tel: "",
            aid: '<%=Request.QueryString["id"] %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetValue: function() {
                var valueArray = new Array();
                var txtArray = new Array();
                var deptIdArray = new Array();
                var deptNameArray = new Array();
                var sexarray = new Array();
                var telarray = new Array();
                if ('<%=Request.QueryString["sModel"]%>' != '1') {
                    $("#spanSelectMore").find("input[name='contactID']:checked").each(function() {
                        valueArray.push($.trim($(this).val()));
                        txtArray.push($.trim($(this).next().html()));
                        deptIdArray.push($(this).next().attr("data-deptID"));
                        deptNameArray.push($(this).next().attr("data-deptName"));
                        sexarray.push($(this).next().attr("data-sex"));
                        telarray.push($(this).next().attr("data-tel"));
                    })
                }
                else {
                    $("#tblList").find("input[name='contactID']:checked").each(function() {
                        valueArray.push($.trim($(this).val()));
                        txtArray.push($.trim($(this).next().html()));
                        deptIdArray.push($(this).next().attr("data-deptID"));
                        deptNameArray.push($(this).next().attr("data-deptName"));
                        sexarray.push($(this).next().attr("data-sex"));
                        telarray.push($(this).next().attr("data-tel"));
                    })
                }
                OrderSellsPage.selectValue = valueArray.join(',');
                OrderSellsPage.selectTxt = txtArray.join(',');
                OrderSellsPage.deptID = deptIdArray.join(',');
                OrderSellsPage.deptName = deptNameArray.join(',');
                OrderSellsPage.Sex = sexarray.join(',');
                OrderSellsPage.Tel = telarray.join(',');
            },
            InitSetSelect: function() {
                if (OrderSellsPage.aid) {
                    var parentSpan = OrderSellsPage.parentWindow.$('#' + OrderSellsPage.aid).parent();
                    var oldValue, oldText, deptId, deptName, sex, tel;
                    if (parentSpan.find("input[type='hidden']").length > 0) {
                        oldValue = parentSpan.find("input[type='hidden']").val().split(',');
                    }
                    if (parentSpan.find("input[type='text']").length > 0) {
                        oldText = parentSpan.find("input[type='text']").val().split(',');
                    }
                    if (parentSpan.find("span[data-class='hideDeptInfo']").length > 0 && parentSpan.find("span[data-class='hideDeptInfo']").attr("data-deptid").length > 0) {
                        deptId = OrderSellsPage.parentWindow.$("#" + parentSpan.find("span[data-class='hideDeptInfo']").attr("data-deptid")).val().split(',');
                    }
                    if (parentSpan.find("span[data-class='hideDeptInfo']").length > 0 && parentSpan.find("span[data-class='hideDeptInfo']").attr("data-deptName").length > 0) {
                        deptName = OrderSellsPage.parentWindow.$("#" + parentSpan.find("span[data-class='hideDeptInfo']").attr("data-deptName")).val().split(',');
                    }
                    if (parentSpan.find("span[data-class='hideDeptInfo']").attr("data-sex").length > 0) {
                        sex = OrderSellsPage.parentWindow.$("#" + parentSpan.find("span[data-class='hideDeptInfo']").attr("data-sex")).val().split(',');
                    }
                    if (parentSpan.find("span[data-class='hideDeptInfo']").attr("data-tel").length > 0) {
                        tel = OrderSellsPage.parentWindow.$("#" + parentSpan.find("span[data-class='hideDeptInfo']").attr("data-tel")).val().split(',');
                    }


                    var sellsData = OrderSellsPage.parentWindow["sellsData"];
                    if (sellsData && sellsData.a.length > 0 && '<%=Request.QueryString["sModel"]%>' != '1') {
                        this.AppendCheckbox(sellsData.a, sellsData.b, sellsData.c, sellsData.d, sellsData.e, sellsData.f);
                        return false;
                    }
                    if (oldValue && oldText) {
                        this.AppendCheckbox(oldValue, oldText, deptId, deptName, sex, tel);
                    }


                }
            },
            AppendCheckbox: function(a, b, c, d, e, f) {
                var str = "";
                var sellsData = { a: [], b: [], c: [], d: [], e: [], f: [] };
                if (a.length > 0 && a.length == b.length) {
                    for (var i = 0; i < a.length; i++) {
                        if (a[i] != "") {
                            sellsData.a.push(a[i]);
                            sellsData.b.push(b[i]);
                            $("input[name='contactID'][value='" + a[i] + "']").attr("checked", "checked");
                            if ('<%=Request.QueryString["sModel"]%>' != '1') {
                                if (c && d && c.length >= i && d.length >= i) {
                                    sellsData.c.push(c[i]);
                                    sellsData.d.push(d[i]);
                                    sellsData.e.push(e[i]);
                                    sellsData.f.push(f[i]);
                                    str += '<label><input checked="checked" type="checkbox" value="' + a[i] + '" name="contactID"><span data-tel="' + f[i] + '" data-sex="' + e[i] + '" data-deptname="' + d[i] + '" data-deptid="' + c[i] + '">' + b[i] + '</span>&nbsp;&nbsp;</label>';
                                } else {
                                    str += '<label><input checked="checked" type="checkbox" value="' + a[i] + '" name="contactID"><span data-tel="" data-deptname="" data-sex="" data-deptid="">' + b[i] + '</span>&nbsp;&nbsp;</label>';
                                    sellsData.c.push("");
                                    sellsData.d.push("");
                                    sellsData.e.push("");
                                    sellsData.f.push("");
                                }
                            }
                        }
                    }
                    OrderSellsPage.parentWindow["sellsData"] = sellsData;
                }
                if (str != "") {
                    $("#spanSelectMore").append(str);
                    this.BindCheckBoxClick();
                }
            },
            RemoveCheckbox: function(val) {
                var sellsData = OrderSellsPage.parentWindow["sellsData"]
                if (sellsData) {
                    for (var i = 0; i < sellsData.a.length; i++) {
                        if (sellsData.a[i] == val) {
                            sellsData.a.splice(i, 1);
                            sellsData.b.splice(i, 1);
                            sellsData.c.splice(i, 1);
                            sellsData.d.splice(i, 1);
                            sellsData.e.splice(i, 1);
                            sellsData.f.splice(i, 1);
                        }
                    }
                    OrderSellsPage.parentWindow["sellsData"] = sellsData;
                }
                $("#spanSelectMore").find("[value='" + val + "']").parent().remove();
            },
            BindCheckBoxClick: function() {
                $("#spanSelectMore").find("input[type='checkbox']").unbind("click").click(function() {
                    if (this.value != "") {
                        $("input[name='contactID'][value='" + this.value + "']").attr("checked", "");
                        OrderSellsPage.RemoveCheckbox(this.value);
                    }
                    $(this).parent().remove();
                })
            },
            SearchFun: function(key, deptId) {
                var data = { tel: '<%=Request.QueryString["tel"] %>', sex: '<%=Request.QueryString["sex"] %>', id: '<%=Request.QueryString["id"] %>', iframeId: '<%=Request.QueryString["iframeId"]%>', pIframeId: '<%=Request.QueryString["pIframeId"]%>', callBackFun: '<%=Request.QueryString["callBackFun"] %>', sModel: '<%=Request.QueryString["sModel"]%>', userName: '', deptId: '' };
                data.userName = key;
                data.deptId = deptId;
                window.location.href = "/CommonPage/OrderSells.aspx?" + $.param(data);
            },
            BtnBind: function() {
                $("#a_btn").click(function() {
                    OrderSellsPage.SetValue();

                    var data = { tel: OrderSellsPage.Tel, sex: OrderSellsPage.Sex, id: '<%=Request.QueryString["id"] %>', value: OrderSellsPage.selectValue, text: OrderSellsPage.selectTxt, hide: '<%=Request.QueryString["hide"] %>', show: '<%=Request.QueryString["show"] %>', deptID: OrderSellsPage.deptID, deptName: OrderSellsPage.deptName };
                    //根据父级是否为弹窗传值
                    var func = '<%=Request.QueryString["callBackFun"] %>';
                    if (func.indexOf('.') == -1) {
                        OrderSellsPage.parentWindow[func](data);
                    } else {
                        OrderSellsPage.parentWindow[func.split('.')[0]][func.split('.')[1]](data);
                    }
                    parent.Boxy.getIframeDialog(OrderSellsPage.iframeID).hide();
                    return false;
                })

                $("#btnSearch").click(function() {
                    OrderSellsPage.SearchFun($("#userName").val(), "");
                })

                //回车事件
                $("input[type='text'][name='userName']").bind("keypress", function(e) {
                    if (e.keyCode == 13) {
                        OrderSellsPage.SearchFun($("#userName").val(), "");
                        return false;
                    }
                });
            }
        }

        $(function() {
            //获得需要赋值页面的window 对象
            if (OrderSellsPage.pIframeID) {
                OrderSellsPage.parentWindow = window.parent.Boxy.getIframeWindow(OrderSellsPage.pIframeID) || window.parent.Boxy.getIframeWindowByID(OrderSellsPage.pIframeID);
            }
            else {
                OrderSellsPage.parentWindow = parent.window;
            }
            //初始化设置选中
            OrderSellsPage.InitSetSelect();
            //初始化绑定事件
            OrderSellsPage.BtnBind();
            //如果是多选
            if ('<%=Request.QueryString["sModel"]%>' != '1') {
                $("input[name='contactID']").click(function() {
                    var sellsData = OrderSellsPage.parentWindow["sellsData"]

                    if (this.checked) {
                        var a = new Array(), b = new Array(), c = new Array(), d = new Array(), e = new Array(), f = new Array();
                        a.push($.trim($(this).val()));
                        b.push($.trim($(this).next().html()));
                        c.push($(this).next().attr("data-deptID"));
                        d.push($(this).next().attr("data-deptName"));
                        e.push($(this).next().attr("data-sex"));
                        f.push($(this).next().attr("data-tel"));
                        if (sellsData) {
                            sellsData.a.push($.trim($(this).val()));
                            sellsData.b.push($.trim($(this).next().html()));
                            sellsData.c.push($(this).next().attr("data-deptID"));
                            sellsData.d.push($(this).next().attr("data-deptName"));
                            sellsData.e.push($(this).next().attr("data-sex"));
                            sellsData.f.push($(this).next().attr("data-tel"));
                        }
                        OrderSellsPage.AppendCheckbox(a, b, c, d, e,f);
                        OrderSellsPage.parentWindow["sellsData"] = sellsData;
                    } else {
                        var _s = this;
                        OrderSellsPage.RemoveCheckbox(_s.value);
                    }
                })
            }
        }) 
    </script>

</body>
</html>

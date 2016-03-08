<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SinglePrint.aspx.cs" Inherits="EyouSoft.Web.PrintPage.SinglePrint" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/Css/print.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <style type="text/css">
        .PrintPreview
        {
            display: none;
        }
        #divImgCachet
        {
            width: 120px;
            height: 120px;
            overflow: hidden;
            text-align: center;
            line-height: 120px;
        }
        .input120
        {
            border-style: none;
        }
        .style1
        {
            width: 140px;
        }
        .style2
        {
            width: 350px;
        }
        .style3
        {
            width: 92px;
        }
        .style4
        {
            border: #000 solid 1px;
            width: 121px;
        }
        .style5
        {
            width: 121px;
        }
        .style7
        {
            border: #000 solid 1px;
        }
        .style8
        {
            height: 20px;
            width: 258px;
        }
        .style9
        {
            width: 219px;
        }
        .style10
        {
            width: 258px;
        }
        .style11
        {
            height: 9px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    <table id="tab_Operate" width="696" cellspacing="0" cellpadding="0" border="0" align="center">
        <tbody>
            <tr>
                <td align="left">
                    <%--  <input id="chk_HideHeader" name="chk_HideHeader" type="checkbox" onclick="PrintMaster.HiddenImgCachet(this,'divHeader');" /><label
                        for="chk_HideHeader" style="cursor: pointer;">隐藏页眉</label>
                    <input id="chk_HideFooter" name="chk_HideFooter" type="checkbox" onclick="PrintMaster.HiddenImgCachet(this,'divFooter');" /><label
                        for="chk_HideFooter" style="cursor: pointer;">隐藏页脚</label>
                    <input id="chk_hideTable" name="chk_HideFooter" type="checkbox" onclick="PrintMaster.HiddenTable();" /><label
                        for="chk_hideTable" style="cursor: pointer;">隐藏表格</label>--%>
                    <%--<input id="chk_HideCachet" name="chk_HideCachet" type="checkbox" onclick="PrintMaster.HiddenImgCachet(this,'divImgCachet');" /><label
                        for="chk_HideCachet" style="cursor: pointer;">盖章</label>&nbsp;--%>
                    <%-- <input id="chkHideTo" style="cursor: pointer;" type="checkbox" onclick="PrintMaster.hideTo(this)" /><label
                        for="chkHideTo">隐藏TO</label>--%>
                </td>
                <td width="350px" align="right">
                    行间距：<a href="javascript:void(0)" onclick="PrintMaster.SetLineHeight(true)">+加</a>
                    <a href="javascript:void(0)" onclick="PrintMaster.SetLineHeight(false)">-减</a>
                    字体：<a href="javascript:void(0)" onclick="PrintMaster.SetFontSize(true)">+加</a> <a
                        href="javascript:void(0)" onclick="PrintMaster.SetFontSize(false)">-减</a>&nbsp;&nbsp;
                    <asp:ImageButton ID="ibtnPrintPage" runat="server" ImageUrl="/images/dayin1-cy.gif"
                        Width="57" Height="19" CssClass="hand" OnClientClick="PrintMaster.PrintPage();return false;" />&nbsp;&nbsp;
                         <asp:ImageButton ID="ToWord" OnClientClick="todocx();return false;" runat="server"
                        ImageUrl="/images/daochu-cy.gif" Width="57" Height="19" CssClass="hand" />&nbsp;&nbsp;
                </td>
            </tr>
        </tbody>
    </table>
    <%--         **************************************表格开始*********************************************************** --%>
    <div id="divContent">
        <table border="0" align="center" cellpadding="0" cellspacing="0" width="696">
            <tr>
                <td align="center">
                    <img src="../images/ym1.gif"/>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list2">
            <tr>
                <td height="40" align="right" style="font-size: 16px; font-weight: bold;width:15%">
                    Invoice #
                </td>
                <td align="center" class="font14" style="width:30%">
                    <asp:Label runat="server" ID="lblOrderCode"></asp:Label>
                </td>
                <th align="right" class="font14" style="width:25%">
                    CUSTOMER ID：
                </th>
                <td align="left" class="font14" style="width:10%">
                    <asp:Label ID="lblcustomername" runat="server" Text=""></asp:Label>
                </td>
                <th align="right" class="font14" style="width:8%">
                    Date：
                </th>
                <td align="center" class="font14" style="width:12%">
                    <asp:Label runat="server" ID="lblLDate"></asp:Label>
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="33" align="left" style="color: #f00;">
                    Bank Account [银行帐号]： Commonwealth Bank， Address: 431-439 Sussex St.Haymarket NSW
                    2000. Australia
                </td>
            </tr>
            <tr>
                <td height="33" align="center">
                    BIC/ SWIFT code: CTBAAU2S | BSB No. 062 010 | Account No.: 10109908 | Account Name:
                    JJ Travel Service
                </td>
            </tr>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0" class="list2">
            <tr>
                <th height="30" colspan="7" align="left" style="background: #993300; color: #fff;">
                    Bill To：
                    <%--<input type="text" id="CompanyName" runat="server" style="background: #993300; color: #fff; width:300px"/>--%>
                </th>
            </tr>
            <tr>
                <td height="30" colspan="7" align="left">
                    [Client Name/Company Name] ：<input type="text" id="CompanyName2" runat="server" style=" width:300px" />
                </td>
            </tr>
            <tr>
                <th height="30" align="center">
                    Quantity<br />
                    数量
                </th>
                <th align="center">
                    Type<br />种类
                </th>
                <th align="center">
                    Description<br />
                    内容
                </th>
                <th align="center">
                    Date
                    <br />
                    日期
                </th>
                <th align="center">
                    Unit price
                    AUD $<br />单价(澳元)
                </th>
                <th align="center">
                    Amount
                    AUD $<br />合计(澳元)
                </th>
                <th align="center">
                    10% GST applied<br />消费税
                </th>
            </tr>
            <%=strbu %>
        </table>
        <table width="696" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="120" rowspan="6" align="left" style="color: #333399; font-size: 15px;
                    font-family: Arial;">
                    <table width="98%" border="0" align="left" cellpadding="0" cellspacing="0" style="border: #ccc solid 1px;
                        background: #e8ecea;" class="paddl">
                        <tr>
                            <td height="25" align="left" class="style2">
                                Thank You For Your Business! 谢謝您的惠顾!<br />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="left" style="color: #f00; font-weight: bold;" 
                                class="style2">
                                我已仔細閱讀並接受如下有關訂購細則:<br />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="left" class="style2">
                                I confirm &amp; understand the term &amp; condition of purchase / ticket / tour
                                &amp; rules, and been informed all the details;<br />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="left" class="style2">
                                Prices and taxes are correct at time of purchase and are subject to change without
                                notice;
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="left" class="style2">
                                Payments made by credit card will incur a surcharge;<br />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="left" class="style2">
                                Additional levies, government charges and other applicable fees may apply and are
                                beyond our control;
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td height="25" align="left" class="style2">
                                Deposit payment is non-refundable, only valid to 3 months, Fail for full payment
                                will lost deposit.
                            </td>
                        </tr>
                    </table>
                </td>
                <th height="30" align="right" class="style3">
                    Paid Amount&nbsp;
                </th>
                <td width="120" align="left" class="td_border">
                    <asp:Label ID="lblPaid" runat="server" Text=""></asp:Label>
                </td>
                <td align="center" class="style4">
                    Credit Card / CASH
                </td>
            </tr>
            <tr>
                <th height="30" align="right" class="style3">
                    Balance due&nbsp;
                </th>
                <td align="left" class="td_border">
                    <input type="text" value="" id="Text2" class="input100">
                </td>
                <td rowspan="2" align="center" valign="top" class="style5">
                    <img src="../images/p9.jpg" width="91" height="90" />
                </td>
            </tr>
            <tr>
                <th align="right" class="style3">
                    Due Date&nbsp;
                </th>
                <td align="left" class="style7">
                    <input type="text" value="" id="txtDate" class="input100" runat="server"/>
                </td>
            </tr>
  <tr>
    <td height="4" colspan="3" align="left"></td>
  </tr>
  <tr>
    <td colspan="3" align="left" class="qianming">Name / 簽發人：<br />Signature/签名：</td>
  </tr>
        </table>
    </div>
    <%--         **************************************表格结束*********************************************************** --%>
    <input id="hideFontSize" type="hidden" value="12" />
        <input id="hideLineHeight" type="hidden" value="26" />

     

    <script type="text/javascript">
        var PrintMaster = {
            HiddenImgCachet: function(Obj, DivID) {//隐藏盖章
                if (DivID == "divImgCachet") {
                    if ($(Obj).attr("checked")) {
                        if ($("#" + DivID).length > 0) $("#" + DivID).html($("#divImgZhang").html());
                    } else {
                        if ($("#" + DivID).length > 0) $("#" + DivID).html("峡州签章");
                    }
                } else {
                    if ($(Obj).attr("checked")) $("#" + DivID).hide();
                    else $("#" + DivID).show();
                }
            },
            PrintPage: function() {//打印
                if (window.print != null) {
                    if (window["PrevFun"] != null) window["PrevFun"]();

                    $("#tab_Operate").hide();
                    $(".unprint").hide();
                    if ($("#chk_hideTable").attr("checked")) PrintMaster.SetTableClass("3");
                    window.print();

                    //还原页面内容
                    window.setTimeout(function() {
                        $("#tab_Operate").show();
                        if ($("#chk_hideTable").attr("checked")) {
                            PrintMaster.SetTableClass("2");
                        }
                        $(".unprint").show();
                    }, 1000);

                } else {
                    alert("没有安装打印机");
                }
            },
            AdaptiveHeight: function(tableId) {
                if (tableId === undefined || typeof (tableId) != "string") {
                    alert("请使用有效的参数");
                    return;
                }

                var oTable = $("#" + tableId);
                if (oTable.length == 0) return;

                var oParent = oTable.parent("td").parent("tr");
                if (oParent.length == 0) return;

                var parentHeight = oParent.height();
                var oTableHeight = oTable.height();
                if (parentHeight > oTableHeight) oTable.height(parentHeight + 10);
            },
            config: {
                minFontSize: 10,
                maxFontSize: 20,
                minLineHeight: 12,
                maxLineHeight: 30
            },
            SetFontSize: function(isIncrease) {
                var currentFontSize = parseInt($("#hideFontSize").val());
                var toFontSize = isIncrease ? currentFontSize + 1 : currentFontSize - 1;

                if (!isIncrease && toFontSize < this.config.minFontSize) {
                    alert("已调整至最小字体" + this.config.minFontSize + "像素");
                    return;
                }

                if (isIncrease && toFontSize > this.config.maxFontSize) {
                    alert("已调整至最大字体" + this.config.maxFontSize + "像素");
                    return;
                }

                $("#divContent td").css({ 'font-size': toFontSize + 'px' });
                $("#hideFontSize").val(toFontSize);
            },
            SetLineHeight: function(isIncrease) {
                var lineHeight = parseInt($("#hideLineHeight").val());
                var toLineHeight = isIncrease ? lineHeight + 1 : lineHeight - 1;

                if (!isIncrease && toLineHeight < this.config.minLineHeight) {
                    alert("最小行间距" + this.config.minLineHeight + "像素");
                    return;
                }

                if (isIncrease && toLineHeight > this.config.maxLineHeight) {
                    alert("最大行间距" + this.config.maxLineHeight + "像素");
                    return;
                }

                $("body").css({ 'line-height': toLineHeight + 'px' });
                $("#hideLineHeight").val(toLineHeight);
            },
            HiddenTable: function() {
                if ($("#chk_hideTable").attr("checked")) PrintMaster.SetTableClass("3");
                else PrintMaster.SetTableClass("2");
            },
            SetTableClass: function(type) {
                $("#divContent").find("table").each(function() {
                    var _self = $(this);
                    if (_self.attr("class") != "") {
                        var className = _self.attr("class").split('_')[0] + "_" + type;
                        _self.attr("class", className + " inputbot");
                    }
                });
            },
            //chk控制打印，[chk in table]，chk单独一行，chkExpr:chk选择器，shExpr:要控制打印内容的选择器
            bindChkClick: function(chkExpr, shExpr) {
                $(chkExpr).bind("click", function() {
                    var _$chkTable = $(chkExpr).closest("table");
                    if ($(this).attr("checked")) {
                        _$chkTable.removeClass("unprint")
                        $(shExpr).show();
                    }
                    else {
                        _$chkTable.addClass("unprint");
                        $(shExpr).hide();
                    }
                });
            },
            //chk控制打印，[chk in tr]，chk与内容一行，chkExpr:chk选择器
            bindChkClickTr: function(chkExpr) {
                $(chkExpr).bind("click", function() {
                    var _$chkTr = $(chkExpr).closest("tr");
                    if ($(this).attr("checked")) {
                        _$chkTr.removeClass("unprint")
                    }
                    else {
                        _$chkTr.addClass("unprint");
                    }
                });
            },
            hideTo: function(obj) {
                if (obj.checked) $("#i_div_to").hide();
                else $("#i_div_to").show();
            }
        };
        function ReplaceInput() {
            if (window["PrevFun"] != null) window["PrevFun"]();

            var _$printdiv = $("#printNone");
            _$printdiv.html($("#divAllHtml").html());
            _$printdiv.find("div[ref='noprint'],[type='hidden']").replaceWith("");
            _$printdiv.find("input[type='checkbox']").replaceWith("");
            _$printdiv.find("input,textarea").each(function() {
                var values = $(this).val().replace(/\n/g, "<br/>");
                $(this).before("<span class='input " + $(this).attr("class") + "'>" + values + "</span>");
                $(this).replaceWith("");
            });
            _$printdiv.find(".unprint").replaceWith("");
            _$printdiv.find(".undaochu").replaceWith("");

            $("#hidPrintHTML").val(_$printdiv.html());

            return true;
        }

        //语言选择
        $(function() {
            $("#ddlLngType").change(function() {
                var url = window.location.href;

                var newurl = url.substring(0, url.indexOf("?"));
                var iBegin = url.indexOf("?");
                var sQuery = url.substring(iBegin + 1);
                var aQuery = sQuery.split("&");
                for (var i = 0; i < aQuery.length; i++) {
                    var k = aQuery[i].indexOf("=");
                    if (k == -1) continue;
                    var key = aQuery[i].substring(0, k);
                    var value = aQuery[i].substring(k + 1);
                    if (key == "LngType") {
                        if (i == 0) {
                            newurl += "?" + key + "=" + $(this).val();
                        }
                        else {
                            newurl += "&" + key + "=" + $(this).val();
                        }
                    }
                    else {
                        if (i == 0) {
                            newurl += "?" + key + "=" + value;
                        }
                        else {
                            newurl += "&" + key + "=" + value;
                        }
                    }
                }

                window.location.href = newurl;
            });
        });
    </script>
     <script type="text/javascript">
         function todocx() {
             var _html = $("#divContent").html();
             $("#i_div_html").html(_html);
             $("#i_div_html").find("img").each(function() {
                 var s = $(this).attr("src").replace("..", "");
                 var hostname = window.location.host;
                 $(this).attr("src", "http://" + hostname + s);
             })
            $("#i_div_html").find("input,textarea").each(function() {
                var values = $(this).val().replace(/\n/g, "<br/>");
                $(this).before("<span class='input " + $(this).attr("class") + "'>" + values + "</span>");
                $(this).replaceWith("");
            });
             $("#txt_docx_html").val($("#i_div_html").html());
             $("#i_form_docx").submit();
             return false;
         }        
        
    </script>

    </form>
    
      <form id="i_form_docx" style="display: none" method="post">
    <input type="text" name="txt_isdocx" id="txt_isdocx" value="1" />
    <textarea name="txt_docx_html" id="txt_docx_html"></textarea>
    <div id="i_div_html">
    </div>
    </form>
</body>
</html>

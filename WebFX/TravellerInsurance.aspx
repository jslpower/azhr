<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TravellerInsurance.aspx.cs"
    Inherits="EyouSoft.WebFX.TravellerInsurance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type" />
    <title>游客保险信息</title>
    <link type="text/css" rel="stylesheet" href="/Css/style.css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"></script>

    <script type="text/javascript" src="/Js/table-toolbar.js"></script>

    <style type="text/css">
        .bumenbox
        {
            line-height: 200%;
        }
        .bumenbox a
        {
            color: #000000;
        }
        .bumenbox a:hover
        {
            color: #FF0000;
        }
    </style>
</head>
<body style="background: 0 none;">
    <form id="Form1" runat="server">
    <asp:HiddenField runat="server" ID="TravellerId" />
    <div class="alertbox-outbox02">
        <table width="99%" border="1" cellpadding="0" cellspacing="0" bordercolor="#85C1DD"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td align="center">
                    <span style="font-size: 14px;">【购买保险操作】</span>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="99%" border="0" cellpadding="0" cellspacing="1" bgcolor="#FFFFFF" style="margin: 0 auto;">
                        <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                            <td height="23" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                是否购买
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                所有游客批量购买此保险
                            </td>
                            <td align="left" bgcolor="#B7E0F3" class="alertboxTableT">
                                保险名称
                            </td>
                            <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                                单价
                            </td>
                            <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                                购买份数
                            </td>
                            <td align="right" bgcolor="#B7E0F3" class="alertboxTableT">
                                小计金额
                            </td>
                        </tr>
                        <asp:Repeater ID="RpInsurance" runat="server">
                            <ItemTemplate>
                                <tr data-id="<%#Eval("InsuranceId") %>">
                                    <td height="28" align="center" bgcolor="#E9F4F9" class="bottomline">
                                        <input type="checkbox" name="checkbox" id="checkbox1" />
                                    </td>
                                    <td align="center" bgcolor="#E9F4F9" class="bottomline">
                                        <input type="checkbox" name="checkbox2" id="checkbox2" />
                                    </td>
                                    <td align="left" bgcolor="#E9F4F9" class="bottomline">
                                        <%#Eval("InsuranceName")%>
                                    </td>
                                    <td align="right" bgcolor="#E9F4F9" class="bottomline">
                                        <input id="txtUnitPrice" type="text" class="formsize80 bk" value="<%#Eval("UnitPrice") %>">
                                    </td>
                                    <td align="right" bgcolor="#E9F4F9" class="bottomline">
                                        <input id="txtBuyNum" type="text" class="formsize40 bk" value="0">
                                    </td>
                                    <td align="right" bgcolor="#E9F4F9" class="bottomline">
                                        <input id="txtSumUnitPrice" type="text" class="formsize80 bk" value="0">
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr>
                            <td height="28" bgcolor="#E9F4F9" align="right" class="bottomline" colspan="5">
                                <strong>合计金额</strong>
                            </td>
                            <td bgcolor="#E9F4F9" align="right" class="bottomline">
                                <input id="txtSumPrice" type="text" class="formsize80 bk" name="text" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>

    <script type="text/javascript">
        $(function() {
            var id = $("#TravellerId").val();
            if (id) {
                var data = {
                    Type: "Do",
                    TravellerId: id
                };
                $.newAjax({
                    url: "TravellerInsurance.aspx?" + $.param(data),
                    type: "post",
                    dataType: "json",
                    success: function(back) {
                        if (back) {
                            var sum = 0;
                            $.each(back, function(i) {
                                $("#txtSumPrice").val(back[i].SumPrice);
                                var InsuranceId = back[i].InsuranceId;
                                $("input[name='checkbox']").each(function() {
                                    if ($(this).closest("tr").attr("data-id") == InsuranceId) {
                                        $(this).attr("checked", "checked");
                                        var tr = $(this).closest("tr");
                                        tr.find("#txtUnitPrice").val(back[i].UnitPrice);
                                        tr.find("#txtBuyNum").val(back[i].BuyNum);
                                        var sumUnitPrice = back[i].UnitPrice * back[i].BuyNum;
                                        tr.find("#txtSumUnitPrice").val(sumUnitPrice);
                                        sum += sumUnitPrice;
                                    }

                                });
                            });
                            $("#txtSumPrice").val(sum);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg("服务器忙！");
                    }
                });
            }
        });
    </script>

</body>
</html>

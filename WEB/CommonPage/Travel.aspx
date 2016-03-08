<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Travel.aspx.cs" Inherits="EyouSoft.Web.CommonPage.Travel" %>

<%@ Register Src="~/UserControl/SelectJourney.ascx" TagName="SelectJourney" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectJourneySpot.ascx" TagName="SelectJourneySpot"
    TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/SelectPriceRemark.ascx" TagName="SelectPriceRemark"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="Stylesheet" />
    <link href="/Css/boxynew.css" rel="Stylesheet" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/jquery.boxy.js"></script>

    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"></script><![endif]-->

    <script type="text/javascript" src="/Js/jquery.blockUI.js"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

</head>
<body>
    <br />
    <form runat="server" id="form1">
    <input type="hidden" name="sl" value="1" />
    <table>
        <tr>
            <th align="right">
                行程备注：<uc1:SelectJourney runat="server" ID="SelectJourney1" />
            </th>
            <td align="left">
                <span id="spanJourney" style="display: inline-block;">
                    <asp:TextBox ID="txtJourney" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                </span>
            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td class="addtableT">
                行程亮点：<uc1:SelectJourneySpot runat="server" ID="SelectJourneySpot1" />
            </td>
            <td class="kuang2" colspan="5">
                <span id="spanPlanContent" style="display: inline-block;">
                    <asp:TextBox ID="txtPlanContent" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                </span>
            </td>
        </tr>
    </table>
    <br />
    <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr>
                <th align="right">
                    报价备注：<uc1:SelectPriceRemark runat="server" ID="SelectPriceRemark1" />
                </th>
                <td align="left">
                    <span id="spanPriceRemark" style="display: inline-block;">
                        <asp:TextBox ID="txtPriceRemark" runat="server" TextMode="MultiLine" CssClass="inputtext formsize800"></asp:TextBox>
                    </span>
                </td>
            </tr>
        </tbody>
    </table>
    <div class="mainbox cunline fixed">
        <ul>
            <li class="cun-cy"><a href="javascript:void(0)" id="i_a_submit">保存</a></li>
        </ul>
        <div class="hr_10">
        </div>
    </div>
    </form>

    <script type="text/javascript">
        $(function() {
            $("#i_a_submit").click(function() {
                $.ajax({
                    type: "POST", url: window.location.href + "&doType=submit", data: $("#<%=form1.ClientID %>").serialize(), cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response == "1") {
                            window.location.href = "/CommonPage/Travel.aspx?sl=1";
                        } else {
                            alert(0);
                        }
                    },
                    error: function() {
                        alert("请求异常");

                    }
                });
            });
        });
    </script>

</body>
</html>

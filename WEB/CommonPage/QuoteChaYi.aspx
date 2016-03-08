<%@ Page Title="差异比较" Language="C#" AutoEventWireup="true" CodeBehind="QuoteChaYi.aspx.cs"
    Inherits="EyouSoft.Web.CommonPage.QuoteChaYi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <form runat="server">
    <div class="alertbox-outbox">
        <div style="text-align: center;">
            <asp:DropDownList runat="server" ID="ddlFrist">
            </asp:DropDownList>
            与
            <asp:DropDownList runat="server" ID="ddlSecond">
            </asp:DropDownList>
            <a id="btn_save" class="box_searchbtn" href="javascript:void(0);">查询</a>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        $(function() {
            $("#btn_save").click(function() {
                var FristQuoteId = $("#<%=this.ddlFrist.ClientID %>").val();
                var SecondQuoteId = $("#<%=this.ddlSecond.ClientID %>").val();

                if (FristQuoteId == SecondQuoteId) {
                    parent.tableToolbar._showMsg("请选择二种不同的报价");
                }
                else {
                    var FristCount = $("#<%=this.ddlFrist.ClientID %>").find("option:selected").text();
                    var SecondCount = $("#<%=this.ddlSecond.ClientID %>").find("option:selected").text();

                    var sl = '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>';

                    var url = "/Quote/BiJiao.aspx?sl=" + sl + "&FristQuoteId=" + FristQuoteId + "&SecondQuoteId=" + SecondQuoteId + "&FristCount=" + encodeURI(FristCount) + "&SecondCount=" + encodeURI(SecondCount);

                    window.parent.open(url, "", "", "");
                    return false;
                }
            });
        });
</script>

</body>
</html>

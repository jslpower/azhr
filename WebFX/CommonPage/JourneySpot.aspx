<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JourneySpot.aspx.cs" Inherits="EyouSoft.WebFX.CommonPage.JourneySpot" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox02">
        <table width="99%" cellspacing="0" cellpadding="0" border="0" style="margin: 0 auto;"
            id="tableData">
            <tbody>
                <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
                    <td width="12%" height="23" bgcolor="#B7E0F3" align="left" class="alertboxTableT">
                        <%=(String)GetGlobalResourceObject("string", "序号")%>
                    </td>
                    <td width="88%" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                        <%=(String)GetGlobalResourceObject("string", "内容")%>
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rpJourneySpot">
                    <ItemTemplate>
                        <itemtemplate>
                        <tr>
                            <td height="28" align="left" bgcolor="#E9F4F9" class="bottomline">
                                <input name="cbxProID" data-index="<%#Container.ItemIndex+1 %>" type="checkbox" value="<%#Eval("id")%>" />
                                <%#Container.ItemIndex+1 %>
                            </td>
                            <td align="left" bgcolor="#E9F4F9" class="bottomline">
                                <span data-class="returnValue">
                                    <%#Eval("JourneySpot")%></span>
                            </td>
                        </tr>
                    </itemtemplate>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal ID="litMsg" runat="server" Text=""></asp:Literal>
            </tbody>
        </table>
        <div class="tablehead">
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <form>
        <div class="alertbox-btn">
            <a id="btnSelect" hidefocus="true" href="javascript:void(0);"><s class="xuanzhe"></s>
                <%=(String)GetGlobalResourceObject("string", "选择")%></a><a id="btnSave" href="javascript:void(0);" hidefocus="true"><s class="baochun"></s><%=(String)GetGlobalResourceObject("string", "保存")%></a>
        </div>
        <br>
        <table width="100%" cellspacing="5" cellpadding="0" border="0">
            <tbody>
                <tr>
                    <td>
                        <textarea name="txtNewInfo" cols="" rows="" class="inputtext formsize800" style="width: 98%;
                            height: 60px;" id="txtNewInfo">&nbsp;</textarea>
                    </td>
                </tr>
            </tbody>
        </table>
        </form>
    </div>
</body>
</html>

<script type="text/javascript">
    var strArr = [];
    $(function() {
        var ids = parent.$("#hd_JourneySpot").next("input[type='hidden']").val();
        if (ids != "") {
            var _ids = ids.split(',');
            if (_ids != null && _ids.length > 0) {
                $("#tableData").find("input[name='cbxProID']").each(function() {
                    var _self = $(this);
                    var val = _self.val();
                    for (var i = 0; i < _ids.length; i++) {
                        if (_ids[i] == val) {
                            _self.attr("checked", true);
                        }
                    }
                });
            }
        }


        $("#tableData").find("input[name='cbxProID']").click(function() {
            var self = $(this);
            var val = self.val();

            ids = parent.$("#hd_JourneySpot").next("input[type='hidden']").val(); //

            if ($(this).attr("checked") == true) {
                var str = parent.document.getElementById("hd_JourneySpot").value;
                var returnValue = $.trim($(this).parent().next().find("span[data-class='returnValue']").html()) + " </br>";
                str += returnValue;
                parent.document.getElementById("hd_JourneySpot").value = str;


                //处理Id
                if (ids != "") {
                    var newIds = new Array();
                    newIds = ids.toString().split(',');

                    var flg = true;
                    for (var i = 0; i < newIds.length; i++) {
                        if (newIds[i] == val) {
                            flg = false;
                            break;
                        }
                    }
                    if (flg) {
                        newIds.push(val);
                    }

                    parent.$("#hd_JourneySpot").next("input[type='hidden']").val(newIds.join(','));
                }
                else {
                    parent.$("#hd_JourneySpot").next("input[type='hidden']").val(val);
                }

            }
            else {

                if (ids != "") {
                    var newIds = new Array();
                    var _ids = ids.toString().split(',');

                    for (var i = 0; i < _ids.length; i++) {
                        if (_ids[i] != val) {
                            newIds.push(_ids[i]);
                        }
                    }

                    parent.$("#hd_JourneySpot").next("input[type='hidden']").val(newIds.join(','));
                }
                else {
                    parent.$("#hd_JourneySpot").next("input[type='hidden']").val(val);
                }
            }

        });

        $("#btnSelect").click(function() {

            var obj = parent.$("#" + SelectInformation.aid);

            $("#tableData").find("input[name='cbxProID']:checked").each(function(i) {
                var strval = $.trim($(this).parent().next().find("span[data-class='returnValue']").html()) + "</br>";
                strArr.push(strval);
            });

            var str = parent.document.getElementById("hd_JourneySpot").value;
            obj.closest("tr").find("textarea").each(function() {
                if ($(this).attr("id")) {
                    var ResultStr = "";
                    for (var i = 0; i < strArr.length; i++) {
                        ResultStr += (i + 1).toString() + "、" + strArr[i];
                    }
                    parent.KEditer.html($(this).attr("id"), ResultStr);
                }
            });


            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        })

        $("#btnSave").click(function() {
            SelectInformation.Save();
            return false;
        });
        var SelectInformation = {
            sl: '<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>',
            LngType: '<%=EyouSoft.Common.Utils.GetQueryStringValue("LgType") %>',
            aid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("Id") %>',
            AreaID: '<%=EyouSoft.Common.Utils.GetQueryStringValue("AreaID") %>',
            Save: function() {
                if ($.trim($("#txtNewInfo").val()) == "") {
                    var msg = '<%=(String)GetGlobalResourceObject("string", "保存内容不能为空")%>';
                    parent.tableToolbar._showMsg(msg);
                    return false;
                }
                $("#btnSave").unbind("click");
                $("#btnSave").html('<s class="baochun"></s><%=(String)GetGlobalResourceObject("string", "提交中")%>');
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/CommonPage/JourneySpot.aspx?type=save&sl=" + SelectInformation.sl + "&LgType=" + SelectInformation.LngType + "&AreaID=" + SelectInformation.AreaID,
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            parent.tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = window.location.href;
                            });
                        }
                    },
                    error: function() {
                        parent.tableToolbar._showMsg(tableToolbar.errorMsg);
                        SelectInformation.BindBtn();
                    }
                });

            },
            BindBtn: function() {
                $("#btnSave").click(function() {
                    SelectInformation.Save();
                    return false;
                })
                $("#btnSave").html('<s class="baochun"></s><%=(String)GetGlobalResourceObject("string", "保存")%>');
            }
        }
    });
    </script>

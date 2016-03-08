<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="selectCity.aspx.cs" Inherits="EyouSoft.WebFX.CommonPage.selectCity" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>菜单选用</title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0" class="alertboxbk2"
            style="margin: 0 auto;">
            <tr>
                <td width="45%" align="left" style="vertical-align:top;">
                    <%=(String)GetGlobalResourceObject("string", "国家") %>：<select id="ddlCountry" name="ddlCountry"
                        class="inputselect"></select>
                    <span style="display: block; height: 10px;"></span><%=(String)GetGlobalResourceObject("string", "省份") %>：<select id="ddlProvice" class="inputselect" name="ddlProvice">
                    </select>
                    <span style="display: block; height: 10px;"></span><%=(String)GetGlobalResourceObject("string", "城市") %>：
                    <select id="ddlCity" class="inputselect" name="ddlCity">
                    </select>
                    <span style="display: block; height: 10px;"></span> <%=(String)GetGlobalResourceObject("string", "县区") %>：
                    <select id="ddlArea" class="inputselect" name="ddlArea">
                    </select>
                </td>
                <td width="18%" align="center">
                    <a href="javascript:;" id="addcity">
                        <img src="../images/addimg.gif" width="48" height="20" style="margin-bottom: 10px;"
                            alt="" /></a><br />
                    <a href="javascript:;" id="delcity">
                        <img src="../images/delimg.gif" width="48" height="20" style="margin-bottom: 10px;"
                            alt="" /></a>
                </td>
                <td width="37%" align="center">
                    <ul id="ul1" class="whitebg">
                    </ul>
                </td>
            </tr>
        </table>
        <div class="alertbox-btn">
            <a href="javascript:;" hidefocus="true" id="btnsave"><s class="baochun"></s><%=(String)GetGlobalResourceObject("string", "保存") %></a><a href="javascript:parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();"
                    hidefocus="true"><s class="chongzhi"></s><%=(String)GetGlobalResourceObject("string", "关闭") %></a></div>
    </div>
</body>
</html>

<script type="text/javascript">
    var iPage = {
        gouwu: ""
    };
    $(function() {
        pcToobar.init({
            gID: "#ddlCountry",
            pID: "#ddlProvice",
            cID: "#ddlCity",
            xID:"#ddlArea",
            comID: '<%=this.SiteUserInfo.CompanyId %>',
            lng: '<%=Request.QueryString["LgType"] %>'
        })
        $("#ul1").delegate("li", "click", function() {
            $(this).toggleClass("selected");
        })
        var data = {
            id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("hideID") %>',
            name: '<%=EyouSoft.Common.Utils.GetQueryStringValue("CityName") %>',
            isMore: '<%=EyouSoft.Common.Utils.GetQueryStringValue("isMore") %>'//0：不多选; 1:多选
        }


        $("#addcity").click(function() {
            if (data.isMore == "0") {
                if ($("#ul1 li").length == 1) {
                    var msg = '<%=(String)GetGlobalResourceObject("string", "只能选择一个县区") %>';
                    alert(msg);
                    return false;
                }
            }
            var thisVal = $("#ddlArea").val();
            if (checkVal(thisVal)) {
                if (thisVal != "-1" && thisVal != "") {
                    var text = $("#ddlArea option:selected").text();
                    var val = $("#ddlArea option:selected").val();
                    $("#ul1").append("<li data-id=" + val + ">" + text + "</li>");
                } 
            }
            return false;
        })
        $("#delcity").click(function() {
            $("#ul1 li").each(function() {
                if ($(this).hasClass("selected")) {
                    $(this).remove();
                }
            })
        })
        $("#btnsave").click(function() {
            var selectVal = "";
            var selectText = "";
            var licount = $("#ul1").find("li").length;
            $("#ul1 li").each(function(i) {
                if (i == licount - 1) {
                    selectVal += $(this).attr("data-id");
                    selectText += $(this).text();
                }
                else {
                    selectVal += $(this).attr("data-id") + ",";
                    selectText += $(this).text() + ",";
                }
            })
            if (selectVal == "") {
                var msg = '<%=(String)GetGlobalResourceObject("string", "请先选择城市") %>';
                alert(msg);
                return false;
            }
            if (selectVal != "") {
                var url = "/CommonPage/selectCity.aspx?citysid=" + selectVal;
                $.newAjax({
                    type: "post",
                    cache: false,
                    async: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            iPage.gouwu = ret.obj;
                        }
                    }
                });
            }
            SetValue(selectVal, selectText);
            return false;
        })
        GetOldCity();

    })

    function checkVal(val) {
        var result = true;
        $("#ul1 li").each(function() {
            if ($(this).attr("data-id") == val) {
                result = false;
                return result;
            }
        })
        return result;
    }
    function GetOldCity() {
        var cityName = '<%=Request.QueryString["CityName"] %>';
        var cityid = '<%=Request.QueryString["hideID"] %>';
        if (cityid != "" && cityName != "") {
            cityName = cityName.toString().split(",");
            cityid = cityid.toString().split(",");
            for (var i = 0; i < cityid.length; i++) {
                $("#ul1").append("<li data-id=" + cityid[i] + ">" + cityName[i] + "</li>");
            }
        }
    }

    function SetValue(val, text) {
        var data = {
            callBack: Boxy.queryString("callBack"),
            hideID: Boxy.queryString("hideID"),
            iframeID: Boxy.queryString("iframeId"),
            pIframeID: '<%=Request.QueryString["pIframeID"] %>',
            showID: Boxy.queryString("showID")
        }

        var args = {
            aid: '<%=Request.QueryString["aid"] %>',
            id: val,
            name: text,
            gouwu: iPage.gouwu
        }

        //根据父级是否为弹窗传值
        if (data.pIframeID != "" && data.pIframeID.length > 0) {
            //定义父级弹窗
            var boxyParent = window.parent.Boxy.getIframeWindow(data.pIframeID) || window.parent.Boxy.getIframeWindowByID(data.pIframeID);
            //判断是否存在回调方法
            if (data.callBack != null && data.callBack.length > 0) {
                if (data.callBack.indexOf('.') == -1) {
                    boxyParent[data.callBack](args);
                }
                else {
                    boxyParent[data.callBack.split('.')[0]][data.callBack.split('.')[1]](args);
                }
            }
            //定义回调
        }
        else {
            //判断是否存在回调方法
            if (data.callBack != null && data.callBack.length > 0) {
                if (data.callBack.indexOf('.') == -1) {
                    window.parent[data.callBack](args);
                }
                else {
                    window.parent[data.callBack.split('.')[0]][data.callBack.split('.')[1]](args);
                }
            }
            //定义回调
        }
        parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
    }
</script>


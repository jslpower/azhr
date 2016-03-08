<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSupplier.aspx.cs" Inherits="EyouSoft.Web.CommonPage.UserSupplier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/js/jquery-1.4.4.js"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script type="text/javascript" src="/js/bt.min.js"></script>

    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"></script><![endif]-->

    <script src="/Js/table-toolbar.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <input type="hidden" name="hideID" id="hideID" value='<%=Request.QueryString["hideID"] %>' />
    <input type="hidden" name="showID" id="ShowID" value='<%=Request.QueryString["ShowID"] %>' />
    <div class="alertbox-outbox">
        <form id="formSearch" runat="server" method="get">
        <table width="99%" id="TabSearch" align="center" cellpadding="0" cellspacing="0"
            bgcolor="#e9f4f9" style="margin: 0 auto">
            <tr>
                <td width="10%" height="28" align="right" bgcolor="#b7e0f3" class="alertboxTableT">
                    <span class="SourceName"></span>名称：
                </td>
                <td width="90%" align="left">
                    <input name="txtName" type="text" class="inputtext formsize100" id="txtName" value='<%=Request.QueryString["txtName"]%>' />
                    国家：
                    <select name="ddlCountry" id="ddlCountry" class="inputselect">
                    </select>
                    省份：
                    <select name="ddlProvice" id="ddlProvice" class="inputselect">
                    </select>
                    城市：
                    <select name="ddlCity" id="ddlCity" class="inputselect">
                    </select>
                    <% if (type == EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店)%>
                    <%   {%>
                    酒店星级：<select name="jiudianxingji" id="jiudianxingji" class="inputselect">
                        <%=EyouSoft.Common.UtilsCommons.GetEnumDDL(EyouSoft.Common.EnumObj.GetList(typeof(EyouSoft.Model.EnumType.GysStructure.JiuDianXingJi)), EyouSoft.Common.Utils.GetQueryStringValue("jiudianxingji"))%>
                    </select>
                    <%  } %>
                    <input type="hidden" name="binkemode" id="binkemode" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("binkemode") %>" />
                    <input type="hidden" name="tourmode" id="tourmode" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("tourmode") %>" />
                    <input type="hidden" name="suppliertype" id="suppliertype" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("suppliertype") %>" />
                    <input type="hidden" name="LgType" id="LgType" value='<%=Request.QueryString["LgType"] %>' />
                    <input type="hidden" name="callback" id="callback" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("callBack") %>" />
                    <input type="hidden" name="iframeid" id="iframeid" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("iframeid") %>" />
                    <input type="hidden" name="pIframeID" id="pIframeID" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("pIframeID") %>" />
                    <input type="hidden" name="ShowID" id="ShowID" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("ShowID") %>" />
                    <input type="hidden" name="aid" id="aid" value="<%=EyouSoft.Common.Utils.GetQueryStringValue("aid") %>" />
                    <input type="submit" value="搜索" class="search-btn" style="cursor: pointer; height: 24px;
                        width: 64px; background: url(/images/cx.gif) no-repeat center center; border: 0 none;
                        margin-left: 5px;" />
                    <input type="button" value="新增" class="search-btn" id="btn_add" style="cursor: pointer;
                        height: 24px; width: 64px; background: url(/images/cx.gif) no-repeat center center;
                        border: 0 none; margin-left: 5px;" />
                </td>
            </tr>
        </table>
        </form>
        <div class="hr_10">
        </div>
        <div style="margin: 0 auto; width: 99%;">
            <span class="formtableT formtableT02 t1">选择供应商 </span>
            <div id="AjaxHotelList" style="width: 100%">
            </div>
            <div class="alertbox-btn">
                <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
        </div>
    </div>
</body>
</html>

<script type="text/javascript">
    var UseSupplier = {
        AjaxURLg: null,
        type: '<%=(int)type %>',
        aid: '<%=Request.QueryString["aid"] %>',
        PageInit: function() {
            if (UseSupplier.type == 2 || UseSupplier.type == 1) {
                $("#a_btn").after("<a href='javascript:void(0)' hidefocus='true' id='a_cencer'><s class='chongzhi'></s>清除</a>");
            }
            if (UseSupplier.type == 8) {
                $("#TabSearch").hide();
            } else {
                pcToobar.init({
                    gID: "#ddlCountry",
                    pID: "#ddlProvice",
                    cID: "#ddlCity",
                    comID: '<%=this.SiteUserInfo.CompanyId %>',
                    gSelect: '<%=Request.QueryString["ddlCountry"]??"0" %>',
                    pSelect: '<%=Request.QueryString["ddlProvice"]??"0" %>',
                    cSelect: '<%=Request.QueryString["ddlCity"]??"0" %>'
                })
            }
            var param = $.param({ provice: $("#ddlProvice").val(), city: $("#ddlCity").val(), name: $("#txtName").val() });

            this.GetUrl(useSupplierPage.type);
        },
        GetUrl: function(type) {
            this.AjaxURLg = "/CommonPage/AjaxRequest/AjaxSupplier.aspx?";
        },
        GetAjaxData: function(pageIndex, url) {
            var ajaxurl = UseSupplier.AjaxURLg;
            //AJAX 加载数据
            $("#AjaxHotelList").html("<div style='width:100%; text-align:center;'><img src='/images/loadingnew.gif' border='0' align='absmiddle'/>&nbsp;正在加载,请等待....&nbsp;</div>");

            var para = { suppliertype: UseSupplier.type, country: '<%=Request.QueryString["ddlCountry"]??"0" %>', provice: '<%=Request.QueryString["ddlProvice"]??"0" %>', city: '<%=Request.QueryString["ddlCity"]??"0" %>', name: $("#txtName").val(), callback: $("#callback").val(), iframeId: $("#iframeid").val(), piframeId: $("#pIframeID").val(), aid: UseSupplier.aid, ShowID: $("#ShowID").val(), tourmode: '<%=Request.QueryString["tourmode"]??"0" %>', binkemode: '<%=Request.QueryString["binkemode"]??"1" %>', LgType: '<%=Request.QueryString["LgType"] %>', jiudianxingji: '<%=Request.QueryString["jiudianxingji"] %>' };
            var url = ajaxurl + $.param(para);
            $.newAjax({
                type: "Get",
                url: url + "&Page=" + pageIndex,
                cache: false,
                success: function(result) {
                    $("#AjaxHotelList").html(result);
                    //初始化选中                   
                    var data = {
                        id: '<%=EyouSoft.Common.Utils.GetQueryStringValue("hideID") %>',
                        caidanid: '<%=EyouSoft.Common.Utils.GetQueryStringValue("hidcaidanid") %>',
                        caidanname: '<%=EyouSoft.Common.Utils.GetQueryStringValue("ShowID") %>'
                    }
                    //选中单选钮
                    $(":radio[value='" + data.id + "']").attr("checked", "checked");
                    $("#tblList").find("input[type='radio']:checked").each(function() {
                        $(this).closest("td").find("input[name='hidcaidanid']").val(data.caidanid);
                        $(this).closest("td").find("input[name='hidcaidanname']").val(data.caidanname);
                    })
                }
            });
        },
        reload: function() {
            window.location.href = window.location.href;
        }
    }


    $(function() {
        UseSupplier.PageInit();
        UseSupplier.GetAjaxData(Boxy.queryString("Page"), UseSupplier.AjaxURLg);
        $("#a_btn").click(function() {
            useSupplierPage.SetValue();
            useSupplierPage.SelectValue();
            return false;
        })
        $("#btn_add").click(function() {
            switch (UseSupplier.type) {
                case '<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.酒店 %>':
                    url = "/Gys/JiuDianEdit.aspx?sl=34";
                    break;
                case '<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.地接社 %>':
                    url = "/Gys/DiJieEdit.aspx?sl=33";
                    break;
                case '<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.餐馆 %>':
                    url = "/Gys/CanGuanEdit.aspx?sl=35";
                    break;
                case '<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.景点 %>':
                    url = "/Gys/jingdianEdit.aspx?sl=38";
                    break;
                case '<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.车队 %>':
                    url = "/Gys/CheDuiEdit.aspx?sl=36";
                    break;
                case '<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.物品 %>':
                    url = "/Gys/WuPinEdit.aspx?sl=40";
                    break;
                case '<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.区间交通 %>':
                    url = "/Gys/jiaotongedit.aspx?sl=37";
                    break;
                case '<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.购物 %>':
                    url = "/Gys/gouwuedit.aspx?sl=39";
                    break;
                case '<%=(int)EyouSoft.Model.EnumType.GysStructure.GysLeiXing.其他 %>':
                    url = "/Gys/qitaedit.aspx?sl=41";
                    break;
                default:

                    break;
            }
            if (url == "") return false;
            top.Boxy.iframeDialog({
                title: "新增供应商",
                iframeUrl: url,
                width: "940px",
                height: "650px",
                afterHide: function() { UseSupplier.reload(); }
            });
            return false;
        })

        $(".alertbox-btn").delegate("#a_cencer", "click", function() {
            var data = {
                callBack: Boxy.queryString("callBack")
            }
            var args = {
                aid: '<%=Request.QueryString["aid"] %>',
                id: "",
                selectValue: "",
                selectTxt: "",
                selecttype: "",
                contactname: "",
                contacttel: "",
                contactfax: "",
                caidanid: "",
                caidanname: "",
                contactmobile: "",
                pricejs: "",
                priceth: "",
                WPrice: ""
            }
            window.parent[data.callBack](args);
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"] %>').hide();
        })
        $("#tblList").find("input[type='radio']").click(function() {
            if ($("#sModel").val() != "2") {
                $("#tblList").find("input[type='radio']").attr("checked", "");
                $(this).attr("checked", "checked");
            }
        });
    });
    var useSupplierPage = {
        _dataObj: {},
        selectValue: "",
        selectTxt: "",
        selecttype: "",
        contactname: "",
        contacttel: "",
        contactfax: "",
        caidanid: "",
        caidanname: "",
        contactmobile: "",
        pricejs: "",
        priceth: "",
        WPrice: "",
        jiesuanfangshi: "",
        SetValue: function() {
            var valueArray = [], txtArray = [], contactname = [], fax = [], tel = [], caidanid = [], caidanname = [], mobile = [], pricejs = [], priceth = [], wPrice = [], jiesuanfangshi = [];
            $("#tblList").find("input[type='radio']:checked").each(function() {
                valueArray.push($(this).val());
                txtArray.push($(this).attr("data-show"));
                contactname.push($(this).attr("data-contactname"));
                tel.push($(this).attr("data-tel"));
                fax.push($(this).attr("data-fax"));
                mobile.push($(this).attr("data-mobile"));
                caidanid.push($(this).closest("td").find("input[name='hidcaidanid']").val());
                caidanname.push($(this).closest("td").find("input[name='hidcaidanname']").val());
                pricejs.push($(this).attr("data-pricejs"));
                priceth.push($(this).attr("data-priceth"));
                wPrice.push($(this).attr("data-WPrice"));
                jiesuanfangshi.push($(this).attr("data-jiesuanfangshi"));
            })

            this.selectValue = valueArray.join(',');
            this.selectTxt = txtArray.join(',');
            this.contactname = contactname.join(',');
            this.contacttel = tel.join(',');
            this.contactfax = fax.join(',');
            this.caidanid = caidanid.join(',');
            this.caidanname = caidanname.join(',');
            this.contactmobile = mobile.join(',');
            this.pricejs = pricejs.join(',');
            this.priceth = priceth.join(',');
            this.WPrice = wPrice.join(',');
            this.jiesuanfangshi = jiesuanfangshi.join(',');
        },
        RadioClickFun: function(args) {
            var rdo = $(args);
            var data = rdo.val().split(',');
            this.selectValue = data[0];
            this.selectTxt = data[1];
            this.contactname = data[2];
            this.contacttel = data[3];
            this.contactfax = data[4];
            this.SelectValue();
        },
        SelectValue: function() {
            var data = {
                callBack: Boxy.queryString("callBack"),
                hideID: Boxy.queryString("hideID"),
                iframeID: Boxy.queryString("iframeId"),
                pIframeID: '<%=Request.QueryString["pIframeID"] %>',
                showID: Boxy.queryString("showID"),
                flagObj: Boxy.queryString("flagObj")
            }

            var args = {
                aid: '<%=Request.QueryString["aid"] %>',
                id: useSupplierPage.selectValue,
                name: useSupplierPage.selectTxt,
                type: '<%=Request.QueryString["suppliertype"] %>',
                showid: data.showID,
                hideid: data.hideID,
                contactname: useSupplierPage.contactname,
                contacttel: useSupplierPage.contacttel,
                contactfax: useSupplierPage.contactfax,
                caidanid: useSupplierPage.caidanid,
                caidanname: useSupplierPage.caidanname,
                contactmobile: useSupplierPage.contactmobile,
                pricejs: useSupplierPage.pricejs,
                pricesell: useSupplierPage.priceth,
                wprice: useSupplierPage.WPrice,
                jiesuanfangshi: useSupplierPage.jiesuanfangshi
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

    }


   
    
    
</script>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XunJia.aspx.cs" Inherits="EyouSoft.WebFX.XunJia"
    Culture="auto" UICulture="auto" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/HeadDistributorControl.ascx" TagName="HeadDistributorControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>�������տͼƻ�</title>
    <link href="Css/fx_style.css" rel="stylesheet" type="text/css" />
    <link href="Css/boxynew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/Js/jquery-1.4.4.js"> </script>

</head>
<body style="background: 0 none;">
    <uc1:HeadDistributorControl runat="server" ID="HeadDistributorControl1" ProcductClass="default Producticon" />
    <div class="list-main">
        <div class="list-maincontent">
            <div class="linebox-menu">
                <a href='XunJiaEdit.aspx?LgType=<%=Request.QueryString["LgType"] %>&sl=0'><span>
                    <%=(String)GetGlobalResourceObject("string", "����ѯ��")%></span></a><a href="javascript:;"
                        id="update"><span>
                            <%=(String)GetGlobalResourceObject("string", "�޸�ѯ��")%></span></a><a href="javascript:void(0);"
                                id="delete"><span><%=(String)GetGlobalResourceObject("string", "ɾ��ѯ��")%></a></span></div>
            <div class="hr_10">
            </div>
            <div class="listsearch">
                <form id="frm" method="get">
                <input type="hidden" id="sl" name="sl" value="0" />
                <%=(String)GetGlobalResourceObject("string", "��·����")%>��
                <select name="a" id="a">
                    <%=EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("a")), SiteUserInfo.CompanyId, (EyouSoft.Model.EnumType.SysStructure.LngType)EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("LgType")))%>
                </select>
                <%=(String)GetGlobalResourceObject("string", "��·����")%>��
                <input name="routename" id="routename" value="<%=Request.QueryString["routename"] %>"
                    type="text" class="searchInput size68" />
                <%=(String)GetGlobalResourceObject("string", "ѯ������")%>��
                <input name="sd" id="sd" value="<%=Request.QueryString["sd"] %>" type="text" class="searchInput size68"
                    onfocus="WdatePicker();" />
                -
                <input name="ed" id="ed" value="<%=Request.QueryString["ed"] %>" type="text" class="searchInput size68"
                    onfocus="WdatePicker();" />
                <a href="javascript:void(0);" id="btnSearch">
                    <img src="<%=GetGlobalResourceObject("string","ͼƬ��������") %>" /></a>
                <input type="hidden" name="LgType" value="<%=Request.QueryString["LgType"] %>" />
                </form>
            </div>
            <div class="listtablebox">
                <table width="100%" border="0" cellpadding="0" cellspacing="0" id="Table1">
                <tbody>
                    <tr class="odd">
                        <th align="center" rowspan="2">
                            <input type="checkbox" name="checkbox" />
                        </th>
                        <th align="center" rowspan="2">
                           ���
                        </th>
                        <th align="center" rowspan="2">
                           �ź�
                        </th>
                          <th align="center" rowspan="2">
                            <%=(String)GetGlobalResourceObject("string", "��·����")%>
                        </th>
                        <th align="center" rowspan="2">
                           ����ʱ��
                        </th>
                        <th align="center" rowspan="2">
                            <%=(String)GetGlobalResourceObject("string", "����")%>
                        </th>
                        <th align="center" rowspan="2">
                           ������
                        </th>
                        <th align="center" rowspan="2">
                           �Ƶ�Ա
                        </th>
                        <th align="center" rowspan="2">
                           ���м�
                        </th>
                        <th align="center" rowspan="2">
                          ͬ�м�
                        </th>
                        <th align="center" colspan="4">
                            <%=(String)GetGlobalResourceObject("string", "����")%>
                        </th>
                        <th align="center" rowspan="2">
                          ����
                        </th>
                    </tr>
                    <tr class="">
                            <th class="nojiacu" align="center">Ԥ</th>
                            <th class="nojiacu" align="center">��</th>
                            <th class="nojiacu" align="center">ʵ</th>
                            <th class="nojiacu" align="center">ʣ</th>
                    </tr>
                    <asp:Repeater runat="server" ID="RtPlan">
                        <ItemTemplate>
                            <tr class="<%#Container.ItemIndex % 2 == 0 ? "" : "odd"%>" data-count="<%#Eval("TimeCount") %>"
                                data-id="<%#Eval("QuoteId") %>" data-parentid='<%#Eval("ParentId")%>'>
                                <td align="center">
                                    <input type="checkbox" name="checkbox" value="<%#Eval("QuoteId") %>" data-type="1" />
                                </td>
                                <td align="center">
                                    <%#GetAreaName(Eval("AreaId"))%>
                                </td>
                                <td align="center">
                                    <%#this.Eval("AreaId")%>
                                </td>
                                <td align="center">
                                    <%#this.Eval("RouteName")%>
                                </td>
                                <td align="center">
                                    <%#this.Eval("ArriveCity")%>
                                </td>
                                <td align="center">
                                    <%#this.Eval("LeaveCity")%>
                                </td>
                                <td align="center">
                                    <b>
                                        <%#this.Eval("Days")%></b>
                                </td>
                                <td align="center">
                                    <%#this.Eval("MinAdults")%>-<%#this.Eval("MaxAdults")%>
                                </td>
                                <td align="center">
                                    <%#GetDateTime(Eval("BuyTime"))%>
                                </td>
                                <td align="center">
                                    <%#GetHtmlByState(Eval("TimeCount").ToString(), (EyouSoft.Model.EnumType.TourStructure.QuoteState)(int)Eval("QuoteStatus"), Eval("CancelReason").ToString())%>
                                </td>
                                <td align="center">
                                    <a href="/PrintPage/QuoteDetail.aspx?QuoteId=<%#this.Eval("QuoteId")%>&LgType=<%=Request.QueryString["LgType"] %>" style="visibility: <%=this.HeadDistributorControl1.IsPubLogin?"hidden":"visibility" %>"
                                        target="_blank">
                                        <%=(String)GetGlobalResourceObject("string", "��ϸ�г̵�")%></a> <a href="/PrintPage/QuoteSimple.aspx?QuoteId=<%#this.Eval("QuoteId")%>&LgType=<%=Request.QueryString["LgType"] %>" style="visibility: <%=this.HeadDistributorControl1.IsPubLogin?"hidden":"visibility" %>"
                                            target="_blank">
                                            <%=(String)GetGlobalResourceObject("string", "��Ҫ�г̵�")%></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:PlaceHolder ID="PhPage" runat="server">
                        <tr>
                            <td colspan="14" align="center" bgcolor="#f4f4f4">
                                <div class="pages">
                                    <cc1:ExporPageInfoSelect runat="server" ID="ExporPageInfoSelect1" />
                                </div>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="litMsg" Visible="false">
                        <tr>
                            <td align='center' colspan='14'>
                                <%=GetGlobalResourceObject("string","��������") %>
                            </td>
                        </tr>
                    </asp:PlaceHolder>
                    </tbody>
                </table>
            </div>
            <div class="hr_10">
            </div>
        </div>
    </div>
    <!--[if IE]><script src="/js/excanvas.js" type="text/javascript" charset="utf-8"> </script><![endif]-->

    <script type="text/javascript" src="/Js/bt.min.js"> </script>

    <script type="text/javascript" src="/Js/jquery.boxy.js"> </script>

    <!--[if lte IE 6]><script src="/js/jquery.bgiframe.min.js" type="text/javascript"> </script><![endif]-->

    <script type="text/javascript" src="/Js/jquery.blockUI.js"> </script>

    <script type="text/javascript" src="/Js/table-toolbar.js"> </script>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"> </script>

    <script type="text/javascript">
        var AcceptPlan = {
            Submit: function() {
                $("#frm").submit();
            },
            Update: function(val) {
                var params = { "id": val, "sl": "0", "LgType": '<%=Request.QueryString["LgType"] %>' };
                location.href = "/XunjiaEdit.aspx?" + $.param(params);
                return false;
            },
            DelAll: function(objArr) {
                var list = new Array();
                var msgList = new Array();
                var state = "";
                //������ť�����������
                for (var i = 0; i < objArr.length; i++) {
                    state = objArr[i].find("span[data-class='QuoteState']").attr("data-state");
                    if (state == "<%=(int)EyouSoft.Model.EnumType.TourStructure.QuoteState.���۳ɹ� %>") {
                        msgList.push("��ǰѡ�����е�" + (i + 1) + "���ѳɹ�����,�޷�ɾ��!");
                    }
                    else {
                        list.push(objArr[i].attr("data-id"));
                    }
                }

                if (msgList.length > 0) {
                    var msg = '<%=GetGlobalResourceObject("string","�޷�ɾ���Ѿ����۳ɹ�������") %>';
                    tableToolbar._showMsg(msg);
                    return false;
                }
                //ִ��
                var lgtype = '<%=Request.QueryString["LgType"] %>';
                $.newAjax({
                    type: "get",
                    cache: false,
                    url: "/Xunjia.aspx?dotype=del&id=" + list.join(',') + "&sl=0&LgType=" + lgtype,
                    dataType: "json",
                    success: function(ret) {
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() {
                                window.location.href = window.location.href;
                            });
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });

            },
            PageInit: function() {
                $("#update").click(function() {
                    var objarr = $("#Table1").find("input[type='checkbox'][data-type='1']:checked");
                    var msg = '<%=(String)GetGlobalResourceObject("string", "��ѡ��һ������")%>';
                    var msg2 = '<%=(String)GetGlobalResourceObject("string", "ֻ��ѡ��һ�����ݽ����޸�")%>';
                    if (objarr.length > 1) {
                        tableToolbar._showMsg(msg2);
                        return false;
                    }
                    if (objarr.length == 0) {
                        tableToolbar._showMsg(msg);
                        return false;
                    }
//                    var state = objarr.closest("tr").find("span[data-class='QuoteState']").attr("data-state");
//                    var str = "";
//                    if (state == "<%=(int)EyouSoft.Model.EnumType.TourStructure.QuoteState.���۳ɹ� %>") {
//                        str = "�Ѿ����۳ɹ������ݲ������޸�";
//                    }
//                    if (str != "") {
//                        tableToolbar._showMsg(str);
//                        return false;
//                    }
                    AcceptPlan.Update(objarr.val());
                })
                $("#delete").click(function() {
                    var cols = [];
                    $("#Table1").find("input[type='checkbox'][data-type='1']:checked").each(function() {
                        cols.push($(this).closest("tr"));
                    });
                    var msg = '<%=(String)GetGlobalResourceObject("string", "��ѡ��һ������")%>';
                    var msg2 = '<%=(String)GetGlobalResourceObject("string", "�Ƿ�ɾ��")%>';
                    if (cols.length == 0) {
                        tableToolbar._showMsg(msg);
                        return false;
                    }

                    if (confirm(msg2)) {
                        AcceptPlan.DelAll(cols);
                    }
                })

                //��ѯ�б�
                $("#btnSearch").click(function() {
                    AcceptPlan.Submit();
                });

                //Enter����
                $("#frm").find(":text").keypress(function(e) {
                    if (e.keyCode == 13) {
                        AcceptPlan.Submit();
                        return false;
                    }
                });

            }
        };
        $(function() {
            AcceptPlan.PageInit();
            //����ȡ��ԭ��
            $("#Table1").find("a[data-class='cancelReason']").each(function() {
                if ($.trim($(this).next().html()) != "") {
                    $(this).bt({
                        contentSelector: function() {
                            return $(this).next().html();
                        },
                        positions: ['left', 'right', 'bottom'],
                        fill: '#FFF2B5',
                        strokeStyle: '#D59228',
                        noShadowOpts: { strokeStyle: "#D59228" },
                        spikeLength: 10,
                        spikeGirth: 15,
                        width: 200,
                        overlap: 0,
                        centerPointY: 1,
                        cornerRadius: 4,
                        shadow: true,
                        shadowColor: 'rgba(0,0,0,.5)',
                        cssStyles: { color: '#00387E', 'line-height': '180%' }
                    });
                }
            })
        });
    </script>

</body>
</html>

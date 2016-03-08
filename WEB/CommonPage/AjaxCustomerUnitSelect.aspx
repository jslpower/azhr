<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AjaxCustomerUnitSelect.aspx.cs"
    Inherits="Web.CommonPage.AjaxCustomerUnitSelect" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
</head>
<body>
    <table width="99%" id="tab_list" border="0" cellspacing="0" cellpadding="0" class="noborderInput">
        <tr>
            <asp:Repeater ID="rpt_list" runat="server">
                <ItemTemplate>
                    <td align="left" style="padding: 5px 5px; width: 25%;">
                        <label>
                            <input type="checkbox" name="contactID" value="<%#Eval("CrmId")%>" data-guoji="<%#Eval("CountryId") %>"
                                data-kehudengjibh="<%#Eval("KeHuDengJiBH") %>" data-lvprice='<%#GetKeHuDengjiPrice(Eval("KeHuDengJiBH")) %>'
                                data-type="<%=(int)type %>" <%#GetAttrStr(Eval("Lxrs")) %> />
                            <input type="radio" name="contactID" value="<%#Eval("CrmId")%>" data-guoji="<%#Eval("CountryId") %>"
                                data-kehudengjibh="<%#Eval("KeHuDengJiBH") %>" data-lvprice='<%#GetKeHuDengjiPrice(Eval("KeHuDengJiBH")) %>'
                                data-type="<%=(int)type %>" <%#GetAttrStr(Eval("Lxrs")) %> />
                            <span data-class="sp_Name">
                                <%#Eval("Name")%></span><span data-class="sp_ContactName" style="display: none"></span></label>
                        <%#EyouSoft.Common.Utils.IsOutTrOrTd(Container.ItemIndex, recordCount, 4)%>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <asp:Literal ID="lbemptymsg" runat="server" Text="<tr><td align='center' height='30'>暂无数据!</td></tr>"></asp:Literal>
        <asp:PlaceHolder ID="phdPages" runat="server">
            <tr>
                <td height="30" colspan="5" align="right">
                    <div id="div_page" style="position: relative; height: 20px;">
                        <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
                    </div>
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>

    <script type="text/javascript">
        var AjaxCustomerUnitSelect = {
            Init: function() {
                //只有报名选用时才执行
                if ((parseInt(Boxy.queryString("IsApply")) || 0) == 1) {
                    //初始化联系人
                    $("#tab_list :checkbox").each(function() {
                        var obj = $(this);
                        obj.closest("td").attr("data-class", "td_popo");
                        obj.closest("td").find("span[data-class='sp_ContactName']").html((function() {
                            //联系人编号
                            var contactids = obj.attr("data-contactid");
                            //联系人名字
                            var contactnames = obj.attr("data-contactname");
                            //联系人手机
                            var mobilephones = obj.attr("data-mobilephone");
                            //联系人电话
                            var contactphones = obj.attr("data-contactphone");
                            //联系人部门
                            var department = obj.attr("data-department");
                            //客户等级
                            var kehudengjibh = obj.attr("data-kehudengjibh");
                            //客户等级对应的价格
                            var kehulvprice = obj.attr("data-lvprice");
                            //联系人传真
                            var contactfax = obj.attr("data-contactfax")
                            var guoji = obj.attr("data-guoji");
                            //联系人个数
                            var i = contactids.length;
                            var strArr = [];
                            strArr.push("<table cellspacing=0 cellpadding=0 border=0 width=100% class=pp-tableclass>");
                            //表头
                            strArr.push("<tr class=pp-table-title><th colspan=6>点击行-选择联系人</th></tr>");
                            strArr.push("<tr class=pp-table-title>");
                            strArr.push("<th align=center  width=7%>选择</th><th align=center  width=7%>编号</th><th align=center width=19%>联系人</th><th align=center width=15%>部门</th><th align=center width=20%>电话</th><th align=center width=20%>手机</th><th align=center width=12%>传真</th>");
                            strArr.push("</tr>");
                            //内容
                            if (obj.attr("data-contactid").length <= 0) {
                                strArr.push("<tr style='padding: 5px 5px' align=center data-name=" + $.trim(obj.parent("td").text()) + "><td align=center colspan=6>暂无联系人,无法选择</td></tr>");
                            }
                            else {
                                while (i--) {
                                    strArr.push("<tr style='padding: 5px 5px' data-contactid=" + contactids[i]); //客户单位联系人ID
                                    strArr.push(" data-value='" + obj.val() + "'"); //客户单位编号
                                    strArr.push(" data-name='" + ($.trim(obj.closest("td").text())) + "'"); //客户单位名称
                                    strArr.push(" data-contactname='" + (contactnames[i]) + "'"); //客户单位联系人名称
                                    strArr.push(" data-contactphone='" + (contactphones[i]) + "'"); //客户单位联系人电话
                                    strArr.push(" data-mobilephone='" + (mobilephones[i]) + "'"); //客户单位联系人手机
                                    strArr.push(" data-department='" + (department[i]) + "'"); //客户单位联系人部门
                                    strArr.push(" data-kehudengjibh='" + (kehudengjibh) + "'"); //客户单位联系人部门
                                    strArr.push(" data-lvprice='" + (kehulvprice) + "'");
                                    strArr.push(" data-contactfax='" + (contactfax) + "'"); //客户单位联系传真
                                    strArr.push(" data-guoji='" + (guoji) + "'"); //客户单位国家编号
                                    strArr.push(" >")
                                    strArr.push("<td align=center><input type='radio' name='contact' /></td>");
                                    strArr.push("<td align=center><a>" + (contactids.length - i) + "</a></td>");
                                    strArr.push("<td align=center><a>" + contactnames[i] + "</a></td>");
                                    strArr.push("<td align=center><a>" + department[i] + "</a></td>");
                                    strArr.push("<td align=center><a>" + contactphones[i] + "</a></td>");
                                    strArr.push("<td align=center><a>" + mobilephones[i] + "</a></td>");
                                    strArr.push("<td align=center><a>" + contactfax[i] + "</a></td>");
                                    strArr.push("<td align=center><a>" + guoji[i] + "</a></td>");
                                    strArr.push("</tr>");
                                }
                            }
                            strArr.push("</table>");
                            return strArr.join('');
                        })())
                    })
                }
                //初始化 单选复选
                if (parseInt(Boxy.queryString("IsMultiple")) === 1) {
                    $(":checkbox").remove();
                }
                else {
                    $(":radio:not('[name='contact']')").remove();
                }
            }
        }
        $(function() {
            AjaxCustomerUnitSelect.Init();
        })
    </script>

</body>
</html>

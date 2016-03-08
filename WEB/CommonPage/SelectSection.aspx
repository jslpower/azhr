<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectSection.aspx.cs"
    Inherits="Web.CommonPage.SelectSection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/css/style.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

</head>
<body style="background: #e9f4f9;">
    <div class="alertbox-outbox02">
        <table width="99%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk2"
            style="margin: 0 auto; border-collapse: collapse;">
            <tr>
                <td align="center">
                    <span style="font-size: 14px;">【请选择<%=Request.QueryString["setTitel"]%>】</span>
                    <input type="hidden" name="sModel" id="sModel" value='<%=Request.QueryString["sModel"] %>' />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater runat="server" ID="RepList">
                        <ItemTemplate>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" 
                                class="tblList" bgcolor="#FFFFFF" style="border-collapse: collapse; margin: 5px 0;">
                                <tr>
                                    <td bgcolor="#C1E5F5" align="center">
                                        <input id="select_top_<%#Container.ItemIndex %>" type="checkbox" value='<%#Eval("DepartId") %>'
                                            data-pid='<%#Eval("PrevDepartId") %>' name="Operator3" />
                                        <input id="select_top_<%#Container.ItemIndex %>" type="radio" value="<%#Eval("DepartId") %>"
                                            name="Operator3" data-pid='<%#Eval("PrevDepartId") %>' />
                                        <label for="select_top_<%#Container.ItemIndex %>">
                                            <%#Eval("DepartName")%></label>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <%#this.GetDepart(Eval("DepartId"),Container.ItemIndex)%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <div style="position: relative; height: 10px;">
        </div>
        <div class="alertbox-btn">
            <a href="javascript:void(0)" hidefocus="true" id="i_a_qingchu" style="display:none"><s class="xuanzhe"></s>清 除</a>
            <a href="javascript:void(0)" hidefocus="true" id="a_btn"><s class="xuanzhe"></s>选 择</a></div>
    </div>

    <script type="text/javascript">
        var SelectSectionPage = {
            selectValue: "",
            selectTxt: "",
            aid: '<%=Request.QueryString["id"]??"" %>',
            parentWindow: null, //要赋值的页面的window对象
            iframeID: '<%=Request.QueryString["iframeId"]%>', //当前弹窗ID
            pIframeID: '<%=Request.QueryString["pIframeId"]%>', //父级弹窗ID
            SetValue: function() {
                var valueArray = new Array();
                var txtArray = new Array();
                $(".tblList").find("input[type='checkbox']:checked,input[type='radio']:checked").each(function() {
                    valueArray.push($.trim($(this).val()));
                    txtArray.push($.trim($(this).next().html()));
                })
                SelectSectionPage.selectValue = valueArray.join(',');
                SelectSectionPage.selectTxt = txtArray.join(',');
            },
            InitSetSelect: function() {
                if (SelectSectionPage.aid) {
                    var oldValue = SelectSectionPage.parentWindow.$('#' + SelectSectionPage.aid).parent().find("input[type='hidden']");
                    if (oldValue.val().length > 0) $("#i_a_qingchu").show();
                    if (oldValue.length > 0) {                        
                        var list = oldValue.val().split(',');
                        for (var i = 0; i < list.length; i++) {
                            $("input[name='Operator3'][value='" + list[i] + "']").attr("checked", "checked");
                        }
                    }
                }
            },
            SelectCheckbox: function(id) {
                var obj = $(".tblList").find("input[type='checkbox'][data-pid='" + id + "']");
                var check = $(".tblList").find("input[type='checkbox'][value='" + id + "']").attr("checked");
                if (obj.length > 0) {
                    if (check) {
                        obj.attr("checked", "checked");
                    } else {
                        obj.removeAttr("checked");
                    }
                    obj.each(function() {
                        SelectSectionPage.SelectCheckbox($(this).val());
                    })

                }
            }
        }

        $(function() {
            $(".tblList").find(" input[type='checkbox']").click(function() {
                SelectSectionPage.SelectCheckbox($(this).val());
            })
            if ($("#sModel").val() == "2") {
                $(".tblList").find("input[type='radio']").remove();
            }
            else {
                $(".tblList").find("input[type='checkbox']").remove();
            }
            //判断是否为二级弹窗
            if (SelectSectionPage.pIframeID) {
                SelectSectionPage.parentWindow = window.parent.Boxy.getIframeWindow(SelectSectionPage.pIframeID);
            }
            else {
                SelectSectionPage.parentWindow = parent.window;
            }
            SelectSectionPage.InitSetSelect();
            $("#a_btn").click(function() {

                SelectSectionPage.SetValue();

                //根据父级是否为弹窗传值
                var data = { id: '<%=Request.QueryString["id"] %>', value: SelectSectionPage.selectValue, text: SelectSectionPage.selectTxt, hide: '<%=Request.QueryString["hide"] %>', show: '<%=Request.QueryString["show"] %>' };
                var callBackFun = '<%=Request.QueryString["callBackFun"] %>';

                if (callBackFun.indexOf('.') == -1) {
                    SelectSectionPage.parentWindow[callBackFun](data);
                } else {
                    SelectSectionPage.parentWindow[callBackFun.split('.')[0]][callBackFun.split('.')[1]](data);
                }
                parent.Boxy.getIframeDialog(SelectSectionPage.iframeID).hide();
                return false;
            });

            $("#i_a_qingchu").click(function() {
                $("input[type='checkbox']").removeAttr("checked");
                $("input[type='radio']").removeAttr("checked");
                $("#a_btn").click();
            });
        })
    </script>

</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="PathList.aspx.cs" Inherits="EyouSoft.Web.Gys.PathList" %>

<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $('#tablelist').find("a[data-contact='contact']").bt({
                contentSelector: function() {
                    return $(this).prev("span").html();
                },
                positions: ['bottom'],
                fill: '#FFF2B5',
                strokeStyle: '#D59228',
                noShadowOpts: { strokeStyle: "#D59228" },
                spikeLength: 5,
                spikeGirth: 15,
                width: 650,
                overlap: 0,
                centerPointY: 4,
                cornerRadius: 4,
                shadow: true,
                shadowColor: 'rgba(0,0,0,.5)',
                cssStyles: { color: '#00387E', 'line-height': '200%' }
            });
        })
    </script>

    <div class="mainbox">
        <div class="searchbox fixed">
            <form id="form1" method="get">
            <span class="searchT">
                <p>
                    线路区域：<select name="ddlArea" id="ddlArea" class="inputselect">
                        <%=EyouSoft.Common.UtilsCommons.GetAreaLineForSelect(EyouSoft.Common.Utils.GetInt(EyouSoft.Common.Utils.GetQueryStringValue("ddlArea")), this.SiteUserInfo.CompanyId)%>
                    </select>
                    线路名称：
                    <input type="text" size="28" maxlength="30" class="inputtext" name="txtRouteName"
                        value='<%=Request.QueryString["txtRouteName"]%>' />
                    天数：
                    <input name="txtDays" type="text" size="10" onafterpaste="this.value=this.value.replace(/\D/g,'')"
                        onkeyup="this.value=this.value.replace(/\D/g,'')" class="inputtext" value='<%=Request.QueryString["txtDays"]%>'
                        valid="isNumber" errmsg="必须是数字!" />
                    发布人：
                    <input type="text" class="inputtext" size="10" name="txtAuthor" value='<%=Request.QueryString["txtAuthor"]%>' />
                    发布日期：
                    <input type="text" class="inputtext" size="10" id="txtStartTime" value='<%=Request.QueryString["txtStartTime"]%>'
                        name="txtStartTime" onfocus="WdatePicker()" />-<input type="text" size="10" id="txtEndTime"
                            name="txtEndTime" class="inputtext" value='<%=Request.QueryString["txtEndTime"]%>'
                            onfocus="WdatePicker({minDate:'#F{$dp.$D(\'txtStartTime\')}'})" />
                    <input type="hidden" name="sl" value="<%=Request.QueryString["sl"] %>" />
                    <input type="submit" value="搜索" class="search-btn" /></p>
            </span>

            <script type="text/javascript">
                function setValue(obj, v) {
                    for (var i = 0; i < obj.options.length; i++) {
                        if (obj.options[i].value == v) {
                            obj.options[i].selected = true;
                        }
                    }
                }
                setValue($("#PathType")[0], '<%=Request.QueryString["PathType"] %>');
            </script>

            </form>
        </div>
        <div class="tablehead" id="ToolBar_1">
            <ul class="fixed">
                <asp:PlaceHolder runat="server" ID="path_add">
                    <li><s class="addicon"></s><a hidefocus="true" class="toolbar_add" href="javascript:void(0)">
                        <span>新增</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="path_edit">
                    <li><s class="updateicon"></s><a class="toolbar_update" hidefocus="true" href="javascript:void(0);">
                        <span>修改</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="path_copy">
                    <li><s class="copyicon"></s><a class="toolbar_copy" hidefocus="true" href="javascript:void(0)">
                        <span>复制</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="path_del">
                    <li><a class="toolbar_delete" hidefocus="true" href="javascript:void(0)"><s class="delicon">
                    </s><span>删除</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="path_print" Visible="false">
                    <li><a class="toolbar_cancer" hidefocus="true" href="javascript:void(0)" onclick="PrintPage('liststyle')">
                        <s class="dayin"></s><span>打印</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="path_exp">
                    <li><a class="toolbar_cancer" hidefocus="true" href="javascript:void(0)" onclick="toXls1();return false;">
                        <s class="daochu"></s><span>导出</span></a></li><li class="line"></li>
                </asp:PlaceHolder>
            </ul>
            <div class="pages">
                <cc1:ExporPageInfoSelect ID="ExporPageInfoSelect1" runat="server" />
            </div>
        </div>
        <!--列表表格-->
        <div class="tablelist-box" id="tablelist" id="tablelist">
            <table cellspacing="0" border="0" width="100%" id="liststyle">
                <tbody>
                    <tr class="odd">
                        <th class="thinputbg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            线路区域
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            线路名称
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            天数
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            发布日期
                        </th>
                        <th align="center" valign="middle" class="th-line h20">
                            发布人
                        </th>
                    </tr>
                    <asp:Repeater ID="rptList" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td align="center">
                                    <input type="hidden" name="ItemUserID" value="<%#Eval("OperatorId")%>" />
                                    <input type="checkbox" id="checkbox" name="checkbox" value="<%#Eval(" TourId") %>">
                                </td>
                                <td align="center">
                                    <%#Eval("AreaName")%>
                                </td>
                                <td align="left">
                                    <%#Eval("RouteName")%>
                                </td>
                                <td align="center">
                                    <%#Eval("TourDays") %>
                                </td>
                                <td align="center">
                                    <%#EyouSoft.Common.UtilsCommons.GetDateString(Eval("IssueTime"), ProviderToDate)%>
                                </td>
                                <td align="center">
                                    <%#Eval("Operator")%></a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div style="border: 0pt none;" class="tablehead">

            <script type="text/javascript">
                document.write(document.getElementById("ToolBar_1").innerHTML);
            </script>

        </div>
    </div>

    <script type="text/javascript" src="/Js/datepicker/WdatePicker.js"></script>

    <script type="text/javascript">
        //页面初始化必须存在方法
        $(function() {
            tableToolbar.IsHandleElse = true;
            //绑定功能按钮
            PathListPage.BindBtn();
            //当列表页面出现横向滚动条时使用以下方法
            //需要左右滚动调用格式：$("需要滚动最外层选择器").moveScroll();
            $('.tablelist-box').moveScroll();
            $(".a_UnitName").click(function() {
                var url = "/ResourceManage/Path/PathEidt.aspx?";
                var self = this;
                url += $.param({
                    sl: '<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路 %>',
                    chakan: "chakan",
                    dotype: "update",
                    id: $(self).attr("data-id")
                })
                window.location.href = url;
                return false;
            })
        })
        var PathListPage = {
            Add: function() {
                window.location.href = "/Gys/PathEidt.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路 %>";
            },
            Update: function(objsArr) {
                window.location.href = "/Gys/PathEidt.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路 %>&id=" + objsArr[0].find("input[type='checkbox']").val();
            },
            Copy: function(objsArr) {
                window.location.href = "/Gys/PathEidt.aspx?sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路 %>&id=" + objsArr[0].find("input[type='checkbox']").val() + "&Type=copy";
            },
            DelAll: function(objArr) {
                //ajax执行文件路径,默认为本页面
                var ajaxUrl = "/Gys/PathList.aspx";
                //定义数组对象
                var list = new Array();
                //遍历按钮返回数组对象
                for (var i = 0; i < objArr.length; i++) {
                    //从数组对象中找到数据所在，并保存到数组对象中
                    if (objArr[i].find("input[type='checkbox']").val() != "on") {
                        list.push(objArr[i].find("input[type='checkbox']").val());
                    }
                }
                //获取默认路径并重新拼接url（注：全局变量劲量不要改变，当常量就行）
                ajaxUrl += "?doType=delete&sl=<%=(int)EyouSoft.Model.EnumType.PrivsStructure.Menu2.资源管理_线路 %>&id=" + list.join(',');
                //执行ajax
                PathListPage.GoAjax(ajaxUrl);
            },
            GoAjax: function(url) {
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: url,
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg, function() { location.reload(); });
                        }
                        else {
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        //ajax异常--你懂得
                        tableToolbar._showMsg(tableToolbar.errorMsg);
                    }
                });
            },
            BindBtn: function() {
                $(".toolbar_add").click(function() {
                    PathListPage.Add();
                    return false;
                })
                tableToolbar.init({
                    objectName: "行!", //这个参数讲不明白，请联系柴逸宁，哈哈

                    //修改-删除-取消-复制 为默认按钮，按钮class对应  toolbar_update  toolbar_delete  toolbar_cancel  toolbar_copy即可
                    updateCallBack: function(objsArr) {
                        //修改
                        PathListPage.Update(objsArr);
                        return false;
                    },
                    deleteCallBack: function(objsArr) {
                        //删除(批量)
                        PathListPage.DelAll(objsArr);
                        return false;
                    },
                    copyCallBack: function(objsArr) {
                        //复制
                        PathListPage.Copy(objsArr)
                        return false;
                    }
                })
            }
        }
       
    </script>

</asp:Content>

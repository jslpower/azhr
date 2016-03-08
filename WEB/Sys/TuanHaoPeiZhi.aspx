<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/Front.Master" AutoEventWireup="true"
    CodeBehind="TuanHaoPeiZhi.aspx.cs" Inherits="EyouSoft.Web.Sys.TuanHaoPeiZhi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <div class="mainbox mainbox-whiteback">
        <!--列表表格-->
        <div class="tablelist-box">
            <div style="background: none #f6f6f6;" class="tablehead">
                <ul class="fixed">
                    <li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="XiTongPeiZhi.aspx?sl=<%=SL %>">
                        <span>打印配置</span></a></li>
                    <%--<li><s class="orderformicon"></s><a class="ztorderform" hidefocus="true" href="YeWuPeiZhi.aspx?sl=<%=SL %>">
                        <span>业务配置</span></a></li>--%>
                    <li><s class="orderformicon"></s><a class="ztorderform de-ztorderform" hidefocus="true"
                        href="TuanHaoPeiZhi.aspx?sl=<%=SL %>"><span>团号配置</span></a></li>
                </ul>
            </div>
            <table width="100%" align="center" id="liststyle">
                <tbody>
                    <tr>
                        <th align="center" class="th-line" colspan="3">
                            团号配置
                        </th>
                    </tr>
                    <tr>
                        <td bgcolor="#e0e9ef" align="right" rowspan="2">
                            团号配置：
                        </td>
                        <td>
                            <fieldset>
                                <legend>规则设定</legend>
                                <table cellspacing="0" cellpadding="3" bordercolor="#93b5d7" border="1" align="left"
                                    width="100%">
                                    <tbody>
                                        <tr>
                                            <td width="13%" bgcolor="#c7e0f9" align="right">
                                                团号组成：
                                            </td>
                                            <td width="87%" align="left" style="overflow: hidden; width: 650px; word-wrap: break-word">
                                                <input type="button" value="增加一项" id="btnAddOption" />
                                                <input type="button" value="减少一项" id="btnDelOption" /><br />
                                                <asp:Repeater ID="repItemList" runat="server">
                                                    <ItemTemplate>
                                                        <select name="ddl_Rule" class="inputselect">
                                                            <%#  Convert.ToInt32(GetDataItem()) == (int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.公司简称 ? "<option value=\"0\" selected=\"selected\">公司简称</option>" : "<option value=\"0\">公司简称</option>"%>
                                                            <%#  Convert.ToInt32(GetDataItem())==(int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.部门简称?"<option value=\"1\" selected=\"selected\">部门简称</option>":"<option value=\"1\">部门简称</option>"%>
                                                            <%#  Convert.ToInt32(GetDataItem())==(int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.团队类型?"<option value=\"2\" selected=\"selected\">团队类型</option>":"<option value=\"2\">团队类型</option>"%>
                                                            <%#  Convert.ToInt32(GetDataItem())==(int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.出团日期?"<option value=\"3\" selected=\"selected\">出团日期</option>":"<option value=\"3\">出团日期</option>"%>
                                                            <%#  Convert.ToInt32(GetDataItem())==(int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.分隔符?"<option value=\"4\" selected=\"selected\">分隔符</option>":"<option value=\"4\">分隔符</option>"%>
                                                            <%#  Convert.ToInt32(GetDataItem())==(int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.序列号?"<option value=\"5\" selected=\"selected\">序列号</option>":"<option value=\"5\">序列号</option>"%>
                                                            <%#  Convert.ToInt32(GetDataItem()) == (int)EyouSoft.Model.EnumType.ComStructure.OptionItemType.客户简码 ? "<option value=\"6\" selected=\"selected\">客户简码</option>" : "<option value=\"6\">客户简码</option>"%>
                                                        </select>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <select name="ddl_Rule" disabled="disabled">
                                                    <option value="5" selected="selected">序列号</option>
                                                </select>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                            <td bgcolor="#c7e0f9" align="right">
                                                实例：
                                            </td>
                                            <td>
                                                <span id="example_code">团队类型代码000-00001</span>
                                                <br />
                                                <span id="example_name">团队类型公司简称-序列号</span>
                                            </td>
                                        </tr>--%>
                                    </tbody>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <fieldset>
                                <legend>自定义编码</legend>
                                <table cellspacing="0" cellpadding="3" border="1" align="left" width="100%">
                                    <tbody>
                                        <tr id="tr_Company" style="">
                                            <td width="13%" bgcolor="#c7e0f9" align="right">
                                                公司简称：
                                            </td>
                                            <td width="87%" align="left">
                                                <asp:TextBox ID="txtCompanyCode" runat="server" CssClass=" inputtext formsize80"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr id="tr_Depart">
                                            <td bgcolor="#c7e0f9" align="right" style="height: 25px">
                                                部门简称 ：
                                            </td>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" bordercolor="#cccc99" border="1" width="100%">
                                                    <tbody>
                                                        <asp:Repeater ID="repDepartList" runat="server">
                                                            <ItemTemplate>
                                                                <%#Container.ItemIndex%3==0 || Container.ItemIndex==0?"<tr>":""%>
                                                                <td align="right">
                                                                    <%#Container.ItemIndex+1%>、<%#Eval("DepartName")%>
                                                                </td>
                                                                <td align="left">
                                                                    <input type="text" maxlength="5" size="8" class="inputtext formsize80" name="inputDepartCode" />
                                                                    <input type="hidden" value="<%#Eval("DepartId")%>" name="inputDepartId">
                                                                </td>
                                                                <%#(Container.ItemIndex+1)%3==0 && Container.ItemIndex>0 ?"</tr>":""%>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </tbody>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="tr_TourClassType">
                                            <td bgcolor="#c7e0f9" align="right">
                                                团队类型：
                                            </td>
                                            <td>
                                                <table width="100%">
                                                    <asp:Repeater ID="repTourTypeList" runat="server">
                                                        <ItemTemplate>
                                                            <%#Container.ItemIndex%4==0 || Container.ItemIndex==0?"<tr>":""%>
                                                            <td>
                                                                <%#Container.ItemIndex+1%>、<%#Eval("Text")%>
                                                                <input type="text" size="8" maxlength="8" class="inputtext formsize80" name="inputTourTypeCode" />
                                                                <input type="hidden" value="<%#Eval("Value")%>" name="inputTourTypeValue">
                                                                &nbsp;
                                                            </td>
                                                            <%#(Container.ItemIndex+1)%4==0 && Container.ItemIndex>0 ?"</tr>":""%>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr id="tr_LeaveDate">
                                            <td bgcolor="#c7e0f9" align="right">
                                                出团日期：<br />
                                            </td>
                                            <td>
                                                <input type="radio" value="0" name="rad_LeaveDateCode" id="rad_LeaveDateCode_0" checked="checked" /><label
                                                    for="rad_LeaveDateCode_0">年月日</label>
                                                <input type="radio" value="1" name="rad_LeaveDateCode" id="rad_LeaveDateCode_1" /><label
                                                    for="rad_LeaveDateCode_1">年-月-日</label>
                                            </td>
                                        </tr>
                                        <tr id="tr_SerialNumber">
                                            <td bgcolor="#c7e0f9" align="right">
                                                序列号：
                                            </td>
                                            <td>
                                                <input type="radio" value="0" name="rad_SeriesCode" id="rad_SeriesCode_0" checked="checked" /><label
                                                    for="rad_SeriesCode_0">流水号</label>
                                                <input type="radio" value="1" name="rad_SeriesCode" id="rad_SeriesCode_1" /><label
                                                    for="rad_SeriesCode_1">字母[A-Z]</label>
                                            </td>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <!--列表结束-->
        <div class="mainbox cunline fixed">
            <div class="hr_10">
            </div>
            <ul>
                <li class="cun-cy"><a id="btnSave" href="javascript:">保存</a> </li>
            </ul>
            <div class="hr_10">
            </div>
        </div>
    </div>
    </form>

    <script type="text/javascript">
        
        var TourRule = {
            DepartValueList:<%=DepartValueList %>,
            TourTypeValueList:<%=TourTypeValueList %>,
            LDateFormatList:<%=LDateFormatList %>,
            SeriesFormatList:<%=SeriesFormatList %>,
            MaxCount: "9", MinCount: "2",
            //当前Select更改前的值
            BeforeValue:-1,
             //常量 代表 公司的OptionId
            COMPANY: "0",
            //常量 代表 部门的OptionId
            DEPART: "1",
            //常量 代表 团队类型的OptionId
            TOURTYPE: "2",
            //常量 代表 出团日期OptionId
            LEAVEDATE: "3",
           //常量，代表 分隔符的OptionId
            SEPARATE: "4",
            //常量，代表 序列号的OptionId
            SERIALNUMBER: "5",
        	//客户简码
        	KEHUJIANMA:"6",
            
            GetAddCount: function() {
                return $("select[name='ddl_Rule']").length;
            },
            AddOption: function() {
                var AddCount = TourRule.GetAddCount();
                if (AddCount >= TourRule.MaxCount) {
                    alert("团号组成最多" + TourRule.MaxCount + "项！");
                    return false;
                }
                $("select[name='ddl_Rule']:last").before(TourRule.GetCloneObject);
            },
            DelOption: function() {
                var AddCount = TourRule.GetAddCount();
                if (AddCount <= TourRule.MinCount) {
                    alert("团号组成最少" + TourRule.MinCount + "项！");
                    return false;
                }
                $("select[name='ddl_Rule']:last").prev("select").remove();
            },
            GetCloneObject: function() {
                var TempObject = $("select[name='ddl_Rule']:eq(0)").clone(true);
                var ruleArray=new Array();
                $("select[name='ddl_Rule']").each(function(){
                    ruleArray.push("["+$(this).val()+"]");
                });
                var ruleArrayStr=ruleArray.join(',');
                if(ruleArrayStr.indexOf("["+TourRule.COMPANY+"]")==-1)
                {
                    TempObject.val(TourRule.COMPAN);
                }
                else if(ruleArrayStr.indexOf("["+TourRule.DEPART+"]")==-1)
                {
                    TempObject.val(TourRule.DEPART);
                }
                else if(ruleArrayStr.indexOf("["+TourRule.TOURTYPE+"]")==-1)
                {
                    TempObject.val(TourRule.TOURTYPE);
                }
                else if(ruleArrayStr.indexOf("["+TourRule.LEAVEDATE+"]")==-1)
                {
                    TempObject.val(TourRule.LEAVEDATE);
                }
            	else if (ruleArrayStr.indexOf("["+TourRule.LEAVEDATE+"]")==-1) {
                    TempObject.val(TourRule.KEHUJIANMA);
                }
                else
                {
                    TempObject.val(TourRule.SEPARATE);
                }
                return TempObject;
            },
            UnBindBtn:function(){
             $("#btnSave").text("提交中...");
             $("#btnSave").unbind("click");
             $("#btnSave").css("background-position", "0 -62px");
            },
            //按钮绑定事件
            BindBtn: function() {
                $("#btnSave").bind("click");
                $("#btnSave").text("保存");
                $("#btnSave").css("background-position", "0-31px");
                $("#btnSave").click(function() {
                    TourRule.Save();
                    return false;
                });
             },
            //提交表单
            Save: function() {
                TourRule.UnBindBtn();
                $.newAjax({
                    type: "post",
                    cache: false,
                    url: "/Sys/TuanHaoPeiZhi.aspx?dotype=save&sl=<%=SL %>",
                    data: $("#btnSave").closest("form").serialize(),
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg,function(){ window.location.href=window.location.href;});
                        } else {
                            tableToolbar._showMsg(ret.msg);
                        }
                        TourRule.BindBtn();
                    },
                    error: function() {
                        tableToolbar._showMsg("操作失败，请稍后再试!");
                        TourRule.BindBtn();
                    }
                });
           },
           Init:function(){
            $("#btnAddOption").bind("click", TourRule.AddOption);
            $("#btnDelOption").bind("click", TourRule.DelOption);
            TourRule.BindBtn();
            var DepartValueList = (TourRule.DepartValueList);
            var TourTypeValueList = (TourRule.TourTypeValueList);
            var LDateFormatList = (TourRule.LDateFormatList);
            for (var i = 0; i < DepartValueList.length; i++) {
                $("input[name='inputDepartId']").each(function(){
                    if($(this).val()==DepartValueList[i].ID)
                       $(this).prev("input").val(DepartValueList[i].Value);
                });
            }
            for (var j = 0; j < TourTypeValueList.length; j++) {
                 $("input[name='inputTourTypeValue']").each(function(){
                    if($(this).val()==TourTypeValueList[j].ID)
                        $(this).prev("input").val(TourTypeValueList[j].Value);
                });
            }
            for (var k = 0; k < LDateFormatList.length; k++) {
                $("input[name='rad_LeaveDateCode']").each(function(){
                    if($(this).val()==LDateFormatList[k].ID)
                        $(this).attr("checked","checked");
                });
            }
           	if (TourRule.SeriesFormatList!=null) {
           		$("input[name='rad_SeriesCode']").each(function() {
           			if ($(this).val() == TourRule.SeriesFormatList.ItemId)
           				$(this).attr("checked", "checked");
           		});
           	}
            $("select[name='ddl_Rule']").focus(function(){
                TourRule.BeforeValue=$(this).val();
            });
            $("select[name='ddl_Rule']").change(function(){
                var changeValue=$(this).val();
                if(changeValue!=TourRule.SEPARATE){
                    if ($(this).prevAll("select[value='" + changeValue + "']").length > 0
					    || $(this).nextAll("select[value='" + changeValue + "']").length > 0
					    || changeValue == TourRule.SERIALNUMBER) {
						    $(this).val(TourRule.BeforeValue);
						    return false;
                        }
                 }
            });
           }
        };
        $(function() {
            TourRule.Init();
        });
    </script>

</asp:Content>

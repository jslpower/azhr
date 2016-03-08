<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/Front.Master"
    CodeBehind="SendMessage.aspx.cs" Inherits="Web.SmsCenter.SendMessage" %>

<%@ Register Src="~/UserControl/SysTop.ascx" TagName="SysTop" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- InstanceBeginEditable name="EditRegion3" -->

    <script src="/Js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <div onclick="show_menuC()" id="Mobile" class="center_btn">
        <a hidefocus="true" href="#">
            <!--<img src="../images/center_l.gif" />-->
        </a>
    </div>
    <div class="mainbox">
        <uc1:SysTop ID="SysTop1" runat="Server"></uc1:SysTop>
        <form id="form" runat="server">
        <div class="sms-addbox">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="alertboxbk3">
                <tr>
                    <th width="20%" align="right" class="addtableT">
                        发送内容：
                    </th>
                    <td width="80%" bgcolor="#FFFFFF">
                        <textarea id="txtSendContent" name="txtSendContent" class="inputtext formsize600"
                            style="height: 80px;" onkeyup="SendSms.fontNum(this);"></textarea><br />
                        <span class="fontred">*注：移动、联通按每条短信70字计费，小灵通按每条短信45字计费。</span>
                        <br />
                        <a href="javascript:" onclick="SendSms.autoSms();">
                            <img src="../images/open-dx.gif" />
                            <b>自动填写发送内容</b> </a>
                        <br />
                        统计: 共 <span id="sNum" class="red">0</span>字。 移动分为 <span id="ysNum" class="red">0</span>
                        条发送， 联通分为 <span id="lsNum" class="red">0</span>条发送， 小灵通分为 <span id="xsNum" class="red">
                            0</span> 条发送<br />
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        手机号码：
                    </th>
                    <td bgcolor="#FFFFFF">
                        <textarea id="txtSendMobile" name="txtSendMobile" class="inputtext formsize600" style="height: 80px;"></textarea><br />
                        <span class="fontred">*注：输入号码时在号码与号码之间必须用“，”号隔开</span>
                        <br />
                        <img src="../images/gr_ckxq.gif" />
                        <a href="javascript:" onclick="SendSms.putInCust();">导入号码</a><br />
                        <%--<img src="../images/gr_ckxq.gif" />
                        <a href="javascript:" onclick="SendSms.putInFile();">从文件导入号码</a><br />--%>
                        <%--（只能导入格式为.xls和.txt的文件，并且xls文件第一列必须是序号，txt文件必须一个号码一行）--%>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        发送方式：
                    </th>
                    <td bgcolor="#FFFFFF">
                        <select name="selSendType" id="selSendType" onchange="return SendSms.selSendType(this)"
                            class="inputselect">
                            <option value="0" selected="selected">直接发送</option>
                            <option value="1">定时发送</option>
                        </select>
                        <input type="text" id="txtSendTime" name="txtSendTime" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm'})"
                            class="inputtext formsize150" style="display: none;" />
                        定时短信不能取消
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        <strong>短信通道：</strong>
                    </th>
                    <td align="left" bgcolor="#FFFFFF">
                        <asp:DropDownList ID="ddlSelChannel" runat="server" CssClass="inputselect">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th align="right" class="addtableT">
                        短信签名：
                    </th>
                    <td bgcolor="#FFFFFF">
                        <%--<asp:CheckBox ID="chkSender" runat="server" />--%>
                        <asp:TextBox ID="txtSender" runat="server" CssClass=" inputtext formsize120" ReadOnly="true" BackColor="#dadada"></asp:TextBox>
                        <%--打勾后短信内容将包含发信人信息--%>（短信签名占用内容字数）
                    </td>
                </tr>
            </table>
            <table width="60%" border="0" cellspacing="0" cellpadding="0" style="margin: 5px auto;">
                <tr>
                    <td colspan="2">
                        <div class="mainbox cunline fixed" style="margin-left: 0;">
                            <ul>
                                <asp:PlaceHolder ID="PSendBtn" runat="server">
                                    <li class="cun-cy"><a href="javascript:" id="btnSave">发 送</a></li></asp:PlaceHolder>
                            </ul>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Literal ID="remainNum" runat="server" Visible="false"></asp:Literal><br />
                        <a href="javascript:;" onclick="return SendSms.recharge();" <%=showPay %>>
                            <img src="/images/dx-chongzhi.gif" /></a>
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>

    <script src="/js/datepicker/WdatePicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        var SendSms = {
            //发送类别
            selSendType: function(tar) {
                if ($(tar).val() == "0") {
                    $("#txtSendTime").hide();
                }
                else {
                    $("#txtSendTime").show();
                }
            },
            BtnBind: function() {
                $("#btnSave").text("发送");
                $("#btnSave").bind("click");
                $("#btnSave").css("background-position", "0 0");
                $("#btnSave").click(function() {
                    SendSms.sendMess();
                });
            },
            UnBtnBind: function() {
                 $("#btnSave").css("background-position", "0 -62px");
                $("#btnSave").unbind("click");
                $("#btnSave").text("正在发送中...");
            },
            //打开弹窗
            openDialog: function(p_url, p_title, p_width, p_height) {
                Boxy.iframeDialog({ title: p_title, iframeUrl: p_url, width: p_width, height: p_height }); return false;
            },
            //从文件导入号码
            putInFile: function() {
                return SendSms.openDialog("/ImportSource/ImportPage.aspx?type=3", "从文件导入号码", "800px", "500px");
            },
            //从客户导入号码
            putInCust: function() {
                return SendSms.openDialog("DrLxr.aspx", "导入号码", "800px", "480px");
            },
            //自动填写发送内容
            autoSms: function() {
                return SendSms.openDialog("/SmsCenter/SmsMessageSel.aspx", "选择短信", "750px", "500px");
            },
            //统计字数
            fontNum: function(tar) {
                var isLong=false;
                var cmsLen = $(tar).val().length; //短信字数
                var cmsLeny = Math.ceil(cmsLen / (isLong ? 210 : 70)); //移动联通条数
                var cmsLenx = Math.ceil(cmsLen / (isLong ? 210 : 45)); //小灵通条数
                $("#sNum").html(cmsLen);
                $("#ysNum,#lsNum").html(cmsLeny);
                $("#xsNum").html(cmsLenx);
            },
            //选中发信人
            selSender: function(tar) {
                if ($(tar).attr("checked")) { $("#txtSendContent").val($("#txtSendContent").val() + "发信人: " + $("#<%=txtSender.ClientID%>").val()); }
            },
            //发送短信
            sendMess: function() {
                if ($.trim($("#txtSendContent").val()) == "") {
                    tableToolbar._showMsg("请填写短信内容！"); return false;
                }
                if($("#selSendType").val()=="1"){
                    if($("#txtSendTime").val()==""){
                        tableToolbar._showMsg("请输入发送时间！"); return false;
                    }
                }
                var mobiles = SendSms.validMobile(); //验证手机号码
                if (mobiles.length < 1) {
                    tableToolbar._showMsg("请填写手机号码！");
                    return false;
                }
                if (mobiles == "0") {
                    return false;
                }
                SendSms.UnBtnBind();
                $.newAjax({
                    type: "post",
                    dataType: "json",
                    url: "/SmsCenter/SendMessage.aspx?dotype=save&sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>",
                    data: $("#<%=form.ClientID%>").serialize(),
                    cache: false,
                    dataType: "json",
                    success: function(ret) {
                        //ajax回发提示
                        if (ret.result == "1") {
                            tableToolbar._showMsg(ret.msg);
                            SendSms.BtnBind();
                        } else {
                            SendSms.BtnBind();
                            tableToolbar._showMsg(ret.msg);
                        }
                    },
                    error: function() {
                        SendSms.BtnBind();
                        tableToolbar._showMsg("服务器忙，请稍后再试！");
                    }
                });
            },
            recharge: function() {
                return SendSms.openDialog("/SmsCenter/AccountRecharge.aspx?sl=<%=EyouSoft.Common.Utils.GetQueryStringValue("sl") %>", "账户充值", "620px", "350px");
            },
            //验证手机号码
            validMobile: function() {
                var dataMobile = [];
                if ($("#txtSendMobile").val() != "")
                    dataMobile = $.trim($("#txtSendMobile").val().replace(/，/gi, ",")).split(","); //导入时的手机号
                else
                    return dataMobile;
                var b = new Array();
                var repeatM = new Array(); //重复手机号
                var errM = new Array(); //格式错误手机号
                var dataTelRight = new Array(); //正确的手机号
                //循环验证手机号是否重复和号码格式
                for (var i = 0, len = dataMobile.length; i < len; i++) {
                    //验证手机格式
                    if (!/^(13|15|18|14)\d{9}$/.test(dataMobile[i]) && !/0\d{9,11}$/.test(dataMobile[i]) ) {
                        errM.push(dataMobile[i]); //压入格式错误的手机号
                        dataMobile[i] = null;
                        //设置当前为null
                    }
                    if (dataMobile[i] != null) {
                        if (b[dataMobile[i]] == null) {
                            b[dataMobile[i]] = i + 1;
                        }
                        else {
                            repeatM.push(dataMobile[i]); //重复号码压入重复数组
                            dataMobile[i] = null; //设置当前为null
                        }
                    }
                    if (dataMobile[i] != null) {//如果不为空则压入正确的号码数组
                        dataTelRight.push(dataMobile[i]);
                    }
                }
                var mobileMess = ""; //提示消息
                if (repeatM.length > 0)
                    mobileMess += "\n号码重复:" + repeatM.toString();
                if (errM.length > 0)
                    mobileMess += "\n错误号码:" + errM.toString();
                if (mobileMess != "") {
                    tableToolbar._showMsg("手机号格式错误！\n" + mobileMess)
                    return "0";
                }
                else {
                    return dataMobile; //无任何错误时写入
                }
            }
        }
        $(function() {
            $("#btnSave").click(function() {
                SendSms.sendMess();
            });
        });
    </script>

</asp:Content>

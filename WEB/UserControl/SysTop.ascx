<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SysTop.ascx.cs" Inherits="Web.UserControl.SysTop" %>
<div class="tablehead border-bot">
    <ul class="fixed" sign="fixed">
        <li><a id="renwu" href="/smscenter/renwu.aspx?sl=57" class="ztorderform"><s class="orderformicon">
        </s><span>短信任务</span></a></li>
        <li><a id="sendmessage" href="/SmsCenter/SendMessage.aspx?sl=57" class="ztorderform">
            <s class="orderformicon"></s><span>发送短信</span></a></li>
        <li><a id="sendhistory" href="/SmsCenter/SendHistory.aspx?sl=57" class="ztorderform">
            <s class="orderformicon"></s><span>发送历史</span></a></li>
        <li><a id="smslist" href="/SmsCenter/SmsList.aspx?sl=57" class="ztorderform"><s class="orderformicon">
        </s><span>常用短信</span></a></li>
        <li><a id="smssetting" href="/SmsCenter/SmsSetting.aspx?sl=57" class="ztorderform"><s
            class="orderformicon"></s><span>短信设置</span></a></li>
        <li><a id="accountinfo" href="/SmsCenter/AccountInfo.aspx?sl=57" class="ztorderform">
            <s class="orderformicon"></s><span>帐户信息</span></a></li>
    </ul>

    <script type="text/javascript">
        $("[sign=fixed]").find("li").each(function() {
            if ($(this).find("a").attr("id") == '<%=str%>')
                $(this).find("a").addClass("de-ztorderform");
            else
                $(this).find("a").removeClass("de-ztorderform");
        });
    </script>

</div>

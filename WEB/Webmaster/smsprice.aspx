<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="smsprice.aspx.cs" Inherits="Web.Webmaster.smsprice" MasterPageFile="~/Webmaster/mpage.Master" %>

<%@ MasterType VirtualPath="~/Webmaster/mpage.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="Scripts" ID="ScriptsContent">
<script type="text/javascript">
    //获取通道select html，v:默认选中的通道value
    function getChannelHtml(v) {
        if (channels.length == 0) return;
        var s = [];
        s.push('<select name="txtChannel">');
        for (var i = 0; i < channels.length; i++) {
            s.push('<option value="' + channels[i].Index + '" ' + (channels[i].Index == v ? 'selected="selected"' : '') + '>' + channels[i].Name + '</option>');
        }
        s.push('</select>');

        return s.join('');
    }

    //初始化价格
    function initPrices() {
        if (prices.length == 0) {
            var s = [];
            s.push('<tr><td style="height:24px;color:#ff0000">当前未设置任何短信通道价格，不设置短信通道价格的系统将不能使用系统提供的发送短信功能。</td></tr>');
            $("#trPriceAfter").before(s.join(""));

            addPrice(0, 0.1, true);
        } else {
            for (var i = 0; i < prices.length; i++) {
                addPrice(prices[i].ChannelIndex, prices[i].Price, i == 0);
            }
        }

        piframeResize()
    }

    //添加一行价格,channel:选中的通道value,price:价格,isaddbtn:是否添加按钮
    function addPrice(channel, price, isaddbtn) {
        var s = [];
        s.push('<tr><td style="height:24px;">');
        s.push('短信通道：');
        s.push(getChannelHtml(channel));
        s.push('&nbsp;&nbsp;&nbsp;&nbsp;短信价格（单位：元）：');
        s.push('<input name="txtPrice" type="text" value="' + price + '" class="input_text" style="width:80px;">');

        if (isaddbtn) {
            s.push('&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0)" onclick="addPrice(0,0.1,false)">添加</a>');
        } else {
        s.push('&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0)" onclick="delPrice(this)">删除</a>');
        }
        s.push('</td></tr>');
        $("#trPriceAfter").before(s.join(""));

        piframeResize()
    }

    //删除一行价格
    function delPrice(obj) {
        $(obj).parent().parent().remove();
    }

    function WebForm_OnSubmit_Validate() {
        var _obj = {};
        var _has = false;

        $("select[name='txtChannel']").each(function() {
            var _s = $(this).find("option:checked").text();
            if (_s == '') return;
            if (_obj[_s] == 'undefined' || _obj[_s] == undefined) _obj[_s] = 1;
            else _obj[_s] = _obj[_s] + 1;
        });

        for (var item in _obj) {
            if (_obj[item] > 1) {
                alert("通道：[" + item + "] 重复");
                return false;
            }

            _has = true;
        }

        if (!_has) {
            alert("至少要填写一个通道信息");
            return false;
        }
    }

    $(document).ready(function() {
        initPrices();
    });
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageTitle" ID="TitleContent">
    子系统短信价格设置-<asp:Literal runat="server" ID="ltrSysName"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageContent" ID="MainContent">
    <table cellpadding="2" cellspacing="1" style="font-size: 12px; width: 100%;">        
        <!--价格区域-->
        <tr id="trPriceAfter">
            <td class="trspace">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="保存通道价格信息" OnClientClick="return WebForm_OnSubmit_Validate()" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="PageRemark" ID="RemarkContent">
    <ul class="decimal">
        <li>不设置短信通道价格的系统将不能使用系统提供的发送短信功能。</li>
        <li>通道价格<=0时，系统将自动默认通道价格为0.1元。</li>
    </ul>
</asp:Content>

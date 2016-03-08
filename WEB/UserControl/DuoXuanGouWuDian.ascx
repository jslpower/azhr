<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DuoXuanGouWuDian.ascx.cs" Inherits="EyouSoft.Web.UserControl.DuoXuanGouWuDian" %>

<input type="hidden" name="txtGWDID" id="txtGWDID" />
<input type="text" name="txtGWDNAME" id="txtGWDNAME" class="input-txt" readonly="readonly"
    style="background: #DADADA" />
<a class="xuanyong" href="javascript:void(0)" id="i_a_dxgwd">&nbsp;</a>

<script type="text/javascript">
    var dxgwd = {
        //选用按钮事件
        xuanYong: function() {
            top.Boxy.iframeDialog({ iframeUrl: "/commonpage/DuoXuanGouWuDian.aspx", title: "购物店", modal: true, width: "800px", height: "500px" });
        }
        , //设置值 _v:{id:"",name:""}
        setValue: function(_v) {
            $("#txtGWDID").val(_v.id);
            $("#txtGWDNAME").val(_v.name).attr("title", _v.name);
        },
        //获取值 return:{id:"",name:""}
        getValue: function() {
            return { id: $.trim($("#txtGWDID").val()), name: $.trim($("#txtGWDNAME").val()) };
        }
    };

    $(document).ready(function() {
        $("#i_a_dxgwd").click(function() { dxgwd.xuanYong(); });
    });
    
</script>

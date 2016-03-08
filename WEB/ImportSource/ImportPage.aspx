<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportPage.aspx.cs" Inherits="EyouSoft.Web.ImportSource.ImportPage" %>
<%@ Register Assembly="ControlLibrary" Namespace="Adpost.Common.ExporPage" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="/Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="/Js/import.js" type="text/javascript"></script>

</head>
<body style="background-color: #e9f4f9">

    <script type="text/javascript">
        var ImportPage = {
            array: [],
            CallBackFun: '<%=Request.QueryString["callbackfun"] %>',
            ParentBox: '<%=Request.QueryString["box"] %>' == '' ? null : parent.$('#<%=Request.QueryString["box"] %>'),
            Type: '<%=Request.QueryString["type"] %>',
            SelectAll:function(){
                var that = this;
                $("#cbxCheckAll").click(function() {
                    $("#tablelist").find("input[type='checkbox']").attr("checked", this.checked);
                })
            }
        }
        function loadexcel(array) {
            loadXls.init(array, "#tablelist", "");
        }
        $(function() {
            ImportPage.SelectAll();

            $("#btnSelect").click(function() {

                ImportPage.array = loadXls.bindIndex();

                if (ImportPage.array.length == 0) {

                    parent.tableToolbar._showMsg("请选择数据!");
                    return false;
                } else {
                    switch (ImportPage.Type) {
                        case "1": //非出境游客列表
                            // CerateType1(ImportPage.array);
                            CerateHx(ImportPage.array);
                            break;
                        case "2": //出境游客列表
                            CerateType2(ImportPage.array);
                            break;
                        case "3": //生成短信发送的手机号码列表
                            GenerateMobileList(ImportPage.array);
                            break;
                        case "4": //生成客户名单列表
                            CerateCustList(ImportPage.array);
                            BackFun();
                            break;
                            case "5"://生成单项客户名单
                            CreateDanXiang(ImportPage.array);
//                            BackFun();
                            break;
                    }
                }
            })
        })
        
        //执行客户名单回调方法-*start*-
        function BackFun(obj) {
            var data = { callBack: Boxy.queryString("callback") }
            if (data.callBack != null && data.callBack.length > 0) {
                window.parent[data.callBack]();
            }
        }
        //执行客户名单回调方法-*end*-

        //生成短信发送的手机号码列表--*Start*--
        function GenerateMobileList(array) {
            var txtSendMobile = parent.document.getElementById("txtSendMobile");
            var rowN = { tel: 0 };
            for (var i = 0; i < array[0].length; i++) {
                if (array[0][i] == "手机号码") {
                    rowN.tel = i;
                }
            }

            var MobileList = new Array();
            for (var i = 1; i < array.length; i++) {
                if (array[i]) {
                    MobileList.push(array[i][rowN.tel]);
                }
            }
            if (txtSendMobile.value != "") {
                txtSendMobile.value += "," + MobileList.join(",");
            }
            else {
                txtSendMobile.value = MobileList.join(",");
            }
            parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
        }
        //生成短信发送的手机号码列表--*End*--

        //非出境游客--Start

        function CerateType1(array) {
            var rowN = { name: 0, type: 0, cardType: 0, cardNo: 0, sex: 0, tel: 0, remarks: 0, msgL: 0, msgB: 0 };
            if (array[0] != undefined) {
                for (var i = 0; i < array[0].length; i++) {
                    switch (array[0][i]) {


                        case "姓名": rowN.name = i; break;
                        case "类型": rowN.type = i; break;
                        case "证件名称": rowN.cardType = i; break;
                        case "证件号码": rowN.cardNo = i; break;
                        case "性别": rowN.sex = i; break;
                        case "联系方式": rowN.tel = i; break;
                        case "保险": rowN.remarks = i; break;
                        case "备注": rowN.msgL = i; break;
                        case "短信通知": rowN.msgB = i; break;
                    }
                }
            }
            if (ImportPage.ParentBox.length > 0) {

                for (var i = 1; i < array.length; i++) {
                    if (array[i]) {
                        var tr = ImportPage.ParentBox.find("tr[class='tempRow']").eq(0).clone(true);
                        tr.find("input[type='hidden']").val("");
                        tr.find("input[name='txt_TravelControl_Name']").val(array[i][rowN.name]).attr("id", parseInt(Math.random(99999) * 100000)); ;
                        tr.find("input[name='txt_TravelControl_Prove']").val(array[i][rowN.cardNo]).attr("id", parseInt(Math.random(99999) * 100000));
                        tr.find("input[name='txt_TravelControl_Phone']").val(array[i][rowN.tel]).attr("id", parseInt(Math.random(99999) * 100000));
                        tr.find("input[name='txt_TravelControl_Remarks']").val(array[i][rowN.msgL]);
                        tr.find("a[class='baoxian']").html('<img border="0" src="/images/y-cuohao.gif">');
                        tr.find("select[name='slt_TravelControl_PeopleType']").find("option").each(function() { if ($(this).text() == array[i][rowN.type]) { $(this).attr("selected", "selected") } });
                        tr.find("select[name='slt_TravelControl_ProveType']").find("option").each(function() { if ($(this).text() == array[i][rowN.cardType]) { $(this).attr("selected", "selected") } });
                        tr.find("select[name='slt_TravelControl_ProveType']").change();
                        tr.find("select[name='Certificates']").change();
                        tr.find("select[name='slt_TravelControl_UserSex']").find("option").each(function() { if ($(this).text() == array[i][rowN.sex]) { $(this).attr("selected", "selected") } });


                        tr.find(":checkbox[value='1']").removeAttr("checked");
                        tr.find(":checkbox[value='2']").removeAttr("checked");

                        if (array[i][rowN.msgB] == "出团") {
                            tr.find(":checkbox[value='1']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControl_LeaveMsg']").val("1")
                        }
                        else if (array[i][rowN.msgB] == "回团") {
                            tr.find(":checkbox[value='2']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControl_BackMsg']").val("1")
                        }
                        else if (array[i][rowN.msgB] == "出团回团") {
                            tr.find(":checkbox[value='1']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControl_LeaveMsg']").val("1")
                            tr.find(":checkbox[value='2']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControl_BackMsg']").val("1")
                        }

                    }
                    ImportPage.ParentBox.append(tr);
                }
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
            }
        }
        //非出境游客--End


        //出境游客--Start
        function CerateType2(array) {
            var rowN = { name: 0, cardType: 0, proveNo: 0, cardNo: 0, sex: 0, type: 0, tel: 0, useDate: 0, isHandle: 0, remarks: 0, visaStatus: 0, msgB: 0 };
            for (var i = 0; i < array[0].length; i++) {
                switch (array[0][i]) {

                    case "姓名": rowN.name = i; break;
                    case "证件类型": rowN.cardType = i; break;
                    case "身份证号码": rowN.proveNo = i; break;
                    case "证件号码": rowN.cardNo = i; break;
                    case "性别": rowN.sex = i; break;
                    case "类型": rowN.type = i; break;
                    case "联系方式": rowN.tel = i; break;
                    case "有效期": rowN.useDate = i; break;
                    case "办理": rowN.isHandle = i; break;
                    case "备注": rowN.remarks = i; break;
                    case "签证状态": rowN.visaStatus = i; break;
                    case "短信通知": rowN.msgB = i; break;
                }
            }
            if (ImportPage.ParentBox.length > 0) {
                for (var i = 1; i < array.length; i++) {
                    if (array[i]) {
                        var tr = ImportPage.ParentBox.find("tr[class='tempRow']").eq(0).clone(true);
                        tr.find("input[type='hidden']").val("");
                        tr.find("input[name='txt_TravelControlS_TreavelID_NameCn']").val(array[i][rowN.name].split(",")[0]).attr("id", parseInt(Math.random(99999) * 100000)); ;
                        tr.find("input[name='txt_TravelControlS_TreavelID_NameEn']").val(array[i][rowN.name].split(",")[1]).attr("id", parseInt(Math.random(99999) * 100000)); ;

                        tr.find("input[name='slt_TravelControlS_Sfz']").val(array[i][rowN.proveNo]).attr("id", parseInt(Math.random(99999) * 100000)); ;
                        tr.find("input[name='txt_TravelControlS_CardNum']").val(array[i][rowN.cardNo]);
                        tr.find("input[name='txt_TravelControlS_ContactTel']").val(array[i][rowN.tel]).attr("id", parseInt(Math.random(99999) * 100000)); ;
                        tr.find("input[name='txt_TravelControlS_ValidDate']").val(array[i][rowN.useDate]);
                        tr.find("textarea[name='txt_TravelControlS_Remarks']").val(array[i][rowN.remarks]);


                        tr.find("select[name='slt_TravelControlS_ProveType']").find("option").each(function() { if ($(this).text() == array[i][rowN.cardType]) { $(this).attr("selected", "selected") } });
                        tr.find("select[name='slt_TravelControlS_Sex']").find("option").each(function() { if ($(this).text() == array[i][rowN.sex]) { $(this).attr("selected", "selected") } });
                        tr.find("select[name='slt_TravelControlS_PeoType']").find("option").each(function() { if ($(this).text() == array[i][rowN.type]) { $(this).attr("selected", "selected") } });
                        tr.find("select[name='slt_TravelControlS_State']").find("option").each(function() { if ($(this).text() == array[i][rowN.visaStatus]) { $(this).attr("selected", "selected") } });


                        tr.find(":checkbox[value='3']").removeAttr("checked");
                        if (array[i][8] == "是") {
                            tr.find(":checkbox[value='3']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControlS_IsBan']").val("1");
                        }
                        else {

                        }


                        tr.find(":checkbox[value='1']").removeAttr("checked");
                        tr.find(":checkbox[value='2']").removeAttr("checked");

                        if (array[i][rowN.msgB] == "出团") {
                            tr.find(":checkbox[value='1']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControlS_LeaveMsg']").val("1")
                        }
                        else if (array[i][rowN.msgB] == "回团") {
                            tr.find(":checkbox[value='2']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControlS_BackMsg']").val("1")
                        }
                        else if (array[i][rowN.msgB] == "出团回团") {
                            tr.find(":checkbox[value='1']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControlS_LeaveMsg']").val("1")
                            tr.find(":checkbox[value='2']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControlS_BackMsg']").val("1")
                        }
                    }
                    ImportPage.ParentBox.append(tr);
                }
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
            }
        }
        //出境游客--End

        //导入客户名单--Start
        function CerateCustList(array) {
            var rowN = { name: 0, type: 0, cardType: 0, cardNo: 0, sex: 0, tel: 0 };
            for (var i = 0; i < array[0].length; i++) {
                switch (array[0][i]) {

                    case "姓名": rowN.name = i; break;
                    case "类型": rowN.type = i; break;
                    case "证件名称": rowN.cardType = i; break;
                    case "证件号码": rowN.cardNo = i; break;
                    case "性别": rowN.sex = i; break;
                    case "联系电话": rowN.tel = i; break;
                }
            }

            if (ImportPage.ParentBox.length > 0) {

                for (var i = 1; i < array.length; i++) {
                    if (array[i]) {
                        var tr = ImportPage.ParentBox.find("tr[class='showlist']").eq(0).clone(true);

                        tr.find("input[name='CustomName']").val(array[i][rowN.name]);
                        if (array[i][rowN.cardType] == "身份证") {
                            tr.find("input[name='CertificatesNum']").val(array[i][rowN.cardNo]).attr("valid", "isIdCard").attr("errmsg", "请输入正确的身份证号码!");
                        }
                        else {
                            tr.find("input[name='CertificatesNum']").val(array[i][rowN.cardNo]);
                        }


                        tr.find("input[name='ContactTel']").val(array[i][rowN.tel]);
                        tr.find("input[type='hidden']").val("");

                        tr.find("select[name='CustomType']").find("option").each(function() { if ($(this).text() == array[i][rowN.type]) { $(this).attr("selected", "selected") } });
                        tr.find("select[name='Certificates']").find("option").each(function() { if ($(this).text() == array[i][rowN.cardType]) { $(this).attr("selected", "selected") } });
                        tr.find("select[name='Sex']").find("option").each(function() { if ($(this).text() == array[i][rowN.sex]) { $(this).attr("selected", "selected") } });

                    }
                    ImportPage.ParentBox.append(tr);
                }
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
            }
        }
        //导入客户名单--End

//        单项客户导入
        function CreateDanXiang(array) {
            var rowN = { name: 0, type: 0, cardType: 0, cardNo: 0, sex: 0, tel: 0,qianfadate:"",CardValidDate:"",birthday:"",qianfadi:"",LiCheng:"",remark:"" };
            if(typeof array[0] != "undefined")
            {
            for (var i = 0; i < array[0].length; i++) {
                switch (array[0][i]) {

                    case "姓名": rowN.name = i; break;
                    case "类型": rowN.type = i; break;
                    case "证件类型": rowN.cardType = i; break;
                    case "证件号码": rowN.cardNo = i; break;
                    case "性别": rowN.sex = i; break;
                    case "联系电话": rowN.tel = i; break;
                    case "签发日期": rowN.qianfadate = i; break;
                    case "有效期": rowN.CardValidDate = i; break;
                    case "出生日期": rowN.birthday = i; break;
                    case "签发地": rowN.qianfadi = i; break;
                    case "里程积分": rowN.LiCheng = i; break;
                    case "备注": rowN.remark = i; break;
                }
            }
            }

            if (ImportPage.ParentBox.length > 0&&array.length>1) {

                for (var i = 1; i < array.length; i++) {
                    if (array[i]) {
                        var tr1 = ImportPage.ParentBox.find("tr[class='showlist']").eq(0).clone();
                        var tr2 = ImportPage.ParentBox.find("tr[class='showlist']").eq(1).clone();

                        tr1.find("input[name='CustomName']").val(array[i][rowN.name]);
                        tr1.find("select[name='CustomType']").find("option").each(function() { if ($(this).text() == array[i][rowN.type]) { $(this).attr("selected", "selected") } });
                        tr1.find("select[name='Certificates']").find("option").each(function() { if ($(this).text() == array[i][rowN.cardType]) { $(this).attr("selected", "selected") } });
                        tr1.find("input[name='CertificatesNum']").val(array[i][rowN.cardNo]);
                        tr1.find("input[name='QianFaDate']").val(array[i][rowN.qianfadate]);
                        tr1.find("input[name='CardValidDate']").val(array[i][rowN.CardValidDate]);

                        tr2.find("input[name='Birthday']").val(array[i][rowN.birthday]);
                        tr2.find("input[name='qianfadi']").val(array[i][rowN.qianfadi]);
                        tr2.find("select[name='Sex']").find("option").each(function() { if ($(this).text() == array[i][rowN.sex]) { $(this).attr("selected", "selected") } });
                        tr2.find("input[name='ContactTel']").val(array[i][rowN.tel]);
                        tr2.find("input[name='licheng']").val(array[i][rowN.LiCheng]);
                        tr2.find("input[name='remark']").val(array[i][rowN.remark]);
                        tr2.find("input[type='hidden']").val("");
                    }
                    ImportPage.ParentBox.append(tr1);
                    ImportPage.ParentBox.append(tr2);
                }
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
            }else{
                $("#tablelist").find("input[type='checkbox']:checked").not("#cbxCheckAll").each(function(){
                    var _tr=$(this).closest("tr");
                    var _td=$(_tr).children("td");
                    if(_td){
                        var tr1 = ImportPage.ParentBox.find("tr[class='showlist']").eq(0).clone();
                        var tr2 = ImportPage.ParentBox.find("tr[class='showlist']").eq(1).clone();

                        tr1.find("input[name='CustomName']").val($(_td).eq(1).html());
                        tr1.find("select[name='CustomType']").find("option").each(function() { if ($(this).text() == "") { $(this).attr("selected", "selected") } });
                        tr1.find("select[name='Certificates']").find("option").each(function() { if ($(this).text() == $(_td).eq(3).html()) { $(this).attr("selected", "selected") } });
                        tr1.find("input[name='CertificatesNum']").val($(_td).eq(4).html());
                        tr1.find("input[name='QianFaDate']").val($(_td).eq(5).html());
                        tr1.find("input[name='CardValidDate']").val($(_td).eq(6).html());

                        tr2.find("input[name='Birthday']").val($(_td).eq(7).html());
                        tr2.find("input[name='qianfadi']").val($(_td).eq(8).html());
                        tr2.find("select[name='Sex']").find("option").each(function() { if ($(this).text() == $(_td).eq(2).html()) { $(this).attr("selected", "selected") } });
                        tr2.find("input[name='ContactTel']").val($(_td).eq(9).html());
                        tr2.find("input[name='licheng']").val("");
                        tr2.find("input[name='remark']").val($(_td).eq(10).html());
                        tr2.find("input[type='hidden']").val("");
                    }
                    ImportPage.ParentBox.append(tr1);
                    ImportPage.ParentBox.append(tr2);
                    parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
                })
            }
        }

        //浙江海峡--begin
        function CerateHx(array) {
            var rowN = { name: 0, sex: 0, type: 0, cardType: 0, cardNo: 0, effectTime: 0, cardId: 0, tel: 0, birthday: 0, remarks: 0, msgB: 0 };
            for (var i = 0; i < array[0].length; i++) {
                switch (array[0][i]) {

                    case "姓名": rowN.name = i; break;
                    case "性别": rowN.sex = i; break;
                    case "类型": rowN.type = i; break;
                    case "证件类型": rowN.cardType = i; break;
                    case "证件号码": rowN.cardNo = i; break;
                    case "有效期": rowN.effectTime = i; break;
                    case "身份证号码": rowN.proveNo = i; break;
                    case "联系方式": rowN.tel = i; break;
                    case "出生日期": rowN.birthday = i; break;
                    case "备注": rowN.remarks = i; break;
                    case "短信通知": rowN.msgB = i; break;

                }
            }
            if (ImportPage.ParentBox.length > 0) {
                for (var i = 1; i < array.length; i++) {
                    if (array[i]) {
                        var tr = ImportPage.ParentBox.find("tr[class='tempRow']").eq(0).clone(true);
                        tr.find("input[type='hidden']").val("");
                        tr.find("input[name='txt_TravelControl_Name']").val(array[i][rowN.name].split(",")[0]).attr("id", parseInt(Math.random(99999) * 100000)); ;
                        tr.find("select[name='slt_TravelControl_UserSex']").find("option").each(function() { if ($(this).text() == array[i][rowN.sex]) { $(this).attr("selected", "selected") } });

                        tr.find("select[name='slt_TravelControl_PeopleType']").find("option").each(function() { if ($(this).text() == array[i][rowN.type]) { $(this).attr("selected", "selected") } });


                        tr.find("select[name='slt_TravelControl_ProveType']").find("option").each(function() { if ($(this).text() == array[i][rowN.cardType]) { $(this).attr("selected", "selected") } });
                        tr.find("input[name='txt_TravelControl_CardNum']").val(array[i][rowN.cardNo]);
                        tr.find("input[name='txt_TravelControl_EffectTime']").val(array[i][rowN.effectTime]);
                        tr.find("input[name='txt_TravelControl_IDCard']").val(array[i][rowN.cardId]);
                        tr.find("input[name='txt_TravelControl_Phone']").val(array[i][rowN.tel]).attr("id", parseInt(Math.random(99999) * 100000));
                        tr.find("input[name='txt_TravelControl_Brithday']").val(array[i][rowN.birthday]);
                        tr.find("textarea[name='txt_TravelControlS_Remarks']").val(array[i][rowN.remarks]);
                        if (array[i][rowN.msgB] == "出团") {
                            tr.find(":checkbox[value='1']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControlS_LeaveMsg']").val("1")
                        }
                        else if (array[i][rowN.msgB] == "回团") {
                            tr.find(":checkbox[value='2']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControlS_BackMsg']").val("1")
                        }
                        else if (array[i][rowN.msgB] == "出团回团") {
                            tr.find(":checkbox[value='1']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControlS_LeaveMsg']").val("1")
                            tr.find(":checkbox[value='2']").attr("checked", "checked");
                            tr.find("input[name='cbx_TravelControlS_BackMsg']").val("1")
                        }
                    }
                    ImportPage.ParentBox.append(tr);
                }
                parent.Boxy.getIframeDialog('<%=Request.QueryString["iframeId"]%>').hide();
            }
        }

        //浙江海峡--end
        
        
        
    </script>

    <form id="form1" runat="server">
    <div class="alertbox-outbox">
        <table width="98%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9" align="center"
            style="margin: 0 auto;" id="Tlist">
            <tbody>
                <tr>
                    <td bgcolor="#C1E5F5" align="left" style="padding: 5px 8px;" class="alertboxTableT"
                        colspan="2">
                        <asp:Label ID="lblTempDown" runat="server" Text=""></asp:Label>&nbsp; （只能导入格式为.xls的文件）
                        <span id="inlist" style="display: none"><a href="/ExcelDownTemp/inside_newdemo.xls">
                            游客名单模板</a></span> <span id="outlist" style="display: none"><a href="/ExcelDownTemp/out_demo.xls">
                                下载出境游客名单模板</a></span> <span id="msglist" style="display: none"><a href="/ExcelDownTemp/短信导入模板.xls">
                                    下载短信列表导入模板</a></span> <span id="custlist" style="display: none"><a href="/ExcelDownTemp/custom_list.xls">
                                        下载客户列表导入模板 </a></span>
                        <span id="danxiang" style="display: none"><a href="/ExcelDownTemp/单项客户导入模版.xls">单项客人名单模板</a></span>                                        
                    </td>
                </tr>
                <tr>
                    <td width="15%" valign="middle" bgcolor="#C1E5F5" align="left" style="padding: 5px 8px;"
                        class="alertboxTableT">
                        从Excel文件导入
                    </td>
                    <td valign="middle" bgcolor="#C1E5F5" align="left" style="padding: 5px 8px;" class="alertboxTableT">
                        <div>
                            <input type="hidden" id="hidFileName" />
                            <span runat="server" id="spanButtonPlaceholder"></span><span id="errMsg" class="errmsg">
                            </span>
                        </div>
                        <div id="divUpload">
                            <div id="divFileProgressContainer">
                            </div>
                            <div id="thumbnails">
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="hr_10">
        </div>
                            <div class="pages">
                <cc1:ExporPageInfoSelect ID="paging" runat="server" />
            </div>

        <div style="margin: 0 auto; width: 98%;">
            <span class="formtableT formtableT02">源数据预览</span>
            <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
                id="tablelist">
<asp:PlaceHolder ID="phd" runat="server" Visible="false">
                    <tr class="">
                        <td align="center">
                            暂无数据
                        </td>
                    </tr>
</asp:PlaceHolder>
<tbody>
<asp:Repeater ID="rpt" runat="server">
<HeaderTemplate>
    <tr style="background: url(/images/y-formykinfo.gif) repeat-x center top;">
        <td width="35px" height="23px" bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            <input type="checkbox" checked="checked" id="cbxCheckAll">
        </td>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            姓名
        </td>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            性别
        </td>
        <%--<td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            类型
        </td>--%>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            证件类型
        </td>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            证件号码
        </td>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            签发日期
        </td>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            有效期
        </td>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            出生日期
        </td>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            签发地
        </td>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            联系电话
        </td>
        <%--<td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            里程积分
        </td>--%>
        <td bgcolor="#b7e0f3" align="center" class="alertboxTableT">
            备注
        </td>
    </tr>
</HeaderTemplate>
<ItemTemplate>
    <tr tid="<%#(this.pageIndex-1)*this.pageSize+Container.ItemIndex+1 %>">
        <td height="23px"><input type="checkbox" checked="checked"><%#(this.pageIndex-1)*this.pageSize+Container.ItemIndex+1 %></td>
        <td><%# Eval("Name")%></td>
        <td><%# Eval("Gender") %></td>
        <%--<td>成人</td>--%>
        <td><%# Eval("CardType")%></td>
        <td><%# Eval("IdNumber")%></td>
        <td><%# Eval("QianFaDate","{0:yyyy-MM-dd}")%></td>
        <td><%# Eval("CardValidDate","{0:yyyy-MM-dd}")%></td>
        <td><%# Eval("Birthday","{0:yyyy-MM-dd}")%></td>
        <td><%# Eval("QianFaDi")%></td>
        <td><%# Eval("Telephone")%></td>
        <%--<td>123</td>--%>
        <td><%# Eval("Remark")%></td>
    </tr>
</ItemTemplate>
</asp:Repeater>
        </tbody>
            </table>
        </div>

        <div class="alertbox-btn">
            <a hidefocus="true" href="javascript:void(0);" id="btnSelect"><s class="xuanzhe"></s>
                选 择</a></div>
    </div>
    </form>

    <script type="text/javascript">
        var DemoType = {
            Type: '<%=Request.QueryString["type"] %>'
        }

        switch (DemoType.Type) {
            case "1": //非出境游客列表
                $("#inlist").show();
                break;
            case "2": //出境游客列表
                $("#outlist").show();
                break;
            case "3": //生成短信发送的手机号码列表
                $("#msglist").show();
                break;
            case "4": //单项业务列表
                $("#custlist").show();
                break;
            case "5": //单项业务列表
                $("#danxiang").show();
                break;
        }
    </script>

</body>
</html>

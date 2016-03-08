<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportPage.aspx.cs" Inherits="EyouSoft.WebFX.ImportSource.ImportPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="/Css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Css/swfupload/default.css" rel="stylesheet" type="text/css" />

    <script src="../Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="../Js/jquery.boxy.js" type="text/javascript"></script>

    <script src="../Js/swfupload/swfupload.js" type="text/javascript"></script>

    <script src="../Js/import.js" type="text/javascript"></script>

</head>
<body style="background-color: #e9f4f9">

    <script type="text/javascript">
        var ImportPage = {
            array: [],
            CallBackFun: '<%=Request.QueryString["callbackfun"] %>',
            ParentBox: '<%=Request.QueryString["box"] %>' == '' ? null : parent.$('#<%=Request.QueryString["box"] %>'),
            Type: '<%=Request.QueryString["type"] %>'
        }
        function loadexcel(array) {
            loadXls.init(array, "#tablelist", "");
        }
        $(function() {
            $("#btnSelect").click(function() {

                ImportPage.array = loadXls.bindIndex();

                if (ImportPage.array.length == 0) {

                    parent.tableToolbar._showMsg("请选择数据!");
                    return false;
                } else {
                    switch (ImportPage.Type) {
                        case "1": //非出境游客列表
                            CerateType1(ImportPage.array);
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
                        tr.find("input[name='CertificatesNum']").val(array[i][rowN.cardNo]);
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
                        <span id="inlist" style="display: none"><a href="/ExcelDownTemp/inside_demo.xls">下载非出境游客名单模板</a></span>
                        <span id="outlist" style="display: none"><a href="/ExcelDownTemp/out_demo.xls">下载出境游客名单模板</a></span>
                        <span id="msglist" style="display: none"><a href="/ExcelDownTemp/短信导入模板.xls">下载短信列表导入模板</a></span>
                        <span id="custlist" style="display: none"><a href="/ExcelDownTemp/custom_list.xls">下载客户列表导入模板
                        </a></span>
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
        <div style="margin: 0 auto; width: 98%;">
            <span class="formtableT formtableT02">源数据预览</span>
            <table width="100%" cellspacing="0" cellpadding="0" bgcolor="#FFFFFF" align="center"
                id="tablelist">
                <tbody>
                    <tr class="">
                        <td align="center">
                            暂无数据
                        </td>
                    </tr>
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
        }
    </script>

</body>
</html>

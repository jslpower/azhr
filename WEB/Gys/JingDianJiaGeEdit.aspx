<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JingDianJiaGeEdit.aspx.cs"
    Inherits="EyouSoft.Web.Gys.JingDianJiaGeEdit" MasterPageFile="~/MasterPage/Boxy.Master" %>

<asp:Content ContentPlaceHolderID="PageBody" runat="server" ID="CPHBODY">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF"
                id="liststyle">
                <tr>
                    <td height="23" width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        编号
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        宾客类型
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        团型
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        时间段
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        门市价
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        同行价
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        结算价
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        儿童价
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        家庭价
                    </td>
                    <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        操作
                    </td>
                </tr>
                <asp:Repeater runat="server" ID="rpt">
                    <ItemTemplate>
                        <tr>
                            <td height="23" width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <%# Container.ItemIndex + 1%>
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <input type="hidden" name="txtJiaGeId" value="<%#Eval("JiaGeId") %>" />
                                <select name="txtBinKeLeiXing" i_val="<%#(int)Eval("BinKeLeiXing") %>">
                                    <option value="">请选择</option>
                                    <option value="1">内宾</option>
                                    <option value="2">外宾</option>
                                </select>
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <select name="txtTuanXing" i_val="<%#(int)Eval("TuanXing") %>">
                                    <option value="">请选择</option>
                                    <option value="0">团</option>
                                    <option value="1">散</option>
                                </select>
                            </td>    
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <input name="txtSTime" type="text" class="formsize80 input-txt" onfocus="WdatePicker()"
                                    value="<%#Eval("STime","{0:yyyy-MM-dd}") %>" />
                                &mdash;
                                <input name="txtETime" type="text" class="formsize80 input-txt" onfocus="WdatePicker()"
                                    value="<%#Eval("ETime","{0:yyyy-MM-dd}") %>" />
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <input name="txtMS" type="text" class="formsize80 input-txt" maxlength="10" value="<%#Eval("JiaGeMS","{0:F2}") %>" />
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <input name="txtTH" type="text" class="formsize80 input-txt" maxlength="10" value="<%#Eval("JiaGeTH","{0:F2}") %>" />
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <input name="txtJS" type="text" class="formsize80 input-txt" maxlength="10" value="<%#Eval("JiaGeJS","{0:F2}") %>" />
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <input name="txtET" type="text" class="formsize80 input-txt" maxlength="10" value="<%#Eval("JiaGeET","{0:F2}") %>" />
                            </td>
                            <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <input name="txtJT" type="text" class="formsize80 input-txt" maxlength="10" value="<%#Eval("JiaGeJT","{0:F2}") %>" />
                            </td>
                            <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                                <a href="javascript:void(0)" class="i_update">修改</a> <a href="javascript:void(0)"
                                    class="i_delete">删除</a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>                
                <tr>
                    <td height="23" width="25" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        -
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <input type="hidden" name="txtJiaGeId" value="" />
                        <select name="txtBinKeLeiXing" i_val="">
                            <option value="">请选择</option>
                            <option value="1">内宾</option>
                            <option value="2">外宾</option>
                        </select>
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <select name="txtTuanXing" i_val="">
                            <option value="">请选择</option>
                            <option value="0">团</option>
                            <option value="1">散</option>
                        </select>
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <input name="txtSTime" type="text" class="formsize80 input-txt" onfocus="WdatePicker()"
                            value="" />
                        &mdash;
                        <input name="txtETime" type="text" class="formsize80 input-txt" onfocus="WdatePicker()"
                            value="" />
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <input name="txtMS" type="text" class="formsize80 input-txt" maxlength="10" value="" />
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <input name="txtTH" type="text" class="formsize80 input-txt" maxlength="10" value="" />
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <input name="txtJS" type="text" class="formsize80 input-txt" maxlength="10" value="" />
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <input name="txtET" type="text" class="formsize80 input-txt" maxlength="10" value="" />
                    </td>
                    <td align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <input name="txtJT" type="text" class="formsize80 input-txt" maxlength="10" value="" />
                    </td>
                    <td width="105" align="center" bgcolor="#B7E0F3" class="alertboxTableT">
                        <a href="javascript:void(0)" id="i_a_insert">添加</a>
                    </td>
                </tr>                
            </table>
        </div>
        <div class="alertbox-btn">
        </div>
    </div>

    <script type="text/javascript">
        var iPage = {
            reload: function() {
                window.location.href = window.location.href;
            },
            getFormInfo: function(obj) {
                var _$tr = $(obj).closest("tr");
                var _data = {};
                _data["txtJiaGeId"] = _$tr.find("input[name='txtJiaGeId']").val();
                _data["txtBinKeLeiXing"] = _$tr.find("select[name='txtBinKeLeiXing']").val();
                _data["txtSTime"] = _$tr.find("input[name='txtSTime']").val();
                _data["txtETime"] = _$tr.find("input[name='txtETime']").val();
                _data["txtJS"] = _$tr.find("input[name='txtJS']").val();
                _data["txtET"] = _$tr.find("input[name='txtET']").val();
                _data["txtJT"] = _$tr.find("input[name='txtJT']").val();
                _data["txtMS"] = _$tr.find("input[name='txtMS']").val();
                _data["txtTH"] = _$tr.find("input[name='txtTH']").val(); 
                _data["txtTuanXing"] = _$tr.find("select[name='txtTuanXing']").val();

                return _data;
            },
            submit: function(obj) {
                var _$obj = $(obj);
                var _data = this.getFormInfo(obj);

                if (_data["txtBinKeLeiXing"] == "") {
                    alert("请选择宾客类型");
                    return false;
                }
                if (_data["txtTuanXing"] == "") {
                    alert("请选择团型");
                    return false;
                }

                if (_data["txtJS"] == "") {
                    alert("请输入结算价");
                    return false;
                }

                if (!RegExps.isNumber.test(_data["txtJS"])) {
                    alert("请输入正确的结算价");
                    return false;
                }

                _$obj.unbind("click");

                $.ajax({
                    type: "POST", url: window.location.href + "&doType=submit", data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.reload();
                        } else {
                            alert(response.msg);
                            iPage.reload();
                        }
                    },
                    error: function() {
                        iPage.reload();
                    }
                });
            },
            del: function(obj) {

                if (!confirm("景点价格信息删除后不可恢复，你确定要删除吗?")) return false;
                var _data = this.getFormInfo(obj);
                $(obj).unbind("click");

                $.ajax({
                    type: "POST", url: window.location.href + "&doType=delete", data: _data, cache: false, dataType: "json", async: false,
                    success: function(response) {
                        if (response.result == "1") {
                            alert(response.msg);
                            iPage.reload();
                        } else {
                            alert(response.msg);
                            iPage.reload();
                        }
                    },
                    error: function() {
                        iPage.reload();
                    }
                });
            }
        };

        $(document).ready(function() {
            $("select[name='txtBinKeLeiXing']").each(function() { var _$obj = $(this); _$obj.val(_$obj.attr("i_val")); });
            $("select[name='txtTuanXing']").each(function() { var _$obj = $(this); _$obj.val(_$obj.attr("i_val")); });
            $("#i_a_insert").click(function() { iPage.submit(this); });
            $(".i_update").click(function() { iPage.submit(this); });
            $(".i_delete").click(function() { iPage.del(this); });
        });
    </script>

</asp:Content>

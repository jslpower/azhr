<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderSee.aspx.cs" Inherits="EyouSoft.WebFX.OrderSee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta content="text/html; charset=gb2312" http-equiv="Content-Type" />
    <title>������Ϣ</title>
    <link type="text/css" rel="stylesheet" href="/Css/style.css" />
    <link type="text/css" rel="stylesheet" href="/Css/boxynew.css" />
</head>
<body style="background: 0 none;">
    <div class="alertbox-outbox">
        <div style="margin: 0 auto; width: 99%;">
            <!--<span class="formtableT formtableT02">��·��Ϣ</span>-->
            <table width="99%" cellspacing="0" cellpadding="0" bgcolor="#e9f4f9">
                <tbody>
                    <tr>
                        <td width="14%" height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            �����ţ�
                        </td>
                        <td width="26%" align="left">
                            <asp:Literal ID="LtOrderCode" runat="server"></asp:Literal>
                        </td>
                        <td width="12%" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            ��Դ��λ��
                        </td>
                        <td width="18%">
                            <asp:Literal ID="LtDCompanyName" runat="server"></asp:Literal>
                        </td>
                        <td width="12%" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            ��ϵ�ˣ�
                        </td>
                        <td width="18%">
                            <asp:Literal ID="LtDContactName" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td class="alertboxTableT" align="right" bgcolor="#b7e0f3" height="28" width="14%">
                            ��ϵ�绰��
                        </td>
                        <td align="left" width="26%">
                            <asp:Literal ID="LtDContactTel" runat="server"></asp:Literal>
                        </td>
                        <td class="alertboxTableT" align="right" bgcolor="#B7E0F3" width="12%">
                            ��������Ա��
                        </td>
                        <td width="18%">
                            <asp:Literal ID="LtSellerName" runat="server"></asp:Literal>
                        </td>
                        <td class="alertboxTableT" align="right" bgcolor="#B7E0F3" width="12%">
                            �µ��ˣ�
                        </td>
                        <td width="18%">
                            <asp:Literal ID="LtOperator" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ������
                        </td>
                        <td align="left">
                            <img width="16" height="15" style="vertical-align: middle" src="/Images/chengren.gif" />����
                            <asp:Literal ID="LtAdults" runat="server"></asp:Literal>
                            &nbsp;
                            <img style="vertical-align: middle" src="/Images/child.gif" />
                            ��ͯ
                            <asp:Literal ID="LtChilds" runat="server"></asp:Literal>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            �۸���ɣ�
                        </td>
                        <td height="28" colspan="3">
                            ����<span class="fontred"><asp:Literal ID="LtAdultPrice" runat="server"></asp:Literal></span>����ͯ<span
                                class="fontred"><asp:Literal ID="LtChildPrice" runat="server" /></span>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ���ӷ��ã�
                        </td>
                        <td align="left">
                            <asp:Literal ID="LtSaleAddCost" runat="server"></asp:Literal>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            ��ע��
                        </td>
                        <td height="28" colspan="3">
                            <asp:Literal ID="LtSaleAddCostRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ���ٷ��ã�
                        </td>
                        <td height="28" align="left">
                            <asp:Literal ID="LtSaleReduceCost" runat="server"></asp:Literal>
                        </td>
                        <td height="28" bgcolor="#B7E0F3" align="right" class="alertboxTableT">
                            ��ע��
                        </td>
                        <td height="28" align="left" colspan="3">
                            <asp:Literal ID="LtSaleReduceCostRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            �ϼƽ�
                        </td>
                        <td height="28" align="left">
                            <strong class="fontred">
                                <asp:Literal ID="LtSumPrice" runat="server"></asp:Literal></strong>
                        </td>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ��ֹԤ��ʱ�䣺
                        </td>
                        <td height="28" align="left" colspan="5">
                            <asp:Literal ID="LtSaveSeatDate" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td height="28" bgcolor="#b7e0f3" align="right" class="alertboxTableT">
                            ������ע��
                        </td>
                        <td height="28" align="left" colspan="5">
                            <asp:Literal ID="LtOrderRemark" runat="server"></asp:Literal>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="hr_10">
        </div>
        <asp:PlaceHolder runat="server" ID="phTraveller">
            <div style="margin: 0 auto; width: 99%;">
                <span class="formtableT formtableT02">�ο���Ϣ</span>
                <table width="99%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr style="background: url(../images/y-formykinfo.gif) repeat-x center top;">
                            <td width="30" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ���
                            </td>
                            <td height="23" bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ����
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ����
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ֤������
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ֤������
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                �Ա�
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ��ϵ��ʽ
                            </td>
                            <td bgcolor="#B7E0F3" align="center" class="alertboxTableT">
                                ��ע
                            </td>
                        </tr>
                        <asp:Repeater runat="server" ID="RpTravller">
                            <ItemTemplate>
                                <tr>
                                    <td align="center">
                                        <%#Container.ItemIndex+1 %>
                                    </td>
                                    <td height="28" align="center">
                                        <%#Eval("CnName") == null ? Eval("EnName") : Eval("CnName")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("VisitorType")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("CardType")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("CardNumber")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Gender")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Contact")%>
                                    </td>
                                    <td align="center">
                                        <%#Eval("Remark")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </asp:PlaceHolder>
    </div>

    <script src="/Js/jquery-1.4.4.js" type="text/javascript"></script>

    <script src="/Js/jquery.boxy.js" type="text/javascript"></script>

</body>
</html>

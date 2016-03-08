<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PriceStand.ascx.cs"
    Inherits="EyouSoft.WebFX.UserControl.PriceStand" %>
<table width="100%" cellspacing="0" style="height: auto; overflow: hidden; border-collapse: collapse;
    margin: 5px 0px;" border="0" cellpadding="0" class="baoj autoAdd"
    id="PriceStand_table">
    <tbody>
        <tr>
            <th width="9%" valign="middle" class="th-line" rowspan="2" bgcolor="#D2F1FE" style="text-align: center">
                标准
            </th>
            <asp:Repeater runat="server" ID="rptTableHead">
                <ItemTemplate>
                    <%if (this.ShowModel)
                      { %>
                    <th valign="middle" height="32" class="th-line" colspan="3" bgcolor="#D2F1FE" style="text-align: center">
                        <%}
                      else
                      { %>
                        <th valign="middle" height="32" class="th-line" colspan="2" bgcolor="#D2F1FE" style="text-align: center">
                            <%} %>
                            <%#Eval("LevelName")%><input type="hidden" value="<%#Eval("LevelId") %>|<%#Eval("LevelName")%>|<%#(int)Eval("LevType") %>"
                                name="hide_PriceStand_LevelData" />
                        </th>
                </ItemTemplate>
            </asp:Repeater>
            <asp:Repeater runat="server" ID="rptTableHeadSys">
                <ItemTemplate>
                    <%if (this.ShowModel)
                      { %>
                    <th valign="middle" height="32" class="th-line" colspan="3" bgcolor="#D2F1FE" style="text-align: center">
                        <%}
                      else
                      { %>
                        <th valign="middle" height="32" class="th-line" colspan="2" bgcolor="#D2F1FE" style="text-align: center">
                            <%} %>
                            <%#Eval("name")%><input type="hidden" value="<%#Eval("id") %>|<%#Eval("name")%>|<%#(int)Eval("LevType") %>"
                                name="hide_PriceStand_LevelData" />
                        </th>
                </ItemTemplate> 
            </asp:Repeater>
            <%if (this.ShowModel == false)
              { %>
            <th width="11%" valign="middle" class="th-line" rowspan="2" bgcolor="#D2F1FE" style="text-align: center">
                操作
            </th>
            <%} %>
        </tr>
        <tr class="addcontentT">
            <asp:Repeater runat="server" ID="rptTableHeadCol">
                <ItemTemplate>
                    <% if (this.ShowModel)
                       {  %>
                    <th valign="middle" class="th-line nojiacu" bgcolor="#D2F1FE" style="text-align: center">
                    </th>
                    <%} %>
                    <th valign="middle" class="th-line nojiacu" bgcolor="#D2F1FE" style="text-align: center">
                        成人
                    </th>
                    <th valign="middle" class="th-line nojiacu" bgcolor="#D2F1FE" style="text-align: center">
                        儿童
                    </th>
                </ItemTemplate>
            </asp:Repeater>
        </tr>
        <%if ((this.SetPriceStandard == null || this.SetPriceStandard.Count == 0) && this.ShowModel == false)
          { %>
        <tr class="tempRow">
            <td style="text-align: center">
                <span class="kuang2">
                    <select class="inputselect" name="sel_PriceStandard_type">
                        <%=GetPriceStandard("")%>
                    </select>
                </span>
            </td>
            <asp:Literal ID="litTableBody" runat="server"></asp:Literal>
            <td style="text-align: center">
                <a class="addbtn" href="javascript:void(0)">
                    <img height="20" width="48" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                        <img height="20" width="48" src="/images/delimg.gif"></a>
            </td>
        </tr>
        <%} %>
        <asp:Repeater ID="rptList" runat="server">
            <ItemTemplate>
                <tr class="tempRow">
                    <td style="text-align: center;">
                        <%if (this.ShowModel)
                          { %>
                        <span data-class="standard" data-id="<%#Eval("Standard")%>">
                            <%#Eval("StandardName").ToString()%></span>
                        <%}
                          else
                          { %><span class="kuang2">
                              <select class="inputselect" name="sel_PriceStandard_type">
                                  <%#GetPriceStandard(Eval("Standard").ToString())%>
                              </select>
                          </span>
                        <%} %>
                    </td>
                    <%#GetTableBody(Eval("PriceLevel"),Container.ItemIndex)%>
                    <%if (this.ShowModel == false)
                      { %>
                    <td style="text-align: center">
                        <a class="addbtn" href="javascript:void(0)">
                            <img height="20" width="48" src="/images/addimg.gif"></a> <a class="delbtn" href="javascript:void(0)">
                                <img height="20" width="48" src="/images/delimg.gif"></a>
                    </td>
                    <%} %>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </tbody>
</table>

<script type="text/javascript">
    var PriceStand = {
        Init: function() {
            $("#PriceStand_table").find("tr[class='tempRow']").each(function(i) {
                $(this).find("input[type='text']").each(function() {
                    var name = $(this).attr("name").split('_');
                    name[4] = i;
                    $(this).attr("name", name.join('_'));
                })
            })
        }
    }
</script>


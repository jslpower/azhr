<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SellsSelect.ascx.cs"
    Inherits="Web.UserControl.SellsSelect" %>
<span id="span<%=this.SetPriv %>">
    <input type="text" errmsg="请输入<%=this.SetTitle %>!" valid="required" id="<%=this.SellsNameClient %>"
        class="inputtext formsize80" name="<%=this.SellsNameClient %>" value="<%=this.SellsName %>">
    <input type="hidden" id="<%=this.SellsIDClient %>" name="<%=this.SellsIDClient %>"
        value="<%=this.SellsID %>" errmsg="<%=this.SetTitle %>输入无效,请重新输入或选择<%=this.SetTitle %>!"
        valid="required" />
    <%if (IsShowSelect)
      { %>
    <a id="<%=this.SetPriv %>_a_btn" title="<%=this.SetTitle %>" data-width="850" data-height="550"
        class="xuanyong" href="/CommonPage/OrderSells.aspx"></a>
    <%} %>
    <span data-class="hideDeptInfo" data-sex="<%=this.ClientSex %>" data-tel="<%=ClientTel %>" data-deptid="<%=this.ClientDeptID %>"
        data-deptname="<%=this.ClientDeptName %>"></span></span>

<script type="text/javascript">
    $(function() {
        if('<%=IsMust %>'=="False"){
            $("#<%=this.SellsNameClient %>").removeAttr("errmsg").removeAttr("valid");
            $("#<%=this.SellsIDClient %>").removeAttr("errmsg").removeAttr("valid");
        }
        if($("#<%=this.SetPriv %>_a_btn").length>0){
        $("#<%=this.SetPriv %>_a_btn").get(0).onclick = function() {
            window["sellsData"] = { a: [], b: [], c: [], d: [],e:[],f:[] };
        }
        }
        newToobar.init({ box: "#span<%=this.SetPriv %>", className: "xuanyong", callBackFun: '<%=CallBackFun %>', para: { sModel: '<%=this.SMode?"2":"1" %>', pIframeId: "<%=this.ParentIframeID %>"} });
        if ('<%=this.ReadOnly.ToString().ToLower() %>' == "true") {
            $("#<%=this.SellsNameClient %>").attr("readonly", "readonly").css("background-color", "#dadada");
            $("<%=this.SetPriv %>_a_btn").unbind("click");
        } else {
            //自动匹配 设置
            $("#<%=this.SellsNameClient %>").autocomplete("/Ashx/GetOrderSells.ashx?companyID=" + tableToolbar.CompanyID, {
                width: 130,
                 selectFirst: <%=SelectFrist.ToString().ToLower() %>,
                 blur: <%=SelectFrist.ToString().ToLower() %>
            }).result(function(e, data) {
                $("#<%=this.SellsIDClient %>").val(data[1]);
                $("#<%=this.SellsNameClient %>").attr("data-old", data[0]);
                if ("<%=this.ClientDeptID %>" != "" && $("#<%=this.ClientDeptID %>").length > 0) {
                    $("#<%=this.ClientDeptID %>").val(data[2]);
                }
                if ("<%=this.ClientDeptName %>" != "" && $("#<%=this.ClientDeptName %>").length > 0) {
                    $("#<%=this.ClientDeptName %>").val(data[3]);
                }
                if("<%=this.ClientSex %>"!="" && $("#<%=this.ClientSex %>").length>0){
                    $("#<%=this.ClientSex %>").val(data[4]);
                }
                if("<%=this.ClientTel %>"!="" && $("#<%=this.ClientTel %>").length>0){
                    $("#<%=this.ClientTel %>").val(data[5]);
                }
            });

            $("#<%=this.SellsNameClient %>").keyup(function() {
                var v = $.trim($(this).val());
                if (v == "") {
                    $(this).parent().find("input[type='hidden']").val("");
                }
                if (v != $.trim($(this).attr("data-old"))) {
                    $(this).parent().find("input[type='hidden']").val("");
                }
            })
        }

    })  
</script>


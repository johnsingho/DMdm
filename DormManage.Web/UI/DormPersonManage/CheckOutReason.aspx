<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOutReason.aspx.cs" Inherits="DormManage.Web.UI.DormPersonManage.CheckOutReason" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../../Styles/reset.css" />
    <link rel="stylesheet" href="../../Styles/main.css" />
    <link rel="stylesheet" href="../../Styles/style.css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/common.js"></script>
    <script type="text/javascript">

        function cancel() {
            //window.parent.cancel();
            window.close();
        }

        function saveComplete() {
            window.parent.location = "UserList.aspx";
        }

        function check(){
            if ($('input[name="ckGLF"]').prop("checked")) {
                document.getElementById("txtMoney").disabled = false;
            }
            else
                document.getElementById("txtMoney").disabled = true;

            if ($('input[name="ckKTF"]').prop("checked")) {
                document.getElementById("txtKTF").disabled = false;
            }
            else
                document.getElementById("txtKTF").disabled = true;

            if ($('input[name="ckYSF"]').prop("checked")) {
                document.getElementById("txtYSF").disabled = false;
            }
            else
                document.getElementById("txtYSF").disabled = true;

            if ($('input[name="ckQTF"]').prop("checked")) {
                document.getElementById("txtQTF").disabled = false;
            }
            else
                document.getElementById("txtQTF").disabled = true;
        }

        function ddlReasonChange() {
            var ddl = document.getElementById("ddlReason");
            if (ddl.options[ddl.selectedIndex].value == "调房") {
                document.getElementById("lblDormArea").style.display = "";
                document.getElementById("ddlDormArea").style.display = "";
            }
            else
            {
                document.getElementById("lblDormArea").style.display = "none";
                document.getElementById("ddlDormArea").style.display = "none";
            }
        }

        function ddlFeeTypeChange() {
            var ddl = document.getElementById("ddlFeeType");
            var txtMoney = document.getElementById("txtMoney");
            if (ddl.options[ddl.selectedIndex].value == "管理费") {
                txtMoney.value='10';
            } else if (ddl.options[ddl.selectedIndex].value == "空调费、管理费") {
                txtMoney.value = '120';
            } else if (ddl.options[ddl.selectedIndex].value == "管理费、钥匙费") {
                txtMoney.value = '20';
            } else if (ddl.options[ddl.selectedIndex].value == "空调费、管理费、钥匙费") {
                txtMoney.value = '130';
            }
            else if (ddl.options[ddl.selectedIndex].value == "其他") {
                txtMoney.value = '';
            }
           
        }

        function save() {
            if ($("#<%=this.ddlReason.ClientID%>").val() == "") {
                alert("请选择退房原因！");
                $("#<%=this.ddlReason.ClientID%>").focus();
             
            }
            var result1 = "";
            var result2 = "";
            var result3 = "";
            var result4 = "";
            var sRetrun = $("#<%=this.ddlReason.ClientID%>").val();
            var result = sRetrun;
            if (result == '调房') result = result+'#'+ $("#<%=this.ddlDormArea.ClientID%>").val();
            if ($('input[name="ckGLF"]').prop("checked")) {
                var sFeeType = $("#<%=this.ddlFeeType.ClientID%>").val();
                var sMoney = $("#<%=this.txtMoney.ClientID%>").val();
              
                result =result+ "@" + sFeeType + "@" + sMoney;
            }
            else{
                result =result+ "@" + "" + "@" + "";
            }
            if ($('input[name="ckKTF"]').prop("checked")) {
                var sFeeType = $("#<%=this.ddlKTF.ClientID%>").val();
                var sMoney = $("#<%=this.txtKTF.ClientID%>").val();

                result = result + "@" + sFeeType + "@" + sMoney;
            }
            else {
                result = result + "@" + "" + "@" + "";
            }
            if ($('input[name="ckYSF"]').prop("checked")) {
                var sFeeType = $("#<%=this.ddlYSF.ClientID%>").val();
                var sMoney = $("#<%=this.txtYSF.ClientID%>").val();
             
                result = result + "@" + sFeeType + "@" + sMoney;
            }
            else {
                result = result + "@" + "" + "@" + "";
            }
            if ($('input[name="ckQTF"]').prop("checked")) {
                var sFeeType = $("#<%=this.ddlQTF.ClientID%>").val();
                var sMoney = $("#<%=this.txtQTF.ClientID%>").val();

                result = result + "@" + sFeeType + "@" + sMoney;
            }
            else {
                result = result + "@" + "" + "@" + "";
            }
            var sRemark = $("#<%=this.txtRemark.ClientID%>").val();
            result = result + "@" + sRemark;
            var bCanLeave = $("#<%=this.chkSignExit.ClientID%>").is(":checked");
            result = result + "@" + (bCanLeave?"1":"0");
            //console.log("result= " + result);
            window.returnValue = result;
            window.close();
        }

        $(function () {
            BindReasonChange("#<%=this.ddlReason.ClientID%>", "#<%=this.chkSignExit.ClientID%>");
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="list">
                <table>
                    <tr>
                        <th>
                            <asp:Label ID="lblReason" runat="server" Text="退房原因："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlReason" runat="server" onchange="ddlReasonChange()">
                            <asp:ListItem Value="辞职">辞职</asp:ListItem>
                            <asp:ListItem Value="自离">自离</asp:ListItem>
                            <asp:ListItem Value="外住">外住</asp:ListItem>
                            <asp:ListItem Value="解雇">解雇</asp:ListItem>
                            <asp:ListItem Value="未入职">未入职</asp:ListItem>
                            <asp:ListItem Value="调房">调房</asp:ListItem>
                            </asp:DropDownList>
                            <span>*</span>
                        </td>
                        <th>
                            <asp:Label ID="lblDormArea" runat="server" Text="分配区域："  style="display:none"></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlDormArea" runat="server"  style="display:none">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <input type="checkbox" id="ckGLF" runat="server"  name="ckGLF" value="" checked="checked" onclick="check()"/>选择 
                            <asp:Label ID="Label2" runat="server" Text="扣费类型："></asp:Label>
                        </th>
                        <td>
                            
                            <asp:DropDownList ID="ddlFeeType" runat="server" onchange="ddlFeeTypeChange()">
                            <asp:ListItem Value="管理费">管理费</asp:ListItem>
                            </asp:DropDownList>
                            <span>*</span>
                        </td>
                         <th>
                            <asp:Label ID="Label3" runat="server" Text="金额："></asp:Label>
                        </th>
                        <td>
                           <asp:TextBox ID="txtMoney" runat="server" TabIndex="2" Text="10"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <th>
                          <input type="checkbox" id="ckKTF"  name="ckKTF" value=""  onclick="check()"/>选择 
                            <asp:Label ID="Label4" runat="server" Text="扣费类型："></asp:Label>
                        </th>
                        <td>
                           
                            <asp:DropDownList ID="ddlKTF" runat="server" onchange="ddlFeeTypeChange()">
                            <asp:ListItem Value="空调费">空调费</asp:ListItem>
                            </asp:DropDownList>
                            <span>*</span>
                        </td>
                         <th>
                            <asp:Label ID="Label5" runat="server" Text="金额："></asp:Label>
                        </th>
                        <td>
                           <asp:TextBox ID="txtKTF" runat="server" TabIndex="2" Text="111" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <th>
                           <input type="checkbox"   name="ckYSF" value=""  onclick="check()"/>选择 
                            <asp:Label ID="Label6" runat="server" Text="扣费类型："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlYSF" runat="server" onchange="ddlFeeTypeChange()">
                            <asp:ListItem Value="钥匙费">钥匙费</asp:ListItem>
                            </asp:DropDownList>
                            <span>*</span>
                        </td>
                         <th>
                            <asp:Label ID="Label7" runat="server" Text="金额："></asp:Label>
                        </th>
                        <td>
                           <asp:TextBox ID="txtYSF" runat="server" TabIndex="2" Text="10" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                        <th>
                           <input type="checkbox"   name="ckQTF" value=""  onclick="check()"/>选择 
                            <asp:Label ID="Label8" runat="server" Text="扣费类型："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlQTF" runat="server" onchange="ddlFeeTypeChange()">
                            <asp:ListItem Value="其他">其他</asp:ListItem>
                            </asp:DropDownList>
                            <span>*</span>
                        </td>
                         <th>
                            <asp:Label ID="Label9" runat="server" Text="金额："></asp:Label>
                        </th>
                        <td>
                           <asp:TextBox ID="txtQTF" runat="server" TabIndex="2" Text="" Enabled="False"></asp:TextBox>
                        </td>
                    </tr
                    <tr>
                        <th>
                            <asp:Label ID="Label1" runat="server" Text="备注："></asp:Label>
                        </th>
                        <td>
                          <asp:TextBox ID="txtRemark" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                        </td>
                        <th>&nbsp;
                        </th>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <th></th>
                        <td>
                            <asp:CheckBox ID="chkSignExit" runat="server" Text="同步签批离职系统" Checked="false"/>
                        </td>                        
                        <th>&nbsp;
                        </th>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </div>

            <div class="pagerbar">
                <input id="btnSave" type="button" value="确 认" onclick="save()" />
                <input id="btnCancel" type="button" value="取 消" onclick="cancel()" />
            </div>
        </div>

    </form>
</body>
</html>

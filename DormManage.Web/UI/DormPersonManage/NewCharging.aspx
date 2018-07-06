<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewCharging.aspx.cs" Inherits="DormManage.Web.UI.DormPersonManage.NewCharging" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../../Styles/reset.css" />
    <link rel="stylesheet" href="../../Styles/main.css" />
    <link rel="stylesheet" href="../../Styles/style.css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/common.js"></script>
    <script type="text/javascript">
        function cancel() {
            window.parent.cancel();
        }

        function saveComplete() {
            window.parent.location = "ChargingDefine.aspx";
        }

        function employeeNoChange() {
            var ajaxData = DormPersonManageAjaxServices.GetEmployeeInfoByEmployeeNo($.trim($("#<%=this.txtEmployeeNo.ClientID%>").val()));
            if (ajaxData.value != null && ajaxData.value.Rows.length > 0) {
                $("#<%=this.txtName.ClientID%>").val(ajaxData.value.Rows[0]["Name"]);
                $("#<%=this.ddlBU.ClientID%>").val(ajaxData.value.Rows[0]["BU"]);
            }
        }

        function save() {
            if ($.trim(($("#<%=this.txtEmployeeNo.ClientID%>")).val()) == "") {
                alert("请输入工号！");
                $("#<%=this.txtEmployeeNo.ClientID%>").focus();
                return false;
            }
            if ($.trim(($("#<%=this.txtName.ClientID%>")).val()) == "") {
                alert("请输入姓名！");
                $("#<%=this.txtName.ClientID%>").focus();
                return false;
            }
            if ($.trim(($("#<%=this.ddlBU.ClientID%>")).val()) == "") {
                alert("请选择事业部！");
                $("#<%=this.ddlBU.ClientID%>").focus();
                return false;
            }
            if ($.trim(($("#<%=this.txtChargeContent.ClientID%>")).val()) == "") {
                alert("请输入扣费内容！");
                $("#<%=this.txtChargeContent.ClientID%>").focus();
                return false;
            }
            if ($.trim(($("#<%=this.txtMoney.ClientID%>")).val()) == "") {
                alert("请输入金额！");
                $("#<%=this.txtMoney.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="list">
                <table>
                    <tr>
                        <th>
                            <asp:Label ID="lblEmployeeNo" runat="server" Text="工号："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtEmployeeNo" runat="server" MaxLength="50" TabIndex="1" onBlur="employeeNoChange()"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblName" runat="server" Text="姓名："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblBU" runat="server" Text="事业部："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlBU" runat="server" TabIndex="3" Width="135px"></asp:DropDownList>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblChargeContent" runat="server" Text="扣费内容："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtChargeContent" runat="server" TabIndex="4" ReadOnly="True">管理费</asp:TextBox>
                            <span>*</span>
                        </td>
                        <th>
                            <asp:Label ID="lblMoney" runat="server" Text="金额："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtMoney" runat="server" MaxLength="50" TabIndex="4"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                         <th>
                            <asp:Label ID="Label1" runat="server" Text="扣费内容："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtAirConditionFee" runat="server" TabIndex="4" ReadOnly="True">空调费</asp:TextBox>
                            <span>*</span>
                        </td>
                        <th>
                            <asp:Label ID="Label2" runat="server" Text="金额："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtAirConditionFeeMoney" runat="server" MaxLength="50" TabIndex="4"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                     <tr>
                         <th>
                            <asp:Label ID="Label3" runat="server" Text="扣费内容："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtRoomKeyFee" runat="server" TabIndex="4" ReadOnly="True">钥匙费</asp:TextBox>
                            <span>*</span>
                        </td>
                        <th>
                            <asp:Label ID="Label4" runat="server" Text="金额："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtRoomKeyFeeMoney" runat="server" MaxLength="50" TabIndex="4"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                         <th>
                            <asp:Label ID="Label5" runat="server" Text="扣费内容："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtOtherFee" runat="server" TabIndex="4" ReadOnly="True">其他费</asp:TextBox>
                            <span>*</span>
                        </td>
                        <th>
                            <asp:Label ID="Label6" runat="server" Text="金额："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtOtherFeeMoney" runat="server" MaxLength="50" TabIndex="4"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="pagerbar">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return save()" OnClick="btnSave_Click" />
                <input id="btnCancel" type="button" value="取 消" onclick="cancel()" />
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCheckIn.aspx.cs" Inherits="DormManage.Web.UI.DormPersonManage.EditCheckIn" %>

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
    <script src="../../Scripts/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function cancel() {
            window.parent.cancel();
        }

        function saveComplete() {
            window.parent.location = "CheckInDefine.aspx";
        }

        function save() {
            if ($.trim(($("#<%=this.txtBU.ClientID%>")).val()) == "") {
                alert("请选择事业部！");
                $("#<%=this.txtBU.ClientID%>").focus();
                return false;
            }
            if ($.trim(($("#<%=this.txtEmployeeType.ClientID%>")).val()) == "") {
                alert("请输入用工类型！");
                $("#<%=this.txtEmployeeType.ClientID%>").focus();
                return false;
            }
            if ($.trim(($("#<%=this.txtCheckinDate.ClientID%>")).val()) == "") {
                alert("请输入入住日期！");
                $("#<%=this.txtCheckinDate.ClientID%>").focus();
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
                            <asp:TextBox ID="txtEmployeeNo" runat="server" MaxLength="50" TabIndex="1" ></asp:TextBox>
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
                          <asp:TextBox ID="txtBU" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                           
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblChargeContent" runat="server" Text="用工类型："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtEmployeeType" runat="server" TabIndex="4"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblPhone" runat="server" Text="手机号码："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtPhone" runat="server" MaxLength="50" TabIndex="4"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr> 
                     <tr>
                        <th>
                            <asp:Label ID="Label1" runat="server" Text="入住日期"></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtCheckinDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
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

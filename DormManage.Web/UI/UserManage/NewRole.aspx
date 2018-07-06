<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRole.aspx.cs" Inherits="DormManage.Web.UI.UserManage.NewRole" %>

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
            window.parent.location = "RoleList.aspx";
        }

        function save() {
            if ($.trim(($("#<%=this.txtRoleName.ClientID%>")).val()) == "") {
                alert("请输入角色名！");
                $("#<%=this.txtRoleName.ClientID%>").focus();
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
                            <asp:Label ID="lblRoleName" runat="server" Text="角色名："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtRoleName" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>

                </table>
            </div>
            <div>
                <div class="treOrgan">
                    <asp:TreeView ID="treAuthority" runat="server" ShowCheckBoxes="Leaf" ShowLines="true" >
                        <LevelStyles>
                            <asp:TreeNodeStyle Font-Bold="True" Font-Size="10pt" Font-Underline="False" />
                        </LevelStyles>
                        <SelectedNodeStyle BackColor="#CCCCFF" Font-Bold="True" ForeColor="Red" />
                    </asp:TreeView>
                </div>
            </div>
            <div class="pagerbar">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return save()" OnClick="btnSave_Click" />
                <input id="btnCancel" type="button" value="取 消" onclick="cancel()" />
            </div>
        </div>
    </form>
</body>
</html>

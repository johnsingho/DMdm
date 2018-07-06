<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewBU.aspx.cs" Inherits="DormManage.Web.UI.UserManage.NewBU" %>

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
            window.parent.location = "BUList.aspx";
        }

        function save() {
            if ($("#<%=this.txtBU.ClientID%>").val() == "")
            {
                alert("请输入事业部！");
                $("#<%=this.txtBU.ClientID%>").focus();
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
                            <asp:Label ID="lblBU" runat="server" Text="事业部："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtBU" runat="server"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="pagerbar">
                <asp:Button ID="btnSave" runat="server" Text="保 存" OnClientClick="return save()" OnClick="btnSave_Click" />
                <input id="btnCancel" type="button" value="取 消" onclick="cancel()" />
            </div>
        </div>
    </form>
</body>
</html>

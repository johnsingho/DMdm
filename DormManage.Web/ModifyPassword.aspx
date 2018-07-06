<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyPassword.aspx.cs" Inherits="DormManage.Web.ModifyPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="Styles/reset.css" />
    <link rel="stylesheet" href="Styles/main.css" />
    <link rel="stylesheet" href="Styles/style.css" />
    <script type="text/javascript" src="Scripts/jquery-1.8.2.min.js"></script>
    <script type="text/javascript" src="Scripts/common.js"></script>
    <title></title>
    <script type="text/javascript">
        $(function () {
            $("#<%=this.txtConfirmNewPassword.ClientID%>").blur(function () {
                if ($(this).val() != $("#<%=this.txtNewPassword.ClientID%>").val()) {
                    alert("两次输入的密码不一致！");
                }
            })
        });

        //取消
        function cancel() {
            window.close();
        }

        //保存
        function save() {
            if ($('#<%=this.txtOldPassword.ClientID%>').val() == "") {
                alert("请输入旧密码！");
                $('#<%=this.txtOldPassword.ClientID%>').focus();
                return false;
            }
            if ($('#<%=this.txtNewPassword.ClientID%>').val() == "") {
                alert("请输入新密码！");
                $('#<%=this.txtNewPassword.ClientID%>').focus();
                return false;
            }
            if ($('#<%=this.txtConfirmNewPassword.ClientID%>').val() == "") {
                alert("请再次输入新密码！");
                $('#<%=this.txtConfirmNewPassword.ClientID%>').focus();
                return false;
            }
            var ajaxData = UserAjaxServices.ModifyPassword('<%=base.SystemAdminInfo.ID%>'
                , $('#<%=this.txtOldPassword.ClientID%>').val(), $('#<%=this.txtConfirmNewPassword.ClientID%>').val());
            if (ajaxData.value != "") {
                alert(ajaxData.value);
            } else {
                alert("保存成功！");
                window.close();
            }
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
                            <asp:Label ID="lblOldPassword" runat="server" Text="旧密码："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblNewPassword" runat="server" Text="新密码："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblConfirmNewPassword" runat="server" Text="确认新密码："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtConfirmNewPassword" runat="server" TextMode="Password"></asp:TextBox>
                            <span>*</span>
                        </td>
                    </tr>

                </table>
            </div>
            <div class="pagerbar">
                <input id="btnSave" type="button" value="保 存" onclick="save()" />
                <input id="btnCancel" type="button" value="取 消" onclick="cancel()" />
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DormManage.Web.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Dormitory Management System</title>
    <link type="text/css" href="styles/base.css" rel="stylesheet" />
    <link type="text/css" href="styles/login.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery-1.8.2.min.js"></script>
    <link rel="shortcut icon" href="images/c_favicon.ico" />
    <style type="text/css">
        .header
        {
            position: relative;
            margin: 0px;
            padding: 0px;
            background: #006699;
            width: 100%;
        }

            .header h1
            {
                font-weight: 700;
                margin: 0px;
                padding: 0px 0px 0px 20px;
                color: #f9f9f9;
                border: none;
                line-height: 2em;
                font-size: 2em;
            }
    </style>
    <script type="text/javascript">

        function inputCheck() {
            if ($("#<%=this.txtUserName.ClientID%>").val() == "") {
                alert("Sorry，用户名不能为空！");
                return false;
            }
            if ($("#<%=this.txtPassWord.ClientID%>").val() == "") {
                alert("Sorry，密码不能为空！");
                return false;
            }
            return true;
        }

    </script>
</head>
<body>
    <form id="frmLogin" runat="server">
<%--        <div class="header">
            <div style='height: 50px;'>
                <h1 style='font-size: 30px; margin-left: 0px; margin-top: -2px; float: left'>Dormitory Management System</h1>
                <img src="images/Flextronics_blue.png" alt="" title="Flextronics" style="height: 60px; margin-top: -1px; float: right" />
            </div>
        </div>--%>
        <div class="wrap">
            <div class="login">
                <div class="main">
                    <dl class="loginWindow">
                        <dt></dt>
                        <dd>
                            <ul class="inputs">
                                <li id="username" class="long">
                                    <asp:TextBox tit="username" ID="txtUserName" runat="server" MaxLength="20" onblur="tblur(this)"></asp:TextBox>
                                </li>
                                <li id="pw" class="long">
                                    <asp:TextBox tit="pw" ID="txtPassWord" runat="server" TextMode="Password" MaxLength="50"
                                        onblur="tblur(this)"></asp:TextBox>
                                </li>
                            </ul>
                            <div class="subm" tit="subm">
                                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="" OnClientClick="return inputCheck()" />
                            </div>
                        </dd>
                        <script type="text/javascript" src="Scripts/login.js"></script>
                    </dl>
                </div>
                <div class="footer">
                    <span id="about" runat="server">About</span>&nbsp;|
                    <asp:Label ID="lblCompany" runat="server"
                        Text="GBS-Flex Info.Tech.(SZ)Co.,Ltd"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateEmpInfo.aspx.cs" Inherits="DormManage.Web.UpdateEmpInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>更新宿舍系统内的员工信息</title>
    <link type="text/css" href="styles/base.css" rel="stylesheet" />
    <link type="text/css" href="styles/login.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/jquery-1.8.2.min.js"></script>
    <script>
        function checkUpload() {
            if ($('#<%=this.fileUpload.ClientID%>').val() == "") {
                alert("请选择需要上传的文件！");
                return false;
            }

            $('#<%=this.lblSuccess.ClientID%>').text('');
            $('#<%=this.lblError.ClientID%>').text('');
            return true;
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 203px;
        }
        .auto-style2 {
            width: 255px;
            height: 294px;
            float: right;
            margin: 77px 39px 0 0;
            display: inline;
        }
        .auto-style3 {
            width: 255px;
            height: 62px;
            float: left;
        }
    </style>
</head>
<body>
    <form id="frmLogin" runat="server">
        <div class="wrap">
            <div class="login">
                <div class="main">
                    <dl class="auto-style2">
                        <dd class="auto-style1">
                            <ul class="auto-style3">
                                <li>上传新员工数据&nbsp;&nbsp;<a href="/FileTemplate/IDL更新模板.xlsx"><b>模板</b></a>
                                </li>
                                <li>
                                    <asp:FileUpload ID="fileUpload" runat="server" />
                                </li>
                            </ul>
                            <div>
                                <asp:Button ID="btnUpload" runat="server" Text="上传" Width="79px" OnClick="btnUpload_Click" OnClientClick="return checkUpload();" />
                            </div>
                            <div>
                                <asp:Label ID="lblSuccess" runat="server" Text="" ForeColor="Blue"></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
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

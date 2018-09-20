<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DormNoticeAdd.aspx.cs" Inherits="DormManage.Web.UI.FlexPlus.DormNoticeAdd" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>宿舍公告</title>
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
            window.parent.location = "DormNoticeList.aspx";
        }

        function save() {
            if ($.trim(($("#<%=this.txtContext.ClientID%>")).val()) == "") {
                alert("请填写内容！");
                $("#<%=this.txtContext.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 230px;
            width: 490px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="list">
                <table>
                    <tr>                        
                        <th>
                            <asp:Label ID="Label1" runat="server" Text="标题："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtTitle" runat="server" Width="500px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="Label2" runat="server" Text="内容："></asp:Label>
                        </th>
                        <td>
                            <textarea id="txtContext" runat="server" placeholder="排版优美的内容，可以使用微信文章编辑器的代码。例如：http://bj.96weixin.com"
                                class="auto-style1"></textarea>
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

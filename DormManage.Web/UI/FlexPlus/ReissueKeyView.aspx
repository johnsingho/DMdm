<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReissueKeyView.aspx.cs" Inherits="DormManage.Web.UI.FlexPlus.ReissueKeyView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看补办钥匙</title>
    <link rel="stylesheet" href="../../Styles/reset.css" />
    <link rel="stylesheet" href="../../Styles/main.css" />
    <link rel="stylesheet" href="../../Styles/style.css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/common.js"></script>
    <style>
        .list table th {
            width: 10%;
        }
        input[type='text']{
            width:90%;
        }
    </style>

    <script>
        function cancel() {
            window.parent.cancel();
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
                            <label>工号</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtEmployeeNo" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>姓名</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtCName" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>手机号码</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtMobileNo" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>宿舍地址</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtDormAddress" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>钥匙类型</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtKeyTypes" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>总费用</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtMoney" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>原因</label>
                        </th>
                        <td>
                            <textarea id="txtReason" runat="server" readonly="readonly" rows="4"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>备注</label>
                        </th>
                        <td>
                            <textarea id="txtMemo" runat="server" readonly="readonly" rows="4"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>提交时间</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="pagerbar">
                <input id="btnCancel" type="button" value="关 闭" onclick="cancel()" />
            </div>
        </div>
    </form>
</body>
</html>

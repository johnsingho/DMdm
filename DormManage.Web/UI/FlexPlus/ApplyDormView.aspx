<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyDormView.aspx.cs" Inherits="DormManage.Web.UI.FlexPlus.ApplyDormView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看宿舍申请</title>
    <link rel="stylesheet" href="../../Styles/reset.css" />
    <link rel="stylesheet" href="../../Styles/main.css" />
    <link rel="stylesheet" href="../../Styles/style.css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/common.js"></script>

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
                            <label>性别</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtSex" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>身份证号码</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtCardNo" runat="server" ReadOnly="True"></asp:TextBox>
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
                            <label>级别</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtGrade" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>宿舍区</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtDormArea" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>申请类型</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtRequireType" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>申请原因</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtRequireReason" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>是否享有住房补贴</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtHasHousingAllowance" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>备注</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtmemo" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>申请时间</label>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RepairDormView.aspx.cs" Inherits="DormManage.Web.UI.FlexPlus.RepairDormView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看宿舍报修</title>
    <link rel="stylesheet" href="../../Styles/reset.css" />
    <link rel="stylesheet" href="../../Styles/main.css" />
    <link rel="stylesheet" href="../../Styles/style.css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/common.js"></script>

    <style>
        .list table th {
            width: 10%;
        }
        input[type='text'] {
            width: 90%;
        }
        .lnkHas{
            color:blue;
            text-decoration:underline;
            cursor:pointer;
        }
        .lnkNoHas{
            visibility:hidden;
        }
    </style>

    <script>
        function cancel() {
            window.parent.cancel();
        }
        function ViewRepairPicture() {
            var batchID = $("#<%=RefImageBatchNo.ClientID%>").val();
            console.log("*batchID=" + batchID);
            window.parent.ViewRepairPicture(batchID);
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
                            <label>预约时间</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtRepairTime" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>待修设备类型</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtDeviceType" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>相关照片</label>
                        </th>
                        <td>
                            <asp:HyperLink ID="lnkView" runat="server" CssClass="lnkNoHas"
                                onclick="ViewRepairPicture()">查看</asp:HyperLink>
                            <asp:HyperLink ID="lblNoHas" runat="server" Visible="false" style="font-style:italic">(无)</asp:HyperLink>
                            <asp:HiddenField ID="RefImageBatchNo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>描述</label>
                        </th>
                        <td>
                            <textarea id="txtRequireDesc" runat="server" readonly="readonly" style="width:90%; height:100px;"
                                rows="5"></textarea>
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

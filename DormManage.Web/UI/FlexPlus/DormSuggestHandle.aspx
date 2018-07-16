<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DormSuggestHandle.aspx.cs" Inherits="DormManage.Web.UI.FlexPlus.DormSuggestHandle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>宿舍建议回复</title>
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
        .myTextArea{
            width:90%;
            height:75px;
        }
    </style>

    <script>
        function cancel() {
            window.parent.cancel();
        }
        function saveComplete(kind) {
            window.parent.location = "DormSuggestList.aspx";
        }

        function save() {
            if ($.trim(($("#<%=this.txtResponse.ClientID%>")).val()) == "") {
                alert("请填写回复消息！");
                $("#<%=this.txtResponse.ClientID%>").focus();
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
                            <label>提交时间</label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtCreateDate" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>建议</label>
                        </th>
                        <td>
                            <textarea id="txtSuggest" runat="server" class="myTextArea"
                                readonly="readonly" rows="5"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <label>回复</label>
                        </th>
                        <td>
                            <textarea id="txtResponse" runat="server" class="myTextArea"
                                rows="5"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="pagerbar">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return save()" OnClick="btnSave_Click" />
                <input id="btnCancel" type="button" value="关 闭" onclick="cancel()" />
            </div>
        </div>
    </form>
</body>
</html>

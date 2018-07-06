<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="DormManage.Web.UI.UserManage.UserList" %>

<!DOCTYPE html>
<%@ Register Assembly="Ctl" Namespace="ExtendGridView" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title></title>
    <link rel="stylesheet" href="../../Styles/main.css" />
    <link rel="stylesheet" href="../../Styles/style.css" />
    <link href="../../Styles/reset.css" rel="stylesheet" />
    <link href="../../Styles/showLoading.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/common.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.showLoading.js" type="text/javascript"></script>
    <script src="../../Scripts/ligerUI1.1.9/js/ligerui.all.js"></script>
    <link href="../../Scripts/ligerUI1.1.9/skins/Aqua/css/ligerui-all.css" rel="stylesheet" />
    <script type="text/javascript">
        function add() {
            $.ligerDialog.open({
                title: "添加",
                width: 800,
                height: 520,
                isResize: true,
                url: 'NewUser.aspx'
            });
            return false;
        }

        function remove() {
            return false;
        }

        function cancel() {
            $.ligerDialog.close();
        }

        function view(id) {
            $.ligerDialog.open({
                title: "编辑",
                width: 800,
                height: 520,
                isResize: true,
                url: 'NewUser.aspx?id=' + id
            });
        }

        function remove() {
            var table = document.getElementById("<%=this.GridView1.ClientID%>");
            var checkCount = 0;
            for (i = 1; i < table.rows.length; i++) {
                var cb = table.rows(i).cells(0).children(0);
                if (cb.checked) {
                    checkCount++;
                    if (confirm("确认删除选中记录？"))
                        return true;
                    else
                        return false;
                }
            }
            if (checkCount == 0)
                alert("请选择需要删除的记录!");
            return false;
        }

        $(function () {
            bindEmpIDCtrl(10, '#<%=txtEmployeeNo.ClientID%>');
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">用户管理->用户列表</span>
            </h3>
        </div>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar">
                    <table>
                        <tr>
                            <th>
                                <asp:Label ID="lblAdAccount" runat="server" Text="AD账号："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtAdAccount" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblEmployeeNo" runat="server" Text="工号："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtEmployeeNo" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblEName" runat="server" Text="英文名："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtEName" runat="server" MaxLength="50" TabIndex="3"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblCName" runat="server" Text="中文名："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtCName" runat="server" MaxLength="50" TabIndex="4"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" class="findBtn" TabIndex="5"
                                    OnClick="btnSearch_Click"></asp:Button>
                                <asp:Button ID="btnNew" Text="新 增" runat="server" class="newBtn" TabIndex="5" OnClick="btnNew_Click" OnClientClick="return add()"></asp:Button>
                                <asp:Button ID="btnRemove" Text="删除" CssClass="deleteBtn" runat="server" OnClick="btnRemove_Click" OnClientClick="return remove()"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div id="loadSearch" class="loading">
                            <div id="searchResult">
                                <div class="viewlist">
                                    <cc1:GridView ID="GridView1" runat="server" CssClass="form" EmptyDataText="<无结果显示>"
                                        AutoGenerateColumns="False" EnableEmptyContentRender="True" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="20px" />
                                                <HeaderTemplate>
                                                    <input type="checkbox" id="chkLeftAll" onclick="CheckAll(this)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLeftSingle" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ADAccount" HeaderText="AD账号" />
                                            <asp:BoundField DataField="EmployeeNo" HeaderText="工号" />
                                            <asp:BoundField DataField="EName" HeaderText="英文名" />
                                            <asp:BoundField DataField="CName" HeaderText="中文名" />
                                            <asp:BoundField DataField="DormName" HeaderText="所辖宿舍区" />
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <td class="pageleft">
                                            </td>
                                            <td class="pageright">
                                                <cc1:Pager ID="Pager1" runat="server" OnCommand="pagerList_Command" 
                                                    PageSize="10" Visible="False" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Pager1" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

                                            <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <%--<td class="pageleft">
                                                <asp:Button ID="btnRemove" Text="删除" CssClass="deleteBtn" runat="server" OnClick="btnRemove_Click" OnClientClick="return remove()"></asp:Button>
                                            </td>--%>
                                            <td class="pageright">
                                                <cc1:Pager ID="Pager2" runat="server" OnCommand="pagerList_Command" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
        </div>
    </form>
</body>
</html>

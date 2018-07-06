<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="DormManage.Web.UI.UserManage.NewUser" %>

<!DOCTYPE html>
<%@ Register Assembly="Ctl" Namespace="ExtendGridView" TagPrefix="cc1" %>

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
            window.parent.location = "UserList.aspx";
        }

        function save() {
            if ($.trim(($("#<%=this.txtADAccount.ClientID%>")).val()) == "") {
                alert("请输入AD账号！");
                $("#<%=this.txtADAccount.ClientID%>").focus();
                return false;
            }
            if ($("#<%=this.ddlRole.ClientID%>").val() == "0") {
                alert("请选择角色！");
                $("#<%=this.ddlRole.ClientID%>").focus();
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
                            <asp:Label ID="lblADAccount" runat="server" Text="AD账号："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtADAccount" runat="server"></asp:TextBox>
                            <span>*</span>
                        </td>
                        <th>
                            <asp:Label ID="lblEmployeeNo" runat="server" Text="工号："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtEmployeeNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblEName" runat="server" Text="英文名："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtEName" runat="server"></asp:TextBox>
                        </td>
                        <th>
                            <asp:Label ID="lblCName" runat="server" Text="中文名："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtCName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblRole" runat="server" Text="角色："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlRole" runat="server">
                            </asp:DropDownList>
                            <span>*</span>
                        </td>
                        <th>&nbsp;
                        </th>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <div class="datapeer">
                <table>
                    <tr>
                        <td style="font-size: 14px; text-align: center; font-weight: bold"><font color="Blue">待选宿舍区</font></td>
                        <td>&nbsp</td>
                        <td style="font-size: 14px; text-align: center; font-weight: bold"><font color="Blue">已选宿舍区</font></td>
                    </tr>
                    <tr>
                        <td class="dt">
                            <div class="viewlist">
                                <cc1:GridView ID="gdvLeft" runat="server" CssClass="form" EmptyDataText="<无结果显示>"
                                    AutoGenerateColumns="False" EnableEmptyContentRender="True" DataKeyNames="id">
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
                                        <asp:BoundField DataField="Name" HeaderText="宿舍区">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                    </Columns>
                                </cc1:GridView>
                            </div>
                        </td>
                        <td class="dt_m">
                            <asp:Button ID="btnAdd" runat="server" CssClass="rightBtn" Text="" OnClick="btnAdd_Click" />                            
                            <br />
                            <br />
                            <asp:Button ID="btnRemove" runat="server" CssClass="leftBtn" Text="" OnClick="btnRemove_Click" />
                        </td>
                        <td class="dt">
                            <div class="viewlist">
                                <cc1:GridView ID="gdvRight" runat="server" CssClass="form" EmptyDataText="<无结果显示>"
                                    AutoGenerateColumns="False" EnableEmptyContentRender="True" DataKeyNames="id">
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
                                        <asp:BoundField DataField="Name" HeaderText="宿舍区">
                                            <HeaderStyle Width="100px" />
                                        </asp:BoundField>
                                    </Columns>
                                </cc1:GridView>
                            </div>
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

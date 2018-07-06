<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AssignRecord.aspx.cs" Inherits="DormManage.Web.UI.AssignRoom.AssignRecord" %>

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
        function ConfirmOperate() {
            if (!confirm("确认执行该操作？")) {
                return false;
            }
            return true;
        }

        function CancelCheckIn() {
            if (!confirm("确认取消入住？")) {
                return false;
            }
            return true;
        }
        function UnLock() {
            var $chkSelect = $("input[type='checkbox']:checked");
            if ($chkSelect.length == 0) {
                alert("请选择需要解锁的房间！");
                return false;
            }
            if (!confirm("确认解锁选中房间？")) {
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">房间分配->分配记录</span>
            </h3>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar">
                    <table>
                        <tr>
                            <th>
                                <asp:Label ID="lblCardNo" runat="server" Text="身份证号码："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtCardNo" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                            </td>
                            <th>状态：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server">
                                    <asp:ListItem Selected="True" Text="--请选择--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="锁定房" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="未入住房" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" class="findBtn" TabIndex="2" OnClick="btnSearch_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
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
                                            <asp:BoundField DataField="DormAreaName" HeaderText="宿舍区" />
                                            <asp:BoundField DataField="BuildingName" HeaderText="楼栋" />
                                            <%--                                            <asp:BoundField DataField="UnitName" HeaderText="单元" />
                                            <asp:BoundField DataField="FloorName" HeaderText="楼层" />--%>
                                            <asp:BoundField DataField="RoomName" HeaderText="房间号" />
                                            <asp:BoundField DataField="RoomTypeName" HeaderText="房间类型" />
                                            <%--<asp:BoundField DataField="RoomType2Name" HeaderText="房间类型2" />--%>
                                            <asp:BoundField DataField="RoomSexType" HeaderText="性别分类" />
                                            <asp:BoundField DataField="BedName" HeaderText="床位号" />
                                            <asp:BoundField DataField="EmployeeNo" HeaderText="工号" />
                                            <asp:BoundField DataField="Name" HeaderText="姓名 " />
                                            <asp:BoundField DataField="CardNo" HeaderText="身份证号码" />
                                            <asp:BoundField DataField="Company" HeaderText="公司" />
                                            <asp:BoundField DataField="BU" HeaderText="事业部" />
                                          
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnOperate" runat="server" CssClass="publicBtn" Text="" OnClientClick="return ConfirmOperate()" OnClick="btnOperate_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnCancelCheckIn" runat="server" CssClass="publicBtn" Text="取消入住" OnClick="btnCancelCheckIn_Click" OnClientClick="return CancelCheckIn()" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                         <td class="pageleft">
                                                <%--<asp:Button ID="btnAllLock" Text="批量解锁" CssClass="publicBtn" runat="server" OnClientClick="return UnLock()" OnClick="btnAllLock_Click"></asp:Button>--%>
                                            </td>
                                            <td class="pageright">
                                                <cc1:Pager ID="Pager1" runat="server" OnCommand="pagerList_Command" />
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
        </div>
    </form>
</body>
</html>

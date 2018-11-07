<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="AssignAvailableBed.aspx.cs" Inherits="DormManage.Web.UI.AssignRoom.AssignAvailableBed" %>

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
        $(function () {
            $("#<%=this.ddlDormArea.ClientID%>").change(function () {
                var html = "<option value='0' select='selected'>--请选择--</option>";
                if ($(this).val() != "0") {
                    var ajaxData = DormManageAjaxServices.GetBuildingByDormAreaID($(this).val());
                    for (var i = 0; i < ajaxData.value.Rows.length; i++) {
                        html += "<option value='" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Building.col_ID%>"] + "'>" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Building.col_Name%>"] + "</option>"
                    }
                }
                $("#<%=this.ddlBuildingName.ClientID%>").html(html);
                $("#<%=this.ddlUnit.ClientID%>").html("<option value='0' select='selected'>--请选择--</option>");
                $("#<%=this.ddlFloor.ClientID%>").html("<option value='0' select='selected'>--请选择--</option>");
            });

            $("#<%=this.ddlBuildingName.ClientID%>").change(function () {
                var html = "<option value='0' select='selected'>--请选择--</option>";
                if ($(this).val() != "0") {
                    var ajaxData = DormManageAjaxServices.GetUnitByBuildingID($(this).val());
                    for (var i = 0; i < ajaxData.value.Rows.length; i++) {
                        html += "<option value='" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Unit.col_ID%>"] + "'>" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Unit.col_Name%>"] + "</option>"
                    }
                }
                $("#<%=this.ddlUnit.ClientID%>").html(html);
                $("#<%=this.ddlFloor.ClientID%>").html("<option value='0' select='selected'>--请选择--</option>");
            });

            $("#<%=this.ddlUnit.ClientID%>").change(function () {
                var html = "<option value='0' select='selected'>--请选择--</option>";
                if ($(this).val() != "0") {
                    var ajaxData = DormManageAjaxServices.GetFloorByUnitID($(this).val());
                    for (var i = 0; i < ajaxData.value.Rows.length; i++) {
                        html += "<option value='" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Floor.col_ID%>"] + "'>" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Floor.col_Name%>"] + "</option>"
                    }
                }
                $("#<%=this.ddlFloor.ClientID%>").html(html);
            });

        });

        function Lock() {
            var $chkSelect = $("input[type='checkbox']:checked");
            if ($chkSelect.length == 0) {
                alert("请选择需要锁定的房间！");
                return false;
            }
            if (!confirm("确认锁定选中房间？")) {
                return false;
            }
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">房间分配->查询床位</span>
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
                                <asp:Label ID="lblDormArea" runat="server" Text="宿舍区："></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea" runat="server" TabIndex="1"></asp:DropDownList>
                            </td>
                            <th>
                                <asp:Label ID="lblBuildingName" runat="server" Text="楼栋："></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuildingName" runat="server" TabIndex="2"></asp:DropDownList>
                            </td>
                            <%--/******************************************************************************/--%>
                            <th style="display: none">
                                <asp:Label ID="lblUnitName" runat="server" Text="单元："></asp:Label>
                            </th>
                            <td style="display: none">
                                <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="3"></asp:DropDownList>
                            </td>
                            <th style="display: none">
                                <asp:Label ID="lblFloor" runat="server" Text="楼层："></asp:Label>
                            </th>
                            <td style="display: none">
                                <asp:DropDownList ID="ddlFloor" runat="server" TabIndex="4"></asp:DropDownList>
                            </td>
                            <%--/******************************************************************************/--%>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lblRoomType" runat="server" Text="房间类型："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType" runat="server"></asp:DropDownList></td>
                            <th style="display: none">
                                <asp:Label ID="lblRoomType2" runat="server" Text="房间类型2："></asp:Label></th>
                            <td style="display: none">
                                <asp:DropDownList ID="ddlRoomType2" runat="server">
                                    <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="1">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>
                            <th>
                                <asp:Label ID="lblRoomSexType" runat="server" Text="性别分类："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType" runat="server">
                                    <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                    <asp:ListItem Value="不限">不限</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查询空床位" runat="server" CssClass="wideBtn" TabIndex="5"
                                    OnClick="btnSearch_Click"></asp:Button>
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
                                            <asp:BoundField DataField="Name" HeaderText="房间号" />
                                            <asp:BoundField DataField="RoomTypeName" HeaderText="房间类型" />
                                            <%--<asp:BoundField DataField="RoomType2" HeaderText="房间类型2" />--%>
                                            <asp:BoundField DataField="RoomSexType" HeaderText="性别分类" />
                                            <asp:BoundField DataField="FreeBedCount" HeaderText="空闲床位数" />
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <td class="pageleft">
                                               <%-- <asp:Button ID="btnAllLock" Text="锁定" CssClass="publicBtn" runat="server" OnClientClick="return Lock()" OnClick="btnAllLock_Click"></asp:Button>--%>
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

<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DormInfo.aspx.cs" Inherits="DormManage.Web.UI.Report.DormInfo" %>

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
            <h3 class="nav_left">我的位置： <span id="navigation">报表->宿舍基本信息</span>
            </h3>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar" style="display:none;">
                    <table>
                        <tr style="display:none;">
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
                        <tr >
                            <th><asp:Button ID="btnExport" Text="导 出" runat="server" class="exportBtn" OnClick="btnExport_Click"></asp:Button>
                                <%--<asp:Label ID="lblRoomType" runat="server" Text="房间类型："></asp:Label>--%></th>
                            <td>
                                <%--<asp:DropDownList ID="ddlRoomType" runat="server"></asp:DropDownList>--%></td>
                            <th style="display: none">
                                <%--<asp:Label ID="lblRoomType2" runat="server" Text="房间类型2："></asp:Label>--%></th>
                            <td style="display: none">
                                <%--<asp:DropDownList ID="ddlRoomType2" runat="server">
                                    <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="1">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList>--%></td>
                            <th>
                                <%--<asp:Label ID="lblRoomSexType" runat="server" Text="性别分类："></asp:Label>--%></th>
                            <td>
                                <%--<asp:DropDownList ID="ddlRoomSexType" runat="server">
                                    <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <td>
                                <%--<asp:Button ID="btnSearch" Text="查 询" runat="server" class="findBtn" TabIndex="5" 
                                    OnClick="btnSearch_Click"></asp:Button>--%>
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
                                        AutoGenerateColumns="False" EnableEmptyContentRender="True"  
                                        OnRowDataBound="GridView1_RowDataBound" CellSpacing="2">
                                        <Columns>                                            
                                            <asp:BoundField DataField="areaname" HeaderText="区域(Area)"/>
                                            <asp:BoundField DataField="grade" HeaderText="级别(Grade)" />
                                            <asp:BoundField DataField="DormNo" HeaderText="栋号(DormNo)" />
                                            <asp:BoundField DataField="TotalBedsQty" HeaderText="可入住总床位数" />
                                            <asp:BoundField DataField="OccupiedQty" HeaderText="已入住人数" />
                                            <asp:BoundField DataField="VacantQty" HeaderText="空闲床位数(Vacant)" />
                                            <asp:BoundField DataField="Occupancyrate" HeaderText="实际入住率(%)" />
                                        </Columns>
                                    </cc1:GridView>
                                </div>                                
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>

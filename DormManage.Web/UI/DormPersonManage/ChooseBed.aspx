<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChooseBed.aspx.cs" EnableEventValidation="false" Inherits="DormManage.Web.UI.DormPersonManage.ChooseBed" %>

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
    <script src="../../Scripts/ligerUI1.1.9/js/ligerui.all.js" type="text/javascript"></script>
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
                $("#<%=this.ddlRoom.ClientID%>").html("<option value='0' select='selected'>--请选择--</option>");
            });

            $("#<%=this.ddlBuildingName.ClientID%>").change(function () {
                var html = "<option value='0' select='selected'>--请选择--</option>";
                if (document.getElementById("thUnitName").style.display != "none") {
                    if ($(this).val() != "0") {
                        var ajaxData = DormManageAjaxServices.GetUnitByBuildingID($(this).val());
                        for (var i = 0; i < ajaxData.value.Rows.length; i++) {
                            html += "<option value='" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Unit.col_ID%>"] + "'>" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Unit.col_Name%>"] + "</option>"
                        }
                    }
                    $("#<%=this.ddlUnit.ClientID%>").html(html);
                    $("#<%=this.ddlFloor.ClientID%>").html("<option value='0' select='selected'>--请选择--</option>");
                    $("#<%=this.ddlRoom.ClientID%>").html("<option value='0' select='selected'>--请选择--</option>");
                }
                else {
                    if ($(this).val() != "0") {
                        var ajaxData = DormManageAjaxServices.GetRoomByBuildingID($(this).val());
                        for (var i = 0; i < ajaxData.value.Rows.length; i++) {
                            html += "<option value='" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Room.col_ID%>"] + "'>" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Room.col_Name%>"] + "</option>"
                        }
                    }
                    $("#<%=this.ddlRoom.ClientID%>").html(html);
                }
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
                $("#<%=this.ddlRoom.ClientID%>").html("<option value='0' select='selected'>--请选择--</option>");
            });

            $("#<%=this.ddlFloor.ClientID%>").change(function () {
                var html = "<option value='0' select='selected'>--请选择--</option>";
                if ($(this).val() != "0") {
                    var ajaxData = DormManageAjaxServices.GetRoomByFloorID($(this).val());
                    for (var i = 0; i < ajaxData.value.Rows.length; i++) {
                        html += "<option value='" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Room.col_ID%>"] + "'>" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Room.col_Name%>"] + "</option>"
                    }
                }
                $("#<%=this.ddlRoom.ClientID%>").html(html);
            });
        })

        //换房
        function changeRoom() {
            var recordId = "";
            var gridView = $("#<%=this.GridView1.ClientID %>")[0];
            var $chkSelect = $("input[name='chkSelect']:checked");
            if ($chkSelect.length == 0) {
                alert("Please select one record.");
            }
            else {
                if (confirm('确定换房？')) {
                    recordId = $chkSelect.val();
                    var rowIndex = $("input[name='chkSelect']:checked")[0].parentElement.parentElement.rowIndex;
                   
                    var sex = $.trim(gridView.rows[rowIndex].cells[4].innerText);
                  
                  if (sex != '<%=Request.QueryString["Sex"]%>') {
                        alert("sorry,选择房间性别分类有误！")
                    }
                    else {
                        var ajaxData = DormPersonManageAjaxServices.ChangeRoom('<%=Request.QueryString["id"]%>', recordId);
                        if (ajaxData.error != null) {
                        }
                        else {
                            alert("换房成功！");
                            window.parent.cancel();
                        }
                    }
                }
            }
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
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
                            <%--/************************************************************************************/--%>
                            <th style="display: none" id="thUnitName">
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
                            <%--/************************************************************************************/--%>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lblRoom" runat="server" Text="房间号："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlRoom" runat="server"></asp:DropDownList></td>
                            <%--                            <th>
                                <asp:Label ID="lblRoomType" runat="server" Text="房间类型："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType" runat="server"></asp:DropDownList></td>
                            <th>
                                <asp:Label ID="lblRoomSexType" runat="server" Text="性别分类："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType" runat="server">
                                    <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>
                                <asp:Label ID="lblBedStatus" runat="server" Text="床位状态："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlBedStatus" runat="server">
                                    <asp:ListItem Selected="True" Value="0">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="1">空闲</asp:ListItem>
                                    <asp:ListItem Value="2">已分配未入住</asp:ListItem>
                                    <asp:ListItem Value="3">已入住</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>
                                <asp:Label ID="lblBed" runat="server" Text="床位号："></asp:Label></th>
                            <td>
                                <asp:TextBox ID="txtBed" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" CssClass="findBtn" TabIndex="5"
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
                                                <%--                                                <HeaderTemplate>
                                                    <input type="checkbox" id="chkLeftAll" onclick="CheckAll(this)" />
                                                </HeaderTemplate>--%>
                                                <ItemTemplate>
                                                    <input type="radio" id="chkLeftSingle" value="<%#Eval("ID") %>" name="chkSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="DormAreaName" HeaderText="宿舍区" />
                                            <asp:BoundField DataField="BuildingName" HeaderText="楼栋" />
                                            <asp:BoundField DataField="UnitName" HeaderText="单元" />
                                            <asp:BoundField DataField="FloorName" HeaderText="楼层" />
                                            <asp:BoundField DataField="RoomTypeName" HeaderText="房间类型" />
                                            <asp:BoundField DataField="RoomSexType" HeaderText="性别分类" />
                                            <asp:BoundField DataField="RoomName" HeaderText="房间号" />
                                            <asp:BoundField DataField="Name" HeaderText="床位号" />
                                            <asp:TemplateField HeaderText="床位状态">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltlBedStatus" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <td class="pageleft">
                                                <asp:Button ID="btnChangeRoom" Text="换房" runat="server" CssClass="publicBtn" TabIndex="6" OnClientClick="return changeRoom()"></asp:Button>
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

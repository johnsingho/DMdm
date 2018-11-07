<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewRoom.aspx.cs" Inherits="DormManage.Web.UI.DormManage.Room.NewRoom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../../../Styles/reset.css" />
    <link rel="stylesheet" href="../../../Styles/main.css" />
    <link rel="stylesheet" href="../../../Styles/style.css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../Scripts/common.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%=this.txtRoom.ClientID%>").focus(function () {
                $("#RoomRequired").html("");
            });
        })
        $(function () {
            $("#<%=this.ddlDormArea.ClientID%>").change(function () {
                var html = "<option value='0' select='selected'>--请选择--</option>";
                if ($(this).val() != "0") {
                    var ajaxData = DormManageAjaxServices.GetBuildingByDormAreaID($(this).val());
                    for (var i = 0; i < ajaxData.value.Rows.length; i++) {
                        html += "<option value='" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Building.col_ID%>"] + "'>" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Building.col_Name%>"] + "</option>"
                    }
                }
                $("#<%=this.ddlBuilding.ClientID%>").html(html);
                $("#<%=this.ddlUnit.ClientID%>").html("<option value='0' select='selected'>--请选择--</option>");
                $("#<%=this.ddlFloor.ClientID%>").html("<option value='0' select='selected'>--请选择--</option>");
            });

            $("#<%=this.ddlBuilding.ClientID%>").change(function () {
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

        })

        function cancel() {
            window.parent.cancel();
        }

        function save() {
            if ($("#<%=this.ddlDormArea.ClientID%>").val() == "0") {
                $("#<%=this.ddlDormArea.ClientID%>").focus();
                return false;
            }
            if ($("#<%=this.ddlBuilding.ClientID%>").val() == "0") {
                $("#<%=this.ddlBuilding.ClientID%>").focus();
                return false;
            }
            if ($("#<%=this.ddlUnit.ClientID%>").val() == "0" && document.getElementById("trUnit").style.display != "none") {
                $("#<%=this.ddlUnit.ClientID%>").focus();
                return false;
            }
            if ($("#<%=this.ddlFloor.ClientID%>").val() == "0" && document.getElementById("trFloor").style.display != "none") {
                $("#<%=this.ddlFloor.ClientID%>").focus();
                return false;
            }
            if ($("#<%=this.ddlRoomType.ClientID%>").val() == "0") {
                $("#<%=this.ddlRoomType.ClientID%>").focus();
                return false;
            }
          <%-- if ($("#<%=this.ddlRoomType2.ClientID%>").val() == "") {
                $("#<%=this.ddlRoomType2.ClientID%>").focus();
                return false;
            }--%>
            if ($("#<%=this.ddlRoomSexType.ClientID%>").val() == "") {
                $("#<%=this.ddlRoomSexType.ClientID%>").focus();
                return false;
            }
            if ($.trim($("#<%=this.txtRoom.ClientID%>").val()) == "") {
                $("#RoomRequired").html("必填");
                return false;
            }
            var entity = {};
            entity.ID = '<%=Request.QueryString["id"]%>' == "" ? 0 : '<%=Request.QueryString["id"]%>';
            entity.Name = $('#<%=this.txtRoom.ClientID%>').val();
            entity.SiteID = '<%=base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID%>';
            entity.Creator = '<%=base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount%>';
            entity.UpdateBy = '<%=base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount%>';
            entity.DormAreaID = $("#<%=this.ddlDormArea.ClientID%>").val();
            entity.BuildingID = $("#<%=this.ddlBuilding.ClientID%>").val();
            entity.UnitID = $("#<%=this.ddlUnit.ClientID%>").val();
            entity.FloorID = $("#<%=this.ddlFloor.ClientID%>").val();
            entity.RoomSexType = $("#<%=this.ddlRoomSexType.ClientID%>").val();
            entity.RoomType = $("#<%=this.ddlRoomType.ClientID%>").val();
            entity.RoomType2 = $("#<%=this.ddlRoomType2.ClientID%>").val() == "" ? 0 : $("#<%=this.ddlRoomType2.ClientID%>").val();
           <%-- entity.KeyCount = $("#<%=this.txtKeyCount.ClientID%>").val();--%>
            var ajaxData = DormManageAjaxServices.EditRoom(entity);
            if (ajaxData.value != "") {
                alert(ajaxData.value);
            }
            else {
                window.parent.location = "RoomDefine.aspx";
            }
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
                            <asp:Label ID="lblDormArea" runat="server" Text="宿舍区："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlDormArea" runat="server" TabIndex="1"></asp:DropDownList>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblBuilding" runat="server" Text="楼栋："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlBuilding" runat="server" TabIndex="2"></asp:DropDownList>
                            <span>*</span>
                        </td>
                    </tr>
                    <%--/******************************************************************************/--%>
                    <tr style="display: none" id="trUnit">
                        <th>
                            <asp:Label ID="lblUnit" runat="server" Text="单元："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="3"></asp:DropDownList>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr style="display: none" id="trFloor">
                        <th>
                            <asp:Label ID="lblFloor" runat="server" Text="楼层："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlFloor" runat="server" TabIndex="4"></asp:DropDownList>
                            <span>*</span>
                        </td>
                    </tr>
                    <%--/******************************************************************************/--%>
                    <tr>
                        <th>
                            <asp:Label ID="lblRoomType" runat="server" Text="房间类型："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlRoomType" runat="server" TabIndex="5"></asp:DropDownList>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <th>
                            <asp:Label ID="lblRoomType2" runat="server" Text="房间类型2："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlRoomType2" runat="server" TabIndex="6">
                                <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">员工宿舍</asp:ListItem>
                                <asp:ListItem Value="2">家庭房</asp:ListItem>
                            </asp:DropDownList>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblRoomSexType" runat="server" Text="性别分类："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlRoomSexType" runat="server" TabIndex="7">
                                <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                <asp:ListItem Value="男">男</asp:ListItem>
                                <asp:ListItem Value="女">女</asp:ListItem>
                                <asp:ListItem Value="不限">不限</asp:ListItem>
                            </asp:DropDownList>
                            <span>*</span>
                        </td>
                    </tr>

                  <%--   <tr>
                        <th>
                            <asp:Label ID="Label1" runat="server" Text="钥匙数量："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtKeyCount" runat="server" TabIndex="9"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <th>
                            <asp:Label ID="lblRoom" runat="server" Text="房间号："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtRoom" runat="server" TabIndex="8"></asp:TextBox>
                            <span>*</span>&nbsp;&nbsp;&nbsp<span id="RoomRequired" style="font-size: small"></span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="pagerbar">
                <input id="btnSave" type="button" value="保 存" onclick="save()" />
                <input id="btnCancel" type="button" value="取 消" onclick="cancel()" />
            </div>
        </div>
    </form>
</body>
</html>

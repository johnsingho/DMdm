<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewFloor.aspx.cs" Inherits="DormManage.Web.UI.DormManage.Floor.NewFloor" %>

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
            $("#<%=this.txtFloor.ClientID%>").focus().val($("#<%=this.txtFloor.ClientID%>").val());
            $("#<%=this.txtFloor.ClientID%>").focus(function () {
                $("#FloorRequired").html("");
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
            })
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
            if ($("#<%=this.ddlUnit.ClientID%>").val() == "0") {
                $("#<%=this.ddlUnit.ClientID%>").focus();
                return false;
            }
            if ($.trim($("#<%=this.txtFloor.ClientID%>").val()) == "") {
                $("#FloorRequired").html("必填");
                return false;
            }
            var entity = {};
            entity.ID = '<%=Request.QueryString["id"]%>' == "" ? 0 : '<%=Request.QueryString["id"]%>';
            entity.Name = $('#<%=this.txtFloor.ClientID%>').val();
            entity.SiteID='<%=(base.UserInfo == null ? base.SystemAdminInfo.SiteID : base.UserInfo.SiteID)%>';
            entity.UpdateBy = '<%=(base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount)%>';
            entity.Creator = '<%=(base.UserInfo == null ? base.SystemAdminInfo.Account : base.UserInfo.ADAccount)%>';
            entity.DormAreaID = $("#<%=this.ddlDormArea.ClientID%>").val();
            entity.BuildingID = $("#<%=this.ddlBuilding.ClientID%>").val();
            entity.UnitID = $("#<%=this.ddlUnit.ClientID%>").val();
            var ajaxData = DormManageAjaxServices.EditFloor(entity);
            if (ajaxData.value != "") {
                alert(ajaxData.value);
            }
            else {
                window.parent.location = "FloorDefine.aspx";
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
                    <tr>
                        <th>
                            <asp:Label ID="lblUnit" runat="server" Text="单元："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="3"></asp:DropDownList>
                            <span>*</span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblFloor" runat="server" Text="楼层："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtFloor" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                            <span>*</span>&nbsp;&nbsp;&nbsp<span id="FloorRequired" style="font-size: small"></span>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewUnit.aspx.cs" Inherits="DormManage.Web.UI.DormManage.Unit.NewUnit" %>

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
            $("#<%=this.txtUnitName.ClientID%>").focus().val($("#<%=this.txtUnitName.ClientID%>").val());
            $("#<%=this.txtUnitName.ClientID%>").focus(function () {
                $("#UnitNameRequired").html("");
            })
        })

        function cancel() {
            window.parent.cancel();
        }

        function save() {
            if ($("#<%=this.ddlDormAreaName.ClientID%>").val() == "0") {
                $("#<%=this.ddlDormAreaName.ClientID%>").focus();
                return false;
            }
            if ($("#<%=this.ddlBuildingName.ClientID%>").val() == "0") {
                $("#<%=this.ddlBuildingName.ClientID%>").focus();
                return false;
            }
            if ($.trim($("#<%=this.txtUnitName.ClientID%>").val()) == "") {
                $("#UnitNameRequired").html("必填");
                return false;
            }
            $.post("AjaxHander.aspx",
                {
                    "DormAreaID": $("#<%=this.ddlDormAreaName.ClientID%>").val(),
                    "BuildingID": $("#<%=this.ddlBuildingName.ClientID%>").val(),
                    "UnitName": $("#<%=this.txtUnitName.ClientID%>").val(),
                    "ID": "<%=Request.QueryString["id"]%>"
                },
                function (data) {
                    if (data == "") {
                        //window.parent.frames["列表iframe名字"].location = "兄弟iframe的url"
                        window.parent.location = "UnitDefine.aspx";
                    }
                    else {
                        $("#<%=this.txtUnitName.ClientID%>").focus().val($("#<%=this.txtUnitName.ClientID%>").val());
                        alert(data);
                    }
                })
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
                            <asp:Label ID="lblDormAreaName" runat="server" Text="宿舍区："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlDormAreaName" runat="server" TabIndex="1" AutoPostBack="True" OnSelectedIndexChanged="ddlDormAreaName_SelectedIndexChanged"></asp:DropDownList>
                            <span>*</span>&nbsp;&nbsp;&nbsp<span id="DormAreaNameRequired" style="font-size: small"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblBuildingName" runat="server" Text="楼栋："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlBuildingName" runat="server"></asp:DropDownList>
                            <span>*</span>&nbsp;&nbsp;&nbsp<span id="BuildingNameRequired" style="font-size: small"></span>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblUnitName" runat="server" Text="单元："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtUnitName" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                            <span>*</span>&nbsp;&nbsp;&nbsp<span id="UnitNameRequired" style="font-size: small"></span>
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

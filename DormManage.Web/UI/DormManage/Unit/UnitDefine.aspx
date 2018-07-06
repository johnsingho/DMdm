<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitDefine.aspx.cs" Inherits="DormManage.Web.UI.DormManage.Unit.UnitDefine" EnableEventValidation="false" %>

<%@ Register Assembly="Ctl" Namespace="ExtendGridView" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title></title>
    <link rel="stylesheet" href="../../../Styles/main.css" />
    <link rel="stylesheet" href="../../../Styles/style.css" />
    <link href="../../../Styles/reset.css" rel="stylesheet" />
    <link href="../../../Styles/showLoading.css" rel="stylesheet" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../../Scripts/common.js" type="text/javascript"></script>
    <script src="../../../Scripts/jquery.showLoading.js" type="text/javascript"></script>
    <script src="../../../Scripts/ligerUI1.1.9/js/ligerui.all.js"></script>
    <link href="../../../Scripts/ligerUI1.1.9/skins/Aqua/css/ligerui-all.css" rel="stylesheet" />
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
            });
        })

        function add() {
            $.ligerDialog.open({
                title: "添加",
                width: 450,
                height: 250,
                isResize: true,
                url: 'NewUnit.aspx'
            });
            return false;
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

        function cancel() {
            $.ligerDialog.close();
        }

        function view(id) {
            $.ligerDialog.open({
                title: "编辑",
                width: 450,
                height: 250,
                isResize: true,
                url: 'NewUnit.aspx?id=' + id
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">宿舍基本管理->单元</span>
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
                            <th>
                                <asp:Label ID="lblUnitName" runat="server" Text="单元："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtUnitName" runat="server" TabIndex="3"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" class="findBtn" TabIndex="4"
                                    OnClick="btnSearch_Click"></asp:Button>
                                <asp:Button ID="btnNew" Text="新 增" runat="server" class="newBtn" TabIndex="5" OnClick="btnNew_Click" OnClientClick="return add()"></asp:Button>
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
                                            <asp:BoundField DataField="Name" HeaderText="单元" />
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <td class="pageleft">
                                                <%--<asp:Button ID="btnRemove" Text="删除" CssClass="deleteBtn" runat="server" TabIndex="7" OnClick="btnRemove_Click" OnClientClick="return remove()"></asp:Button>--%>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BedDefine.aspx.cs" Inherits="DormManage.Web.UI.DormManage.Bed.BedDefine" EnableEventValidation="false" %>

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
    <script src="../../../Scripts/ligerUI1.1.9/js/ligerui.all.js" type="text/javascript"></script>
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

        function add() {
            $.ligerDialog.open({
                title: "添加",
                width: 550,
                height: 380,
                isResize: true,
                url: 'NewBed.aspx'
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
                width: 550,
                height: 380,
                isResize: true,
                url: 'NewBed.aspx?id=' + id
            });
        }

        function upload() {
            if ($('#<%=this.FileUpload1.ClientID%>').val() == "") {
                alert("请选择需要上传的文件！");
                return false;
            }
            return true;
        }

        function changeEnable(obj) {
            if (confirm("确认更改选中床位的状态？")) {

                var id = $(obj).attr("name");
                var table = $("#<%=this.GridView1.ClientID%>");
                var tr = $(obj).parent().parent(); //获取当前行
                var index = $("#<%=this.GridView1.ClientID%> tr").index(tr); //获取行索引

                var value = document.getElementById(id).value;

               

                var ajaxData = DormManageAjaxServices.ChangeBegEnable( id, value);
                if (ajaxData.error != null) {
                    alert("更改状态失败"+ajaxData.error.Message);
                   
                }
                else {
                    alert("更改状态成功！");
                    document.getElementById("btnSearch").click()
                  
                }
            }
        }
        
        function getMultipleCheck() {
            var selects = [];
            var rows = $('#<%=this.GridView1.ClientID%> tr');
            rows.each(function (i, tr) {
                if (!tr.cells || !tr.cells[0].children) { return; }
                if (tr.cells[0].children[0].checked) {
                    var vid = $("input.publicBtn", tr).prop('id');
                    if (vid && !isNaN(vid)) {
                        selects.push(vid);
                    }
                }
            });
            return selects;
        }
        function batchEnable() {
            var selects = getMultipleCheck();
            var cnt = selects.length;
            var sprompt = '确认启用所选的'+cnt+'个床位？'
            if (cnt > 0 && confirm(sprompt)) {
                var ajaxData = DormManageAjaxServices.BatchChangeBedEnable(selects, true);
                if (ajaxData.error != null) {
                    alert("批量启用失败：" + ajaxData.error.Message);
                }
                else {
                    alert("批量启用成功！");
                }
                reload();
            } else {
                alert("请勾选需要操作的床位！");
            }
            return false;
        }
        function batchDisable() {
            var selects = getMultipleCheck();
            var cnt = selects.length;
            var sprompt = '确认禁用所选的' + cnt + '个床位？'
            if (cnt > 0 && confirm(sprompt)) {
                var ajaxData = DormManageAjaxServices.BatchChangeBedEnable(selects, false);
                if (ajaxData.error != null) {
                    alert("批量禁用失败：" + ajaxData.error.Message);
                }
                else {
                    alert("批量禁用成功！");
                }
                reload();
            } else {
                alert("请勾选需要操作的床位！");
            }
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">宿舍基本管理->床位号</span>
            </h3>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
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
                            <th>
                                <asp:Label ID="lblBedStatus" runat="server" Text="床位状态："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlBedStatus" runat="server">
                                    <asp:ListItem Selected="True" Value="0">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="1">空闲</asp:ListItem>
                                    <asp:ListItem Value="2">已分配未入住</asp:ListItem>
                                    <asp:ListItem Value="3">已入住</asp:ListItem>
                                </asp:DropDownList></td>
                            <th>
                                <asp:Label ID="lblBed" runat="server" Text="床位号："></asp:Label></th>
                            <td>
                                <asp:TextBox ID="txtBed" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" class="findBtn" TabIndex="5"
                                    OnClick="btnSearch_Click"></asp:Button>
                                <asp:Button ID="btnNew" Text="新 增" runat="server" class="newBtn" TabIndex="6" OnClientClick="return add()"></asp:Button>
                                <asp:Button ID="btnBatchEnable" Text="批量启用" runat="server" class="wideBtn" TabIndex="7" OnClientClick="return batchEnable()"></asp:Button>
                                <asp:Button ID="btnBatchDisable" Text="批量禁用" runat="server" class="wideBtn" TabIndex="8" OnClientClick="return batchDisable()"></asp:Button>
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
                                            <asp:BoundField DataField="UnitName" HeaderText="单元" />
                                            <asp:BoundField DataField="FloorName" HeaderText="楼层" />
                                            <%--                                            <asp:BoundField DataField="RoomTypeName" HeaderText="房间类型" />
                                            <asp:BoundField DataField="RoomSexType" HeaderText="性别分类" />--%>
                                            <asp:BoundField DataField="RoomName" HeaderText="房间号" />
                                            <asp:BoundField DataField="Name" HeaderText="床位号" />
                                            <asp:TemplateField HeaderText="床位状态">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltlBedStatus" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="KeyCount" HeaderText="钥匙数量" />
                                            <asp:TemplateField HeaderText="是否启用">
                                                <ItemTemplate>
                                                    <input id="<%#Eval("ID") %>" name="<%#Eval("ID") %>" type="button" value="<%#Eval("IsEnable") %>" class="publicBtn" onclick="changeEnable(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc1:GridView>
                                </div>

                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <td class="pageleft">
                                                <%--<asp:Button ID="btnRemove" Text="删除" CssClass="deleteBtn" runat="server" OnClick="btnRemove_Click" OnClientClick="return remove()"></asp:Button>--%>
                                            </td>
                                            <td class="pageright">
                                                <cc1:Pager ID="Pager1" runat="server" OnCommand="pagerList_Command" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td class="pageright">
                                                <a href="../../../FileTemplate/宿舍信息导入模板.xlsx">Download Template</a>
                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                <asp:Button ID="btnImport" runat="server" class="importBtn" Text="导 入" OnClick="btnImport_Click" />
                                                <asp:Button ID="btnUpload" runat="server" class="exportBtn" Text="上传" OnClick="btnUpload_Click" OnClientClick="return upload()" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Pager1" />
                        <asp:AsyncPostBackTrigger ControlID="btnImport" />
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:PostBackTrigger ControlID="btnUpload" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>

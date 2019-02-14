<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuickAssignRoom.aspx.cs" Inherits="DormManage.Web.UI.AssignRoom.QuickAssignRoom" EnableEventValidation="false" %>

<!DOCTYPE html>
<%@ Register Assembly="Ctl" Namespace="ExtendGridView" TagPrefix="cc1" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title>待入职床位分配</title>
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
        function ClearIDInfo() {
            $("#<%=txtScanCardNO.ClientID %>").val('');
            $("#<%=txtScanName.ClientID %>").val('');
        }

        function MyGetData()//OCX读卡成功后的回调函数
        {
            x = document.getElementById("GT2ICROCX");
            $("#<%=txtScanCardNO.ClientID %>").val(x.CardNo); //<-- 卡号--!>
            $("#<%=txtScanName.ClientID %>").val(x.Name); //<-- 名字--!>
        }

        function MyClearData()//OCX读卡失败后的回调函数
        {
            //ClearIDInfo();
        }

        function MyGetErrMsg()//OCX读卡消息回调函数
        {
            //Status.value = GT2ICROCX.ErrMsg;
        }

        function StartRead()//开始读卡
        {
            x = document.getElementById("GT2ICROCX");
            x.Start() //循环读卡
        }
        if (isIE()) {
            setInterval(StartRead, 1000); //设置每隔1秒钟读取一次
        }
    </script>
    <script type="text/javascript" for="GT2ICROCX" event="GetData">
        MyGetData()
    </script>
    <script type="text/javascript" for="GT2ICROCX" event="GetErrMsg">
        MyGetErrMsg()
    </script>
    <script type="text/javascript" for="GT2ICROCX" event="ClearData">
        MyClearData()
    </script>

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
        });

        function ddlBuildingNameChange() {
            var ddl = document.getElementById("ddlBuildingName")
            var html = "<option value='0' select='selected'>--请选择--</option>";
            if (ddl.options[ddl.selectedIndex].value != "0") {
                var ajaxData = DormManageAjaxServices.GetRoomByBuildingID(ddl.options[ddl.selectedIndex].value);
                for (var i = 0; i < ajaxData.value.Rows.length; i++) {
                    html += "<option value='" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Room.col_ID%>"] + "'>" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Room.col_Name%>"] + "</option>"
                }
            }
            $("#<%=this.ddlRoom.ClientID%>").html(html);
        }

        function ddlRoomChange() {
            var ddl = document.getElementById("ddlRoom")
            var html = "<option value='0' select='selected'>--请选择--</option>";
            if (ddl.options[ddl.selectedIndex].value != "0") {
                var ajaxData = DormManageAjaxServices.GetBedByRoomID(ddl.options[ddl.selectedIndex].value);
                for (var i = 0; i < ajaxData.value.Rows.length; i++) {
                    html += "<option value='" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Bed.col_ID%>"] + "'>" + ajaxData.value.Rows[i]["<%=DormManage.Models.TB_Bed.col_Name%>"] + "</option>"
                }
            }
            $("#<%=this.ddlBeg.ClientID%>").html(html);
        }


        function checkPara() {
            var sid = $.trim($("#<%=txtScanCardNO.ClientID%>").val());
            if (!CheckIdCard(sid) && !confirm('身份证不是有效的中国身份证号，要继续使用吗？')) {
                return false;
            }
            var recordId = "";
            var $chkSelect = $("input[name='chkSelect']:checked");
            if ($chkSelect.length == 0) {
                alert("Please select one record.");
            }
            else {
                if (confirm('确定分配？')) {
                    recordId = $chkSelect.val();
                    document.getElementById("selbagID").value = recordId;
                    return true;
                }
            }
            return false;
        }

    </script>
</head>
<body>
    <object id="GT2ICROCX" name="GT2ICROCX" width="0" height="0"
        classid="CLSID:220C3AD1-5E9D-4B06-870F-E34662E2DFEA"
        codebase="IdrOcx.cab#version=1,0,1,2">
    </object>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">房间分配->待入职床位分配</span>
            </h3>
        </div>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar">
                    <table>
                        <tr>
                            <th>
                                <asp:Label ID="lblScanName" runat="server" Text="姓名："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtScanName" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblIdCard" runat="server" Text="身份证："></asp:Label></th>
                            <td>
                                <asp:TextBox ID="txtScanCardNO" runat="server" Width="180px" MaxLength="18"></asp:TextBox>
                            </td>
                            <td>
                                <input type="text" id="selbagID" style="display: none" runat="server" />
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" CssClass="findBtn" TabIndex="5"
                                    OnClick="btnSearch_Click"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="btnAssign" runat="server" CssClass="publicBtn" Text="分配" OnClientClick="return checkPara()" OnClick="btnAssign_Click" />
                            </td>
                        </tr>
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
                                <asp:DropDownList ID="ddlBuildingName" runat="server" TabIndex="2" onchange="ddlBuildingNameChange()"></asp:DropDownList>
                            </td>
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
                            <th>
                                <asp:Label ID="lblRoom" runat="server" Text="房间号："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlRoom" runat="server" onchange="ddlRoomChange()"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lblRoomType" runat="server" Text="房间类型："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType" runat="server"></asp:DropDownList></td>
                            <th>
                                <asp:Label ID="lblBed" runat="server" Text="床位号："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlBeg" runat="server" TabIndex="4"></asp:DropDownList>
                            </td>
                            <th>
                                <asp:Label ID="lblRoomSexType" runat="server" Text="性别分类："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType" runat="server">
                                    <asp:ListItem Selected="True" Value="">--请选择--</asp:ListItem>
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                        </tr>
                    </table>
                </div>

                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
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
                                                <%--  <asp:Button ID="btnChangeRoom" Text="换房" runat="server" CssClass="publicBtn" TabIndex="6" OnClientClick="return changeRoom()"></asp:Button>--%>
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
                        <asp:PostBackTrigger ControlID="btnSearch" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>

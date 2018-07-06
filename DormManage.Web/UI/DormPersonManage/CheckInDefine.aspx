<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="CheckInDefine.aspx.cs" Inherits="DormManage.Web.UI.DormPersonManage.CheckInDefine" %>

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
    <script src="../../Scripts/DatePicker/WdatePicker.js"></script>
    <link href="../../Scripts/ligerUI1.1.9/skins/Aqua/css/ligerui-all.css" rel="stylesheet" />

    <script type="text/javascript">
        function MyGetData()//OCX读卡成功后的回调函数
        {
            x = document.getElementById("GT2ICROCX");
            $("#<%=this.txtScanCardNO.ClientID %>").val(x.CardNo); //<-- 卡号--!>
            $("#<%=txtWorkDayNo.ClientID%>").val(""); //清除员工卡号
            $("#<%=txtName.ClientID%>").val("");      //清除员工名字
        }

        function MyClearData()//OCX读卡失败后的回调函数
        {
            //$("#<%=this.txtScanCardNO.ClientID %>").val("");
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
        function add() {
            return false;
        }

        function remove() {
            return false;
        }

        function cancel() {
            $.ligerDialog.close();
        }

        function view(id) {
        }

        function importComplete() {
            var ajax = DormPersonManageAjaxServices.ExportImportErrorData();
            var xls, xlBook, xlSheet;
            var reportSource = ajax.value;
            if (ajax.error == null) {
                if (parseInt(reportSource[1]) == 0) {
                    $.ligerDialog.error("Sorry! there is no data to export.");
                    return;
                }
                try {
                    xls = new ActiveXObject("Excel.Application");
                }
                catch (exp) {
                    $.ligerDialog.warn("Your browser does not support ActiveXObject.", "Warn");
                    return;
                }
                var path = '<% =("http://" + Request.Url.Authority + Utility.ExcelTemplateRoot) %>员工登记信息导入信息模板.xlsx';
                xlBook = xls.Workbooks.Open(path);
                xlSheet = xlBook.Worksheets(1);
                with (xlSheet) {
                    window.clipboardData.setData("Text", reportSource[0]);
                    Paste(Cells(2, 1));
                    window.clipboardData.clearData("Text");
                }
                xls.Visible = true;
            }
            else {
                alert(ajax.error);
            }
        }

        function upload() {
            if ($('#<%=this.FileUpload1.ClientID%>').val() == "") {
                alert("请选择需要上传的文件！");
                return false;
            }
            return true;
        }
         function openEditView() {
             var id = "";
          var gridView = $("#<%=this.GridView1.ClientID %>")[0];
            var $chkSelect = $("input[name='chkSelect']:checked");
            if ($chkSelect.length == 0) {
                alert("Please select one record.");
            }
            else {
                id = $chkSelect.val();
                var rowIndex = $("input[name='chkSelect']:checked")[0].parentElement.parentElement.rowIndex;
                var employeeNo = $.trim(gridView.rows[rowIndex].cells[1].innerText);
                var name = $.trim(gridView.rows[rowIndex].cells[2].innerText);
                var BU = $.trim(gridView.rows[rowIndex].cells[4].innerText);
                var EmployeeTypeName = $.trim(gridView.rows[rowIndex].cells[5].innerText);
                var CheckInDate = $.trim(gridView.rows[rowIndex].cells[7].innerText);
                var Telephone = $.trim(gridView.rows[rowIndex].cells[13].innerText);

                $.ligerDialog.open({
                    title: "修改入住信息",
                    width: 800,
                    height: 550,
                    isResize: true,
                    url: 'EditCheckIn.aspx?id=' + id + '&employeeNo=' + employeeNo + '&name=' + escape(name) +
                        '&BU=' + escape(BU) + '&EmployeeTypeName=' + escape(EmployeeTypeName)+
                        '&CheckInDate=' + escape(CheckInDate) + '&Telephone=' + Telephone
                   
                });
            }
            
         }

        $(function () {
            bindEmpIDCtrl(10, '#<%=txtWorkDayNo.ClientID%>', '#<%=txtScanCardNO.ClientID%>', '#<%=txtName.ClientID%>');
            $('#<%=txtWorkDayNo.ClientID%>').focus();
        })
    </script>
</head>
<body>
    <object id="GT2ICROCX" name="GT2ICROCX" width="0" height="0"
        classid="CLSID:220C3AD1-5E9D-4B06-870F-E34662E2DFEA"
        codebase="IdrOcx.cab#version=1,0,1,2">
    </object>

    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">宿舍人员管理->入住记录</span>
            </h3>
        </div>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar">
                    <table>
                        <tr>
                            <th>
                                <asp:Label ID="lblEmployeeNo" runat="server" Text="工号："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtWorkDayNo" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblName" runat="server" Text="姓名："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtName" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblCardNo" runat="server" Text="身份证号码："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtScanCardNO" runat="server" MaxLength="50" TabIndex="3"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblMobile" runat="server" Text="手机号码："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtMobile" runat="server" MaxLength="11" TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lblBu" runat="server" Text="事业部："></asp:Label>
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBu" runat="server"></asp:DropDownList>
                            </td>
                            <th>开始时间：
                            </th>
                            <td>
                                <asp:TextBox ID="txtStartDay" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                            <th>结束时间：
                            </th>
                            <td>
                                <asp:TextBox ID="txtEndDay" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
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
                                <asp:DropDownList ID="ddlBuildingName" runat="server" TabIndex="2"></asp:DropDownList>
                            </td>
                            <th>
                                <asp:Label ID="lblRoomType" runat="server" Text="房间类型："></asp:Label></th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType" runat="server"></asp:DropDownList></td>
                            <th>
                                <asp:Label ID="Label1" runat="server" Text="房间号："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtRoom" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" class="findBtn" TabIndex="4"
                                    OnClick="btnSearch_Click"></asp:Button>&nbsp;
                                <asp:Button ID="btnEdit" Text="编 辑" runat="server" class="publicBtn" TabIndex="5" OnClientClick="openEditView()"></asp:Button>
                                <asp:Button ID="btnExport" Text="导 出" runat="server" class="exportBtn" TabIndex="6" OnClick="btnExport_Click"></asp:Button>

                            </td>
                        </tr>
                    </table>
                </div>
                <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="0">
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
                                               
                                                <ItemTemplate>
                                                    <input type="radio" id="chkLeftSingle" value="<%#Eval("ID") %>" name="chkSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeNo" HeaderText="工号" />
                                            <asp:BoundField DataField="Name" HeaderText="姓名" />
                                            <asp:TemplateField HeaderText="性别">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltlSex" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="BU" HeaderText="事业部" />
                                            <asp:BoundField DataField="EmployeeTypeName" HeaderText="用工类型" />
                                            <asp:BoundField DataField="CardNo" HeaderText="身份证号码" />
                                            <asp:BoundField DataField="CheckInDate" HeaderText="入住日期" />
                                            <asp:BoundField DataField="DormAreaName" HeaderText="宿舍区" />
                                            <asp:BoundField DataField="BuildingName" HeaderText="楼栋" />
                                            <%--                                            <asp:BoundField DataField="UnitName" HeaderText="单元" />
                                            <asp:BoundField DataField="FloorName" HeaderText="楼层" />--%>
                                            <asp:BoundField DataField="RoomName" HeaderText="房间号" />
                                            <asp:BoundField DataField="RoomType" HeaderText="房间类型" />
                                            <asp:BoundField DataField="BedName" HeaderText="床位号" />
                                            <asp:BoundField DataField="Telephone" HeaderText="手机号码" />
                                            
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <td class="pageleft">
                                                <%--                                                <asp:Button ID="btnRemove" Text="删除" CssClass="deleteBtn" runat="server" TabIndex="7" OnClick="btnRemove_Click" OnClientClick="return remove()"></asp:Button>--%>
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
                                                <a href="../../FileTemplate/员工登记信息导入信息模板.xlsx">Download Template</a>
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
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                        <asp:AsyncPostBackTrigger ControlID="btnImport" />
                         <asp:AsyncPostBackTrigger ControlID="btnEdit" />
                        <asp:PostBackTrigger ControlID="btnUpload" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>

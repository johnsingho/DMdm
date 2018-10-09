<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChargingDefine.aspx.cs" Inherits="DormManage.Web.UI.DormPersonManage.ChargingDefine" %>

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
    <script src="../../Scripts/DatePicker/WdatePicker.js"></script>
    <script src="../../Scripts/ligerUI1.1.9/js/ligerui.all.js"></script>
    <link href="../../Scripts/ligerUI1.1.9/skins/Aqua/css/ligerui-all.css" rel="stylesheet" />

    <script type="text/javascript">
        function add() {
            $.ligerDialog.open({
                title: "添加",
                width: 550,
                height: 450,
                isResize: true,
                url: 'NewCharging.aspx?edit=false'
            });
            return false;
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
                var BU = $.trim(gridView.rows[rowIndex].cells[3].innerText);             
                var Money = $.trim(gridView.rows[rowIndex].cells[4].innerText);             
                var AirConditionFeeMoney = $.trim(gridView.rows[rowIndex].cells[5].innerText);
                var RoomKeyFeeMoney = $.trim(gridView.rows[rowIndex].cells[6].innerText);             
                var OtherFeeMoney = $.trim(gridView.rows[rowIndex].cells[7].innerText);
                var CreateTime = $.trim(gridView.rows[rowIndex].cells[8].innerText);
               
                $.ligerDialog.open({
                    title: "修改扣费记录",
                    width: 800,
                    height: 550,
                    isResize: true,
                    url: 'NewCharging.aspx?edit=true&id=' + id + '&employeeNo=' + employeeNo + '&name=' + escape(name) +
                        '&BU=' + escape(BU) + '&Money=' + escape(Money) +
                        '&AirConditionFeeMoney=' + escape(AirConditionFeeMoney) +
                        '&RoomKeyFeeMoney=' + escape(RoomKeyFeeMoney) +
                        '&OtherFeeMoney=' + escape(OtherFeeMoney) + '&CreateTime=' + CreateTime
                   
                });
            }
            
        }

        function remove() {
            return false;
        }

        function cancel() {
            $.ligerDialog.close();
        }

        function view(id) {
            $.ligerDialog.open({
                title: "编辑",
                width: 550,
                height: 450,
                isResize: true,
                url: 'NewCharging.aspx?id=' + id
            });
        }
        function importComplete() {
            var url = "/UI/Common/ExcelDownHandler.aspx?action=ImpErrCharing";
            window.open(url, "down");
            return;

            <%--var ajax = DormPersonManageAjaxServices.ExportImportErrorData();
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
                var path = '<% =("http://" + Request.Url.Authority + Utility.ExcelTemplateRoot) %>扣费记录导入模板.xlsx';
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
            }--%>
        }
        function upload() {
            if ($('#<%=this.FileUpload1.ClientID%>').val() == "") {
                alert("请选择需要上传的文件！");
                return false;
            }
            return true;
        }
        $(function () {
            bindEmpIDCtrl(10, '#<%=txtWorkDayNo.ClientID%>', null, '#<%=txtName.ClientID%>');
            $('#<%=txtWorkDayNo.ClientID%>').focus();
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">宿舍人员管理->扣费记录</span>
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
                            <td>
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" CssClass="findBtn" TabIndex="3"
                                    OnClick="btnSearch_Click"></asp:Button>
                                <asp:Button ID="btnNew" Text="新 增" runat="server" CssClass="newBtn" TabIndex="4" OnClientClick="return add()"></asp:Button>&nbsp;
                                 <asp:Button ID="btnEdit" Text="编 辑" runat="server" class="publicBtn" TabIndex="5" OnClientClick="openEditView()"></asp:Button>
                                <asp:Button ID="btnExport" runat="server" Text="导出" CssClass="exportBtn" OnClick="btnExport_Click" />
                            </td>
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
                                                 <ItemTemplate>
                                                    <input type="radio" id="chkLeftSingle" value="<%#Eval("ID") %>" name="chkSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeNo" HeaderText="工号" />
                                            <asp:BoundField DataField="Name" HeaderText="姓名" />
                                            <asp:BoundField DataField="BU" HeaderText="事业部" />
                                            <asp:BoundField DataField="Money" HeaderText="管理费" />
                                            <asp:BoundField DataField="AirConditionFeeMoney" HeaderText="空调费" />
                                            <asp:BoundField DataField="RoomKeyFeeMoney" HeaderText="钥匙费" />
                                            <asp:BoundField DataField="OtherFeeMoney" HeaderText="其他费" />
                                            <asp:BoundField DataField="Allfee" HeaderText="总金额" />
                                            <asp:BoundField DataField="CreateTime" HeaderText="日期" />
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
                                                <a href="../../FileTemplate/扣费记录导入模板.xlsx">Download Template</a>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllowanceApply.aspx.cs" Inherits="DormManage.Web.UI.Allowance.AllowanceApply" %>

<%@ Register Assembly="Ctl" Namespace="ExtendGridView" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script src="../../Scripts/DatePicker/WdatePicker.js"></script>
    <script src="../../../Scripts/ligerUI1.1.9/js/ligerui.all.js" type="text/javascript"></script>
    <link href="../../../Scripts/ligerUI1.1.9/skins/Aqua/css/ligerui-all.css" rel="stylesheet" />

    <script type="text/javascript">
        function MyGetData()//OCX读卡成功后的回调函数
        {
            x = document.getElementById("GT2ICROCX");
            $("#<%=this.txtScanCardNO.ClientID %>").val(x.CardNo); //<-- 卡号--!>
            $("#<%=txtWorkDayNo.ClientID%>").val(""); //清除员工卡号
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
          function upload() {
            if ($('#<%=this.FileUpload1.ClientID%>').val() == "") {
                alert("请选择需要上传的文件！");
                return false;
            }
            return true;
          }
        function importComplete() {
            var url = "/UI/Common/ExcelDownHandler.aspx?action=ImpErrAllowanceApply";
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
                var path = '<% =("http://" + Request.Url.Authority + Utility.ExcelTemplateRoot) %>员工住房津贴申请模板.xlsx';
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

        $(function () {
            bindEmpIDCtrl(10, '#<%=txtWorkDayNo.ClientID%>', '#<%=txtScanCardNO.ClientID%>');
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
            <h3 class="nav_left">我的位置： <span id="navigation">住房津贴->住房津贴申请</span>
            </h3>
        </div>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar">
                    <table>
                        <tr>
                            <th>
                                <asp:Label ID="Label1" runat="server" Text="工号："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtWorkDayNo" runat="server" Width="100px" MaxLength="18"></asp:TextBox>
                                <input type="text" id="Text1" style="display: none" runat="server" />
                            </td>
                            <th>
                                <asp:Label ID="lblDormAreaName" runat="server" Text="身份证："></asp:Label>
                            </th>
                            <td>
                               <asp:TextBox ID="txtScanCardNO" runat="server" Width="180px" MaxLength="18"></asp:TextBox>
                                <input type="text" id="selAreaID" style="display:none" runat="server"/>                                
                            </td>
                            <td>
                                <asp:Button ID="btnAssign" Text="申 请" runat="server"  CssClass="findBtn" TabIndex="1" OnClick="btnAssign_Click"></asp:Button>
                            </td>
                        </tr>
                        <tr>                            
                            <th>
                                <asp:Label ID="lblBu" runat="server" Text="事业部："></asp:Label>
                            </th>                            
                            <td>
                                <asp:TextBox ID="txtBu" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblEmpType" runat="server" Text="工种："></asp:Label>
                            </th>                            
                            <td>
                                <asp:DropDownList ID="ddlEmpType" runat="server" TabIndex="3"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <asp:Label ID="lblHireDate" runat="server" Text="入职日期："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtHireDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblEffectiveDate" runat="server" Text="生效日期："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtEffectiveDate" runat="server" onclick="WdatePicker({dateFmt:'yyyy-MM-01'})"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSel" Text="查 询" runat="server"  CssClass="findBtn" TabIndex="4" OnClick="btnSel_Click"></asp:Button>
                                <asp:Button ID="btnExport" Text="导 出" runat="server" class="exportBtn" TabIndex="5" OnClick="btnExport_Click"></asp:Button>
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
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeNo" HeaderText="工号" />
                                            <asp:BoundField DataField="Name" HeaderText="姓名" />
                                            <asp:BoundField DataField="CardNo" HeaderText="身份证" />
                                            <asp:BoundField DataField="Sex" HeaderText="性别" />
                                            <asp:BoundField DataField="Company" HeaderText="公司" />
                                            <asp:BoundField DataField="BU" HeaderText="BU" />
                                            <asp:BoundField DataField="Grade" HeaderText="级别" />
                                            <asp:BoundField DataField="HireDate" HeaderText="入职日期" />
                                            <asp:BoundField DataField="CheckOutDate" HeaderText="退宿时间" />
                                            <asp:BoundField DataField="EmployeeTypeName" HeaderText="工种" />
                                            <asp:BoundField DataField="AllowanceDate" HeaderText="申请时间" />
                                            <asp:BoundField DataField="EffectiveDate" HeaderText="生效时间" />
                                            <asp:BoundField DataField="BZ" HeaderText="备注" />
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
                                                <a href="../../FileTemplate/员工住房津贴申请模板.xlsx">Download Template</a>
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
                        <asp:AsyncPostBackTrigger ControlID="btnAssign" />
                         <asp:AsyncPostBackTrigger ControlID="btnImport" />
                        <asp:PostBackTrigger ControlID="btnUpload" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>

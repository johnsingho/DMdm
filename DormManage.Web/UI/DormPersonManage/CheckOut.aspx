<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="DormManage.Web.UI.DormPersonManage.CheckOut" %>

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
    <link href="../../Scripts/ligerUI1.1.9/skins/Aqua/css/ligerui-all.css" rel="stylesheet" />
    <script src="../../Scripts/DatePicker/WdatePicker.js"></script>

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

        function checkOut(obj) {

//            var oRet = $.ligerDialog.open({
//                title: "添加",
//                width: 500,
//                height: 220,
//                isResize: true,
//                url: 'CheckOutReason.aspx'
            //            });
            var id = $(obj).attr("name");
            var sFeatures = "dialogHeight:20;dialogWidth:50;resizable:yes;center:yes;status:no;scroll:no;";
            var sUrl = "CheckOutReason.aspx?checkinID="+id;
            var oRet = window.showModalDialog(sUrl, "", sFeatures);
            if (typeof (oRet) != "undefined" && oRet != null) {

               
                var table = $("#<%=this.GridView1.ClientID%>");
                var tr = $(obj).parent().parent();//获取当前行
                var index = $("#<%=this.GridView1.ClientID%> tr").index(tr);//获取行索引
                              
                var ajaxData = DormPersonManageAjaxServices.CheckOut(id, oRet);
                if (ajaxData.error != null) {
                    alert(ajaxData.error.Message);
                    alert("退房失败");
                }
                else {
                    alert("退房成功！");
                    table[0].deleteRow(index);
                }
            }

//            if (confirm("确定退房？")) {
//                var id = $(obj).attr("name");
//                var table = $("#<%=this.GridView1.ClientID%>");
//                var tr = $(obj).parent().parent();//获取当前行
//                var index = $("#<%=this.GridView1.ClientID%> tr").index(tr);//获取行索引
//                var ajaxData = DormPersonManageAjaxServices.CheckOut(id);
//                if (ajaxData.error != null) {
//                    //alert(ajaxData.error.Message);
//                    alert("退房失败");
//                }
//                else {
//                    alert("退房成功！");
//                    table[0].deleteRow(index);
//                }
//            }
        }

        $(function () {
            bindEmpIDCtrl(10, '#<%=txtWorkDayNo.ClientID%>', '#<%=txtScanCardNO.ClientID%>');
            $('#<%=txtWorkDayNo.ClientID%>').focus();
        })

        //导入批量退房
        function import_checkout() {
                $.ligerDialog.open({
                    title: "导入批量退房",
                    width: 350,
                    height: 280,
                    isResize: false,
                    url: 'CheckOutImport_form.aspx'
                });
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
            <h3 class="nav_left">我的位置： <span id="navigation">宿舍人员管理->员工退房</span>
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
                                <asp:TextBox ID="txtWorkDayNo" runat="server" MaxLength="20" TabIndex="1"></asp:TextBox>
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
                                <asp:TextBox ID="txtScanCardNO" runat="server" MaxLength="18" TabIndex="3" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>                            
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
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" CssClass="findBtn" TabIndex="4"
                                    OnClick="btnSearch_Click"></asp:Button>
                                <%-- <asp:Button ID="btnNew" Text="新 增" runat="server" class="newBtn" TabIndex="5" OnClick="btnNew_Click" OnClientClick="return add()"></asp:Button>--%>
                            </td>
                            <td>
                                <asp:Button ID="btnImport" Text="导 入" runat="server" 
                                    class="importBtn"
                                    TabIndex="5" OnClientClick="import_checkout();return false;">
                                </asp:Button>
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
                                                <HeaderTemplate>
                                                    <input type="checkbox" id="chkLeftAll" onclick="CheckAll(this)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLeftSingle" runat="server" />
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
                                            <asp:BoundField DataField="KeyCount" HeaderText="钥匙" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <input id="btnCheckOut" name="<%#Eval("ID") %>" type="button" value="退房" class="publicBtn" onclick="checkOut(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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

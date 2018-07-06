<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeRoomRecord.aspx.cs" Inherits="DormManage.Web.UI.DormPersonManage.ChangeRoomRecord" %>

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
    $(function () {
        bindEmpIDCtrl(10, '#<%=txtWorkDayNo.ClientID%>', '#<%=txtScanCardNO.ClientID%>');
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
            <h3 class="nav_left">我的位置： <span id="navigation">宿舍人员管理->换房记录</span>
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
                                <asp:Label ID="lblCardNo" runat="server" Text="身份证号码："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtScanCardNO" runat="server" MaxLength="50" TabIndex="2"></asp:TextBox>
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
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" class="findBtn" TabIndex="3"
                                    OnClick="btnSearch_Click"></asp:Button>
                            </td>
                            <td>
                                <asp:Button ID="btnExport" Text="导 出" runat="server" class="exportBtn" TabIndex="4" OnClick="btnExport_Click"></asp:Button>
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
                                            <asp:BoundField DataField="ChangeRoomDate" HeaderText="换房日期" />
                                            <asp:BoundField DataField="DormAreaName" HeaderText="宿舍区(原|新)" HtmlEncode="false" />
                                            <asp:BoundField DataField="BuildingName" HeaderText="楼栋(原|新)" HtmlEncode="false" />
                                            <%--                                            <asp:BoundField DataField="UnitName" HeaderText="单元(原|新)" HtmlEncode="false"/>
                                            <asp:BoundField DataField="FloorName" HeaderText="楼层(原|新)" HtmlEncode="false"/>--%>
                                            <asp:BoundField DataField="RoomName" HeaderText="房间号(原|新)" HtmlEncode="false" />
                                            <asp:BoundField DataField="RoomType" HeaderText="房间类型(原|新)" HtmlEncode="false" />
                                            <asp:BoundField DataField="BedName" HeaderText="床位号(原|新)" HtmlEncode="false" />
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <td class="pageleft" align="left">
                                                <%-- <asp:Button ID="btnRemove" Text="删除" CssClass="deleteBtn" runat="server" TabIndex="7" OnClick="btnRemove_Click" OnClientClick="return remove()"></asp:Button>--%>
                                            </td>
                                            <td class="pageright" align="right">
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignDormArea.aspx.cs" Inherits="DormManage.Web.UI.AssignRoom.AssignDormArea" %>

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
          function getArea() {
            var recordId = "";
          <%--  var gridView = $("#<%=this.GridView1.ClientID %>")[0];--%>
            var $chkSelect = $("input[name='chkSelect']:checked");
            if ($chkSelect.length == 0) {
                alert("Please select one record.");
            }
            else {
                if (confirm('确定分配？')) {
                    recordId = $chkSelect.val();
                    document.getElementById("selAreaID").value = recordId;
                    return true;
                }
            }
            return false;
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
            <h3 class="nav_left">我的位置： <span id="navigation">房间分配->宿舍区分配</span>
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
                                <asp:TextBox ID="txtWorkDayNo" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblDormAreaName" runat="server" Text="身份证："></asp:Label>
                            </th>
                            <td>
                               <asp:TextBox ID="txtScanCardNO" runat="server" Width="180px" MaxLength="18"></asp:TextBox>                                
                            </td>
                            <td>
                                <input type="text" id="selAreaID" style="display:none" runat="server"/>
                                <asp:Button ID="btnAssign" Text="分 配" runat="server" OnClientClick="return getArea()" CssClass="findBtn" TabIndex="2"
                                    OnClick="btnAssign_Click"></asp:Button>
                            </td>

                            <td>
                                <asp:Button ID="btnCancel" Text="取消分配" runat="server"  CssClass="publicBtn" TabIndex="2"
                                    OnClick="btnCancel_Click"></asp:Button>
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
                                                <%--                                                <HeaderTemplate>
                                                    <input type="checkbox" id="chkLeftAll" onclick="CheckAll(this)" />
                                                </HeaderTemplate>--%>
                                                <ItemTemplate>
                                                    <input type="radio" id="chkLeftSingle" value="<%#Eval("ID") %>" name="chkSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="name" HeaderText="宿舍区" />
                                              <asp:BoundField DataField="Count" HeaderText="已分配数量" />
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
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
                        <asp:AsyncPostBackTrigger ControlID="btnAssign" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>

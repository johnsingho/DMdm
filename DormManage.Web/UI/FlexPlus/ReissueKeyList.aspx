<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReissueKeyList.aspx.cs" Inherits="DormManage.Web.UI.FlexPlus.ReissueKeyList" %>

<%@ Register Assembly="Ctl" Namespace="ExtendGridView" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>补办钥匙</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
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
        function CheckboxValue() {
            var $chkSelect = $("input[name='chkSelect']:checked");
            return $chkSelect.val();
        }

        //新增
        function add() {
            var sTitle = "宿舍公告 - 添加";
            var url = "/UI/FlexPlus/DormNoticeAdd.aspx";
            $.ligerDialog.open({
                title: sTitle,
                width: 600,
                height: 400,
                isResize: true,
                url: url
            });
        }
        //编辑
        function edit() {
            var key = CheckboxValue();
            if (key != null) {
                var sTitle = "宿舍公告 - 编辑";
                var url = "/UI/FlexPlus/DormNoticeAdd.aspx?key=" + key;
                $.ligerDialog.open({
                    title: sTitle,
                    width: 600,
                    height: 400,
                    isResize: true,
                    url: url
                });
            }
        }
        function cancel() {
            $.ligerDialog.close();
        }

        //删除
        function Delete() {
            var key = CheckboxValue();
            if (key!=null) {
                DormManageAjaxServices.DelDormNotice(key);
                reload();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">Flex+后台-->补办钥匙</span>
            </h3>
        </div>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar">
                    <table>
                        <tr>
                            <th>钥匙类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlKeyTypes" runat="server"></asp:DropDownList>
                            </td>
                            <th>状态：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </td>
                            <th></th>
                            <td style="width: 400px">
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" class="publicBtn leftOff" TabIndex="4" OnClick="btnSearch_Click"></asp:Button>
                                <asp:Button ID="btnHandle" Text="审 批" runat="server" class="publicBtn leftOff" TabIndex="5" OnClientClick="return DoApply();"></asp:Button>
                            </td>
                            <td></td>
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
                                        AutoGenerateColumns="False" EnableEmptyContentRender="True" DataKeyNames="ID" OnRowDataBound="GridView1_RowDataBound" >
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderStyle Width="20px" />
                                                <ItemTemplate>
                                                    <input type="radio" id="chkLeftSingle" value="<%#Eval("ID") %>" name="chkSelect" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="NoticeTitle" HeaderText="标题" />
                                            <asp:BoundField DataField="cnt" HeaderText="访问人数" />
                                            <asp:BoundField DataField="CreateDate" HeaderText="创建时间" />
                                            <asp:TemplateField HeaderText="状态">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltlStatus" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>
</body>
</html>

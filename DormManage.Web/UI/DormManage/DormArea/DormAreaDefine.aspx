<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DormAreaDefine.aspx.cs" Inherits="DormManage.Web.UI.DormManage.DormArea.DormAreaDefine" %>

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
        function add() {
            $.ligerDialog.open({
                title: "添加",
                width: 400,
                height: 180,
                isResize: true,
                url: 'NewDormArea.aspx'
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
                width: 400,
                height: 180,
                isResize: true,
                url: 'NewDormArea.aspx?id=' + id
            });
        }
          function changeEnable(obj) {
            if (confirm("确认更改选中区域的状态？")) {

                var id = $(obj).attr("name");
                var table = $("#<%=this.GridView1.ClientID%>");
                var tr = $(obj).parent().parent(); //获取当前行
                var index = $("#<%=this.GridView1.ClientID%> tr").index(tr); //获取行索引

                var value = document.getElementById(id).value;

               

                var ajaxData = DormManageAjaxServices.ChangeDormAreaEnable(id, value);
                if (ajaxData.error != null) {
                    alert("更改状态失败"+ajaxData.error.Message);
                   
                }
                else {
                    alert("更改状态成功！");
                    document.getElementById("btnSearch").click()
                  
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">宿舍基本管理->宿舍区</span>
            </h3>
        </div>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar">
                    <table>
                        <tr>
                            <th>
                                <asp:Label ID="lblDormAreaName" runat="server" Text="宿舍区："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtDormAreaName" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" CssClass="findBtn" TabIndex="2"
                                    OnClick="btnSearch_Click"></asp:Button>
                                <asp:Button ID="btnNew" Text="新 增" runat="server" CssClass="newBtn" TabIndex="3" OnClientClick="return add()"></asp:Button>
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
                                            <asp:BoundField DataField="Name" HeaderText="宿舍区" />
                                               <asp:TemplateField HeaderText="是否启用">
                                                <ItemTemplate>
                                                    <input id="<%#Eval("ID") %>" name="<%#Eval("ID") %>" type="button" value="<%#Eval("IsEnable1") %>" class="publicBtn" onclick="changeEnable(this)" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <td class="pageleft">
                                           <%--     <asp:Button ID="btnRemove" Text="删除" CssClass="deleteBtn" runat="server" TabIndex="7" OnClick="btnRemove_Click" OnClientClick="return remove()"></asp:Button>--%>
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

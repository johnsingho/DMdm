<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyDormList.aspx.cs" Inherits="DormManage.Web.UI.FlexPlus.ApplyDormList" %>

<%@ Register Assembly="Ctl" Namespace="ExtendGridView" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>住宿申请</title>
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
        function GetSelItem() {
            var recordId = "";
            var $chkSelect = $("input[name='chkSelect']:checked");
            if ($chkSelect.length == 0) {
                return null;
            }
            return $chkSelect.toArray();
        }

        function DoApply() {
            var arrSel = GetSelItem();
            if (!arrSel || !arrSel.length) {
                alert("请选择要处理的记录.");
                return;
            }
            var keys = "";
            for (var i = 0; i < arrSel.length; i++) {
                keys += $(arrSel[i]).val() + ",";
            }
            keys=keys.slice(0, -1);
            var sUrl = 'ApplyDormHandle.aspx?keys=' + escape(keys);
            $.ligerDialog.open({
                title: "审核住宿申请",
                width: 620,
                height: 400,
                isResize: true,
                url: sUrl
            });
            return false;
        }
        function cancel() {
            $.ligerDialog.close();
        }
        function ViewRow(obj, id) {
            //var id = $(obj).attr("name");
            $.ligerDialog.open({
                title: "查看入住申请",
                width: 400,
                height: 550,
                isResize: true,
                url: 'ApplyDormView.aspx?id=' + id                   
            });
        }
        function AssignRoomRow(obj, workdayNo, idNo) {
            //var id = $(obj).attr("name");
            var sUrl = "/Index.aspx?page=/UI/AssignRoom/AssignRoom.aspx";
            var para = "?WordDayNo="+workdayNo+"&IDNo="+idNo;
            top.location.href = sUrl + escape(para);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="navigation_wrap">
            <h3 class="nav_left">我的位置： <span id="navigation">Flex+后台-->住宿申请</span>
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
                                <asp:TextBox ID="txtName" runat="server" MaxLength="50" TabIndex="2" autocomplete="off" ></asp:TextBox>
                            </td>
                            <th>
                                <asp:Label ID="lblCardNo" runat="server" Text="身份证号码："></asp:Label>
                            </th>
                            <td>
                                <asp:TextBox ID="txtScanCardNO" runat="server" MaxLength="18" TabIndex="3" Width="160px" autocomplete="off"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <th>入住类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRequiredType" runat="server"></asp:DropDownList>
                            </td>
                            <th>状态：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlStatus" runat="server"></asp:DropDownList>
                            </td>
                            <th>
                            </th>
                            <td style="width:400px">
                                <asp:Button ID="btnSearch" Text="查 询" runat="server" class="publicBtn leftOff" TabIndex="4" OnClick="btnSearch_Click"></asp:Button>
                                <asp:Button ID="btnHandle" Text="审 批" runat="server" class="publicBtn leftOff" TabIndex="5" OnClientClick="return DoApply();"></asp:Button>              
                            </td>
                            <td>                                
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
                                                <HeaderTemplate>
                                                    <input type="checkbox" id="chkLeftAll" onclick="CheckAll(this)" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <input type="checkbox" id="chkSelect" name="chkSelect" value="<%#Eval("ID")%>" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeNo" HeaderText="工号" />
                                            <asp:BoundField DataField="CName" HeaderText="姓名" />
                                            <asp:TemplateField HeaderText="性别" HeaderStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltlSex" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CardNo" HeaderText="身份证号码" HeaderStyle-Width="80px"/>
                                            <asp:BoundField DataField="MobileNo" HeaderText="手机号码" HeaderStyle-Width="100px" />
                                            <asp:BoundField DataField="Grade" HeaderText="级别" HeaderStyle-Width="25px">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DormArea" HeaderText="宿舍区" />
                                            <asp:TemplateField HeaderText="申请类型">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltlRequireType" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="RequireReason" HeaderText="申请原因" />
                                            <asp:TemplateField HeaderText="住房补贴">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltlHasHousingAllowance" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="memo" HeaderText="备注" />
                                            <asp:TemplateField HeaderText="状态">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltlStatus" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CreateDate" HeaderText="申请时间" DataFormatString="{0:yyyy-MM-dd}"/>
                                            <asp:BoundField DataField="Response" HeaderText="回复" />
                                            <asp:BoundField DataField="UpdateDate" HeaderText="更新时间" DataFormatString="{0:yyyy-MM-dd}"/>
                                           <asp:TemplateField>
                                               <ItemTemplate>
                                                   <input id="btnView" name="<%#Eval("ID") %>" type="button" value="查看" class="publicBtn"
                                                       onclick="ViewRow(this, '<%#Eval("ID")%>')" />
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <input id = "btnAssignRoom" name = "<%#Eval("ID") %>" type="button" value="分配房间" class="publicBtn"
                                                                onclick="AssignRoomRow(this, '<%#Eval("EmployeeNo")%>', '<%#Eval("CardNo")%>')" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </cc1:GridView>
                                </div>
                                <div class="pagerbar">
                                    <table>
                                        <tr>
                                            <td class="pageleft">
                                                <%--<asp:Button ID="btnRemove" Text="删除" CssClass="deleteBtn" runat="server" TabIndex="7" OnClick="btnRemove_Click" OnClientClick="return remove()"></asp:Button>--%>
                                            </td>
                                            <td class="pageright">
                                                <cc1:Pager ID="Pager1" runat="server" OnCommand="pagerList_Command"/>
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

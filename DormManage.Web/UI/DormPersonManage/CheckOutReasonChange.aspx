<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOutReasonChange.aspx.cs" Inherits="DormManage.Web.UI.DormPersonManage.CheckOutReasonChange" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改退房原因</title>
    <link rel="stylesheet" href="../../Styles/reset.css" />
    <link rel="stylesheet" href="../../Styles/main.css" />
    <link rel="stylesheet" href="../../Styles/style.css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/common.js"></script>
        
    <style>
        .list table td span {
            color: #000;
            font-size: 12px;
            position: static;
            font-weight: 500;
        }
    </style>
    <script type="text/javascript">
        function cancel() {
            window.close();
        }

        function save() {
            if ($("#<%=this.ddlReason.ClientID%>").val() == "") {
                alert("请选择退房原因！");
                $("#<%=this.ddlReason.ClientID%>").focus();
            }

            var sReason = $("#<%=this.ddlReason.ClientID%>").val();
            var bCanLeave = $("#<%=this.chkSignExit.ClientID%>").is(":checked");
            var sCanLeave = bCanLeave ? "1" : "0";
            var result = sReason + "@" + sCanLeave;
            window.returnValue = result;
            window.close();
        }

        function ReSetChkSign() {
            var sReason = $("#<%=this.ddlReason.ClientID%>").val();
            RefreshReason(sReason, "#<%=this.chkSignExit.ClientID%>");
        }
        $(function () {
            BindReasonChange("#<%=this.ddlReason.ClientID%>", "#<%=this.chkSignExit.ClientID%>");
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="list">
                <table>
                    <tr>
                        <th>
                            <asp:Label ID="lblReason" runat="server" Text="退房原因："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlReason" runat="server">
                                <asp:ListItem Value="辞职">辞职</asp:ListItem>
                                <asp:ListItem Value="自离">自离</asp:ListItem>
                                <asp:ListItem Value="外住">外住</asp:ListItem>
                                <asp:ListItem Value="解雇">解雇</asp:ListItem>
                                <asp:ListItem Value="未入职">未入职</asp:ListItem>
                                <asp:ListItem Value="调房">调房</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>                            
                        </th>
                        <td>
                            <asp:CheckBox ID="chkSignExit" runat="server" Text="同步签批离职系统" />
                        </td>
                    </tr>
                </table>
            </div>

            <div class="pagerbar">
                <input id="btnSave" type="button" value="确 认" onclick="save()" />
                <input id="btnCancel" type="button" style="margin-right:10px" value="取 消" onclick="cancel()" />
            </div>
        </div>

    </form>
</body>
</html>

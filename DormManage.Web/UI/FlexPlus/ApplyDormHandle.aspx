<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyDormHandle.aspx.cs" Inherits="DormManage.Web.UI.FlexPlus.ApplyDormHandle" %>

<%@ Register Assembly="Ctl" Namespace="AutoTextbox" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../../Styles/reset.css" />
    <link rel="stylesheet" href="../../Styles/main.css" />
    <link rel="stylesheet" href="../../Styles/style.css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/common.js"></script>
    <script src="../../Scripts/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function cancel() {
            window.parent.cancel();
        }
        function saveComplete() {
            window.parent.location = "ApplyDorm.aspx";
        }

        function save() {
            if ($.trim(($("#<%=this.txtReply.ClientID%>")).val()) == "") {
                alert("请填写回复消息！");
                $("#<%=this.txtReply.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            height: 166px;
            width: 473px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="list">
                <table>
                    <tr>
                        <th>
                            <asp:Label ID="lblHandle" runat="server" Text="是否允许住宿："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlHandle" runat="server" 
                                AutoPostBack="True" 
                                OnSelectedIndexChanged="ddlHandle_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblDormArea" runat="server" Text="宿舍区："></asp:Label>
                        </th>
                        <td>
                            <asp:DropDownList ID="ddlDormArea" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <asp:Label ID="lblReply" runat="server" Text="回复消息："></asp:Label>
                        </th>
                        <td>
                            <textarea id="txtReply" runat="server" class="auto-style1"></textarea>                           
                        </td>
                    </tr>
                </table>
            </div>
            <div class="pagerbar">
                <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return save()" OnClick="btnSave_Click" />
                <input id="btnCancel" type="button" value="取 消" onclick="cancel()" />
            </div>
        </div>
    </form>
</body>
</html>

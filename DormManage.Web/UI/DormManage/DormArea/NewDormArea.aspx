<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewDormArea.aspx.cs" Inherits="DormManage.Web.UI.DormManage.DormArea.NewDormArea" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" href="../../../Styles/reset.css" />
    <link rel="stylesheet" href="../../../Styles/main.css" />
    <link rel="stylesheet" href="../../../Styles/style.css" />
    <script src="../../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../../Scripts/common.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%=this.txtDormAreaName.ClientID%>").focus().val($("#<%=this.txtDormAreaName.ClientID%>").val());
            $("#<%=this.txtDormAreaName.ClientID%>").focus(function () {
                $("#DormAreaNameRequired").html("");
            })
        })

        function cancel() {
            window.parent.cancel();
        }

        function save() {
            if ($.trim(($("#<%=this.txtDormAreaName.ClientID%>")).val()) == "") {
                $("#DormAreaNameRequired").html("必填");
                return false;
            }
            $.post("AjaxHander.aspx",
                {
                    "DormAreaName": $("#<%=this.txtDormAreaName.ClientID%>").val(),
                    "ID": "<%=Request.QueryString["id"]%>"
                },
                function (data) {
                    if (data == "") {
                        //window.parent.frames["列表iframe名字"].location = "兄弟iframe的url"
                        window.parent.location = "DormAreaDefine.aspx";
                    }
                    else {
                        $("#<%=this.txtDormAreaName.ClientID%>").focus().val($("#<%=this.txtDormAreaName.ClientID%>").val());
                        alert(data);
                    }
                })
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <div class="list">
                <table>
                    <tr>
                        <th>
                            <asp:Label ID="lblDormAreaName" runat="server" Text="宿舍区："></asp:Label>
                        </th>
                        <td>
                            <asp:TextBox ID="txtDormAreaName" runat="server" MaxLength="50" TabIndex="1"></asp:TextBox>
                            <span>*</span>&nbsp;&nbsp;&nbsp<span id="DormAreaNameRequired" style="font-size: small"></span>
                        </td>
                    </tr>

                </table>
            </div>
            <div class="pagerbar">
                <input id="btnSave" type="button" value="保 存" onclick="save()" />
                <input id="btnCancel" type="button" value="取 消" onclick="cancel()" />
            </div>
        </div>
    </form>
</body>
</html>

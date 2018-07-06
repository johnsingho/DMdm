<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckOutImport_form.aspx.cs" Inherits="DormManage.Web.UI.DormPersonManage.CheckOutImport_form" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>上传导入退宿记录</title>
    <link rel="stylesheet" href="../../../Styles/reset.css" />
    <link rel="stylesheet" href="../../../Styles/main.css" />
    <link rel="stylesheet" href="../../../Styles/style.css" />

    <link href="../../Styles/reset.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/common.js" type="text/javascript"></script>

    <script>
        function showMessage(str) {
            $("#lblMsg").html(str);
        }
        //function Show()
        //{
        //    $("#mydiv").show();
        //}
        function cancel() {
            window.parent.cancel();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="list">
            <table>
                <tr>
                    <th>
                        <span>模板:</span>
                    </th>
                    <td>
                        <a href="../../FileTemplate/员工退宿导入信息模板.xlsx">Download Template</a>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span>附件:</span>
                    </th>
                    <td>
                        <asp:FileUpload ID="fulExcelFile" runat="server" Width="260px" />
                    </td>
                </tr>
                <tr>
                    <td class="Label" align="left" colspan="2" style="color: Red;" id="lblMsg">                        
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblResultMsgOk" runat="server" Text="" style="color:Blue; font-size: 14px;"></asp:Label>
            <asp:Label ID="lblResultMsgErr" runat="server" Text="" style="color:Red; font-size: 14px;"></asp:Label>
        </div>

        <div class="pagerbar">
            <asp:Button ID="btnImport" runat="server" Text="导 入" OnClick="btnImport_Click" />
            <asp:Button ID="btnDownFails" runat="server" Text="下载导入失败列表" OnClick="btnDownFails_Click" Enabled="False"  />
            <asp:Button ID="btnCancel" runat="server" Text="关 闭" OnClientClick="cancel();" />
            <asp:HiddenField ID="hidFailXls" runat="server" />
        </div>
        <%--    <div id='mydiv' style=' display:none;
        filter:alpha(opacity=50);  /*支持 IE 浏览器*/ -moz-opacity:0.50; /*支持 FireFox 浏览器*/ opacity:0.50;  /*支持 Chrome, Opera, Safari 等浏览器*/
        position:absolute;z-index:600;width:400px; height:200px; text-align:center;padding-top:0px; top: -3px; left: -14px; margin-top: 0px;' >
        <img src='../../Themes/Images/Waiting.gif' height="200px" width="400px">
    </div>--%>
    </form>
</body>
</html>


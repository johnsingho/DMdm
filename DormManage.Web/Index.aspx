<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="DormManage.Web.Index" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dormitory Management System</title>
    <link href="Styles/reset.css" rel="stylesheet" />
    <link href="Styles/main.css" rel="stylesheet" />
    <link rel="shortcut icon" href="images/c_favicon.ico" />
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="Scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            initIframe();
        })

        function initIframe() {
            if ($("span#hideImg").attr('show') == "1") {
                $("#frm").width($(window).width() - 209);
            }
            else {
                $("#frm").width($(window).width() - 9);
            }
            $("#frm").height($(window).height() - 20);
        }

        window.setInterval("initIframe()", 200)

        //修改密码
        function modifyPassword() {
            var answer = window.showModalDialog('ModifyPassword.aspx', '修改密码', 'dialogWidth:450px;dialogHeight:200px;center: yes;help:yes;resizable:yes;status:yes')
            return false;
        }

        function SetFrameUrl(url) {
            $("#frm").prop("src", url);
        }
        $(function () {
            SetFrameUrl($("#<%=hidTar.ClientID %>").val());
        });
    </script>
    <style>
        .flexP {
            display:inline !important;
            background: none !important;            
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="frmIndex" runat="server">
        <!--header-->
        <div class="header">
            <div>
            </div>
            <ul>
                <li>
                    <a class="flexP" style="margin-right:18px;" href="/FlexPlusIndex.aspx"> 
                        Flex+宿舍系统后台
                    </a>
                </li>
                <li>
                    <asp:Label ID="lblUserNameWelcome" runat="server" Text=""></asp:Label>
                    <asp:Button ID="btnModifyInfo" runat="server" Text="" Height="16px" OnClientClick="return modifyPassword()" />
                </li>                
                <li><span class="line"></span></li>
                <li>
                    <asp:LinkButton ID="lnkExit" CssClass="exit" runat="server" OnClick="lnkExit_Click">
                        <asp:Label ID="lblExit" runat="server" Text="退出"></asp:Label>
                    </asp:LinkButton>
                </li>
            </ul>
        </div>
        <div>
            <table class="frame">
                <tr>
                    <td id="tree_nav" style="background-color: snow">
                        <!--tree-->
                        <div id="tree">
                            <asp:TreeView ID="treModule" runat="server" ShowExpandCollapse="true"
                                Target="frm" ShowLines="true" ForeColor="Black">
                                <LevelStyles>
                                    <asp:TreeNodeStyle Font-Bold="True" Font-Size="11pt" Font-Underline="False" />
                                </LevelStyles>
                                <SelectedNodeStyle BackColor="#CCCCFF" Font-Bold="True" ForeColor="Blue" />
                            </asp:TreeView>
                        </div>
                        <!--tree end-->
                    </td>
                    <td class="middlebar">
                        <a href="#" id="mid" name="mid" onfocus="this.blur()">
                            <%--<input type="image" title="关闭" /></a>--%>
                            <span id="hideImg" style="display: block; width: 5px" show="1"></span>
                        </a>
                    </td>
                    <td valign="top">
                        <iframe runat="server" frameborder="0" id="frm" name="frm" scrolling="yes" width="100%"
                            height="100%" src="HomePage.aspx"></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <!--footer-->
        <div class="footer" id="footer">
            <table>
                <tr>
                    <td>&nbsp;&nbsp;
                    </td>
                    <td>版权所有：伟创力信息技术(深圳)有限公司
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidTar" runat="server" />
    </form>
</body>
</html>

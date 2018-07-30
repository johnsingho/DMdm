<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlexPlusIndex.aspx.cs" Inherits="DormManage.Web.FlexPlusIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Flex+ 宿舍系统后台</title>
    <link href="Styles/reset.css" rel="stylesheet" />
    <link href="Styles/main.css" rel="stylesheet" />
    <link rel="shortcut icon" href="images/c_favicon.ico" />
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="Scripts/common.js" type="text/javascript"></script>

    <style>
        .headerTitle {
            color: #d2deec;
            font-size: 24px;
            font-family: 'Microsoft YaHei','Segoe UI', Tahoma, sans-serif;
            margin-left: 18px;
            padding-top: 5px;
        }
    </style>

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

        window.setInterval("initIframe()", 200);

        function SetFrameUrl(url) {
            $("#frm").prop("src", url);
        }
        $(function () {
            SetFrameUrl($("#<%=hidTar.ClientID %>").val());
        });
    </script>
</head>
<body>
<form id="frmIndex" runat="server">
        <!--header-->
        <div class="header" style="background-color:darkslategray !important;background:none;">
            <div class="headerTitle">
                Flex+ 宿舍系统后台
            </div>
            <ul>
                <li>
                    <asp:Label ID="lblUserNameWelcome" runat="server" Text=""></asp:Label>
                </li>
                <li><span class="line"></span></li>
                <li>
                    <a href="/Index.aspx">返回宿舍管理页面</a>
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
                            height="100%" src=""></iframe>
                    </td>
                </tr>
            </table>
        </div>
        <!--footer-->
        <div class="footer" id="footer" style="background-color:darkslategray">
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

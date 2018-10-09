<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenImage.aspx.cs" Inherits="DormManage.Web.UI.Common.OpenImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Repeater ID="rp_Item" runat="server">
            <ItemTemplate>
                <p>
                    <img src='<%# DataBinder.Eval(Container.DataItem, "FileUrl") %>'
                        alt="picture" style="border:1px solid gray;"/>
                </p>
            </ItemTemplate>
            <FooterTemplate>
                <% if (rp_Item != null)
                    {
                        if (rp_Item.Items.Count == 0)
                        {
                            Response.Write("<span style='color:red;text-align:center'>没有找到您要的相关数据！</span>");
                        }
                    } %>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>

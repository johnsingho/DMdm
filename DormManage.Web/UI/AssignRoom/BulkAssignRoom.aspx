<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="BulkAssignRoom.aspx.cs" Inherits="DormManage.Web.UI.AssignRoom.BulkAssignRoom" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <title></title>
    <link rel="stylesheet" href="../../Styles/reset.css" />
    <link rel="stylesheet" href="../../Styles/main.css" />
    <link rel="stylesheet" href="../../Styles/style.css" />
    <script src="../../Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/common.js"></script>

    <script type="text/javascript">
        function MyGetData()//OCX读卡成功后的回调函数
        {
            x = document.getElementById("GT2ICROCX");
            $("#<%=this.txtScanCardNO.ClientID %>").val(x.CardNo); //<-- 卡号--!>
            $("#<%=txtWorkDayNo.ClientID%>").val(""); //清除员工卡号
        }

        function MyClearData()//OCX读卡失败后的回调函数
        {
            //$("#<%=this.txtScanCardNO.ClientID %>").val("");
        }

        function MyGetErrMsg()//OCX读卡消息回调函数
        {
            //Status.value = GT2ICROCX.ErrMsg;
        }

        function StartRead()//开始读卡
        {
            x = document.getElementById("GT2ICROCX");
            x.Start() //循环读卡
        }
        if (isIE()) {
            setInterval(StartRead, 1000); //设置每隔1秒钟读取一次
        }
    </script>
    <script type="text/javascript" for="GT2ICROCX" event="GetData">
        MyGetData()
    </script>
    <script type="text/javascript" for="GT2ICROCX" event="GetErrMsg">
        MyGetErrMsg()
    </script>
    <script type="text/javascript" for="GT2ICROCX" event="ClearData">
        MyClearData()
    </script>

    <script type="text/javascript">
        $(function () {
            //$("#<%=this.txtScanCardNO.ClientID%>").focus();
            $("#<%=this.txtWorkDayNo.ClientID%>").focus();
        });
        function addTabItem(tabid) {
            var tab = $("#" + tabid);
            var tr = $("tr", tab);
            for (var i = 0; i < tr.length; i++) {
                if (tr[i].style.display == "none") {
                    tr[i].style.display = "block";
                    return;
                }
            }
        }

        function deleteTabItem(obj) {
            var objTr = obj.parentElement.parentElement;
            //clearFileInput(objTr.cells[0].children[0]);
            //宿舍区
            objTr.cells[1].children[0].value = "0";
            //楼栋
            objTr.cells[3].children[0].value = "0";
            //房间类型
            objTr.cells[5].children[0].value = "0";
            //事业部
            objTr.cells[7].children[0].value = "";
            objTr.style.display = "none";
        }

        function OK(obj) {
            if (confirm("确认？")) {
                $("#" + obj.id).attr("style", "display:none");
                if (obj.id == "<%=this.btnOK.ClientID%>") {
                    $("input:[name=operate]").attr("style", "display:none");
                    return true;
                }
                else {
                    $("input:[name=femaleOperate]").attr("style", "display:none");
                    return true;
                }
            }
            else {
                return false
            }
        }

        function clearID(obj) {
            $("#<%=this.txtWorkDayNo.ClientID%>").val();
            $("#<%=this.txtScanCardNO.ClientID %>").val("");
        }

        function bindBuilding(obj) {
            var html = "<option value='0' select='selected'>--请选择--</option>";
            var ajaxData = DormManageAjaxServices.GetLockBuildingByDormAreaID($(obj).val());
            for (var i = 0; i < ajaxData.value.length; i++) {
                html += "<option value='" + ajaxData.value[i].BuildingID + "'>" + ajaxData.value[i].BuildingName + "</option>"
            } 
            switch (obj.id) {
                case "<%=this.ddlDormArea0.ClientID%>":
                    $("#<%=this.ddlBuilding0.ClientID%>").html(html);
                    break;
                case "<%=this.ddlDormArea1.ClientID%>":
                    $("#<%=this.ddlBuilding1.ClientID%>").html(html);
                    break;
                case "<%=this.ddlDormArea2.ClientID%>":
                    $("#<%=this.ddlBuilding2.ClientID%>").html(html);
                    break;
                case "<%=this.ddlDormArea3.ClientID%>":
                    $("#<%=this.ddlBuilding3.ClientID%>").html(html);
                    break;
                case "<%=this.ddlDormArea4.ClientID%>":
                    $("#<%=this.ddlBuilding4.ClientID%>").html(html);
                    break;
                case "<%=this.ddlDormArea5.ClientID%>":
                    $("#<%=this.ddlBuilding5.ClientID%>").html(html);
                    break;
                case "<%=this.ddlDormArea6.ClientID%>":
                    $("#<%=this.ddlBuilding6.ClientID%>").html(html);
                    break;
                case "<%=this.ddlDormArea7.ClientID%>":
                    $("#<%=this.ddlBuilding7.ClientID%>").html(html);
                    break;
                case "<%=this.ddlDormArea8.ClientID%>":
                    $("#<%=this.ddlBuilding8.ClientID%>").html(html);
                    break;
                case "<%=this.ddlDormArea9.ClientID%>":
                    $("#<%=this.ddlBuilding9.ClientID%>").html(html);
                    break;

                case "<%=this.ddlFemaleDormArea0.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding0.ClientID%>").html(html);
                    break;
                case "<%=this.ddlFemaleDormArea1.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding1.ClientID%>").html(html);
                    break;
                case "<%=this.ddlFemaleDormArea2.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding2.ClientID%>").html(html);
                    break;
                case "<%=this.ddlFemaleDormArea3.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding3.ClientID%>").html(html);
                    break;
                case "<%=this.ddlFemaleDormArea4.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding4.ClientID%>").html(html);
                    break;
                case "<%=this.ddlFemaleDormArea5.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding5.ClientID%>").html(html);
                    break;
                case "<%=this.ddlFemaleDormArea6.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding6.ClientID%>").html(html);
                    break;
                case "<%=this.ddlFemaleDormArea7.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding7.ClientID%>").html(html);
                    break;
                case "<%=this.ddlFemaleDormArea8.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding8.ClientID%>").html(html);
                    break;
                case "<%=this.ddlFemaleDormArea9.ClientID%>":
                    $("#<%=this.ddlFemaleBuilding9.ClientID%>").html(html);
                    break;
            }
        }

        $(function () {
            bindEmpIDCtrl(10, '#<%=txtWorkDayNo.ClientID%>', '#<%=txtScanCardNO.ClientID%>');
        })
    </script>
</head>
<body  >
    <form id="form1" runat="server">
        <asp:Timer ID="ReadCardTimer" runat="server" Interval="500000" OnTick="ReadCardTimer_Tick"></asp:Timer>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <%--        <div class="wrapper">
            <div class="content">
                <div class="searchbar">--%>
        <div style="position: fixed; float: none; top: 0px; margin: auto; height: 45px">
            <table style="margin-top: 10px; margin-bottom: 15px; margin-left: 25px">
                <tr>
                    <%--<th>身份证号码：</th>--%>
                    <td>                        
                       <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                             <asp:Label ID="Label1" runat="server" Text="工号："></asp:Label>
                             <asp:TextBox ID="txtWorkDayNo" runat="server" Width="100px"></asp:TextBox>
                             <asp:Label ID="Label2" runat="server" Text="身份证："></asp:Label>
                             <asp:TextBox ID="txtScanCardNO" runat="server" Width="180px" MaxLength="18"></asp:TextBox>
                         </ContentTemplate>
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="btnAssign" EventName="Click" />
                         </Triggers>
                     </asp:UpdatePanel>
                        
                    </td>
                    <td></td>
                    <td>
                        <asp:Button ID="btnAssign" runat="server" CssClass="publicBtn" Text="分配"  OnClick="btnAssign_Click" />
                        
                        </td>
                </tr>
            </table>
        </div>
        <%--                </div>
            </div>
        </div>--%>
        <br />
        <br />
        <div class="navigation_wrap">
            <h3 class="nav_left">批量分房-男
            </h3>
        </div>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar">
                    <table id="tblMaleContent">
                        <tr>
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea0" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding0" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType0" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType20" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType0" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU0" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck0" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="+" onclick="addTabItem('tblMaleContent')" />&nbsp;
                          &nbsp;
                                <asp:Button ID="btnOK" runat="server" Text="OK" OnClientClick="return OK(this)" OnClick="btnOK_Click" />
                            </td>
                        </tr>
                        <tr style="display: none" id="tr1">
                            <th>宿舍区：</th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea1" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding1" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType1" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType21" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType1" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU1" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck1" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="-" onclick="deleteTabItem(this, 'tblMaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr2">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea2" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding2" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType2" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType22" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType2" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU2" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck2" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="-" onclick="deleteTabItem(this, 'tblMaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr3">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea3" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding3" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType3" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType23" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType3" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU3" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck3" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="-" onclick="deleteTabItem(this, 'tblMaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr4">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea4" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding4" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType4" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType24" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType4" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU4" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck4" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="-" onclick="deleteTabItem(this, 'tblMaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr5">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea5" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding5" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType5" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType25" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU5" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck5" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="-" onclick="deleteTabItem(this, 'tblMaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr6">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea6" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding6" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType6" runat="server"></asp:DropDownList></td>
                            <%-- <th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType26" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU6" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck6" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="-" onclick="deleteTabItem(this, 'tblMaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr7">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea7" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding7" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType7" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType27" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU7" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck7" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="-" onclick="deleteTabItem(this, 'tblMaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr8">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea8" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding8" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType8" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType28" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU8" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck8" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="-" onclick="deleteTabItem(this, 'tblMaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr9">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlDormArea9" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlBuilding9" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType9" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomType29" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男" Selected="True">男</asp:ListItem>
                                    <asp:ListItem Value="女">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlBU9" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnCheck9" runat="server" Text="检查" />&nbsp;<input type="button" name="operate" value="-" onclick="deleteTabItem(this, 'tblMaleContent')" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%; text-align: center">
                    <tr style="background-color: aliceblue;">
                        <th>工号</th>
                        <th>姓名</th>
                        <th>身份证号码</th>
                        <th>事业部</th>
                        <th>宿舍区</th>
                        <th>楼栋</th>
                        <th>房间号</th>
                        <th>床位号</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtEmployeeNo" runat="server" Width="100%"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtCardNo" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtBU" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtDormArea" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtBuilding" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtRoom" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtBed" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCheck0" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck1" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck2" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck3" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck4" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck5" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck6" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck7" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck8" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck9" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck0" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck1" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck2" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck3" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck4" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck5" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck6" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck7" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck8" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck9" />
                <asp:AsyncPostBackTrigger ControlID="btnOK" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleOK" />
                <asp:AsyncPostBackTrigger ControlID="btnAssign" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="navigation_wrap">
            <h3 class="nav_left">批量分房-女
            </h3>
        </div>
        <div class="wrapper">
            <div class="content">
                <div class="searchbar">
                    <table id="tblFemaleContent">
                        <tr>
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea0" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding0" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType0" runat="server"></asp:DropDownList></td>
                            <%-- <th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType20" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType0" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU0" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck0" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="+" onclick="addTabItem('tblFemaleContent')" />

                                &nbsp;<asp:Button ID="btnFemaleOK" runat="server" Text="OK" OnClientClick="return OK(this)" OnClick="btnFemaleOK_Click" />
                            </td>
                        </tr>
                        <tr style="display: none" id="tr_1">
                            <th>宿舍区：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea1" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding1" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType1" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType21" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType1" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU1" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck1" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="-" onclick="deleteTabItem(this, 'tblFemaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr_2">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea2" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding2" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType2" runat="server"></asp:DropDownList></td>
                            <%-- <th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType22" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType2" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU2" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck2" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="-" onclick="deleteTabItem(this, 'tblFemaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr_3">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea3" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding3" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType3" runat="server"></asp:DropDownList></td>
                            <%-- <th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType23" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType3" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU3" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck3" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="-" onclick="deleteTabItem(this, 'tblFemaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr_4">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea4" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding4" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType4" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType24" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType4" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU4" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck4" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="-" onclick="deleteTabItem(this, 'tblFemaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr_5">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea5" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding5" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType5" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType25" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU5" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck5" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="-" onclick="deleteTabItem(this, 'tblFemaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr_6">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea6" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding6" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType6" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType26" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU6" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck6" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="-" onclick="deleteTabItem(this, 'tblFemaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr_7">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea7" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding7" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType7" runat="server"></asp:DropDownList></td>
                            <%--<th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType27" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU7" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck7" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="-" onclick="deleteTabItem(this, 'tblFemaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr_8">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea8" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding8" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType8" runat="server"></asp:DropDownList></td>
                            <%-- <th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType28" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU8" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck8" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="-" onclick="deleteTabItem(this, 'tblFemaleContent')" /></td>
                        </tr>
                        <tr style="display: none" id="tr_9">
                            <th>宿舍区：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleDormArea9" runat="server"></asp:DropDownList>
                            </td>
                            <th>楼栋：
                            </th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBuilding9" runat="server"></asp:DropDownList>
                            </td>
                            <th>房间类型：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType9" runat="server"></asp:DropDownList></td>
                            <%-- <th>房间类型2：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomType29" runat="server">
                                    <asp:ListItem Value="1" Selected="True">员工宿舍</asp:ListItem>
                                    <asp:ListItem Value="2">家庭房</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <%--                            <th>性别分类：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleRoomSexType5" runat="server" Enabled="false" Width="45px">
                                    <asp:ListItem Value="男">男</asp:ListItem>
                                    <asp:ListItem Value="女" Selected="True">女</asp:ListItem>
                                </asp:DropDownList></td>--%>
                            <th>事业部：</th>
                            <td>
                                <asp:DropDownList ID="ddlFemaleBU9" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnFemaleCheck9" runat="server" Text="检查" />&nbsp;<input type="button" name="femaleOperate" value="-" onclick="deleteTabItem(this, 'tblFemaleContent')" /></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <table style="width: 100%; text-align: center">
                    <tr style="background-color: aliceblue;">
                        <th>工号</th>
                        <th>姓名</th>
                        <th>身份证号码</th>
                        <th>事业部</th>
                        <th>宿舍区</th>
                        <th>楼栋</th>
                        <th>房间号</th>
                        <th>床位号</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFemaleEmployeeNo" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtFemaleName" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtFemaleCardNo" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtFemaleBU" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtFemaleDormArea" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtFemaleBuilding" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtFemaleRoom" runat="server" Width="100%"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtFemaleBed" runat="server" Width="100%"></asp:TextBox></td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnCheck0" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck1" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck2" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck3" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck4" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck5" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck6" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck7" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck8" />
                <asp:AsyncPostBackTrigger ControlID="btnCheck9" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck0" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck1" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck2" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck3" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck4" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck5" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck6" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck7" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck8" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleCheck9" />
                <asp:AsyncPostBackTrigger ControlID="btnOK" />
                <asp:AsyncPostBackTrigger ControlID="btnFemaleOK" />
                <asp:AsyncPostBackTrigger ControlID="btnAssign" />
                <asp:AsyncPostBackTrigger ControlID="ReadCardTimer" />
            </Triggers>
        </asp:UpdatePanel>

    </form>
</body>
</html>

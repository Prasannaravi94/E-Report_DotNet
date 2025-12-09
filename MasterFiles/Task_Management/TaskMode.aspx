<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskMode.aspx.cs" Inherits="Task_Management_TaskMode" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mode of Task</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <%--<link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />--%>
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <style type="text/css">
        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
            width: 190px;
            height: 21px;
            font: bold;
            background-image: url('Images/arrow_sort_d.gif');
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow:;
        }

        #effect {
            width: 180px;
            height: 160px;
            padding: 0.4em;
            position: relative;
            overflow: auto;
        }

        .textbox {
            width: 185px;
            height: 14px;
        }

        body {
            font-size: 62.5%;
            background: none !important;
            background-color: #fafdff !important;
        }

        td.stylespc {
            padding-bottom: 20px;
            padding-right: 10px;
        }

        .style1 {
            width: 195px;
        }

        .style2 {
            width: 232px;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .textbox {
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            color: #90a1ac;
            font-size: 14px;
            padding-left: 10px;
            height: 35px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function fncheck() {
            var colname = document.getElementById("<%=txtTaskShortName.ClientID%>").value.trim();
            if (colname.length <= 0) {
                alert('Short Name should not be empty...');
                document.getElementById("<%=txtTaskShortName.ClientID%>").focus();
                return false;
            }

            var colname1 = document.getElementById("<%=txtTaskName.ClientID%>").value.trim();
            if (colname1.length <= 0) {
                alert('Task Name should not be empty...');
                document.getElementById("<%=txtTaskName.ClientID%>").focus();
                return false;
            }
        }

        function clearall() {
            document.getElementById("<%=txtTaskShortName.ClientID%>").value = "";
            document.getElementById("<%=txtTaskName.ClientID%>").value = "";
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <center>
                            <table>
                                <tr>
                                    <td align="center">
                                        <h2 class="text-center">Task Management - Mode Creation</h2>
                                    </td>
                                </tr>
                            </table>
                        </center>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblTaskShortName" runat="server" Text="Short Name &nbsp;" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtTaskShortName" runat="server" CssClass="textbox"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblTaskName" runat="server" Text="Task Name &nbsp;" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtTaskName" runat="server" CssClass="textbox"></asp:TextBox>
                            </div>
                        </div>
                        <asp:HiddenField ID="hidTaskID" runat="server" />
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" OnClick="btnSubmit_Click" OnClientClick="return fncheck()" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="savebutton" OnClientClick="return clearall()" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-6">
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                    <asp:GridView ID="grdTask" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" BorderWidth="0" Width="100%"
                                        OnRowCommand="grdTask_RowCommand" OnRowUpdating="grdTask_RowUpdating"
                                        OnRowEditing="grdTask_RowEditing" OnRowCancelingEdit="grdTask_RowCancelingEdit">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task_Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTask_Id_Edit" runat="server" Text='<%#Eval("Mode_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Short Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTask_ShortName_Edit" runat="server" Text='<%# Bind("Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Task Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTask_Name_Edit" runat="server" Text='<%# Bind("Mode_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Mode_ID") %>' CommandName="Edit">Edit
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

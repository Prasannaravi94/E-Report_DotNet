<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FVExpense_Parameter.aspx.cs"
    Inherits="MasterFiles_FVExpense_Parameter" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fixed/Variable Expense</title>
    <%-- <link type="text/css" rel="Stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script language="javascript" type="text/javascript">


        function cbmchng(e) {

            var Id = e.id;

            var select = e.id;
            //alert(this.value);

            var Data = [];

            Data = select.split('_');



            var txtLimitID = Data[0] + "_" + Data[1] + "_txtLimit";
            //alert(txtLimitID);
            var txtLimit1ID = Data[0] + "_" + Data[1] + "_txtLimit1";

            var tt = $("#" + Id).val();

            if (tt == 'L') {

                $("#" + txtLimitID).css('display', 'block');
                $("#" + txtLimit1ID).css('display', 'block');
                //               
            }
            else {



                $("#" + txtLimitID).css('display', 'none');
                $("#" + txtLimit1ID).css('display', 'none');
                $("#" + txtLimitID).val("");
                $("#" + txtLimit1ID).val("");


            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">

                    <div class="col-lg-12">
                        <h2 class="text-center">Fixed/Variable Expense</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="row  justify-content-center clearfix">
                                <div class="col-lg-11">
                                    <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="60px"
                                        Text="Add" OnClick="btnNew_Click" />

                                </div>
                            </div>
                            <div class="display-name-heading text-center clearfix">
                                <div class="d-inline-block division-name">
                                    <asp:Label ID="lblLevel" runat="server" Text="Level"></asp:Label>
                                    <%--  <asp:Label ID="lblDesignation" runat="server" Text=" Designation - Level :  "></asp:Label>--%>
                                </div>
                                <div class="d-inline-block align-middle">
                                    <div class="single-des-option">
                                        <asp:DropDownList ID="ddlLevel" runat="server" AutoPostBack="true" CssClass="nice-select"
                                            OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Base/MGR Level" Value="1"></asp:ListItem>
                                            <%--<asp:ListItem Text="Line Managers" Value="2"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />

                            <div class="row justify-content-center">
                                <asp:Label ID="lblSelect" Text="Select the Designation" ForeColor="Red" Font-Size="Large"
                                    runat="server">
                                </asp:Label>
                            </div>
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <center>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <table align="center" style="width: 100%">
                                                    <tr>
                                                        <td align="center">
                                                            <asp:GridView ID="grdFVeExpParameter" runat="server" Width="100%" HorizontalAlign="Center"
                                                                OnRowUpdating="grdFVeExpParameter_RowUpdating" OnRowEditing="grdFVeExpParameter_RowEditing"
                                                                OnRowDeleting="grdFVeExpParameter_RowDeleting" EmptyDataText="No Records Found"
                                                                OnRowCreated="grdFVeExpParameter_RowCreated" OnRowDataBound="grdFVeExpParameter_RowDataBound"
                                                                OnRowCancelingEdit="grdFVeExpParameter_RowCancelingEdit" AutoGenerateColumns="false"
                                                                GridLines="None" CssClass="table">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="10px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Parameter Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExpParameter_Code" runat="server" Text='<%#Eval("Expense_Parameter_Code")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Parameter Name" ItemStyle-HorizontalAlign="Left"
                                                                        HeaderStyle-Width="140px">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtExpParameter_Name" runat="server" CssClass="input" Height="38px" MaxLength="100"
                                                                                Text='<%# Bind("Expense_Parameter_Name") %>'></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtExpParameter_Name" Display="Dynamic"
                                                                                ErrorMessage="Enter Parameter Name."></asp:RequiredFieldValidator>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExpParameter_Name" runat="server" Text='<%# Bind("Expense_Parameter_Name") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Expense Type"
                                                                        HeaderStyle-Width="90px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExpenseType" runat="server" Text='<%# Bind("Param_type") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="nice-select" onchange="cbmchng(this)">
                                                                                <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                                                                <asp:ListItem Value="F" Text="Fixed"></asp:ListItem>
                                                                                <asp:ListItem Value="V" Text="Variable"></asp:ListItem>
                                                                                <asp:ListItem Text="Variable with Limit" Value="L"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Base Level Limit Amount"
                                                                        HeaderStyle-Width="90px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExpenseType1" runat="server" Text='<%# Bind("Fixed_Amount") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtLimit" Text='<%# Bind("Fixed_Amount") %>' runat="server" CssClass="input" Height="38px"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="MGR Limit Amount"
                                                                        HeaderStyle-Width="90px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblExpenseType2" runat="server" Text='<%# Bind("Fixed_Amount1") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txtLimit1" Text='<%# Bind("Fixed_Amount1") %>' runat="server" CssClass="input" Height="38px"></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ShowHeader="True" EditText="Inline Edit"
                                                                        HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"
                                                                        HeaderStyle-Width="80px"></asp:CommandField>
                                                                    <%-- <asp:HyperLinkField HeaderText="Edit" Text="Edit" HeaderStyle-ForeColor="white" DataNavigateUrlFormatString="DesignationCreation.aspx?Designation_Code={0}"
                                DataNavigateUrlFields="Designation_Code">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                        </asp:HyperLinkField>--%>
                                                                    <asp:TemplateField HeaderText="Delete" Visible="false" HeaderStyle-Width="50px">
                                                                      
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Expense_Parameter_Code") %>'
                                                                                CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Designation');">Delete
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </center>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>

            </div>
            <br />
            <br />

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>

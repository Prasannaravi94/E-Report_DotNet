<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Leave_Setup.aspx.cs" Inherits="MasterFiles_Options_Leave_Setup" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Setup</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {

            $("#tblCustomers [id*=chkleave]").click(function () {
                if ($(this).is(":checked")) {
                    $("#tblCustomers [id*=chkdefault]").removeAttr("checked");
                }
            });
            $("#tblCustomers [id*=chkdefault]").click(function () {

                if ($(this).is(":checked")) {
                    $("#tblCustomers [id*=chkleave]").removeAttr("checked");
                }
            });
        });
    </script>
      <script type="text/javascript">
        function isAlpha(event) {
            var keyCode = event.keyCode ? event.keyCode : event.which;
            var keyChar = String.fromCharCode(keyCode);

            // Allow only alphabets (a-z, A-Z) and optionally space
            var regex = /^[a-zA-Z\s]*$/;

            if (!regex.test(keyChar) && keyCode != 8 && keyCode != 32) {  // Allow Backspace (8) and Space (32)
                event.preventDefault();
            }
        }
    </script>
    <script type="text/javascript">
        function validatePastedText(event, textbox) {
            setTimeout(function () {
                // Get the pasted value
                var pastedValue = textbox.value;

                // Remove numbers and special characters (keep only alphabets and spaces)
                var cleanedValue = pastedValue.replace(/[^a-zA-Z\s]/g, '');

                // Update the textbox with the cleaned value
                textbox.value = cleanedValue;
            }, 1);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <br />
                        <h2 class="text-center" style="border-bottom: none !important;" id="hHeading" runat="server"></h2>

                        <div class="card border-primary">
                            <div class="card-header">
                                <h6 class="card-title">Setup</h6>
                            </div>
                            <div class="card-body">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lbllve" runat="server" CssClass="label" Visible="false">As Per Leave Entitlement</asp:Label>
                                        <asp:RadioButtonList ID="rdomandt" runat="server" RepeatDirection="Horizontal" Visible="false" CssClass="pull-right" Font-Bold="true">
                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="head" runat="server" CssClass="label">Leave Type Requirement:</asp:Label>
                                    </div>
                                </div>
                                <div class="designation-reactivation-table-area clearfix">
                                    <div class="display-table clearfix">
                                        <div class="table-responsive">
                                            <table id="tblSecSale" cellpadding="5" cellspacing="5" style="width: 100%" class="table">
                                                <tr class="rpttr">
                                                    <th id="tdSNo" runat="server">#
                                                    </th>
                                                    <th id="tdPName" runat="server">Type
                                                    </th>

                                                    <asp:Repeater ID="rptleavetypeHeader" runat="server">
                                                        <ItemTemplate>
                                                            <th id="tdMainHdrSecVal" runat="server" align="center">
                                                                <asp:Literal ID="litSfName" Text='<%#Eval("Leave_SName") %>' runat="server"></asp:Literal>
                                                                <br />
                                                            </th>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tr>
                                                <tbody>
                                                    <tr>
                                                        <asp:Repeater ID="rpttype" runat="server">
                                                            <ItemTemplate>
                                                                <tr class="rpttr">

                                                                    <td id="tdlcode" runat="server" align="left">
                                                                        <%#Container.ItemIndex+1 %>
                                                                        <div>
                                                                            <%--     <asp:Literal ID="litsno" runat="server" Text='<%#Eval("slno") %>'></asp:Literal>--%>

                                                                            <asp:HiddenField ID="hidleave_Code" runat="server" Value='<%#Eval("value") %>' />
                                                                            <%-- <asp:HiddenField ID="hidPDesc" runat="server" Value='<%#Eval("Product_Description") %>' />--%>
                                                                        </div>
                                                                    </td>

                                                                    <td id="tdlname" runat="server" align="left">
                                                                        <div>
                                                                            <%--     <asp:Literal ID="litsno" runat="server" Text='<%#Eval("slno") %>'></asp:Literal>--%>
                                                                            <asp:Label ID="litcatname" Text='<%#Eval("category") %>' runat="server"></asp:Label>
                                                                            <%-- <asp:HiddenField ID="hidPDesc" runat="server" Value='<%#Eval("Product_Description") %>' />--%>
                                                                        </div>
                                                                        <%-- <div >
                                            <asp:Label ID="litpname" Text='<%#Eval("category") %>' runat="server"></asp:Label>
                                        </div>--%>
                                                                    </td>
                                                                    <td id="tdlshnme" runat="server" align="left" visible="false">
                                                                        <%#Container.ItemIndex+1 %>
                                                                        <div>
                                                                            <%--     <asp:Literal ID="litsno" runat="server" Text='<%#Eval("slno") %>'></asp:Literal>--%>
                                                                            <asp:HiddenField ID="hdnshtnme" runat="server" Value='<%#Eval("shtnme") %>' />
                                                                            <%-- <asp:HiddenField ID="hidPDesc" runat="server" Value='<%#Eval("Product_Description") %>' />--%>
                                                                        </div>
                                                                    </td>

                                                                    <asp:Repeater ID="rptDetSecSale" runat="server">
                                                                        <ItemTemplate>
                                                                            <td id="tdSecQty" runat="server" align="center"
                                                                                valign="middle" class="DetSecSale">
                                                                                <asp:CheckBox ID="chkleave" runat="server" Text="."></asp:CheckBox>

                                                                                <asp:HiddenField ID="hidlvecode" runat="server" EnableViewState="true" Value='<%#Eval("Leave_code") %>' />
                                                                                <asp:HiddenField ID="hidlvename" Value='<%#Eval("Leave_Name") %>' EnableViewState="true" runat="server" />
                                                                            </td>
                                                                        </ItemTemplate>
                                                                    </asp:Repeater>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:HiddenField ID="hidprdcnt" runat="server" />
                                                        <asp:HiddenField ID="hidSfCnt" runat="server" />
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <br />
                                        <asp:CheckBox ID="chkdefault" runat="server" Text="Default"></asp:CheckBox>
                                    </div>
                                    <div class="w-100 designation-submit-button text-center clearfix">
                                        <br />
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Save" OnClick="btnSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="card border-primary">
                            <div class="card-header">
                                <h6 class="card-title">Type</h6>
                            </div>
                            <div class="card-body">
                                <div class="designation-reactivation-table-area clearfix">
                                    <div class="display-table clearfix">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdleave" runat="server" Width="100%" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" GridLines="None" ShowFooter="True" OnRowCreated="grdleave_RowCreated" OnSelectedIndexChanging="grdleave_SelectedIndexChanging"
                                                CssClass="table" PagerStyle-CssClass="gridview1" OnRowCommand="grdleave_RowCommand">
                                                <%--<HeaderStyle Font-Bold="false" />
                        <PagerStyle CssClass ="pgr" />
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt" />--%>

                                                <Columns>

                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdleave.PageIndex * grdleave.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave_code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcode" runat="server" Text='<%#Eval("Leave_code")%>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtlve_Sname" onkeypress="return isAlpha(event);" onpaste="validatePastedText(event, this)" MaxLength="3" runat="server" CssClass="input" Text='<%# Bind("Leave_SName") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_SName" runat="server" CssClass="input" onkeypress="return isAlpha(event);" onpaste="validatePastedText(event, this)" MaxLength="3" align="left"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtlve_name" onkeypress="return isAlpha(event);" onpaste="validatePastedText(event, this)" MaxLength="25" runat="server" CssClass="input" Text='<%# Bind("Leave_Name") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_Name" runat="server" CssClass="input" onkeypress="return isAlpha(event);" onpaste="validatePastedText(event, this)" MaxLength="25" align="left"></asp:TextBox>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="More">
                                                        <FooterTemplate>
                                                            <asp:Button ID="btnadd" runat="server" CssClass="savebutton" CausesValidation="true" CommandName="Select" Text="Add" OnClientClick="" />
                                                            <%--<asp:LinkButton ID="LkB1" runat="server" CommandName="Select">Add Folder</asp:LinkButton>--%>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Deactivate/Reactivate" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%--<asp:Button ID="btnDelete" runat ="server" CssClass="savebutton" Width="50px" Height="20px" CommandArgument='<%# Eval("Move_MailFolder_Id") %>' CommandName ="Delete" Text="Delete" OnClientClick="return confirm('Do you want to Delete the Folder Name');" />--%>
                                                           
                                                              <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Leave_code") %>' CommandName="ToggleStatus"
                                                                OnClientClick='<%# GetClientClickConfirmation(Eval("Active_Flag")) %>'>
                                                             <%# Eval("Active_Flag") != null && Eval("Active_Flag").ToString() == "0" ?  "Deactivate" : "<span style=\"color: red;\">Reactivate</span>" %>
                                                            </asp:LinkButton>
                                                            
                                                            <%-- <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Leave_code") %>'
                                                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Leave');">Deactivate
                                                            </asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>

                                                
                                                 <EmptyDataTemplate>
                                                    <tr>
                                                        <th scope="col">S.No</th>
                                                        <th scope="col">Short Name</th>
                                                        <th scope="col">Name</th>
                                                        <th>More</th>
                                                        <th>Deactivate</th>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:TextBox ID="txt_SName" runat="server" CssClass="input" MaxLength="3" onkeypress="return isAlpha(event);"  onpaste="validatePastedText(event, this)"   align="left"></asp:TextBox>

                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txt_Name" runat="server" CssClass="input" onkeypress="return isAlpha(event);" onpaste="validatePastedText(event, this)"  MaxLength="25"  align="left"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnadd" runat="server" CssClass="savebutton" CausesValidation="true" Width="60px" Height="25px" CommandName="Select" Text="Add" OnClientClick="" />
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </EmptyDataTemplate>
                                                <%--<EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="w-100 designation-submit-button text-center clearfix">
                                        <br />
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="savebutton" Text="Save" OnClick="btnUpdate_Click" />
                                    </div>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomizedColumn.aspx.cs"
    Inherits="SecondarySales_CustomizedColumn" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Secondary Sales Setup - Customized Column based on formula</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <link href="../JScript/Secondary_CSS.css" rel="stylesheet" type="text/css" />
    <link href="../JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/BootStrap/dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="../JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function fncheck() {

            var colname = document.getElementById("<%=txtColName.ClientID%>").value.trim();
            if (colname.length <= 0) {
                createCustomAlert('Column Name should not be empty...');
                document.getElementById("<%=txtColName.ClientID%>").focus();
                return false;
            }

            var RB1 = document.getElementById("<%=rdoDisable.ClientID%>");
            var radio = RB1.getElementsByTagName("input");
            var isChecked = false;
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    isChecked = true;
                    break;
                }
            }

            if (!isChecked) {
                createCustomAlert("Disable Mode should not be empty");
                return false;
            }

            var par = document.getElementById("<%=ddlParam.ClientID%>").value.trim();
            if (par == "0") {
                createCustomAlert('Select Calculation Param...');
                document.getElementById("<%=ddlParam.ClientID%>").focus();
                return false;
            }

            //            var opr = document.getElementById("<%=ddlOpr.ClientID%>").value.trim();
            //            var lit_formula = document.getElementById("<%=litFormula.ClientID%>");
            //            if (opr == "0") {
            //                if (lit_formula.innerText.trim() == "-----") {
            //                    alert('Select Operator...');
            //                    document.getElementById("<%=ddlOpr.ClientID%>").focus();
            //                    return false;
            //                }
            //            }
        }

        function calc_formula(ddl, lit) {
            var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
            var selectedValue = ddl.value;

            if (selectedText.trim().length > 0) {
                var lit_formula = document.getElementById("<%=litFormula.ClientID%>");
                var hidFormula = document.getElementById("<%=hidFormula.ClientID%>");
                var hidCalc = document.getElementById("<%=hidCalc.ClientID%>");

                if (lit_formula.innerText.trim() == "-----") {
                    lit_formula.innerText = "";
                    lit_formula.innerText = lit_formula.innerText + " " + selectedText.trim();
                    hidFormula.value = hidFormula.value + " " + selectedValue.trim();
                    hidCalc.value = hidCalc.value + " " + selectedText.trim();
                }
                else {
                    var strCalc = lit_formula.innerText;
                    strCalc = strCalc.trim();
                    strCalc = strCalc.slice(-1);
                    if ((strCalc == "+") || (strCalc == "-")) {
                        lit_formula.innerText = lit_formula.innerText + " " + selectedText.trim();
                        hidFormula.value = hidFormula.value + " " + selectedValue.trim();
                        hidCalc.value = hidCalc.value + " " + selectedText.trim();
                    }
                }

            }
        }

        function calc_opr_formula(ddl, lit) {
            var selectedText = ddl.options[ddl.selectedIndex].innerHTML;
            var selectedValue = ddl.value;

            //  alert(selectedValue);

            if (selectedText.trim().length > 0) {
                var lit_formula = document.getElementById("<%=litFormula.ClientID%>");
                var hidFormula = document.getElementById("<%=hidFormula.ClientID%>");
                var hidCalc = document.getElementById("<%=hidCalc.ClientID%>");

                var strCalc = lit_formula.innerText;
                strCalc = strCalc.trim();
                strCalc = strCalc.slice(-1);
                if (!((strCalc == "+") || (strCalc == "-"))) {
                    lit_formula.innerText = lit_formula.innerText + " " + selectedText.trim();
                    hidCalc.value = hidCalc.value + " " + selectedText.trim();
                    hidFormula.value = hidFormula.value + " " + selectedValue.trim();
                }

                //   ddl.value = 0;
            }
        }

        function clearall() {
            document.getElementById("<%=txtColName.ClientID%>").value = "";
            //document.getElementById("<%=rdoDisable.ClientID%>");

            document.getElementById("<%=ddlParam.ClientID%>").value = "0";
            document.getElementById("<%=ddlOpr.ClientID%>").value = "0";
            document.getElementById("<%=litFormula.ClientID%>").innerText = " ----- ";
            document.getElementById("<%=btnSubmit.ClientID%>").value = "Submit";
            return false;
        }

        function clearFormula() {
            document.getElementById("<%=litFormula.ClientID%>").innerText = " ----- ";
            document.getElementById("<%=hidFormula.ClientID%>").value = "";
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ucl:Menu ID="menu1" runat="server" />
        <center>
            <br />
            <table align="center" border="1">
                <tbody>
                    <tr>
                        <%--  <td>
                        <asp:Label ID="lblColName" runat="server" Text="User Defined Column Name &nbsp;"  SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtColName" runat="server" Width="200" SkinID="MandTxtBox"></asp:TextBox>
                    </td>   --%>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblColName" runat="server" SkinID="lblMand" Height="18px" Width="200px">
                            <span style="color:Red">*</span>User Defined Column Name</asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:TextBox ID="txtColName" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'"
                                onblur="this.style.backgroundColor='White'" TabIndex="2" runat="server" Width="200px"
                                MaxLength="120" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <%-- <td>
                        <asp:Label ID="lblDisable" runat="server" Text="Disable Mode " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList ID="rdoDisable" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                            <asp:ListItem Value="N" Text="No"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>--%>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblDisable" runat="server" SkinID="lblMand" Height="18px" Width="200px">
                            <span style="color:Red">*</span>Disable Mode</asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:RadioButtonList ID="rdoDisable" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="Y" Text="Yes"></asp:ListItem>
                                <asp:ListItem Value="N" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <%--<tr>
                    <td>
                        <asp:Label ID="lblOrder" runat="server" Text="Order By " SkinID="lblMand"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOrderBy" runat="server" Width="30" SkinID="MandTxtBox" MaxLength="3"></asp:TextBox>
                    </td>
                </tr>--%>
                    <tr style="height: 30px;">
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblCalcParam" runat="server" SkinID="lblMand" Height="19px" Width="150px">
                            <span style="color:Red">*</span>Calculation Param</asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlParam" runat="server" SkinID="ddlRequired" onchange="calc_formula(this);">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlOpr" runat="server" SkinID="ddlRequired" onchange="calc_opr_formula(this);">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="+" Text="+"></asp:ListItem>
                                <asp:ListItem Value="-" Text="-"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <%-- <td>
                            <asp:Label ID="lblCalcParam" runat="server" Text="Calculation Param " SkinID="lblMand"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlParam" runat="server" SkinID="ddlRequired" onchange="calc_formula(this);">
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <asp:DropDownList ID="ddlOpr" runat="server" SkinID="ddlRequired" onchange="calc_opr_formula(this);">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="+" Text="+"></asp:ListItem>
                                <asp:ListItem Value="-" Text="-"></asp:ListItem>
                            </asp:DropDownList>
                        </td>--%>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <%-- <asp:Label ID="lblFormula" runat="server" Text="Derived Formula " SkinID="lblMand"></asp:Label>--%>
                            <asp:Label ID="lblFormula" runat="server" SkinID="lblMand" Height="19px" Width="150px">
                            <span style="color:Red">&nbsp;&nbsp;</span>Derived Formula</asp:Label>
                        </td>
                        <td colspan="3" align="left" class="stylespc">
                            <asp:Label ID="litFormula" runat="server" Text=" ----- " BackColor="#00FFCC" ForeColor="#CC3300"
                                Font-Bold="True" Font-Size="Medium" Font-Names="Verdana"></asp:Label>
                            <asp:HiddenField ID="hidFormula" runat="server" />
                            <asp:HiddenField ID="hidCalc" runat="server" />
                            <asp:HiddenField ID="hid_Col_SNo" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblClosing_Calc" runat="server" SkinID="lblMand" Height="18px" Width="200px">
                            <span style="color:Red">*</span>Calculation Needed</asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:RadioButtonList ID="rblCalculation" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Value="D(+)" Text="Addition Based Calculation"></asp:ListItem>
                                <asp:ListItem Value="D(-)" Text="Subtraction Based Calculation"></asp:ListItem>
                                <asp:ListItem Value="N" Text="None"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="70px" Height="25px"
                CssClass="savebutton" OnClick="btnSubmit_Click" OnClientClick="return fncheck()" />
            &nbsp;&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="Reset" Width="70px" Height="25px"
                CssClass="savebutton" OnClick="btnReset_Click" OnClientClick="return clearall()" />
            &nbsp;&nbsp;
            <asp:Button ID="btnClear" runat="server" Text="Clear Formula" Width="120px" Height="25px"
                CssClass="savebutton" OnClick="btnClear_Click" OnClientClick="return clearFormula()" />
            <br />
            <br />
            <asp:GridView ID="grdSecSales" runat="server" Width="70%" HorizontalAlign="Center"
                GridLines="None" OnRowDataBound="grdSecSales_RowDataBound" OnRowCommand="grdSecSales_RowCommand"
                OnRowDeleting="grdSecSales_RowDeleting" AutoGenerateColumns="false" CssClass="mGridImg_1"
                AlternatingRowStyle-CssClass="alt" OnRowUpdating="grdSecSales_RowUpdating" OnRowEditing="grdSecSales_RowEditing"
                OnRowCancelingEdit="grdSecSales_RowCancelingEdit">
                <HeaderStyle Font-Bold="False" />
                <SelectedRowStyle BackColor="#1a87b9" />
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-Width="20">
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col SNo" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblColSNo_Edit" runat="server" Text='<%#Eval("Col_SNo")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Col Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:Label ID="lblColName_Edit" runat="server" Text='<%# Bind("Col_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Disable Mode" ItemStyle-Width="60">
                        <ItemTemplate>
                            <asp:Label ID="lblDisable_Edit" runat="server" Text='<%# Bind("Dis_Mode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order By" ItemStyle-Width="50" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblOrder_Edit" runat="server" Text='<%# Bind("Order_By") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Derived Formula" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="lblFormula_Edit" runat="server" Text='<%# Bind("Der_Formula") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Calculation Mode" ItemStyle-Width="200">
                        <ItemTemplate>
                            <asp:Label ID="lblCalculMode_Edit" runat="server" Text='<%# Bind("CalculationMode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="20">
                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                        </ControlStyle>
                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Col_SNo") %>'
                                CommandName="Edit">Edit
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="20">
                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                        </ControlStyle>
                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Col_SNo") %>'
                                CommandName="Delete" OnClientClick="return confirm('Do you want to delete?');">Delete
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </center>
    </div>
    </form>
</body>
</html>

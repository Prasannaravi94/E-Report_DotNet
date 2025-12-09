<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WrkTypeWise_Allowance.aspx.cs"
    Inherits="MasterFiles_WrkTypeWise_Allowance" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Work Type Wise Allowance - Fare Fixation</title>
    <%--<link type="text/css" rel="Stylesheet" href="../../css/style.css" />--%>

</head>
<body>
    <script language="javascript">
        function ShowHideTextBox(ddlId) {
            var ddl = document.getElementById(ddlId.id);

            if (ddl.value == "FA")  //your condition
            {
                document.getElementById('txtfixed_amt').style.display = 'none';
                document.getElementById('txtfixed_amt1').style.display = 'none';
                document.getElementById('txtfixed_amt2').style.display = 'none';
            }
            else {
                document.getElementById('txtfixed_amt').style.display = '';
                document.getElementById('txtfixed_amt1').style.display = '';
                document.getElementById('txtfixed_amt2').style.display = '';

            }
        }
    </script>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
          
            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <h2 class="text-center">Work Type Wise Allowance - Fare Fixation</h2>

                        <div class="row justify-content-center">
                            <div class="col-lg-4">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblDesignation"  runat="server" Text=" Designation - Level : " CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlDesignation" runat="server" AutoPostBack="true" CssClass="nice-select"
                                            DataTextField="name" DataValueField="code" OnSelectedIndexChanged="ddlDesignation_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <center>
                            <asp:Label ID="lblSelect" Text="Select the Designation" ForeColor="Red" Font-Size="Large"
                                runat="server"></asp:Label>
                        </center>
                        <div class="display-table3rowspan clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                <asp:GridView ID="grdWTAllowance" Width="100%" runat="server" AutoGenerateColumns="false"
                                    CssClass="table" AlternatingRowStyle-CssClass="alt" GridLines="None" ShowHeader="false"
                                    OnRowDataBound="grdWTAllowance_RowDataBound" OnRowCreated="grvMergeHeader_RowCreated">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="7%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Work Type" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ForeColor="red" Font-Size="9pt" Style="white-space: pre" ToolTip="Work Types Available in DCR Entry" ID="lblWorktype_Name" runat="server" Text='<%# Eval("Worktype_Name_B")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Allowance and Fare Type" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="40%">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="Territory_Type" ToolTip="Select the Method of Allowance and Fare type" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged" Width="200px" runat="server" SkinID="ddlRequired" Visible="false">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="NA" Value="NA"></asp:ListItem>
                                                    <asp:ListItem Text="HQ" Value="HQ"></asp:ListItem>
                                                    <asp:ListItem Text="EX" Value="EX"></asp:ListItem>
                                                    <asp:ListItem Text="OS" Value="OS"></asp:ListItem>
                                                    <asp:ListItem Text="Allowance / Fare Auto" Value="AA-FA"></asp:ListItem>
                                                    <asp:ListItem Text="Allowance Selectable / Fare Enterable" Value="AS-FE"></asp:ListItem>
                                                    <asp:ListItem Text="Allowance Auto / No Fare" Value="AA-NF"></asp:ListItem>
                                                    <asp:ListItem Text="No Allowance / Fare Automatic" Value="NA-FA"></asp:ListItem>
                                                    <asp:ListItem Text="Allowance Selectable / Fare Auto" Value="AS-FA"></asp:ListItem>
                                                    <asp:ListItem Text="Allowance Enterable / Fare Auto" Value="AE-FA"></asp:ListItem>
                                                    <asp:ListItem Text="Allowance Auto / Fare Enterable" Value="AA-FE"></asp:ListItem>
                                                    <asp:ListItem Text="Allowance Enterable / Fare Enterable" Value="AE-FE"></asp:ListItem>
                                                    <asp:ListItem Text="Fixed Allowance" Value="FA"></asp:ListItem>
                                                    <asp:ListItem Text="Variable Allowance" Value="VA"></asp:ListItem>

                                                </asp:DropDownList>
                                                <asp:DropDownList ID="Territory_Type_FW" Width="200px" runat="server" SkinID="ddlRequired" Visible="false">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="EX - Fare Editable" Value="EX"></asp:ListItem>
                                                    <asp:ListItem Text="OS - Fare Editable" Value="OS"></asp:ListItem>
                                                    <asp:ListItem Text="EX/OS - Fare Editable" Value="EXOS"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(HQ)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(EX)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt1" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount1")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code1" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt2" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount2")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code2" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt6" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount6")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code6" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt7" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount7")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code7" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt8" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount8")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code8" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt9" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount9")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code9" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt10" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount10")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code10" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt11" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount11")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code11" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(HQ)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt3" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount3")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code3" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(EX)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt4" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount4")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code4" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt5" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount5")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code5" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt12" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount12")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code12" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt13" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount13")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code13" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt14" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount14")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code14" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt15" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount15")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code15" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt16" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount16")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code16" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fixed Amount(OS)" HeaderStyle-Width="90%" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtfixed_amt17" ToolTip="Enter the Fixed Amount" Width="50px" CssClass="input" Text='<%# Eval("fixed_amount17")%>' runat="server" Visible='<%#Eval("Expense_Type").ToString()=="Fixed Allowance"? true : false %>'></asp:TextBox>
                                                <asp:HiddenField ID="code17" runat="server" Value='<%# Bind("Worktype_Code_B") %>' />
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                    </Columns>

                                </asp:GridView>

                            </div>
                            <br />
                            <center>
                                <asp:Button ID="btnSave" Text="Save" CssClass="savebutton" runat="server"
                                    OnClick="btnSave_Click" />
                            </center>
                        </div>

                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
<script type="text/javascript">
    function terr_onchange($e) {
        alert("ssss");
        //alert($e);
        //InitializeVariables();
        if ($e.value == "OS") {
            //alert(lblError);
            var txt = document.getElementById("txtfixed_amt");
            alert(txt);
            //txt.visible = true;
            //alert("ttt111");
        }
    }

    function terr_onchange11(txt) {


        if (txt.value == null || isNaN(txt.value)) {
            lblError.style.visibility = "visible";
        }
    }
</script>
</html>

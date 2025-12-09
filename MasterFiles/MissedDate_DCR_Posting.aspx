<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MissedDate_DCR_Posting.aspx.cs" Inherits="MasterFiles_MissedDate_DCR_Posting" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Posting</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />



    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>

    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

        });
    </script>



    <script type="text/javascript" language="javascript">
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= grdMissedDCR.ClientID %>');
            var validator = document.getElementById('RequiredFieldValidator1');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;

                            if (confirm('Do you want to Save?')) {

                            }
                            else {
                                return false;
                            }
                            return true;

                            if (confirm('Do you want to Save?')) {
                                if (confirm('Are you sure?')) {
                                    ShowProgress();

                                    return true;

                                }
                                else {
                                    return false;
                                }
                            }
                            else {
                                return false;
                            }
                        }
                    }
                }
            }
            alert("Please Select at least one record.");

            return false;
        }

    </script>

    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {

                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <center>
                <table>

                    <tr style="height: 25px;">
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblYear" runat="server" Text="Applied Year" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblMonth" runat="server" Text="Applied Month" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                <asp:ListItem Value="12" Text="December"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height: 25px;padding-top:5px">
                        <td align="center" class="stylespc" colspan="2">

                            <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" />
                        </td>

                    </tr>
                </table>

            </center>
            <center>
                <br />
                <br />
                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="grdMissedDCR" runat="server" Width="85%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                    OnRowCreated="grdMissedDCR_RowCreated"
                                    GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Font-Bold="False" />
                                    <PagerStyle CssClass="pgr"></PagerStyle>
                                    <SelectedRowStyle BackColor="BurlyWood" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdMissedDCR.PageIndex * grdMissedDCR.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="White">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" Text="Cancel All" runat="server" onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkRelease" runat="server"  Text="."/>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Trans_SlNo" Visible="false">
                                            <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrans_SlNo" runat="server" Text='<%#   Bind("Trans_SlNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                            <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("sf_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-ForeColor="white">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Emp Id" HeaderStyle-ForeColor="white">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpid" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="white">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblhq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Desig" HeaderStyle-ForeColor="white">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesig" runat="server" Text='<%# Bind("Desig_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date" HeaderStyle-ForeColor="white">
                                            <ControlStyle Width="90%"></ControlStyle>
                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Activity_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </tbody>

                </table>

                <br />
                <center>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Width="90px" Height="25px" Text="Save" OnClientClick="return validateCheckBoxes()" CssClass="BUTTON"
                                    OnClick="btnSubmit_Click" />


                            </td>
                        </tr>
                    </table>
                </center>

            </center>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>

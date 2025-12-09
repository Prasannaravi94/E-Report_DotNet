<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SS_Edit.aspx.cs" Inherits="MasterFiles_MR_SSale_SS_Edit" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>SS Delete</title>
      <style type="text/css">
      td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
       <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
       <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
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

        $('#btnGo').click(function () {

            var Mode = $('#<%=ddlmode.ClientID%> :selected').text();
            if (Mode == "--Select--") { alert("Select Mode."); $('#ddlmode').focus(); return false; }
            var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
            if (Name == "--Select--") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
            var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
            if (Month == "--Select--") { alert("Select Year."); $('#ddlMonth').focus(); return false; }
            var Year = $('#<%=ddlYear.ClientID%> :selected').text();
            if (Year == "--Select--") { alert("Select Year."); $('#ddlYear').focus(); return false; }
        
            

        });
    }); 
    </script>
     <script type="text/javascript">
         $(function () {
             var $txt = $('input[id$=txtNew]');
             var $ddl = $('select[id$=ddlFieldForce]');
             var $items = $('select[id$=ddlFieldForce] option');

             $txt.keyup(function () {
                 searchDdl($txt.val());
             });

             function searchDdl(item) {
                 $ddl.empty();
                 var exp = new RegExp(item, "i");
                 var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                 if (arr.length > 0) {
                     countItemsFound(arr.length);
                     $.each(arr, function () {
                         $ddl.append(this);
                         $ddl.get(0).selectedIndex = 0;
                     }
                    );
                 }
                 else {
                     countItemsFound(arr.length);
                     $ddl.append("<option>No Items Found</option>");
                 }
             }

             function countItemsFound(num) {
                 $("#para").empty();
                 if ($txt.val().length) {
                     $("#para").html(num + " items found");
                 }

             }
         });
    </script>
</head>
<body>
    <form id="form1" runat="server">
      <div>
        <ucl:Menu ID="menu1" runat="server" />
       <center>
        <br />
            <table >
            <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" Text="Mode " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlmode" runat="server" SkinID="ddlRequired" AutoPostBack="true" 
                            onselectedindexchanged="ddlmode_SelectedIndexChanged" >
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="SS Entry"></asp:ListItem>
                      <%--  <asp:ListItem Value="2" Text="Bifur Entry"></asp:ListItem>
                         <asp:ListItem Value="3" Text="Base Entry"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblSF" runat="server" Text="Field Force " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                     <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" 
                            ToolTip="Enter Text Here"></asp:TextBox>
                        <asp:DropDownList ID="ddlFieldForce" runat="server" Width="250px" SkinID="ddlRequired" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFMonth" runat="server" Text="Month " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFYear" runat="server" Text="Year " SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>                   
                <tr>
                    <td>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnGo" runat="server" Text="Go" Width="30px" Height="25px" CssClass="BUTTON" 
                            onclick="btnGo_Click"/>&nbsp;&nbsp;
                               <asp:Button ID="btnClear" runat="server" Text="Clear" Width="60px" 
                            Height="25px" CssClass="BUTTON" onclick="btnClear_Click" 
                            />
                    </td>
                </tr>
            </table>
            <br />
            <asp:GridView ID="grdSecSales" runat="server" Width="40%" HorizontalAlign="Center" GridLines="None" 
                EmptyDataText="No Records Found"   
                AutoGenerateColumns="false" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt">
                <HeaderStyle Font-Bold="False" />
                <SelectedRowStyle BackColor="BurlyWood" />
                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="50">
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stockiest Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblStockiestCode" runat="server" Text='<%#Eval("Stockist_Code")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Stockiest Name" ItemStyle-HorizontalAlign="Left" >
                        <ItemTemplate>
                            <asp:Label ID="lblStockiestName" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSaleEntry" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"  />
            </asp:GridView>
        <br />
        <table>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="Update" CssClass="BUTTON" Visible="false"
                    OnClientClick="return confirm('Do you want to allow Sec Sales Edit for the selected stockiest(s)');" 
                    onclick="btnSubmit_Click"/>
            </td>
        </tr>
    </table>
    </center>    
    </div>

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mgr_SFC_Updation.aspx.cs"
    Inherits="MasterFiles_Mgr_SFC_Updation" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customized Allowance Type - Change</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
   <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=grdDist.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>

     <script type="text/javascript" language="javascript">
         $(function () {
             $('#btnExcel').click(function () {
                 var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                 location.href = url
                 return false
             })
         })
    </script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
            $('#btnSF').click(function () {
                var Prod = $('#<%=ddlSubdiv.ClientID%> :selected').text();
                if (Prod == "---Select---") { alert("Select Salesforce Name."); $('#ddlSubdiv').focus(); return false; }
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlSubdiv]');
            var $items = $('select[id$=ddlSubdiv] option');

            $txt.on('keyup', function () {
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
<script type="text/javascript" language="javascript">
         $(document).ready(function () {
             $("#testImg").hide();
             $('#linkcheck').click(function () {
                 window.setTimeout(function () {
                     $("#testImg").show();
                 }, 500);
             })
         });
               
</script> 
</head>
<body>
<script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <form id="form1" runat="server">
    <div>
    <div id="mainDiv" runat="server">
        <div id="Divid" runat="server"></div>
        <br />
        <center>
            <table>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblSubdiv" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
			<asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false" OnTextChanged="txtNew_TextChanged" AutoPostBack="true"
            ToolTip="Enter Text Here"></asp:TextBox>
<asp:LinkButton ID="linkcheck" runat="server" 
                            onclick="linkcheck_Click">
                          <img src="../../Images/Selective_Mgr.png" />
					</asp:LinkButton>
                        <asp:DropDownList ID="ddlSubdiv" SkinID="ddlRequired" runat="server"  Visible="false" AutoPostBack="true" onselectedindexchanged="ddlFieldForce_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
<td>                   
                          <div id="testImg">
                                            <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height:20px;" runat="server" /><span
                                                style="font-family: Verdana; color: Red; font-weight: bold; ">Loading Please Wait...</span>
                           </div>
                    </td>
                </tr>
                 <tr>
                <td align="left">
                    <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible ="false" ></asp:Label>
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible ="false" >
                     
                    </asp:DropDownList>
                       <asp:DropDownList ID="ddlSF1" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>
                </td>
            </tr>
            </table>
        </center>
        <br />
        <center>
            <asp:Button ID="btnSF" runat="server" Width="30px" Height="25px" Text="Go" Visible="false" CssClass="BUTTON"
                OnClick="btnSF_Click" />
        </center>
        <br />
       <center>
      <asp:Label ID="lblSelect" Text="Select the Fieldforce" ForeColor="Red" Font-Size="Large"
                            runat="server"></asp:Label>
                            </center>
                    <center>
      <asp:Label ID="lblmap" Text="Fix Your Allowance First" ForeColor="Red" Font-Size="Large" Visible="false"
                            runat="server"></asp:Label>
                            </center>
      </div>
      
      <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <table width="70%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdDist" runat="server" Width="90%" HorizontalAlign="Center" OnRowDataBound="grdWTAllowance_RowDataBound"
                                AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdDist.PageIndex * grdDist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="FieldForce Name">
                                        
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Name" style="white-space:pre" runat="server" Text='<%#   Bind("sf_name") %>'></asp:Label>
                                            <asp:HiddenField ID="hidsfcode" runat="server" value='<%# Bind("Sf_code") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="EmpCode">
                                        
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblempid" style="white-space:pre" runat="server" Text='<%#   Bind("sf_emp_id") %>'></asp:Label>
                                       </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="From">                                        
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" style="white-space:pre" runat="server" Text='<%#   Bind("Sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" style="white-space:pre" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            <asp:HiddenField ID="tocode" runat="server" value='<%# Bind("to_code") %>' />
                                            <asp:HiddenField ID="tocat" runat="server" Value='<%# Bind("Town_Cat") %>' />
                                          </ItemTemplate>
                                        </asp:TemplateField>

                                     <asp:TemplateField HeaderText="MR Category" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSFType" runat="server" Text='<%#Eval("Town_Cat")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MGR Category" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            
                                             <asp:DropDownList ID="Territory_Type" Width="100px" Visible="false" runat="server" SkinID="ddlRequired">
                                            <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                            <asp:ListItem Text="HQ" Value="HQ"></asp:ListItem>
                                            <asp:ListItem Text="EX" Value="EX"></asp:ListItem>
                                            <asp:ListItem Text="OS" Value="OS"></asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                  
                                    
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                <td></td>
                </tr>
                <tr>
                <td Height="13"></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSave" Text="Save" Width="70px" CssClass="BUTTON" runat="server" Visible="false" OnClick="btnSave_Click"/>
                    </td>
                </tr>
               
                </tbody>
            </table>
      </asp:Panel>
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>

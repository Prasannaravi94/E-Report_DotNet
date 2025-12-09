<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Distance_fixation_view_new_1.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Distance Fixation View</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=grdSalesForce.ClientID %>');
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
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
    <form id="form1" runat="server">
    <div>
        <div id="mainDiv" runat="server">
            <div id="Divid" runat="server">
            </div>
            <br />
            <center>
                <table>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblSubdiv" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
 			<asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
            ToolTip="Enter Text Here"></asp:TextBox>
            <asp:LinkButton ID="linkcheck" runat="server" 
                            onclick="linkcheck_Click">
                          <img src="../../Images/Selective_Mgr.png" />
                            </asp:LinkButton>
                            <asp:DropDownList ID="ddlSubdiv" SkinID="ddlRequired" visible="false" runat="server">
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
                </table>
            </center>
            <br />
            <center>
                <asp:Button ID="btnSF" runat="server" Width="30px" Height="25px" Text="Go" CssClass="BUTTON" Visible="false"
                    OnClick="btnSF_Click" />
            </center>
            <br />
            <table align="right" style="margin-right: 5%">
                <tr>
                    <td align="right">
                        <asp:Panel ID="pnlprint" runat="server" Visible="false">
                            <asp:Button ID="btnExcel" Style="width: 60px; height: 25px" runat="server" Text="Excel" />&nbsp
                            <input type="button" id="btnPrint" value="Print" style="width: 60px; height: 25px"
                                onclick="PrintGridData()" />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divHqId" runat="server" visible="false">
            <center>
                <u><font class="print" style="color: blue; cursor: pointer; font-weight: bold" size="2">
                    SFC View For The HQ : </font><font style="color: red; cursor: pointer; font-weight: bold"
                        size="3">-<span id="hqId" runat="server"></span></font></u></center>
        </div>
         <asp:Panel ID="pnlContents" runat="server" Width="100%">
        <table width="100%" align="center">
            <tr>
                <td align="center">
                    <asp:Label ID="lblHQ"  Visible="false" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblEX" Visible="false" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblEXOS" Visible="false" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                </td>
                <td align="center">
                    <asp:Label ID="lblFareKM" Visible="false" Font-Bold="true" Font-Names="Verdana" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
       
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center"
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
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Code">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblusrdfd_username" runat="server" Text='<%#   Bind("usrdfd_username") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce Name">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Name" runat="server" Text='<%#   Bind("sf_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Hq_Name" runat="server" Text='<%#   Bind("Sf_hq_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_designation_short_name" runat="server" Text='<%#   Bind("sf_designation_short_name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="From">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("from_code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="From Category" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblfromcat" runat="server" Text='<%#Eval("from_cat")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("to_code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="To Category" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblcattyp" runat="server" Text='<%#Eval("To_Cat")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Distance" ItemStyle-HorizontalAlign="Left">
                                        <ControlStyle Width="90%"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Distance_In_Kms") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" Height="5px" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table width="50%" align="center">
              
                    <tr>
                    <td align="center">
                        <asp:GridView ID="Gridwrktyp" runat="server" Width="30%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                            CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" Visible="false">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText=" Field Name">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsf" Width="100px" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Working Type">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblwrktyp" runat="server" Text='<%# Bind("type_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" Height="5px" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                    <td align="right">
                        <asp:GridView ID="gvExpense" runat="server" Width="30%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                            CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblusrdfd_username" Width="100px" runat="server" Text='<%#   Bind("Expense_Parameter_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left">
                                    </ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSF_Name" runat="server" Text='<%#   Bind("amount") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" Height="5px" BorderColor="Black" BorderStyle="Solid"
                                BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
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


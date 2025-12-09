<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Distance_fixation_view.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Distance Fixation View</title>
    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>

    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=grdSalesForce1.ClientID %>');
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
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
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
                <div class="container home-section-main-body position-relative clearfix">
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <br />
                            <h2 class="text-center" id="hHeading" runat="server"></h2>

                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblSubdiv" runat="server" SkinID="lblMand" Text="FieldForce Name"></asp:Label>
                                    <asp:DropDownList ID="ddlSubdiv" SkinID="ddlRequired" runat="server" CssClass="custom-select2">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                    </asp:DropDownList>
                                </div>

                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />
                                    <asp:Button ID="btnSF" runat="server" CssClass="savebutton" Text="Go" OnClick="btnSF_Click" />
                                </div>
                            </div>
                        </div>

                        <table width="100%">
                            <tr>
                                <td width="80%"></td>
                                <td align="right">
                                    <asp:Panel ID="pnlprint" runat="server" Visible="false">
                                        <table>
                                            <tr>
                                                <td style="padding-right: 30px">
                                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintGridData();">
                                                        <asp:Image ID="Image1" runat="server" Width="20px" Height="20px" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label2" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                                <td style="padding-right: 15px">
                                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png" ToolTip="Excel" Width="20px" Height="20px" />
                                                    </asp:LinkButton>
                                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>

                        <div class="col-lg-13">
                            <div class="designation-reactivation-table-area clearfix">
                                <p>
                                    <br />
                                </p>
                                <div id="divHqId" runat="server" visible="false">
                                    <center>
                                        <u><font class="print" style="color: blue; cursor: pointer; font-weight: bold" size="2">SFC View For The HQ : </font><font style="color: red; cursor: pointer; font-weight: bold"
                                            size="3">-<span id="hqId" runat="server"></span></font></u></center>
                                </div>
                                <asp:Panel ID="pnlContents" runat="server" Width="100%">
                                    <table width="100%" align="center">
                                        <tr>
                                            <td align="center">
                                                <asp:Label ID="lblHQ" Visible="false" Font-Bold="true" Font-Size="11px" Font-Names="Verdana" runat="server"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblEX" Visible="false" Font-Bold="true" Font-Size="11px" Font-Names="Verdana" runat="server"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblEXOS" Visible="false" Font-Bold="true" Font-Size="11px" Font-Names="Verdana" runat="server"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label ID="lblFareKM" Visible="false" Font-Bold="true" Font-Size="11px" Font-Names="Verdana" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div class="display-table clearfix">
                                        <div class="table-responsive">
                                            <center>
                                                <asp:Panel ID="Panel2" runat="server">
                                                    <div class="display-Approvaltable clearfix">
                                                        <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                                            <asp:GridView ID="grdSalesForce1" runat="server" AlternatingRowStyle-CssClass="alt" 
                                                                AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                                                GridLines="None" HorizontalAlign="Center" BorderWidth="0"
                                                                OnRowCreated="grdSalesForce1_RowCreated" OnRowDataBound="grdSalesForce1_RowDataBound"
                                                                ShowHeader="False" Width="100%">
                                                                <Columns>
                                                                </Columns>
                                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </center>

                                            <asp:GridView ID="grdSalesForce" runat="server" Width="95%" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt" Visible="false">
                                                <HeaderStyle Font-Bold="False" />
                                                <SelectedRowStyle BackColor="BurlyWood" />
                                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-Font-Size="10px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Employee Code" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblusrdfd_username" runat="server" Text='<%#Eval("usrdfd_username")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSF_Name" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HQ" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSF_Hq_Name" runat="server" Text='<%#Eval("Sf_hq_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSF_designation_short_name" runat="server" Text='<%#Eval("sf_designation_short_name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#Eval("from_code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From Category" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfromcat" runat="server" Text='<%#Eval("from_cat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsfName" runat="server" Text='<%#Eval("to_code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="To Category" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcattyp" runat="server" Text='<%#Eval("To_Cat")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Distance" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Distance_In_Kms")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Black" Height="5px" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </div>

                                        <br />
                                        <div>
                                            <table align="center">
                                                <tr align="center">
                                                    <td>
                                                        <asp:GridView ID="Gridwrktyp" runat="server" Width="30%" HorizontalAlign="Center" Visible="false"
                                                            AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                            GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt" >
                                                            <HeaderStyle Font-Bold="False" />
                                                            <SelectedRowStyle BackColor="BurlyWood" />
                                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                            <Columns>
                                                                 <asp:TemplateField HeaderText="HQ" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="#636d73" ItemStyle-ForeColor="#636d73" HeaderStyle-BackColor="#F1F5F8" ItemStyle-BackColor="#F1F5F8" >
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Fieldforce Name" HeaderStyle-ForeColor="#636d73" ItemStyle-ForeColor="#636d73" HeaderStyle-BackColor="#F1F5F8" ItemStyle-BackColor="#F1F5F8" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblsf" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                
                                                                <asp:TemplateField HeaderText="Working Type"  HeaderStyle-ForeColor="#636d73"  HeaderStyle-Font-Size="11px"  HeaderStyle-BackColor="#F1F5F8"  ItemStyle-Font-Size="10px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblwrktyp" runat="server" BackColor="#F1F5F8" ForeColor="#636d73" Text='<%#Eval("type_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <EmptyDataRowStyle ForeColor="Black" Height="5px" BorderColor="Black" BorderStyle="Solid"
                                                                BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:GridView>
                                                    </td>
                                                    <td width="80px"></td>
                                                    <td>
                                                        <asp:GridView ID="gvExpense" runat="server" Width="40%" HorizontalAlign="Center"
                                                            AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                            GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">
                                                            <HeaderStyle Font-Bold="False" />
                                                            <SelectedRowStyle BackColor="BurlyWood" />
                                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Name" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="#636d73" ItemStyle-ForeColor="#636d73" HeaderStyle-BackColor="#F1F5F8" ItemStyle-BackColor="#F1F5F8">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblusrdfd_username" runat="server" Text='<%#Eval("Expense_Parameter_Name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="10px" HeaderStyle-ForeColor="#636d73" ItemStyle-ForeColor="#636d73" HeaderStyle-BackColor="#F1F5F8" ItemStyle-BackColor="#F1F5F8">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSF_Name" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataRowStyle ForeColor="Black" Height="5px" BorderColor="Black" BorderStyle="Solid"
                                                                BorderWidth="2" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>

                        </div>
                    </div>
                </div>

















            </div>

            <asp:Panel ID="pnlvf" runat="server" Width="100%">


                <table width="100%" align="center">
                    <tbody>
                        <tr>
                            <td align="center"></td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <table width="50%" align="center">

                    <tr>
                        <td align="center"></td>
                    </tr>
                </table>
            </asp:Panel>
            
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

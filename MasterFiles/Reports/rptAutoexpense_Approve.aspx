<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptAutoexpense_Approve.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <title>Expense Statement Approval View</title>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
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
    <style type="text/css">
        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .gridheight {
            overflow-y: auto !important;
            height: 500px !important;
        }

        .gridFixedHeader {
            position: relative;
            top: expression(this.offsetParent.scrollTop);
            z-index: 10;
        }
                #grdSalesForce > tbody > tr:nth-child(1) {
            position: sticky;
            left: 0px;
            top:0px;
            z-index: 1;
            background-color: #F1F5F8;
        }
        #grdSalesForce > tbody > tr:nth-child(n) > th:nth-child(1) {
            position: sticky;
            left: 0px;
            top:0px;
            z-index: 2;
        }
        #grdSalesForce > tbody > tr:nth-child(n) > th:nth-child(2) {
            position: sticky;
            left: 41px;
            top:0px;
            z-index: 2;
        }
        #grdSalesForce > tbody > tr:nth-child(n+1) > td:nth-child(1)
         {
            position: sticky;
            left: 0px;
            top:0px;
            
        }
        #grdSalesForce > tbody > tr:nth-child(n+1) > td:nth-child(2)
         {
            position: sticky;
            left: 41px;
            top:0px;
            background-color:white;
        }

    </style>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlSubdiv]").select2();
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
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div>
                <%--<ucl:Menu ID="menu1" runat="server" />--%>
                <div>
                    <div class=" home-section-main-body position-relative clearfix">
                        <div class="row justify-content-center">
                            <div class="col-lg-5">
                                <h2 class="text-center" style="border-bottom: none">Approval (Active)</h2>
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblSubdiv" runat="server" CssClass="label" Text="FieldForce Name"></asp:Label>
                                        <asp:DropDownList ID="ddlSubdiv" runat="server" CssClass="custom-select2 nice-select" Width="100%" Visible="true"></asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" CssClass="label" Visible="false"></asp:DropDownList>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblMonth" runat="server" CssClass="label" Text="Month"></asp:Label>
                                        <asp:DropDownList ID="monthId" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblYr" runat="server" CssClass="label" Text="Year"></asp:Label>
                                        <asp:DropDownList ID="yearID" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btnSF" runat="server" Text="Go" CssClass="savebutton" Visible="true"
                                        OnClick="btnSF_Click" />
                                </div>

                                

                            </div>
                            <div class="col-lg-12">
                                <table align="right" style="margin-right: 20px;">
                                    <tr>
                                       
                                        <td align="right">
                                            <asp:Panel ID="pnlprint" runat="server" Visible="false">
                                                <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintGridData();">
                                                    <asp:Image ID="Image1" runat="server" ImageUrl="../../../assets/images/Printer.png" ToolTip="Print" Width="35px" Style="border-width: 0px;" />
                                                </asp:LinkButton>
                                                <p>
                                                    <asp:Label ID="Label1" runat="server" Text="Print" CssClass="label" Font-Size="14px"></asp:Label>
                                                </p>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                                <div class="designation-reactivation-table-area clearfix">
                                    <div class="display-table clearfix">
                                        <div class="table-responsive gridheight">
                                            <asp:GridView ID="grdSalesForce" runat="server" Width="85%" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                                CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                                AllowSorting="true">
                                                <%--OnSorting="grdSalesForce_Sorting"--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee ID">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsfempid" runat="server" Font-Size='8' Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fieldforce Name">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="sfNameHidden" runat="server" Value='<%#Eval("sf_name")%>' />
                                                            <asp:HiddenField ID="sfCodeHidden" runat="server" Value='<%#Eval("SF_Code")%>' />
                                                            <asp:HiddenField ID="Hiddenhold" runat="server" Value='<%#Eval("SF_VacantBlock")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Head Quater" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label align='left' ID="lblsfName" runat="server" Font-Size='8' Text='<%# Bind("sf_HQ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldesign" runat="server" Font-Size='8' Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DOJ" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDoJ" runat="server" Font-Size='8' Text='<%# Bind("Sf_Joining_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server" Font-Size='8' Text='<%# Bind("Status") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="viewHidden" runat="server" Value='Print' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Submission Date" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldt" runat="server" Font-Size='8' Text='<%# Bind("Date") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MGR Approved Date" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDTMGR" runat="server" Text='<%# Bind("Approval_Datea") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Admin Approved Date" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDTadmin" runat="server" Text='<%# Bind("admin_approval_date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DA" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsfAll" runat="server" Font-Size='8' Text='<%# Bind("allowance") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fare" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsfFare" runat="server" Font-Size='8' Text='<%# Bind("fare") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label13" Style="text-align: center; font-family: Calibri" Font-Size='8' Text='<%# Eval("Fixed_Column1")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label14" Style="text-align: center; font-family: Calibri" Font-Size='8' Text='<%# Eval("Fixed_Column2")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label15" Style="text-align: center; font-family: Calibri" Font-Size='8' Text='<%# Eval("Fixed_Column3")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label16" Style="text-align: center; font-family: Calibri" Font-Size='8' Text='<%# Eval("Fixed_Column4")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label17" Style="text-align: center; font-family: Calibri" Font-Size='8' Text='<%# Eval("Fixed_Column5")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label18" Style="text-align: center; font-family: Calibri" Font-Size='8' Text='<%# Eval("Fixed_Column6")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label167" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column7")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label168" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column8")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label169" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column9")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label170" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column10")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Miscellaneous" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmisamt" runat="server" Font-Size='8' Text='<%# Bind("mis_Amt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Additional Expense" ItemStyle-HorizontalAlign="Left">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblexpamt" runat="server" Font-Size='8' Text='<%# Bind("rw_amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" + " ItemStyle-HorizontalAlign="Left" Visible="true">
                                                        <ItemStyle Width="40px" HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIncrement" runat="server" Font-Size='8' Text='<%# Bind("Increment") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText=" - " ItemStyle-HorizontalAlign="Left" Visible="true">
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDecrement" runat="server" Font-Size='8' Text='<%# Bind("Decrement") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Claimed <br/>Amount(By MR)" ItemStyle-HorizontalAlign="Left">
                                                        <ControlStyle Width="90%"></ControlStyle>
                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClimedAmnt" runat="server" Font-Size='8' Text='<%# Bind("tot") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved <br/>Amount(By Admin)" ItemStyle-HorizontalAlign="Left">

                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAppAmnt" runat="server" Font-Size='8' Text='<%# Bind("appAmnt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="IP address" ItemStyle-HorizontalAlign="Left">

                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblipaddress" runat="server" Font-Size='8' Text='<%# Bind("IP_Address") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Approved By" ItemStyle-HorizontalAlign="Left">

                                                        <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                        <HeaderStyle></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsessionname" runat="server" Font-Size='8' Text='<%# Bind("session_name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" Font-Bold="True" HorizontalAlign="Center"
                                                    VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="loading" align="center">
                                        Loading. Please wait.<br />
                                        <br />
                                        <img src="../../Images/loader.gif" alt="" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center"></td>
                    </tr>
                </tbody>
            </table>
        </div>
		<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>

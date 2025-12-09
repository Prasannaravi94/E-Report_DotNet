<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Subdiv_Salesforcewise.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Subdivision - FieldForcewise View</title>

    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=grdSalesForce.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', '');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
                if (Prod == "---Select---") { alert("Select Sub Division Name."); $('#ddlSubdiv').focus(); return false; }
            });
        });
    </script>

     <script type="text/javascript">
        function Filter() {
            var grid = document.getElementById("<%=grdSalesForce.ClientID %>");
            var row = grid.rows.length;
            var FForce = document.getElementById("grdSalesForce_ctl01_txtsfName");
            var Desig = document.getElementById("grdSalesForce_ctl01_txtSFType");
            var HQ = document.getElementById("grdSalesForce_ctl01_txtHQ");
            var ReportingTo = document.getElementById("grdSalesForce_ctl01_txtReporting");
           

            var FForceValue = FForce.value.toLowerCase();
            var DesigValue = Desig.value.toLowerCase();
            var HQValue = HQ.value.toLowerCase();
            var ReportingToValue = ReportingTo.value.toLowerCase();           

            var FForceSplitter = FForceValue.split();
            var DesigSplitter = DesigValue.split();
            var HQSplitter = HQValue.split();
            var ReportingToSplitter = ReportingToValue.split();
          

            var display = '';

            var FForceRowValue = '';
            var DesigRowValue = '';
            var HQRowValue = '';
            var ReportingToRowValue = '';

            for (var i = 1; i < row; i++) {
                display = 'none';

                FForceRowValue = grid.rows[i].cells[1].innerText;
                DesigRowValue = grid.rows[i].cells[2].innerText;
                HQRowValue = grid.rows[i].cells[3].innerText;
                ReportingToRowValue = grid.rows[i].cells[4].innerText;

                for (var j = 0; j < FForceSplitter.length; j++) {
                    for (var k = 0; k < DesigSplitter.length; k++) {
                        for (var f = 0; f < HQSplitter.length; f++) {
                            for (var d = 0; d < ReportingToSplitter.length; d++) {

                                if (FForceRowValue.toLowerCase().indexOf(FForceSplitter[j]) >= 0 && DesigRowValue.toLowerCase().indexOf(DesigSplitter[k]) >= 0 && HQRowValue.toLowerCase().indexOf(HQSplitter[f]) >= 0 && ReportingToRowValue.toLowerCase().indexOf(ReportingToSplitter[d]) >= 0) {
                                            display = '';
                                        }

                                        else {
                                            display = 'none';
                                            break;                                       

                                }
                            }
                        }
                    }
                }
                grid.rows[i].style.display = display;
            }
        }
    </script>
     <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function tog(v) { return v ? 'addClass' : 'removeClass'; }
        $(document).on('input', '.clearable', function () {
            $(this)[tog(this.value)]('x');
        }).on('mousemove', '.x', function (e) {
            $(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]('onX');
        }).on('touchstart click', '.onX', function (ev) {
            ev.preventDefault();
            $(this).removeClass('x onX').val('').change();
            Filter();
        });
    </script>



</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">

                    <div class="col-lg-12">
                        <h2 class="text-center">Subdivision - FieldForcewise View</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading text-center clearfix">
                                <div class="d-inline-block division-name">
                                    <asp:Label ID="lblSubdiv" runat="server" Text="Sub Division Name"></asp:Label>
                                </div>
                                <div class="d-inline-block align-middle">
                                    <div class="single-des-option">
                                        <asp:DropDownList ID="ddlSubdiv" CssClass="nice-select" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <br />
                            <div style="text-align: center">
                                <asp:Button ID="btnSF" runat="server" Text="Go" CssClass="savebutton"
                                    OnClick="btnSF_Click" />
                            </div>
                            <br />

                            <div class="row ">
                                <div class="col-lg-12">
                                    <asp:Panel ID="pnlprint" runat="server" CssClass="panelmarright" Visible="false">
                                        <asp:LinkButton ID="lnkPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData()">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="30px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="display-table clearfix" align="center">
                                <div class="table-responsive overflow-x-none" align="center">

                                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                        CssClass="table">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>                                                  
                                                     <asp:Label ID="lblsfName" Text="FieldForce Name" runat="server" /><br />
                                                    <asp:TextBox ID="txtsfName" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="150px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                   <HeaderTemplate>                                                  
                                                     <asp:Label ID="lblSFType" Text="Designation" runat="server" /><br />
                                                    <asp:TextBox ID="txtSFType" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="150px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSFType" runat="server" Text='<%#Eval("Designation_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                 <HeaderTemplate>                                                  
                                                     <asp:Label ID="lblHQ" Text="HQ" runat="server" /><br />
                                                    <asp:TextBox ID="txtHQ" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="150px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reporting To" ItemStyle-HorizontalAlign="Left">
                                                 <HeaderTemplate>                                                  
                                                     <asp:Label ID="lblReporting" Text="Reporting To" runat="server" /><br />
                                                    <asp:TextBox ID="txtReporting" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="150px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstateName" runat="server" Text='<%# Bind("Reporting_To") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle  CssClass="no-result-area" />
                                    </asp:GridView>
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

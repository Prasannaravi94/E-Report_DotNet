<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_View.aspx.cs" Inherits="MasterFiles_Stockist_View"
    EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist View</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>

    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JsFiles/jquery.tooltip.min.js"></script>
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
            $('#btnSearch').click(function () {

                //var divi = $('#<%=ddlFields.ClientID%> :selected').text();
                // var divi1 = $('#<%=ddlSrc.ClientID%> :selected').text();
                //if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc').focus(); return false; }


            });
        });
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            //var printWindow = window.open('', '', 'height=400,width=800');
            var printWindow = window.open('', '', '');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

    <script src="../JScript/Service_CRM/Stockist_JS/Stockist_Add_Detail_JS.js" type="text/javascript"></script>

     <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
          $(document).ready(function () {
              $(".custom-select2").select2();
          });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
        </div>

        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center" style="border-bottom: 0px">Stockist View</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-name-heading clearfix">

                            <div class="row justify-content-center clearfix">
                                <div class="col-lg-3">
                                    <asp:Label ID="SearchBy" runat="server" Text="SearchBy" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlFields" runat="server" SkinID="ddlRequired" CssClass="nice-select">
                                        <asp:ListItem Value="">---Select---</asp:ListItem>
                                        <asp:ListItem Value="Stockist_Name">Stockist Name</asp:ListItem>
                                        <asp:ListItem Value="State" Selected="true">State Name</asp:ListItem>
                                        <asp:ListItem Value="Territory">HQ Name</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-lg-3">
                                    <div class="single-des clearfix" style="padding-top: 19px;">
                                        <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%"></asp:TextBox>

                                    </div>
                                    <div id="DDdisplay" style="margin-top: -20px;">
                                        <asp:DropDownList ID="ddlSrc" runat="server"
                                            CssClass="custom-select2 nice-select" Width="100%" TabIndex="4">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnProduct" runat="server" />
                                    </div>

                                </div>
                                <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="50px"
                                        Text="Go" CssClass="savebutton" OnClientClick="return ProcessData()"></asp:Button>

                                    <asp:Button ID="btnclr" OnClick="btnClear_Click" runat="server" Width="50px" Height="25px"
                                        Visible="false" Text="Clear" CssClass="savebutton"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <br />

                        <div class="text-center" style="border-bottom: 0px">
                            <asp:Label ID="lblValue" Text="Select the State Name" runat="server" Style="font-weight: bold; color: Red"></asp:Label>
                        </div>
                        <br />
                        <div class="row justify-content-center clearfix" style="scrollbar-width: thin; overflow-x: auto">
                            <table width="100%">
                                <tbody>
                                    <tr>

                                        <td colspan="2" align="center">
                                            <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                                runat="server" HorizontalAlign="Center">
                                                <SeparatorTemplate>
                                                </SeparatorTemplate>
                                                <ItemTemplate>
                                                    &nbsp
                                              <asp:LinkButton ID="lnkbtnAlpha" Font-Size="15px" runat="server"
                                                  CommandArgument='<%#Bind("Stockist_Name") %>' Text='<%#Bind("Stockist_Name") %>'>
                                              </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="row ">
                            <div class="col-lg-12">

                                <asp:Panel ID="pnlprint" runat="server" CssClass="panelmarright" Visible="false">
                                    <asp:LinkButton ID="btnPrint" ToolTip="Print" runat="server" OnClientClick="return PrintPanel();">
                                        <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="30px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClientClick="RefreshParent();">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="30px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                  
                                   <%-- <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                  OnClientClick="RefreshParent();" />--%>
                                </asp:Panel>

                            </div>
                        </div>

                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:Panel ID="pnlContents" runat="server">
                                    <table width="100%" align="center">
                                        <tbody>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:GridView ID="grdStockist" runat="server" Width="100%" HorizontalAlign="Center"
                                                        AutoGenerateColumns="False" EmptyDataText="No Records Found" OnPageIndexChanging="grdStockist_PageIndexChanging"
                                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                                        AllowSorting="True" OnSorting="grdStockist_Sorting">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ControlStyle Width="90%"></ControlStyle>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdStockist.PageIndex * grdStockist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Stockist_Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("Stockist_Code") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Stockist Name" SortExpression="Stockist_Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStockistName" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ERP Code" Visible="true" SortExpression="Stockist_Designation">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblERPCode" runat="server" Text='<%# Bind("Stockist_Designation") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="State" Visible="true" SortExpression="State">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HQ Name" Visible="true"
                                                                SortExpression="Territory">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Address">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStockist_Address" runat="server" Text='<%# Bind("Stockist_Address") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Phone No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStockist_Mobile" runat="server" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="FieldForce Name">
                                                                <ItemTemplate>
                                                                    <%--   <asp:Label ID="chkboxSalesforce" runat="server" Text='<%# ((string)Eval("SfName")).Replace("\n", "<br/>") %>' ></asp:Label>--%>
                                                                    <asp:Literal runat="server" ID="Values" Text='<%# string.Join("<br />", Eval("SfName").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                                    </asp:Literal>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </asp:Panel>
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
          <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

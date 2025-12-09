<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptAutoexpense_Approve_Resigned.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Expense Statement Approval View(Vacant/Resigned)</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
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

        .buttonlabel {
            background-color: #f1f5f8;
            border-radius: 10px;
            height: 35px;
            padding: 8px 10px 10px 10px;
            font-size: 14px !important;
            font-weight: 400;
            display: inline-block;
            margin-left: 875px;
            margin-top: -18px;
            color: #292a55 !important;
        }
    </style>
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
            <div id="Divid" runat="server"></div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <h2 class="text-center" style="border-bottom: none !important">Approval (Vacant/Resinged)</h2>
                        <asp:LinkButton ID="lnk" runat="server" Text="Old/Resigned Expense Process" CssClass="buttonlabel label" OnClick="lnk_Click">
                        </asp:LinkButton>
                    </div>
                    <div class="col-lg-5">
                        <div class="designation-area clearfix">
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
                                        CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <HeaderStyle Font-Bold="False" />
                                        <PagerStyle CssClass="pgr"></PagerStyle>
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
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
                                                    <asp:Label ID="lblsfempid" runat="server" Font-Size='8' Text='<%# Bind("Employee_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fieldforce Name">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="sfNameHidden" runat="server" Value='<%#Eval("sf_name")%>' />
                                                    <asp:HiddenField ID="sfCodeHidden" runat="server" Value='<%#Eval("SF_Code")%>' />
                                                    <asp:HiddenField ID="emphidden" runat="server" Value='<%#Eval("Employee_Id") %>' />
                                                    <asp:HiddenField ID="LstDCRdt" runat="server" Value='<%#Eval("dt") %>' />

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Head Quater" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesign" runat="server" Font-Size='8' Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DCR Start Date" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStartdt" runat="server" Text='<%# Bind("DCR_Start_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DCR end Date" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEnddt" runat="server" Text='<%# Bind("DCR_End_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Submission Date" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldt" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DA" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfAll" runat="server" Text='<%# Bind("allowance") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fare" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfFare" runat="server" Text='<%# Bind("fare") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column1")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label14" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column2")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label15" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column3")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label16" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column4")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label17" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column5")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="Label166" Style="text-align: center; font-family: Calibri" Text='<%# Eval("Fixed_Column6")%>' runat="server"></asp:Label>
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
                                                    <asp:Label ID="lblmisamt" runat="server" Text='<%# Bind("mis_Amt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" + " ItemStyle-HorizontalAlign="Left" Visible="true">
                                                <ItemStyle Width="40px" HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIncrement" runat="server" Text='<%# Bind("Increment") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText=" - " ItemStyle-HorizontalAlign="Left" Visible="true">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDecrement" runat="server" Text='<%# Bind("Decrement") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Claimed <br/>Amount(By MR)" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClimedAmnt" runat="server" Text='<%# Bind("tot") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved <br/>Amount(By Admin)" ItemStyle-HorizontalAlign="Left">

                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAppAmnt" runat="server" Text='<%# Bind("appAmnt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="sl_no" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblslno" runat="server" Text='<%# Bind("sl_no") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" Font-Bold="True" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
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
        </div>
    </form>
</body>
</html>

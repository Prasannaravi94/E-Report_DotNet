<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllowanceFixation_View.aspx.cs"
    Inherits="MasterFiles_AllowanceFixation_View" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Allowance Fixation View</title>
    <%--  <link type="text/css" rel="Stylesheet" href="../css/style.css" />--%>
    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <script type="text/javascript">
        function Calculation() {
            alert('test')
            var grid = document.getElementById("<%= grdWTAllowance.ClientID%>");
            for (var i = 0; i < grid.rows.length - 1; i++) {
                var txtAmountReceive = $("input[id*=myTextBox13]")
                if (txtAmountReceive[i].value != '') {

                    alert(txtAmountReceive[i].value);

                }
            }
        }
    </script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlRegion]');
            var $items = $('select[id$=ddlRegion] option');

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
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
        </div>
        <div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Allowance Fixation View</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblRegionWise" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>

                                        <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                            ToolTip="Enter Text Here"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="false"
                                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button Visible="true" ID="btnGo" runat="server" Width="50px" Text="Go" CssClass="savebutton"
                                            OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row">
                                <div class="col-lg-12">

                                    <asp:Panel ID="pnlExcel" runat="server" CssClass="panelmarright" Visible="false">
                                        <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClick="btnExcel_Click">
                                            <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="30px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <%-- <asp:LinkButton ID="btnExcel" BorderColor="Black" BorderWidth="1PX" BackColor="#66ff66" Text="Excel" OnClick="btnExcel_Click" runat="server" Style="text-align: center; width: 60px; height: 22px; text-decoration: none; color: Black"></asp:LinkButton>--%>
                                    </asp:Panel>

                                    <asp:Panel ID="pnlprint" runat="server" CssClass="panelmarright" Visible="false">
                                        <asp:LinkButton ID="lnkPrint" ToolTip="Print" runat="server">
                                            <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="30px" Style="border-width: 0px;" />
                                        </asp:LinkButton>
                                        <%--   <input type="button" id="btnPrint" value="Print" style="width: 60px; height: 25px"
                                            onclick="PrintGridData()" />--%>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; height: 700px">
                                    <asp:GridView ID="grdWTAllowance" Width="100%" runat="server" AutoGenerateColumns="false"
                                        BorderStyle="Solid" CssClass="table" AlternatingRowStyle-CssClass="alt"
                                        GridLines="None">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="4%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFieldForceName" Width="240px" Text='<%# Eval("sf_Name")%>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation_Code" HeaderStyle-Width="10%" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesigcode" Text='<%# Eval("Designation_Code")%>' Visible="false"
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SF_Code" HeaderStyle-Width="10%" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfcode" Text='<%# Eval("sf_code")%>' Visible="false"
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" Text='<%# Eval("Designation_Short_Name")%>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_hq" Width="120px" Text='<%# Eval("sf_hq")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ_Exp" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtHq" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("HQ_Allowance")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EX-HQ" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtEXHQ" Style="text-align: center;" Width="80px" CssClass="input" Text='<%# Eval("EX_HQ_Allowance")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OS" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtOS" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("OS_Allowance")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hill" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtHill" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Hill_Allowance")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fare/KM" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtFareKm" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("FareKm_Allowance")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range of Fare" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <table id="tblData" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="center" width="3%">Range I Above Kms.&nbsp;
                                        <asp:Label ID="txtRange" Style="text-align: center;" Width="50px" Text='60' CssClass="input" runat="server"></asp:Label>
                                                                <asp:RadioButtonList Enabled="false" ID="rbtLstRating" runat="server"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                    <asp:ListItem Text="Consolidated" Selected="true" Value="Consolidated"></asp:ListItem>
                                                                    <asp:ListItem Text="Separate" Value="Separate"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="txtRangeofFare1" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Range_of_Fare1")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range of Fare" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <table id="tblData2" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="center">Range II Above Kms.&nbsp;
                                        <asp:Label ID="txtRange2" Style="text-align: center;" Width="50px" Text='60' CssClass="input" runat="server"></asp:Label>
                                                                <asp:RadioButtonList Enabled="false" ID="rbtLstRating2" runat="server"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                    <asp:ListItem Text="Consolidated" Selected="True" Value="Consolidated"></asp:ListItem>
                                                                    <asp:ListItem Text="Separate" Value="Separate"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="txtRangeofFare2" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Range_of_Fare2")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox13" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column1")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox14" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column2")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox15" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column3")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox16" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column4")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox17" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column5")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column1" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox20" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Wtype_Fixed_Column1")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column2" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox21" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Wtype_Fixed_Column2")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column3" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox22" Style="text-align: center;" Width="70px" Text='<%# Eval("Wtype_Fixed_Column3")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column4" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox23" Style="text-align: center;" Width="70px" Text='<%# Eval("Wtype_Fixed_Column4")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column5" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox24" Style="text-align: center;" Width="70px" Text='<%# Eval("Wtype_Fixed_Column5")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Effective From" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtEffective" Style="text-align: center;" Width="100px" CssClass="input" runat="server" Text='<%# Eval("Effective_Form")%>' ReadOnly="false" MaxLength="10"></asp:Label>
                                                    <%-- <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffective"  runat="server" />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField>                                                          
                               <ItemTemplate>
                                    <asp:Repeater ID="rptCont" runat="server">
                                        <ItemTemplate>
                                                   <asp:TextBox ID="txtRept" runat="server"></asp:TextBox> 
                                        </ItemTemplate>
                                    </asp:Repeater>
                              </ItemTemplate>
                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>

                                    <br />

                                    <asp:Panel ID="pnlContents" runat="server" Width="100%" Style="display: none">
                                        <asp:GridView ID="GrdViewExpense" runat="server" Width="100%" HorizontalAlign="Center"
                                            AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                            CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" HeaderStyle-Width="4%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" SkinID="lblMand" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFieldForceName" Width="240px" Text='<%# Eval("sf_Name")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation_Code" HeaderStyle-Width="10%" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesigcode" Text='<%# Eval("Designation_Code")%>' Visible="false"
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SF_Code" HeaderStyle-Width="10%" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsfcode" Text='<%# Eval("sf_code")%>' Visible="false"
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignation" Text='<%# Eval("Designation_Short_Name")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HQ" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsf_hq" Width="120px" Text='<%# Eval("sf_hq")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HQ_Exp" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtHq" Style="text-align: center;" Width="50px" Text='<%# Eval("HQ_Allowance")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="EX-HQ" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtEXHQ" Style="text-align: center;" Width="60px" Text='<%# Eval("EX_HQ_Allowance")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OS" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtOS" Style="text-align: center;" Width="50px" Text='<%# Eval("OS_Allowance")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hill" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtHill" Style="text-align: center;" Width="50px" Text='<%# Eval("Hill_Allowance")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fare/KM" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtFareKm" Style="text-align: center;" Width="50px" Text='<%# Eval("FareKm_Allowance")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Range of Fare" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <table id="tblData" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center" width="3%">Range I Above Kms.&nbsp;
                                        <asp:Label ID="txtRange" Style="text-align: center;" Width="50px" Text='60' runat="server"></asp:Label>
                                                                    <asp:RadioButtonList ID="rbtLstRating" runat="server"
                                                                        RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                        <asp:ListItem Text="Consolidated" Selected="true" Value="Consolidated"></asp:ListItem>
                                                                        <asp:ListItem Text="Separate" Value="Separate"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRangeofFare1" Style="text-align: center;" Width="50px" Text='<%# Eval("Range_of_Fare1")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Range of Fare" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <table id="tblData2" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td align="center">Range II Above Kms.&nbsp;
                                        <asp:Label ID="txtRange2" Style="text-align: center;" Width="50px" Text='60' runat="server"></asp:Label>
                                                                    <asp:RadioButtonList ID="rbtLstRating2" runat="server"
                                                                        RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                        <asp:ListItem Text="Consolidated" Selected="True" Value="Consolidated"></asp:ListItem>
                                                                        <asp:ListItem Text="Separate" Value="Separate"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRangeofFare2" Style="text-align: center;" Width="50px" Text='<%# Eval("Range_of_Fare2")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox13" Style="text-align: center;" Width="50px" Text='<%# Eval("Fixed_Column1")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox14" Style="text-align: center;" Width="50px" Text='<%# Eval("Fixed_Column2")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox15" Style="text-align: center;" Width="50px" Text='<%# Eval("Fixed_Column3")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox16" Style="text-align: center;" Width="50px" Text='<%# Eval("Fixed_Column4")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox17" Style="text-align: center;" Width="50px" Text='<%# Eval("Fixed_Column5")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Wtype_Fixed_Column1" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox20" Style="text-align: center;" Width="50px" Text='<%# Eval("Wtype_Fixed_Column1")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Wtype_Fixed_Column2" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox21" Style="text-align: center;" Width="50px" Text='<%# Eval("Wtype_Fixed_Column2")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Wtype_Fixed_Column3" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox22" Style="text-align: center;" Width="50px" Text='<%# Eval("Wtype_Fixed_Column3")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Wtype_Fixed_Column4" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox23" Style="text-align: center;" Width="50px" Text='<%# Eval("Wtype_Fixed_Column4")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Wtype_Fixed_Column5" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TextBox24" Style="text-align: center;" Width="50px" Text='<%# Eval("Wtype_Fixed_Column5")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Effective From" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtEffective" Style="text-align: center;" Width="80px" runat="server" Text='<%# Eval("Effective_Form")%>' ReadOnly="false" MaxLength="10"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>

                                    </asp:Panel>
                                </div>
                            </div>
                            <br />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

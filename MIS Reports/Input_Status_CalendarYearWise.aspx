<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input_Status_CalendarYearWise.aspx.cs" Inherits="MIS_Reports_Input_Status_CalendarYearWise" %>

<%@ Register Src="~/UserControl/MenuUserControl_TP.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Despatch Status</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" href="../../css/Report.css" rel="Stylesheet" />
    <%--<link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />--%>
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <style type="text/css">
        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
            width: 190px;
            height: 21px;
            font: bold;
            background-image: url('Images/arrow_sort_d.gif');
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow:;
        }

        #effect {
            width: 180px;
            height: 160px;
            padding: 0.4em;
            position: relative;
            overflow: auto;
        }

        .textbox {
            width: 185px;
            height: 14px;
        }

        body {
            font-size: 62.5%;
        }

        td.stylespc {
            padding-bottom: 20px;
            padding-right: 10px;
        }

        .style1 {
            width: 195px;
        }

        .style2 {
            width: 232px;
        }
        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }
    </style>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $('#btnGo').click(function () {

            setTimeout(function () {
                var loading = $(".loading");
                loading.hide();
                $('.modal').hide();
            }, 3000);
        });
                

                $('#btnexcel').click(function () {

                    setTimeout(function() {
                        var loading = $(".loading");
                        loading.hide();
                        $('.modal').hide();
                    }, 3000);
                });
            });
        </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <div>
            <br />
            <br />
            <tr>
                                <td align="center">
                        <h2 class="text-center">Input Status Financial Year Wise</h2></td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                           </tr>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <div class="designation-area clearfix">
                                                        <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Financia Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFinancial" runat="server" CssClass="nice-select" > <%--AutoPostBack="true" OnSelectedIndexChanged="ddlmode_SelectedIndexChanged"--%>
                                        
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:CheckBox ID="Fieldforcewise" Text="FFwise" runat="server" />
                            </div>
<%--                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>

                                    <div class="row">
                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" Visible="false"
                                                CssClass="nice-select">
                                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-lg-6" style="padding-bottom: 0px;">

                                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" CssClass="nice-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select " Width="100%">
                                    </asp:DropDownList>
                                    <br /> 
                                    <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>


<%--                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-7">
                                        <asp:CheckBox ID="chkVacant" Text="With Vacant" Font-Size="Medium" Font-Names="Calibri"
                                            Checked="true" ForeColor="Red" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>--%>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix" style="padding-bottom: 20px;">

                            <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Text="View" OnClick="btnGo_Click" />
                            <asp:Button ID="btnexcel" runat="server" CssClass="savebutton" Text="Excel" OnClick="btnexcel_Click" />
                            <%--  <asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="View" CssClass="savebutton"
                                      OnClick="btnGo_Click" />--%>
                           
                        </div>
                        </div>
						<br />
                    <div class="col-lg-9">
                        <div class="designation-reactivation-table-area clearfix">
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow:inherit">
                                    <asp:GridView ID="grdDespatch" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" BorderWidth="0" Width="100%">
                                        <HeaderStyle Font-Bold="False" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprdcode" runat="server" Text='<%#Eval("productc")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GiftS Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprd_erp" runat="server" Text='<%#Eval("Gift_SName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gift Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprd_Name" runat="server" Text='<%#Eval("Gift_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gift Type" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltype" runat="server" Text='<%#Eval("Gift_Type")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening Balance" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblob" runat="server" Text='<%# Bind("Opening_Balance") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Despatch Qty" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDes" runat="server" Text='<%# Bind("Despatch_Qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issued Qty" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblissued" runat="server" Text='<%# Bind("Input_Issued") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Closing" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblclosing" runat="server" Text='<%# Bind("Closing") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        </div>
                    <div class="col-lg-12">
                        <div class="designation-reactivation-table-area clearfix">
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow:inherit">
                                    <asp:GridView ID="Griddespatchdetail" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" BorderWidth="0" Width="100%">
                                        <HeaderStyle Font-Bold="False" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprdcode" runat="server" Text='<%#Eval("productc")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Sf Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfcode" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblff_Name" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblffHq" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Employee ID" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblffempid" runat="server" Text='<%#Eval("sf_emp_id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblffdesign" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GiftS Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprd_erp" runat="server" Text='<%#Eval("Gift_SName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gift Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblprd_Name" runat="server" Text='<%#Eval("Gift_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gift Type" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltype" runat="server" Text='<%#Eval("Gift_Type")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Opening Balance" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblob" runat="server" Text='<%# Bind("Opening_Balance") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Despatch Qty" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDes" runat="server" Text='<%# Bind("Despatch_Qty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issued Qty" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblissued" runat="server" Text='<%# Bind("Input_Issued") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Closing" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblclosing" runat="server" Text='<%# Bind("Closing") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                          </div>
                    </div>
                </div>
            </div>

            <br />
            <br />


        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>

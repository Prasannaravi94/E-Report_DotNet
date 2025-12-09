<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order_booking.aspx.cs" EnableEventValidation="false"
    Inherits="MasterFiles_MR_Order_booking" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Booking Entry</title>
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script src="../../JsFiles/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/bootstrap_3.3.7.js"></script>
    <link type="text/css" rel="stylesheet" href="../../css/bootstrap_3.3.7.css" />
    <script type="text/javascript" src="../../JsFiles/Order.js"></script>

    <style type="text/css">
        header {
            max-width: 100%;
            margin: 0px auto;
            padding: 0px;
            border-radius: 0px;
        }

        .navbar-nav {
            margin: 7.5px -15px;
        }

        #tblprd div {
            display: none;
        }

        #tblprd .btn-warning {
            width: 70px;
            height: 32px;
            border-radius: 8px;
            background-color: #e9f7fb;
            color: #27b4e0;
            font-size: 12px;
            font-weight: 600;
            border: 0px;
            cursor: pointer;
            margin: 0 3px;
            padding: 0px;
            margin-top: 5px;
            margin-bottom: 5px;
        }

        .GridStyle {
            border: 3px solid #DCE2E8;
            background-color: White;
            font-family: arial;
            font-size: 11px;
            border-collapse: collapse;
            margin-bottom: 0px;
            height: 20px;
        }

            .GridStyle tr {
                border: 1px solid rgb(217, 231, 255);
                color: Black;
            }


            /* Your grid header column style */
            .GridStyle th {
                background-color: #F1F5F8;
                border: none;
                text-align: left;
                font-weight: bold;
                font-size: 15px;
                padding: 4px;
                color: Black;
            }
            /* Your grid header link style */
            .GridStyle tr th a, .GridStyle tr th a:visited {
                color: Black;
            }

            .GridStyle tr th, .GridStyle tr td table tr td {
                border: none;
            }

            .GridStyle td {
                border-bottom: 1px solid rgb(217, 231, 255);
                padding: 2px;
            }

        table.GridStyle tr:hover {
            background-color: #fdff9c;
        }
    </style>
    <%--    for calender --%>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <%--<link type="text/css" rel="Stylesheet" href="../../css/Order.css" />--%>
    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('#txtdate').datepicker
            ({
                changeMonth: true,
                changeYear: true,
                yearRange: '1930:' + new Date().getFullYear().toString(),
                dateFormat: 'dd/mm/yy'
            });
        });

    </script>
    <style type="text/css">
        .mGridd {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGridd td {
                padding: 2px;
                border: solid 1px #c1c1c1;
            }

            .mGridd th {
                padding: 4px 2px;
                color: black;
                background: #afdd92 url(grd_head.png) repeat-x top;
                border-left: solid 1px #525252;
                font-size: 0.9em;
                text-align: center;
            }

            .mGridd .alt {
                background: #fcfcfc url(grd_alt.png) repeat-x top;
            }

            .mGridd .pgr {
                background: #0097AC url(grd_pgr.png) repeat-x top;
            }

                .mGridd .pgr table {
                    margin: 5px 0;
                }

                .mGridd .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .mGridd .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGridd .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }

        .GVdd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-family: Andalus;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            width: 70px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }

        .dtxt {
            border: 1px solid #ffffFF;
            border-radius: 4px;
            margin: 2px;
            width: 90px;
            text-align: center;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }

        .btn:hover {
            background-color: #9ACD32;
        }

        .GVFixedHeader {
            font-weight: bold;
            background-color: Green;
            position: relative;
            top: expression(this.parentNode.parentNode.parentNode.scrollTop-1);
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }
    </style>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlstock_list]").select2();
        });
    </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">

                    <h2 class="text-center">Order Booking Entry</h2>
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-5">
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <asp:TextBox ID="lblname" runat="server" CssClass="custom-select2 nice-select" Enabled="false" Width="100%">
                                </asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblstock_list" Text="Stockist Name" runat="server" CssClass="label">
                                </asp:Label>
                                <asp:DropDownList ID="ddlstock_list" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lbldate" Text="Date" runat="server" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="nice-select form-control"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="Mode" runat="server" CssClass="label" Text="Mode "></asp:Label>
                                    <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="1" Text="Pharmacist/Chemist"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Hospital"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Doctor"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="."></asp:Label>
                                    <asp:DropDownList ID="ddlmode_val" runat="server" CssClass="nice-select" Width="100%">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hdnsf_code" runat="server" />
                                    <asp:HiddenField ID="hdndiv_code" runat="server" />
                                    <asp:HiddenField ID="hdnsub_div" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" />
                            <asp:Button ID="btnclear" runat="server" Text="Clear" CssClass="savebutton" />
                        </div>
                    </div>
                    <div class="col-lg-1">
                    </div>
                    <div class="col-lg-5">

                        <table width="100%" class="table" style="border: 1px solid #DCE2E8; margin-top: 20px;">
                            <tr style="border: 1px solid #DCE2E8;" class="success">
                                <th colspan="2" style="border: 1px solid #DCE2E8; text-align: center; color: #D35400; font-weight: bold">Listed Doctor
                                </th>
                                <th colspan="2" style="border: 1px solid #DCE2E8; text-align: center; color: #1A5276; font-weight: bold">Pharmacist
                                </th>
                                <th colspan="2" style="border: 1px solid #DCE2E8; text-align: center; color: #6C3483; font-weight: bold">Hospital
                                </th>
                                <th colspan="2" style="border: 1px solid #DCE2E8; text-align: center; color: #D35400; font-weight: bold">Stockist
                                </th>
                            </tr>
                            <tr class="active" style="border: 1px solid #DCE2E8;">
                                <td align="left" style="border: 1px solid #DCE2E8;">Total
                                </td>
                                <td align="left" style="border: 1px solid #DCE2E8;">
                                    <asp:Label ID="totdr" runat="server" ForeColor="Magenta"></asp:Label>
                                </td>
                                <td align="left" style="border: 1px solid #DCE2E8;">Total
                                </td>
                                <td align="left" style="border: 1px solid #DCE2E8;">
                                    <asp:Label ID="totpharm" runat="server" ForeColor="Magenta"></asp:Label>
                                </td>
                                <td align="left" style="border: 1px solid #DCE2E8;">Total
                                </td>
                                <td align="left" style="border: 1px solid #DCE2E8;">
                                    <asp:Label ID="tothosp" runat="server" ForeColor="Magenta"></asp:Label>
                                </td>
                                <td align="left" style="border: 1px solid #DCE2E8;">Total
                                </td>
                                <td align="left" style="border: 1px solid #DCE2E8;">
                                    <asp:Label ID="totstockist" runat="server" ForeColor="Magenta"></asp:Label>
                                </td>
                            </tr>
                        </table>

                    </div>

                </div>
            </div>
            <br />
            <br />
            <panel id="fulpnl">
             <div class="row justify-content-center">
             <div class="col-lg-5">
                 <div id="div_prd" align="left"  runat="server" style="display:none;">
                 <asp:Label ID="lblprd" Text="Product Name" runat="server" CssClass="lable_css"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:DropDownList ID="ddlprd" CssClass="btn dropdown-toggle dropdown-toggle-split" Width="200px"  runat="server"></asp:DropDownList>
                 <asp:Button ID="btnprdGo" runat="server" Text="Go" Width="50px"  
                  CssClass="savebutton"  />
                 </div>
            </div>
            </div>
             

             <center>
             <br />
        
             <table width="80%">
                <tr>
                    <td>
                    <section class="">
                   <div class="container">
                    <table id="tblprd"  class="GridStyle" style="width:100%; display:none">
                    </table>
                    </div>
                    </section>
                        <br />

                   <div width="100%" id="totall" style="float:right; padding-right:5%; display:none">

                    <asp:Label ID="lblsaletot" runat="server" Font-Bold="True" ForeColor="#db006e"  Text="Sale Tot :-"
                    Font-Size="14px" BackColor="#F1F5F8" BorderColor="#DCE2E8" BorderStyle="Solid"
                    BorderWidth="2px" Width="130px" ></asp:Label>
   
                    <asp:Label ID="lblfoctot" runat="server" Font-Bold="True" ForeColor="#db006e" Font-Names="Andalus" Text="FOC Tot :-"
                    Font-Size="14px" BackColor="#F1F5F8" BorderColor="#DCE2E8" BorderStyle="Solid"
                    BorderWidth="2px" Width="100px" ></asp:Label>

                    <asp:Label ID="lblratetot" runat="server" Font-Bold="True" ForeColor="#db006e" Font-Names="Andalus" Text="Rate Tot :-"
                    Font-Size="14px" BackColor="#F1F5F8" BorderColor="#DCE2E8" BorderStyle="Solid"
                    BorderWidth="2px" Width="130px" ></asp:Label>

                    <asp:Label ID="lblamtot" runat="server" Font-Bold="True" ForeColor="#db006e" Font-Names="Andalus" Text="Amount Tot :-"
                    Font-Size="14px" BackColor="#F1F5F8" BorderColor="#DCE2E8" BorderStyle="Solid"
                    BorderWidth="2px" Width="150px" ></asp:Label>

                    <asp:Label ID="lblNRV" runat="server" Font-Bold="True" ForeColor="#db006e" Font-Names="Andalus" Text="NRV Tot :-"
                    Font-Size="14px" BackColor="#F1F5F8" BorderColor="#DCE2E8" BorderStyle="Solid"
                    BorderWidth="2px" Width="150px" ></asp:Label>
                    
                    <asp:Label ID="lblnettot" runat="server" Font-Bold="True" ForeColor="#db006e" Font-Names="Andalus" Text="Net Amt Tot :-"
                    Font-Size="14px" BackColor="#F1F5F8" BorderColor="#DCE2E8" BorderStyle="Solid"
                    BorderWidth="2px" Width="150px" ></asp:Label>

             </div>

                           <asp:HiddenField ID="hdntot_values" runat="server" />
                        <%--<asp:GridView ID="grdPrd" runat="server" GridLines="Both" HeaderStyle-BackColor="#0097AC"
                           AllowSorting="true" RowStyle-Wrap="false" HeaderStyle-Font-Bold="true" AutoGenerateColumns="false"
                            HeaderStyle-BorderStyle="Solid" HeaderStyle-BorderColor="#EFF7EA" HeaderStyle-BorderWidth="1px"
                            EmptyDataText="No Records Found" Width="80%" Font-Size="8pt" HeaderStyle-Font-Size="16px"
                            HeaderStyle-Font-Names="Andalus" CssClass="mGridd tablesorter" HeaderStyle-Height="24px"
                            AlternatingItemStyle-BorderStyle="Groove" Font-Names="Segoe UI Semibold" Font-Bold="True">
                            <HeaderStyle CssClass="GVFixedHeader" />
                            <AlternatingRowStyle BorderStyle="Solid" BackColor="#EFF7EA" />
                            <HeaderStyle BackColor="#0097AC" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
                                Height="10px" />
                            <Columns>
                                <asp:TemplateField HeaderText="#" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnProduct_Code_SlNo" runat="server" Value='<%#Eval("Product_Code_SlNo") %>' />
                                        <asp:Label ID="lblProduct_name" runat="server" Text='<%#  Eval("Product_Detail_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pack" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpack" runat="server" Text='<%#  Eval("Pack") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtsaleqty" runat="server" onkeyup="FetchData(this)" Text='<%#  Eval("saleqty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FOC Qty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblFoc_qty" runat="server" Text='<%#  Eval("Foc_qty") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                         <asp:TextBox ID="lblrate" runat="server" Text='<%#  Eval("rate") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblamt" runat="server" Text='<%#  Eval("amt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>--%>
                    </td>
                </tr>
            </table>
            <br />
             <div class="div_fixed_Submit">
            <asp:Button ID="btnsubmit" runat="server" Text="Save" Width="60px"  CssClass="savebutton"   style="display:none" 
                             />
                            </div>
            
            </center>
            </panel>
        </div>
        <br />
        <br />
        <div>
        </div>
    </form>
    <script type="text/javascript">
        function AddNewRecord() {

            //            var grd = document.getElementById('grdPrd');
            //            var tbod = grd.rows[0].parentNode;
            //            alert(grd.rows.length);
            //            var newRow = grd.rows[grd.rows.length - 1].cloneNode(true);
            //          
            //            tbod.appendChild(newRow);
            //            return false;


            //            var rows = '<tr>';
            //            rows += '<th>S.No</th>';

            //            rows += '<th>Product Name</th>';
            //            rows += '<th>Pack</th>';
            //            rows += '<th>Sale Qty</th>';
            //            rows += '<th>FOC Qty</th>';
            //            rows += '<th>Rate</th>';
            //            rows += '<th>Amount</th>';

            //            rows += '</tr>';
            //            $("#tblprd").html(rows);
            return false;

            //            $.ajax({

            //                type: "POST",
            //                url: "TP_ENTRY_STP.aspx/GetCategory_List",
            //                data: '{objCat:' + JSON.stringify(Catobj) + '}',
            //                contentType: "application/json; charset=utf-8",
            //                dataType: "json",
            //                success: function (data) {
            //                    if (data.d.length > 0) {

            //                        var Total = 0;
            //                        var totdr = 0;
            //                        for (var i = 0; i < data.d.length; i++) {
            //                            rows += '<tr>';
            //                            rows += '<td>' + data.d[i].SNo + '</td>';

            //                            rows += '<td>' + data.d[i].CatName + '</td>';
            //                            rows += '<td>' + data.d[i].DrCnt + '</td>';
            //                            rows += '<td>' + data.d[i].VisitCnt + '</td>';
            //                            // rows += '<td>' + data.d[i].RemainStock + '</td>';
            //                            //   rows += '<td><a id="lnkStock_' + i + '" href="#"  title="RemainStock">' + data.d[i].RemainStock + '</a></td>';
            //                            totdr = totdr + parseInt(data.d[i].DrCnt);
            //                            Total = Total + parseInt(data.d[i].VisitCnt);

            //                            rows += '</tr>';
            //                        }


            //                        rows += '<tr>';
            //                        rows += '<td colSpan="2">Total</td>';
            //                        rows += '<td>' + totdr + '</td>';
            //                        rows += '<td>' + Total + '</td>';
            //                        //rows += '<td>' + Total + '</td>';
            //                        rows += '</tr>';

            //                        $("#tblprd").html(rows);
            //                    }

            //                },
            //                error: function (res) {
            //                }
            //            });



        }
    </script>
    <script type="text/javascript">
        function FetchData(lnk) {
            var idString = lnk.id;
            var $i1 = idString.indexOf("_");
            $i1 = $i1 + 4;
            var $i2 = idString.indexOf("_", $i1);
            var cnt = 0;
            var index = '';

            if ((idString.substring($i1, $i2) - 1) < 10) {

                var cIdprev = parseInt(idString.substring($i1, $i2));
                index = cnt.toString() + cIdprev.toString();
            }
            else {
                var cIdprev = parseInt(idString.substring($i1, $i2));
                index = cIdprev;
            }
            alert(index);
        }
    </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>

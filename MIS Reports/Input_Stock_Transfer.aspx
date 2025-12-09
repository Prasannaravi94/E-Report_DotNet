<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input_Stock_Transfer.aspx.cs" Inherits="MIS_Reports_Input_Stock_Transfer" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sample-Stock Transfer</title>
<%--     <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
      <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
      <script type="text/javascript">
        $('#btnGo').click(function () {
                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }

                var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
                var sf_name = document.getElementById('<%=ddlFieldForce.ClientID%>').text;

                showModalPopUp(sfcode, Month1, Year1, SName);


            });
    </script>
     <script type="text/javascript">
         $(function () {
             var $txt = $('input[id$=txtNew]');
             var $ddl = $('select[id$=ddlFieldForce]');
             var $items = $('select[id$=ddlFieldForce] option');

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
     <script type="text/javascript">
        function Validate() {
            //Reference the GridView.
    
            var gv = document.getElementById("<%= grdsample.ClientID %>");
        var lb = gv.getElementsByTagName("span");
        var rowCount = gv.rows.length;
        //Reference all INPUT elements.
        // var inputs = grid.getElementsByTagName("INPUT");
 
        //Set the Validation Flag to True.
       // var isValid = true;
        for (var i = 1; i < rowCount; i++) {

            var row = gv.rows[i];
            var txt = row.getElementsByTagName('input');
            var label = row.getElementsByTagName('span');
            var area = row.getElementsByTagName('textarea');
            if (Number(txt[0].value) > label[3].innerHTML) {
                alert('Not Possible to Enter more then Closing Qty')
                txt[0].focus();
                return false;
            } 

        }
 
        }
    </script>
        <script type="text/javascript">
        var click = 0;
        $(function () {
            $("[id*=txtQuantity]").val("0");
            $('[id*=btn1], [id*=btn2]').on('click', function () {
                var value = 0;
                if ($(this).attr('class') == 'Minus') {
                    value = parseInt($(this).closest('tr').find('[id*=txtTransferQty]').val());
                    click = 1;
                }
                if ($(this).attr('class') == 'Plus') {
                    value = parseInt($(this).closest('tr').find('[id*=txtTransferQty]').val());
                    click = 2;
                }
                if (value < 0) {
                    value = 0;

                }
                $(this).closest('tr').find('[id*=txtTransferQty]').val(value);
                CalculateTotal($(this).closest('tr').find('[id*=txtTransferQty]'));
                return false;
            });
        });
        $("body").on("change keyup", "[id*=txtTransferQty]", function () {
            CalculateTotal($(this));
        });

        function CalculateTotal(ele) {
            //Check whether Quantity value is valid Float number.
            var quantity = parseFloat($.trim($(ele).val()));
            if (isNaN(quantity)) {
                quantity = 0;
            }

            //Update the Quantity TextBox.
            $(ele).val(quantity);

            //Calculate and update Row Total.
            var row = $(ele).closest("tr");
            //if ($(this).attr('class') == 'Minus') {
            if (click == 2) {
                $("[id*=lbltransferqty]", row).html(parseFloat($(".ClosingBal", row).html()) + parseFloat($(ele).val()));
                $("[id*=hiddtransqty]", row).html("PLUS");
                $("[id*=HiddenField1]", row).val(parseFloat($(".ClosingBal", row).html()) + parseFloat($(ele).val()));
            }
            else if (click == 1) {
                $("[id*=lbltransferqty]", row).html(parseFloat($(".ClosingBal", row).html()) - parseFloat($(ele).val()));
                $("[id*=hiddtransqty]", row).html("MINUS");
                $("[id*=HiddenField1]", row).val(parseFloat($(".ClosingBal", row).html()) - parseFloat($(ele).val()));
            }

            //}
            //else if ($(this).attr('class') == 'Plus') {
            //            $("[id*=lbltransferqty]", row).html(parseFloat($(".ClosingBal", row).html()) + parseFloat($(ele).val()));
            //}

            //$("[id*=lbltransferqty]", row).html(parseFloat($(this).closest('tr').prev().prev().children('td.this-is-a-label')) + parseFloat($(ele).val()));

            //Calculate and update Grand Total.
            //var grandTotal =0;
            //$("[id*=lbltransferqty]").each(function () {
            //    grandTotal = grandTotal + parseFloat($(this).html());
            //});
            //$("[id*=lblGrandTotal]").html(grandTotal.toString());
        }
    </script>
    <script type="text/javascript">
         $(function () {
             var $txt = $('input[id$=txtNew1]');
             var $ddl = $('select[id$=toddlFieldForce]');
             var $items = $('select[id$=toddlFieldForce] option');

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
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
        <link href="../../assets/css/select2.min.css" rel="stylesheet" />
       <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
    <script type="text/javascript" src="//code.jquery.com/jquery-3.6.0.min.js"></script>
        
</head>
<body>
    <form id="form1" runat="server">
        <div>
                  <div id="Divid" runat="server">
        </div>
    <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                 <h2 class="text-center">Stock Transfer/Adjustments - FF wise</h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblmode" runat="server" CssClass="label" Text="Mode"></asp:Label>
                                <asp:DropDownList ID="ddlmode" OnSelectedIndexChanged="ddlmodeselectedindexchange" AutoPostBack="true" runat="server" Width="200px" CssClass="nice-select" > <%--onchange="jsFunction(this.value);"--%>
                                    <asp:ListItem Selected="True" Value="1" Text="Active Fieldforce to Active Fieldforce Stock Transfer Active"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Vacant Fieldforce to Active Filedforce Stock Transfer"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Stock Adjustment"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Process"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="From Fieldforce"></asp:Label>

                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="custom-select2 nice-select" Visible="false" SkinID="ddlRequired"></asp:DropDownList>
                            </div>
                            <div class="single-des clearfix" id="tofieldforce">
                                <asp:Label ID="lblffto" runat="server" CssClass="label" Text="To Fieldforce"></asp:Label>

                                <asp:DropDownList ID="toddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF2" runat="server" CssClass="custom-select2 nice-select" Visible="false" SkinID="ddlRequired"></asp:DropDownList>


                            </div>
                 <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6" id="ddlmon">
                                        <asp:Label ID="lblMonth" runat="server" Text="Month" CssClass="label"></asp:Label>

                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select">
                                            <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-6" id="ddlyear">
                                        <asp:Label ID="lblYear" runat="server" CssClass="label" Text="Year"></asp:Label>

                                        <asp:DropDownList ID="ddlYear" runat="server" Width="80px" CssClass="nice-select">
                                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                            </div>


                                    <br />
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnGo" runat="server" Width="50px" Height="25px" Text="View" CssClass="savebutton" OnClick="BtnGo_Click" />

                            </div>
                            </div>
                            </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>

                <div class="display-table clearfix">
                    <div class="table-responsive">
                        <asp:GridView ID="grdsample" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" EmptyDataText="No Records Found"  width="100%"> <%--OnRowDataBound="grdDr_RowDataBound"--%>
                          <Columns>
                           <asp:TemplateField HeaderText="S.No">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdsample.PageIndex * grdsample.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <%--<asp:TemplateField HeaderText="Trans_Sl_No" Visible="false">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Trans_SlNo" runat="server" Text='<%#Eval("Trans_sl_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <%--  <asp:TemplateField HeaderText="Unique_Sl_No" Visible="false">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Unique_Trans_SlNo" runat="server" Text='<%#Eval("sl_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Product code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblprdtcode" runat="server" Text='<%#Eval("Gift_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Product Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblprdtName" runat="server" Text='<%#Eval("Gift_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ERP Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblsaleerpcode" runat="server" Text='<%#Eval("Gift_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Closing Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lblClosingBal" runat="server" Text='<%#Eval("InputQty_AsOnDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="Transfer Qty">
                                <ItemTemplate>
                                     <asp:TextBox ID="txtTransferQty" runat="server" Width="100px" Text="" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              </Columns>
                             <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                            VerticalAlign="Middle"/>
                        </asp:GridView>
                        <asp:GridView ID="grdadjusment" runat="server" AutoGenerateColumns="false" CssClass="table" PagerStyle-CssClass="gridview1" GridLines="None" EmptyDataText="No Records Found" Width="100%">
                            <%--OnRowDataBound="grdDr_RowDataBound"--%>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdsample.PageIndex * grdsample.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Trans_Sl_No" Visible="false">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Trans_SlNo" runat="server" Text='<%#Eval("Trans_sl_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <%--  <asp:TemplateField HeaderText="Unique_Sl_No" Visible="false">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Unique_Trans_SlNo" runat="server" Text='<%#Eval("sl_No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Product code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprdtcode" runat="server" Text='<%#Eval("Gift_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprdtName" runat="server" Text='<%#Eval("Gift_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ERP Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsaleerpcode" runat="server" Text='<%#Eval("Gift_SName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ClosingBalance">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClosingBal" runat="server" CssClass="ClosingBal" Text='<%#Eval("InputQty_AsOnDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Set Qty">
                                    <ItemTemplate>
                                        <asp:Button ID="btn1" runat="server" Text=" - " CssClass="Minus" />
                                        <asp:TextBox ID="txtTransferQty" runat="server" Width="100px" MaxLength="5" Text="" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                        <asp:Button ID="btn2" runat="server" Text=" + " CssClass="Plus" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transfer Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltransferqty" runat="server" SkinID="lblMand"></asp:Label>
                                        <asp:HiddenField runat="server" ID="hiddtransqty" />
                                        <asp:HiddenField runat="server" ID="HiddenField1" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                                        </div>
                    </div>
        <center>
                       <br />
              <asp:Button ID="btntransfer" runat="server" Width="80px" Height="25px" Text="Transfer" CssClass="savebutton" OnClientClick="return Validate()" OnClick="BtnTransfer_Click" Visible="false" />
            </center>
    </div>
              <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
        </div>
    </form>
</body>
</html>


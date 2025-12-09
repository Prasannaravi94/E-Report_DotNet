<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductRate.aspx.cs" Inherits="MasterFiles_ProductRate" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
  <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product Rate</title>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
      <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />

    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
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
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
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
            $('#btnSubmit').click(function () {
                if ($("#txtEffFrom").val() == "") { alert("Please Enter Effective From Date."); $('#txtEffFrom').focus(); return false; }
            });

        });
    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            $("input[type='text'], select").keydown(
  function (event) {
      if ((event.keyCode == 39) || (event.keyCode == 9 && event.shiftKey == false)) {
          var inputs = $(this).parents("form").eq(0).find("input[type='text'], select");
          var idx = inputs.index(this);
          if (idx == inputs.length - 1) {
              inputs[0].select()
          } else {
              $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                  $(this).attr("style", "BACKGROUND-COLOR: white; ");
              });

              inputs[idx + 1].focus();
          }
          return false;
      }
      if ((event.keyCode == 37) || (event.keyCode == 9 && event.shiftKey == true)) {
          var inputs = $(this).parents("form").eq(0).find("input[type='text'], select");
          var idx = inputs.index(this);
          if (idx > 0) {
              $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                  $(this).attr("style", "BACKGROUND-COLOR: white; ");
              });


              inputs[idx - 1].focus();
          }
          return false;
      }
  });
            //For navigating using up and down arrow of the keyboard
            $("input[type='text'], select").keydown(
  function (event) {
      if ((event.keyCode == 40)) {
          if ($(this).parents("tr").next() != null) {
              var nextTr = $(this).parents("tr").next();
              var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
              var idx = inputs.index(this);
              nextTrinputs = nextTr.find("input[type='text'], select");
              if (nextTrinputs[idx] != null) {
                  $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                      $(this).attr("style", "BACKGROUND-COLOR: white; ");
                  });

                  nextTrinputs[idx].focus();
              }
          }
          else {
              $(this).focus();
          }
      }
      if ((event.keyCode == 38)) {
          if ($(this).parents("tr").next() != null) {
              var nextTr = $(this).parents("tr").prev();
              var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
              var idx = inputs.index(this);
              nextTrinputs = nextTr.find("input[type='text'], select");
              if (nextTrinputs[idx] != null) {
                  $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                      $(this).attr("style", "BACKGROUND-COLOR: white;");
                  });

                  nextTrinputs[idx].focus();
              }
              return false;
          }
          else {
              $(this).focus();
          }
      }
  });
        });    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />


            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Product Rate</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblState" runat="server" CssClass="label" >State Name<span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblEffFrom" runat="server" CssClass="label" >Effective From <span style="color: Red;padding-left:5px;">*</span></asp:Label>
                                <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input" onkeypress="Calendar_enter(event);" 
                                    Width="100%" TabIndex="6"></asp:TextBox>
                               <%-- <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom" runat="server" />--%>
                                 <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffFrom" CssClass= " cal_Theme1" Format="dd/MM/yyyy"/>
                            </div>
                        </div>
                        <br />
                        <div class="w-100 designation-submit-button text-center clearfix">
                           <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Text="Go" 
                        OnClick="btnGo_Click" />

                        </div>
                        <br />
                        <br />
                    </div>
    
                </div>

                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">

                                    <div id="tblRate" runat="server" width="100%" align="center">

                                    <asp:GridView ID="grdProdRate" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table"
                                        AllowSorting="True" OnSorting="grdProdRate_Sorting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="40px">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Prod_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProd_Code" runat="server" Text='<%#   Bind("Product_Detail_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="240px" HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProdName" CssClass="input" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProdName" runat="server" Text='<%# Bind("Product_Detail_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ERP Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbERP" runat="server" Text='<%#Bind("Sale_Erp_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sale Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSaleUnit" runat="server" Text='<%#Bind("Product_Sale_Unit")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        
                                            <asp:TemplateField HeaderText="MRP">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMRP" Width="80px" Style="text-align: right;" MaxLength="8"
                                                        CssClass="input" Text='<%#(Eval("MRP_Price"))%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Retailer Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRP" Width="80px" Style="text-align: right;" MaxLength="8"
                                                        CssClass="input" Text='<%#(Eval("Retailor_Price"))%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Distributor Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDP" Width="80px" Style="text-align: right;" MaxLength="8"
                                                        CssClass="input" Text='<%#(Eval("Distributor_Price"))%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="NRV Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtNSR" Width="80px" Style="text-align: right;" MaxLength="8"
                                                        CssClass="input" Text='<%#(Eval("NSR_Price"))%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Target Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTarg" Width="80px" Style="text-align: right;" MaxLength="8"
                                                        CssClass="input" Text='<%#(Eval("Target_Price"))%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sample Price">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSamp" Width="80px" Style="text-align: right;" MaxLength="8"
                                                        CssClass="input" Text='<%#(Eval("Sample_Price"))%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>


                            </div>
                        </div>

                    </div>

                </div>
                <br />
                <div class="row justify-content-center">
                      <asp:Button ID="btnSubmit" CssClass="savebutton" runat="server" Text="Save" Visible="false" 
                                OnClick="btnSubmit_Click" />

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

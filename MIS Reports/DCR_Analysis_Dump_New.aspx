<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Analysis_Dump_New.aspx.cs" Inherits="MIS_Reports_DCR_Analysis_Dump_New" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR Analysis Dump </title>
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
       <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#btnSubmit').click(function () {
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
    <%-- <ucl:Menu ID="menu1" runat="server" /> --%>
         <div id="Divid" runat="server">
            </div>
        <br />
        <center>
<div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
         <h2 class="text-center">DCR Analysis - Dump</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                   <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    Visible="false" ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                    <%--     <asp:ListItem Selected="True">---Select---</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>

                        <div class="single-des clearfix">
                                            <asp:Label ID="lblfrom" runat="server" CssClass="label" Text="From Date"></asp:Label><br />
                                            <asp:TextBox ID="txtFromdte" runat="server" CssClass="input" onkeypress="Calendar_enter(event);" Width="100%"></asp:TextBox>
                                           
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFromdte" CssClass=" cal_Theme1" />
                                        </div>

                                        <div class="single-des clearfix">
                                            <asp:Label ID="lblto" runat="server" CssClass="label" Text="To Date"></asp:Label><br />
                                            <asp:TextBox ID="txtTodte" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                           
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTodte" CssClass=" cal_Theme1" />
                                        </div>
                        </div>
               
             </div>
                            </div>
                        </div>
                    </div>
    </div>

               <%--  <tr>
                      <td align="left" class="stylespc">
                         <asp:Label ID="lblfrom" runat="server" SkinID="lblMand" Text="From Date"  ></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                       <asp:TextBox ID="txtFromdte" runat="server"  Height="22px" MaxLength="10" 
                        CssClass="DOBDate" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                         onkeypress="CheckNumeric(event);" SkinID="TxtBxNumOnly"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc"> 
                        <asp:Label ID="lblto" runat="server" SkinID="lblMand" Text="To Date"  ></asp:Label>
                    </td>
                   <td align="left" class="stylespc">
                        <asp:TextBox ID="txtTodte" runat="server" SkinID="TxtBxNumOnly" Height="22px" MaxLength="10" 
                         CssClass="DOBDate" onfocus="this.style.backgroundColor='#E0EE9D'" onblur="this.style.backgroundColor='White'"
                        onkeypress="CheckNumeric(event);" ></asp:TextBox>
                       
                    </td>
                </tr>--%>
          
         
          </center>

           <br />
            <center>
               
                <asp:LinkButton ID="btnSubmit" runat="server" Font-Size="Medium" Font-Bold="true"
                    Text="Download Excel" OnClick="btnSubmit_Click" />
            </center>
       
          <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </div>
    </form>
</body>
</html>

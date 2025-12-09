<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BulkEditDCRStDt.aspx.cs" EnableEventValidation="false"
    Inherits="MasterFiles_BulkEditDCRStDt" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit DCR Start Date</title>
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
   
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
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFilter]');
            var $items = $('select[id$=ddlFilter] option');

            $txt.keyup(function () {
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
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });
    </script>

    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

    <style type="text/css">
          .display-table .table tr td:first-child
         {
             color:#636d73;

         }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Bulk Edit DCR Start Date</h2>

                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row clearfix">
                                    <div class="col-lg-4">
                                        <div class="single-des clearfix">
                                            <asp:Label ID="lblTPDCRStartDate" runat="server" Text="Starting Date" CssClass="label"></asp:Label>
                                            <%--     <asp:TextBox ID="txtTPDCRStartDate" runat="server" TabIndex="1" ReadOnly="true" SkinID="MandTxtBox"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTPDCRStartDate" />--%>
                                            <asp:TextBox ID="txtTPDCRStartDate" onkeypress="Calendar_enter(event);" TabIndex="1" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" CssClass="cal_Theme1"
                                                TargetControlID="txtTPDCRStartDate" />
                                        </div>
                                    </div>

                                    <div class="col-lg-2" style="padding-top: 19px; padding-left: 0px">

                                        <asp:Button ID="btnGo" runat="server" TabIndex="2" CssClass="savebutton" Text="Set Starting Date" Width="120px"
                                            OnClick="btnGo_Click" />
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblFilter" runat="server" Text="Filter By Manager" CssClass="label"></asp:Label>
                                        <%-- <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                            ToolTip="Enter Text Here"></asp:TextBox>--%>
                                        <%--<asp:LinkButton ID="linkcheck" runat="server" 
                                               onclick="linkcheck_Click">
                                               <img src="../Images/Selective_Mgr.png" />
                                                 </asp:LinkButton>--%>
                                        <%--  <div id="testImg">
                                            <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;" runat="server" /><span
                                                style="font-family: Verdana; color: Red; font-weight: bold;">Loading Please Wait...</span>
                                        </div>--%>
                                        <asp:DropDownList ID="ddlFilter" TabIndex="3" runat="server" CssClass="custom-select2 nice-select">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 17px; padding-left: 0px">
                                        <asp:Button ID="btnsrch" TabIndex="4" runat="server" Width="50px" Text="Go" OnClick="btnsrch_Click"
                                            CssClass="savebutton" />

                                    </div>
                                </div>

                            </div>
                            <div class="row justify-content-center ">
                                <asp:Label ID="lblSelect" Text="Select the Manager" runat="server"
                                    ForeColor="Red" Font-Size="Large" Visible="false"></asp:Label>
                            </div>

                            <br />
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">

                                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        Visible="false" OnRowDataBound="grdSalesForce_RowDataBound" AutoGenerateColumns="false"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="sans-serif" ForeColor="#483d8b" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDes" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last DCR Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtLastDCRStDt" runat="server" Text='<%# Bind("Last_DCR_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Existing DCR Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtExtDCRStDt" runat="server" Text='<%# Bind("Last_DCR_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New DCR Date">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDCRStDt" runat="server" CssClass="input" onkeypress="Calendar_enter(event);"></asp:TextBox>
                                                    <%--       <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                                    TargetControlID="txtDCRStDt" />--%>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDCRStDt" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                                    </asp:CalendarExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--     <asp:TemplateField HeaderText="Color" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBackColor" runat="server"  Text='<%# Bind("Des_Color") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <br />
                            <div class="row justify-content-center">
                                <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="savebutton" Visible="false" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="div_fixed">
                                <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Text="Update" Visible="false" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>

                </div>

                <asp:Button ID="btnback" runat="server" CssClass="backbutton" Text="Back" OnClick="btnback_Click" />

            </div>
            <br />
            <br />

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

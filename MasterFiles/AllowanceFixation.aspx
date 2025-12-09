<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllowanceFixation.aspx.cs"
    Inherits="MasterFiles_AllowanceFixation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Allowance Fixation</title>
    <%-- <link type="text/css" rel="Stylesheet" href="../css/style.css" />--%>
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
    <script type="text/javascript" language="javascript">

        $(window).on("load", function () {

            //$("#block").show();
            //$("#access").hide();
           

        });

        $(document).ready(function () {
          
            //$("#block").show();
            //$("#access").hide();
         
            $("#testImg").hide();
            $(".custom-select2").select2();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500); linkcheck_Click
            })
        });

        function Validate_Auth() {
            var Auth = "til@san";
            if ($("#password").val() == Auth) {
                $("#block").hide();             
                $("#access").show();
               
            } else {
                $("#block").show();
                $("#access").hide();
               
                if (Auth == "") { alert("Enter Password"); } else { alert("Enter Correct Password") }

            }

        }

        //function btngo1() {
        //    $("#block").hide();
        //    $("#access").show();
        //}
        //function btndes() {
        //    $("#block").hide();
        //    $("#access").show();
        //}
        //function btnsave() {
        //    $("#block").hide();
        //    $("#access").show();
        //}

    </script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
  
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body style="overflow-x:scroll">
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
    <script language="javascript">
        $(function () {
            $("#grdWTAllowance").find("th,td").show();
            //          var colindex = $("#txtProBraName").val();
            //$("#grdWTAllowance").find("th:nth-child(" + 9 + "), td:nth-child(" + 9 + ")").hide();
            //$("#grdWTAllowance").find("th:nth-child(" + 10 + "), td:nth-child(" + 10 + ")").hide();
            $("#grdWTAllowance").find("th:nth-child(" + 12 + "), td:nth-child(" + 12 + ")").hide();
            $("#grdWTAllowance").find("th:nth-child(" + 13 + "), td:nth-child(" + 13 + ")").hide();

            //$("#grdWTAllowance").find("th:nth-child(" + 13 + "), td:nth-child(" + 13 + ")").hide();

            $("input#txtossmdes").hide();
            $('#Label117').hide();

            $("input#txtosnmdes").hide();
            $('#Label118').hide();

            //$("input#txtspecosdes").hide();
            //$('#Label9').hide();

            //$("input#txtspecmeetdes").hide();
            //$('#Label10').hide();

            //$("input#txtspectrandes").hide();
            //$('#Label11').hide();
        });

    </script>

    <script type="text/javascript">
        $(function () {
            $("[id*=chkCountry]").click(function () {
                if ($(this).is(":checked") == true) {
                    $("#grdWTAllowance").find("th,td").show();
                    //                    var colindex = $("#txtProBraName").val();
                    $("#grdWTAllowance").find("th:nth-child(" + 12 + "), td:nth-child(" + 12 + ")").show();
                    $("#grdWTAllowance").find("th:nth-child(" + 13 + "), td:nth-child(" + 13 + ")").show();




                    //$("input#txtspehqdes").show();
                    //$('#Label7').show();

                    //$("input#txtspecexdes").show();
                    //$('#Label8').show();

                    $("input#txtossmdes").show();
                    $('#Label117').show();

                    $("input#txtosnmdes").show();
                    $('#Label118').show();

                }
                else if ($(this).is(":checked") == false) {
                    $("#grdWTAllowance").find("th,td").show();
                    //                    var colindex = $("#txtProBraName").val();

                    $("#grdWTAllowance").find("th:nth-child(" + 12 + "), td:nth-child(" + 12 + ")").hide();
                    $("#grdWTAllowance").find("th:nth-child(" + 13 + "), td:nth-child(" + 13 + ")").hide();


                    $("input#txtossmdes").hide();
                    $('#Label117').hide();

                    $("input#txtosnmdes").hide();
                    $('#Label118').hide();

                    //$("input#txtspehqdes").hide();
                    //$('#Label7').hide();

                    //$("input#txtspecexdes").hide();
                    //$('#Label8').hide();


                }
            });
        });
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
    <style>
        .display-reportMaintable .table th {
            padding: 0px 0px;
        }
        #grdWTAllowance > tbody > tr:nth-child(1) > th:nth-child(1)
        {
                position: sticky !important;
                left: 0px !important;
                top: 0px !important;
                z-index: 2 !important;
        }
          #grdWTAllowance > tbody > tr:nth-child(1) > th:nth-child(2)
        {
                position: sticky !important;
                left: 40px !important;
                top: 0px !important;
                z-index: 2 !important;
                min-width:153px;
        }
            #grdWTAllowance > tbody > tr:nth-child(1) > th:nth-child(3)
        {
                position: sticky !important;
                left: 269px !important;
                top: 0px !important;
                z-index: 2 !important;
                min-width:153px;
        }
                 #grdWTAllowance > tbody > tr:nth-child(1) > th:nth-child(4)
        {
                position: sticky !important;
                left: 421px !important;
                top: 0px !important;
                z-index: 2 !important;
                min-width:153px;
        }
        #grdWTAllowance > tbody > tr:nth-child(1) > th:nth-child(5) {
            position: sticky !important;
            left: 573px !important;
            top: 0px !important;
            z-index: 2 !important;
            min-width: 100px;
        }
            #grdWTAllowance > tbody > tr:nth-child(1) > th:nth-child(n+6)
                {
            position: sticky !important;
            /*left: 573px !important;*/
            top: 0px !important;
            z-index: 1 !important;
        
        }

        #grdWTAllowance > tbody > tr:nth-child(n+1) > td:nth-child(1) {
            position: sticky;
            left: 0px;
            z-index: 1;
        }
        #grdWTAllowance > tbody > tr:nth-child(n+1) > td:nth-child(2) {
            position: sticky;
            left: 40px;
            z-index: 1;
            min-width:153px;
            background-color: white;
        }
        #grdWTAllowance > tbody > tr:nth-child(n+1) > td:nth-child(3) {
            position: sticky;
            left: 269px;
            z-index: 1;
            min-width:153px;
            background-color: white;
        }
        #grdWTAllowance > tbody > tr:nth-child(n+1) > td:nth-child(4) {
            position: sticky;
            left: 421px;
            z-index: 1;
            min-width:153px;
            background-color: white;
        }
         #grdWTAllowance > tbody > tr:nth-child(n+1) > td:nth-child(5) {
            position: sticky;
            left: 573px;
            z-index: 1;
            background-color: white;
            min-width: 100px;
        }
    </style>
    <form id="form1" runat="server">
              
        <div>
            <ucl:Menu ID="menu1" runat="server" />
        </div>
        <div>
              <div id="block" style="display:none">

                <h3 class="text-center">Authorized Personals Only</h3>
            <br />  <br /> 
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="designation-area clearfix">-
                            <div class="single-des clearfix">
                                
                                <div class="row clearfix">
                                    <div class="col-lg-5"></div>
                                    <div class="col-lg-2">
                                        <asp:textbox ID="password" TextMode="Password" placeholder="Enter Password" runat="server" Visible="True" Width="100%" ></asp:textbox>
                                           
                                    
                                    </div>

                                    <div id="gobutton" class="col-lg-4" style="margin-left: 20px;">
                                        <asp:Button ID="Button1"  runat="server" Width="65px" Text="Go" CssClass="savebutton" Visible="true" OnClientClick="Validate_Auth(); return false;" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>


        </div>
            <div id="access"  class="container home-section-main-body position-relative clearfix" style="max-width:1350px;">
                <div class="row justify-content-center ">
                    <div class="col-lg-12">
                        <h2 class="text-center">Allowance Fixation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblRegionWise" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>
                                        <%--    <asp:TextBox ID="txtNew" runat="server"  CssClass="input" Width="100%"
                                            ToolTip="Enter Text Here"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlRegion" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="false"
                                            OnSelectedIndexChanged="ddlRegion_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button Visible="true" ID="btnGo" runat="server" Width="50px" Text="Go" CssClass="savebutton"
                                          OnClientClick="btngo1()"  OnClick="btnSubmit_Click" />
                                    </div>
                                    <div class="col-lg-4">
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="label"></asp:Label>
                                        <div class="single-des clearfix">
                                            <asp:TextBox ID="txtTPStartDate" TabIndex="1" runat="server" CssClass="input" Width="100%"
                                                OnTextChanged="txtTPStartDate_TextChanged" AutoPostBack="true">
                                            </asp:TextBox>
                                        </div>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MM-yyyy" CssClass="cal_Theme1"
                                            TargetControlID="txtTPStartDate" />
                                    </div>
                                    <asp:CheckBox ID="chkCountry" Text="Special Allowance (If Required)" runat="server" />
                                </div>
                            </div>
                            <center>
                                <asp:Label ID="lblSelect" Style="white-space: pre" Text="Select the Fieldforce" ForeColor="Red" Font-Size="Large"
                                    runat="server"></asp:Label></center>
                            <br />


                            <asp:Panel ID="pnldes" runat="server" Visible="true">
                                <div class="single-des">
                                    <div class="row">
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="Design"></asp:Label>
                                            <asp:DropDownList ID="ddldesig" runat="server" CssClass="nice-select" AutoPostBack="false">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label7" runat="server" CssClass="label" Text="Category"></asp:Label>
                                            <asp:DropDownList ID="dd2cat" runat="server" CssClass="nice-select" AutoPostBack="false">
                                                <asp:ListItem Text="Mega-Metro" Value="Mega-Metro"></asp:ListItem>
                                                <asp:ListItem Text="Metro" Value="Metro"></asp:ListItem>
                                                <asp:ListItem Text="Non-Metro" Value="Non-Metro"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label8" runat="server" CssClass="label" Text="Status"></asp:Label>
                                            <asp:DropDownList ID="dd3sts" runat="server" CssClass="nice-select" AutoPostBack="false">
                                                <asp:ListItem Text="trainee" Value="trainee"></asp:ListItem>
                                                <asp:ListItem Text="Probation" Value="Probation"></asp:ListItem>
                                                <asp:ListItem Text="Confirmed" Value="Confirmed"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label2" runat="server" CssClass="label" Text="HQ"></asp:Label>
                                            <asp:TextBox ID="txthqdes" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label3" runat="server" CssClass="label" Text="EX"></asp:Label>
                                            <asp:TextBox ID="txtEXdes" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label4" runat="server" CssClass="label" Text="OS Metro"></asp:Label>
                                            <asp:TextBox ID="txtosdes" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:5px">
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label5" runat="server" CssClass="label" Text="Hill"></asp:Label>
                                            <asp:TextBox ID="txthilldes" runat="server"  Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label117" runat="server" CssClass="label" Text="OS SemiMetro"></asp:Label>
                                            <asp:TextBox ID="txtossmdes" runat="server"  Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label118" runat="server" CssClass="label" Text="OS nonMetro"></asp:Label>
                                            <asp:TextBox ID="txtosnmdes" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label6" runat="server" CssClass="label" Text="Fare/KM"></asp:Label>
                                            <asp:TextBox ID="txtfaredes" runat="server"  Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Lbllmt" runat="server" CssClass="label" Text="OS With bill Limit"></asp:Label>
                                            <asp:TextBox ID="txtlmt" runat="server"  Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Lbllclcon" runat="server" CssClass="label" Text="Local Conveyance"></asp:Label>
                                            <asp:TextBox ID="txtlclcon" runat="server"  Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label12" runat="server" CssClass="label" Text="Range I Above Kms"></asp:Label>
                                            <asp:TextBox ID="txtrange1des" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label13" runat="server" CssClass="label" Text="Range II Above Kms"></asp:Label>
                                            <asp:TextBox ID="txtrange2des" runat="server"  Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:5px">
                                        <div class="col-lg-2">
                                            <asp:Label ID="Labelrg3" runat="server" CssClass="label" Text="Range III Above Kms"></asp:Label>
                                            <asp:TextBox ID="txtrange3des" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label14" runat="server" CssClass="label" Visible="false" Text="one"></asp:Label>
                                            <asp:TextBox ID="TextBoxx1" runat="server"  Visible="false" Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label15" runat="server" CssClass="label" Visible="false" Text="two"></asp:Label>
                                            <asp:TextBox ID="TextBoxx2" runat="server"  Visible="false" Width="100%" CssClass="input"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label16" runat="server" CssClass="label" Text="three" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TextBoxx3" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label17" runat="server" CssClass="label" Text="four" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TextBoxx4" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label18" runat="server" CssClass="label" Text="five" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TextBoxx5" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-top:5px">
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label19" runat="server" CssClass="label" Text="six" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TextBoxx6" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label120" runat="server" CssClass="label" Text="seven" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TextBoxx7" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label21" runat="server" CssClass="label" Text="seven" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TextBoxx8" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label22" runat="server" CssClass="label" Text="seven" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TextBoxx9" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Label ID="Label23" runat="server" CssClass="label" Text="seven" Visible="false"></asp:Label>
                                            <asp:TextBox ID="TextBoxx10" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Button Visible="true" ID="btndes" runat="server" Width="60px"  Text="Go"
                                                CssClass="savebutton" OnClientClick="btndes()" OnClick="btndes_Click" />
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <br />

                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow:inherit;">
                                    <asp:GridView ID="grdWTAllowance" Width="100%" runat="server" AutoGenerateColumns="false"
                                        BorderStyle="Solid" CssClass="table" AlternatingRowStyle-CssClass="alt" style="background-color:white"
                                        GridLines="None" OnRowDataBound="grdWTAllowance_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="4%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFieldForceName" Width="210px" Text='<%# Eval("sf_Name")%>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Employee Code" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpcode" Text='<%# Eval("sf_emp_id")%>'
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
                                            <asp:TemplateField HeaderText="Design" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" Text='<%# Eval("Designation_Short_Name")%>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCat" Text='<%# Eval("sf_desgn")%>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSts" Text='<%# Eval("Fieldforce_Type")%>'
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_hq" Width="100px" Text='<%# Eval("sf_hq")%>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ_Exp" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtHq" Style="text-align: center;" Width="60px" CssClass="input" Text='<%# Eval("HQ_Allowance")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EX-HQ" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEXHQ" Style="text-align: center;" Width="60px" CssClass="input" Text='<%# Eval("EX_HQ_Allowance")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OS Metro" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOS" Style="text-align: center;" Width="60px" CssClass="input" Text='<%# Eval("OS_Allowance")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hill" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtHill" Style="text-align: center;" Width="60px" CssClass="input" Text='<%# Eval("Hill_Allowance")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OS Semi Metro" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtMetro" Style="text-align: center;" Width="50px" CssClass="input" Text='<%# Eval("OS_SM")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OS Non-Metro" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtnonMetro" Style="text-align: center;" Width="50px" CssClass="input" Text='<%# Eval("OS_NM")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fare/KM" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFareKm" Style="text-align: center;" Width="60px" CssClass="input" Text='<%# Eval("FareKm_Allowance")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OS With Bill Limit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtosbill" Style="text-align: center;" Width="60px" CssClass="input" Text='<%# Eval("os_with_lmt")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Local Conveyance" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtlocal" Style="text-align: center;" Width="60px" CssClass="input" Text='<%# Eval("loal_convey")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range of Fare" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <table id="tblData" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="center" width="3%">Range I Above Kms.&nbsp;
                                        <asp:TextBox ID="txtRange" Style="text-align: center;" Width="50px" Text='60' CssClass="input" runat="server"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtRangeofFare1" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Range_of_Fare1")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range of Fare" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <table id="tblData2" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="center">Range II Above Kms.&nbsp;
                                        <asp:TextBox ID="txtRange2" Style="text-align: center;" Width="50px" CssClass="input" Text='60' runat="server"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtRangeofFare2" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Range_of_Fare2")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Range of Fare" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <table id="tblData3" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td align="center">Range III Above Kms.&nbsp;
                                        <asp:TextBox ID="txtRange3" Style="text-align: center;" Width="50px" CssClass="input" Text='60' runat="server"></asp:TextBox>
                                                                <asp:RadioButtonList ID="rbtLstRating3" runat="server"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Table">
                                                                    <asp:ListItem Text="Consolidated" Selected="True" Value="Consolidated"></asp:ListItem>
                                                                    <asp:ListItem Text="Separate" Value="Separate"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRangeofFare3" Style="text-align: center;" CssClass="input" Width="70px" Text='<%# Eval("Range_of_Fare3")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>






                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox13" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column1")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox14" Style="text-align: center; font-family: Calibr" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column2")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox15" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column3")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox16" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column4")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox17" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column5")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox18" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column6")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox19" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column7")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox20" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column8")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox21" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column9")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Range of Fare" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox22" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Fixed_Column10")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column1" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox23" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Wtype_Fixed_Column1")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column2" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox24" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Wtype_Fixed_Column2")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column3" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox25" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Wtype_Fixed_Column3")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column4" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox26" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Wtype_Fixed_Column4")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Wtype_Fixed_Column5" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBox27" Style="text-align: center;" Width="70px" CssClass="input" Text='<%# Eval("Wtype_Fixed_Column5")%>' runat="server"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Effective From" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtEffective" Style="text-align: center;" Width="90px" CssClass="input" runat="server" Text='<%# Eval("Effective_Form")%>' ReadOnly="false" MaxLength="10"></asp:TextBox>
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
                                </div>
                            </div>
                            <br />
                            <center>
                                <asp:Button ID="btnSave" CssClass="savebutton"
                                    runat="server"  Visible="false"
                                    Text="Save" OnClientClick="btnsave()" OnClick="btnSave_Click" />
                            </center>

                        </div>
                    </div>
                </div>
            </div>
            <br />
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

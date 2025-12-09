<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserList_NewWindow.aspx.cs" Inherits="MasterFiles_UserList_NewWindow" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">


    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>User List</title>
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" type="image/png" href="../assets/images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <link rel="stylesheet" href="../../assets/css/responsive.css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->

    <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />


    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--  <link type="text/css" rel="stylesheet" href="../css/AdminMenuStyle.css" />--%>
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

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
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
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
            $('#btnGo').click(function () {

                var divi = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Division Name."); $('#ddlDivision').focus(); return false; }
                var Field = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var Field = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field == "") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var State = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (State == "---Select State---") { alert("Select State Name."); $('#ddlFieldForce').focus(); return false; }


            });
        });
    </script>

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
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>

</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">User List</h2>

                        <%--<div class="designation-reactivation-table-area clearfix">--%>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" CssClass="nice-select" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblFilter" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>

                                    <div class="row">
                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="nice-select" Visible="false">
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

                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select"
                                        OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                    </asp:DropDownList>&nbsp;
                                <asp:CheckBox ID="chkVacant" Text=" Without - Vacant" Font-Size="Medium"
                                    Checked="true" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click"
                                CssClass="savebutton" />
                        </div>

                    </div>
                </div>

                <div class="row ">
                    <div class="col-lg-12">

                        <asp:Panel ID="pnlprint" runat="server" CssClass="panelmarright" Visible="false">
                            <%--  <input type="button" id="btnPrint" value="Print" style="width:60px;height:25px; background-color:LightBlue; "    />--%>
                            <asp:LinkButton ID="lnkPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData()">
                                <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print" Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>&nbsp&nbsp
                            <asp:LinkButton ID="btnExcel" ToolTip="Pdf" runat="server" OnClientClick="RefreshParent();">
                                <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>&nbsp&nbsp
                            <asp:LinkButton ID="imgpdf" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click">
                                <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/pdf.png" ToolTip="Pdf" Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>
                        </asp:Panel>
                    </div>
                </div>

                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlContents" runat="server">
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="overflow:inherit; scrollbar-width: thin;">
                                    <asp:GridView ID="grdSalesForce" runat="server" style="background-color:white"
                                        AutoGenerateColumns="false" OnRowDataBound="grdSalesForce_RowDataBound"
                                        GridLines="None" CssClass="table">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("Sf_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Drs Count" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrsCnt" runat="server" Font-Bold="true" Font-Names="sans-serif" ForeColor="Red" Text='<%# Bind("Lst_drCount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp Id" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_emp_id" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblPlace" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="State Name" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblFieldForce" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Design" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reporting" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" Visible="false" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblUserName" runat="server" Style="font-weight: 700" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Password" Visible="false" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblPassword" runat="server" Style="font-weight: 700" Text='<%# Bind("sf_password") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="DOJ" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDOJ" runat="server" Style="font-weight: 700" Text='<%# Bind("Sf_Joining_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email Id" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmail" runat="server" Style="font-weight: 700" Text='<%# Bind("SF_Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblContact" runat="server" Style="font-weight: 700" Text='<%# Bind("SF_Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server" Style="font-weight: 700" Text='<%# Bind("SF_Per_ContactAdd_One") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank Name"  ItemStyle-HorizontalAlign="Left">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbankname" runat="server"  Style="font-weight: 700" Text='<%# Bind("SF_ContactAdd_One") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bank A/C No"  ItemStyle-HorizontalAlign="Left">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblacc_no" runat="server"  Style="font-weight: 700" Text='<%# Bind("SF_ContactAdd_Two") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IFSC Code"  ItemStyle-HorizontalAlign="Left">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblifsc" runat="server"  Style="font-weight: 700" Text='<%# Bind("SF_City_Pincode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch Name"  ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle Width="120px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblbranchname" runat="server"  Style="font-weight: 700" Text='<%# Bind("SF_Per_ContactAdd_Two") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--  <asp:TemplateField HeaderText="Last DCR Date"  ItemStyle-HorizontalAlign="Left">
                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDCRDate" runat="server" Style="font-weight: 700" Text='<%# Bind("Last_DCR_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>


                                            <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left">

                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblS" runat="server" Text='<%# Bind("sf_Tp_Active_flag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBackColor" runat="server" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSFType" runat="server" Text='<%# Bind("sf_type") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                        <div class="no-result-area" id="div1" runat="server" visible="false">
                            No Records Found
                        </div>

                    </div>
                </div>

            </div>





            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

    </form>
</body>
</html>

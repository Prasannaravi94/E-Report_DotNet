<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Other_Setup.aspx.cs" Inherits="MasterFiles_Options_Other_Setup" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Other Setup</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link href="../../JScript/Bootstrap/dist/css/bootstrap.css" />
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<style type="text/css">
        .spc
        {
            padding-left: 5%;
        }
        .spc1
        {
            padding-left: 10%;
        }
        
        .box
        {
            background: #FFFFFF;
            border: 4px solid #7E8D29;
            border-radius: 8px;
        }
        .box1
        {
            background: #FFFFFF;
            border: 2px solid #5f9ea0;
            border-radius: 8px;
        }
        
        .tableHead
        {
            background: #FFFFFF;
            color: black;
            border-style: solid;
            border-width: 1px;
            border-color: #a2cd5a;
        }
        .break
        {
            height: 4px;
        }
    </style>--%>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($('#rdotarfix :radio:checked').length > 0) {
                    return true;
                }
                else {
                    createCustomAlert('Select Target Fixation Based On')
                    return false;
                }
            })
        })

    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($('#rdotarcalen :radio:checked').length > 0) {
                    return true;
                }
                else {
                    createCustomAlert('Select Target Calender Based On')
                    return false;
                }
            })
        })

    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($('#rdodrbus :radio:checked').length > 0) {
                    return true;
                }
                else {
                    createCustomAlert('Select Dr Business Entry Calculation Based On')
                    return false;
                }
            })
        })

    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $('#btnSubmit').click(function () {
                if ($("#ddlresig").val() == 0) {
                    return true;
                }
                else {
                    if ($("#txtresig").val().length > 0) {
                    }
                    else {
                        alert('Enter Caption')
                        $('#txtresig').focus();
                        return false;
                    }
                }
            })
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <br />
                        <h2 id="hHeading" class="text-center" runat="server" style="border-bottom: none !important;"></h2>
                        <div class="row justify-content-center">
                            <div class="col-lg-6">
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Target Fixation</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label2" runat="server" CssClass="label">Target Fixation Based On</asp:Label>
                                                <asp:RadioButtonList ID="rdotarfix" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Value="1">Financial Year</asp:ListItem>
                                                    <asp:ListItem Value="2">FY - Half Yearly</asp:ListItem>
                                                    <asp:ListItem Value="3">FY - Quarterly</asp:ListItem>
                                                    <asp:ListItem Value="4">Calender Year</asp:ListItem>
                                                    <asp:ListItem Value="5">CY - Half Yearly</asp:ListItem>
                                                    <asp:ListItem Value="6">CY - Quarterly</asp:ListItem>
                                                    <asp:ListItem Value="7">Monthly</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label1" runat="server" CssClass="label">Target Calender Based On</asp:Label>
                                                <asp:RadioButtonList ID="rdotarcalen" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Value="1">MRP Price</asp:ListItem>
                                                    <asp:ListItem Value="2">Target Price</asp:ListItem>
                                                    <asp:ListItem Value="3">Retailor Price</asp:ListItem>
                                                    <asp:ListItem Value="4">NSR Price</asp:ListItem>
                                                    <asp:ListItem Value="5">Distributor Price</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">SS Entry Setup</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label3" runat="server" CssClass="label">SS Entry Mandatory</asp:Label>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lbldaterange" runat="server" CssClass="label">Date Range</asp:Label>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblfrom" runat="server" CssClass="label">From</asp:Label>
                                                <asp:DropDownList ID="ddlStart_date" runat="server">
                                                    <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblto" runat="server" CssClass="label">To</asp:Label>
                                                <asp:DropDownList ID="ddlEnd_date" runat="server">
                                                    <asp:ListItem Value="0">---Select---</asp:ListItem>
                                                    <asp:ListItem Value="1">1</asp:ListItem>
                                                    <asp:ListItem Value="2">2</asp:ListItem>
                                                    <asp:ListItem Value="3">3</asp:ListItem>
                                                    <asp:ListItem Value="4">4</asp:ListItem>
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Value="6">6</asp:ListItem>
                                                    <asp:ListItem Value="7">7</asp:ListItem>
                                                    <asp:ListItem Value="8">8</asp:ListItem>
                                                    <asp:ListItem Value="9">9</asp:ListItem>
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="11">11</asp:ListItem>
                                                    <asp:ListItem Value="12">12</asp:ListItem>
                                                    <asp:ListItem Value="13">13</asp:ListItem>
                                                    <asp:ListItem Value="14">14</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label13" runat="server" CssClass="label">Line Manager Locking System Needed ( Not Submitted SS Entry for Subordinates )</asp:Label>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:RadioButtonList ID="rdossLock" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label14" runat="server" CssClass="label">Locking Day</asp:Label>


                                                <asp:DropDownList ID="ddlLockDay" runat="server">
                                                    <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                                    <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                                    <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                                    <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Hospital Business Entry</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblHos" runat="server" CssClass="label">Calculation Based On</asp:Label>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:RadioButtonList ID="rdoHosbus" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Value="M">MRP Price</asp:ListItem>
                                                    <asp:ListItem Value="T">Target Price</asp:ListItem>
                                                    <asp:ListItem Value="R">Retailor Price</asp:ListItem>
                                                    <asp:ListItem Value="N">Rate - Enterable</asp:ListItem>
                                                    <asp:ListItem Value="D">Distributor Price</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Chemist Business Entry</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label4" runat="server" CssClass="label">Calculation Based On</asp:Label>
                                                <asp:RadioButtonList ID="rdo_ListDRbus" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Value="M">MRP Price</asp:ListItem>
                                                    <asp:ListItem Value="T">Target Price</asp:ListItem>
                                                    <asp:ListItem Value="R">Retailor Price</asp:ListItem>
                                                    <asp:ListItem Value="N">NSR Price</asp:ListItem>
                                                    <asp:ListItem Value="D">Distributor Price</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Leave Entitlement</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label5" runat="server" CssClass="label">Leave Entitlement for MR Needed</asp:Label>
                                                <asp:RadioButtonList ID="rdoentitlemr" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label6" runat="server" CssClass="label">Leave Entitlement for Manager Needed</asp:Label>
                                                <asp:RadioButtonList ID="rdoentitlemgr" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>

                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Doctor Business Entry</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label7" runat="server" CssClass="label">Calculation Based On</asp:Label>
                                                <asp:RadioButtonList ID="rdodrbus" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Value="M">MRP Price</asp:ListItem>
                                                    <asp:ListItem Value="T">Target Price</asp:ListItem>
                                                    <asp:ListItem Value="R">Retailor Price</asp:ListItem>
                                                    <asp:ListItem Value="N">NSR Price</asp:ListItem>
                                                    <asp:ListItem Value="D">Distributor Price</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">CRM</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label8" runat="server" CssClass="label">Who will raise the CRM</asp:Label>
                                                <asp:RadioButtonList ID="rbtCRMMgr" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Value="N">Base Level</asp:ListItem>
                                                    <asp:ListItem Value="M">Manager</asp:ListItem>
                                                    <asp:ListItem Value="Y">Base Level/Manager</asp:ListItem>
                                                    <asp:ListItem Value="A">Admin</asp:ListItem>
                                                    <%-- <asp:ListItem Value="AB">Admin/Business Entry</asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label9" runat="server" CssClass="label">Approval for CRM</asp:Label>

                                                <asp:RadioButtonList ID="rbtCRMAprl" runat="server" RepeatDirection="Vertical">
                                                    <asp:ListItem Value="LM">Line Manager Only</asp:ListItem>
                                                    <asp:ListItem Value="All">All Manager</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">Leave Setup</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label10" runat="server" CssClass="label">No.of Days Leave Allowed</asp:Label>
                                                <asp:TextBox ID="txtleave" runat="server" MaxLength="3" onkeypress="CheckNumeric(event);"
                                                    CssClass="input" Width="100%"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label11" runat="server" CssClass="label">Delayed Status Caption show in the DCR view as</asp:Label>
                                                <asp:TextBox ID="txtdelay" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:TextBox ID="txtshortname" runat="server" Width="100%" MaxLength="3" CssClass="input"></asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label12" runat="server" CssClass="label">Whenever the Fieldforce is Resigned, Additionaly include the Caption in the Fieldforce Name:</asp:Label>

                                                <asp:DropDownList ID="ddlresig" runat="server">
                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                    <asp:ListItem Value="1">Front Side of the Name</asp:ListItem>
                                                    <asp:ListItem Value="2">Back Side of the Name</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:TextBox ID="txtresig" MaxLength="12" CssClass="input" Width="100%" runat="server">                          
                                                </asp:TextBox>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label15" runat="server" CssClass="label">Sample Inventory Effective Month/Year</asp:Label>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblMonth" runat="server" CssClass="label">Month</asp:Label>
                                                <asp:DropDownList ID="ddl_Sample_Month" runat="server">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblYear" runat="server" CssClass="label">Year</asp:Label>
                                                <asp:DropDownList ID="ddl_Sample_Year" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label16" runat="server" CssClass="label">Input Inventory Effective Month/Year</asp:Label>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblInputM" runat="server" CssClass="label">Month</asp:Label>
                                                <asp:DropDownList ID="ddl_Input_Month" runat="server">
                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
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
                                            <div class="single-des clearfix">
                                                <asp:Label ID="lblInputy" runat="server" CssClass="label">Year</asp:Label>
                                                <asp:DropDownList ID="ddl_Input_Year" runat="server">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label17" runat="server" CssClass="label">DCR Approval Remarks Needed</asp:Label>

                                                <asp:RadioButtonList ID="RbtDCRApprRemks" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                 <br />
                                  <div class="card border-primary">
                                    <div class="card-header">
                                        <h6 class="card-title">SAMPLE/INPUT ACKNOWLEDGEMENT</h6>
                                    </div>
                                    <div class="card-body">
                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label18" runat="server" CssClass="label">Sample Acknowledgement Needed</asp:Label>
                                                <asp:RadioButtonList ID="RadioSample" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="single-des clearfix">
                                                <asp:Label ID="Label19" runat="server" CssClass="label">Input Acknowledgement Needed</asp:Label>
                                                <asp:RadioButtonList ID="RadioInput" runat="server" CssClass="pull-right" RepeatDirection="Horizontal">
                                                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                    <asp:ListItem Value="N">No</asp:ListItem>

                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                           <div class="card border-primary">
                           
                           <div class="card-header">
                                 <div id="Div15" runat="server" style="font-weight: bold; color: #895310; font-size: 16px; font-family:calibri;
                                    text-decoration: underline;">
                                    Mail System</div>
                                <asp:RadioButton ID="rdomail" runat="server" Text="Mail Display at Home Page as Mandatory" AutoPostBack="true" GroupName="TP" />
                            </div>  
                          <div class="card-body">
                              <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                            &nbsp;<asp:Label ID="Label22" Font-Size="Small" runat="server" Text="    Yes - Mail will display at Home Page(Without Read,They Cannot Get Homepage Link)"></asp:Label><br />
                          <asp:Label ID="Label23" Font-Size="Small" runat="server" Text="No - Mail will display at Home Page(Without Read,They Can Get Homepage Link)"></asp:Label>
                                <asp:UpdatePanel ID="updpnlDesignmail" runat="server">
                                    
                                    <ContentTemplate>
                                        <asp:GridView ID="gvDesignationmail" runat="server" AutoGenerateColumns="False" Width="70%"
                                            CellPadding="2" CellSpacing="2" GridLines="None" Font-Names="Arial">
                                            
                                            <Columns>

                                                <asp:TemplateField>
                                                    <ItemTemplate >
                                                       <asp:Label ID="appr" runat="server"  Font-Names="Arial"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesignation" runat="server" Font-Bold="true" ForeColor="#8B0000" Text='<% #Eval("Designation_Short_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkId" Text="No" Width="60px" Font-Bold="true" runat="server" Style="margin-left: 40px"
                                                            AutoPostBack="true" OnCheckedChanged="chkId_OnCheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="20px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkNo" Text="Yes" Width="60px" Font-Bold="true"  runat="server" AutoPostBack="true"
                                                            OnCheckedChanged="chkNo_OnCheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                           
                            <div class="single-des clearfix">
                                <asp:RadioButton ID="rdonoMail" runat="server" Text="Mail Not at All Needed at HomePage" AutoPostBack="true"
                                    GroupName="TP"  />
                            </div>
                           <div class="single-des clearfix">

                            <div class="break">
                            </div>
                           </div>
                        </div>
                    </div>
                            </div>
                            </div>
                        </div>

                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Save" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnClear" runat="server" CssClass="savebutton" Text="Clear" OnClick="btnClear_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

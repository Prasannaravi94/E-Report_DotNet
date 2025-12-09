<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDrEdit.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_ListedDrEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%--<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>--%>
<%--<%@ Register Src="~/UserControl/MenuUserControl_TP.ascx" TagName="Menu" TagPrefix="ucl1" %>--%>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - Listed Doctor</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../../css/style.css" /> --%>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" />

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

        .marRight {
            margin-right: 35px;
        }

        .normal {
            background-color: white;
        }

        .highlight_clr {
            /*background-color: lightblue;*/
        }

        .border1 {
            text-decoration: line-through Red;
        }

        .min-width {
            min-width: 170px;
        }

        .closeLoginPanel {
            font-family: Verdana, Helvetica, Arial, sans-serif;
            height: 14px;
            font-size: 11px;
            font-weight: bold;
            position: absolute;
            top: -2px;
            right: 1px;
        }

            .closeLoginPanel a {
                /*background-color: Yellow;*/
                cursor: pointer;
                color: Black;
                text-align: center;
                text-decoration: none;
                padding: 3px;
            }

        #CblDoctorCode tr td .border {
            border: 0px solid #dee2e6 !important;
        }

        .display-table .table td {
            padding: 15px 15px !important;
        }
    </style>

    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">

        function Validate_1() {
            var gridView = $("table[id*=grdDoctor]");
            var dropdownList = $("table[id*=grdDoctor] select");

            var GvLength = $("table[id*=grdDoctor] tr:not(:first)").length;

            selected = $("table[id*=grdDoctor]  tr td select");

            Len = $("table[id*=grdDoctor]  tr td select").length;
            //alert(Len);

            Textbox = $("table[id*=grdDoctor] tr td input:text");

            TxtLen = $("table[id*=grdDoctor] tr td input:text").length;

            if (GvLength > 0) {

                for (var i = 0; i < Len; i++) {

                    var ddlTerritory = $(selected[i]).closest("td").find("select[id$='Territory_Code'] option:selected").text();
                    var ddlSpecial = $(selected[i]).closest("td").find("select[id$='Doc_Special_Code'] option:selected").text();
                    var ddlCategory = $(selected[i]).closest("td").find("select[id$='Doc_Cat_Code'] option:selected").text();
                    var ddlQualication = $(selected[i]).closest("td").find("select[id$='Doc_QuaCode'] option:selected").text();
                    var ddlClass = $(selected[i]).closest("td").find("select[id$='Doc_ClsCode'] option:selected").text();


                    //                   var Address = $(Textbox[i]).closest("td").find("[id$='ListedDR_Address1']").val();
                    //                   var Lst_DOB = $(Textbox[i]).closest("td").find("[id$='ListedDR_DOB']").val();
                    //                   var Lst_DOW = $(Textbox[i]).closest("td").find("[id$='ListedDR_DOW']").val();
                    //                   var No_Of_Visit = $(Textbox[i]).closest("td").find("[id$='No_of_Visit']").val();
                    //                   var Lst_Mobile = $(Textbox[i]).closest("td").find("[id$='ListedDR_Mobile']").val();
                    //                   var Lst_Phone = $(Textbox[i]).closest("td").find("[id$='ListedDR_Phone']").val();
                    //                   var Lst_Email = $(Textbox[i]).closest("td").find("[id$='ListedDR_EMail']").val();



                    for (var j = 0; j < TxtLen; j++) {

                        var Lst_Dr_Name = $(Textbox[j]).closest("td").find("[id$='ListedDr_Name']").val();

                        if (Lst_Dr_Name == "") {

                            alert("Please Enter Doctor Name");
                            $(this).focus();
                            return false;

                        }
                        else {
                        }
                    }

                    if (ddlTerritory == "---Select---") {

                        alert("Please Select Territory");
                        $(this).focus();
                        return false;
                    }
                    else if (ddlSpecial == "---Select---") {
                        alert("Please Select Speciality");
                        $(this).focus();
                        return false;
                    }
                    else if (ddlCategory == "---Select---") {
                        alert("Please Select Category");
                        $(this).focus();
                        return false;
                    }
                    else if (ddlQualication == "---Select---") {
                        alert("Please Select Qualification");
                        $(this).focus();
                        return false;
                    }
                    else if (ddlClass == "---Select---") {
                        alert("Please Select Class");
                        $(this).focus();
                        return false;
                    }

                    else if (Lst_Dr_Name == "") {

                        alert("Please Enter Doctor Name");
                        $(this).focus();
                        return false;

                    }
                    else {
                    }

                }

            }

        }

    </script>
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

    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
             ({
                 changeMonth: true,
                 changeYear: true,
                 yearRange: '1930:' + new Date().getFullYear().toString(),
                 //                yearRange: "2010:2017",
                 dateFormat: 'dd/mm/yy'
             });
        });

    </script>

    <script type="text/javascript">

        function checkAll(obj1) {

        }

    </script>


</head>
<body>
    <form id="form1" runat="server" name="frmquickproduct_entry" method="post">

        <div>
            <div id="Divid" runat="server"></div>
            <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>--%>
            <%--<ucl:Menu ID="menu1" runat="server" />--%>

            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center" style="border-bottom: 0px">Bulk Edit - Listed Doctor</h2>
                        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Center">
                            <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                        </asp:Panel>
                        <br />
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row justify-content-center">
                                    <asp:Label ID="lblTitle" runat="server" Text="Select the Fields to Edit" ForeColor="#696d6e" Font-Bold="true"
                                        TabIndex="6">
                                    </asp:Label>
                                </div>
                                <br />
                                <center>
                                    <div style="overflow-x: auto;">
                                        <%--<div class="row justify-content-center" style="overflow-x: auto; padding-bottom: 20px; margin-left: -45px; margin-right: -45px;">--%>
                                        <asp:CheckBoxList ID="CblDoctorCode" runat="server"
                                            RepeatColumns="6" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="Territory_Code">Territory</asp:ListItem>
                                            <asp:ListItem Value="Doc_Special_Code">Speciality</asp:ListItem>
                                            <asp:ListItem Value="Doc_Cat_Code">Category</asp:ListItem>
                                            <asp:ListItem Value="Doc_QuaCode">Qualification</asp:ListItem>
                                            <asp:ListItem Value="Doc_ClsCode">Class</asp:ListItem>
                                            <asp:ListItem Value="ListedDR_Address1">Address</asp:ListItem>
                                            <asp:ListItem Value="ListedDR_DOB">DOB</asp:ListItem>
                                            <asp:ListItem Value="ListedDR_DOW">DOW</asp:ListItem>
                                            <asp:ListItem Value="No_of_Visit">No of Visit</asp:ListItem>
                                            <asp:ListItem Value="ListedDR_Mobile">Mobile No</asp:ListItem>
                                            <asp:ListItem Value="ListedDR_Phone">Telephone No</asp:ListItem>
                                            <asp:ListItem Value="ListedDR_EMail">EMail ID</asp:ListItem>
                                            <asp:ListItem Value="ListedDr_Name">Listed Dr Name</asp:ListItem>
                                            <asp:ListItem Value="ListedDr_PinCode">Pin Code</asp:ListItem>
                                            <asp:ListItem Value="Doctor_Type">Doctor Type</asp:ListItem>
                                            <asp:ListItem Value="Day_1">DAY1/DAY2/DAY3</asp:ListItem>
                                            <asp:ListItem Value="Dr_Potential">Dr Potential(Capacity)/Dr Contribution</asp:ListItem>
                                            <asp:ListItem Value="Town_City">Town/City</asp:ListItem>
                                            <asp:ListItem Value="Geo_Tag_Count">Geo Tag Count</asp:ListItem>
                                            <asp:ListItem Value="Unique_Dr_Code">Unique Dr Code</asp:ListItem>
                                            <asp:ListItem Value="Pan_Card">Pan Card</asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </center>
                                <br />
                                <div class="row justify-content-center">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label>
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                            <%-- <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                            <asp:ListItem Text="Doctor Name" Value="7"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-5">
                                        <div class="single-des clearfix" style="padding-top: 19px">
                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%" Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px">
                                            <asp:DropDownList ID="ddlSrc2" runat="server" AutoPostBack="false" Visible="false" OnSelectedIndexChanged="ddlSrc2_SelectedIndexChanged"
                                                CssClass="nice-select" TabIndex="4">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btnOk" runat="server" CssClass="savebutton" Text="Go"
                                        OnClick="btnOk_Click" />
                                    <asp:Button ID="btnClr" CssClass="savebutton" runat="server" Text="Clear"
                                        OnClick="btnClr_Click" />
                                </div>
                            </div>
                            <div class="designation-reactivation-table-area clearfix">
                                <br />
                                <div class="display-table clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin; overflow: inherit">
                                        <div runat="server" id="tblDoctor" visible="false" width="100%">
                                            <%--<asp:UpdatePanel ID="updatepanel1" runat="server">
                                            <ContentTemplate>--%>
                                            <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center"
                                                OnRowDataBound="grdDoctor_RowDataBound" OnRowCreated="grdListedDR_RowCreated"
                                                AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemStyle Width="20px"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDoctorCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doctor" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ListedDrName" runat="server" SkinID="TxtBxAllowSymb" Width="165px" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Territory_Code" runat="server" Width="140px" CssClass="nice-select" DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Speciality" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="min-width">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Doc_Special_Code" runat="server" CssClass="nice-select" DataSource="<%# FillSpeciality() %>" DataTextField="Doc_Special_SName" DataValueField="Doc_Special_Code">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="min-width">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Doc_Cat_Code" runat="server" CssClass="nice-select" DataSource="<%# FillCategory() %>" DataTextField="Doc_Cat_SName" DataValueField="Doc_Cat_Code">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="min-width">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Doc_QuaCode" runat="server" CssClass="nice-select" DataSource="<%# FillQualification() %>" DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="min-width">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Doc_ClsCode" runat="server" CssClass="nice-select" DataSource="<%# FillClass() %>" DataTextField="Doc_ClsSName" DataValueField="Doc_ClsCode">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="min-width">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ListedDR_Address1" onkeypress="AlphaNumeric(event);" CssClass="input" Height="38px" runat="server" Width="350px" Text='<%# Bind("ListedDR_Address1") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DOB" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%--<asp:TextBox ID="ListedDR_DOB"  SkinID="TxtBxAllowSymb" onkeypress="Calendar_enter(event);" runat="server" MaxLength="12" Text='<%# Bind("ListedDR_DOB") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="ListedDR_DOB" Height="38px"
                                                                runat="server" CssClass="input" MaxLength="12" Text='<%# Bind("ListedDR_DOB") %>'></asp:TextBox>
                                                            <asp:CalendarExtender
                                                                ID="CalendarExtender1" Format="dd/MM/yyyy" CssClass="cal_Theme1"
                                                                TargetControlID="ListedDR_DOB"
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DOW" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%-- <asp:TextBox ID="ListedDR_DOW"  SkinID="TxtBxAllowSymb" onkeypress="Calendar_enter(event);"  runat="server" MaxLength="12" Text='<%# Bind("ListedDR_DOW") %>'></asp:TextBox>--%>
                                                            <asp:TextBox ID="ListedDR_DOW" Height="38px"
                                                                runat="server" MaxLength="12" CssClass="input" Text='<%# Bind("ListedDR_DOW") %>'></asp:TextBox>
                                                            <asp:CalendarExtender
                                                                ID="CalendarExtender2" Format="dd/MM/yyyy"
                                                                TargetControlID="ListedDR_DOW" CssClass="cal_Theme1"
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Visit" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>

                                                            <%--   <asp:DropDownList ID="No_of_Visit" runat="server" CssClass="nice-select">
                                                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                </asp:DropDownList>--%>
                                                            <asp:TextBox ID="No_of_Visit" CssClass="input" runat="server" MaxLength="5"
                                                                Text='<%# Bind("No_of_Visit") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ListedDR_Mobile" CssClass="input" Height="38px" runat="server" MaxLength="30" Text='<%# Bind("ListedDR_Mobile") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Telephone No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ListedDR_Phone" CssClass="input" Height="38px" runat="server" MaxLength="30" Text='<%# Bind("ListedDR_Phone") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EMail ID" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ListedDR_EMail" CssClass="input" Height="38px" runat="server" MaxLength="25" Text='<%# Bind("ListedDR_EMail") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>

                                                            <asp:TextBox ID="txtTerritory" runat="server" CssClass="input" Height="38px" Width="200px"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnTerritoryId" runat="server"></asp:HiddenField>
                                                            <asp:PopupControlExtender ID="txtTerritory_PopupControlExtender" runat="server" Enabled="True"
                                                                ExtenderControlID="" TargetControlID="txtTerritory" PopupControlID="Panel2" OffsetY="2" Position="Bottom">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none; border-radius: 8px; overflow-x: auto; height: 200px; width: 200px; scrollbar-width: thin">
                                                                <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                                                    align="right">
                                                                    <div class="closeLoginPanel">
                                                                        <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                                            title="Close">X</a>
                                                                    </div>
                                                                </div>
                                                                <asp:CheckBoxList ID="ChkTerritory" runat="server" Width="180px" CssClass="gridcheckbox"
                                                                    DataTextField="Territory_Name" DataValueField="Territory_Code" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ChkTerritory_SelectedIndexChanged" onclick="checkAll(this);">
                                                                </asp:CheckBoxList>
                                                            </asp:Panel>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Listed Dr Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ListedDr_Name" runat="server" CssClass="input" Height="38px" Width="165px" Text='<%# Bind("ListedDr_Name") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pin Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="ListedDr_PinCode" runat="server" CssClass="input" Width="165px"
                                                                Text='<%# Bind("ListedDr_PinCode") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doctor Type" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Doctor_Type" runat="server" CssClass="nice-select">
                                                                <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="CRM Doctors"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 
                                                    <asp:TemplateField HeaderText="DAY1/DAY2/DAY3" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="260px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDay1" Width="40px" onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input" runat="server" MaxLength="2" Text='<%# Bind("Day_1") %>'></asp:TextBox>

                                                            <asp:TextBox ID="txtDay2" Width="40px" onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input" runat="server" MaxLength="2" Text='<%# Bind("Day_2") %>'></asp:TextBox>


                                                            <asp:TextBox ID="txtDay3" Width="40px" onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input" runat="server" MaxLength="2" Text='<%# Bind("Day_3") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dr Potential(Capacity) /  Dr Contribution" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="260px" />
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPot" Width="50px" onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input" runat="server" MaxLength="7" Text='<%# Bind("Dr_Potential") %>'></asp:TextBox>

                                                            <asp:TextBox ID="txtContri" Width="50px" onkeypress="AlphaNumeric_NoSpecialChars(event);" CssClass="input" runat="server" MaxLength="7" Text='<%# Bind("Dr_Contribution") %>'></asp:TextBox>



                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Town/City" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>

                                                            <asp:DropDownList ID="Town_City" runat="server" CssClass="nice-select" DataSource="<%# FillCity() %>" DataTextField="Town_City" DataValueField="Town_City">
                                                            </asp:DropDownList>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Geo Tag Count" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Geo_Tag_Count" runat="server" CssClass="nice-select">
                                                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                                                <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                                                <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                                                <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                                                <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                                                <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                                            </asp:DropDownList>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unique Dr Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Unique_Dr_Code" MaxLength="10" runat="server" CssClass="input" Width="165px" Text='<%# Bind("Unique_Dr_Code") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pan Card" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="Pan_Card" runat="server" CssClass="input" Width="165px"
                                                                Text='<%# Bind("Pan_Card") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <%--<EmptyDataRowStyle BackColor="AliceBlue" ForeColor="DarkBlue" Font-Names="Verdana"  HorizontalAlign="Center" Font-Bold="true"  />            --%>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                            <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            <br />
                            <div class="row justify-content-center">
                                <asp:Button ID="btnUpdate" CssClass="savebutton" runat="server" Text="Update" Visible="false"
                                    OnClick="btnUpdate_Click" OnClientClick="return Validate_1();" />
                            </div>
                            <div class="div_fixed">
                                <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="savebutton"
                                    OnClick="btnSave_Click" OnClientClick="return Validate_1();" Visible="false" />
                            </div>

                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
        <br />
        <br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>

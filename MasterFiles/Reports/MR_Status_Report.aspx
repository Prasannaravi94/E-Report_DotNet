<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MR_Status_Report.aspx.cs" Inherits="Reports_MR_Status_Report" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Doctor and Chemist Master</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <link rel="stylesheet" href="../../assets/css/Calender_CheckBox.css" type="text/css" />
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
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

        #tbl {
            border-collapse: collapse;
        }

        /*table, td, th {
            border: 1px solid black;
        }*/

        #tblSFRpt {
        }

        #tblLocationDtls {
            margin-left: 300px;
        }

        .style2 {
            width: 50px;
            height: 25px;
        }

        .style3 {
            height: 25px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
         /*Fixed Heading & Fixed Column-Begin*/
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }

     
        .display-reporttable .table tr th:first-child {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 0;
            z-index: 2;
        }

        .display-reporttable .table tr:nth-child(n+2) td:first-child {
            position: -webkit-sticky;
            position: sticky;
            left: 0;
            z-index: 0;
        }

        .display-reporttable .table tr th:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 38px;
            /*background: inherit;*/
            z-index: 2;
            min-width: 158px;
        }

        .display-reporttable .table tr:nth-child(n+2) td:nth-child(2) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 38px;
        }

        .display-reporttable .table tr th:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            left: 170px;
            /*background: inherit;*/
            z-index: 2;
        }

        .display-reporttable .table tr:nth-child(n+2) td:nth-child(3) {
            position: sticky;
            position: -webkit-sticky;
            z-index: 0;
            background-color: white;
            /*background: inherit;*/
            left: 170px;
        }
        /*Fixed Heading & Fixed Column-End*/
    </style>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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
            $('#btnSubmit').click(function () {

                var divi = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Division Name."); $('#ddlDivision').focus(); return false; }
                var Field = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var Field1 = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Field1 == "") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var State = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (State == "---Select---") { alert("Select State Name."); $('#ddlFieldForce').focus(); return false; }


            });
        });
    </script>
    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="js/Quicksearch.js"></script>--%>

    <link type="text/css" rel="stylesheet" href="../css/repstyle.css" />
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>


    <script type="text/javascript">
        function Filter() {
            var grid = document.getElementById("<%=GrdDoctor.ClientID %>");
            var row = grid.rows.length;
            var Code = document.getElementById("GrdDoctor_ctl01_txtUsrDfd_UserName");
            var Name = document.getElementById("GrdDoctor_ctl01_txtSf");
            var HQ = document.getElementById("GrdDoctor_ctl01_txtSf_HQ");
            var Desig = document.getElementById("GrdDoctor_ctl01_txtsf_Designation_Short_Name");
            var State = document.getElementById("GrdDoctor_ctl01_txtState_Name");
            var Manager1 = document.getElementById("GrdDoctor_ctl01_txtReporting_Manager1");
            var Manager2 = document.getElementById("GrdDoctor_ctl01_txtReporting_Manager2");

            var CodeValue = Code.value.toLowerCase();
            var NameValue = Name.value.toLowerCase();
            var HQValue = HQ.value.toLowerCase();
            var DesigValue = Desig.value.toLowerCase();
            var StateValue = State.value.toLowerCase();
            var Manager1Value = Manager1.value.toLowerCase();
            var Manager2Value = Manager2.value.toLowerCase();

            var CodeSplitter = CodeValue.split();
            var NameSplitter = NameValue.split();
            var HQSplitter = HQValue.split();
            var DesigSplitter = DesigValue.split();
            var StateSplitter = StateValue.split();
            var Manager1Splitter = Manager1Value.split();
            var Manager2Splitter = Manager2Value.split();

            var display = '';

            var CodeRowValue = '';
            var NameRowValue = '';
            var HQRowValue = '';
            var DesigRowValue = '';
            var StateRowValue = '';
            var Manager1RowValue = '';
            var Manager2RowValue = '';

            for (var i = 1; i < row; i++) {
                display = 'none';

                CodeRowValue = grid.rows[i].cells[1].innerText;
                NameRowValue = grid.rows[i].cells[2].innerText;
                HQRowValue = grid.rows[i].cells[3].innerText;
                DesigRowValue = grid.rows[i].cells[4].innerText;
                StateRowValue = grid.rows[i].cells[5].innerText;
                Manager1RowValue = grid.rows[i].cells[6].innerText;
                Manager2RowValue = grid.rows[i].cells[7].innerText;

                for (var j = 0; j < CodeSplitter.length; j++) {
                    for (var k = 0; k < NameSplitter.length; k++) {
                        for (var f = 0; f < HQSplitter.length; f++) {
                            for (var d = 0; d < DesigSplitter.length; d++) {
                                for (var r = 0; r < StateSplitter.length; r++) {
                                    for (var s = 0; s < Manager1Splitter.length; s++) {
                                        for (var m = 0; m < Manager2Splitter.length; m++) {
                                            if (NameRowValue.toLowerCase().indexOf(NameSplitter[k]) >= 0 && HQRowValue.toLowerCase().indexOf(HQSplitter[f]) >= 0 && CodeRowValue.toLowerCase().indexOf(CodeSplitter[j]) >= 0 &&
                                                DesigRowValue.toLowerCase().indexOf(DesigSplitter[d]) >= 0 && StateRowValue.toLowerCase().indexOf(StateSplitter[r]) >= 0 && Manager1RowValue.toLowerCase().indexOf(Manager1Splitter[s]) >= 0 && Manager2RowValue.toLowerCase().indexOf(Manager2Splitter[s]) >= 0) {
                                                display = '';
                                            }

                                            else {
                                                display = 'none';
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                grid.rows[i].style.display = display;
            }
        }
    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function tog(v) { return v ? 'addClass' : 'removeClass'; }
        $(document).on('input', '.clearable', function () {
            $(this)[tog(this.value)]('x');
        }).on('mousemove', '.x', function (e) {
            $(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]('onX');
        }).on('touchstart click', '.onX', function (ev) {
            ev.preventDefault();
            $(this).removeClass('x onX').val('').change();
            Filter();
        });
    </script>

</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <%--<ucl:Menu ID="menu1" runat="server" />--%>
            <br />


            <div class="container home-section-main-body position-relative clearfix" style="max-width:1350px">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Doctor and Chemist Master</h2>


                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblView" runat="server" Text="View By" CssClass="label"></asp:Label>
                                    <asp:RadioButtonList ID="rdoMGRState" runat="server" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnSelectedIndexChanged="rdoMGRState_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="FieldForce-wise&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
                                            Selected="True"></asp:ListItem>
                                        <%--<asp:ListItem Value="1" Text="State-wise"></asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblState" runat="server" Text="State" CssClass="label"></asp:Label>

                                    <div class="row">
                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" Visible="false"
                                                CssClass="nice-select">
                                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                                <%--<asp:ListItem Value="2" Text="HQ"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" CssClass="nice-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" CssClass="custom-select2 nice-select">
                                    </asp:DropDownList>
                                    <br />

                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="single-des clearfix">
                                <asp:CheckBox ID="chkDeactive" Checked="false" Text="Include Deactive List"
                                    runat="server" />
                            </div>
                            <br />

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton"
                                Text="View" OnClick="btnSubmit_Click" />
                            <label id="lblwidth" runat="server" width="300px"></label>
                            <asp:LinkButton ID="btnExcel" runat="server" Text="Export to Excel" Font-Names="Verdana" Font-Bold="true" Font-Size="10px"
                                BorderColor="Black" BorderStyle="Solid" BorderWidth="0" Height="25px" Width="120px"
                                OnClick="btnExcel_Click"></asp:LinkButton>
                        </div>

                    </div>
                </div>

                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <br />
                        <asp:Panel ID="pnlContents" runat="server">
                            <div class="display-Approvaltable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:Table ID="tbl" runat="server" CssClass="table">
                                    </asp:Table>
                                </div>
                            </div>
                            <div class="display-reporttable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                    <asp:GridView ID="GrdDoctor" runat="server" Width="90%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" PageSize="10" GridLines="None" style="background-color:white"
                                        CssClass="table" OnRowDataBound="GrdDoctor_DataBound" ShowFooter="True" PagerStyle-CssClass="gridview1">
                                        <FooterStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-CssClass="stickyFirstRow">
                                                <%-- <ControlStyle Width="50%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SF_Code" Visible="false" HeaderStyle-CssClass="stickyFirstRow">

                                                <ControlStyle Width="20%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfCode" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Emp_Code" HeaderStyle-CssClass="stickyFirstRow">
                                                <ControlStyle Width="72px"></ControlStyle>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblUsrDfd_UserName" Text="Emp_Code" runat="server" /><br />
                                                    <asp:TextBox ID="txtUsrDfd_UserName" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbUsrDfd_UserName" runat="server" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-CssClass="stickyFirstRow">
                                                <ControlStyle Width="150px"></ControlStyle>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSf" Text="FieldForce Name" runat="server" /><br />
                                                    <asp:TextBox ID="txtSf" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HQ" HeaderStyle-CssClass="stickyFirstRow">
                                                <%--  <ControlStyle Width="100px"></ControlStyle>--%>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblSf_HQ" Text="HQ" runat="server" /><br />
                                                    <asp:TextBox ID="txtSf_HQ" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbSf_HQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-CssClass="stickyFirstRow">
                                                <%-- <ControlStyle Width="80px"></ControlStyle>--%>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblsf_Designation_Short_Name" Text="Designation" runat="server" /><br />
                                                    <asp:TextBox ID="txtsf_Designation_Short_Name" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbsf_Designation_Short_Name" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="State_Name" HeaderStyle-CssClass="stickyFirstRow">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblState_Name" Text="State_Name" runat="server" /><br />
                                                    <asp:TextBox ID="txtState_Name" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <%--  <ControlStyle Width="150px"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState_Name" runat="server" Text='<%# Bind("State_Name") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="First Level Manager" HeaderStyle-CssClass="stickyFirstRow">
                                                <ControlStyle Width="150px"></ControlStyle>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblReporting_Manager1" Text="First Level Manager" runat="server" /><br />
                                                    <asp:TextBox ID="txtReporting_Manager1" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReporting_Manager1" runat="server" Text='<%# Bind("Reporting_Manager1") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Second Level Manager" HeaderStyle-CssClass="stickyFirstRow">
                                                <ControlStyle Width="170px"></ControlStyle>
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblReporting_Manager2" Text="Second Level Manager" runat="server" /><br />
                                                    <asp:TextBox ID="txtReporting_Manager2" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReporting_Manager2" runat="server" Text='<%# Bind("Reporting_Manager2") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce Name" Visible="false" HeaderStyle-CssClass="stickyFirstRow">
                                                <%-- <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDivision_Code" runat="server" Text='<%# Bind("Division_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFooterDivision_Code" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Active Territory" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                <%-- <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblActive_Territory" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=1&status=0", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("Active_Territory") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblActTerrTotal" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=1&status=0")%>' Target="_blank" runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DeActive Territory" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                <%-- <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblDeActive_Territory" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=1&status=1", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("DeActive_Territory") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblDeActiveTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=1&status=1")%>'
                                                        runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Active ListedDR" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                <%-- <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblActive_ListedDR" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=2&status=0", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("Active_ListedDR") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblActiveLstDRTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=2&status=0")%>'
                                                        runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="DeActive ListedDR" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                <%-- <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblDeActive_ListedDR" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=2&status=1", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("DeActive_ListedDR") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblDeActiveLstDRTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=2&status=1")%>'
                                                        runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Active UnListedDR" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow" Visible="false">
                                                <%--  <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblActive_UnListedDR" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=3&status=0", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("Active_UnListedDR") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblActiveUnLstDRTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=3&status=0")%>'
                                                        runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DeActive UnListedDR" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow" Visible="false">
                                                <%-- <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblDeActive_UnListedDR" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=3&status=1", Eval("Sf_Code"),Eval("sf_name"))%>' runat="server" Text='<%# Bind("DeActive_UnListedDR") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblDeActiveUnLstDRTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=3&status=1")%>'
                                                        runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active Chemists" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                <%--  <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblActive_Chemists" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=4&status=0", Eval("Sf_Code"),Eval("sf_name"))%>'
                                                        runat="server" Text='<%# Bind("Active_Chemists") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblActiveChemistTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=4&status=0")%>'
                                                        runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DeActive Chemists" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                <%-- <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblDeActive_Chemists" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=4&status=1", Eval("Sf_Code"),Eval("sf_name"))%>'
                                                        runat="server" Text='<%# Bind("DeActive_Chemists") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblDeActiveChemistTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=4&status=1")%>'
                                                        runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active Stockiest" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow" Visible="false">
                                                <%-- <ControlStyle Width="10%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblActive_Stockiest" runat="server" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=5&status=0", Eval("Sf_Code"),Eval("sf_name"))%>' Text='<%# Bind("Active_Stockiest") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblActiveStockTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=5&status=0")%>'
                                                        runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DeActive Stockiest" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow" Visible="false">
                                                <%--  <ControlStyle Width="1%"></ControlStyle>--%>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblDeActive_Stockiest" runat="server" Target="_blank"
                                                        NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code={0}&sf_name={1}&type=5&status=1", Eval("Sf_Code"),Eval("sf_name"))%>' Text='<%# Bind("DeActive_Stockiest") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:HyperLink ID="lblDeActiveStockTotal" Target="_blank" NavigateUrl='<%#String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?Sf_Code=0&sf_name=0&type=5&status=1")%>'
                                                        runat="server"></asp:HyperLink>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                        <br />
                    </div>


                </div>

                <div class="no-result-area" id="div1" runat="server" visible="false">
                    No Records Found
                </div>

            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
            <br />
            <br />
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js" type="text/javascript"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

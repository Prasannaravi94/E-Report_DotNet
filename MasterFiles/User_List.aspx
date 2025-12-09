<%@ Page Language="C#" AutoEventWireup="true" CodeFile="User_List.aspx.cs" Inherits="MasterFiles_User_List" %>

<%@ Register Src="~/UserControl/pnlMenu_TP.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_TP_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User List</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link rel="stylesheet" href="../E-Report_DotNet/assets/css/Calender_CheckBox.css" type="text/css" />

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

        .marright {
            margin-left: 85%;
        }

        .custom-select2 {
            -webkit-tap-highlight-color: transparent;
            background-color: #fff;
            border-radius: 5px;
            border: solid 1px #e8e8e8;
            box-sizing: border-box;
            clear: both;
            cursor: pointer;
            display: block;
            float: left;
            font-family: inherit;
            font-size: 14px;
            font-weight: normal;
            height: 42px;
            line-height: 40px;
            outline: none;
            padding-left: 18px;
            padding-right: 30px;
            position: relative;
            text-align: left !important;
            -webkit-transition: all 0.2s ease-in-out;
            transition: all 0.2s ease-in-out;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            white-space: nowrap;
            width: auto;
        }

        .display-reportMaintable #grdSalesForce th:first-child {
            background-color: #F1F5F8;
            color: #636d73;
            font-size: 12px;
        }

        .display-reportMaintable #grdSalesForce tr:nth-child(2) td:first-child {
            background-color: transparent;
            color: #636d73;
        }

        .display-reportMaintable #grdSalesForce tr td:first-child {
            background-color: transparent;
            border-top: 1px solid #dee2e6;
        }
        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
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
        function PrintGridData() {

            var prtGrid = document.getElementById('<%=grdSalesForce.ClientID %>');

            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', ''); //left = 0, top = 0, width = 800, height = 500, tollbar = 0, scrollbars = 1, status = 0, resizable = yes
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>

    <script type="text/javascript">
        function view_Data(v_Act_Code) {
            window.open("Approval_Reporting.aspx?sf_code=" + v_Act_Code, 'ViewChange',
        'height=440,width=650,left=150,top=150,screenX=0,screenY=100');

            return false;
        }

    </script>
    <script type="text/javascript">
        function OpenPop(url, newname, settings, sf_code, sf_name, designation_short_name, sf_hq, Reporting_To_SF) {
            window.open(url + "?sf_code=" + sf_code + "&sf_name=" + sf_name + "&designation_short_name=" + designation_short_name + "&sf_hq=" + sf_hq + "&Reporting_To_SF=" + Reporting_To_SF, newname, settings);
            return false;
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

        function OpenNewWindow() {

            //   window.open("DoctorBirthday_View.aspx", "List", "scrollbars=true, resizable=yes,width=700,height=500");

            window.open('UserList_NewWindow.aspx', null, 'pnllnk.style.visibility = "hidden";');

            return false;

        }
    </script>

    <script type="text/javascript">
        function Filter() {
            var grid = document.getElementById("<%=grdSalesForce.ClientID %>");
            var row = grid.rows.length;
            var HQ = document.getElementById("grdSalesForce_ctl01_txtHQ");
            var State = document.getElementById("grdSalesForce_ctl01_txtStateName");
            var FForce = document.getElementById("grdSalesForce_ctl01_txtFieldForce");
            var EmpID = document.getElementById("grdSalesForce_ctl01_txtempid");
            var Desig = document.getElementById("grdSalesForce_ctl01_txtDesig");
            var Reporting = document.getElementById("grdSalesForce_ctl01_txtReporting");
            var Status = document.getElementById("grdSalesForce_ctl01_txtStatus");

            var HQValue = HQ.value.toLowerCase();
            var StateValue = State.value.toLowerCase();
            var FForceValue = FForce.value.toLowerCase();
            var EmpIDValue = EmpID.value.toLowerCase();
            var DesigValue = Desig.value.toLowerCase();
            var RptValue = Reporting.value.toLowerCase();
            var StatusValue = Status.value.toLowerCase();

            //var HQSplitter = HQValue.split(' ');
            //var StateSplitter = StateValue.split(' ');
            //var FForceSplitter = FForceValue.split(' ');
            //var DesigSplitter = DesigValue.split(' ');
            //var RptSplitter = RptValue.split(' ');

            var HQSplitter = HQValue.split();
            var StateSplitter = StateValue.split();
            var FForceSplitter = FForceValue.split();
            var EmpIDSplitter = EmpIDValue.split();
            var DesigSplitter = DesigValue.split();
            var RptSplitter = RptValue.split();
            var StatusSplitter = StatusValue.split();

            var display = '';

            var HQRowValue = '';
            var StateRowValue = '';
            var FForceRowValue = '';
            var EmpIDRowValue = '';
            var DesigRowValue = '';
            var RptRowValue = '';
            var StatusRowValue = '';

            for (var i = 1; i < row; i++) {
                display = 'none';
                var checkBox = document.getElementById("chkdoctor");
                if (checkBox.checked == true) {
                    HQRowValue = grid.rows[i].cells[0].innerText;
                    StateRowValue = grid.rows[i].cells[1].innerText;
                    FForceRowValue = grid.rows[i].cells[2].innerText;
                    EmpIDRowValue = grid.rows[i].cells[3].innerText;
                    DesigRowValue = grid.rows[i].cells[4].innerText;
                    RptRowValue = grid.rows[i].cells[5].innerText;
                    StatusRowValue = grid.rows[i].cells[8].innerText;
                }
                else {
                    HQRowValue = grid.rows[i].cells[1].innerText;
                    StateRowValue = grid.rows[i].cells[2].innerText;
                    FForceRowValue = grid.rows[i].cells[3].innerText;
                    EmpIDRowValue = grid.rows[i].cells[5].innerText;
                    DesigRowValue = grid.rows[i].cells[6].innerText;
                    RptRowValue = grid.rows[i].cells[7].innerText;
                    StatusRowValue = grid.rows[i].cells[10].innerText;
                }

                for (var j = 0; j < HQSplitter.length; j++) {
                    for (var k = 0; k < StateSplitter.length; k++) {
                        for (var f = 0; f < FForceSplitter.length; f++) {
                            for (var m = 0; m < EmpIDSplitter.length; m++) {
                                for (var d = 0; d < DesigSplitter.length; d++) {
                                    for (var r = 0; r < RptSplitter.length; r++) {
                                        for (var s = 0; s < StatusSplitter.length; s++) {
                                            if (EmpIDRowValue.toLowerCase().indexOf(EmpIDSplitter[m]) >= 0 && StateRowValue.toLowerCase().indexOf(StateSplitter[k]) >= 0 && FForceRowValue.toLowerCase().indexOf(FForceSplitter[f]) >= 0 && HQRowValue.toLowerCase().indexOf(HQSplitter[j]) >= 0 && DesigRowValue.toLowerCase().indexOf(DesigSplitter[d]) >= 0 && RptRowValue.toLowerCase().indexOf(RptSplitter[r]) >= 0 && StatusRowValue.toLowerCase().indexOf(StatusSplitter[s]) >= 0) {
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

    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
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
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
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
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblsubdiv" runat="server" Text="Subdivision Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlsubdiv" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblFilter" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>

                                    <div class="row">
                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="nice-select" Visible="false">
                                                <%-- SkinID="ddlRequired"--%>
                                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired" CssClass="nice-select" Visible="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                                        OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                    </asp:DropDownList>

                                    <br />
                                    <br />
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-7">
                                        <asp:CheckBox ID="chkVacant" Text=" Without - Vacant (For Mgr's Only)" Checked="true" runat="server" />
                                    </div>
                                    <div class="col-lg-5">
                                        <asp:CheckBox ID="chkBase" Text="Including Vacant - Base Level"
                                            Checked="true" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-7">
                                        <asp:CheckBox ID="chkdoctor" Text=" Without - Dr Count" Font-Size="Medium"
                                            Checked="true" runat="server" />
                                    </div>
                                </div>

                                <asp:Button ID="btnmgrgo" runat="server" Text="Go" OnClick="btnmgrgo_Click" CssClass="savebutton" />

                            </div>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <div class="row">
                                <div class="col-lg-12" >
                                    <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click"
                                        CssClass="savebutton" />
                                </div>
                               <%-- <div class="col-lg-5" align="left" style="margin-top: 10px;">--%>
                                    <asp:ImageButton ID="imgNew"  runat="server" BorderStyle="Solid" BorderColor="Red" Width="28px" ImageUrl="~/Images/new window.png" OnClientClick="return OpenNewWindow() ;" Visible="false" />
                               <%-- </div>--%>
                            </div>
                              <br />
                            <div class="row">
                                <div class="col-lg-12">
                                <asp:LinkButton ID="btnExcelSubmit" runat="server" Font-Size="Medium" Font-Bold="true"
                                Text="Download Excel" OnClick="btnExcelSubmit_Click" />
                                    </div>
                                 </div>
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
                            <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClientClick="RefreshParent();">
                                <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel" Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>&nbsp&nbsp
                            <asp:LinkButton ID="imgpdf" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click" Visible="false">
                                <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/pdf.png" ToolTip="Pdf" Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>
                        </asp:Panel>

                    </div>
                </div>
                <asp:ScriptManager ID="scriptmanager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="updatepnl" runat="server">
                    <ContentTemplate>
                        <div class="row justify-content-center">
                            <div class="col-lg-12">

                                <asp:Panel ID="pnlContents" runat="server">
                                    <div class="display-reportMaintable clearfix">
                                        <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit;">

                                            <asp:HiddenField ID="hfClassApplied" runat="server" />

                                            <asp:GridView ID="grdSalesForce" runat="server" OnRowCommand="grdSalesForce_RowCommand"
                                                AutoGenerateColumns="false" OnRowDataBound="grdSalesForce_RowDataBound"
                                                CssClass="table" GridLines="None">

                                                <Columns>
                                                    <%--    <asp:TemplateField HeaderText="#" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdSalesForce.PageIndex * grdSalesForce.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>--%>
                                                   

                                                    <asp:TemplateField HeaderText="Drs Count" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblDrsCnt" runat="server" Font-Size="12px" Font-Bold="true" Width="10%" Font-Names="sans-serif" Forecolor="Red" Text='<%# Bind("Lst_drCount") %>' ></asp:Label>--%>
                                                            <asp:HyperLink ID="lblDrsCnt" Target="_blank" runat="server" NavigateUrl='<%# String.Format("~/MasterFiles/Reports/rptMRStatus.aspx?sf_code={0}&sf_name={1}&type={2}&status={3}", Eval("Sf_code"), Eval("Sf_Name"),2,0) %>' Text='<%# Bind("Lst_drCount") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" SortExpression="sf_hq" HeaderStyle-CssClass="stickyFirstRow">
                                                        <%--  <ControlStyle Width="90%"></ControlStyle>--%>
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkHQ" runat="server" Text="HQ" CommandName="Sort" CommandArgument="sf_hq" />
                                                            <%-- <asp:Label ID="lblHQ" Text="HQ" runat="server" />--%><br />
                                                            <asp:TextBox ID="txtHQ" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlace" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="State Name" ItemStyle-HorizontalAlign="Left" SortExpression="StateName" HeaderStyle-CssClass="stickyFirstRow">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkStateName" runat="server" Text="State Name" CommandName="Sort" CommandArgument="StateName" />
                                                            <asp:TextBox ID="txtStateName" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblState" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Code"  HeaderStyle-CssClass="stickyFirstRow">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("Sf_code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left" SortExpression="Sf_Name" HeaderStyle-CssClass="stickyFirstRow">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkFieldForce" runat="server" Text="FieldForce Name" CommandName="Sort" CommandArgument="Sf_Name" />
                                                            <asp:TextBox ID="txtFieldForce" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="130px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFieldForce" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DOJ" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDOJ" runat="server" Text='<%# Bind("Sf_Joining_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Employee ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkempid" runat="server" Text="Employee ID" CommandName="Sort" CommandArgument="sf_emp_id" />
                                                            <asp:TextBox ID="txtempid" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsf_emp_id" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" SortExpression="Designation_Short_Name" HeaderStyle-CssClass="stickyFirstRow">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkDesig" runat="server" Text="Designation" CommandName="Sort" CommandArgument="Designation_Short_Name" />
                                                            <asp:TextBox ID="txtDesig" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesignation" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reporting" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkReporting" runat="server" Text="Reporting" CommandName="Sort" CommandArgument="Reporting_To_SF" />
                                                            <asp:TextBox ID="txtReporting" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUserName" runat="server" Style="font-weight: 700" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Password" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <%--<HeaderStyle Width="120px" />--%>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPassword" runat="server" Style="font-weight: 700" Text='<%# Bind("sf_password") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                        <HeaderTemplate>
                                                            <asp:LinkButton ID="lnkStatus" runat="server" Text="Status" CommandName="Sort" CommandArgument="sf_Tp_Active_flag" />
                                                            <br />
                                                            <asp:TextBox ID="txtStatus" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px"></asp:TextBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>

                                                            <asp:LinkButton ID="lblS" runat="server" Font-Underline="false" ForeColor="#636d73"
                                                                OnClientClick='<%#  String.Format("return OpenPop(\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\")","Approval_Reporting.aspx","newWindow","scrollbars=yes,resizable=yes, width=550, height=550", Eval("sf_code").ToString().Trim(), Eval("sf_name"),Eval("designation_short_name"),Eval("sf_hq"),Eval("Reporting_To_SF")) %>'
                                                                Text='<%# Bind("sf_Tp_Active_flag") %>'> 

                                                         <%--    <asp:LinkButton ID="lblS" runat="server" Font-Size="10px" Font-Underline="false" Font-Names="Verdana" Forecolor="#000000"
                                                                 OnClientClick='<%#string.Format("return view_Data(\"{0}\");",
                                                                    DataBinder.Eval(Container.DataItem, "sf_code")) %>'
                                                                   Text='<%# Bind("sf_Tp_Active_flag") %>'>--%>
                                          
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Color" Visible="false" HeaderStyle-CssClass="stickyFirstRow">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBackColor" runat="server" Text='<%# Bind("Desig_Color") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" Visible="false" HeaderStyle-CssClass="stickyFirstRow">
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

                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <br />
            <br />
        </div>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>

    </form>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Screenwise_Lock.aspx.cs" Inherits="MasterFiles_Options_Screenwise_Lock" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Screenwise Lock</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script>
        function Filter() {
            var grid = document.getElementById("<%=gvDetails.ClientID %>");
            var row = grid.rows.length;
            var HQ = document.getElementById("grdSalesForce_ctl01_txtHQ");
            var State = document.getElementById("grdSalesForce_ctl01_txtStateName");
            var FForce = document.getElementById("grdSalesForce_ctl01_txtFieldForce");
            var Desig = document.getElementById("grdSalesForce_ctl01_txtDesig");
            var Reporting = document.getElementById("grdSalesForce_ctl01_txtReporting");
            var Status = document.getElementById("grdSalesForce_ctl01_txtStatus");

            var HQValue = HQ.value.toLowerCase();
            var StateValue = State.value.toLowerCase();
            var FForceValue = FForce.value.toLowerCase();
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
            var DesigSplitter = DesigValue.split();
            var RptSplitter = RptValue.split();
            var StatusSplitter = StatusValue.split();

            var display = '';

            var HQRowValue = '';
            var StateRowValue = '';
            var FForceRowValue = '';
            var DesigRowValue = '';
            var RptRowValue = '';
            var StatusRowValue = '';

            for (var i = 1; i < row; i++) {
                display = 'none';
                var checkBox = document.getElementById("chkdoctor");
                if (checkBox.checked == true) {
                    HQRowValue = grid.rows[i].cells[1].innerText;
                    StateRowValue = grid.rows[i].cells[2].innerText;
                    FForceRowValue = grid.rows[i].cells[3].innerText;
                    DesigRowValue = grid.rows[i].cells[4].innerText;
                    RptRowValue = grid.rows[i].cells[5].innerText;
                    StatusRowValue = grid.rows[i].cells[8].innerText;
                }
                else {
                    HQRowValue = grid.rows[i].cells[2].innerText;
                    StateRowValue = grid.rows[i].cells[3].innerText;
                    FForceRowValue = grid.rows[i].cells[4].innerText;
                    DesigRowValue = grid.rows[i].cells[5].innerText;
                    RptRowValue = grid.rows[i].cells[6].innerText;
                    StatusRowValue = grid.rows[i].cells[9].innerText;
                }

                for (var j = 0; j < HQSplitter.length; j++) {
                    for (var k = 0; k < StateSplitter.length; k++) {
                        for (var f = 0; f < FForceSplitter.length; f++) {
                            for (var d = 0; d < DesigSplitter.length; d++) {
                                for (var r = 0; r < RptSplitter.length; r++) {
                                    for (var s = 0; s < StatusSplitter.length; s++) {
                                        if (StateRowValue.toLowerCase().indexOf(StateSplitter[k]) >= 0 && FForceRowValue.toLowerCase().indexOf(FForceSplitter[f]) >= 0 && HQRowValue.toLowerCase().indexOf(HQSplitter[j]) >= 0 && DesigRowValue.toLowerCase().indexOf(DesigSplitter[d]) >= 0 && RptRowValue.toLowerCase().indexOf(RptSplitter[r]) >= 0 && StatusRowValue.toLowerCase().indexOf(StatusSplitter[s]) >= 0) {
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
                grid.rows[i].style.display = display;
            }
        }
    </script>
    <style>
        input[type="checkbox"] + label {
            color: white;
        }
        .display-table .table th {
            padding: 10px 6px;
        }
        .display-table .table td {
            padding: 15px 10px !important;
        }
    </style>
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
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" CssClass="label">FieldForce Name</asp:Label>
                                <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:DropDownList ID="ddlFieldForce" CssClass="custom-select2 nice-select" runat="server"></asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:DropDownList ID="ddlSF" CssClass="custom-select2 nice-select" runat="server" Visible="false"></asp:DropDownList>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="overflow:inherit; scrollbar-width: thin;">
                                    <asp:GridView ID="gvDetails" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowDataBound="gvDetails_RowDataBound"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">
                                        <%--<HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>--%>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SF Code" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("SF_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce" ItemStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblFF" Text="FieldForce" runat="server" CssClass="label" Width="100%"></asp:Label>
                                                    <asp:TextBox ID="txtFieldForce" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="130px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Design" ItemStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblDesignation" Text="Designation" runat="server" CssClass="label" Width="100%"></asp:Label>
                                                    <asp:TextBox ID="txtDesig" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDG" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblHQ" Text="HQ" runat="server" CssClass="label" Width="100%"></asp:Label>
                                                    <asp:TextBox ID="txtHQ" runat="server" CssClass="input clearable" onkeyup="Filter();" Width="100px" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_hq" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DCR_Lock1" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDCR_Lock" runat="server" Text='<%#Eval("DCR_Lock")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DCR Lock" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLevel1" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TP Lock" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTP_Lock" runat="server" Text='<%#Eval("TP_Lock")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TP Lock" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLevel2" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SDP_Lock" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSDP_Lock" runat="server" Text='<%#Eval("SDP_Lock")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Territory Visit Lock" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLevel3" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Camp_Lock" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCamp_Lock" runat="server" Text='<%#Eval("Camp_Lock")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Campaign Lock" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLevel4" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DR_Lock" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDR_Lock" runat="server" Text='<%#Eval("DR_Lock")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor Map Lock" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkLevel5" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Coredr_Lock" ItemStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-ForeColor="White">
                                <ItemTemplate>
                                    <asp:Label ID="lblCore_Lock" runat="server" Text='<%#Eval("Coredr_Lock")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Coredr Lock" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel6" runat="server" Text="."/>
                                </ItemTemplate>
                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unlst Cnt" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtUnlist" runat="server" CssClass="input" onkeypress="CheckNumeric(event);" MaxLength="4" Text='<%#Eval("Unlst_Cnt")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnSumbit" runat="server" Text="Save" CssClass="savebutton" Visible="false" OnClick="btnSumbit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

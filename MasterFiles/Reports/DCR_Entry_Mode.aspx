<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Entry_Mode.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR - Status (count - Modewise)</title>
    <%--    <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />

    <style type="text/css">
        #grdSalesForce .small {
            font-size: 100%;
        }

        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }
    </style>


</head>
<body>
    <script type="text/javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=grdSalesForce.ClientID %>');
            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', '');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            // $('input:text:first').focus();
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
            $('#btnSF').click(function () {
                var Prod = $('#<%=ddlSubdiv.ClientID%> :selected').text();
                if (Prod == "---Select---") { alert("Select Salesforce Name."); $('#ddlSubdiv').focus(); return false; }
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>

    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server"></div>


            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">DCR - Status (count - Modewise)</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblSubdiv" runat="server" CssClass="label" Text="FieldForce Name"></asp:Label>
                                <asp:DropDownList ID="ddlSubdiv" CssClass="custom-select2 nice-select" runat="server" Width="100%">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                        <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <%--         <div class="col-lg-6">
                                        <asp:Label ID="lblMonth" runat="server" CssClass="label" Text="Month"></asp:Label>
                                        <asp:DropDownList ID="monthId" runat="server" CssClass="nice-select"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblYr" runat="server" CssClass="label" Text="Year"></asp:Label>
                                        <asp:DropDownList ID="yearID" runat="server" CssClass="nice-select"></asp:DropDownList>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSF" runat="server" Text="Go" CssClass="savebutton"
                                OnClick="btnSF_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row ">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlprint" runat="server" Visible="false" CssClass="panelmarright">
                            <input type="image" id="btnPrint" src="../../assets/images/Printer.png" style="border-width: 0px; width: 30px"
                                onclick="PrintGridData()" />
                        </asp:Panel>
                    </div>
                </div>

                <div class="display-table clearfix" align="center">
                    <div class="table-responsive" align="center" style="scrollbar-width: thin;">
                        <table width="100%" align="center">
                            <tbody>
                                <tr>
                                    <td align="center">
                                        <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                                            AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                            CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fieldforce Name" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>

                                                        <asp:HiddenField ID="sfNameHidden" runat="server" Value='<%#Eval("sf_name")%>' />
                                                        <asp:HiddenField ID="sfCodeHidden" runat="server" Value='<%#Eval("SF_Code")%>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Head Quater" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("sf_HQ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Emp Id" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Design" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesig" runat="server" Text='<%# Bind("sf_Designation_short_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Desktop" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesk" runat="server" Text='<%# Bind("Desktop") %>'></asp:Label>
                                                        <asp:Panel ID="Panel1" runat="server" Style="background-color: #FFFFFF">
                                                            <asp:Label ID="Labeldesk" runat="server" Text='<%# Bind("desdt")%>'></asp:Label>
                                                        </asp:Panel>
                                                        <ajax:BalloonPopupExtender ID="PopupControlExtender" runat="server"
                                                            TargetControlID="lbldesk"
                                                            BalloonPopupControlID="Panel1"
                                                            Position="BottomRight"
                                                            BalloonStyle="Rectangle"
                                                            BalloonSize="Small"
                                                            CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                                            CustomClassName="oval"
                                                            UseShadow="true"
                                                            ScrollBars="Auto"
                                                            DisplayOnMouseOver="true"
                                                            DisplayOnFocus="false"
                                                            DisplayOnClick="false" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblApps" runat="server" Text='<%# Bind("Mobile")%>'></asp:Label>
                                                        <asp:Panel ID="Panel2" runat="server" Style="background-color: #FFFFFF">
                                                            <asp:Label ID="LabelApps" runat="server" Text='<%# Bind("mobdt")%>'></asp:Label>
                                                        </asp:Panel>
                                                        <ajax:BalloonPopupExtender ID="PopupControlExtender1" runat="server"
                                                            TargetControlID="lblApps"
                                                            BalloonPopupControlID="Panel2"
                                                            Position="BottomRight"
                                                            BalloonStyle="Rectangle"
                                                            BalloonSize="Small"
                                                            CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                                            CustomClassName="oval"
                                                            UseShadow="true"
                                                            ScrollBars="Auto"
                                                            DisplayOnMouseOver="true"
                                                            DisplayOnFocus="false"
                                                            DisplayOnClick="false" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Apps" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("Apps")%>'></asp:Label>
                                                        <asp:Panel ID="Panel3" runat="server" Style="background-color: #FFFFFF">
                                                            <asp:Label ID="LabelMobile" runat="server" Text='<%# Bind("Appdt")%>'></asp:Label>
                                                        </asp:Panel>
                                                        <ajax:BalloonPopupExtender ID="PopupControlExtender3" runat="server"
                                                            TargetControlID="lblMobile"
                                                            BalloonPopupControlID="Panel3"
                                                            Position="BottomRight"
                                                            BalloonStyle="Rectangle"
                                                            BalloonSize="Small"
                                                            CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                                            CustomClassName="oval"
                                                            UseShadow="true"
                                                            ScrollBars="Auto"
                                                            DisplayOnMouseOver="true"
                                                            DisplayOnFocus="false"
                                                            DisplayOnClick="false" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="E-detailing" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEdt" runat="server" Text='<%# Bind("Edt")%>'></asp:Label>
                                                        <asp:Panel ID="Panel5" runat="server" Style="background-color: #FFFFFF">
                                                            <asp:Label ID="LabelEdt" runat="server" Text='<%# Bind("edtdt")%>'></asp:Label>
                                                        </asp:Panel>
                                                        <ajax:BalloonPopupExtender ID="PopupControlExtender5" runat="server"
                                                            TargetControlID="lblEdt"
                                                            BalloonPopupControlID="Panel5"
                                                            Position="BottomRight"
                                                            BalloonStyle="Rectangle"
                                                            BalloonSize="Small"
                                                            CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                                            CustomClassName="oval"
                                                            UseShadow="true"
                                                            ScrollBars="Auto"
                                                            DisplayOnMouseOver="true"
                                                            DisplayOnFocus="false"
                                                            DisplayOnClick="false" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Others" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblothers" runat="server" Text='<%# Bind("Oths")%>'></asp:Label>
                                                        <asp:Panel ID="Panel4" runat="server" Style="background-color: #FFFFFF">
                                                            <asp:Label ID="Labelothers" runat="server" Text='<%# Bind("othdt")%>'></asp:Label>
                                                        </asp:Panel>
                                                        <ajax:BalloonPopupExtender ID="PopupControlExtender4" runat="server"
                                                            TargetControlID="lblothers"
                                                            BalloonPopupControlID="Panel4"
                                                            Position="BottomRight"
                                                            BalloonStyle="Rectangle"
                                                            BalloonSize="Small"
                                                            CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                                            CustomClassName="oval"
                                                            UseShadow="true"
                                                            ScrollBars="Auto"
                                                            DisplayOnMouseOver="true"
                                                            DisplayOnFocus="false"
                                                            DisplayOnClick="false" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="iOS" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbliOS" runat="server" Text='<%# Bind("iOS")%>'></asp:Label>
                                                        <asp:Panel ID="pnliOS" runat="server" Style="background-color: #FFFFFF">
                                                            <asp:Label ID="LabeliOS" runat="server" Text='<%# Bind("iOSdt")%>'></asp:Label>
                                                        </asp:Panel>
                                                        <ajax:BalloonPopupExtender ID="PopupCntrlExtendiOS" runat="server"
                                                            TargetControlID="lbliOS"
                                                            BalloonPopupControlID="pnliOS"
                                                            Position="BottomRight"
                                                            BalloonStyle="Rectangle"
                                                            BalloonSize="Small"
                                                            CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                                            CustomClassName="oval"
                                                            UseShadow="true"
                                                            ScrollBars="Auto"
                                                            DisplayOnMouseOver="true"
                                                            DisplayOnFocus="false"
                                                            DisplayOnClick="false" />
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>

            <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "M-yyyy",
                    viewMode: "months",
                    minViewMode: "months",
                    language: "tr"
                });
            });
        </script>
    </form>
</body>
</html>

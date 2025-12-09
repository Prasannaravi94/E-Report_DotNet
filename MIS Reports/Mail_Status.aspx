<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mail_Status.aspx.cs" Inherits="MasterFiles_Mail_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mail - Status</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript">
        function PrintGridData() {

            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
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

            });
        });
    </script>
    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
        ({
            changeMonth: true,
            changeYear: true,
            yearRange: '2000:' + new Date().getFullYear().toString(),
            //                yearRange: "2010:2017",
            dateFormat: 'dd/mm/yy'
        });
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Mail - Status</h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" Text="From Date" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtStartDate" runat="server"
                                    CssClass="input" onkeypress="Calendar_enterBa(event);" onpaste="return false"
                                    TabIndex="9" Width="100%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtStartDate" CssClass="cal_Theme1"
                                    runat="server" />
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" Text="To Date" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtEndDate" runat="server"
                                    CssClass="input" onkeypress="Calendar_enterBa(event);" onpaste="return false"
                                    TabIndex="9" Width="100%"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEndDate" CssClass="cal_Theme1"
                                    runat="server" />
                            </div>

                        </div>

                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSF" runat="server" Text="View" CssClass="savebutton"
                                OnClick="btnSF_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <table align="right" style="margin-right: 5%">
                    <tr>
                        <td align="right">
                            <asp:Panel ID="pnlprint" runat="server" Visible="false">
                                <input type="button" id="btnPrint" value="Print" style="width: 60px; height: 25px"
                                    onclick="PrintGridData()" />
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="display-reportMaintable clearfix">
                    <div class="table-responsive" style="scrollbar-width: thin;">
                        <asp:GridView ID="grdMail" runat="server" Width="95%" HorizontalAlign="Center"
                            AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                            CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt" PageSize="20" AllowPaging="true" OnPageIndexChanging="grdMail_PageIndexChanging">

                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdMail.PageIndex * grdMail.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mail Sent From">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="sfNameHidden" runat="server" Text='<%# Eval("sf_name") %>'></asp:Label>
                                        <asp:HiddenField ID="sfCodeHidden" runat="server" Value='<%# Eval("sf_code") %>' />
                                        <asp:HiddenField ID="slnoHidden" runat="server" Value='<%# Eval("trans_sl_no") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mail Sent to" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="center"></ItemStyle>

                                    <ItemTemplate>
                                        <asp:HiddenField ID="lblMailHidden" runat="server" Value='<%# Eval("cnt") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Subject" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left"></ItemStyle>

                                    <ItemTemplate>
                                        <asp:HiddenField ID="lblSubject" runat="server" Value='<%# Eval("Mail_Subject") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sent Date" ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle HorizontalAlign="left"></ItemStyle>

                                    <ItemTemplate>
                                        <asp:Label ID="lbltime" runat="server" Text='<%# Eval("mail_sent_time") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <EmptyDataRowStyle CssClass="no-result-area" />
                        </asp:GridView>
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
    </form>
</body>
</html>

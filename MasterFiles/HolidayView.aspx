<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayView.aspx.cs" Inherits="MasterFiles_HolidayView" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday View</title>
    <%--    <link type="text/css" rel="stylesheet" href="../css/style.css" />   --%>
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

                var SMonth = $('#<%=ddlState.ClientID%> :selected').text();
                if (SMonth == "---Select---") { alert("Select State."); $('#ddlState').focus(); return false; }

            });
        });
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

        @-webkit-keyframes blinker {
            from {
                opacity: 1.0;
            }

            to {
                opacity: 0.4;
            }
        }

        .clndrHldy {
            text-decoration: blink;
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 1s;
            -webkit-animation-iteration-count: infinite;
            -webkit-animation-timing-function: ease-in-out;
            -webkit-animation-direction: alternate;
        }
        .tablecell {
            padding:10px
        }
      
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server"></div>
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Holiday View</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-name-heading clearfix">
                            <div class="row clearfix">
                                <div class="col-lg-3">
                                    <asp:Label ID="lblstate" runat="server" Text="State:" Visible="false" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select" Visible="false">
                                    </asp:DropDownList>
                                    <%--  <asp:ListItem Text="TamilNadu" Value="1"></asp:ListItem>
                                     <asp:ListItem Text="Kerala" Value="2"></asp:ListItem>--%>
                                </div>

                                <div class="col-lg-3">
                                    <asp:Label ID="lblyear" runat="server" Text="Year" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlYr" runat="server" CssClass="nice-select">
                                        <%-- <asp:ListItem Text="1996" Value="1996"></asp:ListItem>
                                          <asp:ListItem Text="1997" Value="1997"></asp:ListItem>
                                           <asp:ListItem Text="2010" Value="2010"></asp:ListItem>
                                           <asp:ListItem Text="2014" Value="2014"></asp:ListItem>--%>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                    <asp:Button ID="btnGo" runat="server" Width="50px" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" />

                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />

                    <div class="display-table clearfix">
                        <div class="table-responsive" style="scrollbar-width: thin;">
                            <div>
                                <center>
                                    <table id="Table1" width="90%" style="vertical-align: top" visible="true" runat="server">
                                        <tr>
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal1" runat="server" BackColor="#F0F8FF" OnDayRender="CalJan_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" CellSpacing="1" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                       
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal2" runat="server" BackColor="#F0F8FF" OnDayRender="CalFeb_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="1996-02-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                             
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal3" runat="server" BackColor="#F0F8FF" OnDayRender="Calmar_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-03-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                        </tr>                                     
                                        <tr>
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal4" runat="server" BackColor="#F0F8FF" OnDayRender="Calapr_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-04-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                              
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal5" runat="server" BackColor="#F0F8FF" OnDayRender="Calmay_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-05-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td >
                                             
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal6" runat="server" BackColor="#F0F8FF" OnDayRender="Caljune_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-06-01" ToolTip="" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal7" runat="server" BackColor="#F0F8FF" OnDayRender="Caljuly_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-07-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                              
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal8" runat="server" BackColor="#F0F8FF" OnDayRender="Calaug_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-08-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                              
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal9" runat="server" BackColor="#F0F8FF" OnDayRender="Calsep_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-09-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal10" runat="server" BackColor="#F0F8FF" OnDayRender="Caloct_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-10-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                              
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal11" runat="server" BackColor="#F0F8FF" OnDayRender="Calnov_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-11-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                              
                                            <td class="tablecell">
                                                <asp:Calendar ID="Cal12" runat="server" BackColor="#F0F8FF" OnDayRender="Caldec_DayRender" BorderColor="#007bff"
                                                    BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                                    ForeColor="black" Height="300px" ShowGridLines="false" Width="300px" NextMonthText=""
                                                    PrevMonthText="" VisibleDate="2008-12-01" Enabled="False">
                                                    <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                                                    <SelectorStyle BackColor="#FFCC66" />
                                                    <OtherMonthDayStyle ForeColor="#CC9966" />
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                                                    <DayHeaderStyle BackColor="#CCCCFF" Font-Bold="True" Height="1px" />
                                                    <TitleStyle BackColor="#A6A6D2" Font-Bold="True" Font-Size="9pt" ForeColor="#0077ff" />
                                                </asp:Calendar>
                                            </td>
                                        </tr>
                                    </table>
                                </center>

                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <asp:Button ID="btnBack" CssClass="backbutton"
                Text="Back" runat="server" OnClick="btnBack_Click" />
        </div>
        <br />
        <br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>

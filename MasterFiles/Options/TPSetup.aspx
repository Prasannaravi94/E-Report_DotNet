<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TPSetup.aspx.cs" Inherits="MasterFiles_Options_TPSetup" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP Setup</title>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../JScript/DateJs/date.js"></script>
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
        /*.table td, .table th {
            padding: .35rem !important;
        }*/
        /*.css-serial td:first-child:before {
            counter-increment: serial-number; 
            content: counter(serial-number); 
        }*/
        #tblTPSetup > tbody > tr:nth-child(n) > td:nth-child(1),
        #tblTPSetup > tbody > tr:nth-child(n) > td:nth-child(2) {
            font-weight: 400;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $(' <div />');
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
    <script>
        var addSerialNumber = function () {
            var i = 0
            setTimeout(function () {
                $('#tblTPSetup > tbody > tr:nth-child(n).snoRow').each(function (index) {
                    $(this).find('td.sno').html(index+1);
                });
            }, 1000);
        };

        addSerialNumber();
    </script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server"></div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-9">
                        <h2 class="text-center">TP Setup</h2>
                        <div class="table-responsive">
                            <table id="tblTPSetup" class="table table-bordered" width="100%">
                                <tr>
                                    <th>s.no</th>
                                    <th>Description</th>
                                    <th style="text-align: center">Mode</th>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td>
                                    <td>Multiple Session Selection in TP is needed</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoAddsessionNeed" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td>
                                    <td>No. Of Sessions Count in TP</td>
                                    <td>
                                        <asp:TextBox ID="txtAddsessionCount" runat="server" CssClass="input" value="0" onkeypress="return event.charCode >= 48 && event.charCode <= 57" MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td>
                                    <td>Multiple Cluster Selection in TP is needed</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoClusterNeed" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td>
                                    <td colspan="2" style="text-align: center; font-weight: 500">Field work Selection </td>
                                </tr>
                                <tr>
                                    <tr>
                                        <td></td>
                                        <td> (i) Doctor Needed</td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoDrNeed" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td> (ii) Chemist Needed</td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoChmNeed" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td></td>
                                        <td> (iii) Stockist Needed</td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoStkNeed" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td> (iv) CIP Needed</td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoCip_Need" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td> (v) Hospital Needed</td>
                                        <td>
                                            <asp:RadioButtonList ID="rdoHospNeed" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">Yes</asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td>
                                    <td>JointWork selection in TP is Needed</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoJWneed" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td>
                                    <td>TP objective Selectable option need in TP</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdotp_objective" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td>
                                    <td>Maximum doctor selection in the fieldwork in TP</td>
                                    <td>
                                        <asp:TextBox ID="txtmax_doc" runat="server" CssClass="input" value="0" onkeypress="return event.charCode >= 48 && event.charCode <= 57" MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td>
                                     <td>Anyone Mandatory for Doctor/ Chemist/ stockist/ CIP/ Hospital selection in Fieldwork</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoFW_meetup_mandatory" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td> 
                                    <td>Based on Cluster Wise Filter for Drs/ Chemist/ CIP/ Stockist/ Hospital</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoclustertype" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Yes</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="snoRow">
                                    <td class="sno"></td>
                                    <td colspan="2" style="text-align: center; font-weight: 500">Editable in TP </td>
                                </tr>
                                <tr>
                                    <td class="sno"></td>
                                    <td> (i) Holiday worktype</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoHoliday_Editable" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="1">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="sno"></td>
                                    <td> (ii) WeekOff</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdoWeeklyoff_Editable" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="1">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="Save" OnClick="btnSubmitNew_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

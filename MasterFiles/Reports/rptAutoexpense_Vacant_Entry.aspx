<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptAutoexpense_Vacant_Entry.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Old/Resigned Expense Submission</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">

        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr) {
            var str = sfcode;
            var n = str.includes("MGR");
            if (n == true) {
                popUpObj = window.open("RptAutoExpense_Mgr_Vacant.aspx?sf_code=" + sfcode + "&mon=" + fmon + "&year=" + fyr,
    "ModalPopUp"//,
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=800," +
    //"height=600," +
    //"left = 0," +
    //"top=0"
    );
            }
            else {
                popUpObj = window.open("RptAutoExpense_Vacant.aspx?sf_code=" + sfcode + "&mon=" + fmon + "&year=" + fyr,
    "ModalPopUp"//,
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=800," +
    //"height=600," +
    //"left = 0," +
    //"top=0"
    );
            }
            popUpObj.focus();
        }

    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlSubdiv]").select2();
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
            $('#btnSubmit').click(function () {



                var Month = $('#<%=monthId.ClientID%> :selected').text();
                if (Month == "  Select Month  ") { alert("Select Month."); $('#monthId').focus(); return false; }

                var year = $('#<%=yearID.ClientID%> :selected').text();
                if (year == "  Select Year  ") { alert("Select Year."); $('#yearID').focus(); return false; }

                var ddlMonth = document.getElementById('<%=monthId.ClientID%>').value;

                var ddlYear = document.getElementById('<%=yearID.ClientID%>').value;


                var sf_code = document.getElementById('<%=ddlSubdiv.ClientID%>').value;



                showModalPopUp(sf_code, ddlMonth, ddlYear);


            });
        });
    </script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });

    </script>
    <script type="text/javascript">


        // LoadModalDiv();

        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlSubdiv]');
            var $items = $('select[id$=ddlSubdiv] option');

            $txt.on('keyup', function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                       function (n) {
                           return exp.test($(n).text());
                       });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                       );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });




    </script>
    <style type="text/css">
        .display-table1 {
            color: #636d73;
            font-size: 12px;
            font-weight: 400;
            text-transform: uppercase;
        }

            .display-table1 .table {
                margin-bottom: 0rem;
            }

                .display-table1 .table th {
                    padding: 5px 5px;
                    border-bottom: 5px solid #fff;
                    border-top: 0px;
                    font-weight: 400;
                    text-align: center;
                    border-left: 1px solid #DCE2E8;
                    vertical-align: inherit;
                }

                    .display-table1 .table th:first-child {
                        font-weight: 500;
                        text-align: left;
                    }

                    .display-table1 .table th:last-child {
                        border-radius: 0px 8px 8px 0px;
                    }

                .display-table1 .table th {
                    background-color: #FFEFD5;
                }

                .display-table1 .table tr:nth-child(2) td:first-child {
                    background-color: #f1f5f8;
                }


                .display-table1 .table td {
                    border-left: 1px solid #DCE2E8;
                    vertical-align: inherit;
                }

                .display-table1 .table tr td:first-child {
                    background-color: #f1f5f8;
                    text-align: center;
                    border: 0px;
                    padding: 5px 5px;
                }

            .display-table1 tr td a {
                color: #1584fb;
                font-size: 12px;
                font-weight: 500;
                padding-right: 4px;
            }

            .display-table1 #expGrid {
                margin-bottom: 1.5rem !important;
            }

        .display-table1-no-result-area {
            border: solid 1px #9aa3a9;
            text-align: center;
            padding: 10px;
            color: #696d6e;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }
    </style>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server"></div>
            <div>
                <div class="container home-section-main-body position-relative clearfix">
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <h2 class="text-center" style="border-bottom: none">Old/Resigned Expense Process</h2>
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblSubdiv" runat="server" CssClass="label" Text="FieldForce Name"></asp:Label>
                                    <asp:DropDownList ID="ddlSubdiv" runat="server" Visible="true" CssClass="custom-select2 nice-select" Width="100%">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnSF" runat="server" Text="Go" CssClass="savebutton" Visible="true"
                                    OnClick="btnSF_Click" />
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <center>
                                <div id="MnthId" runat="server" visible="false">
                                    <table>
                                        <tr>
                                            <td align="left" id="stdt" runat="server"></td>
                                        </tr>
                                        <tr style="margin-bottom: 10px;">
                                            <td align="left" id="enddt" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div class="designation-reactivation-table-area clearfix">
                                                    <br />
                                                    <div class="display-table1 clearfix">
                                                        <div class="table-responsive">
                                                            <center>
                                                                <asp:GridView ID="ExpSubmitgrdvw" runat="server" Width="85%" HorizontalAlign="Center"
                                                                    AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                                                    CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Month(submitted)" ItemStyle-HorizontalAlign="Left">
                                                                            <ControlStyle></ControlStyle>
                                                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center" Wrap="false"></ItemStyle>
                                                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmnthyr" runat="server" Text='<%# Bind("mn_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataRowStyle CssClass="display-table1-no-result-area" />
                                                                </asp:GridView>
                                                            </center>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <div class="designation-reactivation-table-area clearfix">
                                                    <div class="display-table1 clearfix">
                                                        <div class="table-responsive">
                                                            <center>
                                                                <asp:GridView ID="ExpNotSubmitgrdvw" runat="server" Width="85%" HorizontalAlign="Center"
                                                                    AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                                                    CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Month(Not Submitted)" ItemStyle-HorizontalAlign="Left">
                                                                            <ControlStyle></ControlStyle>
                                                                            <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9" HorizontalAlign="center" Wrap="false"></ItemStyle>
                                                                            <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#9AA3A9"></HeaderStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblmnthName" runat="server" Text='<%# Bind("mn_Name") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataRowStyle CssClass="display-table1-no-result-area" />
                                                                </asp:GridView>
                                                            </center>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                            <td align="left" class="stylespc">
                                                <asp:Label ID="lblMonth" runat="server" CssClass="label" Text="Month"></asp:Label>
                                            </td>
                                            <td align="left" class="stylespc">
                                                <asp:DropDownList ID="monthId" runat="server"></asp:DropDownList></td>
                                            <td align="left" class="stylespc">
                                                <asp:Label ID="lblYr" runat="server" CssClass="label" Text="Year"></asp:Label>
                                            </td>
                                            <td align="left" class="stylespc">
                                                <asp:DropDownList ID="yearID" runat="server"></asp:DropDownList>
                                            </td>
                                            <td class="stylespc">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Go" CssClass="savebutton" Visible="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>

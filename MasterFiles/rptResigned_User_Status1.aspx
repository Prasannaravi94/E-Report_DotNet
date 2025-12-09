<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptResigned_User_Status1.aspx.cs"
    Inherits="MasterFiles_rptResigned_User_Status1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../../assets/css/nice-select.css" />
    <link rel="stylesheet" href="../../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../../assets/css/style.css" />
    <script src="../../../assets/js/jQuery.min.js" type="text/javascript"></script>
    <script src="../../../assets/js/jquery.nice-select.min.js" type="text/javascript"></script>
    <script src="../../../assets/js/main.js" type="text/javascript"></script>

    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, month, year, level, sf_Name) {
            popUpObj = window.open("Report/rptTPView.aspx?sf_code=" + sfcode + "&cur_month=" + month + "&cur_year=" + year + "&level=" + level + "&sf_name=" + sf_Name,
     "_blank",
    "ModalPopUp" //+
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=700," +
    //"height=500," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }

        function showModal(sfcode, month, year, sf_Name) {
            popUpObj = window.open("Reports/Rpt_DCR_View.aspx?sf_code=" + sfcode + "&cur_month=" + month + "&cur_year=" + year + "&Mode=  Detailed View" + "&sf_name=" + sf_Name,
     "_blank",
    "ModalPopUp" //+
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=700," +
    //"height=500," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }
        function show(sfcode, month, year, month, year, sf_Name, Detailed) {
            popUpObj = window.open("LeaveResigned.aspx?sfcode=" + sfcode + "&FMonth=" + month + "&FYear=" + year + "&TMonth=" + month + "&TYear=" + year + "&sf_name=" + sf_Name + "&Detailed=" + Detailed,
     "_blank",
    "ModalPopUp" //+
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=700," +
    //"height=500," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }
        function showInputStatus(sfcode, month, year, month, year, sf_Name) {
            popUpObj = window.open("rptinputstatus_New2_New.aspx?sfcode=" + sfcode + "&FMonth=" + month + "&FYear=" + year + "&TMonth=" + month + "&TYear=" + year + "&sf_name=" + sf_Name,
     "_blank",
    "ModalPopUp" //+
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=700," +
    //"height=500," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }

        function showExpenseStatus(sfcode, month, year, sf_Name, dcr_StartDate, dcr_EndDate) {
            popUpObj = window.open("RptAutoExpense_Vacant.aspx?sf_code=" + sfcode + "&cur_month=" + month + "&cur_year=" + year + "&Mode=  Detailed View" + "&sf_name=" + sf_Name + "&DcrStartDate=" + dcr_StartDate + "&DcrEndDate=" + dcr_EndDate,
     "_blank",
    "ModalPopUp" //+
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=700," +
    //"height=500," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }

        function showPayslipStatus(sfcode, month, year, sf_Name, dcr_StartDate, dcr_EndDate, emp_code) {
            popUpObj = window.open("Frm_Tablets_PaySlipView.aspx?sf_code=" + sfcode + "&cur_month=" + month + "&cur_year=" + year + "&Mode=  Detailed View" + "&sf_name=" + sf_Name + "&DcrStartDate=" + dcr_StartDate + "&DcrEndDate=" + dcr_EndDate + "&Emp_Code=" + emp_code,
     "_blank",
    "ModalPopUp" //+
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=700," +
    //"height=500," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }



    </script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .rptCellBorder {
            border: 1px solid;
            border-color: #999999;
        }

        .remove {
            text-decoration: none;
        }
    </style>
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
            $('#btnSubmit').click(function () {
                var SName = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (SName == "---Select---") { alert("Select Month  / Year."); $('#ddlMonth').focus(); return false; }


                var ddlmonthyear = document.getElementById('<%=ddlMonth.ClientID%>').value;


                var str = ddlmonthyear;

                var a1 = new Array();

                a1 = str.split("-");

                var month1 = a1[0];
                var year1 = a1[1];

                var sf_Name = $('#<%=lblsf_Name.ClientID%>').text();
                var sfcode = $('#<%=lblsfcode.ClientID%>').text();
                var tp_DCR = $('#<%=lbltpdcr.ClientID%>').text();
                var dcr_StartDate = $('#<%=lbldcrstart_Date.ClientID%>').text();
                var emp_code=$('#<%=lblemployeecode.ClientID%>').text();
                var dcr_EndDate = $('#<%=lbldcrend_Date.ClientID%>').text();
                var month = $('#<%=lblmonth.ClientID%>').text();
                var year = $('#<%=lblyear.ClientID%>').text();

                if (tp_DCR == 'TP') {
                    showModalPopUp(sfcode, month1, year1, -1, sf_Name);
                }
                else if (tp_DCR == 'DCR') {
                    showModal(sfcode, month1, year1, sf_Name);
                }
                else if (tp_DCR == 'Leave') {
                    show(sfcode, month1, year1, month1, year1, sf_Name, 0);
                }
                else if (tp_DCR == 'InputStatus') {
                    showInputStatus(sfcode, month1, year1, month1, year1, sf_Name)
                }
                else if (tp_DCR == 'ExpenseStatus') {
                    showExpenseStatus(sfcode, month1, year1, sf_Name, dcr_StartDate, dcr_EndDate)
                }
                else if (tp_DCR == 'Paylsip') {
                    showPayslipStatus(sfcode, month1, year1, sf_Name, dcr_StartDate, dcr_EndDate, emp_code)
                }
            });
        });
    </script>

    <style type="text/css">
        .display-Approvaltable .table tr:first-child td:first-child {
            background-color: #F1F5F8;
            color: #636d73;
            font-size: 14px;
            font-weight: 400;
        }

        .display-Approvaltable .table tr:nth-child(3) td:first-child {
            background-color: white;
            color: #636d73;
            font-size: 12px;
        }

        .display-Approvaltable .table tr:first-child, .display-Approvaltable .table tr:nth-child(2) {
            border-left: none;
        }

        .display-Approvaltable .table tr td:first-child {
            background-color: white;
            border-top: 1px solid #dee2e6;
        }

        .display-Approvaltable .table tr:first-child td:first-child {
            border-top: none;
        }
    </style>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div class="row justify-content-center">
                        <div class="col-lg-9">
                            <center>
                                <h2 class="text-center">
                                    <asp:Label ID="lblHead" Text="TP View for Resigned User" runat="server"></asp:Label></h2>
                            </center>
                        </div>
                        <div class="col-lg-3">
                            <asp:Panel ID="pnlbutton" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td></td>

                                        <td align="right">
                                            <table>
                                                <tr>
                                                    <td style="padding-right: 40px">
                                                        <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent();">
                                                            <asp:Image ID="Image4" runat="server" ImageUrl="../../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                                        </asp:LinkButton>
                                                        <asp:Label ID="Label5" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <br />
                <div class="container clearfix" style="max-width: 1350px;">
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-name-heading text-center clearfix">
                                    <div class="d-inline-block division-name">
                                        <asp:Label ID="Label1" runat="server" Text="Month  / Year"></asp:Label>
                                    </div>
                                    <div class="d-inline-block align-middle">
                                        <div class="single-des-option">
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row" style="display: none">
                                    <div class="col-lg-12">
                                        <asp:Label ID="lblsf_Name" runat="server" SkinID="lblMand"></asp:Label>
                                        <asp:Label ID="lblsfcode" runat="server" SkinID="lblMand"></asp:Label>
                                        <asp:Label ID="lbltpdcr" runat="server" SkinID="lblMand"></asp:Label>
                                         <asp:Label ID="lbldcrstart_Date" runat="server" SkinID="lblMand"></asp:Label>
                                        <asp:Label ID="lbldcrend_Date" runat="server" SkinID="lblMand"></asp:Label>
                                        <asp:Label ID="lblmonth" runat="server" SkinID="lblMand"></asp:Label>
                                        <asp:Label ID="lblyear" runat="server" SkinID="lblMand"></asp:Label>
                                        <asp:Label ID="lblemployeecode" runat="server" SkinID="lblMand"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div style="text-align: center">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="View" />
                                    <br />
                                    <br />
                                    <asp:Label ID="lblnorecords" runat="server" Visible="false" Font-Bold="true" Font-Size="14px" Text="No Records Found For Selected Month and Year"></asp:Label>
                                    <br />
                                </div>
                                <br />
                                <div style="text-align: center">
                                    <asp:Label ID="Label2" runat="server" Visible="false" CssClass="reportheader" Text="Leave Details"></asp:Label>
                                </div>
                                <br />
                                <div class="display-Approvaltable clearfix" align="center">
                                    <div class="table-responsive overflow-x-none" align="center">
                                        <asp:GridView ID="GrdFixation" Visible="false" runat="server" AlternatingRowStyle-CssClass="alt"
                                            AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                            GridLines="None" HorizontalAlign="Center" OnRowCreated="GrdFixation_RowCreated"
                                            ShowHeader="False" Width="100%" OnRowDataBound="GrdFixation_RowDataBound">

                                            <Columns>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                <br />
                <br />
                <%--     <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>--%>
            </div>
			<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>
    </form>
</body>
</html>

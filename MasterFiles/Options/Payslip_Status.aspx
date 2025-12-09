<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payslip_Status.aspx.cs" Inherits="MasterFiles_AnalysisReports_Payslip_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payslip Status</title>
    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
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

                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Name == "---Select---") {
                    alert("Select Fieldforce Name."); $('#ddlFieldForce').focus();
                    return false;
                }
                else if (Year == "---Select---") {
                    alert("Select Year.");
                    $('#ddlYear').focus();
                    return false;
                }
                else if (Month == "---Select---") {
                    alert("Select Month.");
                    $('#ddlMonth').focus();
                    return false;
                }
                else {

                    var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                    var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                    var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;

                    var DivCode = $("#hdnDivCode").val();

                    showModalPopUp(sf_Code, Month1, Year1, Name);
                }



                //  showModalPopUp(sf_Code, Month1, Year1, Name);


            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#hdnDivCode").val("");
            $.ajax({
                type: "POST",
                url: "Payslip.aspx/GetDivision_Code",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //alert(data.d);
                    $("#hdnDivCode").val(data.d);
                },
                error: function (res) {
                }
            });
        });
    </script>
    <script type="text/javascript">
        var popUpObj;
      
           
        function showModalPopUp(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Payslip_Status.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&sf_name=" + sf_name,
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
            popUpObj.focus();

        }
      
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server"></div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">Payslip Status</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <input type="hidden" id="hdnDivCode" />
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                           <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select"></asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select"></asp:DropDownList>
                        </div>
                       
                        <div class="single-des clearfix">
                            <asp:Label ID="lblMoth" runat="server" Text="Month" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select">
                                <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblToYear" runat="server" Text="Year" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>

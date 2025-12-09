<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payslip.aspx.cs" Inherits="MasterFiles_AnalysisReports_Payslip" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Payslip View</title>
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

                    showModalPopUp(sf_Code, Month1, Year1, DivCode);
                }



                //  showModalPopUp(sf_Code, Month1, Year1, Name);


            });

            $('#btnSubmit1').click(function () {

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

                    var emp_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                    var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                    var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
                    var checkvacant = document.getElementById('<%=chckvacant.ClientID%>');
                    if (checkvacant.checked == true) {
                        var checkvacant1 = document.getElementById('<%=chckvacant.ClientID%>').value;
                        var chckvacant1 = checkvacant1;
                    }

                    var DivCode = $("#hdnDivCode").val();

                    showModalPopUp1(emp_Code, Month1, Year1, DivCode,chckvacant1);
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
       var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp(sf_Code, Month1, Year1, DivCode) {
           
            popUpObj = window.open("Frm_Tablets_PaySlipView.aspx?SF_Code=" + sf_Code + "&Month=" + Month1 + "&Year=" + Year1 + "&div_code=" + DivCode,
               "ModalPopUp" + randomnumber
                                   // "ModalPopUp",
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
 <script type="text/javascript">
        var popUpObjV;
        var randomnumber1 = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp1(emp_Code, Month1, Year1, DivCode, chckvacant1) {

            popUpObjV = window.open("Frm_Tablets_PaySlipView.aspx?EMP_CODE=" + emp_Code + "&Month=" + Month1 + "&Year=" + Year1 +  "&div_code=" + DivCode + "&Vacant=" + chckvacant1,
     "ModalPopUp" + randomnumber1
                                   // "ModalPopUp",
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
            popUpObjV.focus();


        }
    </script>
 <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
                                    <h2 class="text-center">Payslip View</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <input type="hidden" id="hdnDivCode" />
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="nice-select">
                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" CssClass="nice-select">
                            </asp:DropDownList>
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
                        <asp:CheckBox ID="chckvacant" value="1" Text="Vacant" OnCheckedChanged="OnCheckBox_Changed" Visible="false" AutoPostBack = "true" runat="server" />
                    </div>
                      <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btngo" runat="server" Text="Go" Visible="false" CssClass="savebutton"  OnClick="btngo_Click" />
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
                    </div>
                       <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnSubmit1" runat="server" Text="View" CssClass="savebutton" Visible="false"/>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptRoutePlan.aspx.cs" Inherits="Reports_rptRoutePlan" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Route Plan View</title>
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, sf_name, mgr) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptRoutePlanView.aspx?sf_code=" + sfcode + " &sf_name=" + sf_name + " &mgract=" + mgr,
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
            // LoadModalDiv();
        }
    </script>
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

                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();

                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }

                var SMr = $('#<%=ddlMR.ClientID%> :selected').text();

                if (SName != '') {

                    var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                }


                // var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                var mr_code = document.getElementById('<%=ddlMR.ClientID%>').value;

                if (mr_code == -1) {

                    showModalPopUp(sf_Code, SName, 2);
                }

                else if (sf_Code != -1 && sf_Code != 0 && SName != '') {

                    showModalPopUp(mr_code, SMr, 3)
                }
                else {

                    showModalPopUp(mr_code, SMr, 3)
                }
                //                  else 
                //                  {
                //                      showModalPopUp(mr_code, SMr, 3);
                //                  }

            });
        });
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
    <style type="text/css">
        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }
    </style>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>

</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <div id="DivMenu" runat="server" style="margin-top:-25px; height:200px;">
            <br />
        </div>

        <link href="../../assets/css/style.css" rel="stylesheet" />
        <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">Route Plan View</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFF" runat="server" Text="Filter By" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" CssClass="nice-select" Visible="false">
                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" CssClass="nice-select">
                            </asp:DropDownList>

                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select"></asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblMR" runat="server" Text="Base Level" CssClass="label" Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlMR" runat="server" CssClass="custom-select2 nice-select" Visible="false" Width="100%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF1" runat="server" Visible="false" CssClass="nice-select"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <br />
                        <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton" Text="View" />
                    </div>
                </div>
            </div>
        </div>
		<script type="text/javascript">
        if ('<%= Session["Div_color"]!= null%>' == 'False') {
            document.body.style.backgroundColor = '#e8ebec';
        } else {
            document.body.style.backgroundColor = '<%= Session["Div_color"] %>'
        }
    </script>
    </form>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
</body>
</html>

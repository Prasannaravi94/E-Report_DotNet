<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Not_At_All_Visit_Drs.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Not_At_All_Visit_Drs" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Not at All Visit Drs</title>
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
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, sf_name, mode) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Not_At_All_Visit_Drs.aspx?sf_code=" + sfcode + "&sf_name=" + sf_name + " &mode=" + mode,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s18.postimg.org/84jvcfa3d/loading_18_ook.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:100px; height: 100px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
            // LoadModalDiv();
        }
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

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


                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Clear---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); return false; }
                var mode = document.getElementById('<%=ddlMode.ClientID%>').value;

                if (mode == "--Select--") { alert("Select Mode."); $('#ddlMode').focus(); return false; }
                var FieldForcecode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                showModalPopUp(FieldForcecode, FieldForce, mode);
            });
        });
    </script>

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
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Not at All Visit Drs</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <%--<asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    ToolTip="Enter Text Here"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                    <%--     <asp:ListItem Selected="True">---Select---</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>

                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMode" runat="server" CssClass="label" Text="Mode"></asp:Label>
                                <asp:DropDownList ID="ddlMode" runat="server" CssClass="nice-select" TabIndex="1">
                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="More than 6 Month" Value="6"></asp:ListItem>
                                    <asp:ListItem Text="More than 5 Month" Value="5"></asp:ListItem>
                                    <asp:ListItem Text="More than 4 Month" Value="4"></asp:ListItem>
                                    <asp:ListItem Text="More than 3 Month" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="More than 2 Month" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="More than 1 Month" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <%-- <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View"
                    BackColor="LightBlue" onclick="btnSubmit_Click" /></center>--%>
                            <asp:Button ID="btnSubmit" runat="server" Text="View"
                                CssClass="savebutton" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Core_drs_Status.aspx.cs" Inherits="MasterFiles_AnalysisReports_Core_drs_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MVD Status</title>
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

        function showModalPopUp(sfcode,  sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("Rpt_Core_drs_Status.aspx?sf_code=" + sfcode + "&sf_name=" + sf_name,
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

                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                    showModalPopUp(sf_Code,  Name);
               

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
                        <h2 class="text-center">MVD Status</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" />
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


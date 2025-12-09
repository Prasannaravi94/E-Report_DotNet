<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stock_Product.aspx.cs" Inherits="Stock_Product" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sale - Stockistwise</title>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        var popUpObj;


        function showModalPopUp(Month1, Year1, FF, FName, Vmode) {
            popUpObj = window.open("rpt_Stock_Product_View.aspx?Invoice_Month=" + Month1 + "&Invoice_Year=" + Year1 + " &sf_code=" + FF + "&sf_name=" + FName + "&Vmode=" + Vmode,
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
// "top=0"
);
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s10.postimg.org/te70y4ctl/loading_12_ook.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:100px; height: 100px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');
            });
        }




        function showModalPopUp_HQ(Month1, Year1, FF, FName, Vmode) {
            popUpObj = window.open("rpt_Stock_Product_HQwise.aspx?Invoice_Month=" + Month1 + "&Invoice_Year=" + Year1 + " &sf_code=" + FF + "&sf_name=" + FName + "&Vmode=" + Vmode,
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

                var ImgSrc = "https://s10.postimg.org/te70y4ctl/loading_12_ook.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:100px; height: 100px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');
            });
        }

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
                            $('#btnGo').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

            $('#btnGo').click(function () {
                var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }


                var Year1 = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
                var FF = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                var mode = $('#<%=ddlmode.ClientID%> :selected').text();
                var Vmode = document.getElementById('<%=ddlmode.ClientID%>').value;


                if (Vmode.trim() == "0") {
                    showModalPopUp(Month1, Year1, FF, Name, Vmode);
                }
                else if (Vmode.trim() == "1") {
                    showModalPopUp_HQ(Month1, Year1, FF, Name, Vmode);
                }

            });
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
        <div id="Divid" runat="server">
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <h2 class="text-center">Sales - Stockistwise</h2>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblYear" runat="server" Text="Year" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlFrmYear" runat="server" CssClass="nice-select">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblMonth" runat="server" Text="Month" CssClass="label"></asp:Label>
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
                            <asp:Label ID="lbMode" runat="server" Text="Mode" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select" AutoPostBack="true">
                                <asp:ListItem Value="0" Selected="True" Text="Stockist-Name Wise"></asp:ListItem>
                                <%--<asp:ListItem Value="1" Text="HQ-wise"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Text="Go" />
                    </div>
                </div>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.          
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

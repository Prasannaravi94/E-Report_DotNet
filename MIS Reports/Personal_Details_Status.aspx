<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Personal_Details_Status.aspx.cs"
    Inherits="MIS_Reports_Personal_Details_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Personal Detail - Status</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, sf_name, SName, detail) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptPersonal_Details_Status.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&SName=" + SName + "&detail=" + detail,
    "ModalPopUp"  //,
    //"toolbar=no," +
    //"scrollbars=yes," +
    //"location=no," +
    //"statusbar=no," +
    //"menubar=no," +
    //"addressbar=no," +
    //"resizable=yes," +
    //"width=900," +
    //"height=600," +
    //"left = 0," +
    //"top=0"
    );
            popUpObj.focus();
            //LoadModalDiv();

            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s9.postimg.org/95yy2iikf/triangle_square_animation_ook.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red; width:180px; margin:0 auto;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:600px; height: 400px;position: fixed;top: 10%;left: 15%;"  alt="" /></div>');

                // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
            });
        }

    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
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
        .button1 {
            border-top: 1px solid #96d1f8;
            background: #BBD823;
            background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#BBD823));
            background: -webkit-linear-gradient(top, #3e779d, #BBD823);
            background: -moz-linear-gradient(top, #3e779d, #BBD823);
            background: -ms-linear-gradient(top, #3e779d, #BBD823);
            background: -o-linear-gradient(top, #3e779d, #BBD823);
            padding: 5px 10px;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
            -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
            box-shadow: rgba(0,0,0,1) 0 1px 0;
            text-shadow: rgba(0,0,0,.4) 0 1px 0;
            color: white;
            font-size: 14px;
            font-family: Georgia, serif;
            text-decoration: none;
            vertical-align: middle;
        }

            .button1:hover {
                border-top-color: #f4ad42;
                background: #f4ad42;
                color: black;
            }

            .button1:active {
                border-top-color: #1b435e;
                background: #1b435e;
            }
    </style>
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
                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                var chkdetail = document.getElementById("chkdetail");

                var sf_name = SName.split('-');

                var detail = 0;

                if (chkdetail.checked) {
                    detail = 1
                }

                var sfcode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                showModalPopUp(sfcode, sf_name[0], SName, detail);


            });
        });
    </script>

    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />


            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Employee Personal Detail - Status</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblname" runat="server" CssClass="label" Text="Select the Fieldforce Name :"></asp:Label>
                                        <%--    <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                            ToolTip="Enter Text Here"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                        </asp:DropDownList>

                                        <div style="padding-top:20px">
                                        <asp:CheckBox ID="chkdetail" Text=" Consolidate" runat="server" />
                                        </div>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 18px">
                                        <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Width="50px" 
                                            Text="View" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>








            <br />
            <center>
                <div style="width: 100%">
                </div>
            </center>
        </div>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

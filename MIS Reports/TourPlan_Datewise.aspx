<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TourPlan_Datewise.aspx.cs"
    Inherits="MIS_Reports_TourPlan_Datewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tour Plan - Datewise</title>
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(cmon, cyear, SF_code, SName) {


            var cbValue = "";
            var cbTxt = "";
            var checked_checkboxes = $("[id*=chkdate] input:checked");
            var message = "";
            checked_checkboxes.each(function () {
                //var value = $(this).parent().attr('cbValue');
                //var text = $(this).closest("td").find("label").html();
                //message += "Text: " + text + " Value: " + value;
                //message += "\n";
                cbValue += $(this).parent().attr('cbValue') + ",";
            });

            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptTourPlan_Datewise.aspx?cmon=" + cmon + "&cyear=" + cyear + "&SF_code=" + SF_code + "&sf_name=" + SName + "&days=" + cbValue,
    "ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
            //LoadModalDiv();
        }



    </script>
    <style type="text/css">
        .box {
            background: #FFFFFF;
            border: 2px solid #7E8D29;
            border-radius: 8px;
        }
    </style>
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
            $('#btnGo').click(function () {

                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                <%--var TMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }--%>

                var ToMonYear = document.getElementById('<%=txtMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = ToMonYear[1];

                if ($("[id$=chkdate]").find("input:checked").length > 0) {
                    <%--var cmon = document.getElementById('<%=ddlMonth.ClientID%>').value;
                    var cyear = document.getElementById('<%=ddlYear.ClientID%>').value;--%>
                    var SF_code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;

                    showModalPopUp(Month1, Year1, SF_code, SName);
                }
                else {
                    alert('Select atleast 1 day.');
                    return false;
                }

            });
        });
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
    <%-- <script type="text/javascript">
          $(document).ready(function () {
              $("[id$=btnGo]").click(function () {
                  if ($("[id$=chkdate]").find("input:checked").length > 0) {
                      return true;
                  } else {
                      alert('Select atleast 1 day.');
                    
                  }
              });
          });
       </script>--%>

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
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Tour Plan - Datewise</h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    Visible="false" ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:LinkButton ID="linkcheck" runat="server" OnClick="linkcheck_Click">
                          <img src="../Images/Selective_Mgr.png" />
                                </asp:LinkButton>--%>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select"
                                    Visible="false">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false">
                                </asp:DropDownList>
                                <%-- <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;"
                                    runat="server" /><span style="font-family: Verdana; color: Red; font-weight: bold;">Loading
                                    Please Wait...</span>--%>
                            </div>
                            <%-- <div class="single-des clearfix">
                                <asp:Label ID="lblMR" runat="server" Text="Base Level" SkinID="lblMand" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Visible="false">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="single-des clearfix">
                                <%--                       <asp:Label ID="Label2" runat="server" CssClass="label" Text="Month"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
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
                                </asp:DropDownList>--%>
                                <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                            </div>
                            <%--      <div class="single-des clearfix">
                                <asp:Label ID="Label3" runat="server" CssClass="label"  Text="Year"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="lbldate" runat="server" CssClass="label" Text="Day">
                                </asp:Label>
                                <asp:CheckBoxList ID="chkdate" RepeatColumns="7" RepeatDirection="Horizontal" runat="server" Width="100%">
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnGo" runat="server" Text="View" CssClass="savebutton"
                                Enabled="false" />
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

                <script type="text/javascript" src="../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "M-yyyy",
                    viewMode: "months",
                    minViewMode: "months",
                    language: "tr"
                });
            });
        </script>
    </form>
</body>
</html>

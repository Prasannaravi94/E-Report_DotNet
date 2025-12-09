<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Drs_Add_Del_at_a_glance.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Drs_Add_Del_at_a_glance" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctors - Addition/Deactivation Status</title>
    <style type="text/css">
        .padding
        {
            padding: 3px;
        }
        .chkboxLocation label
        {
            padding-left: 5px;
        }
        td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("RptDrs_Add_Del_at_a_glance.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=800," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            // LoadModalDiv();
            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";
 
                var ImgSrc = "http://s11.postimg.org/47q6jab8j/konlang.gif"

 
 $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
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

                var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

                var TYear = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
                var TMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }


                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Year1 = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;
                var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;

                var FYea = $('#<%=ddlFrmYear.ClientID%> :selected').val();
                var TYea = $('#<%=ddlToYear.ClientID%> :selected').val();
                var frmMonth = $('#ddlFrmMonth').find(":selected").index();
                var ToMonth = $('#ddlToMonth').find(":selected").index();

                if ((frmMonth <= ToMonth && parseInt(FYea) === parseInt(TYea)) || (parseInt(FYea) < parseInt(TYea) && (frmMonth <= ToMonth || frmMonth >= ToMonth))) {
                    showModalPopUp(sf_Code, Month1, Year1, Year2, Month2, Name);
                }
                else {
                    alert("Select Valid Month & Year...");
                    $('#ddlFrmMonth').focus(); return false;
                }
            });
        }); 
    </script>
     <script type="text/javascript">


        function showLoader(loaderType) {



            if (loaderType == "Search") {
                document.getElementById("loaderSearch").style.display = '';
                document.getElementById("SPl").style.display = '';

            }

        }
    </script>
    <link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Doctors - Addition/Deactivation Status </h2>
     <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                                    AutoPostBack="true" onchange="showLoader('Search')">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select"></asp:DropDownList>
                                <img src="../../Images/loading/loading19.gif" style="display: none;" id="loaderSearch" />
                                <span id="SPl"
                                    style="color: Red; font-weight: bold; display: none; width: 200px">Please Wait....</span>
                            </div>
               
               <%-- <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFieldForce" runat="server"  SkinID="ddlRequired" >
                           
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>--%>
                 
                           <%-- <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name"></asp:Label>
                            <asp:DropDownList ID="ddlFieldForce" data-live-search="true" AutoPostBack="true"  
                             class="selectpicker" runat="server" Width="250px" >
                            </asp:DropDownList>
                                                      
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" ></asp:DropDownList>
                            <img src="../../Images/loading/loading19.gif" style="display: none;" id="loaderSearch" />
                            <span id="SPl"
                                style="font-family: Verdana; color: Red; font-weight: bold; display: none; width: 200px">Please Wait....</span>
                                </div>--%>

                       <%-- </td>
                         <td align="left" style="padding-bottom:10px;padding-right :10px;">
                    <asp:HiddenField ID="hdnBasedOn" runat="server" />
                    <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name" Visible="false" SkinID="lblMand"></asp:Label>
                </td>
                    </tr>--%>
              
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%">
                        <asp:Label ID="lblFMonth" runat="server" Text="From Month" CssClass="label"></asp:Label>
                       
                        <asp:DropDownList ID="ddlFrmMonth" runat="server" CssClass="nice-select">
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
                                <div style="float: right; width: 45%">
                        <asp:Label ID="lblFYear" runat="server" Text="From Year" CssClass="label" Width="60"></asp:Label>
                       
                        <asp:DropDownList ID="ddlFrmYear" runat="server" CssClass="nice-select"
                            Width="60">
                        </asp:DropDownList>
                     </div>
                            </div>

               
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%">
                        <asp:Label ID="lblTMonth" runat="server"  Text="To Month" CssClass="label"></asp:Label>
                        <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="nice-select">
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
                                <div style="float: right; width: 45%">
                        <asp:Label ID="lblTYear" runat="server" Text="To Year" Width="60" CssClass="label"></asp:Label>
                       
                        <asp:DropDownList ID="ddlToYear" runat="server" CssClass="nice-select"
                            Width="60">
                        </asp:DropDownList>
                    </div>
                            </div>
               </div>
           <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton"/>
                        </div>
           
           </div>
                    </div>
            </div>
    </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/js/bootstrap-select.min.js"></script>
    </form>
</body>
</html>

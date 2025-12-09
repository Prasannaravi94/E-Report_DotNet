<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGR_Working_Hrs_View.aspx.cs" Inherits="MasterFiles_AnalysisReports_MGR_Working_Hrs_View" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      <title>Fieldforce Working Hours</title>
       
        <style type="text/css">
         .padding
        {
            padding:3px;
        }
          .chkboxLocation label 
{  
    padding-left: 5px; 
}
    td.stylespc
        {
            padding-bottom:10px;
            padding-right :10px;
        }
    </style>
        <link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
      
        <script type="text/javascript">
            var popUpObj;

            function showModalPopUp(sfcode, fmon, fyr, sf_name) {
                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("Rpt_MGR_Working_Hrs_View.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + "&sf_name=" + sf_name,
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
                $(popUpObj.document.body).ready(function () {

                    //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                    //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";


                    var ImgSrc = "http://s11.postimg.org/47q6jab8j/konlang.gif"



                    $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
                    // $(popUpObj.document.body).append('<div><p>Loading Please Wait ....</p></div><div class="preload"> <img src="http://i.imgur.com/KUJoe.gif" style=" width: 100px; height: 100px;position: fixed;top: 50%;left: 50%;"></div>');
                });
            }
</script> 

<%--<script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>--%>
<script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
   
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
            if (Name == "---Select Clear---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
            var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
            if (FYear == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
            var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
            if (FMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }

              
            var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
            var Year1 = $('#<%=ddlYear.ClientID%> :selected').text();
            var Month1 = $('#ddlMonth').find(":selected").index();

            showModalPopUp(sf_Code, Month1, Year1, Name);
            
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
    <br />
    <center>
          <table>
               
                <tr>
                    <td align="left"  style="padding-bottom:10px;padding-right :10px;">
                        <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" style="padding-bottom:10px;padding-right :10px;width:80%">
                      <%-- <div class="row-fluid">
                        <asp:DropDownList ID="ddlFieldForce" data-live-search="true" class="selectpicker" runat="server"  SkinID="ddlRequired">
                       
                        </asp:DropDownList>
                        </div>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>--%>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>
                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true"
                                SkinID="ddlRequired" CssClass="custom-select2 nice-select" Visible="false">
                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                               SkinID="ddlRequired">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true"
                                CssClass="custom-select2 nice-select" Width="100%">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-bottom:10px;padding-right :10px;">
                        <asp:Label ID="lblFrmMoth" runat="server" Text="Month" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" style="padding-bottom:10px;padding-right :10px;">
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
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
                        
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-bottom:10px;padding-right :10px;">
                         <asp:Label ID="lblToYear" runat="server" Text="Year" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" style="padding-bottom:10px;padding-right :10px;">                         
                        <asp:DropDownList ID="ddlYear" runat="server" Width="80px" SkinID="ddlRequired">
                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
               
                 
            </table>
            <br />
            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View"
                BackColor="LightBlue"  />
       <%--     &nbsp
            <asp:Button ID="btnClear" runat="server" Width="70px" Height="25px" Text="Clear"
                CssClass="btnnew" OnClick="btnClear_Click" />--%>
         <!-- Bootstrap -->
        <script type="text/javascript" src="../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

        </center>

<%--    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
  <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
       
  <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/js/bootstrap-select.min.js"></script>--%>
    </div>
    
    </form>
</body>
</html>

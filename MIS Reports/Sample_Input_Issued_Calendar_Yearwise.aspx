<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample_Input_Issued_Calendar_Yearwise.aspx.cs" Inherits="MIS_Reports_Sample_Input_Issued_Calendar_Yearwise" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sample/Input Dump Financial Year Wise</title>
    <link type="text/css" rel="Stylesheet" href="../../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
      <link rel="stylesheet" href="../../assets/css/Calender_CheckBox.css" type="text/css" />
    <%-- <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <style type="text/css">
        .NoRecord {
            font-size: 10pt;
            font-weight: bold;
            color: Black;
            background-color: White;
        }

        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .marright {
            margin-left: 85%;
        }
    </style>

     <style type="text/css">
      
        .tr_Sno {
            background: #414d55;
            color: white;
            font-weight: 400;
            border-radius: 8px 0 0 8px;
            font-size: 12px;
            border-bottom: 10px solid #fff;
            font-family: Roboto;
            border-left: 0px solid #F1F5F8;
        }

        .tr_th {
            padding: 20px 15px;
            border-bottom: 10px solid #fff;
            border-top: 0px;
            font-size: 12px;
            font-weight: 400;
            text-align: center;
            border-left: 1px solid #DCE2E8;
            vertical-align: inherit;
            text-transform: uppercase;
        }
        .no-result-area 
        {
           border: solid 1px #d1e2ea;
           text-align: center;
           padding: 10px;
           color: #696d6e;
           font-size: 18px;margin-top:5px;
       }
        .display-callAvgreporttable .table tr:nth-child(2) td:first-child {
           background-color: #f1f5f8 !important;
           color:#636D73 !important;
         }
         
    </style>
   

    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
            $('#btnGo').click(function () {

                var mode = $('#<%=ddlmode.ClientID%> :selected').text();
                if (mode == "---Select---") { alert("Select Mode."); $('#ddlmode').focus(); return false; }

<%--                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }--%>




            });
        });
    </script>

     <link href="../../assets/css/select2.min.css" rel="stylesheet" />
     <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>


</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <br />
            <tr>
                                <td align="center">
                        <h2 class="text-center">Sample/Input Dump Financial Year Wise</h2></td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                           </tr>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <div class="designation-area clearfix">

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" > <%--OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                        AutoPostBack="true"--%>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblmode" runat="server" CssClass="label" Text="Select the Mode"></asp:Label>
                                    <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select" > <%--AutoPostBack="true" OnSelectedIndexChanged="ddlmode_SelectedIndexChanged"--%>
                                        <asp:ListItem Value="0" Text="Sample"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Input"></asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                                                        <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Financia Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFinancial" runat="server" CssClass="nice-select" > <%--AutoPostBack="true" OnSelectedIndexChanged="ddlmode_SelectedIndexChanged"--%>
                                        
                                    </asp:DropDownList>

                                </div>
                            </div>

<%--                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>

                                    <div class="row">
                                        <div class="col-lg-6" style="padding-bottom: 0px;">
                                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" Visible="false"
                                                CssClass="nice-select">
                                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-lg-6" style="padding-bottom: 0px;">

                                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" CssClass="nice-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select " Width="100%">
                                    </asp:DropDownList>
                                    <br /> 
                                    <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>


<%--                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-7">
                                        <asp:CheckBox ID="chkVacant" Text="With Vacant" Font-Size="Medium" Font-Names="Calibri"
                                            Checked="true" ForeColor="Red" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>--%>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix" style="padding-bottom: 20px;">

                            <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Text="Download" OnClick="btnGo_Click" />
                            <%--  <asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="View" CssClass="savebutton"
                                      OnClick="btnGo_Click" />--%>
                           
                        </div>
						<br />

                    </div>
                </div>
            </div>

            <br />
            <br />
<%--            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>--%>

        </div>
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

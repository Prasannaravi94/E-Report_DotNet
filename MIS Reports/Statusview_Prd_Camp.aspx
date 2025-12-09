<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statusview_Prd_Camp.aspx.cs"
    Inherits="MIS_Reports_Statusview_Prd_Camp" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Status View</title>
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
    <script type="text/javascript" language="javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript">
        function PrintGridData() {

            var prtGrid = document.getElementById('<%=tbl.ClientID %>');

            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, sf_name, sf_short, sf_hq) {
            popUpObj = window.open("Doctor_Prod_Map_Details.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&sf_hq=" + sf_hq,
    "ModalPopUp" //,
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
        }

        var popUpObj3;
        function showModalPopUp2(sfcode, sf_name) {
            popUpObj3 = window.open("rptHospital_Listeddr_Map.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name,
    "ModalPopUp" //,
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
            popUpObj3.focus();
            //LoadModalDiv();
            $(popUpObj3.document.body).ready(function () {
                var ImgSrc = "https://s4.postimg.org/j1heaetwt/loading16.gif"
                $(popUpObj3.document.body).append('<div><p style="color:blue;margin-top:10%;margin-left:40%;">Loading Please Wait....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:310px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }

        var popUpObj5;
        function showModalPopUp5(sfcode, sf_name, sf_short, sf_hq) {
            popUpObj5 = window.open("Listeddr_Chemists_Map.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&sf_hq=" + sf_hq + "&MR=0",
    "ModalPopUp" //,
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
            popUpObj5.focus();
            //LoadModalDiv();
        }

        var popUpObj1;
        function showModalPopUp1(sfcode, sf_name) {
            //popUpObj1 = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj1 = window.open("Core_drs_details.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name,
    "ModalPopUp" //,
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
            popUpObj1.focus();
            //LoadModalDiv();
        }

        var popUpObj2;
        function showModalPopUp3(sfcode, sf_name, sf_short, sf_hq) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj2 = window.open("geo_drs_details.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&sf_hq=" + sf_hq,
   "ModalPopUp" //,
   // "toolbar=no," //+
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
            popUpObj2.focus();
            //LoadModalDiv();
        }


        var popUpObj22;
        function showModalPopUp22(sfcode, sf_name, sf_short, sf_hq, Div_code) {
            popUpObj22 = window.open("Listeddr_Campaign_Map.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&Div_code=" + Div_code + "&sf_hq=" + sf_hq + "&MR=0" ,
    "ModalPopUp" //,
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
            popUpObj22.focus();
            //LoadModalDiv();
        }
        var popUpObjchm;
        function showModalPopUpchm(sfcode, sf_name, sf_short, sf_hq) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObjchm = window.open("geo_chm_details.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&sf_hq=" + sf_hq,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObjchm.focus();
            //LoadModalDiv();
        }

        function showModalPopUpCheMap(sfcode, sf_name, sf_short, sf_hq) {
            popUpObj27 = window.open("/MasterFiles/Chemist_Campaign/Chemist_Campaign_Map_Status.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&sf_hq=" + sf_hq + "&MR=0",
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObj27.focus();
            //LoadModalDiv();
        }

        var popUpObjstk;
        function showModalPopUpstk(sfcode, sf_name, sf_short, sf_hq) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObjstk = window.open("geo_stk_details.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&sf_hq=" + sf_hq,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
            popUpObjstk.focus();
            //LoadModalDiv();


          
                popUpObj.focus();
                //LoadModalDiv();
            

        }
    </script>
    <script type="text/javascript">
        var popUpObjVC;
        function showModalPopUp_VC(sfcode, sf_name, sf_short, sf_hq, Div_code, flg) {
            popUpObjVC = window.open("Statusview_VisitCard_view.aspx?sfcode=" + sfcode + "&sf_name=" + sf_name + "&sf_short=" + sf_short + "&Div_code=" + Div_code + "&sf_hq=" + sf_hq + "&flg=" + flg,
     "ModalPopUp",
     "toolbar=no," +
     "scrollbars=yes," +
     "location=no," +
     "statusbar=no," +
     "menubar=no," +
     "addressbar=no," +
     "resizable=yes," +
     "width=900," +
     "height=600," +
     "left = 0," +
     "top=0"
     );
            popUpObj.focus();
            //LoadModalDiv();
        }
    </script>
     <script type="text/javascript">
    var popUpObj10;
        function showModalPopUpcount(div_code, div_name,mode) {
            popUpObj10 = window.open("SampleInput_CountDetail.aspx?&Div_code=" + div_code + "&Div_name=" + div_name +  "&mode=" + mode,
                "ModalPopUp" //,
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
            popUpObj10.focus();
            //LoadModalDiv();
        }
         </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }




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

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <table>
                            <tr>
                                <td align="center">
                        <h2 class="text-center">Status View</h2></td>
                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                             <td align="right">
                                   <asp:LinkButton ID="LinkButton1" Text="Status" runat="server" ForeColor="#6B8E23" CssClass="blink_me"
                                            Font-Bold="true" Font-Names="Verdana" OnClick="linkstatuscount_click"></asp:LinkButton> 
                                </td>
                           </tr>
                                 </table>
                        <div class="designation-area clearfix">

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblmode" runat="server" CssClass="label" Text="Select the Mode"></asp:Label>
                                    <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select" AutoPostBack="true" OnSelectedIndexChanged="ddlmode_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Doctor Business Valuewise - Status"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Hospital - Listed Doctor Map"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Listed Doctor - Product Map"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Listed Doctor - Chemists Map"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Listed Doctor - Campaign Map"></asp:ListItem>
                                          <asp:ListItem Value="12" Text="Chemist - Campaign Map"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="SFC Updation"></asp:ListItem>
										<asp:ListItem Value="11" Text="Visiting Card/Registration Number - Status"></asp:ListItem>
                                    <%--    <asp:ListItem Value="4" Text="Manager - Core Drs Map"></asp:ListItem>--%>
                                        <asp:ListItem Value="9" Text="RCPA Entry - Status"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Geo - Tagged drs - Status"></asp:ListItem>
										
                                    </asp:DropDownList>

                                </div>
                            </div>

                            <div class="single-des clearfix">
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
                            </div>


                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-7">
                                        <asp:CheckBox ID="chkVacant" Text="With Vacant" Font-Size="Medium" Font-Names="Calibri"
                                            Checked="true" ForeColor="Red" runat="server" Visible="false" />
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix" style="padding-bottom: 20px;">

                            <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Text="View" OnClick="btnGo_Click" />
                            <%--  <asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="View" CssClass="savebutton"
                                      OnClick="btnGo_Click" />--%>
                           
                        </div>
						<br />
            <asp:Panel ID="pnlbutton" runat="server">
                <table width="100%">
                    <tr>
                        <td width="80%"></td>
                        <td align="Center">
                            <table>
                                <tr>

                                    <td>
                                        <asp:Button ID="btnExcel2" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                             BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="linkDetail" Text="All" runat="server" ForeColor="#6B8E23" CssClass="blink_me"
                                            Font-Bold="true" Font-Names="Verdana" OnClick="linkDetail_Click"></asp:LinkButton></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

                    </div>
                </div>

                <div class="row ">
                    <div class="col-lg-12">
                        <asp:Panel ID="pnlprint" runat="server" CssClass="panelmarright" Visible="false">
                            <%--  <input type="button" id="btnPrint" value="Print" style="width:60px;height:25px; background-color:LightBlue; "    />--%>
                            <asp:LinkButton ID="lnkPrint" ToolTip="Print" runat="server" OnClientClick="PrintGridData()">
                                <asp:Image ID="Image3" runat="server" ImageUrl="../../assets/images/Printer.png" ToolTip="Print"
                                    Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClientClick="RefreshParent();">
                                <asp:Image ID="Image2" runat="server" ImageUrl="../../assets/images/Excel.png" ToolTip="Excel"
                                    Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="imgpdf" ToolTip="Pdf" runat="server" OnClick="btnPDF_Click" Visible="false">
                                <asp:Image ID="Image1" runat="server" ImageUrl="../../assets/images/pdf.png" ToolTip="Pdf"
                                    Width="30px" Style="border-width: 0px;" />
                            </asp:LinkButton>
                        </asp:Panel>

                    </div>
                </div>

                <div class="row justify-content-center">
                    <div class="col-lg-11">

                        <asp:Panel ID="pnlContents" runat="server">
                            <div class="display-callAvgreporttable clearfix">

                                <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                    <asp:Table ID="tbl" runat="server" GridLines="None" CssClass="table"
                                        Width="100%">
                                    </asp:Table>
                                    <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue"
                                        Visible="false" Height="20px"  BorderStyle="Solid" BorderWidth="2"
                                        Font-Bold="True">No Records Found</asp:Label>
                                </div>

                            </div>
                        </asp:Panel>
                       <center>
                    <table width="60%">
                        <caption>
                             <asp:Panel ID="pnlContents1" runat="server">
                           <%-- <asp:GridView ID="GrdFFcount"  runat="server" AlternatingRowStyle-CssClass="alt"
                                AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found"
                                GridLines="None" HorizontalAlign="Center" BorderWidth="0"
                                OnRowCreated="GrdFFcount_RowCreated"  OnRowDataBound="GrdFFcount_RowDataBound"
                                ShowHeader="False" Width="100%">

                                <Columns>
                                </Columns>
                                <EmptyDataRowStyle CssClass="no-result-area" />
                            </asp:GridView>--%>
                                 <asp:GridView ID="GrdFFcount" runat="server" AlternatingRowStyle-CssClass="alt" 
                                AutoGenerateColumns="true" CssClass="mGrids" EmptyDataText="No Records Found" 
                                GridLines="Both" HorizontalAlign="Center"  BorderWidth="1"
                                OnRowCreated="GrdFFcount_RowCreated" onrowdatabound="GrdFFcount_RowDataBound" 
                                ShowHeader="False" Width="98%" Font-Names="calibri" Font-Size="Small">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt" />
                                <RowStyle HorizontalAlign="left" VerticalAlign="Middle" Font-Size="Small" Font-Names="calibri" />
                                <Columns>
                                </Columns>
                                <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" 
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" ForeColor="Black" 
                                    Height="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:GridView>
                             </asp:Panel>
                        </caption>
                    </table>
                           </center>
                </div>
                  
                </div>

            </div>

            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>

        </div>
         <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

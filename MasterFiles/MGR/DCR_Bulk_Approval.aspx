<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Bulk_Approval.aspx.cs"
    Inherits="MasterFiles_MGR_DCR_Bulk_Approval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>DCR - Approval</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="../../images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="../../assets/css/nice-select.css">
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/css/style.css">
    <link rel="stylesheet" href="../../assets/css/responsive.css">
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" type="text/css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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

        #btnBack {
            margin-left: 0px;
        }
    </style>
    <script type="text/javascript">
        function checkAll() {
            var grid = document.getElementById('<%= grdDCR.ClientID %>');
            if (grid != null) {
                var inputList = grid.getElementsByTagName("input");
                var cnt = 0;
                var index = '';
                var chkall = document.getElementById('grdDCR_ctl01_chkAll');
                var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                for (i = 2; i < inputList.length; i++) {
                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {
                        index = i.toString();
                    }
                    var chkapp = document.getElementById('grdDCR_ctl' + index + '_chkAppDCR');
                    var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');
                    if (chkall.checked) {
                        chkapp.checked = true;
                        chkrej.checked = false;
                        chkrejall.checked = false;

                        document.getElementById("btnReject").style.visibility = "hidden";
                        document.getElementById("btnApprove").style.visibility = "visible";
                    }
                    else {
                        chkapp.checked = false;

                        document.getElementById("btnReject").style.visibility = "hidden";
                        document.getElementById("btnApprove").style.visibility = "hidden";
                    }
                    if (chkrejall.checked) {
                        document.getElementById("btnReject").style.visibility = "visible";
                        document.getElementById("btnApprove").style.visibility = "hidden";
                    }
                }
            }
        }

        function checkAllRej() {
            var grid = document.getElementById('<%= grdDCR.ClientID %>');
            if (grid != null) {
                var inputList = grid.getElementsByTagName("input");
                var cnt = 0;
                var index = '';
                var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                var chkall = document.getElementById('grdDCR_ctl01_chkAll');
                for (i = 2; i < inputList.length; i++) {
                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {
                        index = i.toString();
                    }
                    var chkapp = document.getElementById('grdDCR_ctl' + index + '_chkAppDCR');
                    var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');
                    if (chkrejall.checked) {
                        chkrej.checked = true;
                        chkapp.checked = false;
                        chkall.checked = false;

                        document.getElementById("btnReject").style.visibility = "visible";
                        document.getElementById("btnApprove").style.visibility = "hidden";
                    }
                    else {

                        chkrej.checked = false;

                        document.getElementById("btnReject").style.visibility = "hidden";
                        document.getElementById("btnApprove").style.visibility = "hidden";
                    }
                    if (chkall.checked) {
                        document.getElementById("btnReject").style.visibility = "hidden";
                        document.getElementById("btnApprove").style.visibility = "visible";
                    }

                }
            }
        }
        function checkapp() {
            var grid = document.getElementById('<%= grdDCR.ClientID %>');

            if (grid != null) {

                var inputList = grid.getElementsByTagName("input");
                var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                var chkall = document.getElementById('grdDCR_ctl01_chkAll');
                var cnt = 0;
                var index = '';
                var Count = 0;
                var CountVisi = 0;

                for (i = 2; i < inputList.length; i++) {

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }


                    var chkapp = document.getElementById('grdDCR_ctl' + index + '_chkAppDCR');
                    var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');


                    if (chkapp.checked) {

                        document.getElementById("btnReject").style.visibility = "hidden";
                        document.getElementById("btnApprove").style.visibility = "visible";

                        CountVisi = CountVisi + 1;
                    }

                    else {
                        Count = Count + 1;
                    }

                    chkrej.checked = false;
                    chkrejall.checked = false;

                    if (Count > 0) {
                        chkall.checked = false;
                    }
                    else {
                        chkall.checked = true;
                    }
                    if (CountVisi == 0) {
                        document.getElementById("btnApprove").style.visibility = "hidden";
                    }
                }
            }
        }
        function checkrej() {
            var grid = document.getElementById('<%= grdDCR.ClientID %>');

            if (grid != null) {


                var inputList = grid.getElementsByTagName("input");
                var chkrejall = document.getElementById('grdDCR_ctl01_chkRejAll');
                var chkall = document.getElementById('grdDCR_ctl01_chkAll');
                var cnt = 0;
                var index = '';
                var Count = 0;
                var CountVisi = 0;

                for (i = 2; i < inputList.length; i++) {

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }

                    var chkapp = document.getElementById('grdDCR_ctl' + index + '_chkAppDCR');
                    var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');

                    if (chkrej.checked) {

                        document.getElementById("btnReject").style.visibility = "visible";
                        document.getElementById("btnApprove").style.visibility = "hidden";

                        CountVisi = CountVisi + 1;
                    }

                    else {

                        Count = Count + 1;
                    }
                    chkapp.checked = false;
                    chkall.checked = false;

                    if (Count > 0) {
                        chkrejall.checked = false;
                    }
                    else {
                        chkrejall.checked = true;
                    }

                    if (CountVisi == 0) {
                        document.getElementById("btnReject").style.visibility = "hidden";
                    }

                }
            }
        }
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
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
    </script>
    <script language="javascript" type="text/javascript">

        function validateTextBox() {
            // Get The base and Child controls
            var index = 0;

            var rowscount = document.getElementById("<%= grdDCR.ClientID %>").rows.length;
            var TargetBaseControl = document.getElementById('<%=this.grdDCR.ClientID%>');

            var TargetChildControl1 = "txtReason";
            var TargetChildControlCheck = "chkRjtDCR";
            // Get the all the control of the type Input in the basse contrl
            var inputList = TargetBaseControl.getElementsByTagName("input");
            // loop thorught the all textboxes


            var value = document.getElementById('<%=txtSetup.ClientID%>').innerHTML;


            var cnt = 0;
            var index = '';
            var index1 = '';
            var Count = 0;
            var validation = 0;
            var validationRej = 0;





            for (i = 2; i <= rowscount; i++) {
                if (i.toString().length == 1) {
                    index = cnt.toString() + i.toString();
                }
                else {

                    index = i.toString();
                }
                var chkapp = document.getElementById('grdDCR_ctl' + index + '_txtReason');
                var chkrej = document.getElementById('grdDCR_ctl' + index + '_chkRjtDCR');
                var chkApprove = document.getElementById('grdDCR_ctl' + index + '_chkAppDCR');


                if (chkrej.checked) {
                    if (chkapp.value == "") {

                        if (validation == 0) {
                            alert('Enter Reason For Rejection');
                            //  $('#TxtSrch').focus();
                            $(chkapp).focus();
                            document.getElementById('grdDCR_ctl' + index + '_TextBox1').style.visibility = "visible";
                            document.getElementById('grdDCR_ctl' + index + '_txtReason').style.backgroundColor = "#ffdab9";
                        }
                        else {
                            document.getElementById('grdDCR_ctl' + index + '_TextBox1').style.visibility = "hidden";
                            document.getElementById('grdDCR_ctl' + index + '_txtReason').style.backgroundColor = "#FFFFFF";
                        }
                        validation = validation + 1;
                    }
                }

                if (value == "Y") {
                    if (chkApprove.checked) {
                        if (chkapp.value == "") {
                            if (validationRej == 0) {
                                alert('Enter Reason For Approve!');
                                document.getElementById('grdDCR_ctl' + index + '_TextBox1').style.visibility = "visible";
                                //  $('#TxtSrch').focus();                    
                                $(chkapp).focus();
                                document.getElementById('grdDCR_ctl' + index + '_txtReason').style.backgroundColor = "#ffdab9";
                            }
                            else {
                                document.getElementById('grdDCR_ctl' + index + '_TextBox1').style.visibility = "hidden";
                                document.getElementById('grdDCR_ctl' + index + '_txtReason').style.backgroundColor = "#FFFFFF";
                            }
                            validationRej = validationRej + 1;
                        }
                    }
                }

                if (!chkApprove.checked && !chkrej.checked) {
                    document.getElementById('grdDCR_ctl' + index + '_TextBox1').style.visibility = "hidden";
                    document.getElementById('grdDCR_ctl' + index + '_txtReason').style.backgroundColor = "#FFFFFF";
                }
            }
            if (validationRej > 0 || validation > 0) {
                return false;
            }

        }
    </script>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>



</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div style="margin-left: 90%">
            <asp:Button ID="btnBack" runat="server" CssClass="savebutton" Text="Back" OnClick="btnBack_Click" />
            <%--<asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />--%>
        </div>
        <br />
        <div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <table id="tblPreview_LstDoc" runat="server" style="width: 100%;" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <h2 class="text-center">
                                        <asp:Label ID="txtSetup" runat="server" ForeColor="White"></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">
                                        <asp:Label ID="lblText" runat="server" Text="Daily Call Report - Approval/Reject For "></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                        </table>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdDCR" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt"
                                        OnRowDataBound="grdDCR_RowDataBound">
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="  Approve All" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAppDCR" runat="server" onclick="checkapp(this); " Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkRejAll" runat="server" Text="  Reject All" onclick="checkAllRej(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRjtDCR" runat="server" onclick="checkrej(this);" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="trans_slno" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltrans_slno" runat="server" Text='<%#Eval("trans_slno")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:HyperLinkField HeaderText="Activity Date"  DataTextField="Activity_Date" 
                                    DataNavigateUrlFormatString="~/MasterFiles/MR/DCR/MR_DCR_Approval.aspx?sfcode={0}&amp;trans_slno={1}&amp;Activity_Date={2}"
                                    DataNavigateUrlFields="SF_Code,trans_slno,Activity_Date" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>  
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                </asp:HyperLinkField> --%>
                                            <asp:HyperLinkField HeaderText="Activity Date" DataTextField="Activity_Date" Target="_blank"
                                                DataNavigateUrlFormatString="~/MasterFiles/Reports/rptDCRViewApprovedDetails.aspx?sf_code={0}&amp;trans_slno={1}&amp;Month={2}&amp;Year={3}&amp;Day={4}&amp;div_code={5}"
                                                DataNavigateUrlFields="SF_Code,trans_slno,MActDate,YActDate,DActDate,division_code"
                                                ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="Activedate" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActdate" runat="server" Text='<%#Eval("Activity_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MActDate" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMActDate" runat="server" Text='<%#Eval("MActDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="YActDate" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblYActDate" runat="server" Text='<%#Eval("YActDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DActDate" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDActDate" runat="server" Text='<%#Eval("DActDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="division_code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldivision_code" runat="server" Text='<%#Eval("division_code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Plan">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerr" runat="server" Text='<%#Eval("Plan_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWork" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Doc Met">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocMet" runat="server" Text='<%#Eval("doc_cnt")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Chem Met">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChemMet" runat="server" Text='<%#Eval("che_cnt")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Stockist Met">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockMet" runat="server" Text='<%#Eval("stk_cnt")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hospitals Met">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblhosMet" runat="server" Text='<%#Eval("hos_cnt")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unlisted Doc Met">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNewDocMet" runat="server" Text='<%#Eval("unlst_cnt")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblRemarks" runat="server" Width="250px" Text='<%#Eval("Remarks")%>'
                                                        TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason for Approve / Rejection">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtReason" runat="server" Width="200" SkinID="MandTxtBox" CssClass="txtclass"></asp:TextBox>
                                                    <asp:Label ID="TextBox1" runat="server" Style="visibility: hidden;"> <span style="Color:Red"> <img   alt=""  src="../../Images/hand.gif" /></span> <span style="Color:Red"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; *Enter The Reason </span> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="loading" align="center">
                                Loading. Please wait.<br />
                                <br />
                                <img src="../../Images/loader.gif" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <center>

            <asp:Button ID="btnApprove" runat="server" CssClass="savebutton" Text="Approve"
                OnClick="btnApprove_Click" OnClientClick="javascript:return validateTextBox();" />&nbsp;&nbsp;
          
       <asp:Button ID="btnReject" runat="server" CssClass="savebutton" Text="Reject"
           OnClick="btnReject_Click" OnClientClick="javascript:return validateTextBox();" />

        </center>
    </form>
    <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>
</body>
</html>

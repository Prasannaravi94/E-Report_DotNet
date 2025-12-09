<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order_Booking_Status.aspx.cs"
    Inherits="MasterFiles_MR_Order_Booking_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Booking</title>

    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />

    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {

                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {

                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>

    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
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

        .div_fixedReactBtn {
            position: fixed;
            top: 486px;
            right: 7px;
            height: 24px;
        }

        .div_fixedDeactBtn {
            position: fixed;
            top: 516px;
            right: 7px;
            height: 24px;
        }

        .div_fixed_Grid {
            position: fixed;
            top: 360px;
            right: 675px;
        }

        .div_fixed_Grid_No {
            position: fixed;
            top: 188px;
            right: 500px;
        }

        .popuph1 {
            color: #292a34;
            font-size: 24px;
            font-weight: 700;
            margin-bottom: 50px;
        }

        .popuplabel {
            color: #696d6e;
            font-size: 12px;
        }

        .marginright {
            margin-left: 95%;
            width: 5%;
        }

        .marRight {
            margin-right: 35px;
        }

        [type="checkbox"]:not(:checked) + label, [type="checkbox"]:checked + label {
            position: relative;
            padding-left: 1.15em !important;
            cursor: pointer;
            vertical-align: top;
            line-height: 20px;
            margin: 2px 0;
            display: inline-block !important;
        }

        .display-table .table tr td [type="checkbox"]:not(:checked) + label, .display-table .table tr td [type="checkbox"]:checked + label {
            padding-left: 1.4em;
        }

        .display-table .table tr th [type="checkbox"]:not(:checked) + label, .display-table .table tr th [type="checkbox"]:checked + label {
            padding-left: 0.0em;
            margin-left: 10px;
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
            $('#Btnsrc').click(function () {

                <%-- var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                var divi1 = $('#<%=ddlSrc2.ClientID%> :selected').text();
                if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc2').focus(); return false; }
                if ($("#txtsearch").val() == "") { alert("Enter Doctor Name."); $('#txtsearch').focus(); return false; }--%>

            });
        });
    </script>



    <script language="javascript" type="text/javascript">
        function popUp(Sf_code, Trans_SlNo, Stockist_Name, DHP_Code, DHP_Name, Order_Date, Mode, Order_Flag) {
            popUpObj = window.open("Order_Booking_Status_View.aspx?Sf_code=" + Sf_code + "&Trans_SlNo=" + Trans_SlNo + "&Stockist_Name=" + Stockist_Name + "&DHP_Code=" + DHP_Code + "&Mode=" + Mode + "&DHP_Name=" + DHP_Name + "&Order_Flag=" + Order_Flag + "&Order_Date=" + Order_Date,
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

            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s4.postimg.org/j1heaetwt/loading16.gif"
                $(popUpObj.document.body).append('<div><p style="color:blue;margin-top:10%;margin-left:40%;">Loading Please Wait....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style="width:310px;height:300px;position:fixed;top:20%;left:30%;"  alt="" /></div>');
            });

        }
    </script>

    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">

        <div>
            <div id="Divid" runat="server"></div>
            <%-- <ucl:Menu ID="menu1" runat="server" />--%>
            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <div class="row justify-content-center ">
                            <div class="col-lg-5">
                                <center>
                                    <table>
                                        <tr>
                                            <td align="center">
                                                <h2 class="text-center">Order Booking Status</h2>
                                            </td>
                                        </tr>
                                    </table>
                                </center>

                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>
                                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
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
                                        <asp:Label ID="lblYear" runat="server" CssClass="label" Text="Year"></asp:Label>
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                            <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="50px" Text="Go" OnClick="Btnsrc_Click" />
                                </div>
                            </div>
                        </div>
                        <center>
                            <div class="row clearfix">
                                <div class="col-lg-11" style="padding-top: 22px;">
                                    <div class="row">

                                        <div class="col-lg-4">
                                            <asp:Image ID="imgCross2lbl" runat="server" Visible="false" ImageUrl="~/Images/Campcross.png" />&nbsp;
                                                   <asp:Label ID="lblcamp" runat="server" ForeColor="#696D6E" Text=" Processed" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-lg-2">
                                            <asp:Image ID="imgCrosslbl" runat="server" Visible="false" ImageUrl="../../Images/cross.png" />&nbsp;
                                                <asp:Label ID="lblvisit" runat="server" ForeColor="#696D6E" Text="Rejected" Visible="false"></asp:Label>
                                        </div>
                                        <%-- <div class="col-lg-2">
                                            <asp:Image ID="imgCross1lbl" runat="server" ImageUrl="../../Images/PinkCross.png" />&nbsp;
                                                 <asp:Label ID="lblcore" runat="server" Text="Core Dr"></asp:Label>
                                        </div>
                                        <div class="col-lg-3">
                                            <asp:Image ID="imgCross3lbl" runat="server" ImageUrl="~/Images/Orange.png" />&nbsp;
                                                <asp:Label ID="lblcrm" runat="server" Text="CRM Dr"></asp:Label>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </center>
                        <br />
                        <br />
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="false" GridLines="None" CssClass="table"
                                    OnRowCreated="grdDoctor_RowCreated" OnRowDataBound="grdDoctor_RowDataBound"
                                    AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" Text="." onclick="checkAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkListedDR" runat="server" Text="." />
                                                <asp:Image ID="imgCross2" runat="server" ImageUrl="../../../Images/cross.png" Visible="false" />
                                                <asp:Image ID="imgCross1" runat="server" ImageUrl="../../../Images/PinkCross.png" Visible="false" />
                                                <asp:Image ID="imgCross" runat="server" ImageUrl="../../../Images/Campcross.png" Visible="false" />
                                                <asp:Image ID="imgCross3" runat="server" ImageUrl="~/Images/Orange.png" Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DHP Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("DHP_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Flag" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFlag" runat="server" Text='<%#Eval("Order_Flag")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Trans_SlNo" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrans_SlNo" runat="server" Text='<%#Eval("Trans_SlNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="From" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockName" runat="server" Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="(Pharmacist/Chemist)/Hospital/Doctor Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("DHP_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mode" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMode" runat="server" Text='<%#Bind("Mode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Order Date" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='<%#Bind("Order_Date") %>'
                                                    OnClientClick='<%# "return popUp(\"" + Eval("Sf_code") + "\",\"" + Eval("Trans_SlNo")  + "\",\"" + Eval("Stockist_Name")  + "\",\"" + Eval("DHP_Code")  + "\",\"" + Eval("DHP_Name")  + "\",\"" + Eval("Order_Date")  + "\",\"" + Eval("Mode")  + "\",\"" + Eval("Order_Flag")  + "\");" %>'>
                                                </asp:LinkButton>
                                                <%-- <asp:Label ID="lblDate" runat="server" Text='<%#Bind("Order_Date") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Value">
                                            <ItemTemplate>
                                                <asp:Label ID="lblValue" runat="server" Text='<%# Bind("Order_Value") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Statu") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>


                         <div>
            <asp:ModalPopupExtender ID="mpeSave" runat="server" PopupControlID="pnlPopup" TargetControlID="btnReject"
                CancelControlID="btnClose" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
        </div>

        <table align="center">
            <tr>
                <td>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" align="center" Style="display: none">
                        <div style="height: 60px">

                            <asp:DropDownList ID="DropDownList1" runat="server">
                                <asp:ListItem Text="Please Select" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                            </asp:DropDownList>

                        </div>
                         <asp:Button ID="btnRejct" runat="server" CssClass="savebutton" OnClick="btnReject_Click" Text="Reject"
                                UseSubmitBehavior="false" Width="70px" />
                         <asp:Button ID="btnClose" runat="server" CssClass="savebutton"   Text="Close"
                                UseSubmitBehavior="false" Width="70px" />                         
                    </asp:Panel>
                   
                </td>
            </tr>
        </table>

                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <div class="div_fixed">
            <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Text="Process" OnClick="btnSave_Click" Visible="false" />
            <br />
            <%--<asp:Button ID="btnReject" runat="server" CssClass="savebutton" Text="Reject" OnClick="btnReject_Click" Visible="false" />--%>
            <asp:Button ID="btnReject" runat="server" CssClass="savebutton" Text="Reject" />
        </div>


        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>


       
        <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>--%>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order_BookingStatus.aspx.cs"
    Inherits="MasterFiles_MR_Order_BookingStatus" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Booking</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    <style type="text/css">
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
            width: 8%;
            height: 8%;
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
    </style>
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

                    if ($('input:text')[curIndex].value == '') {
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
                            $('#btnApprove').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnRejct').click(function () {
                var ddlReson = $("#ddlReson").val();
                if (ddlReson == "Other Reason" || ddlReson == "5") {
                    if ($("#txtRmrks").val() == "") {
                        $('#txtRmrks').focus();
                        alert("Enter Remarks.");
                        return false;
                    }
                }
            });
        });
    </script>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
</head>
<body>

    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

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

                                    <%--<div class="single-des clearfix">
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
                                    </div>--%>

                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblEffFrom" runat="server" CssClass="label"> From Date<span style="Color:Red;padding-left:5px;"></span></asp:Label>
                                        <div id="dvEffc_Frm" class="row-fluid">
                                            <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input"
                                                onkeypress="Calendar_enter(event);" Width="100%"
                                                onblur="this.style.backgroundColor='White'" TabIndex="6"></asp:TextBox>
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffFrom" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                                            <%--<asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom"
                                            runat="server" />--%>
                                        </div>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblEffTo" runat="server" CssClass="label"> To Date<span style="Color:Red;padding-left:5px;"></span></asp:Label>
                                        <div id="dvEffc_To" class="row-fluid">
                                            <asp:TextBox ID="txtEffTo" runat="server" CssClass="input"
                                                onkeypress="Calendar_enter(event);" Width="100%"
                                                TabIndex="7" onblur="this.style.backgroundColor='White'" />
                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEffTo" CssClass=" cal_Theme1" Format="dd/MM/yyyy" />
                                            <%--   <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffTo"
                                    runat="server" />--%>
                                        </div>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblMode" runat="server" Text="Mode" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlMode" runat="server" CssClass="nice-select">
                                            <asp:ListItem Value="-1" Text="---All Mode---"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Pending"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="Order Received"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Invoiced"></asp:ListItem> 
                                            <asp:ListItem Value="4" Text="Despatched"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="Delivered"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Rejected"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="50px" Text="Go" OnClick="Btnsrc_Click" />
                                </div>
                            </div>
                        </div>


                        <center>
                            <%--  <div class="row clearfix">--%>
                            <div class="row justify-content-center ">
                                <div class="col-lg-11" style="padding-top: 22px;">
                                    <div class="row">
                                        <div class="col-lg-2">
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:TextBox ID="Red" runat="server" Width="15px" Height="15px" BackColor="Red" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="Label6" runat="server" ForeColor="#696D6E" Font-Size="12px" Text=" Pending"></asp:Label>
                                        </div>

                                        <div class="col-lg-1">
                                            <asp:TextBox ID="Orange" runat="server" Width="15px" Height="15px" BackColor="Orange" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="Label1" runat="server" ForeColor="#696D6E" Font-Size="12px" Text=" Received "></asp:Label>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:TextBox ID="ForestGreen" runat="server" Width="15px" Height="15px" BackColor="ForestGreen" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="Label2" runat="server" ForeColor="#696D6E" Font-Size="12px" Text=" Delivered"></asp:Label>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:TextBox ID="Purple" runat="server" Width="15px" Height="15px" BackColor="Purple" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="Label3" runat="server" ForeColor="#696D6E" Font-Size="12px" Text=" Despatched"></asp:Label>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:TextBox ID="DeepPink" runat="server" Width="15px" Height="15px" BackColor="DeepPink" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="Label4" runat="server" ForeColor="#696D6E" Font-Size="12px" Text=" Invoiced"></asp:Label>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:TextBox ID="IndianRed" runat="server" Width="15px" Height="15px" BackColor="IndianRed" Enabled="false"></asp:TextBox>
                                            <asp:Label ID="Label5" runat="server" ForeColor="#696D6E" Font-Size="12px" Text=" Rejected"></asp:Label>
                                        </div>
                                        <div class="col-lg-1">
                                             <asp:TextBox ID="imgCross2lbl" runat="server" Width="15px" Height="15px" style="background-image: url(../../Images/Campcross.png); border: none;" Enabled="false"  Visible="false"></asp:TextBox>
                                            <%--<asp:Image ID="imgCross2lbl" runat="server" Visible="false" Width="15px" Height="15px" ImageUrl="~/Images/Campcross.png" />--%>
                                                   <asp:Label ID="lblcamp" runat="server" ForeColor="#696D6E" Font-Size="12px" Text=" Processed" Visible="false"></asp:Label>
                                        </div>
                                        <div class="col-lg-1">
                                              <asp:TextBox ID="imgCrosslbl" runat="server" Width="15px" Height="15px" style="background-image: url(../../Images/cross.png); border: none;" Enabled="false"  Visible="false"></asp:TextBox>
                                            <%--<asp:Image ID="imgCrosslbl" runat="server" Visible="false" Width="15px" Height="15px" ImageUrl="../../Images/cross.png" />--%>
                                                <asp:Label ID="lblvisit" runat="server" ForeColor="#696D6E" Font-Size="12px" Text="Rejected" Visible="false"></asp:Label>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </center>


                        <br />
                        <br />
                         <div class="display-table clearfix">
                        <%--<div class="display-reportMaintable clearfix">--%>
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center"
                                    EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                                    PageSize="10" OnPageIndexChanging="grdDoctor_PageIndexChanging" OnRowCreated="grdDoctor_RowCreated"
                                    OnRowDataBound="grdDoctor_RowDataBound" GridLines="None" CssClass="table"
                                    PagerStyle-CssClass="gridview1" AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                   <asp:Label ID="lblSNo" runat="server" Text='<%# (grdDoctor.PageIndex * grdDoctor.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFFName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="From" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockName" runat="server" Text='<%#Eval("Stockist_Name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pharma/Hos/Doc Name" ItemStyle-HorizontalAlign="Left">
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
                                        <asp:TemplateField HeaderText="Trans_SlNo" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrans_SlNo" runat="server" Text='<%#Eval("Trans_SlNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
        <div class="div_fixed">
            <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Text="Process" Visible="false" />
            <br />
            <%--<asp:Button ID="btnReject" runat="server" CssClass="savebutton" Text="Reject" OnClick="btnReject_Click" Visible="false" />--%>
            <asp:Button ID="btnReject" runat="server" CssClass="savebutton" Text="Reject" Visible="false" />
        </div>

        <table>
            <td>
                <asp:ModalPopupExtender ID="mpeSave" runat="server" PopupControlID="pnlPopup" TargetControlID="btnReject"
                    OkControlID="btnClose" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </td>
        </table>

        <table align="center">
            <tr>
                <td>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none; width: 35%; height: 55%; overflow: scroll; scrollbar-width: thin;">
                        <%--OnClick="btnYes_Click"--%>
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-9">
                                <div class="single-des clearfix">
                                    <asp:Label ID="Label11" runat="server" CssClass="popuph1" Text="">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Reject Reason</asp:Label>
                                </div>

                                <div class="single-des clearfix">
                                    <asp:Label ID="lblRsn" runat="server" CssClass="label" Text="Reason : "></asp:Label>
                                    <asp:DropDownList ID="ddlReson" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="1" Text="Payment Pending"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Out of Stock"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Price Issues"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Transportation Issue"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="Other Reason"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblrmrk" runat="server" CssClass="label" Text="Remarks : "></asp:Label>
                                    <asp:TextBox ID="txtRmrks" runat="server" Width="340px" Height="70px" TextMode="MultiLine" ReadOnly="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <center>
                            <asp:Button ID="btnRejct" runat="server" CssClass="savebutton" OnClick="btnReject_Click" Text="Reject"
                                Width="70px" />
                            <asp:Button ID="btnClose" runat="server" CssClass="savebutton" Text="Close"
                                UseSubmitBehavior="false" Width="70px" />
                        </center>


                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

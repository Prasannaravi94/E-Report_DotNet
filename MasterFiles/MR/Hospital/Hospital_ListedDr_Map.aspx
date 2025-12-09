<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hospital_ListedDr_Map.aspx.cs" Inherits="MasterFiles_MR_Hospital_HospitalCreation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Hospital Listed Doctor Map</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />--%>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <style type="text/css">
        .textalign {
            text-align: center;
            font-weight: bold;
        }

        .mycheckbox input[type="checkbox"] {
            margin-right: 7px;
        }

        .borderalignment {
            border-bottom: 1px solid #DCE2E8;
            border-right: 1px solid #DCE2E8;
            border-left: 1px solid #DCE2E8;
        }

        .alignment {
            text-align: center;
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
                var type = $('#<%=ddlHospitalName.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Hospital."); $('#ddlHospitalName').focus(); return false; }
            });
        });

        function confirm_Save() {
            if (confirm('Do you want to Tag the Doctors?')) {
                if (confirm('Are you sure?')) {
                    ShowProgress();
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server"></div>



            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" style="border-bottom: none" id="heading" runat="server"></h2>

                        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="center">
                            <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                        </asp:Panel>
                        <%-- <table id="table1" runat="server" width="90%">
                            <tr>
                                <td align="right" colspan="2">
                                   <asp:Button ID="btnback" CssClass="savebutton" Text="back" runat="server"
                                        OnClick="btnback_click" />
                                </td>
                            </tr>
                        </table>--%>
                        <br />
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblHos" runat="server" Text="Hospital Name" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlHospitalName" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnGo" Text="GO" CssClass="savebutton" runat="server"
                                OnClick="btnGo_Click" />
                            <asp:Button ID="btnclr" OnClick="btnclr_Click" runat="server" Text="Clear"
                                CssClass="resetbutton"></asp:Button>
                        </div>

                        <br />
                    </div>

                    <asp:Button ID="btnback1" runat="server" CssClass="backbutton" Text="Back"  OnClick="btnback1_Click" />
                </div>
                <br />
                <div class="row justify-content-center">
               
                    <asp:Label ID="lblSelect" Text="Select Hospital & Press Go button" runat="server"
                        Visible="false"></asp:Label>
                </div>
                <br />

                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="display-reportMaintable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin; padding-bottom: 20px;">
                                <table align="center" width="100%" cellpadding="1" cellspacing="1" style="border-collapse: collapse">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:DataList ID="DataList1" HeaderStyle-BackColor="#f1f5f8" Width="100%" CellPadding="5"
                                                    runat="server" RepeatDirection="Vertical"
                                                    RepeatColumns="2">
                                                    <HeaderTemplate>
                                                        <div style="padding: 20px; min-width: 1050px">
                                                            <asp:Label ID="lblsln" Text="#" Font-Bold="true" Width="50px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDocName" Text="Listed Doctor Name" Font-Bold="true" Width="200px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblSpcl" Text="Speciality" Font-Bold="true" Width="100px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblSubArea" Text="Sub Area" Font-Bold="true" Width="170px" runat="server"></asp:Label>

                                                            <asp:Label ID="lbl1" Text="#" Font-Bold="true" Width="50px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl2" Text="Listed Doctor Name" Font-Bold="true" Width="200px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl3" Text="Speciality" Font-Bold="true" Width="100px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl4" Text="Sub Area" Font-Bold="true" Width="100px" runat="server"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemStyle CssClass="borderalignment" />
                                                    <AlternatingItemStyle />
                                                    <ItemStyle />
                                                    <ItemTemplate>
                                                        <b></b>
                                                        <div style="min-width: 420px; vertical-align: inherit; display: inline-block">
                                                            <asp:Label ID="lblSLNO" runat="server" Width="50px" CssClass="alignment" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="lblDoctorsCode" runat="server" Text='<%#Eval("ListedDrCode")%>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkDocName" onclick="ChkFn(this)" Width="200px" CssClass="mycheckbox"
                                                                runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ListedDr_Name")%>' />
                                                            <asp:Label ID="lblSpec" runat="server" Text='<%#Eval("Doc_Spec_ShortName")%>' Width="100px"></asp:Label>
                                                            <asp:Label ID="lblprd_sale" runat="server" Text='<%#Eval("territory_Name")%>' Width="100px" ></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <br />

                <div class="row justify-content-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="savebutton" Visible="false"
                        OnClick="btnSubmit_Click" OnClientClick="return confirm_Save();" />
                </div>
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MGR_Working_Hrs_View2.aspx.cs" Inherits="MasterFiles_AnalysisReports_MGR_Working_Hrs_View2" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Time Status II</title>
    <style type="text/css">
        .display-reportMaintable .table tr:nth-child(2) td:first-child {
            background-color: #f1f5f8 !important;
            color: #414d55 !important;
        }

        .display-reportMaintable {
            text-transform: none !important;
        }

        span {
            font-size: 14px;
        }

        #tblMsgInfo > tbody > tr > td {
            border: 1px solid #aba3a3;
        }
    </style>
    <script type="text/javascript">
    function showTimeStatusZoom(sfcode, div_code, fmon, fyr,fday,parameter) {
            popUpObj = window.open("rptMGRWorking_Hrs_ViewZoom.aspx?sfcode=" + sfcode + "&div_code=" + div_code + "&Month=" + fmon + "&Year=" + fyr + "&Day=" + fday+"&IsDash=0&parameter=" + parameter, "ModalPopUp" );
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
    </script>
    <script type="text/javascript" src="/JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="/JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGo').click(function () {
                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var Day = $('#<%=ddlDay.ClientID%> :selected').text();
                if (Day == "---Select---") { alert("Select Day."); $('#ddlDay').focus(); return false; }
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

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Time Status II</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>

                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                </asp:DropDownList>

                            </div>


                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%">
                                    <asp:Label ID="lblFMonth" runat="server" CssClass="label" Text="Month"></asp:Label>
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
                                <div style="float: right; width: 45%">
                                    <asp:Label ID="lblFYear" runat="server" CssClass="label" Text="Year"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%">
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="Day"></asp:Label>
                                    <asp:DropDownList ID="ddlDay" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                        <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="float: right; width: 45%">
                                    <div class="w-100 designation-submit-button clearfix">
                                        <br />
                                        <asp:Button ID="btnGo" runat="server" Text="View" CssClass="savebutton" OnClick="btnGo_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                    <div class="col-lg-10">
                        <table class="table" id="tblMsgInfo" runat="server" border="1" visible="false">
                            <tr>
                                <td>
                                    <asp:Label ID="lblFFmsg" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblhq" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDesign" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEmpCode" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDOJ" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWorkType" runat="server"></asp:Label>
                                </td>
                                <%--  <td>
                                    <asp:Label ID="lblMonth" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblYear" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                </td>--%>
                            </tr>
                        </table>
                        <div class="display-reportMaintable clearfix">
                            <div class="table-responsive">
                                <asp:GridView ID="GrdTimeSt" runat="server" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="true" CssClass="table" EmptyDataText="No Records Found" Font-Bold="true"
                                    GridLines="None" HorizontalAlign="Center" OnRowDataBound="GrdTimeSt_RowDataBound"
                                    ShowHeader="false" Width="100%">

                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

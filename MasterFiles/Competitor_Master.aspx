<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Competitor_Master.aspx.cs"
    Inherits="MasterFiles_Competitor_Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Competitor Master</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>

    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />
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

        .closeLoginPanel {
            font-family: Verdana, Helvetica, Arial, sans-serif;
            height: 14px;
            font-size: 15px;
            font-weight: bold;
            position: absolute;
            top: -2px;
            right: 1px;
        }

            .closeLoginPanel a {
                /*background-color: Yellow;*/
                cursor: pointer;
                color: Black;
                text-align: center;
                text-decoration: none;
                padding: 3px;
            }

        /*#Panel1 {
            top: 238px !important;
        }*/
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
    <link type="text/css" rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#<%=btnview.ClientID%>').click(function () {

                popUpObj = window.open("Competitor_View.aspx", "_blank");
            });
        })
    </script>
    <script type="text/javascript">
         $(function () {
             $('#<%=btnOur_prd_view.ClientID%>').click(function () {

                 popUpObj = window.open("Competitor_Ourprd_View.aspx",
     "_blank"
    );

                 return false;
             });
         })
    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            //  $('input:text:first').focus();
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
                if ($("#txtCompetitorName").val() == "") { alert("Enter Name."); $('#txtCompetitorName').focus(); return false; }

            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlcompe]');
            var $items = $('select[id$=ddlcompe] option');

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
    <script type="text/javascript">
        function HidePopup() {

            var popup = $find('txtprd_PopupControlExtender');
            popup.hide();

        }
    </script>

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
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Competitor Master</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-8">
                                        <asp:Button ID="btnComp" runat="server" CssClass="savebutton" Width="120px"
                                            Text="Add Competitor" OnClick="btnComp_Click" />
                                        <asp:Button ID="btnPrd" runat="server" CssClass="resetbutton" Width="175px"
                                            Text="Add Competitor-Products" OnClick="btnPrd_Click" />
                                        <asp:Button ID="btnComVsPrd" runat="server" CssClass="resetbutton" Width="180px"
                                            Text="Competitor vs Product Tag" OnClick="btnComVsPrd_Click" />
                                        <asp:Button ID="btnview" runat="server" CssClass="resetbutton" Width="60px"
                                            Text="View" />
                                        <asp:Button ID="btnAddOurprb" runat="server" CssClass="resetbutton" Width="230px"  Text="Our Product - Competitor Product Map" OnClick="btnAddOurprb_Click" />
                        <asp:Button ID="btnOur_prd_view" runat="server" CssClass="resetbutton" Width="230px" 
                        Text="Our Product - Competitor Product View" />

                                    </div>
                                </div>
                                <br />
                                <div class="single-des clearfix">
                                    <%--<div class="d-inline-block division-name">--%>
                                    <center>
                                        <table cellpadding="5">
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblname" runat="server" CssClass="label" Visible="false" Height="18px">Competitor Name</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <div class="row clearfix">
                                                        <%-- <div class="col-lg-3">
                                                    <asp:TextBox ID="txtNew" runat="server" Width="115px" CssClass="input" Visible="false"
                                                        ToolTip="Enter Text Here"></asp:TextBox>
                                                    
                                                </div>--%>
                                                        <div class="col-lg-12">
                                                            <asp:DropDownList ID="ddlcompe" runat="server"
                                                                Visible="false" CssClass="custom-select2 nice-select" AutoPostBack="true" TabIndex="7" OnSelectedIndexChanged="ddlcompe_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblprd" runat="server" CssClass="label" Visible="false" Height="18px">Product Name</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:UpdatePanel ID="updatepanel1" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtprd" ReadOnly="true" CssClass="input" Width="100%" runat="server"
                                                                Visible="false"></asp:TextBox>
                                                            <asp:PopupControlExtender ID="txtprd_PopupControlExtender" runat="server" DynamicServicePath=""
                                                                Enabled="True" ExtenderControlID="" TargetControlID="txtprd" PopupControlID="Panel1"
                                                                OffsetY="2" Position="Bottom">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="Panel1" runat="server" Height="185px" Width="365px"
                                                                BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa"
                                                                Style="display: none; border-radius: 8px; display: none; width: 300px; height: 185px; overflow: scroll;scrollbar-width: thin;">
                                                                <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                                                    align="right">
                                                                    <div class="closeLoginPanel">
                                                                        <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                                            title="Close">X</a>
                                                                    </div>
                                                                </div>
                                                                <table></table>
                                                                <asp:CheckBoxList ID="Chkprd" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Chkprd_SelectedIndexChanged">
                                                                </asp:CheckBoxList>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left">
                                                    <asp:Label ID="lblcomp" runat="server" CssClass="label" Visible="false" Height="18px">Competitor Name</asp:Label>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtCompetitorName" CssClass="input"
                                                        Visible="false"  TabIndex="2" runat="server"
                                                        Width="200px" MaxLength="120" onkeypress="AlphaNumeric_NoSpecialChars(event);">
                                                    </asp:TextBox>
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td align="center">
                                                    <asp:Button ID="btnSubmit" Visible="false" runat="server" Width="60px"
                                                        CssClass="savebutton" Text="Add" OnClick="btnSubmit_Click" />
                                                </td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </center>
                                    <%--                                </div>--%>
                                </div>
                            </div>
                        
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <table align="center" style="width: 100%">
                                        <tbody>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:GridView ID="grdCampet" runat="server" Width="100%" HorizontalAlign="Center"
                                                        OnRowCommand="grdCampet_RowCommand" AutoGenerateColumns="false" AllowPaging="True"
                                                        PageSize="10" EmptyDataText="No Records Found" OnRowCreated="grdCampet_RowCreated"
                                                        OnPageIndexChanging="grdCampet_PageIndexChanging" GridLines="None" CssClass="table"
                                                        PagerStyle-CssClass="GridView1" AlternatingRowStyle-CssClass="alt" AllowSorting="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5px">
                                                             
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdCampet.PageIndex * grdCampet.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Campetitor Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComp_Sl_No" runat="server" Text='<%#Eval("Comp_Sl_No")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="250px" HeaderText="Competitor Name"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComp_Name" runat="server" Text='<%# Bind("Comp_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center"
                                                                DataNavigateUrlFormatString="Competitor_Master.aspx?Comp_Sl_No={0}&amp;Comp_Name={1}&amp;type=1"
                                                                DataNavigateUrlFields="Comp_Sl_No,Comp_Name,Comp_Sl_No">
                                                                <%--                                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                                            </asp:HyperLinkField>
                                                            <asp:TemplateField HeaderText="Deactivate" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                                <%--                                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Comp_Sl_No") %>'
                                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate Competitor');">Deactivate
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:GridView ID="grdprd" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                        OnRowCommand="grdprd_RowCommand" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                                                        OnRowCreated="grdprd_RowCreated" OnPageIndexChanging="grdprd_PageIndexChanging"
                                                        GridLines="None" CssClass="table" PagerStyle-CssClass="GridView1" AlternatingRowStyle-CssClass="alt"
                                                        AllowSorting="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5px">
                                                                <ItemTemplate>

                                                                    <asp:Label ID="lblSNo2" runat="server" Text='<%# (grdprd.PageIndex * grdprd.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Comp_Prd_Sl_No Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComp_Prd_Sl_No" runat="server" Text='<%#Eval("Comp_Prd_Sl_No")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="250px" HeaderText="Competitor-Product Name"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComp_Prd_Name" runat="server" Text='<%# Bind("Comp_Prd_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center"
                                                                DataNavigateUrlFormatString="Competitor_Master.aspx?Comp_Prd_Sl_No={0}&amp;Comp_Prd_Name={1}&amp;type=2"
                                                                DataNavigateUrlFields="Comp_Prd_Sl_No,Comp_Prd_Name,Comp_Prd_Sl_No">
                                                                <%--                                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                                            </asp:HyperLinkField>
                                                            <asp:TemplateField HeaderText="Deactivate" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                                <%--                                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Comp_Prd_Sl_No") %>'
                                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate Product');">Deactivate
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="center">
                                                    <asp:GridView ID="grdComVsprd" runat="server" Width="100%" HorizontalAlign="Center"
                                                        AutoGenerateColumns="false" OnRowCommand="grdComVsprd_RowCommand" AllowPaging="True"
                                                        PageSize="10" EmptyDataText="No Records Found" OnRowCreated="grdComVsprd_RowCreated"
                                                        OnPageIndexChanging="grdComVsprd_PageIndexChanging" GridLines="None" CssClass="table"
                                                        PagerStyle-CssClass="GridView1" AlternatingRowStyle-CssClass="alt" AllowSorting="True">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo3" runat="server" Text='<%# (grdComVsprd.PageIndex * grdComVsprd.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sl_No" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSl_No" runat="server" Text='<%#Eval("Sl_No")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="250px" HeaderText="Competitor Name"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblComp_Name" runat="server" Text='<%# Bind("Comp_Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="120px" HeaderText="Product Tagged"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprd_tagged" runat="server" Text='<%# Bind("Comp_Prd_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center"
                                                                DataNavigateUrlFormatString="Competitor_Master.aspx?Sl_No={0}&amp;Comp_Prd_name_Mapp={1}&amp;Comp_Name_Mapp_id={2}&amp;type=3"
                                                                DataNavigateUrlFields="Sl_No,Comp_Prd_name,Comp_Sl_No,Sl_No">
                                                                <%--                                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                                            </asp:HyperLinkField>
                                                            <asp:TemplateField HeaderText="Deactivate" HeaderStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                                <%--                                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Sl_No") %>'
                                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate');">Deactivate
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
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
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

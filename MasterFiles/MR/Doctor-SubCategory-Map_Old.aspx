<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor-SubCategory-Map.aspx.cs" EnableEventValidation="false" Inherits="MasterFiles_MR_Doctor_SubCategory_Map" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Campaign - Map</title>
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".img").click(function () {               
                if ($(this).attr('tag') == "plus") {
                    if ($(this).closest("tr").next().css('display') == "table-row") {
                        $(this).closest("tr").after("<tr class='camps'><td></td><td colspan = '1000'>" + $(this).next().html() + "</td></tr>")
                        $(this).next().remove();
                    }
                    else {
                        $(this).closest("tr").next().css('display', 'table-row');
                    }
                    if ($(this).closest("tr").next().css('display') != "table-row") {
                        $(this).closest("tr").after("<tr class='camps'><td></td><td colspan = '1000'>" + $(this).next().html() + "</td></tr>")
                        $(this).next().remove();
                    }
                    $(this).attr("src", "../../Images/minus.png");
                    $(this).attr("tag", "minus");
                }
                else if ($(this).attr('tag') == "minus") {
                    $(this).attr("src", "../../Images/plus.png");
                    $(this).attr("tag", "plus");
                    $(this).closest("tr").next().css('display', 'none');
                }
            });
        });
    </script>
    <script type="text/javascript">
        function ChkFn(x) {
            aid = x.id.split('_');
            y = document.getElementById('grdDoctor_' + aid[1] + '_grdCampaign_' + aid[3] + '_chkCatName')
            if (x.checked == true)
                document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML += y.parentNode.getElementsByTagName('label')[0].innerHTML + ', '
            else
                document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML = document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML.replace(y.parentNode.getElementsByTagName('label')[0].innerHTML + ', ', '')
        }

    </script>
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

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .display-table .table tr:nth-child(2) td:first-child {
            background-color: #f1f5f8 !important;
        }
    </style>
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
    <script type="text/javascript">
        $(document).ready(function () {
            function blinker() {
                $('.blink_me').fadeOut(500);
                $('.blink_me').fadeIn(500);
            }

            setInterval(blinker, 1000);
        });
    </script>
</head>
<body class="bodycolor">
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">Doctor - Campaign Map</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblType" runat="server" CssClass="label" Text="Filter By"></asp:Label>
                            <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                <%-- <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                <asp:ListItem Text="Doctor Name" Value="7"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:TextBox ID="txtsearch" runat="server" CssClass="textbox" Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlSrc2" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlSrc2_SelectedIndexChanged"
                                SkinID="ddlRequired" TabIndex="4">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btnOk" runat="server" CssClass="savebutton" Text="Go" OnClick="btnOk_Click" />
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="designation-reactivation-table-area clearfix">
                        <br />
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin; max-height: 700px;">
                                <asp:GridView ID="grdDoctor" runat="server" AlternatingRowStyle-CssClass="alt"
                                    AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                    GridLines="None" HorizontalAlign="Center" OnRowDataBound="OnRowDataBound" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle Width="7%" />
                                            <ItemTemplate>
                                                <img class="img" alt="" src="../../Images/plus.png" tag="plus" />
                                                <asp:Panel ID="pnlDetails" runat="server" Style="display: none">
                                                    <asp:GridView ID="grdCampaign" runat="server" Width="44%" AutoGenerateColumns="false" CssClass="mGridImg">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Campaign Name" HeaderStyle-BackColor="#F1F5F8" ItemStyle-HorizontalAlign="Left" HeaderStyle-ForeColor="Black">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkCatName" onclick="ChkFn(this)" runat="server" Text='<%# Eval("Doc_SubCatName") %>' />
                                                                    <asp:Label ID="cbSubCat" runat="server" Text='<%# Eval("Doc_SubCatCode") %>' Visible="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </asp:Panel>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrcode" runat="server" Text='<%# Eval("ListedDrCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ListedDr Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrName" runat="server" Text='<%# Eval("ListedDr_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField  DataField="ListedDr_Name"  HeaderText="ListedDr Name" ItemStyle-HorizontalAlign="Left"  />--%>
                                     <%--   <asp:BoundField DataField="Doc_QuaName" HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" />--%>
                                        <asp:BoundField DataField="Doc_Special_SName" HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" />
                                        <asp:BoundField DataField="Doc_Cat_SName" HeaderText="Category" ItemStyle-HorizontalAlign="Left" />
                                      <%--  <asp:BoundField DataField="Doc_ClsSName" HeaderText="Class" ItemStyle-HorizontalAlign="Left" />--%>
                                        <%-- <asp:BoundField DataField="ListedDr_Mobile" HeaderText="Mobile" ItemStyle-HorizontalAlign="Left" />--%>
                                        <asp:BoundField DataField="territory_Name" HeaderText="Territory" ItemStyle-HorizontalAlign="Left" />
                                        <%--  <asp:BoundField  DataField="Doc_SubCatName" HeaderText="Mapped Campaign" ItemStyle-HorizontalAlign="Left" />--%>
                                         <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtListedDr_Mobile" runat="server" Text='<%#Eval("ListedDr_Mobile") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Chemists" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlChem" runat="server" CssClass="nice-select" DataSource="<%# FillChemist() %>"
                                                        DataTextField="Chemists_Name" DataValueField="Chemists_Code">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Mapped Campaign" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Doc_SubCatName" runat="server" Text='<%#Eval("Doc_SubCatName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <br />
                    <center>
                        <asp:Label ID="lbllock" runat="server" Visible="false" CssClass="blink_me label" Style="font-size: 18px; color: red; font-weight: bold;">"Campaign Tag" as Locked. Get the Approval from Admin</asp:Label>
                    </center>
                    <br />
                    <center>
                        <asp:Button ID="btnDraft" runat="server" Text="Draft Save" CssClass="savebutton" OnClick="btnDraft_Click" />
                        <asp:Button ID="btnSubmit" runat="server" Text="Final Submit" CssClass="savebutton" OnClick="btnSubmit_Click" />
                    </center>
                    <div class="div_fixed">
                        <asp:Button ID="btnSave" runat="server" Text="Final Submit" CssClass="savebutton" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>

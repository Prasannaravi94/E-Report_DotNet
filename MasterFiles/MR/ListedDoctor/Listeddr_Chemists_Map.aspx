<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_Chemists_Map.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_Listeddr_Chemists_Map" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Chemists Tag</title>

    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[src*=plus]").live("click", function () {
                if ($(this).closest("tr").next().css('display') == "table-row") {
                    $(this).closest("tr").after("<tr class='camps'><td></td><td colspan = '1000'>" + $(this).next().html() + "</td></tr>")
                    $(this).next().remove();
                } else
                    $(this).closest("tr").next().css('display', 'table-row');
                $(this).attr("src", "../../../Images/minus.png");
            });
            $("[src*=minus]").live("click", function () {
                $(this).attr("src", "../../../Images/plus.png");
                $(this).closest("tr").next().css('display', 'none');
            });

        });
    </script>
    <script type="text/javascript">
        function ChkFn(x) {
            aid = x.id.split('_');
            y = document.getElementById('grdDoctor_' + aid[1] + '_grdCampaign_' + aid[3] + '_chkCatName')
            z = document.getElementById('grdDoctor_' + aid[1] + '_grdCampaign_' + aid[3] + '_ddlPriority');
            var pval = z.getAttribute('p');
            // alert('|'+pval+'|');
            z.setAttribute('p', ((z.value != '0') ? z.options[z.selectedIndex].text : ""));
            document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML = document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML.replace(y.parentNode.getElementsByTagName('label')[0].innerHTML + ((pval != '') ? ' ( ' + pval + ' )' : '') + ', ', '')
            if (y.checked == true)
                document.getElementById('grdDoctor_' + aid[1] + '_Doc_SubCatName').innerHTML += y.parentNode.getElementsByTagName('label')[0].innerHTML + ((z.value != '0') ? ' ( ' + z.options[z.selectedIndex].text + ' )' : '') + ', '


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

        function confirm_Save() {
            if (confirm('Do you want to Tag the Chemists?')) {
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
            $('#btnGo').click(function () {
                var type = $('#<%=ddldr.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Listed Doctor."); $('#ddldr').focus(); return false; }
            });
        });
    </script>


    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" style="border-bottom: none">Listed Doctor Chemists Tag</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="single-des-option">
                                    <asp:Label ID="lblDr" runat="server" Text="Listed Doctor Name" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddldr" runat="server" CssClass="custom-select2 nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <br />
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnGo" Text="GO" CssClass="savebutton" runat="server"
                                OnClick="btnGo_Click" />
                            <%--<asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="GO" CssClass="savebutton"
                                OnClick="btnGo_Click" />--%>
                            <asp:Button ID="btnclr" OnClick="btnClear_Click" runat="server" Text="Clear"
                                CssClass="resetbutton"></asp:Button>
                        </div>

                        <br />
                    </div>

                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                </div>
                <br />
                <div class="row justify-content-center">
                    <asp:Label ID="lblSelect" Text="Select the Chemists" runat="server" Visible="false"></asp:Label>
                </div>
                <br />

                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="display-reportMaintable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin; padding-bottom: 20px;">
                                <table align="center" width="100%" cellpadding="1" cellspacing="1" style="border-collapse: collapse">
                                    <tbody>
                                        <tr>
                                            <td align="center">

                                                <asp:DataList ID="DataList1" HeaderStyle-BackColor="#f1f5f8" Width="85%" CellPadding="5"
                                                    runat="server" RepeatDirection="Vertical"
                                                    RepeatColumns="2">
                                                    <HeaderStyle />
                                                    <HeaderTemplate>
                                                        <div style="padding: 20px; min-width: 450px">
                                                            <asp:Label ID="lblsln" Text="#" Font-Bold="true" Width="90px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblDocVisit" Text="Chemists Name" Font-Bold="true" Width="150px" runat="server"></asp:Label>
                                                            <asp:Label ID="lblunit" Text="Territory Name" Font-Bold="true" Width="200px" runat="server"></asp:Label>

                                                            <asp:Label ID="lbl1" Text="#" Font-Bold="true" Width="90px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl2" Text="Chemists Name" Font-Bold="true" Width="180px" runat="server"></asp:Label>
                                                            <asp:Label ID="lbl3" Text="Territory Name" Font-Bold="true" runat="server"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemStyle CssClass="borderalignment " />
                                                    <AlternatingItemStyle />
                                                    <ItemStyle />
                                                    <ItemTemplate>

                                                        <b></b>
                                                        <div style="min-width: 420px; vertical-align: inherit; display: inline-block">
                                                            <asp:Label ID="lblSLNO" runat="server" Width="70px" CssClass="alignment" Text='<%# Container.ItemIndex+1 %>'></asp:Label>
                                                            <asp:Label ID="lblChemistsCode" runat="server" Text='<%#Eval("Chemists_Code")%>' Visible="false"></asp:Label>
                                                            <asp:CheckBox ID="chkCatName" onclick="ChkFn(this)" Width="200px"
                                                                runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Chemists_Name")%>' />

                                                            <asp:Label ID="lblprd_sale" runat="server" Text='<%#Eval("territory_Name")%>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>

                                                <%--  </asp:DataList>--%>
                                            </td>

                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <%--<table align ="center" >
                   <tr>
                     <td colspan="2" align="center">
                        <asp:CheckBoxList ID="chkprd" CssClass="chkboxLocation" CellPadding="10" Visible="false"  RepeatColumns="3" Font-Bold="true" Font-Names="Verdana" Font-Size="11px"  RepeatDirection="vertical"  Width="300px" runat="server">
                        </asp:CheckBoxList>
                    </td>
                   </tr>
                </table>
                --%>
                <div class="row justify-content-center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Save" CssClass="savebutton" Visible="false"
                        OnClick="btnSubmit_Click" OnClientClick="return confirm_Save();" />
                </div>
            </div>
            <br />
            <br />
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

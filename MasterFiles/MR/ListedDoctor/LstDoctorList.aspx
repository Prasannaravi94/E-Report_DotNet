<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LstDoctorList.aspx.cs" Inherits="MasterFiles_ListedDoctor_LstDoctorList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Details</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../../css/style.css" />--%>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
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

        .alignment {
            min-width: 120px;
        }

        /*.gridview1 {
            background-color: #336699;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }

            .gridview1 a {
                margin: auto 1%;
                border-style: none;
                border-radius: 50%;
                background-color: #444;
                padding: 5px 7px 5px 7px;
                color: #fff;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview1 a:hover {
                    background-color: #1e8d12;
                    color: #fff;
                }

            .gridview1 td {
                border-style: none;
            }

            .gridview1 span {
                background-color: #ae2676;
                color: #fff;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 7px 5px 7px;
            }*/

        .mGridImg1 {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }

            .mGridImg1 td {
                padding: 2px;
                border-color: Black;
                background: F2F1ED;
                font-size: small;
                font-family: Calibri;
            }

            .mGridImg1 th {
                padding: 4px 2px;
                color: white;
                background: #336699;
                border-color: Black;
                border-left: solid 1px Black;
                border-right: solid 1px Black;
                border-top: solid 1px Black;
                border-bottom: solid 1px Black;
                font-weight: normal;
                font-size: small;
                font-family: Calibri;
            }

            .mGridImg1 .pgr {
                background: #336699;
            }

                .mGridImg1 .pgr table {
                    margin: 5px 0;
                }

                .mGridImg1 .pgr td {
                    border-width: 0;
                    text-align: left;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: Red;
                    line-height: 12px;
                }

                .mGridImg1 .pgr a {
                    color: White;
                    text-decoration: none;
                }

                    .mGridImg1 .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
					.display-table .table td {
    padding: 15px 15px !important;}
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

                var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                var divi1 = $('#<%=ddlSrc2.ClientID%> :selected').text();
                if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc2').focus(); return false; }
                if ($("#txtsearch").val() == "") { alert("Enter Doctor Name."); $('#txtsearch').focus(); return false; }

            });
            $('#btnGo').click(function () {
                var st = $('#<%=ddlSFCode.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Field Force Name."); $('ddlSFCode').focus(); return false; }

            });
        });
    </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlSFCode]');
            var $items = $('select[id$=ddlSFCode] option');

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

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Listed Doctor Details</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <table width="90%">
                                    <tr>
                                        <td align="right" colspan="3">
                                            <%--     <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" 
                    onclick="btnBack_Click" />--%>
                                            <div style="margin-left: 90%">
                                                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 8.2%"></td>
                                        <td></td>
                                        <td align="right" width="30%">
                                            <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana"
                                                Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div class="row clearfix">

                                    <div class="col-lg-6">
                                        <div class="row">
                                            <div class="col-lg-10">
                                                <asp:Panel ID="pnlAdmin" runat="server">
                                                    <asp:Label ID="lblSalesforce" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>
                                                    <%-- <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                                ToolTip="Enter Text Here"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlSFCode" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                                    </asp:DropDownList>

                                                </asp:Panel>
                                            </div>
                                            <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                                <asp:Button ID="btnGo" runat="server" Width="50px" Text="Go" CssClass="savebutton"
                                                    OnClick="btnSubmit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                 <br />
                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-11">
                                        <asp:Button ID="btnQAdd" runat="server" CssClass="savebutton" Text="Add Listed Doctor"
                                            Width="170px" OnClick="btnQAdd_Click" />
                                        <asp:Button ID="btnEdit" runat="server" CssClass="resetbutton" Text="Edit All Listed Doctor"
                                            Width="160px" OnClick="btnEdit_Click" />
                                        <asp:Button ID="btnDeAc" runat="server" CssClass="resetbutton" Text="Deactivate Listed Doctor"
                                            Width="175px" OnClick="btnDeAc_Click" />
                                        <asp:Button ID="btnDAdd" runat="server" CssClass="resetbutton" Text="Detail Add Listed Doctor"
                                            Width="175px" OnClick="btnDAdd_Click" />
                                        <asp:Button ID="btnSlNoChg" runat="server" CssClass="resetbutton" Text="Change Sl.No"
                                            Width="120px" OnClick="btnSlNoChg_Click" />
                                        <asp:Button ID="btnReAc" runat="server" CssClass="resetbutton" Text="Reactivate Listed Doctor"
                                            Width="170px" OnClick="btnReAc_Click" />
                                        <asp:Button ID="btntypemap" runat="server" CssClass="resetbutton" Width="130px" Visible="false"
                                            Text="ListedDr-Type Map" OnClick="btntypemap_Click" />
                                        <asp:Button ID="btnpromap" runat="server" CssClass="resetbutton" Width="160px" Text="Listed Dr-Product Tag"
                                            OnClick="btnpromap_Click" />
                                        <asp:Button ID="btnChemMap" runat="server" CssClass="resetbutton" Width="175px" Text="Listed Dr-Chemist Tag"
                                            OnClick="btnChemMap_Click" />
                                         <asp:Button ID="btnDrProMap" runat="server" CssClass="resetbutton" Width="205px" Text="Classic Listed dr product Map"
                                            OnClick="btnDrProMap_Click" />
                                    </div>
                                </div>
                                <br />
                                <br />

                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label>
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                            <%--   <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                            <asp:ListItem Text="Doctor Name" Value="7"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="single-des clearfix" style="padding-top: 19px;">
                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%"
                                                Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px;">
                                            <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" CssClass="nice-select"
                                                TabIndex="4">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="50px"
                                            Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                                    </div>


                                </div>

                            </div>

                            <br />
                            <br />
                            <div class="row" style="scrollbar-width: thin; overflow-x: auto">
                                <table width="100%">
                                    <tr>
                                        <td align="center" colspan="2" style="width: 50%">
                                            <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                                runat="server" HorizontalAlign="Center" AlternatingItemStyle-ForeColor="Red">
                                                <SeparatorTemplate>
                                                </SeparatorTemplate>
                                                <ItemTemplate>
                                                    &nbsp
                            <asp:LinkButton ID="lnkbtnAlpha" Font-Size="15px"
                                runat="server" CommandArgument='<%#bind("ListedDr_Name") %>' Text='<%#bind("ListedDr_Name") %>'>
                            </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:DataList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="row" style="scrollbar-width: thin; overflow-x: auto">
                                <div class="col-lg-12">
                                    <asp:Panel ID="pnlselect" runat="server" Visible="false">
                                        <table width="100%">
                                            <tr>
                                                <td align="center">
                                                    <asp:Label ID="lblSelect" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"
                                                        Text="Select the FieldForce and Press the 'Go' Button"></asp:Label>

                                                    <asp:Label ID="lblSelec1" runat="server" Font-Size="Large" ForeColor="Red" Visible="false"
                                                        Text="Click the 'ALL' Link"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </div>
                            </div>

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                    <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center"
                                        EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                                        PageSize="10" OnPageIndexChanging="grdDoctor_PageIndexChanging" OnRowCreated="grdDoctor_RowCreated"
                                        OnRowUpdating="grdDoctor_RowUpdating" OnRowEditing="grdDoctor_RowEditing" OnRowCancelingEdit="grdDoctor_RowCancelingEdit"
                                        OnRowDataBound="grdDoctor_RowDataBound" GridLines="None" CssClass="table"
                                        PagerStyle-CssClass="gridview1" AllowSorting="True" style="background-color:white"
                                        OnSorting="grdDoctor_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="ListedDr_Name" HeaderText="Listed Doctor Name"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDocName" CssClass="input" Height="38px" runat="server" Enabled="false" Text='<%#Eval("ListedDr_Name")%>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDoc" runat="server" SetFocusOnError="true" ControlToValidate="txtDocName" Display="Dynamic"
                                                        ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_Cat_ShortName" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDocCat" AutoPostBack="false" runat="server" CssClass="nice-select" DataSource="<%# Doc_Category() %>"
                                                        DataTextField="Doc_Cat_SName" DataValueField="Doc_Cat_Code">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddlDocCat" ID="RequiredFieldValidator2"
                                                        ErrorMessage="*Required" InitialValue="0" runat="server"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_Spec_ShortName" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Speciality">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDocSpec" runat="server" CssClass="nice-select" DataSource="<%# Doc_Speciality() %>"
                                                        DataTextField="Doc_Special_SName" DataValueField="Doc_Special_Code">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddlDocSpec" ID="RequiredFieldValidator3"
                                                        ErrorMessage="*Required" InitialValue="0" runat="server"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_Qua_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Qualification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDocQua" runat="server" CssClass="nice-select" DataSource="<%# Doc_Qualification() %>"
                                                        DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddlDocQua" ID="RequiredFieldValidator4"
                                                        ErrorMessage="*Required" InitialValue="0" runat="server"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_Class_ShortName" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Class">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDocClass" runat="server" CssClass="nice-select" DataSource="<%# Doc_Class() %>"
                                                        DataTextField="Doc_ClsSName" DataValueField="Doc_ClsCode">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddlDocClass" ID="RequiredFieldValidator5"
                                                        ErrorMessage="*Required" InitialValue="0" runat="server"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="territory_Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderText="Territory">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlterr" runat="server" CssClass="nice-select" DataSource="<%# Doc_Territory() %>"
                                                        DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ControlToValidate="ddlterr" ID="RequiredFieldValidator6"
                                                        ErrorMessage="*Required" InitialValue="0" runat="server"
                                                        Display="Dynamic"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-CssClass="alignment"
                                                HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"></asp:CommandField>
                                            <asp:HyperLinkField HeaderText="View" Text="View" DataNavigateUrlFormatString="ListedDr_DetailAdd.aspx?type=1&ListedDrCode={0}"
                                                DataNavigateUrlFields="ListedDrCode" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="ListedDr_DetailAdd.aspx?type=2&ListedDrCode={0}"
                                                DataNavigateUrlFields="ListedDrCode" ItemStyle-HorizontalAlign="Center"></asp:HyperLinkField>
                                            <asp:HyperLinkField HeaderText="Add/Deactivate" ItemStyle-HorizontalAlign="Center"
                                                Text="Add/Deactivate" DataNavigateUrlFormatString="AddAgainstDeactivatedDR.aspx?dr_code={0}"
                                                DataNavigateUrlFields="ListedDrCode"></asp:HyperLinkField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
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
                <img src="../../../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

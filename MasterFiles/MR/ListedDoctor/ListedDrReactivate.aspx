<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDrReactivate.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_ListedDrReactivate" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Reactivation</title>
    <%-- <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
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

        .marRight {
            margin-right: 35px;
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

                var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                var divi1 = $('#<%=ddlSrc2.ClientID%> :selected').text();
                if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc2').focus(); return false; }
                if ($("#txtsearch").val() == "") { alert("Enter Doctor Name."); $('#txtsearch').focus(); return false; }

            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server"></div>
            <%--<ucl:Menu ID="menu1" runat="server" />--%>

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Listed Doctor Reactivation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="center">
                                    <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                                </asp:Panel>

                                <table id="Table1" runat="server" width="90%">
                                    <tr>
                                        <td align="right" width="30%">
                                            <%--    <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <%-- <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server"
                                                OnClick="btnBack_Click" />--%>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label>
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select"
                                            AutoPostBack="true" TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                            <%--  <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                            <asp:ListItem Text="Doctor Name" Value="7"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-3">
                                        <div class="single-des clearfix" style="padding-top: 19px;">
                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%" Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px;">
                                            <asp:DropDownList ID="ddlSrc2" runat="server" AutoPostBack="true" Visible="false"
                                                OnSelectedIndexChanged="ddlSrc2_SelectedIndexChanged" CssClass="nice-select"
                                                TabIndex="4">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="50px" Text="Go" OnClick="Btnsrc_Click"
                                            Visible="false" />
                                    </div>

                                </div>
                            </div>

                            <br />
                            <br />

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table"
                                        OnRowCreated="grdDoctor_RowCreated" OnRowDataBound="grdDoctor_RowDataBound"
                                        AllowSorting="True" OnSorting="grdDoctor_Sorting">

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
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="ListedDr_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Listed Deactivated Doctors Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="ListedDr_Deactivate_Date" ItemStyle-HorizontalAlign="Left" HeaderText="Deactivated Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeact_Date" runat="server" Text='<%#Eval("ListedDr_Deactivate_Date")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Doc_Special_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Speciality">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_Cat_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_QuaName" ItemStyle-HorizontalAlign="Left" HeaderText="Qualification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQua" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_ClsName" ItemStyle-HorizontalAlign="Left" HeaderText="Class">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="territory_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Territory">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
        <div class="div_fixed">
            <asp:Button ID="btnSave" runat="server" CssClass="savebutton" Text="Re-Activate" OnClick="btnSave_Click" Visible="false" />
        </div>
        <br />
        <br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>

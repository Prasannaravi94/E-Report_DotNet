<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnLstDoctorList.aspx.cs" Inherits="MasterFiles_MR_UnListedDoctor_UnLstDoctorList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UnListed Doctor Details</title>
     <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
       <%--<link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />--%>
    <%--<link href="../../../assets/css/style.css" rel="stylesheet" />--%>
      <style type="text/css">
        .modal
        {
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
        .loading
        {
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
        .align
          {
              min-width:120px;
          }
          #dlAlpha tr td
         {
             padding-right:1px;
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
                 if (divi1 == "---Select---") { alert("Select " +divi); $('#ddlSrc2').focus(); return false; }
                 if ($("#txtsearch").val() == "") { alert("Enter Doctor Name."); $('#txtsearch').focus(); return false; }

             });
         }); 
    </script>
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
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">UnListed Doctor Details</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-2">
                                        <%--<asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" 
                                        onclick="btnBack_Click" />--%>
                                        <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />
                                        <asp:Panel ID="pnlAdmin" runat="server">
                                            <asp:Label ID="lblSalesforce" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>
                                            <asp:DropDownList ID="Alpha" runat="server" AutoPostBack="true" CssClass="nice-select" OnSelectedIndexChanged="Alpha_SelectedIndexChanged">
                                                <asp:ListItem Selected="true">---ALL---</asp:ListItem>
                                                <asp:ListItem>A</asp:ListItem>
                                                <asp:ListItem>B</asp:ListItem>
                                                <asp:ListItem>C</asp:ListItem>
                                                <asp:ListItem>D</asp:ListItem>
                                                <asp:ListItem>E</asp:ListItem>
                                                <asp:ListItem>F</asp:ListItem>
                                                <asp:ListItem>G</asp:ListItem>
                                                <asp:ListItem>H</asp:ListItem>
                                                <asp:ListItem>I</asp:ListItem>
                                                <asp:ListItem>J</asp:ListItem>
                                                <asp:ListItem>K</asp:ListItem>
                                                <asp:ListItem>L</asp:ListItem>
                                                <asp:ListItem>M</asp:ListItem>
                                                <asp:ListItem>N</asp:ListItem>
                                                <asp:ListItem>O</asp:ListItem>
                                                <asp:ListItem>P</asp:ListItem>
                                                <asp:ListItem>Q</asp:ListItem>
                                                <asp:ListItem>R</asp:ListItem>
                                                <asp:ListItem>S</asp:ListItem>
                                                <asp:ListItem>T</asp:ListItem>
                                                <asp:ListItem>U</asp:ListItem>
                                                <asp:ListItem>V</asp:ListItem>
                                                <asp:ListItem>W</asp:ListItem>
                                                <asp:ListItem>X</asp:ListItem>
                                                <asp:ListItem>Y</asp:ListItem>
                                                <asp:ListItem>Z</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-lg-4" style="margin-top: 24px; padding: 0px;">
                                        <asp:DropDownList ID="ddlSFCode" runat="server"
                                            CssClass="custom-select2 nice-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2" style="margin-top: auto;">
                                        <asp:Button ID="btnGo" runat="server" Width="45px" Text="Go" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                        <asp:Label ID="lblTerrritory" runat="server" SkinID="lblMand" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="display-name-heading clearfix">
                            <div class="row clearfix">
                                <div class="col-lg-10">
                                    <asp:Button ID="btnQAdd" runat="server" CssClass="savebutton" Text="Add UnListed Doctor" Width="145px" onclick="btnQAdd_Click"  />
                                    <asp:Button ID="btnEdit" runat="server" CssClass="resetbutton" Text="Edit All UnListed Doctor" Width="165px" onclick="btnEdit_Click" />
                                    <asp:Button ID="btnDeAc" runat="server" CssClass="resetbutton" Text="Deactivate UnListed Doctor" Width="185px" onclick="btnDeAc_Click" />
                                   <asp:Button ID="btnReAc" runat="server" CssClass="resetbutton" Text="Reactivate UnListed Doctor" Width="185px" onclick="btnReAc_Click" />
                                </div>
                            </div>
                            <br />
                            <div class="row clearfix">
                                <div class="col-lg-3">
                                    <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By" ></asp:Label>
                                    <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                        TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                        <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                        <%--  <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                        <asp:ListItem Text="Doctor Name" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-2" style="padding-top: 19px; padding-left: 0px;">
                                    <div class="single-des clearfix">
                                        <asp:TextBox id="txtsearch" runat="server" Width="100%" CssClass="input" Visible= "false" ></asp:TextBox>
                                    </div>
                                    <div style="margin-top: -20px">
                                        <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" OnSelectedIndexChanged="ddlSrc2_SelectedIndexChanged"
                                            CssClass="nice-select" TabIndex="4">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-1" style="margin: 17px; margin-left: -25px;">
                                    <asp:Button ID="Btnsrc" runat="server" Width="45px" CssClass="savebutton" Text=">>" OnClick="Btnsrc_Click" Visible="false" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row" style="scrollbar-width: thin; overflow-x: auto">
                            <table width="100%">
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                            runat="server" HorizontalAlign="Center">
                                            <SeparatorTemplate></SeparatorTemplate>
                                            <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnAlpha" Font-Size="14px" runat="server" CommandArgument='<%#bind("UnListedDr_Name") %>' Text='<%#bind("UnListedDr_Name") %>'>
                                            </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <table width="100%" align="center">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                                                    OnRowDataBound="grdDoctor_RowDataBound" OnRowUpdating="grdDoctor_RowUpdating" OnRowEditing="grdDoctor_RowEditing"
                                                    OnRowCancelingEdit="grdDoctor_RowCancelingEdit"
                                                    OnRowCreated="grdDoctor_RowCreated" OnPageIndexChanging="grdDoctor_PageIndexChanging" OnRowCommand="grdDoctor_RowCommand"
                                                    GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                                    AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdDoctor_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdDoctor.PageIndex * grdDoctor.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UnListed Doctor Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("UnListedDrCode")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField SortExpression="UnListedDr_Name" ItemStyle-HorizontalAlign="Left" HeaderText="UnListed Doctor Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("UnListedDr_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtDocName" CssClass="input" runat="server" Text='<%#Eval("UnListedDr_Name")%>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDoc" runat="server" ControlToValidate="txtDocName" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Doc_Special_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Speciality">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlDocSpec" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Speciality() %>" DataTextField="Doc_Special_Name" DataValueField="Doc_Special_Code">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ControlToValidate="ddlDocSpec" ID="RequiredFieldValidator2"
                                                                    ErrorMessage="*Required" InitialValue="0" runat="server"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Doc_Cat_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Category">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlDocCat" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Category() %>" DataTextField="Doc_Cat_Name" DataValueField="Doc_Cat_Code">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ControlToValidate="ddlDocCat" ID="RequiredFieldValidator3"
                                                                    ErrorMessage="*Required" InitialValue="0" runat="server"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" SortExpression="Doc_QuaName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlDocQua" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Qualification() %>" DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ControlToValidate="ddlDocQua" ID="RequiredFieldValidator4"
                                                                    ErrorMessage="*Required" InitialValue="0" runat="server"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left" SortExpression="Doc_ClsName">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlDocClass" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Class() %>" DataTextField="Doc_ClsName" DataValueField="Doc_ClsCode">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ControlToValidate="ddlDocClass" ID="RequiredFieldValidator5"
                                                                    ErrorMessage="*Required" InitialValue="0" runat="server"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left" SortExpression="territory_Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddlterr" runat="server" SkinID="ddlRequired" DataSource="<%# Doc_Territory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ControlToValidate="ddlterr" ID="RequiredFieldValidator6"
                                                                    ErrorMessage="*Required" InitialValue="0" runat="server"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="CENTER" ItemStyle-CssClass="align"
                                                            ShowEditButton="True"></asp:CommandField>
                                                        <asp:HyperLinkField HeaderText="View" Text="View" ItemStyle-HorizontalAlign="CENTER" DataNavigateUrlFormatString="UnListedDr_View.aspx?type=1&UnListedDrCode={0}"
                                                            DataNavigateUrlFields="UnListedDrCode"></asp:HyperLinkField>
                                                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="CENTER" DataNavigateUrlFormatString="UnListedDr_View.aspx?type=2&UnListedDrCode={0}"
                                                            DataNavigateUrlFields="UnListedDrCode"></asp:HyperLinkField>
                                                        <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="CENTER">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("UnListedDrCode") %>'
                                                                    CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the UnListedDR');">Deactivate
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
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    </form>
</body>
</html>

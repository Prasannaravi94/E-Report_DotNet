<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorSpecialityList.aspx.cs" Inherits="MasterFiles_DoctorSpecialityList" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor-Speciality</title>

    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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
         min-width:130px!important;
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ucl:Menu ID="menu1" runat="server" /> 
     
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Doctor-Speciality</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-name-heading clearfix">
                            <div class="row clearfix">
                                <div class="col-lg-7">
                                    <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="65px" Text="Add" OnClick="btnNew_Click" />
                                    <asp:Button ID="btnBulkEdit" runat="server" CssClass="resetbutton" Width="80px" Text="Bulk Edit" OnClick="btnBulkEdit_Click" />
                                    <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="resetbutton" Width="80px" Text="S.No Gen" OnClick="btnSlNo_Gen_Click" />
                                    <asp:Button ID="btnTransfer" runat="server" CssClass="resetbutton" Width="130px" Text="Transfer Speciality" OnClick="btnTransfer_Spec_Click" />
                                    <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton" Width="120px" Text="Reactivation" OnClick="btnReactivate_Click" />
                                </div>
                            </div>
                        </div>
                        <p>
                         <br />
                         </p>
                        <div class="display-table clearfix">
                            <div class="table-responsive">
                                <asp:GridView ID="grdDocSpe" runat="server" Width="100%" HorizontalAlign="Center" 
                                 AutoGenerateColumns="false" AllowPaging="True" PageSize="10" 
                                 onrowupdating="grdDocSpe_RowUpdating" onrowediting="grdDocSpe_RowEditing"
                                 onrowdeleting="grdDocSpe_RowDeleting" EmptyDataText="No Records Found" 
                                 onpageindexchanging="grdDocSpe_PageIndexChanging" OnRowCreated="grdDocSpe_RowCreated"
                                 onrowcancelingedit="grdDocSpe_RowCancelingEdit"  onrowcommand="grdDocSpe_RowCommand"                 
                                 GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" 
                                   AlternatingRowStyle-CssClass="alt" AllowSorting="True" OnSorting="grdDocSpe_Sorting">
                                <Columns>
                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdDocSpe.PageIndex * grdDocSpe.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Speciality Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocSpeCode" runat="server" Text='<%#Eval("Doc_Special_Code")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="Doc_Special_SName" ItemStyle-Width="120px" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDoc_Spe_SName" runat="server" CssClass="input" MaxLength="6" Text='<%# Bind("Doc_Special_SName") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDoc_Spe_SName" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                        
                                    <asp:TemplateField SortExpression="Doc_Special_Name" ItemStyle-Width="260px" ItemStyle-HorizontalAlign="Left" HeaderText="Speciality Name">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDocSpeName" CssClass="input" Width="200px" runat="server" MaxLength="100" Text='<%# Bind("Doc_Special_Name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDocSpeName" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="No of Doctors" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Spec_Count") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="No of Slides" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblslide" runat="server" Text='<%# Bind("slide_count") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>      
                                    <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                         HeaderStyle-HorizontalAlign="CENTER" ItemStyle-HorizontalAlign="Center" ShowEditButton="True"  ItemStyle-CssClass="align">
                                    </asp:CommandField>
                                    <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-Width="130px" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="DoctorSpeciality.aspx?Doc_Special_Code={0}"
                                        DataNavigateUrlFields="Doc_Special_Code">
                                    </asp:HyperLinkField>
                                    <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="150px">
                                       <%-- <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Doc_Special_Code") %>'
                                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Doctor Speciality');">Deactivate
                                            </asp:LinkButton>
                                            <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                            <img src="../Images/deact2.png" alt="" width="75px" title="This Speciality Exists in Doctor" />
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Delete">
                                       <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                        </ControlStyle>
                                        <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Doc_Special_Code") %>'
                                                CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Doctor Speciality');">Delete
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                       </asp:TemplateField>--%>
                                </Columns>
                                <EmptyDataRowStyle CssClass="no-result-area"/>
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
    <img src="../Images/loader.gif" alt="" />
</div>
    </div>
    </form>
</body>
</html>

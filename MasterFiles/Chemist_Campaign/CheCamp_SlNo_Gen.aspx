<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheCamp_SlNo_Gen.aspx.cs" Inherits="MasterFiles_Chemist_Campaign_CheCamp_SlNo_Gen" %>

<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Chemist Campaign Serial No Generation</title>
     <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
   <%-- <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
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
</head>
<body>
      <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />


            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" id="Heading" runat="server"></h2>
                        <div class="designation-reactivation-table-area clearfix">

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin">
                        <asp:GridView ID="grdCheCat" runat="server" Width="50%" HorizontalAlign="Center" EmptyDataText="No Records Found" 
                            AutoGenerateColumns="false" AllowSorting="True" OnSorting="grdCheSubCat_Sorting" GridLines="Both"
                             CssClass="mGrid" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood"/>
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>                
                               <asp:TemplateField HeaderText="Che_SubCatCode" Visible="false">
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="Che_Cat_Code" runat="server" Text='<%#   Bind("chm_campaign_code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="chm_campaign_SName" SortExpression="chm_campaign_SName" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Left" ShowHeader="true" HeaderText="Short Name" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="chm_campaign_name" SortExpression="chm_campaign_name" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Left" HeaderText="Chemist Campaign Name" ShowHeader="true" ItemStyle-Width="20%"/>
                                <asp:TemplateField HeaderText="Existing S.No" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="New S.No" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSlNo" onkeypress="CheckNumeric(event);" MaxLength="3" runat="server" Width="50%" SkinID="MandTxtBox"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
   <EmptyDataRowStyle CssClass="no-result-area" />                       

                        </asp:GridView>
                                     </div>
                            </div>
                            <br />
                   
        <br />
        <center>
            <asp:Button ID="btnSubmit" runat="server" Width="150px" Height="30px" Text="Generate - Sl No" CssClass="BUTTON"
                onclick="btnSubmit_Click" />
            &nbsp;
            <asp:Button ID="btnClear" runat="server" Width="60px" Height="30px" Text="Clear" CssClass="BUTTON"
                onclick="btnClear_Click" />
        </center>
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
    </form>
</body>
</html>


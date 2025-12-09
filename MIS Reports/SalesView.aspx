<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesView.aspx.cs" Inherits="MIS_Reports_SalesView" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Sales Bill View</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
      <script type="text/javascript" src="../JScript/DateJs/date.js"></script>
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
         td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
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
      <div id="Divid" runat="server">
        </div>

        <br />
            <br />
         <br />
           <table width="100%" align="center">
            <tbody>
            
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdSalesBillView" runat="server" Width="87%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="False"                           
                            GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="#8989ca" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>

                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdSalesBillView.PageIndex * grdSalesBillView.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                           
                          <%--      <asp:TemplateField HeaderText="Sl No." ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblSlNo" runat="server" Text='<%#Eval("Sl_No")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>

<HeaderStyle ForeColor="White"></HeaderStyle>


<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>--%>
                                      <asp:TemplateField HeaderText="Division Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblDivisionName" runat="server" Text='<%#Eval("Division_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>

<HeaderStyle ForeColor="White"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Date" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblsaledate" runat="server" Text='<%#Eval("Sale_Date")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>

<HeaderStyle ForeColor="White"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="EMP Code" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblEmpcode" runat="server" Text='<%#Eval("Emp_Code")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>

<HeaderStyle ForeColor="White"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Employee Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblhq" runat="server" Text='<%#Eval("Emp_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>

<HeaderStyle ForeColor="White"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="HQ Code" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblHQ_Code" runat="server" Text='<%#Eval("HQ_Code")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>

<HeaderStyle ForeColor="White"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="HQ Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblHQ_Name" runat="server" Text='<%#Eval("HQ_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>

<HeaderStyle ForeColor="White"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Chemist ERP Code" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblChemist_ERP_Code" runat="server" Text='<%#Eval("Chemist_ERP_Code")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
<HeaderStyle ForeColor="White"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblChemist_Name" runat="server" Text='<%#Eval("Chemist_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
<HeaderStyle ForeColor="White"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblDoctor_Name" runat="server" Text='<%#Eval("Doctor_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
<HeaderStyle ForeColor="White"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                
                                  <asp:TemplateField HeaderText="Doctor Code"
 ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblDoctor_Code" runat="server" Text='<%#Eval("Doctor_Code")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
<HeaderStyle ForeColor="White"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Sales Qty"
 ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblSale_Qty" runat="server" Text='<%#Eval("Sale_Qty")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
<HeaderStyle ForeColor="White"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Sales Value"
 ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblSale_Value" runat="server" Text='<%#Eval("Sale_Value")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
<HeaderStyle ForeColor="White"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Product Name"
 ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblProduct_Name" runat="server" Text='<%#Eval("Product_Name")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
<HeaderStyle ForeColor="White"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Code" 
 ItemStyle-HorizontalAlign="Left" 
                                    HeaderStyle-ForeColor="White">                                    
                                    <ItemTemplate>
                                     <asp:Label ID="lblProduct_Code" runat="server" Text='<%#Eval("Product_Code")%>' SkinID="lblMand"></asp:Label>
                                    </ItemTemplate>
<HeaderStyle ForeColor="White"></HeaderStyle>
<ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:TemplateField>
                                
                                  
                             

                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle"  />
                                </asp:GridView>
    </td>
    </tr>
    </tbody>
    </table>
          <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>

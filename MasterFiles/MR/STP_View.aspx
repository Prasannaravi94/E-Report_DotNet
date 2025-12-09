<%@ Page Language="C#" AutoEventWireup="true" CodeFile="STP_View.aspx.cs" Inherits="MasterFiles_MR_STP_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<link type="text/css" rel="stylesheet" href="../css/Report.css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script language="Javascript">
         function RefreshParent() {
             window.opener.document.getElementById('form1').click();
             window.close();
         }
    </script>
     <script type="text/javascript">
         $(function () {
             $('#btnExcel').click(function () {
                 var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                 location.href = url
                 return false
             })
         })
    </script>

     <style type="text/css">
     .box
        {
            background: #FFFFFF;
         
            border: 2px solid #FC8EAC;
            border-radius: 8px;
        }
        
         .box2
        {
            background: #FFFFFF;
            border: 2px solid #7E8D29;
            border-radius: 8px;
        }
        
        .GridStyle
{
    border: 6px solid rgb(217, 231, 255);
    background-color: White;
    font-family: arial;
    font-size: 11px;
    border-collapse: collapse;
    margin-bottom: 0px;
}
.GridStyle tr
{
    border: 1px solid rgb(217, 231, 255);
    color: Black;
    height: 10px;
}
/* Your grid header column style */
.GridStyle th
{
    background-color: rgb(217, 231, 255);
    border: none;
    text-align: left;
    font-weight: bold;
    font-size: 15px;
    padding: 4px;
    color:Black;
}
/* Your grid header link style */
.GridStyle tr th a,.GridStyle tr th a:visited
{
        color:Black;
}
.GridStyle tr th, .GridStyle tr td table tr td
{
    border: none;
}

.GridStyle td
{
    border-bottom: 1px solid rgb(217, 231, 255);
    padding: 2px;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div>

        <asp:Panel ID="pnlbutton" runat="server">
        <table width="100%">
            <tr>
            <td width="20%"></td><td></td>
                <td width="80%" align="center" >
                <asp:Label ID="lblccp" runat="server" Text="STP - View" Font-Size="18px" Font-Underline="true" Font-Bold="true" ForeColor="Magenta"></asp:Label>
                </td>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClick="btnPrint_Click" />
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                   />
                            </td>
                           
                            <td>
                                <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                    OnClientClick="RefreshParent();" OnClick="btnClose_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
       
    </asp:Panel>

    <center>
    <table >
  <tr align="center">
  
              <td align="center" >
               <asp:Label ID="lblfieldname" runat="server" Font-Size="14px" Text="Fieldforce Name:" ></asp:Label>
               <asp:Label ID="lblname" runat="server" Font-Bold="true" Font-Size="16px" ForeColor="#C36241"></asp:Label>
              </td>
                    </tr>

                
    </table>
   
    </center>
    <center> 
        <asp:Panel ID="pnlContents" runat="server">
           <asp:Label ID="Label2" Text="Plan Name : Monday 1" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="lblday" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label1" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label3" Text="Plan Name : Monday 2" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label4" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label5" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label6" Text="Plan Name : Monday 3" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label7" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label8" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

           

             <asp:Label ID="Label12" Text="Plan Name : Monday 4" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label13" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label14" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label15" Text="Plan Name : Tuesday 1" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label16" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_t1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label17" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_t1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>
             <asp:Label ID="Label18" Text="Plan Name : Tuesday 2" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label19" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_t2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label20" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_t2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label21" Text="Plan Name : Tuesday 3" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label22" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_t3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label23" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_t3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label24" Text="Plan Name : Tuesday 4" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center"  width="100%" >
           
                <tr >
                    <td align="center"  width="50%"style="vertical-align:top">
                    
              
                            <asp:Label ID="Label25" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_t4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label26" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_t4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label27" Text="Plan Name : Wednesday 1" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
            <table align="center" width="100%" >
           
                <tr > 
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label28" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_w1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label29" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_w1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label30" Text="Plan Name : Wednesday 2" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label31" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_w2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label32" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_w2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label33" Text="Plan Name : Wednesday 3" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label34" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_w3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label35" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_w3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label36" Text="Plan Name : Wednesday 4" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label37" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_w4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%"  style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label38" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_w4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label39" Text="Plan Name : Thursday 1" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label40" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_th1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label41" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_th1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label42" Text="Plan Name : Thursday 2" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label43" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_th2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label44" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_th2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label45" Text="Plan Name : Thursday 3" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label46" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_th3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label47" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_th3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label48" Text="Plan Name : Thursday 4" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label49" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_th4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label50" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_th4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label51" Text="Plan Name : Friday 1" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label52" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_f1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label53" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_f1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label54" Text="Plan Name : Friday 2" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center"  width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label55" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_f2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label56" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_f2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label57" Text="Plan Name : Friday 3" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label58" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_f3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label59" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_f3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label60" Text="Plan Name : Friday 4" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label61" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_f4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label62" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_f4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label63" Text="Plan Name : Saturday 1" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label64" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_sa1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label65" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_sa1" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label66" Text="Plan Name : Saturday 2" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label67" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_sa2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label68" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_sa2" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label69" Text="Plan Name : Saturday 3" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label70" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_sa3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label71" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_sa3" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

             <asp:Label ID="Label72" Text="Plan Name : Saturday 4" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#724624"></asp:Label>
            <table align="center" width="100%" >
           
                <tr >
                    <td align="center" width="50%" style="vertical-align:top">
                    
              
                            <asp:Label ID="Label73" Text="Doctor" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                   
                        <asp:GridView ID="grddr_sa4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Doctor Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("ListedDr_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                  <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQual" runat="server" Text='<%#  Eval("Doc_Qua_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblspec" runat="server" Text='<%#  Eval("Doc_Spec_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#  Eval("Doc_Cat_ShortName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Visit Fqy" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvisit" runat="server" Text='<%#  Eval("visit_frequency") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                         </td>
                       <td align="center" width="50%" style="vertical-align:top">
                       
                        
                         <asp:Label ID="Label74" Text="Chemist" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                         <asp:GridView ID="grdchem_sa4" runat="server" Width="98%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt" EmptyDataText="No Records Found">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                               <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <%-- <asp:TemplateField HeaderText="Plan Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlan_Name" runat="server" Text='<%#  Eval("Day_Plan_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Chemist Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemists_Name" runat="server" Text='<%#  Eval("Chemists_Name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>

                                
                                

                               <asp:TemplateField HeaderText="Patch Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterritory_name" runat="server" Text='<%#  Eval("territory_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>


                            </Columns>
                            <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                   
                </tr>
            
               
            </table>

            
            
        
        </asp:Panel>
        </center>
    </div>
    </form>
</body>
</html>


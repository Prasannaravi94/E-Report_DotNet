<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptTPCCP_View.aspx.cs" Inherits="MIS_Reports_rptTPCCP_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CCP - View</title>
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
                <asp:Label ID="lblccp" runat="server" Text="CCP - View" Font-Size="18px" Font-Underline="true" Font-Bold="true" ForeColor="Magenta"></asp:Label>
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
        <br />
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
    <br />
    </center>
    <center>
        <asp:Panel ID="pnlContents" runat="server">
            <table align="center" width="90%" class="box">
           
                <tr >
                    <td align="center" style="display:inline;">
                    
                        <div>
                            <asp:Label ID="jan" Text="January" Font-Bold="true" runat="server" Font-Size="16px" ForeColor="#728C00"></asp:Label>
                        </div>
                        <asp:GridView ID="grdjan" runat="server" Width="60%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" OnRowDataBound="grdTP_RowDataBound" CssClass="GridStyle" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                    </td>
                    <td align="center" style="display:inline">
                      <div>
                <asp:Label ID="feb" Text="February" Font-Bold="true" runat="server" ForeColor="#DE5D83" Font-Size="16px" ></asp:Label>
                </div>
                        <asp:GridView ID="grdfeb" runat="server" Width="80%" HorizontalAlign="Center" RowStyle-Wrap="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                    </td>
                    <td align="center" style="display:inline" >
                      <div>
                <asp:Label ID="mar" Text="March" runat="server" Font-Bold="true" ForeColor="#79443B" Font-Size="16px" ></asp:Label>
                </div>
                        <asp:GridView ID="grdmar" runat="server" Width="80%" HorizontalAlign="Center" RowStyle-Wrap="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                    </td>
                    <td align="center" style="display:inline" >
                      <div>
                <asp:Label ID="apr" Text="April" runat="server" Font-Bold="true" ForeColor="#CC0000" Font-Size="16px"></asp:Label>
                </div>
                        <asp:GridView ID="grdapr" runat="server" Width="80%" HorizontalAlign="Center" RowStyle-Wrap="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td></td>

                  <td align="center" style="display:inline" >
                        <div>
                            <asp:Label ID="may" Text="May" Font-Bold="true" runat="server" ForeColor="#BF94E4" Font-Size="16px"></asp:Label>
                        </div>
                        <asp:GridView ID="grdmay" runat="server" Width="80%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td></td>
                     <td align="center" style="display:inline" >
                      <div>
                <asp:Label ID="jun" Text="June" runat="server" Font-Bold="true" ForeColor="#FF007F" Font-Size="16px"></asp:Label>
                </div>
                        <asp:GridView ID="grdjun" runat="server" Width="80%" HorizontalAlign="Center" RowStyle-Wrap="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
           
            
                    <td align="center" style="display:inline" >
                      <div>
                <asp:Label ID="jul" Text="July" runat="server" Font-Bold="true" ForeColor="#004225" Font-Size="16px" ></asp:Label>
                </div>
                        <asp:GridView ID="grdjul" runat="server" Width="80%" HorizontalAlign="Center" RowStyle-Wrap="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td>
                    </td>
                    <td align="center" style="display:inline" >
                      <div>
                <asp:Label ID="aug" Text="August" runat="server" Font-Bold="true" ForeColor="#800020" Font-Size="16px" ></asp:Label>
                </div>
                        <asp:GridView ID="grdaug" runat="server" Width="80%" HorizontalAlign="Center" RowStyle-Wrap="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td></td>
                      <td align="center" style="display:inline">
                        <div>
                            <asp:Label ID="sep" Text="September" runat="server" Font-Bold="true" ForeColor="#99BADD" Font-Size="16px"></asp:Label>
                        </div>
                        <asp:GridView ID="grdsep" runat="server" Width="80%" HorizontalAlign="Center" AutoGenerateColumns="false" RowStyle-Wrap="false"
                            GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound" AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td></td>
                       <td align="center" style="display:inline" >
                      <div>
                <asp:Label ID="oct" Text="October" runat="server" Font-Bold="true" ForeColor="#ED9121" Font-Size="16px"></asp:Label>
                </div>
                        <asp:GridView ID="grdoct" runat="server" Width="80%" HorizontalAlign="Center" RowStyle-Wrap="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td></td>
                       <td align="center" style="display:inline" >
                      <div>
                <asp:Label ID="nov" Text="November" runat="server" Font-Bold="true" ForeColor="#007BA7" Font-Size="16px"></asp:Label>
                </div>
                        <asp:GridView ID="grdnov" runat="server" Width="80%" HorizontalAlign="Center" RowStyle-Wrap="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td></td>
                         
                    <td align="center" style="display:inline" >
                      <div>
                <asp:Label ID="dec" Text="December" runat="server" Font-Bold="true" ForeColor="#2A52BE" Font-Size="16px"></asp:Label>
                </div>
                        <asp:GridView ID="grddec" runat="server" Width="80%" HorizontalAlign="Center" RowStyle-Wrap="false"
                            AutoGenerateColumns="false" GridLines="None" CssClass="GridStyle" OnRowDataBound="grdTP_RowDataBound"
                            AlternatingRowStyle-CssClass="alt">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Date" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%#  Eval("date") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Day" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay_name" runat="server" Text='<%#  Eval("day_name") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CCP" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldays_check" runat="server" Text='<%#  Eval("days_check") %>' ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
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

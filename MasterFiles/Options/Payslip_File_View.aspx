<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payslip_File_View.aspx.cs"
    Inherits="Payslip_File_View" %>
    <%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu1" TagPrefix="ucl1" %>
<%@ Register Src ="~/UserControl/MR_Menu.ascx" TagName ="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payslip Files View</title>
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
         <style type="text/css">
         
    
     .gridview1{
    background-color:#99B7B7;
    border-style:none;
   padding:2px;
   margin:2% auto;
   
   
}

 .gridview1 a{
  margin:auto 1%;
  border-style:none;
    border-radius:50%;
      background-color:#444;
      padding:5px 7px 5px 7px;
      color:#fff;
      text-decoration:none;
      -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;
     
}
.gridview1 a:hover{
    background-color:#1e8d12;
    color:#fff;
}
.gridview1 td{
    border-style:none;
}
.gridview1 span{
    background-color:#ae2676;
    color:#fff;
     -o-box-shadow:1px 1px 1px #111;
      -moz-box-shadow:1px 1px 1px #111;
      -webkit-box-shadow:1px 1px 1px #111;
      box-shadow:1px 1px 1px #111;

    border-radius:50%;
    padding:5px 7px 5px 7px;
}
   </style>
  
</head>
<body>
    <form id="form1" runat="server">
     <div>
      <div id="Divid" runat="server"></div>
     <br />
    <center>
        <asp:GridView ID="GridView1" runat="server" CssClass="mGridImg"  Width="40%" AutoGenerateColumns="false" EmptyDataText="No Files Available">
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server"  />
                           <asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("Value") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

              
             
               
                <asp:BoundField DataField="Text" HeaderText="File Name" />
                  <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Download">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                            CommandArgument='<%# Eval("Value") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
              <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:GridView>
        <br />
        <asp:LinkButton ID="btnDownload" runat="server" Text="Bulk Download" Width="130px" Height="25px" BackColor="LightBlue" Visible="false"  OnClick="DownloadFiles" />
        
    </center>
    </div>
    </form>
</body>
</html>

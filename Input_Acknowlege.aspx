<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input_Acknowlege.aspx.cs" Inherits="Input_Acknowlege" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Acknowledge</title>

   
     <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/style.css" />
     <style type="text/css">
         #rdoyesNo
{
color:Black;
font-family:Verdana;
font-size:25px;
font-style:normal;
font-weight:bold;
}

#Red
{
color:#FF0019;
}
#Green
{
color:#2B5400;
}
</style>
   <script type="text/javascript">
        function Validate() {
            //Reference the GridView.
    
            var gv = document.getElementById("<%= grdinput.ClientID %>");
        var lb = gv.getElementsByTagName("span");
        var rowCount = gv.rows.length;
        //Reference all INPUT elements.
        // var inputs = grid.getElementsByTagName("INPUT");
 
        //Set the Validation Flag to True.
       // var isValid = true;
        for (var i = 1; i < rowCount; i++) {

            var row = gv.rows[i];
            var txt = row.getElementsByTagName('input');
            var label = row.getElementsByTagName('span');
            var area = row.getElementsByTagName('textarea');
            if (number(txt[0].value) > number(label[2].innerHTML)) {
                alert('Not Possible to Enter more then Despatched Qty')
                txt[0].focus();
                return false;
            } else {
                if (txt[0].value != label[2].innerHTML) {
                  
                    if (area[0].value == "") {
                        alert('Enter Remarks')
                        area[0].focus();
                      
                        return false;
                    }
                    else {

                    }
                }

            }

        }
 
        }
    </script> 
   
</head>
<body>
    <form id="form1" runat="server">
          <div id="Divid" runat="server">
            </div>
        <br/>
        
            <br />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                            <tr>
                                <td align="center">
                                    <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize; font-size: 22px;"
                                        CssClass="reportheading"> </asp:Label>
                                </td>
                                 
                            </tr>
                            <tr style="height:15px">

                            </tr>
                            
                             <tr>
                                <td align="center">
                                    <h2 class="text-center">Input Acknowledgement</h2>
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="right">
                                    
                                  
                           <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" Visible="false" CssClass="savebutton"
                               Text="Direct to Home Page" OnClick="btngohome_click" />
                                    &nbsp;&nbsp;
                           <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" CssClass="resetbutton"
                               Text="Logout" OnClick="btnLogout_Click" />
                                </td>
                            </tr>
                             
                             <tr>
                              <td align="center">
                                  <asp:label ID="lblipmonth" runat="server" Text="" ForeColor="Black"  Font-Size="Medium"></asp:label> 
                                </td>
                            </tr>
                        </table>
                       
                        <center>
                            
                            <br />
                            <br />
                            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <div class="display-table clearfix">
                        <div class="table-responsive" style="scrollbar-width: thin;">
                            <asp:GridView ID="grdinput" runat="server" Width="100%" HorizontalAlign="Center"
                                AutoGenerateColumns="False" AllowSorting="true"
                               EmptyDataText="No Records Found" 
                                GridLines="None" CssClass="table">
                       
                          <Columns>
                           <asp:TemplateField HeaderText="S.No">
                                <ControlStyle Width="90%"></ControlStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdinput.PageIndex * grdinput.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Input Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblinputname" runat="server" Text='<%#Eval("Gift_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Despatch Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbldespatchiput" runat="server" Text='<%#Eval("Despatch_Actual_qty") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Reced.Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtreceivedqty" runat="server" Text='<%#Eval("Despatch_Actual_qty") %>' Width="50px" MaxLength="3"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="code" Visible="false">
                                <ItemTemplate>
                                     <asp:Label ID="lblcode" runat="server" visible="false" Text='<%#Eval("productc")  %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Num" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblnum" runat="server" Text='<%#Eval("upl_sl_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemarks" runat="server" Width="250px" 
                                                            TextMode="MultiLine"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                              </Columns>
                          
                        </asp:GridView>
                    </div>
                    </div>

                </div>

            </div>
                          <div class="single-des clearfix">
                                    <div class="single-des-option">
                                         <asp:Button ID="Button1" runat="server" Text="Save" Width="60px" CssClass="savebutton" OnClientClick="return Validate()"  OnClick="Button1_Click"  Visible="false"/>
                                        </div>
                            </div>
                            <br />
                            <br />
                            <div class="single-des clearfix" style="min-width:280px">
                                    
                                      <asp:RadioButtonList ID="rdoyesNo" AutoPostBack="true" runat="server"   
                                     RepeatDirection="Horizontal"  Width="200px" Height="40px" 
                                     onselectedindexchanged="rdoyesNo_SelectedIndexChanged" >
                                    <asp:ListItem ID="Green" Value="1">Yes</asp:ListItem>
                                    <asp:ListItem ID="Red"   Value="2">No</asp:ListItem>
                                   
                                </asp:RadioButtonList>
                                        </div>
                           
                        </center>
                    </div>
                </div>
            </div>
            <br />
            <br />
      
  
    </form>
</body>
</html>

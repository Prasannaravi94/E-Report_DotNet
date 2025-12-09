<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TaskStatus.aspx.cs" Inherits="Task_Management_TaskStatus" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Task Status</title>
        <style type="text/css">
      td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    </style>
 <script type="text/javascript" language="javascript">
     function fncheck() {
         var colname = document.getElementById("<%=txtTaskShortName.ClientID%>").value.trim();
         if (colname.length <= 0) {
             alert('Short Name should not be empty...');
             document.getElementById("<%=txtTaskShortName.ClientID%>").focus();
             return false;
         }

         var colname1 = document.getElementById("<%=txtTaskName.ClientID%>").value.trim();
         if (colname1.length <= 0) {
             alert('Status Name should not be empty...');
             document.getElementById("<%=txtTaskName.ClientID%>").focus();
             return false;
         }
     }

     function clearall() {
         document.getElementById("<%=txtTaskShortName.ClientID%>").value = "";
         document.getElementById("<%=txtTaskName.ClientID%>").value = "";
         return false;
     }                 
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<ucl:Menu ID="menu1" runat="server" />
        <center>
        <br />
        <table align="center" border="1" >
            <tbody>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblTaskShortName" runat="server" Text="Short Name &nbsp;" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtTaskShortName" runat="server" Width="100" SkinID="MandTxtBox"></asp:TextBox>
                    </td>                    
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblTaskName" runat="server" Text="Status Name &nbsp;" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtTaskName" runat="server" Width="250" SkinID="MandTxtBox"></asp:TextBox>
                    </td>
                    
                </tr>
              </tbody>
       </table>
       <asp:HiddenField ID="hidTaskID" runat="server" />
       <br />
       <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="60px" Height="25px" BackColor="LightBlue"  
               onclick="btnSubmit_Click" OnClientClick="return fncheck()" />
       &nbsp;&nbsp;
       <asp:Button ID="btnReset" runat="server" Text="Reset" Width="60px" Height="25px" BackColor="LightBlue" 
                OnClientClick="return clearall()" />
       <br />
       <br />

        <asp:GridView ID="grdTask" runat="server" Width="40%" HorizontalAlign="Center" GridLines="None" 
            onrowcommand="grdTask_RowCommand"  AutoGenerateColumns="false" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt"
            onrowupdating="grdTask_RowUpdating" onrowediting="grdTask_RowEditing" onrowcancelingedit="grdTask_RowCancelingEdit">
            <HeaderStyle Font-Bold="False" />
            <SelectedRowStyle BackColor="BurlyWood" />
            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
            <Columns>
             
                <asp:TemplateField HeaderText="#" ItemStyle-Width="20">
                    <ItemTemplate>
                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Task_Id" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblTask_Id_Edit" runat="server" Text='<%#Eval("Status_ID")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60">
                    <ItemTemplate>
                        <asp:Label ID="lblTask_ShortName_Edit" runat="server" Text='<%# Bind("Short_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Task Name" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100">
                    <ItemTemplate>
                        <asp:Label ID="lblTask_Name_Edit" runat="server" Text='<%# Bind("Status_Name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
               

                   <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20">
                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                    </ControlStyle>
                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Status_ID") %>'
                            CommandName="Edit">Edit
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       <br />  
       </center>       
    </div>
    </form>
</body>
</html>

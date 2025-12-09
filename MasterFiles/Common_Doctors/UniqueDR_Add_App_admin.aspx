<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UniqueDR_Add_App_admin.aspx.cs" Inherits="MasterFiles_UniqueDR_Add_App_admin" %>
<%--<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menumas" TagPrefix="ucl" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>New Unique Dr - Approval</title>
 <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    
     <script src="../../JsFiles/jquery-1.4.1.min.js" type="text/javascript"></script>
     <script type="text/javascript">
         function openWindow(C_Doctor_Code, ListedDrCode) {
             var mrcode = $("#hdnSfCode").val();
           
             //   window.open('Common_Doctor_Updation_FDC.aspx?Code=' + code, 'open_window', ' width=640, height=480, left=0, top=0');
             var popUpObj;
           //  var randomnumber = Math.floor((Math.random() * 100) + 1);

             //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
             popUpObj = window.open("Unique_Dr_View_admin.aspx?C_Doctor_Code=" + C_Doctor_Code + "&ListedDrCode=" + ListedDrCode + "&Mrcode=" + mrcode + "&type=" + 2,
    "ModalPopUp",
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=900," +
    "height=600," +
    "left = 0," +
    "top=0"
    );
             popUpObj.focus();
             //
             $(popUpObj.document.body).ready(function () {
                 var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                 // var ImgSrc = "https://s3.postimg.org/x2mwp52dv/loading1.gif"
                 $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
             });
         }
       


</script>
  
</head>
<body>
    <form id="form1" runat="server">
    <div>
  <ucl:Menumas ID="menumas" runat="server" /> 
     <br />
  
          <div style="margin-left:90%">
    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" /> 

     </div>    
        <br />
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                      <asp:HiddenField runat="server" ID="hdnSfCode" />
                   
                        <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt" 
                            OnRowCreated="grdDoctor_RowCreated"  OnRowDataBound="grdDoctor_RowDataBound" 
                            AllowSorting="True" OnSorting="grdDoctor_Sorting">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                              
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Common dr Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCommon" runat="server" Text='<%#Eval("C_Doctor_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Name" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Deactivated Date"  Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeact_Date" runat="server" Text='<%#Eval("ListedDr_Deactivate_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Qualification" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblQua" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblMob" runat="server" Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="City" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                             <%--   <asp:TemplateField HeaderText="Division Name" >
                                    <ItemTemplate>
                                        <asp:Label ID="lbldivision" runat="server" Text='<%# Bind("Division_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Area Cluster Name" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                   <asp:TemplateField>
                                    <ItemTemplate>
                                                 <%--       <a href="#" onclick='openWindow("<%# Eval("C_Doctor_Code") %>");'>Detail View</a>--%>
                                                       <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='Click Here to Approve'
                                         OnClientClick='<%# "return openWindow(\"" + Eval("C_Doctor_Code") + "\",\"" +  Eval("ListedDrCode")  + "\");" %>' >
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                    </asp:TemplateField>
                            </Columns>
                             <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>

  </div>
 
  
    </form>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UniqueDR_Add_Approval.aspx.cs" Inherits="MasterFiles_UniqueDR_Add_Approval" %>
<%--<%@ Register Src ="~/UserControl/MGR_Menu.ascx" TagName ="Menu" TagPrefix="ucl" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Addition - Approval</title>
 <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />
       <script type="text/javascript">
           function checkAll(objRef) {
               var GridView = objRef.parentNode.parentNode.parentNode;
               var inputList = GridView.getElementsByTagName("input");
               for (var i = 0; i < inputList.length; i++) {
                   //Get the Cell To find out ColumnIndex
                   var row = inputList[i].parentNode.parentNode;
                   if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                       if (objRef.checked) {
                           //If the header checkbox is checked
                           //check all checkboxes
                           //and highlight all rows
                           //row.style.backgroundColor = "aqua";
                           inputList[i].checked = true;
                       }
                       else {
                           //If the header checkbox is checked
                           //uncheck all checkboxes
                           //and change rowcolor back to original 
                           //                    if (row.rowIndex % 2 == 0) {
                           //                        //Alternating Row Color
                           //                        row.style.backgroundColor = "#C2D69B";
                           //                    }
                           //                    else {
                           //                        row.style.backgroundColor = "white";
                           //                    }
                           inputList[i].checked = false;
                       }
                   }
               }
           }
    </script>
     <script src="../../JsFiles/jquery-1.4.1.min.js" type="text/javascript"></script>
     <script type="text/javascript">
         function openWindow(C_Doctor_Code, ListedDrCode) {
             var mrcode = $("#hdnSfCode").val();
             var mode = $("#hdnMode").val();
             //   window.open('Common_Doctor_Updation_FDC.aspx?Code=' + code, 'open_window', ' width=640, height=480, left=0, top=0');
             if (mode == "Existing") {
                 var popUpObj;
                 var randomnumber = Math.floor((Math.random() * 100) + 1);

                 //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                 popUpObj = window.open("../Common_Doctors/Common_Doctor_Updation_FDC.aspx?C_Doctor_Code=" + C_Doctor_Code + "&ListedDrCode=" + ListedDrCode + "&Mrcode=" + mrcode + "&Mode=" + mode,
    "ModalPopUp" + randomnumber,
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
             else if (mode == "New") {
                 var popUpObj;
                 //  var randomnumber = Math.floor((Math.random() * 100) + 1);

                 //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                 popUpObj = window.open("Unique_Dr_View_admin.aspx?C_Doctor_Code=" + C_Doctor_Code + "&ListedDrCode=" + ListedDrCode + "&Mrcode=" + mrcode + "&type=" + 1,
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

         }

</script>
   <script type="text/javascript">
       function validate() {
           var txtreject = document.getElementById('<%=txtreject.ClientID %>').value;
           if (txtreject == "") {
               alert("Please Enter the Reason");
               document.getElementById('<%=txtreject.ClientID %>').focus();
               return false;
           }

           var confirm_value = document.createElement("INPUT");
           confirm_value.type = "hidden";
           confirm_value.name = "confirm_value";
           if (confirm("Do you want to Reject ?")) {
               confirm_value.value = "Yes";
           }
           else {
               confirm_value.value = "No";
           }
           document.forms[0].appendChild(confirm_value);
       }
    </script>
     <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="../../JsFiles/jquery.tooltip.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".gridImages").tooltip({
                track: true,
                delay: 0,
                showURL: false,
                fade: 100,
                bodyHandler: function () {
                    return $($(this).next().html());
                },
                showURL: false
            });
        })
    </script>
    <style type="text/css">
        .GridviewDiv
        {
            font-size: 100%;
            font-family: 'Lucida Grande' , 'Lucida Sans Unicode' , Verdana, Arial, Helevetica, sans-serif;
            color: #303933;
        }
        .headerstyle
        {
            color: #FFFFFF;
            border-right-color: #abb079;
            border-bottom-color: #abb079;
            background-color: #df5015;
            padding: 0.5em 0.5em 0.5em 0.5em;
            text-align: center;
        }
        #tooltip
        {
            position: absolute;
            z-index: 3000;
            border: 1px solid #111;
            background-color: #FEFFFF;
            padding: 5px;
            opacity: 1.55;
        }
        #tooltip h3, #tooltip div
        {
            margin: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   <%--  <ucl:Menu ID="menu1" runat="server" /> --%>
   <br />
    <div style="margin-left:90%">
    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" /> 

     </div>    
        <br />
       
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                      <div class="GridviewDiv">
                      <asp:HiddenField runat="server" ID="hdnSfCode" />
                      <asp:HiddenField runat="server" ID="hdnMode" />
                        <asp:GridView ID="grdDoctor" runat="server" Width="90%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false" GridLines="None" CssClass="mGridImg" AlternatingRowStyle-CssClass="alt" 
                            OnRowCreated="grdDoctor_RowCreated"  OnRowDataBound="grdDoctor_RowDataBound" 
                            AllowSorting="True" OnSorting="grdDoctor_Sorting">
                            <HeaderStyle Font-Bold="False" />
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" Text="  Select All" onclick="checkAll(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkListedDR" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                <asp:TemplateField HeaderText="Listed Doctor Name" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Deactivated Date" HeaderStyle-ForeColor="Black" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDeact_Date" runat="server" Text='<%#Eval("ListedDr_Deactivate_Date")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Qualification" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQua" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMob" runat="server" Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Clinic Name" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClinic" runat="server" Text='<%# Bind("ListedDr_Hospital") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Clinic Addr." HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblClAddr" runat="server" Text='<%# Bind("Hospital_Address") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                             <%--   <asp:TemplateField HeaderText="Division Name" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lbldivision" runat="server" Text='<%# Bind("Division_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Area Cluster Name" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="City" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="State" HeaderStyle-ForeColor="Black">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSt" runat="server" Text='<%# Bind("State_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Visiting Card">
                                        <ItemStyle Width="90px" HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <%--Image in Gridview--%>
                                            <asp:Image ID="Image1" Width="25px" Height="25px" runat="server" class="gridImages"
                                                ImageUrl='<%#Eval("Visiting_Card") %>' />
                                            <div id="tooltip" style="display: none;">
                                                <table>
                                                    <tr>
                                                        <%--Image to Show on Hover--%>
                                                        <td>
                                                            <asp:Image ID="imgUserName" Width="250px" Height="120px" ImageUrl='<%#Eval("Visiting_Card") %>'
                                                                runat="server" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   <asp:TemplateField >
                                    <ItemTemplate>
                                                 <%--       <a href="#" onclick='openWindow("<%# Eval("C_Doctor_Code") %>");'>Detail View</a>--%>
                                                       <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='Detail View'
                                         OnClientClick='<%# "return openWindow(\"" + Eval("C_Doctor_Code") + "\",\"" +  Eval("ListedDrCode")  + "\");" %>' >
                                        </asp:LinkButton>

                                    </ItemTemplate>
                                    </asp:TemplateField>
                            </Columns>
                             <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:GridView>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>

  </div>
  <br />
  <center>
        <asp:Button ID="btnApprove" runat="server" CssClass="savebutton" Text="Approve - Add Lst Drs" Width="170px" OnClick="btnApprove_Click" />&nbsp;
       <asp:Button ID="btnReject" runat="server" CssClass="savebutton" Text="Reject" Width="70px" OnClick="btnReject_Click"  />
          <asp:Label ID="lblRejectReason" Text="Reject Reason : " Visible="false" SkinID="lblMand" runat="server"></asp:Label>
            &nbsp;
        <asp:TextBox ID="txtreject" runat="server" TextMode="MultiLine" BorderStyle="Solid" BorderColor="Gray" Visible="false"  Height="70px" Width="350px"></asp:TextBox>
          &nbsp
          <asp:Button ID="btnSubmit" CssClass="savebutton" Width="140px" runat="server" Visible="false" OnClientClick="return validate();"
                Text="Confirm Reject" OnClick="btnSubmit_Click"  />
        </center>
  
    </form>
</body>
</html>
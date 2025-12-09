<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UniqueDr_Approval_Pending.aspx.cs" Inherits="MasterFiles_UniqueDr_Approval_Pending" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unique Doctor's - Pending Approval View</title>
   <link type="text/css" rel="stylesheet" href="../../../css/MR.css" />
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
            .bgClr{        
        background-color:Green;
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
   <script type="text/javascript">
       $(function () {
           $(".btn").click(function () {
               $('.btn').removeClass('bgClr');
               $(this).addClass('bgClr');
           });
       });
   </script>
      <script type="text/javascript">
          function openWindow(sf_code, ListedDrCode) {

              var popUpObj;
              //  var randomnumber = Math.floor((Math.random() * 100) + 1);

              //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
              popUpObj = window.open("Unique_Dr_View_admin.aspx?mrcode=" + sf_code + "&ListedDrCode=" + ListedDrCode + "&type=" + 5,
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
        <ucl:Menu ID="menu1" runat="server" />      
       
     
    <%--    <asp:RadioButtonList ID="App_Pending" Width="600px" runat="server" RepeatColumns="3" >
        <asp:ListItem Text="Listed Dr Additional Approval Pending List" Value="0" Selected="True"></asp:ListItem>
           <asp:ListItem Text="Listed Dr Deactivation Approval Pending List" Value="1"></asp:ListItem>
              <asp:ListItem Text="Approval - Rejection List" Value="2"></asp:ListItem>
        </asp:RadioButtonList> --%>
        <center>
        <table border="1" >
        <tr>
        <td>
        <asp:Button ID="btnAdd_Pending" runat="server" 
                Text="Unique Dr Addition - Approval Pending" 
                onclick="btnAdd_Pending_Click" />&nbsp;&nbsp;
        </td>
        <td>
        <asp:Button ID="btnDeact_Pending" runat="server" 
                Text="Unique Dr Deactivation - Approval Pending" 
                onclick="btnDeact_Pending_Click" />&nbsp;&nbsp;
        </td>
        <td>
        <asp:Button ID="btnReject" runat="server" Text="Approval - Rejection List" 
                onclick="btnReject_Click" />&nbsp;&nbsp;
        </td>
        </tr>
        </table>
      </center>
      <br />
      <asp:Panel ID="pnlList" runat="server" HorizontalAlign="Center">
      <asp:Label ID="lblADD" runat="server" Text="Unique Dr Additional Approval Pending List" Font-Size="14px" Font-Names="Verdana" ForeColor="Blue" Visible="false" Font-Underline="true"></asp:Label>
        <asp:Label ID="lblDeAct" runat="server" Text="Unique Dr Deactivation Approval Pending List" Font-Size="14px" Font-Names="Verdana" ForeColor="Blue" Visible="false" Font-Underline="true"></asp:Label>
        <asp:Label ID="lblReject" runat="server" Text="Approval - Rejection List" Font-Size="14px" Font-Names="Verdana" ForeColor="Blue" Visible="false" Font-Underline="true"></asp:Label>
      </asp:Panel>
        <table width="100%" align="center" style="margin-top:10px">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                            AutoGenerateColumns="false"   OnRowDataBound="grdDoctor_RowDataBound"                                              
                            GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                            AllowSorting="True" >
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left">                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDocName" SkinID="MandTxtBox" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qualification" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQua" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Speciality" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMob" runat="server" Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="City" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCity" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="Pincode" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                       <asp:Label ID="lblpin" runat="server" Text='<%# Bind("ListedDr_PinCode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Area Cluster Name" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                 
                                 <asp:TemplateField HeaderText="Reason" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblReason" runat="server" Text='<%# Bind("Deact_Reason") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                  <asp:TemplateField HeaderText="Rejected By" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblrej" runat="server" Text='<%# Bind("Listeddr_App_Mgr") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                  <asp:TemplateField HeaderText="Mode" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmode" runat="server" Text='<%# Bind("mode") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                 <asp:TemplateField>
                                    <ItemTemplate>                                                
                                                       <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='Rejection ReEntry'
                                         OnClientClick='<%# "return openWindow(\"" + Eval("sf_code") + "\",\"" +  Eval("ListedDrCode")  + "\");" %>' >
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
          <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    </form>
</body>
</html>

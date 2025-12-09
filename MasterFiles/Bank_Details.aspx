<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bank_Details.aspx.cs" Inherits="MasterFiles_Bank_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Details</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
   <%-- <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>--%>
    <style type="text/css">
       .table td, .table th {
            border-color: #DCE2E8;
            border-right: none;
        }
       #form1 .container
       {
           max-width: 1540px;
       }
    </style>
</head>
<body>
    <form id="form1" runat="server">
 <%--          <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>--%>
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <br />
            <table width="100%">
                <tr>
                    <td width="80%"></td>
                    <td align="right">
                        <table>
                            <tr>
                               
                                <td style="padding-right: 15px">
                                    <asp:LinkButton ID="btnExcel" ToolTip="Excel" runat="server" OnClick="btnExcel_Click">
                                        <asp:Image ID="Image2" runat="server" ImageUrl="../../../assets/images/Excel.png"  ToolTip="Excel" Width="35px" Style="border-width: 0px;"/>
                                    </asp:LinkButton>
                                    <asp:Label ID="Label3" runat="server" Text="Excel" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>
                               
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div  class="container home-section-main-body position-relative clearfix">
            
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                         <div class="col-lg-2">
                            <br />
                            <br />
                            <div runat="server" id="divSettings" style="margin-left: 35px; cursor: pointer;">
                                <asp:ImageButton ID="settings" runat="server" ImageUrl="../../Images/cog.png" ToolTip="Show/Hide Grid Columns" Style="width: 30px; height: 30px; position: absolute;" />
                                <asp:Label ID="Label5" runat="server" Text="Show/Hide Columns" CssClass="label" Font-Size="14px" Style="margin-left: 32px; margin-top: 4px; height: 30px; display: inline-block; vertical-align: middle; font-weight: 401;"></asp:Label>
                            </div>
                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" CancelControlID="btnCancel"
                                PopupControlID="Panel2" TargetControlID="divSettings" BackgroundCssClass="modalBackgroundNew">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Width="250px" align="center" Style="display: none; height: auto;">
                                <div class="header">
                                    Show/Hide Column
                                </div>
                                <div class="body">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div style="height: auto; background-color: #f4f8fa; border: 1px solid Silver; overflow: auto; color: #90a1ac; font-size: 14px; border-radius: 10px; border: 1px solid #d1e2ea; background-color: #f4f8fa; margin-top: 0px; text-align: left;">
                                                <asp:CheckBoxList ID="cblGridColumnList" Font-Size="8pt" runat="server"
                                                    CssClass="chkChoice">
                                                </asp:CheckBoxList>
                                                <br />
                                                <br />
                                                <div class="w-100 designation-submit-button text-center clearfix">
                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="savebutton" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </asp:Panel>
                        </div>
                         <h2 class="text-center">Bank Details</h2>

                        <asp:Panel ID="pnlContents" runat="server">
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" >  <%-- style="scrollbar-width: thin; overflow: inherit;"--%>


                                    <asp:GridView ID="grdBankDetails" runat="server"
                                        AutoGenerateColumns="false"   OnRowCreated="GVMissedCall_RowCreated" OnRowDataBound="GrdDoctor_RowDataBound"  
                                        CssClass="table" GridLines="Both" BorderColor="WhiteSmoke" BorderWidth="1">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Division Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiv" runat="server" Text='<%# Bind("Division_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sf Code" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfCode" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="D.O.J" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDateofjoin" runat="server" Text='<%# Bind("Sf_Joining_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUserName" runat="server" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp Id" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEmpid" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfname" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="HQ" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfHq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Desig" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfDesig" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Active Status" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSftp" runat="server" Text='<%# Bind("sf_Tp_Active_flag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Bank Name"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBankname" runat="server"  Text='<%# Bind("Bank_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Acc No"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblACC" runat="server"  Text='<%# Bind("Bank_AcNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="IFSC Code"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIFSC" runat="server" Text='<%# Bind("IFS_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Category"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server"  Text='<%# Bind("Category") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Address" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfAddress" runat="server"  Text='<%# Bind("SF_Address") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Mobile No"  >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfMob" runat="server"  Text='<%# Bind("SF_Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField  HeaderText="Email Id" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfEmail" runat="server"  Text='<%# Bind("SF_Email") %>' ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        </Columns>

                                    </asp:GridView>


                                </div>
                            </div>
                        </asp:Panel>
                        <div class="no-result-area" id="divid" runat="server" visible="false">
                            No Records Found
                        </div>

                    </div>


                </div>



            </div>
            <br />
            <br />

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>

    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LstDoctorView.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_LstDoctorView" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor View</title>
      <style type="text/css">
             /*table {
    border-collapse: collapse;
}
table, td, th {
    border: 1px solid black;
}*/
          /*.display-table .table tr:nth-child(1) td:first-child
          {
              background-color: #414d55;
              color: #ffffff;
          }
          .display-table .table tr:nth-child(2) td:first-child
          {
              background-color:none;
          }*/
</style>
     <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
     <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
    <div id="Divid" runat="server">
    </div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Listed Doctor View</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-5">
                                        <asp:Panel ID="pnlSf" runat="server">
                                            <asp:Label ID="lblSF" runat="server" Text="Field Force Name " CssClass="label"></asp:Label>
                                            <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="false" Width="100%"></asp:DropDownList>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-lg-2" style="margin-top: auto; padding-left: 0px;">
                                        <asp:Button ID="Button1" runat="server" Width="50px" Text="Go" OnClick="btnGo_Click" CssClass="savebutton" />
                                    </div>
                                </div>
                                <br />
                                <div class="single-des clearfix">
                                    <asp:Panel ID="pnltype" runat="server" Visible="false">
                                        <div class="row clearfix">
                                            <div class="col-lg-8"></div>
                                            <div class="col-lg-3">
                                                <asp:Label ID="lblType" runat="server" Text="Type" CssClass="label"></asp:Label>
                                                <asp:DropDownList ID="rdoType" runat="server" Width="110%" CssClass="nice-select">
                                                    <asp:ListItem Value="0" Text="Category" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Speciality"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Class"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Qualification"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-1" style="margin-left: -25px; margin-top: 23px;">
                                                <asp:Button ID="btnGo" runat="server" Width="50px" CssClass="savebutton" Text=">>" OnClick="btnGo_Click" />
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                        <div class="display-Approvaltable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:Table ID="tbl" runat="server" GridLines="None" CssClass="table" Width="100%">
                                </asp:Table>
                                <asp:Label ID="lblNoRecord" runat="server" CssClass="no-result-area" Width="100%" Visible="false">No Records Found</asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <br /><br />  
         <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

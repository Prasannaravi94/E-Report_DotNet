<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mgrwise_Core_Doc_Map.aspx.cs" Inherits="MasterFiles_Options_Mgrwise_Core_Doc_Map" %>
<%@ Register Src ="~/UserControl/MenuUserControl.ascx" TagName ="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Managerwise Core Doctor Mapping</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
      <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
      <script type="text/javascript">
          $(document).ready(function () {
              function blinker() {
                  $('.blink_me').fadeOut(500);
                  $('.blink_me').fadeIn(500);
              }

              setInterval(blinker, 1000);
          });
    </script>
    <%--<style>
        input[type="checkbox"] + label {
            color: white;
        }
    </style>--%>
    <style type="text/css">
        input[type="checkbox"] + label, input[type="checkbox"]:checked + label {
            color: white;
        }
    </style>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <style>
         .resetbutton {
           border:solid 2px !important;
           color: black !important;
      }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server"></div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Managerwise Core Doctor Mapping</h2>
                        <br />
                        <asp:Panel ID="pnlCore" runat="server" Visible="false">
                            <center>
                                <div class="designation-area clearfix">
                            <div class="single-des clearfix"></div>
                                <table width="100%" bgcolor="#F1F5F8">
                                    <tr>
                                        <td align="left">

                                <asp:Button ID="btnSave" runat="server" CssClass="resetbutton" Width="175px" Height="25px" Text="Final Submit [For all MR's]" OnClick="btnSave_Click"/>
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblHead" runat="server" Text="Core Doctor Map" CssClass="label" Font-Bold="True" Font-Names="Verdana" Font-Size="12px"></asp:Label>

                                        </td>
                                        <td align="right" width="15%">
                                            <asp:Button ID="btnDraftSave" runat="server" CssClass="resetbutton" Width="135px"
                                                Height="25px" Text="MR Wise Draft Save" OnClick="btnDraftSave_Click"/>
                                        </td>
                                        <td align="right">
                                            <asp:Button ID="btnClear" CssClass="resetbutton" runat="server" Width="60px"
                                                Height="25px" Text="Clear" OnClick="btnClear_Click"/></td>
                                        <td align="right">
                                            <asp:Label ID="lblmgr" runat="server" Text="Manager : " CssClass="label" ></asp:Label>

                                            <asp:DropDownList ID="ddlMGR" runat="server" SkinID="ddlRequired" Width="170px" AutoPostBack="true" OnSelectedIndexChanged="ddlMGR_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                                                   <td align="right">
                                                <asp:Label ID="lblMR" runat="server" Text="MR: " CssClass="label"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:DropDownList ID="ddlMR" runat="server" SkinID="ddlRequired" Width="130px">
                                                </asp:DropDownList>

                                            </td>
                                            <td align="right">
                                                <asp:Button ID="btngo" runat="server" Width="35px" Text="Go" CssClass="savebutton" OnClick="btngo_Click" />
                                            </td>
                                    </tr>
                                </table>
                                
                                    </div>
                                <br />
                                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="overflow:inherit; scrollbar-width: thin;">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvDetails" runat="server" Width="100%" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowDataBound="gvDetails_RowDataBound"
                                                GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">
                                                <%--<HeaderStyle Font-Bold="False" />
                        <SelectedRowStyle BackColor="BurlyWood"/>
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>--%>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doc Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doctor" ItemStyle-Width="200">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Specialty" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpecName" runat="server" Text='<%#Eval("Doc_Spec_ShortName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category" HeaderStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCatName" runat="server" Text='<%#Eval("Doc_Cat_ShortName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Territory" HeaderStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTerrName" runat="server" Text='<%#Eval("territory_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Level1" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel1" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Level2" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel2" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                                    <%-- <asp:TemplateField HeaderText="Level3" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel3" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Level4" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkLevel4" runat="server" Checked="true" Text="." />
                                </ItemTemplate>
                            </asp:TemplateField>

                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                                    </div>
                            </center>
                        </asp:Panel>
                        <br />
                        <center>
                            <asp:Label ID="lbllock" runat="server" Visible="false" CssClass="blink_me" Style="font-size: 18px; color: red; font-weight: bold;">"Core Doctor Map" as Locked. Get the Approval from Admin</asp:Label>
                        </center>
                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>
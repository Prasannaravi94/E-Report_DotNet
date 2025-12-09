<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDR_SlNo_Gen.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_ListedDR_SlNo_Gen" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Serial No Generation</title>
    <%--  <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <style type="text/css">
        .marRight {
            margin-right: 35px;
        }
       .display-table .table tr td .nice-select .list {
            max-height:200px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server"></div>
            <%-- <ucl:Menu ID="menu1" runat="server" />--%>

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Listed Doctor Serial No Generation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="center">
                                    <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                                </asp:Panel>

                                <table id="Table1" runat="server" width="90%">
                                    <tr>
                                        <td align="right" width="30%">
                                            <%--    <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <%--<asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server"
                                                OnClick="btnBack_Click" />--%>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                         
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                    <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" OnRowDataBound="grdDoctor_RowDataBound"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" 
                                        AllowSorting="True" OnSorting="grdDoctor_Sorting">
                                      
                                        <Columns>
                                              <asp:TemplateField  HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSN" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="ListedDr_Name" HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left"
                                              >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_Cat_ShortName" ItemStyle-HorizontalAlign="Left" HeaderText="Category" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_Spec_ShortName" ItemStyle-HorizontalAlign="Left" HeaderText="Speciality" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_Qua_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Qualification" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Doc_Class_ShortName" ItemStyle-HorizontalAlign="Left" HeaderText="Class" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="territory_Name" ItemStyle-HorizontalAlign="Left" HeaderText="Territory" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Existing S.No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New S.No" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlSlNo" runat="server" DataSource="<%# Get_SlNo() %>" DataTextField="SlNO" DataValueField="SlNO" CssClass="nice-select" ></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <br /><br />
                            <center>
                                <asp:Button ID="btnSubmit" runat="server"  Text="Save" CssClass="savebutton"
                                    OnClick="btnSubmit_Click" />
                             
                              <asp:Button ID="btnClear" runat="server"  Text="Clear" CssClass="resetbutton"
                                  OnClick="btnClear_Click" />
                            </center>

                        </div>
                    </div>

                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
                </div>
            </div>

            <br />
            <br />
        </div>

    </form>
</body>
</html>

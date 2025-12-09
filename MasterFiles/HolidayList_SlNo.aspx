<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayList_SlNo.aspx.cs"
    Inherits="MasterFiles_HolidayList_SlNo" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday SlNo Generation</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <br />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
               
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center">Holiday Serial No Generation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading text-center clearfix">
                                <div class="d-inline-block division-name">Division Name</div>
                                <div class="d-inline-block align-middle">
                                    <div class="single-des-option">
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                             <p>
                                <br />
                            </p>
                           <%-- <h2 class="text-center">Base Level</h2>--%>
                            <div class="display-table clearfix" align="center">
                                <div class="table-responsive overflow-x-none" align="center">
                                    <asp:GridView ID="grdHoliday" runat="server"  
                                        PageSize="40"  AllowSorting="True" AutoGenerateColumns="false"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNumber" runat="server" Text='<%#  (grdHoliday.PageIndex * grdHoliday.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Holiday_Id" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHoliday_Id" runat="server" Text='<%#Bind("Holiday_Id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Holiday Name" ItemStyle-Width="55%">
                                              
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtHolidayName" CssClass="input"  Width="160px" runat="server"
                                                        MaxLength="70" onkeypress="CharactersOnly(event);" Text='<%# Bind("Holiday_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHolidayName" runat="server" Text='<%# Bind("Holiday_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month" ItemStyle-Width="15%" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("MonthName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="Fixed Date" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                          
                            <ItemTemplate>
                                <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("Fixed_date") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Existing S.No"  ItemStyle-Width="13%" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="New S.No" ItemStyle-Width="13%" 
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSlNo" onkeypress="CheckNumeric(event);" runat="server" MaxLength="3"
                                                        Width="50%" CssClass="input"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                       
                                    </asp:GridView>

                                    <br />
                                    <div class="no-result-area" id="divid" runat="server" visible="false">
                                        No Records Found
                                    </div>
                                    </div>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Generate - Sl No"  Width="150px"
                                        CssClass="savebutton" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear"
                                        CssClass="resetbutton" OnClick="btnClear_Click" />
                              
                            </div>


                        </div>
                    </div>
                </div>
                <asp:Button ID="btnback" runat="server" Text="Back"  CssClass="backbutton" OnClick="btnback_Click"/>
              
            </div>

        </div>
    </form>
</body>
</html>

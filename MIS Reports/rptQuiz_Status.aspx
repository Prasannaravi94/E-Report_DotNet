<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptQuiz_Status.aspx.cs" Inherits="MIS_Reports_rptQuiz_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
          <link type="text/css" rel="stylesheet" href="../css/Report.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />
        <center>
        <asp:Label ID="lblSal" Font-Size="Medium" ForeColor="Red" Font-Bold="true" Font-Underline="true" runat="server" ></asp:Label>
        </center>
        <br />
       <center>
        <asp:Label ID="lblsfname" Font-Size="14px" Font-Bold="true" ForeColor="Green"  runat="server" ></asp:Label>
        </center>
          <center>
        <table width="80%">
            <tr>
                <td>
                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                        AutoGenerateColumns="false" EmptyDataText="No Records Found" CssClass="mGrid" OnRowDataBound="grdSalesForce_RowDataBound" 
                        AlternatingRowStyle-CssClass="alt" >
                        <HeaderStyle Font-Bold="False" />
                        <PagerStyle CssClass="pgr"></PagerStyle>
                        <SelectedRowStyle BackColor="BurlyWood" />
                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:BoundField DataField="Question_Id" HeaderText="Code" Visible="false" />
                          <%--  <asp:TemplateField HeaderText="Sf_Code" Visible="false" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Question" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblQus" runat="server" Text='<%# Bind("Question_Text") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Actual Answer" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblact" runat="server" Text='<%# Bind("Input_Text") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Selected Answer" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCorrect" runat="server" Text='<%# Bind("InputName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="Selected Answer" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblCorrectAtt" runat="server" Text='<%# Bind("NameAttempt2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>        
                            
                                <asp:TemplateField HeaderText="Correct" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblCorrectans" runat="server" ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                               
                  
                  
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </center>
    </div>
    </form>
</body>
</html>

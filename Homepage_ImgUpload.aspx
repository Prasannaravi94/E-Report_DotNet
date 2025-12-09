<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Homepage_ImgUpload.aspx.cs"
    Inherits="MasterFiles_Options_Homepage_ImgUpload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page Image Upload</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblHome" CssClass="label" runat="server">Login Page Image</asp:Label>
                                <asp:FileUpload ID="fileUpload1" runat="server" CssClass="input" Width="100%" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Subject</asp:Label>
                                <asp:TextBox ID="txtSub" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="savebutton" OnClick="btnUpload_Click" />
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="Id"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" OnRowCommand="gvDetails_RowCommand"
                                        OnRowDeleting="gvDetails_RowDeleting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" HeaderStyle-ForeColor="white" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="Subject" HeaderText="Subject" ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundField DataField="FileName" HeaderText="FileName" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="FilePath" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Upload_Date" HeaderText="Upload Date and Time" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Id") %>'
                                                        CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Image');">Delete
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Usermanual_Upload.aspx.cs" Inherits="MasterFiles_Options_Usermanual_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>User Manual Upload</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>

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
                                <asp:Label ID="Label1" runat="server" CssClass="label"> Upload</asp:Label>
                                <asp:FileUpload ID="fileUpload1" runat="server" CssClass="input" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Subject</asp:Label>
                                <asp:TextBox ID="txtFileSubject" CssClass="input" runat="server"></asp:TextBox>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnUpload" runat="server" CssClass="savebutton" Text="Upload" Width="70px" Height="25px" OnClick="Upload" />

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
                                    <asp:GridView ID="gvDetails" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowCommand="gvDetails_RowCommand" OnRowDeleting="gvDetails_RowDeleting"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="ID" ItemStyle-HorizontalAlign="Left" Visible="false" />
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FileSubject" HeaderText="Subject" ItemStyle-HorizontalAlign="Left" />
                                            <%-- <asp:TemplateField HeaderText="Uploaded File" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFileName" runat="server" Text='<%# Eval("FileName") %>' OnClick="DownloadFile" CommandArgument='<%# Eval("FileName") %>' >
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                            <asp:BoundField DataField="FileName" HeaderText="File Name" ItemStyle-HorizontalAlign="Left" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Download">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                                        CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Update_dtm" HeaderText="Upload Date" ItemStyle-HorizontalAlign="Left" />

                                            <%-- <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                       <asp:LinkButton ID="lnkbutDel" runat="server" OnClientClick="return confirm('Do you want to delete the Image');">Delete
                                </asp:LinkButton>
                                </ItemTemplate>
                        </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDel" Font-Size="11px" Font-Names="Verdana" runat="server" CommandArgument='<%# Eval("ID") %>'
                                                        CommandName="Delete" OnClientClick="return confirm('Do you want to delete the File');">Delete
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

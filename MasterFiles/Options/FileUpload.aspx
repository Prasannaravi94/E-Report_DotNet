<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="MasterFiles_Options_FileUpload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>File Upload</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script src="../../JScript/jquery-1.10.2.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function () {

            $("[id*=ChkAll]").bind("click", function () {

                if ($(this).is(":checked")) {

                    $("[id*=chkDesig] input").attr("checked", "checked");

                } else {

                    $("[id*=chkDesig] input").removeAttr("checked");

                }

            });

            $("[id*=chkDesig] input").bind("click", function () {

                if ($("[id*=chkDesig] input:checked").length == $("[id*=chkDesig] input").length) {

                    $("[id*=ChkAll]").attr("checked", "checked");

                } else {

                    $("[id*=ChkAll]").removeAttr("checked");

                }

            });

        });

    </script>
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
                                <asp:Label ID="Label1" runat="server" CssClass="label" Width="100%">File Upload</asp:Label>
                                <asp:FileUpload ID="fileUpload1" CssClass="input" Width="100%" runat="server" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Width="100%">Subject</asp:Label>
                                <asp:TextBox ID="txtFileSubject" runat="server" Width="100%" CssClass="input"></asp:TextBox>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDes" runat="server" CssClass="label" Width="100%">Designation</asp:Label>
                                <asp:CheckBox ID="ChkAll" runat="server" Text=" All" Style="font-size: x-small; color: black; font-family: Verdana;" />
                                <asp:CheckBoxList ID="chkDesig" DataTextField="Designation_Short_Name" Width="100%"
                                    DataValueField="Designation_Code" runat="server" RepeatDirection="Vertical" RepeatColumns="4"
                                    TabIndex="7" Style="font-size: x-small; color: black; font-family: Verdana;">
                                </asp:CheckBoxList>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnUpload" runat="server" CssClass="savebutton" Text="Upload" OnClick="Upload" />

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
                                            <asp:BoundField DataField="Designation_Short_Name" HeaderText="Send to (Desig.)" ItemStyle-HorizontalAlign="Left" />
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
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>

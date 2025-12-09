<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_BulkUpload.aspx.cs" Inherits="MasterFiles_Options_Listeddr_BulkUpload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor Upload - Bulk</title>
    <style type="text/css">
        .modal {
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

        .loading {
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

        #divdr {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 130px;
        }

        #detail {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 130px;
            padding: 2px;
        }

        #divcat {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 90px;
        }

        #detailcat {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 90px;
            padding: 2px;
        }

        #divTerr {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }

        #detailTerr {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }

        #divcls {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }

        #detailCls {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <br />
                    <h2 class="text-center" id="hHeading" runat="server"></h2>

                    <div class="row justify-content-center">
                        <div class="col-lg-4">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblExcel" runat="server" CssClass="label">Excel file</asp:Label>
                                    <asp:FileUpload ID="FlUploadcsv" runat="server" CssClass="input" />
                                </div>
                                <div class="single-des clearfix">
                                    <asp:CheckBox ID="chkDeact" runat="server" ForeColor="Red" Font-Size="12px" Font-Names="Verdana"
                                        Text="Deactivate Existing Doctor List ( if Yes then Check this Option )"
                                        OnCheckedChanged="chkDeact_CheckedChanged" />
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />
                                    <asp:Button ID="Button1" runat="server" CssClass="savebutton" Text="Upload" OnClick="btnUpload_Click" />
                                </div>
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblIns" runat="server" CssClass="label" ForeColor="Red">NOTE:</asp:Label>
                                    <asp:Label ID="Label1" runat="server" CssClass="label">1) Sheet Name Must be 'UPL_Doctor_Bulk'</asp:Label>
                                </div>
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblExc" runat="server" CssClass="label">Excel Format File</asp:Label>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download Here" OnClick="lnkDownload_Click"> 
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive">
                                        <asp:Repeater ID="rptSpec" runat="server">
                                            <HeaderTemplate>
                                                <div id="divdr" style="background-color: #F1F5F8; text-align: center">
                                                    Speciality
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="detail" style="background-color: White">
                                                    <div style="background-color: White">
                                                        <%#Eval("Doc_Special_Name") %>
                                                    </div>
                                                    <%--   <asp:Literal ID="litName" Text='<%#Eval("Doc_Special_Name") %>' runat="server"></asp:Literal>--%>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive">
                                        <asp:Repeater ID="rptCat" runat="server">
                                            <HeaderTemplate>
                                                <div id="divcat" style="background-color: #F1F5F8; text-align: center">
                                                    Category
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="detailcat" style="background-color: White;">
                                                    <div><%#Eval("Doc_Cat_Name")%></div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive">
                                        <asp:Repeater ID="rptcls" runat="server">
                                            <HeaderTemplate>
                                                <div id="divcls" style="background-color: #F1F5F8; text-align: center">
                                                    Class
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="detailCls" style="background-color: White;">
                                                    <div>
                                                        <%#Eval("Doc_ClsName")%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive">
                                        <asp:Repeater ID="rptTerr" runat="server">
                                            <HeaderTemplate>
                                                <div id="divTerr" style="background-color: #F1F5F8; text-align: center">
                                                    Territory
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="detailTerr" style="background-color: White;">
                                                    <div>
                                                        <%#Eval("Territory_Name")%>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
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
    </form>
</body>
</html>

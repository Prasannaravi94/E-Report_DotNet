<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDr_Upload.aspx.cs"
    Inherits="MasterFiles_Options_ListedDr_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor Upload</title>
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
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
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
                                    <asp:Label ID="lblFilter" runat="server" CssClass="label">Field Force Name</asp:Label>
                                    <asp:DropDownList ID="ddlFilter" runat="server" CssClass="custom-select2 nice-select">
                                        <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="single-des clearfix">
                                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                </div>
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblExcel" runat="server" CssClass="label">Choose the Excel file</asp:Label>
                                    <asp:FileUpload ID="FlUploadcsv" runat="server" CssClass="input" />
                                </div>
                                <div class="single-des clearfix">
                                    <asp:CheckBox ID="chkDeact" runat="server" ForeColor="Red" Font-Size="12px" Font-Names="Verdana"
                                        Text="Deactivate Existing Doctor List ( if Yes then Check this Option )"
                                        OnCheckedChanged="chkDeact_CheckedChanged" />
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />
                                    <asp:Button ID="Button1" runat="server" CssClass="savebutton" Width="150px" Text="Upload the Doctor list" OnClick="btnUpload_Click" />
                                </div>
                                <div class="single-des clearfix">
                                    <br />
                                    <asp:Label ID="lblIns" runat="server" CssClass="label" Font-Underline="true">Important Note: (For Upload Instruction)</asp:Label>
                                    <br />
                                    <br />
                                    <asp:Label ID="Label1" CssClass="label" runat="server">*** Doctor Name, Category, Speciality, Class and Territory Name is Mandatory.</asp:Label>
                                    <br />
                                    <asp:Label ID="Label2" CssClass="label" runat="server">*** Kindly Copy the Doctor Category,Class and Speciality from Right Side Corner and Paste into the Excel File. Donot type your own.</asp:Label>
                                    <br />
                                    <asp:Label ID="Label3" CssClass="label" runat="server">*** For Existing Territories,have to Copy and Paste from the Right Side Corner after Selecting the Fieldfoce Name with Pressing the 'Go' Button.New Territories will be Created Automatically with referring the Excel File.</asp:Label>
                                    <br />
                                    <asp:Label ID="Label4" CssClass="label" runat="server">*** For Front and Back letters, Avoid the 'Space' for the Mandatory Fields. </asp:Label>
                                    <br />
                                    <asp:Label ID="Label5" CssClass="label" runat="server">*** 'Type' Column is for Territory Category. Enter Only Numeric as 1,2,3 and 4. Donot Enter Any Characters. i.e 1 for  HQ, 2 for  EX, 3 for OS, 4 for OS-EX.</asp:Label>
                                    <br />
                                    <asp:Label ID="Label6" CssClass="label" runat="server">*** DOB & DOW Must be a 'MM/DD/YYYY' Format.</asp:Label>
                                    <br />
                                    <asp:Label ID="Label7" CssClass="label" runat="server">*** Don't make Any Special Formats in the Excel File.</asp:Label>
                                    <br />
                                    <asp:Label ID="Label8" CssClass="label" runat="server">*** Kindly Avoid the Special Characters in all Columns like  ''  ' \ </asp:Label>
                                    <br />
                                    <asp:Label ID="Label9" CssClass="label" runat="server">*** Kindly Remove the Blank Columns and Blank Rows.</asp:Label>
                                    <br />
                                    <asp:Label ID="Label10" CssClass="label" runat="server">*** Save the Excel File Extension as .xls , then Upload.</asp:Label>
                                    <br />
                                </div>
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblExc" runat="server" CssClass="label" BackColor="#F1F5F8" Font-Underline="true" Font-Bold="true">UPLOAD - Excel Format File</asp:Label>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Font-Size="12px" Font-Names="Verdana" Text="Download Here" OnClick="lnkDownload_Click"> 
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
                                                <div id="divdr" style="background-color: #F1F5F8;">
                                                    Speciality
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div id="detail" style="background-color: White;">
                                                    <div>
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
                                                <div id="divcat" style="background-color: #F1F5F8;">
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
                                                <div id="divcls" style="background-color: #F1F5F8;">
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
                                                <div id="divTerr" style="background-color: #F1F5F8;">
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
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

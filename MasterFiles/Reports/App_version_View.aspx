<%@ Page Language="C#" AutoEventWireup="true" CodeFile="App_version_View.aspx.cs" Inherits="Reports_App_version_View" %>

<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>App Version View</title>
    <%--    <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
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

        #tbl {
            border-collapse: collapse;
        }

         #divclss {
                    max-width: 1950px;
                  }

        /*table, td, th {
            border: 1px solid black;
        }*/

        .table {
    width:  100%;
     width: auto;
    margin: 0 auto;
}

        #tblSFRpt {
        }

        #tblLocationDtls {
            margin-left: 300px;
        }

        .style2 {
            width: 50px;
            height: 25px;
        }

        .style3 {
            height: 25px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
                 .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }
    </style>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSubmit').click(function () {

                var divi = $('#<%=ddlDivision.ClientID%> :selected').text();
                if (divi == "--Select--") { alert("Select Division Name."); $('#ddlDivision').focus(); return false; }

            });
        });
    </script>
    <%--    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <%--<ucl:Menu ID="menu1" runat="server" />--%>
            <br />
            <br />
            <div id="divclss" class="container home-section-main-body position-relative clearfix " >
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="heading" runat="server"></h2>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDivision" runat="server" Text="Division Name " CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnSubmit" runat="server" CssClass="savebutton"
                                    Text="View" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="display-table clearfix">
                             <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:Panel ID="pnlContents" runat="server" >
                                    <asp:GridView ID="GrdDoctor" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None" OnRowDataBound="GrdDoctor_RowDataBound"
                                        CssClass="table" ShowFooter="True" AlternatingRowStyle-CssClass="alt" Style="font-size: 11px; ">
                                        <FooterStyle HorizontalAlign="Center" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left"
                                                Visible="false">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSfCode" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Division Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblDiv_Name" runat="server" Text='<%# Bind("division_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Emp Code" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbUsrDfd_UserName" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblUrNm" runat="server" Text='<%# Bind("UsrDfd_UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblSf" runat="server" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbSf_HQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Design" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbsf_Designation_Short_Name" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Version" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblversion" runat="server" Text='<%# Bind("version") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mode" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMode" runat="server" Text='<%# Bind("mode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LAST LOGIN DATE & TIME" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsigntm" runat="server" Text='<%# Bind("signin_time") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Device Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-CssClass="stickyFirstRow">

                                                <ItemTemplate >
                                                    <asp:Label ID="lbldevice" runat="server" Text='<%# Bind("device_name") %>' ></asp:Label>
                                                </ItemTemplate>
                                                  <ItemStyle Width="600px" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>

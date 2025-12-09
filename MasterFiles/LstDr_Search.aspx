<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LstDr_Search.aspx.cs" Inherits="MasterFiles_LstDr_Search" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Search</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .marright {
            margin-left: 85%;
        }
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>

    <script type="text/javascript">
        function PrintGridData() {

            var prtGrid = document.getElementById('<%=grdSalesForce.ClientID %>');

            prtGrid.border = 1;
            var prtwin = window.open('', 'PrintGridViewData', '');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();
        }

    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
            $('#btnGo').click(function () {

                var type = $('#<%=lblHQ.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Listed Doctor."); $('#lblHQ').focus(); return false; }
            });
        });
    </script>


    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddldr]');
            var $items = $('select[id$=ddldr] option');

            $txt.keyup(function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                   function (n) {
                       return exp.test($(n).text());
                   });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                   );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }
            }
        });
    </script>


    <script language="javascript" type="text/javascript">
        function popUp(sfcode, sf_name, Div_code, Dr_Code, HQ, Desig) {
            strOpen = "rptLstDr_Search.aspx?sfcode=" + sfcode + "&Dr_Code=" + Dr_Code + "&Div_code=" + Div_code + "&HQ=" + HQ + "&Desig=" + Desig + "&sf_name=" + sf_name
            window.open(strOpen, 'popWindow', '');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <br />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="heading" runat="server"></h2>
                        <div class="designation-area clearfix">
                            <%-- <div class="single-des clearfix">
                                <asp:Label ID="lblDivision" runat="server" Text="Division Name " SkinID="lblMand"></asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDr" runat="server" Text="Listed Doctor Name" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtNew" runat="server" CssClass="input" Width="100%"
                                    ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:Label ID="lblRequired" runat="server" CssClass="label">
                                   <span style="Color:Red">Please Enter Minimun 4 Letters*</span> 
                                </asp:Label>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblHQ" runat="server" Text="HQ" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlHQ" runat="server" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click"
                                    CssClass="savebutton" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="display-table clearfix">
                            <div class="table-responsive"  style="scrollbar-width: thin;max-height:700px">
                                <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                                    EmptyDataText="No Records Found" AutoGenerateColumns="false" OnRowDataBound="grdSalesForce_RowDataBound"
                                    GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt">
                                    <HeaderStyle Font-Bold="False" />
                                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sfcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("Sf_code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Division Name"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDiv_Name" runat="server"
                                                    Text='<%# Bind("Division_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FieldForce Name" 
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFieldForce" runat="server" 
                                                    Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HQ" 
                                            ItemStyle-HorizontalAlign="Left">

                                            <ItemTemplate>
                                                <asp:Label ID="lblHQ" runat="server" 
                                                    Text='<%# Bind("sf_hq") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" 
                                            ItemStyle-HorizontalAlign="Left">

                                            <ItemTemplate>
                                                <asp:Label ID="lblDesig" runat="server" 
                                                    Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Doc Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDoc_Code" runat="server" Text='<%# Bind("ListedDrCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Listed Dr Name" 
                                            ItemStyle-HorizontalAlign="Left">

                                            <ItemTemplate>
                                                <asp:Label ID="lblLisdr_Name" runat="server" 
                                                    Text='<%# Bind("ListedDr_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qualification" 
                                            ItemStyle-HorizontalAlign="Left">

                                            <ItemTemplate>
                                                <asp:Label ID="lblQual" runat="server" 
                                                    Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Speciality" 
                                            ItemStyle-HorizontalAlign="Left">

                                            <ItemTemplate>
                                                <asp:Label ID="lblSpec" runat="server" 
                                                    Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" 
                                            ItemStyle-HorizontalAlign="Left">

                                            <ItemTemplate>
                                                <asp:Label ID="lblCat" runat="server" 
                                                    Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class" 
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblClass" runat="server" 
                                                    Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" 
                                            ItemStyle-HorizontalAlign="Left">

                                            <ItemTemplate>
                                                <asp:Label ID="lblTerr" runat="server" 
                                                    Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View" 
                                            HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblDrsCnt" runat="server" Font-Size="12px" Font-Bold="true" Width="10%" Font-Names="sans-serif" Forecolor="Red" Text='<%# Bind("Lst_drCount") %>' ></asp:Label>--%>

                                                <asp:LinkButton ID="lblDrsCnt" runat="server" CausesValidation="False" Text="Click Here"
                                                    OnClientClick='<%# "return popUp(\"" + Eval("sf_code") + "\",\"" + Eval("sf_name")  + "\",\"" + Eval("Division_Code")  + "\",\"" + Eval("ListedDrCode")  + "\",\"" + Eval("Sf_HQ")  + "\",\"" + Eval("sf_Designation_Short_Name")  + "\");" %>'>
                                                </asp:LinkButton>
                                                <%--  <asp:HyperLink ID="lblDrtgsCnt" Target="_blank" runat="server" NavigateUrl='<%# String.Format("~/MasterFiles/rptLstDr_Search.aspx?sf_code={0}&sf_name={1}&Division_Code={2}&ListedDrCode={3}", Eval("Sf_code"), Eval("Sf_Name"),Eval("Division_Code"),Eval("ListedDrCode")) %>'
                                           Text="Click Here" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
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
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>

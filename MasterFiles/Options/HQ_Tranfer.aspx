<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HQ_Tranfer.aspx.cs" Inherits="MasterFiles_Options_HQ_Tranfer" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager Transfer to Other HQ</title>
    <%--<style type="text/css">
        table.gridtable
        {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }
        table.gridtable th
        {
            padding: 5px;
        }
        table.gridtable td
        {
            border-width: 1px;
            padding: 5px;
            border-style: solid;
            border-color: #666666;
        }
    </style>--%>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>

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
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFromFieldForce]');
            var $items = $('select[id$=ddlFromFieldForce] option');

            $txt.on('keyup', function () {
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
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=TextBox1]');
            var $ddl = $('select[id$=ddlToFieldForce]');
            var $items = $('select[id$=ddlToFieldForce] option');

            $txt.on('keyup', function () {
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
    <script type="text/javascript" language="javascript">
        function Validate() {
            if (confirm('Do you want to Transfer HQ?')) {
                return true;
            }
            else {
                return false;
            }

        }


    </script>

    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
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
                            $('#btnTransfer').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnTransfer').click(function () {

                var ddlFromFieldForce = $('#<%=ddlFromFieldForce.ClientID%> :selected').text();
                if (ddlFromFieldForce == "---Select Clear---") { alert("Select MGR for Tranfer."); $('#ddlFromFieldForce').focus(); return false; }

                var ddlToFieldForce = $('#<%=ddlToFieldForce.ClientID%> :selected').text();
                if (ddlToFieldForce == "---Select Clear---") { alert("Select MGR for Tranfer."); $('#ddlToFieldForce').focus(); return false; }


                if (confirm('Do you want to Transfer HQ?')) {
                    return true;
                }
                else {
                    return false;
                }


            });
        });
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <br />
                    <h2 class="text-center" id="hHeading" runat="server"></h2>
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Select MGR for Transfer"></asp:Label>
                                    <asp:DropDownList ID="ddlFromFieldForce" runat="server" AutoPostBack="true" CssClass="custom-select2 nice-select"
                                        OnSelectedIndexChanged="ddlFromFieldForce_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" CssClass="custom-select2 nice-select" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lbltoFF" runat="server" CssClass="label" Text="Select MGR for Transfer"></asp:Label>

                                    <asp:DropDownList ID="ddlToFieldForce" runat="server" AutoPostBack="true" SkinID="ddlRequired"
                                        OnSelectedIndexChanged="ddlToFieldForce_SelectedIndexChanged" CssClass="custom-select2 nice-select">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF2" runat="server" CssClass="custom-select2 nice-select" Visible="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblsub1" Text="Subordinates" runat="server" CssClass="label" ForeColor="Red" Visible="false"
                                        Font-Size="Large"></asp:Label>
                                </div>
                            </div>
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="grdField1" runat="server" HorizontalAlign="Center" EmptyDataText="No Listed Doctor's Found"
                                                    AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                                    AlternatingRowStyle-CssClass="alt">
                                                    <HeaderStyle Font-Bold="False" />
                                                    <PagerStyle CssClass="gridview1"></PagerStyle>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemTemplate>
                                                                <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdField1.PageIndex * grdField1.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="sf_code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsfcode" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fieldforce Name"
                                                            ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsfName" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcat" runat="server" Text='<%# Bind("sf_designation_short_name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                            HeaderText="HQ">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhq" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                    </Columns>
                                                    <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                        BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                        VerticalAlign="Middle" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-5">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">

                                    <asp:Label ID="lblsub2" Text="Subordinates" runat="server" CssClass="label" ForeColor="Red" Visible="false"
                                        Font-Size="Large"></asp:Label>
                                </div>
                            </div>
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive">
                                        <td style="border: none;" colspan="2">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdField2" runat="server" HorizontalAlign="Center" EmptyDataText="No Listed Doctor's Found"
                                                        AutoGenerateColumns="false" GridLines="None"
                                                        CssClass="table" PagerStyle-CssClass="gridvview1" AlternatingRowStyle-CssClass="alt">
                                                        <HeaderStyle Font-Bold="False" />
                                                        <PagerStyle CssClass="gridvview1"></PagerStyle>
                                                        <SelectedRowStyle BackColor="BurlyWood" />
                                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#">
                                                                <ItemTemplate>
                                                                    <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdField2.PageIndex * grdField2.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="sf_code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsfcode" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fieldforce Name"
                                                                ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsfName" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                                HeaderText="Designation">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcat" runat="server" Text='<%# Bind("sf_designation_short_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left"
                                                                HeaderText="HQ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblhq" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                                            BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                                            VerticalAlign="Middle" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <br />
                    <div class="designation-area clearfix">
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnTransfer" runat="server" Text="Transfer HQ" CssClass="savebutton"
                                OnClick="btnTransfer_Click" />
                        </div>
                    </div>
                    <div class="loading" align="center">
                        Loading. Please wait.<br />
                        <br />
                        <img src="../../Images/loader.gif" alt="" />
                    </div>
                </div>
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>


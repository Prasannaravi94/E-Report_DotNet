<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GPS_GEOFence.aspx.cs" Inherits="GPS_GEOFence" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html
xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gps/GeoFence and Geo Tagg Deletion</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="../JScript/DateJs/date.js"></script>
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
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $(' <div />');
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
                if ($("#txtmsg").val() == "") { alert("Enter Notification Message."); $('#txtmsg').focus(); return false; }

            });
        });


    </script>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });

    </script>
    <script type="text/javascript">
        $(function () {
            $("#btnSubmit").click(function () {
                var startDate = document.getElementById("txtEffFrom").value;
                var endDate = document.getElementById("txtEffTo").value;
                alert(startDate);
                alert(endDate);
                if ((Date.parse(startDate) >= Date.parse(endDate))) {
                    alert("Effective To date should be greater Efffective From date");
                    return false;

                }
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

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
                    $ddl.append(" <option>No Items Found</option>");
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
        function SelectAllCheckboxes(chk, selector) {

            $('#<%=grdgps.ClientID%>').find(selector + " input:checkbox").each(function () {
                $(this).prop("checked", $(chk).prop("checked"));
            });
        }


    </script>


    <style type="text/css">
        .PanelClass {
            border: solid 1px Black;
        }

            .PanelClass:hover {
                border: solid 1px #FF0000;
                color: red;
            }

        .table tr td [type="checkbox"]:not(:checked) + label, .table tr td [type="checkbox"]:checked + label {
            color: white;
        }
    </style>

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
            <%--
				<ucl:Menu ID="menu1" runat="server" />--%>
            <div id="Divid" runat="server"></div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">

                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:RadioButtonList ID="rdoFenceTagg" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdoFenceTagg_SelectedIndexChanged" Width="100%">
                                    <asp:ListItem Value="1">Gps/Geo Fence User Allocation</asp:ListItem>
                                    <asp:ListItem Value="2">Geo Tagg Deletion</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <asp:Panel ID="pnlGeoFence" runat="server">
                    <div class="row justify-content-center">
                        <div class="col-lg-5">
                            <h2 class="text-center" runat="server">
                                <asp:Label ID="Label1" runat="server" Text="Gps/Geo Fence User Allocation" Font-Size="20px"></asp:Label></h2>
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                    <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                        ToolTip="Enter Text Here"></asp:TextBox>
                                    <asp:LinkButton ID="linkcheck" runat="server"
                                        OnClick="linkcheck_Click">
											<img src="../Images/Selective_Mgr.png" />
                                    </asp:LinkButton>--%>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                        <%-- <asp:ListItem Selected="True">---Select---</asp:ListItem>--%>
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select"></asp:DropDownList>
                                </div>

                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" Width="60px" OnClick="btnGo_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="display-reportMaintable clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">

                                    <asp:GridView ID="grdgps" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" EmptyDataText="No Records Found"
                                        AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdgps_RowDataBound" Height="191px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdgps.PageIndex * grdgps.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ControlStyle Width="90%"></ControlStyle>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_code" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:TemplateField  ItemStyle-HorizontalAlign="Left" HeaderText="Reporting Mgr" Visible="false">
                                    <ControlStyle Width="90%"></ControlStyle>
                                    <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left" ></ItemStyle>                                
                                    <ItemTemplate>
                                        <asp:Label ID="lblReport" runat="server" Text='<%# Bind("Reporting_To_leve1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField> --%>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="FieldForce Name">
                                                <ControlStyle Width="90%"></ControlStyle>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_name" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="HQ">
                                                <ControlStyle Width="60%"></ControlStyle>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblhq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Designation">
                                                <ControlStyle Width="10%"></ControlStyle>

                                                <ItemTemplate>
                                                    <asp:Label ID="lbldesg" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="GPS">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="GPS " onclick="SelectAllCheckboxes(this, '.GPS')" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkId" runat="server" Font-Bold="true" CssClass="GPS" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="GEO Fencing">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll_G" runat="server" Text="GEO Fencing Doctor" onclick="SelectAllCheckboxes(this, '.GEO')" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkfencing" runat="server" Font-Bold="true" CssClass="GEO" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="GeoNeed" Visible="false">
                                                <ControlStyle Width="90%"></ControlStyle>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblGeoNeed" runat="server" Text='<%# Bind("GeoNeed") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="GeoFence" Visible="false">
                                                <ControlStyle Width="90%"></ControlStyle>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblGeoFence" runat="server" Text='<%# Bind("Geofence") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="GEO Fencing Chemist">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll_chem" runat="server" Text="GEO Fencing Chemist" onclick="SelectAllCheckboxes(this, '.chem')" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkfencingche" runat="server" Font-Bold="true" CssClass="chem" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="GEO Fencing Stock">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll_stock" runat="server" Text="GEO Fencing Stock" onclick="SelectAllCheckboxes(this, '.stock')" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkfencingstock" runat="server" Font-Bold="true" CssClass="stock" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Chemist" Visible="false">
                                                <ControlStyle Width="90%"></ControlStyle>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblGeoChe" runat="server" Text='<%# Bind("GeoFencingche") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="stock" Visible="false">
                                                <ControlStyle Width="90%"></ControlStyle>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblGeostck" runat="server" Text='<%# Bind("GeoFencingstock") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="1%" ItemStyle-HorizontalAlign="center" HeaderText="Detailing Offline Mode">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkOffline_Mode" runat="server" Text="Detailing Offline Mode" onclick="SelectAllCheckboxes(this, '.offline')" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkOfflineMode" runat="server" Font-Bold="true" CssClass="offline" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Detailing Offline Mode" Visible="false">
                                                <ControlStyle Width="90%"></ControlStyle>

                                                <ItemTemplate>
                                                    <asp:Label ID="lbldigital_offline" runat="server" Text='<%# Bind("digital_offline") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>

                            <br />
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnUpdate" CommandName="Update" runat="server"
                                    CssClass="savebutton" Text="Save Setting" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                    CssClass="resetbutton" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>

            </div>
            <br />
            <br />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

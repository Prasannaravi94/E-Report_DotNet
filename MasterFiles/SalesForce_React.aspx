<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesForce_React.aspx.cs" Inherits="MasterFiles_SalesForce_React" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
        .display-table .table td {
            padding: 15px 15px !important;
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
    <style type="text/css">
        #tooltip {
            position: absolute;
            z-index: 3000;
            border: 1px solid #111;
            background-color: #FEE18D;
            padding: 5px;
            opacity: 0.85;
        }

            #tooltip h3, #tooltip div {
                margin: 0;
            }
        .alignment {
            min-width:125px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center"><span runat="server" id="levelset">Field F<asp:LinkButton ID="lnk" runat="server" Text="o" OnClick="lnk_Click" ForeColor="#292a34"></asp:LinkButton>rce Reactivation </span></h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">

                                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowDataBound="grdSalesForce_RowDataBound"
                                        OnRowCommand="grdSalesForce_RowCommand" GridLines="None" CssClass="table" 
                                       AllowSorting="true">
                                       
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSF_Code" runat="server" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="User Name" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FieldForce Name" >
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Design" >
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="HQ" >
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="State" >
                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reporting"  ItemStyle-HorizontalAlign="Left">

                                                <ItemStyle HorizontalAlign="Left" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrepo" runat="server" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Doctor Count"  ItemStyle-HorizontalAlign="Left">

                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldrcnt" runat="server" Text='<%# Bind("Drcnt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="alignment">
                                               
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblS" runat="server"  ForeColor="#483d8b" Text='<%# Bind("sf_Tp_Active_flag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reactivate" > 
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("SF_Code") %>'
                                                        CommandName="Reactivate" OnClientClick="return confirm('Do you want to Reactivate the Fieldforce');">Reactivate 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delete" >
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeacti" runat="server" CommandArgument='<%# Eval("SF_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Delete the Fieldforce');">Delete 
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </div>
                            </div>

                        </div>
                    </div>

                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
                </div>
            </div>

            <br />
            <br />
           
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockistList.aspx.cs" Inherits="MasterFiles_StockistList" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist List</title>

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

        .alignment {
            min-width: 120px;
        }
    </style>
    <style type="text/css">
        .blink {
            animation: blink-animation 1s steps(5, start) infinite;
            -webkit-animation: blink-animation 1s steps(5, start) infinite;
        }

        @keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }

        @-webkit-keyframes blink-animation {
            to {
                visibility: hidden;
            }
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

        $(document).ready(function () {
            $('#foobar #foo').change(function () {
                $('#foobar1 img.loading').show();

                $('#foobar1 #foo1').load('foo.html', function () {
                    $('#foobar1 img.loading').hide();

                });

            });
        });
    </script>
    <script type="text/javascript">

        function Search_Gridview(strKey, strGV) {
            var strData = strKey.value.toLowerCase().split(" ");
            var tblData = document.getElementById(strGV);
            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }

    </script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript">


        //        function showLoader(loaderType) {
        //            $("#TxtSrch").hide();
        //            $("#Btnsrc").hide();
        //            $("#ddlStockist").hide();

        //            if (loaderType == "Search") {
        //                document.getElementById("loaderSearch").style.display = '';
        //            }

        //        }

    </script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../JScript/jquery-1.10.2.js" type="text/javascript"></script>

    <script src="../JScript/Service_CRM/Stockist_JS/Stockist_Add_Detail_JS.js" type="text/javascript"></script>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
          $(document).ready(function () {
              $(".custom-select2").select2();
          });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div  class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div  class="col-lg-11">
                        <h2 class="text-center" style="border-bottom: 0px">Stockist List</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-12">

                                        <asp:Button ID="btnNew" runat="server" Width="60px" CssClass="savebutton"
                                            Text="Add" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnReActivate" runat="server" Width="100px" CssClass="resetbutton"
                                            Text="ReActivation" OnClick="btnReActivate_Click" />
                                        <asp:Button ID="btnStockistDeActivate" runat="server" Width="130px"
                                            CssClass="resetbutton" Text="Bulk DeActivation" OnClick="btnStockistDeActivate_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="SearchBy" runat="server" Text="SearchBy" CssClass="label"> </asp:Label>
                                        <asp:DropDownList ID="ddlFields" runat="server" CssClass="nice-select">
                                            <asp:ListItem Value="">---Select---</asp:ListItem>
                                            <asp:ListItem Value="Stockist_Name" Selected="true">Stockist Name</asp:ListItem>
                                            <asp:ListItem Value="State">State Name</asp:ListItem>
                                            <asp:ListItem Value="Territory">HQ Name</asp:ListItem>
                                            <asp:ListItem Value="Stockist_Designation">ERP Code</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="single-des clearfix" style="padding-top: 19px;">
                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                        </div>
                                        <div id="DDdisplay" class="single-des clearfix" style="margin-top: -20px;" >
                                            <asp:DropDownList ID="ddlSrc" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                                                TabIndex="4">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnProduct" runat="server" />
                                        </div>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">

                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="50px"
                                            Text="Go" CssClass="savebutton" OnClientClick="return ProcessData()"></asp:Button>

                                        <asp:Button ID="btnclr" runat="server" Width="60px" Height="25px" Visible="false"
                                            Text="Clear" CssClass="savebutton"></asp:Button>

                                    </div>


                                </div>
                            </div>
                            <br />
                           
                            <div class="row" style="scrollbar-width: thin; overflow-x: auto">
                                <table width="100%" align="center">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" style="width: 50%" align="center">
                                                <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                                    runat="server" HorizontalAlign="Center" Visible="false">
                                                    <SeparatorTemplate>
                                                    </SeparatorTemplate>
                                                    <ItemTemplate>
                                                        &nbsp
                                                         <asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Size="15px"
                                                             CommandArgument='<%#bind("stockist_name") %>' Text='<%#bind("stockist_name") %>'>
                                                         </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <br />

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="gv_Stock" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt"
                                        Visible="false">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stockist_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Code1" runat="server" Text='<%# Eval("Stockist_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stockist Name" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Name1" CssClass="blink" Style="color: Red;" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="ERP Code" HeaderStyle-Width="300px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                     <asp:Label ID="lblStockist_Designation1" CssClass="blink" Style="color: Red;" runat="server" Text="Select ERP"></asp:Label>
                                                   <%-- <asp:Label ID="lblStockist_Designation1" CssClass="blink" Style="color: Red;" runat="server" Text='<%# Bind("Stockist_Designation") %>'></asp:Label>--%>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState1" runat="server" CssClass="blink" Style="color: Red;" Text='<%# Bind("State") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ Name" Visible="true" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritory1" runat="server" CssClass="blink" Style="color: Red;" Text='<%# Bind("Territory") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Person" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_ContactPerson1" CssClass="blink" Style="color: Red;" runat="server" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile No" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Mobile1" runat="server" CssClass="blink" Style="color: Red;" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" ControlStyle-CssClass="blink" ItemStyle-ForeColor="Red"></asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-Font-Bold="true" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeativate" runat="server" Text=''></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                    <asp:GridView ID="grdStockist" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" OnRowUpdating="grdStockist_RowUpdating" OnRowDataBound="grdStockist_RowDataBound"
                                        OnRowEditing="grdStockist_RowEditing" OnRowCreated="grdStockist_RowCreated" OnRowCancelingEdit="grdStockist_RowCancelingEdit"
                                        EmptyDataText="No Records Found" OnRowCommand="grdStockist_RowCommand" OnPageIndexChanging="grdStockist_PageIndexChanging"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AllowPaging="True" PageSize="10" AllowSorting="True" OnSorting="grdStockist_Sorting"
                                        Visible="false">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdStockist.PageIndex * grdStockist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stockist_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Code" runat="server" Text='<%# Eval("Stockist_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Stockist_Name" HeaderText="Stockist Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtStockist_Name" runat="server" CssClass="input" MaxLength="150" Height="38px"
                                                        Text='<%# Bind("Stockist_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Name" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>

                                             <asp:TemplateField SortExpression="Stockist_Designation" HeaderText="ERP Code" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtStockist_Designation" runat="server" CssClass="input" MaxLength="150" Height="38px"
                                                        Text='<%# Bind("Stockist_Designation") %>' Enabled="false"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Designation" runat="server" Text='<%# Bind("Stockist_Designation") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="State">
                                                <EditItemTemplate>
                                                    <%-- <asp:TextBox ID="txtState" runat="server" SkinID="TxtBxNumOnly" MaxLength="10" Text='<%# Bind("State") %>'></asp:TextBox>--%>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>' Visible="false" />
                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="revDdlState" runat="server" ControlToValidate="ddlState" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="Please Select State"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ Name" Visible="true">
                                                <EditItemTemplate>
                                                    <%--  <asp:TextBox ID="txtTerritory" runat="server" SkinID="TxtBxNumOnly" MaxLength="10"
                                            Text='<%# Bind("Territory") %>'></asp:TextBox>--%>
                                                    <asp:Label ID="lblHQName" runat="server" Text='<%# Bind("Territory") %>' Visible="false" />
                                                    <asp:DropDownList ID="ddlHQ" runat="server" CssClass="nice-select">
                                                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="revDdlMatch" runat="server" ControlToValidate="ddlHQ" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="Please Select HQ Name"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtStockist_ContactPerson" runat="server" CssClass="input" Height="38px"
                                                        MaxLength="150" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_ContactPerson" runat="server" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile No">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtStockist_Mobile" runat="server" CssClass="input" MaxLength="10" Height="38px"
                                                        Text='<%# Bind("Stockist_Mobile") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Mobile" runat="server" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Fieldforce Name">             
             <ItemTemplate> 
                 <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("SF_Code") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>--%>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-CssClass="alignment"
                                                HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"></asp:CommandField>

                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="Stockist_Creation.aspx?stockist_code={0}"
                                                DataNavigateUrlFields="stockist_code"></asp:HyperLinkField>

                                            <asp:TemplateField HeaderText="Deactivate">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Stockist_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Stockist');">Deactivate
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
                    <asp:Button ID="btnBack" runat="server" Width="60px" Height="25px" CssClass="savebutton"
                        Text="Back" Visible="false" />
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
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_Bulk_Activation.aspx.cs"
    Inherits="MasterFiles_Stockist_Bulk_Activation" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ReActivation Stockist</title>

    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {

                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
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

        .marRight {
            margin-right: 35px;
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

        });
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
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center" style="border-bottom: 0px">ReActivation Stockist</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-name-heading clearfix">

                            <div class="row clearfix">
                                <div class="col-lg-3">
                                    <asp:Label ID="SearchBy" runat="server" Text="SearchBy" CssClass="label"> </asp:Label>
                                    <asp:DropDownList ID="ddlFields" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="">---Select---</asp:ListItem>
                                        <asp:ListItem Value="Stockist_Name" Selected="true">Stockist Name</asp:ListItem>
                                        <asp:ListItem Value="State">State Name</asp:ListItem>
                                        <asp:ListItem Value="Territory">HQ Name</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="col-lg-3">
                                    <div class="single-des clearfix" style="padding-top: 19px;">
                                        <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    </div>
                                    <div id="DDdisplay" style="margin-top: -20px;">
                                        <asp:DropDownList ID="ddlSrc" runat="server" width="100%"
                                         CssClass="custom-select2 nice-select" TabIndex="4">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnProduct" runat="server" />
                                    </div>

                                </div>
                                <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="50px"
                                        Text="Go" CssClass="savebutton" OnClientClick="return ProcessData()"></asp:Button>

                                    <asp:Button ID="btnclr" runat="server" Visible="false"
                                        Text="Clear" CssClass="resetbutton"></asp:Button>
                                </div>


                            </div>
                        </div>
                        <br />
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
                                <div style="width: 100%">
                                    <center>
                                        <asp:GridView ID="gv_Activate" runat="server" Width="100%" HorizontalAlign="Center"
                                            AutoGenerateColumns="false" GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
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
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
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
                                                <asp:TemplateField HeaderText="Contact Person" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockist_ContactPerson1" CssClass="blink" Style="color: Red;" runat="server" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No" ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockist_Mobile1" runat="server" CssClass="blink" Style="color: Red;" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deactivate Date" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeactivateDate1" runat="server" CssClass="blink" Style="color: Red;" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:HyperLinkField HeaderText="Activate" Text="Activate" ControlStyle-CssClass="blink" ItemStyle-ForeColor="Red"></asp:HyperLinkField>

                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </center>
                                </div>
                                <center>
                                    <div>
                                        <asp:GridView ID="grdAct_Stockist" runat="server" Width="100%" HorizontalAlign="Center"
                                            AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None"
                                            CssClass="table" PagerStyle-CssClass="gridview1"
                                            Visible="false" AllowPaging="True" PageSize="10" AllowSorting="True"
                                            OnPageIndexChanging="grdAct_Stockist_PageIndexChanging"
                                            OnRowCommand="grdAct_Stockist_RowCommand" OnSorting="grdAct_Stockist_Sorting">

                                            <Columns>
                                                <asp:TemplateField HeaderText="#">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo_A" runat="server" Text='<%# (grdAct_Stockist.PageIndex * grdAct_Stockist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stockist_Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockist_Code_A" runat="server" Text='<%# Eval("Stockist_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="Stockist_Name" HeaderText="Stockist Name" ItemStyle-HorizontalAlign="Left">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtStockist_Name_A" runat="server" CssClass="input" MaxLength="150"
                                                            Text='<%# Bind("Stockist_Name") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockist_Name_A" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State" SortExpression="State">
                                                    <EditItemTemplate>
                                                        <%-- <asp:TextBox ID="txtState" runat="server" SkinID="TxtBxNumOnly" MaxLength="10" Text='<%# Bind("State") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblState_A" runat="server" Text='<%# Bind("State") %>' Visible="false" />
                                                        <asp:DropDownList ID="ddlState_A" runat="server" CssClass="nice-select">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblState_A" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HQ Name" Visible="true" SortExpression="Territory">
                                                    <EditItemTemplate>
                                                        <%--  <asp:TextBox ID="txtTerritory" runat="server" SkinID="TxtBxNumOnly" MaxLength="10"
                                            Text='<%# Bind("Territory") %>'></asp:TextBox>--%>
                                                        <asp:Label ID="lblHQName_A" runat="server" Text='<%# Bind("Territory") %>' Visible="false" />
                                                        <asp:DropDownList ID="ddlHQ_A" runat="server" CssClass="nice-select">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTerritory_A" runat="server" Text='<%# Bind("Territory") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtStockist_ContactPerson_A" runat="server" CssClass="input"
                                                            MaxLength="150" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockist_ContactPerson_A" runat="server" Text='<%# Bind("Stockist_ContactPerson") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile No">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtStockist_Mobile_A" runat="server" CssClass="input" MaxLength="10"
                                                            Text='<%# Bind("Stockist_Mobile") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStockist_Mobile_A" runat="server" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DeActivate Date" SortExpression="DeActivateDate">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDeAct" runat="server" CssClass="input" MaxLength="10" Text='<%# Bind("DeActivateDate") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeActDate" runat="server" Text='<%# Bind("DeActivateDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Activate">

                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbutActivate" runat="server" CommandArgument='<%# Eval("Stockist_Code") %>'
                                                            CommandName="Activate" OnClientClick="return confirm('Do you want to Activate the Stockist');">Activate
                                                        </asp:LinkButton>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>
                                    </div>
                                </center>
                            </div>
                        </div>

                    </div>

                </div>
                <asp:Button ID="btnBack" runat="server" CssClass="backbutton"
                    Text="Back" OnClick="btnBack_Click" Visible="true" />
            </div>
        </div>

        <br />
        <br />
         <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

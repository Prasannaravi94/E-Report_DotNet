<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_Bulk_DeActivation.aspx.cs"
    Inherits="MasterFiles_Stockist_Bulk_DeActivation" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist Deactivation</title>

    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />
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

        .display-table .table tr td [type="checkbox"]:not(:checked) + label, .display-table .table tr td [type="checkbox"]:checked + label {
            padding-left: 1.4em;
        }

        .display-table .table tr th [type="checkbox"]:not(:checked) + label, .display-table .table tr th [type="checkbox"]:checked + label {
            padding-left: 0.0em;
            margin-left: 10px;
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
    <script type="text/javascript">
        $(document).ready(function () {


            var arrayOfValues = [];

            //            $("#btnSubmit").click(function () {

            //                $("#<%=grdStockist.ClientID %> input:checkbox[id*=chkStockist]").each(function () {

            //                    var StockCode = $(this).closest('tr').find('td:eq(2)').text();

            //                    alert(StockCode);
            //                    arrayOfValues.push(StockCode);
            //                }).get();
            //            });


            var gridView1Control = document.getElementById('<%= grdStockist.ClientID %>');


            $('#<%=btnSubmit.ClientID %>').click(function (e) {

                $('input:checkbox[id$=chkStockist]:checked', gridView1Control).each(function (item, index) {

                    // var StockCode = $(this).closest('tr').find('td:eq(1)').val();
                    var id = $(this).closest('tr').find('input:hidden[id$=hdnId]').val();
                    //  alert(id);

                });

            });


        });
    </script>

    <script src="../JScript/Service_CRM/Stockist_JS/Stockist_Add_Detail_JS.js" type="text/javascript"></script>

    
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
          $(document).ready(function () {
              $(".custom-select2").select2();
          });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <div>
                <ucl:Menu ID="menu1" runat="server" />
            </div>
            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" style="border-bottom: 0px">Stockist Deactivation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="SearchBy" runat="server" Text="SearchBy" CssClass="label"> </asp:Label>
                                        <asp:DropDownList ID="ddlFields" runat="server" CssClass="nice-select">
                                            <asp:ListItem Value="">---Select---</asp:ListItem>
                                            <asp:ListItem Value="Stockist_Name">Stockist Name</asp:ListItem>
                                            <asp:ListItem Value="State" Selected="true">State Name</asp:ListItem>
                                            <asp:ListItem Value="Territory">HQ Name</asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-lg-3">
                                        <div class="single-des clearfix" style="padding-top: 19px;">
                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%"></asp:TextBox>

                                        </div>
                                        <div id="DDdisplay" style="margin-top: -20px;">
                                            <asp:DropDownList ID="ddlSrc" runat="server" CssClass="custom-select2 nice-select" Width="100%"
                                                TabIndex="4">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnProduct" runat="server" />
                                        </div>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Width="50px"
                                            Text="Go" CssClass="savebutton" OnClientClick="return ProcessData()"></asp:Button>

                                        <asp:Button ID="btnclr" OnClick="btnClear_Click" runat="server"
                                            Visible="false" Text="Clear" CssClass="resetbutton"></asp:Button>
                                    </div>


                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="text-center" style="border-bottom: 0px">
                                <asp:Label ID="lblValue" Text="Select the State Name" runat="server" Style="font-weight: bold; color: Red" Visible="false"></asp:Label>
                            </div>

                            <div class="row" style="scrollbar-width: thin; overflow-x: auto">

                                <table width="100%">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" align="center"></td>
                                        </tr>
                                        <tr>
                                            <td />
                                            <td colspan="2" align="center">
                                                <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                                    runat="server" HorizontalAlign="Center">
                                                    <SeparatorTemplate>
                                                    </SeparatorTemplate>
                                                    <ItemTemplate>
                                                        &nbsp
                            <asp:LinkButton ID="lnkbtnAlpha" Font-Size="15px" runat="server"
                                CommandArgument='<%#Eval("Stockist_Name") %>' Text='<%#Eval("Stockist_Name") %>'>
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
                                    <asp:GridView ID="grdStockist" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="False" EmptyDataText="No Records Found" GridLines="None"
                                        CssClass="table"
                                        AllowSorting="True" OnSorting="grdStockist_Sorting">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdStockist.PageIndex * grdStockist.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="." onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkStockist" runat="server" Text="." />
                                                    <asp:HiddenField ID="hdnId" runat="server" Value='<%#Eval("Stockist_Code") %>' />
                                                    <asp:Image ID="imgCross" runat="server" ImageUrl="../../../Images/cross.png" Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Stockist_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("Stockist_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stockist Name" SortExpression="Stockist_Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockistName" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State" Visible="true" SortExpression="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ Name" Visible="true" SortExpression="Territory">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Address" runat="server" Text='<%# Bind("Stockist_Address") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Mobile" runat="server" Text='<%# Bind("Stockist_Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FieldForce Name">
                                                <ItemTemplate>
                                                    <%--   <asp:Label ID="chkboxSalesforce" runat="server" Text='<%# ((string)Eval("SfName")).Replace("\n", "<br/>") %>' ></asp:Label>--%>
                                                    <asp:Literal runat="server" ID="Values" Text='<%# string.Join("<br />", Eval("SfName").ToString().Split(new []{","},StringSplitOptions.None)) %>'>
                                                    </asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>

                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton"
                        Text="Back" OnClick="btnBack_Click" Visible="true" />
                </div>
                <br />
                <br />
                <center>
                    <asp:Button ID="btnSubmit" runat="server" Text="De-Activate"
                        CssClass="savebutton" OnClick="btnSubmit_Click" />
                </center>
                <div class="div_fixed">
                    <asp:Button ID="btnSave" runat="server" CssClass="savebutton"
                        Text="De-Activate" OnClick="btnSave_Click" />
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
            <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

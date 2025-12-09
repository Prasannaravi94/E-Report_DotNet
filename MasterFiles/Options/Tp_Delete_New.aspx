<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tp_Delete_New.aspx.cs" Inherits="MasterFiles_Options_Tp_Delete_New" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP Delete</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
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

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>

    <script type="text/javascript">

        function ShowgridData() {
            var para = $('#txtMonthYear').val();
            //alert(para);
            $.ajax({
                type: "POST",
                url: "TPDelete.aspx/GetGriddata",
                data: '{name:' + para + '  }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert(response.d);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">

                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server">TP Delete</h2>

                        <div class="designation-area clearfix">
                         
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Width="100%">FieldForce Name</asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" CssClass="custom-select2 nice-select" runat="server"></asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false"></asp:DropDownList>
                            </div>
                           
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            
                            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" />

                        </div>
                    </div>

                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdTP" runat="server" EmptyDataText="No Records Found" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" OnRowDataBound="grdTP_RowDataBound"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1">
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <Columns>
                                           
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="." onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSNo" Text="." runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SF Code" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSFCode" runat="server" Text='<%#Eval("sf_code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Field Force" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSFName" runat="server" Text='<%#Eval("sf_name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReporting" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%#Eval("sf_hq")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tour Month" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTour_Month" runat="server" Text='<%#Eval("Tour_Month")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tour Year" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTour_Year" runat="server" Text='<%#Eval("Tour_Year")%>'></asp:Label>
                                                    <asp:HiddenField ID ="min_tour_date" runat="server"  Value="" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TP Entered" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkTP" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Last Month TP" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFutureTP" runat="server" Text='<%#Eval("Last_tp_dt")%>' Visible="false"></asp:Label>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                           
                            <asp:Button ID="btnSubmit" runat="server" Text="TP Delete" CssClass="savebutton"
                                OnClientClick="deleteconfirm();"
                                OnClick="btnSubmit_Click" Visible="false" />

                        </div>
                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">

            function deleteconfirm() {
                var boolvalconf = confirm("Do you want to delete the Tour Plan?");
                if (boolvalconf) {
                    var boolvalconfagain = confirm("Are you sure to delete the Tour Plan?");
                    if (boolvalconfagain) {
                        return true;
                    }
                   
                }
                else {
                    return false;
                }
            }


            $(function () {
                $('[id*=txtMonthYear]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "M-yyyy",
                    viewMode: "months",
                    minViewMode: "months",
                    language: "tr"
                });
            });
        </script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>

    </form>
</body>
</html>

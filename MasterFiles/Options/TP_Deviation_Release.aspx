<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TP_Deviation_Release.aspx.cs" Inherits="MasterFiles_Options_TP_Deviation_Release" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TP Devation - Release</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Grid.css" />--%>
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

        });
    </script>

    <script type="text/javascript" language="javascript">
        function validateCheckBoxes() {
            var isValid = false;
            var gridView = document.getElementById('<%= grdRelease.ClientID %>');
            var validator = document.getElementById('RequiredFieldValidator1');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;

                            if (confirm('Do you want to Realease?')) {

                            }
                            else {
                                return false;
                            }
                            return true;

                            if (confirm('Do you want to Realease?')) {
                                if (confirm('Are you sure?')) {
                                    ShowProgress();

                                    return true;

                                }
                                else {
                                    return false;
                                }
                            }
                            else {
                                return false;
                            }
                        }
                    }
                }
            }
            alert("Please Select at least one record.");

            return false;
        }

        //        function confirm_Save() {

        //            if (confirm('Do you want to Realease?')) {
        //                if (confirm('Are you sure?')) {
        //                    ShowProgress();

        //                    return true;

        //                }
        //                else {
        //                    return false;
        //                }
        //            }
        //            else {
        //                return false;
        //            }
        //        }
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
            <div id="Divid" runat="server">
            </div>
            <br />
  
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblYear" runat="server" Text="Year" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>

                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMonth" runat="server" Text="Month" CssClass="label"></asp:Label>

                                <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>
                             
                            <%--<asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                           ToolTip="Enter Text Here"></asp:TextBox>--%>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" >
                            </asp:DropDownList>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <br />
                                 <asp:Button ID="btnGo" runat="server" CssClass="savebutton"  Text="Go" OnClick="btnGo_Click" />
                               
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                   <asp:GridView ID="grdRelease" runat="server" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=""  ItemStyle-HorizontalAlign="Center" >
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" Text="Release All" runat="server" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRelease" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sf Code" Visible="false">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblsf_code" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FieldForce Name" >
                                                <ItemTemplate>

                                                    <asp:Label ID="lblSfName" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Eval("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Designation" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesi" runat="server" Text='<%# Eval("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last DCR Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbllast_dcr" runat="server" Text='<%# Eval("last_dcr_date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Delayed_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reason for Deviation" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDevRea" runat="server" Width="200px" Text='<%# Eval("Deviation_Reason") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Reason for Release" >
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtreason" Width="280px"  Height="40px" runat="server" CssClass="input"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>

                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />

                            <asp:Button ID="btnSubmit" runat="server" Text="Release" OnClientClick="return validateCheckBoxes()" CssClass="savebutton" Visible="false"
                                OnClick="btnSubmit_Click" />
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
          <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

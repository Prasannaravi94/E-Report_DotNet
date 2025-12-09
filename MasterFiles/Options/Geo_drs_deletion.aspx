<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Geo_drs_deletion.aspx.cs" Inherits="MasterFiles_Options_Geo_drs_deletion" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Geo Tagg Drs Deletion</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
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
            var gridView = document.getElementById('<%= Geodrs.ClientID %>');
            var validator = document.getElementById('RequiredFieldValidator1');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;

                            if (confirm('Do you want to Delete the Selected Drs?')) {

                            }
                            else {
                                return false;
                            }
                            return true;

                            if (confirm('Do you want to Delete the Selected Drs?')) {
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
    </script>


    <script type="text/javascript" language="javascript">
        function validateCheckBoxeschem() {
            var isValid = false;
            var gridView = document.getElementById('<%= GrdChemist.ClientID %>');
            var validator = document.getElementById('RequiredFieldValidator1');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;

                            if (confirm('Do you want to Delete the Selected Chemists?')) {

                            }
                            else {
                                return false;
                            }
                            return true;

                            if (confirm('Do you want to Delete the Selected Chemists?')) {
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
    </script>
    <script type="text/javascript" language="javascript">
        function validateCheckBoxesStock() {
            var isValid = false;
            var gridView = document.getElementById('<%= GrdStockist.ClientID %>');
            var validator = document.getElementById('RequiredFieldValidator1');
            for (var i = 1; i < gridView.rows.length; i++) {
                var inputs = gridView.rows[i].getElementsByTagName('input');
                if (inputs != null) {
                    if (inputs[0].type == "checkbox") {
                        if (inputs[0].checked) {
                            isValid = true;

                            if (confirm('Do you want to Delete the Selected Stockists?')) {

                            }
                            else {
                                return false;
                            }
                            return true;

                            if (confirm('Do you want to Delete the Selected Stockists?')) {
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
    <script type="text/javascript">
        function checkAllchem(objRef) {
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
    <script type="text/javascript">
        function checkAllStck(objRef) {
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

    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
                var type = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }

            });
        });

    </script>
    <style type="text/css">
         .table th:first-child,.table th:nth-child(2)
         {
             width:5%;
         }
       .table tr [type="checkbox"]:not(:checked) + label, .table tr [type="checkbox"]:checked + label {
            padding-left: 0.75em;
            color:white;
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
            <div id="Divid" runat="server"></div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">

                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:RadioButtonList ID="rdoFenceDel" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" Width="100%">
                                    <asp:ListItem Value="1">Doctor</asp:ListItem>
                                    <asp:ListItem Value="2">Chemist</asp:ListItem>
                                    <asp:ListItem Value="3">Stockist</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <br />
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" Text="FieldForce Name" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                </asp:DropDownList>
                            </div>

                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Width="60px" Text="Go" OnClick="btnGo_Click" />
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
                                <asp:Panel ID="PnlDrs" runat="server" Visible="false">

                                    <asp:GridView ID="Geodrs" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" Text="." runat="server" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkRelease" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbllstdrName" name="lbllstdrName" runat="server" Text='<%# Bind("listeddr_name") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor Specialty">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbllstdrspec" name="lbllstdrspec" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lat">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_lat" name="lbl_lat" runat="server" Text='<%# Bind("lat") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Long">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_long" name="lbl_long" runat="server" Text='<%# Bind("long") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MapId" Visible="false">
                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="mapid" runat="server" Text='<%#   Bind("Mapid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDR_Code" runat="server" Text='<%#   Bind("ListeddrCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </asp:Panel>
                                <asp:Panel ID="PnlChemist" runat="server" Visible="false">

                                    <asp:GridView ID="GrdChemist" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAllC" Text="." runat="server" onclick="checkAllchem(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkReleasech" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Chemists Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbllChemists_Name" name="lbllChemists_Name" runat="server" Text='<%# Bind("Chemists_Name") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lat">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_lat" name="lbl_lat" runat="server" Text='<%# Bind("lat") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Long">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_long" name="lbl_long" runat="server" Text='<%# Bind("long") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="MapId" Visible="false">
                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="mapid" runat="server" Text='<%#   Bind("Mapid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Chemists Code" Visible="false">
                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblChem_Code" runat="server" Text='<%#   Bind("Chemists_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </asp:Panel>
                                <asp:Panel ID="PnlStockist" runat="server" Visible="false">

                                    <asp:GridView ID="GrdStockist" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAllStck" Text="." runat="server" onclick="checkAllStck(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkReleasestk" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stockist Name">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbllStockist_Name" name="lbllStockist_Name" runat="server" Text='<%# Bind("Stockist_Name") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lat">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_lat" name="lbl_lat" runat="server" Text='<%# Bind("lat") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Long">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_long" name="lbl_long" runat="server" Text='<%# Bind("long") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="MapId" Visible="false">
                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="mapid" runat="server" Text='<%#   Bind("Mapid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stockist Code" Visible="false">
                                                <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStockist_Code" runat="server" Text='<%#   Bind("Stockist_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                </asp:Panel>
                            </div>
                        </div>

                        <br />
                        <center>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnSubmit" runat="server" Text="Delete" OnClientClick="return validateCheckBoxes()" CssClass="savebutton" Visible="false"
                                            OnClick="btnSubmit_Click" />
                                        <asp:Button ID="btnsubmitchemist" runat="server" Text="Delete" OnClientClick="return validateCheckBoxeschem()" CssClass="savebutton" Visible="false" OnClick="btnsubmitchemist_Click" />
                                        <asp:Button ID="btnSubmit1Stockist" runat="server" Text="Delete" OnClientClick="return validateCheckBoxesStock()" CssClass="savebutton" Visible="false" OnClick="btnSubmit1Stockist_Click" />
                                    </td>
                                </tr>
                            </table>
                        </center>
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
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>


<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetupScreen.aspx.cs" Inherits="MasterFiles_Options_SetupScreen" 
    MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Setup for Screen Access</title>
    <%--<style type="text/css">
        #tblDocRpt
        {
            margin-left: 300px;
        }
         .textSelector
        {
             cursor:pointer 
        }
         .textHQ
        {
             cursor:pointer 
        }
       
    </style>
    
 
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        var tempScrollLeft;
        window.addEventListener('scroll', function () {
            tempScrollLeft = $(window).scrollLeft();
        });
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:checkbox').click(function (e) {
                $(window).scrollLeft(tempScrollLeft);
            });
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

                var Force = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Force == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }


            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

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
    <script type="text/javascript">
        function SelectAllCheckboxes(chk, selector) {
            $('#<%=grdSalesForce.ClientID%>').find(selector + " input:checkbox").each(function () {
                $(this).prop("checked", $(chk).prop("checked"));
                //$('#grdSalesForce').scrollLeft($(this).scrollLeft());
            });
        }
    </script>
    <%--<style type="text/css">
        input[type="checkbox"]:checked {
            box-shadow: 0 0 0 1px hotpink;
            height: 18px;
            width: 18px;
            cursor: pointer;
            color: red;
        }

        input[type="checkbox"] {
            cursor: pointer;
        }
    </style>--%>

    <script type="text/javascript">
        $(document).on("click", ".textSelector", function () {

            $(this).removeClass("textSelector");
            $(this).addClass("check");

            var row = $(this).closest("tr");
            var inputList = row.find("input[type=checkbox]");


            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                inputList[i].checked = true;
            }
        });

        $(document).on("click", ".check", function () {

            $(this).removeClass("check");
            $(this).addClass("textSelector");

            var row = $(this).closest("tr");
            var inputList = row.find("input[type=checkbox]");



            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                inputList[i].checked = false;
            }
        });

    </script>
    <script type="text/javascript">
        $(document).on("click", ".textHQ", function () {
            $(this).removeClass("textHQ");
            $(this).addClass("check");

            var row = $(this).closest("tr");
            var inputList = row.find("input[type=checkbox]");


            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                inputList[i].checked = true;
            }
        });

        $(document).on("click", ".check", function () {

            $(this).removeClass("check");
            $(this).addClass("textHQ");

            var row = $(this).closest("tr");
            var inputList = row.find("input[type=checkbox]");



            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                inputList[i].checked = false;
            }
        });

    </script>
    <style>
        td:first-child {
            color: #636d73 !important;
        }

        .display-table .table tr td:first-child {
            padding: 2px 2px !important;
        }

        .display-table .table td {
            padding: 10px 5px !important;
        }

        #grdSalesForce > tbody > tr:nth-child(1) > td:nth-child(1) {
            background-color: Lavender;
            position: sticky;
            left: 0px;
            z-index: 1;
            min-width: 153px;
        }

        #grdSalesForce > tbody > tr:nth-child(1) > td:nth-child(2) {
            background-color: Lavender;
            position: sticky;
            left: 152px;
            z-index: 1;
        }

        #grdSalesForce > tbody > tr:nth-child(1) > td:nth-child(3) {
            background-color: Lavender;
            position: sticky;
            left: 200px;
            z-index: 1;
        }

        #grdSalesForce > tbody > tr:nth-child(n+3) > td:nth-child(1) {
            position: sticky;
            left: 0px;
            z-index: 1;
        }

        #grdSalesForce > tbody > tr:nth-child(n+3) > td:nth-child(2) {
            background-color: white;
            position: sticky;
            left: 152px;
            z-index: 1;
        }

        #grdSalesForce > tbody > tr:nth-child(n+3) > td:nth-child(3) {
            background-color: white;
            position: sticky;
            left: 200px;
            z-index: 1;
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
<body style="overflow-x: scroll">
    <form id="form1" runat="server">

        <div>

            <ucl:Menu ID="menu1" runat="server" />

            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label">FieldForce Name</asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select"
                                    OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="custom-select2 nice-select" Visible="false"></asp:DropDownList>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="overflow: inherit; scrollbar-width: thin;">

                                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center" Style="background-color: white"
                                        AutoGenerateColumns="false" OnRowCreated="grdSalesForce_RowCreated" OnRowDataBound="grdSalesForce_RowDataBound"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" EmptyDataText="No Records Found"
                                        AlternatingRowStyle-CssClass="alt" ShowHeader="False">
                                        <PagerStyle CssClass="gridview1"></PagerStyle>
                                        <RowStyle HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_code" runat="server" Text='<%# Bind("sf_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_name" runat="server" CssClass="textSelector" Text='<%# Bind("sf_name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDG" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_hq" runat="server" CssClass="textHQ" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDoctorAdd" CssClass="firstcheck" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDoctorEdit" CssClass="second" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDoctorDeAct" CssClass="third" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDoctorView" CssClass="fourth" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDoctorReAct" CssClass="fifth" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDoctorName" CssClass="six" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkNewDoctorAdd" CssClass="UA" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkNewDoctorEdit" CssClass="UE" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkNewDoctorDeAct" CssClass="UD" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkNewDoctorView" CssClass="UV" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkNewDoctorReAct" CssClass="UR" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkChemAdd" CssClass="CA" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkChemEdit" CssClass="CE" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkChemDeAct" CssClass="CD" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkChemView" CssClass="CV" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkChemReAct" CssClass="CR" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkTerrAdd" CssClass="TA" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkTerrEdit" CssClass="TE" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkTerrDeAct" CssClass="TD" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkTerrView" CssClass="TV" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkTerrReact" CssClass="TR" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkClassAdd" CssClass="HA" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkClassEdit" CssClass="HE" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkClassDeAct" CssClass="HD" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkClassView" CssClass="HV" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkClassReAct" CssClass="HR" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkPCPM" CssClass="PA" Text="&nbsp;" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>

                                </div>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnSubmit" runat="server" Visible="false" Text="Save" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                <asp:Button ID="btnClear" runat="server" CssClass="savebutton" Visible="false" Text="Clear" />
                            </div>
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

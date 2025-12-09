<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DoctorCategoryList.aspx.cs"
    EnableEventValidation="false" Inherits="MasterFiles_DoctorCategoryList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor-Category</title>
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

        .icon-invisible {
            visibility: hidden;
        }

        .table tr td img {
            max-width: 250px !important;
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
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />
    <link href="../css/Font-Awesome-4.7.0/css/font-awesome.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnGo').click(function () {
                var v = $("#<%=rbtnLstdDrWrngCrtn.ClientID %> input:checked").length;
                if (v == 0) { alert('Select an Option from Menu List'); $('#rbtnLstdDrWrngCrtn').focus(); return false; }
                var type = $('#<%=ddlSFCode.ClientID%> :selected').text();
                if (type == "---Select---") { alert("Select Field Force."); $('#ddlSFCode').focus(); return false; }
            });
        });
    </script>
    <script type="text/javascript">
        function RefreshUpdatePanel() {


            //$(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlSFCode]');
            var $items = $('select[id$=ddlSFCode] option');
            var item = $txt.val();

            //$txt.keyup(function () {
            //    searchDdl($txt.val());
            //});

            //function searchDdl(item) {
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
            //}

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
            //});
        }
    </script>
    <script type="text/javascript">
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                document.getElementById("loaderSearchddlSFCode").style.display = '';
            }
        }
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
        #gvDoctor [type="checkbox"]:not(:checked) + label, #gvDoctorFF [type="checkbox"]:not(:checked) + label,
        #gvDoctor [type="checkbox"]:checked + label, #gvDoctorFF [type="checkbox"]:checked + label {
            padding-left: 0.15em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:UpdatePanel runat="server" ID="upnlWrngCreation">
            <ContentTemplate>
                <div>
                    <ucl:Menu ID="menu1" runat="server" />


                    <div class="container home-section-main-body position-relative clearfix">
                        <div class="row justify-content-center ">
                            <div class="col-lg-11">

                                <h2 class="text-center">Doctor-Category</h2>
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:LinkButton ID="lnkbtnLstdrWrngCreation" runat="server" SkinID="lblMand" CommandArgument="Show"
                                                OnClick="lnkbtnLstdrWrngCreation_Click"><i class="fa fa-circle icon-invisible"></i></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <div class="designation-reactivation-table-area clearfix">
                                    <div class="display-name-heading clearfix">
                                        <div class="row clearfix">
                                            <div class="col-lg-10">
                                                <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="45px"
                                                    Text="Add" OnClick="btnNew_Click" />
                                                <asp:Button ID="btnBulkEdit" runat="server" CssClass="resetbutton"
                                                    Text="Bulk Edit" OnClick="btnBulkEdit_Click" />
                                                <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="resetbutton"
                                                    Text="S.No Gen" OnClick="btnSlNo_Gen_Click" />
                                                <asp:Button ID="btnTransfer_Cat" runat="server" CssClass="resetbutton" Width="140px"
                                                    Text="Transfer Category" OnClick="btnTransfer_Cat_Click" />
                                                <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton"
                                                    Text="Reactivation" OnClick="btnReactivate_Onclick" />
                                            </div>
                                        </div>
                                    </div>
                                    <p>
                                        <br />
                                    </p>
                                    <div class="display-table clearfix">
                                        <div class="table-responsive">
                                            <asp:GridView ID="grdDocCat" runat="server" Width="100%" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowUpdating="grdDocCat_RowUpdating"
                                                OnRowEditing="grdDocCat_RowEditing" OnRowDeleting="grdDocCat_RowDeleting" EmptyDataText="No Records Found"
                                                OnPageIndexChanging="grdDocCat_PageIndexChanging" OnRowCreated="grdDocCat_RowCreated"
                                                OnRowCancelingEdit="grdDocCat_RowCancelingEdit" OnRowCommand="grdDocCat_RowCommand"
                                                GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                                AllowSorting="True" OnSorting="grdDocCat_Sorting">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocCatCode" runat="server" Text='<%#Eval("Doc_Cat_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Cat_SName"
                                                        HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtDoc_Cat_SName" runat="server" CssClass="input" Width="100px"
                                                                Text='<%# Bind("Doc_Cat_SName") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDoc_Cat_SName" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Doc_Cat_Name"
                                                        HeaderText="Category Name" ItemStyle-HorizontalAlign="Left">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtDocCatName" runat="server" CssClass="input" Width="100px" Text='<%# Bind("Doc_Cat_Name") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocCatName" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Visit" ItemStyle-HorizontalAlign="Left">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtvisit" CssClass="input" onkeypress="CheckNumeric(event);" Width="100px"
                                                                runat="server" Text='<%# Bind("No_of_visit") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVisit" runat="server" Text='<%# Bind("No_of_visit") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Doctors" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Cat_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"></asp:CommandField>
                                                    <asp:HyperLinkField HeaderText="Edit" Text="Edit" ItemStyle-HorizontalAlign="Center"
                                                        DataNavigateUrlFormatString="DoctorCategory.aspx?Doc_Cat_Code={0}" DataNavigateUrlFields="Doc_Cat_Code"></asp:HyperLinkField>
                                                    <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Doc_Cat_Code") %>'
                                                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Doctor Category');">Deactivate
                                                            </asp:LinkButton>
                                                            <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                      <img src="../Images/deact2.png" alt="" width="75px" title="This Category Exists in Doctor" />
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Delete">
                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>
                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Doc_Cat_Code") %>'
                                    CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Doctor Category');">Delete
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="loading" align="center">
                                        Loading. Please wait.<br />
                                        <br />
                                        <img src="../Images/loader.gif" alt="" />
                                    </div>
                                </div>
                                <br />
                                <br />

                                <div id="dvLstdrWrngCreation" runat="server" visible="false">
                                    <center>
                                        <table width="50%" style="border: solid 1px #347C17; background-color: white; border-collapse: collapse">
                                            <tr>
                                                <td>
                                                    <label id="lblOption" style="margin-left: 10px; color: #8A2EE6; font-family: Verdana; font-weight: bold; text-transform: capitalize; font-size: 14px; text-align: center;">
                                                        &nbsp;Menu List</label>
                                                    <br />
                                                    <br />
                                                    <asp:RadioButtonList ID="rbtnLstdDrWrngCrtn" Style="margin-left: 10px;" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rbtnLstdDrWrngCrtn_SelectedIndexChanged" runat="server">
                                                        <asp:ListItem Value="1" Text=" Listed Doctor Wrong Creation - FieldForcewise"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text=" Listed Doctor Wrong Creation - Divisionwise"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />

                                        <div class="designation-area clearfix">
                                            <div class="single-des clearfix">
                                                <table width="90%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblSalesforce" runat="server" CssClass="label" Text="Field Force Name: "
                                                                Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNew" runat="server" CssClass="input" Visible="false" onkeyup="RefreshUpdatePanel();"
                                                                ToolTip="Enter Text Here" AutoPostBack="true" OnTextChanged="txtNew_TextChanged"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlSFCode" runat="server" CssClass="nice-select" Width="100%" AutoPostBack="true"
                                                                Visible="false" OnSelectedIndexChanged="ddlSFCode_SelectedIndexChanged" onchange="showLoader('Search1')">
                                                            </asp:DropDownList>
                                                        </td>

                                                        <td style="display: none; color: red; font-weight: bold;" id="loaderSearchddlSFCode">
                                                            <ul class="fa-ul">
                                                                <li><i class="fa-li fa fa-refresh fa-spin"></i>Loading...</li>
                                                            </ul>
                                                        </td>
                                            </div>                                           
                                        </div>
                                       
                                      <%--  <caption>--%>
                                           
                                            <table>
                                                <tr>
                                                    <td>&nbsp; </td>
                                                    <td class="stylespc">
                                                        <asp:Button ID="btnGo" runat="server" CausesValidation="false" CssClass="savebutton" Height="25px" OnClick="btnGo_Click" Text="GO" Width="40px" />
                                                        <asp:Button ID="btnClear" runat="server" CssClass="savebutton" Height="25px" OnClick="btnClear_Click" Text="Clear" Width="60px" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <div class="display-table clearfix">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvDoctorFF" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found" GridLines="None" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" RowStyle-HorizontalAlign="Center" Width="70%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" Text="." />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkListedDR" runat="server" Text="." />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Listed Doctor Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Listed Doctor Created Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCrtDate" runat="server" Text='<%#Eval("ListedDr_Created_Date")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Activity Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblActDate" runat="server" Text='<%#Eval("Activity_Date")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                    <asp:GridView ID="gvDoctor" runat="server" AlternatingRowStyle-CssClass="alt" AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found" GridLines="None" HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" OnRowDataBound="gvDoctor_RowDataBound" RowStyle-HorizontalAlign="Center" Width="70%">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" Text="." />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkListedDR" runat="server" Text="." />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-Width="">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text="<%#  ((GridViewRow)Container).RowIndex + 1 %>"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSf_Code" runat="server" Text='<%#Eval("Sf_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="FieldForce Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSf_Name" runat="server" Text='<%#Eval("Sf_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="HQ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSf_HQ" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Designation">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblsf_Designation" runat="server" Text='<%#Eval("sf_Designation_Short_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <br />
                                            <asp:Button ID="btnProcess" runat="server" CommandName="Process" CssClass="savebutton" Height="25px" OnClick="btnProcess_Click" Text="Process" Visible="false" Width="60px" />
                                       <%-- </caption>--%>
                                    </center>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
                <caption>
                    <br />
                    <br />
                </caption>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnGo" />
                <asp:PostBackTrigger ControlID="btnProcess" />
                <asp:AsyncPostBackTrigger ControlID="txtNew" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Work_Type_Setup_Security.aspx.cs" Inherits="MasterFiles_Options_Work_Type_Setup" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Work Type Setup</title>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
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
            //            $('input:text:first').focus();
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

                var type = $('#<%=ddltype.ClientID%> :selected').text();
                if (type == "--Select--") { alert("Please Select Type."); $('#ddltype').focus(); return false; }

            });
        });
    </script>
    <script type="text/javascript">
        function HidePopup() {
            var mpu = $find('txtButt_Acc_PopupControlExtender');
            mpu.hide();
        }
    </script>

    <script type="text/javascript">
        function HidePopup1() {

            var mpu = $find('txtDesig_PopupControlExtender');
            mpu.hide();
        }
    </script>

    <script type="text/javascript">
        function HidePopup2() {

            var mpu = $find('txtDes_PopupControlExtender');
            mpu.hide();
        }
    </script>
    <script type="text/javascript">
        function SelectAll(objRef) {
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

        function confirm_Save() {

            if (confirm('Do you want to Deactivate?')) {
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
    </script>


    <%-- <script type="text/javascript">
               function gvValidate() {

                   var f = document.getElementById("grdwrktype");
                   if (f != null) {
                       var TargetChildtxtWrkSName = "txtWrkSName";
                       var TargetChildtxtWrkOrder = "txtWrkOrder";
                       var TargetChildtxtButt_Acc = "txtButt_Acc";
                       var TargetChildtxtDesig = "txtDesig";                       

                       var Inputs = f.getElementsByTagName("input");
                       for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                           if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildtxtWrkSName, 0) >= 0) {
                               if (Inputs[i].value == "") {
                                   alert("Enter Short Name");
                                   f.getElementsByTagName("input").item(i).focus();
                                   return false;
                               }
                           }
                           if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildtxtWrkOrder, 0) >= 0) {
                               if (Inputs[i].value == "") {
                                   alert("Enter Order By");
                                   f.getElementsByTagName("input").item(i).focus();
                                   return false;
                               }
                           }

                           if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildtxtButt_Acc, 0) >= 0) {
                               if (Inputs[i].value == "") {
                                   alert("Select Button Provision in DCR");
                                   f.getElementsByTagName("input").item(i).focus();
                                   return false;
                               }
                           }
                           if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildtxtDesig, 0) >= 0) {
                               if (Inputs[i].value == "----Select----" || Inputs[i].value == "") {
                                   alert("Select Desigantion");
                                   f.getElementsByTagName("input").item(i).focus();
                                   return false;
                               }
                           }
                           
                       }

                   }

               }         
    </script>--%>

    <%-- <script type="text/javascript">
               function valid() {

                   var f = document.getElementById("grdwrktype");
                   if (f != null) {
                       var TargetChildtxtname = "name";
                       var TargetChildtxtshort = "short";
                       var TargetChildtxtorder = "order";
                       var TargetChildtxtbutt = "butt";
                       var TargetChildtxtDes = "txtDes";

                       var Inputs = f.getElementsByTagName("input");
                       for (var i = 0; i < f.getElementsByTagName("input").length; i++) {
                           if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildtxtname, 0) >= 0) {
                               if (Inputs[i].value == "") {
                                   alert("Enter WorkType Name");
                                   f.getElementsByTagName("input").item(i).focus();
                                   return false;
                               }
                           }
                           if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildtxtshort, 0) >= 0) {
                               if (Inputs[i].value == "") {
                                   alert("Enter Short Name");
                                   f.getElementsByTagName("input").item(i).focus();
                                   return false;
                               }
                           }

                           if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildtxtorder, 0) >= 0) {
                               if (Inputs[i].value == "") {
                                   alert("Enter Order By");
                                   f.getElementsByTagName("input").item(i).focus();
                                   return false;
                               }
                           }
                           if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildtxtbutt, 0) >= 0) {
                               if (Inputs[i].value == "") {
                                   alert("Enter Button Provision in DCR");
                                   f.getElementsByTagName("input").item(i).focus();
                                   return false;
                               }
                           }
                           if (Inputs[i].type == 'text' && Inputs[i].id.indexOf(TargetChildtxtDes, 0) >= 0) {
                               if (Inputs[i].value == "---Select---" || Inputs[i].value == "") {
                                   alert("Select Desigantion");
                                   f.getElementsByTagName("input").item(i).focus();
                                   return false;
                               }
                           }

                       }

                   }

               }

         
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <%-- <table width="80%">
            <tr>
                <td style="width: 9.2%" />
                <td>
                    <asp:Button ID="btnNew" runat="server" Width="60px" Height="25px" CssClass="savebutton"
                        Text="Add" />&nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>--%>


            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <%--<asp:UpdatePanel ID="updatepanel4" runat="server">
                                <ContentTemplate>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDivision" runat="server" CssClass="label">Division Name</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lbltype" runat="server" CssClass="label">Type</asp:Label>
                                <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true"
                                    TabIndex="2">
                                    <asp:ListItem Selected="True" Text="--Select--" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Base Level" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Manager" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                             <div class="single-des clearfix">
                                  <asp:CheckBox ID="Reactchk" runat="server" CssClass="label" Text="Reactive Work Types" />  
                                   </div>
                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnGo" runat="server" align="center" Text="Go" CssClass="savebutton"
                                    OnClick="btnGo_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-12">

                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdwrktype" runat="server" Width="100%" HorizontalAlign="Center" OnRowCreated="grdwrktype_RowCreated"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None" OnPageIndexChanging="grdwrktype_PageIndexChanging"
                                        ShowFooter="true" OnRowDataBound="grdwrktype_RowDataBound"
                                        CssClass="table" PagerStyle-CssClass="gridview1" OnSelectedIndexChanging="grdwrktype_SelectedIndexChanging">

                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="." onclick="SelectAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkWorktype" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="More">
                                                <FooterTemplate>
                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="savebutton" CommandName="Select" OnClientClick="return valid()" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdwrktype.PageIndex * grdwrktype.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="WorkType_Code_B" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWrkCode" runat="server" Text='<%#Eval("WorkType_Code_B")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Worktype Name " ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWrkName" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="name" runat="server" CssClass="input"></asp:TextBox>
                                                </FooterTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtWrkSName" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" CssClass="input" MaxLength="5" Text='<%# Bind("WType_SName") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="short" runat="server" CssClass="input" MaxLength="5"></asp:TextBox>
                                                </FooterTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Order By" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtWrkOrder" onkeypress="CheckNumeric(event);" runat="server" CssClass="input" MaxLength="4" Text='<%# Bind("WorkType_Orderly") %>'></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:TextBox ID="order" runat="server" SkinID="TxtBxAllowSymb" CssClass="input" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="WorkType Color in TP " Visible="false" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTp_Flag" runat="server"
                                                        DataSource="<%# FillTp_Flag() %>" DataTextField="TP_Flag" DataValueField="TP_Flag">
                                                    </asp:DropDownList>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlTpFlag" runat="server"
                                                        DataSource="<%# FillTp_Flag() %>" DataTextField="TP_Flag" DataValueField="TP_Flag">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Display in TP_DCR " ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTp_Dcr" runat="server">
                                                        <asp:ListItem Value="T">TP</asp:ListItem>
                                                        <asp:ListItem Value="D">DCR</asp:ListItem>
                                                        <asp:ListItem Value="T,D">TP,DCR</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlTpDcr" runat="server">
                                                        <asp:ListItem Value="T">TP</asp:ListItem>
                                                        <asp:ListItem Value="D">DCR</asp:ListItem>
                                                        <asp:ListItem Value="T,D">TP,DCR</asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Place Selection in TP " ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>

                                                    <asp:DropDownList ID="ddlPlace_inv" runat="server">
                                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlplace" runat="server">
                                                        <asp:ListItem Value="N">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Button Provision in DCR" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updatepanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtButt_Acc" runat="server" CssClass="input"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnButtId" runat="server"></asp:HiddenField>
                                                            <asp:PopupControlExtender ID="txtButt_Acc_PopupControlExtender" runat="server" Enabled="True"
                                                                ExtenderControlID="" TargetControlID="txtButt_Acc" PopupControlID="Panel2" OffsetY="22">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="Panel2" runat="server" Height="230px" BorderStyle="Solid"
                                                                BorderWidth="1px" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                                Style="display: none">
                                                                <div style="height: 15px; position: relative; background-color: #4682B4; text-transform: capitalize; width: 100%; float: left"
                                                                    align="right">
                                                                    <asp:Button ID="Button2" Style="font-family: Verdana; font-size: 7pt; font-weight: bold; width: 25px; background-color: #414d55; color: white; margin-top: -1px;"
                                                                        Text="X" runat="server" OnClick="btnClose_Click" OnClientClick="HidePopup();" />

                                                                </div>
                                                                <asp:CheckBoxList ID="chkButt_Acc" runat="server" CssClass="collp"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="chkButt_Acc_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Remarks" Value="R"></asp:ListItem>
                                                                    <asp:ListItem Text="Leave" Value="L"></asp:ListItem>
                                                                    <asp:ListItem Text="Listed Doctor" Value="D"></asp:ListItem>
                                                                    <asp:ListItem Text="Chemist" Value="C"></asp:ListItem>
                                                                    <asp:ListItem Text="Stockist" Value="S"></asp:ListItem>
                                                                    <asp:ListItem Text="Hospital" Value="H"></asp:ListItem>
                                                                    <asp:ListItem Text="Unlisted Doctor" Value="U"></asp:ListItem>
                                                                </asp:CheckBoxList>

                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:TextBox ID="butt" runat="server" Text="Remarks" CssClass="input">
                                        
                                                    </asp:TextBox>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="FieldWork Indicator" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddl_indicator" runat="server"
                                                        DataSource="<%# FillWork_indicator() %>" DataTextField="FieldWork_Indicator" DataValueField="FieldWork_Indicator">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddlindi" runat="server">
                                                        <asp:ListItem Value="N">N</asp:ListItem>
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="updatepanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtDesig" runat="server" Text="ALL" CssClass="input"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnDesigId" runat="server"></asp:HiddenField>
                                                            <asp:PopupControlExtender ID="txtDesig_PopupControlExtender" runat="server" Enabled="True"
                                                                ExtenderControlID="" TargetControlID="txtDesig" PopupControlID="Panel3" OffsetY="22">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="Panel3" runat="server" Height="230px" BorderStyle="Solid"
                                                                BorderWidth="1px" CssClass="collp" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                                Style="display: none">
                                                                <div style="height: 15px; position: relative; background-color: #4682B4; text-transform: capitalize; width: 100%; float: left"
                                                                    align="right">
                                                                    <asp:Button ID="Button3" Style="font-family: Verdana; font-size: 7pt; font-weight: bold; width: 25px; background-color: #414d55; color: White; margin-top: -1px;"
                                                                        Text="X" runat="server" OnClick="btnClose_Click" OnClientClick="HidePopup1();" />

                                                                </div>
                                                                <asp:CheckBoxList ID="ChkDesig" runat="server" BorderStyle="None" CssClass="collp"
                                                                    DataTextField="Designation_Short_Name" DataValueField="Designation_Code" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ChkDesig_SelectedIndexChanged" onclick="checkAll(this);">
                                                                </asp:CheckBoxList>
                                                                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [State_Code],[StateName] FROM [Mas_State]"></asp:SqlDataSource>--%>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>

                                                <FooterTemplate>

                                                    <asp:UpdatePanel ID="updatepanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtDes" runat="server" Text="ALL" CssClass="input"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnDesId" runat="server"></asp:HiddenField>
                                                            <asp:PopupControlExtender ID="txtDes_PopupControlExtender" runat="server" Enabled="True"
                                                                ExtenderControlID="" TargetControlID="txtDes" PopupControlID="Panel5" OffsetY="22">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="Panel5" runat="server" BorderStyle="Solid"
                                                                BorderWidth="1px" CssClass="collp" Direction="LeftToRight" ScrollBars="Auto" BackColor="#CCCCCC"
                                                                Style="display: none">
                                                                <div style="height: 15px; position: relative; background-color: #4682B4; text-transform: capitalize; width: 100%; float: left"
                                                                    align="right">
                                                                    <asp:Button ID="Button4" Style="font-family: Verdana; font-size: 7pt; font-weight: bold; width: 25px; background-color: #414d55; color: White; margin-top: -1px;"
                                                                        Text="X" runat="server" OnClick="btnClose_Click" OnClientClick="HidePopup2();" />

                                                                </div>
                                                                <asp:CheckBoxList ID="ChkDes" runat="server" BorderStyle="None" CssClass="collp"
                                                                    DataTextField="Designation_Short_Name" DataValueField="Designation_Code" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ChkDes_SelectedIndexChanged" onclick="checkAll(this);">
                                                                </asp:CheckBoxList>
                                                                <%--<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Ereportcon %>"
                                                        SelectCommand="SELECT [State_Code],[StateName] FROM [Mas_State]"></asp:SqlDataSource>--%>
                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </FooterTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                    <asp:GridView ID="Reactgrd" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found" GridLines="None" 
                                        ShowFooter="true" 
                                        CssClass="table" PagerStyle-CssClass="gridview1">

                                        <Columns>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="." onclick="SelectAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkWorktype" runat="server" Text="." />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                          <%--  <asp:TemplateField HeaderText="More">
                                                <FooterTemplate>
                                                    <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="savebutton" CommandName="Select" OnClientClick="return valid()" />
                                                </FooterTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdwrktype.PageIndex * grdwrktype.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="WorkType_Code_B" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWrkCode" runat="server" Text='<%#Eval("WorkType_Code_B")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Worktype Name " ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWrkName" runat="server" Text='<%#Eval("Worktype_Name_B")%>'></asp:Label>
                                                </ItemTemplate>
                                                <%--<FooterTemplate>
                                                    <asp:TextBox ID="name" runat="server" CssClass="input"></asp:TextBox>
                                                </FooterTemplate>--%>

                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btnUpdate" CssClass="savebutton" runat="server" Text="Update"
                                        OnClick="btnUpdate_Click" OnClientClick="return gvValidate()" />
                                    <asp:Button ID="btnDeactivate" runat="server" Text="De-Activate" CssClass="savebutton"
                                        OnClick="btnDeactivate_Click" OnClientClick="return confirm_Save();" />
                                </div>
                                 <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btnReactivate" CssClass="savebutton" runat="server" Text="Reactivate"
                                        OnClick="btnReactivate_Click" OnClientClick="return gvValidate()" />
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    </form>
</body>
</html>

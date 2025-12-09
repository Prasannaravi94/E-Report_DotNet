<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedDRCreation.aspx.cs" Inherits="MasterFiles_MR_ListedDRCreation" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu2" TagPrefix="ucl1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Creation</title>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>

    <%-- <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" />
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

        .normal {
            background-color: white;
        }

        .highlight_clr {
            /*background-color: LightBlue;*/
        }

        .clp {
            border-collapse: collapse;
            background-color: White;
        }

        .closeLoginPanel {
            font-family: Verdana, Helvetica, Arial, sans-serif;
            height: 14px;
            font-size: 11px;
            font-weight: bold;
            position: absolute;
            top: -2px;
            right: 1px;
        }

            .closeLoginPanel a {
                /*background-color: Yellow;*/
                cursor: pointer;
                color: Black;
                text-align: center;
                text-decoration: none;
                padding: 3px;
            }

        .width {
            min-width: 230px;
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
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript">
    </script>
    <script type="text/javascript">
        function ValidateEmptyValue() {

            var grid = document.getElementById('<%= grdListedDR.ClientID %>');

            if (grid != null) {

                var Inputs = grid.getElementsByTagName("input");
                var cnt = 0;
                var index = '';
                var isEntry = false;
                for (i = 2; i < Inputs.length; i++) {
                    if (Inputs[i].type == 'text') {
                        if (i.toString().length == 1) {

                            index = cnt.toString() + i.toString();
                        }
                        else {

                            index = i.toString();
                        }
                        var isEmpty = false;
                        var DoctorName = document.getElementById('grdListedDR_ctl' + index + '_ListedDR_Name');
                        var Address = document.getElementById('grdListedDR_ctl' + index + '_ListedDR_Address1');
                        var Category = document.getElementById('grdListedDR_ctl' + index + '_ddlCatg');
                        var Speciality = document.getElementById('grdListedDR_ctl' + index + '_ddlspcl');

                        var Territory = document.getElementById('grdListedDR_ctl' + index + '_ddlTerr');
                        if (DoctorName.value != '' && Address.value != '' && Category.value != '0' && Speciality.value != '0' && Territory.value != '0') {
                            isEntry = true;
                        }
                        if (DoctorName.value == '' && Address.value == '' && Category.value == '0' && Speciality.value == '0' && Territory.value == '0') {
                            isEmpty = true;
                        }
                        if ((isEntry == false) || (isEmpty == false)) {
                            if (DoctorName.value == '') {
                                alert('Enter Listed Doctor Name');
                                DoctorName.focus();
                                return false;
                            }
                            else if (Address.value == '') {
                                alert('Enter Address');
                                Address.focus();
                                return false;
                            }
                            else if (Category.value == '0') {
                                alert('Select Category');
                                Category.focus();
                                return false;
                            }
                            else if (Speciality.value == '0') {
                                alert('Select Speciality');
                                Speciality.focus();
                                return false;
                            }

                            else if (Territory.value == '0') {
                                alert('Select Territory');
                                Territory.focus();
                                return false;
                            }
                        }
                    }
                }

            }
            if (isEntry) {
                return true;
            }
        }
    </script>

    <style type="text/css">
        .AutoExtender {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
            margin-left: 10px;
            overflow-y: scroll;
            height: 200px;
        }

        .AutoExtenderList {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Black;
        }

        .AutoExtenderHighlight {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }

        #divwidth {
            width: 150px !important;
        }

            #divwidth div {
                width: 150px !important;
            }
			.display-table .table td {
    padding: 5px !important;
}
    </style>
    <script type="text/javascript">
        function HidePopup() {

            var popup = $find('txtTerritory_PopupControlExtender');
            popup.hide();
        }
    </script>
</head>
<body style="overflow-x:scroll">
    <form id="form1" runat="server" name="frmquickproduct_entry" method="post">

        <div>
            <div id="Divid" runat="server"></div>
            <%--<ucl:Menu ID="menu1" runat="server" />--%>
            <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>--%>

            <div class="home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Listed Doctor Creation</h2>
                        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Center">
                            <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                        </asp:Panel>
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-3">
                                <asp:Label ID="lblListed" runat="server" Text="No of Listed Drs - " CssClass="label"></asp:Label>
                                <asp:Label ID="lblDrcount" runat="server" ForeColor="Red" CssClass="label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label ID="lblappcnt" runat="server" Text="Listed Drs Approval Pending - " CssClass="label"></asp:Label>
                                <asp:Label ID="lblapp" runat="server" ForeColor="Red" CssClass="label" Font-Bold="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label ID="lblListeddr" runat="server" Text="Lst Drs Deactivation Pending - " CssClass="label"></asp:Label>
                                <asp:Label ID="lbldeact" runat="server" ForeColor="Red" Font-Bold="true" CssClass="label"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label ID="lblListeddr1" runat="server" Text="Add Against Deactivation Pending - " CssClass="label"></asp:Label>
                                <asp:Label ID="lbladddeact" runat="server" ForeColor="Red" Font-Bold="true" CssClass="label"></asp:Label>
                            </div>
                        </div>
                        <%--<asp:UpdatePanel ID="updatepanel1" runat="server">
                            <ContentTemplate>--%>
                        <div class="designation-reactivation-table-area clearfix">
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit ">
                                    <asp:GridView ID="grdListedDR" runat="server" Width="100%" HorizontalAlign="Center" OnRowCreated="grdListedDR_RowCreated"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" GridLines="None"
                                        OnRowDataBound="grdListedDR_RowDataBound" OnPageIndexChanging="grdListedDR_PageIndexChanging"
                                        CssClass="table" PagerStyle-CssClass="gridview1">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Initial">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIni" runat="server" Text="Dr."></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Doctor Name" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle Width="160px" />
                                                <ItemTemplate>
                                                    <asp:TextBox ID="ListedDR_Name" CssClass="input" Height="38px" onkeypress="AlphaNumeric_NoSpecialChars(event);" runat="server" Width="180px"
                                                        Text='<%#Eval("ListedDR_Name")%>'></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server"
                                                        ServiceMethod="AutoCompleteAjaxRequest"
                                                        ServicePath="~/MasterFiles/MR/ListedDoctor/Webservice/AutoComplete.asmx"
                                                        MinimumPrefixLength="1" CompletionInterval="100"
                                                        EnableCaching="false" CompletionSetCount="10"
                                                        TargetControlID="ListedDR_Name"
                                                        FirstRowSelected="false">
                                                    </asp:AutoCompleteExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="ListedDR_Address1" onkeypress="AlphaNumeric(event);" runat="server" CssClass="input" Height="38px" MaxLength="250"
                                                        Width="250px" Text='<%#Eval("ListedDR_Address1")%>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlCatg" runat="server" CssClass="nice-select" DataSource="<%# FillCategory() %>"
                                                        DataTextField="Doc_Cat_SName" DataValueField="Doc_Cat_Code">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Speciality" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlspcl" runat="server" CssClass="nice-select" DataSource="<%# FillSpeciality() %>"
                                                        DataTextField="Doc_Special_SName" DataValueField="Doc_Special_Code">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlQual" runat="server" CssClass="nice-select" DataSource="<%# FillQualification() %>"
                                                        DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlClass" runat="server" CssClass="nice-select" DataSource="<%# FillClass() %>"
                                                        DataTextField="Doc_ClsSName" DataValueField="Doc_ClsCode">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlType" runat="server" SkinID="ddlRequired">                                           
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="HQ" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="EX" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="OS" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="OS-EX" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>     --%>
                                            <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="width">

                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlTerr" Width="150px" runat="server" CssClass="nice-select" DataSource="<%# FillTerritory() %>"
                                                        DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%--<asp:UpdatePanel ID="updatepanel2" runat="server">
                                            <ContentTemplate>--%>
                                                    <asp:TextBox ID="txtTerritory" runat="server" CssClass="input" Height="38px" Width="200px"></asp:TextBox>
                                                    <asp:HiddenField ID="hdnTerritoryId" runat="server"></asp:HiddenField>
                                                    <asp:PopupControlExtender ID="txtTerritory_PopupControlExtender" runat="server" Enabled="True"
                                                        ExtenderControlID="" TargetControlID="txtTerritory" PopupControlID="Panel2" OffsetY="2" Position="Bottom">
                                                    </asp:PopupControlExtender>
                                                    <asp:Panel ID="Panel2" runat="server" BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none; border-radius: 8px; overflow-x: auto; width: 200px; height: 130px; scrollbar-width: thin">
                                                        <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                                            align="right">
                                                            <div class="closeLoginPanel">
                                                                <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                                    title="Close">X</a>
                                                            </div>
                                                        </div>
                                                        <asp:CheckBoxList ID="ChkTerritory" runat="server" Width="180px" CssClass="gridcheckbox"
                                                            DataTextField="Territory_Name" DataValueField="Territory_Code" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ChkTerritory_SelectedIndexChanged" onclick="checkAll(this);">
                                                        </asp:CheckBoxList>
                                                    </asp:Panel>
                                                    <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>

                        </div>
                        <%--                        </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <br />
                        <br />
                        <center>
                            <asp:Button ID="btnSave" CssClass="savebutton" runat="server" Text="Save" OnClick="btnSave_Click" OnClientClick="return ValidateEmptyValue()" />
                            <asp:Button ID="btnClear" CssClass="savebutton" runat="server" Text="Clear" OnClick="btnClear_Click" /></center>
                    </div>
                    <asp:Button ID="btnBack" CssClass="backbutton" PostBackUrl="~/MasterFiles/MR/ListedDoctor/LstDoctorList.aspx" Text="Back" runat="server" />
                </div>
            </div>
            <br />
            <br />
        </div>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>

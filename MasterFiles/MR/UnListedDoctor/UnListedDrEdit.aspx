<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UnListedDrEdit.aspx.cs" Inherits="MasterFiles_MR_UnListedDoctor_UnListedDrEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Edit - UnListed Doctor</title>
     <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" type="text/css" />
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
      <style type="text/css">
        .modal
        {
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
        .loading
        {
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
        [type="checkbox"]:disabled:checked + label:after
        {
            top: 0em;
            left: .2em;
        }
          /*.marRight
        {
            margin-right:35px;
        }*/
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
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="Divid" runat="server"></div>

        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center">
                <div class="col-lg-11">
                    <h2 class="text-center" style="border-style: none;">Bulk Edit - UnListed Doctor</h2>
                    <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" Style="text-align: center; font-size: 18px;" CssClass="marRight">
                        <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                    </asp:Panel>
                    <br />
                    <div class="row justify-content-center">
                        <table id="Table1" runat="server" width="90%">
                            <tr>
                                <td align="right" width="30%">
                                    <%-- <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2">
                                    <asp:Button ID="btnBack" CssClass="savebutton" Visible="false" Text="Back" runat="server"
                                        OnClick="btnBack_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="row justify-content-center" style="text-align: center;">
                        <asp:Label ID="lblTitle" runat="server" ForeColor="#696D6E" Text="Select the Fields to Edit" TabIndex="6"></asp:Label>
                    </div>
                    <div class="row justify-content-center" style="overflow-x: auto;">
                        <table border="0" cellpadding="3" cellspacing="3" id="tblLocationDtls" align="center">
                            <tr>
                                <td align="left">
                                    <asp:CheckBoxList ID="CblDoctorCode" CssClass="Checkbox" runat="server"
                                        RepeatColumns="6" RepeatDirection="Horizontal" Width="700px" Style="margin-top: 25px;">
                                        <asp:ListItem Value="Territory_Code">&nbsp;Territory</asp:ListItem>
                                        <asp:ListItem Value="Doc_Special_Code">&nbsp;Speciality</asp:ListItem>
                                        <asp:ListItem Value="Doc_Cat_Code">&nbsp;Category</asp:ListItem>
                                        <asp:ListItem Value="Doc_QuaCode">&nbsp;Qualification</asp:ListItem>
                                        <asp:ListItem Value="Doc_ClsCode">&nbsp;Class</asp:ListItem>
                                        <asp:ListItem Value="UnListedDR_Address1">&nbsp;Address</asp:ListItem>
                                        <asp:ListItem Value="UnListedDR_DOB">&nbsp;DOB</asp:ListItem>
                                        <asp:ListItem Value="UnListedDR_DOW">&nbsp;DOW</asp:ListItem>
                                        <asp:ListItem Value="No_of_Visit">&nbsp;No of Visit</asp:ListItem>
                                        <asp:ListItem Value="UnListedDR_Mobile">&nbsp;Mobile No</asp:ListItem>
                                        <asp:ListItem Value="UnListedDR_Phone">&nbsp;Telephone No</asp:ListItem>
                                        <asp:ListItem Value="UnListedDR_EMail">&nbsp;EMail ID</asp:ListItem>
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="row justify-content-center" style="text-align: center;">
                        <table>
                            <tr>
                                <%-- <td style="width: 15%" />--%>
                                <td>
                                    <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label>
                                    <asp:DropDownList ID="ddlSrch" runat="server" Width="125%" CssClass="nice-select" AutoPostBack="true"
                                        TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                        <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                                        <%--  <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                                        <asp:ListItem Text="Doctor Name" Value="7"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <div class="single-des clearfix" style="padding-top: 36px; padding-left: 0px;">
                                        <asp:TextBox ID="txtsearch" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        <asp:DropDownList ID="ddlSrc2" runat="server" AutoPostBack="true" Visible="false" OnSelectedIndexChanged="ddlSrc2_SelectedIndexChanged"
                                            CssClass="nice-select" TabIndex="4">
                                        </asp:DropDownList>
                                    </div>
                                </td>
                                <td>&nbsp;</td>
                                <td style="padding-top: 20px;">
                                    <asp:Button ID="btnOk" runat="server" CssClass="savebutton" Width="45px" Text="Go"
                                        OnClick="btnOk_Click" />
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <div class="single-des clearfix" style="padding-top: 36px; padding-left: 0px;">
                                        <asp:Button ID="btnClr" CssClass="resetbutton" runat="server" Width="60px" Text="Clear"
                                            OnClick="btnClr_Click" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <table runat="server" id="tblDoctor" visible="false" width="100%" align="center"><%-- style="margin-left: 200px"--%>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowDataBound="grdDoctor_RowDataBound"
                                                GridLines="None" CssClass="table" PagerStyle-CssClass="GridView1"
                                                AlternatingRowStyle-CssClass="alt">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doctor_Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDoctorCode" runat="server" Text='<%#Eval("UnListedDrCode")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doctor" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="160px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="ListedDr_Name" runat="server" CssClass="label" Width="160px" Text='<%# Bind("UnListedDr_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Territory_Code" runat="server" Width="140px" CssClass="nice-select" DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Speciality" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Doc_Special_Code" runat="server" CssClass="nice-select" DataSource="<%# FillSpeciality() %>" DataTextField="Doc_Special_Name" DataValueField="Doc_Special_Code">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Doc_Cat_Code" runat="server" CssClass="nice-select" DataSource="<%# FillCategory() %>" DataTextField="Doc_Cat_Name" DataValueField="Doc_Cat_Code">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Doc_QuaCode" CssClass="nice-select" runat="server" DataSource="<%# FillQualification() %>" DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="Doc_ClsCode" runat="server" CssClass="nice-select" DataSource="<%# FillClass() %>" DataTextField="Doc_ClsName" DataValueField="Doc_ClsCode">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="UnListedDR_Address1" CssClass="input" runat="server" Width="200px" Text='<%# Bind("UnListedDR_Address1") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DOB" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="UnListedDR_DOB" CssClass="input" onkeypress="Calendar_enter(event);" runat="server" Height="40px" MaxLength="12" Text='<%# Bind("UnListedDR_DOB") %>'></asp:TextBox>
                                                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="UnListedDR_DOB" CssClass= " cal_Theme1" Format="dd/MM/yyyy"/>--%>
                                                            <asp:CalendarExtender CssClass= "cal_Theme1"
                                                                ID="CalendarExtender1" Format="dd/MM/yyyy"
                                                                TargetControlID="UnListedDR_DOB"
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DOW" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="UnListedDR_DOW" onkeypress="Calendar_enter(event);" CssClass="input" runat="server" Height="40px" MaxLength="12" Text='<%# Bind("UnListedDR_DOW") %>'></asp:TextBox>
                                                            <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="UnListedDR_DOW" CssClass= " cal_Theme1" Format="dd/MM/yyyy"/>--%>
                                                            <asp:CalendarExtender CssClass= " cal_Theme1"
                                                                ID="CalendarExtender2" Format="dd/MM/yyyy"
                                                                TargetControlID="UnListedDR_DOW"
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Visit" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="No_of_Visit" CssClass="input" runat="server" MaxLength="5" Text='<%# Bind("No_of_Visit") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="UnListedDR_Mobile" CssClass="input" runat="server" MaxLength="12" Text='<%# Bind("UnListedDR_Mobile") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Telephone No" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="UnListedDR_Phone" SkinID="TxtBxAllowSymb" runat="server" MaxLength="12" Text='<%# Bind("UnListedDR_Phone") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EMail ID" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="UnListedDR_EMail" CssClass="input" runat="server" MaxLength="25" Text='<%# Bind("UnListedDR_EMail") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:Button ID="btnUpdate" CssClass="savebutton" runat="server" Text="Update" Visible="false"
                                                OnClick="btnUpdate_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="Button1" runat="server" CssClass="backbutton" Text="Back" OnClick="buttonBack_Click" />
            </div>
        </div>
        <br />
        </div>
    <div class="div_fixed">
         <asp:Button ID="btnSave" runat="server" Text="Update" Visible="false" CssClass="savebutton" 
            onclick="btnSave_Click" />
    </div>    
     <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>

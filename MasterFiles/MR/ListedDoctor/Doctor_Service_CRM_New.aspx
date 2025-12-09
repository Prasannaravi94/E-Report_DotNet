<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_New.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_New" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Doctor Service CRM</title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <link href="../../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet"
        type="text/css" />
    <link href="../../../JScript/ModelPopUpCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
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
        .gridview1
        {
            background-color: #336699;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }
        
        .gridview1 a
        {
            margin: auto 1%;
            border-style: none;
            border-radius: 50%;
            background-color: #444;
            padding: 5px 7px 5px 7px;
            color: #fff;
            text-decoration: none;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
        }
        .gridview1 a:hover
        {
            background-color: #1e8d12;
            color: #fff;
        }
        .gridview1 td
        {
            border-style: none;
        }
        .gridview1 span
        {
            background-color: #ae2676;
            color: #fff;
            -o-box-shadow: 1px 1px 1px #111;
            -moz-box-shadow: 1px 1px 1px #111;
            -webkit-box-shadow: 1px 1px 1px #111;
            box-shadow: 1px 1px 1px #111;
            border-radius: 50%;
            padding: 5px 7px 5px 7px;
        }
        .mGridImg1
        {
            width: 100%; /*background:url(menubg.gif) center center repeat-x;*/
            background: white;
        }
        .mGridImg1 td
        {
            padding: 2px;
            border-color: Black;
            background: F2F1ED;
            font-size: small;
            font-family: Calibri;
        }
        
        .mGridImg1 th
        {
            padding: 4px 2px;
            color: white;
            background: #336699;
            border-color: Black;
            border-left: solid 1px Black;
            border-right: solid 1px Black;
            border-top: solid 1px Black;
            border-bottom: solid 1px Black;
            font-weight: normal;
            font-size: small;
            font-family: Calibri;
        }
        .mGridImg1 .pgr
        {
            background: #336699;
        }
        .mGridImg1 .pgr table
        {
            margin: 5px 0;
        }
        .mGridImg1 .pgr td
        {
            border-width: 0;
            text-align: left;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: Red;
            line-height: 12px;
        }
        .mGridImg1 .pgr a
        {
            color: White;
            text-decoration: none;
        }
        .mGridImg1 .pgr a:hover
        {
            color: #000;
            text-decoration: none;
        }
    </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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


            $('#Btnsrc').click(function () {

                var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                var divi1 = $('#<%=ddlSrc2.ClientID%> :selected').text();
                if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc2').focus(); return false; }
                if ($("#txtsearch").val() == "") { alert("Enter Doctor Name."); $('#txtsearch').focus(); return false; }

            });
            $('#btnGo').click(function () {
                var st = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Field Force Name."); $('ddlFieldForce').focus(); return false; }

            });

        }); 
    </script>
    <link href="../../../JScript/Service_CRM/Crm_Dr_Css_Ob/Service_CRM_Entry_Css.css"
        rel="stylesheet" type="text/css" />
    <script src="../../../JScript/Service_CRM/Crm_Dr_JS/Service_CRM_Entry_JS.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        
        <center>
       
            <table border="0" cellpadding="3" cellspacing="3" align="center">
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFinanYear" runat="server" Text="Financial Year" SkinID="lblMand" 
                        Height="19px" Width="100px"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFinancial" runat="server" 
                        SkinID="ddlRequired" Width="150px">                       
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFieldForce" runat="server" Text="Field Force Name" SkinID="lblMand" Height="19px" Width="100px"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>
                </tr>  
                   <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblMode" runat="server" Text="Mode" SkinID="lblMand" Height="19px" Width="100px"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMode" runat="server" SkinID="ddlRequired" Width="150px">
                        <asp:ListItem Value="1" Text="Doctor"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Chemist/Pharmacy"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>  
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" 
                         Width="60px" Height="25px" onclick="btnGo_Click"  />
                          <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="savebutton" 
                         Width="60px" Height="25px" onclick="btnClear_Click"  />
                    </td>
                </tr>
            </table>

        </center>
        <br />
        <table width="100%" runat="server" id="tblSearch">
            <tr>
                <td style="width: 3.6%" />
                <td>
                    <asp:Label ID="lblType" runat="server" SkinID="lblMand" Text="Search By"></asp:Label>
                    <asp:DropDownList ID="ddlSrch" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                        TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                        <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Doctor Qualification" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Doctor Class" Value="5"></asp:ListItem>
                        <%--   <asp:ListItem Text="Doctor Territory" Value="6"></asp:ListItem>--%>
                        <asp:ListItem Text="Doctor Name" Value="7"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" CssClass="TEXTAREA"
                        Visible="false"></asp:TextBox>
                    <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" SkinID="ddlRequired"
                        TabIndex="4">
                    </asp:DropDownList>
                    <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="30px" Height="25px"
                        Text="Go" OnClick="Btnsrc_Click" Visible="false" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 50%">
                    <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                        runat="server" HorizontalAlign="Center" AlternatingItemStyle-ForeColor="Red"
                        Visible="false">
                        <SeparatorTemplate>
                        </SeparatorTemplate>
                        <ItemTemplate>
                            &nbsp
                            <asp:LinkButton ID="lnkbtnAlpha" ForeColor="Black" Font-Names="Calibri" Font-Size="14px"
                                runat="server" CommandArgument='<%#Eval("ListedDr_Name") %>' Text='<%#Eval("ListedDr_Name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>

           <table width="100%" runat="server" id="tblSr_Chemist">
            <tr>
                <td style="width: 3.6%" />
                <td>
                    <asp:Label ID="lblType_Chm" runat="server" SkinID="lblMand" Text="Search By"></asp:Label>
                    <asp:DropDownList ID="ddlSrch_Chm" runat="server" SkinID="ddlRequired" AutoPostBack="true"
                        TabIndex="1" OnSelectedIndexChanged="ddlSrch_Chm_SelectedIndexChanged">
                       <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                       <asp:ListItem Text="Chemist Name" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="txtsearch_Chm" runat="server" SkinID="MandTxtBox" CssClass="TEXTAREA"
                        Visible="false"></asp:TextBox>
                    <asp:DropDownList ID="ddlSrc2_Chm" runat="server" Visible="false" SkinID="ddlRequired"
                        TabIndex="4">
                    </asp:DropDownList>
                    <asp:Button ID="Btnsrc_Chm" runat="server" CssClass="savebutton" Width="30px" Height="25px"
                        Text="Go" OnClick="Btnsrc_Chm_Click" Visible="false" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="width: 50%">
                    <asp:DataList ID="dlAlpha_Chm" RepeatDirection="Horizontal"
                        runat="server" HorizontalAlign="Center" AlternatingItemStyle-ForeColor="Red"
                        Visible="false">
                        <SeparatorTemplate>
                        </SeparatorTemplate>
                        <ItemTemplate>
                            &nbsp
                            <asp:LinkButton ID="lnkbtnAlpha_Chm" ForeColor="Black" Font-Names="Calibri" Font-Size="14px"
                                runat="server" CommandArgument='<%#Eval("Chemists_Name") %>' Text='<%#Eval("Chemists_Name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </table>

        <br />
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="grdDoctor" runat="server" Width="85%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                            OnRowDataBound="grdDoctor_RowDataBound" PageSize="10" OnPageIndexChanging="grdDoctor_PageIndexChanging"
                            OnRowCreated="grdDoctor_RowCreated" GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr"
                            AlternatingRowStyle-CssClass="alt" AllowSorting="True" Visible="false">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="gridview1"></PagerStyle>
                            <%--<SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>--%>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdDoctor.PageIndex * grdDoctor.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Code" HeaderStyle-CssClass="ParamVisible"
                                    ItemStyle-CssClass="ParamVisible">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Category" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Speciality" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Qualification" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQl" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Class" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Territory" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Click Here To Make as Service">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                     <%--   <a  href="Doctor_Service_Entry_New.aspx?ListedDrCode=<%#Eval("ListedDrCode")%>&Mode=Doctor"
                                            onclick="return ShowProgress();" class="Service_CSS" style="color: DarkBlue;
                                            font-weight: bold; font-size: x-small; font-family: Verdana; text-align: center">
                                            Add Service </a>
--%>
                                      <asp:Label ID="lblimg" runat="server" Text="Add Service" Visible="false">                                        
                                        <img src="../../../Images/deact1.png" alt="" width="55px" title="Add Service" />
                                      </asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Click Hear To Edit/Enter Business">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <a id="hlDrEdit" href="Doctor_Service_CRM_Edit.aspx?ListedDrCode=<%#Eval("ListedDrCode")%>"
                                            onclick="return ShowProgress();" class="Service_CSS" style="color: DarkBlue;
                                            font-weight: bold; font-size: x-small; font-family: Verdana; text-align: center"
                                            title="Edit/Approve">Edit/Approve</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <%--    <asp:TemplateField HeaderText="Service Close">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <a href="Doctor_Service_CRM_ServiceClose.aspx?ListedDrCode=<%#Eval("ListedDrCode")%>"
                                            onclick="return ShowProgress();" class="Service_CSS" style="color: DarkBlue;
                                            font-weight: bold; font-size: x-small; font-family: Verdana; text-align: center">
                                            Click here</a>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                             <%--   <asp:TemplateField HeaderText="Click">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <a href="#" onclick="return ShowProgress();" class="Service_CSS" style="color: DarkBlue;
                                            font-weight: bold; font-size: x-small; font-family: Verdana; text-align: center">
                                            Approve</a>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Status" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Table ID="tblCloseService" runat="server">
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Approval Staus" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <a href="#" id="btnApproveLink">Click</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                    <td  colspan="2" align="center">

            <%--                     <asp:GridView ID="GridView1" runat="server" Width="85%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                            OnRowDataBound="grdDoctor_RowDataBound" PageSize="10" OnPageIndexChanging="grdDoctor_PageIndexChanging"
                            OnRowCreated="grdDoctor_RowCreated" GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr"
                            AlternatingRowStyle-CssClass="alt" AllowSorting="True" Visible="false">--%>

                    <asp:GridView ID="gvChemist" runat="server" Width="85%" HorizontalAlign="Center"
                            EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                            PageSize="10"  GridLines="None" CssClass="mGridImg" PagerStyle-CssClass="pgr"                            
                            AlternatingRowStyle-CssClass="alt" AllowSorting="True" Visible="false" OnRowCreated="gvChemist_RowCreated"
                            OnRowDataBound="gvChemist_RowDataBound" OnPageIndexChanging="gvChemist_PageIndexChanging">
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="gridview1"></PagerStyle>
                            <%--<SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>--%>
                            <Columns>
                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <%--  <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>--%>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  (gvChemist.PageIndex * gvChemist.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chemists Code" HeaderStyle-CssClass="ParamVisible"
                                    ItemStyle-CssClass="ParamVisible">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemistCode" runat="server" Text='<%#Eval("Chemists_Code")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Chemists Name" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblChemistName" runat="server" Text='<%#Eval("Chemists_Name")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Contact" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContact" runat="server" Text='<%# Bind("Chemists_Contact") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Address" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("Chemists_Address1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Phone" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Chemists_Phone") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                              
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Territory" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Click Here To Make as Service">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                      <%--  <a href="Doctor_Service_Entry_New.aspx?ChemistCode=<%#Eval("Chemists_Code")%>&Mode=Chemist"
                                            onclick="return ShowProgress();" class="Service_CSS" style="color: DarkBlue;
                                            font-weight: bold; font-size: x-small; font-family: Verdana; text-align: center">
                                            Add Service </a>--%>

                                              <asp:Label ID="lblimg" runat="server" Text="Add Service" Visible="false">                                        
                                        <img src="../../../Images/deact1.png" alt="" width="55px" title="Add Service" />
                                      </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Click Hear To Edit/Enter Business">
                                    <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                    </ControlStyle>
                                    <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <a id="hlDrEdit" href="Doctor_Service_CRM_Edit.aspx?ListedDrCode=<%#Eval("Chemists_Code")%>"
                                            onclick="return ShowProgress();" class="Service_CSS" style="color: DarkBlue;
                                            font-weight: bold; font-size: x-small; font-family: Verdana; text-align: center"
                                            title="Edit/Approve">Edit/Approve</a>
                                    </ItemTemplate>
                                </asp:TemplateField>                         
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Status" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <asp:Table ID="tblCloseService" runat="server">
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="Approval Staus" HeaderStyle-ForeColor="White">
                                    <ItemTemplate>
                                        <a href="#" id="btnApproveLink">Click</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
        <div>
            <div>
                <div style="float: right; margin-top: 30px">
                    <div id="output_Field_plus">
                    </div>
                    <div id="overlay_Field_plus" class="web_dialog_overlay">
                    </div>
                    <div id="dialog_Field_plus" class="web_dialog">
                        <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                            <tr>
                                <td class="web_dialog_title">
                                    Approval Status Detail
                                </td>
                                <td class="web_dialog_title align_right">
                                    <a href="#" id="btnClose_Field_plus">Close</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divDr">                                        
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="StatusDiv">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>

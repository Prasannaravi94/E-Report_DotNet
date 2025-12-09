<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BlockSFList.aspx.cs" Inherits="MasterFiles_BlockSFList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Blocked - Fieldforce List</title>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <style type="text/css">
        .blink_me {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 1s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 1s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        .blink {
            animation: blink-animation 1s steps(5, start) infinite;
            -webkit-animation: blink-animation 1s steps(5, start) infinite;
        }

        @keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }

        @-webkit-keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <br />
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td style="width: 4.2%" />
                        <td align="left" style="width: 50%">
                            <asp:Label ID="SearchBy" Font-Bold="true" runat="server" Text="SearchBy" ForeColor="Purple">
                            </asp:Label>
                            &nbsp;  
                           <asp:DropDownList ID="ddlFields" SkinID="ddlRequired" runat="server" CssClass="DropDownList"
                               AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                               <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                               <asp:ListItem Value="UsrDfd_UserName">User Name</asp:ListItem>
                               <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                               <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>
                               <asp:ListItem Value="sf_emp_id">Employee Id</asp:ListItem>
                               <%--<asp:ListItem Value="StateName">State</asp:ListItem>--%>
                               <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                           </asp:DropDownList>
                            <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'" Width="150px" CssClass="TEXTAREA"></asp:TextBox>
                            <asp:DropDownList ID="ddlSrc" runat="server" Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'" OnSelectedIndexChanged="ddlSrc_SelectedIndexChanged"
                                SkinID="ddlRequired" TabIndex="4">
                            </asp:DropDownList>
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Go"
                                Width="29px" Height="25px" CssClass="BUTTON" Visible="false"></asp:Button>
                        </td>

                    </tr>
                </tbody>
            </table>
            <br />
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <%--<td style="width: 20%" />--%>
                        <td colspan="2" align="center">
                            <%--<asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                runat="server" Width="20%" HorizontalAlign="center">
                                <SeparatorTemplate>
                                </SeparatorTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Names="Calibri" Font-Size="14px" ForeColor="#8A2EE6" CommandArgument='<%#bind("sf_name") %>'
                                        Text='<%#bind("sf_name") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>--%>
                        </td>
                    </tr>
                </tbody>
            </table>
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="90%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                AutoGenerateColumns="false" AllowPaging="True" PageSize="15" OnRowCommand="grdSalesForce_RowCommand"
                                OnPageIndexChanging="grdSalesForce_PageIndexChanging" GridLines="None" CssClass="mGrid"
                                PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                        <ControlStyle Width="50%" CssClass="TEXTAREA"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblHq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDes" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSt" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Reporting To" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrep" runat="server" Text='<%# Bind("Reporting_To_SF") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Reason for Block" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblReason" runat="server" Text='<%# Bind("sf_BlkReason") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Activate">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LnkAct" runat="server" CommandArgument='<%# Eval("SF_Code") %>'
                                        CommandName="Activate" OnClientClick="return confirm('Do you want to Activate the Saleforce');">Activate</asp:LinkButton> 
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                    <asp:HyperLinkField HeaderText="Activate" Text="Activate" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sfreason={1}"
                                        DataNavigateUrlFields="SF_Code,sf_BlkReason">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField HeaderText="Vacant" Text="To Vacant" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;Reporting_To_SF={1}"
                                        DataNavigateUrlFields="SF_Code,Reporting_To_SF">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                        <ItemStyle BorderStyle="Solid" ForeColor="DarkBlue" BorderWidth="1px"
                                            HorizontalAlign="Center" Font-Bold="False"></ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px"></HeaderStyle>
                                    </asp:HyperLinkField>
                                    <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;state={1}"
                                        DataNavigateUrlFields="SF_Code,StateName">
                                        <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                    </asp:HyperLinkField>

                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="GridView1" runat="server" Width="85%" HorizontalAlign="Center" OnRowDataBound="GridView1_Rowdatabound"
                                AutoGenerateColumns="false" PageSize="10" EmptyDataText="No Records Found"
                                GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="gridview1"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No">

                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name" Visible="false"
                                        HeaderStyle-ForeColor="white">

                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="300px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-ForeColor="white">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName2" runat="server" CssClass="blink_me" Text='<%# Bind("search") %>'></asp:Label></a>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="80px" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-ForeColor="white">

                                        <%--<ItemTemplate>
                                            <asp:Label ID="lblDesiName2" runat="server" CssClass="blink_me" Text='<%# Bind("search2") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HQ" HeaderStyle-ForeColor="white" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">

                                        <%--<ItemTemplate>
                                            <asp:Label ID="lblHQ2" runat="server" CssClass="blink_me" Text='<%# Bind("search3") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State" HeaderStyle-ForeColor="white" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                        <%--   <ItemTemplate>
                                            <asp:Label ID="lblstateName2" runat="server" CssClass="blink_me" Text='<%# Bind("search4") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                        
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activate" HeaderStyle-ForeColor="white" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblactivate" runat="server" CssClass="blink_me" Text='<%# Bind("search2") %>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Vacant" HeaderStyle-ForeColor="white" ItemStyle-Font-Bold="true" ItemStyle-Font-Size="15px" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblvacant" runat="server" CssClass="blink_me" Text='<%# Bind("search3") %>'></asp:Label>
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
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>

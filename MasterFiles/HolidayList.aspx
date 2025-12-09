<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HolidayList.aspx.cs" Inherits="MasterFiles_HolidayList" MaintainScrollPositionOnPostback="true"%>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday List</title>
    <link type="text/css" rel="stylesheet" href="../css/font-awesome.css" />
    <%--   <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <style type="text/css">
        .chkChoice input[type="checkbox"] {
            margin-right: 5px;
            margin-top: 10px;
        }

        .chkChoice td {
            padding-left: 5px;
        }

        .icon-invisible {
            visibility: hidden;
        }

        .alignment {
            min-width: 120px;
        }
    </style>
    <%--<script type="text/javascript">
            var EmptyDataText = "No Records Found"
            function ShowEmptyDataHeader() {
                var Grid = document.getElementById("<%=grdHoliday.ClientID%>");
                var cell = Grid.getElementsByTagName("TD")[0];
                if (cell != null && cell.innerHTML == EmptyDataText) {
                    document.getElementById("dvHeader").style.display = "block";
                }
            }
            window.onload = ShowEmptyDataHeader;
    </script>--%>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <link href="../assets/css/select2.min.css" rel="stylesheet" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Holiday List</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <table width="92%">
                                    <tr>
                                        <td align="right">
                                            <asp:LinkButton ID="lblbtnHtransfer" runat="server" SkinID="lblMand" CommandArgument="Show"
                                                OnClick="lblbtnHtransfer_Click"><i class="fa fa-circle icon-invisible"></i></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-12">
                                        <%--<asp:Button ID="btnNew" runat="server" CssClass="savebutton" Text="Add/Edit (Multi State)"
                                            Width="150px" Height="25px" OnClick="btnNew_Click" />&nbsp;
                                            <asp:Button ID="Button1" runat="server" CssClass="savebutton" Text="old" onClick="btnold_Click" />&nbsp;--%>
                                        <asp:Button ID="btnSingleNew" runat="server" CssClass="savebutton" Text="Add/Edit (Single State)"
                                            Width="160px" OnClick="btnSingleNew_Click" />
                                        <asp:Button ID="btnView" runat="server" CssClass="resetbutton" Text="Calendar View" Width="135px"
                                            OnClick="btnView_Click" />
                                        <asp:Button ID="btnCons" runat="server" CssClass="resetbutton" Text="Consolidated View"
                                            Width="135px" OnClick="btnCons_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblsr" runat="server" Text="Select the State" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass="custom-select2 nice-select"
                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-3">
                                        <asp:Label ID="lblYear" runat="server" Text="Year" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="nice-select"
                                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdHoliday" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="False" AllowPaging="True" OnRowUpdating="grdHoliday_RowUpdating"
                                        OnRowEditing="grdHoliday_RowEditing" OnRowDeleting="grdHoliday_RowDeleting" EmptyDataText="No Records Found"
                                        OnPageIndexChanging="grdHoliday_PageIndexChanging" OnRowCreated="grdHoliday_RowCreated"
                                        OnRowCancelingEdit="grdHoliday_RowCancelingEdit" GridLines="None" CssClass="table"
                                        PagerStyle-CssClass="gridview1" AllowSorting="True">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HSlno" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHSlno" runat="server" Text='<%#Eval("Sl_No")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="StateCode" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStateCode" runat="server" Text='<%#Eval("State_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Academic_Year" ItemStyle-HorizontalAlign="Center"
                                                HeaderText="Academic Year">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblYear" runat="server" Text='<%#Eval("Academic_Year")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="State_Name" HeaderText="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblState" runat="server" Text='<%#Eval("State_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Holiday_Name" HeaderText="Holiday Name"
                                                ItemStyle-HorizontalAlign="Center">
                                                <%-- <EditItemTemplate>
                                <asp:TextBox ID="txtHolidayeName"  SkinID="TxtBxAllowSymb"  runat="server"  Text='<%# Bind("Holiday_Name") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHolidayName" runat="server" Text='<%# Bind("Holiday_Name") %>'></asp:Label>
                                                    <asp:Label ID="lblHolidayNameSlNo" Text='<%# Bind("Holiday_Name_Sl_No") %>' runat="server"
                                                        Visible="false" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Month">
                                                <%-- <EditItemTemplate>
                                <asp:TextBox ID="txtHolidayeName"  SkinID="TxtBxAllowSymb"  runat="server"  Text='<%# Bind("Holiday_Name") %>'></asp:TextBox>
                            </EditItemTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmonth" runat="server" Text='<%# Bind("Month") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Holiday_Date" HeaderText="Holiday Date"
                                                ItemStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDate" runat="server" onkeypress="Calendar_enter(event);" CssClass="input"
                                                        Text='<%# Bind("Holiday_Date") %>'></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" Format="dd-MM-yyyy" TargetControlID="txtDate" CssClass="cal_Theme1"
                                                        runat="server" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDate" runat="server" Text='<%# Bind("Holiday_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Inline Edit" HeaderStyle-HorizontalAlign="Center" Visible="true">
                            <ControlStyle Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                            </ControlStyle>  
                            <ItemStyle HorizontalAlign="Center" />                          
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbutEdit" runat="server" Enabled="true" Text="Inline Edit">
                                </asp:LinkButton>
                            </ItemTemplate>                            
                        </asp:TemplateField>--%>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="alignment"
                                                HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True"></asp:CommandField>
                                            <%-- <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="HolidayFixation_old.aspx?Sl_No={0}"
                                DataNavigateUrlFields="Sl_No">
                                <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True">
                                </ControlStyle>
                                <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>
                            </asp:HyperLinkField>    --%>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="Center" Visible="true" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDel" runat="server" CommandArgument='<%# Eval("Sl_No") %>'
                                                        CommandName="Delete" OnClientClick="return confirm('Do you want to delete the Holiday');">Delete
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                    <asp:Label ID="lbl_Old_Date_Tmp" Text="" Visible="false" runat="server" />
                                </div>
                            </div>

                        </div>

                        <br />

                        <div class="row justify-content-center">
                            <div class="col-lg-12">
                                <div id="divHtransfer" runat="server" visible="false">
                                    <div style="text-align: center;">
                                        <asp:Label ID="lblHtransfer" runat="server" Text="Holiday Transfer - Division">
                                        </asp:Label>
                                    </div>
                                    <br />

                                    <div class="row">
                                        <div class="col-lg-1">
                                        </div>
                                        <div class="col-lg-5">
                                            <asp:Label ID="lbldivi" runat="server" Text="Division Name:" CssClass="label"></asp:Label>
                                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-lg-5" style="padding-top: 18px">
                                            <asp:DropDownList ID="ddlDDivision" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlDDivision_SelectedIndexChanged"
                                                AutoPostBack="true" Visible="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />

                                    <asp:UpdatePanel ID="upStateWiseHoliday" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-1">
                                                </div>
                                                <div class="col-lg-5">
                                                    <asp:Label ID="lblTState" runat="server" Text="State:"  Visible="false"
                                                        CssClass="label"></asp:Label>
                                                    <asp:CheckBoxList ID="cblTState"  runat="server" OnSelectedIndexChanged="cblTState_SelectedIndexChanged"
                                                        RepeatColumns="2" DataTextField="Ch_Name" DataValueField="Ch_Code" AutoPostBack="true">
                                                    </asp:CheckBoxList>
                                                    <%--OnSelectedIndexChanged="cblTState_SelectedIndexChanged"--%>
                                                </div>
                                                <div class="col-lg-5">
                                                    <asp:Label ID="lblDState" runat="server" Text="State:" Visible="false"
                                                        CssClass="label"></asp:Label>
                                                    <asp:CheckBoxList ID="cblDState"  runat="server" RepeatColumns="2"
                                                        DataTextField="statename" DataValueField="state_code" Enabled="false">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row justify-content-center">
                                                <asp:Button ID="btnTransfer" runat="server" CssClass="savebutton" Text="Transfer"
                                                    Visible="false" OnClick="btnTransfer_Click" OnClientClick="return ValidateEmptyValue()" />
                                                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="resetbutton" OnClick="btnClear_Click"
                                                    Visible="false" />
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
    <script type="text/javascript">
        function ValidateEmptyValue() {
            var group = $('#<%=cblTState.ClientID%> input:checked');
            var hasChecked = false;
            for (var i = 0; i < group.length; i++) {
                if (group[i].checked)
                    hasChecked = true;
                break;
            }
            if (hasChecked == false) {
                alert("Select atleast one State"); $('cblTState').focus(); return false;
            }
        }

        function blinker() {
            $('.blink_me').fadeOut(500);
            $('.blink_me').fadeIn(500);
        }
        setInterval(blinker, 1500);

    </script>
</body>
</html>

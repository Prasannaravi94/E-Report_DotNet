<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddAgainstDeactivatedDR.aspx.cs" Inherits="MasterFiles_MR_ListedDoctor_AddAgainstDeactivatedDR" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add against Deactivated Doctor</title>

    <%-- <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <%-- <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />--%>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
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

        .display-table .table tr:first-child th:first-child {
            background-color: #F1F5F8;
            color: #636d73;
            
            font-size: 14px;
        }
        .display-table .table1 tr:first-child th:first-child {
            background-color: #F1F5F8;
           
            
            font-size: 14px;
        }
        #grdListedDR tr:nth-child(2) td:first-child {
            background-color: white;
        }
        .table #grdListedDR_ctl02_ChkTerritory tr td:first-child
        {
             background-color: #f4f8fa;
        }

        #grdOrgDR tr td:first-child {
            background-color: white;
             color: #636d73;
        }

        .width {
            min-width: 150%;
        }
         .width1 {
            min-width: 130%;
        }
        .Ter-min-width {
            min-width: 250px;
        }

        .min-width {
            min-width: 200px;
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
        function btnback_click() {
            var url = "LstDoctorList.aspx"
            $(location).attr('href', url);

            return false;
        }
    </script>
    <script type="text/javascript">
        function HidePopup() {

            var popup = $find('txtTerritory_PopupControlExtender');
            popup.hide();
        }
    </script>
</head>
<body style="overflow:scroll">
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>


            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <h2 class="text-center">Add against Deactivated Doctor</h2>
                        <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="center">
                            <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                        </asp:Panel>
                        <br />
                        <br />
                        <div class="display-table clearfix">
                          <div class="table-responsive" style="scrollbar-width: thin;overflow-x: inherit;">
                                <asp:GridView ID="grdListedDR" runat="server" Width="100%" HorizontalAlign="Center"
                                    AutoGenerateColumns="false" OnRowDataBound="grdListedDR_RowDataBound"
                                    GridLines="None" CssClass="table">

                                    <Columns>

                                        <asp:TemplateField HeaderText="Listed Doctor Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="180px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ListedDR_Name" onkeypress="AlphaNumeric_NoSpecialChars(event);" Height="40px" CssClass="input" runat="server" Width="180px" Text='<%#Eval("ListedDR_Name")%>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDoc" runat="server" SetFocusOnError="true" ControlToValidate="ListedDR_Name" Display="Dynamic"
                                                    ErrorMessage="Required"></asp:RequiredFieldValidator>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ListedDR_Address1" runat="server" onkeypress="AlphaNumeric(event);" Height="40px" CssClass="input" MaxLength="200" Width="200px" Text='<%#Eval("ListedDR_Address1")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="min-width">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlCatg" runat="server" CssClass="nice-select" DataSource="<%# FillCategory() %>" DataTextField="Doc_Cat_SName" DataValueField="Doc_Cat_Code">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Speciality" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="min-width">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlspcl" runat="server" CssClass="nice-select" DataSource="<%# FillSpeciality() %>" DataTextField="Doc_Special_SName" DataValueField="Doc_Special_Code">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="min-width">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlQual" runat="server" CssClass="nice-select" DataSource="<%# FillQualification() %>" DataTextField="Doc_QuaName" DataValueField="Doc_QuaCode">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="min-width">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlClass" runat="server" CssClass="nice-select" DataSource="<%# FillClass() %>" DataTextField="Doc_ClsSName" DataValueField="Doc_ClsCode">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="Ter-min-width">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlTerr" runat="server" CssClass="nice-select" DataSource="<%# FillTerritory() %>" DataTextField="Territory_Name" DataValueField="Territory_Code">
                                                </asp:DropDownList>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>

                                                <asp:TextBox ID="txtTerritory" runat="server" CssClass="input" Height="40px" Width="200px"></asp:TextBox>
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

                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <br />
                                <br />
                                <asp:GridView ID="grdOrgDR" runat="server"
                                    HorizontalAlign="Center" ShowHeader="true"
                                    AutoGenerateColumns="false" GridLines="none" CssClass="table width1"
                                    BorderStyle="None">
                                    
                                    <Columns>
                                  <%--       <asp:TemplateField HeaderText="Listed Doctor Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="180px">
                                            <ItemTemplate> 
                                        <asp:Label ID="lblDrName" Text='<%#Eval("ListedDR_Name")%>'  runat="server" />
                                                  </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                        <asp:Label ID ="lblDrAddress" Text='<%#Eval("ListedDr_Address1")%>' runat="server"/>
                                          </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="min-width">
                                            <ItemTemplate>
                                        <asp:Label ID ="lblDocCatName" Text='<%#Eval("Doc_Cat_Name")%>' runat="server" />
                                          </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Speciality" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="min-width">
                                            <ItemTemplate>
                                        <asp:Label ID ="lblDocSpecName" Text='<%#Eval("Doc_Special_Name")%>' runat="server"/>
                                          </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Qualification" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="min-width">
                                            <ItemTemplate>
                                        <asp:Label ID ="lblDoc_QuaName"  Text='<%#Eval("Doc_QuaName")%>' runat="server" />
                                          </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Class" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="min-width">
                                                <ItemTemplate>
                                        <asp:Label ID ="lblDoc_ClsName"  Text='<%#Eval("Doc_ClsName")%>' runat="server" />
                                          </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Territory" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="150px" ItemStyle-CssClass="Ter-min-width">
                                            <ItemTemplate>
                                        <asp:Label ID="lblterritory_Name"  Text='<%#Eval("territory_Name")%>'   runat="server" />
                                          </ItemTemplate>
                                        </asp:TemplateField>--%>

                                         <asp:BoundField DataField="ListedDR_Name"  HeaderText="Listed Doctor Name" />
                                        <asp:BoundField DataField="ListedDr_Address1"  HeaderText="Address" />
                                        <asp:BoundField DataField="Doc_Cat_Name"  HeaderText="Category"/>
                                        <asp:BoundField DataField="Doc_Special_Name"  HeaderText="Speciality"  />
                                        <asp:BoundField DataField="Doc_QuaName" HeaderText="Qualification" />
                                        <asp:BoundField DataField="Doc_ClsName"  HeaderText="Class" />
                                        <asp:BoundField DataField="territory_Name" HeaderText="Territory" />

                                              
                                    </Columns>
                                </asp:GridView>

                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row justify-content-center">
                            <asp:Button ID="btnSave" CssClass="savebutton" runat="server" Text="Save"
                                OnClick="btnSave_Click" />

                            <asp:Button ID="btnClear" CssClass="resetbutton" runat="server" Text="Clear"
                                OnClick="btnClear_Click" />
                        </div>

                    </div>
                    <%-- <asp:Panel ID="pnlback" runat="server" HorizontalAlign="Right" Width="97%">--%>
                    <asp:Button ID="btnBack" CssClass="backbutton" OnClientClick="return btnback_click();" Text="Back" runat="server" />
                    <%-- </asp:Panel>--%>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="State_HO_View.aspx.cs" Inherits="MasterFiles_State_HO_View" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Statewise-Holiday-Fixation</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>


    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />

    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
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
    <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <style type="text/css">
        .txt:focus {
            border-color: #56aff1;
            background-color: #fff4d8;
        }

        .test tr input {
            margin-right: 10px;
            padding-right: 10px;
        }

        selectpicker > ul {
            display: none;
        }

        .selectpicker > span {
            display: none;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .bootstrap-select:not([class*="col-"]):not([class*="form-control"]):not(.input-group-btn) {
            width: 295px !important;
            color: #90a1ac;
            font-size: 14px !important;
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            padding-left: 3px;
            padding-right: 3px;
        }

        #desig .nice-select {
            display: none;
        }
    </style>
    <script type="text/javascript">
        function HidePopup() {

            var popup = $find('txtWeekoff_PopupControlExtender');
            popup.hide();
        }
    </script>
</head>
<body>
    <script type="text/javascript">
        function ValidateEmptyValue() {

            var flag = true;
            var dropdowns = new Array(); //Create array to hold all the dropdown lists.
            var gridview = document.getElementById('<%=grdSubHoID.ClientID%>'); //GridView1 is the id of ur gridview.
            dropdowns = gridview.getElementsByTagName('select'); //Get all dropdown lists contained in GridView1.
            for (var i = 0; i < dropdowns.length; i++) {
                //alert('4');

                if (dropdowns.item(i).value == '-Select-') //If dropdown has no selected value
                {

                    dropdowns.item(i).focus();
                    //                    var myDropDown = document.getElementById("DropDownList1");
                    //                    var length = myDropDown.options.length;
                    //                    myDropDown.size = length;   
                    //                    var x = document.getElementById("DropDownList1");
                    //                    x.size = x.options.length; 
                    //                  

                    flag = false;
                    break; //break the loop as there is no need to check further.

                }
            }
            if (!flag) {
                alert('Select Weekoff');
            }
            return flag;
        }

    </script>

    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <br />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <h2 class="text-center">Statewise-Weekoff-Fixation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading text-center clearfix">
                                <div class="d-inline-block division-name">Division Name</div>
                                <div class="d-inline-block align-middle">
                                    <div class="single-des-option">
                                        <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlselectindex_1">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                            </div>
                            <p>
                                <br />
                            </p>

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="min-height:250px;">
                                    <asp:GridView ID="grdSubHoID" DataKeyNames="State_Code" runat="server"
                                        HorizontalAlign="Center" AutoGenerateColumns="false"
                                        GridLines="None" CssClass="table"
                                        OnRowDataBound="grdSubHoID_RowDataBound">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="sf_code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#Eval("State_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="StateName" ItemStyle-Width="37%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Name" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ShortName" ItemStyle-Width="25%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_UsrName" runat="server" Text='<%# Bind("ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Weekoff">
                                                <ItemTemplate>

                                                    <%-- <asp:UpdatePanel ID="updatepanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:TextBox ID="txtWeekoff" runat="server" CssClass="input" Width="250px" Text='<%# Bind("Holiday_Mode") %>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdnStateId" runat="server"></asp:HiddenField>
                                                            <asp:PopupControlExtender ID="txtWeekoff_PopupControlExtender" runat="server" Enabled="True"
                                                                ExtenderControlID="" TargetControlID="txtWeekoff" PopupControlID="Panel1" OffsetY="2" Position="Bottom">
                                                            </asp:PopupControlExtender>
                                                            <asp:Panel ID="Panel1" runat="server" Height="230px" Width="250px"
                                                                BorderWidth="1px" BorderColor="#d1e2ea" Direction="LeftToRight" BackColor="#f4f8fa" Style="display: none; border-radius: 8px">

                                                                <div style="height: 17px; position: relative; text-transform: capitalize; width: 100%; float: left"
                                                                    align="right">
                                                                    <div class="closeLoginPanel">
                                                                        <a onclick="Sys.Extended.UI.PopupControlBehavior.__VisiblePopup.hidePopup();return false;"
                                                                            title="Close">X</a>
                                                                    </div>
                                                                </div>
                                                                <asp:CheckBoxList ID="ChkWeekoff" runat="server"
                                                                    AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ChkWeekoff_SelectedIndexChanged" onclick="checkAll(this);" CssClass="gridcheckbox">
                                                                    <asp:ListItem>ALL</asp:ListItem>
                                                                    <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                                                                </asp:CheckBoxList>

                                                            </asp:Panel>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>--%>

                                                    <asp:Label ID="lblwkof" runat="server" Text='<%# Bind("Holiday_Mode") %>' Visible="false"></asp:Label>
                                                    <div id="desig">
                                                        <asp:ListBox ID="ListBox1" runat="server" class="btn btn-secondary dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" SelectionMode="Multiple" data-placeholder="--select--" CssClass="selectpicker" Width="150px" Height="200px">
                                                            <asp:ListItem Text="Sunday" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Monday" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Tuesday" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Wednesday" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="Thursday" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="Friday" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="Saturday" Value="6"></asp:ListItem>
                                                        </asp:ListBox>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>

                                    <br />
                                    <div class="no-result-area" id="divid" runat="server" visible="false">
                                        No Records Found
                                    </div>

                                </div>
                            </div>

                            <br />
                            <div align="center">
                                <asp:Button ID="But_update" runat="server" Text="Update" OnClientClick="return ValidateEmptyValue()" CssClass="savebutton"
                                    OnClick="But_update_Click" />
                            </div>


                        </div>
                    </div>
                </div>

            </div>

            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
            <br />
            <br />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/js/bootstrap-select.min.js"></script>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TransferTerritory.aspx.cs" Inherits="MasterFiles_MR_Territory_TransferTerritory" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Territory</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />  --%>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />--%>
    <%--<link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />--%>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" type="text/css" />
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
        /*.marRight
        {
            margin-right:35px;
        }*/
        #grdTerritory [type="checkbox"]:not(:checked) + label, #grdTerritory [type="checkbox"]:checked + label {
            padding-left: 0;
            color: white;
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
        function ValidateCheckBoxList() {
            var gridView = document.getElementById("<%=grdTerritory.ClientID %>");
            var checkBoxes = gridView.getElementsByTagName("input");
            for (var i = 0; i < checkBoxes.length; i++) {
                if (checkBoxes[i].type == "checkbox" && checkBoxes[i].checked) {
                    var flag = true;
                    var dropdowns = new Array();
                    dropdowns = gridView.getElementsByTagName('select');
                    if (dropdowns.item(i).value == "-1") {
                        flag = false;
                    }

                    if (!flag) {
                        alert("Select transfer to");
                    }
                    return flag;
                }
            }
            alert("Select transfer from");
            return false;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div></div>
        <div id="Divid" runat="server">
        </div>
        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Transfer Territory</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <%-- <ucl:Menu ID="menu1" runat="server" /> --%>
                        <asp:Panel ID="pnlsf" runat="server" Style="text-align: center; font-size: 18px;" HorizontalAlign="Right" CssClass="marRight">
                            <asp:Label ID="lblTerrritory" runat="server" Visible="true" Font-Names="Tahoma"></asp:Label>
                        </asp:Panel>
                        <br />
                        <br />
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                <table align="center" width="100%">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:GridView ID="grdTerritory" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                                AutoGenerateColumns="false" OnRowDataBound="grdTerritory_RowDataBound" ShowFooter="true"
                                                GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkTerritory" Width="20px" runat="server" Text="." />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderStyle-Width="290px" HeaderText="Transfer From" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTerritory_Name" runat="server" Text='<%# Bind("Territory_Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotal" runat="server" Text="Total Count"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Listed Doctors" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="140px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblListedDRCnt" runat="server" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalqty" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Chemists" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="140px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblChemistsCnt" runat="server" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalChemists_Count" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of UnListed Doctors" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="140px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnListedDRCnt" runat="server" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalUnListedDR_Count" runat="server" />
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No of Hospitals" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle Width="140px" />
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblHospitalCnt" runat="server" Text='<%# Bind("Hospital_Count") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalHospital_Count" runat="server"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transfer To" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="125px">
                                                        <%--                                                        <FooterStyle BackColor="White" ForeColor="Red" Font-Bold="true" />--%>
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlTerritory" CssClass="nice-select" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="no-result-area" />
                                                <FooterStyle Font-Bold="True" ForeColor="Black" HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </td>
                                    </tr>

                                </table>
                                <br />
                                <center>
                                    <asp:Button ID="btnTransfer" runat="server" Text="Transfer" CssClass="savebutton" Width="90px"
                                        OnClick="btnTransfer_Click" OnClientClick="return ValidateCheckBoxList()" />
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
            </div>
        </div>
        <br />
        <br />
        <%--<table width="90%">
      
        <tr> 
          <td align="right" width="30%">--%>
        <%--  <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
        <%-- </td>
                </tr>
                <tr>
                <td align="right">
                    <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" 
                    onclick="btnBack_Click" />
                    </td>                    
     </tr>
     </table>--%>

        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>

    </form>
</body>
</html>

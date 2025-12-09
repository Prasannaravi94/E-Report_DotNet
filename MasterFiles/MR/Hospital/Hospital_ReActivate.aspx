<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Hospital_ReActivate.aspx.cs" Inherits="MasterFiles_MR_Hospital_Hospital_ReActivate" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hospital ReActivate</title>
     <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />--%>
    <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" type="text/css" />  
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type = "text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        //row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original 
                        //                    if (row.rowIndex % 2 == 0) {
                        //                        //Alternating Row Color
                        //                        row.style.backgroundColor = "#C2D69B";
                        //                    }
                        //                    else {
                        //                        row.style.backgroundColor = "white";
                        //                    }
                        inputList[i].checked = false;
                    }
                }
            }
        }
</script>  
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
         #grdHospital [type="checkbox"]:not(:checked) + label, #grdHospital [type="checkbox"]:checked + label
          {            
            padding-left: 0.05em;   
            color:white;     
          }
           .display-table .table th:nth-child(2) {
              padding: 20px 20px;
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
    <%-- <ucl:Menu ID="menu1" runat="server" /> --%>
        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Hospital ReActivate</h2>
                    <asp:Panel ID="pnlsf" runat="server" HorizontalAlign="Right" Style="text-align: center; font-size: 18px;" CssClass="marRight">
                        <asp:Label ID="lblTerrritory" runat="server" Visible="true"></asp:Label>
                    </asp:Panel>
                    <br />
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <table id="Table1" runat="server" width="90%">
                                    <tr>
                                        <td align="right" width="30%">
                                            <%-- <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server"
                                                OnClick="btnBack_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" align="center">
                                    <tbody>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:GridView ID="grdHospital" runat="server" Width="90%" HorizontalAlign="Center"
                                                    AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowDataBound="grdHospital_RowDataBound"
                                                    GridLines="None" CssClass="table" AlternatingRowStyle-CssClass="alt"
                                                    AllowSorting="True" OnSorting="grdHospital_Sorting">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkAll" runat="server" Text="." onclick="checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkHospital" Text="." runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hospital Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHospitalCode" runat="server" Text='<%#Eval("Hospital_Code")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Hospital_Name" HeaderText="Hospital Name" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHospitalName" runat="server" Text='<%#Eval("Hospital_Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Hospital_Contact" HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHospitalContact" runat="server" Text='<%#Eval("Hospital_Contact")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="territory_Name" HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area"/>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </tbody>
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
         <asp:Button ID="btnSave" runat="server" Text="Re-Activate" CssClass="savebutton" onclick="btnSave_Click" Visible="false" />
    </div>    
      <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>

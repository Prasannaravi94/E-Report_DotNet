<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecialityWise_Qualification.aspx.cs" Inherits="MasterFiles_SpecialityWise_Qualification" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Speciality Wise-Qualification Transfer</title>
    <%--<link type="text/css" rel="stylesheet" href="../../../css/style.css" />--%>
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
       .table [type="checkbox"]:not(:checked) + label, .table [type="checkbox"]:checked + label {
            padding-left: 0.00em;
            color:white;
        }
    </style>
    <script type="text/javascript">
        function checkAll(objRef) {
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
</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>

        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <h2 class="text-center" id="heading" runat="server"></h2>

                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="lblhead" runat="server" Text="Select the Speciality" CssClass="label"></asp:Label>
                            <asp:DropDownList ID="ddlspecial" runat="server" CssClass="nice-select" ></asp:DropDownList>
                        </div>
                    </div>

                    <div class="w-100 designation-submit-button text-center clearfix">
                        <asp:Button ID="btngo" runat="server" Text="GO"  CssClass="savebutton" OnClick="btngo_click" />
                    </div>
                </div>
            </div>
            <br />
            <div id="tbl1" runat="server" visible="false" width="100%">
                <div class="row justify-content-center">
                    <div class="col-lg-8">
                        <div class="display-table clearfix">
                            <div class="table-responsive">
                                <asp:GridView ID="grdQual" runat="server" AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">

                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdQual.PageIndex * grdQual.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" Text="Transfer" />
                                            </HeaderTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkAllspecial" runat="server" onclick="checkAll(this);" Text="." />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkspecial" runat="server" Text="." />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qualification_code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblqualcode" runat="server" Text='<%#Eval("Doc_QuaCode") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qualification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblqual" runat="server" Text='<%#Eval("Doc_QuaName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No.of Drs Available">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldrcnt" runat="server" Text='<%#Eval("cnt") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">

                                <asp:Label ID="lbltospecial" runat="server" Text="To Speciality" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddltospclty" runat="server" Width="100px" CssClass="nice-select"></asp:DropDownList>
                            </div>
                        </div>

                        <center>
                <asp:Button ID="btnupdate" runat="server" Text="Update" OnClick="btnupdate_Click" Visible="false" Width="100px"
                    OnClientClick="return confirm('Do you want to Update?') &&  confirm('Are you sure want to Update?');" CssClass="savebutton" />
           </center>
                    </div>
                </div>

            </div>

            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="backbutton" OnClick="btnBack_Click" />
        </div>
        <br />
        <br />
    </form>
</body>
</html>

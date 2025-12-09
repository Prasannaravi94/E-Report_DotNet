<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Convert_Unlistto_Listeddr.aspx.cs"
    Inherits="MasterFiles_MGR_Convert_Unlistto_Listeddr" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unlisted Drs Convert to Listed Drs</title>
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
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
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblSF" runat="server" CssClass="label">Field Force Name</asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" AutoPostBack="true">
                                </asp:DropDownList>&nbsp;
                                 <asp:DropDownList ID="ddlSF" runat="server" CssClass="custom-select2 nice-select" Visible="false">
                                 </asp:DropDownList>
                            </div>
                            <div class="w-100 designation-submit-button text-center clearfix">
                                <asp:Button ID="btnGo" runat="server" Text="Go" OnClick="btnGo_Click" CssClass="savebutton" />
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="grdDoctor" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                        AutoGenerateColumns="false" GridLines="None" CssClass="table" OnRowDataBound="grdDoctor_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="&nbsp; &nbsp; Select All" onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkListedDR" Text="." runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UnListed Doctor Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("UnListedDrCode")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UnListed Doctor Name" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("UnListedDr_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qualification" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQual" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("UnListedDr_Address1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpeciality" runat="server" Text='<%# Bind("Doc_Special_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Doc_Cat_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_ClsSName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTerritory" runat="server" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btnConvert" runat="server" Width="200px" Text="Convert to Listed Doctor" OnClientClick="return confirm('Do you want to Convert the Doctor(s)?') &&  confirm('Are you sure want to Convert?');" Visible="false"
                                        CssClass="savebutton" OnClick="btnConvert_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

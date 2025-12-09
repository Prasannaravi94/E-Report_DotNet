<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HQ_Interchage.aspx.cs" Inherits="MasterFiles_HQ_Interchage" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Interchange Drs/Chemists/Hospitals</title>

    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />

    <style type="text/css">
        table.gridtable {
            font-family: verdana,arial,sans-serif;
            font-size: 11px;
            color: #333333;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
        }

            table.gridtable th {
                padding: 5px;
            }

            table.gridtable td {
                border-width: 1px;
                padding: 5px;
                border-style: solid;
                border-color: #666666;
            }
    </style>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
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

        .table [type="checkbox"]:checked + label {
            padding-left: 1em;
        }
		.display-table .table td {
    padding: 15px 0px !important;}
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

     <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />

    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>

    <link href="../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
        </div>


        <div class="container home-section-main-body position-relative clearfix">
            <br />
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Interchange Drs/Chemists/Hospitals</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-name-heading clearfix">
                            <div class="row  justify-content-center clearfix">

                                <div class="col-lg-5">
                                    <div class="designation-area clearfix">
                                        <div class="single-des clearfix">
                                            <asp:Label ID="lblFF" runat="server" CssClass="label" Text="From Fieldforce"></asp:Label>
                                            <%--<asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" 
                            OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                        </asp:DropDownList>--%>
                                            <asp:DropDownList ID="ddlFromFieldForce" runat="server" AutoPostBack="true" CssClass="custom-select2 nice-select"
                                                OnSelectedIndexChanged="ddlFromFieldForce_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="w-100 designation-submit-button text-center clearfix">
                                            <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" Width="15%" OnClick="btnGo_Click" />
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="display-table clearfix">
                                        <div class="table-responsive" style="scrollbar-width: thin;">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdTerritory" runat="server" Width="50%" HorizontalAlign="Center"
                                                        EmptyDataText="No Records Found" AutoGenerateColumns="false"
                                                        GridLines="None" CssClass="table">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdTerritory.PageIndex * grdTerritory.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="450px" HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTerritory_Name" runat="server" Text='<%# Bind("Territory_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of List Dr" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="140px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblListedDRCnt" runat="server" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of Chem" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="140px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblChemistsCnt" runat="server" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of UnList Dr" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="140px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnListedDRCnt" runat="server" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of Hsptal" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="140px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHospitalCnt" runat="server" Text='<%# Bind("Hospital_Count") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <%--    <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" AutoPostBack="true"
                                                            Text="Select" />--%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkTerritory" runat="server" Checked="true" AutoPostBack="true" Text="." />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-1 text-center" style="margin-top: 20%; padding-left: 2px;">
                                    <asp:Panel ID="pnlmove" runat="server" Visible="false">
                                        <%--<img src="../Images/move.gif" />--%>
                                        <img src="../Images/in.gif" />
                                    </asp:Panel>

                                    <asp:Label ID="lblinterchange" Text="Interchange" Font-Size="Medium" Font-Bold="true" ForeColor="Red" Visible="false" runat="server"></asp:Label>

                                    <asp:Panel ID="pnlmove1" runat="server" Visible="false">
                                        <%--<img src="../Images/move1.gif" />--%>
                                        <img src="../Images/in1.gif" />
                                    </asp:Panel>
                                </div>
                                <div class="col-lg-5">
                                    <div class="designation-area clearfix">
                                        <div class="single-des clearfix">
                                            <asp:Label ID="lbltoFF" runat="server" CssClass="label" Text="To Fieldforce"></asp:Label>
                                            <asp:DropDownList ID="ddlToFieldForce" runat="server" AutoPostBack="true" CssClass="custom-select2 nice-select"
                                                OnSelectedIndexChanged="ddlToFieldForce_SelectedIndexChanged">
                                                <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    
                                        <div class="w-100 designation-submit-button text-center clearfix">
                                            <asp:Button ID="btnGo1" runat="server" Width="15%" Text="Go" CssClass="savebutton" OnClick="btnGo1_Click" />

                                        </div>
                                        <div class="row">
                                            <div class="col-lg-5 justify-content-center">
                                            <asp:CheckBox ID="chkvacant" runat="server" AutoPostBack="true"
                                                Text="Only Vacant Id's" OnCheckedChanged="chkvacant_CheckedChanged" />
                                                </div>
                                        </div>

                                    </div>
                                    <br />
                                    <div class="display-table clearfix">
                                        <div class="table-responsive" style="scrollbar-width: thin;">

                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grdterritory1" runat="server" Width="50%" HorizontalAlign="Center"
                                                        EmptyDataText="No Records Found" AutoGenerateColumns="false"
                                                        GridLines="None" CssClass="table">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdterritory1.PageIndex * grdterritory1.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <%--     <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" AutoPostBack="true"
                                                            Text="Select" />--%>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkListedDR" runat="server" Checked="true" AutoPostBack="true" Text="." />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderStyle-Width="450px" HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTerritory_Name" runat="server" Text='<%# Bind("Territory_Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of Listed Doctors" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="140px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblListedDRCnt" runat="server" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of Chemists" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="140px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblChemistsCnt" runat="server" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of UnListed Doctors" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="140px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnListedDRCnt" runat="server" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="No of Hospitals" ItemStyle-HorizontalAlign="Center">
                                                                <ItemStyle Width="140px" />
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHospitalCnt" runat="server" Text='<%# Bind("Hospital_Count") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>



                    </div>
                </div>
                <asp:Button ID="btnback" runat="server" Text="Back" CssClass="backbutton" OnClick="btnback_Click" />
            </div>
            <br />
            <br />
            <div class="row justify-content-center">
                <asp:Button ID="btnTransfer" runat="server" Text="Interchange" Visible="false" CssClass="savebutton"
                    OnClick="btnTransfer_Click" />
                &nbsp;&nbsp;
        <asp:Button ID="btnRep" runat="server" Text="Replicate" CssClass="savebutton" Visible="false" OnClick="btnRep_Click" />

                &nbsp;&nbsp;
                <asp:Panel runat="server" HorizontalAlign="Right">
                    <asp:Button ID="btnclear" Text="Clear" runat="server" CssClass="resetbutton" OnClick="btnclear_Click" />
                </asp:Panel>
            </div>
        </div>

        <%--<img src="../Images/move1.gif" />--%>

        <%--   <td>
                    <table>
                        <tr>
                            <td style="border: none">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="border: none" valign="middle">
                                <img src="../../Images/arr (1).gif" alt="" style="vertical-align: middle" />
                            </td>
                        </tr>
                    </table>
                </td>--%>
        <br />
        <br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
        
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

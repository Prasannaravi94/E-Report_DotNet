<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Distance_Fixation.aspx.cs"
    Inherits="MasterFiles_Distance_Fixation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SFC Updation</title>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>

    <script type="text/javascript">
        function GridViewRepeatColumns(grdHQ, repeatColumns) {
            //Created By: Brij Mohan(http://techbrij.com)
            GridViewRepeatColumns("<%=grdHQ.ClientID %>", 2);
            if (repeatColumns < 2) {
                alert('Invalid repeatColumns value');
                return;
            }
            var $gridview = $('#' + grdHQ);
            var $newTable = $('<table></table>');

            //Append first row in table
            var $firstRow = $gridview.find('tr:eq(0)'),
            firstRowHTML = $firstRow.html(),
            colLength = $firstRow.children().length;

            $newTable.append($firstRow);

            //Append first row cells n times
            for (var i = 0; i < repeatColumns - 1; i++) {
                $newTable.find('tr:eq(0)').append(firstRowHTML);
            }

            while ($gridview.find('tr').length > 0) {
                var $gridRow = $gridview.find('tr:eq(0)');
                $newTable.append($gridRow);
                for (var i = 0; i < repeatColumns - 1; i++) {
                    if ($gridview.find('tr').length > 0) {
                        $gridRow.append($gridview.find('tr:eq(0)').html());
                        $gridview.find('tr:eq(0)').remove();
                    }
                    else {
                        for (var j = 0; j < colLength; j++) {
                            $gridRow.append('<td></td>');
                        }
                    }
                }
            }
            //update existing GridView
            $gridview.html($newTable.html());
        }

    </script>

    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>

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
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">SFC Updation</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="display-name-heading clearfix">
                            <div class="row  justify-content-center clearfix">
                                <div class="col-lg-5">
                                    <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="false" CssClass="custom-select2 nice-select" Width="100%">
                                        <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-1" style="padding-top: 18px">
                                    <asp:Button ID="btnGo" runat="server" Width="50px" Text="Go" CssClass="savebutton"
                                        OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnclear" runat="server" CssClass="resetbutton" Width="50px" Text="Clear" OnClick="btnClear_Click" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-7">
                                <asp:Label ID="lblFieldName" runat="server" Font-Size="15px"
                                    Visible="true"></asp:Label>
                            </div>
                            <div class="col-lg-3">
                                <asp:Label ID="lblTerrritory" runat="server" Font-Size="15px"
                                    Visible="true"></asp:Label>
                            </div>
                        </div>
                        <br />
                        <div class="row justify-content-center">
                            <asp:Label ID="lblSelect" Text="Please Select the Field Force Name" runat="server"
                                ForeColor="Red" Font-Size="Large"></asp:Label>
                        </div>

                        <div class="display-table clearfix " style="padding-top: 10px">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:Panel ID="pnlDist" runat="server" Visible="false">
                                    <table width="100%" align="center">
                                        <tr>
                                            <td align="center" style="padding-bottom: 20px">
                                                <asp:Label ID="lblHQSta" runat="server" Text="Head Quarter" CssClass="h2" Font-Size="15px"></asp:Label>
                                                <span style="color: #0077FF; font-size: 15px;" class="h2">(No Fare Only Allowance)</span>
                                            </td>
                                            <%-- <td><asp:Label ID="lblFare" runat="server" Text="No Fare Only Allowance" ForeColor="Red"></asp:Label></td>--%>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="grdHQ" runat="server" Width="80%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                    OnRowDataBound="grdHQ_RowDataBound"
                                                    GridLines="None" BorderStyle="Solid" CssClass="table" EmptyDataText="No Head Quarter Found"
                                                    PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Territory" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHQDoctor" runat="server" Text='<%# Eval("Territory_Name")%>'></asp:Label>
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("ListedDR_Count")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="padding-bottom: 20px;">
                                                <asp:Label ID="Label2" runat="server" Text="EX Station" CssClass="h2" Font-Size="15px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="grdEX" runat="server" Width="80%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                    GridLines="None" BorderStyle="Solid" CssClass="table" EmptyDataText="No EX Station Found"
                                                    PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFromEX" runat="server" Text='<%#Eval("Sf_HQ")%>'>
                                                                </asp:Label>
                                                                <asp:HiddenField ID="hdnFrmTerrCode" runat="server" Value='<%#Eval("sf_code")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblToEX" runat="server" Text='<%#Eval("Territory_Name")%>'>
                                                                </asp:Label>
                                                                <asp:HiddenField ID="hdnToTerrCode" runat="server" Value='<%#Eval("Territory_Code")%>' />
                                                                <asp:HiddenField ID="hidcat" runat="server" Value='<%#Eval("Territory_Cat")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Distance(One Way in Kms)">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtKms" runat="server" Text='<%#Eval("Distance") %>' CssClass="input"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="padding-bottom: 20px">
                                                <asp:Label ID="Label1" runat="server" Text="Out Station" CssClass="h2" Font-Size="15px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="grdOS" runat="server" Width="80%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                    GridLines="None" BorderStyle="Solid" CssClass="table" EmptyDataText="No Out Station Found"
                                                    PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFromOs" runat="server" Text='<%#Eval("Sf_HQ")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnOSFrmTerrCode" runat="server" Value='<%#Eval("sf_code")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblToOs" runat="server" Text='<%#Eval("Territory_Name")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnOSToTerrCode" runat="server" Value='<%#Eval("Territory_Code")%>' />
                                                                <asp:HiddenField ID="oSHidCat" runat="server" Value='<%#Eval("Territory_Cat")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Distance(One Way in Kms)" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOsKms" runat="server" CssClass="input" Text='<%#Eval("Distance") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" style="padding-bottom: 20px">
                                                <asp:Label ID="Label3" runat="server" Text="OS-EX" CssClass="h2" Font-Size="15px"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:GridView ID="grdOSEX" runat="server" Width="80%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                                    GridLines="None" BorderStyle="Solid" CssClass="table" EmptyDataText="No OS-EX Station Found"
                                                    PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#">
                                                            <ItemStyle Width="5%" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFromOSEX" runat="server" Text='<%#Eval("FName")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnOSEXFrmTerrCode" runat="server" Value='<%#Eval("FCode")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblToOSEX" runat="server" Text='<%#Eval("TName")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnOSEXToTerrCode" runat="server" Value='<%#Eval("TCode")%>' />
                                                                <asp:HiddenField ID="oSEXHidCat" runat="server" Value='<%#Eval("TCat")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Distance(One Way in Kms)" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOsExKms" runat="server" Text='<%#Eval("Distance")%>' CssClass="input"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                     <%--   <asp:TemplateField HeaderText="Return<br/>To HQ" ItemStyle-HorizontalAlign="Left">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtOsExRthq" runat="server" Text='<%#Eval("Rtn_to_hq_dist")%>' CssClass="input"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>

                        <br />
                        <center>
                            <asp:Button ID="btnSave" runat="server" CssClass="savebutton"
                                Text="Save SFC" Visible="false" OnClick="btnSave_Click" />
                        </center>

                    </div>
                </div>
            </div>
        </div>

        <br />
        <br />
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

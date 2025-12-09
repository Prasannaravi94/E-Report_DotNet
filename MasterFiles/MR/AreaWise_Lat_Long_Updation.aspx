<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AreaWise_Lat_Long_Updation.aspx.cs" Inherits="MasterFiles_MR_AreaWise_Lat_Long_Updation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Area Cluster-Lat/Long</title>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
    <style>
        #grdTerritory tr td table tr td:first-child {
            background-color: transparent;
            color: #636d73;
        }
    </style>
</head>
<body>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlSFCode]');
            var $items = $('select[id$=ddlSFCode] option');

            $txt.on('keyup', function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                  function (n) {
                      return exp.test($(n).text());
                  });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                  );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />

        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Area Cluster-Lat/Long</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-6">
                                        <asp:Panel ID="pnlAdmin" runat="server">
                                            <div style="float: left; width: 80%">
                                                <asp:Label ID="lblSalesforce" runat="server" CssClass="label" Text="Field Force Name"></asp:Label>
                                                <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                                ToolTip="Enter Text Here"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlSFCode" runat="server" CssClass="nice-select custom-select2" Width="100%">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="float: right; width: 18%; padding-top: 28px">
                                                <asp:Button ID="btnGo" runat="server" Width="60px" Text="Go" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                            </div>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblTerrritory" runat="server" Font-Size="12px" Font-Names="Verdana" Visible="true"></asp:Label>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:GridView ID="grdTerritory" runat="server" Width="100%" HorizontalAlign="Center"
                                    AllowSorting="true" EmptyDataText="No Records Found" OnPageIndexChanging="grdTerritory_PageIndexChanging"
                                    AutoGenerateColumns="false" AllowPaging="True"
                                    PageSize="100" GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                    AlternatingRowStyle-CssClass="alt">

                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="12px" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdTerritory.PageIndex * grdTerritory.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Territory_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTerritory_Code" runat="server" Text='<%#Eval("Territory_Code")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Width="250px" HeaderStyle-HorizontalAlign="Center" HeaderText="List (DR CNT)"
                                            ItemStyle-HorizontalAlign="Left" SortExpression="Territory_Name">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtTerritory_Name" runat="server" Width="250px" onkeypress="AlphaNumeric_NoSpecialChars_New(event);" CssClass="input" MaxLength="150"
                                                    Text='<%# Bind("Territory_Name") %>'></asp:TextBox>
                                                <asp:TableCell Width="30px">
                                                    <asp:Label ID="lblListedDRCnt" runat="server" Visible="false" ForeColor="DarkBlue" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                </asp:TableCell>

                                                <asp:RequiredFieldValidator ID="rfvDoc" runat="server" SetFocusOnError="true" ControlToValidate="txtTerritory_Name" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Table ID="tblTerritoryCnt" runat="server">
                                                    <asp:TableRow>
                                                        <asp:TableCell BorderStyle="None" Width="30px">
                                                            <asp:Label ID="lblTerritory_Name" runat="server" Width="250px" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                        </asp:TableCell>
                                                        <asp:TableCell Width="30px">
                                                            <asp:Label ID="lblListedDRCnt" runat="server" ForeColor="DarkBlue" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                        </asp:TableCell>

                                                    </asp:TableRow>
                                                </asp:Table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City" HeaderStyle-Width="140px" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType" runat="server" Text='<%# Bind("Alias_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" HeaderStyle-Width="40px" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblType1" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="Territory_Type" runat="server" SkinID="ddlRequired">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="HQ" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="EX" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="OS" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="OS-EX" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ControlToValidate="Territory_Type" ID="RequiredFieldValidator2"
                                                    ErrorMessage="*Required" InitialValue="0" runat="server" SetFocusOnError="true"
                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lat" HeaderStyle-Width="120px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_terr_nme_lat" runat="server" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars_New(event);" CssClass="input" MaxLength="150"
                                                    Text='<%# Bind("Territory_Name") %>'></asp:TextBox>
                                                <asp:TableCell Width="15px">
                                                    <asp:TextBox ID="txtlat" runat="server" Width="120px"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:RequiredFieldValidator ID="rfvDoc" runat="server" SetFocusOnError="true" ControlToValidate="txtTerritory_Name" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Table ID="tclterrritorylat" runat="server">
                                                    <asp:TableRow>
                                                        <asp:TableCell Width="15px">
                                                            <asp:TextBox ID="txtdcrlat" runat="server" Width="120px" ForeColor="DarkBlue" CssClass="input" Text='<%# Bind("lat") %>'></asp:TextBox>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="long" HeaderStyle-Width="120px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_terr_nme_long" runat="server" Width="120px" onkeypress="AlphaNumeric_NoSpecialChars_New(event);" CssClass="input" MaxLength="150"
                                                    Text='<%# Bind("Territory_Name") %>'></asp:TextBox>
                                                <asp:TableCell>
                                                    <asp:TextBox ID="txtlong" runat="server" Width="120px"></asp:TextBox>
                                                </asp:TableCell>
                                                <asp:RequiredFieldValidator ID="rfvDoc" runat="server" SetFocusOnError="true" ControlToValidate="txtTerritory_Name" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Table ID="tclterrritorylong" runat="server">
                                                    <asp:TableRow>
                                                        <asp:TableCell>
                                                            <asp:TextBox ID="txtdcrlong" runat="server" Width="120px" ForeColor="DarkBlue" CssClass="input" Text='<%# Bind("long") %>'></asp:TextBox>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Lat(2)" HeaderStyle-ForeColor="White" HeaderStyle-Width="120px">
                                     <EditItemTemplate>
                                        <asp:TextBox ID="txt_terr_nme_lat2" runat="server"  Width="120px" onkeypress="AlphaNumeric_NoSpecialChars_New(event);" SkinID="TxtBxAllowSymb" MaxLength="150"
                                            Text='<%# Bind("Territory_Name") %>'></asp:TextBox>
                                               <asp:TableCell>
                                                   <asp:TextBox ID="txtlat2" runat="server" Width="120px"></asp:TextBox>
                                                </asp:TableCell>
                                                     <asp:RequiredFieldValidator ID="rfvDoc" runat="server"  setfocusonerror="true" ControlToValidate="txtTerritory_Name"  ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Table ID="tclterrritorylat2" runat="server">
                                            <asp:TableRow>
                                             <asp:TableCell>
                                                    <asp:TextBox ID="txtdcrlat2" runat="server" Width="120px" ForeColor="DarkBlue" Text='<%# Bind("lat1") %>'></asp:TextBox>
                                                </asp:TableCell>
                                         </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                               </asp:TemplateField>
                                     <asp:TemplateField HeaderText="long(2)" HeaderStyle-ForeColor="White" HeaderStyle-Width="120px">
                                     <EditItemTemplate>
                                        <asp:TextBox ID="txt_terr_nme_long2" runat="server"  Width="120px" onkeypress="AlphaNumeric_NoSpecialChars_New(event);" SkinID="TxtBxAllowSymb" MaxLength="150"
                                            Text='<%# Bind("Territory_Name") %>'></asp:TextBox>
                                               <asp:TableCell >
                                                   <asp:TextBox ID="txtlong2" runat="server" Width="120px"></asp:TextBox>
                                                </asp:TableCell>
                                                     <asp:RequiredFieldValidator ID="rfvDoc" runat="server"  setfocusonerror="true" ControlToValidate="txtTerritory_Name"  ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Table ID="tclterrritorylong2" runat="server">
                                            <asp:TableRow>
                                             <asp:TableCell>
                                                    <asp:TextBox ID="txtdcrlong2" runat="server" Width="120px" ForeColor="DarkBlue" Text='<%# Bind("long1") %>'></asp:TextBox>
                                                </asp:TableCell>
                                         </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                               </asp:TemplateField>--%>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>
                        <center>
                              <asp:Button ID="btnsave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" />
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <br />
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    </form>
</body>
</html>

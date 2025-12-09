<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mgr_Auto_Allowance_Fixation.aspx.cs"
    Inherits="MasterFiles_Mgr_Auto_Allowance_Fixation" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager Auto - Allowance Fixation</title>
    <%--<link type="text/css" rel="Stylesheet" href="../css/style.css" />--%>
    <script src="../JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {



            $("table[id*=grdMgrAllowance]  tr td select").change(function () {

                // alert(this.id);

                var Id = this.id;
                var select = this;

                if (this.value == 'ACT' || this.value == 'HQ') {
                    // $('.Dist').css('display', 'none');



                    //var ID = $("#" + Id).next('td').find('input:text').attr('id');
                    var ID = $(select).closest("tr").find("input[type=text][id*=txtdist]").css('display', 'none');

                    //alert(ID);

                }
                else {
                    //  $('.Dist').css('display', 'block');
                    var ID = $(select).closest("tr").find("input[type=text][id*=txtdist]").css('display', 'block');
                }
            });

        });




    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlSubdiv]');
            var $items = $('select[id$=ddlSubdiv] option');

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
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
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
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-5">
                        <h2 class="text-center">Manager Auto - Allowance Fixation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="designation-area clearfix">
                                <div class="single-des clearfix">

                                    <asp:Label ID="lblSubdiv" runat="server" CssClass="label" Text="FieldForce Name : "></asp:Label>

                                    <%-- <div class="single-des clearfix">
                                        <asp:TextBox ID="txtNew" runat="server" Width="100px" CssClass="input" Visible="false"
                                            ToolTip="Enter Text Here"></asp:TextBox>--%>
                                    <%--<asp:LinkButton ID="linkcheck" runat="server"
                                            OnClick="linkcheck_Click">
                                            <%--<asp:TextBox ID="txtNew" runat="server" Width="100px" CssClass="input"
                                            ToolTip="Enter Text Here"></asp:TextBox>--%>
                                    <%--<img src="../Images/Selective_Mgr.png"style="height:20px;max-width:100%"/>
                                        </asp:LinkButton>--%>
                                    <%--</div>--%>

                                    <asp:DropDownList ID="ddlSubdiv" CssClass="custom-select2 nice-select" runat="server">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" CssClass="nice-select" runat="server" Visible="false">
                                    </asp:DropDownList>
                                    <%--  <div id="testImg">
                                            <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;" runat="server" /><span
                                                style="font-family: Verdana; color: Red; font-weight: bold;">Loading Please Wait...</span>
                                        </div>--%>
                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <asp:Button ID="btngo"  Text="Go" runat="server" OnClick="btnGo_Click" CssClass="savebutton" />
                                </div>

                                <div align="center">
                                    <asp:Label ID="lblSelect" Text="Select the Fieldforce" ForeColor="Red" 
                                        runat="server"></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit;">
                                <table width="100%" align="center">
                                    <tbody>
                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:GridView ID="grdMgrAllowance" Width="100%" runat="server" AutoGenerateColumns="false" OnRowDataBound="grdWTAllowance_RowDataBound"
                                                    BorderStyle="Solid" CssClass="table" AlternatingRowStyle-CssClass="alt" GridLines="None">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Fieldforce" HeaderStyle-Width="50%" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblField_Name" CssClass="label" runat="server" Text='<%# Eval("sf_name")%>'></asp:Label>
                                                                <asp:HiddenField runat="server" ID="hidcode" Value='<%# Eval("sf_code")%>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Allowance Type" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="Territory_Type" Width="200px" runat="server" CssClass="nice-select">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    <%-- <asp:ListItem Text="HQ" Value="HQ"></asp:ListItem>
                                                                    <asp:ListItem Text="EX" Value="EX"></asp:ListItem>--%>
                                                                    <asp:ListItem Text="OS" Value="OS"></asp:ListItem>
                                                                    <asp:ListItem Text="ACTUAL" Value="ACT"></asp:ListItem>
                                                                    <asp:ListItem Text="EX" Value="EX"></asp:ListItem>
                                                                    <asp:ListItem Text="Customized Allowance" Value="CA"></asp:ListItem>

                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Distance One Way (MGR HQ-MR HQ)" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtdist" runat="server" Width="70px" Height="38px" CssClass="input" Text='<%# Bind("Dist") %>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnSave" Text="Save"  CssClass="savebutton" runat="server" Visible="false"
                                                    OnClick="btnSave_Click" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
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
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>

</html>

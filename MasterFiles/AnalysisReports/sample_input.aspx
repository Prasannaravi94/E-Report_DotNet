<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sample_input.aspx.cs" Inherits="MasterFiles_AnalysisReports_sample_input" %>


<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sample Input Despatch</title>
    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

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
        $('#btnSubmit').click(function () {

            var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }
                var mode = $('#<%=ddlmode.ClientID%> :selected').text();
        if (mode == "---Select---") { alert("Select Mode"); $('#ddlmode').focus(); return false; }

        var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
        var Year1 = $('#<%=ddlFrmYear.ClientID%> :selected').text();
       var Month1 = $('#ddlFrmMonth').find(":selected").index();
        //var Fmode = document.getElementById('<%=ddlmode.ClientID%>').value;

                var mnth = Month1, yr = parseInt(Year1), validate = '', tmp = '';

                showModalPopUp(sf_Code, Month1, Year1, Name, prdvalue, prdname, vacant, Fmode);
                //if ((Month1 <= Month2 && parseInt(Year1) === parseInt(Year2)) || (parseInt(Year1) < parseInt(Year2) && (Month1 <= Month2 || Month1 >= Month2))) {
                //    if (mode == "1" || mode == "2") {
                //        showModalPopUp(sf_Code, Month1, Year1, Name, mode);
                //    }
                //}
                //else {
                //    alert("Select Valid Month & Year...");
                //    return false;
                //}
            });

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
                        <h2 class="text-center">Sample Input Despatch</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                    Visible="false" ToolTip="Enter Text Here"></asp:TextBox>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                    <%--     <asp:ListItem Selected="True">---Select---</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>


                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%">
                                    <asp:Label ID="lblFrmMoth" runat="server" Text="From Month" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlFrmMonth" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="float: right; width: 45%">
                                    <asp:Label ID="Label2" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFrmYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="Label1" runat="server" CssClass="label" Text="Mode"></asp:Label>
                            <asp:DropDownList ID="ddlmode" runat="server" CssClass="custom-select2 nice-select">
                               
                                <asp:ListItem Value="1" Text="Sample Despatch"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Input Despatch"></asp:ListItem>
                            </asp:DropDownList>
                            <%--<div class="single-des clearfix">
                            <asp:CheckBox ID="chkDetail" runat="server" Text="Check Vacant" />
                        </div>--%>

                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <%--           <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View"
                    BackColor="LightBlue" onclick="btnSubmit_Click" /></center>--%>
                            <asp:LinkButton ID="btnSubmit" runat="server" Font-Size="Medium" Font-Bold="true"
                                Text="Download Excel" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>


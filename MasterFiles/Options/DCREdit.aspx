<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCREdit.aspx.cs" Inherits="MasterFiles_Options_DCREdit" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<title>DCR Edit</title>--%>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <%--<link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="Stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="//ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="//ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnGo').click(function () {

                <%--var SMonth = $('#<%#ddlMonth.ClientID%> :selected').text();
                if (SMonth == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }--%>

            });
        });
    </script>
    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
            ({
                changeMonth: true,
                changeYear: true,
                yearRange: '1930:' + new Date().getFullYear().toString(),
                //                yearRange: "2010:2017",
                dateFormat: 'dd/mm/yy'
            });
        });

    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
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
    <%--<script type="text/javascript" language="javascript">
     $(document).ready(function () {
         $('#btnSub').click(function () {
             if ($("#txtFromdte").val() != "") {
                 if ($("#txtTodte").val() != "") {
                     return true;
                 }
                 else {
                     alert('Enter To Date')
                     return false;
                 }
             }
             else {
                 alert('Enter From Date')
                 return false;
             }

         })
     }); 

    </script>--%>

    <script type="text/javascript" language="javascript">
        function Altert() {
            if (confirm('Do you want to Insert?')) {
            }
            else {
                return false;
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
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <br />
                        <h2 class="text-center" id="hHeading" runat="server"><a href="/MIS Reports/sample_And_Input_Process.aspx"style="color:#292a34">D</a>CR Edi<a href="/MIS Reports/Sample_Input_CarryForward.aspx"style="color:#292a34">t</a></h2>




                        <table width="100%" align="center">
                            <tbody>
                                <tr align="right">
                                    <td>
                                        <asp:HyperLink ID="href" runat="server" NavigateUrl="MissedDate_DCR_Posting.aspx" ForeColor="White">.</asp:HyperLink>
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label">FieldForce Name</asp:Label>
                                <%-- <asp:TextBox ID="txtNew" runat="server"  Width="100px" CssClass="input"
                                    ToolTip="Enter Text Here"></asp:TextBox>       --%>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" OnSelectedIndexChanged="ddlFieldForce_indexchanged" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Value="-1" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                <input type="text" id="txtMonthYear" runat="server" class="nice-select" readonly="true" />
                                <%--<asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>--%>
                                <%--           <asp:Label ID="lblMonth" runat="server" CssClass="label">Month</asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="January"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="February"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="March"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="April"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="May"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="June"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="July"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="August"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="September"></asp:ListItem>
                                    <asp:ListItem Value="10" Text="October"></asp:ListItem>
                                    <asp:ListItem Value="11" Text="November"></asp:ListItem>
                                    <asp:ListItem Value="12" Text="December"></asp:ListItem>
                                </asp:DropDownList>--%>
                            </div>
                            <%--            <div class="single-des clearfix">
                                <asp:Label ID="lblYear" runat="server" CssClass="label">Year</asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="lbljoing" runat="server" Visible="false" CssClass="label">Joining Date</asp:Label>
                                <asp:Label ID="txtjoing" runat="server" Font-Bold="true" CssClass="label" ForeColor="#FF5733" Visible="false"></asp:Label>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblreport_start" runat="server" CssClass="label" Visible="false">Report Start Date</asp:Label>
                                <asp:Label ID="txtreport_start" runat="server" CssClass="label" Font-Bold="true" ForeColor="#FF5733" Visible="false"></asp:Label>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lbltp" runat="server" CssClass="label" Visible="false">Current TP Date</asp:Label>
                                <asp:Label ID="txtTP" runat="server" CssClass="label" ForeColor="#FF5733" Font-Bold="true" Visible="false"></asp:Label>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblmode" runat="server" CssClass="label" Visible="false">Insert Mode</asp:Label>
                                <asp:RadioButtonList ID="rdodate" runat="server" Visible="false" >
                                    <asp:ListItem Value="1" Text="Insert Missed Dates"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Insert Missed Dates and Report Start Date"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Insert Missed Dates and Report Start Date and TP Start Date"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Change Report Start Date"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Change TP Start Date"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Change Report Start Date and TP Date"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblfrom" runat="server" CssClass="label" Style="display: none">From Date</asp:Label>
                                <asp:TextBox ID="txtFromdte" runat="server" MaxLength="10" Style="display: none"
                                    CssClass="input DOBDate" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                <asp:CalendarExtender ID="CalStartDate" Format="dd/MM/yyyy" TargetControlID="txtFromdte" CssClass="cal_Theme1"
                                    runat="server" />
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblto" runat="server" CssClass="label" Style="display: none">To Date</asp:Label>
                                <asp:TextBox ID="txtTodte" runat="server" MaxLength="10" Style="display: none"
                                    CssClass="input DOBDate" onkeypress="CheckNumeric(event);"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtTodte" CssClass="cal_Theme1"
                                    runat="server" />
                            </div>
                            <div class="single-des clearfix">
                                <span runat="server" id="levelset" style="border-style: none; font-family: Verdana; font-size: 14px; border-color: #E0E0E0; color: #8A2EE6">
                                    <asp:LinkButton ID="lnk" ForeColor="#e0f3ff" runat="server" OnClick="lnk_Click">.</asp:LinkButton></span>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Text="Go" OnClick="btnGo_Click" />
                            <asp:Button ID="btnSub" runat="server" CssClass="savebutton" Style="display: none" Text="Insert" OnClick="btnSub_Click" />
                        </div>

                        <div id="div_del" runat="server" style="float: right">
                            <asp:ImageButton ID="img_dcr" ImageUrl="~/Images/curved-arrow.jpg" Enabled="false" Width="30px" ToolTip="DCR Deletion"
                                Height="25px" runat="server" OnClick="img_dcr_Click" />
                        </div>

                    </div>
                    <div class="col-lg-11">
                        <div class="designation-reactivation-table-area clearfix">
                            <p>
                                <br />
                            </p>
                            <div class="display-table clearfix">
                                <div class="table-responsive">

                                    <asp:GridView ID="grdTP" runat="server" Width="50%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
                                        AlternatingRowStyle-CssClass="alt">
                                        <HeaderStyle Font-Bold="False" />
                                        <SelectedRowStyle BackColor="BurlyWood" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTrans_SlNo" runat="server" Text='<%#Eval("Trans_SlNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DCR Date">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDate" Text='<%#Eval("Activity_Date")%>' runat="server" />
                                                    <%--&nbsp;<asp:Label ID="lblDate" runat="server" Text=''></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Work Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWorkType" runat="server" Text='<%# Eval("worktype_name_b") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="Update" CssClass="savebutton" Visible="false"
                                OnClientClick="return confirm('Do you want to allow DCR Edit for the selected date(s)');"
                                OnClick="btnSubmit_Click" />
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

        <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "M-yyyy",                    
                    viewMode: "months",
                    startDate: '-1m',
                    minViewMode: "months",
                    language: "tr"
                });
            });
        </script>
    </form>
</body>
<script type="text/javascript">


    $('#rdodate input').click(function () {
        var value = $("#rdodate").find('input[type=radio]:checked').val();

        if (value == "1" || value == "2" || value == "3") {

            document.getElementById("lblfrom").style.display = "block";
            document.getElementById("txtFromdte").style.display = "block";
            document.getElementById("lblto").style.display = "block";
            document.getElementById("txtTodte").style.display = "block";
            document.getElementById("btnSub").style.display = "block";

        }

        else if (value == "4" || value == "5" || value == "6") {

            document.getElementById("lblfrom").style.display = "block";
            document.getElementById("txtFromdte").style.display = "block";
            document.getElementById("lblfrom").innerHTML = "Start Date";

            document.getElementById("lblto").style.display = "none";
            document.getElementById("txtTodte").style.display = "none";
            document.getElementById("btnSub").style.display = "block";
        }


    });



</script>
<script type="text/javascript" language="javascript">
    $(document).ready(function () {
        $('#btnSub').click(function () {
            var value = $("#rdodate").find('input[type=radio]:checked').val();
            if (value == "1" || value == "2" || value == "3") {
                if ($("#txtFromdte").val() != "") {
                    if ($("#txtTodte").val() != "") {
                        if (confirm('Do you want to Insert?')) {
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        alert('Enter To Date')
                        $("#txtTodte").focus();
                        return false;
                    }
                }
                else {
                    alert('Enter From Date')
                    $("#txtFromdte").focus();
                    return false;
                }

            }

            else if (value == "4" || value == "5" || value == "6") {

                if ($("#txtFromdte").val() == "") {
                    alert('Enter Start Date')
                    $("#txtFromdte").focus();
                    return false;
                }
                else {
                    if (confirm('Do you want to Insert?')) {
                    }
                    else {
                        return false;
                    }
                }

            }

        })
    });

</script>



</html>

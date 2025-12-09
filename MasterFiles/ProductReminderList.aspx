<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductReminderList.aspx.cs"
    Inherits="MasterFiles_ProductReminderList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Details</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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

        .align {
            min-width: 150px;
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
            $('#Btnsrc').click(function () {

                var divi = $('#<%=ddlSrch.ClientID%> :selected').text();
                    var divi1 = $('#<%=ddlSrc2.ClientID%> :selected').text();
                    if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc2').focus(); return false; }
                    if (divi == "Gift Name") {
                        if ($("#txtsearch").val() == "") { alert("Enter Gift Name."); $('#txtsearch').focus(); return false; }
                    }
                });
            });
    </script>



    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <%--<link type="text/css" rel="Stylesheet" href="../../css/style.css" />--%>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        var today = new Date();
        var lastDate = new Date(today.getFullYear(), today.getMonth(0) - 1, 31);
        var year = today.getFullYear() - 1;


        var dd = today.getDate();
        var mm = today.getMonth() + 01; //January is 0!
        var yyyy = today.getFullYear();

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.DOBDate').datepicker
            ({
                minDate: dd + '/' + mm + '/' + yyyy,
                //                minDate: new Date(year, 11, 1),
                //                maxDate: new Date(year, 11, 31),

                dateFormat: 'dd/mm/yy'
                //                yearRange: "2010:2017",
            });
        });
    </script>




    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            var ddlVal = $("#ddlSrch").val();
            if (ddlVal == "2") {
                $("#txtsearch").css("display", "block");
                $("#ddlSrc2").css("display", "none");
            }
            else if (ddlVal == "3") {
                $("#ddlSrc2").css("display", "block");
                $("#txtsearch").css("display", "none");
                var TypeDel = {};
                TypeDel.Type = ddlVal;
                //                 GetDropDownVal(TypeDel, ddlVal);
            }
            else {
                $("#ddlSrc2").css("display", "none");
                $("#txtsearch").css("display", "none");
                var TypeDel = {};
                TypeDel.Type = ddlVal;
                //                 GetDropDownVal(TypeDel, ddlVal);
            }


            $("#ddlSrch").change(function () {
                var ddlVal = $("#ddlSrch").val();
                if (ddlVal == "2") {
                    $("#txtsearch").css("display", "block");
                    $("#ddlSrc2").css("display", "none");
                }
                else if (ddlVal == "3") {
                    $("#ddlSrc2").css("display", "block");
                    $("#txtsearch").css("display", "none");
                    var TypeDel = {};
                    TypeDel.Type = ddlVal;

                    //                     GetDropDownVal(TypeDel, ddlVal);
                }
                else {
                    $("#ddlSrc2").css("display", "none");
                    $("#txtsearch").css("display", "none");
                    var TypeDel = {};
                    TypeDel.Type = ddlVal;
                    //                     GetDropDownVal(TypeDel, ddlVal);
                }
            });
        });


        function GetDropDownVal(TypeDel, ddlVal) {
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "Reactivate_Gifts.aspx/GetDropDown",
                data: '{objDDL:' + JSON.stringify(TypeDel) + '}',
                dataType: "json",
                success: function (result) {
                    $('#ddlSrc2').empty();
                    //  $('#ddlSrc').append("<option value='0'>--Select--</option>");
                    if (ddlVal == "3") {

                        $.each(result.d, function (key, value) {
                            $('#ddlSrc2').append($("<option></option>").val(value.Gift_Code).html(value.Gift_Name));
                        });
                    }
                },
                error: function ajaxError(result) {
                    alert("Error");
                }
            });
        }

        function ProcessData() {
            $("#hdnProduct").val($("#ddlSrc2 option:selected").val());
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Input Details</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-9">
                                        <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Width="60px" Visible="false"
                                            Text="Add" OnClick="btnNew_Click" />
                                        <asp:Button ID="btnView" runat="server" CssClass="resetbutton" Width="70px"
                                            Text="View" OnClick="btnView_Click" />
                                        <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton" Width="110px"
                                            Text="Reactivation" OnClick="btnReactivate_Click" />
                                    </div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblEffDt" runat="server" CssClass="label" Text="Effective From Date"></asp:Label>
                                        <asp:DropDownList ID="Ddl_EffDate" runat="server" AutoPostBack="true" CssClass="nice-select"
                                            OnSelectedIndexChanged="Ddl_EffDate_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label>
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" AutoPostBack="true"
                                            TabIndex="1" OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Gift Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="State" Value="3"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2" style="padding-top: 19px; padding-left: 0px;">
                                        <div class="single-des clearfix">
                                            <asp:TextBox ID="txtsearch" runat="server" Width="100%" CssClass="input" Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px">
                                            <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" OnSelectedIndexChanged="ddlSrc2_SelectedIndexChanged"
                                                CssClass="nice-select" TabIndex="4">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="margin: 20px; margin-left: -25px;">
                                        <asp:Button ID="Btnsrc" runat="server" CssClass="savebutton" Width="45px" Visible="false"
                                            Text="Go" OnClick="Btnsrc_Click" />
                                    </div>
                                    <div class="col-lg-6"></div>
                                </div>
                            </div>
                            <br />
                            <div class="row justify-content-center" style="scrollbar-width: thin; overflow-x: auto">

                                <table width="50%" align="center">
                                    <tbody>
                                        <tr>
                                            <td style="width: 20%" />
                                            <td colspan="2">
                                                <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                                    runat="server" Width="70%" HorizontalAlign="left">
                                                    <SeparatorTemplate>
                                                    </SeparatorTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkbtnAlpha" Font-Names="Calibri" Font-Size="14px"
                                                            runat="server" CommandArgument='<%#bind("Gift_Name") %>' Text='<%#bind("Gift_Name") %>'>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdGift" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                        AllowPaging="True" PageSize="25" OnRowUpdating="grdGift_RowUpdating" OnRowEditing="grdGift_RowEditing"
                                        OnPageIndexChanging="grdGift_PageIndexChanging" OnRowCreated="grdGift_RowCreated"
                                        OnRowCancelingEdit="grdGift_RowCancelingEdit" OnRowCommand="grdGift_RowCommand"
                                        EmptyDataText="No Records Found" OnRowDataBound="grdGift_RowDataBound" GridLines="None"
                                        CssClass="table" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt"
                                        AllowSorting="True" OnSorting="grdGift_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemStyle Width="50px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Style="text-align: center" Text='<%# (grdGift.PageIndex * grdGift.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gift_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftCode" runat="server" Text='<%#Eval("Gift_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Gift_Name" HeaderText="Name"
                                                ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="GiftName" runat="server" Width="150px" CssClass="input" MaxLength="150"
                                                        Text='<%# Bind("Gift_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftName" runat="server" Text='<%# Bind("Gift_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Gift_SName" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGiftSN" CssClass="input" Width="150px" runat="server" MaxLength="150"
                                                        Text='<%# Bind("Gift_SName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftSN" runat="server" Text='<%# Bind("Gift_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Gift_Type" HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="120px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftType" runat="server" Text='<%# Bind("Gift_Type") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlGiftType" runat="server" CssClass="nice-select">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Literature/Lable" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Special Gift" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Doctor Kit" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Ordinary Gift" Value="4"></asp:ListItem>

                                                        <asp:ListItem Text="HVG" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="Paper Gift" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="Self" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="LVG" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="MVG" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="Others" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="Gift" Value="11"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eff.ToDate">
                                                <ItemStyle Width="100px" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGiftval" CssClass="input" Width="60px" runat="server"
                                                        MaxLength="5" Text='<%# Bind("Gift_Effective_To") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftVal" runat="server" Text='<%# Bind("Gift_Effective_To") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                                ShowEditButton="True" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="align">
                                                <%--<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                            <ItemStyle ForeColor="DarkBlue" Width="90px" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                                Font-Bold="True"></ItemStyle>--%>
                                            </asp:CommandField>
                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="ProductReminder.aspx?Gift_Code={0}"
                                                DataNavigateUrlFields="Gift_Code">
                                                <ItemStyle Width="60px" />
                                                <%--<ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="Deactivate">
                                                <%--<ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                            <ItemStyle ForeColor="DarkBlue" Width="100px" Font-Bold="False"></ItemStyle>--%>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Gift_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Product');">Deactivate
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>







            <%--        <br />
        <table width="100%">
            <tr>
                <td style="width: 7.2%" />
                <td align="left">
                  <div style="width: 100%; height: 40px">
                        <div style="float: left">
                    
                     </div>
                        <div style="float: left">
                    
                    </div>
                        <div style="float: left">
                    
                     </div>
                        <div style="float: left">
                    
                          </div>
                        </div>
                </td>
            </tr>
        </table>
        
        <table width="100%" align="center">
            <tbody>
                <tr>
                    <td colspan="2" align="center">
                        
                            <HeaderStyle Font-Bold="False" />
                            <PagerStyle CssClass="pgr"></PagerStyle>
                            <SelectedRowStyle BackColor="BurlyWood" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                            
                            <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
        </table>--%>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
    </form>
</body>
</html>

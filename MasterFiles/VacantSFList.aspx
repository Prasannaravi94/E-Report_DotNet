<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VacantSFList.aspx.cs" EnableEventValidation="false" Inherits="MasterFiles_VacantSFList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vacant / Hold - Fieldforce List</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

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
         .Max-width {
           width: max-content;
            display: block;
        }
       .alignment
       {
           min-width: 120px;
       }

        /*.gridview1 {
            background-color: #666699;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }

            .gridview1 a {
                margin: auto 1%;
                border-style: none;
                border-radius: 50%;
                background-color: #444;
                padding: 5px 7px 5px 7px;
                color: #fff;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview1 a:hover {
                    background-color: #1e8d12;
                    color: #fff;
                }

            .gridview1 td {
                border-style: none;
            }

            .gridview1 span {
                background-color: #ae2676;
                color: #fff;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 7px 5px 7px;
            }*/

        .blink_me {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 1s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 1s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        .blink {
            animation: blink-animation 1s steps(5, start) infinite;
            -webkit-animation: blink-animation 1s steps(5, start) infinite;
        }

        @keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }

        @-webkit-keyframes blink-animation {
            to {
                visibility: hidden;
            }
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

    <script type="text/javascript" language="javascript">

        ////$(document).ready(function () {

        ////    var ddlVal = $("#ddlFields").val();

        ////    if (ddlVal == "UsrDfd_UserName" || ddlVal == "Sf_Name" || ddlVal == "Sf_HQ" || ddlVal == "sf_emp_id") {
        ////        $("#txtsearch").css("display", "block");
        ////        $("#ddlSrc").css("display", "none");
        ////    }
        ////    else if (ddlVal == "StateName" || ddlVal == "Designation_Name") {
        ////        $("#ddlSrc").css("display", "block");
        ////        $("#txtsearch").css("display", "none");



        ////        var TypeDel = {};
        ////        TypeDel.Type = ddlVal;

        ////        GetDropDownVal(TypeDel, ddlVal);

        ////    }

        ////    $("#ddlFields").change(function () {

        ////        var ddlVal = $("#ddlFields").val();

        ////        if (ddlVal == "UsrDfd_UserName" || ddlVal == "Sf_Name" || ddlVal == "Sf_HQ" || ddlVal == "sf_emp_id") {
        ////            $("#txtsearch").css("display", "block");
        ////            $("#ddlSrc").css("display", "none");
        ////        }
        ////        else if (ddlVal == "StateName" || ddlVal == "Designation_Name") {
        ////            $("#ddlSrc").css("display", "block");
        ////            $("#txtsearch").css("display", "none");

        ////            var TypeDel = {};
        ////            TypeDel.Type = ddlVal;

        ////            GetDropDownVal(TypeDel, ddlVal);
        ////        }

        ////    });


        ////});


        function GetDropDownVal(TypeDel, ddlVal) {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "SalesForceList.aspx/GetDropDown",
                data: '{objDDL:' + JSON.stringify(TypeDel) + '}',
                dataType: "json",
                success: function (result) {
                    $('#ddlSrc').empty();
                    //  $('#ddlSrc').append("<option value='0'>--Select--</option>");

                    if (ddlVal == "StateName") {

                        $.each(result.d, function (key, value) {
                            $('#ddlSrc').append($("<option></option>").val(value.State_Code).html(value.StateName));
                        });

                    }
                    else if (ddlVal == "Designation_Name") {

                        $.each(result.d, function (key, value) {

                            $('#ddlSrc').append($("<option></option>").val(value.Designation_Code).html(value.Designation_Name));


                        });
                        //var HQName = $("#ddlSrc").find("select option:selected").text();
                        //$("#ddlSrc option:contains(" + HQName + ")").attr('selected', 'selected');
                    }

                },
                error: function ajaxError(result) {
                    ////alert("Error");
                }
            });

        }

        function ProcessData() {
            $("#hdnProduct").val($("#ddlSrc option:selected").val());


            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Vacant / Hold - Fieldforce List</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="SearchBy" runat="server" Text="SearchBy" CssClass="label">
                                        </asp:Label>
                                     
                                                <asp:DropDownList ID="ddlFields" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                                                    <asp:ListItem Value="UsrDfd_UserName">User Name</asp:ListItem>
                                                    <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                                                    <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>
                                                    <asp:ListItem Value="sf_emp_id">Employee Id</asp:ListItem>
                                                    <asp:ListItem Value="StateName">State</asp:ListItem>
                                                    <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                                                </asp:DropDownList>
                                           
                                    </div>

                                    <div class="col-lg-2">
                                        <div class="single-des clearfix" style="padding-top: 19px;">
                                            <asp:TextBox ID="txtsearch" runat="server" Width="100%"
                                                CssClass="input"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px;">
                                            <asp:DropDownList ID="ddlSrc" runat="server" CssClass="nice-select" Visible="false"
                                                TabIndex="4">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnProduct" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btnGo" OnClick="btnGo_Click" runat="server" Text="Go" Width="50px"
                                            CssClass="savebutton" Visible="false" OnClientClick="return ProcessData()"></asp:Button>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblFieldForceType" runat="server" Text="Filter by" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlFieldForceType" runat="server"
                                            TabIndex="1" CssClass="nice-select"
                                            Font-Bold="False">
                                            <%--  <asp:ListItem Text="---Select---" Value="" ></asp:ListItem> --%>
                                            <%-- <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                                       <asp:ListItem Text="MR" Value="1"></asp:ListItem>
                                       <asp:ListItem Text="Manager" Value="2"></asp:ListItem>--%>

                                            <asp:ListItem Text="Vacant Id's" Value="R"></asp:ListItem>
                                            <asp:ListItem Text="Hold Id's" Value="H"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" TabIndex="2" runat="server"
                                            Text="Go" Width="50px" CssClass="savebutton"></asp:Button>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <br />
                        <br />
                        <div class="row" style="scrollbar-width: thin; overflow-x: auto">
                            <table width="100%" align="center">
                                <tbody>
                                    <tr>
                                        <%--<td style="width: 20%" />--%>
                                        <td colspan="2" align="center">
                                            <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                                runat="server" Width="30%" HorizontalAlign="center">
                                                <SeparatorTemplate>
                                                </SeparatorTemplate>
                                                <ItemTemplate>
                                                    <%--<asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Names="Calibri" Font-Size="14px" ForeColor="#8A2EE6" CommandArgument='<%#bind("sf_name") %>'
                                        Text='<%#bind("sf_name") %>'>
                                    </asp:LinkButton>--%>
                                                    <asp:LinkButton ID="lnkLetter" runat="server" Font-Size="15px"
                                                        CommandName="Filter"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Letter")%>'>
      <%# DataBinder.Eval(Container, "DataItem.Letter")%>
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

                                <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center" PagerStyle-CssClass="gridview1"
                                    AutoGenerateColumns="false" AllowPaging="True" PageSize="10" OnRowCommand="grdSalesForce_RowCommand"
                                    EmptyDataText="No Records Found" GridLines="None" OnPageIndexChanging="grdSalesForce_PageIndexChanging"
                                    CssClass="table">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHq" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDes" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSt" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Doctor Count">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblDr_count" runat="server" Text='<%# Bind("Dr_Count") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:HyperLinkField HeaderText="Activate" Text="Activate" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sfname={1}"
                                            DataNavigateUrlFields="SF_Code,Sf_Name"></asp:HyperLinkField>

                                        <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;state={1}"
                                            DataNavigateUrlFields="SF_Code,StateName"></asp:HyperLinkField>

                                        <asp:HyperLinkField HeaderText="Rejoin" Text="Rejoin" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sfname={1}&amp;state_code={2}"
                                            DataNavigateUrlFields="SF_Code,Sf_Name,state_code"></asp:HyperLinkField>

                                        <asp:HyperLinkField HeaderText="Release Hold" Text="Release Hold" Visible="false" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sfname={1}&amp;state_code={2}&amp;sf_hold_realse={3}" ControlStyle-CssClass="Max-width"
                                            DataNavigateUrlFields="SF_Code,Sf_Name,state_code,state_code"></asp:HyperLinkField>

                                        <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("SF_Code") %>'
                                                    CommandName="Deactivate" OnClientClick="return confirm_DeActive();">Deactivate</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:HyperLinkField HeaderText="Vacant" Text="To Vacant" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sf_hq={1}&amp;make_vac={2}" ControlStyle-CssClass="Max-width"
                                            DataNavigateUrlFields="SF_Code,Sf_HQ,Sf_HQ"></asp:HyperLinkField>

                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <asp:GridView ID="GridView1" runat="server" Width="100%" HorizontalAlign="Center" OnRowDataBound="GridView1_Rowdatabound"
                                    AutoGenerateColumns="false" PageSize="10" EmptyDataText="No Records Found"
                                    GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">

                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                            <ItemTemplate>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="User Name" Visible="false">

                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="300px" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsfName2" runat="server" CssClass="blink_me" Text='<%# Bind("search") %>'></asp:Label></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="80px" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">

                                            <%--<ItemTemplate>
                                            <asp:Label ID="lblDesiName2" runat="server" CssClass="blink_me" Text='<%# Bind("search2") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                        
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">

                                            <%--<ItemTemplate>
                                            <asp:Label ID="lblHQ2" runat="server" CssClass="blink_me" Text='<%# Bind("search3") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="State" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                            <%--   <ItemTemplate>
                                            <asp:Label ID="lblstateName2" runat="server" CssClass="blink_me" Text='<%# Bind("search4") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                        
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activate" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblactivate" runat="server" CssClass="blink_me" Text='<%# Bind("search2") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Edit" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbledit" runat="server" CssClass="blink_me" Text='<%# Bind("search3") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejoin" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrej" runat="server" CssClass="blink_me" Text='<%# Bind("search4") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Deactivate" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldeact" runat="server" CssClass="blink_me" Text='<%# Bind("search5") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>

                            </div>
                        </div>

                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                </div>
                <br />
                <br />
                <div class="loading" align="center">
                    Loading. Please wait.<br />
                    <br />
                    <img src="../Images/loader.gif" alt="" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesForceList.aspx.cs" EnableEventValidation="false" Inherits="MasterFiles_SalesForceList" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Field Force</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
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

        .display-table .table td, .display-table .table td {
            padding: 15px 5px !important;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
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
        function confirm_DeActive() {
            if (confirm('Do you want to Deactivate the Fieldforce?')) {
                if (confirm('Are you sure?')) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFilter]');
            var $items = $('select[id$=ddlFilter] option');

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
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JsFiles/jquery.tooltip.min.js"></script>
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
            $('#btnSearch').click(function () {

                //var divi = $('#<%=ddlFields.ClientID%> :selected').text();
                // var divi1 = $('#<%=ddlSrc.ClientID%> :selected').text();
                //if (divi1 == "---Select---") { alert("Select " + divi); $('#ddlSrc').focus(); return false; }


            });
        });
    </script>
    <style type="text/css">
        #tooltip {
            position: absolute;
            z-index: 3000;
            border: 1px solid #111;
            background-color: #FEE18D;
            padding: 5px;
            opacity: 0.85;
        }

            #tooltip h3, #tooltip div {
                margin: 0;
            }

        .Max-width {
            width: max-content;
            display: block;
        }

        .alignment {
            min-width: 120px;
        }

        .headeralignment {
            min-width: 85px;
        }
        #grdSalesForce > tbody > tr:nth-child(1) > th:nth-child(4) {
            width: 100px;
        }
        #grdSalesForce > tbody > tr:nth-child(1) > th:nth-child(5){
            width: 100px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            var ddlVal = $("#ddlFields").val();

            if (ddlVal == "UsrDfd_UserName" || ddlVal == "Sf_Name" || ddlVal == "Sf_HQ" || ddlVal == "sf_emp_id") {
                $("#txtsearch").css("display", "block");
                $("#ddlSrc").css("display", "none");
                document.getElementById("ddlSrcdisplay").style.display = "none";

            }
            else if (ddlVal == "StateName" || ddlVal == "Designation_Name") {
                $("#ddlSrc").css("display", "block");
                $("#txtsearch").css("display", "none");
                document.getElementById("ddlSrcdisplay").style.display = "block";



                var TypeDel = {};
                TypeDel.Type = ddlVal;

                GetDropDownVal(TypeDel, ddlVal);

            }

            $("#ddlFields").change(function () {

                var ddlVal = $("#ddlFields").val();

                if (ddlVal == "UsrDfd_UserName" || ddlVal == "Sf_Name" || ddlVal == "Sf_HQ" || ddlVal == "sf_emp_id") {
                    $("#txtsearch").css("display", "block");
                    $("#ddlSrc").css("display", "none");
                    document.getElementById("ddlSrcdisplay").style.display = "none";
                }
                else if (ddlVal == "StateName" || ddlVal == "Designation_Name") {
                    $("#ddlSrc").css("display", "block");
                    $("#txtsearch").css("display", "none");
                    document.getElementById("ddlSrcdisplay").style.display = "block";

                    var TypeDel = {};
                    TypeDel.Type = ddlVal;

                    GetDropDownVal(TypeDel, ddlVal);
                }

            });


        });


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
                    alert("Error");
                }
            });

        }

        function ProcessData() {
            $("#hdnProduct").val($("#ddlSrc option:selected").val());


            return true;
        }

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
    <%--<link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet"/>--%>
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

            <div class="home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Field Force</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-11">

                                        <asp:Button ID="btnNew" runat="server" ToolTip="Click Here to Create the New ID's" CssClass="savebutton" Text="New ID Creation" Width="140px"
                                            OnClick="btnNew_Click" />
                                        <asp:Button ID="btnMulti_Divi" runat="server" Enabled="false" Width="180px" CssClass="resetbutton" Text="Multi Division Selection"
                                            OnClick="btnDivision_Click" Visible="false" />
                                        <asp:Button ID="btnHo_Create" runat="server" Width="160px" CssClass="resetbutton" Text="Audit-ID Creation"
                                            OnClick="btnHo_Create_Click" />
                                        <asp:Button ID="btnPromoDePromo" runat="server" Width="185px" CssClass="resetbutton" Text="Promotion / De-Promotion"
                                            OnClick="btnPromoDePromo_Click" />
                                        <asp:Button ID="btnApproval" runat="server" CssClass="resetbutton" Width="160px" Text="Approval Changes"
                                            OnClick="btnApproval_Click" />
                                        <asp:Button ID="btnReactivate" runat="server" CssClass="resetbutton" Width="140px" Text="Reactivation"
                                            OnClick="btnReactivate_Click" />
                                        <asp:Button ID="btninterchange" runat="server" CssClass="resetbutton" Width="180px" Text="Interchange/Transfer"
                                            OnClick="btninterchange_Click" />
                                        <asp:Button ID="btnBkEd" runat="server" CssClass="resetbutton" Text="Bulk Edit" Width="160px"
                                            OnClick="btnBkEd_Click" />
                                        <asp:Button ID="btnBulk" runat="server" CssClass="resetbutton" Text="Edit - DCR Start Date"
                                            Width="185px" OnClick="btnBulk_Click" />
                                        <asp:Button ID="btnBulkTP" runat="server" CssClass="resetbutton" Text="Edit - TP Start Date"
                                            Width="160px" OnClick="btnBulkTP_Click" />
                                        <asp:Button ID="btnStatus" runat="server" CssClass="resetbutton" Width="140px" Text="Field Force Status"
                                            OnClick="btnStatus_Click" />
                                        <asp:Button ID="btnVac" runat="server" CssClass="resetbutton" Width="180px" Text="View Vacant/ Hold ID's"
                                            OnClick="btnVac_Click" />
                                        <asp:Button ID="btnBlk" runat="server" CssClass="resetbutton" Width="160px" Text="View Blocked ID's"
                                            OnClick="btnBlk_Click" />

                                        <asp:Button ID="btnPromo" runat="server" CssClass="resetbutton" Width="250px" Text="BaseLevel⇌Manager(Promo/DePro)"
                                            OnClick="btnPromo_Click" />

                                    </div>
                                </div>
                                <br />
                                <br />
                                <%--  <asp:UpdatePanel ID="updateP" runat="server">
     
           <ContentTemplate>  --%>
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="SearchBy" runat="server" Text="SearchBy" CssClass="label">
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlFields" runat="server" CssClass="custom-select2 nice-select" Width="100%">
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
                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px;" id="ddlSrcdisplay">
                                            <asp:DropDownList ID="ddlSrc" runat="server" CssClass="custom-select2 nice-select" TabIndex="4" Width="100%">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnProduct" runat="server" />
                                        </div>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Go" Width="50px"
                                            CssClass="savebutton" Visible="false" OnClientClick="return ProcessData()"></asp:Button>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblFilter" runat="server" Text="Filter By Manager" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlFilter" data-live-search="true" runat="server" CssClass="custom-select2 nice-select">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false"></asp:DropDownList>
                                        <%-- <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA"
                                Visible="false" ToolTip="Enter Text Here"></asp:TextBox>
                            <asp:LinkButton ID="linkcheck" runat="server" OnClick="linkcheck_Click">
                          <img src="../Images/Selective_Mgr.png" />
                            </asp:LinkButton>
                            <div id="testImg">
                                <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;"
                                    runat="server" /><span style="font-family: Verdana; color: Red; font-weight: bold;">Loading
                                        Please Wait...</span>
                            </div>--%>
                                        <%-- </div>
                               <div style="float: left">
                            <asp:DropDownList ID="ddlFilter" SkinID="ddlRequired" runat="server" Width="300px"
                                Visible="false">
                            </asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                            </asp:DropDownList>
                                     </div>--%>
                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Width="50px"
                                            Text="Go" OnClick="btnGo_Click" />
                                    </div>
                                </div>

                            </div>
                            <%--  </ContentTemplate>
       <Triggers>
       <asp:PostBackTrigger ControlID="btnSearch" />
       <asp:PostBackTrigger ControlID="btnGo" />
     </Triggers>
       </asp:UpdatePanel>--%>
                            <br />
                            <br />
                            <div class="row" style="scrollbar-width: thin; overflow-x: auto">
                                <table width="85%" align="center">
                                    <tbody>
                                        <tr>
                                            <%--<td style="width: 20%" />--%>
                                            <td colspan="2" align="center">
                                                <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                                    runat="server" Width="40%" HorizontalAlign="center">
                                                    <SeparatorTemplate>
                                                    </SeparatorTemplate>
                                                    <ItemTemplate>
                                                        <%--  <asp:LinkButton ID="lnkbtnAlpha" runat="server" Font-Names="Calibri" Font-Size="15px" ForeColor="#8A2EE6" CommandArgument='<%#bind("sf_name") %>'
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

                                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                                        OnRowUpdating="grdSalesForce_RowUpdating" OnRowEditing="grdSalesForce_RowEditing"
                                        OnPageIndexChanging="grdSalesForce_PageIndexChanging" OnRowCreated="grdSalesForce_RowCreated"
                                        OnRowCancelingEdit="grdSalesForce_RowCancelingEdit" OnRowCommand="grdSalesForce_RowCommand"
                                        OnRowDataBound="grdSalesForce_RowDataBound" AllowSorting="True" OnSorting="grdSalesForce_Sorting"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1">

                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Sf_UserName" HeaderText="User Name" Visible="false">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtUsrName" runat="server" CssClass="input" Text='<%# Bind("Sf_UserName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:TemplateField HeaderText="Details">
                                      <ItemTemplate>
                                      <a href="#" class="gridViewToolTip" style="text-decoration:none"> <%# Eval("Sf_Name")%></a>
                                           <div id="tooltip" style="display: none;">
                                                <table>
                                                    <tr>
                                                        <td style="white-space: nowrap;">
                                                            <b>UserName:</b>&nbsp;
                                                        </td>
                                                        <td>
                                                            <%# Eval("Sf_UserName")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="white-space: nowrap;">
                                                            <b>Sf Name:</b>&nbsp;
                                                        </td>
                                                        <td>
                                                            <%# Eval("Sf_Name")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                      </ItemTemplate>
                                    </asp:TemplateField>--%>
                                            <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtsfName" CssClass="input " Height="38px" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvsf" runat="server" SetFocusOnError="true" ControlToValidate="txtsfName"
                                                        ErrorMessage="*Enter Name"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSFType" runat="server" Text='<%#Eval("Type")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Designation_Name" HeaderText="Design" HeaderStyle-CssClass="headeralignment">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlDesign" runat="server" CssClass="nice-select" DataSource="<%# Fill_Design() %>"
                                                        DataTextField="Designation_Name" DataValueField="Designation_Code">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvdes" runat="server" SetFocusOnError="true" ControlToValidate="ddlDesign" InitialValue="0"
                                                        ErrorMessage="*Select Desigantion"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Sf_HQ" HeaderText="HQ">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtHQ" CssClass="input" Height="38px" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvhq" runat="server" SetFocusOnError="true" ControlToValidate="txtHQ"
                                                        ErrorMessage="*Enter HQ"></asp:RequiredFieldValidator>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="StateName" HeaderText="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select" DataSource="<%# FillState() %>"
                                                        DataTextField="StateName" DataValueField="state_code">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-CssClass="alignment"
                                                ShowEditButton="True"></asp:CommandField>

                                            <asp:HyperLinkField HeaderText="Edit" Text="Edit" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}"
                                                DataNavigateUrlFields="SF_Code"></asp:HyperLinkField>
                                            <asp:HyperLinkField HeaderText="Vacant" Text="To Vacant" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sf_hq={1}" ControlStyle-CssClass="Max-width"
                                                DataNavigateUrlFields="SF_Code,Sf_HQ"></asp:HyperLinkField>

                                            <asp:HyperLinkField HeaderText="Hold" Text="To Hold" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sf_hq={1}&amp;sf_hold={2}" ControlStyle-CssClass="Max-width"
                                                DataNavigateUrlFields="SF_Code,Sf_HQ,Sf_HQ"></asp:HyperLinkField>

                                            <asp:HyperLinkField HeaderText="Block" ItemStyle-HorizontalAlign="Center" Text="Block" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sf_type={1}"
                                                DataNavigateUrlFields="SF_Code,SF_Type"></asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("SF_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm_DeActive();">Deactivate</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>


                                    <asp:GridView ID="GridView1" runat="server" Width="100%" HorizontalAlign="Center"
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

                                            <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName2" runat="server" CssClass="blink_me" Text='<%# Bind("search") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Design" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">


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
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbledit" runat="server" CssClass="blink_me" Text='<%# Bind("search2") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vacant" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvacant" runat="server" CssClass="blink_me" Text='<%# Bind("search3") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Block" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblblock" runat="server" CssClass="blink_me" Text='<%# Bind("search4") %>'></asp:Label>
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
        </div>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

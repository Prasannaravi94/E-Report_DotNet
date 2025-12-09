<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approval_List.aspx.cs" Inherits="MasterFiles_Approval_List"    %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Approval - Changes (Fieldforce wise)</title>

    <link href="../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />


    <style type="text/css">
        .style1 {
            width: 2.6%;
            height: 64px;
        }

        .style2 {
            width: 50%;
            height: 64px;
        }

        /*.gridview1 {
            background-color: #666699;
            border-style: none;
            padding: 2px;
            margin: 2% auto;
        }*/

        /*.gridview1 a {
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

    <style type="text/css">
       .display-Approvaltable .table tr td:last-child
       {
           min-width:110px;

       }
        .display-Approvaltable .table td {
            padding: 5px 4px !important;
        }
    </style>

</head>
<script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
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

<body style="overflow-x:scroll">
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">Approval - Changes (Fieldforce wise)</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-11">
                                        <asp:Button ID="btnApproval" runat="server" CssClass="savebutton" Width="105px" Text="Approval Edit"
                                            OnClick="btnApproval_Click" />
                                        <%-- <table border="0" id="tblMgrDtls" align="right" style="width: 43%">
                                          <tr style="height: 30px">
                                          <td>
                                          <asp:Label ID="lblFilter" runat="server" Text="Select the Manager"></asp:Label>
                                            &nbsp;
                                           <asp:DropDownList ID="ddlFilter" TabIndex="1" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                          </asp:DropDownList>
                                           &nbsp;
                                           <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" CssClass="savebutton" />
                                         </td>
                                        </tr>
                                      </table>--%>
                                    </div>
                                </div>
                                <br />
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="SearchBy" runat="server" Text="SearchBy" CssClass="label">
                                            <asp:DropDownList ID="ddlFields" runat="server" CssClass="nice-select"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                                                <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                                                <asp:ListItem Value="UsrDfd_UserName">User Name</asp:ListItem>
                                                <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                                                <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>
                                                <asp:ListItem Value="sf_emp_id">Employee Id</asp:ListItem>
                                                <%--<asp:ListItem Value="StateName">State</asp:ListItem>--%>
                                                <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                                            </asp:DropDownList>
                                        </asp:Label>
                                    </div>

                                    <div class="col-lg-2">
                                        <div class="single-des clearfix" style="padding-top: 19px;">
                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%"
                                                Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px;">
                                            <asp:DropDownList ID="ddlSrc" runat="server" Visible="false" CssClass="nice-select" OnSelectedIndexChanged="ddlSrc_SelectedIndexChanged"
                                                TabIndex="4">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Go" Width="50px" CssClass="savebutton" Visible="false"></asp:Button>
                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-4">

                                        <asp:Label ID="lblFilter" runat="server" Text="Select the Manager" CssClass="label"></asp:Label>
                                        <%-- <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                            ToolTip="Enter Text Here"></asp:TextBox>
                                        <asp:LinkButton ID="linkcheck" runat="server"
                                            OnClick="linkcheck_Click">
                          <img src="../Images/Selective_Mgr.png" />
                                        </asp:LinkButton>
                                        <div id="testImg">
                                            <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;" runat="server" /><span
                                                style="font-family: Verdana; color: Red; font-weight: bold;">Loading Please Wait...</span>--%>

                                        <asp:DropDownList ID="ddlFilter" TabIndex="1" runat="server" Visible="false" CssClass="custom-select2 nice-select" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                        </asp:DropDownList>
                                    </div>

                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btnGo" runat="server" Width="50px" Text="Go" Visible="false" OnClick="btnGo_Click" CssClass="savebutton" />
                                    </div>
                                </div>
                            </div>

                        </div>

                        <br />
                        <br />

                        <%--<table width="100%" align="center">
                    <tbody>
                    <tr>
                        <td class="style1" />
                        <td align="left" class="style2">
                            <asp:Label ID="SearchBy" Font-Bold="true" ForeColor="Purple"  runat="server" Text="SearchBy">
                            </asp:Label>
                            &nbsp;  
                            <asp:DropDownList ID="ddlFields" SkinID="ddlRequired" runat="server" CssClass="DropDownList"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
                                <asp:ListItem Selected="true" Value="">---Select---</asp:ListItem>
                               <asp:ListItem Value="UsrDfd_UserName">User Name</asp:ListItem>
                                <asp:ListItem Value="Sf_Name">FieldForce Name</asp:ListItem>
                                <asp:ListItem Value="Sf_HQ">HQ</asp:ListItem>--%>
                        <%--<asp:ListItem Value="StateName">State</asp:ListItem>
                                <asp:ListItem Value="Designation_Name">Designation</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" onfocus="this.style.backgroundColor='#E0EE9D'" Width="150px" CssClass="TEXTAREA"
                                Visible="false"></asp:TextBox>
                            <asp:DropDownList ID="ddlSrc" runat="server" Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'" OnSelectedIndexChanged="ddlSrc_SelectedIndexChanged"
                                SkinID="ddlRequired" TabIndex="4">
                            </asp:DropDownList>
                            <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="Go" Width="30px" Height="25px" CssClass="savebutton" Visible="false">
                            </asp:Button>


                            <table border="0" id="tblMgrDtls" align="right" style="width: 43%">
                                 <tr style="height: 30px">
                                   <td>
                                   <asp:Label ID="lblFilter" runat="server" ForeColor="Purple" Text="Select the Manager"></asp:Label>
                                    &nbsp;
                                   <asp:DropDownList ID="ddlFilter" TabIndex="1" runat="server" SkinID="ddlRequired" OnSelectedIndexChanged="ddlFilter_SelectedIndexChanged">
                                    </asp:DropDownList>
                                   &nbsp;
                                   <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px" Text="Go" OnClick="btnGo_Click" CssClass="savebutton" />
                                   </td>
                                </tr>
                         </table>

                        </td>
                       </tr>
                </tbody>
            </table>--%>

                        <div class="display-Approvaltable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">

                                <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center" EmptyDataText="No Records Found"
                                    AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="grdSalesForce_PageIndexChanging"
                                    OnPreRender="grdSalesForce_PreRender" OnRowCreated="grdSalesForce_RowCreated" OnRowEditing="grdSalesForce_RowEditing"
                                    GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" style="background-color:white"
                                    OnRowUpdating="grdSalesForce_RowUpdating" OnRowCancelingEdit="grdSalesForce_RowCancelingEdit" OnRowDataBound="grdSalesForce_RowDataBound"
                                    ShowHeader="false">

                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSF_Code" runat="server" Text='<%# Bind("SF_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FieldForce Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Design">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="HQ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reporting To">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReporting" runat="server" Text='<%# Bind("Reporting_To") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Reporting" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblReport" runat="server" Text='<%# Bind("Reporting") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DCR_AM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDCRReporting" runat="server" Text='<%# Bind("DCR_AM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlDCR" runat="server" CssClass="nice-select" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TP_AM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTPReporting" runat="server" Text='<%# Bind("TP_AM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlTP" runat="server" CssClass="nice-select" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                </asp:DropDownList>
                                            </EditItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LstDr_AM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLstReporting" runat="server" Text='<%# Bind("LstDr_AM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlLstDr" runat="server" CssClass="nice-select" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave_AM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLeaveReporting" runat="server" Text='<%# Bind("Leave_AM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlLeave" runat="server" CssClass="nice-select" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SS_AM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSSReporting" runat="server" Text='<%# Bind("SS_AM") %>'></asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlSS" runat="server" CssClass="nice-select" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Expense_AM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblExpReporting" runat="server" Text='<%# Bind("Expense_AM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlExp" runat="server" CssClass="nice-select" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Otr_AM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOtrReporting" runat="server" Text='<%# Bind("Otr_AM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlOtr" runat="server" CssClass="nice-select" DataSource="<%# FillSalesForce_Rep() %>"
                                                    DataTextField="Sf_Name" DataValueField="SF_Code">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                            ShowEditButton="True"></asp:CommandField>
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

                                        <asp:TemplateField HeaderText="Design" HeaderStyle-Width="80px" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">

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
                                        <asp:TemplateField HeaderText="Inline Edit" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lbledit" runat="server" CssClass="blink_me" Text='<%# Bind("search2") %>'></asp:Label>
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

        <script type="text/javascript">
            $(document).ready(function () {
                $(".custom-select2").select2();
            });
        </script>


    </form>
</body>
</html>

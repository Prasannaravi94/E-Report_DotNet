<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Salesforce_Promo_DePromo.aspx.cs" Inherits="MasterFiles_Salesforce_Promo_DePromo" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Field Force Promotion / De-Promotion (Baselevel & Managers)</title>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

    <style type="text/css">
          .max-width {
           width: max-content;
            display: block;
        }
        
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


            <div class="container home-section-main-body position-relative clearfix">
                <br />
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center">
                            <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="SearchBy" CssClass="label" runat="server" Text="SearchBy">
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlFields" runat="server" CssClass="nice-select"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlFields_SelectedIndexChanged">
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

                                            <asp:TextBox ID="txtsearch" runat="server" CssClass="input" Width="100%"
                                                Visible="false"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px;">
                                            <asp:DropDownList ID="ddlSrc" runat="server" Visible="false"
                                                CssClass="nice-select" TabIndex="4">
                                            </asp:DropDownList>

                                        </div>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 19px; padding-left: 0px">
                                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" CssClass="savebutton" Width="50px" Text="Go" Visible="false"></asp:Button>

                                    </div>
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-4">
                                        <asp:Label ID="lblFilter" runat="server" Text="Filter By Manager" CssClass="label"></asp:Label>
                                        <asp:Label ID="lblFieldForceType" runat="server" Text="Filter by" CssClass="label" Visible="false"></asp:Label>

                                        <%--  <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                            ToolTip="Enter Text Here"></asp:TextBox>
                                        <asp:LinkButton ID="linkcheck" runat="server"
                                            OnClick="linkcheck_Click">
                                        <img src="../Images/Selective_Mgr.png" />
                                        </asp:LinkButton>
                                    <div id="testImg">
                                            <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;" runat="server" /><span
                                                style="font-family: Verdana; color: Red; font-weight: bold;">Loading Please Wait...</span>
                                        </div>--%>

                                        <asp:DropDownList ID="ddlFilter" CssClass="custom-select2 nice-select" Visible="false" runat="server">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass=" nice-select">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlFieldForceType" runat="server" Visible="false"
                                            TabIndex="1" CssClass=" nice-select"
                                            Font-Bold="False">
                                            <asp:ListItem Text="---Select---" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Base Level" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Manager" Value="2"></asp:ListItem>
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-lg-1" style="padding-top: 17px; padding-left: 0px">
                                        <asp:Button ID="btnGo" runat="server" CssClass="savebutton" Width="50px" Visible="false" Text="Go" OnClick="btnGo_Click" />
                                        <asp:Button ID="btsearc" OnClick="btsearc_Click" TabIndex="2" runat="server"
                                            Text="Go" CssClass="savebutton" Width="50px"></asp:Button>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <br />
                            <div class="row " style="scrollbar-width: thin;overflow-x: auto">
                                <table width="30%" align="center">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                                    runat="server" Width="70%" HorizontalAlign="left">
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

                                    <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                                        AutoGenerateColumns="false" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                                        OnPageIndexChanging="grdSalesForce_PageIndexChanging" OnRowCreated="grdSalesForce_RowCreated"
                                        AllowSorting="True" OnSorting="grdSalesForce_Sorting"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" >
                                       
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
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUsrName" runat="server" Text='<%# Bind("Sf_UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Sf_Name" HeaderText="FieldForce Name" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Designation_Name" HeaderText="Designation" >                                              
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesiName" runat="server" Text='<%# Bind("Designation_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="Sf_HQ" HeaderText="HQ" >    
                                                <ItemTemplate>
                                                    <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField SortExpression="StateName" HeaderText="State" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstateName" runat="server" Text='<%# Bind("StateName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:HyperLinkField HeaderText="Promote" Text="Promote" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sfusername={1}"
                                                DataNavigateUrlFields="SF_Code,Sf_UserName">
                                            </asp:HyperLinkField>

                                            <asp:HyperLinkField HeaderText="De-Promote" Text="De-Promote" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;desgname={1}" ControlStyle-CssClass="max-width"
                                                DataNavigateUrlFields="SF_Code,Designation_Name">
                                            </asp:HyperLinkField>

                                            <asp:HyperLinkField HeaderText="Promote" Text="Promote" Visible="false" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sf_hq={1}&amp;Designation_Name={2}"
                                                DataNavigateUrlFields="SF_Code,Sf_HQ,Designation_Name"> 
                                            </asp:HyperLinkField>

                                            <asp:HyperLinkField HeaderText="De-Promote" Text="De-Promote" Visible="false" DataNavigateUrlFormatString="~/MasterFiles/SalesForce.aspx?sfcode={0}&amp;sf_hq={1}&amp;Reporting_To_Manager={2}" ControlStyle-CssClass="max-width"
                                                DataNavigateUrlFields="SF_Code,Sf_HQ,Reporting_To">
                                            </asp:HyperLinkField>

                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>

                                    <asp:GridView ID="GridView1" runat="server" Width="100%" HorizontalAlign="Center" OnRowDataBound="GridView1_Rowdatabound"
                                        AutoGenerateColumns="false" PageSize="10" EmptyDataText="No Records Found"
                                        GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1" >     
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

                                            <asp:TemplateField HeaderText="FieldForce Name" HeaderStyle-Width="300px"  ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">                                               
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsfName2" runat="server" CssClass="blink_me" Text='<%# Bind("search") %>'></asp:Label></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="80px"  ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                                <%--<ItemTemplate>
                                            <asp:Label ID="lblDesiName2" runat="server" CssClass="blink_me" Text='<%# Bind("search2") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                        
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HQ"  ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">

                                                <%--<ItemTemplate>
                                            <asp:Label ID="lblHQ2" runat="server" CssClass="blink_me" Text='<%# Bind("search3") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State"  ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                                <%--   <ItemTemplate>
                                            <asp:Label ID="lblstateName2" runat="server" CssClass="blink_me" Text='<%# Bind("search4") %>'></asp:Label>
                                        </ItemTemplate>--%>
                                        
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Promote" ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPromote" runat="server" CssClass="blink_me" Text='<%# Bind("search2") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="De-Promote"  ItemStyle-ForeColor="Red" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDepro" runat="server" CssClass="blink_me" Text='<%# Bind("search3") %>'></asp:Label>
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
              
                       <asp:Button ID="btnback" runat="server" CssClass="backbutton"  Text="Back" OnClick="btnback_Click" />  
              
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

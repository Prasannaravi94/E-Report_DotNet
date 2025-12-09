<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Territory.aspx.cs" Inherits="MasterFiles_MR_Territory" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Territory List</title> 

  <%-- <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../../css/MR.css" />--%>
    <style type="text/css">
        .modal
        {
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
        .loading
        {
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
        .align
        {
            min-width:150px;
        }

        .display-table .table tr td #grdTerritory_ctl02_tblTerritoryCnt tr td:first-child
        {
            background-color: #f1f5f8;
            font-weight: 400;
            color: #636D73;
        }
        /*.display-table .table tr td:first-child
        {
            nth-child(2)
        }*/
        .display-table .table tr td #grdTerritory_ctl03_tblTerritoryCnt tr td :first-child
        {
            color:#636D73;
        }
        /*.display-table .table tr td:first-child {
    background-color: #f1f5f8;
    text-align: center;
    border: 0px;
    padding: 15px 10px;
}*/
  
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
        function OpenNewWindow() {

            window.open('Territory_Help.aspx', null, 'height=400, width=300,top=0,left=0, status=no, resizable=yes, scrollbars=yes, toolbar=no,location=no, menubar=no');
            return false;
        }
    </script> 
   <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);

                    if ($('input:text')[curIndex].value == '') {
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
                            $('btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnGo').click(function () {
                var st = $('#<%=ddlSFCode.ClientID%> :selected').text();
                if (st == "---Select---") { alert("Select Field Force Name."); $('ddlSFCode').focus(); return false; }

            });
        }); 
    </script>
       <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
      <script type="text/javascript">
          $(function () {
              var $txt = $('input[id$=txtNew]');
              var $ddl = $('select[id$=ddlSFCode]');
              var $items = $('select[id$=ddlSFCode] option');

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
     <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
      <script type="text/javascript">
          $(function () {
              var $txt = $('input[id$=txtNew]');
              var $ddl = $('select[id$=ddlSFCode]');
              var $items = $('select[id$=ddlSFCode] option');

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
  <%--  <link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet"/>--%>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
<%--    <link href="" />--%>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <div id="Divid" runat="server">
        </div>

        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center ">
                <div class="col-lg-11">
                    <h2 class="text-center">Territory List</h2>
                    <div class="designation-reactivation-table-area clearfix">
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-2" style="margin-right: -52px;">
                                        <%--     <asp:Button ID="btnBack" CssClass="savebutton" Text="Back" runat="server" 
                                onclick="btnBack_Click" />--%>
                                        <%--<div style="margin-left:90%">
                                <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />
                                </div>    --%>

                                        <asp:Label ID="lblFilter" runat="server" Text="Field Force Name" CssClass="label"></asp:Label>
                                    </div>
                                </div>
                                <div class="row clearfix">
                                    <div class="col-lg-4">
                                        <asp:DropDownList ID="ddlSFCode"
                                            runat="server" CssClass="custom-select2 nice-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-1"  style="margin-left: -25px;">
                                        <asp:Button ID="btnGo" runat="server" Width="40px" Text="Go" CssClass="savebutton" OnClick="btnSubmit_Click" />
                                        <span id="SPl"
                                            style="font-family: Verdana; color: Red; font-weight: bold; display: none; width: 200px">Please Wait....</span>
                                        <asp:HiddenField ID="hdnBasedOn" runat="server" />
                                        <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name" Visible="false" SkinID="lblMand"></asp:Label>
                                    </div>
                                 <div class="col-lg-5"></div>
                               </div>
                            </div>
                        </div>
                        <br />
                        <div class="display-name-heading clearfix">
                            <div class="row clearfix">
                                <div class="col-lg-8">
                                    <asp:Button ID="btnNew" runat="server" CssClass="savebutton" Text="Add Territory" Width="100px" OnClick="btnNew_Click" />
                                    <%--  <asp:Button ID="btnDetailAdd" runat="server" CssClass="savebutton" Text="Detail Add" onClick="btnDetailAdd_Click" />&nbsp;--%>
                                    <asp:Button ID="btnEdit" runat="server" CssClass="resetbutton" Text="Edit All Territory" Width="120px"
                                        OnClick="btnEdit_Click" />
                                    <asp:Button ID="btnTranfer" runat="server" CssClass="resetbutton" Text="Transfer Territory" Width="125px"
                                        OnClick="btnTransfer_Click" />
                                    <asp:Button ID="btnSlNo_Gen" runat="server" CssClass="resetbutton" Width="75px" Text="S.No Gen" OnClick="btnSlNo_Gen_Click" />
                                    <asp:Button ID="btnreact" runat="server" CssClass="resetbutton" Width="95px"
                                        Text="Reactivation" OnClick="btnreact_Click" />
                                        <asp:Button ID="btndeact" runat="server" CssClass="resetbutton" Width="120px" 
                        Height="25px" Text="Bulk Deactivation" onclick="btndeact_Click"  />
                                </div>
                            </div>
                        </div>
                        <p>
                            <br />
                        </p>
                        <div class="display-table clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;overflow:inherit">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:GridView ID="grdTerritory" runat="server" Width="100%" HorizontalAlign="Center"
                                                    AllowSorting="true" OnSorting="grdTerritory_Sorting" EmptyDataText="No Records Found"
                                                    OnRowUpdating="grdTerritory_RowUpdating" OnRowEditing="grdTerritory_RowEditing"
                                                    OnPageIndexChanging="grdTerritory_PageIndexChanging" OnRowCreated="grdTerritory_RowCreated"
                                                    OnRowCancelingEdit="grdTerritory_RowCancelingEdit" OnRowCommand="grdTerritory_RowCommand"
                                                    OnRowDataBound="grdTerritory_RowDataBound" AutoGenerateColumns="false" AllowPaging="True"
                                                    PageSize="10 " GridLines="None" CssClass="table" PagerStyle-CssClass="gridview1"
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
                                                        <asp:TemplateField HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center" HeaderText="List (DR CNT) | (Chem Cnt) | (UnLst DR Cnt)"
                                                            SortExpression="Territory_Name">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtTerritory_Name" runat="server"  onkeypress="AlphaNumeric_NoSpecialChars_New(event)" CssClass="input" MaxLength="80"
                                                                    Text='<%# Bind("Territory_Name") %>'></asp:TextBox>
                                                                <asp:TableCell Width="30px">
                                                                    <asp:Label ID="lblListedDRCnt" runat="server" Visible="false" ForeColor="DarkBlue" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="30px">
                                                                    <asp:Label ID="lblChemistsCnt" runat="server" Visible="false" ForeColor="DarkMagenta" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:TableCell Width="30px">
                                                                    <asp:Label ID="lblUnListedDRCnt" runat="server" Visible="false" ForeColor="DarkGreen" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                                                </asp:TableCell>
                                                                <asp:RequiredFieldValidator ID="rfvDoc" runat="server" SetFocusOnError="true" ControlToValidate="txtTerritory_Name" ErrorMessage="*Required"></asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Table ID="tblTerritoryCnt" runat="server">
                                                                    <asp:TableRow>
                                                                        <asp:TableCell BorderStyle="None" Width="30px">
                                                                            <asp:Label ID="lblTerritory_Name" runat="server"  Width="300px" Text='<%# Bind("Territory_Name") %>'></asp:Label>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Width="30px">
                                                                            <asp:Label ID="lblListedDRCnt" runat="server" ForeColor="DarkBlue" Text='<%# Bind("ListedDR_Count") %>'></asp:Label>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Width="30px">
                                                                            <asp:Label ID="lblChemistsCnt" runat="server" ForeColor="DarkMagenta" Text='<%# Bind("Chemists_Count") %>'></asp:Label>
                                                                        </asp:TableCell>
                                                                        <asp:TableCell Width="30px">
                                                                            <asp:Label ID="lblUnListedDRCnt" runat="server" ForeColor="DarkGreen" Text='<%# Bind("UnListedDR_Count") %>'></asp:Label>
                                                                        </asp:TableCell>
                                                                    </asp:TableRow>
                                                                </asp:Table>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Short Name" SortExpression="Territory_Sname"
                                                            Visible="false">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtTerritory_Sname" CssClass="input" runat="server" MaxLength="3" onkeypress="AlphaNumeric_NoSpecialChars_New(event);"
                                                                    Text='<%# Bind("Territory_Sname") %>'></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTerritory_Sname" runat="server" Text='<%# Bind("Territory_Sname") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblType" runat="server" Text='<%# Bind("Territory_Cat") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="Territory_Type" runat="server" CssClass="nice-select">
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


                                                          <asp:TemplateField HeaderText="Territory Visit" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTerritory_Visit" runat="server" Text='<%# Bind("Territory_Visit") %>'></asp:Label>
                                                            </ItemTemplate>                                                         
                                                        </asp:TemplateField>


                                                        <asp:HyperLinkField HeaderText="Detail Add" Text="Detail Add" DataNavigateUrlFormatString="Territory_Detail.aspx?Territory_Code={0}"
                                                            DataNavigateUrlFields="Territory_Code" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="align">
                                                           <%-- <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center" Width="90px"></ItemStyle>--%>
                                                        </asp:HyperLinkField>
                                                        <%--<asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="align"
                                                            ShowEditButton="True">
                                                           <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                                                Font-Bold="True"></ItemStyle>
                                                        </asp:CommandField>--%>
                                                        <asp:TemplateField HeaderText="Deactivate">
                                                            <%--<ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False" HorizontalAlign="Center"></ItemStyle>--%>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Territory_Code") %>'
                                                                    CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Territory');">Deactivate
                                                                </asp:LinkButton>
                                                                <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                                               <img src="../../../Images/deact2.png" alt="" width="85px" title="Kindly Transfer the Territory then deactivate" />
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="no-result-area"/>
                                                </asp:GridView>
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
        <br /><br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../../Images/loader.gif" alt="" />
        </div>
    </div>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>--%>
 <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/js/bootstrap-select.min.js"></script>
    </form>
</body>
</html>

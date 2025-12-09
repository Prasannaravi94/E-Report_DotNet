<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reactivate_Gifts.aspx.cs"
    Inherits="MasterFiles_Reactivate_Gifts" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reactivate Inputs</title>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
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

        .div_fixedReactBtn {
            position: fixed;
            top: 486px;
            right: 7px;
            height: 24px;
        }

        .div_fixedDeactBtn {
            position: fixed;
            top: 516px;
            right: 7px;
            height: 24px;
        }

        .div_fixed_Grid {
            position: fixed;
            top: 360px;
            right: 675px;
        }

        .div_fixed_Grid_No {
            position: fixed;
            top: 188px;
            right: 500px;
        }

        .popuph1 {
            color: #292a34;
            font-size: 24px;
            font-weight: 700;
            margin-bottom: 50px;
        }

        .popuplabel {
            color: #696d6e;
            font-size: 12px;
        }

        .marginright {
            margin-left: 95%;
            width: 5%;
        }
        [type="checkbox"]:not(:checked) + label, [type="checkbox"]:checked + label {
        position: relative;
        padding-left: 1.15em!important;
        cursor: pointer;
        vertical-align: top;
        line-height: 20px;
        margin: 2px 0;
        display: inline-block!important;
       }
    </style>
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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <%--    <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }

        //        function checkActive(objRefs) {
        //            var GridView = objRefs.parentNode.parentNode.parentNode;
        //            var inputList = GridView.getElementsByTagName("input");
        //            for (var i = 0; i < inputList.length; i++) {
        //                //Get the Cell To find out ColumnIndex
        //                var row = inputList[i].parentNode.parentNode;

        //             if (inputList[i].type == "checkbox" && objRefs != inputList[i])
        //                {
        //                    if (objRefs.checked) {
        //                       
        ////                        document.getElementById("btnActivate").style.visibility = "hidden";
        ////                         btnActivate.disabled = 'false';
        //                    }
        //                     else {
        //                       
        ////                         document.getElementById("btnActivate").style.visibility = "visible";
        ////                        btnActivate.disabled = 'true';
        //                    }
        //                }
        //            }
        //        }
    </script>
    <script src="http://code.jquery.com/jquery-1.8.2.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <%-- <link type="text/css" rel="Stylesheet" href="../../css/style.css" />--%>
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

            $('#btnSave').click(function () {
                if ($("#txtEffTo").val() == "") { alert("Enter Eff. To Date."); $('#txtEffTo').focus(); return false; }
            });
        });
    </script>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" style="border-style: none">Reactivate Inputs</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">
                                <div class="row clearfix">
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblType" runat="server" CssClass="label" Text="Search By"></asp:Label>
                                        <asp:DropDownList ID="ddlSrch" runat="server" CssClass="nice-select" TabIndex="1" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged">
                                            <asp:ListItem Text="ALL" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Gift Name" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="State" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Only Deactivated" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Only Eff.To Date Closed" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Bulk Deactivate" Value="6"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2" style="padding-top: 19px; padding-left: 0px;">
                                        <div class="single-des clearfix">
                                            <asp:TextBox ID="txtsearch" runat="server" Visible="false" CssClass="input" Width="100%"></asp:TextBox>
                                        </div>
                                        <div style="margin-top: -20px">
                                            <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" onfocus="this.style.backgroundColor='#E0EE9D'"
                                                CssClass="nice-select" TabIndex="4">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-1" style="margin: 20px; margin-left: -25px;">
                                        <asp:Button ID="Btnsrc" runat="server" Visible="false" CssClass="savebutton" Text="Go"
                                            OnClientClick="return ProcessData()" OnClick="Btnsrc_Click" Style="width: 50px;" />
                                    </div>
                                    <%--OnClick="Btnsrc_Click"--%>
                                    <%--     <td width="40%" align="center">
                                <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                                    runat="server" Width="55%" HorizontalAlign="center">
                                    <SeparatorTemplate>
                                    </SeparatorTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnAlpha" ForeColor="#8A2EE6" runat="server" CommandArgument='<%#bind("Gift_Name") %>'
                                            Text='<%#bind("Gift_Name") %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:DataList>
                                    </td>--%>
                                    <div class="col-lg-3"></div>
                                    <div class="col-lg-3">
                                        <asp:Label ID="lblEffDt" runat="server" CssClass="label" Text="Effective From Date"></asp:Label>
                                        <asp:DropDownList ID="Ddl_EffDate" runat="server" AutoPostBack="true" CssClass="nice-select"
                                            OnSelectedIndexChanged="Ddl_EffDate_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <asp:GridView ID="grdGift" runat="server" Width="100%" HorizontalAlign="Center" AutoGenerateColumns="false"
                                        AllowPaging="True" PageSize="25" OnRowUpdating="grdGift_RowUpdating" OnRowEditing="grdGift_RowEditing"
                                        OnPageIndexChanging="grdGift_PageIndexChanging" OnRowCreated="grdGift_RowCreated"
                                        OnRowCancelingEdit="grdGift_RowCancelingEdit" OnRowCommand="grdGift_RowCommand"
                                        EmptyDataText="No Records Found" OnRowDataBound="grdGift_RowDataBound" GridLines="None"
                                        CssClass="table" PagerStyle-CssClass="GridView1"
                                        AllowSorting="True" OnSorting="grdGift_Sorting">
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemStyle Width="40px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%# (grdGift.PageIndex * grdGift.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text="." onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemStyle Width="90px" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkGifts" runat="server"  Text="." onclick="checkActive(this);" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Gift_Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftCode" runat="server" Text='<%#Eval("Gift_Code")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Gift_Name" HeaderText="Name"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="170px" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="GiftName" runat="server" CssClass="input" MaxLength="150"
                                                        Text='<%# Bind("Gift_Name") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftName" runat="server" Text='<%# Bind("Gift_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Gift_SName" HeaderText="Short Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="140px" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGiftSN" CssClass="input" runat="server" MaxLength="15"
                                                        Text='<%# Bind("Gift_SName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftSN" runat="server" Width="100px" Text='<%# Bind("Gift_SName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField SortExpression="Gift_Type" HeaderText="Type" ItemStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="130px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftType" runat="server" Text='<%# Bind("Gift_Type") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlGiftType" runat="server" SkinID="ddlRequired">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Literature/Lable" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Special Gift" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Doctor Kit" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Ordinary Gift" Value="4"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value" Visible="false">
                                                <ItemStyle Width="70px" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtGiftval" CssClass="input" Width="60px" runat="server"
                                                        MaxLength="5" Text='<%# Bind("Gift_Value") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGiftVal" runat="server" Text='<%# Bind("Gift_Value") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Effe.To.Dt">
                                                <ItemStyle Width="90px" />
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEffToDate" CssClass="input" Width="90px" runat="server"
                                                        Text='<%# Bind("Gift_Effective_To") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEffToDate" runat="server" Text='<%# Bind("Gift_Effective_To") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Active Flag" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFlag" runat="server" Text='<%# Bind("Gift_Active_Flag") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gift Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGift_Code" runat="server" Text='<%# Bind("Gift_Code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                <ItemStyle Width="120px" ForeColor="DarkBlue" Font-Bold="true" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:CommandField ShowHeader="True" EditText="Inline Edit" HeaderText="Inline Edit"
                                    HeaderStyle-HorizontalAlign="CENTER" ShowEditButton="True">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle ForeColor="DarkBlue" HorizontalAlign="Center" Font-Size="XX-Small" Font-Names="Verdana"
                                        Font-Bold="True"></ItemStyle>
                                </asp:CommandField>--%>
                                            <asp:HyperLinkField HeaderText="Edit/Reactivate" Text="Edit/Reactivate" ItemStyle-HorizontalAlign="Center"
                                                DataNavigateUrlFormatString="ProductReminder.aspx?Gift_Code={0}" DataNavigateUrlFields="Gift_Code">
                                                <%--<ItemStyle Width="100px" Font-Bold="False"></ItemStyle>
                                            <ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                            <ItemStyle ForeColor="DarkBlue" Font-Bold="False"></ItemStyle>--%>
                                            </asp:HyperLinkField>
                                            <asp:TemplateField HeaderText="Deactivate">
                                                <%--<ControlStyle ForeColor="DarkBlue" Font-Size="XX-Small" Font-Names="Verdana" Font-Bold="True"></ControlStyle>
                                            <ItemStyle ForeColor="DarkBlue" Width="100px" Font-Bold="False"></ItemStyle>--%>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbutReactivate" runat="server" CommandArgument='<%# Eval("Gift_Code") %>'
                                                        CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Input');">Deactivate
                                                    </asp:LinkButton>
                                                    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false">                                        
                                               <img src="../Images/deact2.png" alt="" width="75PX" title="This Gift Already Deactivated" />
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                </div>
            </div>
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../Images/loader.gif" alt="" />
            </div>
        </div>
        <div class="div_fixedReactBtn">
            <asp:Button ID="btnActivate" runat="server" Text="Re-Activate" Width="90px"
                CssClass="savebutton" />
        </div>
        <div class="div_fixedReactBtn">
            <asp:Button ID="btnDeActivate" runat="server" Text="De-Activate" Width="90px"
                CssClass="savebutton" Visible="false" OnClick="btnDeActivate_Click" />
        </div>
        <table>
            <td>
                <asp:ModalPopupExtender ID="mpeSave" runat="server" PopupControlID="pnlPopup" TargetControlID="btnActivate"
                    OkControlID="btnNo" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
            </td>
        </table>


        <%--<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#basicExampleModal">
  Launch demo modal
</button>

<!-- Modal -->
<div class="modal fade" id="basicExampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel"
  aria-hidden="true">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Modal title</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Save changes</button>
      </div>
    </div>
  </div>
</div>--%>


        <table align="center">
            <tr>
                <td>
                    <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none; width: 30%; height: 45%; overflow: scroll; scrollbar-width: thin;">
                        <%--OnClick="btnYes_Click"--%>
                        <asp:Button ID="btnNo" runat="server" CssClass="marginright" Text="x" BackColor="#F1F5F8" />
                        <div class="row justify-content-center">
                            <div class="col-lg-9">
                                <asp:Label ID="Label11" runat="server" CssClass="popuph1" Text="">" Effective To Date "- Closed</asp:Label>
                            </div>

                        </div>
                        <br />
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-9">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblEffTo" runat="server" Width="150px" CssClass="popuplabel">Effective To<span style="Color:Red;padding-left:5px;">*</span></asp:Label>
                                    <asp:TextBox ID="txtEffTo" runat="server" CssClass="input" Width="100%"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEffTo" CssClass="cal_Theme1" Format="dd/MM/yyyy" />
                                </div>
                            </div>
                        </div>
                        <br />
                        <center>
                            <asp:Button ID="btnSave" runat="server" CssClass="savebutton" OnClick="btnsave_Click" Text="Update"
                                UseSubmitBehavior="false" Width="70px" />
                        </center>
                        <br />
                        <br />
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

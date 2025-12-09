<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Notfication_Msg.aspx.cs" Inherits="Notfication_Msg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notification Message</title>

    <%--   <link href="JScript/BootStrap/dist/css/bootstrap.css" rel="stylesheet" type="text/css" />--%>
    <link href="JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <%--    <link href="/../netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="/../cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />--%>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <link type="text/css" rel="stylesheet" href="//cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {

            var ddlVal = $("#ddlwise").val();
            if (ddlVal == "1") {
                $("#desig").css("display", "block");
                $("#state").css("display", "none");
                $("#sub").css("display", "none");
                $("#field").css("display", "none");
            }
            else if (ddlVal == "2") {
                $("#desig").css("display", "none");
                $("#state").css("display", "block");
                $("#sub").css("display", "none");
                $("#field").css("display", "none");

            }
            else if (ddlVal == "3") {
                $("#desig").css("display", "none");
                $("#state").css("display", "none");
                $("#sub").css("display", "block");
                $("#field").css("display", "none");

            }
            else if (ddlVal == "4") {
                $("#desig").css("display", "none");
                $("#state").css("display", "none");
                $("#sub").css("display", "none");
                $("#field").css("display", "block");
            }
            else {
                $("#desig").css("display", "none");
                $("#state").css("display", "none");
                $("#sub").css("display", "none");
                $("#field").css("display", "block");
            }
        });


        $(function () {
            $("#ddlwise").change(function () {
                var ddlVal = $("#ddlwise").val();
                if (ddlVal == "1") {
                    $("#desig").css("display", "block");
                    $("#state").css("display", "none");
                    $("#sub").css("display", "none");
                    $("#field").css("display", "none");
                }
                else if (ddlVal == "2") {
                    $("#desig").css("display", "none");
                    $("#state").css("display", "block");
                    $("#sub").css("display", "none");
                    $("#field").css("display", "none");

                }
                else if (ddlVal == "3") {
                    $("#desig").css("display", "none");
                    $("#state").css("display", "none");
                    $("#sub").css("display", "block");
                    $("#field").css("display", "none");

                }
                else if (ddlVal == "4") {
                    $("#desig").css("display", "none");
                    $("#state").css("display", "none");
                    $("#sub").css("display", "none");
                    $("#field").css("display", "block");

                }
                else {
                    $("#desig").css("display", "none");
                    $("#state").css("display", "none");
                    $("#sub").css("display", "none");
                    $("#field").css("display", "block");

                }
            });
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
            $('#btnSubmit').click(function () {

                if ($("#txtmsg").val() == "") { alert("Enter Notification Message."); $('#txtmsg').focus(); return false; }
				var SpeIgnore = /^[^\"\\']+$/;
				if (SpeIgnore.test($("#txtmsg").val()) == false) { alert("Notification Message should allow other special character. Except Backslash, single quotes and double quotes."); $('#txtmsg').focus(); return false; }
                if ($("#txtEffFrom").val() == "") { alert("Enter Effective From."); $('#txtEffFrom').focus(); return false; }
                if ($("#txtEffTo").val() == "") { alert("Enter Effective To."); $('#txtEffTo').focus(); return false; }

            });


        });
    </script>



    <style type="text/css">
        body {
            font-size: 12pt;
        }

        .Grid th {
            color: #fff;
            background-color: #A6A6D2;
        }
        /* CSS to change the GridLines color */
        .Grid, .Grid th, .Grid td {
            font-size: small;
            font-family: Calibri;
            font-weight: bold;
            border: 1px solid;
        }

        .GridHeader {
            text-align: center !important;
        }

        .hover {
            cursor: text;
        }

        #grdnotify [type="checkbox"]:not(:checked) + label, #grdnotify [type="checkbox"]:checked + label {
            padding-left: 0.15em;
            color: white;
        }

        .table th:nth-child(2) {
            min-width: 40px;
        }

        .selectpicker > ul {
            display: none;
        }

        .selectpicker > span {
            display: none;
        }

        .bodycolor {
            background: none !important;
            background-color: #fafdff !important;
        }

        .bootstrap-select:not([class*="col-"]):not([class*="form-control"]):not(.input-group-btn) {
            width: 295px !important;
            color: #90a1ac;
            font-size: 14px !important;
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
            padding-left: 3px;
            padding-right: 3px;
        }

        #desig .nice-select, #state .nice-select, #sub .nice-select {
            display: none;
        }

        #bs-select-2 {
            scrollbar-width: thin;
        }

        .single-des .dropdown-item, .single-des .show > .btn-light.dropdown-toggle, .single-des .btn-light, .single-des .btn-light:hover {
            color: #90a1ac;
        }
      
    </style>
    <%--  <script type="text/javascript">
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
          
    </script>--%>

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
        <script type="text/javascript">
            function checkAll() {
                var grid = document.getElementById('<%= grdnotify.ClientID %>');
                if (grid != null) {
                    var inputList = grid.getElementsByTagName("input");
                    var cnt = 0;
                    var counter = 0;
                    var index = '';
                    var chkall = document.getElementById('grdnotify_ctl01_checkAll');

                    for (i = 2; i < inputList.length + 1; i++) {
                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {
                            index = i.toString();
                        }
                        var chkapp = document.getElementById('grdnotify_ctl' + index + '_chkSf');


                        if (chkall.checked) {

                            chkapp.checked = true;


                            $("#<%=grdnotify.ClientID%> input[id*='checkAll']:checkbox").each(function (index) {
                                if ($(this).is(':checked'))

                                    counter++;

                            });


                        }

                        else {
                            chkapp.checked = false;
                        }
                    }
                    document.getElementById('lblfieldcnt').textContent = "No.Of fieldforce Selected:";
                    document.getElementById('lblSelectedCount').textContent = counter;

                    // alert(counter);
                }
            }
            function checkapp() {
                var grid = document.getElementById('<%= grdnotify.ClientID %>');

                if (grid != null) {

                    var inputList = grid.getElementsByTagName("input");

                    var cnt = 0;
                    var counter2 = 0;
                    var index = '';

                    for (i = 2; i < inputList.length + 1; i++) {

                        if (i.toString().length == 1) {
                            index = cnt.toString() + i.toString();
                        }
                        else {

                            index = i.toString();
                        }

                        var chkapp = document.getElementById('grdnotify_ctl' + index + '_chkSf');
                        var chkall = document.getElementById('grdnotify_ctl01_checkAll');

                        if (chkapp.checked) {

                            counter2++;

                        }
                        else {

                            chkall.checked = false;
                        }
                    }

                    document.getElementById('lblfieldcnt').textContent = "No.Of fieldforce Selected:";
                    document.getElementById('lblSelectedCount').textContent = counter2;

                }
            }
        </script>




        <script type="text/javascript">
            function showModalPopUp() {

                popUpObj = window.open("preview_link.aspx?",
        "ModalPopUp"//,
        //"toolbar=no," +
        //"scrollbars=yes," +
        //"location=no," +
        //"statusbar=no," +
        //"menubar=no," +
        //"addressbar=no," +
        //"resizable=yes," +
        //"width=700," +
        //"height=700," +
        //"left = 0," +
        //"top=100"
        );
                popUpObj.focus();
                //LoadModalDiv();
            }

            function showModalPopUpDelete() {

                popUpObj = window.open("NotificationMsg_delete.aspx?",
        "ModalPopUp"//,
        //"toolbar=no," +
        //"scrollbars=yes," +
        //"location=no," +
        //"statusbar=no," +
        //"menubar=no," +
        //"addressbar=no," +
        //"resizable=yes," +
        //"width=700," +
        //"height=700," +
        //"left = 0," +
        //"top=100"
        );
                popUpObj.focus();
                //LoadModalDiv();
            }
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $('#lnkpreview').click(function () {

                    showModalPopUp();
                });
                $('#lnkdeleteMsg').click(function () {

                    showModalPopUpDelete();
                });
            });
        </script>
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-12">

                        <h2 class="text-center" runat="server">
                            <asp:Label ID="lblhead" runat="server" Text="Notification Message"></asp:Label>
                        </h2>

                        <table align="left" style="width: 9px">
                            <tr align="center">
                                <td align="center">
                                    <span runat="server" id="Span1" style="border-style: none; font-family: Verdana; font-size: 14px; border-color: #E0E0E0; color: #8A2EE6">
                                        <asp:LinkButton ID="lnk2" ForeColor="#e0f3ff" runat="server" Text="." OnClick="lnk2_Click" Font-Underline="true" Visible="false"></asp:LinkButton></span>
                                </td>
                            </tr>
                        </table>
                        <table align="right">
                            <tr align="center">
                                <td align="center">
                                    <%--<span runat="server" id="levelset" style="border-style:none; font-family:Verdana; font-size:14px; border-color:#E0E0E0; color :#8A2EE6"><asp:LinkButton ID="lnk" ForeColor="#e0f3ff" runat="server" Text="." onclick="lnk_Click" Font-Underline="false"></asp:LinkButton></span>--%>
                                </td>
                            </tr>
                        </table>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-1">
                                    </div>
                                    <div class="col-lg-3">
                                       <%-- <span class="label">Filter<asp:LinkButton ID="lnk" class="label" ForeColor="white" Font-Underline="false" runat="server"
                                            Text="o" OnClick="lnk_Click" style="cursor: auto;">.</asp:LinkButton>By</span>--%>
                                        <asp:Label ID="lblselect" runat="server" Text="Filter By" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlwise" runat="server" CssClass="nice-select" AutoPostBack="true">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Designtion Wise" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="State" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Sub DivisionWise" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="FieldForce (Team Wise)" Value="4" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4" style="padding-top: 29px">
                                        <div id="desig" >
                                            <asp:ListBox ID="lstDesig" runat="server" SelectionMode="Multiple" CssClass="selectpicker" OnCheckedChanged="chkLevelAll_CheckedChanged" Width="100%"></asp:ListBox>
                                        </div>
                                        <div id="state">
                                            <asp:ListBox ID="lstState" runat="server" SelectionMode="Multiple" CssClass="selectpicker" Width="100%"></asp:ListBox>
                                        </div>
                                        <div id="sub">
                                            <asp:ListBox ID="lstsubdiv" runat="server" SelectionMode="Multiple" CssClass="selectpicker" Width="100%"></asp:ListBox>
                                        </div>
                                        <div id="field">
                                            <asp:DropDownList ID="lstfieldforce" runat="server" CssClass="custom-select2  nice-select" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3" style="padding-top: 29px">
                                        <asp:Button ID="btngo" runat="server" Text="GO" CssClass="savebutton" Width="60px" OnClick="btn_goclick" />&nbsp
                                        <asp:LinkButton ID="lnkpreview" runat="server" Text="Preview" OnClick="btnlink_click" Font-Size="14px"></asp:LinkButton>
                                                        &nbsp;&nbsp;
                             <asp:LinkButton ID="lnkdeleteMsg" runat="server" Text="Delete" ForeColor="red" Font-Size="14px"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div align="center">
                            <asp:Label ID="lblfieldcnt" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblSelectedCount" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </div>
                        <br />
                        <div class="display-reportMaintable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin">
                                <asp:GridView ID="grdnotify" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-Font-Bold="true"
                                    GridLines="None" HeaderStyle-CssClass="Hprint" CssClass="table"
                                    HeaderStyle-HorizontalAlign="Left" Width="100%" EmptyDataText="No Records Found">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%# (grdnotify.PageIndex * grdnotify.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center">
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" Text="." />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSf" runat="server" onclick="checkapp(this);" Text="." />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_Code" Visible="false" runat="server" Text='<% #Eval("sf_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="300px" HeaderText="FieldForce Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_Name" runat="server" Text='<% #Eval("sf_name") %>' Width="230px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Designation" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation_Short_Name" runat="server" Text='<% #Eval("Designation_Short_Name") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="HQ" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_HQ" runat="server" Text='<% #Eval("Sf_HQ") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="State" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstate" runat="server" Text='<% #Eval("StateName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBackcolor" runat="server" Text='<% #Eval("des_color") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" HeaderText="Sub Div" HeaderStyle-Width="150px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsub" runat="server" Text='<% #Eval("sub") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">
                                        <span runat="server" class="label" id="Span3" visible="false">Notificati<a href="Options/TPSetup.aspx" style="color:#696d6e">o</a>n Message :</span>

                                        <asp:TextBox ID="txtmsg" runat="server" TextMode="MultiLine" Height="100px" Width="100.5%" Visible="false" CssClass="input" ondrop="return false;" oncopy="return false" onpaste="return false" oncut="return false"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">
                                <div class="designation-area clearfix">
                                    <div class="single-des clearfix">

                                        <asp:Label ID="lbleffeFrom" runat="server" CssClass="label" Text="Effective From" Visible="false"></asp:Label><br />
                                        <asp:TextBox ID="txtEffFrom" runat="server" CssClass="input" onkeypress="Calendar_enter(event);" Visible="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="txtEffFrom" CssClass="cal_Theme1"
                                            runat="server">
                                        </asp:CalendarExtender>
                                    </div>
                                    <div class="single-des clearfix">
                                        <asp:Label ID="lblEffTo" runat="server" CssClass="label" Text="Effective To" Visible="false"></asp:Label><br />
                                        <asp:TextBox ID="txtEffTo" runat="server" CssClass="input" onkeypress="Calendar_enter(event);" Visible="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="txtEffTo" CssClass="cal_Theme1"
                                            runat="server">
                                        </asp:CalendarExtender>
                                    </div>

                                    <div class="single-des clearfix">
                                        <span runat="server" id="Span2" style="border-style: none; font-family: Verdana; font-size: 14px; border-color: #E0E0E0; color: #8A2EE6">
                                            <asp:LinkButton ID="linkthree" ForeColor="#e0f3ff" runat="server" Text="." OnClick="lnk3_Click" Font-Underline="false" Visible="false"></asp:LinkButton></span>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <br />
                        <center>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="savebutton" Visible="false"
                                OnClick="btnSubmit_Click" />
                       </center>



                    </div>
                </div>
            </div>
            <br />
            <br />

        </div>
        <%--   <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
        <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/js/bootstrap-select.min.js"></script>
        <link type="text/css" href="../css/multiple-select.css" rel="Stylesheet" />
        <script type="text/javascript" src="../JsFiles/multiple-select_2.js"></script>
        <script type="text/javascript">

            $('[id*=lstDesig]').multipleSelect();
            $('[id*=lstState]').multipleSelect();
            $('[id*=lstsubdiv]').multipleSelect();


        </script>--%>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/js/bootstrap-select.min.js"></script>
    </form>
</body>
</html>

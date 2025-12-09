<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_admin_Approval.aspx.cs" Inherits="MasterFiles_MGR_Listeddr_admin_Approval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Listed Doctor Addition - Approval</title>
    <meta name="description" content="">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/png" href="../../images/logo.png" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css">
    <link rel="stylesheet" href="../../assets/css/nice-select.css">
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/css/style.css">
    <link rel="stylesheet" href="../../assets/css/responsive.css">
     <link href="../../../assets/css/Calender_CheckBox.css" rel="stylesheet" type="text/css" />
    <!--[if IE]><script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script><![endif]-->
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>

    <style type="text/css">
       
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray;
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

        .buttons {
            width: 165px !important;
        }
         [type="checkbox"]:not(:checked) + label, [type="checkbox"]:checked + label
           {
               padding-left: 0;
           }
         #btnBack{
            margin-left : 0px
        }
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        //row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original 
                        //                    if (row.rowIndex % 2 == 0) {
                        //                        //Alternating Row Color
                        //                        row.style.backgroundColor = "#C2D69B";
                        //                    }
                        //                    else {
                        //                        row.style.backgroundColor = "white";
                        //                    }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <div style="margin-left: 90%">
                <asp:Button ID="btnBack" runat="server" CssClass="savebutton" Text="Back" OnClick="btnBack_Click" />
            </div>
            <br />
            <center>
                <div class="container home-section-main-body position-relative clearfix">
                    <div class="row justify-content-center">
                        <div class="col-lg-11">
                            <div class="designation-reactivation-table-area clearfix">
                                <div class="display-table clearfix">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdDoctor" runat="server" HorizontalAlign="Center"
                                            AutoGenerateColumns="false" GridLines="None" AllowPaging="True"
                                            CssClass="table" OnRowCreated="grdDoctor_RowCreated"
                                            AllowSorting="True" OnSorting="grdDoctor_Sorting">
                                            <Columns>
                                               <%-- <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" Text="Select All" CssClass="CheckBoxlabelColor" onclick="checkAll(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkListedDR" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Select" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkListedDR" runat="server" Text="."/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Listed Doctor Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocCode" runat="server" Text='<%#Eval("ListedDrCode")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Listed Doctor Name" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocName" runat="server" Text='<%#Eval("ListedDr_Name")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deactivated Date" HeaderStyle-ForeColor="Black" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeact_Date" runat="server" Text='<%#Eval("ListedDr_Deactivate_Date")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Speciality" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpl" runat="server" Text='<%# Bind("Doc_Special_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcat" runat="server" Text='<%# Bind("Doc_Cat_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qualification" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQua" runat="server" Text='<%# Bind("Doc_QuaName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Class" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCls" runat="server" Text='<%# Bind("Doc_ClsName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Territory" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblterr" runat="server" Text='<%# Bind("territory_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div class="loading" align="center">
                                    Loading. Please wait.<br />
                                    <br />
                                    <img src="../../Images/loader.gif" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </center>
        </div>
        <br />
        <center>
            <asp:Button ID="btnApprove" runat="server" CssClass="savebutton buttons" Text="Approve - Add Lst Drs" OnClick="btnApprove_Click" />&nbsp;
            <asp:Button ID="btnReject" runat="server" CssClass="savebutton" Text="Reject" OnClick="btnReject_Click" />
        </center>
    </form>
    <script src="../../assets/js/jQuery.min.js"></script>
    <script src="../../assets/js/popper.min.js"></script>
    <script src="../../assets/js/bootstrap.min.js"></script>
    <script src="../../assets/js/jquery.nice-select.min.js"></script>
    <script src="../../assets/js/main.js"></script>
</body>
</html>

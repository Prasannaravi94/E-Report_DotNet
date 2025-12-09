<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit_Quiz_Questions.aspx.cs"
    Inherits="MasterFiles_Options_Edit_Quiz_Questions" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Quiz Question List</title>
    <%--<link href="../../css/style.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: gray !important;
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
            }, 1000);
        }
        $('#form').live("submit", function () {
            ShowProgress();
        });
    </script>
    <%--<style type="text/css">
        .NewBtn
        {
            background: #25A6E1;
            background: -moz-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#25A6E1),color-stop(100%,#188BC0));
            background: -webkit-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -o-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -ms-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            filter: progid: DXImageTransform.Microsoft.gradient( startColorstr='#25A6E1',endColorstr='#188BC0',GradientType=0);
            padding: 5px 10px;
            color: #fff;
            font-family: 'Helvetica Neue' ,sans-serif;
            font-size: 15px;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border: 1px solid #1A87B9;
            cursor: pointer;
        }
        .NewBtn:hover
        {
            color: Black;
        }
        .panelbtn
        {
            float: right;
            margin-right: 100px;
        }
    </style>
   
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link href="../../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet"
        type="text/css" />
    <link href="../../JScript/BootStrap/dist/css/GridTable.css" rel="stylesheet" type="text/css" />--%>

    <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../JScript/Service_CRM/Quiz_JS/EditQuiz_JS.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div>
            <asp:HiddenField ID="hidSurveyId" runat="server" />
            <br />
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-11">
                        <br />
                        <h2 class="text-center" style="border-bottom: none !important;" id="hHeading" runat="server"></h2>

                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-table clearfix">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvEditQuiz" runat="server" OnRowCreated="grdSurveyQuestions_RowCreated"
                                        AutoGenerateColumns="False" HorizontalAlign="Center" GridLines="None" Width="100%" AllowSorting="true" CellPadding="2"
                                        CellSpacing="0" OnRowCommand="grdSurveyQuestions_RowCommand" OnRowDeleting="grdSurveyQuestions_RowDeleting"
                                        ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" EmptyDataRowStyle-ForeColor="Red"
                                        EmptyDataRowStyle-BackColor="White" EmptyDataRowStyle-Font-Size="Small" CssClass="table">
                                        <%--<FooterStyle BackColor="#86AEFC" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#4D89BD" ForeColor="White" Height="12px" Font-Bold="false"
                    Font-Size="Small" HorizontalAlign="Left" Font-Names="Verdana" />
                <RowStyle BackColor="#EFF3FB" Font-Names="Verdana" Font-Size="13px" ForeColor="#400000" />
                <EditRowStyle HorizontalAlign="Left" CssClass="gvedit" />
                <SelectedRowStyle BackColor="#ADAEB4" ForeColor="GhostWhite" />
                <AlternatingRowStyle BackColor="White" />--%>
                                        <Columns>
                                            <asp:BoundField DataField="Question_type_id" HeaderText="Code" Visible="false"></asp:BoundField>
                                            <asp:BoundField DataField="Question_Id" HeaderText="Code" Visible="false">
                                                <ItemStyle BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                                                    VerticalAlign="Middle" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <span class="hiddenfield">
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        <asp:HiddenField ID="key" runat="server" Value='<%#Eval("Question_Id") %>'></asp:HiddenField>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Question_Type_Name" HeaderText="Question Type"></asp:BoundField>
                                            <%--  <asp:BoundField DataField="Question_Text" HeaderText="Question">
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                    </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Question" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuestion" runat="server" Text='<%# Eval("Question_Text") %>'
                                                        CssClass="input" Style="height: 100px !important;" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Answer Choice" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Panel ID="pnlAnswerOptions" runat="server">
                                                    </asp:Panel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="Input_Text" HeaderText="Correct Ans">
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                    </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Correct Ans" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCorrectAns" CssClass="CorectAns" runat="server" Text='<%#Eval("Input_Text") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="LinkButton1" runat="server" ToolTip="Delete question" CommandArgument='<%# Eval("Question_Id") %>'
                                                        ImageUrl="~/images/Delete.png" CommandName="Delete" OnClientClick="return confirm('Do you want to delete this Question?');" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="savebutton" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:Button ID="btnBack" runat="server" CssClass="backbutton" Text="Back" OnClick="btnBack_Click" />
                </div>
            </div>
        </div>
        <%-- <div>
            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
            <asp:Table ID="Table1" runat="server">
            </asp:Table>
        </div>--%>
        <br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
        </div>
    </form>
</body>
</html>

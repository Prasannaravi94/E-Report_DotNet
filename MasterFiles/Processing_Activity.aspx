<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Processing_Activity.aspx.cs" Inherits="MasterFiles_Processing_Activity" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<%--    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>--%>
    <script type="text/javascript">
        function SelectAllCheckboxes(chk, selector) {
            $('#<%=grdActUpload.ClientID%>').find(selector + " input:checkbox").each(function () {
                $(this).prop("checked", $(chk).prop("checked"));
            });
            //if ($(chk).prop("checked")) {
            //    $("#btnSave").show();
            //} else {
            //    $("#btnSave").hide();
            //}
        }
    </script>
    <script type="text/javascript">
        function SelectCheckboxes(chk) {
            var i = 0;
            $('#<%=grdActUpload.ClientID%>').find(".AllTick input:checkbox").each(function () {
                i += $(this).prop("checked") ? 1 : 0;
            });
            //if (i > 0) {
              //  $("#btnSave").show();
               // $("#chkTick").prop("checked",true);
            //} else {
               // $("#btnSave").hide();
                //$("#chkTick").prop("checked",false);
            //}
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Processing Activity</h2>

                        <div class="designation-area clearfix">

                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblMoth" runat="server" Text="Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtMonthYear" runat="server" CssClass="nice-select"></asp:TextBox>
                                </div>
                                <div style="float: right; width: 50%;">
                                    <br />
                                    <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" OnClick="btnGo_Click" />
                                </div>
                            </div>
                            <asp:Panel ID="pnlFFDate" runat="server" Visible="false">
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblFilter" runat="server" Text="Field Force Name" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                                <div class="single-des clearfix">
                                    <div style="float: left; width: 45%;">
                                        <asp:Label ID="lblDate" runat="server" Text="Date" CssClass="label"></asp:Label>
                                        <asp:DropDownList ID="ddlDate" runat="server" Width="100%" CssClass="custom-select2 nice-select">
                                            <asp:ListItem Value="--All--"></asp:ListItem>
                                            <asp:ListItem Value="1"></asp:ListItem>
                                            <asp:ListItem Value="2"></asp:ListItem>
                                            <asp:ListItem Value="3"></asp:ListItem>
                                            <asp:ListItem Value="4"></asp:ListItem>
                                            <asp:ListItem Value="5"></asp:ListItem>
                                            <asp:ListItem Value="6"></asp:ListItem>
                                            <asp:ListItem Value="7"></asp:ListItem>
                                            <asp:ListItem Value="8"></asp:ListItem>
                                            <asp:ListItem Value="9"></asp:ListItem>
                                            <asp:ListItem Value="10"></asp:ListItem>
                                            <asp:ListItem Value="11"></asp:ListItem>
                                            <asp:ListItem Value="12"></asp:ListItem>
                                            <asp:ListItem Value="13"></asp:ListItem>
                                            <asp:ListItem Value="14"></asp:ListItem>
                                            <asp:ListItem Value="15"></asp:ListItem>
                                            <asp:ListItem Value="16"></asp:ListItem>
                                            <asp:ListItem Value="17"></asp:ListItem>
                                            <asp:ListItem Value="18"></asp:ListItem>
                                            <asp:ListItem Value="19"></asp:ListItem>
                                            <asp:ListItem Value="20"></asp:ListItem>
                                            <asp:ListItem Value="21"></asp:ListItem>
                                            <asp:ListItem Value="22"></asp:ListItem>
                                            <asp:ListItem Value="23"></asp:ListItem>
                                            <asp:ListItem Value="24"></asp:ListItem>
                                            <asp:ListItem Value="25"></asp:ListItem>
                                            <asp:ListItem Value="26"></asp:ListItem>
                                            <asp:ListItem Value="27"></asp:ListItem>
                                            <asp:ListItem Value="28"></asp:ListItem>
                                            <asp:ListItem Value="29"></asp:ListItem>
                                            <asp:ListItem Value="30"></asp:ListItem>
                                            <asp:ListItem Value="31"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: right; width: 50%;">
                                        <br />
                                        <asp:Button ID="btnGo2" runat="server" Text="View" CssClass="savebutton" OnClick="btnGo2_Click" />
                                    </div>
                                </div>
                                <div class="single-des clearfix">
                                    <asp:CheckBox ID="chkTick" runat="server" Text="With Processed" AutoPostBack="true" OnCheckedChanged="chkTick_ChckedChanged" Visible="false"></asp:CheckBox>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>

                    <br />
                    <br />
                    <br />
                    <br />

                </div>
                <div class="row justify-content-center">
                    <div class="col-lg-10">
                        <div class="display-table clearfix">
                            <div class="table-responsive">
                                <asp:GridView ID="grdActUpload" runat="server" Width="95%" HorizontalAlign="Center" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowCommand="grdActUpload_RowCommand" GridLines="None" CssClass="table" PagerStyle-CssClass="pgr" OnRowDataBound="grdActUpload_RowDataBound" AllowPaging="True"
                                        PageSize="150" OnPageIndexChanging="grdActUpload_PageIndexChanging"
                                    AlternatingRowStyle-CssClass="alt" AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSNo" runat="server" Text='<%#  (grdActUpload.PageIndex * grdActUpload.PageSize) +((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activity_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivity_ID" runat="server" Text='<%#Eval("Activity_ID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sf_Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_Code" runat="server" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Activity Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivity_Name" runat="server" Text='<%# Bind("Activity_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%--<asp:HiddenField ID="hdnProcess" runat="server" Value='<%# Bind("Process_Flag") %>' />--%>
                                                <asp:CheckBox ID="chkdelete" runat="server" Text="." CssClass="AllTick" ></asp:CheckBox> <%--onclick="SelectCheckboxes(this)"--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Field Force Name" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSf_Name" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_hq" runat="server" Text='<%# Bind("sf_hq") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDesignation_Short_Name" runat="server" Text='<%# Bind("Designation_Short_Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Emp ID" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsf_emp_id" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                     
                                         <asp:TemplateField HeaderText="Date Activity Approval" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate_Activity_Approval" runat="server" Text='<%# Bind("Date_Activity_Approval") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Approved Bill" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivity_Approved_Bill_Amount" runat="server" Text='<%# Bind("Activity_Approved_Bill_Amount") %>'></asp:Label>
                                                    <asp:hiddenfield ID="hdnMonth_Activity" runat="server" value='<%# Eval ("Month_Activity") %>'></asp:hiddenfield>
                                                    <asp:hiddenfield ID="hdnYear_Activity" runat="server" value='<%# Eval ("Year_Activity") %>'></asp:hiddenfield>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Print View" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                              <%--  <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Activity_ID") %>'
                                                    CommandName="Click">Click Here
                                                </asp:LinkButton>--%>
                                                 <a href="#" onclick="window.open('Processing_ActPrint.aspx?Activity_ID=<%#Eval("Activity_ID")%>&amp;Sf_Code=<%#Eval("Sf_Code")%>&amp;Date_Activity_Approval=<%#Eval("Date_Activity_Approval")%>&amp;Activity_Approved_Bill_Amount=<%#Eval("Activity_Approved_Bill_Amount")%>&amp;Month_Activity=<%#Eval("Month_Activity")%> &amp;Year_Activity=<%#Eval("Year_Activity")%> &amp;Activity_Description=<%#Eval("Activity_Description")%> &amp;' ,'PrintMe','height=700px,width=700px,scrollbars=1');">Click Print</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Payment credited on" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProcess_Date" runat="server" Text='<%# Bind("Process_Date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Payment credited on" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblProcess_Date" runat="server" Text='<%# Bind("Process_Date") %>'></asp:Label>--%>
                                                <asp:textbox ID="txtProcess_Date" runat="server" Text='<%# Bind("Process_Date") %>'></asp:textbox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Process" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnProcess" runat="server" Value='<%# Bind("Process_Flag") %>' />
                                                <asp:CheckBox ID="chkProcess" runat="server" Text="." CssClass="AllTick" onclick="SelectCheckboxes(this)"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="no-result-area" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="w-100 designation-submit-button text-center clearfix">
                    <br />
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" visible="false" />
                    <asp:Button ID="btndelete" runat="server" Text="Delete" CssClass="savebutton" OnClick="btnDelete_Click" visible="false" />
                </div>
            </div>

        </div>
        <!-- Bootstrap -->
        <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
        <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
        <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
        <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
        <script type="text/javascript">
            $(function () {
                $('[id*=txtMonthYear]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "M-yyyy",
                    viewMode: "months",
                    minViewMode: "months",
                    language: "tr"
                });
                $('[id*=txtProcess_Date]').datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd/mm/yyyy",
                    viewMode: "days",
                    minViewMode: "days",
                    language: "tr"
                });
            });
        </script>
    </form>
</body>
</html>

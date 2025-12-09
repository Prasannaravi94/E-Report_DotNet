<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DCR_Entry_Mode.aspx.cs"
    Inherits="MasterFiles_Subdiv_Salesforcewise" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" tagPrefix="ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DCR - Status (count - Modewise)</title>
  <%--  <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
     <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />

     <style type="text/css">
        #grdSalesForce .small {
            font-size: 100%;
        }

        .stickyFirstRow {
            position: sticky;
            position: -webkit-sticky;
            top: 0;
            z-index: 1;
            background: inherit;
        }
    </style>
</head>
<body>
 <script type="text/javascript">
     function PrintGridData() {
         var prtGrid = document.getElementById('<%=grdSalesForce.ClientID %>');
         prtGrid.border = 1;
         var prtwin = window.open('', 'PrintGridViewData', 'left=0,top=0,width=800,height=500,tollbar=0,scrollbars=1,status=0,resizable=yes');
         prtwin.document.write(prtGrid.outerHTML);
         prtwin.document.close();
         prtwin.focus();
         prtwin.print();
         prtwin.close();
     }

    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
     <script type="text/javascript" language="javascript">
         $(function () {
             $('#btnExcel').click(function () {
                 var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                 location.href = url
                 return false
             })
         })
    </script>

<script type="text/javascript">
        $(document).ready(function () {
            // $('input:text:first').focus();
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
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });
            $('#btnSF').click(function () {
                var Prod = $('#<%=ddlSubdiv.ClientID%> :selected').text();
                if (Prod == "---Select---") { alert("Select Salesforce Name."); $('#ddlSubdiv').focus(); return false; }
            });
        });
    </script>
     <script type="text/javascript">
         $(document).ready(function () {

             $('#checkall').click(function () {
                 
                 debugger
                 var CHK = document.getElementById("<%=chkdate.ClientID%>");
                 
                 var checkboxes = CHK.getElementsByTagName("input");
                 
                //var eles = $(":input[name^='q1_']");
                 var label = CHK.getElementsByTagName("label");
                
                 if (checkall.checked) {
                     
                     for (var i = 0; i < checkboxes.length; i++) {
                         
                         checkboxes[i].checked = true;
                         
                }
            } else {
                for (var i = 0; i < checkboxes.length; i++) {
                    checkboxes[i].checked = false;
                }
            }

          
            })
          
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    
      <link href="//netdna.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" rel="stylesheet"/>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <form id="form1" runat="server">
    <div>
    <div id="Divid" runat="server"></div>
        <br />
        <center>
              <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">DCR - Status (count - Modewise)</h2>
                         <div class="designation-area clearfix">
                            <div class="single-des clearfix">
<div class="single-des-option" align="left">
                            <asp:Label ID="lblFilter" runat="server" Text="FieldForce Name" CssClass="label" Font-Bold="true" Font-Size="Medium"></asp:Label>
                       </div>

                           <div class="row-fluid">
                            <asp:DropDownList ID="ddlSubdiv" data-live-search="true" 
                             class="selectpicker" runat="server" CssClass="nice-select" Width="250px" >
                            </asp:DropDownList>
                          
                            
                                  
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select" Width="250px"></asp:DropDownList>
                            </div>
                                    </div>
                                 </div>
                            <img src="../../Images/loading/loading19.gif" style="display: none;" id="loaderSearch" />
                            <span id="SPl"
                                style="font-family: Verdana; color: Red; font-weight: bold; display: none; width: 200px">Please Wait....</span>

                                                 
                    <asp:HiddenField ID="hdnBasedOn" runat="server" />
                    <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name" Visible="false" CssClass="label"></asp:Label>

                <div class="single-des clearfix">
                               <div class="single-des-option" align="left">
                 
                    
                        <asp:Label ID="lblMonth" runat="server" CssClass="label" Text="Month"></asp:Label>
                   </div>
                   <asp:dropdownlist ID="monthId" runat="server" CssClass="nice-select"></asp:dropdownlist>
                      </div>
                                     
              
                      <div class="single-des clearfix">
                                  <div class="single-des-option" align="left">
                        <asp:Label ID="lblYr" runat="server" CssClass="label" Text="Year"></asp:Label>
                   </div>
                   <asp:DropDownList ID="yearID" runat="server" CssClass="nice-select"></asp:DropDownList>
                   
                                         </div>
                                    
                       <div class="single-des clearfix">
                               <div class="single-des-option" align="left">
                        <asp:Label ID="lblmode" runat="server" CssClass="label" Text="Mode"></asp:Label>
                                   </div>
                   <asp:DropDownList ID="ModeId" runat="server" AutoPostBack="true" OnSelectedIndexChanged="datechange_SelectedIndexChanged" CssClass="nice-select">
                    <asp:ListItem Selected="True" Text="---Select---"></asp:ListItem>
                            <asp:ListItem Value="1" Text="CountWise"></asp:ListItem>
                            <asp:ListItem Value="2" Text="DateWise"></asp:ListItem>
                    </asp:DropDownList>

                    
                                    
                     </div>


                    <div class="single-des clearfix">
                    <div class="single-des-option" align="left">
                        <asp:CheckBox ID="chkVacant" Text="Including Vacant" runat="server"  />
                    </div>
                     </div>
                        <div class="single-des clearfix">
                          <div class="single-des-option" align="left">     
                        <asp:CheckBox ID="checkall" Text=" all" runat="server" onclick="checkAll(this)" />
                        <asp:CheckBoxList ID="chkdate" RepeatColumns="7" RepeatDirection="Horizontal" runat="server"> 
                         </asp:CheckBoxList>  
                     </div>
                             </div>
                                    
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />          
                   <asp:Button ID="btnSF" runat="server" Width="30px" Height="25px" Text="Go" CssClass="savebutton"
                OnClick="btnSF_Click" />
                </div>
                               </div>
            </div>
        </center>
        <br />
        <table align="right" style="margin-right: 5%">
            <tr>
                 <td>
                                <asp:Button ID="btnPrint" runat="server" Visible="false" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                     />
                            </td>
                            <td>
                                <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                    BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                     />
                            </td>
            </tr>
        </table>
         <asp:Panel ID="pnlContents" runat="server">
            <table width="100%" align="center">
                <tbody>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdSalesForce" runat="server" Width="65%" HorizontalAlign="Center"
                                AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid"  BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                         <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="sfNameHidden" runat="server" Value='<%#Eval("sf_name")%>' />
                                            <asp:HiddenField ID="sfCodeHidden" runat="server" Value='<%#Eval("SF_Code")%>' />
                                                                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Head Quater" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									<asp:TemplateField HeaderText="Emp Id" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesig" runat="server" Text='<%# Bind("sf_Designation_short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Desktop" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesk1" runat="server" Text='<%# Bind("Desktop") %>'></asp:Label>
                                            <asp:Panel ID="Panel1" runat="server" style="background-color:#cccccc">
                                    <asp:Label ID="Labeldesk2" runat="server" Text='<%# Bind("desdt")%>'></asp:Label>
                           </asp:Panel>
                             <ajax:BalloonPopupExtender ID="PopupControlExtender" runat="server"
                                   TargetControlID="lbldesk1"
                                    BalloonPopupControlID="Panel1"
                                    Position="BottomRight"
                                   BalloonStyle="Cloud"
                                   BalloonSize="Small"
                                    CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                     CustomClassName="oval"
                                     UseShadow="true"
                                    ScrollBars="Auto"
                                     DisplayOnMouseOver="true"
                                     DisplayOnFocus="false"
                                     DisplayOnClick="false" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApps1" runat="server" Text='<%# Bind("Mobile")%>'></asp:Label>
                                            <asp:Panel ID="Panel2" runat="server" style="background-color:#cccccc">
                                            <asp:Label ID="LabelApps2" runat="server" Text='<%# Bind("mobdt")%>'></asp:Label>
                           </asp:Panel>
                             <ajax:BalloonPopupExtender ID="PopupControlExtender1"  runat="server"
                                   TargetControlID="lblApps1"
                                    BalloonPopupControlID="Panel2"
                                    Position="BottomRight"
                                   BalloonStyle="Cloud"
                                   BalloonSize="Small"
                                    CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                     CustomClassName="oval"
                                     UseShadow="true"
                                    ScrollBars="Auto"
                                     DisplayOnMouseOver="true"
                                     DisplayOnFocus="false"
                                     DisplayOnClick="false" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apps" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile1" runat="server" Text='<%# Bind("Apps")%>'></asp:Label>
                                             <asp:Panel ID="Panel3" runat="server" style="background-color:#cccccc">
                                            <asp:Label ID="LabelMobile2" runat="server" Text='<%# Bind("Appdt")%>'></asp:Label>
                           </asp:Panel>
                             <ajax:BalloonPopupExtender ID="PopupControlExtender3"  runat="server"
                                   TargetControlID="lblMobile1"
                                    BalloonPopupControlID="Panel3"
                                    Position="BottomRight"
                                   BalloonStyle="Rectangle"
                                   BalloonSize="Small"
                                    CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                     CustomClassName="oval"
                                     UseShadow="true"
                                    ScrollBars="Auto"
                                     DisplayOnMouseOver="true"
                                     DisplayOnFocus="false"
                                     DisplayOnClick="false" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="E-detailing" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEdt1" runat="server" Text='<%# Bind("Edt")%>'></asp:Label>
                                         <asp:Panel ID="Panel5" runat="server" style="background-color:#cccccc">
                                            <asp:Label ID="LabelEdt2" runat="server" Text='<%# Bind("edtdt")%>'></asp:Label>
                           </asp:Panel>
                             <ajax:BalloonPopupExtender ID="PopupControlExtender5"  runat="server"
                                   TargetControlID="lblEdt1"
                                    BalloonPopupControlID="Panel5"
                                    Position="BottomRight"
                                   BalloonStyle="Rectangle"
                                   BalloonSize="Small"
                                    CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                    CustomClassName="oval"
                                    UseShadow="true"
                                    ScrollBars="Auto"
                                     DisplayOnMouseOver="true"
                                     DisplayOnFocus="false"
                                     DisplayOnClick="false" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Others" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblothers1" runat="server" Text='<%# Bind("Oths")%>'></asp:Label>
                                             <asp:Panel ID="Panel4" runat="server" style="background-color:#cccccc">
                                            <asp:Label ID="Labelothers2" runat="server" Text='<%# Bind("othdt")%>'></asp:Label>
                           </asp:Panel>
                             <ajax:BalloonPopupExtender ID="PopupControlExtender4"  runat="server"
                                   TargetControlID="lblothers1"
                                    BalloonPopupControlID="Panel4"
                                    Position="BottomRight"
                                   BalloonStyle="Cloud"
                                   BalloonSize="Small"
                                    CustomCssUrl="CustomStyle/BalloonPopupOvalStyle.css"
                                     CustomClassName="oval"
                                     UseShadow="true"
                                    ScrollBars="Auto"
                                     DisplayOnMouseOver="true"
                                     DisplayOnFocus="false"
                                     DisplayOnClick="false" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>


                            <asp:GridView ID="GridSales1" runat="server" Width="65%" HorizontalAlign="Center" OnRowCreated="grvMergeHeader_RowCreated" ShowHeader="false"
                                AutoGenerateColumns="false" Font-Size="10" EmptyDataText="No Records Found" GridLines="None"
                                CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                <HeaderStyle Font-Bold="False" />
                                <PagerStyle CssClass="pgr"></PagerStyle>
                                <SelectedRowStyle BackColor="BurlyWood" />
                                <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSNo1" runat="server" Text='<%# (GridSales1.PageIndex * GridSales1.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid"  BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                         <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:HiddenField ID="sfNameHidden1" runat="server" Value='<%#Eval("sf_name")%>' />
                                            <asp:HiddenField ID="sfCodeHidden1" runat="server" Value='<%#Eval("SF_Code")%>' />
                                                                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Head Quater" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblsfName1" runat="server" Text='<%# Bind("sf_HQ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
									<asp:TemplateField HeaderText="Emp Id" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpId1" runat="server" Text='<%# Bind("sf_emp_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesig1" runat="server" Text='<%# Bind("sf_Designation_short_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Desktop" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            
                                            
                                    <asp:Label ID="Labeldesk4" runat="server" Text='<%# Bind("desdt")%>'></asp:Label>
                           
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Desktop" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lbldesk3" runat="server" Text='<%# Bind("Desktop") %>'></asp:Label>
                                            
                                    
                           
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                     
                                     <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            
                                            
                                            <asp:Label ID="LabelApps4" runat="server" Text='<%# Bind("mobdt")%>'></asp:Label>
                           
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblApps3" runat="server" Text='<%# Bind("Mobile")%>'></asp:Label>
                                            
                                            
                           
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Apps" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            
                                             
                                            <asp:Label ID="LabelMobile4" runat="server" Text='<%# Bind("Appdt")%>'></asp:Label>
                          
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Apps" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMobile3" runat="server" Text='<%# Bind("Apps")%>'></asp:Label>
                                             
                                            
                          
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                     
                                     <asp:TemplateField HeaderText="E-detailing" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            
                                         
                                            <asp:Label ID="LabelEdt4" runat="server" Text='<%# Bind("edtdt")%>'></asp:Label>
                          
                                        </ItemTemplate>

                                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="E-detailing" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblEdt3" runat="server" Text='<%# Bind("Edt")%>'></asp:Label>
                                         
                                            
                          
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    
                                
                                     <asp:TemplateField HeaderText="Others" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblothers4" runat="server" Text='<%# Bind("othdt")%>'></asp:Label>
                                            
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Others" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="center">
                                        </ItemStyle>
                                        <HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="Black"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblothers3" runat="server" Text='<%# Bind("oths")%>'></asp:Label>
                                            
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                        </td>
                    </tr>
                </tbody>
            </table>
            </asp:Panel>
         <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </div>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
  <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.2/js/bootstrap.min.js"></script>
  <script src="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/js/bootstrap-select.min.js"></script>
    </form>
</body>
</html>

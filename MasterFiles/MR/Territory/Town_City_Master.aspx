<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Town_City_Master.aspx.cs" Inherits="MasterFiles_MR_Town_City_Master" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Town/City Add/Edit/Deactivate</title>
        <link type="text/css" rel="stylesheet" href="../../../css/style.css" />  
       <link type="text/css" rel="stylesheet" href="../../../css/Grid.css" />
      <style type="text/css">
/**   .gv

        {

            font-family: Arial;

            margin-top: 30px;

            font-size: 14px;

        }

        .gv th

        {

            background-color: #5D7B9D;

            font-weight: bold;

            color: #fff;

            padding: 2px 10px;

        }

        .gv td

        {

            padding: 2px 10px;

        }

        input[type="submit"]

        {

            margin: 2px 10px;

            padding: 2px 20px;

            background-color: #5D7B9D;

            border-radius: 10px;

            border: solid 1px #000;

            cursor: pointer;

            color: #fff;

        }

        input[type="submit"]:hover

        {

            background-color: orange;

        }**/
          td.stylespc
        {
            padding-bottom:5px;
            padding-right :5px;
        }
    </style>
      <script type="text/javascript" src="../../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="../../../JsFiles/CommonValidation.js"></script>
  
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
                               $('#btnSubmit').focus();
                           }
                       }
                   }
               });
               $("input:text").on("keypress", function (e) {
                   if (e.which === 32 && !this.value.length)
                       e.preventDefault();
               });

               $('#btnSave').click(function () {

                   var st = $('#<%=ddlState.ClientID%> :selected').text();
                   if (st == "---Select---") { alert("Select State."); $('#ddlState').focus(); return false; }
                   if ($("#txtCity").val() == "") { alert("Enter City Name."); $('#txtCity').focus(); return false; }

                   var type = $('#<%=Territory_Type.ClientID%> :selected').text();
                   if (type == "--Select--") { alert("Select Type."); $('#Territory_Type').focus(); return false; }


               });
               $('#btnUpdate').click(function () {
                   var st = $('#<%=ddlState.ClientID%> :selected').text();
                   if (st == "---Select---") { alert("Select State."); $('#ddlState').focus(); return false; }

                   if ($("#txtCity").val() == "") { alert("Enter City Name."); $('#txtCity').focus(); return false; }

                   var type = $('#<%=Territory_Type.ClientID%> :selected').text();
                   if (type == "--Select--") { alert("Enter Type."); $('#Territory_Type').focus(); return false; }


               });
               $('#btnGo').click(function () {
                   var st = $('#<%=ddlState.ClientID%> :selected').text();
                   if (st == "---Select---") { alert("Select State."); $('#ddlState').focus(); return false; }

                });
           });

    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div id="Divid" runat="server">
        </div>
         
  
    <div>
    <center>
      <table align="center" style="position: relative; top: 20px;">

            <tr>

                <td>

                    <table align="center">
                    <tr>
                  <td align="left" class="stylespc">
                      <asp:Label ID="lblState" runat="server" SkinID="lblMand" Width="100px"><span style="color:Red">*</span>State Name</asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlState" runat="server" 
                        SkinID="ddlRequired"  >                                                        
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnGo" runat="server" Width="30px" Height="25px"  Text="Go"
                            BackColor="LightBlue" onclick="btnGo_Click" />
                    </td>
                    </tr>

                        <tr>

                            <td align="left" class="stylespc">

                             <asp:Label ID="lblCity" runat="server" SkinID="lblMand">Town/City Name</asp:Label>

                            </td>

                            <td align="left" class="stylespc">

                                <asp:TextBox ID="txtCity" runat="server" MaxLength="50" SkinID="MandTxtBox" Width="250px"></asp:TextBox>

                            </td>

                        </tr>

                        <tr>

                            <td align="left" class="stylespc">

                                 <asp:Label ID="Label1" runat="server" SkinID="lblMand">Type</asp:Label>
                            </td>

                            <td align="left" class="stylespc">

                                 <asp:DropDownList ID="Territory_Type" runat="server" SkinID="ddlRequired">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Metro" Value="ME"></asp:ListItem>
                                                    <asp:ListItem Text="Non-Metro" Value="NM"></asp:ListItem>
                                                   
                                                </asp:DropDownList>

                            </td>

                        </tr>

                     

                        <tr>

                            <td colspan="2" align="center">
                            <br />

                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="50px" Height="25px" BackColor="LightBlue" OnClick="btnSave_Click" />

                                <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="50px" Height="25px" BackColor="LightBlue" OnClick="btnUpdate_Click"

                                    Visible="false" />

                                <asp:Button ID="btnClear" runat="server" Text="Clear" Width="50px" Height="25px" BackColor="LightBlue" OnClick="btnClear_Click" />

                            </td>

                        </tr>

                    </table>

                </td>

            </tr>

            <tr>

                <td align="center">

                    <br />

                    <asp:Label ID="lblMessage" runat="server" EnableViewState="false" ForeColor="Blue"></asp:Label>

                </td>

            </tr>

           
        </table>
        <br />
            <asp:DataList ID="dlAlpha" RepeatDirection="Horizontal" OnItemCommand="dlAlpha_ItemCommand"
                        runat="server" HorizontalAlign="Center" AlternatingItemStyle-ForeColor="Red">
                        <SeparatorTemplate>
                        </SeparatorTemplate>
                        <ItemTemplate>
                            &nbsp
                            <asp:LinkButton ID="lnkbtnAlpha" ForeColor="Black" Font-Names="Calibri" Font-Size="14px"
                                runat="server" CommandArgument='<%#bind("Town_Name") %>' Text='<%#bind("Town_Name") %>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
        <br />
          <asp:GridView ID="gvCity" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" OnRowDataBound="gvCity_RowDataBound"
           PageSize="10"  OnPageIndexChanging="gvCity_PageIndexChanging" AllowPaging="true" 
                        EmptyDataText="No Records Found" Width="75%" GridLines="both" CssClass="mGridImg" PagerStyle-CssClass="pgr" EmptyDataRowStyle-ForeColor="Red">

                        <Columns>
                            <asp:TemplateField HeaderText="#" HeaderStyle-Width="12px" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (gvCity.PageIndex * gvCity.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            <asp:TemplateField HeaderText="Town/City Name">

                                <ItemTemplate>

                                    <asp:Label ID="lblCity" runat="server" Text='<%#Eval("Town_Name") %>'></asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Type">

                                <ItemTemplate>

                                          <asp:Label ID="lbltype" runat="server" Text='<%#Eval("Town") %>'></asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type" Visible="false">

                                <ItemTemplate>

                                          <asp:Label ID="type" runat="server" Text='<%#Eval("Town_Type") %>'></asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="State Name">

                                <ItemTemplate>

                                    <asp:Label ID="lblSt" runat="server" Text='<%#Eval("State_Name") %>'></asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>
                       <%--     
                            <asp:TemplateField HeaderText="Active Territory" Visible="false">

                                <ItemTemplate>

                                    <asp:Label ID="lblDr" runat="server" Text='<%#Eval("Active") %>'></asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>--%>
                         

                          
                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">

                                <ItemTemplate>

                                    <asp:LinkButton ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" />
                                 
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">

                                <ItemTemplate>

                                    

                                    <asp:LinkButton ID="btnDelete" runat="server" Text="Deactivate" OnClientClick="return confirm('Do you want to Deactivate City.');"

                                        OnClick="btnDelete_Click" />

                                    <asp:Label ID="lblCityID" runat="server" Text='<%#Eval("Sl_No") %>' Visible="false"></asp:Label>
    <asp:Label ID="lblimg" runat="server" Text="Deactivate" Visible="false" >                                        
                                      <img src="../../../Images/deact1.png" alt="" width="50px" title="This City Exists in Territory" />
                                          </asp:Label> 
                                </ItemTemplate>

                            </asp:TemplateField>

                        </Columns>

                    </asp:GridView>
        <input type="hidden" runat="server" id="hidCityID" />
        </center>
    </div>
    </form>
</body>
</html>

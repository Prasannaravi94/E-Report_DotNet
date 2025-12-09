<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmBrandWiseSlidesUpd.aspx.cs" 
    Inherits="MasterFiles_Options_frmBrandWiseSlidesUpd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajx" %>
<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" data-ng-app="UploadApp">
    <title>Brand Wise Slides Upload</title>
    <link type="text/css" rel="Stylesheet" href="SlideFile/SlideUpd.css" />
    <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <script type="text/javascript" src="../../JsFiles/common.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="SlideFile/SlideUpd.js"></script>
    
     <%-- New DropDown Script --%>

  

    <%-- End --%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/json2/20130526/json2.min.js"></script>
    <script type="text/javascript">
        $('#<%=GrdUpload.ClientID%>').ready(function () {
            var ddlDivision = $("#ddlDivision").val();
            var ddlBrand = $("#ddlBrand").val();
            var SubDiv = $("#ddlSubdivision").val();
            $.ajax({
                type: 'POST',
                url: 'frmBrandWiseSlidesUpd.aspx/ConvertDataTabletoString',
                data: '{division:' + JSON.stringify(ddlDivision) + ',Brand:' + JSON.stringify(ddlBrand) + ',SubDiv:' + JSON.stringify(SubDiv) + ' }',
                //data: { get_param: 'value' },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    //console.log(data.d);
                    //alert(data.d);
                    var json_obj = $.parseJSON(data.d)
                    var Row = 1;
                    var text;
                    $("#GrdUpload tr").not("tr:first").each(function (index) {
                        Row = Row + 1;
                        $(this).closest('tr').find("td:eq(2)").each(function (i) {

                            var output = json_obj[index].Product_Detail_Name
                            //alert($(this).find('input:checkbox').length);
                            $(this).find('input:checkbox').each(function (j) {
                                var ChkId = $(this).attr('id');
                                var ChkHidden = $(this).find('input:hidden').attr('id');
                                if (Row > 9) {
                                    text = $("#" + "GrdUpload_ctl" + Row + "_ddlProd").find('label')[j];
                                }
                                else {
                                    text = $("#" + "GrdUpload_ctl0" + Row + "_ddlProd").find('label')[j];
                                }
                                var text1 = text.innerText;
                                var array = output.split(",");

                                //if (array.length > 1) {
                                //    $.each(array, function (i) {
                                //        var arrayVal = array[i];
                                //        if (arrayVal == text1) {                                                
                                //            $(this).prop("checked", true);
                                //        }
                                //    });
                                //}
                                //else {

                                //    if (output == text1) {
                                //        $(this).prop("checked", true);
                                //} 
                                //}

                                if (array.length > 1) {
                                    for (var i = 0; i < array.length; i++) {
                                        var arrayVal = array[i];
                                        if ($.trim(arrayVal) ==$.trim(text1)) {
                                            var id = text.htmlFor;
                                            $("#" + id).prop('checked', true);
                                        }
                                    }
                                }
                                else {

                                    if (output == text1) {
                                        $(this).prop("checked", true);
                                    }
                                }

                            });

                        });

                        $(this).closest('tr').find("td:eq(3)").each(function (i) {

                            var output = json_obj[index].Doc_Special_Name
                            //alert($(this).find('input:checkbox').length);
                            $(this).find('input:checkbox').each(function (j) {
                                var ChkId = $(this).attr('id');
                                var ChkHidden = $(this).find('input:hidden').attr('id');
                                if (Row > 9) {
                                    text = $("#" + "GrdUpload_ctl" + Row + "_ddlSpeciality").find('label')[j];
                                }
                                else {
                                    text = $("#" + "GrdUpload_ctl0" + Row + "_ddlSpeciality").find('label')[j];
                                }
                                var text1 = text.innerText;
                                var array = output.split(",");

                                if (array.length > 1) {
                                    for (var i = 0; i < array.length; i++) {
                                        var arrayVal = array[i];
                                        if (arrayVal == text1) {
                                            var id = text.htmlFor;
                                            $("#" + id).prop('checked', true);
                                        }
                                    }
                                }
                                else {

                                    if (output == text1) {
                                        $(this).prop("checked", true);
                                    }
                                }

                            });

                        });

                        $(this).closest('tr').find("td:eq(4)").each(function (i) {

                            var output = json_obj[index].Product_Grp_Name
                            //alert($(this).find('input:checkbox').length);
                            $(this).find('input:checkbox').each(function (j) {
                                var ChkId = $(this).attr('id');
                                var ChkHidden = $(this).find('input:hidden').attr('id');
                                if (Row > 9) {
                                    text = $("#" + "GrdUpload_ctl" + Row + "_ddlHer").find('label')[j];
                                }
                                else {
                                    text = $("#" + "GrdUpload_ctl0" + Row + "_ddlHer").find('label')[j];
                                }
                                var text1 = text.innerText;
                                var array = output.split(",");

                                if (array.length > 1) {
                                    for (var i = 0; i < array.length; i++) {
                                        var arrayVal = array[i];
                                        if (arrayVal == text1) {
                                            var id = text.htmlFor;
                                            $("#" + id).prop('checked', true);
                                        }
                                    }
                                }
                                else {

                                    if (output == text1) {
                                        $(this).prop("checked", true);
                                    }
                                }

                            });

                        });

                    });
                }
            });
        });
    </script>
    <style type="text/css">
        form .progress
        {
            line-height: 15px;
        }
        .progress
        {
            display: inline-block;
            width: 100px;
            border: 3px groove #CCC;
        }
        .progress div
        {
            font-size: smaller;
            background: orange;
            width: 0;
        }
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        table
        {
            border: 1px solid #ccc;
        }
        table th
        {
            background-color: #F7F7F7;
            color: #333;
            font-weight: bold;
        }
        table th, table td
        {
            padding: 5px;
            border-color: #ccc;
        }
    </style>
     
    
    
</head>
<body>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="script" runat="server" EnablePageMethods="true"></asp:ScriptManager>
    <div>
    
        <div id="Divid" runat="server">
            </div>
        <center>
            <table width="90%">
                <tr>
                    <td style="background-color:#00cc44">
                        <div  align="center">
                            <span style="font-family:Verdana;font-size:14pt;font-weight:bold">Slide Upload for E-detailing</span>
                        </div>
                    </td>
                    <td>
                        <asp:Button ID="btnBack" PostBackUrl="~/BasicMaster.aspx" style="width:50px;height:30px" runat="server"
                            Text="Back" />
                    </td>
                </tr>
            </table>
        </center>
        <br />
        
        <center>
       
            <table cellpadding="3" cellspacing="3" width="65%">
                <tr>
                    <td>
                        <asp:Label ID="lblDivision" SkinID="lblMand" Text="Division" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDivision" AutoPostBack="false" SkinID="ddldcr" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged"
                            Enabled="false" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblSubdivision" SkinID="lblMand" Text="Sub division" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubdivision" SkinID="ddldcr" onchange="BrandWisePrd()" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblBrand" SkinID="lblMand" Text="Brand" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlBrand" SkinID="ddldcr" onchange="BrandWisePrd()" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" />--%>
                        <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click"  />
                    </td>
                </tr>
            </table>
          
            <br />
            
                    <table cellpadding="3" cellspacing="3">
                      
                        <tr align="left">
                            <td>
                                <asp:Label ID="lblProd" SkinID="lblMand" Text="Product" runat="server"></asp:Label>
                            </td>
                            <td>
                                <div class="multiselect">
                                    <div class="selectBox" onclick="showCheckboxes()">
                                        <select id="SelPrd">
                                            <option value="0">Select an option</option>
                                        </select>
                                        <div class="overSelect">
                                        </div>
                                    </div>
                                    <div id="checkboxes" style="position: absolute; background-color: beige; font-family: Verdana">
                                    </div>
                                </div>
                                
                            </td>
                            <td>
                                <asp:Label ID="lblSpec" SkinID="lblMand" Text="Speciality" runat="server"></asp:Label>
                            </td>
                            <td>
                                <div class="multiselect">
                                    <div class="selectBox" onclick="showSpec()">
                                        <select id="SelSpec">
                                            <option value="0">Select an option</option>
                                        </select>
                                        <div class="overSelect">
                                        </div>
                                    </div>
                                    <div id="DivSpec" style="position: absolute; background-color: beige">
                                    </div>
                                </div>
                                <%--<asp:DropDownCheckBoxes ID="ddlSpeciality" runat="server">
                            <Style SelectBoxWidth="200px" DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="110" />
                        </asp:DropDownCheckBoxes>--%>
                            </td>
                            <td>
                                <asp:Label ID="lblHera" SkinID="lblMand" Text="Therapy" runat="server"></asp:Label>
                            </td>
                            <td>
                                <div class="multiselect">
                                    <div class="selectBox" onclick="showThera()">
                                        <select id="SelTher">
                                            <option value="0">Select an option</option>
                                        </select>
                                        <div class="overSelect">
                                        </div>
                                    </div>
                                    <div id="divThero" style="position: absolute; background-color: beige">
                                    </div>
                                </div>
                                <%-- <asp:DropDownCheckBoxes ID="ddlHera" runat="server">
                            <Style SelectBoxWidth="200px" DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="110" />
                        </asp:DropDownCheckBoxes>--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSlides" SkinID="lblMand" runat="server" Text="Slides :"></asp:Label>
                            </td>
                            <td>
                                <%-- <asp:FileUpload ID="fileUpload" AllowMultiple="true" runat="server" />--%>
                                <asp:FileUpload runat="server" ID="fileUpload" AllowMultiple="true" />
                            </td>
                            <td colspan="2">
                                <%--<asp:Button ID="btnSubmit" runat="server" Text="Submit" />--%>
                                <%--<asp:Button ID="btnUpload" runat="server" Text="Submit" OnClientClick="makeAjaxCall()" />--%>
                                <input type="button" value="Upload" onclick="makeAjaxCall()" />
                            </td>
                        </tr>
                        <%--<tr>
                <td>
                    <div ng-app="fileUpload" class="container">
                        <div class="row" ng-controller="upload">
                            <div class="col-md-6">
                                <input type="file" fileinput="file" filepreview="filepreview" />
                            </div>
                            <div class="col-md-6">
                                <img ng-src="{{filepreview}}" class="img-responsive" ng-show="filepreview" />
                            </div>
                        </div>
                    </div>
                </td>
                </tr>--%>
                    </table>
                 
            
          
            <br />
            <br />
            
               
           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
            <asp:GridView ID="GrdUpload" runat="server" Width="90%" HorizontalAlign="Center"
                CssClass="mGrid" AutoGenerateColumns="false" PageSize="10" EmptyDataText="No Records Found"
                GridLines="Both" AlternatingRowStyle-CssClass="alt" OnRowDeleting="GrdUpload_Deleting">
                <Columns>
                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#336277"
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SI_NO" ItemStyle-HorizontalAlign="Left" Visible="false"
                        HeaderStyle-BackColor="#336277" HeaderStyle-ForeColor="White">
                        <ControlStyle Width="180px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="imgSI_NO" runat="server" Height="100px" Width="100px" Text='<%# Eval("SI_NO") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Slides" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#336277"
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="180px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Image ID="imgName" runat="server" Height="120" Width="150" ImageUrl='<%# Eval("Img_Name") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Priortity" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#336277"
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="180px"></ControlStyle>
                        <ItemTemplate>
                            <asp:TextBox ID="txtPriortity" runat="server" Height="100" Width="100" Style="text-align: center;
                                border: 0" Text='<%# Eval("Priority") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Product" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#336277"
                        HeaderStyle-ForeColor="White">
                        <ItemTemplate>
                            <asp:DropDownCheckBoxes ID="ddlProd" AddJQueryReference="True" DataSource="<%# FillProduct() %>"
                                DataTextField="Product_Detail_Name" DataValueField="Product_Code_SlNo" runat="server"
                                UseSelectAllNode="false">
                                <Style SelectBoxWidth="200px" DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="110" />
                            </asp:DropDownCheckBoxes>
                            &nbsp;
                            <%-- <asp:ExtendedRequiredFieldValidator ID="erfvPrd" runat="server"
                                ControlToValidate="ddlProd" ErrorMessage="Required" ForeColor="Red">
                                </asp:ExtendedRequiredFieldValidator>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#336277"
                        HeaderStyle-ForeColor="White">
                        <ItemTemplate>
                            <%-- <select id="ddlSpeciality"  multiple="multiple"> 
                              </select>--%>
                            <asp:DropDownCheckBoxes ID="ddlSpeciality" AddJQueryReference="True" DataSource="<%# FillSpec() %>"
                                DataTextField="Doc_Cat_Name" DataValueField="Doc_Cat_Code" runat="server" UseSelectAllNode="false">
                                <Style SelectBoxWidth="200px" DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="110" />
                            </asp:DropDownCheckBoxes>
                            &nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Therapy" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#336277"
                        HeaderStyle-ForeColor="White">
                        <ItemTemplate>
                            <%--<select id="ddlHer"  multiple="multiple"> 
                              </select>--%>
                            <asp:DropDownCheckBoxes ID="ddlHer" AppendDataBoundItems="true" DataSource="<%# FillHer() %>"
                                DataTextField="Product_Grp_Name" DataValueField="Product_Grp_Code" runat="server"
                                UseSelectAllNode="false">
                                <Style SelectBoxWidth="200px" DropDownBoxBoxWidth="160" DropDownBoxBoxHeight="110" />
                            </asp:DropDownCheckBoxes>
                            &nbsp;
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Deleted" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#336277"
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="19px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete" CssClass="BGImage" runat="server"  CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
            </asp:GridView>
           
            <br />
            <asp:Button ID="btnUpdate" Text="Update" Visible="false" runat="server" OnClick="btnUpdate_OnClick" />
             <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnView" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnUpload" EventName="Click" />
                    
                </Triggers>
            </asp:UpdatePanel>--%>
        </center>
    </div>
    <div>
    </div>
    
    </form>
</body>
</html>

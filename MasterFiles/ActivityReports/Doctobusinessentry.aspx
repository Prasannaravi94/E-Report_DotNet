<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeFile="Doctobusinessentry.aspx.cs" Inherits="MasterFiles_ActivityReports_Doctobusinessentry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>

 <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack())
                args.set_cancel(true);
            postBackElement = args.get_postBackElement();

            if (postBackElement.id == 'Btnsrc')
                $get('UpdateProgress1').style.display = 'block';
        }
        function EndRequest(sender, args) {
            if (postBackElement.id == 'Btnsrc')
                $get('UpdateProgress1').style.display = 'none';
        }
      
           
        
    </script>
    <script type="text/javascript">
        $(function () {
            $(".table1").bind("mouseover", function () {
                $(this).css("background-color", "#d6e9c6");
            });
            $(".table1").bind("mouseout", function () {
                $(this).css("background-color", "transparent");

            });
        });
    </script>
<style type="text/css">
   
.regular-checkbox {
background-color: #FF0000;
}
    .ddl
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
                    
                     
        background-image:url('css/download%20(2).png');
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
         .ddl1
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
                    
                     
        
  
            background-position:88px;
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
    .modalBackground
    {
        background-color: Black;
      
           width:200px;
           height:500px;
      
    }
    .modalPopup
    {
        background-color: #FFFFFF;
       width:200px;
        height:500px;
        border: 3px solid #0DA9D0;
        border-radius: 12px;
        padding:0;
      
    }
    .modalPopup .header
    {
        background-color: #2FBDF1;
        height: 30px;
        color: White;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
        border-top-left-radius: 6px;
        border-top-right-radius: 6px;
    }
    .modalPopup .body
    {
        min-height: 50px;
        line-height: 30px;
        text-align: center;
        font-weight: bold;
    }
    .modalPopup .footer
    {
        padding: 6px;
    }
    .modalPopup .yes, .modalPopup .no
    {
        height: 23px;
        color: White;
        line-height: 23px;
        text-align: center;
        font-weight: bold;
        cursor: pointer;
            width: 40px;
        border-radius: 4px;
    }
    .modalPopup .yes
    {
        background-color: #2FBDF1;
        border: 1px solid #0DA9D0;
    }
    .modalPopup .no
    {
        background-color: #9F9F9F;
        border: 1px solid #5C5C5C;
    }
</style>

   
   <script type="text/javascript">
       var sum = 0;

       function FetchData(button) {

           var cell = button.parentNode.parentNode.parentNode.parentNode;
           var row = button.parentNode.parentNode;
           var Roow = cell.getAttribute("rel");
           var Roow1 = button.getAttribute("rel");

           var label = GetChildControl(row, "product").innerHTML;
           var label1 = GetChildControl(row, "txtprice").value;
           var label2 = GetChildControl(row, "ffvalue").value;

           if (label2 == "") {
               var Multi = parseFloat(label) * parseFloat(label1);

               GetChildControl(row, "ffvalue").value = Multi;

               if (!isNaN(Multi) && Multi.length != 0) {
                   sum += parseFloat(Multi);

               }
           }
           else {

               if (!isNaN(label2) && label2.length != 0) {
                   sum -= parseFloat(label2);
               }
               var Multi = parseFloat(label) * parseFloat(label1);

               GetChildControl(row, "ffvalue").value = Multi;
               if (!isNaN(Multi) && Multi.length != 0) {
                   sum += parseFloat(Multi);
               }

           }



           if (Roow > 9) {
               var ff = parseInt(document.getElementById("ctl00_MainContent_DataList1_ctl" + Roow + "_fsum").value);
               if (isNaN(ff)) {
                   ff = 0;
                   if (!isNaN(sum) && sum.length != 0) {
                       ff += parseFloat(sum);
                   }
               }
               else {
                   if (!isNaN(sum) && sum.length != 0) {
                       ff += parseFloat(sum);
                   }
               }
               document.getElementById("ctl00_MainContent_DataList1_ctl" + Roow + "_gridsum").value = ff;
           }
           else {
               var ffe = parseInt(document.getElementById("ctl00_MainContent_DataList1_ctl0" + Roow + "_fsum").value);
               if (isNaN(ffe)) {
                   ffe = 0;
                   if (!isNaN(sum) && sum.length != 0) {
                       ffe += parseFloat(sum);
                   }
               }
               else {
                   if (!isNaN(sum) && sum.length != 0) {
                       ffe += parseFloat(sum);
                   }
               }
               document.getElementById("ctl00_MainContent_DataList1_ctl0" + Roow + "_gridsum").value = ffe;
           }



           return false;


       };

       function fetc(button) {


           var row = button.parentNode.parentNode;
           var label1 = GetChildControl(row, "fsum").value;
           label1 = sum;

       }
       function GetChildControl(element, id) {
           var child_elements = element.getElementsByTagName("*");
           for (var i = 0; i < child_elements.length; i++) {
               if (child_elements[i].id.indexOf(id) != -1) {
                   return child_elements[i];

               }


           }


       };

       var datasum = 0;
       var data1su = 0;
       var totsum = 0;
       function CheckToochSelection(a) {

           var fv = sum;

           var all = new Array();


           if (a > 8) {
               i = a + 1;
               var t = "Completed";


               var f = parseInt(document.getElementById("ctl00_MainContent_DataList1_ctl" + i + "_fsum").value);
               if (isNaN(f)) {
                   f = 0;
                   if (!isNaN(sum) && sum.length != 0) {
                       f += parseFloat(sum);
                   }
               }
               else {
                   if (!isNaN(sum) && sum.length != 0) {
                       f += parseFloat(sum);
                   }
               }
               document.getElementById("ctl00_MainContent_DataList1_ctl" + i + "_fsum").value = f;
               document.getElementById("ctl00_MainContent_DataList1_ctl" + i + "_Label4").value = t;

               var id = document.getElementById("ctl00_MainContent_DataList1_ctl" + i + "_Label4");
               id.style.backgroundColor = "yellow";

               //               if (!isNaN(f) && sum.length != 0) {
               //                   data1su += f;
               //               }
               //               alert(data1su);

               sum = 0;

           }
           else {
               var tt = "Completed";
               i = a + 1;
               var fp = parseInt(document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "_fsum").value);
               if (isNaN(fp)) {
                   fp = 0;
                   if (!isNaN(sum) && sum.length != 0) {
                       fp += parseFloat(sum);
                   }
               }
               else {
                   if (!isNaN(sum) && sum.length != 0) {
                       fp += parseFloat(sum);
                   }
               }
               document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "_fsum").value = fp;
               document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "_Label4").value = tt;



               var idd = document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "_Label4");
               idd.style.backgroundColor = "yellow";

               sum = 0;
               //               if (!isNaN(fp) && fp.length != 0) {
               //                   datasum += fp;
               //               }


               //               alert(datasum);
           }

           //           document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "pnlPopup").hide();
           alert(" Product Quantity Saved successfully");
           var gg = 0;
           $('.dataGroupItem').each(function () {
               if (!isNaN(this.value) && this.value.length != 0) {
                   gg += parseFloat(this.value);
               }

               //Todo
           });

           //           totsum = datasum + data1su;
           document.getElementById("ctl00_MainContent_totval").value = gg;
           document.getElementById("ctl00_MainContent_tot").value = gg;
           //           alert(totsum);
           //           var datalist = document.getElementById('<%=DataList1.ClientID%>').childNodes[0];
           //           var txtbox = datalist.getElementsById("fsum").value;
           //           alert(txtbox);

           //           alert(gg);
       }

       function closepop() {
           $('#mpe').dialog('close');

       }    
          
  </script>
 
<script type="text/javascript">

    function drpClick() {
        var ss = document.getElementById("ctl00_MainContent_DataList1_ctl0" + i + "lblInput");
        ss.style.backgroundColor = "red";

    }

    function Show() {

        var TargetBaseControl = document.getElementById('<%=this.DataList1.ClientID%>');
        TargetBaseControl.getElementsByTagName("btnShow").click();


    }



    function btnClick(obj) {
        $(obj).parent().find(":text").val("your value");
        event.preventDefault();
    }
    function kk() {
        var TargetBaseControl = document.getElementById('<%=this.DataList1.ClientID%>');

        var TargetChildControl1 = "txtQuantity";
        var Inputs = TargetBaseControl.getElementsByTagName("input");


        for (var n = 0; n < Inputs.length; ++n) {
            if (Inputs[n].type == 'text' && Inputs[n].id.indexOf(TargetChildControl1, 0) >= 0) {
                // Validate for input
                if (Inputs[n].value != "")



                    return true;

                alert("dgfdg");

                return false;

            }
        }

    }
 


</script>
<script type="text/javascript">
    function cleardropdown() {
        // To clear dropdown values we need to write code like as shown below
        $('#ctl00_MainContent_ddlFieldForce').empty();
        // Bind new values to dropdown
        //    $('#ctl00_MainContent_ddlFieldForce').each(function () {
        //// Create option
        //var option = $("<option />");
        //option.attr("value", '0').text('Select User');
        //$('#ctl00_MainContent_ddlFieldForce').append(option);
        //});
    }
</script>
    <style type="text/css">
        #effect
        {
            width: 180px;
            height: 160px;
            padding: 0.4em;
            position: relative;
            overflow: auto;
        }
        .highlight {
  background-color: #008000;
}
        .textbox
        {
            width: 185px;
            height: 14px;
        }
        body
        {
            font-size: 62.5%;
        }
        td.stylespc
        {
            padding-bottom: 5px;
            padding-right: 5px;
        }
        .dtxt
        {
            border:1px solid #ffffFF;
           border-radius:4px;
            margin:2px;
            width: 70px;
          text-align: center;
                    
         background-image:url('css/download%20(2).png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
        .dd
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;
            width: 70px;
          
            
                     
         background-image:url('css/download%20(2).png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            
        }
        .GVdd
        {
            border:1px solid #1E90FF;
           border-radius:4px;
            margin:2px;     
            
             font-family:Andalus;
            
                     
         background-image:url('css/download%20(2).png');
            background-position:88px;
            background-repeat:no-repeat;
            text-indent: 0.01px;/*In Firefox*/
            text-overflow: '';/*In Firefox*/
        }
        .style1
        {
            height: 30px;
        }
        .style2
        {
            width: 8%;
            height: 30px;
        }
    </style>
     <link type="text/css" rel="stylesheet" href="../../css/style.css" />
    <link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
  <script type="text/javascript" src="../../JsFiles/jquery.effects.core.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery.effects.blind.js"></script>
   <script type="text/javascript" src="../../JsFiles/multiple-select.js"></script>
 
     <div id="Divid" runat="server">
        </div>
  
  <center>
        <table>
            <tr>
                <td align="center" style="color: #8A2EE6; font-family: Verdana; font-weight: bold;
                    text-transform: capitalize; font-size: 14px; text-align: center;">
                    <asp:Label ID="lblHead" runat="server" Text="Listed Doctor- Business Entry" Font-Underline="true" 
                        Font-Bold="True" BorderStyle="None" Font-Names="Andalus" Font-Size="20px"></asp:Label>
                </td>
            </tr>
        </table>
    </center>
   </br>
   </br>

    <center>
        <table>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblFieldForceName" runat="server" Text="FieldForce Name " Width="100px" SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" AutoPostBack="false" CssClass="ddl"
                        OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblYear" runat="server" Text="Year " SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired" CssClass="dd">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="left" class="stylespc">
                    <asp:Label ID="lblMonth" runat="server" Text="Month " SkinID="lblMand"></asp:Label>
                </td>
                <td align="left" class="stylespc">
                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired" 
                        CssClass="dd">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td>&nbsp</td><td>&nbsp</td></tr><tr>
                <td  align="center">
                    <asp:Button ID="btnGo" runat="server" Text="Go" Width="30px" Height="25px" CssClass="savebutton"
                        OnClick="btnGo_Click" Font-Names="Andalus" />
                </td><td> 
                    <asp:Button ID="LinkButton1" runat="server" Text="Clear List" 
                        Width="70px" Height="25px" CssClass="savebutton" onclick="LinkButton1_Click" 
                        Font-Names="Andalus"  /></td><td colspan="2"><%--<asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                        Font-Names="Aparajita" Font-Size="Medium" onclick="LinkButton1_Click" 
                        OnClientClick="valit()">Clear List</asp:LinkButton>--%></td>
            
            
                
            </tr>




</table>
</br>
</center>

<center>



               
               
                 
<table width="100%">  
            <tr>
      
                <td align="right" style="padding-right:200px" >
                    <asp:Panel ID="Panel1" runat="server" align="right" 
                        width="380px" BackColor="#8dd5e4" 
                        BorderColor="#00CCFF" BorderStyle="Solid" BorderWidth="1px">
                    
                   
                  
                <table><tr><td>  <asp:Label ID="lblType" runat="server" 
                                SkinID="lblMand"  Text="Filter By" 
                        Font-Bold="True" Font-Names="Andalus" ForeColor="#00FFCC" 
                        Font-Underline="True" Width="70px"></asp:Label></td><td> 
                    <%--    <asp:UpdatePanel ID="UpdatePanel2" runat="server" > 
                
                <ContentTemplate>--%>
                 <asp:DropDownList ID="ddlSrch" runat="server" Width="120px" 
                       CssClass="ddl"  OnClientClick="callAjax();" SkinID="ddlRequired"
                        AutoPostBack="true" TabIndex="1" 
                        OnSelectedIndexChanged="ddlSrch_SelectedIndexChanged" Height="24px">
                          <asp:ListItem Text="Select" Value="0" ></asp:ListItem>
                        <asp:ListItem Text="SVL No" Value="1" ></asp:ListItem>
                        <asp:ListItem Text="Doctor Speciality" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Doctor Category" Value="3"></asp:ListItem>
                           <asp:ListItem Text="Doctor Name" Value="4" Selected="True"></asp:ListItem> 
                             <asp:ListItem Text="Sub Area" Value="5"></asp:ListItem>                                
                     
                    </asp:DropDownList><asp:TextBox ID="txtsearch" runat="server" SkinID="MandTxtBox" 
                        CssClass="TEXTAREA" Visible="false" Height="22px" Width="108px" ></asp:TextBox>
                    <asp:DropDownList ID="ddlSrc2" runat="server" Visible="false" CssClass="ddl"
                        OnSelectedIndexChanged="ddlSrc2_SelectedIndexChanged" Width="120px"
                        SkinID="ddlRequired" TabIndex="4">
                    </asp:DropDownList> 
  <%--                  </ContentTemplate>
 </asp:UpdatePanel>--%>
  </td><td><asp:Button ID="Btnsrc" runat="server"
                        CssClass="savebutton" Width="40px" Height="24px" Text="Go" OnClick="Btnsrc_Click"
                        Font-Bold="False" Font-Names="Andalus" /></td></tr></table>
              
                    
                  <%--<asp:UpdateProgress ID="UpdateProgress1" 
     AssociatedUpdatePanelID="UpdatePanel2" runat="server">
<ProgressTemplate><img src="../../Images/ajax-loader.gif" /> </ProgressTemplate> 
</asp:UpdateProgress>  
                   --%>
                           
     
      </asp:Panel>
                                   </td>
            </tr>  </table>
         <%-- </asp:Panel>--%>
            
  </center>
  <table><tr><td><div class="div_fixed"><asp:Button ID="submit2" CssClass="savebutton" 
          runat="server" Text="Final Submit" Width="58px" Height="25px" style="padding-right:170px;"
                        OnClick="btnSubmit_Click" Font-Bold="False" 
          Font-Names="Andalus" /></div></td></tr><tr>
         <td><div class="div_fixed"> <asp:TextBox ID="totval" runat="server"   BorderColor="#00CCFF" BorderStyle="Solid"  Font-Bold="True" Width="90px" Font-Names="Andalus"  ForeColor="Red"  BackColor="#d6e9c6" ></asp:TextBox></div></td></tr></table>
<div style="padding-left:120px;">

   
   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server"  >
               
                <ContentTemplate>--%>
             
    <asp:DataList ID="DataList1" runat="server" RepeatColumns="1"   OnItemCommand="dlList_ItemCommand"    GridLines="Both" 
         HeaderStyle-BackColor="#0097AC" HeaderStyle-BorderStyle="Solid"  HeaderStyle-BorderColor="Black"
         HeaderStyle-BorderWidth="1px" Width="85%"  Font-Size="8pt"  
                        HeaderStyle-Font-Size="16px" HeaderStyle-Font-Names="Andalus" HeaderStyle-Height="24px"
                        onselectedindexchanged="DataList1_SelectedIndexChanged"  
                        AlternatingItemStyle-BorderStyle="Groove" Font-Names="Segoe UI Semibold" 
                        Font-Bold="True">
     
   
        <AlternatingItemStyle BorderStyle="Solid" />
     
   
        <HeaderStyle BackColor="#0097AC" BorderColor="Black" BorderStyle="Solid" 
            BorderWidth="1px" Height="10px" />
     
   
     <HeaderTemplate  >
               <asp:Label ID="Label1" Text="#" Font-Bold="true" Width="40px" Height="30px"   runat="server" ></asp:Label>
                <asp:Label ID="lblDocVisit" Text="Listed Doctor Name" Font-Bold="true" Width ="199px"     runat="server"></asp:Label>
                 <asp:Label ID="lblsln" Text="Specialty" Font-Bold="true" Width="107px"   runat="server"></asp:Label>

                <asp:Label ID="lblunit" Text="Category" Font-Bold="true" Width="120px"  runat="server"></asp:Label>
                   <asp:Label ID="Label3" runat="server" Text="Sub Area" Font-Bold="true" Width="179px"  ></asp:Label>
                <asp:Label ID="qty" runat="server" Text="Product" Font-Bold="true" Width="132px"  ></asp:Label>
                  <asp:Label ID="Valuehead" runat="server" Text="Value" Font-Bold="true" Width="83px"   ></asp:Label>
                  <asp:Label ID="SALEHEAD" runat="server" Text="Sale" Font-Bold="true" Width="98px"  ></asp:Label>
                  
                
                                   </HeaderTemplate>
                                     <ItemStyle BackColor="White" ForeColor="Black" 
            BorderWidth="2px"  />
           
            <ItemStyle BorderColor="#0097AC" BorderStyle="Solid"  BorderWidth="1px" />  
              
   <ItemTemplate >
   
    
<table class="table1 Item"><tr align="left"><td> <asp:Label id="lblSLNO" runat="server" Width="50px" Text='<%# Container.ItemIndex+1 %>' ></asp:Label></td><td width="50px">  <asp:Label ID="lblInput" runat="server" Width="225px"  Text='<%# Eval("ListedDr_Name") %>'></asp:Label></td>
<td width="50px">  <asp:Label ID="Label1" runat="server" Width="85px"  Text='<%# Eval("Doc_Spec_ShortName") %>'></asp:Label></td><td width="50px">  <asp:Label ID="Label2" runat="server" Width="100px"  Text='<%# Eval("Doc_Cat_ShortName") %>'></asp:Label></td><td width="50px">  <asp:Label ID="teritory" runat="server" Width="185px"  Text='<%# Eval("territory_Name") %>' ></asp:Label></td>



       <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("ListedDrCode") %>' />
<td width="60px">
    <asp:DropDownList ID="ddl" runat="server" CssClass="GVdd" Width="100px"   OnClientClick="return drpClick()"  > 
<asp:ListItem Text="Select Product" Value="0"></asp:ListItem> 
 

 
</asp:DropDownList> 
</td>
<td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>

<%-- <td width="50px"> <asp:Label ID="Label4" runat="server" Width="60px" onkeyup="FetchData(this)"   ></asp:Label></td>--%>
 <td width="75px"><asp:TextBox ID="fsum" runat="server"   Width="65px" MaxLength="5"  style="text-align:center" class="dataGroupItem"  onkeyup="FetchData(this)" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
 <td width="50px"> <asp:TextBox ID="Label4" runat="server"   Width="65px"     CssClass="dtxt" onkeyup="FetchData(this)" ForeColor="#FF3300"></asp:TextBox></td>
 <%-- <td width="50px"><asp:TextBox ID="fval" runat="server"   Width="45px" MaxLength="5" class="dataGroupItem"   CssClass="dd" onkeyup="FetchData(this)" ></asp:TextBox></td>--%>
 
<%--<td width="50px"><asp:Label ID="sumval" runat="server" Width="100px" Font-Bold="true" Font-Size="Small"    ForeColor="Black"></asp:Label></td>
<td width="50px"><asp:Label ID="sale" runat="server" Width="120px" Font-Bold="true" Font-Size="Small"    ForeColor="Black"></asp:Label></td>--%>

<td><asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="display: none" /></td></tr></table><table><tr><td >

<asp:modalpopupextender ID="mpe" runat="server" PopupControlID="pnlPopup" TargetControlID="ddl" 
     CancelControlID="btnNo"   
        BackgroundCssClass="modalBackground" >
</asp:modalpopupextender></td></table>
 <table><tr><td>
<asp:Panel ID="pnlPopup" runat="server"  CssClass="modalPopup" Style="display: none;width:350px; height:500px; overflow:scroll;   "  >

     <table>
     <tr>
      <td>&nbsp</td>
     </tr>
     
     <tr align="left"><td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<td align="center"><asp:Button ID="btnSubmit" runat="server" Text="Save"   UseSubmitBehavior="false"  CssClass="yes"  OnClientClick='<%# "return CheckToochSelection(" + Container.ItemIndex +");" %>'     />&nbsp<asp:Button ID="btnNo" runat="server" Text="Close" CssClass="no" />
       </td>
   <tr><td colspan="3">    <asp:Label ID="Label6" runat="server" Text="_________________________________________________________________________"></asp:Label></td></tr>
     </tr>  <tr><td><asp:Label ID="Label8"  runat="server"  Width="100px" Font-Bold="true" ForeColor="#0097AC" Text="Doctor Name-" Font-Names="Andalus" Font-Size="Medium" ></asp:Label></td><td><asp:Label ID="docname" runat="server" Font-Names="Andalus" Font-Size="small" Width="136px"    ></asp:Label></td> </tr>
     
     </table>
      
      
    <asp:Label ID="Label5" runat="server" Text="__________________________________________________________________________"></asp:Label>
   
   

    <asp:GridView ID="GridView1" runat="server" EnableViewState="true" Width="30%" AutoGenerateColumns="false" ShowFooter="true"  OnSelectedIndexChanged="DataList1_SelectedIndexChanged"  rel="<%# Container.ItemIndex +1 %>">
 <HeaderStyle />  <Columns> 
                                                       <asp:TemplateField HeaderText="#" HeaderStyle-Width="25px" HeaderStyle-ForeColor="black" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSNo" runat="server" Width="35px" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Product Name" HeaderStyle-ForeColor="Black"  HeaderStyle-BackColor="#0097AC" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle"
                                                HeaderStyle-HorizontalAlign="Justify">
                                                <ItemTemplate>
                                                    <asp:Label ID="detailname" runat="server" Width="125px" Text='<%# Eval("Product_Detail_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                                 </asp:TemplateField>
                                                 
                                                
                                             <asp:TemplateField HeaderText="Pack" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Left"  FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle" HeaderStyle-BackColor="#0097AC" 
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="pack" runat="server" Text='<%# Eval("Product_Sale_Unit") %>' Width="60px"  ></asp:Label>
                                    </ItemTemplate>
                                                 </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Quantity" HeaderStyle-ForeColor="Black" 
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtprice" runat="server"  Width="45px" MaxLength="5" onpaste="return false"  CssClass="dd" onkeyup="FetchData(this)" rel="<%# Container.DataItemIndex  %>" BackColor="#E3FDF8"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtprice">
                                                    </asp:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <%--  <asp:TemplateField  HeaderStyle-ForeColor="Black"  ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC" 
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                             
                                                    <asp:Label ID="ff" runat="server" Width="20px"  ></asp:Label>
                                    </ItemTemplate>
                                                 </asp:TemplateField>--%>
                                           <asp:TemplateField  HeaderStyle-ForeColor="black" HeaderText="Value"  ItemStyle-HorizontalAlign="Left" 
                                                HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center" FooterStyle-VerticalAlign="Middle">
                                                <ItemTemplate> 
                                                
                                                 <asp:TextBox ID="ffvalue" runat="server"  Width="60px" MaxLength="5"  CssClass="dtxt" onkeyup="FetchData(this)" rel="<%# Container.DataItemIndex %>" ></asp:TextBox>
                                              
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField >
                                                <ItemTemplate>
                                                 <asp:Label ID="product" runat="server" Width="100px" Text='<%# Eval("Retailor_Price") %>' ForeColor="White" ></asp:Label>
                                                   <%-- <asp:TextBox ID="txtQuantity" runat="server"  Width="45px" Text='<%# Eval("Retailor_Price") %>' MaxLength="5"  onkeyup="FetchData(this)"></asp:TextBox>
                                                  --%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                           
                                          
    </Columns> 
      <HeaderStyle  BorderColor="Black"  BorderWidth="1px" BorderStyle="Solid" BackColor="#0097AC" Font-Bold="True" ForeColor="Black" />
    <FooterStyle BackColor="#ffffff" Font-Bold="True" ForeColor="Black" />
    </asp:GridView>
     <asp:Label ID="Label9" runat="server" Text="____________________________________________________________________________"></asp:Label> 
   <asp:Label ID="Label7" runat="server" Text="Total Amount-" Font-Bold="True" ForeColor="Black" Font-Names="Andalus" Font-Size="14px" BackColor="#cfddfa" BorderColor="#f9a799" BorderStyle="Solid" BorderWidth="1px"></asp:Label>  <asp:TextBox ID="gridsum"  CssClass="dd"  runat="server" onkeyup="FetchData(this)"  rel="<%# Container.ItemIndex +1 %>" ></asp:TextBox>
      <asp:Label ID="Label10" runat="server" Text="____________________________________________________________________________"></asp:Label> 
      <div class="footer" align="right">
  
        <asp:Button ID="btnYes" runat="server" Text="Save" UseSubmitBehavior="false"  CausesValidation="false" OnClientClick='<%# "return CheckToochSelection(" + Container.ItemIndex +");" %>'    CssClass="yes"  />
        
   
    </div>
  <%--  <asp:TextBox ID="TextBox1" runat="server"  Width="45px" MaxLength="5" CssClass="dd" Text="2" onkeyup="FetchData(this)"></asp:TextBox>--%>
 
</asp:Panel>
 
</td>
</tr>

</table> </ItemTemplate>
<FooterTemplate >
    <br>
</br>
     <br>
  
</br>
<asp:Label style="padding-left:400px;" Visible='<%#bool.Parse((DataList1.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="No Record Found!" Font-Bold="True" Font-Size="18px" ForeColor="#009999"></asp:Label>
</FooterTemplate>
                                 <SelectedItemStyle Font-Names="Andalus"  /> </asp:DataList>
                                <%--  </ContentTemplate>
 </asp:UpdatePanel>--%>
                                 <asp:Label runat="server" ID="NoRecord" Visible="false" Text="No Record Found!" Font-Bold="True" Font-Size="18px" ForeColor="#009999"></asp:Label>
      <table>
    
      <tr><td>
          <asp:Label ID="totalval" runat="server" Font-Bold="True" Text=" Total Amount-" ForeColor="Black" Font-Names="Andalus" Font-Size="14px" BackColor="#cfddfa" BorderColor="#f9a799" BorderStyle="Solid" BorderWidth="1px"></asp:Label></td><td>
              <asp:TextBox ID="tot" runat="server" Font-Bold="True" 
                style="padding-right:60px" Width="45px" Font-Names="Andalus"  ForeColor="Red" Text-align="Center"
                  BackColor="#d6e9c6" BorderColor="#00CCFF" BorderStyle="Solid" 
                  BorderWidth="2px" ></asp:TextBox>
             </td></tr>
             <tr><td>&nbsp</td></tr>
      <tr><td>                        
 <asp:Button ID="submit1" runat="server" Text="Final Submit" Width="80px" Height="25px" CssClass="savebutton"
                        OnClick="btnSubmit_Click" Font-Names="Andalus" /></td></tr> </table> 

</div>
 
</asp:Content>


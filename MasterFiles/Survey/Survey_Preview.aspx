<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survey_Preview.aspx.cs" Inherits="MasterFiles_Survey_Survey_Preview" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey Preview</title>
       <style type="text/css">
        .style1 {
            width: 100%;
            margin-left: 0px;
            height: 200px;
        }

        .style2 {
            width: 150px;
        }

        .menu {
            height: 38px;
            top: 152px;
            left: 13px;
            position: absolute;
            width: 550px;
        }

        .style4 {
            width: 85%;
            color: #000066;
            font-size: xx-large;
            font-family: "Copperplate Gothic Bold";
            height: 76px;
        }

        .style5 {
            width: 15%;
            height: 76px;
        }

        h1 {
            margin: .2em 0 0;
            color: #003399;
            font-size: 40px;
            text-shadow: 0 1px 0 #cccccc, 0 2px 0 #c9c9c9, 0 3px 0 #bbbbbb, 0 4px 0 #b9b9b9, 0 5px 0 #aaaaaa, 0 6px 1px rgba(0, 0, 0, 0.1), 0 0 5px rgba(0, 0, 0, 0.1), 0 1px 3px rgba(0, 0, 0, 0.3), 0 3px 5px rgba(0, 0, 0, 0.2), 0 5px 10px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.2), 0 20px 20px rgba(0, 0, 0, 0.15);
        }

        body {
            background-color: white;
        }

        .boxes {
            margin: 10px 0px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
        .autocomplete_completionListElement
{
    margin : 0px!important ;
    background-color : inherit ;
    color : windowtext ;
    border : buttonshadow ;
    border-width : 1px ;
    border-style : solid ;
    cursor : 'default' ;
    overflow : auto ;
    height : 100px ;
    font-family : Tahoma ;
    font-size : small ;
    text-align : left ;
    list-style-type : none ;
    }
/* AutoComplete highlighted item */
.autocomplete_highlightedListItem
   {
    background-color : #ffff99 ;
    color : black ;
    padding : 1px ;
    }

    /* AutoComplete item */
.autocomplete_listItem
    {
    background-color : window ;
    color : windowtext ;
    padding : 1px ;
   }
.Search{
width:480px;height:49px; border:3px solid black;

font-size:22px;color:blue;
background-image:url('images/search.jpg');
background-repeat:no-repeat;
background-position:center;outline:0;
}


  .AutoExtender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            padding: 10px;
            background-color: White;
          Width:250px;
            overflow-y:scroll;
            height:200px;
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Black;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
         .Hide
    {
        display: none;
    }
     .panelbtn
        {
            float: right;
            margin-right: 30px;
        }
        #customers
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 70%;
        }
        
        #customers td, #customers th
        {
            border: 1px solid #ddd;
            padding: 8px;
        }
        
        #customers tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        #customers tr:hover
        {
            background-color: #ddd;
        }
            #customers tr
        {
            background-color: white;
        }
        
        #customers th
        {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: left;
            background-color: #4D67A2;
            color: white;
        }
        #rcorners3 
        {
            align:center;
    border-radius: 25px;
    background: url(../../Images/Paper.gif);
    background-position: left top;
    background-repeat: repeat;
    padding: 20px; 
    width: 80%;
    height: 100%;    
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <center>
            <h1 id="head" runat="server">Survey Preview</h1>
        </center>
       <asp:HiddenField ID="hidSurveyId" runat="server" />
     <asp:Panel ID="pnl" runat="server" CssClass="panelbtn">
            <asp:Button ID="btnBack" runat="server" Text="Back" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="25px" Width="80px" OnClick="btnBack_Click" 
                 />
        </asp:Panel>
        <br />
        <br />
    <center>
         <table id="customers" width="90%">
                <tr>
                    <th rowspan="2">Survey Detail</th>
                    
                    <td>
                        Survey Title
                    </td>
                    <td>
                      Creation Date
                    </td>
                    <td>
                        Effective From
                    </td>
                    <td>
                       Effective To
                    </td>
                   <%-- <td>
                       No of Questions
                    </td>--%>
                 
                </tr>
                <tr>
                   
                
                    <td>
                     <asp:Label ID="lbltitle" Font-Bold="true" ForeColor="BlueViolet" runat="server"></asp:Label>
                    </td>
                     <td>
                        <asp:Label ID="lblcrt" Font-Bold="true" ForeColor="BlueViolet" runat="server"></asp:Label> 
                    </td>
                    <td>
                       <asp:Label ID="lblefffrom" Font-Bold="true" ForeColor="BlueViolet" runat="server"></asp:Label> 
                    </td>
                    <td>
                       <asp:Label ID="lbleffto" Font-Bold="true" ForeColor="BlueViolet" runat="server"></asp:Label> 
                    </td>
                   
                  <%--  <td>--%>
                          <asp:Label ID="lblques" Font-Bold="true" ForeColor="BlueViolet" Visible="false" runat="server"></asp:Label> 
                   <%-- </td>--%>
                </tr>
            </table>
        <br />
          <asp:GridView ID="GridView1" runat="server" BackColor="#4D67A2" Font-Bold="False" Font-Size="Medium"
        ForeColor="Black" HorizontalAlign="Center" Width="100%" AllowSorting="true" CellPadding="2" AutoGenerateColumns="false" 
                OnRowCreated="GridView1_RowCreated" AllowPaging="true" PageSize="10" PagerStyle-CssClass="padding"   onpageindexchanging="GridView1_PageIndexChanging" 
           >
            <FooterStyle BackColor="#86AEFC" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#4D89BD" ForeColor="White" Height="16px" Font-Bold="false"
            Font-Size="Small" HorizontalAlign="Left" Font-Names="Verdana" BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                <PagerStyle BackColor="#4D89BD" ForeColor="White" Height="12px" Font-Bold="true" 
            Font-Size="Small" HorizontalAlign="Left" Font-Names="Verdana" BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
        <RowStyle BackColor="#EFF3FB" Font-Names="Verdana" Font-Size="13px" ForeColor="#400000" />
             <SelectedRowStyle BackColor="#ADAEB4" ForeColor="GhostWhite" />
        <AlternatingRowStyle BackColor="White" />
            <Columns>
                  <asp:TemplateField HeaderText="S.No">
                           <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" Height="16px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (GridView1.PageIndex * GridView1.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
               
                  <asp:BoundField DataField="id" HeaderText="Id" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" >
                       <HeaderStyle BorderColor="Black" Height="16px" 
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                <asp:BoundField DataField="Question" HeaderText="Question Name"  HeaderStyle-Width="500px" >
                      <HeaderStyle BorderColor="Black" Height="16px" 
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                <asp:BoundField DataField="Control" HeaderText="Control Type" >
                      <HeaderStyle BorderColor="Black" Height="16px" 
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                 <asp:TemplateField HeaderText="Question Choice" ItemStyle-HorizontalAlign="Left">
                          <HeaderStyle BorderColor="Black" Height="16px"  
             BorderWidth="1px" BorderStyle="Solid" />
                <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
           
                <ItemTemplate>
                    <asp:Panel ID="pnlAnswerOptions" runat="server">
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
                   <asp:BoundField DataField="Date" HeaderText="Date" >
                         <HeaderStyle BorderColor="Black" Height="16px" 
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                <asp:TemplateField HeaderText="Process Type" >
                     <HeaderStyle BorderColor="Black" Height="16px" 
             BorderWidth="1px" BorderStyle="Solid" />
                    <ItemTemplate>
                        <asp:CheckBoxList ID="chkpro" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="Drs." Value="D" >    </asp:ListItem>                      
                            
                             <asp:ListItem Text="Chm." Value="C" >    </asp:ListItem>                           
                          <asp:ListItem Text="Hos." Value="H" >    </asp:ListItem> 
                             <asp:ListItem Text="Stk." Value="S" >    </asp:ListItem> 
                             <asp:ListItem Text="Prd." Value="P" >    </asp:ListItem> 
                        </asp:CheckBoxList>
                    </ItemTemplate>
                </asp:TemplateField>
              
            </Columns>
        </asp:GridView>

    </center>
    </div>
    </form>
</body>
</html>

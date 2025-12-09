<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survey_Creation.aspx.cs" Inherits="MasterFiles_Survey_Survey_Creation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Create - Survey</title>
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

       
         .head2 {
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
    </style>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.7.js" type="text/javascript"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/jquery-ui.js" type="text/javascript"></script>
    <link href="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.16/themes/humanity/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        var j = jQuery.noConflict();
        j(document).ready(function () {
            j('.ProcessDate').datepicker
            ({
                changeMonth: true,
                changeYear: true,
                yearRange: '1930:' + new Date().getFullYear().toString(),
                //                yearRange: "2010:2017",
                dateFormat: 'dd/mm/yy'
            });
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0');
            var yyyy = today.getFullYear();


            //today = dd + '/' + mm + '/' + yyyy;


            //$('#txtdate').val(today);
        });


    </script>
 
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                $("#btnAddQuestion").bind("click", function () {
                    if ($("#txtsurvey").val() == "") { alert("Enter Title."); $('#txtsurvey').focus(); return false; }
                    if ($("#txtFdate").val() == "") { alert("Enter From Date."); $('#txtFdate').focus(); return false; }
                    if ($("#txtTdate").val() == "") { alert("Enter To Date."); $('#txtTdate').focus(); return false; }
                 
                    var startDate = new Date($('#txtFdate').val());
                    var endDate = new Date($('#txtTdate').val());
                    if (startDate > endDate) {
                        alert("To date should be greater than From date"); $('#txtTdate').focus(); return false;
                    }
                   // if ($("#txtno").val() == "") { alert("Enter No of Question."); $('#txtno').focus(); return false; }
                });
            });
            </script>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ToolkitScriptManager>
        <center>
            <h1 id="head" runat="server" class="head2">Create - Survey</h1>
        </center>
       <asp:HiddenField ID="hidSurveyId" runat="server" />
     <asp:Panel ID="pnl" runat="server" CssClass="panelbtn">
            <asp:Button ID="btnQues" runat="server" Text="Create - Question" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="150px"
                    OnClick="btnQues_Click" />&nbsp;
          <asp:Button ID="btnADD" runat="server" Text="Update - Survey" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="150px"
                    OnClick="btnADD_Click" />&nbsp;
          <asp:Button ID="btnview" runat="server" Text="View" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="80px" 
                 />&nbsp;
            <asp:Button ID="btnBack" runat="server" Text="Home" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="80px" OnClick="btnBack_Click" 
                 />
        </asp:Panel>
        <br />
        <br />
        <asp:Image ID="image1" ImageUrl="~/MasterFiles/Survey/images/Question.jpeg" Width="450px" Height="220px" runat="server" />
                    <table bgcolor="White" border="0" align="right" width="60%"
                        style="border: 1px; border-color: #003366; left: 302px;"
                        frame="border">
                         <tr>
                            <td bgcolor="#003399" colspan="2">
                                <asp:Label ID="Label4" runat="server" ForeColor="White" Text="Question Selection"
                                    Font-Size="X-Large" Font-Names="Copperplate Gothic Bold" Font-Bold="False"
                                    Style="text-align: center" align="center"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="4"></td>
                        </tr>

                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblsurvey" runat="server" Text="Title of Survey" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtsurvey" runat="server" Width="200px" SkinID="MandTxtBox" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label1" runat="server" Text="Process From Date" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtFdate" SkinID="MandTxtBox" runat="server" onkeypress="Calendar_enter(event);"  autocomplete="off" CssClass="input ProcessDate form-control"></asp:TextBox>

                    </td>
                </tr>

                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label2" runat="server" Text="Process To Date" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtTdate" SkinID="MandTxtBox" runat="server" onkeypress="Calendar_enter(event);" autocomplete="off" CssClass="input ProcessDate form-control"></asp:TextBox>

                    </td>
                </tr>
                        <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label5" runat="server" Text="No of Questions" SkinID="lblMand" Visible="false"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="txtno" SkinID="MandTxtBox" runat="server"  autocomplete="off" Visible="false"></asp:TextBox>

                    </td>
                </tr>
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="Label3" runat="server" Text="Question Search" SkinID="lblMand"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" CssClass="Search" SkinID="MandTxtBox" Width="500px" OnTextChanged="TextBox1_TextChanged"
                            ></asp:TextBox>

                        <%--  <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="AutoCompleteQues"
                                                    ServicePath="~/MasterFiles/MR/ListedDoctor/Webservice/AutoComplete.asmx" Mini mumPrefixLength="1"
                                                    CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="TextBox1"
                                                    FirstRowSelected="false">
                                                </asp:AutoCompleteExtender>--%>
                        <%--<asp:AutoCompleteExtender ServiceMethod="GetCompletionList" MinimumPrefixLength="1"
                            CompletionInterval="10" EnableCaching="false" CompletionSetCount="1" TargetControlID="TextBox1"
                            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
                        </asp:AutoCompleteExtender>--%>
                        <%--<asp:AutoCompleteExtender
runat="server"
ID="AutoCompleteExtender1"
TargetControlID="TextBox1"

ServiceMethod="GetCompletionList"
MinimumPrefixLength="1"
CompletionInterval="1"
EnableCaching="true"
CompletionSetCount="1"  CompletionListCssClass="autocomplete_completionListElement"

  CompletionListItemCssClass="autocomplete_listItem"

  CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" />--%>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList"
                                             MinimumPrefixLength="1"
                                             CompletionInterval="100" EnableCaching="false"
                                            CompletionSetCount="10" CompletionListCssClass="AutoExtender"
                                            CompletionListItemCssClass="AutoExtenderList" CompletionListElementID="divwidth"
                                            CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                             TargetControlID="TextBox1" FirstRowSelected="false">
                                        </asp:AutoCompleteExtender>    
                    </td>
                </tr>
            </table>

      
       <br />

        <br />
        <center>
        <asp:GridView ID="GridView1" runat="server" BackColor="#4D67A2" Font-Bold="False" Font-Size="Medium"
        ForeColor="Black" HorizontalAlign="Center" Width="100%" AllowSorting="true" CellPadding="2" AutoGenerateColumns="false" OnRowDeleting="OnRowDeleting" OnRowDataBound="OnRowDataBound"
           >
            <FooterStyle BackColor="#86AEFC" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#4D89BD" ForeColor="White" Height="12px" Font-Bold="false"
            Font-Size="Small" HorizontalAlign="Left" Font-Names="Verdana" BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
        <RowStyle BackColor="#EFF3FB" Font-Names="Verdana" Font-Size="13px" ForeColor="#400000" />
             <SelectedRowStyle BackColor="#ADAEB4" ForeColor="GhostWhite" />
        <AlternatingRowStyle BackColor="White" />
            <Columns>
                  <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="30px">
                           <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
               
                  <asp:BoundField DataField="id" HeaderText="Id" ItemStyle-CssClass="Hide" HeaderStyle-CssClass="Hide" >
                       <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                <asp:BoundField DataField="Question" HeaderText="Question Name"  HeaderStyle-Width="500px" >
                      <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                <asp:BoundField DataField="Control" HeaderText="Control Type" >
                      <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                   <asp:BoundField DataField="Date" HeaderText="Date" >
                         <HeaderStyle BorderColor="Black" Width="100px"
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                <asp:TemplateField HeaderText="Process Type">
                     <HeaderStyle BorderColor="Black"
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
                <asp:CommandField ShowDeleteButton="True" ButtonType="Button">
                       <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                    </asp:CommandField>
            </Columns>
        </asp:GridView>
            <br />
            <asp:Button ID="btnAddQuestion" runat="server" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="25px" Width="160px" Text="Create Survey" OnClick="btnAddQuestion_Click" />
            </center>
          <table style="margin-top: 40px; visibility: hidden">
            <tr>
                <td>Question Name
                </td>
                <td>Control Name
                </td>
                <td>Control Para
                </td>
                <td>Question Add Names
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtques" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtcont" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtpara" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtid" runat="server"></asp:TextBox>
                </td>

            </tr>
        </table>

    </form>
</body>
</html>

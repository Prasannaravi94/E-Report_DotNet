<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survey_Process_Screen.aspx.cs" Inherits="MasterFiles_Survey_Survey_Process_Screen" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Processing Zone</title>
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

        .head {
            margin: .2em 0 0;
            color: #003399;
            font-size: 40px;
            font-weight:bold;
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
    <%--<script type="text/javascript">
        $(document)["ready"](function () {
            $("#loading")["show"]();
            var e = document.getElementById("ddlwise");
            var mode = e.options[e.selectedIndex].value;
            if (mode == 0) {
                document.getElementById('ddlFrom').style.display = "none";
                document.getElementById('ddlTo').style.display = "none";
                document.getElementById('lbljoin').style.display = "none";
                document.getElementById('lblSt').style.display = "none";
                document.getElementById('ddlst').style.display = "none";
                document.getElementById('lblDesig').style.display = "none";
                document.getElementById('ddlDesig').style.display = "none";
                document.getElementById('lblsub').style.display = "none";
                document.getElementById('ddlsubdiv').style.display = "none";
                // document.getElementById('btngo').style.display = "none";
            }
            var TMonth = $("#ddlFrom").val();
            var TYear = $("#ddlTo").val();
            var St = $("#ddlst").val();
            var Sub = $("#ddlsubdiv").val();
            var Data = mode + "^" + TMonth + "^" + TYear + "^" + St + "^" + Sub
            $["ajax"]({
                type: "POST",
                url: "../webservice/Quiz_QuestionWS.asmx/BindUserList",
                data: '{objData:' + JSON.stringify(Data) + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (variable_0) {
                    if (variable_0["d"]["length"] > 0) {
                        var variable_1 = "<thead><tr>";
                        variable_1 += "<th style=\"width:34px\"><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" onchange=\"checkAll(this)\"/><div><input type=\"checkbox\" id=\"chkHead\" class=\"selectAll\" style=\"border-color:white\" onchange=\"checkAll(this)\"/></div></th>";
                        variable_1 += "<th style=\"width:33px\">S.No<div>S.No</div></th>";
                        variable_1 += "<th style=\"width:270px\">Field Force Name<div>Field Force Name</div></th>";
                        variable_1 += "<th style=\"width:130px\">HQ<div>HQ</div></th>";
                        variable_1 += "<th style=\"width:10px\">Designation<div>Designation</div></th>";
                        variable_1 += "<th style=\"width:167px\">State<div>State</div></th>";
                        variable_1 += "<th style=\"display:none\">SF Code<div>SF Code</div></th>";
                        variable_1 += "</tr></thead>";
                        variable_1 += "<tbody>";
                        for (var variable_2 = 0; variable_2 < variable_0["d"]["length"]; variable_2++) {
                            variable_1 += "<tr>";
                            if (variable_0["d"][variable_2]["ChkBox"] == "True") {
                                variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\"><input type=\"checkbox\" class = \"chcktbl\" disabled=\"disabled\" style=\"display:none;border-color:Red;\"  id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                                variable_1 += "<td style=\"width:50px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                                variable_1 += "<td id=\"SFName\" style=\"width:250px;color:Blue;text-decoration: line-through\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                            } else {
                                variable_1 += "<td style=\"width:50px\"><input type=\"checkbox\" class = \"chcktbl\" id=\"delit_" + variable_0["d"][variable_2]["RowNo"] + "\" /></td>";
                                variable_1 += "<td style=\"width:50px\">" + variable_0["d"][variable_2]["RowNo"] + "</td>";
                                variable_1 += "<td id=\"SFName\" style=\"width:250px;\">" + variable_0["d"][variable_2]["FieldForceName"] + "</td>"
                            };
                            variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["HQ"] + "</td>";
                            variable_1 += "<td style=\"width:100px\">" + variable_0["d"][variable_2]["Designation"] + "</td>";
                            variable_1 += "<td style=\"width:150px\">" + variable_0["d"][variable_2]["State"] + "</td>";
                            variable_1 += "<td style=\"display:none\">" + variable_0["d"][variable_2]["SF_Code"] + "</td>";
                            variable_1 += "</tr>"
                        };
                        variable_1 += "</tbody>";
                        $("#tblUserList")["html"](variable_1);
                        $("#loading")["hide"]()
                    }
                    else {

                    }
                },
                error: function (variable_3) {

                }
            });
        });
                    
                    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
         <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="../../css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../JsFiles/bootstrap-3.0.3.min.js"></script>
    <link href="../../css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    <script src="../../JsFiles/bootstrap-multiselect.js"
        type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstsub]').multiselect({
                enableFiltering: true,
                maxHeight: 250,
                enableCaseInsensitiveFiltering: true,
                includeSelectAllOption: true,
                buttonWidth: '250px'
            });
            $('[id*=lstst]').multiselect({
                enableFiltering: true,
                enableCaseInsensitiveFiltering:true,
                maxHeight: 250,
                includeSelectAllOption: true,
                buttonWidth: '250px'
            });
            $('[id*=lstSf]').multiselect({
                enableFiltering: true,
              
                enableCaseInsensitiveFiltering: true,
                maxHeight: 250 ,
                includeSelectAllOption: true,
                buttonWidth: '250px'
            });
            $('[id*=lstCat]').multiselect({
                enableFiltering: true,
                maxHeight: 250,
                enableCaseInsensitiveFiltering: true,
                includeSelectAllOption: true,
                buttonWidth: '250px'
            });
            $('[id*=lstCls]').multiselect({
                enableFiltering: true,
                enableCaseInsensitiveFiltering: true,
                maxHeight: 250,
                includeSelectAllOption: true,
                buttonWidth: '250px'
            });
            $('[id*=lstSpec]').multiselect({
                enableFiltering: true,

                enableCaseInsensitiveFiltering: true,
                maxHeight: 250,
                includeSelectAllOption: true,
                buttonWidth: '250px'
            });
            $('[id*=lstChem]').multiselect({
                enableFiltering: true,

                enableCaseInsensitiveFiltering: true,
                maxHeight: 250,
                includeSelectAllOption: true,
                buttonWidth: '250px'
            });
            $('[id*=lsthq]').multiselect({
                enableFiltering: true,

                enableCaseInsensitiveFiltering: true,
                maxHeight: 250,
                includeSelectAllOption: true,
                buttonWidth: '250px'
            });
            $('[id*=lstHos]').multiselect({
                enableFiltering: true,

                enableCaseInsensitiveFiltering: true,
                maxHeight: 250,
                includeSelectAllOption: true,
                buttonWidth: '250px'
            });
        });
    </script>

      
      <%--   <script type="text/javascript">
             function showDrop(select) {

                 if (select.value == 0) {
                   
                     document.getElementById('lblSt').style.display = "none";
                     document.getElementById('lstst').style.display = "none";                   
                     document.getElementById('lblsub').style.display = "none";
                     var x = document.getElementById('lstsub');
                     x.style.visibility = "hidden";
                   //  document.getElementById('lstsub').style.display = 'none';
                     lstsub.Style.Add("display", "none");
                     //  document.getElementById('btngo').style.display = "none";
                 }
                 else if (select.value == 1) {
                    
                     document.getElementById('lblSt').style.display = "block";
                     document.getElementById('lstst').style.display = "block";                  
                     document.getElementById('lblsub').style.display = "";
                     document.getElementById('lstsub').style.display = "";
                     //  document.getElementById('btngo').style.display = "block";

                 }

                 else if (select.value == 2) {
                    
                     document.getElementById('lblSt').style.display = "none";
                     document.getElementById('lstst').style.display = "none";                    
                     document.getElementById('lblsub').style.display = "none";
                     document.getElementById('lstsub').style.display = "none";

                 }
                 else if (select.value == 3) {
                   
                     document.getElementById('lblSt').style.display = "none";
                     document.getElementById('lstst').style.display = "none";
                    
                     document.getElementById('lblsub').style.display = "block";
                     document.getElementById('lstsub').style.display = "block";

                 }
             }
    </script>--%>
    <div>
     <center>
            <asp:Label id="head" class="head" runat="server">Processing Zone</asp:Label>
        </center>
       <asp:HiddenField ID="hidSurveyId" runat="server" />
     <asp:Panel ID="pnl" runat="server" CssClass="panelbtn">
            <asp:Button ID="btnBack" runat="server" Text="Back" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="80px" OnClick="btnBack_Click" 
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
                    <td>
                       No of Questions
                    </td>
                 
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
                   
                    <td>
                          <asp:Label ID="lblques" Font-Bold="true" ForeColor="BlueViolet" runat="server"></asp:Label> 
                    </td>
                </tr>
            </table>
        <br />
         <center>
        <table width="60%" >
            <tr>
                <th  align="left" class="stylespc" colspan="6" style="background-color:lightblue;text-align:center; font-family:Copperplate Gothic Bold; font-weight:bold; color:black">
                    <asp:Label ID="lblFilter" runat="server">Fieldforce Selection</asp:Label>
                </th>
             
            </tr>
             <tr style="border-style:none">
                 <td>
                    <br />

                 </td>
                 </tr>
            <tr>
                <td align="left" class="stylespc" style="border-style:none">
                    <asp:Label ID="lblSt" runat="server">State</asp:Label>
                </td>
                <td align="left" class="stylespc" >
                   
                      <asp:ListBox ID="lstst" runat="server" SelectionMode="Multiple" Width="250px" AutoPostBack="true"  OnSelectedIndexChanged="lstst_SelectedIndexChanged" 
                            ></asp:ListBox>
                </td>
          
                <td align="left" class="stylespc">
                    <asp:Label ID="lblsub" runat="server">Subdivision</asp:Label>
                </td>
                <td align="left" class="stylespc">
                  <asp:ListBox ID="lstsub" runat="server" SelectionMode="Multiple" Width="250px" AutoPostBack="true"  OnSelectedIndexChanged="lstsub_SelectedIndexChanged" 
                            ></asp:ListBox>
                </td>
            
                <td align="left" class="stylespc">
                    <asp:Label ID="Label1" runat="server">Team</asp:Label>
                </td>
                <td align="left" class="stylespc" >
                  <asp:ListBox ID="lstSf" runat="server" SelectionMode="Multiple" Width="50px"
                            ></asp:ListBox>
                </td>
            </tr>
               <tr >
                 <td>
                    <br />

                 </td>
                 </tr>
           
              <tr>
                <th  align="left" class="stylespc" colspan="6"  style="background-color:lightblue;text-align:center; font-family:Copperplate Gothic Bold; font-weight:bold; color:black">
                    <asp:Label ID="Label2" runat="server">Listed Doctor Selection</asp:Label>
                </th>
             
            </tr>
             <tr >
                 <td>
                    <br />

                 </td>
                 </tr>
            <tr>
                <td align="left" class="stylespc" style="border-style:none">
                    <asp:Label ID="Label3" runat="server">Category</asp:Label>
                </td>
                <td align="left" class="stylespc" >
                   
                      <asp:ListBox ID="lstCat" runat="server" SelectionMode="Multiple" Width="250px" 
                            ></asp:ListBox>
                </td>
          
                <td align="left" class="stylespc">
                    <asp:Label ID="Label4" runat="server">Speciality</asp:Label>
                </td>
                <td align="left" class="stylespc">
                  <asp:ListBox ID="lstSpec" runat="server" SelectionMode="Multiple" Width="250px"
                            ></asp:ListBox>
                </td>
            
                <td align="left" class="stylespc">
                    <asp:Label ID="Label5" runat="server">Class</asp:Label>
                </td>
                <td align="left" class="stylespc" >
                  <asp:ListBox ID="lstCls" runat="server" SelectionMode="Multiple" Width="50px"
                            ></asp:ListBox>
                </td>
            </tr>
              <tr >
                 <td>
                    <br />

                 </td>
                 </tr>
           
              <tr>
                <th  align="left" class="stylespc" colspan="6"  style="background-color:lightblue;text-align:center; font-family:Copperplate Gothic Bold; font-weight:bold; color:black">
                    <asp:Label ID="Label6" runat="server">Chemist/Hospital/Stockist Selection</asp:Label>
                </th>
             
            </tr>
             <tr >
                 <td>
                    <br />

                 </td>
                 </tr>
            <tr>
                <td align="left" class="stylespc" style="border-style:none">
                    <asp:Label ID="Label7" runat="server">Hospital</asp:Label>
                </td>
                <td align="left" class="stylespc" >
                   
                      <asp:ListBox ID="lstHos" runat="server" SelectionMode="Multiple" Width="250px" 
                            ></asp:ListBox>
                </td>
          
                <td align="left" class="stylespc">
                    <asp:Label ID="Label8" runat="server">Chemist</asp:Label>
                </td>
                <td align="left" class="stylespc">
                  <asp:ListBox ID="lstChem" runat="server" SelectionMode="Multiple" Width="250px"
                            ></asp:ListBox>
                </td>
            
                <td align="left" class="stylespc">
                    <asp:Label ID="Label9" runat="server">Stockist HQ</asp:Label>
                </td>
                <td align="left" class="stylespc" >
                  <asp:ListBox ID="lsthq" runat="server" SelectionMode="Multiple" Width="50px"
                            ></asp:ListBox>
                </td>
            </tr>
            </table>
             <br />

                 <asp:Button ID="btnProcess" runat="server" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="170px" Text="Process"  OnClick="btnProcess_Click"/>
        </center>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survey_Ques_Creation.aspx.cs" Inherits="MasterFiles_Survey_Survey_Ques_Creation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create - Question</title>
    <script language="javascript" type="text/javascript">
        function showDrop(dropdown) {
            var myindex = dropdown.selectedIndex
            var SelValue = dropdown.options[myindex].value
         
            $('#txtNoOfOption').val("");
            $('#txtmax').val("");
            $("#boxes").empty();
            if (SelValue == 1 || SelValue == 2) {
                document.getElementById("trAnswer").style.display = 'none';
                document.getElementById("trmax").style.display = '';
               document.getElementById("trNoOfOption").style.display = 'none';
            }
            else if (SelValue == 3 ||  SelValue == 4) {
                document.getElementById("trAnswer").style.display = '';
                document.getElementById("trmax").style.display = 'none';
                document.getElementById("trNoOfOption").style.display = '';
            }
        }

    </script>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
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
          .Hide
    {
        display: none;
    }
    </style>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
            font-size: 9pt;
        }

        .label {
            display: inline-block;
            font-size: 9pt;
            color: black;
            font-family: Verdana;
        }

        .dropDown {
            height: 27px;
            width: 182px;
            font-size: 8pt;
            color: #000000;
            padding: 1px 3px 0.2em;
            height: 25px;
            border-top-style: groove;
            font-family: Verdana;
            border-right-style: groove;
            border-left-style: groove;
            border-bottom-style: groove;
        }

        .Textbox {
            font-size: 8pt;
            color: black;
            border-top-style: groove;
            border-right-style: groove;
            border-left-style: groove;
            height: 22px;
            padding-left: 4px;
            background-color: white;
            border-bottom-style: groove;
        }
         .panelbtn {
            float: right;
          
        }
    </style>


    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
  
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>



    <script type="text/javascript">

        <%--        function preventMultipleSubmissions() {

            $('#<%=btnAddQuestion.ClientID %>').prop('disabled', true);

        }

        window.onbeforeunload = preventMultipleSubmissions;--%>

    </script>
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
            width:250px;
        }
         .panelbtn {
            float: right;
          
             margin-top: 10px;
        }
         .padding{
          
             padding: 0 6px;
         }
    </style>
</head>
<body style="background-color: white">
    <form id="form1" runat="server">
        <script type="text/javascript">
            $(document).ready(function () {
                document.getElementById("trAnswer").style.display = 'none';
                document.getElementById("trNoOfOption").style.display = 'none';
                document.getElementById("trmax").style.display = 'none';
                var add = $('#add_boxes');
                var all = $('#boxes');
                var amountOfInputs = 0;
                var maximumBoxes = 10;

                $("#txtNoOfOption").keyup(function () {


                    var str = $("#txtNoOfOption").val();
                    // create a limit
                    if (str >= maximumBoxes) {
                        alert("You cannot have more than 10 boxes!");
                        $("#boxes").empty();
                        return;
                    }

                    // var listItem = $('<li class="box"></li>');
                    // we will add 2 boxes here, but we can modify this in the amountOfBoxes value
                    $("#boxes").empty();

                    for (var i = 0, j = 1; i < str; i++) {

                        all.append(' ' + j + ' <input type="text" name="tst" class="boxes"/> <br>');
                        j++;
                    }
                    // listItem.append('<input type="text" class="output" name="value" />');
                    // Lets add a link to remove this group as well, with a removeGroup class
                    //  listItem.append('<input type="button" value="Remove" class="removeGroup" />')
                    //  listItem.appendTo(all);
                });
                $("#btnAddQuestion").bind("click", function () {
                   
                   
                    var con_id = $('#<%=ddlQuestionType.ClientID%> :selected').val();
                    var con_name = $('#<%=ddlQuestionType.ClientID%> :selected').text();
                    if (con_name == "-- Select Question Type--") { alert("Select Question Type."); $('#ddlQuestionType').focus(); return false; }
                    var con_txt = $('#<%=ddlQuestionType.ClientID%> :selected').text();
                 
                    if ($("#txtQuestionText").val() == "") { alert("Enter Question Text."); $('#txtQuestionText').focus(); return false; }
                    var ques_name = $("#txtQuestionText").val();
                    if (con_id == 1 || con_id == 2) {
                        if ($("#txtmax").val() == "") { alert("Enter Max Length."); $('#txtmax').focus(); return false; }
                        var max = $("#txtmax").val();
                    }
                    else {
                        var max = "";
                        if ($("#txtNoOfOption").val() == "") { alert("Enter No Option."); $('#txtNoOfOption').focus(); return false; }

                    }
                    var values = "";
                    $("input[name=tst]").each(function () {
                        if ($(this).val() != '') {
                            values += $(this).val() + ",";
                        }
                    });
                   
                    $.ajax({
                        type: 'POST',
                        contentType: "application/json; charset=utf-8",
                        url: 'Survey_Ques_Creation.aspx/InsertQus',
                        data: "{'cont_id':'" + con_id + "','cont_txt':'" + con_txt + "','cont_para':'" + max + "', 'Ques_Name':'" + ques_name + "', 'Ques_Add':'" + values + "'}",
                        async: false,
                        success: function (response) {
                            $('#txtQuestionText').val('');
                            $("#ddlQuestionType").val("");
                            window.location.reload(true);
                            alert("Saved Successfully");
                         
                        },
                        error: function () {
                            console.log('there is some error');
                        }
                    });
                });
                // This will tie in ANY input you add to the page. I have added them with the class `input`, but you can use any class you want, as long as you target it correctly.
                //$(document).on("keyup", "input.input", function (event) {
                //    // Get the group
                //    var group = $(this).parent();
                //    // Get the children (all that arent the .output input)
                //    var children = group.children("input:not(.output)");
                //    // Get the input where you want to print the output
                //    var output = group.children(".output");
                //    // Set a value
                //    var value = 0;
                //    // Here we will run through every input and add its value
                //    children.each(function () {
                //        // Add the value of every box. If parseInt fails, add 0.
                //        value += parseInt(this.value) || 0;
                //    });
                //    // Print the output value
                //    output.val(value);
                //});

                // Lets implement your remove field option by removing the groups parent div on click
                //$(document).on("click", ".removeGroup", function (event) {
                //    event.preventDefault();
                //    $(this).parent(".box").remove();
                //});
            });
        </script>
        
            <center>
        <h1>
           Create - Question</h1>
             
    </center>
              <asp:Panel ID="pnl" runat="server" CssClass="panelbtn">
                     <asp:Button ID="btncreate" runat="server" Text="Create - Survey" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="150px"
                    OnClick="btnCreate_Click" />&nbsp;
                <asp:Button ID="btnADD" runat="server" Text="Update - Survey" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="150px"
                    OnClick="btnADD_Click" />&nbsp;
                   <asp:Button ID="btnview" runat="server" Text="View" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="80px" 
                 />&nbsp;
            <asp:Button ID="btnBack" runat="server" Text="Home" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="80px" OnClick="btnBack_Click" 
                 />
        </asp:Panel>
        <br />
           <br />
         <br />
       
                <asp:HiddenField ID="hidSurveyId" runat="server" />
           

                    <asp:Image ID="image1" ImageUrl="~/MasterFiles/Quesionaire/images/survey.jpg" runat="server" />
                     <table bgcolor="White" border="0" align="right" width="60%"
                        style="border: 1px; border-color: #003366; left: 350px;"
                        frame="border">
                        <tr>
                            <td bgcolor="#003399" colspan="2">
                                <asp:Label ID="Label1" runat="server" ForeColor="White" Text="Question Creation"
                                    Font-Size="X-Large" Font-Names="Copperplate Gothic Bold" Font-Bold="False"
                                    Style="text-align: center" align="center"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="4"></td>
                        </tr>

                        <tr>
                            <%--     <table style="height: 38px" id="tblQuestion" class="tbl_Inside">
                                    <tr>--%>

                            <td class="stylespc">
                                <asp:Label ID="lblQuestion_Type" Width="200px" runat="server" CssClass="label" Text="Question Type"></asp:Label>
                            </td>
                            <td class="stylespc">

                                <asp:DropDownList ID="ddlQuestionType" CssClass="dropDown" runat="server" onchange="showDrop(this)">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="stylespc" style="vertical-align: middle">
                                <asp:Label ID="lblQuestionText" runat="server" CssClass="label" Text="Question Text"></asp:Label>
                            </td>
                            <td class="stylespc">
                                <asp:TextBox ID="txtQuestionText" runat="server" CssClass="Textbox" Width="500px"
                                    Height="100px" TextMode="MultiLine">                                        
                                </asp:TextBox>
                            </td>
                        </tr>
                          <tr id="trmax">
                            <td class="stylespc">
                                <asp:Label ID="Label2" runat="server" CssClass="label" Text="Max Length"></asp:Label>
                            </td>
                            <td class="stylespc">
                                <asp:TextBox ID="txtmax" runat="server" CssClass="Textbox" Width="100px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr id="trNoOfOption">
                            <td class="stylespc">
                                <asp:Label ID="lblNoofOption" runat="server" CssClass="label" Text="No Of Option"></asp:Label>
                            </td>
                            <td class="stylespc">
                                <asp:TextBox ID="txtNoOfOption" runat="server" CssClass="Textbox" Width="100px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr id="trAnswer">
                            <td class="stylespc" style="vertical-align: middle"></td>
                            <td class="stylespc">
                                <div id="boxes">
                                </div>

                            </td>
                        </tr>
                        <%-- <tr id="trCorrectAns">
                                        <td class="stylespc">
                                            <asp:Label ID="lblCrctAns" runat="server" CssClass="label" Text=" Correct Answer"></asp:Label>
                                        </td>
                                        <td class="stylespc">
                                            <asp:TextBox ID="txtAns" runat="server" CssClass="Textbox" ReadOnly="true" Width="180px"></asp:TextBox>
                                        </td>
                                    </tr>--%>
                        <tr>

                            <td>
                                <br />
                            </td>
                        </tr>
                        <tr >
                            <td class="stylespc" ></td>
                            <td class="stylespc" >
                                <asp:Button ID="btnAddQuestion" runat="server" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="25px" Width="170px" Text="Add Question" />
                            </td>
                        </tr>
                    </table>
                    <br />

                    <br />
                      <center>
        <asp:GridView ID="GridView1" runat="server" BackColor="#4D67A2" Font-Bold="False" Font-Size="Medium"
        ForeColor="Black" HorizontalAlign="Center" Width="100%" AllowSorting="true" CellPadding="2" AutoGenerateColumns="false" 
              OnRowCommand="GridView1_RowCommand" AllowPaging="true" PageSize="10" PagerStyle-CssClass="padding"   onpageindexchanging="GridView1_PageIndexChanging" 
           >
            <FooterStyle BackColor="#86AEFC" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#4D89BD" ForeColor="White" Height="12px" Font-Bold="false"
            Font-Size="Small" HorizontalAlign="Left" Font-Names="Verdana" BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
             <PagerStyle BackColor="#4D89BD" ForeColor="White" Height="12px" Font-Bold="true" 
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
                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (GridView1.PageIndex * GridView1.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
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
                <asp:BoundField DataField="Control" HeaderText="Control Type"  >
                      <HeaderStyle BorderColor="Black" Width="200px" 
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                   <asp:BoundField DataField="Creation_Date" HeaderText="Creation Date" >
                         <HeaderStyle BorderColor="Black" Width="170px" 
             BorderWidth="1px" BorderStyle="Solid" />
                      </asp:BoundField>
                
             <asp:TemplateField HeaderText="Deactivate" HeaderStyle-Width="100px">
                             <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("id") %>'
                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Question');">Deactivate
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
                          </center>
                    <div class="loading" align="center">
                        Loading. Please wait.<br />
                        <br />
                        <img src="../../Images/loader.gif" alt="" />
                    </div>
               
     
    </form>
</body>
</html>

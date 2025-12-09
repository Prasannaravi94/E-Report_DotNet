<%@ Page Language="C#" MasterPageFile="~/MasterFiles/Quesionaire/UserMasterPage.master" AutoEventWireup="true"
    CodeFile="AddQuestions.aspx.cs" Inherits="MasterFiles_Quesionaire_AddQuestions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script language="javascript" type="text/javascript">

        function QuestionTypeChange(dropdown) {

            var myindex = dropdown.selectedIndex
            var SelValue = dropdown.options[myindex].value
            if (SelValue == 7) {
                document.getElementById("divInputOptions").style.display = 'none';

                document.getElementById("divmas2").style.display = '';
                document.getElementById("divmas").style.display = '';
            }
            else {
                document.getElementById("divInputOptions").style.display = '';

                document.getElementById("divmas").style.display = 'none';
                document.getElementById("divmas2").style.display = 'none';
            }
        }
        function GetSelectedItem() {
            var rb = document.getElementById("<%=RblType.ClientID%>");
            var radio = rb.getElementsByTagName("input");
            var label = rb.getElementsByTagName("label");
            for (var i = 0; i < radio.length; i++) {
                if (radio[i].checked) {
                    if (radio[i].value == "PB") {
                        document.getElementById("divlist").style.display = '';
                    }
                    else {
                        document.getElementById("divlist").style.display = 'none';
                    }
                    break;
                }
            }

            return false;
        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            GetSelectedItem();
            var ans = document.getElementById('<%=ddlAns.ClientID%>');
            if (ans.selectedIndex != 0) {
                if (ans.selectedIndex != 7) {
                    document.getElementById("divInputOptions").style.display = '';

                    document.getElementById("divmas").style.display = 'none';
                    document.getElementById("divmas2").style.display = 'none';
                }
            }
            $("#ctl00_ContentPlaceHolder1_ddlQues").change(function () {

                var Ques = $("#ctl00_ContentPlaceHolder1_ddlQues option:selected").val();

                $('[id*=divAnswer]').hide();
                if ($(this).val() != 0) {
                    PopulateAns($(this).val());
                }

            });
           
        });
           function PopulateAns(answer) {

            $.ajax({
                type: "POST",
                url: "AddQuestions.aspx/Bind_Answer",
                data: '{answer:"' + answer + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                
                    var cities = r.d;
                    var repeatColumns = parseInt("<%=chkAnswer.RepeatColumns%>");
                    if (repeatColumns == 0) {
                        repeatColumns = 1;
                    }
                    var cell = $("[id*=chkAnswer] td").eq(0).clone(true);
                    if (cities.length > 0) {
                        $('[id*=divAnswer]').show();
                        $("[id*=chkAnswer] tr").remove();
                    }
                    $.each(cities, function (i) {
                        var row;
                        if (i % repeatColumns == 0) {
                            row = $("<tr />");
                            $("[id*=chkAnswer] tbody").append(row);
                        } else {
                            row = $("[id*=chkAnswer] tr:last-child");
                        }
                        var checkbox = $("input[type=checkbox]", cell);
                        checkbox[0].checked = false;
                        checkbox[0].id = checkbox[0].id.replace("0", i);
                        checkbox[0].name = "Answer";
                        checkbox.val(this.Ans);

                        var label = cell.find("label");
                        if (label.length == 0) {
                            label = $("<label />");
                        }
                        label.attr("for", checkbox[0].id);
                        label.html(this.Ans);
                        cell.append(label);
                        row.append(cell);
                        cell = $("[id*=chkAnswer] td").eq(0).clone(true);
                    });

                    $("[id*=chkAnswer] input[type=checkbox]").click(function () {
                        var cell = $(this).parent();
                        var hidden = cell.find("input[type=hidden]");
                        var label = cell.find("label");
                        if ($(this).is(":checked")) {
                            if (hidden.length == 0) {
                                hidden = $("<input type = 'hidden' />");
                                cell.append(hidden);
                            }
                            hidden[0].name = "Data";
                            hidden.val(label.text());
                            cell.append(hidden);
                        } else {
                            cell.remove(hidden);
                        }
                    });

                }
            , failure: function (response) {
                alert(response.d);
            },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
    </script>
     <link href="../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Validate() {
            var ques = document.getElementById('<%=ddlQuestionType.ClientID%>');
            var ans = document.getElementById('<%=ddlAns.ClientID%>');
            var mas = document.getElementById('<%=ddlMas.ClientID%>');
            var qustxt = document.getElementById('<%=txtQuestionText.ClientID%>').value.trim();

            var sub = $("#ctl00_ContentPlaceHolder1_chkSubdiv :checkbox:checked").length;
            var rbt = $("#ctl00_ContentPlaceHolder1_RblType :radio:checked").length;
           
         
            if (ques.selectedIndex == 0) {
                alert("Please select QuestionType!!");
                ques.focus();
                return false;
            }
            else if (sub == 0) {

                alert("Please Select Subdivision");
                return false;
            }
         
            else if (rbt == 0) {

                alert("Please Select Product Mode");
                return false;
            }
        
           else if (ans.selectedIndex == 0) {
                alert("Please select AnswerType!!");
                ans.focus();
                return false;
            }
            else if (ans.selectedIndex == 7) {
                if (mas.selectedIndex == 0) {
                    alert("Please select Master!!");
                    ans.focus();
                    return false;
                }
            }
            else if (qustxt.length <= 0) {
                alert('Please Enter Question Text');
               
                return false;
            }

            
        }
    </script>
     <script type="text/javascript">


         $(document).ready(function () {

             $("#btnReActivate_Plus").click(function (e) {
                 ShowDialog_Plus(false);
                 e.preventDefault();
             });

             $("#btnClose_Plus").click(function (e) {
                 HideDialog_Plus();
                 e.preventDefault();
             });
         });

         function ShowDialog_Plus(modal) {
             $("#overlay_Plus").show();
             $("#dialog_Plus").fadeIn(300);

             if (modal) {
                 $("#overlay_Plus").unbind("click");
             }
             else {
                 $("#overlay_Plus").click(function (e) {
                     HideDialog_Plus();
                 });
             }
         }

         function HideDialog_Plus() {
             $("#overlay_Plus").hide();
             $("#dialog_Plus").fadeOut(300);
         }


    </script>
    <style type="text/css">
        .style6 {
            height: 15px;
        }

        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            font-size: 11px;
            font-family: Calibri;
            -webkit-appearance: none;
            width: 300px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }
    </style>
      <style type="text/css">
        .web_dialog_overlay {
            position: fixed;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            height: 100%;
            width: 100%;
            margin: 0;
            padding: 0;
            background: #000000;
            opacity: .15;
            filter: alpha(opacity=15);
            -moz-opacity: .15;
            z-index: 101;
            display: none;
        }

        .web_dialog {
            display: none;
            position: fixed;
      
            min-height: 180px;
            max-height: auto;
            top: 50%;
            left: 50%;
            margin-left: -190px;
            margin-top: -100px;
            background-color: #ffffff;
            border: 2px solid #336699;
            padding: 0px;
            z-index: 102;
            font-family: Verdana;
            font-size: 10pt;
        }

        .web_dialog_title {
            border-bottom: solid 2px Teal;
            background-color: Teal;
            padding: 4px;
            color: White;
            font-weight: bold;
        }

            .web_dialog_title a {
                color: White;
                text-decoration: none;
            }

        .align_right {
            text-align: right;
        }

        .Formatrbtn label {
            margin-right: 30px;
        }


        /* hover style just for information */
        label:hover:before {
            border: 1px solid #4778d9 !important;
        }


        .btnReAct {
            display: inline-block;
            padding: 3px 9px;
            margin-bottom: 0;
            font-size: 12px;
            font-weight: normal;
            line-height: 1.42857143;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            -ms-touch-action: manipulation;
            touch-action: manipulation;
            cursor: pointer;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            background-image: none;
            border: 1px solid transparent;
            border-radius: 4px;
            margin-top: 25px;
        }

        .btnReActivation {
            color: #fff;
            background-color: #158263;
            border-color: #158263;
        }

            .btnReActivation:hover {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:focus, .btnReActivation.focus {
                color: #fff;
                background-color: #2b9a7b;
                border-color: #2b9a7b;
            }

            .btnReActivation:active, .btnReActivation.active {
                color: #fff;
                background-color: #158263;
                border-color: #158263;
                background-image: none;
            }


        #btnClose_Plus:focus {
            outline-offset: -2px;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:hover, #btnClose_Plus:focus {
            color: #fff;
            text-decoration: underline;
        }

        #btnClose_Plus:active, #btnClose_Plus:hover {
            outline: 0px none currentColor;
        }
    </style>
  
    <script type="text/javascript">
        $(document).ready(function () {
            function blinker() {
                $('.blink_me').fadeOut(500);
                $('.blink_me').fadeIn(500);
            }

            setInterval(blinker, 1000);
        });
    </script>
    <script type="text/javascript">
        function printChecked() {
            // var items = document.getElementsByName('acs');
            var qus = document.getElementById('<%=ddlQues.ClientID%>').value.trim();
            var items = $("#ctl00_ContentPlaceHolder1_chkAnswer :checkbox:checked");
            var hdnQues = document.getElementById("<%=hidQues.ClientID%>");
            var hidAns = document.getElementById("<%=hidAns.ClientID%>");
            var selectedItems = "";
            hdnQues.value = "";
            hidAns.value = "";
            for (var i = 0; i < items.length; i++) {
                if (items[i].type == 'checkbox' && items[i].checked == true)
                    selectedItems += items[i].value + "/";
            }

            hdnQues.value = qus;
            hidAns.value = selectedItems;
            HideDialog_Plus();

        }
     
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hidSurveyId" runat="server" />
       <asp:HiddenField ID="hidQues" runat="server"  />
        <asp:HiddenField ID="hidAns" runat="server" />
    <div>
        <div style="margin-left: 90%">
            <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" />

        </div>
        <asp:Image ID="image1" ImageUrl="~/MasterFiles/Quesionaire/images/survey.jpg" runat="server" />
        <table bgcolor="White" border="0" align="right" width="40%"
            style="border: 1px; border-color: #003366; top: 139px; left: 502px; position: absolute; height: 235px"
            frame="border">
            <tr>
                <td bgcolor="#003399">
                    <asp:Label ID="Label1" runat="server" ForeColor="#7CC243" Text="Question Entry"
                        Font-Size="X-Large" Font-Names="Copperplate Gothic Bold" Font-Bold="False"
                        Style="text-align: center" align="center"></asp:Label>
                </td>
            </tr>
            <tr>
                <td height="4"></td>
            </tr>

            <tr>
                <td width="30%">&nbsp;<asp:Label ID="lblQuestion_Type" runat="server" Text="QUESTION TYPE" Font-Size="14px"
                    BackColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <asp:DropDownList ID="ddlQuestionType" runat="server"
                        Style="border-top-style: groove; border-right-style: groove; border-left-style: groove; border-bottom-style: groove;"
                        Height="28px" Width="193px">
                    </asp:DropDownList>
                    </br>
                     <%--  <asp:RadioButtonList ID="RblType" CssClass="Radio" runat="server" RepeatColumns="2"
                            Font-Names="Verdana" Font-Size="X-Small">
                            <asp:ListItem Value="PB" >Product Based &nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="CB">Competitor Based &nbsp;&nbsp;</asp:ListItem>
                          
                        </asp:RadioButtonList>--%>
                    </br>
                   
                </td>
            </tr>
            <tr>
                <td width="30%">&nbsp;<asp:Label ID="Label3" runat="server" Text="SUBDIVISION" Font-Size="14px"
                    BackColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <asp:CheckBoxList ID="chkSubdiv" runat="server" DataTextField="subdivision_name" CssClass="chkboxLocation"
                        DataValueField="subdivision_code" RepeatDirection="Vertical" RepeatColumns="4"
                        Style="font-size: x-small; color: black; font-family: Verdana;"
                        OnSelectedIndexChanged = "OnCheckBox_Changed" AutoPostBack = "true"
                        >
                    </asp:CheckBoxList>

                    <%-- <asp:RadioButtonList ID="RblType" CssClass="Radio" runat="server" RepeatColumns="2"
                            Font-Names="Verdana" Font-Size="X-Small">
                            <asp:ListItem Value="PB" >Product Based &nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="CB">Competitor Based &nbsp;&nbsp;</asp:ListItem>
                          
                        </asp:RadioButtonList>--%>
                    </br>
                   
                </td>
            </tr>
            <tr>
                <td width="30%">&nbsp;<asp:Label ID="Label4" runat="server" Text="PRODUCT" Font-Size="14px"
                    BackColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="40%">


                    <asp:RadioButtonList ID="RblType" CssClass="Radio" runat="server" RepeatColumns="2"
                        Font-Names="Verdana" Font-Size="X-Small" onchange="return GetSelectedItem()">
                        <asp:ListItem Value="PB">Product Based &nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="CB">Competitor Based &nbsp;&nbsp;</asp:ListItem>

                    </asp:RadioButtonList>

                    <div id="divlist" style="display: none">
                        <asp:ListBox ID="lstProd" runat="server" SelectionMode="Multiple" CssClass="ddl"></asp:ListBox>
                    </div>
                    </br>
                </td>
            </tr>
            <tr>
                <td width="30%">&nbsp;<asp:Label ID="Label2" runat="server" Text="ANSWER TYPE" Font-Size="14px"
                    BackColor="White"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <asp:DropDownList ID="ddlAns" runat="server" onchange="QuestionTypeChange(this)"
                        Style="border-top-style: groove; border-right-style: groove; border-left-style: groove; border-bottom-style: groove;"
                        Height="28px" Width="193px">
                    </asp:DropDownList>
                      &nbsp;&nbsp;
                          <a href="#" id="btnReActivate_Plus" style="color: Blue; font-weight: bold; font-family: @NSimSun; font-size: 14px"
                    shape="circle">Is there any Parent Question?</a>
                     
                       
                    </br>
                    </br>
                   
                </td>
            </tr>
            <tr>
                <td width="30%">
                    <div id="divmas2" style="display: none">
                        &nbsp;<asp:Label ID="lblmas" runat="server" Text="MASTER" Font-Size="14px"
                            BackColor="White"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <div id="divmas" style="display: none">

                        <asp:DropDownList ID="ddlMas" runat="server"
                            Style="border-top-style: groove; border-right-style: groove; border-left-style: groove; border-bottom-style: groove;"
                            Height="28px" Width="193px">
                            <asp:ListItem Value="0" Text="-- Select Master--"></asp:ListItem>
                            <asp:ListItem Value="DRS" Text="Listed Doctor"></asp:ListItem>
                            <asp:ListItem Value="CHM" Text="Chemist"></asp:ListItem>
                            <asp:ListItem Value="HOS" Text="Hospital"></asp:ListItem>
                            <asp:ListItem Value="STK" Text="Wholesaler"></asp:ListItem>
                            <asp:ListItem Value="UNL" Text="Unlisted Doctor"></asp:ListItem>
                            <asp:ListItem Value="CB" Text="Competitor Brand Name"></asp:ListItem>
                        </asp:DropDownList>
                        </br>
                    
                    </div>
                </td>

            </tr>
            <tr>
                <td>&nbsp;<asp:Label ID="lblQuestionText" runat="server" Text="QUESTION TEXT" Font-Size="14px">
                </asp:Label>

                </td>
            </tr>
            <tr>
                <td>

                    <asp:TextBox ID="txtQuestionText" runat="server" Height="75px" Width="500px" TextMode="MultiLine"
                        Style="border-top-style: groove; border-right-style: groove; border-left-style: groove; border-bottom-style: groove;"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td width="70%">
                    <div id="divInputOptions" style="display: none">
                        <table width="100%" border="0">
                            <tr>
                                <td>Answer Choices:
                                
                               
                                    Enter each choice on a separate line.
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:TextBox ID="txtInputOptions" runat="server" Height="75px" Width="500px" TextMode="MultiLine"
                                        Style="border-top-style: groove; border-right-style: groove; border-left-style: groove; border-bottom-style: groove;"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>

            <tr>

                <td>
                    <asp:Button ID="btnAddQuestion" runat="server" Text="Add Question" BackColor="LightBlue"
                        OnClick="btnAddQuestion_Click1" OnClientClick="return Validate();"
                        Style="top: 755px; left: 433px; height: 26px; width: 119px" />
                </td>
            </tr>
        </table>
        <div>
                <br />
                <div id="output_Plus">
                </div>
                <div id="overlay_Plus" class="web_dialog_overlay">
                </div>
                <div id="dialog_Plus" class="web_dialog">
                    <table style="width: 100%; border: 0px;" cellpadding="3" cellspacing="0">
                        <tr>
                            <td class="web_dialog_title">Parent Question

                            </td>
                            <td class="web_dialog_title align_right">
                                <a href="#" id="btnClose_Plus">Close</a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <br />
                                <center>
                                    <table border="0" cellpadding="3" cellspacing="3" id="tblDivisionDtls" align="center">
                                       
                                        <tr>
                                            <td align="left" class="stylespc">
                                                <asp:Label ID="lblQues" runat="server" SkinID="lblMand" ><span style="color:Red">*</span>Question</asp:Label>
                                            </td>
                                            <td align="left" class="stylespc">
                                                <asp:DropDownList ID="ddlQues" runat="server" Width="300px" SkinID="ddlRequired" >
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td align="left" class="stylespc">
                                                <asp:Label ID="Label5" runat="server" SkinID="lblMand" ><span style="color:Red">*</span>Answer</asp:Label>
                                            </td>
                                            <td align="left" class="stylespc">
                                             <%--    <asp:CheckBoxList ID="chkans" runat="server" CssClass="chkboxLocation"
 RepeatDirection="Vertical" RepeatColumns="4"
                        Style="font-size: x-small; color: black; font-family: Verdana;"
                        
                        >
                    </asp:CheckBoxList>--%>
                     <div id="divAnswer" style="display: none">
                <asp:CheckBoxList ID="chkAnswer" CssClass="chkboxLocation"  RepeatDirection="Vertical" RepeatColumns="4"
                        Style="font-size: x-small; color: black; font-family: Verdana;" runat="server">
                    <asp:ListItem Text="" Value=""></asp:ListItem>
                </asp:CheckBoxList>
            </div>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center;">
                                <%--<asp:Button ID="btnsave" runat="server" Text="Save" CssClass="btnReAct btnReActivation" OnClick="btnsave_click"
                                     />--%>
                                     <input type="button" onclick='printChecked()'  value="Save"/>
                                <%--<asp:Button ID="btnActive_Plus" runat="server" Text="Activate" OnClick="btnActive_Plus_Click"
                                            CssClass="btn btnReActivation" />--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        <link type="text/css" href="../../css/multiple-select.css" rel="Stylesheet" />

        <script type="text/javascript" src="../../JsFiles/multiple-select_2.js"></script>
        <script type="text/javascript">

            $('[id*=lstProd]').multipleSelect();
        </script>
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Designation_level.aspx.cs"
    Inherits="MasterFiles_Designation_level" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Designation level - Mgr</title>
   <%-- <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://www.w3schools.com/js/myScript1.js"></script>

    <link href="../../JScript/Service_CRM/Crm_Dr_Css_Ob/SS_Report_Table_CSS.css" rel="stylesheet"  type="text/css" />
    <link rel="stylesheet" href="../assets/css/Calender_CheckBox.css" type="text/css" />
    

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

        .box {
            background: #F3F6ED;
            border: 3px solid #7E8D29;
            border-radius: 8px;
        }

        .break {
            height: 80px;
        }
       
    </style>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>

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
    <style type="text/css">
        .button1 {
            border-top: 1px solid #96d1f8;
            background: #BBD823;
            background: -webkit-gradient(linear, left top, left bottom, from(#3e779d), to(#BBD823));
            background: -webkit-linear-gradient(top, #3e779d, #BBD823);
            background: -moz-linear-gradient(top, #3e779d, #BBD823);
            background: -ms-linear-gradient(top, #3e779d, #BBD823);
            background: -o-linear-gradient(top, #3e779d, #BBD823);
            padding: 5px 10px;
            -webkit-border-radius: 8px;
            -moz-border-radius: 8px;
            border-radius: 8px;
            -webkit-box-shadow: rgba(0,0,0,1) 0 1px 0;
            -moz-box-shadow: rgba(0,0,0,1) 0 1px 0;
            box-shadow: rgba(0,0,0,1) 0 1px 0;
            text-shadow: rgba(0,0,0,.4) 0 1px 0;
            color: white;
            font-size: 14px;
            font-family: Georgia, serif;
            text-decoration: none;
            vertical-align: middle;
        }

            .button1:hover {
                border-top-color: #f4ad42;
                background: #f4ad42;
                color: black;
            }

            .button1:active {
                border-top-color: #1b435e;
                background: #1b435e;
            }
    </style>
    <%--<script type="text/javascript">
        function getselected() {
            var message = "";
            var checkboxlist = document.getElementById("<%=Chkfirst.ClientID%>");
            var checkboxes = checkboxlist.getElementsByTagName("input");
            for (var i = 0; i < checkboxes.length; i++) {
                if (checkboxes[i].checked) {
                    var valuess = checkboxes[i].value;
                    message += valuess + ',';
                    alert(text);
                }
            }
            alert(message);
            return false;
        }

    </script>--%>

    <script type="text/javascript">

        function getselected() {
            $chk_1 = $("[id$=Chkfirst] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();

            $chk_2 = $("[id$=chksecond] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();


            $chk_3 = $("[id$=chkthird] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();

            $chk_4 = $("[id$=chkfourth] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();

            $chk_5 = $("[id$=chkfifth] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();

            $chk_6 = $("[id$=Chksixth] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();

            $chk_7 = $("[id$=chkseventh] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();

            $chk_8 = $("[id$=chkeighth] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();

            $chk_9 = $("[id$=chkninth] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();

            $chk_10 = $("[id$=chktenth] input[type=checkbox]:checked").map(function (index, foElem) {
                return $(this).next().html();
            }).get();



            var des = $chk_1 + ',' + $chk_2 + ',' + $chk_3 + ',' + $chk_4 + ',' + $chk_5 + ',' + $chk_6 + ',' + $chk_7 + ',' + $chk_8 + ',' + $chk_9 + ',' + $chk_10;

            var one = $chk_2;
            var two = $chk_2;

            var arrInput = [one, two];
            var sorted_arr = arrInput.sort();
            var results = [];
            for (var i = 0; i < arrInput.length - 1; i++) {
                if (sorted_arr[i + 1] == sorted_arr[i]) {
                    results.push(sorted_arr[i]);
                }
            }

            alert("duplicate values: " + results);

            //            var first = $("[id*=Chkfirst] input:checked");
            //            var second = $("[id*=chksecond] input:checked");
            //            var third = $("[id*=chkthird] input:checked");
            //            var fourth = $("[id*=chkfourth] input:checked");
            //            var fifth = $("[id*=chkfifth] input:checked");
            //            var sixth = $("[id*=Chksixth] input:checked");
            //            var seventh = $("[id*=chkseventh] input:checked");
            //            var eigth = $("[id*=chkeighth] input:checked");
            //            var ninth = $("[id*=chkninth] input:checked");
            //            var tenth = $("[id*=chktenth] input:checked");

            //            var cbValue = "";
            //            first.each(function () {
            //                cbValue += $(this).parent().attr('cbValue') + ",";
            //            });

            //            var cbValue2 = "";
            //            second.each(function () {
            //                cbValue2 += $(this).parent().attr('cbValue2') + ",";
            //            });

            //            var cbValue3 = "";
            //            third.each(function () {
            //                cbValue3 += $(this).parent().attr('cbValue3') + ",";
            //            });
            //            var cbValue4 = "";
            //            fourth.each(function () {
            //                cbValue4 += $(this).parent().attr('cbValue4') + ",";
            //            });
            //            var cbValue5 = "";
            //            fifth.each(function () {
            //                cbValue5 += $(this).parent().attr('cbValue5') + ",";
            //            });
            //            var cbValue6 = "";
            //            sixth.each(function () {
            //                cbValue6 += $(this).parent().attr('cbValue6') + ",";
            //            });
            //            var cbValue7 = "";
            //            seventh.each(function () {
            //                cbValue7 += $(this).parent().attr('cbValue7') + ",";
            //            });
            //            var cbValue8 = "";
            //            eigth.each(function () {
            //                cbValue8 += $(this).parent().attr('cbValue8') + ",";
            //            });
            //            var cbValue9 = "";
            //            ninth.each(function () {
            //                cbValue9 += $(this).parent().attr('cbValue9') + ",";
            //            });
            //            var cbValue10 = "";
            //            tenth.each(function () {
            //                cbValue10 += $(this).parent().attr('cbValue10') + ",";
            //            });

            //                       var names = cbValue + cbValue2 + cbValue3 + cbValue4 + cbValue5 + cbValue6 + cbValue7 + cbValue8 + cbValue9 + cbValue10;
            //                       var arr = [1,1];
            //            function returnDuplicates(arr) {
            //                return arr.reduce(function (dupes, val, i) {
            //                    if (arr.indexOf(val) !== i && dupes.indexOf(val) === -1) {
            //                        dupes.push(val);
            //                    }
            //                    return dupes;
            //                }, []);
            //            }

            //            alert(returnDuplicates(arr));


            //            var arrInput = [cbValue + cbValue2 + cbValue3 + cbValue4 + cbValue5 + cbValue6 + cbValue7 + cbValue8 + cbValue9 + cbValue10];

            //            alert(arrInput);
            //                    var sorted_arr = arrInput.sort();   
            //                    var results = [];  
            //                    for (var i = 0; i < arrInput.length - 1; i++) {  
            //                        if (sorted_arr[i + 1] == sorted_arr[i]) {  
            //                            results.push(sorted_arr[i]);  
            //                        }  
            //                    }

            //                    alert("duplicate values: " + results);
            return false;
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />
            <br />
            <br />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-10">
                          <br />
                        <h2 class="text-center">Designation Level – Mgr</h2>
                    
                        <div class="des-single-label">
                            <label for="">First Level</label>
                            <%-- <asp:Label ID="lblfirst_desg" runat="server"  Text=" First Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtfirst" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="txtfirst_PopupControlExtender" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtfirst" PopupControlID="Panel1" Position="Bottom"  
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel1" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px">   
                                                                         
                                            <asp:CheckBoxList ID="Chkfirst" runat="server" AutoPostBack="True" 
                                                OnSelectedIndexChanged="Chkfirst_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="des-single-label">
                            <label for="">Second Level</label>
                            <%--  <asp:Label ID="lblsecond" runat="server" CssClass="label" Text=" Second Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtsecond" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtsecond" PopupControlID="Panel2"  Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel2" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px">  
                                            <asp:CheckBoxList ID="chksecond" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chksecond_SelectedIndexChanged" >
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="des-single-label">
                            <label for="">Third Level</label>
                            <%--<asp:Label ID="lblthird" runat="server" CssClass="label" Text=" Third Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtthird" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender2" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtthird" PopupControlID="Panel3" Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel3" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px">  
                                            <asp:CheckBoxList ID="chkthird" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkthird_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="des-single-label">
                            <label for="">Fourth Level</label>
                            <%--<asp:Label ID="lblfourth" runat="server" CssClass="label" Text=" Fourth Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtfourth" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender3" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtfourth" PopupControlID="Panel4" Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel4" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px">  
                                            <asp:CheckBoxList ID="chkfourth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkfourth_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="des-single-label">
                            <label for="">Fifth Level</label>
                            <%--<asp:Label ID="lblfifth" runat="server" CssClass="label" Text="Fifth Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtfifth" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender4" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtfifth" PopupControlID="Panel5" Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel5" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px">
                                            <asp:CheckBoxList ID="chkfifth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkfifth_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="des-single-label">
                            <label for="">Sixth Level</label>
                            <%--<asp:Label ID="lblsixth" runat="server" CssClass="label" Text="Sixth Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtsixth" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender5" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtsixth" PopupControlID="Panel6" Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel6" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px"> 
                                            <asp:CheckBoxList ID="Chksixth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Chksixth_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="des-single-label">
                            <label for="">Seventh Level</label>
                            <%--<asp:Label ID="lblSeventh" runat="server" CssClass="label" Text="Seventh Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtseventh" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender6" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtseventh" PopupControlID="Panel7" Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel7" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px">
                                            <asp:CheckBoxList ID="chkseventh" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkseventh_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="des-single-label">
                            <label for="">Eigth Level</label>
                            <%--<asp:Label ID="lbleighth" runat="server" CssClass="label" Text="Eigth Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txteighth" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender7" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txteighth" PopupControlID="Panel8" Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel8" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px">
                                            <asp:CheckBoxList ID="chkeighth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkeighth_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="des-single-label">
                            <label for="">Ninth Level</label>
                            <%--<asp:Label ID="lblninth" runat="server" CssClass="label" Text="Ninth Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtninth" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender8" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txtninth" PopupControlID="Panel9" Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel9" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px">
                                            <asp:CheckBoxList ID="chkninth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkninth_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <div class="des-single-label">
                            <label for="">Tenth Level</label>
                            <%--<asp:Label ID="lbltenth" runat="server" CssClass="label" Text="Tenth Level"></asp:Label>--%>
                            <div class="single-des-option">
                                <asp:UpdatePanel ID="updatepanel10" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txttenth" ReadOnly="true" CssClass="input" Width="140px" runat="server"></asp:TextBox>
                                        <asp:PopupControlExtender ID="PopupControlExtender9" runat="server" DynamicServicePath=""
                                            Enabled="True" ExtenderControlID="" TargetControlID="txttenth" PopupControlID="Panel10" Position="Bottom"
                                            OffsetY="2">
                                        </asp:PopupControlExtender>
                                        <asp:Panel ID="Panel10" runat="server" Height="135px" Width="140px" BorderWidth="1px" BorderColor="#d1e2ea" BackColor="#f4f8fa"
                                            Direction="LeftToRight" ScrollBars="Auto" Style="display: none;border-radius:8px">
                                            <asp:CheckBoxList ID="chktenth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chktenth_SelectedIndexChanged">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>


                        <div class="w-100 designation-submit-button text-center clearfix">
                            <asp:Button ID="btnSubmit" runat="server"  CssClass="savebutton"   Text="Save"   OnClick="btnSubmit_Click" />     
                        </div>
                        <p>
                            <br />
                        </p>
                     
                    </div>
                </div>
                <asp:Button ID="btnBack" runat="server" Text="Back"  CssClass="backbutton" OnClick="btnBack_Click"/>
            </div>           
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survey_Process_View.aspx.cs" Inherits="MasterFiles_Survey_Survey_Process_View" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Survey - View</title>
    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, Mode, Survey_Id, Name) {
            popUpObj = window.open("Rpt_Survey_Process_View.aspx?sf_code=" + sfcode + "&Mode=" + Mode + "&Survey_Id=" + Survey_Id + "&Name=" + Name,
  "ModalPopUp",
  "toolbar=no," +
  "scrollbars=yes," +
  "location=no," +
  "statusbar=no," +
  "menubar=no," +
  "addressbar=no," +
  "resizable=yes," +
  "width=800," +
  "height=600," +
  "left = 0," +
  "top=0"
  );
            popUpObj.focus();
            // LoadModalDiv();
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s10.postimg.org/b9kmgkw55/triangle_square_animation.gif"
                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 500px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');

            });
        }
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
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

            $('#btnSubmit').click(function () {
                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                var Mode = $('#<%=ddlMode.ClientID%> :selected').text();
                if (Mode == "---Select---") { alert("Select Mode."); $('#ddlMode').focus(); return false; }
                var Survey = $('#<%=ddlSurvey.ClientID%> :selected').text();
                if (Survey == "---Select---") { alert("Select Year."); $('#ddlSurvey').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Mode_Id = document.getElementById('<%=ddlMode.ClientID%>').value;
                var Survey_Id = document.getElementById('<%=ddlSurvey.ClientID%>').value;

                showModalPopUp(sf_Code, Mode_Id, Survey_Id, Name);
            });
        });
    </script>


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });

    </script>



    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.on('keyup', function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                       function (n) {
                           return exp.test($(n).text());
                       });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                       );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }
            }
        });
    </script>


</head>
<body>
    <form id="form2" runat="server">

        <div>
            <div id="Divid" runat="server">
            </div>
            <br />
            <center>
                <asp:Label ID="Lblmain" runat="server" Text="Coverage Analysis" SkinID="lblMand"></asp:Label>
                <table>
                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">

                            <asp:TextBox ID="txtNew" runat="server" SkinID="MandTxtBox" Width="100px" CssClass="TEXTAREA" Visible="false"
                                ToolTip="Enter Text Here"></asp:TextBox>
                            <asp:LinkButton ID="linkcheck" runat="server"
                                OnClick="linkcheck_Click">
                          <img src="../../Images/Selective_Mgr.png" />
                            </asp:LinkButton>
                            <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" Width="300px" Visible="false"></asp:DropDownList>
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>

                        </td>
                        <td>
                            <div id="testImg">
                                <img id="Img1" alt="" src="~/Images/loading/loading19.gif" style="height: 20px;" runat="server" /><span
                                    style="font-family: Verdana; color: Red; font-weight: bold;">Loading Please Wait...</span>
                            </div>
                        </td>
                    </tr>


                    <tr style="height: 25px;">
                        <td align="left" class="stylespc" width="120px">
                            <asp:Label ID="lblMode" runat="server" Text="Mode" SkinID="lblMand"></asp:Label>
                        </td>
                        <td align="left" class="stylespc">
                            <asp:DropDownList ID="ddlMode" Width="250px" runat="server" SkinID="ddlRequired">
                                <asp:ListItem Value="0" Text="Question Wise"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Answer Wise"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td align="left" class="stylespc">
                            <asp:Label ID="lblFF" runat="server" Text="Survey Name" SkinID="lblMand"></asp:Label>
                        </td>
                        <td class="stylespc">
                            <asp:DropDownList ID="ddlSurvey" Width="250px" runat="server" SkinID="ddlRequired"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View"
                    BackColor="LightBlue" />
            </center>
        </div>
    </form>
</body>
</html>



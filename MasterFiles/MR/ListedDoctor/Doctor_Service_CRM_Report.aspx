<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Service_CRM_Report.aspx.cs"
    Inherits="MasterFiles_MR_ListedDoctor_Doctor_Service_CRM_Report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Doctor Service CRM </title>
    <link type="text/css" rel="stylesheet" href="../../../css/style.css" />
    <script src="../../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <link href="../../../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../../../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script src="../../../JScript/jquery-1.10.2.js" type="text/javascript"></script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode,Sf_Name, fmon, fyr, tmon, tyr, stockcode, type) {
            popUpObj = window.open("rptDoctorService_CRM.aspx?sfcode=" + sfcode + "&Sf_Name=" + Sf_Name + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr,
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

            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s2.postimg.org/l99kqyrk9/loading_9_k.gif";

                //var ImgSrc = "https://s14.postimg.org/z7zlmgvn5/loading_28_ook.gif";

                // var Text = "http://s9.postimg.org/hyt713i5b/Text_Purple.gif";

                var Text = "";

                $(popUpObj.document.body).append('<div><center><img src="' + Text + '"  alt="" /></center></div><div> <img src="' + ImgSrc + '"  style=" width:150px; height:150px;position: fixed;top: 15%;left:35%;"  alt="" /></div>');

            });
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            $("#btnGo").click(function (e) {

                if (Validation() === true) {
                    var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                    if (FieldForce == "---Select Clear---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); return false; }

                    var FYear = $('#<%=ddlYear.ClientID%> :selected').text();
                    if (FYear == "---Select---") { alert("Select From Year."); $('#ddlYear').focus(); return false; }
                    var FMonth = $('#<%=ddlMonth.ClientID%> :selected').text();
                    if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlMonth').focus(); return false; }

                    var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                    if (TYear == "---Select---") { alert("Select To Year."); $('#ddlTYear').focus(); return false; }
                    var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                    if (TMonth == "---Select---") { alert("Select To Month."); $('#ddlTMonth').focus(); return false; }


                    var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                    var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
                    var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                    var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;

                    var ddlFName = document.getElementById('<%=ddlFieldForce.ClientID%>').value;


                    var str = ddlFName;
                    console.log(str.includes("MR"));

                   
                    if (str.includes("MR") == true) {
                        showModalPopUp(ddlFName, FieldForce, Month1, Year1, Month2, Year2)
                    }
                    else {

                        showModalPopUp(ddlFName, FieldForce, Month1, Year1, Month2, Year2)
                    }
                }

                e.preventDefault();

            });

        });

        function Validation() {

           
            var ddl_FMonth = $("#ddlMonth :selected").text();
            var ddl_FYear = $("#ddlYear :selected").text();
            var ddl_TMonth = $("#ddlTMonth :selected").text();
            var ddl_TYear = $("#ddlTYear :selected").text();

            if (ddl_FMonth == "--Select--") {
                createCustomAlert("Please Select From Month");
                $("#ddlMonth").focus();
                return false;
            }
            else if (ddl_FYear == "--Select--") {
                createCustomAlert("Please Select From Year");
                $("#ddlYear").focus();
                return false;
            }
            else if (ddl_TMonth == "--Select--") {
                createCustomAlert("Please Select To Month");
                $("#ddlTMonth").focus();
                return false;
            }
            else if (ddl_TYear == "--Select--") {
                createCustomAlert("Please Select To Year");
                $("#ddlTYear").focus();
                return false;
            }
            //            else if (ddl_FMonth > ddl_TMonth && ddl_FYear == ddl_TYear) {

            //                createCustomAlert("To Month must be greater than From Month");
            //                $("#ddlTMonth").focus();
            //                return false;
            //            }
            else if (ddl_FYear > ddl_TYear) {
                createCustomAlert("To Year must be greater than From Year");
                $("#ddlTYear").focus();
                return false;
            }

            else {
                return true;
            }

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="Divid" runat="server">
        </div>
        <br />
        <div>
            <center>
        <br />
            <table border="0" cellpadding="3" cellspacing="3" align="center">
                <tr>
                   <%-- <td align="left" class="stylespc">
                        <asp:Label ID="lblSF" runat="server" Text="Field Force Name " SkinID="lblMand" Height="19px" Width="100px"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired" >
                        </asp:DropDownList>
                    </td>--%>

                     <td align="left" class="stylespc" width="90px">
                        <asp:Label ID="lblFF" runat="server" Text="Field Force Name" SkinID="lblMand" Height="19px" Width="100px"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlFieldForce" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>

                </tr>
                  
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblFMonth" runat="server" Text="From Month " SkinID="lblMand" Height="19px" Width="100px"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
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
                        &nbsp;
                        <asp:Label ID="lblFYear" runat="server" Text="From Year " SkinID="lblMand" ></asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>                   
                <tr>
                    <td align="left" class="stylespc">
                        <asp:Label ID="lblTMonth" runat="server" Text="To Month " SkinID="lblMand" Height="19px" Width="100px"></asp:Label>
                    </td>
                    <td align="left" class="stylespc">
                        <asp:DropDownList ID="ddlTMonth" runat="server" SkinID="ddlRequired">
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
                        &nbsp;
                        <asp:Label ID="lblTYear" runat="server" Text="To Year " SkinID="lblMand"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlTYear" runat="server" SkinID="ddlRequired">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>       
            </table>
            <div>
            <br />
             <asp:Button ID="btnGo" runat="server" Text="Go" CssClass="savebutton" 
             Width="60px" Height="25px"   />
            <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="savebutton" 
                Width="60px" Height="25px"   />
            </div>
        </center>
        </div>
    </div>
    </form>
</body>
</html>

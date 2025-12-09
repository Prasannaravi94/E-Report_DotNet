<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Quiz_Status.aspx.cs" Inherits="MIS_Reports_Quiz_Status" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quiz Test Result</title>
    <%--  <link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
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


    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
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

            $('#btnview').click(function () {

                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                if (Year == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
                showModalPopUp_Dt(sf_Code, Month1, Year1, Name);

            });
            $('#btngraph').click(function () {
                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                if (Year == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
                showModalPopUp(sf_Code, Month1, Year1, Name);
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function popUp(sf_code, sf_name, ans) {
            var Year1 = document.getElementById('<%=ddlYear.ClientID%>').value;
            var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;
            strOpen = "rptQuiz_Status.aspx?sf_code=" + sf_code + "&sf_name=" + sf_name + "&ans=" + ans + "&month=" + Month1 + "&year=" + Year1
            window.open(strOpen, 'popWindow', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=400,height=600,left = 0,top = 0');
        }
    </script>
    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Quiz_Graph.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&sf_name=" + sf_name,
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

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s10.postimg.org/b9kmgkw55/triangle_square_animation.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 500px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });

        }
    </script>
     <script type="text/javascript">

        $(function () {

            $("[id*=ChkAll]").bind("click", function () {

                if ($(this).is(":checked")) {

                    $("[id*=chkdate] input").attr("checked", "checked");

                } else {

                    $("[id*=chkdate] input").removeAttr("checked");

                }

            });

            $("[id*=chkdate] input").bind("click", function () {

                if ($("[id*=chkdate] input:checked").length == $("[id*=chkdate] input").length) {

                    $("[id*=ChkAll]").attr("checked", "checked");

                } else {

                    $("[id*=ChkAll]").removeAttr("checked");

                }

            });

        });

    </script>
     <script type="text/javascript">
       var popUpObj;

       function showModalPopUp_Dt(sfcode, fmon, fyr, sf_name) {
           var cbValue = "";
           var cbTxt = "";
         
           var checked_checkboxes = $("[id*=chkdate] input:checked");
           var message = "";
           if (document.getElementById("ChkAll").checked == false) {
               checked_checkboxes.each(function () {
                   //var value = $(this).parent().attr('cbValue');
                   //var text = $(this).closest("td").find("label").html();
                   //message += "Text: " + text + " Value: " + value;
                   //message += "\n";
                   cbValue += $(this).parent().attr('cbValue') + ",";
               });
           }
           else {
               cbValue = "ALL";
           }
           //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
           popUpObj = window.open("Quiz_Status_dtwise.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&sf_name=" + sf_name+ "&days=" + cbValue,
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

               //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

               //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

               var ImgSrc = "https://s10.postimg.org/b9kmgkw55/triangle_square_animation.gif"

               // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


               $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 500px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


           });

       }
</script>
   <%-- <script type="text/javascript">
        var popUpObj;

        function showModalPopUp_Dt(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("Quiz_Status_dtwise.aspx?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&sf_name=" + sf_name,
     "ModalPopUp"//,
     //"toolbar=no," +
     //"scrollbars=yes," +
     //"location=no," +
     //"statusbar=no," +
     //"menubar=no," +
     //"addressbar=no," +
     //"resizable=yes," +
     //"width=800," +
     //"height=600," +
     //"left = 0," +
     //"top=0"
     );
            popUpObj.focus();
            // LoadModalDiv();
            $(popUpObj.document.body).ready(function () {

                //  var ImgSrc = "../E-Report_DotNet/Images/loading/loading47.gif";

                //  var ImgSrc = "http://i.imgur.com/KUJoe.gif";

                var ImgSrc = "https://s10.postimg.org/b9kmgkw55/triangle_square_animation.gif"

                // var ImgSrc = "https://s9.postimg.org/lr9knv0of/loading_Square.gif";


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:500px; height: 500px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });

        }
    </script>--%>

    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="dheading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblDivision" runat="server" Visible="false" Text="Division Name " CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select" Visible="false" AutoPostBack="true" OnSelectedIndexChanged="ddldivision_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Filed Force Name" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" Visible="false"
                                    OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged" SkinID="ddlRequired">
                                    <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                    OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged" SkinID="ddlRequired">
                                </asp:DropDownList>

                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%"></asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" SkinID="ddlRequired"></asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblMoth" runat="server" Text="Month" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblToYear" runat="server" Text="Year" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                             <div class="single-des clearfix">
                                <asp:Label ID="lbldate" runat="server" CssClass="label" Text="Day">  </asp:Label>
                                  <asp:CheckBox ID="ChkAll" runat="server" Text=" All"  />
                        <asp:CheckBoxList ID="chkdate" Width="85%" Font-Size="8pt" CssClass="chkChoice" RepeatColumns="7" RepeatDirection="Horizontal" runat="server">
                        </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Width="70px" Height="25px" Text="View" Visible="false"
                                BackColor="LightBlue" OnClick="btnSubmit_Click" />
                            <asp:Button ID="btnview" runat="server" CssClass="savebutton" Text="View" />
                            &nbsp;&nbsp;        
                <asp:LinkButton ID="lnkExcel" ToolTip="Pdf" runat="server" OnClick="btnExcel_Click" Visible="false">
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/excel_Img.png" ToolTip="Excel" Width="20px" Style="border-width: 0px;" />
                </asp:LinkButton>
                            &nbsp;&nbsp;
             <asp:ImageButton ID="btngraph" Width="40px" Visible="false" runat="server" ImageUrl="~/Images/graph.png" />
                        </div>
                    </div>
                </div>

                <div class="display-reporttable clearfix">
                    <div class="table-responsive" style="scrollbar-width: thin;">

                        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
                            Width="80%">
                        </asp:Table>
                        <asp:Panel ID="pnlContents" runat="server" Width="100%">
                            <table width="80%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                                            AutoGenerateColumns="false" EmptyDataText="No Records Found" CssClass="mGridImg"
                                            AlternatingRowStyle-CssClass="alt" OnRowDataBound="grdSalesForce_RowDataBound">
                                            <HeaderStyle Font-Bold="False" />
                                            <PagerStyle CssClass="pgr"></PagerStyle>
                                            <SelectedRowStyle BackColor="BurlyWood" />
                                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Sf_Code" Visible="false" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsfName" runat="server" Text='<%# Bind("Sf_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Designation Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldes" runat="server" Text='<%# Bind("sf_Designation_Short_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHQ" runat="server" Text='<%# Bind("Sf_HQ") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total No of Questions" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQus" runat="server" Text='<%# Bind("Qus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="No of Correct Answer (1st Attempt)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--  <asp:Label ID="lblAns" runat="server" Text='<%# Bind("Ans") %>'></asp:Label>--%>
                                                        <asp:LinkButton ID="lnkcount" runat="server" CausesValidation="False" Text='<%# Eval("Ans") %>'
                                                            OnClientClick='<%# "return popUp(\"" + Eval("sf_code")  + "\",\"" + Eval("sf_Name")  + "\",\"" + 1  + "\");" %>'>
                                       
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Color" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBackColor" runat="server" Font-Size="10px" Font-Names="sans-serif" ForeColor="#483d8b" Text='<%# Bind("des_color") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marks in (%) (1st Attempt)" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle BackColor="LightBlue" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPercent" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No of Correct Answer (2nd Attempt)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%--  <asp:Label ID="lblAns" runat="server" Text='<%# Bind("Ans") %>'></asp:Label>--%>
                                                        <asp:LinkButton ID="lnkcount1" runat="server" CausesValidation="False" Text='<%# Eval("SecondAns") %>'
                                                            OnClientClick='<%# "return popUp(\"" + Eval("sf_code")  + "\",\"" + Eval("sf_Name")  + "\",\"" + 2  + "\");" %>'>
                                       
                                                        </asp:LinkButton>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marks in (%) (2nd Attempt)" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle BackColor="LightBlue" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPercent2" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Average (%)" HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle BackColor="LightBlue" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltot" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
    </form>
</body>
</html>

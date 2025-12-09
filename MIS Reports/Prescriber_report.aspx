<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Prescriber_report.aspx.cs" Inherits="MIS_Reports_Prescriber_report" %>

<!DOCTYPE html>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Prescriber Detail</title>
    <link type="text/css" rel="Stylesheet" href="../css/rptMissCall.css" />
    <%--  <link type="text/css" rel="stylesheet" href="../css/Grid.css" />
    <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, Month, Year, sf_name, Desig) {
            var cbValue = "";
            var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            //for (var i = 0; i < checkbox.length; i++) {
            //    if (checkbox[i].checked) {
            //        cbTxt += label[i].innerHTML + ",";
            //    }
            //}

            var checked_checkboxes = $("[id*=cbSpeciality] input:checked");
            var message = "";
            checked_checkboxes.each(function () {
                cbValue += $(this).parent().attr('cbValue') + ",";
            });
            if (cbValue == "") { alert("Select Speciality."); return false; }
            popUpObj = window.open("rptPrescriber_report.aspx?sfcode=" + sfcode + "&Month=" + Month + "&Year=" + Year + "&sf_name=" + sf_name + "&Desig=" + Desig + "&Spec=" + cbValue,
                "ModalPopUp"//,
                //"toolbar=no," +
                //"scrollbars=yes," +
                //"location=no," +
                //"statusbar=no," +
                //"menubar=no," +
                //"addressbar=no," +
                //"resizable=yes," +
                //"width=900," +
                //"height=600," +
                //"left = 0," +
                //"top=0"
            );
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s27.postimg.org/ke5a9z0o3/11_8_little_loader.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:650px; height: 300px;position: fixed;top: 20%;left: 10%;"  alt="" /></div>');
            });
        }

    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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
            $('#btnGo').click(function () {
                var SName = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (SName == "---Select Clear---") { alert("Select Field Force Name."); $('#ddlFieldForce').focus(); return false; }
                var Desig = $('#<%=ddlDesig.ClientID%> :selected').text();
                if (Desig == "---Select---") { alert("Select Designation."); $('#ddlDesig').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }
                var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                if (Year == "---Select---") { alert("Select Year."); $('#ddlFYear').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                var Month = document.getElementById('<%=ddlMonth.ClientID%>').value;

                showModalPopUp(sf_Code, Month, Year, SName, Desig);

            });
        });
    </script>
     
    <style type="text/css">
        .ddl {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .dd {
            border: 1px solid #1E90FF;
            border-radius: 4px;
            margin: 2px;
            background-image: url('css/download%20(2).png');
            background-position: 88px;
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
            text-overflow: ''; /*In Firefox*/
        }

        .ddl1 {
            border: 1px solid #1E90FF;
            border-radius: 5px;
            width: 190px;
            height: 21px;
            font: bold;
            background-image: url('Images/arrow_sort_d.gif');
            background-repeat: no-repeat;
            text-indent: 0.01px; /*In Firefox*/
        }

        #GrdRangeVal > tbody > tr:nth-child(n) > td {
            width: 30px;
            padding: 2px 7px;
        }
    </style>

     <script>
	        $(document).ready(function () {
	            $('#chkAllSpec').click(function () {
	                var checked = $(this).prop('checked');
	                $('#<%=cbSpeciality.ClientID%>').find('input:checkbox').prop('checked', checked);
	            });
	        })
    </script>
    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id*=ddlFieldForce]").select2();
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
                        <center>
                        <table>
                            <tr>
                                <td align="center">
                                    <h2 class="text-center">Prescriber Detail</h2>
                                </td>
                            </tr>
                        </table>
                    </center>
                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" SkinID="lblMand" Text="Fieldforce Name"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" SkinID="ddlRequired" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" SkinID="ddlRequired" Visible="false" CssClass="ddl">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" runat="server" SkinID="lblMand" Text="Designation"></asp:Label>
                                <asp:DropDownList ID="ddlDesig" runat="server" Width="300px" SkinID="ddlRequired">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblMonth" runat="server" SkinID="lblMand" Text="Month"></asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" SkinID="ddlRequired">
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
                                <div style="float: right; width: 50%;">
                                    <asp:Label ID="lblYear" runat="server" SkinID="lblMand" Text="Year" Width="60"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" SkinID="ddlRequired"
                                        Width="60">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label2" runat="server" SkinID="lblMand" Text="Speciality"></asp:Label>
                                <asp:CheckBox ID="chkAllSpec" runat="server" Text="All" />
                                <asp:CheckBoxList runat="server" ID="cbSpeciality" RepeatDirection="Horizontal" CellSpacing="20"
                                    RepeatColumns="7">
                                </asp:CheckBoxList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label3" runat="server" SkinID="lblMand" Text="Business Range"></asp:Label>
                                <asp:GridView ID="GrdRangeVal" runat="server" AutoGenerateColumns="false" CssClass="mGrids" AlternatingRowStyle-CssClass="alt" ShowHeader="False">
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="30">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFromRange" runat="server" Text='<%# Eval("From_Range") %>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="30">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtToRange" runat="server" Text='<%# Eval("To_Range") %>' Enabled="false"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle BackColor="AliceBlue" BorderColor="Black" BorderStyle="Solid"
                                    BorderWidth="2" Font-Bold="True" ForeColor="Black" Height="5px" HorizontalAlign="Center"
                                    VerticalAlign="Middle" />
                            </asp:GridView>
                                </div>
                        </div>
                    </div>
                    <div class="w-100 designation-submit-button text-center clearfix">
                    <asp:Button ID="btnGo" runat="server" Width="40px" Height="25px" Text="View" CssClass="savebutton" />
                        </div>
                    </center>
                   
                </div>
            </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Statusview_VisitCard_view.aspx.cs"
    Inherits="MIS_Reports_Statusview_VisitCard_view" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="../../css/repstyle.css" />
    <style type="text/css">
        .tr_det_head {
            font-family: Verdana;
            color: White;
            font-size: 9pt;
            height: 22px;
            font-weight: bold;
            font-family: Calibri;
            background: #0097AC;
            border-color: Black;
        }
    </style>
    <script src="../../JsFiles/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script src="../../JsFiles/jquery.tooltip.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $(".gridImages").tooltip({
                track: true,
                delay: 0,
                showURL: false,
                fade: 100,
                bodyHandler: function () {
                    return $($(this).next().html());
                },
                showURL: false
            });
        })
    </script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>

    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script language="Javascript" type="text/javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <link type="text/css" rel="stylesheet" href="../css/style.css" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {

                        inputList[i].checked = true;

                        document.getElementById("btnDelete").style.visibility = "visible";
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }



        function checkapp() {
            var grid = document.getElementById('<%= grdDoctor.ClientID %>');

            if (grid != null) {

                var inputList = grid.getElementsByTagName("input");

                var chkall = document.getElementById('grdDoctor_ctl01_chkAll');
                var cnt = 0;
                var index = '';
                var Count = 0;
                var CountVisi = 0;

                for (i = 2; i <= inputList.length + 1; i++) {

                    if (i.toString().length == 1) {
                        index = cnt.toString() + i.toString();
                    }
                    else {

                        index = i.toString();
                    }


                    var chkAppDel = document.getElementById('grdDoctor_ctl' + index + '_chkAppDel');
                    //var chkrej = document.getElementById('grdDoctor_ctl' + index + '_chkRjtDCR');


                    if (chkAppDel.checked) {

                        document.getElementById("btnDelete").style.visibility = "visible";

                        CountVisi = CountVisi + 1;
                    }

                    else {
                        Count = Count + 1;
                    }

                    //chkrej.checked = false;
                    //chkrejall.checked = false;

                    if (Count > 0) {
                         chkall.checked = false;
                    }
                    else {
                        chkall.checked = true;
                    }
                    if (CountVisi == 0) {
                        document.getElementById("btnDelete").style.visibility = "hidden";
                    }
                }
            }
        }

    </script>
    <style type="text/css">
        .bar {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <center>
                <table width="100%">
                    <tr>
                        <td width="80%"></td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="return PrintPanel();" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnPDF" runat="server" Text="PDF" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px" Visible="false"
                                            OnClick="btnPDF_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnClose" runat="server" Text="Close" Font-Names="Verdana" Font-Size="10px"
                                            BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                            OnClientClick="RefreshParent();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <center>

                    <asp:Panel ID="pnlContents" runat="server" Width="100%">
                        <div align="center">
                            <asp:Label ID="lblHead" runat="server"  Font-Underline="True"
                                Font-Bold="True" Font-Size="9pt"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="LblForceName" runat="server" Font-Bold="True" Font-Names="Verdana"
                                Font-Size="9pt"></asp:Label>
                        </div>
                        <br />
                        <table width="100%" align="center">
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="grdDoctor" runat="server" Width="90%" HorizontalAlign="Center" GridLines="Both" Font-Names="calibri" Font-Size="small"
                                        BorderWidth="1" AutoGenerateColumns="false" OnRowDataBound="grdDoctor_RowDataBound" HeaderStyle-Font-Size="8pt"
                                        CssClass="mGrid">

                                        <HeaderStyle BorderWidth="1" />
                                        <RowStyle BorderWidth="1" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="sf_code" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" Visible="false">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_code" runat="server" Text='<%# Bind("sf_code") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Fieldforce Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblsf_Name" runat="server" Text='<%# Bind("sf_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dr_Code" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White" Visible="false">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrCode" runat="server" Text='<%# Bind("ListedDrCode") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Listed Doctor Name" ItemStyle-HorizontalAlign="Left"
                                                HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDRName" runat="server" Text='<%# Bind("ListedDr_Name") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qual." ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQual" runat="server" Text='<%# Bind("Doc_Qua_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("ListedDr_Address1") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Speciality" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSpeciality" runat="server" Text='<%# Bind("Doc_Spec_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Bind("Doc_Cat_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Class" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClass" runat="server" Text='<%# Bind("Doc_Class_ShortName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Bind("ListedDr_Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EMail" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEMail" runat="server" Text='<%# Bind("ListedDr_Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="imgpath" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC"
                                                HeaderStyle-ForeColor="White" Visible="false">
                                                <ControlStyle></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImgpath" runat="server" Text='<%# Bind("visiting_card") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visiting Card" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC" Visible="true"
                                                HeaderStyle-ForeColor="White">

                                                <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Image ID="Image1" Width="25px" Height="25px" runat="server" class="gridImages"
                                                        ImageUrl='<%#Eval("visiting_card") %>' />
                                                    <div id="tooltip" style="display: none;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Image ID="imgUserName" Width="250px" Height="120px" ImageUrl='<%#Eval("visiting_card") %>'
                                                                        runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Registration Number" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#0097AC" Visible="true"
                                                HeaderStyle-ForeColor="White">
                                                <ControlStyle Width="90%"></ControlStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReg" runat="server" Text='<%# Bind("Registration_Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deactivation" ItemStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#0097AC" HeaderStyle-ForeColor="White">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkAll" runat="server" Text=" Deletion All " onclick="checkAll(this);" />
                                                </HeaderTemplate>
                                                <ItemStyle Width="40px" />
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAppDel" runat="server" onclick="checkapp(this); " />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </td>
                            </tr>
                        </table>
                    </asp:Panel>

                </center>

                <center>

                    <asp:Button ID="btnDelete" runat="server" BackColor="LightBlue" Text="Deactivate" Width="100px" Height="25px"
                        OnClick="btnDelete_Click" />&nbsp;&nbsp;
              
                </center>
        </div>
    </form>
</body>
</html>

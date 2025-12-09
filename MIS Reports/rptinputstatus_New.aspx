<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptinputstatus_New.aspx.cs" Inherits="MIS_Reports_rptinputstatus_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Despatch View</title>
 <link id="Link1" type="text/css" runat="server" rel="stylesheet" href="../../css/Report.css" />
        <link type="text/css" rel="stylesheet" href="../css/Report.css" />
 <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, sfname) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptinputstatus_New2.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&sfname=" + sfname,
     "_blank",
    "ModalPopUp," +
    "toolbar=no," +
    "scrollbars=yes," +
    "location=no," +
    "statusbar=no," +
    "menubar=no," +
    "addressbar=no," +
    "resizable=yes," +
    "width=800," +
    "height=500," +
    "left = 0," +
    "top=0"
    );
            popUpObj.focus();
            $(popUpObj.document.body).ready(function () {
                //var ImgSrc = "https://s3.postimg.org/d8ztbxaub/loading14.gif"
                var ImgSrc = "https://s27.postimg.org/ke5a9z0o3/11_8_little_loader.gif"
                $(popUpObj.document.body).append('<div><p style="color:orange;">Loading Please Wait.....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:650px; height: 300px;position: fixed;top: 20%;left: 10%;"  alt="" /></div>');
            });
        }
   
    </script>
     <link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/css/footable.min.css"
    rel="stylesheet" type="text/css" />
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-footable/0.1.0/js/footable.min.js"></script>




<script type="text/javascript">
    $(function () {
        $('[id*=GrdDCRDelayed]').footable();
    });
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
      <script language="javascript" type="text/javascript">

          function popUp(SF_Code, SF_Name) {
              strOpen = "samplemr.aspx?sfcode=" + SF_Code + "&FMonth=" + "4" + "&FYear=" + "2016" + "&TMonth=" + "4" + "&TYear=" + "2016" + "&SF_Name=" + SF_Name,
             window.open(strOpen, 'popWindow', 'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=800,height=600,left = 0,top = 0');
          }
    </script>
    <script type="text/javascript">
        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;         
            element.style.cursor = 'pointer';
            element.style.textDecoration = 'overline';
            element.style.color = 'red';
        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
            element.style.color = 'black';

        }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

        <asp:Panel ID="pnlbutton" runat="server">
            <table width="100%">
                <tr>
                <td width="20%"></td>
                   <td align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Input Despatch Status for the month of " ForeColor="#794044"
                                Font-Underline="True" Font-Size="14px" Font-Bold="True"></asp:Label>
                        </td>
                    <td align="right">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnPrint" runat="server" Text="Print" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        OnClick="btnPrint_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnExcel" runat="server" Text="Excel" Font-Names="Verdana" Font-Size="10px"
                                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1" Height="25px" Width="60px"
                                        />
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
        </asp:Panel>
        <br />

            <div>
                <table width="100%" align="center">
                    <tr>
                        <td width="5.5%">
                        </td>
                        <td align="left">
                            <asp:Label ID="lblIdRegionName" Text="Filed Force Name :" runat="server" SkinID="lblMand"></asp:Label>
                            <asp:Label ID="lblRegionName" runat="server" SkinID="lblMand" Font-Bold="true"></asp:Label>
                        </td>
                      
                    </tr>
                </table>
            </div>
            <br />
        <asp:Panel ID="pnlContents" runat="server">
           <%-- <center>
                <table border="0" width="90%">
                    <tr>
                        <td align="center">
                            <asp:Label ID="lblHead" runat="server" Text="Delayed Status for the month of "
                                Font-Underline="True" Font-Size="Small" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </center>--%>

            <%--<asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" Style="border-collapse: collapse;
                border: solid 1px Black;" GridLines="Both" Width="95%">
            </asp:Table>--%>
        
        <center>

            <asp:Table ID="tblworktype" runat="server" Width="95%"></asp:Table>
           
            <asp:Label ID="lblNoRecord" runat="server" Width="60%" ForeColor="Black" BackColor="AliceBlue" 
                Visible="false" Height="20px" BorderColor="Black" BorderStyle="Solid" BorderWidth="2"
                Font-Bold="True">No Records Found</asp:Label>

                <asp:GridView ID="grdDespatch" runat="server" Width="90%" HorizontalAlign="Center" ShowFooter="true"
                AutoGenerateColumns="false" PageSize="10" EmptyDataText="No Records Found" GridLines="None" OnRowDataBound="grdDespatch_OnRowDataBound"
                CssClass="mGrid" AlternatingRowStyle-CssClass="alt" >
                 <HeaderStyle Font-Bold="False" />
                            <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
               
                <Columns>                
                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left" 
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="10%"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Font-Size="10px" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SF_Code" ItemStyle-HorizontalAlign="Left"  
                        HeaderStyle-ForeColor="White" Visible="false">
                        <ControlStyle Width="20%"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSfCode" runat="server" Font-Size="10px" Text='<%# Bind("Sf_Code") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                   
                     <asp:TemplateField HeaderText="FieldForce Name" ItemStyle-HorizontalAlign="Left" 
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="180px"></ControlStyle>
                        <ItemTemplate>
                           <%-- <asp:Label ID="lblSf_name" runat="server" Font-Size="10px" Text='<%# Bind("sf_name") %>'></asp:Label>--%>
                            <asp:LinkButton ID="linksf_name" runat="server" Font-Size="12px" Text='<%# Bind("sf_name") %>'></asp:LinkButton>

                            <%--  <asp:LinkButton ID="linksf_name" runat="server" CausesValidation="False" Text='<%# Bind("sf_name") %>' Font-Bold="true" 
                                         OnClientClick='<%# "return popUp(\"" + Eval("SF_Code") + "\",\"" + Eval("sf_name")  + "\");" %>' >
                                        </asp:LinkButton>--%>
                        </ItemTemplate>
                      <%--  <FooterTemplate >
                         <asp:Label ID="lbltot" Text="Total" Font-Bold="true" runat="server" ></asp:Label>
                        </FooterTemplate>--%>

                    </asp:TemplateField>   

                    <asp:TemplateField HeaderText="HQ" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="110px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblSf_HQ" runat="server" Font-Size="10px" Text='<%# Bind("hq") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="left" 
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="50px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDesig" runat="server" Font-Size="10px" Text='<%# Bind("desg") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="OB" ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="70px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblopening" runat="server" Font-Size="10px" Text='<%# Bind("opening") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Despatch Quantity" ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="70px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblinputqty" runat="server" Font-Size="10px" Text='<%# Bind("Input_qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Issued Quantity" ItemStyle-HorizontalAlign="Center" 
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="80px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblissued" runat="server" Font-Size="10px" Text='<%# Bind("Issed_qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="CB" ItemStyle-HorizontalAlign="Left"  
                        HeaderStyle-ForeColor="White">
                        <ControlStyle Width="150px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblclosing" runat="server" Font-Size="10px" Text='<%# Bind("Closing") %>'></asp:Label>
                        </ItemTemplate>
                       
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Desig_Color" ItemStyle-HorizontalAlign="Center" Visible="false" 
                       HeaderStyle-ForeColor="White">
                        <ControlStyle Width="40px"></ControlStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblDesigColor" runat="server" Text='<%# Bind("clr") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField> 
                                                                                                                                                              
                </Columns>
                <EmptyDataRowStyle ForeColor="Black" BackColor="AliceBlue" Height="5px" BorderColor="Black"
                    BorderStyle="Solid" BorderWidth="2" Font-Bold="True" HorizontalAlign="Center"
                    VerticalAlign="Middle" />
            </asp:GridView>
        </center>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
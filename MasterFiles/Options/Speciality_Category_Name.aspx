<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Speciality_Category_Name.aspx.cs" Inherits="MasterFiles_Options_Speciality_Category_Name" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Speciality,Category and Class</title>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <br />
       
        <center>
         <h2 style="color:darkred">  <u>Speciality and Category Names (For Pasting Purpose in the Uploaded Excel)</u></h2>
            <br />
            <br />
     <table width="60%">
        <tr>
            <td>
                        <asp:Repeater ID="rptSpec" runat="server">
                            <HeaderTemplate>
                                <div id="divdr" style="background-color: #F1F5F8; text-align: center; border:1px solid">
                                    <Font Color="Red" Size="4">Speciality Name (as per Master) </Font>
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div id="detail" style="background-color: White">
                                    <div style="background-color: White; border-color:deepskyblue; border:1px dotted; height:23px; font-family:Tahoma; font-size:13px">
                                        <%#Eval("Doc_Special_Name") %>
                                    </div>
                                    <%--   <asp:Literal ID="litName" Text='<%#Eval("Doc_Special_Name") %>' runat="server"></asp:Literal>--%>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
            </td>
            <td style="vertical-align:top">
                        <asp:Repeater ID="rptCat" runat="server">
                            <HeaderTemplate>
                                <div id="divcat" style="background-color: #F1F5F8; text-align: center;border:1px solid">
                                    <Font Color="Red" Size="4">Category Name (as per Master) </Font>
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="background-color: White; border-color:deepskyblue; border:1px dotted; height:23px; font-family:Tahoma; font-size:13px">
                                    <div><%#Eval("Doc_Cat_Name")%></div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
            </td>
            <!--<td style="vertical-align:top">
                        <asp:Repeater ID="rptcls" runat="server">
                            <HeaderTemplate>
                                <div id="divcls" style="background-color: #F1F5F8; text-align: center;border:1px solid">
                                    Class
                                </div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div id="detailCls" style="background-color: White;border:1px solid;font-family:Verdana;font-size:12px">
                                    <div>
                                        <%#Eval("Doc_ClsName")%>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                         </td>-->
            </tr>
         </table>
            </center>
    </form>
</body>
</html>

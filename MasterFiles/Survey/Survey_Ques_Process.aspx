<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survey_Ques_Process.aspx.cs" Inherits="MasterFiles_Survey_Survey_Ques_Process" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update - Survey</title>
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
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }

        .panelbtn {
            float: right;
            margin-right: 70px;
        }
        .line-through {
    text-decoration: line-through;
    color:red;
  
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
            <h1>Update - Survey</h1>
        </center>
            <br />
            <asp:Panel ID="pnl" runat="server" CssClass="panelbtn">
                <asp:Button ID="btnADD" runat="server" Text="Create - Survey" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="150px"
                    OnClick="btnADD_Click" />&nbsp;
                  <asp:Button ID="btnQues" runat="server" Text="Create - Question" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="150px"
                    OnClick="btnQues_Click" />&nbsp;
                  <asp:Button ID="btnview" runat="server" Text="View" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="80px" 
                 />&nbsp;
                  <asp:Button ID="btnBack" runat="server" Text="Home" BackColor="#003366" Font-Names="Copperplate Gothic Bold" Font-Bold="False" ForeColor="White" Font-Size="Large" Height="30px" Width="80px" OnClick="btnBack_Click" 
                 />
            </asp:Panel>&nbsp;
          
            <br />
            <br />
            <center>
         <asp:GridView ID="grdSurvey" runat="server" AutoGenerateColumns="False" BackColor="#4D67A2"
                Font-Bold="False" Font-Size="Medium" ForeColor="Black" HorizontalAlign="Center"
                CssClass="table table-bordered table-striped" Width="90%" AllowSorting="true"  CellPadding="2" CellSpacing="0" OnRowCommand="grdSurvey_RowCommand" OnRowDeleting="grdSurvey_RowDeleting"
                >
               <%-- OnPreRender="GridView1_PreRender"--%>
                <FooterStyle BackColor="#86AEFC" HorizontalAlign="Left" />
                <HeaderStyle BackColor="#4D89BD" ForeColor="White" Height="12px" Font-Bold="false"
                    Font-Size="Small" HorizontalAlign="Left" Font-Names="Verdana"  />
                <RowStyle BackColor="#EFF3FB" Font-Names="Verdana" Font-Size="13px" ForeColor="#400000" />
                <EditRowStyle HorizontalAlign="Left" CssClass="gvedit" />
                <SelectedRowStyle BackColor="#ADAEB4" ForeColor="GhostWhite" />
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="Survey_Id" HeaderText="Code" Visible="false">
                        <ControlStyle BackColor="#80FFFF" />
                        <ItemStyle BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                            VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Survey Title" >
                        <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False" />
                        <ItemTemplate>
                            <%-- <a href="#" onclick="ViewQuestionPage('<%# Eval("Survey_Id") %>')">
                                <%# Eval("Quiz_Title")%></a>--%>
                            <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("Survey_Title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Creation_Date" HeaderText="Created On">
                             <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                    </asp:BoundField>
                       <asp:BoundField DataField="From_date" HeaderText="Process From Date">
                                <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                    </asp:BoundField>
                     <asp:BoundField DataField="To_Date" HeaderText="Process To Date">
                              <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NQues" HeaderText="No Of Questions">
                             <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
                    </asp:BoundField>
                     
                    <asp:TemplateField HeaderText="Create Q/A / Edit">
                             <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                              <div id="divq" runat="server">
                                
<span class="line-through">Create Q/A</span>
                           </div>
                            <div id="divview"  runat="server">
                            <a href="#" onclick="javascript:window.location.href='Survey_Creation.aspx?Survey_Id=<%# Eval("Survey_ID") %>';">
                                Create Q/A</a>
                                </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Preview Q/A">
                             <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                               
                            <a href="#" onclick="javascript:window.location.href='Survey_Preview.aspx?Survey_Id=<%# Eval("Survey_ID") %>';">
                                Preview</a>
                                
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                             <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate> 
                             <div id="div1" runat="server">
                                
<span class="line-through">Process</span>
                           </div>
                            <div id="divdata"  runat="server">                        
                           <a href="#" onclick="javascript:window.location.href='Survey_Process_Screen.aspx?Survey_Id=<%# Eval("Survey_ID") %>';">
                                Process</a>
                                </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Processed">
                             <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="Small" ForeColor="Green" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <asp:Label ID="lblProcessed" runat="server" Text='<%#Eval("Processed") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="Edit">
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <a href="#" onclick="javascript:window.location.href='Edit_Quiz_Questions.aspx?surveyId=<%# Eval("Survey_ID") %>';">
                                Edit</a>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Deactivate">
                             <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("Survey_Id") %>'
                                CommandName="Deactivate" OnClientClick="return confirm('Do you want to Deactivate the Survey');">Deactivate
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Close">
                             <HeaderStyle BorderColor="Black"
             BorderWidth="1px" BorderStyle="Solid" />
                        <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                        <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                            ForeColor="darkblue" />
                        <ItemTemplate>
                                <div id="divClosed" runat="server">
                                
<span class="line-through">Closed</span>
                           </div>
                               <div id="divclose"  runat="server">    
                            <asp:LinkButton ID="lnkClose" runat="server" CommandArgument='<%# Eval("Survey_Id") %>'
                                CommandName="Close" OnClientClick="return confirm('Do you want to Close the Survey');">Close
                            </asp:LinkButton>
                                   </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Close Flag" Visible="false">
                             
                        <ItemTemplate>
                            <asp:Label ID="lblclose" runat="server"  Text='<%#Eval("Close_flag") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </center>
        </div>
    </form>
</body>
</html>

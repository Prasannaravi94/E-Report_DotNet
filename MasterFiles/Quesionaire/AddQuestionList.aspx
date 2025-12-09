<%@ Page Language="C#" MasterPageFile="~/MasterFiles/Quesionaire/UserMasterPage.master" AutoEventWireup="true"
    CodeFile="AddQuestionList.aspx.cs" Inherits="MasterFiles_Quesionaire_AddQuestionList"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <asp:ImageButton ID="btnBack" ImageUrl="~/Images/back3.jpg" runat="server" OnClick="btnBack_Click" Style="top: 80px;
        left: 1110px; position: absolute; "
        />

 
    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/MasterFiles/Quesionaire/images/quest.jpg" Style="top: 120px;
        left: 1110px; position: absolute; height: 36px; width: 144px" 
        OnClick="ImageButton1_Click" />
      
    <asp:HiddenField ID="hidSurveyId" runat="server" />
    <br />
    <br />
    <asp:GridView ID="grdSurveyQuestions" runat="server" OnRowCreated="grdSurveyQuestions_RowCreated" ShowHeaderWhenEmpty="true" 
        AutoGenerateColumns="False" BackColor="#4D67A2" Font-Bold="False" Font-Size="Medium"
        ForeColor="Black" HorizontalAlign="Center" Width="85%" AllowSorting="true" CellPadding="2"
        CellSpacing="0" OnRowCommand="grdSurveyQuestions_RowCommand" OnRowDeleting="grdSurveyQuestions_RowDeleting">
        <FooterStyle BackColor="#86AEFC" HorizontalAlign="Left" />
        <HeaderStyle BackColor="#4D89BD" ForeColor="White" Height="12px" Font-Bold="false"
            Font-Size="Small" HorizontalAlign="Left" Font-Names="Verdana" />
        <RowStyle BackColor="#EFF3FB" Font-Names="Verdana" Font-Size="13px" ForeColor="#400000" />
        <EditRowStyle HorizontalAlign="Left" CssClass="gvedit" />
        <SelectedRowStyle BackColor="#ADAEB4" ForeColor="GhostWhite" />
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="Answer_Id" HeaderText="Code" Visible="false">
                <ControlStyle BackColor="#80FFFF" />
                <ItemStyle BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                    VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="Question_Id" HeaderText="Code" Visible="false">
                <ControlStyle BackColor="#80FFFF" />
                <ItemStyle BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left"
                    VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="Question_Type" HeaderText="
            Question Type">
                <ControlStyle Width="98%" />
                <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
            </asp:BoundField>
            <%-- <asp:BoundField DataField="Gif t_Value" HeaderText="Gift Value">
                <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
            </asp:BoundField>--%>
            <asp:BoundField DataField="Question_Text" HeaderText="Question">
                <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
            </asp:BoundField>
             <asp:BoundField DataField="Answer_Type" HeaderText="Answer Type">
                <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" HorizontalAlign="Left" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Answer Choice" ItemStyle-HorizontalAlign="Left">
                <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                    ForeColor="darkblue" />
                <ItemTemplate>
                    <asp:Panel ID="pnlAnswerOptions" runat="server">
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
           <%-- <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center">
                <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                    ForeColor="darkblue" />
                <ItemTemplate>
                    <img id="LinkButton1" src="images/edit1.gif" title="Edit question" alt="" style="cursor: hand"
                        onclick="javascript:window.location.href='AddSurveyQuestions.aspx?Question_Id=<%# Eval("Question_Id") %>';" />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                <ControlStyle Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="darkblue" />
                <ItemStyle BorderColor="LightGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="False"
                    ForeColor="darkblue" />
                <ItemTemplate>
                    <asp:ImageButton ID="LinkButton1" runat="server" ToolTip="Delete question" CommandArgument='<%# Eval("Question_Id") %>'
                        ImageUrl="~/MasterFiles/Quesionaire/images/Delete.png" CommandName="Delete" OnClientClick="return confirm('Do you want to delete this Question?');" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
         <EmptyDataTemplate>
        <div align="center" style="background-color:White; color:Red">No records found.</div>
    </EmptyDataTemplate>
    </asp:GridView>
</asp:Content>

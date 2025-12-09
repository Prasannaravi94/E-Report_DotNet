<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptQuiz_Status.aspx.cs" Inherits="MIS_Reports_rptQuiz_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--  <link type="text/css" rel="stylesheet" href="../css/Report.css" />--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />

    <style type="text/css">
        .table1, td {
            padding: 4px;
        }
    </style>
    <script type="text/javascript" language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <table width="100%">
                <tr>
                    <td></td>
                    <td align="right">
                        <table>
                            <tr>
                           
                                <td style="padding-right: 50px">
                                    <asp:LinkButton ID="btnClose" ToolTip="Close" runat="server" OnClientClick="RefreshParent()">
                                        <asp:Image ID="Image4" runat="server" ImageUrl="../../assets/images/Close.png" ToolTip="Close" Width="35px" Style="border-width: 0px;" />
                                    </asp:LinkButton>
                                    <asp:Label ID="Label4" runat="server" Text="Close" CssClass="label" Font-Size="14px"></asp:Label>

                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
            <div class="container home-section-main-body position-relative clearfix" style="max-width: 1350px;">
                <div class="row justify-content-center">
                    <div class="col-lg-12">
                        <center>
                            <asp:Label ID="lblSal" CssClass="reportheader" runat="server"></asp:Label>
                        </center>
                        <br />
                       
                        <center>
                            <asp:Label ID="lblsfname" Font-Size="14px" ForeColor="Red" CssClass="label" runat="server"></asp:Label>
                        </center>
                        <br />
                        <br />
                        <div class="display-reportMaintable clearfix">
                            <div class="table-responsive" style="scrollbar-width: thin;">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdSalesForce" runat="server" Width="100%" HorizontalAlign="Center"
                                                AutoGenerateColumns="false" EmptyDataText="No Records Found" CssClass="table" OnRowDataBound="grdSalesForce_RowDataBound"
                                                AlternatingRowStyle-CssClass="alt" GridLines="None">
                                               
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSNo" runat="server" Text='<%# (grdSalesForce.PageIndex * grdSalesForce.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Question_Id" HeaderText="Code" Visible="false" />
                                                    <%--  <asp:TemplateField HeaderText="Sf_Code" Visible="false" ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblSF_Code" runat="server" Text='<%#   Bind("SF_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Question" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQus" runat="server" Text='<%# Bind("Question_Text") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actual Answer" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblact" runat="server" Text='<%# Bind("Input_Text") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Selected Answer" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCorrect" runat="server" Text='<%# Bind("InputName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Selected Answer" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCorrectAtt" runat="server" Text='<%# Bind("NameAttempt2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Correct" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCorrectans" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdQuizResultAvg" runat="server" AutoGenerateColumns="false" GridLines="None" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <table width="13%" id="table1">
                                                                <tr>
                                                                    <td colspan="3" style="font-weight: bolder; color: red; font-size: 15px; text-align: left">Summary</td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Total Question </td>
                                                                    <td>:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblTotalQuestion" runat="server" Text='<%# Bind("TotalQuestion") %>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Correct Answer  </td>
                                                                    <td>:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblCorrectAnswer" runat="server" Text='<%# Bind("CorrectAnswer")%>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Wrong Answer  </td>
                                                                    <td>:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblWrongAnswer" runat="server" Text='<%# Bind("WrongAnswer")%>'></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Score  </td>
                                                                    <td>:</td>
                                                                    <td>
                                                                        <asp:Label ID="lblScore" runat="server" Text='<%# Bind("Score") %>'></asp:Label></td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
    </form>
</body>
</html>

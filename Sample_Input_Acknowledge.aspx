<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample_Input_Acknowledge.aspx.cs" Inherits="MasterFiles_MR_Sample_Input_Acknowledge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title>Sample Acknowledgement</title>

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/style.css" />

    <style type="text/css">
        #rdoyesNo {
            color: Black;
            font-family: Verdana;
            font-size: 25px;
            font-style: normal;
            font-weight: bold;
        }

        #Red {
            color: #FF0019;
        }

        #Green {
            color: #2B5400;
        }
    </style>
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        .marright {
            margin-left: 90%;
        }

        .error {
            color: Red;
            display: none;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function Validate() {
            //Reference the GridView.
    
            var gv = document.getElementById("<%= grdsample.ClientID %>");
        var lb = gv.getElementsByTagName("span");
        var rowCount = gv.rows.length;
        //Reference all INPUT elements.
        // var inputs = grid.getElementsByTagName("INPUT");
 
        //Set the Validation Flag to True.
       // var isValid = true;
        for (var i = 1; i < rowCount; i++) {

            var row = gv.rows[i];
            var txt = row.getElementsByTagName('input');
            var label = row.getElementsByTagName('span');
            var area = row.getElementsByTagName('textarea');
            if (number(txt[0].value) > number(label[3].innerHTML)) {
                alert('Not Possible to Enter more then Despatched Qty')
                txt[0].focus();
                return false;
            } else {
                if (txt[0].value != label[3].innerHTML) {
                  
                    if (area[0].value == "") {
                        alert('Enter Remarks')
                        area[0].focus();
                      
                        return false;
                    }
                    else {

                    }
                }

            }

        }
 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <br />

        <br />
        <br />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                    <table width="100%" border="0" cellpadding="0" cellspacing="4" align="center">
                        <tr>
                            <td align="center">
                                <asp:Label ID="LblUser" runat="server" Text="User" Style="text-transform: capitalize; font-size: 22px;"
                                    CssClass="reportheading"> </asp:Label>
                            </td>

                        </tr>
                        <tr style="height: 15px">
                        </tr>

                        <tr>
                            <td align="center">
                                <h2 class="text-center">Sample Acknowledgement</h2>
                            </td>

                        </tr>
                        <tr>
                            <td align="right">


                                <asp:Button ID="btnHome" runat="server" Width="150px" Height="30px" Visible="false" CssClass="savebutton"
                                    Text="Direct to Home Page" OnClick="btngohome_click" />
                                &nbsp;&nbsp;
                           <asp:Button ID="btnLogout" runat="server" Width="90px" Height="30px" CssClass="resetbutton"
                               Text="Logout" OnClick="btnLogout_Click" />
                            </td>
                        </tr>

                        <tr>
                            <td align="center">
                                <asp:Label ID="lblmonth" runat="server" Text="" ForeColor="Black" Font-Size="Medium"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <center>

                        <br />
                        <br />
                        <div class="row justify-content-center">
                            <div class="col-lg-12">
                                <div class="display-table clearfix">
                                    <div class="table-responsive" style="scrollbar-width: thin;">
                                        <asp:GridView ID="grdsample" runat="server" Width="100%" HorizontalAlign="Center"
                                            AutoGenerateColumns="False" AllowSorting="true"
                                            EmptyDataText="No Records Found"
                                            GridLines="None" CssClass="table">

                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ControlStyle Width="90%"></ControlStyle>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSNo" runat="server" Text='<%# (grdsample.PageIndex * grdsample.PageSize) + ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprdtcode" runat="server" Text='<%#Eval("Product_Code_SlNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprdtname" runat="server" Text='<%#Eval("Product_Detail_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pack">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpack" runat="server" Text='<%#Eval("Product_Sale_Unit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Despatch Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldespatch" runat="server" Text='<%#Eval("Despatch_Actual_qty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Actual Reced.Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtreceivedqty" runat="server" Text='<%#Eval("Despatch_Actual_qty") %>' Width="50px" MaxLength="3"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mnth" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMnth" runat="server" Text='<%#Eval("Trans_Month") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Num" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnum" runat="server" Text='<%#Eval("upl_sl_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemarks" runat="server" Width="250px"
                                                            TextMode="MultiLine"></asp:TextBox>
                                                       
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                </div>

                            </div>

                        </div>
                        <div class="single-des clearfix">
                            <div class="single-des-option">
                                <asp:Button ID="btnsavesample" runat="server" Text="Save" Width="60px" CssClass="savebutton" OnClientClick="return Validate()" OnClick="btnsavesample_click" Visible="false" />
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="single-des clearfix" style="min-width: 280px">

                            <asp:RadioButtonList ID="rdoyesNo" AutoPostBack="true" runat="server"
                                RepeatDirection="Horizontal" Width="200px" Height="40px"
                                OnSelectedIndexChanged="rdoyesNo_SelectedIndexChanged">
                                <asp:ListItem ID="Green" Value="1">Yes</asp:ListItem>
                                <asp:ListItem ID="Red" Value="2">No</asp:ListItem>

                            </asp:RadioButtonList>
                        </div>

                    </center>
                </div>
            </div>
        </div>
        <br />
        <br />


    </form>
</body>
</html>

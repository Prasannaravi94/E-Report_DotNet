<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Input_Modify_MRWise_Rpt.aspx.cs" Inherits="MIS_Reports_Input_Modify_MRWise_Rpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- <link type="text/css" rel="stylesheet" href="../css/Report.css" />
    <script src="../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="description" content="" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="//fonts.googleapis.com/css?family=Roboto:300,400,500,700,900&display=swap"
        rel="stylesheet" />
    <link rel="stylesheet" href="../../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../../assets/css/style.css" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <link rel="stylesheet" href="../assets/css/font-awesome.min.css" />
    <link rel="stylesheet" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" href="../assets/css/style.css" />
    <link rel="stylesheet" href="../assets/css/nice-select.css" />
 
    <script src="../../JsFiles/jquery-1.10.1.js" type="text/javascript"></script>
    <script language="Javascript">
        function RefreshParent() {
            window.opener.document.getElementById('form1').click();
            window.close();
        }
    </script>
    <script type="text/javascript">
        var popUpObj;
        function showModalPopUp(sfcode, fmon, fyr, tmon, tyr, sfname, div_code) {
            //popUpObj = window.open("VisitDetailsReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rptinputstatus_New2_New.aspx?sfcode=" + sfcode + "&FMonth=" + fmon + "&FYear=" + fyr + "&TMonth=" + tmon + "&TYear=" + tyr + "&sfname=" + sfname + "&div_code=" + div_code,
      "ModalPopUp," //+
      //"toolbar=no," +
      //"scrollbars=yes," +
      //"location=no," +
      //"statusbar=no," +
      //"menubar=no," +
      //"addressbar=no," +
      //"resizable=yes," +
      //"width=800," +
      //"height=500," +
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

    <script type="text/javascript">
        $(function () {
            $('#btnExcel').click(function () {
                var url = 'data:application/vnd.ms-excel,' + encodeURIComponent($('#pnlContents').html())
                location.href = url
                return false
            })
        })
    </script>

    <style type="text/css">
        .blink_me {
            -webkit-animation-name: blinker;
            -webkit-animation-duration: 1s;
            -webkit-animation-timing-function: linear;
            -webkit-animation-iteration-count: infinite;
            -moz-animation-name: blinker;
            -moz-animation-duration: 1s;
            -moz-animation-timing-function: linear;
            -moz-animation-iteration-count: infinite;
            animation-name: blinker;
            animation-duration: 1s;
            animation-timing-function: linear;
            animation-iteration-count: infinite;
        }

        @-moz-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @-webkit-keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        @keyframes blinker {
            0% {
                opacity: 1.0;
            }

            50% {
                opacity: 0.0;
            }

            100% {
                opacity: 1.0;
            }
        }

        .blink {
            animation: blink-animation 1s steps(5, start) infinite;
            -webkit-animation: blink-animation 1s steps(5, start) infinite;
        }

        @keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }

        @-webkit-keyframes blink-animation {
            to {
                visibility: hidden;
            }
        }
    </style>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript"> 
        var $items;
        $(document).ready(function () {
            $items = $('select[id$=ddlStateHdn] option');
        });
        function movetoNext(CurrentFieldID) {
            var idString = CurrentFieldID.id;
            var $i1 = idString.indexOf("_");
            $i1 = $i1 + 4;
            var $i2 = idString.indexOf("_", $i1);
            var cnt = 0;
            var index = '';

            if ((idString.substring($i1, $i2) - 1) < 9) {
                var cIdprev = parseInt(idString.substring($i1, $i2));
                index = cnt.toString() + cIdprev.toString();
            }
            else {
                var cIdprev = parseInt(idString.substring($i1, $i2));
                index = cIdprev;
            }
            //grdDespatch_ctl02_ddlState
            var $txt1 = CurrentFieldID.value;
            var $txt = $('input[id$="grdDespatch_ctl' + index + '_txtsearch"]');
            var $ddl = $('select[id$="grdDespatch_ctl' + index + '_ddlState"]');
            //var $items = $('select[id$="ddlStateHdn"] option');

            //$txt.keyup(function () {
            //if($txt.val()!="")
                searchDdl($txt.val());
         
            //});


            function searchDdl(item) {
                $ddl.empty();

                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                    function (n) {
                        return exp.test($(n).text());
                    });

                if (arr.length > 0) {
                    debugger
                    //var i = 0;
                    //countItemsFound(arr.length);
                    $.each(arr, function () {

                       // $ddl[i].append(this);
                        // i++;
                        $ddl.append('<option value=' + this.value + '>' + this.text + '</option>');

                        $ddl.get(0).selectedIndex = 0;
                        //$ddl.get(0).selectedIndex = 0;

                        //$ddl.append(this);
                        //console.log($ddl.selector);
                        //$ddl[0].get(0).selectedIndex = 0;
                    }
                    );
                    //i = 0;
                }
                else {
                    countItemsFound(arr.length);
                    //$('#'+$ddl[0].id).append("<option>No Items Found</option>")
                    $ddl.append("<option>No Items Found</option>");
                }
                console.log($ddl.selector);
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.length) {
                    $("#para").html(num + " items found");
                }
            }
        }

    </script>
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../assets/css/select2.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pnlbutton" runat="server">
        </asp:Panel>
        <asp:Panel ID="pnlContents" runat="server" Width="100%">
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-center">
                    <div class="col-lg-12">
                        <div class="designation-reactivation-table-area clearfix">
                            <asp:DropDownList ID="ddlStateHdn" runat="server" Style="display: none;"
                                DataTextField="Gift_Name" DataValueField="Gift_Code">
                            </asp:DropDownList>
                            <br />
                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin; overflow:inherit">
                                    <asp:GridView ID="grdDespatch" runat="server" AlternatingRowStyle-CssClass="alt"
                                        AutoGenerateColumns="false" CssClass="table" EmptyDataText="No Records Found"
                                        GridLines="None" HorizontalAlign="Center" OnRowDataBound="grdDr_RowDataBoud" BorderWidth="0" Width="100%" OnRowDeleting="Gridview1_RowDeleting" ShowFooter="true">
                                        <HeaderStyle Font-Bold="False" />
                                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="#">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSNo" runat="server" Text='<%#  ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
<%--                                            <asp:TemplateField HeaderText="Product Code" ItemStyle-HorizontalAlign="Left" Visible="false">
                                                <ItemTemplate>
                                                         <asp:DropDownList ID="ddlState" runat="server" CssClass="nice-select" DataSource="<%# FillState() %>"
                                                        DataTextField="StateName" DataValueField="state_code"></asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Product Name" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtsearch" runat="server" onkeyup="movetoNext(this)">

                                                    </asp:TextBox>
                                                   <%-- <asp:Label ID="lblprd_Name" runat="server" Text='<%#Eval("Product_Detail_Name")%>'></asp:Label>--%>
                                                    <asp:DropDownList ID="ddlState" runat="server" DataSource="<%# FillState() %>"
                                                        DataTextField="Gift_Name" DataValueField="Gift_Code"></asp:DropDownList> <%--CssClass="custom-select2 nice-select"--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issued" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <asp:textbox ID="lblissued" runat="server" Text='<%# Bind("issue_befor") %>'></asp:textbox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />
                        </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="true" />
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="no-result-area" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                <center>
                <asp:Button ID="btntransfer" runat="server" Width="80px" Height="25px" Text="Update" CssClass="savebutton" OnClick="BtnUpdate_Click" Visible="false" />


            </center>
        </asp:Panel>
    </form>
    <script src="//cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
</body>
</html>


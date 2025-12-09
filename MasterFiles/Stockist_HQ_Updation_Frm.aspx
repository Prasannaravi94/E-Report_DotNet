<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Stockist_HQ_Updation_Frm.aspx.cs"
    Inherits="MasterFiles_Stockist_HQ_Updation_Frm" EnableEventValidation="false" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stockist - HQ - Updation</title>

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
    <%--  <link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <link href="../assets/css/Calender_CheckBox.css" rel="stylesheet" />
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <link href="../JScript/BootStrap/dist/css/jquerysctipttop.css" rel="stylesheet" type="text/css" />
        <%--<link href="../JScript/BootStrap/dist/css/ServiceCSS.css" rel="stylesheet" type="text/css" />--%>
    <link href="../JScript/DateJs/AlertCSS.css" rel="stylesheet" type="text/css" />
    <script src="../JScript/DateJs/ValidationAlertJs.js" type="text/javascript"></script>

    <script src="../JScript/Service_CRM/Stockist_JS/Stockist_HQ_Updation_JS.js" type="text/javascript"></script>
     <%--<link href="../JScript/Service_CRM/Crm_Dr_Css_Ob/Stockist_HQ_Updation_Css.css" rel="stylesheet"
        type="text/css" />--%>


    <style type="text/css">
        .input-sm {
            -webkit-tap-highlight-color: transparent;
            background-color: #fff;
            border-radius: 5px;
            border: solid 1px #e8e8e8;
            box-sizing: border-box;
            clear: both;
            cursor: pointer;
            display: block;
            float: left;
            font-family: inherit;
            font-size: 14px;
            font-weight: normal;
            height: 42px;
            line-height: 40px;
            outline: none;
            padding-left: 18px;
            padding-right: 30px;
            position: relative;
            text-align: left !important;
            -webkit-transition: all 0.2s ease-in-out;
            transition: all 0.2s ease-in-out;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            white-space: nowrap;
            width: auto;
        }

        .input-sm {
            width: 100%;
            color: #414d55;
            font-size: 14px;
        }

        .home-section-main-body .input-sm {
            color: #90a1ac;
            font-size: 14px;
            border-radius: 8px;
            border: 1px solid #d1e2ea;
            background-color: #f4f8fa;
        }

        .input-sm option {
            background-color: white;
            scrollbar-width: thin;
        }

            .input-sm option selected {
                background-image: linear-gradient(to top, #0496ff 0%, #28b5e0 100%);
                color: #fff;
            }

        .display-table #tblStockHQ th:first-child {
            font-size: 14px;
        }

        .textbox, .input-sm {
            min-width: 150px;
        }
     
    </style>

    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>
    <link href="../../../assets/css/select2.min.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <ucl:Menu ID="menu1" runat="server" />

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center ">
                    <div class="col-lg-11">
                        <h2 class="text-center" style="border-bottom: none">Stockist - HQ - Updation</h2>
                        <div class="designation-reactivation-table-area clearfix">
                            <div class="display-name-heading clearfix">

                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-12">
                                        <asp:Button ID="btnEdit" runat="server" Width="80px" CssClass="savebutton"
                                            Text="Bulk Edit" OnClick="btnEdit_Click" />
                                    </div>
                                </div>
                                <br />
                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-3">
                                        <%--
                                           <asp:Label ID="lblSaleField" runat="server" Text="" SkinID="lblMand"></asp:Label>
                                           <span style="margin-left:102px;"><b>:</b></span>
                                        --%>
                                        <asp:CheckBoxList ID="chkSelect"
                                            RepeatDirection="Vertical" RepeatColumns="2" runat="server">
                                            <asp:ListItem Text="Field Forcewise" Value="1" ClientValue="1" onclick="UncheckOthers(this);"></asp:ListItem>
                                            <asp:ListItem Text="Statewise" Value="2" ClientValue="2" onclick="UncheckOthers(this);"></asp:ListItem>
                                        </asp:CheckBoxList>
                                    </div>
                                </div>
                                <br />

                                <div class="row  justify-content-center clearfix">
                                    <div class="col-lg-5">
                                        <div id="Filed_Div" style="display: none">
                                            <div class="row clearfix">
                                                <div class="col-lg-10">
                                                    <div class="single-des clearfix">
                                                        <asp:Label ID="Lbldivi" runat="server" CssClass="label">Field Force</asp:Label>
                                                        <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2" style="padding-top: 23px; padding-left: 0px">
                                                    <input type="button" id="btnGo" value="Go" class="savebutton" style="width: 50px;" />
                                                </div>
                                            </div>
                                        </div>
                                        <div id="State_Div" style="display: none">
                                            <div class="row clearfix">
                                                <div class="col-lg-10">
                                                    <div class="single-des clearfix">
                                                        <asp:Label ID="lblStateName" runat="server" CssClass="label">State Name</asp:Label>
                                                        <asp:DropDownList ID="ddlStateName" runat="server" CssClass="custom-select2 nice-select" Width="100%">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-lg-2" style="padding-top: 23px; padding-left: 0px">
                                                    <input type="button" id="btnStateName" value="Go" class="savebutton" style="width: 50px;" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <br />
                            <br />

                            <div class="display-table clearfix">
                                <div class="table-responsive" style="scrollbar-width: thin;">
                                    <table id="tblStockHQ" class="table" style="width: 100%">
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="row  justify-content-center clearfix">
                                <div class="col-lg-2">
                                    <input type="button" id="btnUpdate" class="savebutton" value="Update" />
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />

        </div>
        <%--  <div id="shader" class="shader">
            <div id="loading" class="bar">
                <p>
                    loading
                </p>
            </div>
        </div>--%>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js"></script>
    </form>
</body>
</html>

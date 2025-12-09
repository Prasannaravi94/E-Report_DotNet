<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Holiday_Upload.aspx.cs" Inherits="MasterFiles_Options_Holiday_Upload" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Holiday Fixation Bulk Upload</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" href="../../Images/favicon.ico" type="image/x-icon" />
    <link href="../../css/bootstrap-4.3.1-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../../css/Font-Awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/buttons/1.5.6/css/buttons.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/responsive/2.2.1/css/responsive.dataTables.css" />
    <link href="../../css/Holiday_Upload.css" rel="stylesheet" />
    <link href="../../css/jquery-confirm.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <input id="DD_SfCode" type="hidden" value='<%= Session["sf_code"] %>' />
        <input id="DD_SfType" type="hidden" value='<%= Session["sf_type"] %>' />
        <input id="DD_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
        <div>
            <ucl:Menu ID="menu1" runat="server" />
        </div>
        <br />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <br />
                    <h2 class="text-center" id="hHeading" runat="server"></h2>
                     <a href="Statecode_Statename.aspx" target="_new" 
       style="float: right; color: red; font-size: 16px;" >State View</a>

                    <div class="designation-area clearfix">
                        <div class="single-des clearfix label">
                            Note:<br />
                            1) Sheet Name Must be 'UPL_Holiday_Fixation'<br />
                            2) Date Format Must be in 'YYYY-MM-DD' Format<br />
                            3) Don't Do Any Special Formats in the Excel File<br />
                            Excel Format File
                            <asp:LinkButton ID="lnkDownload" runat="server" Font-Size="12px" Font-Names="Verdana"
                                Text="Download Here" OnClick="lnkDownload_Click"> 
                            </asp:LinkButton>
                        </div>

                        <br />
                        <div class="single-des clearfix label">
                            <div class="row justify-content-center">
                                <div class="col-lg-9">
                                    <input type="file" id="fileUpload" class="input" style="width:100%" />
                                </div>
                                <div class="col-lg-2">
                                    <button type="button" id="upload" runat="server" class="savebutton">Process</button>
                                </div>
                            </div>
                        </div>
                        <div id="eData">
                            <br />
                            <span id="lblError" style="color: Red;">Kindly rectify the error! Uploaded Holiday Name(s) not matched with Holiday Master:</span>
                            <table id="tblUploadE" class="table table-bordered tbSSBFS" runat="server" width="100%" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script src="../../JsFiles/jquery-3.4.1.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script src="../../css/bootstrap-4.3.1-dist/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/responsive/2.2.1/js/dataTables.responsive.js"></script>
    <script type="text/javascript" src="//cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/xlsx.full.min.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/xlsx/0.13.5/jszip.js"></script>
    <script type="text/javascript" src="../../JScript/Holiday_Upload.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-confirm.js"></script>
    <div class="container">
        <div class="row">
            <div id="loader" style="display: none">
                <div class="dot">
                </div>
                <div class="dot">
                </div>
                <div class="dot">
                </div>
                <div class="dot">
                </div>
                <div class="dot">
                </div>
                <div class="dot">
                </div>
                <div class="dot">
                </div>
                <div class="dot">
                </div>
                <div class="lading">
                </div>
            </div>
        </div>
    </div>
</body>
</html>

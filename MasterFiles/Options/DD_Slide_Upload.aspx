<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DD_Slide_Upload.aspx.cs" Inherits="MasterFiles_DD_Slide_Upload"
    EnableEventValidation="false" EnableViewState="true" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>--%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Slide_Upload_-_E-Detailing</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" href="../../Images/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap4.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/buttons/1.5.6/css/buttons.dataTables.min.css" />
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/responsive/2.2.1/css/responsive.dataTables.css" />
    <link href="../../css/Font-Awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/themes/smoothness/jquery-ui.css" media="screen" />
    <link type="text/css" rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/plupload/2.3.6/jquery.plupload.queue/css/jquery.plupload.queue.css" media="screen" />
    <link type="text/css" href="//fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,400,600,700,300|Bree+Serif" rel="stylesheet" />
    <link type="text/css" rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/css/bootstrap-select.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.css" />
    <link href="../../css/EDetailing/DD_Slide_Upload.css" rel="stylesheet" />
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.min.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <input id="DD_SfCode" type="hidden" value='<%= Session["sf_code"] %>' />
        <input id="DD_SfType" type="hidden" value='<%= Session["sf_type"] %>' />
        <input id="DD_DivCode" type="hidden" value='<%= Session["div_code"] %>' />
        <div>
            <%--<ucl:Menu ID="menu1" runat="server" />--%>
        </div>
        <br />
        <div class="container" style="background-color: white;">
            <div class="row">
                <div class="col-sm-12">
                    <div class="pull-left">
                        <a href="#" class="help" title="Help" data-toggle='modal' data-target='#modelHelp'>Help&nbsp;<i class="fa fa-question-circle"></i></a>
                    </div>
                    <div class="pull-right">
                        <asp:Button ID="btnBack" runat="server" ClientIDMode="Static" OnClick="btnBack_Click" CssClass="btn btn-sm btn-primary" Text="Back" />
                    </div>
                </div>
            </div>
            <br />
            <center>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="btn-group btn-toggle">
                            <button id="btnUpload" class="btn btn-default" data-toggle="collapse" data-target="divUpload">Upload</button>
                            <button id="btnView" class="btn btn-primary" data-toggle="collapse" data-target="divView">View</button>
                            <button id="btnPriority" class="btn btn-default" data-toggle="collapse" data-target="divPriority">Priority</button>
                            <%--<button id="btnPreview" class="btn btn-default" data-toggle="collapse" data-target="divPreview">Preview</button>--%>
                        </div>
                        <br />
                        <br />
                        <div id="divUpload">
                            <div class="form-group form-group-sm">
                                <div class="row">
                                    <div class="col-md-1 col-sm-1">
                                        <asp:Label ID="lblUDivision" Text="Division :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlUDivision" runat="server" ClientIDMode="Static" CssClass="selectpicker form-control"
                                            Enabled="false" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Label ID="lblUSubdivision" Text="Sub Division :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlUSubdivision" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" />
                                    </div>
                                    <div class="col-md-1 col-sm-1">
                                        <asp:Label ID="lblUBrand" Text="Brand :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlUBrand" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Button ID="btnUView" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Go" />
                                    </div>
                                </div>
                                <br />
                                <div class="row uCategory">
                                    <div class="col-md-1 col-sm-1">
                                        <asp:Label ID="lblUProd" Text="Product :" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-3">
                                        <asp:DropDownList ID="ddlUProduct" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" multiple data-actions-box="true" />
                                    </div>
                                    <div class="col-md-1 col-sm-1">
                                        <asp:Label ID="lblUSpec" Text="Speciality :" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-3">
                                        <asp:DropDownList ID="ddlUSpeciality" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" multiple data-actions-box="true" />
                                    </div>
                                    <div class="col-md-1 col-sm-1">
                                        <asp:Label ID="lblUTherapy" Text="Therapy :" runat="server" />
                                    </div>
                                    <div class="col-md-3 col-sm-3">
                                        <asp:DropDownList ID="ddlUTherapy" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" multiple data-actions-box="true" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div id="uploader">
                                            <p></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divView">
                            <div class="form-group form-group-sm">
                                <div class="row">
                                    <div class="col-md-1 col-sm-1">
                                        <asp:Label ID="lblVDivision" Text="Division :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlVDivision" runat="server" ClientIDMode="Static" CssClass="selectpicker form-control"
                                            Enabled="false" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Label ID="lblVSubdivision" Text="Sub Division :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlVSubdivision" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" />
                                    </div>
                                    <div class="col-md-1 col-sm-1">
                                        <asp:Label ID="lblVBrand" Text="Brand :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlVBrand" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Button ID="btnVView" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Go" />
                                    </div>
                                </div>
                                <br />
                                <div class="vCategory">
                                    <div class="row">
                                        <div class="col-md-1 col-sm-1">
                                            <asp:Label ID="lblFilter" Text="Filter By :" runat="server" />
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input type="radio" class="custom-control-input" id="rdProduct" value="1" name="filter">
                                                <label class="custom-control-label" for="rdProduct">
                                                    Product
                                                </label>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input type="radio" class="custom-control-input" id="rdSpeciality" value="2" name="filter" />
                                                <label class="custom-control-label" for="rdSpeciality">Speciality</label>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input type="radio" class="custom-control-input" id="rdTherapy" value="3" name="filter" />
                                                <label class="custom-control-label" for="rdTherapy">Therapy</label>
                                            </div>
                                        </div>
                                        <div class="col-md-2 col-sm-2">
                                            <div class="custom-control custom-radio custom-control-inline">
                                                <input type="radio" class="custom-control-input" id="rdNone" value="0" name="filter" checked />
                                                <label class="custom-control-label" for="rdNone">
                                                    None
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-1 col-sm-1">
                                            <asp:Label ID="lblVProd" Text="Product :" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-3">
                                            <asp:DropDownList ID="ddlVProduct" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                                data-live-search="true" />
                                        </div>
                                        <div class="col-md-1 col-sm-1">
                                            <asp:Label ID="lblVSpec" Text="Speciality :" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-3">
                                            <asp:DropDownList ID="ddlVSpeciality" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                                data-live-search="true" />
                                        </div>
                                        <div class="col-md-1 col-sm-1">
                                            <asp:Label ID="lblVTherapy" Text="Therapy :" runat="server" />
                                        </div>
                                        <div class="col-md-3 col-sm-3">
                                            <asp:DropDownList ID="ddlVTherapy" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                                data-live-search="true" />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-3 col-sm-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3 col-sm-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3 col-sm-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3 col-sm-3">
                                        <%--<a href="#" class="Priority float" title="Priority / Common Update"><i class="fa fa-check my-float"></i></a>--%>
                                        <button class="btn btn-sm btn-primary float Common" width="100%" title='Common Update'>
                                            Update&nbsp;<i class="fa fa-check"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table id="grdUpload" class="table table-bordered tbSSBFS" runat="server" width="100%" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divPriority">
                            <div class="form-group form-group-sm">
                                <div class="row">
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Label ID="lblMasPriority" Text="Update Priority for :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <div class="custom-control custom-radio custom-control-inline">
                                            <input type="radio" class="custom-control-input" id="rdBrandP" value="0" name="Priority" />
                                            <label class="custom-control-label" for="rdBrandP">
                                                Brand
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <div class="custom-control custom-radio custom-control-inline">
                                            <input type="radio" class="custom-control-input" id="rdProductP" value="1" name="Priority" />
                                            <label class="custom-control-label" for="rdProductP">Product</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <div class="custom-control custom-radio custom-control-inline">
                                            <input type="radio" class="custom-control-input" id="rdSpecialityP" value="2" name="Priority" />
                                            <label class="custom-control-label" for="rdSpecialityP">Speciality</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <div class="custom-control custom-radio custom-control-inline">
                                            <input type="radio" class="custom-control-input" id="rdTherapyP" value="3" name="Priority" />
                                            <label class="custom-control-label" for="rdTherapyP">
                                                Therapy
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row prdPriority">
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Label ID="lblPSubdivision" Text="Sub Division :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlPSubdivision" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" />
                                    </div>
                                    <div class="col-md-1 col-sm-1">
                                        <asp:Label ID="lblPMode" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlPMode" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Button ID="btnPView" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Go" />
                                    </div>
                                </div>
                                <br />
                                <div class="row grdPriority">
                                    <div class="col-md-3 col-sm-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3 col-sm-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3 col-sm-3">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3 col-sm-3">
                                        <%--<a href="#" class="MasPriority" title="Priority Update"><i class="fa fa-check my-float"></i></a>--%>
                                        <button class="btn btn-sm btn-primary float MasPriority" width="100%" title='Priority Update'>
                                            Update&nbsp;<i class="fa fa-check"></i>
                                        </button>
                                    </div>
                                </div>
                                <div class="row grdPriority">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table id="grdPriority" class="table table-bordered tbSSBFS" runat="server" width="100%" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divPreview">
                            <div class="form-group form-group-sm">
                                <div class="row prdPreview">
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Label ID="lblPreSubdivision" Text="Sub Division :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlPreSubdivision" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" />
                                    </div>
                                    <div class="col-md-1 col-sm-1">
                                        <asp:Label ID="lblPreMode" Text="Brand :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:DropDownList ID="ddlPreMode" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                            data-live-search="true" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Button ID="btnPreViewGo" runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Go" />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-2 col-sm-2">
                                        <asp:Label ID="lblMasPreview" Text="Update Preview for :" runat="server" />
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <div class="custom-control custom-radio custom-control-inline">
                                            <input type="radio" class="custom-control-input" id="rdBrandPre" value="0" name="Preview" />
                                            <label class="custom-control-label" for="rdBrandPre">
                                                Brand
                                            </label>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <div class="custom-control custom-radio custom-control-inline">
                                            <input type="radio" class="custom-control-input" id="rdProductPre" value="1" name="Preview" />
                                            <label class="custom-control-label" for="rdProductPre">Product</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <div class="custom-control custom-radio custom-control-inline">
                                            <input type="radio" class="custom-control-input" id="rdSpecialityPre" value="2" name="Preview" />
                                            <label class="custom-control-label" for="rdSpecialityPre">Speciality</label>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2">
                                        <div class="custom-control custom-radio custom-control-inline">
                                            <input type="radio" class="custom-control-input" id="rdTherapyPre" value="3" name="Preview" />
                                            <label class="custom-control-label" for="rdTherapyPre">
                                                Therapy
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row grdPreview">
                                    <div class="col-sm-12">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <table id="grdPreview" class="table table-bordered tbSSBFS" runat="server" width="100%" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </center>
            <br />
            <center>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group form-group-sm">
                        </div>
                    </div>
                </div>
            </center>
        </div>
        <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel">Edit</h4>
                    </div>
                    <div class="modal-body">
                        <%--<form method='post' action='' enctype="multipart/form-data">
                            <div id='preview'>
                                <img id='imgName' style='height: 120px; width: 150px;'>
                            </div>
                            <br />
                            <asp:Label ID="lblMProduct" Text="Product :" runat="server" />
                            <asp:DropDownList ID="ddlMProduct" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                data-live-search="true" multiple />
                            <br />
                            <asp:Label ID="lblMSpeciality" Text="Speciality :" runat="server" />
                            <asp:DropDownList ID="ddlMSpeciality" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                data-live-search="true" multiple />
                            <br />
                            <asp:Label ID="lblMTherapy" Text="Therapy :" runat="server" />
                            <asp:DropDownList ID="ddlMTherapy" ClientIDMode="Static" CssClass="selectpicker form-control" runat="server"
                                data-live-search="true" multiple />
                            <br />
                            <asp:Label ID="lblMImage" Text="Select file :" runat="server" />
                            <input type='file' name='file' id='file' class='form-control'><br>
                            <input type='button' class='btn btn-info' value='Upload' id='upload'>
                        </form>--%>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="submit" class="btn btn-primary">Save changes</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="modelHelp" tabindex="-1" role="dialog" aria-labelledby="lblHelp" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">
                            <span aria-hidden="true" class="">×   </span><span class="sr-only">Close</span>
                        </button>
                        <h4 class="modal-title" id="lblHelp">Help</h4>
                    </div>
                    <div class="modal-body1">
                        <ol style="list-style-type: disc; padding-left: 20px;">
                            <li>Upload
                                <ol style="list-style-type: disc; margin-left: 20px;">
                                    <li>Supported files Formats:
                                        <ol style="list-style-type: disc; margin-left: 20px;">
                                            <li>Image - jpg,gif,png</li>
                                            <li>Document - pdf</li>
                                            <li>Video - mp4,ogv,avi,mov,flv,3gp</li>
                                            <li>HTML - zip</li>
                                        </ol>
                                    </li>
                                    <li>HTML source files must be compressed to ZIP file format</li>
                                    <li>Compressed file must include single root folder only
                                        <ol style="disc; margin-left: 20px;">
                                            <li>- Ex: FOLDER_NAME/Index.html</li>
                                        </ol>
                                    </li>
                                    <li>Maximum file size:</li>
                                </ol>
                            </li>
                            <br />
                            <li>View
                                <ol style="list-style-type: disc; margin-left: 20px;">
                                    <li>To Group/Ungroup Multiple slides, Check/Uncheck Common field</li>
                                </ol>
                            </li>
                        </ol>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.12.4.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.3/umd/popper.min.js" integrity="sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49" crossorigin="anonymous"></script>
    <script type="text/javascript" src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.3/js/bootstrap.min.js" integrity="sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy" crossorigin="anonymous"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/jquery-ui.js"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/plupload/2.3.6/plupload.full.min.js" charset="UTF-8"></script>
    <script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/plupload/2.3.6/jquery.plupload.queue/jquery.plupload.queue.min.js" charset="UTF-8"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap4.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/dataTables.buttons.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/responsive/2.2.1/js/dataTables.responsive.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.flash.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.32/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.32/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.5.6/js/buttons.print.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.js"></script>
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/bootstrap-select@1.13.10/dist/js/bootstrap-select.min.js"></script>
    <script type="text/javascript" src="../../JScript/Service_CRM/EDetailing/DD_Slide_Upload.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery-confirm/3.3.2/jquery-confirm.min.js"></script>
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

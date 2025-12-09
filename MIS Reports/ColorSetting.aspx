<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ColorSetting.aspx.cs" Inherits="MIS_Reports_ColorSetting" %>

<%@ Register Src="~/UserControl/pnlMenu.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Color setting</title>
    <meta http-equiv="content-type" content="text/html;charset=utf-8" />
    <meta name="author" content="Fajar Chandra" />
    <meta name="viewport" content="width=device-width,initial-scale=1" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>

    <style type="text/css">
        code {
            background: #eee;
            border: dotted 1px #ccc;
        }

        select,
        input[type=text],
        input[type=button] {
            background: #fff;
            color: #000;
            font-family: "Tahoma", "Arial", sans-serif;
            font-size: 14px;
            border: solid 1px #aaa;
            border-radius: 4px;
            box-shadow: inset 1px 1px 1px rgba(0,0,0,.3);
            padding: 0 5px;
            box-sizing: border-box;
            height: 26px;
            line-height: 26px;
        }

        input[type=button] {
            background: #fff;
            box-shadow: inset -1px -1px 1px rgba(0,0,0,.3);
        }

            input[type=button]:active {
                box-shadow: inset 1px 1px 1px rgba(0,0,0,.3);
            }

        input[type=text][readonly] {
            background: #ddd;
        }

        .row {
            margin: 0 -10px;
        }

            .row:after {
                content: '';
                display: block;
                clear: both;
            }

        .col-4 {
            float: left;
            width: 25%;
            padding: 0 10px;
            box-sizing: border-box;
        }

            .col-4 select,
            .col-4 input {
                width: 100%;
                box-sizing: border-box;
            }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <br />
        <br />
		<h2 class="text-center" id="heading" runat="server"></h2>
        <br />
        <script type="text/javascript" src="../assets/js/colorpalette/jquery.wheelcolorpicker.js"></script>
        <link type="text/css" rel="stylesheet" href="../assets/css/colorpalette/wheelcolorpicker.css" />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-4">
                    
                    <div class="row">
                        <center>
                            <div class="col-lg-8">
                                Select Color:
                            <input type="text" cssclass="nice-select" id="txtColor" data-wheelcolorpicker data-wcp-sliders="wv" data-wcp-preview="true" />
                            </div>
                            <br />
                            <div class="col-lg-8">
                                Select Division:
                                <asp:DropDownList ID="ddlDivisionCode" runat="server" onchange="GetMaster1Details()" CssClass="nice-select" SkinID="ddlRequired" Style="width: 100%; height: 19px;">
                                </asp:DropDownList>
                            </div>
                            <br />
                            <br />
                            <button type="button" value="Save Color" text="Save Color" class="savebutton" onclick="ColorSaveFun()">Save Color</button>
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <style type="text/css">
            .color-block {
                max-width: 340px;
                width: 100%;
                box-sizing: border-box;
            }

            .color-preview-box {
                display: inline-block;
                width: .75em;
                height: .75em;
                vertical-align: middle;
                padding: 2px;
                background-clip: content-box;
                border: solid 1px #888;
            }
        </style>
        <script type="text/javascript">
            $(function () {
                $('#color-block').on('colorchange', function (e) {
                    var color = $(this).wheelColorPicker('value');
                    var alpha = $(this).wheelColorPicker('color').a;
                    $('.color-preview-box').css('background-color', color);
                    $('.color-preview-text').text(color);
                    document.body.style.backgroundColor = color;
                    $('.color-preview-alpha').text(Math.round(alpha * 100) + '%');
                });
            });
    
            //get color values
            function ColorSaveFun() {
                var GetColorCode = "#" + document.getElementById('txtColor').value;
                var GetDivision = document.getElementById('ddlDivisionCode').value;
                ColorSave(GetDivision + "^" + GetColorCode);
				alert("Saved color");
            }
            // save to color DB
            function ColorSave(DataColorCode) {
                $.ajax({
                    type: 'POST',
                    url: "ColorSetting.aspx/bg_colorSave",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    data: '{objData:' + JSON.stringify(DataColorCode) + '}',
                    success: function (data) {

                    },
                });
            }
        </script>
    </form>
</body>
</html>




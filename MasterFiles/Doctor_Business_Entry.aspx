<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Doctor_Business_Entry.aspx.cs" Inherits="MasterFiles_Doctor_Business_Entry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head runat="server">
    <title>Daily Work Entry</title>
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
    <link href="../css/font-awesome.css" rel="stylesheet" type="text/css" />

    <link href="../css/Dr_Busi_Entry.css" rel="stylesheet" type="text/css" />
    <script src="../JsFiles/jquery-1.8.3.min.js" type="text/javascript"></script>
   <%-- <script src="../../JsFiles/Dr_Business_Entry_1.0.js" type="text/javascript"></script>--%>
     <script src="../../JsFiles/Dr_Business_Entry_1.1.js" type="text/javascript"></script>

    <style>
        .modalMain_One {
            position: relative;
            margin: 15% auto;
            padding: 5px 20px 13px 20px;
            border-radius: 10px;
            color: white;
            width: 500px;
        }


        .modalDialog {
            position: fixed;
            font-family: Arial, Helvetica, sans-serif;
            top: 0;
            right: 0;
            bottom: 0;
            left: 0;
            background: rgba(0, 0, 0, 0.1);
            z-index: 99999;
            /*opacity: 0;*/
            -webkit-transition: opacity 400ms ease-in;
            -moz-transition: opacity 400ms ease-in;
            transition: opacity 400ms ease-in;
            /*pointer-events: none;*/
            /*opacity: 1;
            pointer-events: auto;*/
        }

        /*.modalDialog:target */ .modaltarget {
            opacity: 1;
            pointer-events: auto;
        }

        /*.modalDialog > div*/ .modalMain {

            position: relative;
            /*margin: 10% auto;*/
            margin: 15% auto;
            padding: 5px 20px 13px 20px;
            border-radius: 10px;
            background: linear-gradient(#265faa, #b3aec8);
           
            color: white;
        }

        .close {
            background: #606061;
            color: #FFFFFF;
            line-height: 25px;
            position: absolute;
            right: -12px;
            text-align: center;
            top: -10px;
            width: 24px;
            text-decoration: none;
            font-weight: bold;
            -webkit-border-radius: 12px;
            -moz-border-radius: 12px;
            border-radius: 12px;
            -moz-box-shadow: 1px 1px 3px #000;
            -webkit-box-shadow: 1px 1px 3px #000;
            box-shadow: 1px 1px 3px #000;
        }

            .close:hover {
                background: #6ed1d8;
            }

        .button-stay {
            display: inline-block;
            vertical-align: middle;
            position: relative;
            overflow: hidden;
            /*min-width: 96px;*/
            width: 30%;
            margin: 10px;
            padding: 8px 0 9px;
            font-size: 14px;
            color: white;
            text-align: center;
            text-decoration: none;
            text-shadow: 0 1px #154c86;
            background-color: #247edd;
            background-clip: padding-box;
            border: 1px solid;
            border-top-color: currentcolor;
            border-right-color: currentcolor;
            border-bottom-color: currentcolor;
            border-left-color: currentcolor;
            border-color: #1c65b2 #18589c #18589c;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-box-shadow: inset 0 1px rgba(255, 255, 255, 0.4), 0 1px 2px rgba(0, 0, 0, 0.2);
            box-shadow: inset 0 1px rgba(255, 255, 255, 0.4), 0 1px 2px rgba(0, 0, 0, 0.2);
            background-image: -webkit-linear-gradient(top, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0) 50%, rgba(0, 0, 0, 0.12) 51%, rgba(0, 0, 0, 0.04));
            background-image: -moz-linear-gradient(top, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0) 50%, rgba(0, 0, 0, 0.12) 51%, rgba(0, 0, 0, 0.04));
            background-image: -o-linear-gradient(top, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0) 50%, rgba(0, 0, 0, 0.12) 51%, rgba(0, 0, 0, 0.04));
            background-image: linear-gradient(to bottom, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0) 50%, rgba(0, 0, 0, 0.12) 51%, rgba(0, 0, 0, 0.04));
        }

        .div-align {
            text-align: center;
        }

        .btn-Draft {
            display: inline-block;
            vertical-align: middle;
            /*position: relative;*/
            overflow: hidden;
            min-width: 96px;
            float: right;
            margin: 10px;
            padding: 8px 0 9px;
            font-size: 14px;
            color: white;
            text-align: center;
            text-decoration: none;
            text-shadow: 0 1px #154c86;
            background-color: #247edd;
            background-clip: padding-box;
            border: 1px solid;
            border-top-color: currentcolor;
            border-right-color: currentcolor;
            border-bottom-color: currentcolor;
            border-left-color: currentcolor;
            border-top-color: currentcolor;
            border-right-color: currentcolor;
            border-bottom-color: currentcolor;
            border-left-color: currentcolor;
            border-color: #1c65b2 #18589c #18589c;
            border-radius: 4px;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-box-shadow: inset 0 1px rgba(255, 255, 255, 0.4), 0 1px 2px rgba(0, 0, 0, 0.2);
            box-shadow: inset 0 1px rgba(255, 255, 255, 0.4), 0 1px 2px rgba(0, 0, 0, 0.2);
            background-image: -webkit-linear-gradient(top, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0) 50%, rgba(0, 0, 0, 0.12) 51%, rgba(0, 0, 0, 0.04));
            background-image: -moz-linear-gradient(top, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0) 50%, rgba(0, 0, 0, 0.12) 51%, rgba(0, 0, 0, 0.04));
            background-image: -o-linear-gradient(top, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0) 50%, rgba(0, 0, 0, 0.12) 51%, rgba(0, 0, 0, 0.04));
            background-image: linear-gradient(to bottom, rgba(255, 255, 255, 0.3), rgba(255, 255, 255, 0) 50%, rgba(0, 0, 0, 0.12) 51%, rgba(0, 0, 0, 0.04));
        }
    </style>



    <style type="text/css">
        .btn {
            border: 2px solid black;
            background-color: white;
            color: black;
            /*padding: 14px 28px;*/
            padding: 10px 25px;
            font-size: 16px;
            cursor: pointer;
        }

        .btnEdt {
            /*border: 2px solid black;*/
            background-color: white;
            color: White;
            background-color: #247edd;
            /*padding: 14px 28px;*/
            padding: 10px 25px;
            font-size: 16px;
            cursor: pointer;
            border-radius: 10px;
        }
        /* Green */
        .FullDy {
            /*border-color: #04AA6D;*/
            border-color: #1ca913;
            color: green;
            border-radius: 10px;
        }

            .FullDy:hover {
                background-color: #1ca913;
                color: white;
                border-radius: 10px;
            }

        /* Blue */
        .HfDay {
            /*border-color: #2196F3;*/
            border-color: #247edd;
            color: dodgerblue;
            border-radius: 10px;
        }

            .HfDay:hover {
                background: #247edd;
                color: white;
                border-radius: 10px;
            }

        .clsDay {
            padding: 2px 8px;
            /*color: #20356E;*/
            font-weight: bolder;
            /*background: #89C15A;*/
            background: rgb(90, 164, 193);
            display: inline-block;
            color: white;
        }

 
        .plnStayPlace {
            /*max-width: 2%;*/
            min-width: 3%;
        }

        #tSFC, #tblSFC {
            /*table-layout: fixed;*/
            width: 90%;
        }




        .highlight {
            background-color: #89C15A;
        }
    </style>




    <style>
        .custom-dropdown {
            /* width: 200px;*/
            width: 15%;
            border: 1px solid #ccc;
            border-radius: 4px;
            position: relative;
            padding: 20px;
        }

        .dropdown-header {
            padding: 10px;
            cursor: pointer;
            background-color: #f9f9f9;
        }

        .dropdown-list {
            display: none; /* Initially hidden */
            border-top: 1px solid #ccc;
            max-height: 150px;
            overflow-y: auto;
            background-color: #fff;
            position: absolute;
            width: 100%;
            z-index: 10;
        }

        .dropdown-item {
            padding: 10px;
            cursor: pointer;
        }

            .dropdown-item:hover {
                background-color: #f1f1f1;
            }

        .show {
            display: block;
        }

        .count {
            font-weight: bold;
            color: #007BFF; /* Bootstrap primary color */
            margin-left: 5px;
        }
    </style>




</head>

<body class="loading">
    <div id="lckMsg"></div>
    <form id="frm" runat="server">
        <div class="modal"></div>






        <div class="Process modl">
            <div class="container">
                <b>Loading....Please Wait..</b>
                <progress></progress>
            </div>
        </div>

        <div class="ProcessMsg modalDialog">
            <div class="container">
                <a class="txtMsg"></a>
                <progress></progress>
            </div>
        </div>

        <div class="pad HDBg" style="height: 55px">
            <a href="#" class="button mnu-bt" onclick="toggleMenu()"><i class="fa fa-bars"></i></a>

            <a runat="server" id="aHome" class="button home-bt">Home</a>


            <div style="display: inline;">
                <div class="hCap">
                    <b>Doctor Business Entry For :</b>
                </div>
                <div runat="server" class="highlightor" id="SFInf" style="font-weight:bolder;font-size:larger"></div>
                -
          <%--<div runat="server" id="divMonth">--%>
                <div class="hCap">
                    <asp:Label ID="lblMonth" runat="server" Text="Month " CssClass="label clsMonth"></asp:Label>

                    <asp:DropDownList ID="ddlMonth" runat="server" class="ddlBox clsMonth" Style="display: inherit; width:70px;">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Feb"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Mar"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Apr"></asp:ListItem>
                        <asp:ListItem Value="5" Text="May"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Jun"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Jul"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Aug"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Sep"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Oct"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Nov"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblYear" runat="server" Text="Year " CssClass="label clsMonth"></asp:Label>
                    <asp:DropDownList ID="ddlYear" runat="server" class="ddlBox clsMonth" Style="display: inherit; width: 70px;">
                    </asp:DropDownList>
                </div>

                <a href="#" class="plnPlholder button go-bt clsMonth" onclick="DCR.ShowCusDt()" style="float: inherit; width: auto">GO</a>
                <%-- </div>--%>

                <div id="DtInf" runat="server" class="dtDisp"></div>

                <a href="#" class="plnPlholder button go-bt ResetGo" onclick="DCR.Reset()" style="float: inherit; width: auto">Reset</a>

              
            </div>
          
        </div>
        <div id="mnuWrapr">

            <div class="aside active">
                <div class="sidebar">


                    <div class="WTplcH">
                        <span class="mnuLblCap">Mode</span>

                    </div>
                    <ul id="mnuTab"></ul>


                    
                </div>
            </div>




            <div id="WorkArea" class="Work-Area" style="max-width: fit-content">
                <div id="planer">
                    <div runat="server" class="plnPlholder" id="Plchold_HQ">
                        <span class="lblCap">Headquarter</span>
                        <asp:DropDownList ID="ddl_HQ" onchange="ChangeHQ();" class="ddlBox" runat="server">
                        </asp:DropDownList>
                    </div>



                    <div class="plnPlholder" runat="server" id="Plchold_SDP">
                        <span runat="server" id="lbSDP" class="lblCap lblSDP">SDP</span>
                        <asp:DropDownList ID="ddl_SDP" class="ddlBox" runat="server">
                            <%--onchange="srtCusTwbs(this);"--%>
                        </asp:DropDownList>
                    </div>

                    <a href="#" class="plnPlholder button go-bt TerriGo" onclick="DCR.ShowDr()" style="float: inherit; width: auto">GO</a>

                 
                </div>
                <div id="working-Area">
                </div>


                <div class="alert-box notice" style="display: none"><span></span></div>

                <div id="openModal_One">
                    <div id="ModalPanel_One">
                        <div id="wProd" class="wind-o">
                            <table class="fg-group TBH">
                                <tr>
                                    <th w="280">Product Name</th>
                                    <th w="80">Pack</th>
                                    <th w="80">Rate</th>
                                    <th w="80">Business Qty</th>
                                    <th w="80">Value</th>




                                    <th w="40"><a href="#" class="button go-bt" onclick="addRow('tProd')">+</a></th>
                                </tr>
                            </table>
                            <div class="wScroll">
                                <table id='tProd' class="fg-group TBD">
                                    <tr>
                                        <td>
                                            <div class="ddl-Box search Prod" data-value="" data-text="- Select the Product -" data-src="d_Prod" data-vf='id' data-tf='name'></div>
                                        </td>
                                        <td>
                                            <asp:Label class="lbl Pack_Inp" runat="server" Text=""></asp:Label>
                                        </td>

                                        <td>
                                            <asp:Label class="lbl Rate_Inp" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <input type="number" class="tBQ" name="tBQty" min="0" maxlength="3"></td>
                                        <td>
                                            <input type="number" class="tBV" name="tBValue" value="0" min="0" maxlength="7" disabled></td>

                                        <td><a href="#" class="button button-red go-bt" onclick="delRow(this)">-</a></td>
                                    </tr>
                                </table>
                            </div>
                            <div style="height: 22px; padding: 2px; margin: 5px 0px;"><a href='#' class='button button-green go-bt' onclick="svWinVal(event)" style="margin: 0px 5px; float: left">Save</a><a href='#' class='button button-red go-bt' style="margin: 0px 5px; float: left" onclick="ddlWinClose()">Cancel</a></div>
                        </div>
                      
                    </div>
                </div>

            </div>


        </div>






        <asp:HiddenField ID="hSF_Code" runat="server" />
        <asp:HiddenField ID="hDiv" runat="server" />
        <asp:HiddenField ID="hSFTyp" runat="server" />
        <asp:HiddenField ID="hDCRDt" runat="server" />
        <asp:HiddenField ID="hSTime" runat="server" />
        <asp:HiddenField ID="hCurrDt" runat="server" />
       

        <asp:HiddenField ID="hTtl_Value" runat="server" />
        <%--Newly Added--%>


        <asp:ScriptManager ID="ScriptService" EnablePageMethods="true" runat="server">
        </asp:ScriptManager>

        <script type="text/javascript">
            _C = {
                //ses: { id: 'ses', cap: 'Session', w: '80', src: 'd_Ses', df: '- Ses -', val: '', ty: 'ddl-Box', iw: '120', gw: '80' },
                //tm: { id: 'tm', cap: 'Time', w: '90', ty: 'txt_Tm' },
                cus: { id: 'cus', cap: 'Customer', w: '400', src: 'd_cus', df: '-- Customer --', val: '', ty: 'ddl-Box search', iw: '400', adf: 'TCd,TNm,sf,D_spec,D_cat' },
                //pob: { id: 'pob', cap: 'POB', w: '90', val: '', df: '4,2', ty: 'txt_Dc', mxl: 7 },
                //jw: { id: 'jw', cap: 'Joint Work', w: '200', src: 'd_JW', df: '', val: '', ty: 'ddl-Box multi', iw: '300', gw: '300' },
                prd: { id: 'prd', cap: 'Product', w: '500', src: 'wProd', vf: 'id', tf: 'name', df: '- Product -', val: '', ty: 'ddl-Box wind', iw: '300' },
                //inp: { id: 'inp', cap: 'Input', w: '90', src: 'wInput', vf: 'id', tf: 'name', df: '- Input -', val: '', ty: 'ddl-Box wind', iw: '300' },
                //rem: { id: 'rem', cap: 'Remark', w: '70', src: 'wRem', df: '', val: '', ty: 'ddl-Box wind', iw: "300", mxl: 350 },
                spec: { id: 'spec', cap: 'Specialty', w: '70', src: 's_spec', df: '', val: '', ty: '', iw: "100", mxl: 100 },
                cat: { id: 'cat', cap: 'Category', w: '70', src: 's_cat', df: '', val: '', ty: '', iw: "100", mxl: 100 },
                sub_area: { id: 'sub_area', cap: 'Sub Area', w: '70', src: 's_B_Qty', df: '', val: '', ty: '', iw: "200", mxl: 200 },
                B_Val: { id: 'B_Val', cap: 'Value', w: '70', src: 's_B_Val', df: '', val: '', ty: '', iw: "100", mxl: 100 },


                go: { id: 'go', cap: 'Go', w: '90', gw: '60', src: 'ins', df: 'Go', val: '', ty: 'button green' }
            };
            __Menu = {
                D: { name: 'Listed Doctor', key: 'D', ic: 'stethoscope', eSrc: 'drs' },
                //C: { name: 'Chemist', key: 'C', ic: 'flask', eSrc: 'chm' },
                //S: { name: 'Stockist', key: 'S', ic: 'ambulance', eSrc: 'stk' },
                //H: { name: 'Hospital', key: 'H', ic: 'hospital-o', eSrc: 'hos' },
                //U: { name: 'Unlisted Doctor', key: 'U', ic: 'stethoscope', eSrc: 'udr' },
                //R: { name: 'Remarks', key: 'R', ic: 'commenting', imp: 1 },
                P: { name: 'Preview', key: 'P', ic: 'search', imp: 1 }
                ////A: { name: 'Day Report', key: 'A', ic: 'search', imp: 1 },
                //E: { name: 'Delete', key: 'E', ic: 'search', imp: 1 }
            };
            document.oncontextmenu = function () { return false; };

          



            document.querySelector('.tBQ').addEventListener('keydown', function (e) {
                // Prevent decimal point and "e"/"E" used in scientific notation
                if (e.key === '.' || e.key === ',' || e.key === 'e' || e.key === 'E') {
                    e.preventDefault();
                }
            });
        </script>






    </form>
</body>
</html>

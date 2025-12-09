<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Visit_Details_Cat_Cls_Spclty_LstDr_Wise.aspx.cs"
    Inherits="MasterFiles_AnalysisReports_Coverage_Analysis" %>

<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Category/Class/Speciality & Listed Doctor Wise Visit Details</title>
    <link href="../../css/Font-Awesome-4.7.0/css/font-awesome.css" rel="stylesheet" />
    <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        var popUpObj;

        <%--var cbValuedate = "";
            var cbTxtdate = "";

            var CHK = document.getElementById("<%=chkdate.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    //alert("Selected = " + label[i].innerHTML);                    
                    cbTxtdate += label[i].innerHTML.trim() + ",";
                    //alert(cbValue);
                }
            }
            var checked_checkboxesdate = $("[id*=chkdate] input:checked");
            var message = "";
            checked_checkboxesdate.each(function () {
                //var value = $(this).parent().attr('cbValue');
                //var text = $(this).closest("td").find("label").html();
                //message += "Text: " + text + " Value: " + value;
                //message += "\n";
                cbValuedate += $(this).parent().attr('cbValueDate') + ",";
            });
            var cbValue = "";
            var cbTxt = "";
            var toYear = "";
            var toMonth = "";--%>
        function showModalPopUp_Dt(sfcode, fmon, fyr, sf_name, ddMdINDEX, chk) {
            var cbValue2 = "";
            var cbValue = "";
            var cbTxt = "";
            var toYear = "";
            var toMonth = "";


            if (ddMdINDEX != 4) {
                var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");
                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        //alert("Selected = " + label[i].innerHTML);                    
                        cbTxt += label[i].innerHTML + ",";
                        //alert(cbValue);
                    }
                }
                //
                var checked_checkboxes = $("[id*=chkdate] input:checked");
                var message = "";
                checked_checkboxes.each(function () {
                    //var value = $(this).parent().attr('cbValue');
                    //var text = $(this).closest("td").find("label").html();
                    //message += "Text: " + text + " Value: " + value;
                    //message += "\n";
                    cbValue2 += $(this).parent().attr('cbValueDate') + ",";
                });
                var checked_checkboxes = $("[id*=cbSpeciality] input:checked");
                var message = "";
                checked_checkboxes.each(function () {
                    //var value = $(this).parent().attr('cbValue');
                    //var text = $(this).closest("td").find("label").html();
                    //message += "Text: " + text + " Value: " + value;
                    //message += "\n";
                    cbValue += $(this).parent().attr('cbValue') + ",";
                });
              //  alert(cbValue2);
            }
            else if (ddMdINDEX === 4) {
                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var frmMonth = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var frmYear = frmMonYear[1];

                var mnth = frmMonth, yr = parseInt(frmYear), validate = '', tmp = '';
                var ToMYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var tMonth = new Date(ToMYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMYear[0] + '-1-01').getMonth() + 1 :
                new Date(ToMYear[0] + '01, 0001').getMonth() + 1;
                var tYear = ToMYear[1];


                while (yr <= parseInt(toYear)) {
                    if (validate != (toMonth.toString() + "_" + toYear)) {
                        if (mnth < 10) {
                            tmp = "0" + mnth.toString();
                        }
                        else
                            tmp = mnth.toString();

                        cbValuedate += tmp + "_" + yr.toString() + ",";
                        validate = mnth.toString() + "_" + yr.toString();
                        mnth++;
                        if (mnth === 13) {
                            mnth = 1;
                            yr += 1;
                        }
                    }
                    else {
                        break;
                    }
                }
                //alert(cbValue);
            }

        var ToMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
            var toMonth = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
            var toYear = ToMonYear[1];
            var url = '';
            if (ddMdINDEX != 0) {
                switch (ddMdINDEX) {
                    case 4:
                        url = "Visit_Details_ListedDr_Report.aspx";
                        break;
                    default:
                        url = "Visit_Details_Cat_Cls_Spclty_Dtwise.aspx";
                        break;
                }
            }
           // alert(cbValue2);
            var popUpObj;
            var randomnumber = Math.floor((Math.random() * 100) + 1);
            popUpObj = window.open(url + "?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&TMonth=" + toMonth + "&Tyear=" + toYear + "&sf_name=" + sf_name + "&cbVal=" + cbValue + "&cbTxt=" + cbTxt + "&cbValue2=" + cbValue2 + "&cMode=" + ddMdINDEX + "&XlsDown=1",
                    "ModalPopUp" + randomnumber//,
                    //"toolbar=no," +
                    //"scrollbars=yes," +
                    //"location=no," +
                    //"statusbar=no," +
                    //"menubar=no," +
                    //"addressbar=no," +
                    //"resizable=yes," +
                    //"width=800," +
                    //"height=600," +
                    //"left = 0," +
                    //"top=0"
                    );
            popUpObj.focus();
            //
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s3.postimg.org/h5u7rfuvn/08_spinner.gif"
                $(popUpObj.document.body).append('<div><p style="color:blue;margin-top:10%;margin-left:40%;">Loading Please Wait....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:310px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });

        }
        function showModalPopUp(sfcode, fmon, fyr, sf_name, ddMdINDEX, div_code) {
            var cbValue = "";
            var cbTxt = "";
            var toYear = "";
            var toMonth = "";

            if (ddMdINDEX != 4) {
                var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");
                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        //alert("Selected = " + label[i].innerHTML);                    
                        cbTxt += label[i].innerHTML + ",";
                        //alert(cbValue);
                    }
                }
                //HIDDEN Values
                cbtx = cbTxt.toString();
                alert(cbtx);
                var checked_checkboxes = $("[id*=cbSpeciality] input:checked");
                var message = "";
                checked_checkboxes.each(function () {
                    //var value = $(this).parent().attr('cbValue');
                    //var text = $(this).closest("td").find("label").html();
                    //message += "Text: " + text + " Value: " + value;
                    //message += "\n";
                    cbValue += $(this).parent().attr('cbValue') + ",";
                });
                //  cbva = cbValue.toString();
                //  alert(cbva);
                //alert(message);                
            }
            else if (ddMdINDEX === 4) {
                    <%--var frmYear = $('#<%=ddlYearTo.ClientID%> :selected').text();
                var frmMonth = $('#ddlMonth').find(":selected").index();
                toYear = $('#<%=ddlYear.ClientID%> :selected').text();
                toMonth = $('#ddlMonthTo').find(":selected").index();--%>
                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var frmMonth = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var frmYear = frmMonYear[1];

                var mnth = frmMonth, yr = parseInt(frmYear), validate = '', tmp = '';


                var ToMYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var tMonth = new Date(ToMYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMYear[0] + '-1-01').getMonth() + 1 :
                new Date(ToMYear[0] + '01, 0001').getMonth() + 1;
                var tYear = ToMYear[1];

                while (yr <= parseInt(tYear)) {
                    if (validate != (tMonth.toString() + "_" + tYear)) {
                        if (mnth < 10) {
                            tmp = "0" + mnth.toString();
                        }
                        else
                            tmp = mnth.toString();

                        cbValue += tmp + "_" + yr.toString() + ",";
                        validate = mnth.toString() + "_" + yr.toString();
                        mnth++;
                        if (mnth === 13) {
                            mnth = 1;
                            yr += 1;
                        }
                    }
                    else {
                        break;
                    }
                }
                //alert(cbValue);
            }

        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
            var toMonth = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
            var toYear = ToMonYear[1];

                <%--toYear = $('#<%=ddlYear.ClientID%> :selected').text();
            toMonth = $('#ddlMonthTo').find(":selected").index();--%>

            var url = '';
            if (ddMdINDEX != 0) {
                switch (ddMdINDEX) {
                    case 4:
                        url = "Visit_Details_ListedDr_Report.aspx";
                        break;
                    default:
                        url = "Visit_Details_Cat_Cls_Spclty_Report.aspx";
                        break;
                }
            }
            var randomnumber = Math.floor((Math.random() * 100) + 1);
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open(url + "?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&TMonth=" + toMonth + "&Tyear=" + toYear + "&sf_name=" + sf_name + "&cbVal=" + cbValue + "&cbTxt=" + cbTxt + "&cMode=" + ddMdINDEX + "&div_code=" + div_code + "&XlsDown=1",
                "ModalPopUp" + randomnumber//,
                //"toolbar=no," +
                //"scrollbars=yes," +
                //"location=no," +
                //"statusbar=no," +
                //"menubar=no," +
                //"addressbar=no," +
                //"resizable=yes," +
                //"width=800," +
                //"height=600," +
                //"left = 0," +
                //"top=0"
                );
            popUpObj.focus();
            //
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s3.postimg.org/h5u7rfuvn/08_spinner.gif"
                $(popUpObj.document.body).append('<div><p style="color:blue;margin-top:10%;margin-left:40%;">Loading Please Wait....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:310px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }
    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

            $('#btnSubmit').click(function () {
                // alert("coming");

                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                //var BaseLvlName = $('#<%=ddlBaseLvl.ClientID%> :selected').text();
                //if (BaseLvlName == "---Select---") { alert("Select Base Level Name."); $('#ddlBaseLvl').focus(); return false; }
                <%--var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                if (Year == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }--%>
                var Month = $('#<%=ddlMode.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Mode."); $('#ddlMode').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                //var mr_Code = document.getElementById('<%=ddlBaseLvl.ClientID%>').value;
                <%--var Year1 = document.getElementById('<%=ddlYearTo.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;--%>
                var ddMdINDEX = $('#ddlMode').find(":selected").index();
                var DateWiseChecked = document.getElementById("<%=chkD.ClientID%>");
                if (ddMdINDEX != 0) {
                    if (ddMdINDEX != 4) {
                        var iCount = 0;
                        var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
                        var checkbox = CHK.getElementsByTagName("input");
                        var label = CHK.getElementsByTagName("label");
                        for (var i = 0; i < checkbox.length; i++) {
                            if (checkbox[i].checked) {
                                iCount++;
                            }
                        }
                        if (iCount === 0) {
                            alert("Select valid Checkbox...");
                            return false;
                        }
                    }

                    var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                    var fromMon = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                    var fromYear = frmMonYear[1];
                    if (ddMdINDEX == "1" || ddMdINDEX == "2")
                    {
                         if (!DateWiseChecked.checked) {
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        var ToMon = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        var ToYear = ToMonYear[1];
                    }

                    }
                   
                    var mnth = fromMon, yr = parseInt(fromYear), validate = '', tmp = '';
                    var div_code;
                    var sf_type = '3';
                    if (sf_type == 1 || sf_type == 2) {//mr or mgr login
                        div_code = '<%=Session["div_code"] %>';
                    }
                    else {
                        div_code = '<%=Session["div_code"] %>';
                        if (div_code.includes(",")) {
                            div_code = div_code.substring(0, div_code.length - 1)
                        }
                    }

                    if (ddMdINDEX != 3 && ddMdINDEX != 4 && ddMdINDEX != 5) {

                        var chkD = document.getElementById("chkD");
                        if (chkD.checked) {
                            var iCount = 0;
                            var CHK = document.getElementById("<%=chkdate.ClientID%>");
                            var checkbox = CHK.getElementsByTagName("input");
                            var label = CHK.getElementsByTagName("label");
                            for (var i = 0; i < checkbox.length; i++) {
                                if (checkbox[i].checked) {
                                    iCount++;
                                }
                            }
                            if (iCount === 0) {
                                alert("Select Day...");
                                return false;
                            }

                            if (DateWiseChecked.checked) {
                                //with daywise
                                //showModalPopUp_WDaywise(sf_Code, fromMon, fromYear, Name, ddMdINDEX, 0);
                                showModalPopUp_Dt(sf_Code, fromMon, fromYear, Name, ddMdINDEX, 0);
                            }
                            else {
                                //without daywise, from -to month year
                                //showModalPopUp_Dt(sf_Code, fromMon, fromYear, ToMon, ToYear, Name, ddMdINDEX, 0);
                                showModalPopUp(sf_Code, fromMon, fromYear, Name, ddMdINDEX, div_code);
                            }

                        }
                        else {
                            showModalPopUp(sf_Code, fromMon, fromYear, Name, ddMdINDEX, div_code);
                        }
                    }
                    else {
                        showModalPopUp(sf_Code, fromMon, fromYear, Name, ddMdINDEX, div_code);
                    }
                    //else {
                    //    alert("Select Valid Month & Year...");
                    //    return false;
                    //}
                }
            });
        });
    </script>

    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp(sfcode, fmon, fyr, sf_name, ddMdINDEX, div_code) {
            var cbValue = "";
            var cbTxt = "";
            var toYear = "";
            var toMonth = "";

            if (ddMdINDEX != 4) {
                var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");
                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        //alert("Selected = " + label[i].innerHTML);                    
                        cbTxt += label[i].innerHTML + ",";
                        //alert(cbValue);
                    }
                }
                //HIDDEN Values
                cbtx = cbTxt.toString();
                $("#cbtx").val(cbtx)
                //  alert(cbtx);
                var checked_checkboxes = $("[id*=cbSpeciality] input:checked");
                var message = "";
                checked_checkboxes.each(function () {
                    //var value = $(this).parent().attr('cbValue');
                    //var text = $(this).closest("td").find("label").html();
                    //message += "Text: " + text + " Value: " + value;
                    //message += "\n";
                    cbValue += $(this).parent().attr('cbValue') + ",";
                });
                cbva = cbValue.toString();
                $("#cbva").val(cbva)
                //  alert(cbva);
                //alert(message);                
            }
            else if (ddMdINDEX === 4) {
                <%--var frmYear = $('#<%=ddlYearTo.ClientID%> :selected').text();
                var frmMonth = $('#ddlMonth').find(":selected").index();
                toYear = $('#<%=ddlYear.ClientID%> :selected').text();
                toMonth = $('#ddlMonthTo').find(":selected").index();--%>
                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var frmMonth = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var frmYear = frmMonYear[1];

                var mnth = frmMonth, yr = parseInt(frmYear), validate = '', tmp = '';


                var ToMYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var tMonth = new Date(ToMYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMYear[0] + '-1-01').getMonth() + 1 :
                new Date(ToMYear[0] + '01, 0001').getMonth() + 1;
                var tYear = ToMYear[1];

                while (yr <= parseInt(tYear)) {
                    if (validate != (tMonth.toString() + "_" + tYear)) {
                        if (mnth < 10) {
                            tmp = "0" + mnth.toString();
                        }
                        else
                            tmp = mnth.toString();

                        cbValue += tmp + "_" + yr.toString() + ",";
                        validate = mnth.toString() + "_" + yr.toString();
                        mnth++;
                        if (mnth === 13) {
                            mnth = 1;
                            yr += 1;
                        }
                    }
                    else {
                        break;
                    }
                }
                //alert(cbValue);
            }

        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
            var toMonth = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
            var toYear = ToMonYear[1];

        <%--toYear = $('#<%=ddlYear.ClientID%> :selected').text();
            toMonth = $('#ddlMonthTo').find(":selected").index();--%>

            var url = '';
            if (ddMdINDEX != 0) {
                switch (ddMdINDEX) {
                    case 4:
                        url = "Visit_Details_ListedDr_Report.aspx";
                        break;
                    default:
                        url = "Visit_Details_Cat_Cls_Spclty_Report.aspx";
                        break;
                }
            }
            var randomnumber = Math.floor((Math.random() * 100) + 1);
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open(url + "?sf_code=" + sfcode + "&FMonth=" + fmon + "&Fyear=" + fyr + "&TMonth=" + toMonth + "&Tyear=" + toYear + "&sf_name=" + sf_name + "&cbVal=" + cbValue + "&cbTxt=" + cbTxt + "&cMode=" + ddMdINDEX + "&div_code=" + div_code + "&XlsDown=1",
                "ModalPopUp" + randomnumber//,
                //"toolbar=no," +
                //"scrollbars=yes," +
                //"location=no," +
                //"statusbar=no," +
                //"menubar=no," +
                //"addressbar=no," +
                //"resizable=yes," +
                //"width=800," +
                //"height=600," +
                //"left = 0," +
                //"top=0"
                );
            popUpObj.focus();
            //
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s3.postimg.org/h5u7rfuvn/08_spinner.gif"
                $(popUpObj.document.body).append('<div><p style="color:blue;margin-top:10%;margin-left:40%;">Loading Please Wait....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:310px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }
    </script>

    <script type="text/javascript">
        var popUpObj;

        function showModalPopUp_Excel(sfcode, fmon, fyr, sf_name, ddMdINDEX, div_code) {
            var cbValue = "";
            var cbTxt = "";
            var toYear = "";
            var toMonth = "";

            if (ddMdINDEX != 4) {
                var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");
                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        cbTxt += label[i].innerHTML + ",";
                    }
                }
                //HIDDEN Values
                cbtx = cbTxt.toString();
                $("#cbtx").val(cbtx)
                //  alert(cbtx);
                var checked_checkboxes = $("[id*=cbSpeciality] input:checked");
                var message = "";
                checked_checkboxes.each(function () {
                    cbValue += $(this).parent().attr('cbValue') + ",";
                });
                cbva = cbValue.toString();
                $("#cbva").val(cbva)
            }
            else if (ddMdINDEX === 4) {
                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var frmMonth = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var frmYear = frmMonYear[1];

                var mnth = frmMonth, yr = parseInt(frmYear), validate = '', tmp = '';


                var ToMYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var tMonth = new Date(ToMYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMYear[0] + '-1-01').getMonth() + 1 :
                new Date(ToMYear[0] + '01, 0001').getMonth() + 1;
                var tYear = ToMYear[1];

                while (yr <= parseInt(tYear)) {
                    if (validate != (tMonth.toString() + "_" + tYear)) {
                        if (mnth < 10) {
                            tmp = "0" + mnth.toString();
                        }
                        else
                            tmp = mnth.toString();

                        cbValue += tmp + "_" + yr.toString() + ",";
                        validate = mnth.toString() + "_" + yr.toString();
                        mnth++;
                        if (mnth === 13) {
                            mnth = 1;
                            yr += 1;
                        }
                    }
                    else {
                        break;
                    }
                }
            }

        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
            var toMonth = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
            var toYear = ToMonYear[1];

            var url = '';
            if (ddMdINDEX != 0) {
                switch (ddMdINDEX) {
                    case 4:
                        url = "Visit_Details_ListedDr_Report.aspx";
                        break;
                    default:
                        url = "Visit_Details_Cat_Cls_Spclty_Report.aspx";
                        break;
                }
            }
            var randomnumber = Math.floor((Math.random() * 100) + 1);

            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s3.postimg.org/h5u7rfuvn/08_spinner.gif"
                $(popUpObj.document.body).append('<div><p style="color:blue;margin-top:10%;margin-left:40%;">Loading Please Wait....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:310px; height: 300px;position: fixed;top: 20%;left: 30%;"  alt="" /></div>');
            });
        }
    </script>

    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //   $('input:text:first').focus();
            $('input:text').bind("keydown", function (e) {
                var n = $("input:text").length;
                if (e.which == 13) { //Enter key
                    e.preventDefault(); //to skip default behavior of the enter key
                    var curIndex = $('input:text').index(this);
                    if ($('input:text')[curIndex].attributes['onfocus'].value != "this.style.backgroundColor='LavenderBlush'" && ($('input:text')[curIndex].value == '')) {
                        $('input:text')[curIndex].focus();
                    }
                    else {
                        var nextIndex = $('input:text').index(this) + 1;

                        if (nextIndex < n) {
                            e.preventDefault();
                            $('input:text')[nextIndex].focus();
                        }
                        else {
                            $('input:text')[nextIndex - 1].blur();
                            $('#btnSubmit').focus();
                        }
                    }
                }
            });
            $("input:text").on("keypress", function (e) {
                if (e.which === 32 && !this.value.length)
                    e.preventDefault();
            });

            $('#lnkExcel').click(function () {

                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                //var BaseLvlName = $('#<%=ddlBaseLvl.ClientID%> :selected').text();
                //if (BaseLvlName == "---Select---") { alert("Select Base Level Name."); $('#ddlBaseLvl').focus(); return false; }
                <%--var Year = $('#<%=ddlYear.ClientID%> :selected').text();
                if (Year == "---Select---") { alert("Select Year."); $('#ddlYear').focus(); return false; }
                var Month = $('#<%=ddlMonth.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Month."); $('#ddlMonth').focus(); return false; }--%>
                var Month = $('#<%=ddlMode.ClientID%> :selected').text();
                if (Month == "---Select---") { alert("Select Mode."); $('#ddlMode').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                //var mr_Code = document.getElementById('<%=ddlBaseLvl.ClientID%>').value;
                <%--var Year1 = document.getElementById('<%=ddlYearTo.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlMonth.ClientID%>').value;--%>
                var ddMdINDEX = $('#ddlMode').find(":selected").index();

                if (ddMdINDEX != 0) {
                    if (ddMdINDEX != 4) {
                        var iCount = 0;
                        var CHK = document.getElementById("<%=cbSpeciality.ClientID%>");
                        var checkbox = CHK.getElementsByTagName("input");
                        var label = CHK.getElementsByTagName("label");
                        for (var i = 0; i < checkbox.length; i++) {
                            if (checkbox[i].checked) {
                                iCount++;
                            }
                        }
                        if (iCount === 0) {
                            alert("Select valid Checkbox...");
                            return false;
                        }
                    }
                    <%--var frmYear = $('#<%=ddlYearTo.ClientID%> :selected').text();
                    var frmMonth = $('#ddlMonth').find(":selected").index();
                    var toYear = $('#<%=ddlYear.ClientID%> :selected').text();
                    var toMonth = $('#ddlMonthTo').find(":selected").index();--%>
                    var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                    var fromMon = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                    var fromYear = frmMonYear[1];

                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var ToMon = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var ToYear = ToMonYear[1];

                    var mnth = fromMon, yr = parseInt(fromYear), validate = '', tmp = '';
                    if ((fromMon <= ToMon && parseInt(fromYear) === parseInt(ToYear)) || (parseInt(fromYear) < parseInt(ToYear) && (fromMon <= ToMon || fromMon >= ToMon))) {
                        var div_code;
                        var sf_type = '<%=Session["sf_type"] %>';

                        if (sf_type == 1 || sf_type == 2) {
                            div_code = '<%=Session["div_code"] %>';
                        }
                        else {
                            div_code = '<%=Session["div_code"] %>';
                            if (div_code.includes(",")) {
                                div_code = div_code.substring(0, div_code.length - 1)
                            }
                        }
                        showModalPopUp_Excel(sf_Code, fromMon, fromYear, Name, ddMdINDEX, div_code);
                    }
                    else {
                        alert("Select Valid Month & Year...");
                        return false;
                    }
                }
            });
        });
    </script>
    <script type="text/javascript">
        /* $(document).ready(
        function(){
        $(":checkbox").click(countChecked)});
        
        function countChecked() {
        var n = $("input:checked").length;
        //alert(n);
        if (n == 1) {
        $(':checkbox:not(:checked)').prop('disabled', true);
        }
        else {
        $(':checkbox:not(:checked)').prop('disabled', false);
        }
        }*/

    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.on('keyup', function () {
                searchDdl($txt.val());
            });

            function searchDdl(item) {
                $ddl.empty();
                var exp = new RegExp(item, "i");
                var arr = $.grep($items,
                  function (n) {
                      return exp.test($(n).text());
                  });

                if (arr.length > 0) {
                    countItemsFound(arr.length);
                    $.each(arr, function () {
                        $ddl.append(this);
                        $ddl.get(0).selectedIndex = 0;
                    }
                  );
                }
                else {
                    countItemsFound(arr.length);
                    $ddl.append("<option>No Items Found</option>");
                }
            }

            function countItemsFound(num) {
                $("#para").empty();
                if ($txt.val().length) {
                    $("#para").html(num + " items found");
                }

            }
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });
        function showLoader(loaderType) {
            if (loaderType == "Search1") {
                document.getElementById("loaderSearchddlSFCode").style.display = '';
            }
        }
    </script>

    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Divid" runat="server">
            </div>
            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center" id="hHeading" runat="server"></h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblFilter" runat="server" Text="Field Force Name" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlFieldForce" runat="server" CssClass="custom-select2 nice-select" Width="100%" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:DropDownList ID="ddlSF" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="Label1" Text="Select Mode " CssClass="label" Visible="true" runat="server" />
                                <asp:DropDownList runat="server" Visible="true" ID="ddlMode" CssClass="nice-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged">
                                    <asp:ListItem Text="---Select---" Value="0" />
                                    <asp:ListItem Text="Category" Value="1" />
                                    <asp:ListItem Text="Speciality" Value="2" />
                                    <asp:ListItem Text="Class" Value="3" />
                                    <asp:ListItem Text="Listed Doctor" Value="4" />
                                    <asp:ListItem Text="Campaign" Value="5" />
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:DropDownList runat="server" Visible="false" ID="ddlBaseLvl" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <div class="row">
                                    <div class="col-lg-6">
                                        <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                        <%--     <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>--%>
                                        <input type="text" id="txtFromMonthYear" runat="server" class="nice-select" readonly="true" />
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                        <%--  <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>--%>
                                        <input type="text" id="txtToMonthYear" runat="server" class="nice-select" readonly="true" />
                                    </div>
                                </div>
                                <%--<div style="float: left; width: 45%">
                                    <asp:Label ID="lblMoth" runat="server" Text="From Month" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                                </div>
                                <div style="float: right; width: 45%">
                                    <asp:Label ID="Label2" runat="server" Text="From Year" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlYearTo" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>


                                <%--<div class="single-des clearfix">
                                <div style="float: left; width: 45%">
                                    <asp:Label ID="lblToYear" runat="server" Text="To Month" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlMonthTo" runat="server" CssClass="nice-select">
                                        <asp:ListItem Value="0" Text="---Select---"></asp:ListItem>
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
                                </div>
                                <div style="float: right; width: 45%">
                                    <asp:Label ID="Label3" runat="server" Text="To Year" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>--%>

                                <div>
                                    <asp:Label ID="lblMode" CssClass="label" runat="server" Text="Category" />
                                    <asp:CheckBoxList runat="server" ID="cbSpeciality" RepeatDirection="Horizontal" CellSpacing="10"
                                        RepeatColumns="7">
                                    </asp:CheckBoxList>

                                    <asp:HiddenField runat="server" ID="cbva" />
                                    <asp:HiddenField runat="server" ID="cbtx" />

                                </div>

                                <div>
                                    <asp:Label ID="lbldate" CssClass="label" runat="server" Text="Day" />
                                    <asp:CheckBoxList runat="server" ID="chkdate" RepeatColumns="7" RepeatDirection="Horizontal" CellSpacing="11">
                                    </asp:CheckBoxList>
                                    <%-- <asp:CheckBox ID="chkdate" runat="server" AutoPostBack="true" text="Day" />--%>
                                </div>

                                <div>
                                    <%--    <asp:Label ID="lblDt" runat="server" Font-Size="12px" ForeColor="Red" Font-Bold="true" Text="Datewise">
                        </asp:Label>--%>
                                    <asp:CheckBox ID="chkD" runat="server" AutoPostBack="true" Text="Datewise"
                                        OnCheckedChanged="chkD_CheckedChanged" />


                                </div>
                                <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />
                                    <asp:Button ID="btnSubmit" runat="server" Text="View" Enabled="false" CssClass="savebutton" />

                                    <br />
                                    <br />
                                    <asp:LinkButton ID="lnkExcel" runat="server" Text="Download Excel" OnClick="lnkExcel_click" Visible="false" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <iframe id="ifmRep" runat="server" style='border: 0; display: none;'></iframe>
            <%--  <script type="text/javascript">
        function openWin()
        {
            x=document.getElementById("<%=ddlFieldForce.ClientID%>");
            mn=document.getElementById("<%=ddlMonth.ClientID%>").value;
            yr = document.getElementById("<%=ddlYear.ClientID%>").value;
            sf=x.value;
            sfn=x.options[x.selectedIndex].text;
            window.open('rptCoverage_Analysis.aspx?sf_code='+sf+'&FMonth=' + mn +'&Fyear=' + Yr+ '&sf_name=' + sfn,null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');
        
        }
    </script>--%>
            <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.3/js/select2.full.min.js" type="text/javascript"></script>
            <!-- Bootstrap Datepicker -->
            <script type="text/javascript" src="../../assets/js/datepicker/jquery-1.8.3.min.js"></script>
            <script type="text/javascript" src="../../assets/js/datepicker/bootstrap.min.js"></script>
            <link href="../../assets/css/datepicker/jquery-1.8.3.min.js" rel="stylesheet" />
            <link href="../../assets/css/datepicker/bootstrap-datepicker.css" rel="stylesheet" />
            <script type="text/javascript" src="../../assets/js/datepicker/bootstrap-datepicker.js"></script>
            <script type="text/javascript">
                $(function () {
                    $('[id*=txtFromMonthYear]').datepicker({
                        changeMonth: true,
                        changeYear: true,
                        format: "M-yyyy",
                        viewMode: "months",
                        minViewMode: "months",
                        language: "tr"
                    });

                    $('[id*=txtToMonthYear]').datepicker({
                        changeMonth: true,
                        changeYear: true,
                        format: "M-yyyy",
                        viewMode: "months",
                        minViewMode: "months",
                        language: "tr"
                    });
                });
            </script>
    </form>
</body>
</html>

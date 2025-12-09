<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_View_Period.aspx.cs" Inherits="MasterFiles_AnalysisReports_Listeddr_View_Period" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listed Doctor Visit - Periodically</title>
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

        function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name, Cat_Name, Cat_Value) {

            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Listeddr_Period.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &cbVal=" + Cat_Value + "&cbTxt=" + Cat_Name + "&sf_name=" + sf_name,
"ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s18.postimg.org/84jvcfa3d/loading_18_ook.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:100px; height: 100px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
            // LoadModalDiv();
        }
        function showModalPopUp_MGR(sfcode, fmon, fyr, tyear, tmonth, sf_name, Cat_Name, Cat_Value) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Listeddr_Period_MGR.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + "&To_year=" + tyear + "&To_Month=" + tmonth + "&cbVal=" + Cat_Value + "&cbTxt=" + Cat_Name + "&sf_name=" + sf_name,
  "ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s12.postimg.org/bfrm3leql/load3.gif"


                $(popUpObj.document.body).append('<div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
            // LoadModalDiv();
        }
        function showModalPopUp_Deact(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Lstdr_Deact.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
  "ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s13.postimg.org/vw4gwxhaf/load5.gif"


                $(popUpObj.document.body).append('<div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        function showModalPopUp_Rem(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Lstdr_Remarks.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &sf_name=" + sf_name,
  "ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s4.postimg.org/4rkbyy91p/Preloader_9.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:100px; height: 100px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });

        }
        function showModalPopUp_Drwise(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_lstdrwise_Remarks.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &sf_name=" + sf_name,
  "ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s13.postimg.org/vw4gwxhaf/load5.gif"


                $(popUpObj.document.body).append('<div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        function showModalPopUp_Deact_MGR(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Listeddr_Incl_Deactivate_MGR.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &sf_name=" + sf_name,
  "ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s13.postimg.org/vw4gwxhaf/load5.gif"


                $(popUpObj.document.body).append('<div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        function showModalPopUp_CoreMap(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Listeddr_CoreMap.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
  "ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s4.postimg.org/4rkbyy91p/Preloader_9.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:100px; height: 100px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        function showModalPopUp_Camp(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Listeddr_Period_Camp.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
  "ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {
                var ImgSrc = "https://s4.postimg.org/4rkbyy91p/Preloader_9.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:100px; height: 100px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }


        function showModalPopUp_CoreMap_Period(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_CoreDr_Mgr_Period.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
  "ModalPopUp"//,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s12.postimg.org/bfrm3leql/load3.gif"


                $(popUpObj.document.body).append('<div class="preload"> <img src="' + ImgSrc + '"  style=" width:350px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }

    </script>
    <script type="text/javascript" src="../../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../../JsFiles/jquery-1.10.1.js"></script>
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

                var Name = $('#<%=ddlMR.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Base Level Name."); $('#ddlMR').focus(); return false; }

                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Clear---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); return false; }

                <%--var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }--%>

                <%--var TYear = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
                var TMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }--%>
                var Type = $('#<%=ddlmode.ClientID%> :selected').text();
                if (Type == "---Select---") { alert("Select Mode."); $('#ddlmode').focus(); return false; }
              
              
                var FieldForcecode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var Year1 = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;--%>

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = frmMonYear[1];

                var Month2 = "", Year2 = "";

                var sType = document.getElementById('<%=ddlmode.ClientID%>').value;
                
                if (sf_Code != -1 && sf_Code != 0 && Name != '') {
                    var sf_Code = document.getElementById('<%=ddlMR.ClientID%>').value;
                    if (sType == "0") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                       
                        if ($('#chkcat input:checked').length > 0) {  

                        var catname = "";
                        var Catvalue = "";

                        var CHK = document.getElementById("<%=chkcat.ClientID%>");
                        var checkbox = CHK.getElementsByTagName("input");
                        var label = CHK.getElementsByTagName("label");

                        for (var i = 0; i < checkbox.length; i++) {
                            if (checkbox[i].checked) {
                                catname += label[i].innerHTML + ",";
                            }
                        }

                        var checked_checkboxes = $("[id*=chkcat] input:checked");
                        checked_checkboxes.each(function () {

                            Catvalue += $(this).parent().attr('cbValue') + ",";
                        });
                        } else { alert('Select Category'); return false; }

                        showModalPopUp(sf_Code, Month1, Year1, Year2, Month2, Name, catname, Catvalue);
                    }
                    else if (sType == "1") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                Year2 = ToMonYear[1];
                if ($('#chkcat input:checked').length > 0) {

                    var catname = "";
                    var Catvalue = "";

                    var CHK = document.getElementById("<%=chkcat.ClientID%>");
                    var checkbox = CHK.getElementsByTagName("input");
                    var label = CHK.getElementsByTagName("label");

                    for (var i = 0; i < checkbox.length; i++) {
                        if (checkbox[i].checked) {
                            catname += label[i].innerHTML + ",";
                        }
                    }

                    var checked_checkboxes = $("[id*=chkcat] input:checked");
                    checked_checkboxes.each(function () {

                        Catvalue += $(this).parent().attr('cbValue') + ",";
                    });
                }
                else {
                    alert('Select Category'); return false;

                }
                        showModalPopUp_MGR(sf_Code, Month1, Year1, Year2, Month2, Name, catname, Catvalue);
                    }
                    else if (sType == "2") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_Deact(sf_Code, Month1, Year1, Year2, Month2, Name);
                    }
                    else if (sType == "3") {

                        showModalPopUp_Rem(sf_Code, Month1, Year1, Name);
                    }
                    else if (sType == "4") {
                        showModalPopUp_Drwise(sf_Code, Month1, Year1, Name);
                    }
                    else if (sType == "9") {
                        showModalPopUp_Deact_MGR(sf_Code, Month1, Year1, Name);
                    }
                    else if (sType == "5") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_CoreMap(sf_Code, Month1, Year1, Year2, Month2, Name);
                    }
                    else if (sType == "6") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_Camp(sf_Code, Month1, Year1, Year2, Month2, Name);
                    }
                    else if (sType == "8") {
                       <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_CoreMap_Period(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
                    }
}
else {
    var Month2 = "", Year2 = "";
    if (sType == "0" || sType == "1" || sType == "2" || sType == "5" || sType == "6" || sType == "8") {
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];
    }
    if (sType == "0") {
        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];
        if ($('#chkcat input:checked').length > 0) {
            var catname = "";
            var Catvalue = "";

            var CHK = document.getElementById("<%=chkcat.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");

            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    catname += label[i].innerHTML + ",";
                }
            }

            var checked_checkboxes = $("[id*=chkcat] input:checked");
            checked_checkboxes.each(function () {

                Catvalue += $(this).parent().attr('cbValue') + ",";
            });
        }
        else {
            alert('Select Category'); return false;

        }


                        showModalPopUp(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce, catname, Catvalue);
                    }
                    else if (sType == "1") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];
        if ($('#chkcat input:checked').length > 0) {

            var catname = "";
            var Catvalue = "";

            var CHK = document.getElementById("<%=chkcat.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");

            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    catname += label[i].innerHTML + ",";
                }
            }

            var checked_checkboxes = $("[id*=chkcat] input:checked");
            checked_checkboxes.each(function () {

                Catvalue += $(this).parent().attr('cbValue') + ",";
            });
        }
        else
        {
            alert('Select Category'); return false;
        }
                        showModalPopUp_MGR(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce, catname, Catvalue);
                    }
                    else if (sType == "2") {
        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];
        showModalPopUp_Deact(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
    }
    else if (sType == "3") {
        showModalPopUp_Rem(FieldForcecode, Month1, Year1, FieldForce);
    }
    else if (sType == "4") {
        showModalPopUp_Drwise(FieldForcecode, Month1, Year1, FieldForce);
    }
    else if (sType == "9") {
        showModalPopUp_Deact_MGR(FieldForcecode, Month1, Year1, FieldForce);
    }
    else if (sType == "5") {
                            <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_CoreMap(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
                    }
                    else if (sType == "6") {
                            <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];
        showModalPopUp_Camp(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
    }
    else if (sType == "8") {
                            <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;--%>
                            var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                            Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                                new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                            Year2 = ToMonYear[1];
                            showModalPopUp_CoreMap_Period(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
                        }

}
            });
        });
    </script>
   <%-- <script type="text/javascript">

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

                var Name = $('#<%=ddlMR.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Base Level Name."); $('#ddlMR').focus(); return false; }

                var FieldForce = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (FieldForce == "---Select Clear---") { alert("Select FieldForce Name."); $('#ddlFieldForce').focus(); return false; }

                <%--var FYear = $('#<%=ddlFrmYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFrmMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

                <%--var TYear = $('#<%=ddlToYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
                var TMonth = $('#<%=ddlToMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }
                var Type = $('#<%=ddlmode.ClientID%> :selected').text();
                if (Type == "---Select---") { alert("Select Mode."); $('#ddlmode').focus(); return false; }
              
                //if ($('#chkcat input:checked').length > 0) { return true; } else { alert('Select Category'); return false; }

                var FieldForcecode = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var Year1 = document.getElementById('<%=ddlFrmYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlFrmMonth.ClientID%>').value;

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = frmMonYear[1];

                var Month2 = "", Year2 = "";

                var sType = document.getElementById('<%=ddlmode.ClientID%>').value;
                //if ($('#chkcat input:checked').length > 0) { return true; } else { alert('Select Category'); return false; }
                if (sf_Code != -1 && sf_Code != 0 && Name != '') {
                    var sf_Code = document.getElementById('<%=ddlMR.ClientID%>').value;
                    if (sType == "0") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];


                        var catname = "";
                        var Catvalue = "";

                        var CHK = document.getElementById("<%=chkcat.ClientID%>");
                        var checkbox = CHK.getElementsByTagName("input");
                        var label = CHK.getElementsByTagName("label");

                        for (var i = 0; i < checkbox.length; i++) {
                            if (checkbox[i].checked) {
                                catname += label[i].innerHTML + ",";
                            }
                        }

                        var checked_checkboxes = $("[id*=chkcat] input:checked");
                        checked_checkboxes.each(function () {

                            Catvalue += $(this).parent().attr('cbValue') + ",";
                        });


                        showModalPopUp(sf_Code, Month1, Year1, Year2, Month2, Name, catname, Catvalue);
                    }
                    else if (sType == "1") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                Year2 = ToMonYear[1];

                var catname = "";
                var Catvalue = "";

                var CHK = document.getElementById("<%=chkcat.ClientID%>");
                        var checkbox = CHK.getElementsByTagName("input");
                        var label = CHK.getElementsByTagName("label");

                        for (var i = 0; i < checkbox.length; i++) {
                            if (checkbox[i].checked) {
                                catname += label[i].innerHTML + ",";
                            }
                        }

                        var checked_checkboxes = $("[id*=chkcat] input:checked");
                        checked_checkboxes.each(function () {

                            Catvalue += $(this).parent().attr('cbValue') + ",";
                        });

                        showModalPopUp_MGR(sf_Code, Month1, Year1, Year2, Month2, Name, catname, Catvalue);
                    }
                    else if (sType == "2") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_Deact(sf_Code, Month1, Year1, Year2, Month2, Name);
                    }
                    else if (sType == "3") {

                        showModalPopUp_Rem(sf_Code, Month1, Year1, Name);
                    }
                    else if (sType == "4") {
                        showModalPopUp_Drwise(sf_Code, Month1, Year1, Name);
                    }
                    else if (sType == "5") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_CoreMap(sf_Code, Month1, Year1, Year2, Month2, Name);
                    }
                    else if (sType == "6") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_Camp(sf_Code, Month1, Year1, Year2, Month2, Name);
                    }
                    else if (sType == "8") {
                       <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_CoreMap_Period(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
                    }
}
else {
    var Month2 = "", Year2 = "";
    if (sType == "0" || sType == "1" || sType == "2" || sType == "5" || sType == "6" || sType == "8") {
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];
    }
    if (sType == "0") {
        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];

        var catname = "";
        var Catvalue = "";

        var CHK = document.getElementById("<%=chkcat.ClientID%>");
                        var checkbox = CHK.getElementsByTagName("input");
                        var label = CHK.getElementsByTagName("label");

                        for (var i = 0; i < checkbox.length; i++) {
                            if (checkbox[i].checked) {
                                catname += label[i].innerHTML + ",";
                            }
                        }

                        var checked_checkboxes = $("[id*=chkcat] input:checked");
                        checked_checkboxes.each(function () {

                            Catvalue += $(this).parent().attr('cbValue') + ",";
                        });


                        showModalPopUp(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce, catname, Catvalue);
                    }
                    else if (sType == "1") {
                        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];
        var catname = "";
        var Catvalue = "";

        var CHK = document.getElementById("<%=chkcat.ClientID%>");
                        var checkbox = CHK.getElementsByTagName("input");
                        var label = CHK.getElementsByTagName("label");

                        for (var i = 0; i < checkbox.length; i++) {
                            if (checkbox[i].checked) {
                                catname += label[i].innerHTML + ",";
                            }
                        }

                        var checked_checkboxes = $("[id*=chkcat] input:checked");
                        checked_checkboxes.each(function () {

                            Catvalue += $(this).parent().attr('cbValue') + ",";
                        });
                        showModalPopUp_MGR(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce, catname, Catvalue);
                    }
                    else if (sType == "2") {
        <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];
        showModalPopUp_Deact(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
    }
    else if (sType == "3") {
        showModalPopUp_Rem(FieldForcecode, Month1, Year1, FieldForce);
    }
    else if (sType == "4") {
        showModalPopUp_Drwise(FieldForcecode, Month1, Year1, FieldForce);
    }
    else if (sType == "5") {
                            <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                        Year2 = ToMonYear[1];
                        showModalPopUp_CoreMap(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
                    }
                    else if (sType == "6") {
                            <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
        var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
        Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
            new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
        Year2 = ToMonYear[1];
        showModalPopUp_Camp(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
    }
    else if (sType == "8") {
                            <%--var Year2 = document.getElementById('<%=ddlToYear.ClientID%>').value;
                        var Month2 = document.getElementById('<%=ddlToMonth.ClientID%>').value;
                            var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                            Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                                new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                            Year2 = ToMonYear[1];
                            showModalPopUp_CoreMap_Period(FieldForcecode, Month1, Year1, Year2, Month2, FieldForce);
                        }

}
            });
        });
    </script>--%>

    <link href="../../assets/css/select2.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.6.3/css/bootstrap-select.min.css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(".custom-select2").select2();
            if ($('#SfType').val() == 1) {
                $("#ddlMR").next(".select2-container").hide();
                $('#lblMR').hide();
            }
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server">
        </div>
        <input id="SfType" type="hidden" value='<%= Session["sf_type"] %>' />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-5">
                    <h2 class="text-center" id="hHeading" runat="server"></h2>

                    <div class="designation-area clearfix">
                        <div class="single-des clearfix">
                            <asp:Label ID="Lblmain" Visible="False" runat="server" CssClass="label" Text="Doctorwise Periodically"></asp:Label>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblDivision" Visible="false" runat="server" CssClass="label" Text="Division "></asp:Label>
                            <asp:DropDownList ID="ddlDivision" runat="server" CssClass="nice-select"
                                OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblmode" runat="server" CssClass="label" Text="Mode"></asp:Label>
                            <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select"
                                OnSelectedIndexChanged="ddlmode_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="0">Visit - Based on Baselevel</asp:ListItem>
                                <asp:ListItem Value="1">Visit - Based on Baselevels/Managers</asp:ListItem>
                                <asp:ListItem Value="2">Visit - Based on Deactivate Drs</asp:ListItem>
                                <asp:ListItem Value="3">Visit (Daywise Remarks)</asp:ListItem>
                                <asp:ListItem Value="4">Visit (Listed Drwise Remarks)</asp:ListItem>
                                <asp:ListItem Value="5">Visit (Core Doctor Mapwise)</asp:ListItem>
                                <asp:ListItem Value="6">Visit (Campaignwise)</asp:ListItem>
                                <asp:ListItem Value="8">Visit (Core Doctor Periodically)</asp:ListItem>
                                 <asp:ListItem Value="9">Deactivate Drs(Incl. Mgr Visit)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblFF" runat="server" CssClass="label" Text="FieldForce Name"></asp:Label>
                            <asp:DropDownList ID="ddlFFType" runat="server" AutoPostBack="true" Visible="false"
                                OnSelectedIndexChanged="ddlFFType_SelectedIndexChanged">
                                <asp:ListItem Value="0" Text="Alphabetical"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Team" Selected="True"></asp:ListItem>

                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:DropDownList ID="ddlAlpha" runat="server" AutoPostBack="true" Visible="false"
                                OnSelectedIndexChanged="ddlAlpha_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix" style="margin-top: -20px;">
                            <asp:DropDownList ID="ddlFieldForce" runat="server" AutoPostBack="true" CssClass="custom-select2 nice-select" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged" Width="100%">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:DropDownList ID="ddlSF" runat="server" Visible="false">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <asp:Label ID="lblMR" runat="server" Text="Base Level" CssClass="label" Visible="false"></asp:Label>
                            <asp:DropDownList ID="ddlMR" runat="server" CssClass="custom-select2 nice-select" Visible="false" Width="100%">
                            </asp:DropDownList>
                        </div>
                        <div class="single-des clearfix">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                </div>
                                <%--              <div class="col-lg-6">
                                    <asp:Label ID="lblFrmMoth" runat="server" Text="From Month" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlFrmMonth" runat="server" CssClass="nice-select">
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
                                <div class="col-lg-6">
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="From Year"></asp:Label>
                                    <asp:DropDownList ID="ddlFrmYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>--%>
                            </div>
                        </div>
                        <%--<div class="single-des clearfix">
                            <div class="row">
                                <div class="col-lg-6">
                                    <asp:Label ID="lbltomon" runat="server" CssClass="label" Text="To Month"></asp:Label>
                                    <asp:DropDownList ID="ddlToMonth" runat="server" CssClass="nice-select">
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
                                <div class="col-lg-6">
                                    <asp:Label ID="lblToYear" runat="server" Text="To Year" CssClass="label"></asp:Label>
                                    <asp:DropDownList ID="ddlToYear" runat="server" CssClass="nice-select">
                                        <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>--%>

                        <div>
                            <asp:Label ID="lblcat" CssClass="label" runat="server" Text="Category" />
                            <asp:CheckBoxList ID="chkcat" CssClass="chkboxLocation" CellPadding="10" RepeatColumns="7" Font-Bold="true" RepeatDirection="vertical" runat="server">
                            </asp:CheckBoxList>


                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="savebutton" OnClick="btnSubmit_Click1" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" BorderWidth="1" GridLines="Both"
            Width="60%">
        </asp:Table>
        <br />
        <br />
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
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

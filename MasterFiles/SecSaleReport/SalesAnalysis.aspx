<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesAnalysis.aspx.cs" Inherits="MasterFiles_SecSaleReport_SalesAnalysis" %>

<%--<%@ Register Src="~/UserControl/MenuUserControl_TP.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_TP_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>--%>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%--<%@ Register Src="~/UserControl/MR_TP_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>--%>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu2" TagPrefix="ucl2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Analysis</title>
    <link type="text/css" rel="Stylesheet" href="../../css/rptMissCall.css" />
    <link type="text/css" rel="stylesheet" href="../../css/Grid.css" />
    <%--<link type="text/css" rel="stylesheet" href="../../css/style.css" />--%>
    <style type="text/css">
        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
    </style>
    <script type="text/javascript">
        var popUpObj;
        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp(sfcode, fmon, fyr, tyear, tmonth, sf_name, St_Name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Sales_Analysis.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + " &St_Name=" + St_Name,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        // LoadModalDiv();
        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp_1(sfcode, fmon, fyr, tyear, tmonth, sf_name, Stok_code, sk_Name, St_Name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Sale_Product.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + " &Stok_code=" + Stok_code + " &sk_Name=" + sk_Name + " &St_Name=" + St_Name,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp_HQ(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Sale_Hqwise.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp_Stk(sfcode, fmon, fyr, tyear, tmonth, sf_name, HQ) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_prod_stkwise.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + " &HQ=" + HQ,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp_Prod(sfcode, fmon, fyr, tyear, tmonth, sf_name, Prod_code, Prod_Name, state) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_HQ_Productwise.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + "&Prod_code=" + Prod_code + "&Prod_Name=" + Prod_Name + "&state=" + state,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp_Stk_ALL(sfcode, fmon, fyr, sf_name) {
            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_prod_stkwise_ALL.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &sf_name=" + sf_name,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }

        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp_HQ_Prod_Ver(sfcode, fmon, fyr, tyear, tmonth, sf_name) {
            var cbValue = "";
            var cbTxt = "";

            var CHK = document.getElementById("<%=chkProduct.ClientID%>");
            var checkbox = CHK.getElementsByTagName("input");
            var label = CHK.getElementsByTagName("label");
            for (var i = 0; i < checkbox.length; i++) {
                if (checkbox[i].checked) {
                    //alert("Selected = " + label[i].innerHTML);                    
                    cbTxt += label[i].innerHTML + ",";
                    //cbValue += label[i].innervalue + ",";
                    //alert(cbValue);
                }
            }

            var checked_checkboxes = $("[id*=chkProduct] input:checked");
            var message = "";
            checked_checkboxes.each(function () {
                //var value = $(this).parent().attr('cbValue');
                //var text = $(this).closest("td").find("label").html();
                //message += "Text: " + text + " Value: " + value;
                //message += "\n";
                cbValue += $(this).parent().attr('cbValue') + ",";
            });


            //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
            popUpObj = window.open("rpt_Hq_Product_Verticalwise.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + "&prod_code=" + cbValue + "&prod_name=" + cbTxt,
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
            $(popUpObj.document.body).ready(function () {

                var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


            });
        }
        var randomnumber = Math.floor((Math.random() * 100) + 1);
        function showModalPopUp_HQ_Multiple(sfcode, fmon, fyr, tyear, tmonth, sf_name, St_Name) {


            if (St_Name == "--ALL--") {
                popUpObj = window.open("rpt_Hq_Multiple_St_ALL.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + "&HQ_name=" + cbTxt + "&st_name=" + St_Name,
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
                $(popUpObj.document.body).ready(function () {

                    var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                    $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


                });
            }


            else {
                var cbValue = "";
                var cbTxt = "";

                var CHK = document.getElementById("<%=chkHQs.ClientID%>");
                var checkbox = CHK.getElementsByTagName("input");
                var label = CHK.getElementsByTagName("label");
                for (var i = 0; i < checkbox.length; i++) {
                    if (checkbox[i].checked) {
                        //alert("Selected = " + label[i].innerHTML);                    
                        cbTxt += label[i].innerHTML + ",";
                        //cbValue += label[i].innervalue + ",";
                        //alert(cbValue);
                    }
                }

                //popUpObj = window.open("MissedCallReport_Level1.aspx?sfcode=" + sfcode + "&cmon=" + cmon + "&cyr=" + cy,
                popUpObj = window.open("rpt_Hq_Multiple.aspx?sf_code=" + sfcode + "&Frm_Month=" + fmon + "&Frm_year=" + fyr + " &To_year=" + tyear + " &To_Month=" + tmonth + " &sf_name=" + sf_name + "&HQ_name=" + cbTxt,
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
                $(popUpObj.document.body).ready(function () {

                    var ImgSrc = "https://s17.postimg.org/his04fcbz/v00106.gif"


                    $(popUpObj.document.body).append('<div><p style="color:red;">Loading Please Wait ....</p></div><div class="preload"> <img src="' + ImgSrc + '"  style=" width:200px; height: 200px;position: fixed;top: 10%;left: 10%;"  alt="" /></div>');


                });
            }
        }

    </script>
    <style type="text/css">
        .height {
            height: 15px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
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

                var Name = $('#<%=ddlFieldForce.ClientID%> :selected').text();
                if (Name == "---Select---") { alert("Select Fieldforce Name."); $('#ddlFieldForce').focus(); return false; }
                <%--var FYear = $('#<%=ddlFYear.ClientID%> :selected').text();
                if (FYear == "---Select---") { alert("Select From Year."); $('#ddlFrmYear').focus(); return false; }
                var FMonth = $('#<%=ddlFMonth.ClientID%> :selected').text();
                if (FMonth == "---Select---") { alert("Select From Month."); $('#ddlFrmMonth').focus(); return false; }

                var TYear = $('#<%=ddlTYear.ClientID%> :selected').text();
                if (TYear == "---Select---") { alert("Select From Year."); $('#ddlToYear').focus(); return false; }
                var TMonth = $('#<%=ddlTMonth.ClientID%> :selected').text();
                if (TMonth == "---Select---") { alert("Select From Month."); $('#ddlToMonth').focus(); return false; }--%>
                var Type = $('#<%=ddltype.ClientID%> :selected').text();
                if (Type == "---Select---") { alert("Select Type."); $('#ddltype').focus(); return false; }
                var Stok = $('#<%=ddlStockiest.ClientID%> :selected').text();
                if (Stok == "---Select---") { alert("Select Stockiest."); $('#ddlStockiest').focus(); return false; }
                var State = $('#<%=ddlStateName.ClientID%> :selected').text();
                if (State == "---Select---") { alert("Select State."); $('#ddlStateName').focus(); return false; }

                var HQ = $('#<%=ddlHQ.ClientID%> :selected').text();
                if (HQ == "---Select---") { alert("Select HQ."); $('#ddlHQ').focus(); return false; }

                var sf_Code = document.getElementById('<%=ddlFieldForce.ClientID%>').value;
                <%--var Year1 = document.getElementById('<%=ddlFYear.ClientID%>').value;
                var Month1 = document.getElementById('<%=ddlFMonth.ClientID%>').value;--%>

                var frmMonYear = document.getElementById('<%=txtFromMonthYear.ClientID%>').value.split('-');
                var Month1 = new Date(frmMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(frmMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(frmMonYear[0] + '01, 0001').getMonth() + 1;
                var Year1 = frmMonYear[1];

                <%--var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                    new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                var Year2 = ToMonYear[1];--%>

                var sType = document.getElementById('<%=ddltype.ClientID%>').value;

                if (Month1 > Month2 && Year1 == Year2) {

                    alert("To Month must be greater than From Month"); return false;
                }
                else if (Year1 > Year2) {
                    alert("To Year must be greater than From Year"); return false;
                }

                if (sType == "1") {
                    <%--var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                    var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>
                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var Year2 = ToMonYear[1];
                    var St_Name = $('#<%=ddlStateName.ClientID%> :selected').text();
                    showModalPopUp(sf_Code, Month1, Year1, Year2, Month2, Name, St_Name);
                }

                else if (sType == "2") {
                    <%--var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                    var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>
                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var Year2 = ToMonYear[1];
                    var Stok_code = document.getElementById('<%=ddlStockiest.ClientID%>').value;
                    var St_Name = $('#<%=ddlStateName.ClientID%> :selected').text();
                    var stk_Name = $('#<%=ddlStockiest.ClientID%> :selected').text();

                    showModalPopUp_1(sf_Code, Month1, Year1, Year2, Month2, Name, Stok_code, stk_Name, St_Name);
                }
                else if (sType == "3") {
                    // showModalPopUp_HQ(sf_Code, Month1, Year1, Name);
                    <%--var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                    var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>
                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var Year2 = ToMonYear[1];
                    showModalPopUp_HQ_Prod_Ver(sf_Code, Month1, Year1, Year2, Month2, Name);
                }
                else if (sType == "4") {
                    <%--var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                    var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>
                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var Year2 = ToMonYear[1];
                    var HQ = $('#<%=ddlHQ.ClientID%> :selected').text();
                    showModalPopUp_Stk(sf_Code, Month1, Year1, Year2, Month2, Name, HQ);
                }
                else if (sType == "7") {
                    showModalPopUp_Stk_ALL(sf_Code, Month1, Year1, Name);
                }
                else if (sType == "6") {
                    <%--var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                    var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>
                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var Year2 = ToMonYear[1];
                    var St_Name = $('#<%=ddlStateName.ClientID%> :selected').text();
                    showModalPopUp_HQ_Multiple(sf_Code, Month1, Year1, Year2, Month2, Name, St_Name);
                }
                else {
                    <%--var Year2 = document.getElementById('<%=ddlTYear.ClientID%>').value;
                    var Month2 = document.getElementById('<%=ddlTMonth.ClientID%>').value;--%>
                    var ToMonYear = document.getElementById('<%=txtToMonthYear.ClientID%>').value.split('-');
                    var Month2 = new Date(ToMonYear[0] + '-1-01').getMonth() + 1 == "NaN" ? new Date(ToMonYear[0] + '-1-01').getMonth() + 1 :
                        new Date(ToMonYear[0] + '01, 0001').getMonth() + 1;
                    var Year2 = ToMonYear[1];
                    var Prod_code = document.getElementById('<%=ddlprod.ClientID%>').value;
                    var Prod_Name = $('#<%=ddlprod.ClientID%> :selected').text();
                    var state = $('#<%=ddlStateName.ClientID%> :selected').text();
                    showModalPopUp_Prod(sf_Code, Month1, Year1, Year2, Month2, Name, Prod_code, Prod_Name, state);
                }
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            var $txt = $('input[id$=txtNew]');
            var $ddl = $('select[id$=ddlFieldForce]');
            var $items = $('select[id$=ddlFieldForce] option');

            $txt.keyup(function () {
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
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            $("#testImg").hide();
            $('#linkcheck').click(function () {
                window.setTimeout(function () {
                    $("#testImg").show();
                }, 500);
            })
        });

    </script>
    <script type="text/javascript">


        function showLoader_Type(loaderType) {



            if (loaderType == "Search_Type") {
                document.getElementById("loaderSearch_Type").style.display = '';
                document.getElementById("SPl").style.display = '';

            }

        }
        function showLoader_St(loaderType) {



            if (loaderType == "Search_St") {
                document.getElementById("loaderSearch_St").style.display = '';
                document.getElementById("st").style.display = '';

            }

        }
        function showLoaderSNameTo(loaderType) {



            if (loaderType == "SearchTo") {
                document.getElementById("loaderSearchTo").style.display = '';
                document.getElementById("SNameTo").style.display = '';

            }

        }
        function showLoader_sf(loaderType) {



            if (loaderType == "Search_sf") {
                document.getElementById("loaderSearch_sf").style.display = '';
                document.getElementById("testImg").style.display = '';

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
        <%--        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>--%>
        <div>
            <div id="Divid" runat="server">
            </div>

            <div class="container home-section-main-body position-relative clearfix">
                <div class="row justify-content-center">
                    <div class="col-lg-5">
                        <h2 class="text-center">Sales Analysis</h2>

                        <div class="designation-area clearfix">
                            <div class="single-des clearfix">
                                <asp:Label ID="lblmode" runat="server" CssClass="label" Text="Mode"></asp:Label>
                                <asp:DropDownList ID="ddlmode" runat="server" CssClass="nice-select">
                                    <asp:ListItem Value="1" Text="Stockistwise"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lbltype" runat="server" CssClass="label" Text="Type"></asp:Label>
                                <asp:DropDownList ID="ddltype" runat="server" CssClass="nice-select" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddltype_SelectedIndexChanged" onchange="showLoader_Type('Search_Type')">
                                    <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Fieldforcewise"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Productwise"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Product HQ wise (Product - Horizontally)"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Product Stockiestwise"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="HQ Productwise"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Product HQ wise (Product - Vertically)"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Product Stockiestwise ALL"></asp:ListItem>
                                </asp:DropDownList>
                                <img src="../../Images/loading/loading19.gif" style="display: none;" id="loaderSearch_Type" />
                                <span id="SPl"
                                    style="font-size: 16px; color: Red; font-weight: bold; display: none; width: 200px">Please Wait....</span>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblFF" runat="server" CssClass="label" Text="Fieldforce Name"></asp:Label>

                                <asp:DropDownList ID="ddlFieldForce" runat="server" Width="100%" CssClass="custom-select2 nice-select" OnSelectedIndexChanged="ddlFieldForce_SelectedIndexChanged"
                                    AutoPostBack="true" onchange="showLoader_sf('Search_sf')">
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddlSF" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>

                                <img src="../../Images/loading/loading19.gif" style="display: none;" id="loaderSearch_sf" />
                                <span id="testImg"
                                    style="font-size: 16px; color: Red; font-weight: bold; display: none; width: 200px">Please Wait....</span>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblst" runat="server" Text="State Name" CssClass="label"></asp:Label>
                                <asp:DropDownList ID="ddlStateName" runat="server" CssClass="nice-select"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlStateName_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Value="0" Text="---Select---"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblHQ" runat="server" CssClass="label" Height="19px" Width="100px" Visible="false">HQ Name<span style="color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:DropDownList ID="ddlHQ" runat="server" Visible="false" CssClass="nice-select">
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblStockiest" runat="server" Text="Stockiest" CssClass="label" Visible="false"></asp:Label>
                                <%--     <asp:DropDownList ID="ddlStockiest" runat="server" SkinID="ddlRequired">
                        </asp:DropDownList>
                    </td>--%>
                                <%--   <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>--%>

                                <asp:DropDownList ID="ddlStockiest" runat="server" CssClass="nice-select" Visible="false">
                                </asp:DropDownList>

                                <%-- <td>
                                            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                <ProgressTemplate>
                                                    <img id="Img1" alt="" src="../../Images/loading/loading19.gif" runat="server" /><span
                                                        style="font-family: Verdana; color: Green; font-weight: bold">Please Wait....</span> </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>--%>
                            </div>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblprod" runat="server" CssClass="label" Text="Product Name" Visible="false"></asp:Label>
                                <asp:DropDownList ID="ddlprod" runat="server" CssClass="nice-select" Visible="false">
                                    <%--<asp:ListItem Value="0" Text ="All Product"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblFrmMoth" runat="server" Text="From Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtFromMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="lbltomon" runat="server" Text="To Month-Year" CssClass="label"></asp:Label>
                                    <asp:TextBox ID="txtToMonthYear" runat="server" CssClass="nice-select" ReadOnly="true"></asp:TextBox>
                                </div>
                                <%--                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblFMonth" runat="server" CssClass="label" Text="From Month"></asp:Label>
                                    <asp:DropDownList ID="ddlFMonth" runat="server" CssClass="nice-select">
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
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="lblFYear" runat="server" CssClass="label" Text="From Year" Width="60"></asp:Label>
                                    <asp:DropDownList ID="ddlFYear" runat="server" CssClass="nice-select" Width="60">
                                    </asp:DropDownList>
                                </div>--%>
                            </div>
                            <%--                            <div class="single-des clearfix">
                                <div style="float: left; width: 45%;">
                                    <asp:Label ID="lblTMonth" runat="server" CssClass="label" Text="To Month"></asp:Label>
                                    <asp:DropDownList ID="ddlTMonth" runat="server" CssClass="nice-select">
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
                                <div style="float: right; width: 45%;">
                                    <asp:Label ID="lblTYear" runat="server" CssClass="label" Text="To Year"></asp:Label>
                                    <asp:DropDownList ID="ddlTYear" runat="server" CssClass="nice-select">
                                    </asp:DropDownList>
                                </div>
                            </div>--%>

                            <div class="single-des clearfix">
                                <asp:Label ID="lblProduct" runat="server" CssClass="label" Text="Product Name" Visible="false"></asp:Label>
                                <asp:CheckBoxList ID="chkProduct" CssClass="chkboxLocation" CellPadding="10" RepeatColumns="2"
                                    Font-Size="11px" RepeatDirection="vertical" Visible="false"
                                    runat="server">
                                </asp:CheckBoxList>
                                <%--<asp:ListItem Value="0" Text ="All Product"></asp:ListItem>--%>
                            </div>
                            <%-- <div class="single-des clearfix">
                                <asp:Label ID="lblState" runat="server" SkinID="lblMand" Height="19px" Width="100px" Visible="false"><span style="color:Red">*</span>State</asp:Label>
                                <asp:DropDownList ID="ddlState" runat="server" Visible="false" SkinID="ddlRequired" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>--%>
                            <div class="single-des clearfix">
                                <asp:Label ID="lblTerritory" runat="server" Height="19px" Visible="false" Width="100px" CssClass="label">
                                HQ Name  <span style="color:Red;padding-left:2px;">*</span></asp:Label>
                                <asp:CheckBoxList ID="chkHQs" CssClass="chkboxLocation" Visible="false" CellPadding="10" RepeatColumns="2"
                                    Font-Size="11px" RepeatDirection="vertical"
                                    runat="server">
                                </asp:CheckBoxList>
                            </div>

                        </div>
                        <div class="w-100 designation-submit-button text-center clearfix">
                            <br />
                            <asp:Button ID="btnSubmit" runat="server" Text="View"
                                CssClass="savebutton" OnClick="btnGo_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="loading" align="center">
                Loading. Please wait.<br />
                <br />
                <img src="../../Images/loader.gif" alt="" />
            </div>
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

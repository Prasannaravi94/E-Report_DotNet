function setonfocus_1(x) {
    x.style.backgroundColor = '#E0EE9D'
}
function setofffocus_1(x) {
    x.style.backgroundColor = 'white'
}


function onlyAlphabets(event,maxlen,objval) {
    //allows only alphabets in a textbox
    if (event.type == "paste") {
        var clipboardData = event.clipboardData || window.clipboardData;
        var pastedData = clipboardData.getData('Text');
        var fndres = pastedData.includes("'");
        if (fndres == true || (parseInt(maxlen) < (parseInt(pastedData.length) + parseInt(objval.value.length)))) {
            if (fndres == true)
            {
                alert('Special character single quote not allowed')
                event.preventDefault();
                return false;
            }
            if (parseInt(maxlen) < (parseInt(pastedData.length) + parseInt(objval.value.length))) {
                alert('character maximum length must less than or equal to ' + maxlen)
                event.preventDefault();
                return false;
            }
        } else {
            if (isNaN(pastedData)) {
                return;

            } else {
                event.prevetDefault();
                }
        }
    }
    var charCode = event.which;
    if (!(charCode >= 65 && charCode <= 120) && (charCode != 32 && charCode != 0) && charCode != 8 && (event.keyCode >= 96 && event.keyCode <= 105)) {
        event.preventDefault();
    }
}

function onlyNumbers(event, maxlen) {
    if (event.type == "paste") {
        var clipboardData = event.clipboardData || window.clipboardData;
        var pastedData = clipboardData.getData('Text');

        if (parseInt(maxlen) < parseInt(pastedData.length)) {
            if (parseInt(maxlen) < parseInt(pastedData.length)) {
                alert('character maximum length must less than or equal to ' + maxlen)
                event.preventDefault();
                return false;
            }
        } else {
            if (isNaN(pastedData)) {
                event.preventDefault();

            } else {
                return;
            }
        }
    }
    var keyCode = event.keyCode || event.which;
    if (keyCode >= 96 && keyCode <= 105) {
        // Numpad keys
        keyCode -= 48;
    }
    var charValue = String.fromCharCode(keyCode);
    if (isNaN(parseInt(charValue)) && event.keyCode != 8 && event.keyCode != 46 && event.keyCode != 190 && event.keyCode != 37 && event.keyCode != 39 && event.keyCode != 9) {
        event.preventDefault();
    }
}

function isNumberKey(evt, element) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57) && !(charCode == 46 || charCode == 8))
        return false;
    else {
        var len = $(element).val().length;
        var index = $(element).val().indexOf('.');
        if (index > 0 && charCode == 46) {
            return false;
        }
        if (index > 0) {
            var CharAfterdot = (len + 1) - index;
            if (CharAfterdot > 3) {
                return false;
            }
        }

    }
    return true;
}




function CheckNumeric(e) {

    //alert(e.keyCode);

    if (window.event) // IE 
    {
        if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8 & e.keyCode != 13 & e.keyCode != 9) {
            event.returnValue = false;
            return false;
        }
    }
    else { // Fire Fox
        if ((e.which < 48 || e.which > 57) & e.which != 8 & e.which != 13 & e.keyCode != 9) {
            e.preventDefault();
            return false;
        }
    }

    var otherKeys = [186, 187, 188, 189, 190, 191, 192, 219, 220, 221, 222, 45];
    if (otherKeys.indexOf(e.keyCode) !== -1) {

        // allow minus sign
        if (e.keyCode === 45 && !e.shiftKey) {
            return true;
        }

        return false;
    }

    return true;

}
function fnAllowNumeric() {
    // Allow numeric characters and dashes

    if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8) {
        // If this is the first character and it is a dash, that's okay
        if (event.srcElement.value.length == 0 && event.keyCode == 189) {
            return true;
        }

        event.preventDefault();
        return false;
    }
}
function AlphaNumeric(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 32 || e.keyCode == 44 || e.keyCode == 95 || e.keyCode == 46 || e.keyCode == 45 || e.keyCode == 35 || e.keyCode == 36 || e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 8 || e.keyCode == 9)) {
            event.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!((e.which >= 48 && e.which <= 57) || (e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 32 || e.which == 44 || e.which == 95 || e.which == 46 || e.which == 45 || e.which == 35 || e.which == 36 || e.which == 37 || e.which == 38 || e.which == 8 || e.keyCode == 9)) {
            e.preventDefault();
            return false;

        }
    }
}


function CharactersOnly(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.which == 8 || e.keyCode == 9 || e.keyCode == 32 || e.keyCode == 95 || e.keyCode == 47)) {
            event.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!((e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 8 || e.keyCode == 9 || e.which == 32 || e.which == 95 || e.keyCode == 47)) {
            e.preventDefault();
            return false;
        }
    }
}

//Email

function ValidateEmail(mail) {
    if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(myForm.emailAddr.value)) {
        return (true)
    }
    alert("You have entered an invalid email address!")
    return (false)
}
function AlphaNumeric_enter(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 32 || e.keyCode == 44 || e.keyCode == 95 || e.keyCode == 46 || e.keyCode == 45 || e.keyCode == 35 || e.keyCode == 36 || e.keyCode == 37 || e.keyCode == 38 || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 13)) {
            event.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!((e.which >= 48 && e.which <= 57) || (e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 32 || e.which == 44 || e.which == 95 || e.which == 46 || e.which == 45 || e.which == 35 || e.which == 36 || e.which == 37 || e.which == 38 || e.which == 8 || e.keyCode == 9 || e.keyCode == 13)) {
            e.preventDefault();
            return false;

        }
    }
}
function Calendar_enter(e) {

    if (window.event) // IE 
    {
        if (!(e.keyCode == 8 || e.keyCode == 9)) {
            event.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!(e.which == 8 || e.keyCode == 9)) {
            e.preventDefault();
            return false;

        }
    }
}
function AlphaNumeric_NoSpecialChars(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 32 || e.keyCode == 8 || e.keyCode == 9)) {
            event.returnValue = false;
            return false;

        }
    }
    else { // Fire Fox
        if (!((e.which >= 48 && e.which <= 57) || (e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 32 || e.which == 8 || e.keyCode == 9)) {
            e.preventDefault();
            return false;

        }
    }
}

function AlphaNumeric_NoSpecialChars_New(e) {

    if (window.event) // IE 
    {
        if (!((e.keyCode >= 47 && e.keyCode <= 57) || (e.keyCode >= 97 && e.keyCode <= 122) || (e.keyCode >= 65 && e.keyCode <= 90) || e.keyCode == 32 || e.keyCode == 8 || e.keyCode == 9 || e.keyCode == 42 || e.keyCode != 43)) {
            event.returnValue = false;
            return false;

        }
        if (e.keyCode == 39 || e.keyCode == 0) {
            e.preventDefault();
            return false;
        }
        if (e.keyCode == 44 || e.keyCode == 0) {
            e.preventDefault();
            return false;
        }
    }
    else { // Fire Fox
        if (!((e.which >= 47 && e.which <= 57) || (e.which >= 97 && e.which <= 122) || (e.which >= 65 && e.which <= 90) || e.which == 32 || e.which == 8 || e.keyCode == 9 || e.keyCode == 42 || e.keyCode != 43)) {
            e.preventDefault();
            return false;

        }
        if (e.which == 39 || e.which == 0) {
            e.preventDefault();
            return false;
        }
        if (e.which == 44 || e.which == 0) {
            e.preventDefault();
            return false;
        }
    }
}



//date format

function dmyFormat(x) {var y, z, dd, mm, yy; if (x != "") { y = x.split('/'); dd = y[1]; mm = y[0]; yy = y[2]; if (dd < 10) dd = "0" + dd; if (mm < 10) mm = "0" + mm; z = dd + "/" + mm + "/" + yy; return z; }
}
var isNav4 = false, isNav5 = false, isIE4 = false
var strSeperator = "/";
var vDateType = 3;
var vYearType = 4;
var vYearLength = 2;
var err = 0;
//var gintCount	= 450

//function textCounter(field,gintCount,maxlimit)
function textCounter(field,maxlimit)
{
	if (field.value.length > maxlimit)
		{field.value = field.value.substring(0, maxlimit);}
    //else
	//	{gintCount = maxlimit - field.value.length;}
}

function DateFormat(vDateName, vDateValue, e, dateCheck) {
        vDateType = 3;
    var whichCode = (window.Event) ? e.which : e.keyCode;

if (vDateValue.length > 8 && isNav4)
{
	if ((vDateValue.indexOf("-") >= 1) || (vDateValue.indexOf("/") >= 1))
	return true;
}

var alphaCheck = " abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ/-";
if (alphaCheck.indexOf(vDateValue) >= 1)
 {
	if (isNav4)
	 {
        vDateName.value = "";
    vDateName.focus();
	vDateName.select();
	return false;
	}
	else
	{
        vDateName.value = vDateName.value.substr(0, (vDateValue.length - 1));
    return false;
	}
}

if (whichCode == 8)
return false;
else
{
	var strCheck = '47,48,49,50,51,52,53,54,55,56,57,58,59,95,96,97,98,99,100,101,102,103,104,105';
	if (strCheck.indexOf(whichCode) != -1)
	{
		if (isNav4)
		{
			if (((vDateValue.length < 6 && dateCheck) || (vDateValue.length == 7 && dateCheck)) && (vDateValue.length >=1))
			{
        alert("Invalid Date\nPlease Re-Enter");
    vDateName.value = "";
				vDateName.focus();
				vDateName.select();
				return false;
			 }
	         if (vDateValue.length == 6 && dateCheck)
	         {
				var mDay = vDateName.value.substr(2,2);
				var mMonth = vDateName.value.substr(0,2);
				var mYear = vDateName.value.substr(4,4)
				if (mYear.length == 2 && vYearType == 4)
				{
					var mToday = new Date();
					//If the year is greater than 30 years from now use 19, otherwise use 20
					var checkYear = mToday.getFullYear() + 30;
					var mCheckYear = '20' + mYear;
					if (mCheckYear >= checkYear)
					mYear = '19' + mYear;
					else
					mYear = '20' + mYear;
				}
				 var vDateValueCheck = mMonth+strSeperator+mDay+strSeperator+mYear;
			     if (!dateValid(vDateValueCheck))
			     {
        alert("Invalid Date\nPlease Re-Enter");
    vDateName.value = "";
					vDateName.focus();
					vDateName.select();
					return false;
				 }
			     return true;
			 }
			  else
			   {
				if (vDateValue.length >= 8  && dateCheck)
				{

					if (vDateType == 3)
						{
							var mMonth = vDateName.value.substr(2,2);
							var mDay = vDateName.value.substr(0,2);
							var mYear = vDateName.value.substr(4,4)
							vDateName.value = mDay+strSeperator+mMonth+strSeperator+mYear;
						}
					var vDateTypeTemp = vDateType;
					vDateType = 1;
					var vDateValueCheck = mMonth+strSeperator+mDay+strSeperator+mYear;
					if (!dateValid(vDateValueCheck))
					{
        alert("Invalid Date\nPlease Re-Enter");
    vDateType = vDateTypeTemp;
						vDateName.value = "";
						vDateName.focus();
						vDateName.select();
						return false;
					}
					vDateType = vDateTypeTemp;
					return true;
				  }
				else
				 {
					if (((vDateValue.length < 8 && dateCheck) || (vDateValue.length == 9 && dateCheck)) && (vDateValue.length >=1))
					{
        alert("Invalid Date\nPlease Re-Enter");
    vDateName.value = "";
						vDateName.focus();
						vDateName.select();
						return false;
					}
				  }
			  }
         }
		 else
		  {
			  if (((vDateValue.length < 8 && dateCheck) || (vDateValue.length == 9 && dateCheck)) && (vDateValue.length >=1))
			  {
        alert("Invalid Date\nPlease Re-Enter");
    vDateName.value = "";
				vDateName.focus();
				return true;
			   }
			  if (vDateValue.length >= 8 && dateCheck)
			  {

					if (vDateType == 3)
						{
							var mDay = vDateName.value.substr(0,2);
							var mMonth = vDateName.value.substr(3,2);
							var mYear = vDateName.value.substr(6,4)
						 }
					if (vYearLength == 4)
					{
						if (mYear.length < 4)
						{
        alert("Invalid Date\nPlease Re-Enter");
    vDateName.value = "";
							vDateName.focus();
							return true;
						  }
					 }
					var vDateTypeTemp = vDateType;
					vDateType = 1;
					var vDateValueCheck = mMonth+strSeperator+mDay+strSeperator+mYear;
					if (mYear.length == 2 && vYearType == 4 && dateCheck) {
					var mToday = new Date();
					var checkYear = mToday.getFullYear() + 30;
					var mCheckYear = '20' + mYear;
					if (mCheckYear >= checkYear)
						mYear = '19' + mYear;
					else
						mYear = '20' + mYear;
					vDateValueCheck = mMonth+strSeperator+mDay+strSeperator+mYear;
					if (vDateTypeTemp == 3)
						vDateName.value = mDay+strSeperator+mMonth+strSeperator+mYear;
	        }
			if (!dateValid(vDateValueCheck))
			{
        alert("Invalid Date\nPlease Re-Enter");
    vDateType = vDateTypeTemp;
				vDateName.value = "";
				vDateName.focus();
				return true;
			}
				vDateType = vDateTypeTemp;
				return true;
		}
		else
		 {
			if (vDateType == 3)
			{
				if (vDateValue.length == 2)
				{
        vDateName.value = vDateValue + strSeperator;
    }
				if (vDateValue.length == 5)
				{
        vDateName.value = vDateValue + strSeperator;
    }
		     }
			return true;
		  }
		}
		if (vDateValue.length == 10&& dateCheck)
		{
			if (!dateValid(vDateName))
			 {
        alert("Invalid Date\nPlease Re-Enter");
    vDateName.focus();
				vDateName.select();
			  }
		 }
	     return false;
	  }
	   else
	   {
			if (isNav4)
			{
        vDateName.value = "";
    vDateName.focus();
				vDateName.select();
				return false;
			  }
			  else
			  {
        vDateName.value = vDateName.value.substr(0, (vDateValue.length - 1));
    return false;
				}
		  }
       }
    }



function dateValid(objName)
{
var strDate;
var strDateArray;
var strDay;
var strMonth;
var strYear;
var intday;
var intMonth;
var intYear;
var booFound = false;
var datefield = objName;
var strSeparatorArray = new Array("-"," ","/",".");
var intElementNr;
var strMonthArray = new Array(12);
strMonthArray[0] = "Jan";
strMonthArray[1] = "Feb";
strMonthArray[2] = "Mar";
strMonthArray[3] = "Apr";
strMonthArray[4] = "May";
strMonthArray[5] = "Jun";
strMonthArray[6] = "Jul";
strMonthArray[7] = "Aug";
strMonthArray[8] = "Sep";
strMonthArray[9] = "Oct";
strMonthArray[10] = "Nov";
strMonthArray[11] = "Dec";
strDate = objName;
if (strDate.length < 1)
{
	return true;
}
for (intElementNr = 0; intElementNr < strSeparatorArray.length; intElementNr++)
{
	if (strDate.indexOf(strSeparatorArray[intElementNr]) != -1)
	{
        strDateArray = strDate.split(strSeparatorArray[intElementNr]);
    if (strDateArray.length != 3)
		{
        err = 1;
    return false;
		}
	else
	{
        strDay = strDateArray[0];
    strMonth = strDateArray[1];
		strYear = strDateArray[2];
	}
	booFound = true;
    }
}
if (booFound == false)
{
	if (strDate.length>5)
	{
        strDay = strDate.substr(0, 2);
    strMonth = strDate.substr(2, 2);
		strYear = strDate.substr(4);
     }
 }
if (strYear.length == 2)
{
        strYear = '20' + strYear;
    }
strTemp = strDay;
strDay = strMonth;
strMonth = strTemp;
intday = parseInt(strDay, 10);
if (isNaN(intday))
{
        err = 2;
    return false;
}
intMonth = parseInt(strMonth, 10);
if (isNaN(intMonth))
{
	for (i = 0;i<12;i++)
	{
		if (strMonth.toUpperCase() == strMonthArray[i].toUpperCase())
		{
        intMonth = i + 1;
    strMonth = strMonthArray[i];
			i = 12;
		}
	}
if (isNaN(intMonth))
{
        err = 3;
    return false;
}
}
intYear = parseInt(strYear, 10);
if (isNaN(intYear))
{
        err = 4;
    return false;
}
if (intMonth>12 || intMonth<1)
{
        err = 5;
    return false;
}
if ((intMonth == 1 || intMonth == 3 || intMonth == 5 || intMonth == 7 || intMonth == 8 || intMonth == 10 || intMonth == 12) && (intday > 31 || intday < 1))
{
        err = 6;
    return false;
}
if ((intMonth == 4 || intMonth == 6 || intMonth == 9 || intMonth == 11) && (intday > 30 || intday < 1))
{
        err = 7;
    return false;
}
if (intMonth == 2)
{
	if (intday < 1)
	{
        err = 8;
    return false;
	}
	if (LeapYear(intYear) == true)
	{
		if (intday > 29)
		{
        err = 9;
    return false;
		 }
	 }
else
{
	if (intday > 28)
	{
        err = 10;
    return false;
      }
}
}
return true;
}
function LeapYear(intYear)
{
	if (intYear % 100 == 0)
	{
		if (intYear % 400 == 0)
		{
			 return true;
		}
	}
	else
	{
		if ((intYear % 4) == 0)
		{ 
			return true;
		}
	}
	return false;
}


function fractionAdjust(num)
{
  var n = num;
  var strNum = ""+n;
  //alert(strNum);
  ind = strNum.indexOf('.');
  var fraction = strNum.substring(strNum.indexOf('.')+1,strNum.length);
  if(ind == -1)
  {
        strNum = strNum + ".00";
    return strNum;
  }
  else
  {
   if(fraction.length > 2)
   {
        frac_after_two = fraction.substring(2, fraction.length);
    roundvalue = Math.round(parseInt(frac_after_two))
    if(roundvalue >= 5)
    {
        upto_two_digitvalue = "." + fraction.substring(0, 2);
    corrected_fraction_value = parseFloat(upto_two_digitvalue);
      before_deci_point = strNum.substring(0,strNum.indexOf('.'))
      final_value = before_deci_point +corrected_fraction_value;
      return final_value;
    }
    else
    {
        upto_two_digitvalue = fraction.substring(0, 2);
    corrected_fraction_value = parseInt(upto_two_digitvalue) ;
      before_deci_point = strNum.substring(0,strNum.indexOf('.'))
     
      final_value = before_deci_point +"."+corrected_fraction_value;
      return final_value;
    }   
   }
   if(fraction.length <=2)
   {
        before_deci_point = strNum.substring(0, strNum.indexOf('.'))     
     final_value = before_deci_point+"."+fraction;
     return final_value;
   }
  }
}
//date format

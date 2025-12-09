using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections.Generic;
using Bus_EReport;
using System.Text;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// Summary description for PaySlip_WS
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class PaySlip_WS : System.Web.Services.WebService
{

    public PaySlip_WS()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }    

    //---------------Goddress Payslip-------------//
    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<PaySlip_ViewDet> Bind_Goddress_Det()
    {

        PaySlip_ViewDet objPay = new PaySlip_ViewDet();

        objPay.DivCode = HttpContext.Current.Session["div_code"].ToString();
        objPay.Sf_Code = HttpContext.Current.Session["SF_Code"].ToString();
        objPay.Month = HttpContext.Current.Session["Month"].ToString();
        objPay.Year = HttpContext.Current.Session["Year"].ToString();

        string DivCode = objPay.DivCode + ",";

        List<PaySlip_ViewDet> objPaySlipData = new List<PaySlip_ViewDet>();

        Product objPaySilp = new Product();
        DataSet dtPaySlip = new DataSet();

        dtPaySlip = objPaySilp.Get_Goddress_PaySlip(objPay.Sf_Code, DivCode, objPay.Month, objPay.Year, objPay.DivCode);

        foreach (DataRow dtRow in dtPaySlip.Tables[0].Rows)
        {
            PaySlip_ViewDet objPayDel = new PaySlip_ViewDet();

            objPayDel.Paymonth = dtRow["Paymonth"].ToString();
            objPayDel.Payyear = dtRow["Payyear"].ToString();
            objPayDel.SlNo = dtRow["S No"].ToString();
            objPayDel.Employee_Code = dtRow["Employee_Code"].ToString();
            objPayDel.EmployeeName = dtRow["Employee_Name"].ToString();
            objPayDel.HQ = dtRow["HQ"].ToString();
            objPayDel.DOJ = dtRow["DOJ"].ToString();
            objPayDel.Designation = dtRow["Designation"].ToString();
            objPayDel.Father_Name = dtRow["Father / Husband Name"].ToString();
            objPayDel.Dept = dtRow["Dept"].ToString();
            objPayDel.ESIC_No = dtRow["ESIC A/c No"].ToString();
            objPayDel.Bank_Name = dtRow["Bank Name"].ToString();
            objPayDel.Bank_No = dtRow["Bank A/c No"].ToString();
            objPayDel.PAN_No = dtRow["PAN_No"].ToString();
            objPayDel.PF_UAN_No = dtRow["PF UAN No"].ToString();
            objPayDel.Aadhar_Card_No = dtRow["Aadhar Card_No"].ToString();
            objPayDel.Basic = dtRow["Basic"].ToString();
            objPayDel.HRA = dtRow["HRA"].ToString();
            objPayDel.Conveyance = dtRow["Conveyance"].ToString();
            objPayDel.Medical_Allowance = dtRow["Medical Allowance"].ToString();
            objPayDel.LTA = dtRow["LTA"].ToString();
            objPayDel.Executive_Allowance = dtRow["Executive Allowance"].ToString();
            objPayDel.Statutory_Bonus_Advance = dtRow["Statutory Bonus Advance"].ToString();
            objPayDel.Washing_Allowance = dtRow["Washing Allowance"].ToString();
            objPayDel.Arrears = dtRow["Arrears"].ToString();
            objPayDel.PLIP = dtRow["PLIP"].ToString();
            objPayDel.Total_Earning = dtRow["Total Earning"].ToString();
            objPayDel.Advance_Against_Salary = dtRow["Advance Against Salary"].ToString();
            objPayDel.PF = dtRow["PF"].ToString();
            objPayDel.ESIC = dtRow["ESIC"].ToString();
            objPayDel.Professional_Tax = dtRow["Professional Tax"].ToString();
            objPayDel.Imprest_cash = dtRow["Imprest cash"].ToString();
            objPayDel.TDS = dtRow["TDS"].ToString();
            objPayDel.MLWF = dtRow["MLWF"].ToString();

            objPayDel.Total_Deduction = dtRow["Total Deduction"].ToString();
            objPayDel.Net_Salary = Convert.ToDouble(dtRow["Net Salary"].ToString());


            objPayDel.YTD = dtRow["YTD"].ToString();
            objPayDel.YTD2 = dtRow["YTD2"].ToString();
            objPayDel.YTD3 = dtRow["YTD3"].ToString();
            objPayDel.YTD4 = dtRow["YTD4"].ToString();
            objPayDel.YTD5 = dtRow["YTD5"].ToString();
            objPayDel.YTD6 = dtRow["YTD6"].ToString();
            objPayDel.YTD7 = dtRow["YTD7"].ToString();
            objPayDel.YTD8 = dtRow["YTD8"].ToString();
            objPayDel.YTD9 = dtRow["YTD9"].ToString();
            objPayDel.YTD10 = dtRow["YTD10"].ToString();
            objPayDel.YTD11 = dtRow["YTD11"].ToString();
            objPayDel.YTD12 = dtRow["YTD12"].ToString();
            objPayDel.YTD13 = dtRow["YTD13"].ToString();
            objPayDel.YTD14 = dtRow["YTD14"].ToString();
            objPayDel.YTD15 = dtRow["YTD15"].ToString();
            objPayDel.YTD16 = dtRow["YTD16"].ToString();
            objPayDel.YTD17 = dtRow["YTD17"].ToString();
            objPayDel.YTD18 = dtRow["YTD18"].ToString();
            objPayDel.YTD19 = dtRow["YTD19"].ToString();
            objPayDel.YTD20 = dtRow["YTD20"].ToString();


            //  int i = decimal.ToInt32(dtRow["Net Salary"].ToString());

            int monthNum = Convert.ToInt32(objPayDel.Paymonth);

            String month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthNum);
            objPayDel.MonthName = month;

            objPaySlipData.Add(objPayDel);

            // object x = objPayDel.Net_Salary;           
            //int a =Convert.ToInt32(x);

            // objPayDel.Rupees = ConvertNumbertoWords(a);

            objPayDel.Rupees = ConvertNumbertoWords(Convert.ToInt32(objPayDel.Net_Salary));

        }

        return objPaySlipData;
    }

    [WebMethod]
    [ScriptMethod]
    public string ConvertNumbertoWords(int number)
    {
        if (number == 0)
            return "ZERO";
        if (number < 0)
            return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";

        if ((number / 1000000000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000000000) + " Billion ";
            number %= 1000000000;
        }

        if ((number / 10000000) > 0)
        {
            words += ConvertNumbertoWords(number / 10000000) + " Crore ";
            number %= 10000000;
        }

        if ((number / 1000000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000000) + " MILLION ";
            number %= 1000000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
            number %= 100;
        }
        if (number > 0)
        {
            if (words != "")
                words += "AND ";
            var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }
        return words;
    }
}


public class PaySlip_ViewDet
{

    public string DivCode { get; set; }
    public string Sf_Code { get; set; }
    public string Month { get; set; }
    public string Year { get; set; }

    public string Paymonth { get; set; }
    public string Payyear { get; set; }
    public string SlNo { get; set; }
    public string Employee_Code { get; set; }
    public string EmployeeName { get; set; }
    public string HQ { get; set; }
    public string DOJ { get; set; }
    public string Designation { get; set; }
    public string Father_Name { get; set; }
    public string Dept { get; set; }
    public string ESIC_No { get; set; }

    public string Bank_Name { get; set; }
    public string Bank_No { get; set; }
    public string PAN_No { get; set; }
    public string PF_UAN_No { get; set; }
    public string Aadhar_Card_No { get; set; }

    public string Basic { get; set; }
    public string HRA { get; set; }
    public string Conveyance { get; set; }
    public string Medical_Allowance { get; set; }
    public string LTA { get; set; }
    public string Executive_Allowance { get; set; }
    public string Statutory_Bonus_Advance { get; set; }
    public string Washing_Allowance { get; set; }
    public string Arrears { get; set; }
    public string PLIP { get; set; }
    public string Total_Earning { get; set; }
    public string Advance_Against_Salary { get; set; }
    public string PF { get; set; }
    public string ESIC { get; set; }

    public string Professional_Tax { get; set; }
    public string Imprest_cash { get; set; }
    public string TDS { get; set; }
    public string MLWF { get; set; }
    public string Total_Deduction { get; set; }
    public double Net_Salary { get; set; }

    public string MonthName { get; set; }
    public string Rupees { get; set; }
    public string Net_Amount { get; set; }


    public string YTD { get; set; }
    public string YTD2 { get; set; }
    public string YTD3 { get; set; }
    public string YTD4 { get; set; }
    public string YTD5 { get; set; }
    public string YTD6 { get; set; }
    public string YTD7 { get; set; }
    public string YTD8 { get; set; }
    public string YTD9 { get; set; }
    public string YTD10 { get; set; }
    public string YTD11 { get; set; }
    public string YTD12 { get; set; }
    public string YTD13 { get; set; }
    public string YTD14 { get; set; }
    public string YTD15 { get; set; }
    public string YTD16 { get; set; }
    public string YTD17 { get; set; }
    public string YTD18 { get; set; }
    public string YTD19 { get; set; }
    public string YTD20 { get; set; }
}
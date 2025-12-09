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

public partial class MasterFiles_AnalysisReports_Frm_Tablets_PaySlipView : System.Web.UI.Page
{
    string SfCode = string.Empty;
    string Month = string.Empty;
    string Year = string.Empty;
    string Vacant = string.Empty;
    string Emp_Code = string.Empty;
    DataSet dsadm = new DataSet();
    string div_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Request.QueryString["div_code"].ToString();
        if (!IsPostBack)
        {

            // SfCode = HttpUtility.UrlDecode(Decrypt(Request.QueryString["SF_Code"]));

            SfCode = (Request.QueryString["SF_Code"]);
            Month = (Request.QueryString["Month"]);
            Year = (Request.QueryString["Year"]);
            Vacant = (Request.QueryString["Vacant"]);
            Emp_Code = (Request.QueryString["EMP_CODE"]);

            //Session["SF_Code"] = SfCode;
            Session["Month"] = Month;
            Session["Year"] = Year;
            FillPayslip();
        }
    }

    private void FillPayslip()
    {
        AdminSetup sf = new AdminSetup();
        if (Vacant == "on")
        {
            dsadm = sf.Gettablet_PaySlip_New(Emp_Code, Month, Year, div_code);
        }
        else
        {
            dsadm = sf.Gettablet_PaySlip(SfCode, Month, Year, div_code);
        }
        if (dsadm.Tables[0].Rows.Count > 0)
        {
            SalesForce sff = new SalesForce();
            string strFrmMonth = sff.getMonthName(Month);
            //string strToMonth = sf.getMonthName(TMonth);
            lblfor.Text = "SALARY SLIP FOR:  " + strFrmMonth + " - " + Year;
            int numDays = DateTime.DaysInMonth(Convert.ToInt32(Year), Convert.ToInt32(Month));
            lblmnth.Text=numDays.ToString();
            lblsf.Text = dsadm.Tables[0].Rows[0]["Employee_Name"].ToString();
            lbldes.Text = dsadm.Tables[0].Rows[0]["Designation_Name"].ToString();
            lbldep.Text = dsadm.Tables[0].Rows[0]["Department_Name"].ToString();
            lblemp.Text = dsadm.Tables[0].Rows[0]["Employee_Id"].ToString();
            lblpf.Text = dsadm.Tables[0].Rows[0]["Pf_Number"].ToString();
            lbldoj.Text = dsadm.Tables[0].Rows[0]["DATE_OF_JOINING"].ToString();
           lblhra.Text = dsadm.Tables[0].Rows[0]["House_Rent_Allowance"].ToString();
            lblpresent.Text = dsadm.Tables[0].Rows[0]["Present"].ToString();            
            lblloss.Text = dsadm.Tables[0].Rows[0]["Loss_of_Pay_Days"].ToString();
            lbluan.Text = dsadm.Tables[0].Rows[0]["UAN_No"].ToString();
            lblbasic.Text = dsadm.Tables[0].Rows[0]["Basic"].ToString();
            lblpfd.Text = dsadm.Tables[0].Rows[0]["Pf_Ded"].ToString();
            lblesi.Text = dsadm.Tables[0].Rows[0]["Esic_No"].ToString();
            lblgross.Text = dsadm.Tables[0].Rows[0]["Grosspay"].ToString();
            lbltotd.Text = dsadm.Tables[0].Rows[0]["Totalded"].ToString();
            lblpfbasic.Text = dsadm.Tables[0].Rows[0]["PF_Basic"].ToString();
            lbllta.Text = dsadm.Tables[0].Rows[0]["LTA"].ToString();
            lblloan.Text = dsadm.Tables[0].Rows[0]["Loans_if_Any"].ToString();
            lbltour.Text = dsadm.Tables[0].Rows[0]["Tour_Advance_Ded"].ToString();
            lblspecial.Text = dsadm.Tables[0].Rows[0]["Special_Allowance"].ToString();
            lblmetro.Text = dsadm.Tables[0].Rows[0]["Metro"].ToString();
            lblincome.Text = dsadm.Tables[0].Rows[0]["Incometaxdeduction"].ToString();
            lblprof.Text = dsadm.Tables[0].Rows[0]["Prof_Tax_Ded"].ToString();
            lblnets.Text = dsadm.Tables[0].Rows[0]["Netsalary"].ToString();
            lbloth.Text = dsadm.Tables[0].Rows[0]["Other_Dedections"].ToString();
            lblcity.Text = dsadm.Tables[0].Rows[0]["City_Comp_Allowance"].ToString();
            lbledu.Text = dsadm.Tables[0].Rows[0]["Education_Allowance"].ToString();
            lbltrav.Text = dsadm.Tables[0].Rows[0]["ta"].ToString();
            lbles.Text = dsadm.Tables[0].Rows[0]["Esi_Ded"].ToString();
            lblbank.Text = dsadm.Tables[0].Rows[0]["Bank_Name"].ToString();
            lblacc.Text = dsadm.Tables[0].Rows[0]["Bank_AcNo"].ToString();
            lblifs.Text = dsadm.Tables[0].Rows[0]["IFS_Code"].ToString();
            lbladv.Text = dsadm.Tables[0].Rows[0]["Adv_against_statutory_Bonus"].ToString();
        }
        else
        {
            MainDiv.Visible = false;
            tblRecord.Visible = true;
        }
    }


}


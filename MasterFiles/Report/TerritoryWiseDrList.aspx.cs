using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_Report_TerritoryWiseDrList : System.Web.UI.Page
{
    string SF_Code = string.Empty;
    string strTerritory_Name = string.Empty;
    string strDate = string.Empty;
    string strDivCode = string.Empty;
    string mon = string.Empty;
    string yr = string.Empty;
    string type = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            TourPlan tp = new TourPlan();
            DataSet ds = new DataSet();
            Distance_calculation_001 Exp = new Distance_calculation_001();

            strTerritory_Name = Request.QueryString["Terr"].ToString();
            SF_Code = Request.QueryString["sf_Code"].ToString();
            strDivCode = Request.QueryString["Div_Code"].ToString();
            strDate = Request.QueryString["Date"].ToString();
            type = Request.QueryString["type"].ToString();
            mon = Request.QueryString["mon"].ToString();
            yr = Request.QueryString["yr"].ToString();
            DataTable dt = Exp.getFieldForce_new(strDivCode, SF_Code);
            string sMonth = getMonthName(Convert.ToInt32(mon)) + " " + yr.ToString();
            lblHead.Text = lblHead.Text +" "+ sMonth ;

            lblFieldForcename.Text = ":" + dt.Rows[0]["sf_name"].ToString();
            lblhq1.Text = ":" + dt.Rows[0]["sf_hq"].ToString();
            lbldesignation.Text = ":" + dt.Rows[0]["sf_Designation_Short_Name"].ToString();
            lblterritoryname.Text = ":" + strTerritory_Name;
            lbltypename.Text = ":" + type;
            lbldate.Text = ":" + strDate;

            ds = tp.get_TerritoryWise_LstDts(strTerritory_Name, SF_Code, strDivCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdTP.DataSource = ds;
                grdTP.DataBind();
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    private string getMonthName(int iMonth)
    {
        string sReturn = string.Empty;

        if (iMonth == 1)
        {
            sReturn = "Jan";
        }
        else if (iMonth == 2)
        {
            sReturn = "Feb";
        }
        else if (iMonth == 3)
        {
            sReturn = "Mar";
        }
        else if (iMonth == 4)
        {
            sReturn = "Apr";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "June";
        }
        else if (iMonth == 7)
        {
            sReturn = "July";
        }
        else if (iMonth == 8)
        {
            sReturn = "Aug";
        }
        else if (iMonth == 9)
        {
            sReturn = "Sep";
        }
        else if (iMonth == 10)
        {
            sReturn = "Oct";
        }
        else if (iMonth == 11)
        {
            sReturn = "Nov";
        }
        else if (iMonth == 12)
        {
            sReturn = "Dec";
        }
        return sReturn;
    }

}
using Bus_EReport;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Reports_RptDcrRemarks_HalfDay : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string strFieledForceName = string.Empty;
    int Month = -1;
    int Year = -1;
    int Day = -1;
    DataSet dsSalesForce = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_code = Request.QueryString["sf_code"].ToString();
        strFieledForceName = Request.QueryString["sf_name"].ToString();
        
        Month = Convert.ToInt16(Request.QueryString["Month"].ToString());
        Year = Convert.ToInt16(Request.QueryString["Year"].ToString());
        Day = Convert.ToInt16(Request.QueryString["Day"].ToString());
        lblHead.Text = Day + "/" + Month + "/" + Year;
        
        
        //LblForceName.Text = "Field Force Name : " + strFieledForceName;
        
        //LblDate.Text = "Date : " + Day + "/" + Month  + "/" + Year;
        //LblCombined.Text = "Field Force Name: " + strFieledForceName + "<br /><br />Date: " + Day + "/" + Month + "/" + Year + "<br />";
        LblCombined.Text = "<strong><b>Field Force Name :</b></strong>" + strFieledForceName + "<br /><br /><strong><b>Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</b></strong>" + Day + "/" + Month + "/" + Year + "<br />";
        LoadRemarks();
    }
    private void LoadRemarks()
    {
        DCR dc = new DCR();
        DataSet dsDcr = new DataSet();
        dsDcr = dc.DcrRemarksHalfDay(sf_code, Day, Month, Year);
        if (dsDcr.Tables[0].Rows.Count > 0)
        {
            Remarks.Visible = true;
            Remarks.Text = "<br /><strong><b>Day Remarks&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</b></strong> " + dsDcr.Tables[0].Rows[0]["Remarks"].ToString();
        }
       
    }
}
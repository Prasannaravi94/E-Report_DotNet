using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_AnalysisReports_DCRDump_New : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsUserList = null;
    DataSet dsDoc = null;
    DataSet dsDCR = null;
    int product_total = 0;
    int FWD_total = 0;
    DateTime dtCurrent;
    DataSet dsProduct = null;
    DataSet dsCatg = null;
    int MonColspan = 0;
    DataSet dsTP = null;
    string tot_dr = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    int imissed_dr = -1;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    DataSet dsSf = null;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string strSf_Code = string.Empty;
    string Monthsub = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {

            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillYear();
            Filldays();
            if (Session["sf_type"].ToString() == "2")
            {
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                FillManagers();
            }
        }
    
        FillColor();
        setValueToChkBoxList();
        //if (Session["sf_type"].ToString() == "2")
        //{
        //}
        //else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        //{
        //    FillManagers();
        //}
        ddlFieldForce.Visible = true;
        btnCSV.Enabled = true;

    }

    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
            }
        }
    }

    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.UserListTP_Hierarchy(div_code, "admin");

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "des_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();

        }
        FillColor();
    }

    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillManagers();
        FillColor();
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        // FillSalesForce();
        string sURL = "rptDelayed_DCR_Status.aspx?cmon=" + ddlMonth.SelectedValue.ToString() + "&cyear=" + ddlYear.SelectedItem.Text.Trim() + "&SF_code=" + ddlFieldForce.SelectedValue;
        string newWin = "window.open('" + sURL + "',null,'resizable=yes,toolbar=no,scrollbars=yes,menubar=no,status=no,width=500,height=600,left=0,top=0');";
        ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
    }



    protected void linkcheck_Click(object sender, EventArgs e)
    {



    }

    private void Filldays()
    {
        int to_days = DateTime.DaysInMonth(Convert.ToInt16(ddlYear.SelectedValue), Convert.ToInt16(ddlMonth.SelectedValue));


        for (int i = 1; i <= to_days; i++)
        {

            chkdate.Items.Add("   " + i.ToString());

        }


    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkdate.Items.Clear();
        Filldays();
        setValueToChkBoxList();
    }

    private void setValueToChkBoxList()
    {
        try
        {

            foreach (ListItem item in chkdate.Items)
            {
                item.Attributes.Add("cbValue", item.Value);
            }

        }
        catch (Exception)
        {
        }
    }
    //private DataTable Getdt()
    //{
    //    string vacant = string.Empty;
    //    DataTable dt = new DataTable();
    //    if (chkWOVacant.Checked)
    //    {
    //        vacant = "1";
    //    }
    //    else
    //    {
    //        vacant = "0";
    //    }

    //    DataTable dtDays = new DataTable();
    //    dtDays.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //    dtDays.Columns["INX"].AutoIncrementSeed = 1;
    //    dtDays.Columns["INX"].AutoIncrementStep = 1;
    //    dtDays.Columns.Add("Dayss", typeof(string));

    //    string dd = hdnDate.Value.Trim();
    //    dd = hdnDate.Value.Remove(hdnDate.Value.Length - 1);

    //    string[] dayss = { dd };

    //    dayss = dd.Split(',');

    //    foreach (string d in dayss)
    //    {
    //        dtDays.Rows.Add(null, d.ToString());
    //    }
        
    //    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //    SqlConnection con = new SqlConnection(strConn);
    //    con.Open();
    //    //SqlCommand cmd = new SqlCommand("DCR_Dump_DetailNew", con);
    //    SqlCommand cmd = new SqlCommand("DCR_Dump_Detail_New2", con);
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.AddWithValue("@Div_Code", div_code);
    //    cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.Trim());
    //    cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
    //    cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
    //    cmd.Parameters.AddWithValue("@vacant", vacant);
    //    cmd.Parameters.AddWithValue("@dtDays", dtDays);
    //    cmd.CommandTimeout = 8000;
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);

    //    DataSet ds = new DataSet();
    //    SalesForce sf = new SalesForce();
    //    da.Fill(ds);
    //    ds.Tables[0].Columns.Remove("SNo");
    //    ds.Tables[0].Columns.Remove("main_sf_code");
    //    ds.Tables[0].Columns.Remove("clr");
    //    ds.Tables[0].Columns.Remove("trans_slno");
    //    ds.Tables[0].Columns.Remove("sf_code");
    //    ds.Tables[0].Columns.Remove("pob_value");
    //    ds.Tables[0].Columns.Remove("plan_name");
    //    dt = ds.Tables[0];

    //    con.Close();

    //    //DataTable dtClone = dt;
    //    return dt;
    //}
    protected void btnCSV_Click(object sender, EventArgs e)
    {

        //DataTable dt = Getdt();
        string vacant = string.Empty;
        DataTable dt = new DataTable();
        if (chkWOVacant.Checked)
        {
            vacant = "1";
        }
        else
        {
            vacant = "0";
        }

        DataTable dtDays = new DataTable();
        dtDays.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtDays.Columns["INX"].AutoIncrementSeed = 1;
        dtDays.Columns["INX"].AutoIncrementStep = 1;
        dtDays.Columns.Add("Dayss", typeof(string));

        
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        con.Open();
        SqlCommand cmd = new SqlCommand();
        //SqlCommand cmd = new SqlCommand("DCR_Dump_DetailNew", con);
        if (chkAllDate.Checked)
        {
            string dd = hdnDate.Value.Trim();
            dd = hdnDate.Value.Remove(hdnDate.Value.Length - 1);

            string[] dayss = { dd };

            dayss = dd.Split(',');

            foreach (string d in dayss)
            {
                dtDays.Rows.Add(null, d.ToString());
            }

            cmd = new SqlCommand("DCR_Dump_Detail_New2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@vacant", vacant);
            cmd.Parameters.AddWithValue("@dtDays", dtDays);
            cmd.CommandTimeout = 8000;

        }
        else
        {
            cmd = new SqlCommand("DCR_Dump_Detail_NewMonthwise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@vacant", vacant);
            //cmd.Parameters.AddWithValue("@dtDays", dtDays);
            cmd.CommandTimeout = 8000;

        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        //ds.Tables[0].Columns.Remove("SNo");
        //ds.Tables[0].Columns.Remove("main_sf_code");
        //ds.Tables[0].Columns.Remove("clr");
        //ds.Tables[0].Columns.Remove("trans_slno");
        //ds.Tables[0].Columns.Remove("sf_code");
        //ds.Tables[0].Columns.Remove("pob_value");
        //ds.Tables[0].Columns.Remove("plan_name");
        dt = ds.Tables[0];

        con.Close();

        //DataTable dtClone = dt;
  
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Call Report.csv");
        Response.Charset = "";
        string csv = string.Empty;

        foreach (DataColumn column in dt.Columns)
        {
            Response.Write(column.ColumnName + ',');
        }

        Response.Write("\r\n");
        int rowClone = -1;
        foreach (DataRow row in dt.Rows)
        {
            foreach (DataColumn column in dt.Columns)
            {
                Response.Write(row[column.ColumnName].ToString().Replace("\n", "").Replace(",", ";") + ',');
            }
            Response.Write("\r\n");
        }
        Response.ContentType = "application/text";
        Response.Flush();
        Response.End();
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        //DataTable dt = Getdt();
        string vacant = string.Empty;
        DataTable dt = new DataTable();
        if (chkWOVacant.Checked)
        {
            vacant = "1";
        }
        else
        {
            vacant = "0";
        }

        DataTable dtDays = new DataTable();
        dtDays.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtDays.Columns["INX"].AutoIncrementSeed = 1;
        dtDays.Columns["INX"].AutoIncrementStep = 1;
        dtDays.Columns.Add("Dayss", typeof(string));

        

        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        
        con.Open();
        SqlCommand cmd = new SqlCommand();
        if (chkAllDate.Checked)
        {
            string dd = hdnDate.Value.Trim();
            dd = hdnDate.Value.Remove(hdnDate.Value.Length - 1);

            string[] dayss = { dd };

            dayss = dd.Split(',');

            foreach (string d in dayss)
            {
                dtDays.Rows.Add(null, d.ToString());
            }
            cmd = new SqlCommand("DCR_Dump_Detail_New2", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@vacant", vacant);
            cmd.Parameters.AddWithValue("@dtDays", dtDays);
            cmd.CommandTimeout = 8000;
            
        }
        else {
            cmd = new SqlCommand("DCR_Dump_Detail_NewMonthwise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Div_Code", div_code);
            cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue.Trim());
            cmd.Parameters.AddWithValue("@cMnth", ddlMonth.SelectedValue);
            cmd.Parameters.AddWithValue("@cYr", ddlYear.SelectedValue);
            cmd.Parameters.AddWithValue("@vacant", vacant);
            //cmd.Parameters.AddWithValue("@dtDays", dtDays);
            cmd.CommandTimeout = 8000;
           
        }
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        SalesForce sf = new SalesForce();
        da.Fill(ds);
        //ds.Tables[0].Columns.Remove("SNo");
        //ds.Tables[0].Columns.Remove("main_sf_code");
        //ds.Tables[0].Columns.Remove("clr");
        //ds.Tables[0].Columns.Remove("trans_slno");
        //ds.Tables[0].Columns.Remove("sf_code");
        //ds.Tables[0].Columns.Remove("pob_value");
        //ds.Tables[0].Columns.Remove("plan_name");
        dt = ds.Tables[0];

        con.Close();
        
        string attachment = "attachment; filename=Call Report.xls";
        Response.Charset = "";
        Response.Clear();
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/text";
        string tab = "";
        Response.Write("<table width = '100%' border='1'>");
        Response.Write("<tr>");
        foreach (DataColumn dc in dt.Columns)
        {
            Response.Write("<th style='background-color:lightblue'>");
            Response.Write(tab + dc.ColumnName);
            Response.Write("</th>");
        }
        Response.Write("</tr>");
        int i;
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            Response.Write("<tr>");
            for (i = 0; i < dt.Columns.Count; i++)
            {
                Response.Write("<td>");
                Response.Write(tab + dr[i].ToString());
                Response.Write("</td>");
            }
            Response.Write("</tr>");
        }

        Response.Buffer = true;
        Response.End();
    }
    protected void chkAllDate_ChckedChanged(object sender, EventArgs e)
    {
        if (chkAllDate.Checked)
        {
            pnlDateWise.Visible = true;
        }
        else {
            pnlDateWise.Visible = false;
        }
    }
}
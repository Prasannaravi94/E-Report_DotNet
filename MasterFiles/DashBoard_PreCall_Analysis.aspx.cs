using Bus_EReport;
using DBase_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class MasterFiles_DashBoard_PreCall_Analysis : System.Web.UI.Page
{
    #region Declaration
    DataTable dtrowClr = new System.Data.DataTable();
    DataSet dsTP = null;
    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string cFMnth = string.Empty;
    string cFYear = string.Empty;
    string cTMnth = string.Empty;
    string cTYear = string.Empty;
    string sCurrentDate = string.Empty;
    int cfmonth;
    int cfyear;
    int ctmonth;
    int ctyear;
    List<int> iLstVstCnt = new List<int>();
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    string parameter = string.Empty;
    DataSet dsts = new DataSet();
    DataTable dtrowdt = new System.Data.DataTable();

    DataSet dsmgrsf = new DataSet();
    DataTable dtsf_code = new DataTable();
    DataSet dsprod = new DataSet();
    DataSet dschem = new DataSet();
    DataSet dsbus = new DataSet();
    string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    string drcode = string.Empty;
    string terrcode = string.Empty;
    #endregion
    #region Page_Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.DataBind();
        try
        {
            sf_code = Request.QueryString["SF"].ToString();
            div_code = Request.QueryString["Div_Code"].ToString();
            drcode = Request.QueryString["doc_id"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["doc_id"]))
            {
                drcode = Request.QueryString["doc_id"].ToString();
            }
            if (string.IsNullOrEmpty(Request.QueryString["cluster_code"]))
            {
                terrcode = Request.QueryString["cluster_code"].ToString();
            }
        }
        catch
        {
            div_code = Request.QueryString["div_Code"].ToString();
            sf_code = Request.QueryString["sfcode"].ToString();
            cFMnth = Request.QueryString["cMnth"].ToString();
            cFYear = Request.QueryString["cYr"].ToString();
            cTMnth = Request.QueryString["cMnth"].ToString();
            cTYear = Request.QueryString["cYr"].ToString();
            if (string.IsNullOrEmpty(Request.QueryString["doc_id"]))
            {
                drcode = Request.QueryString["doc_id"].ToString();
            }
            if (string.IsNullOrEmpty(Request.QueryString["cluster_code"]))
            {
                terrcode = Request.QueryString["cluster_code"].ToString();
            }
        }
        

        if (sf_code.Contains("MR"))
        {
            sf_type = "1";
        }
        else if (sf_code.Contains("MGR"))
        {
            sf_type = "2";
        }
        else
        {
            sf_type = "3";
        }

        if (!Page.IsPostBack)
        {
            if (sf_type == "1" || sf_type == "MR")
            {

                FillManagers();

                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
                GetWorkName();

                FillTerritory();
                FillDr();
            }
            else if (sf_type == "2" || sf_type == "MGR")
            {
                FillManagers();
                //  ddlFieldForce.SelectedValue = sf_code;
                GetWorkName();
                FillTerritory();
                FillDr();
                //ddlFieldForce.SelectedIndex = 2;
            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                FillManagers();
                ddlFieldForce.SelectedIndex = 2;
            }

            TourPlan tp = new TourPlan();
            dsTP = tp.Get_TP_Edit_Year(div_code);

            if (sf_type == "1" || sf_type == "MR")
            {
                FillReport();
            }
            else
            {
                FillReport();
            }
        }
        else
        {
            if (sf_type == "1" || sf_type == "MR")
            {

            }
            else if (sf_type == "2" || sf_type == "MGR")
            {

            }
            else
            {
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
            }
        }
    }
    #endregion

    private void FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        DataSet dsListedDR = new DataSet();
        dsListedDR = lstDR.FetchTerritory(ddlFieldForce.SelectedValue);
        ddlTerritory.DataTextField = "Territory_Name";
        ddlTerritory.DataValueField = "Territory_Code";
        ddlTerritory.DataSource = dsListedDR;
        ddlTerritory.DataBind();
        //if (ddlTerritory.Items.Count > 1)
        //{
        //    ddlTerritory.SelectedIndex = 1;
        //}
        if (Request.QueryString["cluster_code"] != "-1")
        {
            
            ddlTerritory.SelectedValue = Request.QueryString["cluster_code"];
            ddlTerritory.Enabled = false;
            // Proceed with docId
        }
      
    }
    private void GetWorkName()
    {

        Territory terr = new Territory();
        DataSet dsTerritory = new DataSet();
        dsTerritory = terr.getWorkAreaName(div_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {

            lblCluster.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString() + ":";
            DynamicLabelLiteral.Text = dsTerritory.Tables[0].Rows[0]["wrk_area_Name"].ToString();
        }
        else
        {
            lblCluster.Text = "Territory:";
        }
    }
    private void FillDr()
    {
        ListedDR lst = new ListedDR();
        DataSet dsLst = new DataSet();
        dsLst = lst.getListedDoctorforTerr(ddlFieldForce.SelectedValue, ddlTerritory.SelectedValue);
        if (dsLst.Tables[0].Rows.Count > 0)
        {
            ddlDr.DataTextField = "ListedDr_Name";
            ddlDr.DataValueField = "ListedDrCode";
            ddlDr.DataSource = dsLst;
            ddlDr.DataBind();
            ddlDr.Items.Insert(0, new ListItem("---Select---", "-1"));
            //if (ddlDr.Items.Count > 1)
            //{
            //    ddlDr.SelectedIndex = 1;
            //}
            if (Request.QueryString["doc_id"] != "-1")
            {

                ddlDr.SelectedValue = Request.QueryString["doc_id"];
                ddlDr.Enabled = false;
                // Proceed with docId
            }
        }
    }
    protected void ddlTerritory_SelectedIndexChanged(object sender, EventArgs e)
    {
        main.Visible = false;
        if (ddlTerritory.SelectedValue != "0")
        {

            FillDr();
        }
        else
        {
            ddlDr.Items.Clear();
        }
    }
    protected void ddlDr_SelectedIndexChanged(object sender, EventArgs e)
    {
        main.Visible = false;
    }
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        main.Visible = false;
        if (ddlFieldForce.SelectedValue != "0")
        {

            FillTerritory();
            if (ddlTerritory.SelectedValue != "0")
            {
                FillDr();
            }
        }
    }

    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        //dsSalesForce = sf.Hierarchy_Team(div_code, ddlFieldForce.SelectedValue);
        if (sf_type == "1" || sf_type == "MR")
        {
            dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        }
        else
        {
            dsSalesForce = sf.SalesForceListMgrGet_MRonly(div_code, sf_code);
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            for (int i = dsSalesForce.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = dsSalesForce.Tables[0].Rows[i];
                if (dr["sf_code"].ToString() == "admin")
                    dr.Delete();
            }

            dsSalesForce.AcceptChanges();

            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("---Select---", "-1"));
        }
    }
    #endregion

    #region btnGo_Click
    protected void btnGo_Click(object sender, EventArgs e)
    {
        string sfName = ddlFieldForce.SelectedItem.Text;
        DateTime visitDate = DateTime.Now;

        SalesForce sf = new SalesForce();


        int insertResult = sf.pcaadd(sf_code, div_code, sfName, visitDate);

        main.Visible = true;
        FillReport();
        // CreateDynamicTable();
        string htmlTable = GetVisit();
        lcvisit.Text = htmlTable;
        string htmlTable2 = GetSample();
        ltsample.Text = htmlTable2;
        string htmlTable3 = GetGift();
        ltgift.Text = htmlTable3;
        string htmlTable4 = GetFeed();
        ltfeed.Text = htmlTable4;
        string htmlTable5 = GetProduct_Visit();
        ltpvist.Text = htmlTable5;
        string htmlTable6 = GetChemist_Visit();
        ltsChem.Text = htmlTable6;
        string htmlTable7 = GetBusiness_Visit();
        ltbus.Text = htmlTable7;
        GetLastvisit();
        string htmlTable8 = GetJointVisit();
        ltjoint.Text = htmlTable8;
        string htmlTable9 = GetEvent();
        ltevent.Text = htmlTable9;
        string htmlTable10 = GetRX_Det();
        ltrx.Text = htmlTable10;
        string htmlTable11 = GetCRM();
        ltCRM.Text = htmlTable11;
        string htmlTable12 = GetRCPA();
        ltRCPA.Text = htmlTable12;
        // FillVisit();
    }
    #endregion

    #region FillReport
    private void FillReport()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        SqlConnection con = new SqlConnection(strConn);
        //string[] msg = ddlFieldForce.SelectedItem.Text.Split('-');


        ListedDR lstdr = new ListedDR();
        DataSet dsDoc = new DataSet();
        if (ddlDr.SelectedValue != "")
        {
            dsDoc = lstdr.ViewListedDr_Precall(ddlDr.SelectedValue);
            if (dsDoc.Tables[0].Rows.Count > 0)
            {
                tblMsgInfo.Visible = true;
                lbladdr.Text = dsDoc.Tables[0].Rows[0]["ListedDr_Address1"].ToString();
                lblmob.Text = dsDoc.Tables[0].Rows[0]["ListedDr_Mobile"].ToString();
                lbldob.Text = dsDoc.Tables[0].Rows[0]["ListedDr_DOB"].ToString();
                if (lbldob.Text == "01 Jan 1900")
                {
                    lbldob.Text = "";
                }
                lbldow.Text = dsDoc.Tables[0].Rows[0]["ListedDr_DOW"].ToString();
                if (lbldow.Text == "01 Jan 1900")
                {
                    lbldow.Text = "";
                }
                lblemail.Text = dsDoc.Tables[0].Rows[0]["ListedDr_Email"].ToString();
                lblhosp.Text = dsDoc.Tables[0].Rows[0]["ListedDr_Hospital"].ToString() +"<br>"+ dsDoc.Tables[0].Rows[0]["Hospital_Address"].ToString();
              
                lblspec.Text = dsDoc.Tables[0].Rows[0]["Doc_Special_Name"].ToString();
                lblqua.Text = dsDoc.Tables[0].Rows[0]["Doc_QuaName"].ToString();
                lblcat.Text = dsDoc.Tables[0].Rows[0]["Doc_Cat_Name"].ToString();
                lblcls.Text = dsDoc.Tables[0].Rows[0]["Doc_ClsName"].ToString();
                lblpin.Text = dsDoc.Tables[0].Rows[0]["ListedDr_Pin"].ToString();
                lblreg.Text = dsDoc.Tables[0].Rows[0]["ListedDr_RegNo"].ToString();
                //lblFFmsg.Text = "<b style='color:#245884'>FieldForce:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_name"];
                //lblhq.Text = "<b style='color:#245884'>HQ:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_hq"];
                //lblDesign.Text = "<b style='color:#245884'>Designation:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_Designation_Short_Name"];
                //lblDOJ.Text = "<b style='color:#245884'>DOJ:</b> " + dsSalesForce.Tables[0].Rows[0]["Sf_Joining_Date"];
                //lblEmpCode.Text = "<b style='color:#245884'>Emp Code:</b>  " + dsSalesForce.Tables[0].Rows[0]["sf_emp_id"];
                DataSet dsvisit = new DataSet();
                dsvisit = lstdr.getPrevious_Visit_Miss(ddlFieldForce.SelectedValue, ddlDr.SelectedValue);
                if (dsvisit.Tables[0].Rows.Count > 0)
                {
                    lblvisitD.Text = dsvisit.Tables[0].Rows[0][0].ToString();
                }
                lblcamp.Text = dsDoc.Tables[0].Rows[0]["Doc_SubCatName"].ToString();

                if (dsDoc.Tables[0].Rows[0]["Core"].ToString() == "NULL" || dsDoc.Tables[0].Rows[0]["Core"].ToString() == "")
                {
                    lblcore.Text = "Not Tagged";
                }
                else
                {
                    lblcore.Text = "Tagged";
                }

                lblcrm.Text = dsDoc.Tables[0].Rows[0]["Crm"].ToString();
            }
            else
            { tblMsgInfo.Visible = false; }
        }

    }
    #endregion
    // private void CreateDynamicTable()
    // {
    //     var lastSixMonths = Enumerable
    //.Range(0, 3)
    //.Select(i => DateTime.Now.AddMonths(i - 3 + 1))
    //.Select(date => date.ToString("MM/yyyy"));
    //     // FillReport();
    //     string[] sck;
    //     sck = lastSixMonths.ToArray();
    //     var last = sck.Last().Split('/');
    //     var first = sck.First().Split('/');
    //     int months = (Convert.ToInt32(last[1]) - Convert.ToInt32(first[1])) * 12 + Convert.ToInt32(last[0]) - Convert.ToInt32(first[0]); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
    //     int cmonth = Convert.ToInt32(first[0]);
    //     int cyear = Convert.ToInt32(first[1]);

    //     int iMn = 0, iYr = 0;
    //     DataTable dtMnYr = new DataTable();
    //     dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //     dtMnYr.Columns.Add("MNTH", typeof(int));
    //     dtMnYr.Columns.Add("YR", typeof(int));
    //     //
    //     while (months >= 0)
    //     {
    //         if (cmonth == 13)
    //         {
    //             cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
    //         }
    //         else
    //         {
    //             iMn = cmonth; iYr = cyear;
    //         }
    //         dtMnYr.Rows.Add(null, iMn, iYr);
    //         months--; cmonth++;
    //     }
    //     int j = 0;
    //     SalesForce sf1 = new SalesForce();
    //     DataTable SfCodes = sf1.getMRJointWork_camp(div_code, sf_code, 0);
    //     dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
    //     dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
    //     dtsf_code.Columns["INX"].AutoIncrementStep = 1;
    //     dtsf_code.Columns.Add("sf_code");
    //     for (int i = 0; i < SfCodes.Rows.Count; i++)
    //     {
    //         //j += 1;
    //         //dtsf_code.Rows.Add(j.ToString());

    //         dtsf_code.Rows.Add(null, SfCodes.Rows[i]["sf_code"]);
    //     }
    //     dsmgrsf.Tables.Add(SfCodes);
    //     //
    //     SalesForce sf = new SalesForce();
    //     DCR dcc = new DCR();
    //     DB_EReporting db = new DB_EReporting();
    //     string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
    //     SqlConnection con = new SqlConnection(strConn);
    //     string sProc_Name = "";

    //     sProc_Name = "Single_Doctor_Visit";

    //     SqlCommand cmd = new SqlCommand(sProc_Name, con);
    //     cmd.CommandType = CommandType.StoredProcedure;
    //     cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
    //     cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
    //     cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
    //     cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code);
    //     cmd.Parameters.AddWithValue("@dr_code", ddlDr.SelectedValue);
    //     cmd.CommandTimeout = 800;
    //     SqlDataAdapter da = new SqlDataAdapter(cmd);
    //     DataSet dsts = new DataSet();
    //     da.Fill(dsts);
    //     dtrowClr = dsts.Tables[0].Copy();
    //     dtrowdt = dsts.Tables[1].Copy();

    //     /*
    //     result = dsts.Tables[1].AsEnumerable()
    //    .GroupBy(row => row.Field<string>("VST"))
    //    .Select(g => g.CopyToDataTable()).ToList();
    //     */
    //     dsts.Tables[0].Columns.RemoveAt(8);
    //     //dsts.Tables[0].Columns.RemoveAt(5);
    //     //dsts.Tables[0].Columns.RemoveAt(1);
    //     dsts.Tables[1].Columns.RemoveAt(0);
    //     if (dsts.Tables[1].Rows.Count > 1)
    //     {
    //         dsts.Tables[1].Rows.RemoveAt(1);
    //     }
    //     else
    //     {
    //         dsts.Tables[1].Rows.RemoveAt(0);
    //     }
    //     GrdFixation.DataSource = dsts.Tables[1];
    //     GrdFixation.DataBind();
    // }
    private void FillReportMGR()
    {
        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        DB_EReporting db = new DB_EReporting();
        SqlConnection con = new SqlConnection(strConn);
        string[] msg = ddlFieldForce.SelectedItem.Text.Split('-');

        dsSalesForce = sf.getSFCodeInfo(ddlFieldForce.SelectedValue, div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            tblMsgInfo.Visible = true;
            //lblFFmsg.Text = "<b style='color:#245884'>FieldForce:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_name"];
            //lblhq.Text = "<b style='color:#245884'>HQ:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_hq"];
            //lblDesign.Text = "<b style='color:#245884'>Designation:</b> " + dsSalesForce.Tables[0].Rows[0]["sf_Designation_Short_Name"];
            //lblDOJ.Text = "<b style='color:#245884'>DOJ:</b> " + dsSalesForce.Tables[0].Rows[0]["Sf_Joining_Date"];
            //lblEmpCode.Text = "<b style='color:#245884'>Emp Code:</b>  " + dsSalesForce.Tables[0].Rows[0]["sf_emp_id"];
        }
        else
        { tblMsgInfo.Visible = false; }

        //string sProc_Name = "Sp_Time_StatusDaywiseMGR";
        //SqlCommand cmd = new SqlCommand(sProc_Name, con);
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        //cmd.Parameters.AddWithValue("@FMonth", ddlMonth.SelectedValue);
        //cmd.Parameters.AddWithValue("@FYear", ddlYear.SelectedValue);
        //cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        //cmd.Parameters.AddWithValue("@Days", ddlDay.SelectedValue);

        //cmd.CommandTimeout = 600;
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //da.Fill(dsts);
        //dtrowClr = dsts.Tables[0].Copy();
        //DataSet dsWorkType = sf.getSFCodeWorkType(ddlFieldForce.SelectedValue, div_code, ddlMonth.SelectedValue, ddlYear.SelectedValue, ddlDay.SelectedValue);
        //if (dsWorkType.Tables[0].Rows.Count > 0)
        //{
        //    lblWorkType.Text = "<b style='color:#245884'>Work Type:</b>" + dsWorkType.Tables[0].Rows[0]["WorkType_Name"];
        //}
        //else { lblWorkType.Text = "<b style='color:#245884'>Work Type:</b> -"; }
        //GrdTimeSt.DataSource = dtrowClr;
        //GrdTimeSt.DataBind();
    }
    private string GetVisit()

    {
        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        // int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        DataTable SfCodes = sf1.getMRJointWork_camp(div_code, ddlFieldForce.SelectedValue, 0);
        DataTable dtsf_code = new DataTable();
        dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
        dtsf_code.Columns["INX"].AutoIncrementStep = 1;
        dtsf_code.Columns.Add("sf_code");
        dtsf_code.Columns.Add("sf_Designation_Short_Name");
        for (int i = 0; i < SfCodes.Rows.Count && i < 4; i++)
        {
            //j += 1;
            //dtsf_code.Rows.Add(j.ToString());

            dtsf_code.Rows.Add(null, SfCodes.Rows[i]["sf_code"], SfCodes.Rows[i]["sf_Designation_Short_Name"]);
        }

        DataSet dsmgrsf = new DataSet();
        dsmgrsf.Tables.Add(SfCodes);   //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Visit_Details";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code);
        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Dynamically build the header
            html.Append("<thead><tr>");
            html.Append("<th>Desig.</th>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            html.Append("<tbody>");

            // Loop through the rows and build the table rows
            string currentDesig = string.Empty;
            Dictionary<string, StringBuilder> monthData = uniqueMonths.ToDictionary(m => m, m => new StringBuilder());

            foreach (DataRow row in dtrowClr.Rows)
            {
                string desig = row["desig"].ToString();
                string month = row["MonthYear"].ToString();
                string dtValues = row["dt"].ToString();

                // When the designation changes, create a new row
                if (currentDesig != desig)
                {
                    // If it's not the first row, append the previous data
                    if (!string.IsNullOrEmpty(currentDesig))
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + currentDesig + "</td>");
                        foreach (var mon in uniqueMonths)
                        {
                            html.Append("<td>" + monthData[mon].ToString() + "</td>");
                        }
                        html.Append("</tr>");
                    }

                    // Reset month data
                    foreach (var key in monthData.Keys)
                    {
                        monthData[key].Clear();
                    }

                    currentDesig = desig;
                }

                // Add the date to the appropriate month column
                if (monthData.ContainsKey(month))
                {
                    monthData[month].Append(dtValues);
                }

            }

            // Append the last row
            html.Append("<tr>");
            html.Append("<td>" + currentDesig + "</td>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month].ToString() + "</td>");
            }
            html.Append("</tr>");

            // Close table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetSample()
    {
        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Sample_Detail";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);

        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Dynamically build the header
            html.Append("<thead><tr>");
            html.Append("<th>Product</th>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            html.Append("<tbody>");

            // Loop through the rows and build the table rows
            string currentDesig = string.Empty;
            Dictionary<string, StringBuilder> monthData = uniqueMonths.ToDictionary(m => m, m => new StringBuilder());

            foreach (DataRow row in dtrowClr.Rows)
            {
                string mode = row["mode"].ToString();
                string month = row["MonthYear"].ToString();
                string dtValues = row["name"].ToString();

                // When the designation changes, create a new row
                if (currentDesig != mode)
                {
                    // If it's not the first row, append the previous data
                    if (!string.IsNullOrEmpty(currentDesig))
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + currentDesig + "</td>");
                        foreach (var mon in uniqueMonths)
                        {
                            html.Append("<td>" + monthData[mon].ToString() + "</td>");
                        }
                        html.Append("</tr>");
                    }

                    // Reset month data
                    foreach (var key in monthData.Keys)
                    {
                        monthData[key].Clear();

                    }

                    currentDesig = mode;
                }
                var Pro = dtValues.Split('$');
                dtValues = string.Join("<br><br>", Pro);
                // Add the date to the appropriate month column
                if (monthData.ContainsKey(month))
                {
                    monthData[month].Append(dtValues);
                }

            }

            // Append the last row
            html.Append("<tr>");
            html.Append("<td>" + currentDesig + "</td>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month].ToString() + "</td>");
            }
            html.Append("</tr>");

            // Close table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetGift()
    {
        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Input_Detail";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);

        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Build the table header
            html.Append("<thead><tr>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            // Open the table body
            html.Append("<tbody>");

            // Dictionary to track data for each month
            Dictionary<string, string> monthData = uniqueMonths.ToDictionary(m => m, m => string.Empty);

            foreach (DataRow row in dtrowClr.Rows)
            {
                // Extract MonthYear and name from the current row
                string month = row["MonthYear"].ToString();
                string dtValues = row["name"].ToString();

                // Update the month data
                if (monthData.ContainsKey(month))
                {
                    monthData[month] = dtValues.Replace("$", "<br><br>"); // Handle newlines
                }
            }

            // Append a single row with all the collected data
            html.Append("<tr>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month] + "</td>");
            }
            html.Append("</tr>");

            // Close the table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetFeed()
    {
        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Remarks_Feed";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);

        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Build the table header
            html.Append("<thead><tr>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            // Open the table body
            html.Append("<tbody>");

            // Dictionary to track data for each month
            Dictionary<string, string> monthData = uniqueMonths.ToDictionary(m => m, m => string.Empty);

            foreach (DataRow row in dtrowClr.Rows)
            {
                // Extract MonthYear and name from the current row
                string month = row["MonthYear"].ToString();
                string dtValues = row["name"].ToString();

                // Update the month data
                if (monthData.ContainsKey(month))
                {
                    monthData[month] = dtValues.Replace("$", "<br><br>"); // Handle newlines
                }
            }

            // Append a single row with all the collected data
            html.Append("<tr>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month] + "</td>");
            }
            html.Append("</tr>");

            // Close the table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetProduct_Visit()
    {
        ListedDR lst = new ListedDR();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        DataTable ProdCodes = lst.get_Prod_map_Pre(ddlDr.SelectedValue, ddlFieldForce.SelectedValue);

        DataTable dtprod = new DataTable();
        dtprod.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtprod.Columns["INX"].AutoIncrementSeed = 1;
        dtprod.Columns["INX"].AutoIncrementStep = 1;
        dtprod.Columns.Add("ProdCode");
        dtprod.Columns.Add("ProdName");

        // Populate dtprod with data from ProdCodes
        for (int i = 0; i < ProdCodes.Rows.Count; i++)
        {
            dtprod.Rows.Add(null, ProdCodes.Rows[i]["Product_Code_SlNo"], ProdCodes.Rows[i]["Product_Name"]);
        }

        // Use Copy() to avoid ownership conflicts
        DataTable copiedProdCodes = ProdCodes.Copy();

        // Add the copied table to the DataSet
        dschem.Tables.Add(copiedProdCodes);  //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Product_Visit";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);
        cmd.Parameters.AddWithValue("@prod_Codes", dtprod);
        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Dynamically build the header
            html.Append("<thead><tr>");
            html.Append("<th>Product Tagged</th>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            html.Append("<tbody>");

            // Loop through the rows and build the table rows
            string currentprod = string.Empty;
            Dictionary<string, StringBuilder> monthData = uniqueMonths.ToDictionary(m => m, m => new StringBuilder());

            foreach (DataRow row in dtrowClr.Rows)
            {
                string prod = row["prod_name"].ToString();
                string month = row["MonthYear"].ToString();
                string dtValues = row["dt"].ToString();

                // When the designation changes, create a new row
                if (currentprod != prod)
                {
                    // If it's not the first row, append the previous data
                    if (!string.IsNullOrEmpty(currentprod))
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + currentprod + "</td>");
                        foreach (var mon in uniqueMonths)
                        {
                            html.Append("<td>" + monthData[mon].ToString() + "</td>");
                        }
                        html.Append("</tr>");
                    }

                    // Reset month data
                    foreach (var key in monthData.Keys)
                    {
                        monthData[key].Clear();
                    }
                    currentprod = prod;
                }

                // Add the date to the appropriate month column
                if (monthData.ContainsKey(month))
                {
                    monthData[month].Append(dtValues);
                }

            }

            // Append the last row
            html.Append("<tr>");
            html.Append("<td>" + currentprod + "</td>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month].ToString() + "</td>");
            }
            html.Append("</tr>");

            // Close table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetChemist_Visit()
    {
        ListedDR lst = new ListedDR();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        DataTable ChemCodes = lst.get_Chem_map_Pre(ddlDr.SelectedValue, ddlFieldForce.SelectedValue);

        DataTable dtchem = new DataTable();
        dtchem.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtchem.Columns["INX"].AutoIncrementSeed = 1;
        dtchem.Columns["INX"].AutoIncrementStep = 1;
        dtchem.Columns.Add("ChemCode");
        dtchem.Columns.Add("ChemName");

        // Populate dtprod with data from ProdCodes
        for (int i = 0; i < ChemCodes.Rows.Count; i++)
        {
            dtchem.Rows.Add(null, ChemCodes.Rows[i]["Chemists_Code"], ChemCodes.Rows[i]["chemists_name"]);
        }

        // Use Copy() to avoid ownership conflicts
        DataTable copiedchemCodes = ChemCodes.Copy();

        // Add the copied table to the DataSet
        dsprod.Tables.Add(copiedchemCodes);  //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Chemist_Visit";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);
        cmd.Parameters.AddWithValue("@chem_Codes", dtchem);
        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Dynamically build the header
            html.Append("<thead><tr>");
            html.Append("<th>Chemist Name</th>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            html.Append("<tbody>");

            // Loop through the rows and build the table rows
            string currentchem = string.Empty;
            Dictionary<string, StringBuilder> monthData = uniqueMonths.ToDictionary(m => m, m => new StringBuilder());

            foreach (DataRow row in dtrowClr.Rows)
            {
                string chem = row["Chem_name"].ToString();
                string month = row["MonthYear"].ToString();
                string dtValues = row["dt"].ToString();

                // When the designation changes, create a new row
                if (currentchem != chem)
                {
                    // If it's not the first row, append the previous data
                    if (!string.IsNullOrEmpty(currentchem))
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + currentchem + "</td>");
                        foreach (var mon in uniqueMonths)
                        {
                            html.Append("<td>" + monthData[mon].ToString() + "</td>");
                        }
                        html.Append("</tr>");
                    }

                    // Reset month data
                    foreach (var key in monthData.Keys)
                    {
                        monthData[key].Clear();
                    }
                    currentchem = chem;
                }

                // Add the date to the appropriate month column
                if (monthData.ContainsKey(month))
                {
                    monthData[month].Append(dtValues);
                }

            }

            // Append the last row
            html.Append("<tr>");
            html.Append("<td>" + currentchem + "</td>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month].ToString() + "</td>");
            }
            html.Append("</tr>");

            // Close table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetBusiness_Visit()
    {
        ListedDR lst = new ListedDR();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        DataTable ProdCodes = lst.Get_Business_Pre(div_code, ddlDr.SelectedValue, ddlFieldForce.SelectedValue);

        DataTable dtprod = new DataTable();
        dtprod.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtprod.Columns["INX"].AutoIncrementSeed = 1;
        dtprod.Columns["INX"].AutoIncrementStep = 1;
        dtprod.Columns.Add("ProdCode");
        dtprod.Columns.Add("ProdName");

        // Populate dtprod with data from ProdCodes
        for (int i = 0; i < ProdCodes.Rows.Count; i++)
        {
            dtprod.Rows.Add(null, ProdCodes.Rows[i]["product_code"], ProdCodes.Rows[i]["Product_Detail_Name"]);
        }

        // Use Copy() to avoid ownership conflicts
        DataTable copiedProdCodes = ProdCodes.Copy();

        // Add the copied table to the DataSet
        //   dschem.Tables.Add(copiedProdCodes);  //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Business_Visit";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);
        cmd.Parameters.AddWithValue("@prod_Codes", dtprod);
        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Dynamically build the header
            html.Append("<thead><tr>");
            html.Append("<th>Business Given Product</th>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            html.Append("<tbody>");

            // Loop through the rows and build the table rows
            string currentprod = string.Empty;
            Dictionary<string, StringBuilder> monthData = uniqueMonths.ToDictionary(m => m, m => new StringBuilder());

            foreach (DataRow row in dtrowClr.Rows)
            {
                string prod = row["prod_name"].ToString();
                string month = row["MonthYear"].ToString();
                string dtValues = row["dt"].ToString();

                // When the designation changes, create a new row
                if (currentprod != prod)
                {
                    // If it's not the first row, append the previous data
                    if (!string.IsNullOrEmpty(currentprod))
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + currentprod + "</td>");
                        foreach (var mon in uniqueMonths)
                        {
                            html.Append("<td>" + monthData[mon].ToString() + "</td>");
                        }
                        html.Append("</tr>");
                    }

                    // Reset month data
                    foreach (var key in monthData.Keys)
                    {
                        monthData[key].Clear();
                    }
                    currentprod = prod;
                }

                // Add the date to the appropriate month column
                if (monthData.ContainsKey(month))
                {
                    monthData[month].Append(dtValues.Replace("0", ""));
                }

            }

            // Append the last row
            html.Append("<tr>");
            html.Append("<td>" + currentprod + "</td>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month].ToString() + "</td>");
            }
            html.Append("</tr>");

            // Close table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private void GetLastvisit()
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        string query = "SELECT TOP 5 CONVERT(CHAR(10), Activity_Date, 105) AS Activity_Date, FORMAT(CAST(Time AS DATETIME), 'hh:mm tt') AS Activity_Time, " +
                       "Activity_Remarks FROM DCRMain_Trans a JOIN DCRDetail_Lst_Trans b ON a.Trans_SlNo = b.Trans_SlNo WHERE a.Sf_Code = '" + ddlFieldForce.SelectedValue + "' " +
                       "AND b.Trans_Detail_Info_Type = 1 AND b.Trans_Detail_Info_Code = '" + ddlDr.SelectedValue + "' ORDER BY CAST(Activity_Date AS DATE) DESC;";

        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Build the table

                string html = "<table class='data-table' style='width:100%; border-collapse:collapse;'>";
                html += "<thead><tr><th>Date</th><th>Time</th><th>Remarks</th></tr></thead>";
                html += "<tbody>";

                while (reader.Read())
                {
                    html += "<tr>";
                    html += "<td  width='120px'>" + reader["Activity_Date"].ToString() + "</td>";
                    html += "<td width='80px'>" + reader["Activity_Time"].ToString() + "</td>";
                    html += "<td>" + reader["Activity_Remarks"].ToString() + "</td>";
                    html += "</tr>";

                }

                html += "</tbody></table>";

                // Set the HTML content to the Literal control
                ltLastvisit.Text = html;
            }
        }
        catch (Exception ex)
        {
            ltLastvisit.Text = "<p style='color:red;'>Error: {ex.Message}</p>";
        }

    }
    private string GetJointVisit()
    {
        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        // int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        DataTable SfCodes = sf1.getMRJointWork_camp(div_code, ddlFieldForce.SelectedValue, 0);
        DataTable dtsf_code = new DataTable();
        dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
        dtsf_code.Columns["INX"].AutoIncrementStep = 1;
        dtsf_code.Columns.Add("sf_code");
        dtsf_code.Columns.Add("sf_Designation_Short_Name");
        for (int i = 0; i < SfCodes.Rows.Count && i < 4; i++)
        {
            //j += 1;
            //dtsf_code.Rows.Add(j.ToString());

            dtsf_code.Rows.Add(null, SfCodes.Rows[i]["sf_code"], SfCodes.Rows[i]["sf_Designation_Short_Name"]);
        }

        DataSet dsmgrsf2 = new DataSet();
        dsmgrsf2.Tables.Add(SfCodes);   //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Jointwork_Details";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code);
        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Dynamically build the header
            html.Append("<thead><tr>");
            html.Append("<th>Desig.</th>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            html.Append("<tbody>");

            // Loop through the rows and build the table rows
            string currentDesig = string.Empty;
            Dictionary<string, StringBuilder> monthData = uniqueMonths.ToDictionary(m => m, m => new StringBuilder());

            foreach (DataRow row in dtrowClr.Rows)
            {
                string desig = row["desig"].ToString();
                string month = row["MonthYear"].ToString();
                string dtValues = row["dt"].ToString();

                // When the designation changes, create a new row
                if (currentDesig != desig)
                {
                    // If it's not the first row, append the previous data
                    if (!string.IsNullOrEmpty(currentDesig))
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + currentDesig + "</td>");
                        foreach (var mon in uniqueMonths)
                        {
                            html.Append("<td>" + monthData[mon].ToString() + "</td>");
                        }
                        html.Append("</tr>");
                    }

                    // Reset month data
                    foreach (var key in monthData.Keys)
                    {
                        monthData[key].Clear();
                    }

                    currentDesig = desig;
                }

                // Add the date to the appropriate month column
                if (monthData.ContainsKey(month))
                {
                    monthData[month].Append(dtValues);
                }

            }

            // Append the last row
            html.Append("<tr>");
            html.Append("<td>" + currentDesig + "</td>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month].ToString() + "</td>");
            }
            html.Append("</tr>");

            // Close table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetEvent()
    {
        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Event_Detail";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);

        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Build the table header
            html.Append("<thead><tr>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            // Open the table body
            html.Append("<tbody>");

            // Dictionary to track data for each month
            Dictionary<string, string> monthData = uniqueMonths.ToDictionary(m => m, m => string.Empty);

            //foreach (DataRow row in dtrowClr.Rows)
            //{
            //    // Extract MonthYear and name from the current row
            //    string month = row["MonthYear"].ToString();
            //    string dtValues = row["name"].ToString();

            //    // Update the month data
            //    if (monthData.ContainsKey(month))
            //    {
            //        monthData[month] = dtValues.Replace("$", "<br>"); // Handle newlines
            //    }
            //}
            foreach (DataRow row in dtrowClr.Rows)
            {
                // Extract MonthYear and name from the current row
                string month = row["MonthYear"].ToString();
                string dtValues = row["name"].ToString();

                // Assuming dtValues is an image file name, construct the full image path
                //if (dtValues.EndsWith(".jpeg$") || dtValues.EndsWith(".jpg$") || dtValues.EndsWith(".png$"))
                //{
                //    if (dtValues.EndsWith("$"))
                //    {
                //        dtValues = dtValues.Replace("$", "");
                //    }
                //    // Construct the full image path
                //    dtValues = "<img src='../../photos/" + dtValues + "' alt='Image' style='width:100px;height:auto;'>";
                //}
                //else
                //{
                //    // Handle newlines or other replacements
                //    dtValues = dtValues.Replace("$", "<br>");
                //}
                var imageNames = dtValues.Split('$');
                if (!string.IsNullOrEmpty(dtValues))
                {
                    if (imageNames.Length > 0)
                    {
                        dtValues = string.Join("<br> <br>", imageNames.Select(name =>
                        String.Format("<img src='../../photos/{0}' alt='Image' style='width:100px;height:auto;'>", name)));
                    }
                    else
                    {
                        dtValues = "No images available.";
                    }
                }
                // Update the month data
                if (monthData.ContainsKey(month))
                {
                    monthData[month] = dtValues;
                }
            }

            // Append a single row with all the collected data
            html.Append("<tr>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month] + "</td>");
            }
            html.Append("</tr>");

            // Close the table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetRCPA()
    {
        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_RCPA_Detail";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);

        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Dynamically build the header
            html.Append("<thead><tr>");
            html.Append("<th>Product (Potential/Yield)</th>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            html.Append("<tbody>");

            // Loop through the rows and build the table rows
            string currentDesig = string.Empty;
            Dictionary<string, StringBuilder> monthData = uniqueMonths.ToDictionary(m => m, m => new StringBuilder());

            foreach (DataRow row in dtrowClr.Rows)
            {
                string mode = row["ProdMode"].ToString();
                string month = row["MonthYear"].ToString();
                string dtValues = row["qty"].ToString();

                // When the designation changes, create a new row
                if (mode != " ")
                {
                    if (currentDesig != mode)
                    {
                        // If it's not the first row, append the previous data
                        if (!string.IsNullOrEmpty(currentDesig))
                        {
                            html.Append("<tr>");
                            html.Append("<td>" + currentDesig + "</td>");
                            foreach (var mon in uniqueMonths)
                            {
                                html.Append("<td>" + monthData[mon].ToString() + "</td>");
                            }
                            html.Append("</tr>");
                        }

                        // Reset month data
                        foreach (var key in monthData.Keys)
                        {
                            monthData[key].Clear();

                        }

                        currentDesig = mode;
                    }
                }
                var Pro = dtValues.Split('$');
                dtValues = string.Join("<br><br>", Pro);
                // Add the date to the appropriate month column
                if (monthData.ContainsKey(month))
                {
                    monthData[month].Append(dtValues);
                }

            }

            // Append the last row
            html.Append("<tr>");
            html.Append("<td>" + currentDesig + "</td>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month].ToString() + "</td>");
            }
            html.Append("</tr>");

            // Close table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetCRM()
    {
        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;
        //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_CRM_Detail";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);

        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Build the table header
            html.Append("<thead><tr>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            // Open the table body
            html.Append("<tbody>");

            // Dictionary to track data for each month
            Dictionary<string, string> monthData = uniqueMonths.ToDictionary(m => m, m => string.Empty);

            foreach (DataRow row in dtrowClr.Rows)
            {
                // Extract MonthYear and name from the current row
                string month = row["MonthYear"].ToString();
                string dtValues = row["name"].ToString();

                // Update the month data
                if (monthData.ContainsKey(month))
                {
                    monthData[month] = dtValues.Replace("$", "<br>"); // Handle newlines
                }
            }

            // Append a single row with all the collected data
            html.Append("<tr>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month].Replace("$", "<br>") + "</td>");
            }
            html.Append("</tr>");

            // Close the table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    private string GetRX_Det()
    {
        ListedDR lst = new ListedDR();
        int iMn = 0, iYr = 0;
        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));
        dtMnYr.Columns.Add("YR", typeof(int));
        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();
        string currentYr = DateTime.Now.Year.ToString();
        int Month = Convert.ToInt32(currentMonth);
        int iyear = Convert.ToInt32(currentYr);

        //int Month = 6;
        //int iyear = 2022;
        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn, iyear); // Add the month and year to the DataTable

            MonthCnt += 1; // Increment the month counter
            Month -= 1;    // Decrement the month

            // If the month goes below 1, reset to December and decrement the year
            if (Month < 1)
            {
                Month = 12;
                iyear -= 1;
            }
        }
        int j = 0;


        // Use Copy() to avoid ownership conflicts
        //DataTable copiedProdCodes = ProdCodes.Copy();

        //// Add the copied table to the DataSet
        //dschem.Tables.Add(copiedProdCodes);  //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";

        sProc_Name = "Precall_Rx_Visit";

        DataTable dtrowClr = new System.Data.DataTable();
        DataTable dtrowdt = new System.Data.DataTable();

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", div_code);
        cmd.Parameters.AddWithValue("@Msf_code", ddlFieldForce.SelectedValue);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);

        cmd.Parameters.AddWithValue("@ListDrCode", ddlDr.SelectedValue);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet dsts = new DataSet();
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        StringBuilder html = new StringBuilder();
        if (dtrowClr.Rows.Count > 0)
        {
            // Extract unique months for the header
            var uniqueMonths = dtrowClr.AsEnumerable()
                                       .Select(row => row["MonthYear"].ToString())
                                       .Distinct()
                                       .OrderBy(month =>
                                           Array.IndexOf(new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }, month))
                                       .ToList();

            // Start the table structure
            html.Append("<table class='data-table' style='width: 100%; border-collapse: collapse;'>");

            // Dynamically build the header
            html.Append("<thead><tr>");
            html.Append("<th>Rx Qty(Value)</th>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<th>" + month + "</th>");
            }
            html.Append("</tr></thead>");

            html.Append("<tbody>");

            // Loop through the rows and build the table rows
            string currentprod = string.Empty;
            Dictionary<string, StringBuilder> monthData = uniqueMonths.ToDictionary(m => m, m => new StringBuilder());

            foreach (DataRow row in dtrowClr.Rows)
            {
                string prod = row["prod_name"].ToString();
                string month = row["MonthYear"].ToString();
                string dtValues = row["dt"].ToString();

                // When the designation changes, create a new row
                if (currentprod != prod)
                {
                    // If it's not the first row, append the previous data
                    if (!string.IsNullOrEmpty(currentprod))
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + currentprod + "</td>");
                        foreach (var mon in uniqueMonths)
                        {
                            html.Append("<td>" + monthData[mon].ToString() + "</td>");
                        }
                        html.Append("</tr>");
                    }

                    // Reset month data
                    foreach (var key in monthData.Keys)
                    {
                        monthData[key].Clear();
                    }
                    currentprod = prod;
                }

                // Add the date to the appropriate month column
                if (monthData.ContainsKey(month))
                {

                    monthData[month].Append(dtValues.Replace("0 ( 0 )", ""));
                }

            }

            // Append the last row
            html.Append("<tr>");
            html.Append("<td>" + currentprod + "</td>");
            foreach (var month in uniqueMonths)
            {
                html.Append("<td>" + monthData[month].ToString() + "</td>");
            }
            html.Append("</tr>");

            // Close table body and table
            html.Append("</tbody></table>");
        }
        else
        {
            html.Append("<p>No data available.</p>");
        }

        // Return the generated HTML table
        return html.ToString();
    }
    #region btnback_Click
    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/MasterFiles/Dashboard_Menu.aspx?sfcode=" + sf_code + "&cMnth=" + DateTime.Now.Month.ToString().Trim() + "&cYr=" + DateTime.Now.Year.ToString().Trim() + "&div_code=" + div_code + "&sf_type=" + sf_type + "");
    }
    #endregion

    private void FillReportMonth()
    {

    }


    protected void GrdTimeSt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int indx = e.Row.RowIndex;
            int k = e.Row.Cells.Count - 5;
            //
            #region Calculations

            for (int l = 2, j = 0; l < e.Row.Cells.Count; l++)
            {


            }
            #endregion

        }
    }
    protected void GrdFixation_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //
            #region Object
            //Creating a gridview object            
            GridView objGridView = (GridView)sender;

            #endregion
            //
            #region Merge cells



            var lastSixMonths = Enumerable
          .Range(0, 3)
          .Select(i => DateTime.Now.AddMonths(i - 3 + 1))
          .Select(date => date.ToString("MM/yyyy"));
            // FillReport();
            string[] sck;
            sck = lastSixMonths.ToArray();
            var last = sck.Last().Split('/');
            var first = sck.First().Split('/');
            int months = (Convert.ToInt32(last[1]) - Convert.ToInt32(first[1])) * 12 + Convert.ToInt32(last[0]) - Convert.ToInt32(first[0]); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
            int cmonth = Convert.ToInt32(first[0]);
            int cyear = Convert.ToInt32(first[1]);

            ViewState["months"] = months;
            ViewState["cmonth"] = cmonth;
            ViewState["cyear"] = cyear;
            SalesForce sf = new SalesForce();
            GridViewRow objgridviewrow = new GridViewRow(1, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow2 = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            GridViewRow objgridviewrow3 = new GridViewRow(3, 0, DataControlRowType.Header, DataControlRowState.Insert);




            //iLstColCnt.Add(3);
            for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
            {
                int iCnt = 0;

                TableCell objtablecell = new TableCell();
                AddMergedCells(objgridviewrow, objtablecell, months + 1, dsmgrsf.Tables[0].Rows[w]["sf_Designation_Short_Name"].ToString(), "dynamic-header", false);

                int j = 1;
                months = (Convert.ToInt32(last[1]) - Convert.ToInt32(first[1])) * 12 + Convert.ToInt32(last[0]) - Convert.ToInt32(first[0]); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
                cmonth = Convert.ToInt32(first[0]);
                cyear = Convert.ToInt32(first[1]);
                if (months >= 0)
                {

                    for (j = 1; j <= months + 1; j++)
                    {


                        TableCell objtablecell2 = new TableCell();
                        AddMergedCells(objgridviewrow2, objtablecell2, 0, sf.getMonthName(cmonth.ToString()).Substring(0, 3) + " - " + cyear, "dynamic-header", true);
                        cmonth = cmonth + 1;
                        if (cmonth == 13)
                        {
                            cmonth = 1;
                            cyear = cyear + 1;
                        }
                    }
                }

                //iLstVstmnt.Add(cmonth1);
                //iLstVstyr.Add(cyear1);

            }


            objGridView.Controls[0].Controls.AddAt(0, objgridviewrow);
            objGridView.Controls[0].Controls.AddAt(1, objgridviewrow2);
            objGridView.Controls[0].Controls.AddAt(2, objgridviewrow3);


            //
            #endregion
            //
        }
    }
    //protected void AddMergedCells(GridViewRow objgridviewrow, TableCell objtablecell, int colspan, string celltext, string backcolor, bool bRowspan)
    //{
    //    objtablecell = new TableCell();
    //    objtablecell.Text = celltext;
    //    objtablecell.ColumnSpan = colspan;
    //    if ((colspan == 0) && bRowspan)
    //    {
    //        objtablecell.RowSpan = 2;
    //    }
    //    //objtablecell.Style.Add("background-color", backcolor);
    //    //objtablecell.Style.Add("border-color", "black");
    //    //objtablecell.Style.Add("color", "white");
    //    objtablecell.HorizontalAlign = HorizontalAlign.Center;
    //    objgridviewrow.Cells.Add(objtablecell);
    //}
    protected void AddMergedCells(GridViewRow row, TableCell cell, int columnSpan, string text, string cssClass, bool isCentered)
    {
        cell.Text = text;
        cell.ColumnSpan = columnSpan;
        cell.CssClass = cssClass; // Apply CSS class
        if (isCentered)
        {
            cell.HorizontalAlign = HorizontalAlign.Center;
        }
        row.Cells.Add(cell);
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    foreach (TableCell cell in e.Row.Cells)
        //    {
        //        cell.Attributes.Add("title", "Tooltip text for " + cell.Text);
        //    }
        //}


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int i = 0;
            int m = 1;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int indx = e.Row.RowIndex;
                int k = e.Row.Cells.Count - 5;
                //
                #region Calculations
                //
                List<int> sDate = new List<int>();
                int iDateList;
                for (int s = 0; s < dtrowdt.Rows.Count; s++)
                {
                    //if (e.Row.Cells[1].Text == dtrowdt.Rows[s][0].ToString())
                    //{
                    for (i = 0, m = 1; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].Text = dtrowdt.Rows[s][m].ToString().Replace("[", "").Replace("]", "");
                        e.Row.Cells[i].Wrap = false;
                        iDateList = 0;
                        sDate.Clear();
                        string[] strSplit = e.Row.Cells[i].Text.Split(',');
                        foreach (string str in strSplit)
                        {
                            if (str != "")
                            {
                                iDateList = Convert.ToInt32(str);
                                sDate.Add(iDateList);
                            }
                        }
                        string sDateList = "";
                        sDate.Sort();
                        foreach (int item in sDate)
                        {
                            sDateList += item.ToString() + ",";
                        }

                        e.Row.Cells[i].Text = sDateList;

                        m++;
                    }
                    i++;

                    break;
                    //}
                }
                #endregion
                //

            }

        }

    }
}
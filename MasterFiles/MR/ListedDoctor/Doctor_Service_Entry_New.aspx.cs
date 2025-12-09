using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;

public partial class MasterFiles_MR_ListedDoctor_Doctor_Service_Entry_New : System.Web.UI.Page
{

    #region Variables
    string sMode = string.Empty;
    string sf_code = string.Empty;
    string FMonth = string.Empty;
    string FYear = string.Empty;
    string TMonth = string.Empty;
    string TYear = string.Empty;
    string div_code = string.Empty;
    string sReturn = string.Empty;
    string sDay = string.Empty;
    string sCnt = string.Empty;
    string vMode = string.Empty;
    string strmode = string.Empty;
    string strFieledForceName = string.Empty;
    string sCurrentDate = string.Empty;
    DataSet dsFF = new DataSet();
    DataSet dsdoc = new DataSet();
    DataTable dtrowClr = new System.Data.DataTable();
    DataTable dtrowdt = new System.Data.DataTable();
    SalesForce dcrdoc = new SalesForce();
    DataSet dsDoctor = new DataSet();
    string sHeading; int sReportType;
    string strCase = string.Empty;
    List<int> iLstVstmnt = new List<int>();
    List<int> iLstVstyr = new List<int>();
    List<DataTable> result = new List<System.Data.DataTable>();
    #endregion
    DataSet dsmgrsf = new DataSet();
    DataTable dtsf_code = new DataTable();
    DataSet dsts = new DataSet();

    string S_Code = string.Empty;
    string doctorcode = string.Empty;
    string Mode = string.Empty;
    string SCode = string.Empty;
    string CrmStatus = string.Empty;
    string F_Code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        string div_code = Session["div_code"].ToString();
        S_Code = Session["SF_Code_N"].ToString();

        string sfCode = string.Empty;
        txtEffFrom.Text = System.DateTime.Now.ToString("dd-MM-yyyy");
        
        div_code = Session["div_code"].ToString();

        if (Request.QueryString["ListedDrCode"] != null && Request.QueryString["ListedDrCode"] != "")
        {
            doctorcode = Request.QueryString["ListedDrCode"];
        }
        if (Request.QueryString["Mode"] != null && Request.QueryString["Mode"] != "")
        {
            Mode = Request.QueryString["Mode"];
        }
        if (Request.QueryString["SCode"] != null && Request.QueryString["SCode"] != "")
        {
            SCode = Request.QueryString["SCode"];
            Session["CrmMgrCode"] = SCode;
        }
        if (Request.QueryString["CrmStatus"] != null && Request.QueryString["CrmStatus"] != "")
        {
            CrmStatus = Request.QueryString["CrmStatus"];
        }
        if (Request.QueryString["F_Code"] != null && Request.QueryString["F_Code"] != "")
        {
            F_Code = Request.QueryString["F_Code"];
            Session["F_Code"] = F_Code;
            Session["sf_code"] = F_Code;
        }


        if (!Page.IsPostBack)
        {
            if (Mode == "Doctor")
            {
                FillReport();
            }
        }

        if (Session["sf_type"].ToString() == "1")
        {
            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            sfCode = Session["sf_code"].ToString();

            //ddlFieldForce.SelectedValue = Session["sf_code"].ToString();
            //ddlFieldForce.Enabled = false;
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            sfCode = Session["sf_code"].ToString();

            Session["backurl"] = "Doctor_Service_CRM_New.aspx";

        }
        else
        {

            UserControl_MenuUserControl c1 =
            (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            sfCode = Session["sf_code"].ToString();
        }
    }

    private void FillReport()
    {

        SalesForce sf1 = new SalesForce();
        int iMn = 0, iYr = 0;

        DataTable dtMnYr = new DataTable();
        dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        dtMnYr.Columns.Add("MNTH", typeof(int));

        int MonthCnt = 0;

        string currentMonth = DateTime.Now.Month.ToString();

        int Month = Convert.ToInt32(currentMonth);

        while (MonthCnt < 3)
        {
            iMn = Month;
            dtMnYr.Rows.Add(null, iMn);
            // months--; cmonth++;
            MonthCnt += 1;
            Month = Convert.ToInt32(Month) - 1;

            if (Month == 0)
            {
                Month = 12;
            }
        }

        string sf_code = Session["sf_code"].ToString();
        string doctorcode = Request.QueryString["ListedDrCode"];
        string div_code = Session["div_code"].ToString();

        int j = 0;
        DataTable SfCodes = sf1.getMRJointWork_camp(div_code, sf_code, 0);
        DataTable dtsf_code = new DataTable();
        dtsf_code.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        dtsf_code.Columns["INX"].AutoIncrementSeed = 1;
        dtsf_code.Columns["INX"].AutoIncrementStep = 1;
        dtsf_code.Columns.Add("sf_code");
        for (int i = 0; i < SfCodes.Rows.Count; i++)
        {
            //j += 1;
            //dtsf_code.Rows.Add(j.ToString());

            dtsf_code.Rows.Add(null, SfCodes.Rows[i]["sf_code"]);
        }

        // DataSet dsmgrsf = new DataSet();
        dsmgrsf.Tables.Add(SfCodes);   //Listeddr_Period_MGR_Proc

        SalesForce sf = new SalesForce();
        DCR dcc = new DCR();
        // DB_EReporting db = new DB_EReporting();
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        string sProc_Name = "";
        sProc_Name = "Listeddr_Period_MGR_Proc";

        SqlCommand cmd = new SqlCommand(sProc_Name, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(div_code));
        cmd.Parameters.AddWithValue("@Msf_code", sf_code);
        cmd.Parameters.AddWithValue("@Mnth_tbl", dtMnYr);
        cmd.Parameters.AddWithValue("@Mgr_Codes", dtsf_code);
        cmd.Parameters.AddWithValue("@ListDrCode", doctorcode);
        cmd.CommandTimeout = 800;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsts);
        dtrowClr = dsts.Tables[0].Copy();
        dtrowdt = dsts.Tables[0].Copy();

        GenerateTable();
    }

    private void GenerateTable()
    {
        TableRow tr_mth_header = new TableRow();
        TableRow tr_sub_header = new TableRow();

        string currentMonth = DateTime.Now.Month.ToString();

        int Month = Convert.ToInt32(currentMonth);
        int iColSpan = 0;

        for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
        {
            TableCell tc_sec_name = new TableCell();
            tc_sec_name.BorderStyle = BorderStyle.Solid;
            tc_sec_name.BorderWidth = 1;
            tc_sec_name.Width = 100;
            tc_sec_name.ColumnSpan = 3;
            Literal lit_sec_name = new Literal();
            string ColName = dsmgrsf.Tables[0].Rows[w]["sf_Designation_Short_Name"].ToString() + "(" + dsmgrsf.Tables[0].Rows[w]["sf_Name"].ToString() + ")";
            lit_sec_name.Text = ColName;
            tc_sec_name.Attributes.Add("Class", "Backcolor");
            tc_sec_name.Controls.Add(lit_sec_name);
            //tr_header.Cells.Add(tc_sec_name);
            tr_mth_header.Cells.Add(tc_sec_name);

            // tc_sec_name.BackColor = System.Drawing.ColorTranslator.FromHtml("#1f9bad");

            tc_sec_name.BackColor = System.Drawing.ColorTranslator.FromHtml("#666699");
            tc_sec_name.ForeColor = System.Drawing.Color.White;

            tr_sub_header.BackColor = System.Drawing.Color.White;
            tr_sub_header.Attributes.Add("Class", "rptCellBorder");

            //  tr_sub_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#1f9bad");
            tr_sub_header.BackColor = System.Drawing.ColorTranslator.FromHtml("#666699");
            tr_sub_header.ForeColor = System.Drawing.Color.White;

            int MonthCnt = 0;
            Month = Convert.ToInt32(currentMonth);

            while (MonthCnt < 3)
            {
                TableCell objtablecell3 = new TableCell();

                SalesForce sf = new SalesForce();
                string MonthName = sf.getMonthName(Month.ToString()).Substring(0, 3);

                TableCell tc_qty = new TableCell();
                tc_qty.BorderStyle = BorderStyle.Solid;
                tc_qty.BorderWidth = 1;
                tc_qty.Width = 50;
                Literal lit_qty = new Literal();
                lit_qty.Text = MonthName;
                tc_qty.Attributes.Add("Class", "Backcolor");
                tc_qty.Controls.Add(lit_qty);
                tr_sub_header.Cells.Add(tc_qty);

                // AddMergedCells(objgridviewrow3, objtablecell3, 0, MonthName, "#0097AC", false);

                MonthCnt += 1;
                Month = Convert.ToInt32(Month) - 1;

                if (Month == 0)
                {
                    Month = 12;
                }

                // Month = Convert.ToInt32(Month) + 1;
            }

        }

        tbl.Rows.Add(tr_mth_header);
        tbl.Rows.Add(tr_sub_header);

        List<string> objData = new List<string>();

        //List<string> objSfData = new List<string>();

        //for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
        //{
        //    string Sf_Code = dsmgrsf.Tables[0].Rows[w]["sf_code"].ToString();

        //    if (dsts.Tables[0].Rows.Contains(Sf_Code))
        //    {

        //    }

        //}

        foreach (DataRow dr in dsts.Tables[0].Rows)
        {
            string Data = dr["Sf_Code"].ToString() + "_" + dr["Month"].ToString() + "_" + dr["dt"].ToString();
            objData.Add(Data);
        }

        TableRow tr_det = new TableRow();
        tr_det.BackColor = System.Drawing.Color.White;
        tr_det.Attributes.Add("Class", "rptCellBorder");

        if (objData.Count > 0)
        {

            if (dsmgrsf.Tables[0].Rows.Count > 0)
            {

                for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
                {

                    string Sf_Code = dsmgrsf.Tables[0].Rows[w]["sf_code"].ToString();

                    int MonthCnt = 0;
                    Month = Convert.ToInt32(currentMonth);

                    // for (int i = 0; i < objData.Count; i++)
                    // {
                    while (MonthCnt < 3)
                    {
                        SalesForce sf = new SalesForce();
                        string MonthName = sf.getMonthName(Month.ToString()).Substring(0, 3);

                        TableCell tc_det_qty = new TableCell();
                        tc_det_qty.BorderStyle = BorderStyle.Solid;
                        tc_det_qty.BorderWidth = 1;
                        // tc_det_qty.HorizontalAlign = HorizontalAlign.Center;
                        tc_det_qty.Width = 50;
                        Literal lit_det_qty = new Literal();
                        lit_det_qty.Text = "";


                        for (int i = 0; i < objData.Count; i++)
                        {
                            if (objData[i].Contains(Sf_Code) && objData[i].Contains(MonthName))
                            {
                                string str = objData[i].ToString();
                                string[] ArrData = str.Split('_');

                                if (ArrData[1].Contains(MonthName))
                                {
                                    lit_det_qty.Text = ArrData[2];
                                }
                            }
                        }

                        tc_det_qty.Attributes.Add("Class", "Backcolor");
                        tc_det_qty.Controls.Add(lit_det_qty);
                        tr_det.Cells.Add(tc_det_qty);

                        MonthCnt += 1;
                        Month = Convert.ToInt32(Month) - 1;

                        if (Month == 0)
                        {
                            Month = 12;
                        }
                    }

                    //  }

                    tbl.Rows.Add(tr_det);

                }
            }
        }
        else
        {

            for (int w = 0; w < dsmgrsf.Tables[0].Rows.Count; w++)
            {

                int MonthCnt = 0;
                Month = Convert.ToInt32(currentMonth);


                while (MonthCnt < 3)
                {
                    TableCell tc_det_qty = new TableCell();
                    tc_det_qty.BorderStyle = BorderStyle.Solid;
                    tc_det_qty.BorderWidth = 1;
                    // tc_det_qty.HorizontalAlign = HorizontalAlign.Center;
                    tc_det_qty.Width = 50;
                    Literal lit_det_qty = new Literal();
                    if (MonthCnt == 0 && w == 0)
                    {
                        lit_det_qty.Text = "No Records Found";
                        tc_det_qty.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lit_det_qty.Text = "";
                    }
                    tc_det_qty.Attributes.Add("Class", "Backcolor");
                    tc_det_qty.Controls.Add(lit_det_qty);
                    tr_det.Cells.Add(tc_det_qty);

                    MonthCnt += 1;
                }

                tbl.Rows.Add(tr_det);
            }
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        try
        {
            Session["sf_code"] = S_Code;
            Server.Transfer("Doctor_Service_CRM_New.aspx");

        }
        catch (Exception ex)
        {

        }
    }
    
}


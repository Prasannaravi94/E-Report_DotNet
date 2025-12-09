using Bus_EReport;
using DBase_EReport;
using System;
using System.Data;
using System.Web.UI;

public partial class MasterFiles_DashBoard_App_usage : System.Web.UI.Page
{
    string strMultiDiv = string.Empty;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsDivision = null;
    DataSet dsdiv = new DataSet();
    DataTable dtFill = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_code = Session["sf_code"].ToString();
        div_code = Session["division_code"].ToString();
        sf_type = Session["sf_type"].ToString();
        if (!Page.IsPostBack)
        {
            Product prd = new Product();
            DataSet dsdiv = new DataSet();
            dsdiv = prd.getMultiDivsf_Name(sf_code);
            strMultiDiv = dsdiv.Tables[0].Rows[0][1].ToString().Remove(dsdiv.Tables[0].Rows[0][1].ToString().Length - 1, 1);
            Filldiv();
        }
        if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
        {
            UserControl_pnlMenu c1 = (UserControl_pnlMenu)LoadControl("~/UserControl/pnlMenu.ascx");
            Divid.Controls.Add(c1);
        }
    }

    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
    }

    protected void btnmgrgo_Click(object sender, EventArgs e)
    {
        //AdminSetup aa = new AdminSetup();
        //DataSet dsAdminSetup = aa.App_Usage_View(ddlDivision.SelectedValue);
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = "Exec App_Usage_Version2 '" + ddlDivision.SelectedValue + "'";
        DataSet dsAdminSetup = db_ER.Exec_DataSet(strQry);
        if (dsAdminSetup.Tables[0].Rows.Count > 0)
        {
            pnlContents.Visible = true;
            WorkType(dsAdminSetup);
            FillLocation(dsAdminSetup);
            FillVisitType(dsAdminSetup);
            FillDoctor(dsAdminSetup);
            FillChemist(dsAdminSetup);
            FillStockist(dsAdminSetup);
            FillUnlisteddr(dsAdminSetup);
            FillCIP(dsAdminSetup);
            FillHospital(dsAdminSetup);
            FillTourPlan(dsAdminSetup);
            FillRCPA(dsAdminSetup);
            FillMissed_Date_Leave(dsAdminSetup);
            FillOrderMgmt(dsAdminSetup);
            FillManager_Options(dsAdminSetup);
            FillCheckInOut(dsAdminSetup);
            //FillTracking(dsAdminSetup);
            FillAdditional_Options(dsAdminSetup);
            FillOthers(dsAdminSetup);
        }
    }

    private void WorkType(DataSet DsGet)
    {
        rptBaseLevel.DataSource = DsGet.Tables[1];
        rptBaseLevel.DataBind();

        rptManager.DataSource = DsGet.Tables[2];
        rptManager.DataBind();
    }

    private void FillLocation(DataSet DsGet)
    {

        dtFill.Clear();
        dtFill.Columns.Add("Ordersl_no");
        dtFill.Columns.Add("ColName");
        int j = 1;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "Location")
            {
                dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                j += 1;
            }
        }
        rptLocation.DataSource = dtFill;
        rptLocation.DataBind();
    }

    private void FillVisitType(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "VisitType")
            {
                dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                j += 1;
            }
        }
        rptVisitType.DataSource = dtFill;
        rptVisitType.DataBind();
    }

    private void FillDoctor(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        int flag = 0;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "DoctorEntry")
            {
                if (DsGet.Tables[0].Rows[i]["col"].ToString() == "Doc_Pob_Need")
                {
                    flag = 1;
                }
                if (DsGet.Tables[0].Rows[i]["TextDrop"].ToString() == "")
                {
                    if (flag == 1)
                    {
                        dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                    }
                }
                else
                {
                    dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                }
                j += 1;
            }
        }
        rptDoctor.DataSource = dtFill;
        rptDoctor.DataBind();
    }

    private void FillChemist(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;

        int flag = 0;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "ChemistEntry")
            {

                if (DsGet.Tables[0].Rows[i]["col"].ToString() == "Chm_Pob_Need")
                {
                    flag = 1;
                }
                if (DsGet.Tables[0].Rows[i]["TextDrop"].ToString() == "")
                {
                    if (flag == 1)
                    {
                        dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                    }
                }
                else
                {
                    dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                }
                               
                j += 1;
            }
        }
        rptChemist.DataSource = dtFill;
        rptChemist.DataBind();
    }

    private void FillStockist(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        int flag = 0;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "StockistEntry")
            {
                if (DsGet.Tables[0].Rows[i]["col"].ToString() == "Stk_Pob_Need")
                {
                    flag = 1;
                }
                if (DsGet.Tables[0].Rows[i]["TextDrop"].ToString() == "")
                {
                    if (flag == 1)
                    {
                        dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                    }
                }
                else
                {
                    dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                }
                j += 1;
            }
        }
        rptStockist.DataSource = dtFill;
        rptStockist.DataBind();
    }

    private void FillUnlisteddr(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        int flag = 0;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "UnlistedDrEntry")
            {
                if (DsGet.Tables[0].Rows[i]["col"].ToString() == "Ul_Pob_Need")
                {
                    flag = 1;
                }
                if (DsGet.Tables[0].Rows[i]["TextDrop"].ToString() == "")
                {
                    if (flag == 1)
                    {
                        dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                    }
                }
                else
                {
                    dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                }
                j += 1;
            }
        }
        rptUnlisteddr.DataSource = dtFill;
        rptUnlisteddr.DataBind();
    }

    private void FillCIP(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        int flag = 0;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "CIPEntry")
            {
               
                if (DsGet.Tables[0].Rows[i]["col"].ToString() == "CIPPOBNd")
                {
                    flag = 1;
                }
                if (DsGet.Tables[0].Rows[i]["TextDrop"].ToString() == "")
                {
                    if (flag == 1)
                    {
                        dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                    }
                }
                else
                {
                    dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                }
                j += 1;
            }
        }
        rptCIP.DataSource = dtFill;
        rptCIP.DataBind();
    }

    private void FillHospital(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        int flag = 0;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "HopsitalEntry")
            {
                if (DsGet.Tables[0].Rows[i]["col"].ToString() == "HosPOBNd")
                {
                    flag = 1;
                }
                if (DsGet.Tables[0].Rows[i]["TextDrop"].ToString() == "")
                {
                    if (flag == 1)
                    {
                        dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                    }
                }
                else
                {
                    dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                }
                j += 1;
            }
        }
        rptHospital.DataSource = dtFill;
        rptHospital.DataBind();
    }

    private void FillTourPlan(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "TourPlan")
            {
                dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                j += 1;
            }
        }
        rptTourPlan.DataSource = dtFill;
        rptTourPlan.DataBind();
    }

    private void FillRCPA(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "RCPA")
            {
                dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                j += 1;
            }
        }
        rptRCPA.DataSource = dtFill;
        rptRCPA.DataBind();
    }

    private void FillMissed_Date_Leave(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "MissDateEntry")
            {
                dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                j += 1;
            }
        }
        rptMissed_Date_Leave.DataSource = dtFill;
        rptMissed_Date_Leave.DataBind();
    }

    private void FillOrderMgmt(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        int flagOrder_management = 0;
        int flagSecondary_order = 0;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "OrderManagement")
            {
                //dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                if (DsGet.Tables[0].Rows[i]["col"].ToString() == "Primary_order")
                {
                    flagOrder_management = 1;
                }
                if (DsGet.Tables[0].Rows[i]["TextDrop"].ToString() == "")
                {
                    if (flagOrder_management == 1)
                    {
                        dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                    }
                }
                else
                {
                    dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                }

                if (DsGet.Tables[0].Rows[i]["col"].ToString() == "Secondary_order")
                {
                    flagSecondary_order = 1;
                }
                if (DsGet.Tables[0].Rows[i]["TextDrop"].ToString() == "")
                {
                    if (flagSecondary_order == 1)
                    {
                        dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                    }
                }
                else
                {
                    dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                }
                j += 1;
            }
        }
        rptOrderMgmt.DataSource = dtFill;
        rptOrderMgmt.DataBind();
    }

    private void FillManager_Options(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "ManagerOptions")
            {
                dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                j += 1;
            }
        }
        rptManager_Options.DataSource = dtFill;
        rptManager_Options.DataBind();
    }

    private void FillCheckInOut(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "CheckInOut")
            {
                dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                j += 1;
            }
        }
        rptCheckInOut.DataSource = dtFill;
        rptCheckInOut.DataBind();
    }

    //private void FillTracking(DataSet DsGet)
    //{
    //    dtFill.Clear();
    //    int j = 1;
    //    for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
    //    {
    //        if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "TrackOption")
    //        {
    //            dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
    //            j += 1;
    //        }
    //    }
    //    rptTracking.DataSource = dtFill;
    //    rptTracking.DataBind();
    //}

    private void FillAdditional_Options(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "AdditionOption")
            {
                dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                j += 1;
            }
        }
        rptAdditionalOptions.DataSource = dtFill;
        rptAdditionalOptions.DataBind();
    }

    private void FillOthers(DataSet DsGet)
    {
        dtFill.Clear();
        int j = 1;
        for (int i = 0; i < DsGet.Tables[0].Rows.Count; i++)
        {
            if (DsGet.Tables[0].Rows[i]["groupName"].ToString() == "Other")
            {
                dtFill.Rows.Add(j, DsGet.Tables[0].Rows[i]["ColName"]);
                j += 1;
            }
        }
        rptOthers.DataSource = dtFill;
        rptOthers.DataBind();
    }
}
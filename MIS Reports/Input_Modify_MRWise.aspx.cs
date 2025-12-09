using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using DBase_EReport;

public partial class MIS_Reports_Input_Modify_MRWise : System.Web.UI.Page
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
    DataSet dsSalesforce = null;
    DataSet dsDoctor = null;
    int tot_days = -1;
    int cday = 1;
    string sDCR = string.Empty;
    int ddate = 0;
    int Trans_Sl_No_From;
    int Detail_Trans_Sl_No;
    //DateTime ServerStartTime;
    //DateTime ServerEndTime;
    int time;
    DB_EReporting db_ER = new DB_EReporting();
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
            //ServerStartTime = DateTime.Now;
            //base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            //FillMR();
            //FillYear();

            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                // FillMRManagers();
                Fillshow();
                FillMasterList();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
                // FillMRfor_mr_To();
                //  FillYear();

            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                //  txtNew.Visible = false;
                //FillMRManagers();
                //FillYear();
                FillMasterList();
                ddlFieldForce.SelectedValue = sf_code;
                ddlFieldForce.Enabled = false;
                Fillshow();
            }

            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                Showlist.Visible = true;
                FillMasterList();

            }

        }

        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }

            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }


        }
    }
    private void FillMasterList()
    {
        //dsSalesForce = sf.UserList_getMR(div_code, sfCode);
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    ddlFieldForce.DataTextField = "sf_name";
        //    ddlFieldForce.DataValueField = "sf_code";
        //    ddlFieldForce.DataSource = dsSalesForce;
        //    ddlFieldForce.DataBind();
        //    ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        //}

        //if (Session["sf_type"].ToString() == "1")
        //{

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
        //}

        // else //if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        //{
        //    dsSalesForce = sf.SalesForceList_New_GetMr(div_code, sfCode);
        //}
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", ""));
        }
    }
    private void Fillshow()
    {
       string strQry = "select Input_Edit_Flag from Option_Sample_Input_Edit where sf_Code='"+sf_code+"'";

        dsDoctor = db_ER.Exec_DataSet(strQry);

        dsDoctor = db_ER.Exec_DataSet(strQry);
        if (dsDoctor.Tables[0].Rows.Count > 0)
        {
            if (dsDoctor.Tables[0].Rows[0]["Input_Edit_Flag"].ToString() == "1")
            {
                Showlist.Visible = true;
            }
            else
            {
                hidelist.Visible = true;
            }

        }
        else
        {
            hidelist.Visible = true;
        }
    }
    //private void FillYear()
    //{
    //    TourPlan tp = new TourPlan();
    //    dsTP = tp.Get_TP_Edit_Year(div_code);
    //    if (dsTP.Tables[0].Rows.Count > 0)
    //    {
    //        for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
    //        {
    //            ddlYear.Items.Add(k.ToString());
    //            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
    //        }
    //    }
    //    ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
    //}

    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time 
    }

    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    protected void BtnGo_Click(object sender, EventArgs e)
    {
        ddlMonth.Enabled = false;
        ddlYear.Enabled = false;
        DataSet da = new DataSet();
        Product Sample_Product = new Product();
        da = Sample_Product.getmotnh_Dcr_sample(div_code, ddlFieldForce.SelectedValue, ddlMonth.SelectedValue, ddlYear.SelectedValue);
        if (da.Tables[0].Rows.Count > 0)
        {

            grdsample.DataSource = da;
            grdsample.DataBind();
        }
        else
        {
            grdsample.DataSource = da;
            grdsample.DataBind();
        }

    }
    protected void BtnTransfer_Click(object sender, EventArgs e)
    {



    }
    [WebMethod]
    public static string GetCustomersdr(string Trans_Sl_No, string Dr_Code, string Date)
    {
        string sf_code = string.Empty;
        string valss = string.Empty;
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        if (HttpContext.Current.Session["sf_code_mr"] != null && HttpContext.Current.Session["sf_code_mr"] != "")
        {
            sf_code = HttpContext.Current.Session["sf_code_mr"].ToString();
        }

        else
        {
            sf_code = HttpContext.Current.Session["sf_code"].ToString();
        }




        string query = "SELECT Trans_Detail_Info_Code, sf_code,Trans_SlNo,Product_Code,LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))) AS Prod_code,right((LTRIM(RTRIM(m.n.value('.[1]', 'varchar" +
            "(8000)')))), len((LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))))) - charindex('~', (LTRIM(RTRIM(m.n.value('.[1]', 'varchar(8000)'))))))issue_Qty into #Drs_Prod_Bfr FROM " +
            "(SELECT c.Trans_SlNo, Trans_Detail_Info_Code, C.sf_code, c.Product_Code,CAST('<XMLRoot><RowData>' + REPLACE(c.Product_Code, '#', '#</RowData><RowData>') + '</RowData></XMLRoot>' AS XML)" +
            " AS x FROM DCRDetail_Lst_Trans C where c.Trans_SlNo = '" + Trans_Sl_No + "' and c.Trans_Detail_Info_Code = '" + Dr_Code + "' and (Product_Code <> '' and Product_Code like '%~%'))t CROSS APPLY x.nodes('/XMLRoot/RowData')m(n) select Trans_SlNo, Trans_Detail_Info_Code, sf_code, replace((replace((replace((replace((Prod_code), '#', '')), '$0', '')), '^0', '')), '$', '')Prod_code,issue_Qty Into #Drs_Prod_Bfr_Filtr from #Drs_Prod_Bfr where (Prod_code<>'' and Prod_code<>'0') select distinct Trans_SlNo,Trans_Detail_Info_Code,sf_code,LEFT(Prod_code + '~', CHARINDEX('~', Prod_code + '~') - 1)Product_Code_SlNo ,replace((LEFT(issue_Qty + '$', CHARINDEX('$', issue_Qty + '$') - 1)), '#', '')issue_befor into #DCR_Detail_Issue_TRANS2 from #Drs_Prod_Bfr_Filtr select Trans_SlNo, Product_Detail_Name, issue_befor from #DCR_Detail_Issue_TRANS2 a,Mas_Product_Detail b where a.Product_Code_SlNo=b.Product_Code_SlNo drop table #Drs_Prod_Bfr drop table #Drs_Prod_Bfr_Filtr drop table #DCR_Detail_Issue_TRANS2";
        // " CASE listeddr_visit_days " +
        //" WHEN '" + dayshort + "' THEN '" + day + "' else 5 END";

        SqlCommand cmd = new SqlCommand(query);
        return GetData(cmd).GetXml();
    }
    private static DataSet GetData(SqlCommand cmd)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    return ds;

                }
            }
        }
    }
}
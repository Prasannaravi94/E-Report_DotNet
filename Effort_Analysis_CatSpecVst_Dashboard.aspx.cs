using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Web.Configuration;
using DBase_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Script.Services;
public partial class Effort_Analysis_CatSpecVst_Dashboard : System.Web.UI.Page
{

    #region All
    public SqlConnection con1;
    public SqlCommand com;
    string constr;
    string div_code = string.Empty;
    static string divcode = string.Empty;
    string Process_type = string.Empty;
    string sfcode = string.Empty;
    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    DataSet dsDesig = new DataSet();
    string subdiv = string.Empty;
    string[] Distinct;
    string[] strsplit;
    string sSub = string.Empty;
    static string sSf_Code = string.Empty;
    static string sDivCode = string.Empty;
    static string smonth = string.Empty;
    static string syear = string.Empty;
    static string tmonth = string.Empty;
    static string tyear = string.Empty;
    static string mode = string.Empty;
    static string sSfName = string.Empty;
    static string FMName = string.Empty;
    static string spec = string.Empty;
    static string SubDiv_Code = string.Empty;
    static string MGR_Code = string.Empty;



    string sf_code = string.Empty;
    string Trans_month_year = string.Empty;
    string cs = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
    SqlConnection con = new SqlConnection();
    SqlDataAdapter adapt;
    DataTable dt;
    DataSet dsLogin = null;
    DataSet dsSalesForce = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsAdmin = null;
    DataSet dsImage = new DataSet();
    DataSet dsBirth = new DataSet();
    DataSet dsImage_FF = new DataSet();
    DataSet dsAdm = null;
    DataSet dsadmn = null;
    DataSet dsAdmNB = null;
    DataSet dsLogin1 = null;
    DataSet dspwd = new DataSet();
    DataSet dsDoc = null;
    DataSet dsFeed = new DataSet();
    DataSet dsVacant = new DataSet();
    DataSet dssample = null;
    DataSet dsinput = null;
    string pwdDt = string.Empty;
    string month = string.Empty;
    string strUserName = string.Empty;
    string strPassword = string.Empty;
    int time;
    int tp_month4;
    string from_year = string.Empty;
    int from_year2;
    int next_month4;
    int to_year2;
    string to_year = string.Empty;
    DataSet dsTp = null;

    private DataSet dsinput1;
    DataSet dsquiz = new DataSet();


    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();

        if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
        {
            div_code = Session["division_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }

        if (!Page.IsPostBack)
        {
            Session["AdminDashHome"] = "Dash";
            Session["Dashpgload"] = "Onload";

            Filldiv();
            if (lstDiv.SelectedValue.ToString() == "ALL")
            {
                Fill_HO_ID_Name();
            }
            else
            {
                FillMRManagers();

                //btnGoBrand.Visible = false;
            }
            if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
            {
                if (Session["Sub_HO_ID"].ToString() == "0")
                {
                    btnHome.Visible = false;
                    libtnHome.Visible = false;
                    liback.Visible = true;
                    btnback.Visible = true;
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('chrt1').style.visibility = 'hidden'; </script>");
                }
                else
                {
                    btnHome.Visible = true;
                    libtnHome.Visible = true;
                    liback.Visible = false;
                    btnback.Visible = false;
                    //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('btnback').style.visibility = 'hidden'; </script>");
                    //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:GetTimeZoneOffset(); ", true);
                }
            }
            else
            {
                btnHome.Visible = true;
                libtnHome.Visible = true;

                liback.Visible = false;
                btnback.Visible = false;
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('btnback').style.visibility = 'hidden'; </script>");
            }

            BindDate();
            FillSpec();
        }
        else
        {
            Session["Dashpgload"] = "";
        }

        if (Request.QueryString["AppDashboard"] != null)
        {
            btnback.Visible = false;
            btnHome.Visible = false;
            btnLogout.Visible = false;
        }
    }


    private void BindDate()
    {
        TourPlan tp = new TourPlan();
        DataSet dsTP = new DataSet();
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
        {
            if (Session["Sub_HO_ID"].ToString() == "0")
            {
                dsTP = tp.Get_TP_Edit_Year(lstDiv.SelectedValue);
            }
            else
            {
                DB_EReporting db = new DB_EReporting();
                string strQry = "";

                strQry = "select max([Year]-1) as Year from Mas_Division where charindex(','+cast(Division_Code as varchar)+',',','+ '" + div_code + "') >0 ";
                dsTP = db.Exec_DataSet(strQry);
            }
        }
        else
        {
            dsTP = tp.Get_TP_Edit_Year(lstDiv.SelectedValue);
        }
        //dsTP = tp.Get_TP_Edit_Year_muldiv(div_code);

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlYear.Items.Add(k.ToString());
            }

            ddlYear.Text = DateTime.Now.Year.ToString();
            //ddlFrmMonth.SelectedValue = DateTime.Today.AddMonths(-1).Month.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
    }


    private void FillSpec()
    {
        ListedDR lst = new ListedDR();
        DataSet dsspec = new DataSet();
        DB_EReporting db = new DB_EReporting();
        string strQry = "";
        if (lstMode.SelectedValue.ToString() == "2")
        {
            if (lstDiv.SelectedValue.ToString() == "ALL")
            {
                strQry = " select distinct Doc_Special_Code,Doc_Special_SName from Mas_Doctor_Speciality where charindex(cast(division_code as varchar),'" + lstFieldForce.SelectedValue + "'+',') >0 and Doc_Special_Active_Flag=0 ";
            }
            else
            {
                strQry = " select distinct Doc_Special_Code,Doc_Special_SName from Mas_Doctor_Speciality where charindex(cast(division_code as varchar),'" + lstDiv.SelectedValue + "'+',') >0 and Doc_Special_Active_Flag=0 ";
            }
            dsspec = db.Exec_DataSet(strQry);

            if (dsspec.Tables[0].Rows.Count > 0)
            {
                lstSpec.DataTextField = "Doc_Special_SName";
                lstSpec.DataValueField = "Doc_Special_Code";
                lstSpec.DataSource = dsspec;
                lstSpec.DataBind();
            }
        }
        else if (lstMode.SelectedValue.ToString() == "3")
        {
            if (lstDiv.SelectedValue.ToString() == "ALL")
            {
                strQry = " select distinct  Doc_SubCatCode,Doc_SubCatSName from Mas_Doc_SubCategory where charindex(cast(division_code as varchar),'" + lstFieldForce.SelectedValue + "'+',') >0 and Doc_SubCat_ActiveFlag=0 ";
            }
            else
            {
                strQry = " select distinct Doc_SubCatCode,Doc_SubCatSName from Mas_Doc_SubCategory where charindex(cast(division_code as varchar),'" + lstDiv.SelectedValue + "'+',') >0 and Doc_SubCat_ActiveFlag=0 ";
            }
            dsspec = db.Exec_DataSet(strQry);

            if (dsspec.Tables[0].Rows.Count > 0)
            {
                lstSpec.DataTextField = "Doc_SubCatSName";
                lstSpec.DataValueField = "Doc_SubCatCode";
                lstSpec.DataSource = dsspec;
                lstSpec.DataBind();
            }
        }
    }

    public void lstDiv_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        if (lstDiv.SelectedValue.ToString() == "ALL")
        {
            Fill_HO_ID_Name();
        }
        else
        {
            FillMRManagers();
        }
        FillSpec();
    }

    public void lstMode_SelectedIndexChanged(object sender, System.EventArgs e)
    {

        if (lstMode.SelectedValue.ToString() == "1")
        {
            lstSpec.Visible = false;
        }
        else if (lstMode.SelectedValue.ToString() == "2")
        {
            lstSpec.Visible = true;
            FillSpec();
        }
        else if (lstMode.SelectedValue.ToString() == "3")
        {
            lstSpec.Visible = true;
            FillSpec();
        }
        else if (lstMode.SelectedValue.ToString() == "4")
        {
            lstSpec.Visible = false;
        }

    }






    public string[] RemoveDuplicates(string[] inputArray)
    {

        int length = inputArray.Length;
        for (int i = 0; i < length; i++)
        {
            for (int j = (i + 1); j < length;)
            {
                if (inputArray[i] == inputArray[j])
                {
                    for (int k = j; k < length - 1; k++)
                        inputArray[k] = inputArray[k + 1]; length--;
                }
                else
                    j++;
            }
        }

        string[] distinctArray = new string[length];
        for (int i = 0; i < length; i++)
            distinctArray[i] = inputArray[i];

        return distinctArray;

    }




    private void FillMRManagers()
    {
        SalesForce sf = new SalesForce();
        //AllFieldforce_Novacant
        //DataSet dsSalesForce = sf.SalesForceListMgrGet(lstDiv.SelectedValue.ToString(), Session["sf_code"].ToString());
        DataSet dsSalesForce = sf.AllFieldforce_Novacant(lstDiv.SelectedValue.ToString(), Session["sf_code"].ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lstFieldForce.DataTextField = "sf_name";
            lstFieldForce.DataValueField = "sf_code";
            lstFieldForce.DataSource = dsSalesForce;
            lstFieldForce.DataBind();

            lstFieldForce.SelectedIndex = 0;
        }
        else
        {
            //lstFieldForce.DataTextField = "sf_name";
            //lstFieldForce.DataValueField = "sf_code";
            lstFieldForce.DataSource = null;
            lstFieldForce.DataBind();
        }
    }



    private void Fill_HO_ID_Name()
    {
        SalesForce sf = new SalesForce();

        DataSet dsSalesForce = null;
        //AllFieldforce_Novacant
        //DataSet dsSalesForce = sf.SalesForceListMgrGet(lstDiv.SelectedValue.ToString(), Session["sf_code"].ToString());
        if (Session["Sub_HO_ID"].ToString() == "0")
        {
            dsSalesForce = sf.Get_HO_ID_Name_Team(Session["HO_ID"].ToString());
        }
        else
        {
            dsSalesForce = sf.Get_HO_ID_Name(Session["HO_ID"].ToString());
        }

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lstFieldForce.DataTextField = "HO_Name";
            lstFieldForce.DataValueField = "Division_Code";
            lstFieldForce.DataSource = dsSalesForce;
            lstFieldForce.DataBind();

            lstFieldForce.SelectedIndex = 0;


        }
        else
        {
            //lstFieldForce.DataTextField = "sf_name";
            //lstFieldForce.DataValueField = "sf_code";
            lstFieldForce.DataSource = null;
            lstFieldForce.DataBind();
        }

    }



    private void Filldiv()
    {
        Division dv = new Division();
        if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
        {
            string[] strDivSplit = div_code.Split(',');
            Array.Sort(strDivSplit);
            //lstDiv.Items.Add("ALL");

            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    DataSet dsdiv = dv.getDivisionHO(strdiv);
                    System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    lstDiv.Items.Add(liTerr);

                    //lstDiv_new.Items.Add(liTerr);
                    //lstDiv_new.Visible = true;
                }
            }
            if (Session["Sub_HO_ID"].ToString() == "0")
            {
                lstDiv.SelectedIndex = 2;
            }
            else
            {
                lstDiv.SelectedIndex = 1;
            }
            //lstDiv_new.SelectedIndex = 2;
        }
        else
        {
            DataSet dsDivision = dv.getDivisionHO(div_code);
            //DataSet dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                lstDiv.DataTextField = "Division_Name";
                lstDiv.DataValueField = "Division_Code";
                lstDiv.DataSource = dsDivision;
                lstDiv.DataBind();

                lstDiv.SelectedIndex = 0;


                //btnGoBrand.Visible = false;
            }
        }
    }



    [WebMethod(EnableSession = true)]
    public static string CatWise(string objData)
    {
        //string div_code = HttpContext.Current.Session["div_code"].ToString();

        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];

        sSf_Code = arr[2];
        mode = arr[3];



        string[] strDivSplit = (sSf_Code).Split(',');

        int div_cnt = strDivSplit.Count() - 1;


        string div_code = arr[4];

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            DataTable sortedDT = new DataTable();

            SqlCommand cmd = new SqlCommand();

            if (div_code == "ALL")
            {
                cmd = new SqlCommand("Effort_Analysis_Dashboard_CatSpecl", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", sSf_Code);
                cmd.Parameters.AddWithValue("@Msf_code", "admin");
                //cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);

                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
            }
            else
            {
                cmd = new SqlCommand("Effort_Analysis_Dashboard_CatSpecl", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(div_code + ","));
                cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
            }
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

            DataView dv = dt.DefaultView;
            //dv.Sort = "cnt desc";
            sortedDT = dv.ToTable();
            con.Close();
            sortedDT.Columns["Cat_Name"].ColumnName = "Cat_Name";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Doc_Cat_Code"].ColumnName = "Doc_Cat_Code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Division_code"].ColumnName = "Division_code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Tot_drs"].ColumnName = "Tot_drs";
            sortedDT.AcceptChanges();
            sortedDT.Columns["1_Vst"].ColumnName = "Visit1";
            sortedDT.AcceptChanges();

            sortedDT.Columns["2_Vst"].ColumnName = "Visit2";
            sortedDT.AcceptChanges();
            sortedDT.Columns["3_Vst"].ColumnName = "Visit3";
            sortedDT.AcceptChanges();

            sortedDT.Columns["4_Vst"].ColumnName = "Visit0";
            sortedDT.AcceptChanges();


            sortedDT.Columns["1Vst_Avg"].ColumnName = "Visit1Avg";
            sortedDT.AcceptChanges();
            sortedDT.Columns["2Vst_Avg"].ColumnName = "Visit2Avg";
            sortedDT.AcceptChanges();
            sortedDT.Columns["3Vst_Avg"].ColumnName = "Visit3Avg";
            sortedDT.AcceptChanges();
            sortedDT.Columns["4Vst_Avg"].ColumnName = "Visit0Avg";
            sortedDT.AcceptChanges();

            string jsonResult = JsonConvert.SerializeObject(sortedDT);

            return jsonResult;

        }
    }




    [WebMethod(EnableSession = true)]
    public static string SpecltyWise(string objData)
    {
        //string div_code = HttpContext.Current.Session["div_code"].ToString();
        string[] arr = objData.Split('^');

        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];

        string[] strDivSplit = (sSf_Code).Split(',');
        int div_cnt = strDivSplit.Count() - 1;
        string div_code = arr[4];

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            DataTable sortedDT = new DataTable();
            //if (mode == "1")
            //{
            SqlCommand cmd = new SqlCommand();
            if (div_code == "ALL")
            {
                cmd = new SqlCommand("Effort_Analysis_Dashboard_SpeclWise", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", sSf_Code);
                cmd.Parameters.AddWithValue("@Msf_code", "admin");
                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
                cmd.Parameters.AddWithValue("@Mode", arr[7]);
                cmd.Parameters.AddWithValue("@Spec_Code", arr[6]);
            }
            else
            {
                cmd = new SqlCommand("Effort_Analysis_Dashboard_SpeclWise", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(div_code + ","));
                cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
                cmd.Parameters.AddWithValue("@Mode", arr[7]);
                cmd.Parameters.AddWithValue("@Spec_Code", arr[6]);
            }
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

            DataView dv = dt.DefaultView;
            dv.Sort = "cnt desc";
            sortedDT = dv.ToTable();
            con.Close();
            sortedDT.Columns["Spec_Name"].ColumnName = "Label";
            sortedDT.AcceptChanges();
            sortedDT.Columns["cnt"].ColumnName = "Value";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Avrg"].ColumnName = "cnt";
            sortedDT.AcceptChanges();

            sortedDT.Columns["Doc_Special_Code"].ColumnName = "Doc_Special_Code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Division_Code"].ColumnName = "Division_Code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Totdrs"].ColumnName = "Totdrs";
            sortedDT.AcceptChanges();
            sortedDT.Columns["miss"].ColumnName = "miss";
            sortedDT.AcceptChanges();

            string jsonResult = JsonConvert.SerializeObject(sortedDT);
            return jsonResult;
        }
    }
    #endregion
    [WebMethod(EnableSession = true)]
    public static string CampWise(string objData)
    {
        //string div_code = HttpContext.Current.Session["div_code"].ToString();
        string[] arr = objData.Split('^');

        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];

        string[] strDivSplit = (sSf_Code).Split(',');
        int div_cnt = strDivSplit.Count() - 1;
        string div_code = arr[4];

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            DataTable sortedDT = new DataTable();
            //if (mode == "1")
            //{
            SqlCommand cmd = new SqlCommand();
            if (div_code == "ALL")
            {
                cmd = new SqlCommand("Effort_Analysis_Dashboard_CampWise", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", sSf_Code);
                cmd.Parameters.AddWithValue("@Msf_code", "admin");
                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
                cmd.Parameters.AddWithValue("@Mode", arr[7]);
                cmd.Parameters.AddWithValue("@Camp_Code", arr[6]);
            }
            else
            {
                cmd = new SqlCommand("Effort_Analysis_Dashboard_CampWise", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(div_code + ","));
                cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
                cmd.Parameters.AddWithValue("@Mode", arr[7]);
                cmd.Parameters.AddWithValue("@Camp_Code", arr[6]);
            }
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

            DataView dv = dt.DefaultView;
            dv.Sort = "cnt desc";
            sortedDT = dv.ToTable();
            con.Close();
            sortedDT.Columns["Camp_Name"].ColumnName = "Label";
            sortedDT.AcceptChanges();
            sortedDT.Columns["cnt"].ColumnName = "Value";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Avrg"].ColumnName = "cnt";
            sortedDT.AcceptChanges();
            sortedDT.Columns["miss"].ColumnName = "miss";
            sortedDT.AcceptChanges();

            sortedDT.Columns["Doc_Special_Code"].ColumnName = "Doc_Special_Code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Division_Code"].ColumnName = "Division_Code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Totdrs"].ColumnName = "Totdrs";
            sortedDT.AcceptChanges();

            string jsonResult = JsonConvert.SerializeObject(sortedDT);
            return jsonResult;
        }
    }
    [WebMethod(EnableSession = true)]
    public static string MVDWise(string objData)
    {
        //string div_code = HttpContext.Current.Session["div_code"].ToString();
        string[] arr = objData.Split('^');

        smonth = arr[0];
        syear = arr[1];
        sSf_Code = arr[2];
        mode = arr[3];

        string[] strDivSplit = (sSf_Code).Split(',');
        int div_cnt = strDivSplit.Count() - 1;
        string div_code = arr[4];

        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            DataTable sortedDT = new DataTable();
            //if (mode == "1")
            //{
            SqlCommand cmd = new SqlCommand();
            if (div_code == "ALL")
            {
                cmd = new SqlCommand("Effort_Analysis_Dashboard_MVDWise", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", sSf_Code);
                cmd.Parameters.AddWithValue("@Msf_code", "admin");
                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
               // cmd.Parameters.AddWithValue("@Mode", arr[7]);
             //   cmd.Parameters.AddWithValue("@Camp_Code", arr[6]);
            }
            else
            {
                cmd = new SqlCommand("Effort_Analysis_Dashboard_MVDWise", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", div_code);
                cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
              //  cmd.Parameters.AddWithValue("@Mode", arr[7]);
             //   cmd.Parameters.AddWithValue("@Camp_Code", arr[6]);
            }
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.SelectCommand = cmd;
            // DataTable dt = new DataTable();
            da.Fill(dt);

            DataView dv = dt.DefaultView;
            dv.Sort = "1_cnt desc";
            sortedDT = dv.ToTable();
            con.Close();
            sortedDT.Columns["Sf_Name"].ColumnName = "Label";
            sortedDT.AcceptChanges();
            sortedDT.Columns["1_cnt"].ColumnName = "Value";
            sortedDT.AcceptChanges();
            sortedDT.Columns["2_Avrg"].ColumnName = "cnt";
            sortedDT.AcceptChanges();
            sortedDT.Columns["3_miss"].ColumnName = "miss";
            sortedDT.AcceptChanges();

            sortedDT.Columns["sfcode"].ColumnName = "sfcode";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Division_Code"].ColumnName = "Division_Code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["0_Totdrs"].ColumnName = "Totdrs";
            sortedDT.AcceptChanges();

            string jsonResult = JsonConvert.SerializeObject(sortedDT);
            return jsonResult;
        }
    }
    private void BindImage()
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath from Mas_LoginPage_Image", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        //DataList1.DataSource = ds;
        //DataList1.DataBind();
    }

    private void BindImage1()
    {
        div_code = Session["div_code"].ToString();

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  DataSet dsImage = new DataSet();
        da.Fill(dsImage);
        con.Close();

    }
    private void BindImage_FieldForce()
    {

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }


        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath from Mas_HomeImage_FieldForce where sf_code='" + sf_code + "' and Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsImage_FF);
        con.Close();
    }


    protected void btnback_click(object sender, EventArgs e)
    {
        Response.Redirect("Sales_DashBoard_Admin_Brand.aspx");
    }

    protected void btngohome_click(object sender, EventArgs e)
    {
        #region MR
        if (Session["sf_type"].ToString() == "1") // MR Login
        {

            UserLogin astp = new UserLogin();
            int iRet = astp.Login_details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
            AdminSetup admin = new AdminSetup();
            dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
            AdminSetup adm_Nb = new AdminSetup();
            dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
            //SalesForce sf_img = new SalesForce();
            //dsImage_FF = sf_img.Sales_Image(Session["div_code"].ToString(), Session["sf_code"].ToString());
            ListedDR lstDr = new ListedDR();
            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            AdminSetup dv = new AdminSetup();
            dsadmn = dv.getHome_Dash_Display(div_code);

            //ListedDR LstDoc = new ListedDR();
            //dsDoc = LstDoc.getLstdDr_Wrng_CreationFFWise(sf_code, div_code);

            AdminSetup adminsa_ip = new AdminSetup();


            dssample = adminsa_ip.getsampleEntry(Session["div_code"].ToString());

            if (dssample.Tables[0].Rows.Count > 0)
            {
                AdminSetup adm1 = new AdminSetup();
                dssample = adm1.getsample(Session["sf_code"].ToString(), Session["div_code"].ToString());
                if (dssample.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("Sample_Input_Acknowledge.aspx");
                }
                else
                {

                }

            }

            dsinput = adminsa_ip.getinputEntry(Session["div_code"].ToString());
            if (dsinput.Tables[0].Rows.Count > 0)
            {

                AdminSetup adm1 = new AdminSetup();
                dsinput1 = adm1.getinput(Session["sf_code"].ToString(), Session["div_code"].ToString());
                if (dsinput1.Tables[0].Rows.Count > 0)
                {
                    System.Threading.Thread.Sleep(time);
                    Response.Redirect("~/Input_Acknowlege.aspx");
                }
                else
                {

                }

            }

            #region tp_validate

            //DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
            //string sMonthName = dtDate.ToString("MMM");
            //TP_New tp = new TP_New();
            //DataSet dsTP = new DataSet();
            ////dsTP = tp.get_TP_Active_Date_New_Index(sf_code);
            //dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);
            //DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
            //string sMonthName1 = dtDate1.ToString();

            //string tp_month = dtDate1.ToString("MMM");

            //string tp_month1 = dtDate1.ToString();

            //string[] tp_month3 = tp_month1.Split('/');

            //tp_month4 = Convert.ToInt32(tp_month3[1]);
            //from_year = tp_month3[2];
            //from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


            //string next_month1 = DateTime.Now.AddMonths(1).ToString();

            //string[] next_month2 = next_month1.Split('/');

            //next_month4 = Convert.ToInt32(next_month2[1]);
            //to_year = next_month2[2];
            //to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

            //string Current_month = DateTime.Now.Month.ToString("MMM");
            //string next_month = DateTime.Now.AddMonths(1).ToString("MMM");


            //dsTp = adm.chk_tpbasedsystem_MR(div_code);

            ////if (dsTp.Tables[0].Rows.Count > 0)
            ////{
            //if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
            //{
            //    AdminSetup ad = new AdminSetup();
            //    dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString()));
            //    if (dsDesig.Tables[0].Rows.Count > 0)
            //    {

            //        if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
            //        {
            //            int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            //            int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

            //            DateTime dt = DateTime.Now;
            //            int day = dt.Day;
            //            int curr_month = dt.Month;
            //            int curr_year = dt.Year;

            //            if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
            //            {

            //                var now = DateTime.Now;
            //                var startOfMonth = new DateTime(now.Year, now.Month, 1);
            //                var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            //                var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


            //                if (startdate <= day && enddate >= day)
            //                {
            //                    int iReturn = -1; TP_New tp_new = new TP_New();
            //                    bool isRepSt = false;
            //                    DataSet dsReject = new DataSet();

            //                    if (tp_month4 == curr_month && from_year2 == curr_year)
            //                    {
            //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());

            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

            //                            if (isRepSt == false)
            //                            {
            //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                            }
            //                            else
            //                            {
            //                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

            //                                if (isRepSt == false)
            //                                {
            //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



            //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

            //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                    if (iReturn > 0)
            //                                    {
            //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
            //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {

            //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

            //                            if (isRepSt == false)
            //                            {
            //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                //string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

            //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                if (iReturn > 0)
            //                                {
            //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
            //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
            //                                }
            //                            }

            //                        }
            //                    }
            //                }
            //                else if (enddate <= DaysInMonth && enddate <= day)
            //                {
            //                    int iReturn = -1; TP_New tp_new = new TP_New();
            //                    bool isRepSt = false;
            //                    DataSet dsReject = new DataSet();


            //                    if (tp_month4 == curr_month && from_year2 == curr_year)
            //                    {
            //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

            //                            if (isRepSt == false)
            //                            {
            //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                            }
            //                            else
            //                            {
            //                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

            //                                if (isRepSt == false)
            //                                {
            //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

            //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                    if (iReturn > 0)
            //                                    {
            //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {

            //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

            //                            if (isRepSt == false)
            //                            {
            //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

            //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                if (iReturn > 0)
            //                                {
            //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
            //                                }
            //                            }


            //                        }
            //                    }
            //                }
            //                else
            //                {

            //                    var today = DateTime.Today;
            //                    var month = new DateTime(today.Year, today.Month, 1);
            //                    var first = month.AddMonths(-1);
            //                    var last_month_date = month.AddDays(-1);

            //                    int iReturn = -1; TP_New tp_new = new TP_New();
            //                    bool isRepSt = false;
            //                     DataSet dsReject = new DataSet();

            //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }

            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

            //                            if (isRepSt == false)
            //                            {

            //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

            //                                if (tp_month4 == curr_month && from_year2 == curr_year)
            //                                {
            //                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                                }
            //                                else
            //                                {
            //                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
            //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                    if (iReturn > 0)
            //                                    {
            //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                                    }
            //                                }
            //                            }

            //                        }
            //                }


            //            }

            //        }



            //    }


            //}

            #endregion



            DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
            string sMonthName = dtDate.ToString("MMM");
            TP_New tp = new TP_New();
            DataSet dsTP = new DataSet();
            // dsTP = tp.get_TP_Active_Date_New_Index(sf_code);

            dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);



            DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
            string sMonthName1 = dtDate1.ToString();

            string tp_month = dtDate1.ToString("MMM");

            string tp_month1 = dtDate1.ToString();

            string[] tp_month3 = tp_month1.Split('/');

            tp_month4 = Convert.ToInt32(tp_month3[1]);
            from_year = tp_month3[2];
            from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


            string next_month1 = DateTime.Now.AddMonths(1).ToString();

            string[] next_month2 = next_month1.Split('/');

            next_month4 = Convert.ToInt32(next_month2[1]);
            to_year = next_month2[2];
            to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

            string Current_month = DateTime.Now.Month.ToString("MMM");
            string next_month = DateTime.Now.AddMonths(1).ToString("MMM");

            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-1);
            var last_month_date = month.AddDays(-1);



            var threemth_back = month.AddMonths(-3);
            DateTime three_month_ago = Convert.ToDateTime(threemth_back);
            DateTime three_mnth_ago_lst_date = new DateTime(three_month_ago.Year, three_month_ago.Month, 1).AddMonths(1).AddDays(-1);

            var twomth_back = month.AddMonths(-2);
            DateTime two_month_ago = Convert.ToDateTime(twomth_back);
            DateTime two_mnth_ago_lst_date = new DateTime(two_month_ago.Year, two_month_ago.Month, 1).AddMonths(1).AddDays(-1);

            int two_mnt = two_mnth_ago_lst_date.Month;
            int tw_year = two_mnth_ago_lst_date.Year;

            int one_mnt = last_month_date.Month;
            int onemth_year = last_month_date.Year;


            dsTp = adm.chk_tpbasedsystem_MR(div_code);

            //if (dsTp.Tables[0].Rows.Count > 0)
            //{
            if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
            {
                AdminSetup ad = new AdminSetup();
                dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString()));
                if (dsDesig.Tables[0].Rows.Count > 0)
                {

                    if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
                    {
                        int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                        DateTime dt = DateTime.Now;
                        int day = dt.Day;
                        int curr_month = dt.Month;
                        int curr_year = dt.Year;

                        if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
                        {

                            var now = DateTime.Now;
                            var startOfMonth = new DateTime(now.Year, now.Month, 1);
                            var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                            var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


                            if (startdate <= day && enddate >= day)//range
                            {
                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;

                                DataSet dsReject = new DataSet();


                                ///start here for check last two months check
                                if (tp_month4 == two_mnt && from_year2 == tw_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                        if (tp_month4 == two_mnt && from_year2 == tw_year)
                                        {
                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                        }

                                    }


                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                            }
                                        }
                                        else
                                        {

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                        // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                    }
                                    else
                                    {

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());

                                        }
                                        else
                                        {


                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                else if (tp_month4 == curr_month && from_year2 == curr_year)
                                {

                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp


                                    if (isRepSt == false)
                                    {
                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                    }
                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                            }
                                        }
                                    }

                                }

                                else
                                {

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                    }
                                    else
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                            }
                                        }
                                    }

                                }
                            }//end of range
                            else if (enddate <= DaysInMonth && enddate <= day)
                            {
                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;
                                DataSet dsReject = new DataSet();


                                ///start here for check last two months check
                                if (tp_month4 == two_mnt && from_year2 == tw_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                        if (tp_month4 == two_mnt && from_year2 == tw_year)
                                        {
                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                        }

                                    }

                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                            }
                                        }
                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {

                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                        // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                    }
                                    else
                                    {

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                        }
                                        else
                                        {

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {

                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }

                                else
                                {

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                    }
                                    else
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                            string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                            }
                                        }
                                    }
                                }


                            }
                            else
                            {



                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;
                                DataSet dsReject = new DataSet();

                                dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                if (dsReject.Tables[0].Rows.Count > 0)
                                {
                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                }

                                else
                                {

                                    ///start here for check last two months check
                                    if (tp_month4 == two_mnt && from_year2 == tw_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                            if (tp_month4 == two_mnt && from_year2 == tw_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                            }

                                        }

                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                            }

                                        }

                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                if (tp_month4 == curr_month && from_year2 == curr_year)
                                                {
                                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                                else
                                                {
                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                            if (tp_month4 == curr_month && from_year2 == curr_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                            }
                                            else
                                            {
                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                        }
                                    }
                                }
                                //end here


                            }


                        }

                    }



                }


            }

            //if (sMonthName1.Substring(2, 9) == DateTime.Now.Date.ToString().Substring(2, 9) && (Session["div_code"].ToString() == "23" || Session["div_code"].ToString() == "21"))
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //}
            // Response.Redirect("Sale_Dashboard.aspx");
            AdminSetup admquiz = new AdminSetup();
            dsquiz = admquiz.Get_Quiz_Process(Session["div_code"].ToString(), Session["sf_code"].ToString());
            if (dsquiz.Tables[0].Rows.Count > 0)
            {
                if (dsquiz.Tables[0].Rows[0]["res"].ToString() == "1" || dsquiz.Tables[0].Rows[0]["res"].ToString() == null || dsquiz.Tables[0].Rows[0]["res"].ToString() == "")
                {
                    Response.Redirect("Cover_Page.aspx?Survey_Id=" + dsquiz.Tables[0].Rows[0]["Survey_Id"].ToString() + " &res=" + dsquiz.Tables[0].Rows[0]["res"].ToString() + "");
                }
            }
            if (Count != 0)
            {
                System.Threading.Thread.Sleep(time);
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");

            }
            //else if (dsImage.Tables[0].Rows.Count > 0)
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Response.Redirect("HomePage_Image.aspx");
            //}
            //else if (dsImage_FF.Tables[0].Rows.Count > 0)
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Server.Transfer("HomePage_FieldForcewise.aspx");

            //}
            else if (dsAdmin.Tables[0].Rows.Count > 0)
            {
                System.Threading.Thread.Sleep(time);
                Server.Transfer("Quote_Design.aspx");

            }
            else if (dsAdmNB.Tables[0].Rows.Count > 0)
            {
                System.Threading.Thread.Sleep(time);
                Server.Transfer("NoticeBoard_design.aspx");

            }
            else if (dsAdm.Tables[0].Rows.Count > 0)
            {
                System.Threading.Thread.Sleep(time);
                Server.Transfer("FlashNews_Design.aspx");
            }
            else if (dsadmn.Tables[0].Rows.Count > 0 && dsadmn.Tables[0].Rows[0]["DOB_DOW"].ToString() == "1")
            {
                System.Threading.Thread.Sleep(time);
                Response.Redirect("DOB_DOW_ListedDr.aspx");
            }
            //else if (dsDoc.Tables[0].Rows.Count > 0)
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Response.Redirect("Wrong_Creation.aspx");
            //}
            else if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                System.Threading.Thread.Sleep(time);
                Response.Redirect("Birthday_Wish.aspx");
            }

            else
            {
                System.Threading.Thread.Sleep(time);
                Server.Transfer("~/Default_MR.aspx");
            }
        }

        #endregion

        #region MGR
        else if (Session["sf_type"].ToString() == "2") // MGR Login
        {
            UserLogin astp = new UserLogin();
            int iRet = astp.Login_details(Session["sf_code"].ToString(), Session["sf_name"].ToString(), Session["div_code"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
            AdminSetup admin = new AdminSetup();
            dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
            AdminSetup adm_Nb = new AdminSetup();
            dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
            ListedDR lstDr = new ListedDR();
            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            #region tp_validate

            //TP_New tp = new TP_New();
            //DataSet dsTP = new DataSet();

            //dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);

            //DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
            //string sMonthName1 = dtDate1.ToString();

            //string tp_month = dtDate1.ToString("MMM");

            //string tp_month1 = dtDate1.ToString();

            //string[] tp_month3 = tp_month1.Split('/');

            //tp_month4 = Convert.ToInt32(tp_month3[1]);
            //from_year = tp_month3[2];
            //from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


            //string next_month1 = DateTime.Now.AddMonths(1).ToString();

            //string[] next_month2 = next_month1.Split('/');

            //next_month4 = Convert.ToInt32(next_month2[1]);
            //to_year = next_month2[2];
            //to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

            //string Current_month = DateTime.Now.Month.ToString("MMM");
            //string next_month = DateTime.Now.AddMonths(1).ToString("MMM");


            //dsTp = adm.chk_tpbasedsystem_MGR(div_code);

            ////if (dsTp.Tables[0].Rows.Count > 0)
            ////{
            //if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
            //{
            //    AdminSetup ad = new AdminSetup();
            //    dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString()));
            //    if (dsDesig.Tables[0].Rows.Count > 0)
            //    {

            //        if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
            //        {
            //            int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
            //            int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

            //            DateTime dt = DateTime.Now;
            //            int day = dt.Day;
            //            int curr_month = dt.Month;
            //            int curr_year = dt.Year;

            //            if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
            //            {

            //                var now = DateTime.Now;
            //                var startOfMonth = new DateTime(now.Year, now.Month, 1);
            //                var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            //                var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


            //                if (startdate <= day && enddate >= day)
            //                {
            //                    int iReturn = -1; TP_New tp_new = new TP_New();
            //                    bool isRepSt = false;
            //                    DataSet dsReject = new DataSet();


            //                    if (tp_month4 == curr_month && from_year2 == curr_year)
            //                    {
            //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

            //                            if (isRepSt == false)
            //                            {
            //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                            }
            //                            else
            //                            {

            //                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

            //                                if (isRepSt == false)
            //                                {
            //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                    //string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

            //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                    if (iReturn > 0)
            //                                    {
            //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
            //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {

            //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

            //                            if (isRepSt == false)
            //                            {
            //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                //string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

            //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                if (iReturn > 0)
            //                                {
            //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
            //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
            //                                }
            //                            }
            //                        }

            //                    }
            //                }
            //                else if (enddate <= DaysInMonth && enddate <= day)
            //                {
            //                    int iReturn = -1; TP_New tp_new = new TP_New();
            //                    bool isRepSt = false;
            //                    DataSet dsReject = new DataSet();

            //                    if (tp_month4 == curr_month && from_year2 == curr_year)
            //                    {
            //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

            //                            if (isRepSt == false)
            //                            {
            //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                            }
            //                            else
            //                            {
            //                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

            //                                if (isRepSt == false)
            //                                {
            //                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

            //                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                    if (iReturn > 0)
            //                                    {
            //                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                    else
            //                    {
            //                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                        if (dsReject.Tables[0].Rows.Count > 0)
            //                        {
            //                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                        }
            //                        else
            //                        {

            //                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

            //                            if (isRepSt == false)
            //                            {
            //                                string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

            //                                string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

            //                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

            //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                if (iReturn > 0)
            //                                {
            //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
            //                                }
            //                            }
            //                        }
            //                    }

            //                }
            //                else
            //                {

            //                    var today = DateTime.Today;
            //                    var month = new DateTime(today.Year, today.Month, 1);
            //                    var first = month.AddMonths(-1);
            //                    var last_month_date = month.AddDays(-1);

            //                    int iReturn = -1; TP_New tp_new = new TP_New();
            //                    bool isRepSt = false;
            //                    DataSet dsReject = new DataSet();

            //                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


            //                    if (dsReject.Tables[0].Rows.Count > 0)
            //                    {
            //                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
            //                    }
            //                    else
            //                    {

            //                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

            //                        if (isRepSt == false)
            //                        {

            //                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

            //                            if (tp_month4 == curr_month && from_year2 == curr_year)
            //                            {
            //                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                            }
            //                            else
            //                            {
            //                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
            //                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
            //                                if (iReturn > 0)
            //                                {
            //                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
            //                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
            //                                }
            //                            }
            //                        }
            //                    }

            //                }


            //            }

            //        }


            //    }


            //}

            #endregion

            DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
            string sMonthName = dtDate.ToString("MMM");
            TP_New tp = new TP_New();
            DataSet dsTP = new DataSet();
            // dsTP = tp.get_TP_Active_Date_New_Index(sf_code);

            dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);



            DateTime dtDate1 = Convert.ToDateTime(dsTP.Tables[0].Rows[0][0].ToString());
            string sMonthName1 = dtDate1.ToString();

            string tp_month = dtDate1.ToString("MMM");

            string tp_month1 = dtDate1.ToString();

            string[] tp_month3 = tp_month1.Split('/');

            tp_month4 = Convert.ToInt32(tp_month3[1]);
            from_year = tp_month3[2];
            from_year2 = Convert.ToInt16(from_year.Substring(0, 4));


            string next_month1 = DateTime.Now.AddMonths(1).ToString();

            string[] next_month2 = next_month1.Split('/');

            next_month4 = Convert.ToInt32(next_month2[1]);
            to_year = next_month2[2];
            to_year2 = Convert.ToInt16(to_year.Substring(0, 4));

            string Current_month = DateTime.Now.Month.ToString("MMM");
            string next_month = DateTime.Now.AddMonths(1).ToString("MMM");

            var today = DateTime.Today;
            var month = new DateTime(today.Year, today.Month, 1);
            var first = month.AddMonths(-1);
            var last_month_date = month.AddDays(-1);



            var threemth_back = month.AddMonths(-3);
            DateTime three_month_ago = Convert.ToDateTime(threemth_back);
            DateTime three_mnth_ago_lst_date = new DateTime(three_month_ago.Year, three_month_ago.Month, 1).AddMonths(1).AddDays(-1);

            var twomth_back = month.AddMonths(-2);
            DateTime two_month_ago = Convert.ToDateTime(twomth_back);
            DateTime two_mnth_ago_lst_date = new DateTime(two_month_ago.Year, two_month_ago.Month, 1).AddMonths(1).AddDays(-1);

            int two_mnt = two_mnth_ago_lst_date.Month;
            int tw_year = two_mnth_ago_lst_date.Year;

            int one_mnt = last_month_date.Month;
            int onemth_year = last_month_date.Year;


            dsTp = adm.chk_tpbasedsystem_MGR(div_code);

            //if (dsTp.Tables[0].Rows.Count > 0)
            //{
            if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
            {
                AdminSetup ad = new AdminSetup();
                dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString()));
                if (dsDesig.Tables[0].Rows.Count > 0)
                {

                    if (dsDesig.Tables[0].Rows[0][0].ToString() != "")
                    {
                        int startdate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                        int enddate = Convert.ToInt16(dsDesig.Tables[0].Rows[0].ItemArray.GetValue(1).ToString());

                        DateTime dt = DateTime.Now;
                        int day = dt.Day;
                        int curr_month = dt.Month;
                        int curr_year = dt.Year;

                        if (startdate != 0 && enddate != 0 && startdate != -1 && enddate != -1)
                        {

                            var now = DateTime.Now;
                            var startOfMonth = new DateTime(now.Year, now.Month, 1);
                            var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                            var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


                            if (startdate <= day && enddate >= day)//range
                            {
                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;

                                DataSet dsReject = new DataSet();


                                ///start here for check last two months check
                                if (tp_month4 == two_mnt && from_year2 == tw_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                        if (tp_month4 == two_mnt && from_year2 == tw_year)
                                        {
                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                        }

                                    }


                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                            }
                                        }
                                        else
                                        {

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                        // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                    }
                                    else
                                    {

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());

                                        }
                                        else
                                        {


                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }
                                else if (tp_month4 == curr_month && from_year2 == curr_year)
                                {

                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp


                                    if (isRepSt == false)
                                    {
                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                    }
                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                            }
                                        }
                                    }

                                }

                                else
                                {

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                    }
                                    else
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();



                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(lastDay_curr_mon));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "1" + "&enddate=" + enddate);
                                            }
                                        }
                                    }

                                }
                            }//end of range
                            else if (enddate <= DaysInMonth && enddate <= day)
                            {
                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;
                                DataSet dsReject = new DataSet();


                                ///start here for check last two months check
                                if (tp_month4 == two_mnt && from_year2 == tw_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                        if (tp_month4 == two_mnt && from_year2 == tw_year)
                                        {
                                            // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                        }

                                    }

                                    else
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                            }
                                        }
                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {

                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                {
                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                    if (isRepSt == false)
                                    {

                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                        // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);

                                    }
                                    else
                                    {

                                        dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                        if (dsReject.Tables[0].Rows.Count > 0)
                                        {
                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                        }
                                        else
                                        {

                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {
                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {

                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                                if (isRepSt == false)
                                                {
                                                    string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                                    string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                                    }
                                                }
                                            }
                                        }

                                    }
                                }

                                else
                                {

                                    dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                    if (dsReject.Tables[0].Rows.Count > 0)
                                    {
                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                    }
                                    else
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, next_month4, to_year2);//chk next month tp

                                        if (isRepSt == false)
                                        {
                                            string tp_date = "1" + "-" + next_month4.ToString() + "-" + to_year2.ToString();

                                            string current_month = DaysInMonth + "-" + curr_month.ToString() + "-" + curr_year.ToString();

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(current_month));

                                            iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            if (iReturn > 0)
                                            {
                                                //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=2");
                                            }
                                        }
                                    }
                                }


                            }
                            else
                            {



                                int iReturn = -1; TP_New tp_new = new TP_New();
                                bool isRepSt = false;
                                DataSet dsReject = new DataSet();

                                dsReject = tp_new.IsTpChk_Rejection(sf_code, div_code);//chk rejection


                                if (dsReject.Tables[0].Rows.Count > 0)
                                {
                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "4" + "&RejectMonth=" + dsReject.Tables[0].Rows[0]["tour_Month"].ToString() + "&RejectYear=" + dsReject.Tables[0].Rows[0]["Tour_Year"].ToString() + "&RejectMGR=" + dsReject.Tables[0].Rows[0]["Tp_Approval_MGR"].ToString());
                                }

                                else
                                {

                                    ///start here for check last two months check
                                    if (tp_month4 == two_mnt && from_year2 == tw_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, two_mnt, tw_year);//chk two month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, three_mnth_ago_lst_date);//delete or autoapprove,  up to 3 month back record

                                            if (tp_month4 == two_mnt && from_year2 == tw_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "5" + "&TwoMonth_back=" + two_mnt + "&Twomnth_year=" + tw_year);
                                            }

                                        }

                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                                string tp_date = "1" + "-" + one_mnt.ToString() + "-" + onemth_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                                }
                                            }
                                            else
                                            {
                                                isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp
                                                if (isRepSt == false)
                                                {
                                                    iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                    {
                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, one_mnt, onemth_year);//chk one month ago tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, two_mnth_ago_lst_date);//delete or autoapprove,  up to 2 month back record

                                            if (tp_month4 == one_mnt && from_year2 == onemth_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=" + "6" + "&OneMonth_back=" + one_mnt + "&Onemnth_year=" + onemth_year);
                                            }

                                        }

                                        else
                                        {
                                            isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                            if (isRepSt == false)
                                            {

                                                iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                                if (tp_month4 == curr_month && from_year2 == curr_year)
                                                {
                                                    // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                                else
                                                {
                                                    string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                    iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                    if (iReturn > 0)
                                                    {
                                                        //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                        Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                        if (isRepSt == false)
                                        {

                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                            if (tp_month4 == curr_month && from_year2 == curr_year)
                                            {
                                                // Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                            }
                                            else
                                            {
                                                string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                                iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                                if (iReturn > 0)
                                                {
                                                    //Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=8");
                                                    Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                                }
                                            }
                                        }
                                    }
                                }
                                //end here
                            }
                        }
                    }
                }
            }
            BindImage1();
            BindImage_FieldForce();
            //  Response.Redirect("Sale_Dashboard.aspx");
            AdminSetup admquiz = new AdminSetup();
            dsquiz = admquiz.Get_Quiz_Process(Session["div_code"].ToString(), Session["sf_code"].ToString());
            if (dsquiz.Tables[0].Rows.Count > 0)
            {
                if (dsquiz.Tables[0].Rows[0]["res"].ToString() == "1" || dsquiz.Tables[0].Rows[0]["res"].ToString() == null || dsquiz.Tables[0].Rows[0]["res"].ToString() == "")
                {
                    Response.Redirect("Cover_Page.aspx?Survey_Id=" + dsquiz.Tables[0].Rows[0]["Survey_Id"].ToString() + " &res=" + dsquiz.Tables[0].Rows[0]["res"].ToString() + "");
                }
            }
            if (Count != 0)
            {
                //System.Threading.Thread.Sleep(time);
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else if (dsImage.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("HomePage_Image.aspx");
            }
            else if (dsImage_FF.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("HomePage_FieldForcewise.aspx");

            }
            else if (dsAdmin.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("Quote_Design.aspx");

            }
            else if (dsAdmNB.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("NoticeBoard_design.aspx");

            }
            else if (dsAdm.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("FlashNews_Design.aspx");
            }

            else if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("Birthday_Wish.aspx");
            }

            else
            {
                Server.Transfer("~/Default_MGR.aspx");
            }
        }
        #endregion
        #region Admin
        else
        {
            if (Session["Sub_HO_ID"].ToString() != "0")
            {
                Server.Transfer("Default.aspx");
            }
            //Server.Transfer("Default_admin.aspx");
        }
        #endregion
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

    //protected void linkDetail_Click(object sender, EventArgs e)
    //{
    //    if (lstMode.SelectedValue.ToString() == "1")
    //    {
    //        linkDetail.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_Code + "', '" + SubDiv_Code + "', '" + MGR_Code + "', '" + "1" + "')");
    //    }
    //    else
    //    {
    //        linkDetail.Attributes.Add("href", "javascript:showModalPopUp('" + sSf_Code + "', '" + SubDiv_Code + "', '" + MGR_Code + "', '" + "0" + "')");
    //    }
    //}
    #region Class Values
    public class values
    {
        public string Sf_Code { get; set; }

        public decimal Target { get; set; }
        public decimal Sale { get; set; }
        public decimal achie { get; set; }
        public decimal PSale { get; set; }
        public decimal Growth { get; set; }

        public decimal PC { get; set; }

        public string P_Sf_Code { get; set; }

        public decimal P_Target { get; set; }
        public decimal P_Sale { get; set; }
        public decimal P_achie { get; set; }
        public decimal P_PSale { get; set; }
        public decimal P_Growth { get; set; }

        public decimal P_PC { get; set; }

        public string Div_Name { get; set; }

        public string Div_cnt { get; set; }

        public string Sf_Code1 { get; set; }
        public decimal Target1 { get; set; }
        public decimal Sale1 { get; set; }
        public decimal achie1 { get; set; }
        public decimal PSale1 { get; set; }
        public decimal Growth1 { get; set; }
        public decimal PC1 { get; set; }
        public string Div_Name1 { get; set; }

        public string Sf_Code2 { get; set; }
        public decimal Target2 { get; set; }
        public decimal Sale2 { get; set; }
        public decimal achie2 { get; set; }
        public decimal PSale2 { get; set; }
        public decimal Growth2 { get; set; }
        public decimal PC2 { get; set; }
        public string Div_Name2 { get; set; }

        public string Sf_Code3 { get; set; }
        public decimal Target3 { get; set; }
        public decimal Sale3 { get; set; }
        public decimal achie3 { get; set; }
        public decimal PSale3 { get; set; }
        public decimal Growth3 { get; set; }
        public decimal PC3 { get; set; }
        public string Div_Name3 { get; set; }

        public string Sf_Code4 { get; set; }
        public decimal Target4 { get; set; }
        public decimal Sale4 { get; set; }
        public decimal achie4 { get; set; }
        public decimal PSale4 { get; set; }
        public decimal Growth4 { get; set; }
        public decimal PC4 { get; set; }
        public string Div_Name4 { get; set; }
    }
    #endregion
}
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

public partial class Sales_DashBoard_Admin_Brand : System.Web.UI.Page
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
    DataSet dsvac = null;
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
    string sf_type = string.Empty;
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
            trDashRow1.Visible = false;
            trDashRow2.Visible = false;
            trDashRow3.Visible = false;
            trDashRow4.Visible = false;
            trDashRow5.Visible = false;
            Filldiv();
            if (lstDiv.SelectedValue.ToString() == "ALL")
            {
                Fill_HO_ID_Name();
                Fill_Div_Brandcontr();
                RowHideShow();
            }
            else
            {
                FillMRManagers();
                lstDiv_new.Visible = false;

                //btnGoBrand.Visible = false;
            }
            FillFinaceYr();
            if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
            {
                if (Session["Sub_HO_ID"].ToString() == "0")
                {
                    libtnHome.Visible = false;
                    btnback.Visible = true;
                    liback.Visible = true;
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('chrt1').style.visibility = 'hidden'; </script>");
                }
                else
                {
                    libtnHome.Visible = true;
                    btnback.Visible = false;
                    liback.Visible = false;
                    //ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:GetTimeZoneOffset(); ", true);
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementByIdlblback('liback').style.visibility = 'hidden';document.getElementById('chrt1').style.visibility = 'hidden';document.getElementById('btnGoBrand').style.visibility = 'visible'; </script>");
                }
            }
            else
            {
                libtnHome.Visible = true;
                btnback.Visible = false;
                liback.Visible = false;
                Common();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('liback').style.visibility = 'hidden';document.getElementById('chrt1').style.visibility = 'hidden';document.getElementById('btnGoBrand').style.visibility = 'hidden'; </script>");
            }
            //trRow.Visible = false;
        }
        else
        {
            Session["Dashpgload"] = "";
        }

        if (Request.QueryString["AppDashboard"] != null)
        {
            liback.Visible = false;
            liSFE.Visible = false;
            libtnHome.Visible = false;
            liLogout.Visible = false;
        }

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getprimary(List<string> lst_Input_Mn_Yr)
    {

        List<values> lstCoverage = new List<values>();


        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select convert(varchar,max(Created_Date),103) date   from Primary_Bill where Division_Code='" + lst_Input_Mn_Yr[0] + "'";



            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    values cpData = new values();
                    cpData.Sf_Code = dr["date"].ToString();



                    lstCoverage.Add(cpData);
                }
            }
            dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }
    public void lstDiv_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (lstDiv.SelectedValue.ToString() == "ALL")
        {
            Fill_HO_ID_Name();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('chrt1').style.visibility = 'hidden';document.getElementById('btnGoBrand').style.visibility = 'visible'; </script>");
        }
        else
        {
            lstDiv_new.Visible = false;
            trDashRow1.Visible = false;
            trDashRow2.Visible = false;
            trDashRow3.Visible = false;
            trDashRow4.Visible = false;
            trDashRow5.Visible = false;
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('chrt1').style.visibility = 'hidden';document.getElementById('btnGoBrand').style.visibility = 'hidden';  </script>");
            FillMRManagers();
        }
    }

    public void lstMode_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('chrt1').style.visibility = 'hidden'; </script>");
        if (lstMode.SelectedValue == "0")
        {
            FillFinaceYr();
            //trRow.Visible = true;
            //trRow.Visible = false;
        }
        else if (lstMode.SelectedValue == "1")
        {
            FillFinaceYr();
            //trRow.Visible = false;
        }
        else if (lstMode.SelectedValue == "2")
        {
            FillFinaceYr();
            //trRow.Visible = false;
        }
    }
    private void Common()
    {
        Session["common"] = "";
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Relevant_HQ_Code from Trans_Customized where division_code='" + lstDiv.SelectedValue + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            DB_EReporting db_ER = new DB_EReporting();
            DataSet dsmgr = new DataSet();
            string strQry2 = "Exec Common_Kolkata_Hirarchy_Self '" + ds.Tables[0].Rows[0]["Relevant_HQ_Code"].ToString() + "', '" + lstDiv.SelectedValue + "','" + lstFieldForce.SelectedValue + "' ";
            dsmgr = db_ER.Exec_DataSet(strQry2);
            if (dsmgr.Tables[0].Rows.Count > 0)
            {
                Session["common"] = dsmgr.Tables[0].Rows[0][0].ToString();
            }
        }
    }

    public void lstFieldForce_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (lstDiv.SelectedValue.ToString() == "ALL")
        {
            Fill_Div_Brandcontr();
            RowHideShow();
        }
        else
        {
            //lstDiv_new.Items.Clear();
            Session["common"] = "";
            SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("select Relevant_HQ_Code from Trans_Customized where division_code='" + lstDiv.SelectedValue + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DB_EReporting db_ER = new DB_EReporting();
                DataSet dsmgr = new DataSet();
                string strQry2 = "Exec Common_Kolkata_Hirarchy_Self '" + ds.Tables[0].Rows[0]["Relevant_HQ_Code"].ToString() + "', '" + lstDiv.SelectedValue + "','" + lstFieldForce.SelectedValue + "' ";
                dsmgr = db_ER.Exec_DataSet(strQry2);
                if (dsmgr.Tables[0].Rows.Count > 0)
                {
                    Session["common"] = dsmgr.Tables[0].Rows[0][0].ToString();
                }
            }
            lstDiv_new.Visible = false;
            trDashRow1.Visible = false;
            trDashRow2.Visible = false;
            trDashRow3.Visible = false;
            trDashRow4.Visible = false;
            trDashRow5.Visible = false;
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'> document.getElementById('btnGoBrand').style.visibility = 'hidden';  </script>");
        }
    }

    private void RowHideShow()
    {
        string[] strDivSplit = (lstFieldForce.SelectedValue.ToString()).Split(',');

        int div_cnt = strDivSplit.Count() - 1;
        Session["NofDiv"] = div_cnt.ToString();

        if (div_cnt == 2)
        {
            trDashRow1.Visible = true;
            trDashRow2.Visible = true;
            trDashRow3.Visible = false;
            trDashRow4.Visible = false;
            trDashRow5.Visible = false;
        }
        else if (div_cnt == 3)
        {
            trDashRow1.Visible = true;
            trDashRow2.Visible = true;
            trDashRow3.Visible = true;
            trDashRow4.Visible = false;
            trDashRow5.Visible = false;
        }
        else if (div_cnt == 4)
        {
            trDashRow1.Visible = true;
            trDashRow2.Visible = true;
            trDashRow3.Visible = true;
            trDashRow4.Visible = true;
            trDashRow5.Visible = false;
        }
        else if (div_cnt == 5)
        {
            trDashRow1.Visible = true;
            trDashRow2.Visible = true;
            trDashRow3.Visible = true;
            trDashRow4.Visible = true;
            trDashRow5.Visible = true;
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

    private void Fill_Div_Brandcontr()
    {
        string[] strDivSplit = (lstFieldForce.SelectedValue.ToString()).Split(',');
        int div_cnt = strDivSplit.Count() - 1;
        Session["NofDiv"] = div_cnt.ToString();

        Division dv = new Division();
        lstDiv_new.Items.Clear();
        if (div_cnt >= 2)
        {
            lstDiv_new.Items.Add("ALL");
        }

        foreach (string strdiv in strDivSplit)
        {
            if (strdiv != "")
            {
                DataSet dsdiv = dv.getDivisionHO(strdiv);
                System.Web.UI.WebControls.ListItem liTerr = new System.Web.UI.WebControls.ListItem();
                liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                lstDiv_new.Items.Add(liTerr);
                lstDiv_new.Visible = true;
                //btnGoBrand.Visible = true;
            }
        }
        if (div_cnt >= 2)
        {
            lstDiv_new.SelectedIndex = 0;
        }
        else
        {
            lstDiv_new.SelectedIndex = 0;
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

            Fill_Div_Brandcontr();
            if (Session["Sub_HO_ID"].ToString() != "0")
            {
                RowHideShow();
            }
        }
        else
        {
            //lstFieldForce.DataTextField = "sf_name";
            //lstFieldForce.DataValueField = "sf_code";
            lstFieldForce.DataSource = null;
            lstFieldForce.DataBind();
        }
    }

    private void FillFinaceYr()
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = sf.Financial_Year_MQY(lstMode.SelectedValue.ToString());
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            lstFinacyr.DataTextField = "MnthYr";
            lstFinacyr.DataValueField = "Finac_id";
            lstFinacyr.DataSource = dsSalesForce;
            lstFinacyr.DataBind();

            lstFinacyr.SelectedIndex = 0;
        }
        else
        {
            lstFinacyr.DataTextField = "MnthYr";
            lstFinacyr.DataValueField = "Finac_id";
            lstFinacyr.DataSource = "";
            lstFinacyr.DataBind();
        }
    }

    private void Filldiv()
    {
        Division dv = new Division();
        if (Session["sf_type"].ToString() == "3" || Session["sf_type"].ToString() == "")
        {
            string[] strDivSplit = div_code.Split(',');

            lstDiv.Items.Add("ALL");
            //lstDiv_new.Items.Add("ALL");

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

                lstDiv_new.Visible = false;
                //btnGoBrand.Visible = false;
            }
        }


    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getChartVal_YTD(List<string> lst_Input_Mn_Yr)
    {


        List<values> lstCoverage = new List<values>();
        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();

            int div_cnt = 0;
            if (lst_Input_Mn_Yr[7] == "ALL")
            {
                string[] strDivSplit = (lst_Input_Mn_Yr[2]).Split(',');

                div_cnt = strDivSplit.Count() - 1;

                if (div_cnt == 1)
                {
                    //cmd.CommandText = "Primary_YTD_All_Div";
                    cmd.CommandText = "Primary_YTD_All_Div_Dash_te";
                    cmd.CommandTimeout = 600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@year", pData[0]);
                    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString((strDivSplit[0].ToString()) + ","));
                    //cmd.Parameters.AddWithValue("@Div_Code", (lst_Input_Mn_Yr[6]));
                    cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString("admin"));
                    //cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);
                    cmd.Parameters.AddWithValue("@FMonth", lst_Input_Mn_Yr[0]);
                    cmd.Parameters.AddWithValue("@FYear", lst_Input_Mn_Yr[1]);
                    cmd.Parameters.AddWithValue("@TMonth", lst_Input_Mn_Yr[4]);
                    cmd.Parameters.AddWithValue("@TYear", lst_Input_Mn_Yr[5]);
                }
                else if (div_cnt >= 2)
                {
                    cmd.CommandText = "Primary_YTD_All_Div_Dash_te";
                    cmd.CommandTimeout = 600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Div_Code", (lst_Input_Mn_Yr[2].ToString()));
                    cmd.Parameters.AddWithValue("@MSf_Code", "admin");
                    cmd.Parameters.AddWithValue("@FMonth", lst_Input_Mn_Yr[0]);
                    cmd.Parameters.AddWithValue("@FYear", lst_Input_Mn_Yr[1]);
                    cmd.Parameters.AddWithValue("@TMonth", lst_Input_Mn_Yr[4]);
                    cmd.Parameters.AddWithValue("@TYear", lst_Input_Mn_Yr[5]);
                }
            }
            else
            {
                //cmd.CommandText = "Primary_YTD_All_Div";
                cmd.CommandText = "Primary_YTD_All_Div_Dash_te";
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString((lst_Input_Mn_Yr[6]) + ","));
                //cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(lst_Input_Mn_Yr[6]));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@FMonth", lst_Input_Mn_Yr[0]);
                cmd.Parameters.AddWithValue("@FYear", lst_Input_Mn_Yr[1]);
                cmd.Parameters.AddWithValue("@TMonth", lst_Input_Mn_Yr[4]);
                cmd.Parameters.AddWithValue("@TYear", lst_Input_Mn_Yr[5]);

                div_cnt = 1;
            }
            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                values cpData = new values();
                cpData.Sf_Code = ds.Tables[0].Rows[0]["Sf_Code"].ToString();
                cpData.Target = Convert.ToDecimal(ds.Tables[0].Rows[0]["Target"].ToString());
                cpData.Sale = Convert.ToDecimal(ds.Tables[0].Rows[0]["Sale"].ToString());
                cpData.achie = Convert.ToDecimal(ds.Tables[0].Rows[0]["achie"].ToString());

                cpData.PSale = Convert.ToDecimal(ds.Tables[0].Rows[0]["PSale"].ToString());
                cpData.Growth = Convert.ToDecimal(ds.Tables[0].Rows[0]["Growth"].ToString());
                cpData.PC = Convert.ToDecimal(ds.Tables[0].Rows[0]["PC"].ToString());
                cpData.Div_cnt = div_cnt.ToString();
                if (lst_Input_Mn_Yr[7] == "ALL")
                {
                    if (div_cnt == 1)
                    {
                        cpData.Div_Name = lst_Input_Mn_Yr[7];
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();
                    }
                    else if (div_cnt == 2)
                    {
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();

                        cpData.Target1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Target"].ToString());
                        cpData.Sale1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Sale"].ToString());
                        cpData.achie1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["achie"].ToString());
                        cpData.Div_Name1 = (ds.Tables[0].Rows[1]["Division_Name"].ToString());
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();
                        cpData.Growth1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Growth"].ToString());
                        cpData.PC1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["PC"].ToString());

                        cpData.Target2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Target"].ToString());
                        cpData.Sale2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Sale"].ToString());
                        cpData.achie2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["achie"].ToString());
                        cpData.Div_Name2 = (ds.Tables[0].Rows[2]["Division_Name"].ToString());
                        cpData.Growth2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Growth"].ToString());
                        cpData.PC2 = Convert.ToDecimal(ds.Tables[2].Rows[0]["PC"].ToString());
                        cpData.Div_Code2 = ds.Tables[0].Rows[2]["division_code"].ToString();
                    }
                    else if (div_cnt == 3)
                    {
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();

                        cpData.Target1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Target"].ToString());
                        cpData.Sale1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Sale"].ToString());
                        cpData.achie1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["achie"].ToString());
                        cpData.Div_Name1 = (ds.Tables[0].Rows[1]["Division_Name"].ToString());
                        cpData.Growth1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Growth"].ToString());
                        cpData.PC1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["PC"].ToString());
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();

                        cpData.Target2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Target"].ToString());
                        cpData.Sale2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Sale"].ToString());
                        cpData.achie2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["achie"].ToString());
                        cpData.Div_Name2 = (ds.Tables[0].Rows[2]["Division_Name"].ToString());
                        cpData.Growth2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Growth"].ToString());
                        cpData.PC2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["PC"].ToString());
                        cpData.Div_Code2 = ds.Tables[2].Rows[0]["division_code"].ToString();

                        cpData.Target3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Target"].ToString());
                        cpData.Sale3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Sale"].ToString());
                        cpData.achie3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["achie"].ToString());
                        cpData.Div_Name3 = (ds.Tables[0].Rows[3]["Division_Name"].ToString());
                        cpData.Growth3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Growth"].ToString());
                        cpData.PC3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["PC"].ToString());
                        cpData.Div_Code3 = ds.Tables[0].Rows[3]["division_code"].ToString();
                    }
                    else if (div_cnt == 4)
                    {
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();

                        cpData.Target1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Target"].ToString());
                        cpData.Sale1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Sale"].ToString());
                        cpData.achie1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["achie"].ToString());
                        cpData.Div_Name1 = (ds.Tables[0].Rows[1]["Division_Name"].ToString());
                        cpData.Growth1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Growth"].ToString());
                        cpData.PC1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["PC"].ToString());
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();

                        cpData.Target2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Target"].ToString());
                        cpData.Sale2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Sale"].ToString());
                        cpData.achie2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["achie"].ToString());
                        cpData.Div_Name2 = (ds.Tables[0].Rows[2]["Division_Name"].ToString());
                        cpData.Growth2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Growth"].ToString());
                        cpData.PC2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["PC"].ToString());
                        cpData.Div_Code2 = ds.Tables[0].Rows[2]["division_code"].ToString();

                        cpData.Target3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Target"].ToString());
                        cpData.Sale3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Sale"].ToString());
                        cpData.achie3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["achie"].ToString());
                        cpData.Div_Name3 = (ds.Tables[0].Rows[3]["Division_Name"].ToString());
                        cpData.Growth3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Growth"].ToString());
                        cpData.PC3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["PC"].ToString());
                        cpData.Div_Code3 = ds.Tables[0].Rows[3]["division_code"].ToString();

                        cpData.Target4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Target"].ToString());
                        cpData.Sale4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Sale"].ToString());
                        cpData.achie4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["achie"].ToString());
                        cpData.Div_Name4 = (ds.Tables[0].Rows[4]["Division_Name"].ToString());
                        cpData.Growth4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Growth"].ToString());
                        cpData.PC4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["PC"].ToString());
                        cpData.Div_Code4 = ds.Tables[0].Rows[4]["division_code"].ToString();
                    }
                    else if (div_cnt == 5)
                    {
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();

                        cpData.Target1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Target"].ToString());
                        cpData.Sale1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Sale"].ToString());
                        cpData.achie1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["achie"].ToString());
                        cpData.Div_Name1 = (ds.Tables[0].Rows[1]["Division_Name"].ToString());
                        cpData.Growth1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Growth"].ToString());
                        cpData.PC1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["PC"].ToString());
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();

                        cpData.Target2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Target"].ToString());
                        cpData.Sale2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Sale"].ToString());
                        cpData.achie2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["achie"].ToString());
                        cpData.Div_Name2 = (ds.Tables[0].Rows[2]["Division_Name"].ToString());
                        cpData.Growth2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Growth"].ToString());
                        cpData.PC2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["PC"].ToString());
                        cpData.Div_Code2 = ds.Tables[0].Rows[2]["division_code"].ToString();

                        cpData.Target3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Target"].ToString());
                        cpData.Sale3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Sale"].ToString());
                        cpData.achie3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["achie"].ToString());
                        cpData.Div_Name3 = (ds.Tables[0].Rows[3]["Division_Name"].ToString());
                        cpData.Growth3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Growth"].ToString());
                        cpData.PC3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["PC"].ToString());
                        cpData.Div_Code3 = ds.Tables[0].Rows[3]["division_code"].ToString();

                        cpData.Target4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Target"].ToString());
                        cpData.Sale4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Sale"].ToString());
                        cpData.achie4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["achie"].ToString());
                        cpData.Div_Name4 = (ds.Tables[0].Rows[4]["Division_Name"].ToString());
                        cpData.Growth4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Growth"].ToString());
                        cpData.PC4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["PC"].ToString());
                        cpData.Div_Code4 = ds.Tables[0].Rows[4]["division_code"].ToString();

                        cpData.Target5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["Target"].ToString());
                        cpData.Sale5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["Sale"].ToString());
                        cpData.achie5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["achie"].ToString());
                        cpData.Div_Name5 = (ds.Tables[0].Rows[5]["Division_Name"].ToString());
                        cpData.Growth5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["Growth"].ToString());
                        cpData.PC5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["PC"].ToString());
                        cpData.Div_Code5 = ds.Tables[0].Rows[5]["division_code"].ToString();
                    }
                }
                else
                {
                    cpData.Div_Name = lst_Input_Mn_Yr[7];
                    cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                    cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();
                }
                lstCoverage.Add(cpData);
            }

            cn.Close();
        }
        return lstCoverage;
    }



    [WebMethod(EnableSession = true)]
    public static string Primary(string objData)
    {
        //string div_code = HttpContext.Current.Session["div_code"].ToString();

        string[] arr = objData.Split('^');
        smonth = arr[0];
        syear = arr[1];

        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];


        string[] strDivSplit = (sSf_Code).Split(',');

        int div_cnt = strDivSplit.Count() - 1;


        string div_code = arr[6];
        string div_code_New = arr[8];
        //int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        //int cmonth = Convert.ToInt32(smonth);
        //int cyear = Convert.ToInt32(syear);
        //int iMn = 0, iYr = 0;

        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        //dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));

        ////
        //DataTable dtcopy = new DataTable();
        //while (months >= 0)
        //{
        //    if (cmonth == 13)
        //    {
        //        cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
        //    }
        //    else
        //    {
        //        iMn = cmonth; iYr = cyear;
        //    }
        //    dtMnYr.Rows.Add(null, iMn, iYr);
        //    months--; cmonth++;
        //}
        using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            DataTable dt = new DataTable();
            // dt.Columns.Add("Label");
            // dt.Columns.Add("Value");
            DataTable sortedDT = new DataTable();

            SqlCommand cmd = new SqlCommand();

            if (div_code == "ALL")
            {
                if (div_code_New == "ALL")
                {
                    cmd = new SqlCommand("Primary_sale_graph_multiple_All_Div_Dash_P", con);
                    cmd.CommandTimeout = 600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Div_Code", sSf_Code);
                    cmd.Parameters.AddWithValue("@Msf_code", "admin");
                    //cmd.Parameters.AddWithValue("@MnthTbl", dtMnYr);

                    cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                    cmd.Parameters.AddWithValue("@FYear", arr[1]);
                    cmd.Parameters.AddWithValue("@TMonth", arr[4]);
                    cmd.Parameters.AddWithValue("@TYear", arr[5]);
                }
                else
                {
                    //cmd = new SqlCommand("Primary_sale_graph_multiple_All_Div", con);
                    cmd = new SqlCommand("Primary_sale_graph_multiple_All_Div_Dash_P", con);
                    cmd.CommandTimeout = 600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(arr[8] + ","));
                    cmd.Parameters.AddWithValue("@Msf_code", "admin");
                    cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                    cmd.Parameters.AddWithValue("@FYear", arr[1]);
                    cmd.Parameters.AddWithValue("@TMonth", arr[4]);
                    cmd.Parameters.AddWithValue("@TYear", arr[5]);

                }
            }
            else
            {
                //cmd = new SqlCommand("Primary_sale_graph_multiple_All_Div", con);
                cmd = new SqlCommand("Primary_sale_graph_multiple_All_Div_Dash_P", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(div_code + ","));
                cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
                cmd.Parameters.AddWithValue("@TMonth", arr[4]);
                cmd.Parameters.AddWithValue("@TYear", arr[5]);
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
            sortedDT.Columns["Prod_Name"].ColumnName = "Label";
            sortedDT.AcceptChanges();
            sortedDT.Columns["cnt"].ColumnName = "Value";
            sortedDT.AcceptChanges();
            sortedDT.Columns["prod_code"].ColumnName = "Code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Division_Name"].ColumnName = "Division_Name";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Target_Val"].ColumnName = "Target_Val";
            sortedDT.AcceptChanges();
            sortedDT.Columns["achie"].ColumnName = "achie";
            sortedDT.AcceptChanges();

            sortedDT.Columns["Growth"].ColumnName = "Growth";
            sortedDT.AcceptChanges();
            sortedDT.Columns["PC"].ColumnName = "PC";
            sortedDT.AcceptChanges();
            string jsonResult = JsonConvert.SerializeObject(sortedDT);

            return jsonResult;

        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getSecondary_Sale_YTD(List<string> lst_Input_Mn_Yr)
    {

        List<values> lstCoverage = new List<values>();

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();

            int div_cnt = 0;
            if (lst_Input_Mn_Yr[7] == "ALL")
            {
                string[] strDivSplit = (lst_Input_Mn_Yr[2]).Split(',');

                div_cnt = strDivSplit.Count() - 1;

                if (div_cnt == 1)
                {
                    //cmd.CommandText = "Secondary_YTD_All_Div";
                    cmd.CommandText = "Secondary_YTD_All_Div_Dash_te";
                    cmd.CommandTimeout = 600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString((strDivSplit[0].ToString()) + ","));
                    cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString("admin"));
                    cmd.Parameters.AddWithValue("@FMonth", Convert.ToString(lst_Input_Mn_Yr[0]));
                    cmd.Parameters.AddWithValue("@FYear", Convert.ToString(lst_Input_Mn_Yr[1]));
                    cmd.Parameters.AddWithValue("@TMonth", Convert.ToString(lst_Input_Mn_Yr[4]));
                    cmd.Parameters.AddWithValue("@TYear", Convert.ToString(lst_Input_Mn_Yr[5]));
                }
                else if (div_cnt >= 2)
                {
                    cmd.CommandText = "Secondary_YTD_All_Div_Dash_te";
                    cmd.CommandTimeout = 600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Div_Code", (lst_Input_Mn_Yr[2].ToString()));
                    cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString("admin"));
                    cmd.Parameters.AddWithValue("@FMonth", Convert.ToString(lst_Input_Mn_Yr[0]));
                    cmd.Parameters.AddWithValue("@FYear", Convert.ToString(lst_Input_Mn_Yr[1]));
                    cmd.Parameters.AddWithValue("@TMonth", Convert.ToString(lst_Input_Mn_Yr[4]));
                    cmd.Parameters.AddWithValue("@TYear", Convert.ToString(lst_Input_Mn_Yr[5]));
                }
            }
            else
            {
                //cmd.CommandText = "Secondary_YTD_All_Div";
                cmd.CommandText = "Secondary_YTD_All_Div_Dash_te";
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@year", pData[0]);
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString((lst_Input_Mn_Yr[6]) + ","));
                cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
                cmd.Parameters.AddWithValue("@FMonth", Convert.ToString(lst_Input_Mn_Yr[0]));
                cmd.Parameters.AddWithValue("@FYear", Convert.ToString(lst_Input_Mn_Yr[1]));
                cmd.Parameters.AddWithValue("@TMonth", Convert.ToString(lst_Input_Mn_Yr[4]));
                cmd.Parameters.AddWithValue("@TYear", Convert.ToString(lst_Input_Mn_Yr[5]));

                div_cnt = 1;
            }

            cmd.Connection = cn;
            cn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            da.Fill(ds);


            if (ds.Tables[0].Rows.Count > 0)
            {
                values cpData = new values();
                cpData.Sf_Code = ds.Tables[0].Rows[0]["Sf_Code"].ToString();
                cpData.Target = Convert.ToDecimal(ds.Tables[0].Rows[0]["Target"].ToString());
                cpData.Sale = Convert.ToDecimal(ds.Tables[0].Rows[0]["Sale"].ToString());
                cpData.achie = Convert.ToDecimal(ds.Tables[0].Rows[0]["achie"].ToString());

                cpData.PSale = Convert.ToDecimal(ds.Tables[0].Rows[0]["PSale"].ToString());
                cpData.Growth = Convert.ToDecimal(ds.Tables[0].Rows[0]["Growth"].ToString());
                cpData.PC = Convert.ToDecimal(ds.Tables[0].Rows[0]["PC"].ToString());
                cpData.Div_cnt = div_cnt.ToString();
                if (lst_Input_Mn_Yr[7] == "ALL")
                {
                    if (div_cnt == 1)
                    {
                        cpData.Div_Name = lst_Input_Mn_Yr[7];
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();
                    }
                    else if (div_cnt == 2)
                    {
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();

                        cpData.Target1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Target"].ToString());
                        cpData.Sale1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Sale"].ToString());
                        cpData.achie1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["achie"].ToString());
                        cpData.Div_Name1 = (ds.Tables[0].Rows[1]["Division_Name"].ToString());
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();
                        cpData.Growth1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Growth"].ToString());
                        cpData.PC1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["PC"].ToString());

                        cpData.Target2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Target"].ToString());
                        cpData.Sale2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Sale"].ToString());
                        cpData.achie2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["achie"].ToString());
                        cpData.Div_Name2 = (ds.Tables[0].Rows[2]["Division_Name"].ToString());
                        cpData.Growth2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Growth"].ToString());
                        cpData.PC2 = Convert.ToDecimal(ds.Tables[2].Rows[0]["PC"].ToString());
                        cpData.Div_Code2 = ds.Tables[0].Rows[2]["division_code"].ToString();
                    }
                    else if (div_cnt == 3)
                    {
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();

                        cpData.Target1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Target"].ToString());
                        cpData.Sale1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Sale"].ToString());
                        cpData.achie1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["achie"].ToString());
                        cpData.Div_Name1 = (ds.Tables[0].Rows[1]["Division_Name"].ToString());
                        cpData.Growth1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Growth"].ToString());
                        cpData.PC1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["PC"].ToString());
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();

                        cpData.Target2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Target"].ToString());
                        cpData.Sale2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Sale"].ToString());
                        cpData.achie2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["achie"].ToString());
                        cpData.Div_Name2 = (ds.Tables[0].Rows[2]["Division_Name"].ToString());
                        cpData.Growth2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Growth"].ToString());
                        cpData.PC2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["PC"].ToString());
                        cpData.Div_Code2 = ds.Tables[2].Rows[0]["division_code"].ToString();

                        cpData.Target3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Target"].ToString());
                        cpData.Sale3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Sale"].ToString());
                        cpData.achie3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["achie"].ToString());
                        cpData.Div_Name3 = (ds.Tables[0].Rows[3]["Division_Name"].ToString());
                        cpData.Growth3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Growth"].ToString());
                        cpData.PC3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["PC"].ToString());
                        cpData.Div_Code3 = ds.Tables[0].Rows[3]["division_code"].ToString();
                    }
                    else if (div_cnt == 4)
                    {
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();

                        cpData.Target1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Target"].ToString());
                        cpData.Sale1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Sale"].ToString());
                        cpData.achie1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["achie"].ToString());
                        cpData.Div_Name1 = (ds.Tables[0].Rows[1]["Division_Name"].ToString());
                        cpData.Growth1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Growth"].ToString());
                        cpData.PC1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["PC"].ToString());
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();

                        cpData.Target2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Target"].ToString());
                        cpData.Sale2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Sale"].ToString());
                        cpData.achie2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["achie"].ToString());
                        cpData.Div_Name2 = (ds.Tables[0].Rows[2]["Division_Name"].ToString());
                        cpData.Growth2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Growth"].ToString());
                        cpData.PC2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["PC"].ToString());
                        cpData.Div_Code2 = ds.Tables[0].Rows[2]["division_code"].ToString();

                        cpData.Target3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Target"].ToString());
                        cpData.Sale3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Sale"].ToString());
                        cpData.achie3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["achie"].ToString());
                        cpData.Div_Name3 = (ds.Tables[0].Rows[3]["Division_Name"].ToString());
                        cpData.Growth3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Growth"].ToString());
                        cpData.PC3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["PC"].ToString());
                        cpData.Div_Code3 = ds.Tables[0].Rows[3]["division_code"].ToString();

                        cpData.Target4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Target"].ToString());
                        cpData.Sale4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Sale"].ToString());
                        cpData.achie4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["achie"].ToString());
                        cpData.Div_Name4 = (ds.Tables[0].Rows[4]["Division_Name"].ToString());
                        cpData.Growth4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Growth"].ToString());
                        cpData.PC4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["PC"].ToString());
                        cpData.Div_Code4 = ds.Tables[0].Rows[4]["division_code"].ToString();
                    }
                    else if (div_cnt == 5)
                    {
                        cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                        cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();

                        cpData.Target1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Target"].ToString());
                        cpData.Sale1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Sale"].ToString());
                        cpData.achie1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["achie"].ToString());
                        cpData.Div_Name1 = (ds.Tables[0].Rows[1]["Division_Name"].ToString());
                        cpData.Growth1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["Growth"].ToString());
                        cpData.PC1 = Convert.ToDecimal(ds.Tables[0].Rows[1]["PC"].ToString());
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();
                        cpData.Div_Code1 = ds.Tables[0].Rows[1]["division_code"].ToString();

                        cpData.Target2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Target"].ToString());
                        cpData.Sale2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Sale"].ToString());
                        cpData.achie2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["achie"].ToString());
                        cpData.Div_Name2 = (ds.Tables[0].Rows[2]["Division_Name"].ToString());
                        cpData.Growth2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["Growth"].ToString());
                        cpData.PC2 = Convert.ToDecimal(ds.Tables[0].Rows[2]["PC"].ToString());
                        cpData.Div_Code2 = ds.Tables[0].Rows[2]["division_code"].ToString();

                        cpData.Target3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Target"].ToString());
                        cpData.Sale3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Sale"].ToString());
                        cpData.achie3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["achie"].ToString());
                        cpData.Div_Name3 = (ds.Tables[0].Rows[3]["Division_Name"].ToString());
                        cpData.Growth3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["Growth"].ToString());
                        cpData.PC3 = Convert.ToDecimal(ds.Tables[0].Rows[3]["PC"].ToString());
                        cpData.Div_Code3 = ds.Tables[0].Rows[3]["division_code"].ToString();

                        cpData.Target4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Target"].ToString());
                        cpData.Sale4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Sale"].ToString());
                        cpData.achie4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["achie"].ToString());
                        cpData.Div_Name4 = (ds.Tables[0].Rows[4]["Division_Name"].ToString());
                        cpData.Growth4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["Growth"].ToString());
                        cpData.PC4 = Convert.ToDecimal(ds.Tables[0].Rows[4]["PC"].ToString());
                        cpData.Div_Code4 = ds.Tables[0].Rows[4]["division_code"].ToString();

                        cpData.Target5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["Target"].ToString());
                        cpData.Sale5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["Sale"].ToString());
                        cpData.achie5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["achie"].ToString());
                        cpData.Div_Name5 = (ds.Tables[0].Rows[5]["Division_Name"].ToString());
                        cpData.Growth5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["Growth"].ToString());
                        cpData.PC5 = Convert.ToDecimal(ds.Tables[0].Rows[5]["PC"].ToString());
                        cpData.Div_Code5 = ds.Tables[0].Rows[5]["division_code"].ToString();
                    }
                }
                else
                {
                    cpData.Div_Name = lst_Input_Mn_Yr[7];
                    cpData.Div_Name = ds.Tables[0].Rows[0]["Division_Name"].ToString();
                    cpData.Div_Code = ds.Tables[0].Rows[0]["division_code"].ToString();
                }
                lstCoverage.Add(cpData);
            }

            //SqlDataReader dr = cmd.ExecuteReader();
            //if (dr.HasRows)
            //{
            //    while (dr.Read())
            //    {
            //        values cpData = new values();
            //        cpData.Sf_Code = dr["Sf_Code"].ToString();

            //        cpData.Target = Convert.ToDecimal(dr["Target"].ToString());

            //        cpData.Sale = Convert.ToDecimal(dr["Sale"].ToString());
            //        cpData.achie = Convert.ToDecimal(dr["achie"].ToString());
            //        cpData.PSale = Convert.ToDecimal(dr["PSale"].ToString());
            //        cpData.Growth = Convert.ToDecimal(dr["Growth"].ToString());
            //        cpData.PC = Convert.ToDecimal(dr["PC"].ToString());
            //        cpData.Div_Name = lst_Input_Mn_Yr[7];
            //        lstCoverage.Add(cpData);
            //    }
            //}
            //dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }




    [WebMethod(EnableSession = true)]
    public static string Secondary(string objData)
    {
        //string div_code = HttpContext.Current.Session["div_code"].ToString();


        string[] arr = objData.Split('^');
        //smonth = arr[0];
        //syear = arr[1];
        //sSf_Code = arr[2];
        //mode = arr[3];
        //tmonth = arr[4];
        //tyear = arr[5];

        smonth = arr[0];
        syear = arr[1];

        sSf_Code = arr[2];
        mode = arr[3];
        tmonth = arr[4];
        tyear = arr[5];

        string[] strDivSplit = (sSf_Code).Split(',');
        int div_cnt = strDivSplit.Count() - 1;

        string div_code = arr[6];
        string div_code_New = arr[8];

        //int months = (Convert.ToInt32(tyear) - Convert.ToInt32(syear)) * 12 + Convert.ToInt32(tmonth) - Convert.ToInt32(smonth); //(Date2.Year - Date1.Year) * 12 + Date2.Month - Date1.Month;
        //int cmonth = Convert.ToInt32(smonth);
        //int cyear = Convert.ToInt32(syear);
        //int iMn = 0, iYr = 0;

        //DataTable dtMnYr = new DataTable();
        //dtMnYr.Columns.Add("INX", typeof(int)).AutoIncrement = true;
        //dtMnYr.Columns["INX"].AutoIncrementSeed = 1;
        //dtMnYr.Columns["INX"].AutoIncrementStep = 1;
        //dtMnYr.Columns.Add("MNTH", typeof(int));
        //dtMnYr.Columns.Add("YR", typeof(int));

        ////
        //DataTable dtcopy = new DataTable();
        //while (months >= 0)
        //{
        //    if (cmonth == 13)
        //    {
        //        cmonth = 01; iMn = cmonth; cyear = cyear + 1; iYr = cyear;
        //    }
        //    else
        //    {
        //        iMn = cmonth; iYr = cyear;
        //    }
        //    dtMnYr.Rows.Add(null, iMn, iYr);
        //    months--; cmonth++;
        //}
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
                if (div_code_New == "ALL")
                {
                    cmd = new SqlCommand("Secondary_sale_graph_multiple_All_Div_Dash_P", con);
                    cmd.CommandTimeout = 600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Div_Code", sSf_Code);
                    cmd.Parameters.AddWithValue("@Msf_code", "admin");
                    cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                    cmd.Parameters.AddWithValue("@FYear", arr[1]);
                    cmd.Parameters.AddWithValue("@TMonth", arr[4]);
                    cmd.Parameters.AddWithValue("@TYear", arr[5]);
                }
                else
                {
                    //cmd = new SqlCommand("Secondary_sale_graph_multiple_All_Div", con);
                    cmd = new SqlCommand("Secondary_sale_graph_multiple_All_Div_Dash_P", con);
                    cmd.CommandTimeout = 600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(arr[8] + ","));
                    cmd.Parameters.AddWithValue("@Msf_code", "admin");
                    cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                    cmd.Parameters.AddWithValue("@FYear", arr[1]);
                    cmd.Parameters.AddWithValue("@TMonth", arr[4]);
                    cmd.Parameters.AddWithValue("@TYear", arr[5]);
                }
            }
            else
            {
                //SqlCommand cmd = new SqlCommand("Secondary_sale_graph_multiple_All_Div", con);
                //cmd = new SqlCommand("Secondary_sale_graph_multiple_All_Div", con);
                cmd = new SqlCommand("Secondary_sale_graph_multiple_All_Div_Dash_P", con);
                cmd.CommandTimeout = 600;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Div_Code", Convert.ToString(div_code + ","));
                cmd.Parameters.AddWithValue("@Msf_code", sSf_Code);
                cmd.Parameters.AddWithValue("@FMonth", arr[0]);
                cmd.Parameters.AddWithValue("@FYear", arr[1]);
                cmd.Parameters.AddWithValue("@TMonth", arr[4]);
                cmd.Parameters.AddWithValue("@TYear", arr[5]);
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
            sortedDT.Columns["Prod_Name"].ColumnName = "Label";
            sortedDT.AcceptChanges();
            sortedDT.Columns["cnt"].ColumnName = "Value";
            sortedDT.AcceptChanges();
            sortedDT.Columns["prod_code"].ColumnName = "Code";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Division_Name"].ColumnName = "Division_Name";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Target_Val"].ColumnName = "Target_Val";
            sortedDT.AcceptChanges();
            sortedDT.Columns["achie"].ColumnName = "achie";
            sortedDT.AcceptChanges();
            sortedDT.Columns["Growth"].ColumnName = "Growth";
            sortedDT.AcceptChanges();
            sortedDT.Columns["PC"].ColumnName = "PC";
            sortedDT.AcceptChanges();
            string jsonResult = JsonConvert.SerializeObject(sortedDT);

            return jsonResult;

        }
    }
    #endregion

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
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "' and (SF_CODE like '%" + sf_code + ',' + "%' or SF_CODE like '%" + ',' + sf_code + ',' + "%'  ) AND Effective_To >= getDate() ", con);
        //   SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
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

            BindImage1();
            BindImage_FieldForce();
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

            //Command by Vasanthi
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
                DataSet dsde = new DataSet();
                SalesForce sff = new SalesForce();
                dsde = sff.getDesignation_BulkEdit(sf_code, div_code);

                AdminSetup ad = new AdminSetup();
                //  dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsLogin.Tables[0].Rows[0]["Designation_Code"].ToString()));
                if (dsde.Tables[0].Rows.Count > 0)
                {
                    dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsde.Tables[0].Rows[0]["Designation_Code"].ToString()));
                }
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
                                //Newly Added by Vasanthi-Begin
                                else if (tp_month4 == curr_month && from_year2 == curr_year)
                                {


                                    isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                    if (isRepSt == false)
                                    {
                                        iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                        //string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                        //iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                        //if (iReturn > 0)
                                        //{

                                            Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                        //}
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
                                //Newly Added by Vasanthi-End
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
                // System.Threading.Thread.Sleep(time);
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");

            }
            //else if (dsImage.Tables[0].Rows.Count > 0)
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Response.Redirect("~/HomePage_Image.aspx");
            //}
            //else if (dsImage_FF.Tables[0].Rows.Count > 0)
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Server.Transfer("~/HomePage_FieldForcewise.aspx");

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
                //Server.Transfer("~/Default_MR.aspx");
                Server.Transfer("~/Default_MR_Basic.aspx");
            }
        }

        #endregion
        //jas 
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
            //Command by Vasanthi
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


            dsvac = adm.getVacant_List(Session["sf_code"].ToString(), Session["div_code"].ToString());
            if (dsvac.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("~/HomePage_VacancyList.aspx?SFCODE=" + Session["sf_code"].ToString() + "&SFNAME=" + Session["sf_name"].ToString() + "&DIV=" + Session["div_code"].ToString());
            }
            else
            {


                //start

                if (dsTp.Tables[0].Rows[0]["TpBased"].ToString() == "0")
                {
                    AdminSetup ad = new AdminSetup();
                    SalesForce sff = new SalesForce();
                    DataSet dsde = new DataSet();
                    dsde = sff.getDesignation_BulkEdit(sf_code, div_code);
                    if (dsde.Tables[0].Rows.Count > 0)
                    {
                        dsDesig = ad.chkRange_tpbased(div_code, Convert.ToInt16(dsde.Tables[0].Rows[0]["Designation_Code"].ToString()));
                    }
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
                                    //Newly Added by Vasanthi-Begin
                                    else if (tp_month4 == curr_month && from_year2 == curr_year)
                                    {


                                        isRepSt = tp_new.IsTpChk_NextMonth(sf_code, div_code, curr_month, curr_year);//chk current month tp

                                        if (isRepSt == false)
                                        {
                                            iReturn = tp.insertDelete_tpdate_tpvalidation(sf_code, div_code, Convert.ToDateTime(last_month_date));//delete up to last month record

                                            //string tp_date = "1" + "-" + curr_month.ToString() + "-" + curr_year.ToString();
                                            //iReturn = tp.update_tpdate_tpvalidation(sf_code, Convert.ToDateTime(tp_date));
                                            //if (iReturn > 0)
                                            //{

                                                Response.Redirect("~/Homepage_TpValidate.aspx?type=3");
                                            //}
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

                                    //Newly Added by Vasanthi-End
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
                //end start
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
                //else if (dsImage.Tables[0].Rows.Count > 0)
                //{
                //    Response.Redirect("~/HomePage_Image.aspx");
                //}
                //else if (dsImage_FF.Tables[0].Rows.Count > 0)
                //{
                //    Server.Transfer("~/HomePage_FieldForcewise.aspx");

                //}
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
    protected void btnSFE_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Effort_Analysis_CatSpecVst_Dashboard.aspx");
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
        public string Div_Code { get; set; }

        public string Div_cnt { get; set; }

        public string Sf_Code1 { get; set; }
        public decimal Target1 { get; set; }
        public decimal Sale1 { get; set; }
        public decimal achie1 { get; set; }
        public decimal PSale1 { get; set; }
        public decimal Growth1 { get; set; }
        public decimal PC1 { get; set; }
        public string Div_Name1 { get; set; }
        public string Div_Code1 { get; set; }

        public string Sf_Code2 { get; set; }
        public decimal Target2 { get; set; }
        public decimal Sale2 { get; set; }
        public decimal achie2 { get; set; }
        public decimal PSale2 { get; set; }
        public decimal Growth2 { get; set; }
        public decimal PC2 { get; set; }
        public string Div_Name2 { get; set; }
        public string Div_Code2 { get; set; }
        public string Sf_Code3 { get; set; }
        public decimal Target3 { get; set; }
        public decimal Sale3 { get; set; }
        public decimal achie3 { get; set; }
        public decimal PSale3 { get; set; }
        public decimal Growth3 { get; set; }
        public decimal PC3 { get; set; }
        public string Div_Name3 { get; set; }
        public string Div_Code3 { get; set; }
        public string Sf_Code4 { get; set; }
        public decimal Target4 { get; set; }
        public decimal Sale4 { get; set; }
        public decimal achie4 { get; set; }
        public decimal PSale4 { get; set; }
        public decimal Growth4 { get; set; }
        public decimal PC4 { get; set; }
        public string Div_Name4 { get; set; }
        public string Div_Code4 { get; set; }

        public string Sf_Code5 { get; set; }
        public decimal Target5 { get; set; }
        public decimal Sale5 { get; set; }
        public decimal achie5 { get; set; }
        public decimal PSale5 { get; set; }
        public decimal Growth5 { get; set; }
        public decimal PC5 { get; set; }
        public string Div_Name5 { get; set; }
        public string Div_Code5 { get; set; }

    }
    protected void btnback_click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }


    #endregion
}
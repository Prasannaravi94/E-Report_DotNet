using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Services;

public partial class Sale_Dashboard_admin : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    static string sSf_Code = string.Empty;
    static string sDiv_Code = string.Empty;
    static string sDay = string.Empty;
    string mnth = string.Empty;
    string yr = string.Empty;
    string day = string.Empty;
    int lastday;
    DataSet dsSalesForce = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        sDiv_Code = Session["div_code"].ToString();
       
        if (!Page.IsPostBack)
        {
           // BindDropDown_Month_Year();

            div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
            if (!Page.IsPostBack)
            {
             
                LblUser.Text =  "Admin";
             //   lbldiv.Text = Session["div_name"].ToString();
               
                mnth = DateTime.Now.Month.ToString();
                day = DateTime.Now.Day.ToString();
                sDay = DateTime.Now.Day.ToString();
                lastday = Convert.ToInt32(day) - 1;
                SalesForce sf = new SalesForce();
                string strFrmMonth = sf.getMonthName(mnth);
                yr = DateTime.Now.Year.ToString();
                lbltarsale.Text = "Target Vs Sale - " + lastday + " " + strFrmMonth + " " + yr;
                FillManagers();
                sSf_Code = ddlFieldForce.SelectedValue;
               // FillReport();


            }
        }
    }
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();

        dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);

        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

        }
    }
    //private void FillReport()
    //{
    //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        if (Session["sf_code"].ToString().Contains("MR"))
    //        {
    //            cmd.CommandText = "Chart_Single_MR_Sale";
    //        }
    //        else if (Session["sf_code"].ToString().Contains("MGR"))
    //        {
    //            cmd.CommandText = "Chart_Single_MGR_Sale";
    //        }
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        //cmd.Parameters.AddWithValue("@year", pData[0]);
    //        cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDiv_Code));
    //        cmd.Parameters.AddWithValue("@MSf_Code", ddlFieldForce.SelectedValue);
    //        cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(mnth));
    //        cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(yr));
    //        //string cDate = "";
    //        //if (mnth == "12")
    //        //    cDate = "01-01-" + (Convert.ToInt32(yr.ToString()) + 1).ToString();
    //        //else
    //        //    cDate = mnth.ToString() + "-01-" + yr.ToString();
    //        cmd.Parameters.AddWithValue("@cDate", Convert.ToInt32(day));
    //        cmd.Connection = cn;
    //        cn.Open();
    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataSet dt = new DataSet();
    //        da.Fill(dt);
    //        if (dt.Tables[0].Rows.Count > 0)
    //        {
    //            lbltar.Text = dt.Tables[0].Rows[0]["target"].ToString();
    //            lblsal.Text = dt.Tables[0].Rows[0]["sale"].ToString();
    //            lblach.Text = dt.Tables[0].Rows[0]["achie"].ToString();
    //            lblLS.Text = dt.Tables[0].Rows[0]["PSale"].ToString();
    //            lblgr.Text = dt.Tables[0].Rows[0]["Growth"].ToString();
    //        }
    //    }
    //}
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<values> getChartVal(List<string> lst_Input_Mn_Yr)
    {
        List<values> lstCoverage = new List<values>();

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand();
            if (Convert.ToString(lst_Input_Mn_Yr[2]).Contains("MR"))
            {
                cmd.CommandText = "Chart_Single_MR_Sale";
            }
            else if (Convert.ToString(lst_Input_Mn_Yr[2]).Contains("MGR"))
            {
                cmd.CommandText = "Chart_Single_MGR_Sale";
            }
            else if (Convert.ToString(lst_Input_Mn_Yr[2]).Contains("admin"))
            {
                cmd.CommandText = "Chart_Single_MGR_Sale";
            }
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@year", pData[0]);
            cmd.Parameters.AddWithValue("@Div_Code", Convert.ToInt32(sDiv_Code));
            cmd.Parameters.AddWithValue("@MSf_Code", Convert.ToString(lst_Input_Mn_Yr[2]));
            cmd.Parameters.AddWithValue("@cMnth", Convert.ToInt32(lst_Input_Mn_Yr[0]));
            cmd.Parameters.AddWithValue("@cYr", Convert.ToInt32(lst_Input_Mn_Yr[1]));
            //string cDate = "";
            //if (lst_Input_Mn_Yr[0].ToString() == "12")
            //    cDate = "01-01-" + (Convert.ToInt32(lst_Input_Mn_Yr[1].ToString()) + 1).ToString();
            //else
            //    cDate = lst_Input_Mn_Yr[0].ToString() + "-01-" + lst_Input_Mn_Yr[1].ToString();
            //cmd.Parameters.AddWithValue("@cDate", cDate);
            cmd.Parameters.AddWithValue("@cDate", Convert.ToInt32(lst_Input_Mn_Yr[3]));
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
                    cpData.Sf_Code = dr["Sf_Code"].ToString();
         
                    cpData.Target = Convert.ToDecimal(dr["Target"].ToString());
                    
                    cpData.Sale = Convert.ToDecimal(dr["Sale"].ToString());
                    cpData.achie = Convert.ToDecimal(dr["achie"].ToString());
                    cpData.PSale = Convert.ToDecimal(dr["PSale"].ToString());
                    cpData.Growth = Convert.ToDecimal(dr["Growth"].ToString());
                    
                    lstCoverage.Add(cpData);
                }
            }
            dr.Close();
            cn.Close();
        }
        return lstCoverage;
    }
    #region Class Values
    public class values
    {
        public string Sf_Code { get; set; }
        
        public decimal Target { get; set; }
        public decimal Sale { get; set; }
        public decimal achie { get; set; }
        public decimal PSale { get; set; }
        public decimal Growth { get; set; }
    }
    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {

        Response.Redirect("BasicMaster.aspx");
    }

}
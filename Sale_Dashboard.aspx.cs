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

public partial class Sale_Dashboard : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;
    static string sSf_Code = string.Empty;
    static string sDiv_Code = string.Empty;
    static string sday = string.Empty;
    string mnth = string.Empty;
    string yr = string.Empty;
    string day = string.Empty;
    static string sDay = string.Empty;
    DataSet dsImage_FF = new DataSet();
    DataSet dsAdmin = new DataSet();

    DataSet dsImage = new DataSet();
    int lastday;

    DataSet dsAdm = null;
    DataSet dsAdmNB = new DataSet();
    DataSet dsSalesForce = new DataSet();
    int Count;
    protected void Page_Load(object sender, EventArgs e)
    {
         sSf_Code = Session["sf_code"].ToString();
        sDiv_Code = Session["div_code"].ToString();
       
        if (!Page.IsPostBack)
        {
           // BindDropDown_Month_Year();

            div_code = Session["div_code"].ToString();
            sf_code = Session["sf_code"].ToString();
            if (!Page.IsPostBack)
            {
             
                LblUser.Text =  Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
             //   lbldiv.Text = Session["div_name"].ToString();

                mnth = DateTime.Now.Month.ToString();
                day = DateTime.Now.Day.ToString();
                sDay = DateTime.Now.Day.ToString();
                lastday = Convert.ToInt32(day) - 1;
                SalesForce sf = new SalesForce();
                string strFrmMonth = sf.getMonthName(mnth);
                yr = DateTime.Now.Year.ToString();
                FillManagers();
                sSf_Code = ddlFieldForce.SelectedValue;
                lbltarsale.Text = "Target Vs Sale - " + lastday + " " + strFrmMonth + " " + yr;
                
              //  lbltarsale.Text = "Target Vs Sale - " + strFrmMonth + " " + yr;
               // FillReport();

                BindImage_FieldForce();
            }
        }
    }
    
    private void BindImage_FieldForce()
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath from Mas_HomeImage_FieldForce where Sf_Code='" + sf_code + "' ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dsImage_FF);
        con.Close();
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
    //        cmd.Parameters.AddWithValue("@MSf_Code", sSf_Code);
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
    protected void btnHome_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "1") // MR Login
        {
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
            AdminSetup admin = new AdminSetup();
            dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
            AdminSetup adm_Nb = new AdminSetup();
            dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
            BindImage_FieldForce();

            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            if (dsImage_FF.Tables[0].Rows.Count > 0)
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
            else if (Count != 0)
            {

                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }

            else
            {
                Server.Transfer("~/Default_MR.aspx");
            }
        }
        else if (Session["sf_type"].ToString() == "2") // MGR Login
        {


            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_Quote_Home(Session["div_code"].ToString());
            AdminSetup admin = new AdminSetup();
            dsAdm = admin.Get_Flash_News_Home(Session["div_code"].ToString());
            AdminSetup adm_Nb = new AdminSetup();
            dsAdmNB = adm_Nb.Get_Notice_Home(Session["div_code"].ToString());
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());
            BindImage_FieldForce();

            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());


            if (dsImage_FF.Tables[0].Rows.Count > 0)
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
            else if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }

            else
            {
                Server.Transfer("~/Default_MGR.aspx");
            }
        }       
    }
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }
    protected void btnHomepage_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "2") // MGR Login
        {
            if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else
            {
                Server.Transfer("~/Default_MGR.aspx");
            }
        }
        else
        {

            if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else
            {
                Server.Transfer("~/Default_MR.aspx");
            }
        }

    }
}
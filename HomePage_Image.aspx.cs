using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;
public partial class HomePage_Image : System.Web.UI.Page
{
    string sfCode = string.Empty;
    string sf_code = string.Empty;
    string desig = string.Empty;
    string div_code = string.Empty;
    DataSet dsAdmin = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsImage = new DataSet();
    DataSet dsImage_FF = new DataSet();
    DataSet dsAdm = null;
    DataSet dsAdmNB = null;
    int time;
    int Count;
    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        sf_code = Session["sf_code"].ToString();
        desig = Session["Designation_Short_Name"].ToString();
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {           
            BindImage();
            BindImage_FieldForce();
        }      
    }
    private void BindImage()
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();

        // SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "' and (Designation_Short_Name like '%"+ desig +','+ "%' or Designation_Short_Name like '%"+','+ desig + ',' + "%'  ) ", con);
      //  SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "' and (SF_CODE like '%" + sf_code + ',' + "%' or SF_CODE like '%" + ',' + sf_code + ',' + "%'  ) AND Effective_To >= getDate() ", con);
        SqlCommand cmd = new SqlCommand("select ROW_NUMBER() OVER (ORDER BY Id ) SL, FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "' and (SF_CODE like '%" + sf_code + ',' + "%' or SF_CODE like '%" + ',' + sf_code + ',' + "%'  ) AND Effective_To >= getDate() order by SL DESC ", con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        DataList1.DataSource = ds;
        DataList1.DataBind();
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
    protected void btnHome_Click(object sender, EventArgs e)
    {
        //Response.Redirect("");
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


       
            // latest added
            TP_New tp_new2 = new TP_New();
            DataSet dsExpense = new DataSet();
            dsExpense = tp_new2.get_Expense_Range(Session["sf_code"].ToString());

            if (dsExpense.Tables[0].Rows.Count > 0 && dsExpense.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "0" && dsExpense.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() != "28")
            {

                if (dsExpense.Tables[0].Rows[0][0].ToString() != "")
                {
                    DateTime dt = DateTime.Now;
                    int day = dt.Day;
                    int curr_month = dt.Month;
                    int curr_year = dt.Year;

                    var now = DateTime.Now;
                    var startOfMonth = new DateTime(now.Year, now.Month, 1);
                    int day1 = startOfMonth.Day;
                    var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
                    var lastDay_curr_mon = new DateTime(now.Year, now.Month, DaysInMonth);


                    //int exp_start = Convert.ToInt16(dsExpense.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());
                    int exp_end = Convert.ToInt16(dsExpense.Tables[0].Rows[0].ItemArray.GetValue(0).ToString());

                    if (dsExpense.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() != "0")
                    {
                        if (day >= 1 && day <= exp_end)
                        {

                            DateTime dat = DateTime.Now;
                            dat = dat.AddMonths(-1);
                            int last_month = dat.Month;
                            int Yearr = dat.Year;

                            TP_New tt = new TP_New();
                            bool isExp = false;
                            isExp = tt.IsExpChk_lastMonth(sf_code, div_code, last_month, Yearr);//chk current month tp

                            if (isExp == false)
                            {

                                Response.Redirect("~/HomePage_Expense.aspx?exp=" + "1" + "&txtUserName=1&txtPassWord=1&enddate=" + exp_end);
                            }
                        }
                        else if (day > exp_end)
                        {
                            DateTime dat = DateTime.Now;
                            dat = dat.AddMonths(-1);
                            int last_month = dat.Month;
                            int Yearr = dat.Year;

                            TP_New tt = new TP_New();
                            bool isExp = false;
                            isExp = tt.IsExpChk_lastMonth(sf_code, div_code, last_month, Yearr);//chk current month tp

                            if (isExp == false)
                            {
                                Response.Redirect("~/HomePage_Expense.aspx?exp=" + "2" + "&txtUserName=1&txtPassWord=1");
                            }
                        }
                    }
                }
            }
            //  latest added end

            DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
            string sMonthName = dtDate.ToString("MMM");
            TP_New tp = new TP_New();
            DataSet dsTP = new DataSet();
            // dsTP = tp.get_TP_Active_Date_New_Index(sf_code);

            dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);

            System.Threading.Thread.Sleep(time);
            Response.Redirect("Sales_DashBoard_Admin_Brand.aspx");

            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            if (dsImage_FF.Tables[0].Rows.Count > 0)
            {
               Server.Transfer("~/HomePage_FieldForcewise.aspx");
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

            DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
            string sMonthName = dtDate.ToString("MMM");
            TP_New tp = new TP_New();
            DataSet dsTP = new DataSet();
            // dsTP = tp.get_TP_Active_Date_New_Index(sf_code);

            dsTP = tp.get_lastTPdate_forTpValida(sf_code, div_code);

            System.Threading.Thread.Sleep(time);
            Response.Redirect("Sales_DashBoard_Admin_Brand.aspx");


            if (dsImage_FF.Tables[0].Rows.Count > 0)
                {
                    Server.Transfer("~/HomePage_FieldForcewise.aspx");
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
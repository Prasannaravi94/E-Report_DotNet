using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class HomePage_Expense : System.Web.UI.Page
{

    string sfCode = string.Empty;
    string from_month = string.Empty;
    string to_month = string.Empty;
    DataSet dsAdmin = null;
    DataSet dsSalesForce = null;
    DataSet dsAdm = null;
    DataSet dsAdmNB = null;
    DataSet dsTP = null;
    int month;
    int year;
    int Count;
    int tp_mon;
    string from_yr = string.Empty;
    int from_yr2;
    string type = string.Empty;
    string next_year = string.Empty;
    int next_year2;
    int next_month;
    DataSet dsImage_FF = new DataSet();
    string div_code = string.Empty;
    string enddate = string.Empty;
    string div_name = string.Empty;
    DataSet dsImage = new DataSet();
    string exp = string.Empty;
    DataSet dsTp = null;
    DataSet dsDesig = null;
    DataSet dsTp2 = null;
    int time;
    int tp_month4;
    string from_year = string.Empty;
    int from_year2;
    int next_month4;
    int to_year2;
    string to_year = string.Empty;
    DataSet dsLogin = null;
    string txtUserName = string.Empty;
    string txtPassWord = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            sfCode = Session["sf_code"].ToString();
            div_code = Session["div_code"].ToString();
            lbldiv_name.Text = Session["div_name"].ToString();
            exp = Request.QueryString["exp"].ToString();
            txtUserName = Request.QueryString["txtUserName"].ToString();
            txtPassWord = Request.QueryString["txtPassWord"].ToString();


            if (!Page.IsPostBack)
            {
                //BindImage();
                BindImage1();
                BindImage_FieldForce();
            }


            DateTime dat = DateTime.Now;
            dat = dat.AddMonths(-1);
            int last_month = dat.Month;
            int Yearr = dat.Year;
            if (exp == "1")
            {
                //DateTime dt = DateTime.Now;
                //int day = dt.Day;
                //enddate = Request.QueryString["enddate"].ToString();
                //if (day == Convert.ToInt16(enddate))
                //{
                //    lblblink.Visible = true;
                //    lblalert.Visible = true;
                //}


                btnNext.Visible = true;
            }
            else
            {
                btnNext.Visible = false;
            }


            System.Globalization.DateTimeFormatInfo mfi = new
            System.Globalization.DateTimeFormatInfo();
            string strlastmonth = mfi.GetMonthName(last_month).ToString();

            clicktp.Text = "Click here to Prepare Your Expense for the Month of" + "<span style='color:red'> " + strlastmonth + " " + Yearr + "</span>";
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Index.aspx");
            Response.Write(ex.Message);
        }

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
        // SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "' and (SF_CODE like '%" + sfCode + ',' + "%' or SF_CODE like '%" + ',' + sfCode + ',' + "%'  ) AND Effective_To >= getDate() ", con);


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  DataSet dsImage = new DataSet();
        da.Fill(dsImage);
        con.Close();

    }



    private void BindImage()
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        //DataList1.DataSource = ds;
        //DataList1.DataBind();
    }

    private void BindImage_FieldForce()
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath from Mas_HomeImage_FieldForce where Sf_Code='" + sfCode + "' ", con);
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
            BindImage1();

            int Count;
            //DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
            //string sMonthName = dtDate.ToString("MMM");
            //TP_New tp = new TP_New();
            //DataSet dsTP = new DataSet();
            //// dsTP = tp.get_TP_Active_Date_New_Index(sf_code);

            //dsTP = tp.get_lastTPdate_forTpValida(sfCode, div_code);

            System.Threading.Thread.Sleep(time);
            Response.Redirect("Sales_DashBoard_Admin_Brand.aspx");


            //Response.Redirect("Information_Page.aspx");

            //if (dsImage.Tables[0].Rows.Count > 0)
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Response.Redirect("HomePage_Image.aspx");
            //}

            //else if (dsImage_FF.Tables[0].Rows.Count > 0)
            //{
            //    Server.Transfer("HomePage_FieldForcewise.aspx");
            //}
            //else if (dsAdmin.Tables[0].Rows.Count > 0)
            //{
            //    Server.Transfer("Quote_Design.aspx");

            //}
            //else if (dsAdmNB.Tables[0].Rows.Count > 0)
            //{
            //    Server.Transfer("NoticeBoard_design.aspx");

            //}
            //else if (dsAdm.Tables[0].Rows.Count > 0)
            //{
            //    Server.Transfer("FlashNews_Design.aspx");
            //}
            //else if (dsSalesForce.Tables[0].Rows.Count > 0)
            //{
            //    Response.Redirect("Birthday_Wish.aspx");
            //}


            //else
           // {
            //    Server.Transfer("~/Default_MR.aspx");
           // }
        }
        if (Session["sf_type"].ToString() == "2") // MGR Login
        {

            //DateTime dtDate = new DateTime(Convert.ToInt16(2016), Convert.ToInt16(DateTime.Now.Month.ToString()), 2);
            //string sMonthName = dtDate.ToString("MMM");
            //TP_New tp = new TP_New();
            //DataSet dsTP = new DataSet();
            //// dsTP = tp.get_TP_Active_Date_New_Index(sf_code);

            //dsTP = tp.get_lastTPdate_forTpValida(sfCode, div_code);

           System.Threading.Thread.Sleep(time);
           Response.Redirect("Sales_DashBoard_Admin_Brand.aspx");


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

    protected void clicktp_Click(object sender, EventArgs e)
    {

       
            if (sfCode.Contains("MR"))//for base level 
            {
              
                    Response.Redirect("MasterFiles/MR/RptAutoExpense_Rowwise.aspx?home=1");
               

            }
            //else if (sfCode.Contains("MGR")) //for manager
            //{
            //    DataTable DCExp = new DataTable();
            //    Distance_calculation Exp = new Distance_calculation();
            //    DCExp = Exp.MGRstpCnt(div_code, Session["Designation_Short_Name"].ToString());
            //    if ("1".Equals(DCExp.Rows[0]["cnt"].ToString()))
            //    {
            //        Response.Redirect("MasterFiles/MR/RptAutoExpense_MGR.aspx?home=1");
            //    }
            //    else
            //    {
            //        Response.Redirect("MasterFiles/MGR/RptAutoExpense_MGR_old.aspx?home=1");
            //    }
            //}
        


    }
}

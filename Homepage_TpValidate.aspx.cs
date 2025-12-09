using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bus_EReport;
using System.Data;
using System.Data.SqlClient;

public partial class Bus_EReport_Homepage_TpValidate : System.Web.UI.Page
{

    string sfCode = string.Empty;
    string from_month = string.Empty;
    string to_month = string.Empty;
    string from_year = string.Empty;
    string to_year = string.Empty;
    DataSet dsAdmin = null;
    DataSet dsSalesForce = null;
    DataSet dsAdm = null;
    DataSet dsAdmNB = null;
    DataSet dsTP = null;
    int month;
    int year;
    int Count;
    int time;
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
    string RejectMonth = string.Empty;
    string RejectMGR = string.Empty;
    string RejectYear = string.Empty;
    string TwoMonth_back = string.Empty;
    string Twomnth_year = string.Empty;
    string OneMonth_back = string.Empty;
    string Onemnth_year = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
        sfCode = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        lbldiv_name.Text = Session["div_name"].ToString();
        //from_month = Request.QueryString["from_month"].ToString();
       // to_month = Request.QueryString["to_month"].ToString();
       // from_year = Request.QueryString["from_year"].ToString();
        //to_year = Request.QueryString["to_year"].ToString();
        type = Request.QueryString["type"].ToString();

        if (!Page.IsPostBack)
        {
            //BindImage();
            BindImage1();
            BindImage_FieldForce();
        }     

        string next_month1 = DateTime.Now.AddMonths(1).ToString();

        string[] next_month2 = next_month1.Split('/');

        next_month = Convert.ToInt32(next_month2[1]);
        next_year = next_month2[2];
        next_year2 = Convert.ToInt16(next_year.Substring(0, 4));

        System.Globalization.DateTimeFormatInfo mfi = new
        System.Globalization.DateTimeFormatInfo();
        string strMonthName = mfi.GetMonthName(next_month).ToString();

        if (type == "1")
        {
            DateTime dt = DateTime.Now;
            int day = dt.Day;
            enddate = Request.QueryString["enddate"].ToString();
            if (day ==Convert.ToInt16(enddate))
            {
                lblblink.Visible = true;
                lblalert.Visible = true;
            }
            if (sfCode.Contains("MGR"))
            {
                btnHome.Visible = true;
                btnHome.Text = "Go to Home Page";
                //btnNext.Visible = true;
                // btnNext.Text = "Go to Home Page";
            }
            else
            {
                btnHome.Visible = true;
                btnNext.Visible = true;
            }
            clicktp.Text = "Click here to Prepare Your Tour Plan for the Month of " + "<span style='color:red'> " + strMonthName +" " +next_year2 + "</span>";
        }
        else if (type == "2")
        {
            
            clicktp.Text = "Click here to Prepare Your Tour Plan for the Month of " + "<span style='color:red'> " + strMonthName + " " + next_year2 + "</span>";
        }
        else if (type == "3")
        {
     
            DateTime dt = DateTime.Now;
            int day = dt.Day;
            int curr_month = dt.Month;
            int curr_year = dt.Year;
            System.Globalization.DateTimeFormatInfo mfi2 = new
            System.Globalization.DateTimeFormatInfo();
            string cur_mon = mfi2.GetMonthName(curr_month).ToString();

            clicktp.Text = "Click here to Prepare Your Tour Plan for the Month of " + "<span style='color:red'> " + cur_mon + " " + curr_year + "</span>";
        }

        else if (type == "4")
        {
            RejectMonth = Request.QueryString["RejectMonth"].ToString();
            RejectMGR = Request.QueryString["RejectMGR"].ToString();
            RejectYear = Request.QueryString["RejectYear"].ToString();

            System.Globalization.DateTimeFormatInfo mfi2 = new
            System.Globalization.DateTimeFormatInfo();
            string cur_mon = mfi2.GetMonthName(Convert.ToInt16(RejectMonth)).ToString();

            clicktp.Text = "Click here to Re-Enter Your TP ( Rejected by " + "<span style='color:#5DADE2'>" + RejectMGR + "</span>" + " ) for the Month of " + "<span style='color:red'> " + cur_mon + " " + RejectYear + "</span>";
            clicktp.Font.Size = 16;
        }

        else if (type == "5")
        {
            TwoMonth_back = Request.QueryString["TwoMonth_back"].ToString();
            Twomnth_year = Request.QueryString["Twomnth_year"].ToString();

            System.Globalization.DateTimeFormatInfo mfi2 = new
            System.Globalization.DateTimeFormatInfo();
            string cur_mon = mfi2.GetMonthName(Convert.ToInt16(TwoMonth_back)).ToString();

            clicktp.Text = "Click here to Prepare Your Tour Plan for the Month of " + "<span style='color:red'> " + cur_mon + " " + Twomnth_year + "</span>";
            clicktp.Font.Size = 16;
        }

        else if (type == "6")
        {
            OneMonth_back = Request.QueryString["OneMonth_back"].ToString();
            Onemnth_year = Request.QueryString["Onemnth_year"].ToString();

            System.Globalization.DateTimeFormatInfo mfi2 = new
            System.Globalization.DateTimeFormatInfo();
            string cur_mon = mfi2.GetMonthName(Convert.ToInt16(OneMonth_back)).ToString();

            clicktp.Text = "Click here to Prepare Your Tour Plan for the Month of " + "<span style='color:red'> " + cur_mon + " " + Onemnth_year + "</span>";
            clicktp.Font.Size = 16;
        }


    }

    private void BindImage()
    {
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" +div_code + "'", con);
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
        // SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlCommand cmd = new SqlCommand("select FilePath,subject from Mas_HomePage_Image where Division_Code = '" + div_code + "' and (SF_CODE like '%" + sfCode + ',' + "%' or SF_CODE like '%" + ',' + sfCode + ',' + "%'  ) AND Effective_To >= getDate() ", con);


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //  DataSet dsImage = new DataSet();
        da.Fill(dsImage);
        con.Close();

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

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            if (Count != 0)
            {
               
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            //else if (dsImage.Tables[0].Rows.Count > 0)
            //{
            //    System.Threading.Thread.Sleep(time);
            //    Response.Redirect("HomePage_Image.aspx");
            //}
            //else if (dsImage_FF.Tables[0].Rows.Count > 0)
            //{
            //   Server.Transfer("HomePage_FieldForcewise.aspx");
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

            if (Count != 0)
            {
               
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            //else if (dsImage.Tables[0].Rows.Count > 0)
            //{
            //    Response.Redirect("HomePage_Image.aspx");
            //}

            //else if (dsImage_FF.Tables[0].Rows.Count > 0)
            //    {
            //        Server.Transfer("HomePage_FieldForcewise.aspx");
            //    }
                           
               
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
    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Index.aspx");
    }

    protected void btnHomepage_Click(object sender, EventArgs e)
    {

        int Count;

        AdminSetup admin = new AdminSetup();

        Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

        if (Session["sf_type"].ToString() == "2") // MGR Login
        {
            if (Count != 0)
            {
                Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            }
            else
            {
                //Server.Transfer("~/Default_MGR.aspx");
                Server.Transfer("MasterFiles/MGR/MGR_Index.aspx");
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
        type = Request.QueryString["type"].ToString();
        if (sfCode.Contains("MR"))//for base level 
        {
            if (type == "1")
            {
                Response.Redirect("MasterFiles/MR/TourPlan.aspx");
            }
            else if (type == "2")
            {
                Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=A");
            }
            else if (type == "3")
            {
                Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=A");
            }
            else if (type == "4")
            {
                Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=A");
            }

            else if (type == "5")
            {
                Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=A");

            }
            else if (type == "6")
            {
                Response.Redirect("MasterFiles/MR/TourPlan.aspx?Index=A");

            }
        }
        else if (sfCode.Contains("MGR")) //for manager
        {
            if (type == "1")
            {
                Response.Redirect("MasterFiles/MGR/TourPlan_Calen.aspx");
            }
            else if (type == "2")
            {
                Response.Redirect("MasterFiles/MGR/TourPlan_Calen.aspx?Index=A");
            }
            else if (type == "3")
            {
                Response.Redirect("MasterFiles/MGR/TourPlan_Calen.aspx?Index=A");
            }
            else if (type == "4")
            {
                Response.Redirect("MasterFiles/MGR/TourPlan_Calen.aspx?Index=A");
            }

            else if (type == "5")
            {
                Response.Redirect("MasterFiles/MGR/TourPlan_Calen.aspx?Index=A");
            }

            else if (type == "6")
            {
                Response.Redirect("MasterFiles/MGR/TourPlan_Calen.aspx?Index=A");
            }
        }

       
    }
}

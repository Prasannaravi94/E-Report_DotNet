using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.SqlClient;

public partial class FlashNews_Design : System.Web.UI.Page
{
    string div_code = string.Empty;
    string strslno = string.Empty;
    DataSet dsAdmin = new DataSet();
    DataSet dsSalesForce = new DataSet();
    DataSet dsImage = new DataSet();
    DataSet dsLogin = null;
    DataSet dsadmn = null;
    DataSet dsImage_FF = new DataSet();
    int Count;
    protected void Page_Load(object sender, EventArgs e)
    {

        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack)
        {
            AdminSetup adm = new AdminSetup();
            dsAdmin = adm.Get_Flash_News(div_code);
            LblUser.Text = "Welcome " + Session["sf_name"] + " - " + Session["Designation_Short_Name"] + " - " + Session["Sf_HQ"];
            lbldiv.Text = Session["div_name"].ToString();
            if (dsAdmin.Tables[0].Rows.Count > 0)
            {
                lblFlash.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                lblFlash.Text = lblFlash.Text.Replace("asdf", "'");
                //lblFlash.Text = dsAdmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
            }

        }
    }
    protected void btnHome_Click(object sender, EventArgs e)
    {
        AdminSetup admin = new AdminSetup();
        if (Session["sf_type"].ToString() == "1") // MR Login
        {
            AdminSetup dv = new AdminSetup();
            dsadmn = dv.getHome_Dash_Display(div_code);

            SalesForce sf = new SalesForce();

            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), div_code);

            int Count;

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            BindImage1();
            BindImage_FieldForce();

            if (dsImage.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("HomePage_Image.aspx");
            }
            else if (dsImage_FF.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("HomePage_FieldForcewise.aspx");
            }
            else if (dsadmn.Tables[0].Rows.Count > 0)
            {
                string strAdd = dsadmn.Tables[0].Rows[0]["DOB_DOW"].ToString();
                if (strAdd == "1")
                {
                    Response.Redirect("DOB_DOW_ListedDr.aspx");
                }
            }
            else if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("Birthday_Wish.aspx");
            }
            //else if (Count != 0)
            //{
            //    Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            //}
            else
            {
                Server.Transfer("~/Default_MR.aspx");
            }
        }
        else if (Session["sf_type"].ToString() == "2") // MGR Login
        {

            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getFieldForce_Birth(Session["sf_code"].ToString(), Session["div_code"].ToString());

            Count = admin.get_Mail_MR_MGR_Count(Session["sf_code"].ToString());

            BindImage1();
            BindImage_FieldForce();

            if (dsImage.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("HomePage_Image.aspx");
            }
            else if (dsImage_FF.Tables[0].Rows.Count > 0)
            {
                Server.Transfer("HomePage_FieldForcewise.aspx");
            }
            else  if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                Response.Redirect("Birthday_Wish.aspx");
            }
            //else if (Count != 0)
            //{
            //    Response.Redirect("MasterFiles/Mails/Mail_Head.aspx");
            //}
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

        string sf_code = Session["sf_code"].ToString();
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

    protected void btnHomepage_Click(object sender, EventArgs e)
    {
        if (Session["sf_type"].ToString() == "2") // MGR Login
        {

            Server.Transfer("~/Default_MGR.aspx");

        }
        else
        {


            Server.Transfer("~/Default_MR.aspx");

        }
    }
}
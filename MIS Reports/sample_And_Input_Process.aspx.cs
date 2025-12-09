using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using DBase_EReport;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class MIS_Reports_sample_And_Input_Process : System.Web.UI.Page
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
    DataTable geteffmonth = new DataTable();
    DataTable geteffyear = new DataTable();
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
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.FindControl("btnBack").Visible = false;
            //FillManagers();
            //FillYear();

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                ddlFFType.Visible = false;

                //FillManagers();
                //FillMRManagers1();
                FillYear();
               // FillColor();
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
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                //FillManagers();
                //ddlFieldForce.SelectedIndex = 1;

                //FillManagers();
                FillYear();
                //OnChange_Mode();
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
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

            }

        }
     //   FillColor();
    }
    private void FillYear()
    {
        TourPlan tp = new TourPlan();
        dsTP = tp.Get_TP_Edit_Year(div_code);
        if (dsTP.Tables[0].Rows.Count > 0)
        {
            for (int k = Convert.ToInt16(dsTP.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
            {
                ddlFYear.Items.Add(k.ToString());
                //ddlTYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                //ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        if (ddlFFType.SelectedValue == "0")
        {

            string squery = "select SI_EM_Month from setup_others where division_code='" + div_code + "'";

            geteffmonth = db_ER.Exec_DataTable(squery);

            ddlFMonth.SelectedValue = geteffmonth.Rows[0]["SI_EM_Month"].ToString();

            ddlFMonth.Enabled = false;

            string squery2 = "select SI_EM_Year from setup_others where division_code='" + div_code + "'";

            geteffyear = db_ER.Exec_DataTable(squery2);

            ddlFYear.SelectedValue = geteffyear.Rows[0]["SI_EM_Year"].ToString();
            ddlFYear.Enabled = false;

        }
        else
        {
            string squery = "select IN_EM_Month from setup_others where division_code='" + div_code + "'";

            geteffmonth = db_ER.Exec_DataTable(squery);

            ddlFMonth.SelectedValue = geteffmonth.Rows[0]["IN_EM_Month"].ToString();

            ddlFMonth.Enabled = false;

            string squery2 = "select IN_EM_Year from setup_others where division_code='" + div_code + "'";

            geteffyear = db_ER.Exec_DataTable(squery2);

            ddlFYear.SelectedValue = geteffyear.Rows[0]["IN_EM_Year"].ToString();
            ddlFYear.Enabled = false;
        }
    }
    protected void OnChange_Mode(object sender, EventArgs e)
    {

        if (ddlFFType.SelectedValue=="0")
        {
            
            string squery = "select SI_EM_Month from setup_others where division_code='"+div_code+"'";

           geteffmonth= db_ER.Exec_DataTable(squery);

            ddlFMonth.SelectedValue = geteffmonth.Rows[0]["SI_EM_Month"].ToString();

            ddlFMonth.Enabled = false;

            string squery2 = "select SI_EM_Year from setup_others where division_code='" + div_code + "'";

            geteffyear= db_ER.Exec_DataTable(squery2);

            ddlFYear.SelectedValue = geteffyear.Rows[0]["SI_EM_Year"].ToString();
            ddlFYear.Enabled = false;
        }
        else
        {
            string squery = "select IN_EM_Month from setup_others where division_code='" + div_code + "'";

            geteffmonth = db_ER.Exec_DataTable(squery);

            ddlFMonth.SelectedValue = geteffmonth.Rows[0]["IN_EM_Month"].ToString();

            ddlFMonth.Enabled = false;

            string squery2 = "select IN_EM_Year from setup_others where division_code='" + div_code + "'";

            geteffyear = db_ER.Exec_DataTable(squery2);

            ddlFYear.SelectedValue = geteffyear.Rows[0]["IN_EM_Year"].ToString();

            ddlFYear.Enabled = false;
        }


    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
        {
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            transaction = connection.BeginTransaction();
            command.Connection = connection;
            command.Transaction = transaction;
            try
            {
                DateTime dt = DateTime.Now;

                if(ddlFFType.SelectedValue=="0")
                {
                    command.CommandText = "delete from Trans_Sample_Stock_FFWise_AsonDate where Division_Code='"+div_code+"'";
                    command.ExecuteNonQuery();

                    command.CommandText = "exec sample_At_A_Glance_Process '" + div_code + "','" + sf_code + "','" + dt.Month + "','" + dt.Year + "','" + dt.Month + "','" + dt.Year + "'";
                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "delete from Trans_input_Stock_FFWise_AsonDate where Division_Code='" + div_code + "'";
                    command.ExecuteNonQuery();

                    command.CommandText = "exec Input_At_A_Glance_Process '" + div_code + "','" + sf_code + "','" + dt.Month + "','" + dt.Year + "','" + dt.Month + "','" + dt.Year + "'";
                    command.ExecuteNonQuery();
                }


                

            }
            catch(Exception ex)
            {
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());

                Console.WriteLine("Message: {0}", ex.Message);


                try
                {

                    transaction.Rollback();

                }

                catch (Exception ex2)
                {


                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());

                    Console.WriteLine("  Message: {0}", ex2.Message);

                }

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');window.location='sample_And_Input_Process.aspx'</script>");
            }
            transaction.Commit();
            connection.Close();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Processed Sucessfully!');window.location='sample_And_Input_Process.aspx'</script>");

        }



    }
 }
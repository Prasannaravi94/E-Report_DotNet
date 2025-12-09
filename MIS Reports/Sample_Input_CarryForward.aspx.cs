using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using DBase_EReport;

public partial class MIS_Reports_Sample_Input_CarryForward : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataSet dsDoc = null;
    DataSet dsDivision = null;
    DataSet dsdiv = null;
    string tot_dr = string.Empty;
    string total_doc = string.Empty;
    string tot_FWD = string.Empty;
    string tot_dcr_dr = string.Empty;
    string sCurrentDate = string.Empty;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    string MultiSf_Code = string.Empty;
    int mode = -1;
    string strSf_Code = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsTP = null;
    DataSet dsadmin = null;
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
            // Filldiv();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //// menu1.FindControl("btnBack").Visible = false;
            FillYear();
            AdminSetup adm = new AdminSetup();
            DB_EReporting db_ER = new DB_EReporting();
            tot_dr = "select Sample_Carry_Forward,Input_Carry_Forward from Setup_Others where division_Code='"+div_code+"'";
            dsadmin = db_ER.Exec_DataSet(tot_dr);

            RadioSample.SelectedValue = "1";
            if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "0")
            {
                RadioSample.SelectedValue = "0";
                RadioSample.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
            }
            else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(0).ToString() == "1")
            {
                RadioSample.SelectedValue = "1";
                RadioSample.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
            }
            RadioInput.SelectedValue = "1";
            if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "0")
            {
                RadioInput.SelectedValue = "0";
                RadioInput.SelectedItem.Attributes.Add("Style", "color:red;font-weight:bold");
            }
            else if (dsadmin.Tables[0].Rows[0].ItemArray.GetValue(1).ToString() == "1")
            {
                RadioInput.SelectedValue = "1";
                RadioInput.SelectedItem.Attributes.Add("style", "color:red;font-weight:bold");
            }
            //FillManagers();
        }
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
                ddlTYear.Items.Add(k.ToString());
                ddlFYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlTYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }
        DateTime FromMonth = DateTime.Now;
        DateTime ToMonth = DateTime.Now;
       // ddlFMonth.Text = FromMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
       // txtToMonthYear.Text = ToMonth.ToString("MMM") + "-" + DateTime.Now.Year.ToString();
        //ddlFMonth.SelectedValue = DateTime.Now.Month.ToString();
        //ddlTMonth.SelectedValue = DateTime.Now.Month.ToString();
    }
    //private void FillManagers()
    //{
    //    SalesForce sf = new SalesForce();

    //    //if (ddlFFType.SelectedValue.ToString() == "1")
    //    //{
    //    //    ddlAlpha.Visible = false;
    //    // dsSalesForce = sf.UserList_Hierarchy(div_code, "admin");

    //    dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
    //    //}
    //    //else if (ddlFFType.SelectedValue.ToString() == "0")
    //    //{
    //    //    FillSF_Alpha();
    //    //    ddlAlpha.Visible = true;
    //    //    dsSalesForce = sf.UserList_Alpha(div_code, "admin");
    //    //}

    //    if (dsSalesForce.Tables[0].Rows.Count > 0)
    //    {
    //        ddlFieldForce.DataTextField = "sf_name";
    //        ddlFieldForce.DataValueField = "sf_code";
    //        ddlFieldForce.DataSource = dsSalesForce;
    //        ddlFieldForce.DataBind();

    //        ddlSF.DataTextField = "Desig_Color";
    //        ddlSF.DataValueField = "sf_code";
    //        ddlSF.DataSource = dsSalesForce;
    //        ddlSF.DataBind();

    //    }
    //}

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

                if (ddlmode.SelectedValue == "0")
                {


                    command.CommandText = "exec sample_At_A_Glance_OB_Reset '" + div_code + "','" + ddlFYear.SelectedValue + "','" + ddlTYear.SelectedValue + "'";

                    command.CommandTimeout = 6000;
                    command.ExecuteNonQuery();
                }
                else
                {


                    command.CommandText = "exec Input_At_A_Glance_OB_Reset '" + div_code + "','" + ddlFYear.SelectedValue + "','" + ddlTYear.SelectedValue + "'";

                    command.CommandTimeout = 6000;
                    command.ExecuteNonQuery();
                }




            }
            catch (Exception ex)
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

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');window.location='Sample_Input_CarryForward.aspx'</script>");
            }
            transaction.Commit();
            connection.Close();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Processed Sucessfully!');window.location='Sample_Input_CarryForward.aspx'</script>");

        }
    }

    protected void btnclick_Click(object sender, EventArgs e)
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

                if (ddlmode.SelectedValue == "0")
                {
                    command.CommandText = "update Setup_Others set Sample_Carry_Forward='" + RadioSample.SelectedValue + "',Input_Carry_Forward='" + RadioInput.SelectedValue + "' where Division_Code='" + div_code + "'";

                    command.ExecuteNonQuery();
                }
                else
                {
                    command.CommandText = "update Setup_Others set Sample_Carry_Forward='" + RadioSample.SelectedValue + "',Input_Carry_Forward='" + RadioInput.SelectedValue + "' where Division_Code='" + div_code + "'";
                    command.ExecuteNonQuery();
                }




            }
            catch (Exception ex)
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

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');window.location='Sample_Input_CarryForward.aspx'</script>");
            }
            transaction.Commit();
            connection.Close();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Processed Sucessfully!');window.location='Sample_Input_CarryForward.aspx'</script>");

        }
    }
}
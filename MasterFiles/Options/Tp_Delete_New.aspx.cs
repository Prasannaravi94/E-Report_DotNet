using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;
using System.Data.SqlClient;

public partial class MasterFiles_Options_Tp_Delete_New : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    DataTable dsTP = null;
    string sfCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {

            //FillColor();
            menu1.Title = this.Page.Title;
          //  menu1.FindControl("btnBack").Visible = false;
            
            FillManagers();
        }

        FillColor();

    }

    private void FillTourPlan()
    {
       
            TourPlan tp = new TourPlan();
            
            dsTP = tp.show_tour_plan(ddlFieldForce.SelectedValue.ToString(), div_code);
            DB_EReporting db = new DB_EReporting();
        if (dsTP.Rows.Count > 0)
            {
                grdTP.Visible = true;
                grdTP.DataSource = dsTP;
                grdTP.DataBind();
            btnSubmit.Visible = true;

            //string qry = "select designation_short_name,Tp_Start_Date, Tp_End_Date, day(getdate()) crnt from Mas_SF_Designation " +
            //             " where Designation_Short_Name =(select distinct sf_Designation_Short_Name " +
            //              " from mas_salesforce where sf_code = '" + ddlFieldForce.SelectedValue.ToString() + "') and Division_Code = '" + div_code + "' ";
            //DataTable des = db.Exec_DataTable(qry);
            //if (des.Rows.Count != 0)
            //{
            //    if (des.Rows[0]["Tp_Start_Date"].ToString() != null && des.Rows[0]["crnt"].ToString() != null)
            //    {
            //        int st = Convert.ToInt16(des.Rows[0]["Tp_Start_Date"].ToString());
            //        //int ed = Convert.ToInt16(des.Rows[0]["Tp_End_Date"].ToString());
            //        int ct = Convert.ToInt16(des.Rows[0]["crnt"].ToString());


            //        if (ct < st)
            //        {
            //            btnSubmit.Visible = true;

            //        }
            //        else
            //        {
            //            btnSubmit.Visible = false;
            //            Response.Write("<script>alert('Tour Plan cannot be deleted since Current date is out of range...');</script>");

            //        }
            //    }
            //}
        }
            else
            {
                grdTP.DataSource = null;
                grdTP.DataBind();

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No Records Found');</script>");
                btnSubmit.Visible = false;
            }
     
    }


    protected void grdTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSFCode = (Label)e.Row.FindControl("lblSFCode");
            Label lblFutureTP = (Label)e.Row.FindControl("lblFutureTP");
            CheckBox chkTP = (CheckBox)e.Row.FindControl("chkTP");
            CheckBox chkSNo = (CheckBox)e.Row.FindControl("chkSNo");
            HiddenField min_tour_date = (HiddenField)e.Row.FindControl("min_tour_date");

            
            
            string dt = dsTP.Rows[0]["min_tour_date"].ToString();
            min_tour_date.Value = dsTP.Rows[0]["min_tour_date"].ToString();
         
        }
    }

    private string getmonthname(int iMonth)
    {
        string sReturn = string.Empty;


        if (iMonth == 1)
        {
            sReturn = "Jan";
        }
        else if (iMonth == 2)
        {
            sReturn = "Feb";
        }
        else if (iMonth == 3)
        {
            sReturn = "Mar";
        }
        else if (iMonth == 4)
        {
            sReturn = "Apr";
        }
        else if (iMonth == 5)
        {
            sReturn = "May";
        }
        else if (iMonth == 6)
        {
            sReturn = "Jun";
        }
        else if (iMonth == 7)
        {
            sReturn = "Jul";
        }
        else if (iMonth == 8)
        {
            sReturn = "Aug";
        }
        else if (iMonth == 9)
        {
            sReturn = "Sep";
        }
        else if (iMonth == 10)
        {
            sReturn = "Oct";
        }
        else if (iMonth == 11)
        {
            sReturn = "Nov";
        }
        else if (iMonth == 12)
        {
            sReturn = "Dec";
        }

        return sReturn;
    }

    private void FillColor()
    {
        int j = 0;


        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }


    private void FillManagers()
    {
        SalesForce sf = new SalesForce();


        dsSalesForce = sf.sp_Tp_fielforce(div_code);


        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillTourPlan();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
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
                SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                SqlCommand cm = new SqlCommand();
                foreach (GridViewRow gridRow in grdTP.Rows)
                {
                    CheckBox chkSNo = (CheckBox)gridRow.Cells[0].FindControl("chkSNo");
                    if (chkSNo.Checked)
                    {
                    Label lblSFCode = (Label)gridRow.Cells[1].FindControl("lblSFCode");
                    Label lblSFName = (Label)gridRow.Cells[2].FindControl("lblSFName");

                    Label lblTour_Month = (Label)gridRow.Cells[3].FindControl("lblTour_Month");
                    Label lblTour_Year = (Label)gridRow.Cells[4].FindControl("lblTour_Year");
                    Label lblFutureTP = (Label)gridRow.Cells[5].FindControl("lblFutureTP");
                    HiddenField min_tour_date = (HiddenField)gridRow.Cells[6].FindControl("min_tour_date");

                    Product prod = new Product();
                    
                    Int32 count1 = 0;
                   

                    command.CommandText = "select count( sf_code) cnt from trans_tp where division_code ='"+ div_code +"' and sf_code='" + lblSFCode.Text + "' ";
                    count1 = (Int32)command.ExecuteScalar();

                   
                    if (count1 != 0)
                    {
                        command.CommandText = "update Mas_Salesforce_DCRTPdate set Last_tp_date = '" + min_tour_date.Value + "' where sf_code= '" + lblSFCode.Text + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "update Mas_Salesforce set Last_tp_date = '" + min_tour_date.Value + "' where sf_code= '" + lblSFCode.Text + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "delete from Tourplan_detail where Div= '" + div_code + "' and SFCode= '" + lblSFCode.Text + "' and Yr= '" + lblTour_Year.Text + "' and Mnth='" + lblTour_Month.Text + "'";
                        command.ExecuteNonQuery();
                        command.CommandText = "delete from trans_tp where division_code= '" + div_code + "' and sf_code= '" + lblSFCode.Text + "' and tour_year= '" + lblTour_Year.Text + "' and Tour_Month='" + lblTour_Month.Text + "'";
                        command.ExecuteNonQuery();
                    }
                    }

                }
                transaction.Commit();

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
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");
            }
            connection.Close();
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Tour Plan Permanently Deleted!!');</script>");
            grdTP.Visible = false;
            btnSubmit.Visible = false;
        }      
    }
   

}
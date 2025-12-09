using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_Options_DCR_Delete_New : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sfCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            TourPlan tp = new TourPlan();
            DataSet dsTP = new DataSet();


            FillSalesForce();
            menu1.Title = this.Page.Title;
           // menu1.FindControl("btnBack").Visible = false;
        }

    }
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        DataSet dsSalesForce = new DataSet();
        dsSalesForce = sf.getSalesForcelist_New(div_code);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
        }
    }
    protected void btnSub_Click(object sender, System.EventArgs e)
    {
        DCR dcr = new DCR();
        int iReturn = -1;
        int iReturn_leave = -1;
        DB_EReporting db = new DB_EReporting();
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = string.Empty;



      DataSet dsleave = dcr.DcrDele_Leave_Check(ddlFieldForce.SelectedValue,txtFromdte.Text);

      if (dsleave.Tables[0].Rows[0]["count"].ToString() == "0" && dsleave.Tables[0].Rows.Count>0)
        {


            iReturn = dcr.DcrDele_New(ddlFieldForce.SelectedValue, txtFromdte.Text);

            //for (DateTime date = Convert.ToDateTime(txtFromdte.Text); date.Date <= Convert.ToDateTime(txtTodte.Text); date = date.AddDays(1))
            //{

            if (iReturn > 0)
            {
                DateTime dd = Convert.ToDateTime(txtFromdte.Text);


                strQry = "Insert into DCR_MissedDates (Sf_Code,Month,Year,Dcr_Missed_Date,Status,Missed_Created_Date,Missed_Release_Date,Finished_Date,Released_by_Whom,Division_Code) values " +
                         " ('" + ddlFieldForce.SelectedValue + "','" + dd.Month + "','" + dd.Year + "','" + dd.ToString("MM/dd/yyyy") + "',1,getdate(),getdate(),null,'admin','" + div_code + "' )";

                iReturn = db.ExecQry(strQry);
            }
           // }


            if (iReturn > 0)
            {
                //Response.Write("DCR Edit Dates have been created successfully");
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('DCR Date Deleted successfully');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record not Exist');</script>");
            }

        }
      else if (dsleave.Tables[0].Rows[0]["count"].ToString() != "0" && dsleave.Tables[0].Rows.Count > 0)
      {
          ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Leave Exist in this Date');</script>");

      }
      else
      {
          ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Record not Exist');</script>");
      }
    }
}
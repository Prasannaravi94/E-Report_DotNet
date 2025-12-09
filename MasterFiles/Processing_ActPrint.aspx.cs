using Bus_EReport;
using DBase_EReport;
using System;
using System.Data;
using System.Web.UI.WebControls;

public partial class MasterFiles_Processing_ActPrint : System.Web.UI.Page
{
    DataSet dsActivityUpload = null;
    string Activity_ID = string.Empty;
    string Sf_Code = string.Empty;
    string Date_Activity_Approval = string.Empty;
    string div_code = string.Empty;

    string Activity_Approved_Bill_Amount = string.Empty;
    string Month_Activity = string.Empty;
    string Year_Activity = string.Empty;
    string Activity_Description = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        Activity_ID = Request.QueryString["Activity_ID"];
        Sf_Code = Request.QueryString["Sf_Code"];
        Date_Activity_Approval = Request.QueryString["Date_Activity_Approval"];

        Activity_Approved_Bill_Amount = Request.QueryString["Activity_Approved_Bill_Amount"];
        Month_Activity = Request.QueryString["Month_Activity"];
        Year_Activity = Request.QueryString["Year_Activity"];
        Activity_Description = Request.QueryString["Year_Activity"];
        
        div_code = Session["div_code"].ToString();
        if (!this.IsPostBack)
        {

            FillPrint();
        }
    }
    private void FillPrint()
    {
        SubDivision dv = new SubDivision();
        //dsActivityUpload = dv.getPrintTransActivityUpload(div_code, Sf_Code, Activity_ID, Date_Activity_Approval);
        DataSet dsActUpload = null;
        DB_EReporting db_ER = new DB_EReporting();
        string strQry = " EXEC Sp_getPrintTransActivityUpload '" + div_code + "','" + Sf_Code + "','" + Activity_ID + "','" + Date_Activity_Approval + "','"+ Activity_Approved_Bill_Amount.Trim() + "','"+ Month_Activity + "','"+ Year_Activity + "' ";

        dsActivityUpload = db_ER.Exec_DataSet(strQry);
        
        DataTable dtActPrint = new DataTable();
  
        if (dsActivityUpload.Tables[0].Rows.Count > 0)
        {
            RptActiviy.DataSource = dsActivityUpload.Tables[0];
            RptActiviy.DataBind();
            lblEmpID.Text = "Emp.No: " + dsActivityUpload.Tables[0].Rows[0]["sf_emp_id"].ToString();
        }
        else
        {
         
            RptActiviy.DataSource = dtActPrint;
            RptActiviy.DataBind();
        }

        int RowLimits = 0;
        if (dsActivityUpload.Tables[1].Rows.Count > 0)
        {
            dtActPrint = dsActivityUpload.Tables[1].Copy();
            RowLimits= 110 - dtActPrint.Rows.Count;
            if (RowLimits > 0)
            {
                for (int i = 0; i < RowLimits; i++)
                { dtActPrint.Rows.Add(null, null, ""); }
            }
            GrdSubActiviy.DataSource = dtActPrint;
            GrdSubActiviy.DataBind();
        }
        else
        {
            RowLimits = 110;
            dtActPrint.Columns.Add("Activity_Addition_Deletion", typeof(int));
            dtActPrint.Columns.Add("Activity_Dateon_Which_Incurred", typeof(string));
            dtActPrint.Columns.Add("Activity_Reason", typeof(string));
            for (int i = 0; i < RowLimits; i++)
            { dtActPrint.Rows.Add(null, "", ""); }
            GrdSubActiviy.DataSource = dtActPrint;
            GrdSubActiviy.DataBind();
        }
    }

    protected void RptActiviy_OnItemCommand(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ////Reference the Repeater Item.
            //RepeaterItem item = e.Item;

            ////Reference the Controls.
            //string lblDateOfApproval = (item.FindControl("lblDateOfApproval") as Label).Text;
            //string lblNameDesignHQ = (item.FindControl("lblNameDesignHQ") as Label).Text;
            //string lblDivisionName = (item.FindControl("lblDivisionName") as Label).Text;
        }
    }
    protected void GrdSubActiviy_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        //foreach (TableCell tc in e.Row.Cells)
        //{
        //    tc.Attributes["style"] = "border-right:3px solid #000000; border-bottom:none";
        //}
    }
}
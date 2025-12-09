using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;

public partial class MasterFiles_Reports_rptDocList_Consolidate : System.Web.UI.Page
{
    string div_code = string.Empty;
    string sf_code = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            DataTable dtUserList = new DataTable();
            SalesForce sf = new SalesForce();

            sf_code=Request.QueryString["sf_code"].ToString();
            div_code=Request.QueryString["div_Code"].ToString();

            dtUserList = sf.getUserListReportingToNew(div_code, sf_code, 0, Session["sf_type"].ToString());
            if (dtUserList.Rows.Count > 0)
            {
                grdSalesForce.DataSource = dtUserList;
                grdSalesForce.DataBind();
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void grdSalesForce_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataSet dsCount = new DataSet();
            ListedDR sf = new ListedDR();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink lblDrsCnt = (HyperLink)e.Row.FindControl("HyperLink1");
                Label lblSF_Code = (Label)e.Row.FindControl("lblSF_Code");
                Label lblDivCode = (Label)e.Row.FindControl("lblDivCode");
                ListedDR lstdr = new ListedDR();
                DataSet dsdr = new DataSet();
                lblDivCode.Text = div_code;
                if (lblSF_Code.Text.Contains("MR"))
                {
                    dsdr = lstdr.getListDr_CountNew(lblSF_Code.Text, div_code);
                }
                   
                if (dsdr.Tables[0].Rows.Count > 0)
                {
                    lblDrsCnt.Text = dsdr.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }

                if (lblSF_Code.Text.Contains("MGR"))
                {
                    dsCount = sf.getListDr_CountNewMGR(lblSF_Code.Text, div_code);
                    lblDrsCnt.Text = dsCount.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                
            }
        }
        catch (Exception ex)
        {

        }
    }
}
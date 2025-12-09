using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Drawing.Imaging;
using DBase_EReport;

using System.Data.SqlClient;

public partial class MasterFiles_MR_SSale_SS_Edit : System.Web.UI.Page
{
    #region "Variable Declarations"
    DataSet dsYear = null;
    string sf_code = string.Empty;
    string div_code = string.Empty;
    DataSet dsState = new DataSet();
    DataSet dsSecSale = new DataSet();
    DataSet dsSalesforce = new DataSet();
    DataSet dssec = new DataSet();
    DataSet dsBifur = new DataSet();
    int iErrReturn = -1;
    string Str_SlNo = string.Empty;
    string Str_Trans = string.Empty;
    DataTable dtSlNo = new DataTable();
    DataSet dsBase = new DataSet();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        //Get the sf_code & div_code from session
        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();

        if (!Page.IsPostBack) // Only on first time page load
        {
            //Populate Year dropdown
            FillYear();

            //Populate MR dropdown as per sf_code
            FillMR();

            menu1.Title = this.Page.Title;
            menu1.FindControl("btnBack").Visible = false;
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        FillStockiest();
    }

    //Populate the Year dropdown
    private void FillYear()
    {
        try
        {
            TourPlan tp = new TourPlan();
            dsYear = tp.Get_TP_Edit_Year(div_code); // Get the Year for the Division
            if (dsYear.Tables[0].Rows.Count > 0)
            {
                for (int k = Convert.ToInt16(dsYear.Tables[0].Rows[0]["Year"]); k <= DateTime.Now.Year + 1; k++)
                {
                    ddlYear.Items.Add(k.ToString());
                    ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                }
            }
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Report", "FillYear()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists. Please contact customer care for support');</script>");
        }
    }

    private void FillMR()
    {
        SalesForce sf = new SalesForce();
        dsSalesforce = sf.SalesForceList_New_GetMr(div_code, sf_code);

        if (dsSalesforce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataValueField = "SF_Code";
            ddlFieldForce.DataTextField = "Sf_Name";
            ddlFieldForce.DataSource = dsSalesforce;
            ddlFieldForce.DataBind();
            ddlFieldForce.Items.Insert(0, new ListItem("--Select--", "-1"));

        }
    }

    private void FillStockiest()
    {
        ddlmode.Enabled = false;
        ddlFieldForce.Enabled = false;
        ddlMonth.Enabled = false;
        ddlYear.Enabled = false;
        txtNew.Enabled = false;
        if (ddlmode.SelectedValue == "1")
        {
            
            SecSale ss = new SecSale();
            dsSecSale = ss.Get_SS_Edit(div_code, ddlFieldForce.SelectedValue.ToString().Trim(), Convert.ToInt16(ddlMonth.SelectedValue.ToString()), Convert.ToInt16(ddlYear.SelectedValue.ToString()));
            if (dsSecSale != null)
            {
                if (dsSecSale.Tables[0].Rows.Count > 0)
                {
                    grdSecSales.Visible = true;
                    grdSecSales.DataSource = dsSecSale;
                    grdSecSales.DataBind();

                    btnSubmit.Visible = true;
                }
                else
                {
                    grdSecSales.DataSource = null;
                    grdSecSales.DataBind();

                    btnSubmit.Visible = false;
                }
            }
            else
            {
                grdSecSales.DataSource = null;
                grdSecSales.DataBind();

                btnSubmit.Visible = false;
            }
        }
       
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int iReturn = -1;
            foreach (GridViewRow gridRow in grdSecSales.Rows)
            {
                Label lblStockiestCode = (Label)gridRow.Cells[1].FindControl("lblStockiestCode");
                CheckBox chkSaleEntry = (CheckBox)gridRow.Cells[3].FindControl("chkSaleEntry");
                if (chkSaleEntry.Checked)
                {
                    if (ddlmode.SelectedValue == "1")
                    {
                        SecSale ss = new SecSale();
                        dssec = ss.Get_Edit_SlNo(div_code, ddlFieldForce.SelectedValue, lblStockiestCode.Text.Trim(), Convert.ToInt32(ddlMonth.SelectedValue), Convert.ToInt32(ddlYear.SelectedValue));
                        if (dssec.Tables[0].Rows.Count > 0)
                        {
                            //foreach (DataRow dr in dssec.Tables[0].Rows)
                            //{
                            //    string SlNo = dr["Sl_No"].ToString();

                            //    Str_SlNo += SlNo + ",";
                            //}
                            //Str_SlNo = Str_SlNo.Remove(Str_SlNo.Length - 1);
                            dtSlNo = dssec.Tables[0].Copy();
                        }
                        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
                        SqlConnection conn = new SqlConnection(strConn);
                       
                       // iReturn = ss.SS_Delete(div_code, ddlFieldForce.SelectedValue.ToString().Trim(), Convert.ToInt32(lblStockiestCode.Text.Trim()), Convert.ToInt32(ddlMonth.SelectedValue.ToString().Trim()), Convert.ToInt32(ddlYear.SelectedValue.ToString().Trim()), dtSlNo, Session["sf_code"].ToString().Trim(), ddlmode.SelectedItem.Text.Trim());
                        conn.Open();
                        // SqlCommand cmd = new SqlCommand("SP_BulkInsert_TransDel_TransVal", conn);
                        SqlCommand cmd = new SqlCommand("SS_Entry_Delete", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Div_Code", div_code);
                        cmd.Parameters.AddWithValue("@sf_code", ddlFieldForce.SelectedValue.ToString().Trim());
                        cmd.Parameters.AddWithValue("@stock_code", Convert.ToInt32(lblStockiestCode.Text.Trim()));
                        cmd.Parameters.AddWithValue("@imonth", Convert.ToInt32(ddlMonth.SelectedValue.ToString().Trim()));
                        cmd.Parameters.AddWithValue("@iyear", Convert.ToInt32(ddlYear.SelectedValue.ToString().Trim()));
                        cmd.Parameters.AddWithValue("@dtSlNo", dtSlNo);
                        cmd.Parameters.AddWithValue("@Approved_By", Session["sf_code"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@Mode", ddlmode.SelectedItem.Text.Trim());
                      
                        cmd.Parameters.Add("@retValue", SqlDbType.Int);
                        cmd.Parameters["@retValue"].Direction = ParameterDirection.Output;
                        cmd.ExecuteNonQuery();
                        iReturn = Convert.ToInt32(cmd.Parameters["@retValue"].Value.ToString());
                        conn.Close();
                        if (iReturn > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Edited successfully');</script>");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists');</script>");
                        }
                        FillStockiest();
                    }
                    
                }
            }

            //if (iReturn > 0)
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Edited successfully');</script>");
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error exists.');</script>");
            //}

           
        }
        catch (Exception ex)
        {
            ErrorLog err = new ErrorLog();
            iErrReturn = err.LogError(Convert.ToInt32(div_code), ex.Message.ToString().Trim(), "Sec Sales Option Edit", "btnSubmit_Click()");
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unknown error exists.');</script>");
        }

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ddlmode.Enabled = true;
        ddlFieldForce.Enabled = true;
        ddlMonth.Enabled = true;
        ddlYear.Enabled = true;
        txtNew.Enabled = true;
        lblFMonth.Visible = true;
        ddlMonth.Visible = true;
        ddlFieldForce.SelectedValue = "-1";
        ddlmode.SelectedValue = "0";
        grdSecSales.Visible = false;
        btnSubmit.Visible = false;
        txtNew.Text = "";
    }
    protected void ddlmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlmode.SelectedValue == "3")
        {
            lblFMonth.Visible = false;
            ddlMonth.Visible = false;
        }
        else
        {
            lblFMonth.Visible = true;
            ddlMonth.Visible = true;
        }
    }
}